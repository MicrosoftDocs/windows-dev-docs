---
title: Share content from your Windows app
description: Learn how to use the Windows Share contract to share text, links, images, and files from packaged Windows apps, including UWP and WinUI 3 desktop apps.
ms.topic: how-to
ms.date: 06/25/2026
ms.localizationpriority: medium
keywords: windows share, share contract, datatransfermanager, datapackage, winui 3, windows app sdk
author: GrantMeStrength
ms.author: jken
---

# Share content from your Windows app

The Windows Share contract lets users quickly share content—such as text, links, photos, and videos—from your app to other apps. For example, a user might want to share a webpage with their friends using a social networking app, or save a link in a notes app to refer to later.

This article explains how to implement the *source* side of the Share contract: the code that prepares content and opens the Windows Share Sheet from your app. For information on receiving shared content, see [Receive shared data in your Windows app](./receive-shared-data.md).

The `Windows.ApplicationModel.DataTransfer` APIs used in this article work for apps that have package identity, including UWP apps and packaged WinUI 3 desktop apps. Unpackaged Win32 apps can participate by granting package identity and then using `IDataTransferManagerInterop`.

> [!NOTE]
> WinUI 3 desktop apps don't have a `CoreWindow`, so you can't call `DataTransferManager.GetForCurrentView()` or `DataTransferManager.ShowShareUI()` directly. Instead, use the [IDataTransferManagerInterop](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop) COM interop interface to associate the `DataTransferManager` with your window handle and to show the share UI. For code examples, see [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects#for-classes-that-implement-idatatransfermanagerinterop). You can also refer to the [WPF Sharing content source app sample](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/ShareSource) for a complete implementation.

## Set up an event handler

Register a [DataRequested](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.datarequested) event handler on your `DataTransferManager` instance. This event fires when the user invokes Share—for example, when they tap a Share button in your app, or when your app determines there is content ready to share.

For WinUI 3 desktop apps, obtain the `DataTransferManager` for your window using `IDataTransferManagerInterop`, as described in [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects#for-classes-that-implement-idatatransfermanagerinterop).

## Provide content in the DataRequested handler

When a [DataRequested](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.datarequested) event occurs, your app receives a [DataRequest](/uwp/api/windows.applicationmodel.datatransfer.datarequest) object containing a [DataPackage](/uwp/api/windows.applicationmodel.datatransfer.datapackage) that you populate with the content to share. You must set a title and at least one data format. A description is optional but recommended.

```csharp
private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
{
    DataRequest request = args.Request;
    request.Data.Properties.Title = "Contoso article";
    request.Data.Properties.Description = "An interesting article from Contoso.";
    request.Data.SetText("Read this article at Contoso.com.");
    request.Data.SetWebLink(new Uri("https://contoso.com/articles/1234"));
}
```

> [!WARNING]
> The `Title` property is required. If you don't set a title, the share operation fails.

## Choose data formats

[DataPackage](/uwp/api/windows.applicationmodel.datatransfer.datapackage) supports multiple built-in data formats. You can provide more than one format in the same package—the receiving app uses whichever format it supports best.

| Format | Method |
|--------|--------|
| Plain text | `SetText` |
| Web links | `SetWebLink` |
| Application links | `SetApplicationLink` |
| HTML | `SetHtmlFormat` |
| Rich text (RTF) | `SetRtf` |
| Bitmap images | `SetBitmap` |
| Files and folders | `SetStorageItems` |
| Custom developer-defined data | `SetData` |

## Set properties

[DataPackagePropertySet](/uwp/api/windows.applicationmodel.datatransfer.datapackagepropertyset) lets you attach additional metadata to the shared content. Adding a thumbnail gives the share sheet a richer visual preview. For more information, see [DataPackagePropertySet](/uwp/api/windows.applicationmodel.datatransfer.datapackagepropertyset).

```csharp
private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
{
    DataRequest request = args.Request; // The DataRequested event provides the request via args.Request.
    request.Data.Properties.Title = "Sunset photo";
    request.Data.Properties.Description = "Taken at Lake Tahoe.";
    // Providing a thumbnail is optional but improves the share sheet preview.
    // request.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(thumbnailFile);
}
```

## Launch the share UI

After registering for the `DataRequested` event, open the Windows Share Sheet when the user invokes Share. For WinUI 3 desktop apps, call `ShowShareUIForWindow` on the `IDataTransferManagerInterop` interface, passing your window's HWND. For code examples, see [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects#for-classes-that-implement-idatatransfermanagerinterop).

## Handle errors

If your app can't provide sharable content—for example, because the user hasn't selected anything—call [FailWithDisplayText](/uwp/api/windows.applicationmodel.datatransfer.datarequest.failwithdisplaytext) to show an informative message in the share UI:

```csharp
private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
{
    if (string.IsNullOrEmpty(selectedText))
    {
        args.Request.FailWithDisplayText("Select some text in the app before sharing.");
        return;
    }

    args.Request.Data.Properties.Title = "Selected text";
    args.Request.Data.SetText(selectedText);
}
```

## Delay share with delegates

If producing the share data is resource-intensive—for example, encoding a large image at multiple resolutions—use a delegate so the data is produced only when the receiving app requests it. This avoids unnecessary work when the user hasn't chosen a target yet.

```csharp
private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
{
    args.Request.Data.Properties.Title = "High-res photo";
    args.Request.Data.SetDataProvider(StandardDataFormats.Bitmap, OnDeferredImageRequested);
}

// Note: DataProviderHandler requires async void. The try/finally ensures the
// deferral completes even if an exception occurs.
private async void OnDeferredImageRequested(DataProviderRequest request)
{
    DataProviderDeferral deferral = request.GetDeferral();
    try
    {
        if (this.imageStream == null)
        {
            return;
        }

        InMemoryRandomAccessStream inMemoryStream = new InMemoryRandomAccessStream();

        // Decode the source image and re-encode at 50% size.
        BitmapDecoder imageDecoder = await BitmapDecoder.CreateAsync(this.imageStream);
        BitmapEncoder imageEncoder = await BitmapEncoder.CreateForTranscodingAsync(
            inMemoryStream, imageDecoder);

        imageEncoder.BitmapTransform.ScaledWidth =
            (uint)(imageDecoder.OrientedPixelWidth * 0.5);
        imageEncoder.BitmapTransform.ScaledHeight =
            (uint)(imageDecoder.OrientedPixelHeight * 0.5);
        await imageEncoder.FlushAsync();

        request.SetData(RandomAccessStreamReference.CreateFromStream(inMemoryStream));
    }
    finally
    {
        deferral.Complete();
    }
}
```

## See also

- [Receive shared data in your Windows app](./receive-shared-data.md)
- [Integrate packaged apps with Windows Share](./integrate-sharesheet-overview.md)
- [Integrate unpackaged apps with Windows Share](./integrate-sharesheet-receive.md#2-handle-the-share-activation)
- [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects#for-classes-that-implement-idatatransfermanagerinterop)
- [IDataTransferManagerInterop](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop)
- [DataTransferManager](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager)
- [DataPackage](/uwp/api/windows.applicationmodel.datatransfer.datapackage)
- [DataRequest](/uwp/api/windows.applicationmodel.datatransfer.datarequest)
- [FailWithDisplayText](/uwp/api/windows.applicationmodel.datatransfer.datarequest.failwithdisplaytext)
- [WPF Sharing content source app sample](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/ShareSource)
