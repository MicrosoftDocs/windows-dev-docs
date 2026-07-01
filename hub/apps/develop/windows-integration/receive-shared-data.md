---
title: Receive shared data in your Windows app
description: Learn how to implement the Share contract target side in a WinUI 3 or Windows App SDK app to receive and process content shared from other apps.
ms.topic: how-to
ms.date: 06/20/2026
ms.localizationpriority: medium
keywords: windows share, share target, share contract, shareoperation, winui 3, windows app sdk, receive data
author: GrantMeStrength
ms.author: jken
---

# Receive shared data in your Windows app

The Windows Share contract allows your app to appear in the Windows Share Sheet as a *target app*—letting users share content from other apps directly into yours. This article explains how to handle shared content once your app is activated as a share target.

Before following the steps here, register your app as a share target:

- **Packaged apps**: See [Integrate packaged apps with Windows Share](./integrate-sharesheet-packaged.md) for manifest registration and activation setup.
- **Unpackaged apps**: See [Integrate unpackaged apps with Windows Share](./integrate-sharesheet-unpackaged.md) for how to grant package identity and register as a share target.

> [!NOTE]
> The activation model for WinUI 3 desktop apps differs from UWP. In WinUI 3, share activation is handled via `AppInstance.GetActivatedEventArgs()` in your app startup code—not via `Application.OnShareTargetActivated()`. The `ShareOperation` and `DataPackageView` APIs described in this article work the same way once you obtain the `ShareTargetActivatedEventArgs`. See [Integrate packaged apps with Windows Share](./integrate-sharesheet-packaged.md#fetch-share-event-arguments) for the Windows App SDK activation pattern.

## Choose data formats to support

When you declare your app as a share target in its package manifest, you specify which file types and data formats your app can receive. Only apps that support the formats being shared appear in the Share Sheet.

You can configure supported types in two ways:

**Using the Visual Studio manifest editor:**

1. Open `package.appxmanifest` in Visual Studio.
2. Select the **Declarations** tab.
3. Choose **Share Target** from the **Available Declarations** list, then select **Add**.
4. Under **Supported File Types**, add the file extensions your app handles (for example, `.jpg`, `.png`). Select **SupportsAnyFileType** to accept all file types.
5. Under **Data Formats**, add the format names your app handles (for example, `Text`, `Uri`, `Bitmap`).

**Directly in the manifest XML:**

```xml
<Extensions>
  <uap:Extension Category="windows.shareTarget">
    <uap:ShareTarget>
      <uap:SupportedFileTypes>
        <uap:SupportsAnyFileType />
      </uap:SupportedFileTypes>
      <uap:DataFormat>Text</uap:DataFormat>
      <uap:DataFormat>Uri</uap:DataFormat>
      <uap:DataFormat>Bitmap</uap:DataFormat>
      <uap:DataFormat>StorageItems</uap:DataFormat>
    </uap:ShareTarget>
  </uap:Extension>
</Extensions>
```

Only register for formats that your app can handle. If you declare a format but can't process it, the user experience suffers.

## Read shared data

When your app is activated as a share target, you receive a [ShareOperation](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation) object via the `ShareTargetActivatedEventArgs`. Its `Data` property is a [DataPackageView](/uwp/api/windows.applicationmodel.datatransfer.datapackageview) that exposes the shared content.

Use `Contains` to check which formats are available, then call the appropriate async method to retrieve the data:

```csharp
ShareOperation shareOperation = args.ShareOperation;

if (shareOperation.Data.Contains(StandardDataFormats.Text))
{
    string text = await shareOperation.Data.GetTextAsync();
    // Process the shared text.
}

if (shareOperation.Data.Contains(StandardDataFormats.WebLink))
{
    Uri webLink = await shareOperation.Data.GetWebLinkAsync();
    // Process the shared link.
}

if (shareOperation.Data.Contains(StandardDataFormats.StorageItems))
{
    IReadOnlyList<IStorageItem> items = await shareOperation.Data.GetStorageItemsAsync();
    // Process the shared files or folders.
}
```

## Report sharing status

If processing the shared data takes time—for example, uploading files to a server—report progress to the system using the [ShareOperation](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation) status methods. This lets the system manage the source app's lifecycle appropriately.

Call these methods in order as your share operation progresses:

| Method | When to call |
|--------|-------------|
| [ReportStarted](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.reportstarted) | As soon as your app begins processing the share. After this point, don't expect further user interaction with the share UI. |
| [ReportDataRetrieved](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.reportdataretrieved) | After your app has obtained all the data it needs from the `DataPackageView`. This allows the system to suspend or terminate the source app. |
| [ReportSubmittedBackgroundTask](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.reportsubmittedbackgroundtask) | If your app continues processing in the background after the share UI is dismissed. |
| [ReportCompleted](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.reportcompleted) | When your app has successfully finished processing the shared content. |
| [ReportError](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.reporterror) | If a serious error occurs. The user sees the message and the share operation ends. |

```csharp
shareOperation.ReportStarted();

try
{
    string text = await shareOperation.Data.GetTextAsync();
    shareOperation.ReportDataRetrieved();

    // Perform any additional processing here...
    await ProcessSharedDataAsync(text);

    shareOperation.ReportCompleted();
}
catch (Exception)
{
    shareOperation.ReportError("Something went wrong. Please try again.");
}
```

> [!NOTE]
> Only call `ReportError` for errors serious enough to end the share operation. For recoverable errors, you can continue processing without calling `ReportError`.

> [!NOTE]
> There are cases where a target app can call `ReportDataRetrieved` before `ReportStarted`—for example, if your app retrieves data as part of activation handling, but only calls `ReportStarted` later when the user explicitly selects a **Share** button.

## Return a QuickLink

When a user shares content to your app, you can return a [QuickLink](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.quicklink) to make future sharing faster. A `QuickLink` appears as a shortcut in the Share Sheet—for example, a contact shortcut that lets the user quickly share with that contact again without navigating your app's UI.

A `QuickLink` has a title, an icon, and an ID. The ID is your app's internal identifier for the shortcut—such as a contact ID or account name. When the user later selects a `QuickLink`, the system activates your app and passes the `QuickLink` ID back via [ShareOperation.QuickLinkId](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.quicklinkid).

Return a `QuickLink` by passing it to [ReportCompleted](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation.reportcompleted):

```csharp
private async Task ReportCompletedWithQuickLink(
    ShareOperation shareOperation, string quickLinkId, string quickLinkTitle)
{
    QuickLink quickLinkInfo = new QuickLink
    {
        Id = quickLinkId,
        Title = quickLinkTitle,

        // QuickLink supported types are configured independently from the manifest.
        SupportedFileTypes = { "*" },
        SupportedDataFormats =
        {
            StandardDataFormats.Text,
            StandardDataFormats.WebLink,
            StandardDataFormats.Bitmap,
            StandardDataFormats.StorageItems
        }
    };

    StorageFile iconFile = await Windows.ApplicationModel.Package.Current
        .InstalledLocation.CreateFileAsync(
            "assets\\contact.png", CreationCollisionOption.OpenIfExists);
    quickLinkInfo.Thumbnail = RandomAccessStreamReference.CreateFromFile(iconFile);

    shareOperation.ReportCompleted(quickLinkInfo);
}
```

> [!NOTE]
> A `QuickLink` stores only the ID—not the associated data. Your app is responsible for persisting any user data (such as contact details) and retrieving it when the `QuickLink` is activated via `ShareOperation.QuickLinkId`.

## See also

- [Share content from your Windows app](./share-content.md)
- [Integrate packaged apps with Windows Share](./integrate-sharesheet-packaged.md)
- [Integrate unpackaged apps with Windows Share](./integrate-sharesheet-unpackaged.md)
- [Share Contract Implementation for Windows App SDK](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [ShareOperation](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation)
- [DataPackageView](/uwp/api/windows.applicationmodel.datatransfer.datapackageview)
- [QuickLink](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.quicklink)
- [ShareTarget schema reference](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-sharetarget)
