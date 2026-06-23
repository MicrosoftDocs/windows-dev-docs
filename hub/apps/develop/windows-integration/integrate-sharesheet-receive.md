---
description: Learn how to receive content from other Windows apps through the Windows Share Sheet - register as a share target and handle shared content.
title: "Receive content in your app - integrate Windows Share"
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 06/22/2026
ms.localizationpriority: medium
---

# Receive content in your app - integrate Windows Share

The Windows Share Sheet allows you to receive content (shared from other apps) through your app. This guide explains how to register your app as a **Share Target** and handle shared content across packaged apps (MSIX), Progressive Web Apps (PWAs), and unpackaged Win32 apps.

## In this article

| Section | What you'll find |
|--|--|
| [Before you declare capabilities](#before-you-declare-capabilities) | Declare only the file types and formats your app handles |
| [Implement Share Target for packaged apps (UWP and packaged desktop)](#implement-share-target-for-packaged-apps-uwp-and-packaged-desktop) | Manifest declaration and activation handling for UWP and packaged desktop apps |
| [Implement Share Target for PWAs](#implement-share-target-for-progressive-web-apps-pwas) | `share_target` manifest and POST handling |
| [Receive shares in an unpackaged Win32 app](#receive-shares-in-an-unpackaged-win32-app) | Grant package identity and register as a Share Target |
| [Best practices](#best-practices) | Recommendations for reliable receive flows |
| [Report receive progress](#report-receive-progress-optional-but-recommended) | Status reporting for large or long-running shares |
| [Troubleshooting](#troubleshooting) | Fixes for common Share Target problems |

## Before you declare capabilities

Most share-target integration bugs come from **declaring more than your app can actually handle**. If your app declares `<uap:SupportsAnyFileType />`, it will appear in the Share Sheet for *every* file type, including files it can't process (for example, a photo editor appearing when a user shares a spreadsheet).

**Always declare only the specific file types and data formats your app can handle.** For example:

```xml
<!-- ✓ Correct: declare only what you support -->
<uap:SupportedFileTypes>
  <uap:FileType>.jpg</uap:FileType>
  <uap:FileType>.png</uap:FileType>
</uap:SupportedFileTypes>

<!-- ✗ Incorrect: declares everything -->
<!-- <uap:SupportsAnyFileType /> -->
```

Reserve `<uap:SupportsAnyFileType />` for general-purpose file movers only (cloud storage, file transfer apps). See [DataFormat & FileType reference](dataformat-reference.md) for declarations by app category.

## Implement Share Target for packaged apps (UWP and packaged desktop)

This section applies to **UWP apps** and **packaged desktop apps** (WinUI 3, WPF, WinForms). Both ship as MSIX packages with package identity, so they declare the Share Target the same way and differ only in how they handle activation (shown in step 2).

### 1. Declare in the manifest

Edit your `package.appxmanifest` to register as a Share Target. Declare **only** the file types and data formats your app handles:

```xml
<Extensions>
  <uap:Extension Category="windows.shareTarget">
    <uap:ShareTarget>
      <uap:SupportedFileTypes>
        <uap:FileType>.jpg</uap:FileType>
        <uap:FileType>.jpeg</uap:FileType>
        <uap:FileType>.png</uap:FileType>
        <uap:FileType>.gif</uap:FileType>
        <uap:FileType>.bmp</uap:FileType>
      </uap:SupportedFileTypes>
      <uap:DataFormat>Bitmap</uap:DataFormat>
    </uap:ShareTarget>
  </uap:Extension>
</Extensions>
```

### 2. Handle the Share activation

When your app is activated as a Share Target, handle the `OnShareTargetActivated` event:

> [!NOTE]
> `OnShareTargetActivated` is the activation override for UWP apps (`Windows.UI.Xaml.Application`). Packaged desktop apps (WinUI 3, WPF, WinForms) receive share activation through `AppInstance.GetActivatedEventArgs` and check for `ExtendedActivationKind.ShareTarget`. See [Get activation info for packaged apps](/windows/apps/desktop/modernize/get-activation-info-for-packaged-apps).

```csharp
protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
{
    ShareOperation shareOperation = args.ShareOperation;
    shareOperation.ReportStarted();

    try
    {
        if (shareOperation.Data.Contains(StandardDataFormats.StorageItems))
        {
            IReadOnlyList<IStorageItem> items = await shareOperation.Data.GetStorageItemsAsync();
            
            // Validate: check count, types, and sizes
            if (items.Count == 0)
            {
                shareOperation.ReportError("No items received.");
                return;
            }

            var file = (IStorageFile)items[0];
            
            // Process the file
            await ProcessImageAsync(file);
        }
        
        shareOperation.ReportCompleted();
    }
    catch (Exception ex)
    {
        shareOperation.ReportError($"Error: {ex.Message}");
    }
}

private async Task ProcessImageAsync(IStorageFile file)
{
    // Your processing logic here
}
```

For packaged desktop apps (WinUI 3, WPF, WinForms) built with the Windows App SDK, there's no `OnShareTargetActivated` override. Instead, inspect activation in your `Main` method and check for `ExtendedActivationKind.ShareTarget`:

```csharp
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;

[STAThread]
static void Main(string[] args)
{
    AppActivationArguments activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
    if (activatedArgs.Kind == ExtendedActivationKind.ShareTarget)
    {
        HandleShareAsync(activatedArgs.Data as ShareTargetActivatedEventArgs);
    }
    else
    {
        // Normal launch path
    }
}

static async void HandleShareAsync(ShareTargetActivatedEventArgs args)
{
    ShareOperation shareOperation = args.ShareOperation;
    shareOperation.ReportStarted();

    if (shareOperation.Data.Contains(StandardDataFormats.StorageItems))
    {
        IReadOnlyList<IStorageItem> items = await shareOperation.Data.GetStorageItemsAsync();
        // Process the shared items.
    }

    shareOperation.ReportCompleted();
}
```

### 3. Choose what data formats to declare

Use this reference to decide what to declare:

| Format | When to use | Example apps |
|--|--|--|
| `StorageItems` | Your app receives files | Photo editors, document readers |
| `Bitmap` | Your app receives images | Image viewers, design apps |
| `Text` | Your app receives plain text | Notes apps, text editors |
| `Html` | Your app receives rich-text content | Email clients, rich editors |
| `Uri` / `WebLink` | Your app handles links | Browsers, link managers |
| `Rtf` | Your app receives formatted text | Word processors |

For more details, see [DataFormat & FileType reference](dataformat-reference.md).

## Implement Share Target for Progressive Web Apps (PWAs)

PWAs on Windows register as a Share Target through the Web App Manifest. Add a `share_target` entry:

```json
{
  "name": "My PWA",
  "short_name": "MyPWA",
  "share_target": {
    "action": "/share",
    "method": "POST",
    "enctype": "multipart/form-data",
    "params": {
      "title": "title",
      "text": "text",
      "url": "url",
      "files": [
        {
          "name": "media",
          "accept": ["image/*", "video/*"]
        }
      ]
    }
  }
}
```

In your `/share` route, handle the POST request:

```javascript
app.post('/share', async (req, res) => {
  const { title, text, url, files } = req.body;

  // Validate and process
  if (files && files.length > 0) {
    const file = files[0];
    // Process the file
    console.log('Received file:', file.originalname);
  }

  if (text) {
    console.log('Received text:', text);
  }

  res.redirect('/');
});
```

Only declare file types your PWA can handle. For example, don't declare `*` as an accept type unless your app truly handles all files.

## Receive shares in an unpackaged Win32 app

To register as a Share Target, your app needs [package identity](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps). If your Win32 app is unpackaged, grant it package identity in one of two ways:

- **Repackage with MSIX** (preferred): use the **Windows Application Packaging Project** template in Visual Studio for a clean, trusted install. See [Set up your desktop application for MSIX packaging](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net).
- **Package with external location** (sparse package): add an empty MSIX package that carries identity, the share target registration, and visual assets, while your existing installer keeps managing the app binaries. Use this only when you have an installer you can't move to MSIX.

The rest of this section walks through the external-location approach.

### 1. Author the package manifest

Create an `AppxManifest.xml` that sets `<uap10:AllowExternalContent>`, declares identity and capabilities, and registers the share target. Keep `Publisher`, `PackageName`, and `ApplicationId` in sync with your `.exe.manifest` and signing certificate.

```xml
<Identity Name="PhotoStoreDemo" ProcessorArchitecture="neutral" Publisher="CN=YourPubNameHere" Version="1.0.0.0" />
<Properties>
  <uap10:AllowExternalContent>true</uap10:AllowExternalContent>
</Properties>
<Dependencies>
  <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.19041.0" MaxVersionTested="10.0.19041.0" />
</Dependencies>
<Capabilities>
  <rescap:Capability Name="runFullTrust" />
  <rescap:Capability Name="unvirtualizedResources" />
</Capabilities>
<Applications>
  <Application Id="PhotoStoreDemo" Executable="PhotoStoreDemo.exe" uap10:TrustLevel="mediumIL" uap10:RuntimeBehavior="win32App">
    <Extensions>
      <uap:Extension Category="windows.shareTarget">
        <uap:ShareTarget Description="Send to PhotoStoreDemo">
          <uap:SupportedFileTypes>
            <uap:FileType>.jpg</uap:FileType>
            <uap:FileType>.png</uap:FileType>
          </uap:SupportedFileTypes>
          <uap:DataFormat>StorageItems</uap:DataFormat>
          <uap:DataFormat>Bitmap</uap:DataFormat>
        </uap:ShareTarget>
      </uap:Extension>
    </Extensions>
  </Application>
</Applications>
```

Add an application manifest (`YourApp.exe.manifest`) that links the executable to the package identity:

```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="1.0.0.0" name="PhotoStoreDemo.app" />
  <msix xmlns="urn:schemas-microsoft-com:msix.v1"
        publisher="CN=YourPubNameHere"
        packageName="PhotoStoreDemo"
        applicationId="PhotoStoreDemo" />
</assembly>
```

### 2. Create and sign the package

Use `MakeAppx.exe` with the `/nv` switch to build a package that contains only the manifest, then sign it with a trusted certificate using `SignTool.exe`:

```console
MakeAppx.exe pack /d <folder with AppxManifest.xml> /p <output>\mypackage.msix /nv
SignTool.exe sign /fd SHA256 /a /f <path to cert> /p <cert key> <path to package>
```

Install the signing certificate to a trusted location on the machine.

### 3. Register the package on first run

On first run, register the external-location package so the app restarts with identity. Provide absolute paths to the external location and the signed `.msix`.

```csharp
[STAThread]
public static void Main(string[] cmdArgs)
{
    if (!ExecutionMode.IsRunningWithIdentity())
    {
        string externalLocation = Environment.CurrentDirectory;
        string externalPkgPath = externalLocation + @"\PhotoStoreDemo.package.msix";

        if (registerPackageWithExternalLocation(externalLocation, externalPkgPath))
        {
            // Registration succeeded - restart so the app runs with identity.
            // Join the arguments into a single string; cmdArgs.ToString() would
            // return the array type name ("System.String[]"), not the arguments.
            string forwardedArgs = cmdArgs is null ? string.Empty : string.Join(" ", cmdArgs);
            Process.Start(Application.ResourceAssembly.Location, arguments: forwardedArgs);
        }
        else
        {
            // Registration failed - run without identity.
            new SingleInstanceManager().Run(cmdArgs);
        }
    }
}
```

### 4. Handle share activation

After the app restarts with identity, handle `ExtendedActivationKind.ShareTarget` as shown in [Handle the Share activation](#2-handle-the-share-activation).

For complete examples, see the [PhotoStoreDemo sample (packaged with external location)](https://github.com/microsoft/AppModelSamples/tree/master/Samples/PackageWithExternalLocation/cppwinrt/PackageWithExternalLocationCppApp) and the [WinUI Share Target sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp).

For source-side desktop sharing, use `IDataTransferManagerInterop` as described in [Share content from your app](integrate-sharesheet-send.md).

## Best practices

Use this checklist when building receive flows.

| Recommended | Avoid | Why it matters |
|--|--|--|
| Declare only specific file extensions and data formats | Declaring `<uap:SupportsAnyFileType />` for non-file-mover apps | Prevents irrelevant target appearances in Share Sheet |
| Validate format, count, file type, and file size before processing | Assuming incoming data always matches expectations | Prevents runtime failures and broken share experiences |
| Declare `Uri` for link handlers and `Bitmap` + `StorageItems` for image handlers | Partial declarations for common share payloads | Ensures your app appears for the content it actually supports |
| Use `ReportStarted`, `ReportDataRetrieved`, and `ReportCompleted` in long-running receive flows | Performing long-running receive work without progress reporting | Keeps share operation reliable and gives the system correct state |

## Report receive progress (optional but recommended)

For large payloads or longer processing, report status from the share target:

```csharp
protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
{
  ShareOperation shareOperation = args.ShareOperation;
  shareOperation.ReportStarted();

  try
  {
    // Acquire the data your app needs.
    var items = await shareOperation.Data.GetStorageItemsAsync();
    shareOperation.ReportDataRetrieved();

    // Process data.
    await ProcessAsync(items);

    shareOperation.ReportCompleted();
  }
  catch (Exception ex)
  {
    shareOperation.ReportError($"Share failed: {ex.Message}");
  }
}
```

Use `ReportCompleted(QuickLink)` when you want to return a QuickLink for future shares.

## Troubleshooting

**My app doesn't appear in the Share Sheet:**
- Verify your manifest declarations match the content being shared (check file types and data formats).
- For packaged apps, ensure you're running the app with package identity.
- Check [DataFormat & FileType reference](dataformat-reference.md) for your app category.

**My app appears for content it can't handle:**
- Narrow your `SupportedFileTypes` and `DataFormat` lists to only what you support.

**The Share Sheet dismisses with an error:**
- Ensure you call `ReportStarted()` before any async work and `ReportCompleted()` when done.
- Handle exceptions and call `ReportError()` with a descriptive message.

**I'm not receiving the file I expect:**
- Check that the file format matches a declared `FileType` or `DataFormat`.
- Add validation logic in your activation handler to inspect what's actually arriving.

## Related content

- [Share content from your app](integrate-sharesheet-send.md)
- [DataFormat & FileType reference](dataformat-reference.md)
- [People on Windows (Cross-device People API)](cross-device-people-api.md)
- [ShareTarget schema reference](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-sharetarget)
- [Web Share API (W3C)](https://www.w3.org/TR/web-share-target/)
