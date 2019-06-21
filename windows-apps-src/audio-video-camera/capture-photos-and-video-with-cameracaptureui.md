---
ms.assetid: CC0D6E9B-128D-488B-912F-318F5EE2B8D3
description: This article describes how to use the CameraCaptureUI class to capture photos or videos using the camera UI built into Windows.
title: Capture photos and video with Windows built-in camera UI
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Capture photos and video with Windows built-in camera UI



This article describes how to use the CameraCaptureUI class to capture photos or videos using the camera UI built into Windows. This feature is easy to use and allows your app to get a user-captured photo or video with just a few lines of code.

If you want to provide your own camera UI or if your scenario requires more robust, low-level control of the capture operation, you should use the [**MediaCapture**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture) object and implement your own capture experience. For more information, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md).

> [!NOTE]
> You should not specify the **webcam** or **microphone** capabilities in your app manifest file if your app only uses CameraCaptureUI. If you do so, your app will be displayed in the device's camera privacy settings, but even if the user denies camera access to your app, it will not prevent the CameraCaptureUI from capturing media. This is because the Windows built-in camera app is a trusted first-party app that requires the user to initiate photo, audio, and video capture with a button press. Your app may fail Windows Application Certification Kit certification when submitted to the Store if you specify the webcam or microphone capabilities when using CameraCaptureUI as your only photo capture mechanism.
> You must specify the webcam or microphone capabilities in your app manifest file if you are using MediaCapture to capture audio, photos, or video programmatically.

## Capture a photo with CameraCaptureUI

To use the camera capture UI, include the [**Windows.Media.Capture**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture) namespace in your project. To do file operations with the returned image file, include [**Windows.Storage**](https://docs.microsoft.com/uwp/api/Windows.Storage).

[!code-cs[UsingCaptureUI](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetUsingCaptureUI)]

To capture a photo, create a new [**CameraCaptureUI**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUI) object. Using the object's [**PhotoSettings**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureui.photosettings) property you can specify properties for the returned photo such as the image format of the photo. By default, the camera capture UI allows the user to crop the photo before it is returned, although this can be disabled with the [**AllowCropping**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureuiphotocapturesettings.allowcropping) property. This example sets the [**CroppedSizeInPixels**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureuiphotocapturesettings.croppedsizeinpixels) to request that the returned image be 200 x 200 in pixels.

> [!NOTE]
> Imaging cropping in the **CameraCaptureUI** is not supported for devices in the Mobile device family. The value of the [**AllowCropping**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureuiphotocapturesettings.allowcropping) property is ignored when your app is running on these devices.

Call [**CaptureFileAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureui.capturefileasync) and specify [**CameraCaptureUIMode.Photo**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUIMode) to specify that a photo should be captured. The method returns a [**StorageFile**](https://docs.microsoft.com/uwp/api/Windows.Storage.StorageFile) instance containing the image if the capture is successful. If the user cancels the capture, the returned object is null.

[!code-cs[CapturePhoto](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetCapturePhoto)]

The **StorageFile** containing the captured photo is given a dynamically generated name and saved in your app's local folder. To better organize your captured photos, you may want to move the file to a different folder.

[!code-cs[CopyAndDeletePhoto](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetCopyAndDeletePhoto)]

To use your photo in your app, you may want to create a [**SoftwareBitmap**](https://docs.microsoft.com/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) object that can be used with several different Universal Windows app features.

First you should include the [**Windows.Graphics.Imaging**](https://docs.microsoft.com/uwp/api/Windows.Graphics.Imaging) namespace in your project.

[!code-cs[UsingSoftwareBitmap](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetUsingSoftwareBitmap)]

Call [**OpenAsync**](https://docs.microsoft.com/uwp/api/windows.storage.istoragefile.openasync) to get a stream from the image file. Call [**BitmapDecoder.CreateAsync**](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapdecoder.createasync) to get a bitmap decoder for the stream. Then call [**GetSoftwareBitmap**](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapdecoder.getsoftwarebitmapasync) to get a **SoftwareBitmap** representation of the image.

[!code-cs[SoftwareBitmap](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetSoftwareBitmap)]

To display the image in your UI, declare an [**Image**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Image) control in your XAML page.

[!code-xml[ImageControl](./code/CameraCaptureUIWin10/cs/MainPage.xaml#SnippetImageControl)]

To use the software bitmap in your XAML page, include the using [**Windows.UI.Xaml.Media.Imaging**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Imaging) namespace in your project.

[!code-cs[UsingSoftwareBitmapSource](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetUsingSoftwareBitmapSource)]

The **Image** control requires that the image source be in BGRA8 format with premultiplied alpha or no alpha, so call the static method [**SoftwareBitmap.Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) to create a new software bitmap with the desired format. Next, create a new [**SoftwareBitmapSource**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource) object and call [**SetBitmapAsync**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource.setbitmapasync) to assign the software bitmap to the source. Finally, set the **Image** control's [**Source**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.image.source) property to display the captured photo in the UI.

[!code-cs[SetImageSource](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetSetImageSource)]

## Capture a video with CameraCaptureUI

To capture a video, create a new [**CameraCaptureUI**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUI) object. Using the object's [**VideoSettings**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureui.videosettings) property you can specify properties for the returned video such as the format of the video.

Call [**CaptureFileAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureui.capturefileasync) and specify [**Video**](https://docs.microsoft.com/uwp/api/windows.media.capture.cameracaptureui.videosettings) to specify that a video should be capture. The method returns a [**StorageFile**](https://docs.microsoft.com/uwp/api/Windows.Storage.StorageFile) instance containing the video if the capture is successful. If the user cancels the capture, the returned object is null.

[!code-cs[CaptureVideo](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetCaptureVideo)]

What you do with the captured video file depends on the scenario for your app. The rest of this article shows you how to quickly create a media composition from one or more captured videos and show it in your UI.

First, add a [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) control in which the video composition will be displayed to your XAML page.

[!code-xml[MediaElement](./code/CameraCaptureUIWin10/cs/MainPage.xaml#SnippetMediaElement)]


With the video file returned from the camera capture UI, create a new [**MediaSource**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource) by calling **[CreateFromStorageFile](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromstoragefile)**. Call the **[Play](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer.Play)** method of the default **[MediaPlayer](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer)** associated with the **MediaPlayerElement** to play the video.

[!code-cs[PlayVideo](./code/CameraCaptureUIWin10/cs/MainPage.xaml.cs#SnippetPlayVideo)]
 

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [**CameraCaptureUI**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.CameraCaptureUI) 
 

 




