---
ms.assetid: 0186EA01-8446-45BA-A109-C5EB4B80F368
description: Learn how to use the AdvancedPhotoCapture class to capture high dynamic range (HDR) and low-light photos in a WinUI 3 app.
title: High dynamic range (HDR) and low-light photo capture
ms.date: 08/22/2024
ms.topic: article
keywords: windows 10, windows 11, winui3, camera
ms.localizationpriority: medium
---
# High dynamic range (HDR) and low-light photo capture

This article shows you how to use the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) class to capture high dynamic range (HDR) photos. This API also allows you to obtain a reference frame from the HDR capture before the processing of the final image is complete.

Other articles related to HDR capture include:

- You can use the [**SceneAnalysisEffect**](/uwp/api/Windows.Media.Core.SceneAnalysisEffect) to allow the system to evaluate the content of the media capture preview stream to determine if HDR processing would improve the capture result. For more information, see [Scene analysis for MediaCapture](scene-analysis-for-media-capture.md).

- Use the [**HdrVideoControl**](/uwp/api/Windows.Media.Devices.HdrVideoControl) to capture video using the Windows built-in HDR processing algorithm. For more information, see [Capture device controls for video capture](capture-device-controls-for-video-capture.md).

- You can use the [**VariablePhotoSequenceCapture**](/uwp/api/Windows.Media.Capture.Core.VariablePhotoSequenceCapture) to capture a sequence of photos, each with different capture settings, and implement your own HDR or other processing algorithm. For more information, see [Variable photo sequence](variable-photo-sequence.md).

> [!NOTE] 
> Starting with Windows 10, version 1709, recording video and using **AdvancedPhotoCapture** concurrently is supported.  This is not supported in previous versions. This change means that you can have a prepared **[LowLagMediaRecording](/uwp/api/windows.media.capture.lowlagmediarecording)** and **[AdvancedPhotoCapture](/uwp/api/windows.media.capture.advancedphotocapture)** at the same time. You can start or stop video recording between calls to **[MediaCapture.PrepareAdvancedPhotoCaptureAsync](/uwp/api/windows.media.capture.mediacapture.prepareadvancedphotocaptureasync)** and **[AdvancedPhotoCapture.FinishAsync](/uwp/api/windows.media.capture.advancedphotocapture.FinishAsync)**. You can also call **[AdvancedPhotoCapture.CaptureAsync](/uwp/api/windows.media.capture.advancedphotocapture.CaptureAsync)** while video is recording. However, some **AdvancedPhotoCapture** scenarios, like capturing an HDR photo while recording video would cause some video frames to be altered by the HDR capture, resulting in a negative user experience. For this reason, the list of modes returned by the **[AdvancedPhotoControl.SupportedModes](/uwp/api/windows.media.devices.advancedphotocontrol.SupportedModes)** will be different while video is recording. You should check this value immediately after starting or stopping video recording to ensure that the desired mode is supported in the current video recording state.

> [!NOTE] 
> Starting with Windows 10, version 1709, when the **AdvancedPhotoCapture** is set to HDR mode, the setting of the [**FlashControl.Enabled**](/uwp/api/windows.media.devices.flashcontrol.enabled) property is ignored and the flash is never fired. For other capture modes, if the **FlashControl.Enabled**, it will override the **AdvancedPhotoCapture** settings and cause a normal photo to be captured with flash. If [**Auto**](/uwp/api/windows.media.devices.flashcontrol.auto) is set to true, the **AdvancedPhotoCapture** may or may not use flash, depending on the camera driver's default behavior for the conditions in the current scene. On previous releases, the **AdvancedPhotoCapture** flash setting always overrides the **FlashControl.Enabled** setting.

There is a full sample demonstrating the use of the **AdvancedPhotoCapture** class that you can use to see the API used in context or as a starting point for your own app. For more information see, [Camera Advanced Capture sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraAdvancedCapture).

## HDR photo capture

### Determine if HDR photo capture is supported on the current device

The HDR capture technique described in this article is performed using the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) object. Not all devices support HDR capture with **AdvancedPhotoCapture**. Determine if the device on which your app is currently running supports the technique by getting the **MediaCapture** object's [**VideoDeviceController**](/uwp/api/Windows.Media.Devices.VideoDeviceController) and then getting the [**AdvancedPhotoControl**](/uwp/api/Windows.Media.Devices.AdvancedPhotoControl) property. Check the video device controller's [**SupportedModes**](/uwp/api/windows.media.devices.advancedphotocontrol.supportedmodes) collection to see if it includes [**AdvancedPhotoMode.Hdr**](/uwp/api/Windows.Media.Devices.AdvancedPhotoMode). If it does, HDR capture using **AdvancedPhotoCapture** is supported.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetHdrSupported":::

### Configure and prepare the AdvancedPhotoCapture object

Because you will need to access the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) instance from multiple places within your code, you should declare a member variable to hold the object.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetDeclareAdvancedCapture":::

In your app, after you have initialized the **MediaCapture** object, create an [**AdvancedPhotoCaptureSettings**](/uwp/api/Windows.Media.Devices.AdvancedPhotoCaptureSettings) object and set the mode to [**AdvancedPhotoMode.Hdr**](/uwp/api/Windows.Media.Devices.AdvancedPhotoMode). Call the [**AdvancedPhotoControl**](/uwp/api/Windows.Media.Devices.AdvancedPhotoControl) object's [**Configure**](/uwp/api/windows.media.devices.advancedphotocontrol.configure) method, passing in the **AdvancedPhotoCaptureSettings** object you created.

Call the **MediaCapture** object's [**PrepareAdvancedPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.prepareadvancedphotocaptureasync), passing in an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the type of encoding the capture should use. The **ImageEncodingProperties** class provides static methods for creating the image encodings that are supported by **MediaCapture**.

**PrepareAdvancedPhotoCaptureAsync** returns the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) object you will use to initiate photo capture. You can use this object to register handlers for the [**OptionalReferencePhotoCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.optionalreferencephotocaptured) and [**AllPhotosCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.allphotoscaptured) which are discussed later in this article.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCreateAdvancedCaptureAsync":::

### Capture an HDR photo

Capture an HDR photo by calling the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) object's [**CaptureAsync**](/uwp/api/windows.media.capture.advancedphotocapture.captureasync) method. This method returns an [**AdvancedCapturedPhoto**](/uwp/api/Windows.Media.Capture.AdvancedCapturedPhoto) object that provides the captured photo in its [**Frame**](/uwp/api/windows.media.capture.advancedcapturedphoto.frame) property. Next, the photo is saved to disk.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCaptureHdrPhotoAsync":::


### Get optional reference frame

The HDR process captures multiple frames and then composites them into a single image after all of the frames have been captured. You can get access to a frame after it is captured but before the entire HDR process is complete by handling the [**OptionalReferencePhotoCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.optionalreferencephotocaptured) event. You don't need to do this if you are only interested in the final HDR photo result.

> [!IMPORTANT]
> [**OptionalReferencePhotoCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.optionalreferencephotocaptured) is not raised on devices that support hardware HDR and therefore do not generate reference frames. Your app should handle the case where this event is not raised.

Because the reference frame arrives out of context of the call to **CaptureAsync**, a mechanism is provided to pass context information to the **OptionalReferencePhotoCaptured** handler. First you should call an object that will contain your context information. The name and contents of this object is up to you. This example defines an object that has members to track the file name and camera orientation of the capture.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetAdvancedCaptureContext":::


Create a new instance of your context object, populate its members, and then pass it into the overload of [**CaptureAsync**](/uwp/api/windows.media.capture.advancedphotocapture.captureasync) that accepts an object as a parameter.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCaptureWithContext":::

In the [**OptionalReferencePhotoCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.optionalreferencephotocaptured) event handler, cast the [**Context**](/uwp/api/windows.media.capture.optionalreferencephotocapturedeventargs.context) property of the [**OptionalReferencePhotoCapturedEventArgs**](/uwp/api/Windows.Media.Capture.OptionalReferencePhotoCapturedEventArgs) object to your context object class. This example modifies the file name to distinguish the reference frame image from the final HDR image and then saves the image to disk.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetOptionalReferencePhotoCaptured":::

### Receive a notification when all frames have been captured

The HDR photo capture has two steps. First, multiple frames are captured, and then the frames are processed into the final HDR image. You can't initiate another capture while the source HDR frames are still being captured, but you can initiate a capture after all of the frames have been captured but before the HDR post-processing is complete. The [**AllPhotosCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.allphotoscaptured) event is raised when the HDR captures are complete, letting you know that you can initiate another capture. A typical scenario is to disable your UI's capture button when HDR capture begins and then reenable it when **AllPhotosCaptured** is raised.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetAllPhotosCaptured":::

### Clean up the AdvancedPhotoCapture object

When your app is done capturing, before disposing of the **MediaCapture** object, you should shut down the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) object by calling [**FinishAsync**](/uwp/api/windows.media.capture.advancedphotocapture.finishasync) and setting your member variable to null.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCleanUpAdvancedPhotoCapture":::

## Low-light photo capture

Starting with Windows 10, version 1607, **AdvancedPhotoCapture** can be used to capture photos using a built-in algorithm that enhances the quality of photos captured in low-light settings. When you use the low-light feature of the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) class, the system will evaluate the current scene and, if needed, apply an algorithm to compensate for low-light conditions. If the system determines that the algorithm is not needed, a regular capture is performed instead.

Before using low-light photo capture, determine if the device on which your app is currently running supports the technique by getting the **MediaCapture** object's [**VideoDeviceController**](/uwp/api/Windows.Media.Devices.VideoDeviceController) and then getting the [**AdvancedPhotoControl**](/uwp/api/Windows.Media.Devices.AdvancedPhotoControl) property. Check the video device controller's [**SupportedModes**](/uwp/api/windows.media.devices.advancedphotocontrol.supportedmodes) collection to see if it includes [**AdvancedPhotoMode.LowLight**](/uwp/api/Windows.Media.Devices.AdvancedPhotoMode). If it does, low-light capture using **AdvancedPhotoCapture** is supported.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetLowLightSupported":::

In your app, after you have initialized the **MediaCapture** object, create an [**AdvancedPhotoCaptureSettings**](/uwp/api/Windows.Media.Devices.AdvancedPhotoCaptureSettings) object and set the mode to [**AdvancedPhotoMode.LowLight**](/uwp/api/Windows.Media.Devices.AdvancedPhotoMode). Call the [**AdvancedPhotoControl**](/uwp/api/Windows.Media.Devices.AdvancedPhotoControl) object's [**Configure**](/uwp/api/windows.media.devices.advancedphotocontrol.configure) method, passing in the **AdvancedPhotoCaptureSettings** object you created.

Call the **MediaCapture** object's [**PrepareAdvancedPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.prepareadvancedphotocaptureasync), passing in an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the type of encoding the capture should use.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCreateAdvancedCaptureLowLightAsync":::

To capture a photo, call [**CaptureAsync**](/uwp/api/windows.media.capture.advancedphotocapture.captureasync).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCaptureLowLight":::

You can capture multiple low-light photos without reconfiguring the **AdvancedPhotoCapture** object, but when you are done capturing, you should call [**FinishAsync**](/uwp/api/windows.media.capture.advancedphotocapture.finishasync) to clean up the object and associated resources.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCleanUpAdvancedPhotoCapture":::

## Working with AdvancedCapturedPhoto objects

[**AdvancedPhotoCapture.CaptureAsync**](/uwp/api/windows.media.capture.advancedphotocapture.captureasync) returns an [**AdvancedCapturedPhoto**](/uwp/api/Windows.Media.Capture.AdvancedCapturedPhoto) object representing the captured photo. This object exposes the [**Frame**](/uwp/api/windows.media.capture.advancedcapturedphoto.frame) property which returns a [**CapturedFrame**](/uwp/api/Windows.Media.Capture.CapturedFrame) object representing the image. The [**OptionalReferencePhotoCaptured**](/uwp/api/windows.media.capture.advancedphotocapture.optionalreferencephotocaptured) event also provides a **CapturedFrame** object in its event args. After you get an object of this type, there are a number of things you can do with it, including creating a [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) or saving the image to a file. 

## Get a SoftwareBitmap from a CapturedFrame

Get a **SoftwareBitmap** from a **CapturedFrame** object by simply accessing the [**SoftwareBitmap**](/uwp/api/windows.media.capture.capturedframe.softwarebitmap) property of the object. However, most encoding formats do not support **SoftwareBitmap** with **AdvancedPhotoCapture**, so you should check and make sure the property is not null before using it.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetSoftwareBitmapFromCapturedFrame":::

In the current release, the only encoding format that supports **SoftwareBitmap** for **AdvancedPhotoCapture** is uncompressed NV12. So, if you want to use this feature, you must specify that encoding when you call [**PrepareAdvancedPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.prepareadvancedphotocaptureasync).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetUncompressedNv12":::

Of course, you can always save the image to a file and then load the file into a **SoftwareBitmap** in a separate step. For more information about working with **SoftwareBitmap**, see [**Create, edit, and save bitmap images**](/windows/uwp/audio-video-camera/imaging).


