---
ms.assetid: 05E418B4-5A62-42BD-BF66-A0762216D033
description: This topic shows you how to get a single preview frame from the media capture preview stream.
title: Get a preview frame
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Get a preview frame


This topic shows you how to get a single preview frame from the media capture preview stream.

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized, and that you have a [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) with an active video preview stream.

In addition to the namespaces required for basic media capture, capturing a preview frame requires the following namespace.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetPreviewFrameUsing":::

When you request a preview frame, you can specify the format in which you would like to receive the frame by creating a [**VideoFrame**](/uwp/api/Windows.Media.VideoFrame) object with the format you desire. This example creates a video frame that is the same resolution as the preview stream by calling [**VideoDeviceController.GetMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getmediastreamproperties) and specifying [**MediaStreamType.VideoPreview**](/uwp/api/Windows.Media.Capture.MediaStreamType) to request the properties for the preview stream. The width and height of the preview stream is used to create the new video frame.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCreateFormatFrame":::

If your [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object is initialized and you have an active preview stream, call [**GetPreviewFrameAsync**](/uwp/api/windows.media.capture.mediacapture.getpreviewframeasync) to get a preview stream. Pass in the video frame created in the last step to specify the format of the returned frame.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetPreviewFrameAsync":::

Get a [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) representation of the preview frame by accessing the [**SoftwareBitmap**](/uwp/api/windows.media.videoframe.softwarebitmap) property of the [**VideoFrame**](/uwp/api/Windows.Media.VideoFrame) object. For information about saving, loading, and modifying software bitmaps, see [Imaging](imaging.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetPreviewBitmap":::

You can also get an [**IDirect3DSurface**](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) representation of the preview frame if you want to use the image with Direct3D APIs.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetPreviewSurface":::

> [!IMPORTANT]
> Either the [**SoftwareBitmap**](/uwp/api/windows.media.videoframe.softwarebitmap) property or the [**Direct3DSurface**](/uwp/api/windows.media.videoframe.direct3dsurface) property of the returned **VideoFrame** may be null depending on how you call **GetPreviewFrameAsync** and also depending on the device on which your app is running.

> - If you call the overload of [**GetPreviewFrameAsync**](/uwp/api/windows.media.capture.mediacapture.getpreviewframeasync) that accepts a **VideoFrame** argument, the returned **VideoFrame** will have a non-null **SoftwareBitmap** and the **Direct3DSurface** property will be null.
> - If you call the overload of [**GetPreviewFrameAsync**](/uwp/api/windows.media.capture.mediacapture.getpreviewframeasync) that has no arguments on a device that uses a Direct3D surface to represent the frame internally, the **Direct3DSurface** property will be non-null and the **SoftwareBitmap** property will be null.
> - If you call the overload of [**GetPreviewFrameAsync**](/uwp/api/windows.media.capture.mediacapture.getpreviewframeasync) that has no arguments on a device that does not use a Direct3D surface to represent the frame internally, the **SoftwareBitmap** property will be non-null and the **Direct3DSurface** property will be null.

Your app should always check for a null value before trying to operate on the objects returned by the **SoftwareBitmap** or **Direct3DSurface** properties.

When you are done using the preview frame, be sure to call its [**Close**](/uwp/api/windows.media.videoframe.close) method (projected to Dispose in C#) to free the resources used by the frame. Or, use the **using** pattern, which automatically disposes of the object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCleanUpPreviewFrame":::

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
 

 
