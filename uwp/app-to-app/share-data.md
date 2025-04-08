---
title: Share data
description: This article explains how to support the Share contract in a desktop or a Universal Windows Platform (UWP) app.
ms.assetid: 32287F5E-EB86-4B98-97FF-8F6228D06782
ms.date: 10/18/2023
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Share data

This article explains how to support the Share contract in a desktop or a Universal Windows Platform (UWP) app. The Share contract is an easy way to quickly share data&mdash;such as text, links, photos, and videos&mdash;between apps. For example, a user might want to share a webpage with their friends using a social networking app, or save a link in a notes app to refer to later.

> [!NOTE]
> The code examples in this article are from UWP apps. Desktop apps should use the [**IDataTransferManagerInterop**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop). For more info, and code examples, see [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects#for-classes-that-implement-idatatransfermanagerinterop). Also see the [WPF Sharing content source app sample](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/ShareSource).

## Set up an event handler

Add a [**DataRequested**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.datarequested) event handler to be called whenever a user invokes share. This can occur either when the user taps a control in your app (such as a button or app bar command) or automatically in a specific scenario (if the user finishes a level and gets a high score, for example).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/app-to-app/share_data/cs/MainPage.xaml.cs" id="SnippetPrepareToShare":::

When a [**DataRequested**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.datarequested) event occurs, your app receives a [**DataRequest**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataRequest) object. This contains a [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) that you can use to provide the content that the user wants to share. You must provide a title and data to share. A description is optional, but recommended.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/app-to-app/share_data/cs/MainPage.xaml.cs" id="SnippetCreateRequest":::

## Choose data

You can share various types of data, including:

- Plain text
- Uniform Resource Identifiers (URIs)
- HTML
- Formatted text
- Bitmaps
- Files
- Custom developer-defined data

The [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) object can contain one or more of these formats, in any combination. The following example demonstrates sharing text.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/app-to-app/share_data/cs/MainPage.xaml.cs" id="SnippetSetContent":::

## Set properties

When you package data for sharing, you can supply a variety of properties that provide additional information about the content being shared. These properties help target apps improve the user experience. For example, a description helps when the user is sharing content with more than one app. Adding a thumbnail when sharing an image or a link to a web page provides a visual reference to the user. For more information, see [**DataPackagePropertySet**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackagePropertySet).

> [!WARNING]
> All properties except the title are optional. The title property is mandatory and must be set.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/app-to-app/share_data/cs/MainPage.xaml.cs" id="SnippetSetProperties":::

## Launch the share UI

A UI for sharing is provided by the system. To launch it, call the [**ShowShareUI**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui) method.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/app-to-app/share_data/cs/MainPage.xaml.cs" id="SnippetShowUI":::

## Handle errors

In most cases, sharing content is a straightforward process. However, there's always a chance that something unexpected could happen. For example, the app might require the user to select content for sharing but the user didn't select any. To handle these situations, use the [**FailWithDisplayText**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataRequest#Windows_ApplicationModel_DataTransfer_DataRequest_FailWithDisplayText_System_String_) method, which will display a message to the user if something goes wrong.

## Delay share with delegates

Sometimes, it might not make sense to prepare the data that the user wants to share right away. For example, if your app supports sending a large image file in several different possible formats, it's inefficient to create all those images before the user makes their selection.

To solve this problem, a [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) can contain a delegate — a function that is called when the receiving app requests data. We recommend using a delegate any time that the data a user wants to share is resource-intensive.

<!-- For some reason, this snippet was inline in the WDCML topic. Suggest moving to VS project with rest of snippets. -->
```cs
async void OnDeferredImageRequestedHandler(DataProviderRequest request)
{
    // Provide updated bitmap data using delayed rendering
    if (this.imageStream != null)
    {
        DataProviderDeferral deferral = request.GetDeferral();
        InMemoryRandomAccessStream inMemoryStream = new InMemoryRandomAccessStream();

        // Decode the image.
        BitmapDecoder imageDecoder = await BitmapDecoder.CreateAsync(this.imageStream);

        // Re-encode the image at 50% width and height.
        BitmapEncoder imageEncoder = await BitmapEncoder.CreateForTranscodingAsync(inMemoryStream, imageDecoder);
        imageEncoder.BitmapTransform.ScaledWidth = (uint)(imageDecoder.OrientedPixelWidth * 0.5);
        imageEncoder.BitmapTransform.ScaledHeight = (uint)(imageDecoder.OrientedPixelHeight * 0.5);
        await imageEncoder.FlushAsync();

        request.SetData(RandomAccessStreamReference.CreateFromStream(inMemoryStream));
        deferral.Complete();
    }
}
```

## See also

- [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects#for-classes-that-implement-idatatransfermanagerinterop)
- [App-to-app communication](index.md)
- [Receive data](receive-data.md)
- [DataPackage](/uwp/api/windows.applicationmodel.datatransfer.datapackage)
- [DataPackagePropertySet](/uwp/api/windows.applicationmodel.datatransfer.datapackagepropertyset)
- [DataRequest](/uwp/api/windows.applicationmodel.datatransfer.datarequest)
- [DataRequested](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.datarequested)
- [FailWithDisplayText](/uwp/api/windows.applicationmodel.datatransfer.datarequest.failwithdisplaytext)
- [ShowShareUi](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui)
