---
author: drewbatgit
ms.assetid: 0186EA01-8446-45BA-A109-C5EB4B80F368
description: This article shows you how to use the AdvancedPhotoCapture class to capture high dynamic range (HDR) and low-light photos.
title: High dynamic range (HDR) and low-light photo capture
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# High dynamic range (HDR) and low-light photo capture



This article shows you how to use the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) class to capture high dynamic range (HDR) photos. This API also allows you to obtain a reference frame from the HDR capture before the processing of the final image is complete.

Other articles related to HDR capture include:

-   You can use the [**SceneAnalysisEffect**](https://msdn.microsoft.com/library/windows/apps/dn948902) to allow the system to evaluate the content of the media capture preview stream to determine if HDR processing would improve the capture result. For more information, see [Scene analysis for MediaCapture](scene-analysis-for-media-capture.md).

-   Use the [**HdrVideoControl**](https://msdn.microsoft.com/library/windows/apps/dn926680) to capture video using the Windows built-in HDR processing algorithm. For more information, see [Capture device controls for video capture](capture-device-controls-for-video-capture.md).

-   You can use the [**VariablePhotoSequenceCapture**](https://msdn.microsoft.com/library/windows/apps/dn652564) to capture a sequence of photos, each with different capture settings, and implement your own HDR or other processing algorithm. For more information, see [Variable photo sequence](variable-photo-sequence.md).



> [!NOTE] 
> Starting with Windows 10, version 1709, recording video and using **AdvancedPhotoCapture** concurrently is supported.  This is not supported in previous versions. This change means that you can have a prepared **[LowLagMediaRecording](https://docs.microsoft.com/uwp/api/windows.media.capture.lowlagmediarecording)** and **[AdvancedPhotoCapture](https://docs.microsoft.com/uwp/api/windows.media.capture.advancedphotocapture)** at the same time. You can start or stop video recording between calls to **[MediaCapture.PrepareAdvancedPhotoCaptureAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.prepareadvancedphotocaptureasync)** and **[AdvancedPhotoCapture.FinishAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.advancedphotocapture.FinishAsync)**. You can also call **[AdvancedPhotoCapture.CaptureAsync](https://docs.microsoft.com/uwp/api/windows.media.capture.advancedphotocapture.CaptureAsync)** while video is recording. However, some **AdvancedPhotoCapture** scenarios, like capturing an HDR photo while recording video would cause some video frames to be altered by the HDR capture, resulting in a negative user experience. For this reason, the list of modes returned by the **[AdvancedPhotoControl.SupportedModes](https://docs.microsoft.com/uwp/api/windows.media.devices.advancedphotocontrol.SupportedModes)** will be different while video is recording. You should check this value immediately after starting or stopping video recording to ensure that the desired mode is supported in the current video recording state.


> [!NOTE] 
> Starting with Windows 10, version 1709, when the **AdvancedPhotoCapture** is set to HDR mode, the setting of the [**FlashControl.Enabled**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Devices.FlashControl.Enabled) property is ignored and the flash is never fired. For other capture modes, if the **FlashControl.Enabled**, it will override the **AdvancedPhotoCapture** settings and cause a normal photo to be captured with flash. If [**Auto**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Devices.FlashControl.Auto) is set to true, the **AdvancedPhotoCapture** may or may not use flash, depending on the camera driver's default behavior for the conditions in the current scene. On previous releases, the **AdvancedPhotoCapture** flash setting always overrides the **FlashControl.Enabled** setting.

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

There is a Universal Windows Sample demonstrating the use of the **AdvancedPhotoCapture** class that you can use to see the API used in context or as a starting point for your own app. For more information see, [Camera Advanced Capture sample](http://go.microsoft.com/fwlink/?LinkID=620517).

## Advanced photo capture namespaces

The code examples in this article use APIs in the following namespaces in addition to the namespaces required for basic media capture.

[!code-cs[HDRPhotoUsing](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetHDRPhotoUsing)]

## HDR photo capture

### Determine if HDR photo capture is supported on the current device

The HDR capture technique described in this article is performed using the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) object. Not all devices support HDR capture with **AdvancedPhotoCapture**. Determine if the device on which your app is currently running supports the technique by getting the **MediaCapture** object's [**VideoDeviceController**](https://msdn.microsoft.com/library/windows/apps/br226825) and then getting the [**AdvancedPhotoControl**](https://msdn.microsoft.com/library/windows/apps/mt147840) property. Check the video device controller's [**SupportedModes**](https://msdn.microsoft.com/library/windows/apps/mt147844) collection to see if it includes [**AdvancedPhotoMode.Hdr**](https://msdn.microsoft.com/library/windows/apps/mt147845). If it does, HDR capture using **AdvancedPhotoCapture** is supported.

[!code-cs[HdrSupported](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetHdrSupported)]

### Configure and prepare the AdvancedPhotoCapture object

Because you will need to access the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) instance from multiple places within your code, you should declare a member variable to hold the object.

[!code-cs[DeclareAdvancedCapture](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetDeclareAdvancedCapture)]

In your app, after you have initialized the **MediaCapture** object, create an [**AdvancedPhotoCaptureSettings**](https://msdn.microsoft.com/library/windows/apps/mt147837) object and set the mode to [**AdvancedPhotoMode.Hdr**](https://msdn.microsoft.com/library/windows/apps/mt147845). Call the [**AdvancedPhotoControl**](https://msdn.microsoft.com/library/windows/apps/mt147840) object's [**Configure**](https://msdn.microsoft.com/library/windows/apps/mt147841) method, passing in the **AdvancedPhotoCaptureSettings** object you created.

Call the **MediaCapture** object's [**PrepareAdvancedPhotoCaptureAsync**](https://msdn.microsoft.com/library/windows/apps/mt181403), passing in an [**ImageEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/hh700993) object specifying the type of encoding the capture should use. The **ImageEncodingProperties** class provides static methods for creating the image encodings that are supported by **MediaCapture**.

**PrepareAdvancedPhotoCaptureAsync** returns the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) object you will use to initiate photo capture. You can use this object to register handlers for the [**OptionalReferencePhotoCaptured**](https://msdn.microsoft.com/library/windows/apps/mt181392) and [**AllPhotosCaptured**](https://msdn.microsoft.com/library/windows/apps/mt181387) which are discussed later in this article.

[!code-cs[CreateAdvancedCaptureAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCreateAdvancedCaptureAsync)]

### Capture an HDR photo

Capture an HDR photo by calling the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) object's [**CaptureAsync**](https://msdn.microsoft.com/library/windows/apps/mt181388) method. This method returns an [**AdvancedCapturedPhoto**](https://msdn.microsoft.com/library/windows/apps/mt181378) object that provides the captured photo in its [**Frame**](https://msdn.microsoft.com/library/windows/apps/mt181382) property.

[!code-cs[CaptureHdrPhotoAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCaptureHdrPhotoAsync)]

Most photography apps will want to encode a captured photo's rotation into the image file so that it can be displayed correctly by other apps and devices. This example shows the use of the helper class **CameraRotationHelper** to calculate the proper orientation for the file. This class is described and listed in full in the article [**Handle device orientation with MediaCapture**](handle-device-orientation-with-mediacapture.md).

The **SaveCapturedFrameAsync** helper method, which saves the image to disk, is discussed later in this article.

### Get optional reference frame

The HDR process captures multiple frames and then composites them into a single image after all of the frames have been captured. You can get access to a frame after it is captured but before the entire HDR process is complete by handling the [**OptionalReferencePhotoCaptured**](https://msdn.microsoft.com/library/windows/apps/mt181392) event. You don't need to do this if you are only interested in the final HDR photo result.

> [!IMPORTANT]
> [**OptionalReferencePhotoCaptured**](https://msdn.microsoft.com/library/windows/apps/mt181392) is not raised on devices that support hardware HDR and therefore do not generate reference frames. Your app should handle the case where this event is not raised.

Because the reference frame arrives out of context of the call to **CaptureAsync**, a mechanism is provided to pass context information to the **OptionalReferencePhotoCaptured** handler. First you should call an object that will contain your context information. The name and contents of this object is up to you. This example defines an object that has members to track the file name and camera orientation of the capture.

[!code-cs[AdvancedCaptureContext](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetAdvancedCaptureContext)]

Create a new instance of your context object, populate its members, and then pass it into the overload of [**CaptureAsync**](https://msdn.microsoft.com/library/windows/apps/mt181388) that accepts an object as a parameter.

[!code-cs[CaptureWithContext](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCaptureWithContext)]

In the [**OptionalReferencePhotoCaptured**](https://msdn.microsoft.com/library/windows/apps/mt181392) event handler, cast the [**Context**](https://msdn.microsoft.com/library/windows/apps/mt181405) property of the [**OptionalReferencePhotoCapturedEventArgs**](https://msdn.microsoft.com/library/windows/apps/mt181404) object to your context object class. This example modifies the file name to distinguish the reference frame image from the final HDR image and then calls the **SaveCapturedFrameAsync** helper method to save the image.

[!code-cs[OptionalReferencePhotoCaptured](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetOptionalReferencePhotoCaptured)]

### Receive a notification when all frames have been captured

The HDR photo capture has two steps. First, multiple frames are captured, and then the frames are processed into the final HDR image. You can't initiate another capture while the source HDR frames are still being captured, but you can initiate a capture after all of the frames have been captured but before the HDR post-processing is complete. The [**AllPhotosCaptured**](https://msdn.microsoft.com/library/windows/apps/mt181387) event is raised when the HDR captures are complete, letting you know that you can initiate another capture. A typical scenario is to disable your UI's capture button when HDR capture begins and then reenable it when **AllPhotosCaptured** is raised.

[!code-cs[AllPhotosCaptured](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetAllPhotosCaptured)]

### Clean up the AdvancedPhotoCapture object

When your app is done capturing, before disposing of the **MediaCapture** object, you should shut down the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) object by calling [**FinishAsync**](https://msdn.microsoft.com/library/windows/apps/mt181391) and setting your member variable to null.

[!code-cs[CleanUpAdvancedPhotoCapture](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCleanUpAdvancedPhotoCapture)]


## Low-light photo capture
Starting with Windows 10, version 1607, **AdvancedPhotoCapture** can be used to capture photos using a built-in algorithm that enhances the quality of photos captured in low-light settings. When you use the low-light feature of the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture) class, the system will evaluate the current scene and, if needed, apply an algorithm to compensate for low-light conditions. If the system determines that the algorithm is not needed, a regular capture is performed instead.

Before using low-light photo capture, determine if the device on which your app is currently running supports the technique by getting the **MediaCapture** object's [**VideoDeviceController**](https://msdn.microsoft.com/library/windows/apps/br226825) and then getting the [**AdvancedPhotoControl**](https://msdn.microsoft.com/library/windows/apps/mt147840) property. Check the video device controller's [**SupportedModes**](https://msdn.microsoft.com/library/windows/apps/mt147844) collection to see if it includes [**AdvancedPhotoMode.LowLight**](https://msdn.microsoft.com/library/windows/apps/mt147845). If it does, low-light capture using **AdvancedPhotoCapture** is supported. 
[!code-cs[LowLightSupported1](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetLowLightSupported1)]

[!code-cs[LowLightSupported2](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetLowLightSupported2)]

Next, declare a member variable to store the **AdvancedPhotoCapture** object. 

[!code-cs[DeclareAdvancedCapture](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetDeclareAdvancedCapture)]

In your app, after you have initialized the **MediaCapture** object, create an [**AdvancedPhotoCaptureSettings**](https://msdn.microsoft.com/library/windows/apps/mt147837) object and set the mode to [**AdvancedPhotoMode.LowLight**](https://msdn.microsoft.com/library/windows/apps/mt147845). Call the [**AdvancedPhotoControl**](https://msdn.microsoft.com/library/windows/apps/mt147840) object's [**Configure**](https://msdn.microsoft.com/library/windows/apps/mt147841) method, passing in the **AdvancedPhotoCaptureSettings** object you created.

Call the **MediaCapture** object's [**PrepareAdvancedPhotoCaptureAsync**](https://msdn.microsoft.com/library/windows/apps/mt181403), passing in an [**ImageEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/hh700993) object specifying the type of encoding the capture should use. 

[!code-cs[CreateAdvancedCaptureLowLightAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCreateAdvancedCaptureLowLightAsync)]

To capture a photo, call [**CaptureAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture.CaptureAsync).

[!code-cs[CaptureLowLight](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCaptureLowLight)]

Like the HDR example above, this example uses a helper class called **CameraRotationHelper** to determine the rotation value that should be encoded into the image so that it can be displayed properly by other apps and devices. This class is described and listed in full in the article [**Handle device orientation with MediaCapture**](handle-device-orientation-with-mediacapture.md).

The **SaveCapturedFrameAsync** helper method, which saves the image to disk, is discussed later in this article.

You can capture multiple low-light photos without reconfiguring the **AdvancedPhotoCapture** object, but when you are done capturing, you should call [**FinishAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture.FinishAsync) to clean up the object and associated resources.

[!code-cs[CleanUpAdvancedPhotoCapture](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCleanUpAdvancedPhotoCapture)]

## Working with AdvancedCapturedPhoto objects
[**AdvancedPhotoCapture.CaptureAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture.CaptureAsync) returns an [**AdvancedCapturedPhoto**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedCapturedPhoto) object representing the captured photo. This object exposes the [**Frame**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedCapturedPhoto.Frame) property which returns a [**CapturedFrame**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CapturedFrame) object representing the image. The [**OptionalReferencePhotoCaptured**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture.OptionalReferencePhotoCaptured) event also provides a **CapturedFrame** object in its event args. After you get an object of this type, there are a number of things you can do with it, including creating a [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.SoftwareBitmap) or saving the image to a file. 

## Get a SoftwareBitmap from a CapturedFrame
It's trivial to get a **SoftwareBitmap** from a **CapturedFrame** object by simply accessing the [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CapturedFrame.SoftwareBitmap) property of the object. However, most encoding formats do not support **SoftwareBitmap** with **AdvancedPhotoCapture**, so you should check and make sure the property is not null before using it.

[!code-cs[SoftwareBitmapFromCapturedFrame](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetSoftwareBitmapFromCapturedFrame)]

In the current release, the only encoding format that supports **SoftwareBitmap** for **AdvancedPhotoCapture** is uncompressed NV12. So, if you want to use this feature, you must specify that encoding when you call [**PrepareAdvancedPhotoCaptureAsync**](https://msdn.microsoft.com/library/windows/apps/mt181403). 

[!code-cs[UncompressedNv12](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetUncompressedNv12)]

Of course, you can always save the image to a file and then load the file into a **SoftwareBitmap** in a separate step. For more information about working with **SoftwareBitmap**, see [**Create, edit, and save bitmap images**](imaging.md).

## Save a CapturedFrame to a file
The [**CapturedFrame**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CapturedFrame) class implements the IInputStream interface, so it can be used as the input to a [**BitmapDecoder**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapDecoder), and then a [**BitmapEncoder**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapEncoder) can be used to write the image data to disk.

In the following example, a new folder in the user's pictures library is created and a file is created within this folder. Note that your app will need to include the **Pictures Library** capability in your app manifest file in order to access this directory. A file stream is then opened to the specified file. Next, the [**BitmapDecoder.CreateAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapDecoder.CreateAsync) is called to create the decoder from the **CapturedFrame**. Then [**CreateForTranscodingAsync**](https://msdn.microsoft.com/library/windows/apps/br226214) creates an encoder from the file stream and the decoder.

The next steps encode the orientation of the photo into the image file by using the [**BitmapProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapEncoder.BitmapProperties) of the encoder. For more information about handling orientation when capturing images, see [**Handle device orientation with MediaCapture**](handle-device-orientation-with-mediacapture.md).

Finally, the image is written to the file with a call to [**FlushAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapEncoder.FlushAsync).

[!code-cs[SaveCapturedFrameAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetSaveCapturedFrameAsync)]

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
