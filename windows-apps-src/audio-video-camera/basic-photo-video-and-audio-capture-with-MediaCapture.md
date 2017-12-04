---
author: drewbatgit
ms.assetid: 66d0c3dc-81f6-4d9a-904b-281f8a334dd0
description: This article shows the simplest way to capture photos and video using the MediaCapture class.
title: Basic photo, video, and audio capture with MediaCapture
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Basic photo, video, and audio capture with MediaCapture


This article shows the simplest way to capture photos and video using the [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture) class. The **MediaCapture** class exposes a robust set of APIs that provide low-level control over the capture pipeline and enable advanced capture scenarios, but this article is intended to help you add basic media capture to your app quickly and easily. To learn about more of the features that  **MediaCapture** provides, see [**Camera**](camera.md).

If you simply want to capture a photo or video and don't intend to add any additional media capture features, or if you don't want to create your own camera UI, you may want to use the [**CameraCaptureUI**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CameraCaptureUI) class, which allows you to simply launch the Windows built-in camera app and receive the photo or video file that was captured. For more information, see [**Capture photos and video with Windows built-in camera UI**](capture-photos-and-video-with-cameracaptureui.md)

The code in this article was adapted from the [**Camera starter kit**](https://go.microsoft.com/fwlink/?linkid=619479) sample. You can download the sample to see the code used in context or to use the sample as a starting point for your own app.

## Add capability declarations to the app manifest

In order for your app to access a device's camera, you must declare that your app uses the *webcam* and *microphone* device capabilities. If you want to save captured photos and videos to the users's Pictures or Videos library, you must also declare the *picturesLibrary* and *videosLibrary* capability.

**To add capabilities to the app manifest**

1.  In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2.  Select the **Capabilities** tab.
3.  Check the box for **Webcam** and the box for **Microphone**.
4.  For access to the Pictures and Videos library check the boxes for **Pictures Library** and the box for **Videos Library**.


## Initialize the MediaCapture object
All of the capture methods described in this article require the first step of initializing the [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture) object by calling the constructor and then calling [**InitializeAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.InitializeAsync). Since the **MediaCapture** object will be accessed from multiple places in your app, declare a class variable to hold the object.  Implement a handler for the **MediaCapture** object's [**Failed**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.Failed) event to be notified if a capture operation fails.

[!code-cs[DeclareMediaCapture](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetDeclareMediaCapture)]

[!code-cs[InitMediaCapture](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetInitMediaCapture)]

## Set up the camera preview
It's possible to capture photos, videos, and audio using **MediaCapture** without showing the camera preview, but typically you want to show the preview stream so that the user can see what's being captured. Also, a few **MediaCapture** features require the preview stream to be running before they can be enbled, including auto focus, auto exposure, and auto white balance. To see how to set up the camera preview, see [**Display the camera preview**](simple-camera-preview-access.md).

## Capture a photo to a SoftwareBitmap
The [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.SoftwareBitmap) class was introduced in Windows 10 to provide a common representation of images across multiple features. If you want to capture a photo and then immediately use the captured image in your app, such as displaying it in XAML, instead of capturing to a file, then you should capture to a **SoftwareBitmap**. You still have the option of saving the image to disk later.

After initializing the **MediaCapture** object, you can capture a photo to a **SoftwareBitmap** using the [**LowLagPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagPhotoCapture) class. Get an instance of this class by calling [**PrepareLowLagPhotoCaptureAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.PrepareLowLagPhotoCaptureAsync), passing in an [**ImageEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the image format you want. [**CreateUncompressed**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.ImageEncodingProperties.CreateUncompressed) creates an uncompressed encoding with the specified pixel format. Capture a photo by calling [**CaptureAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagPhotoCapture.CaptureAsync), which returns a [**CapturedPhoto**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CapturedPhoto) object. Get a **SoftwareBitmap** by accessing the [**Frame**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CapturedPhoto.Frame) property and then the [**SoftwareBitmap**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CapturedFrame.SoftwareBitmap) property.

If you want, you can capture multiple photos by repeatedly calling **CaptureAsync**. When you are done capturing, call [**FinishAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture.FinishAsync) to shut down the **LowLagPhotoCapture** session and free up the associated resources. After calling **FinishAsync**, to begin capturing photos again you will need to call [**PrepareLowLagPhotoCaptureAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.PrepareLowLagPhotoCaptureAsync) again to reinitialize the capture session before calling [**CaptureAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagPhotoCapture.CaptureAsync).

[!code-cs[CaptureToSoftwareBitmap](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetCaptureToSoftwareBitmap)]

For information about working with the **SoftwareBitmap** object, including how to display one in a XAML page, see [**Create, edit, and save bitmap images**](imaging.md).

## Capture a photo to a file
A typical photography app will save a captured photo to disk or to cloud storage and will need to add metadata, such as photo orientation, to the file. The following example shows you how to capture an photo to a file. You still have the option of creating a **SoftwareBitmap** from the image file later. 

The technique shown in this example captures the photo to an in-memory stream and then transcode the photo from the stream to a file on disk. This example uses [**GetLibraryAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Storage.StorageLibrary.GetLibraryAsync) to get the user's pictures library and then the [**SaveFolder**](https://msdn.microsoft.com/library/windows/apps/Windows.Storage.StorageLibrary.SaveFolder) property to get a reference default save folder. Remember to add the **Pictures Library** capability to your app manifest to access this folder. [**CreateFileAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Storage.StorageFolder.CreateFileAsync) creates a new [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/Windows.Storage.StorageFile) to which the photo will be saved.

Create an [**InMemoryRandomAccessStream**](https://msdn.microsoft.com/library/windows/apps/Windows.Storage.Streams.InMemoryRandomAccessStream) and then call [**CapturePhotoToStreamAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.CapturePhotoToStreamAsync) to capture a photo to the stream, passing in the stream and an [**ImageEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the image format that should be used. You can create custom encoding properties by initializing the object yourself, but the class provides static methods, like [**ImageEncodingProperties.CreateJpeg**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.ImageEncodingProperties.CreateJpeg) for common encoding formats. Next, create a file stream to the output file by calling [**OpenAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Storage.StorageFile.OpenAsync). Create a [**BitmapDecoder**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapDecoder) to decode the image from the in memory stream and then create a [**BitmapEncoder**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapEncoder) to encode the image to file by calling [**CreateForTranscodingAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapEncoder.CreateForTranscodingAsync).

You can optionally create a [**BitmapPropertySet**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapPropertySet) object and then call [**SetPropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/br226252.aspx) on the image encoder to include metadata about the photo in the image file. For more information about encoding properties, see [**Image metadata**](image-metadata.md). Handling device orientation properly is essential for most photography apps. For more information, see [**Handle device orientation with MediaCapture**](handle-device-orientation-with-mediacapture.md).

Finally, call [**FlushAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.BitmapEncoder.FlushAsync) on the encoder object to transcode the photo from the in-memory stream to the file.

[!code-cs[CaptureToFile](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetCaptureToFile)]

For more information about working with files and folders, see [**Files, folders, and libraries**](https://msdn.microsoft.com/windows/uwp/files/index).

## Capture a video
Quickly add video capture to your app by using the [**LowLagMediaRecording**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording) class. First, declare a class variable to for the object.

[!code-cs[LowLagMediaRecording](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetLowLagMediaRecording)]

Next, create a **StorageFile** object to which the video will be saved. Note that to save to the user's video library, as shown in this example, you must add the **Videos Library** capability to your app manifest. Call [**PrepareLowLagRecordToStorageFileAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.PrepareLowLagRecordToStorageFileAsync) to initialize the media recording, passing in the storage file and a [**MediaEncodingProfile**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.MediaEncodingProfile) object specifying the encoding for the video. The class provides static methods, like [**CreateMp4**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.MediaEncodingProfile.CreateMp4), for creating common video encoding profiles.

Finally, call [**StartAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.StartAsync) to begin capturing video.

[!code-cs[StartVideoCapture](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetStartVideoCapture)]

To stop recording video, call [**StopAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.StopAsync).

[!code-cs[StopRecording](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetStopRecording)]

You can continue to call **StartAsync** and **StopAsync** to capture additional videos. When you are done capturing videos, call [**FinishAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.FinishAsync) to dispose of the capture session and clean up associated resources. After this call, you must call **PrepareLowLagRecordToStorageFileAsync** again to reinitialize the capture session before calling **StartAsync**.

[!code-cs[FinishAsync](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetFinishAsync)]

When capturing video, you should register a handler for the [**RecordLimitationExceeded**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.RecordLimitationExceeded) event of the **MediaCapture** object, which will be raised by the operating system if you surpass the limit for a single recording, currently three hours. In the handler for the event, you should finalize your recording by calling [**StopAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.StopAsync).

[!code-cs[RecordLimitationExceeded](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetRecordLimitationExceeded)]

[!code-cs[RecordLimitationExceededHandler](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetRecordLimitationExceededHandler)]

### Play and edit captured video files
Once you have captured a video to a file, you may want to load the file and play it back within your app's UI. You can do this using the **[MediaPlayerElement](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement)** XAML control and an associated **[MediaPlayer](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer)**. For information on playing media in a XAML page, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md).

You can also create a **[MediaClip](https://docs.microsoft.com/uwp/api/windows.media.editing.mediaclip)** object from a video file by calling **[CreateFromFileAsync](https://docs.microsoft.com/uwp/api/windows.media.editing.mediaclip#Windows_Media_Editing_MediaClip_CreateFromFileAsync_Windows_Storage_IStorageFile_)**.  A **[MediaComposition](https://docs.microsoft.com/uwp/api/windows.media.editing.mediacomposition)** provides basic video editing functionality like arranging the sequence of **MediaClip** objects, trimming video length, creating layers, adding background music, and applying video effects. For more information on working with media compositions, see [Media compositions and editing](media-compositions-and-editing.md).

## Pause and resume video recording
You can pause a video recording and then resume recording without creating a separate output file by calling [**PauseAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.PauseAsync) and then calling [**ResumeAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.ResumeAsync).

[!code-cs[PauseRecordingSimple](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetPauseRecordingSimple)]

[!code-cs[ResumeRecordingSimple](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetResumeRecordingSimple)]

Starting with Windows 10, version 1607, you can pause a video recording and receive the last frame captured before the recording was paused. You can then overlay this frame on the camera preview to allow the user to align the camera with the paused frame before resuming recording. Calling [**PauseWithResultAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.PauseWithResultAsync) returns a [**MediaCapturePauseResult**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapturePauseResult) object. The [**LastFrame**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapturePauseResult.LastFrame) property is a [**VideoFrame**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.VideoFrame) object representing the last frame. To display the frame in XAML, get the **SoftwareBitmap** representation of the video frame. Currently, only images in BGRA8 format with premultiplied or empty alpha channel are supported, so call [**Convert**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Imaging.SoftwareBitmap.Covert) if necessary to get the correct format.  Create a new [**SoftwareBitmapSource**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource) object and call [**SetBitmapAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource.SetBitmapAsync) to initialize it. Finally, set the **Source** property of a XAML [**Image**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.Image) control to display the image. For this trick to work, your image must be aligned with the **CaptureElement** control and should have an opacity value less than one. Don't forget that you can only modify the UI on the UI thread, so make this call inside [**RunAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Core.CoreDispatcher.RunAsync).

**PauseWithResultAsync** also returns the duration of the video that was recorded in the preceeding segment in case you need to track how much total time has been recorded.

[!code-cs[PauseCaptureWithResult](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetPauseCaptureWithResult)]

When you resume recording, you can set the source of the image to null and hide it.

[!code-cs[ResumeCaptureWithResult](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetResumeCaptureWithResult)]

Note that you can also get a result frame when you stop the video by calling [**StopWithResultAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.StopWithResultAsync).


## Capture audio 
You can quickly add audio capture to your app by using the same technique shown above for capturing video. The example below creates a **StorageFile** in the application data folder. Call [**PrepareLowLagRecordToStorageFileAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture.PrepareLowLagRecordToStorageFileAsync) to initialize the capture session, passing in the file and a [**MediaEncodingProfile**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.MediaEncodingProfile) which is generated in this example by the [**CreateMp3**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.MediaProperties.MediaEncodingProfile.CreateMp3) static method. To begin recording, call [**StartAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.StartAsync).

[!code-cs[StartAudioCapture](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetStartAudioCapture)]


Call [**StopAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagPhotoSequenceCapture.StopAsync) to stop the audio recording.

[!code-cs[StopRecording](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetStopRecording)]

You can call **StartAsync** and **StopAsync** multiple times to record several audio files. When you are done capturing audio, call [**FinishAsync**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.LowLagMediaRecording.FinishAsync) to dispose of the capture session and clean up associated resources. After this call, you must call **PrepareLowLagRecordToStorageFileAsync** again to reinitialize the capture session before calling **StartAsync**.

[!code-cs[FinishAsync](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetFinishAsync)]

## Related topics

* [Camera](camera.md)
* [Capture photos and video with Windows built-in camera UI](capture-photos-and-video-with-cameracaptureui.md)
* [Handle device orientation with MediaCapture](handle-device-orientation-with-mediacapture.md)
* [Create, edit, and save bitmap images](imaging.md)
* [Files, folders, and libraries](https://msdn.microsoft.com/windows/uwp/files/index)

