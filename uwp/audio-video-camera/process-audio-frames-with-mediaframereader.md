---
ms.assetid: D6A785C6-DF28-47E6-BDC1-7A7129EC40A0
description: This article shows you how to use a MediaFrameReader with MediaCapture to get AudioFrames containing audio data from a capture source.
title: Process audio frames with MediaFrameReader
ms.date: 04/18/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Process audio frames with MediaFrameReader

This article shows you how to use a [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) with [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) to get audio data from a media frame source. To learn about using a **MediaFrameReader** to get image data, such as from a color, infrared, or depth camera, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md). That article provides a general overview of the frame reader usage pattern and discusses some additional features of the **MediaFrameReader** class, such as using **MediaFrameSourceGroup** to retrieve frames from multiple sources at the same time. 

> [!NOTE] 
> The features discussed in this article are only available starting with Windows 10, version 1803.

> [!NOTE] 
> There is a Universal Windows app sample that demonstrates using **MediaFrameReader** to display frames from different frame sources, including color, depth, and infrared cameras. For more information, see [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames).

## Setting up your project
The process for acquiring audio frames is largely the same as acquiring other types of media frames. As with any app that uses **MediaCapture**, you must declare that your app uses the *webcam* capability before attempting to access any camera device. If your app will capture from an audio device, you should also declare the *microphone* device capability. 

**Add capabilities to the app manifest**

1.  In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2.  Select the **Capabilities** tab.
3.  Check the box for **Webcam** and the box for **Microphone**.
4.  For access to the Pictures and Videos library check the boxes for **Pictures Library** and the box for **Videos Library**.



## Select frame sources and frame source groups

The first step in capturing audio frames is to initialize a [**MediaFrameSource**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSource) representing the source of the audio data, such as a microphone or other audio capture device. To do this, you must create a new instance of the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object. For this example, the only initialization setting for the **MediaCapture** is setting the [**StreamingCaptureMode**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.streamingcapturemode) to indicate that we want to stream audio from the capture device. 

After calling [**MediaCapture.InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync), you can get the list of accessible media frame sources with the [**FrameSources**](/uwp/api/windows.media.capture.mediacapture.framesources) property. This example uses a Linq query to select all frame sources where the [**MediaFrameSourceInfo**](/uwp/api/windows.media.capture.frames.mediaframesourceinfo) describing the frame source has a  [**MediaStreamType**](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.mediastreamtype) of **Audio**, indicating that the media source produces audio data.

If the query returns one or more frame sources, you can check the [**CurrentFormat**](/uwp/api/windows.media.capture.frames.mediaframesource.currentformat) property to see if the source supports the audio format you desire - in this example, float audio data. Check the [**AudioEncodingProperties**](/uwp/api/windows.media.capture.frames.mediaframeformat.audioencodingproperties) to make sure the audio encoding you desire is supported by the source.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetInitAudioFrameSource":::

## Create and Start the MediaFrameReader

Get a new instance of **MediaFrameReader** by calling [**MediaCapture.CreateFrameReaderAsync**](/uwp/api/windows.media.capture.mediacapture.createframereaderasync#Windows_Media_Capture_MediaCapture_CreateFrameReaderAsync_Windows_Media_Capture_Frames_MediaFrameSource_), passing the **MediaFrameSource** object you selected in the previous step. By default, audio frames are obtained in buffered mode, making it less likely that frames will be dropped, although this can still occur if you are not processing audio frames fast enough and fill up the system's alloted memory buffer.

Register a handler for the [**MediaFrameReader.FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event, which is raised by the system when a new frame of audio data is available. Call [**StartAsync**](/uwp/api/windows.media.capture.frames.mediaframereader.startasync) to begin the acquisition of audio frames. If the frame reader fails to start, the status value returned from the call will have a value other than [**Success**](/uwp/api/windows.media.capture.frames.mediaframereaderstartstatus).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetCreateAudioFrameReader":::

In the **FrameArrived** event handler, call [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.mediaframereader.tryacquirelatestframe) on the **MediaFrameReader** object passed as the sender to the handler to attempt to retrieve a reference to the latest media frame. Note that this object can be null, so you should always check before using the object. The typs of media frame wrapped in the **MediaFrameReference** returned from **TryAcquireLatestFrame** depends on what type of frame source or sources you configured the frame reader to acquire. Since the frame reader in this example was set up to acquire audio frames, it gets the underlying frame using the [**AudioMediaFrame**](/uwp/api/windows.media.capture.frames.mediaframereference.audiomediaframe) property. 

This **ProcessAudioFrame** helper method in the example below shows how to get an [**AudioFrame**](/uwp/api/windows.media.audioframe) which provides information such as the timestamp of the frame and whether it is discontinuous from the **AudioMediaFrame** object. To read or process the audio sample data, you will need to get the [**AudioBuffer**](/uwp/api/windows.media.audiobuffer) object from the **AudioMediaFrame** object, create an [**IMemoryBufferReference**](/uwp/api/windows.foundation.imemorybufferreference), and then call the COM method **IMemoryBufferByteAccess::GetBuffer** to retrieve the data. See the note below the code listing for more information on accessing native buffers.

The format of the data depends on the frame source. In this example, when selecting a media frame source, we explicitly made certain that the selected frame source used a single channel of float data. The rest of the example code shows how to determine the duration and sample count for the audio data in the frame.  

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetProcessAudioFrame":::

> [!NOTE] 
> In order to do operate on the audio data, you must access a native memory buffer. To do this, you must use the **IMemoryBufferByteAccess** COM interface by including the code listing below. Operations on the native buffer must be performed in a method that uses the **unsafe** keyword. You also need to check the box to allow unsafe code in the **Build** tab of the **Project -> Properties** dialog.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/FrameRenderer.cs" id="SnippetIMemoryBufferByteAccess":::

## Additional information on using MediaFrameReader with audio data

You can retrieve the [**AudioDeviceController**](/uwp/api/Windows.Media.Devices.AudioDeviceController) associated with the audio frame source by accessing the [**MediaFrameSource.Controller**](/uwp/api/windows.media.capture.frames.mediaframesource.controller) property. This object can be used to get or set the stream properties of the capture device or to control the capture level. The following example mutes the audio device so that frames continue to be acquired by the frame reader, but all samples have value of 0.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetAudioDeviceControllerMute":::

You can use an [**AudioFrame**](/uwp/api/windows.media.audioframe) object to pass audio data captured by a media frame source into an [**AudioGraph**](/uwp/api/windows.media.audio.audiograph). Pass the frame into the [**AddFrame**](/uwp/api/windows.media.audio.audioframeinputnode.addframe) method of an [**AudioFrameInputNode**](/uwp/api/windows.media.audio.audioframeinputnode). For more information on using audio graphs to capture, process, and mix audio signals, see [Audio graphs](audio-graphs.md).

## Related topics

* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames)
* [Audio graphs](audio-graphs.md)
 
