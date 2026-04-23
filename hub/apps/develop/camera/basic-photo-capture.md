---
title: Basic photo, video, and audio capture with MediaCapture
description: Learn how to capture photos and video using the MediaCapture class.  
ms.topic: article
ms.date: 07/23/2024
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
#customer intent: As a developer, I want to access the camera in a Windows app using WinUI 3.
---

# Basic photo, video, and audio capture with MediaCapture in a WinUI 3 app

This article shows the simplest way to capture photos and video using the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class. The **MediaCapture** class exposes a robust set of APIs that provide low-level control over the capture pipeline and enable advanced capture scenarios, but this article is intended to help you add basic media capture to your app quickly and easily. To learn about more of the features that  **MediaCapture** provides, see [**Camera**](camera.md).

## Initialize the MediaCapture object

All of the capture methods described in this article require the first step of initializing the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object. This includes instantiating the object, selecting a capture device, setting initialization parameters, and then calling [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync). Typically camera apps will display the camera preview while capturing photos or video in their UI using the [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement). For a walkthrough of initializing **MediaCapture** and showing the preview in a XAML UI, see [Show the camera preview in a WinUI 3 app](camera-quickstart-winui3.md). The code examples in this article will assume that an initialized instance of **MediaCapture** has already been created.

## Capture a photo to a SoftwareBitmap

The [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) class provides a common representation of images across multiple features. If you want to capture a photo and then immediately use the captured image in your app, such as displaying it in XAML, instead of capturing to a file, then you should capture to a **SoftwareBitmap**. You still have the option of saving the image to disk later.

Capture a photo to a **SoftwareBitmap** using the [**LowLagPhotoCapture**](/uwp/api/Windows.Media.Capture.LowLagPhotoCapture) class. Get an instance of this class by calling [**PrepareLowLagPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagphotocaptureasync), passing in an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the image format you want. [**CreateUncompressed**](/uwp/api/windows.media.mediaproperties.imageencodingproperties.createuncompressed) creates an uncompressed encoding with the specified pixel format. Initiate photo capture by calling [**CaptureAsync**](/uwp/api/windows.media.capture.lowlagphotocapture.captureasync), which returns a [**CapturedPhoto**](/uwp/api/Windows.Media.Capture.CapturedPhoto) object. Get a **SoftwareBitmap** by accessing the [**Frame**](/uwp/api/windows.media.capture.capturedphoto.frame) property and then the [**SoftwareBitmap**](/uwp/api/windows.media.capture.capturedframe.softwarebitmap) property.

You can capture multiple photos by repeatedly calling **CaptureAsync**. When you are done capturing, call [**FinishAsync**](/uwp/api/windows.media.capture.advancedphotocapture.finishasync) to shut down the **LowLagPhotoCapture** session and free up the associated resources. After calling **FinishAsync**, to begin capturing photos again you will need to call [**PrepareLowLagPhotoCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagphotocaptureasync) again to reinitialize the capture session before calling [**CaptureAsync**](/uwp/api/windows.media.capture.lowlagphotocapture.captureasync).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraLowLagPhotoCapture":::

## Capture a photo to a memory stream

You can use **MediaCapture** to capture a photo to an in-memory stream, which you can then use to transcode the photo from the stream to a file on disk.

Create an [**InMemoryRandomAccessStream**](/uwp/api/Windows.Storage.Streams.InMemoryRandomAccessStream) and then call [**CapturePhotoToStreamAsync**](/uwp/api/windows.media.capture.mediacapture.capturephototostreamasync) to capture a photo to the stream, passing in the stream and an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object specifying the image format that should be used. You can create custom encoding properties by initializing the object yourself, but the class provides static methods, like [**ImageEncodingProperties.CreateJpeg**](/uwp/api/windows.media.mediaproperties.imageencodingproperties.createjpeg) for common encoding formats. 

Create a [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) to decode the image from the in memory stream. Create a [**BitmapEncoder**](/uwp/api/Windows.Graphics.Imaging.BitmapEncoder) to encode the image to file by calling [**CreateForTranscodingAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.createfortranscodingasync).

You can optionally create a [**BitmapPropertySet**](/uwp/api/Windows.Graphics.Imaging.BitmapPropertySet) object and then call [**SetPropertiesAsync**](/uwp/api/windows.graphics.imaging.bitmapproperties.setpropertiesasync) on the image encoder to include metadata about the photo in the image file. For more information about encoding properties, see [**Image metadata**](/windows/uwp/audio-video-camera/image-metadata).

Finally, call [**FlushAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.flushasync) on the encoder object to transcode the photo from the in-memory stream to the file.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraLowLagPhotoCapture":::


## Capture a video

Quickly add video capture to your app by using the [**LowLagMediaRecording**](/uwp/api/Windows.Media.Capture.LowLagMediaRecording) class.

First, the **LowLagMediaRecording** needs to persist while video is being captured, so declare a class variable to for the object.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraDeclareLowLagMediaRecording":::


Call [**PrepareLowLagRecordToStorageFileAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagrecordtostoragefileasync) to initialize the media recording, passing in the storage file and a [**MediaEncodingProfile**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) object specifying the encoding for the video. The class provides static methods, like [**CreateMp4**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4), for creating common video encoding profiles. Call [**StartAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.startasync) to begin capturing video.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraStartLowLagMediaRecording":::


To stop recording video, call [**StopAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.stopasync).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraStopLowLagMediaRecording":::

You can continue to call **StartAsync** and **StopAsync** to capture additional videos. When you are done capturing videos, call [**FinishAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.finishasync) to dispose of the capture session and clean up associated resources. After this call, you must call **PrepareLowLagRecordToStorageFileAsync** again to reinitialize the capture session before calling **StartAsync**.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraFinalizeLowLagMediaRecording":::

When capturing video, you should register a handler for the [**RecordLimitationExceeded**](/uwp/api/windows.media.capture.mediacapture.recordlimitationexceeded) event of the **MediaCapture** object, which will be raised by the operating system if you surpass the limit for a single recording, currently three hours. In the handler for the event, you should finalize your recording by calling [**StopAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.stopasync).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraRecordLimitationExceeded":::


You can pause a video recording and then resume recording without creating a separate output file by calling [**PauseAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.pauseasync) and then calling [**ResumeAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.resumeasync).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraPauseVideoCapture":::

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraResumeVideoCapture":::

Calling [**PauseWithResultAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.pausewithresultasync) returns a [**MediaCapturePauseResult**](/uwp/api/Windows.Media.Capture.MediaCapturePauseResult) object. The [**LastFrame**](/uwp/api/windows.media.capture.mediacapturepauseresult.lastframe) property is a [**VideoFrame**](/uwp/api/Windows.Media.VideoFrame) object representing the last frame. To display the frame in XAML, get the **SoftwareBitmap** representation of the video frame. Currently, only images in BGRA8 format with premultiplied or empty alpha channel are supported, so call [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) if necessary to get the correct format.  **PauseWithResultAsync** also returns the duration of the video that was recorded in the preceding segment in case you need to track how much total time has been recorded.

You can also get a result frame when you stop the video by calling [**StopWithResultAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.stopwithresultasync).

### Play and edit captured video files

Once you have captured a video to a file, you may want to load the file and play it back within your app's UI. You can do this using the **[MediaPlayerElement](/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement)** XAML control and an associated **[MediaPlayer](/uwp/api/windows.media.playback.mediaplayer)**. For information on playing media in a XAML page, see [Play audio and video with MediaPlayer](/windows/uwp/audio-video-camera/play-audio-and-video-with-mediaplayer).

[TBD - Is the MediaComposition framework supported / recommended for WinUI?]

You can also create a **[MediaClip](/uwp/api/windows.media.editing.mediaclip)** object from a video file by calling **[CreateFromFileAsync](/uwp/api/windows.media.editing.mediaclip.createfromfileasync)**.  A **[MediaComposition](/uwp/api/windows.media.editing.mediacomposition)** provides basic video editing functionality like arranging the sequence of **MediaClip** objects, trimming video length, creating layers, adding background music, and applying video effects. For more information on working with media compositions, see [Media compositions and editing](/windows/uwp/audio-video-camera/media-compositions-and-editing).


## Capture audio 

You can quickly add audio capture to your app by using the same technique shown above for capturing video. Call [**PrepareLowLagRecordToStorageFileAsync**](/uwp/api/windows.media.capture.mediacapture.preparelowlagrecordtostoragefileasync) to initialize the capture session, passing in the file and a [**MediaEncodingProfile**](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) which is generated in this example by the [**CreateMp3**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp3) static method. To begin recording, call [**StartAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.startasync).

[TBD - This code is throwing 'The request is invalid in the current state.']

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraStartAudioCapture":::


Call [**StopAsync**](/uwp/api/windows.media.capture.lowlagphotosequencecapture.stopasync) to stop the audio recording.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraStopAudioCapture":::

You can call **StartAsync** and **StopAsync** multiple times to record several audio files. When you are done capturing audio, call [**FinishAsync**](/uwp/api/windows.media.capture.lowlagmediarecording.finishasync) to dispose of the capture session and clean up associated resources. After this call, you must call **PrepareLowLagRecordToStorageFileAsync** again to reinitialize the capture session before calling **StartAsync**.

For information about detecting when the system changes the audio level of the audio capture stream, see [Detect and respond to audio level changes by the system](detect-audio-level-changes.md). 

