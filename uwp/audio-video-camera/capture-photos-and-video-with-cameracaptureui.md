---
ms.assetid: CC0D6E9B-128D-488B-912F-318F5EE2B8D3
description: This article describes how to use the [**CameraCaptureUI**](/uwp/api/windows.media.capture.cameracaptureui) class to capture photos or videos by using the camera UI built into Windows.
title: Capture photos and video with the Windows built-in camera UI
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs: 
- csharp
- cppwinrt
---

# Capture photos and video with the Windows built-in camera UI

This article describes how to use the [**CameraCaptureUI**](/uwp/api/windows.media.capture.cameracaptureui) class to capture photos or videos by using the camera UI built into Windows. This feature is easy to use. It allows your app to get a user-captured photo or video with just a few lines of code.

If you want to provide your own camera UI, or if your scenario requires more robust, low-level control of the capture operation, then you should use the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class, and implement your own capture experience. For more information, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md).

> [!NOTE]
> You shouldn't specify the **webcam** nor **microphone** capabilities in your app manifest file if your app only uses **CameraCaptureUI**. If you do, your app will be displayed in the device's camera privacy settings, but even if the user denies camera access to your app, this won't prevent the **CameraCaptureUI** from capturing media. <p>This is because the Windows built-in camera app is a trusted first-party app that requires the user to initiate photo, audio, and video capture with a button press. Your app may fail Windows Application Certification Kit certification when submitted to Microsoft Store if you specify the webcam or microphone capabilities when using **CameraCaptureUI** as your only photo capture mechanism.<p>
You must specify the **webcam** or **microphone** capabilities in your app manifest file if you're using **MediaCapture** to capture audio, photos, or video programmatically.

## Capture a photo with CameraCaptureUI

To use the camera capture UI, include the [**Windows.Media.Capture**](/uwp/api/Windows.Media.Capture) namespace in your project. To do file operations with the returned image file, include [**Windows.Storage**](/uwp/api/Windows.Storage).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetUsingCaptureUI":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.h" id="SnippetUsingCaptureUI":::

To capture a photo, create a new [**CameraCaptureUI**](/uwp/api/Windows.Media.Capture.CameraCaptureUI) object. By using the object's [**PhotoSettings**](/uwp/api/windows.media.capture.cameracaptureui.photosettings) property, you can specify properties for the returned photo, such as the image format of the photo. By default, the camera capture UI supports cropping the photo before it's returned. This can be disabled with the [**AllowCropping**](/uwp/api/windows.media.capture.cameracaptureuiphotocapturesettings.allowcropping) property. This example sets the [**CroppedSizeInPixels**](/uwp/api/windows.media.capture.cameracaptureuiphotocapturesettings.croppedsizeinpixels) to request that the returned image be 200 x 200 in pixels.

> [!NOTE]
> Image cropping in the **CameraCaptureUI** isn't supported for devices in the Mobile device family. The value of the [**AllowCropping**](/uwp/api/windows.media.capture.cameracaptureuiphotocapturesettings.allowcropping) property is ignored when your app is running on these devices.

Call [**CaptureFileAsync**](/uwp/api/windows.media.capture.cameracaptureui.capturefileasync) and specify [**CameraCaptureUIMode.Photo**](/uwp/api/Windows.Media.Capture.CameraCaptureUIMode) to specify that a photo should be captured. The method returns a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) instance containing the image if the capture is successful. If the user cancels the capture, the returned object is null.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetCapturePhoto":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.cpp" id="SnippetCapturePhoto":::

The **StorageFile** containing the captured photo is given a dynamically generated name and saved in your app's local folder. To better organize your captured photos, you can move the file to a different folder.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetCopyAndDeletePhoto":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.cpp" id="SnippetCopyAndDeletePhoto":::

To use your photo in your app, you may want to create a [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) object that can be used with several different Universal Windows app features.

First, include the [**Windows.Graphics.Imaging**](/uwp/api/Windows.Graphics.Imaging) namespace in your project.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetUsingSoftwareBitmap":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.h" id="SnippetUsingSoftwareBitmap":::

Call [**OpenAsync**](/uwp/api/windows.storage.istoragefile.openasync) to get a stream from the image file. Call [**BitmapDecoder.CreateAsync**](/uwp/api/windows.graphics.imaging.bitmapdecoder.createasync) to get a bitmap decoder for the stream. Then, call [**GetSoftwareBitmap**](/uwp/api/windows.graphics.imaging.bitmapdecoder.getsoftwarebitmapasync) to get a **SoftwareBitmap** representation of the image.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetSoftwareBitmap":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.cpp" id="SnippetSoftwareBitmap":::

To display the image in your UI, declare an [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image) control in your XAML page.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml" id="SnippetImageControl":::
:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.xaml" id="SnippetImageControl":::

To use the software bitmap in your XAML page, include the using [**Windows.UI.Xaml.Media.Imaging**](/uwp/api/Windows.UI.Xaml.Media.Imaging) namespace in your project.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetUsingSoftwareBitmapSource":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.h" id="SnippetUsingSoftwareBitmapSource":::

The **Image** control requires that the image source be in BGRA8 format with premultiplied alpha or no alpha. Call the static method [**SoftwareBitmap.Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) to create a new software bitmap with the desired format. Next, create a new [**SoftwareBitmapSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource) object and call it [**SetBitmapAsync**](/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource.setbitmapasync) to assign the software bitmap to the source. Finally, set the **Image** control's [**Source**](/uwp/api/windows.ui.xaml.controls.image.source) property to display the captured photo in the UI.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetSetImageSource":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.cpp" id="SnippetSetImageSource":::

## Capture a video with CameraCaptureUI

To capture a video, create a new [**CameraCaptureUI**](/uwp/api/Windows.Media.Capture.CameraCaptureUI) object. By using the object's [**VideoSettings**](/uwp/api/windows.media.capture.cameracaptureui.videosettings) property, you can specify properties for the returned video, such as the format of the video.

Call [**CaptureFileAsync**](/uwp/api/windows.media.capture.cameracaptureui.capturefileasync) and specify [**Video**](/uwp/api/windows.media.capture.cameracaptureui.videosettings) to capture a video. The method returns a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) instance containing the video if the capture is successful. If you cancel the capture, the returned object is null.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetCaptureVideo":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.cpp" id="SnippetCaptureVideo":::

What you do with the captured video file depends on the scenario for your app. The rest of this article shows you how to quickly create a media composition from one or more captured videos and show it in your UI.

First, add a [**MediaPlayerElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) control in which the video composition will display on your XAML page.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml" id="SnippetMediaElement":::

When the video file returns from the camera capture UI, create a new [**MediaSource**](/uwp/api/windows.media.core.mediasource) by calling **[CreateFromStorageFile](/uwp/api/windows.media.core.mediasource.createfromstoragefile)**. Call the **[Play](/uwp/api/windows.media.playback.mediaplayer.Play)** method of the default **[MediaPlayer](/uwp/api/windows.media.playback.mediaplayer)** associated with the **MediaPlayerElement** to play the video.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cs/MainPage.xaml.cs" id="SnippetPlayVideo":::
:::code language="cppwinrt" source="~/../snippets-windows/windows-uwp/audio-video-camera/CameraCaptureUIWin10/cppwinrt/MainPage.cpp" id="SnippetPlayVideo":::

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [CameraCaptureUI](/uwp/api/Windows.Media.Capture.CameraCaptureUI)
