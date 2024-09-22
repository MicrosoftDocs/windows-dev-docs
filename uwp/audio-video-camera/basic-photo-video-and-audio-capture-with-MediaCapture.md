---
ms.assetid: 66d0c3dc-81f6-4d9a-904b-281f8a334dd0
description: This article shows the simplest way to capture photos and video using the MediaCapture class.
title: Basic photo, video, and audio capture with MediaCapture
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Basic photo, video, and audio capture with MediaCapture


This article shows the simplest way to capture photos and video using the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class. The **MediaCapture** class exposes a robust set of APIs that provide low-level control over the capture pipeline and enable advanced capture scenarios, but this article is intended to help you add basic media capture to your app quickly and easily. To learn about more of the features that  **MediaCapture** provides, see [**Camera**](camera.md).

If you simply want to capture a photo or video and don't intend to add any additional media capture features, or if you don't want to create your own camera UI, you may want to use the [**CameraCaptureUI**](/uwp/api/Windows.Media.Capture.CameraCaptureUI) class, which allows you to simply launch the Windows built-in camera app and receive the photo or video file that was captured. For more information, see [**Capture photos and video with Windows built-in camera UI**](capture-photos-and-video-with-cameracaptureui.md)

The code in this article was adapted from the [**Camera starter kit**](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraStarterKit) sample. You can download the sample to see the code used in context or to use the sample as a starting point for your own app.

## Add capability declarations to the app manifest

In order for your app to access a device's camera, you must declare that your app uses the *webcam* and *microphone* device capabilities. If you want to save captured photos and videos to the users's Pictures or Videos library, you must also declare the *picturesLibrary* and *videosLibrary* capability.

**To add capabilities to the app manifest**

1.  In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2.  Select the **Capabilities** tab.
3.  Check the box for **Webcam** and the box for **Microphone**.
4.  For access to the Pictures and Videos library check the boxes for **Pictures Library** and the box for **Videos Library**.


## Initialize the MediaCapture object
All of the capture methods described in this article require the first step of initializing the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object by calling the constructor and then calling [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync). Since the **MediaCapture** object will be accessed from multiple places in your app, declare a class variable to hold the object.  Implement a handler for the **MediaCapture** object's [**Failed**](/uwp/api/windows.media.capture.mediacapture.failed) event to be notified if a capture operation fails.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetDeclareMediaCapture":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetInitMediaCapture":::

> [!NOTE]
> Windows allows users to grant or deny access to the device's camera in the Windows Settings app, under **Privacy & Security -> Camera**. When initializing the capture device, apps should check whether they have access to the camera and handle the case where access is denied by the user. For more information, see [Handle the Windows camera privacy setting](/windows/uwp/audio-video-camera/camera-privacy-setting).

## Set up the camera preview
It's possible to capture photos, videos, and audio using **MediaCapture** without showing the camera preview, but typically you want to show the preview stream so that the user can see what's being captured. Also, a few **MediaCapture** features require the preview stream to be running before they can be enabled, including auto focus, auto exposure, and auto white balance. To see how to set up the camera preview, see [**Display the camera preview**](simple-camera-preview-access.md).

## Capture a photo to a SoftwareBitmap
The [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) class was introduced in Windows 10 to provide a common representation of images across multiple features. If you want to capture a photo and then immediately use the captured image in your app, such as displaying it in XAML, instead of capturing to a file, then you should capture to a **SoftwareBitmap**. You still have the option of saving the image to disk later.

After initializing the **MediaCapture** object, you can capture a photo to a **SoftwareBitmap** using the [**LowLagPhotoCapture**](/uwp/api/Windows.Media.Capture.LowLagPhotoCapture) class. Get an instance of this class by calling [**PrepareLowLagPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagphotocaptureasync), passing in an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the image format you want. [**CreateUncompressed**](/uwp/api/windows.media.mediaproperties.imageencodingproperties.createuncompressed) creates an uncompressed encoding with the specified pixel format. Capture a photo by calling [**CaptureAsync**](/uwp/api/windows.media.capture.lowlagphotocapture.captureasync), which returns a [**CapturedPhoto**](/uwp/api/Windows.Media.Capture.CapturedPhoto) object. Get a **SoftwareBitmap** by accessing the [**Frame**](/uwp/api/windows.media.capture.capturedphoto.frame) property and then the [**SoftwareBitmap**](/uwp/api/windows.media.capture.capturedframe.softwarebitmap) property.

If you want, you can capture multiple photos by repeatedly calling **CaptureAsync**. When you are done capturing, call [**FinishAsync**](/uwp/api/windows.media.capture.advancedphotocapture.finishasync) to shut down the **LowLagPhotoCapture** session and free up the associated resources. After calling **FinishAsync**, to begin capturing photos again you will need to call [**PrepareLowLagPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagphotocaptureasync) again to reinitialize the capture session before calling [**CaptureAsync**](/uwp/api/windows.media.capture.lowlagphotocapture.captureasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetCaptureToSoftwareBitmap":::

Starting with Windows, version 1803, you can access the [**BitmapProperties**](/uwp/api/windows.media.capture.capturedframe.bitmapproperties) property of the **CapturedFrame** class returned from **CaptureAsync** to retrieve metadata about the captured photo. You can pass this data into a **BitmapEncoder** to save the metadata to a file. Previously, there was no way to access this data for uncompressed image formats. You can also access the [**ControlValues**](/uwp/api/windows.media.capture.capturedframe.controlvalues) property to retrieve a [**CapturedFrameControlValues**](/uwp/api/windows.media.capture.capturedframecontrolvalues) object that describes the control values, such as exposure and white balance, for the captured frame.

For information about using **BitmapEncoder** and about working with the **SoftwareBitmap** object, including how to display one in a XAML page, see [**Create, edit, and save bitmap images**](imaging.md). 

For more information on setting capture device control values, see [Capture device controls for photo and video](capture-device-controls-for-photo-and-video-capture.md).

Starting with Windows 10, version 1803, you can get the metadata, such as EXIF information, for photos captured in uncompressed format by accessing the [**BitmapProperties**](/uwp/api/windows.media.capture.capturedframe.bitmapproperties) property of the **CapturedFrame** returned by **MediaCapture**. In previous releases this data was only accessible in the header of photos captured to a compressed file format. You can provide this data to a [**BitmapEncoder**](/uwp/api/windows.graphics.imaging.bitmapencoder) when manually writing an image file. For more information on encoding bitmaps, see [Create, edit, and save bitmap images](imaging.md).  You can also access the 
frame control values, such as exposure and flash settings, used when the image was captured by accessing the [**ControlValues**](/uwp/api/windows.media.capture.capturedframe.controlvalues) property. For more information, see [Capture device controls for photo and video capture](capture-device-controls-for-photo-and-video-capture.md).

## Capture a photo to a file
A typical photography app will save a captured photo to disk or to cloud storage and will need to add metadata, such as photo orientation, to the file. The following example shows you how to capture a photo to a file. You still have the option of creating a **SoftwareBitmap** from the image file later. 

The technique shown in this example captures the photo to an in-memory stream and then transcode the photo from the stream to a file on disk. This example uses [**GetLibraryAsync**](/uwp/api/windows.storage.storagelibrary.getlibraryasync) to get the user's pictures library and then the [**SaveFolder**](/uwp/api/windows.storage.storagelibrary.savefolder) property to get a reference default save folder. Remember to add the **Pictures Library** capability to your app manifest to access this folder. [**CreateFileAsync**](/uwp/api/windows.storage.storagefolder.createfileasync) creates a new [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) to which the photo will be saved.

Create an [**InMemoryRandomAccessStream**](/uwp/api/Windows.Storage.Streams.InMemoryRandomAccessStream) and then call [**CapturePhotoToStreamAsync**](/uwp/api/windows.media.capture.mediacapture.capturephototostreamasync) to capture a photo to the stream, passing in the stream and an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the image format that should be used. You can create custom encoding properties by initializing the object yourself, but the class provides static methods, like [**ImageEncodingProperties.CreateJpeg**](/uwp/api/windows.media.mediaproperties.imageencodingproperties.createjpeg) for common encoding formats. Next, create a file stream to the output file by calling [**OpenAsync**](/uwp/api/windows.storage.storagefile.openasync). Create a [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) to decode the image from the in memory stream and then create a [**BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder) to encode the image to file by calling [**CreateForTranscodingAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.createfortranscodingasync).

You can optionally create a [**BitmapPropertySet**](/uwp/api/Windows.Graphics.Imaging.BitmapPropertySet) object and then call [**SetPropertiesAsync**](/uwp/api/windows.graphics.imaging.bitmapproperties.setpropertiesasync) on the image encoder to include metadata about the photo in the image file. For more information about encoding properties, see [**Image metadata**](image-metadata.md). Handling device orientation properly is essential for most photography apps. For more information, see [**Handle device orientation with MediaCapture**](handle-device-orientation-with-mediacapture.md).

Finally, call [**FlushAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.flushasync) on the encoder object to transcode the photo from the in-memory stream to the file.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetCaptureToFile":::

For more information about working with files and folders, see [**Files, folders, and libraries**](../files/index.md).

## Capture a video
Quickly add video capture to your app by using the [**LowLagMediaRecording**](/uwp/api/Windows.Media.Capture.LowLagMediaRecording) class. First, declare a class variable to for the object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetLowLagMediaRecording":::

Next, create a **StorageFile** object to which the video will be saved. Note that to save to the user's video library, as shown in this example, you must add the **Videos Library** capability to your app manifest. Call [**PrepareLowLagRecordToStorageFileAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagrecordtostoragefileasync) to initialize the media recording, passing in the storage file and a [**MediaEncodingProfile**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) object specifying the encoding for the video. The class provides static methods, like [**CreateMp4**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4), for creating common video encoding profiles.

Finally, call [**StartAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.startasync) to begin capturing video.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetStartVideoCapture":::

To stop recording video, call [**StopAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.stopasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetStopRecording":::

You can continue to call **StartAsync** and **StopAsync** to capture additional videos. When you are done capturing videos, call [**FinishAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.finishasync) to dispose of the capture session and clean up associated resources. After this call, you must call **PrepareLowLagRecordToStorageFileAsync** again to reinitialize the capture session before calling **StartAsync**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetFinishAsync":::

When capturing video, you should register a handler for the [**RecordLimitationExceeded**](/uwp/api/windows.media.capture.mediacapture.recordlimitationexceeded) event of the **MediaCapture** object, which will be raised by the operating system if you surpass the limit for a single recording, currently three hours. In the handler for the event, you should finalize your recording by calling [**StopAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.stopasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetRecordLimitationExceeded":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetRecordLimitationExceededHandler":::

### Play and edit captured video files
Once you have captured a video to a file, you may want to load the file and play it back within your app's UI. You can do this using the **[MediaPlayerElement](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement)** XAML control and an associated **[MediaPlayer](/uwp/api/windows.media.playback.mediaplayer)**. For information on playing media in a XAML page, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md).

You can also create a **[MediaClip](/uwp/api/windows.media.editing.mediaclip)** object from a video file by calling **[CreateFromFileAsync](/uwp/api/windows.media.editing.mediaclip.createfromfileasync)**.  A **[MediaComposition](/uwp/api/windows.media.editing.mediacomposition)** provides basic video editing functionality like arranging the sequence of **MediaClip** objects, trimming video length, creating layers, adding background music, and applying video effects. For more information on working with media compositions, see [Media compositions and editing](media-compositions-and-editing.md).

## Pause and resume video recording
You can pause a video recording and then resume recording without creating a separate output file by calling [**PauseAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.pauseasync) and then calling [**ResumeAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.resumeasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetPauseRecordingSimple":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetResumeRecordingSimple":::

Starting with Windows 10, version 1607, you can pause a video recording and receive the last frame captured before the recording was paused. You can then overlay this frame on the camera preview to allow the user to align the camera with the paused frame before resuming recording. Calling [**PauseWithResultAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.pausewithresultasync) returns a [**MediaCapturePauseResult**](/uwp/api/Windows.Media.Capture.MediaCapturePauseResult) object. The [**LastFrame**](/uwp/api/windows.media.capture.mediacapturepauseresult.lastframe) property is a [**VideoFrame**](/uwp/api/Windows.Media.VideoFrame) object representing the last frame. To display the frame in XAML, get the **SoftwareBitmap** representation of the video frame. Currently, only images in BGRA8 format with premultiplied or empty alpha channel are supported, so call [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) if necessary to get the correct format.  Create a new [**SoftwareBitmapSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource) object and call [**SetBitmapAsync**](/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource.setbitmapasync) to initialize it. Finally, set the **Source** property of a XAML [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image) control to display the image. For this trick to work, your image must be aligned with the **CaptureElement** control and should have an opacity value less than one. Don't forget that you can only modify the UI on the UI thread, so make this call inside [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync).

**PauseWithResultAsync** also returns the duration of the video that was recorded in the preceding segment in case you need to track how much total time has been recorded.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetPauseCaptureWithResult":::

When you resume recording, you can set the source of the image to null and hide it.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetResumeCaptureWithResult":::

Note that you can also get a result frame when you stop the video by calling [**StopWithResultAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.stopwithresultasync).


## Capture audio 
You can quickly add audio capture to your app by using the same technique shown above for capturing video. The example below creates a **StorageFile** in the application data folder. Call [**PrepareLowLagRecordToStorageFileAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagrecordtostoragefileasync) to initialize the capture session, passing in the file and a [**MediaEncodingProfile**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) which is generated in this example by the [**CreateMp3**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp3) static method. To begin recording, call [**StartAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.startasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetStartAudioCapture":::


Call [**StopAsync**](/uwp/api/windows.media.capture.lowlagphotosequencecapture.stopasync) to stop the audio recording.

## Related topics

* [Camera](camera.md)  
:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetStopRecording":::

You can call **StartAsync** and **StopAsync** multiple times to record several audio files. When you are done capturing audio, call [**FinishAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.finishasync) to dispose of the capture session and clean up associated resources. After this call, you must call **PrepareLowLagRecordToStorageFileAsync** again to reinitialize the capture session before calling **StartAsync**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetFinishAsync":::


## Detect and respond to audio level changes by the system
Starting with Windows 10, version 1803, your app can detect when the system lowers or mutes the audio level of your app's audio capture and audio render streams. For example, the system may mute your app's streams when it goes into the background. The [**AudioStateMonitor**](/uwp/api/windows.media.audio.audiostatemonitor) class allows you to register to receive an event when the system modifies the volume of an audio stream. Get an instance of **AudioStateMonitor** for monitoring audio capture streams by calling [**CreateForCaptureMonitoring**](/uwp/api/windows.media.audio.audiostatemonitor.createforcapturemonitoring#Windows_Media_Audio_AudioStateMonitor_CreateForCaptureMonitoring). Get an instance for monitoring audio render streams by calling [**CreateForRenderMonitoring**](/uwp/api/windows.media.audio.audiostatemonitor.createforrendermonitoring). Register a handler for the [**SoundLevelChanged**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevelchanged) event of each monitor to be notified when the audio for the corresponding stream category is changed by the system.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetAudioStateMonitorUsing":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetAudioStateVars":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetRegisterAudioStateMonitor":::

In the **SoundLevelChanged** handler for the capture stream, you can check the [**SoundLevel**](/uwp/api/windows.media.audio.audiostatemonitor.soundlevel) property of the **AudioStateMonitor** sender to determine the new sound level. Note that a capture stream should never be lowered, or "ducked", by the system. It should only ever be muted or switched back to full volume. If the audio stream is muted, you can stop a capture in progress. If the audio stream is restored to full volume, you can start capturing again. The following example uses some boolean class variables to track whether the app is currently capturing audio and if the capture was stopped due to the audio state change. These variables are used to determine when it's appropriate to programmatically stop or start audio capture.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetCaptureSoundLevelChanged":::

The following code example illustrates an implementation of the **SoundLevelChanged** handler for audio rendering. Depending on your app scenario, and the type of content you are playing, you may want to pause audio playback when the sound level is ducked. For more information on handling sound level changes for media playback, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetRenderSoundLevelChanged":::


* [Capture photos and video with Windows built-in camera UI](capture-photos-and-video-with-cameracaptureui.md)
* [Handle device orientation with MediaCapture](handle-device-orientation-with-mediacapture.md)
* [Create, edit, and save bitmap images](imaging.md)
* [Files, folders, and libraries](../files/index.md)
