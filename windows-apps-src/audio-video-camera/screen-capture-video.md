---
title: Screen capture to video
description: This article describes how to use the Windows.Graphics.Capture APIs to make a video capture of an application window.
ms.date: 07/28/2020
ms.topic: article
dev_langs:
- csharp
keywords: windows 10, uwp, screen capture, video
ms.localizationpriority: medium
---
# Screen capture to video

This article describes how to use the Windows.Graphics.Capture APIs to make a video capture of an application window. For information on screen capturing still images, see [Screeen capture](screen-capture-video). To download a working sample that captures the screen to a video file that you can use to learn or as a starting point for your own app, see TBD.

## Overview of the video capture process
This article provides a walkthrough of all of the code needed to capture the contents of a window to a video file. There are a lot of steps in the process and it requires a fair amount of code, but the high-level structure of a screen recorder app isn't that complicated. The screen capture process has three main components:

- The [Windows.GraphicsCapture](/uwp/api/windows.graphics.capture) APIs do the work of actually grabbing the pixels from the screen. The [GraphicsCaptureItem](/uwp/api/windows.graphics.capture.graphicscaptureitem) class represents the window being captured. The [GraphicsCaptureSession](/uwp/api/windows.graphics.capture.graphicscapturesession) is used to start and stop the capture operation. The [Direct3D11CaptureFramePool](/uwp/api/windows.graphics.capture.direct3d11captureframepool) class maintains a buffer of frames into which the screen contents are copied.
- The [MediaStreamSource](/uwp/api/windows.media.core.mediastreamsource) class receives the captured frames and generates a video stream.
- The [MediaTranscoder](/uwp/api/windows.media.transcoding.mediatranscoder) class receives the stream produced by the **MediaStreamSource** and encodes it into a video file.

The example code shown below can be categorized into a few different tasks:

- **Initialization** - This includes instantiating the classes described above, picking a window to capture, and setting up the encoding parameters such as resolution and frame rate.
- **Event handlers and threading** - The primary driver of the main capture loop is the **MediaStreamSource** which requests frames periodically through the [SampleRequested](/uwp/api/windows.media.core.mediastreamsource.samplerequested) event. This example uses events to coordinate the requests for new frames between the different components of the pipeline. Synchronization is important for allowing frames to be captured and encoded simultaneously without resources being overwritten while they are being used.
- **Copying frames** - Perhaps the most technically complex task of the capture pipeline is copying captured frames from the frame buffer into a separate Direct3D surface that can be passed to the **MediaStreamSource**. This task is implemented in the **WaitForNewFrame** method shown in the walkthrough below. 

### About the Direct3D APIs
As stated above, the copying of each captured frame is probably the most complex part of the implementation shown in this article. At a low level, this operation is done using Direct3D. For this example, we are using the [SharpDX](http://sharpdx.org/) library to perform the Direct3D operations from C#. This library is no longer officially supported, but it was chosen because it's performance at low-level copy operations is well-suited for this scenario. We have tried to keep the Direct3D operations as discrete as possible to make it easier for you to substitute your own code or other libraries for these tasks.

## Setting up your project
The example code in this walkthrough was created using the C# UWP app project template in Visual Studio. In order to use the **Windows.Graphics.Capture** APIs in your app, you must include the **Graphics Capture** capability in your project. This example saves generated video files to the Videos Library on the device. To access this folder you must include the **Videos Library** capability.

To install the SharpDX Nuget package, in Visual Studio select **Manage Nuget Packages**. In the Browse tab search for the SharpDX.Direct3D11 package and click **Install**.

Note that in order to minimize the size of the code listings, the code in the walkthrough below omits explicit namespace references and the declaration of global variables, named with a leading underscore, "_".

## Setup for encoding

The **SetupEncoding** method described in this section initializes most of the classes that will be used to capture and encode video frames and sets the encoding parameters for the resulting video file output. The following sections discuss the initialization steps implemented in this method.

### Check for capture support
Before beginning the capture process, you need to call [GraphicsCaptureSession.IsSupported](/uwp/api/windows.graphics.capture.graphicscapturesession.issupported) to make sure that the screen capture feature is supported on the current device.

### Initialize Direct3D interfaces
This sample uses Direct3D to copy the pixels captured from the screen into a texture that is encoded as a video frame. The helper methods used to initialize the Direct3D interfaces, **CreateD3DDevice** and **CreateSharpDXDevice**, are shown later in this article.

### Initialze a GraphicsCaptureItem
A [GraphicsCaptureItem](/uwp/api/windows.graphics.capture.graphicscaptureitem) represents an item on the screen that is going to be captured, typically a window. Create a [GraphicsCapturePicker](/uwp/api/windows.graphics.capture.graphicscapturepicker) and call [PickSingleItemAsync](/uwp/api/windows.graphics.capture.graphicscapturepicker.picksingleitemasync) to allow the user to pick an item to capture.

### Create a blank texture template
Create a blank texture resource that will serve as the template for the textures that will be store each video frame. This texture can't be created until the **GraphicsCaptureItem** is created and we know it's dimensions. See the description of the **WaitForNewFrame** to see how this blank texture is used. The helper method for creating this texture is also shown later in this article.

### Create a MediaEncodingProfile and VideoStreamDescriptor
 In this example, an instance of the [MediaStreamSource](/uwp/api/windows.media.core.mediastreamsource) class will take images captured by the screen and encode them into a video stream. Then, the video stream will be transcoded into a video file by the [MediaTranscoder](/uwp/api/windows.media.transcoding.mediatranscoder) class. A [VideoStreamDecriptor](/uwp/api/windows.media.core.videostreamdescriptor) provides encoding parameters, such as resolution and frame rate, for the **MediaStreamSource**. The video file encoding parameters for the **MediaTranscoder** are specified with a [MediaEncodingProfile](/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile).

The size used for video encoding doesn't have to be the same as the size of the window being captured. A typical implementation will let the user set the desired encoding properties in the app UI or settings. To keep this example simple, the encoding settings are just hard-coded and use the capture item's actual dimensions.

### Create the MediaStreamSource and MediaTranscoder objects
As mentioned above, the **MediaStreamSource** object encodes individual frames into a video stream. Call the constructor for this class, passing in the **MediaEncodingProfile** created in the previous step. In this example, we set the buffer time to zero and register handlers for the [Starting](uwp/api/windows.media.core.mediastreamsource.starting) and [SampleRequested](/uwp/api/windows.media.core.mediastreamsource.samplerequested) events, which will be shown later in this article. Create a new instance of the **MediaTranscoder** class and enable hardware acceleration.

### Create an output file
The final step in this method is to create a file to which the video will be transcoded. For this example, we will just create a uniquely named file in the Videos Library folder on the device. Note that in order to access this folder, your app must specify the "Videos Library" capability in the app manifest. Once the file has been created, open it for read and write, and pass the resulting stream into the **EncodeAsync** method which will be shown next.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_SetupEncoding":::

## Start encoding
The **EncodeAsync** method in this example checks to make sure we aren't already recording, and if not, it calls the helper method **StartCapture** to begin capturing frames from the screen. This method is shown later in this article. Next, [PrepareMediaStreamSourceTranscodeAsync](/uwp/api/windows.media.transcoding.mediatranscoder.preparemediastreamsourcetranscodeasync) is called to get the **MediaTranscoder** ready to transcode the video stream produced by the **MediaStreamSource** object to the stream associated with our output file, using the encoding profile we created in the previous section. Once the transcoder has been prepared, call [TranscodeAsync](/uwp/api/windows.media.transcoding.preparetranscoderesult.transcodeasync) to start transcoding. For more information on using the **MediaTranscoder**, see [Transcode media files](/windows/uwp/audio-video-camera/transcode-media-files).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_EncodeAsync":::

## Handle MediaStreamSource events
The **MediaStreamSource** object takes frames that we capture from the screen and transforms them into a video stream that can be saved to a file using the **MediaTranscoder**. We pass the frames to the **MediaStreamSource** in the event handlers called by the object.

The [SampleRequested](/uwp/api/windows.media.core.mediastreamsource.samplerequested) event is raised when the **MediaStreamSource** is ready for a new video frame. After making sure we are currently recording, the helper method **WaitForNewFrame** is called to get a new frame captured from the screen. This method, shown later in this article, returns a [ID3D11Surface](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) object containing the captured frame. We've wrapped the interface in a structure so that we can also return the system time at which the frame was captured. Both the frame and the system time are passed into the [MediaStreamSample.CreateFromDirect3D11Surface](/uwp/api/windows.media.core.mediastreamsample.createfromdirect3d11surface) factory method and the resulting [MediaStreamSample](/uwp/api/windows.media.core.mediastreamsample) is set to the [MediaStreamSourceSampleRequest.Sample](MediaStreamSourceSampleRequest.Sample) property of the [MediaStreamSourceSampleRequestedEventArgs](/uwp/api/windows.media.core.mediastreamsourcesamplerequestedeventargs). This is how the captured frame is provided to the **MediaStreamSource**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_OnMediaStreamSourceSampleRequested":::

In the handler for the [Starting](/uwp/api/windows.media.core.mediastreamsource.starting) event, we call **WaitForNewFrame**, but only pass the system time the frame was captured to the [MediaStreamSourceStartingRequest.SetActualStartPosition](/uwp/api/windows.media.core.mediastreamsourcestartingrequest.setactualstartposition) method, which the **MediaStreamSource** uses to properly encode the timing of the subsequent frames.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_OnMediaStreamSourceStarting":::

## Start capturing
The **StartCapture** method shown in this step was called in a previous step from the **EncodeAsync** helper method. First, this method initializes up a set of event objects that are used to control the flow of the capture operation.

- **_multithread** is a helper class wrapping the SharpDX library's **Multithread** object that will be used to make sure that no other threads access the SharpDX texture while it's being copied.
- **_frameEvent** is used to signal that a new frame has been captured and can be passed to the **MediaStreamSource**
- **_closedEvent** signals that recording has stopped and that we shouldn't wait for any new frames.

The frame event and closed event are added to an array so we can wait for either one of them in the capture loop.

The rest of the *StartCapture** method sets up the Windows.Graphics.Capture APIs that will do the actual screen capturing. First, an event is registered for the **CaptureItem.Closed** event. Next, a [Direct3D11CaptureFramePool](/uwp/api/windows.graphics.capture.direct3d11captureframepool) is created, which allows multiple captured frames to be buffered at a time. The [CreateFreeThreaded](/uwp/api/windows.graphics.capture.direct3d11captureframepool.createfreethreaded) method is used to create the frame pool so that the [FrameArrived](/uwp/api/windows.graphics.capture.direct3d11captureframepool.framearrived) event is called on the pool's own worker thread rather than on the App's main thread. Next, a handler is registered for the **FrameArrived** event. Finally, a [GraphicsCaptureSession](/uwp/api/windows.graphics.capture.graphicscapturesession) is created for the selected **CaptureItem** and the capture of frames is initiated by calling [StartCapture](/uwp/api/windows.graphics.capture.graphicscapturesession).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_StartCapture":::

## Handle graphics capture events
The previous section we registered two handlers for graphics capture events and set up some events to help manage the flow of the capture loop.

The **FrameArrived** event is raised when the **Direct3D11CaptureFramePool** has a new captured frame available. In the handler for this event, call [TryGetNextFrame](/uwp/api/windows.graphics.capture.direct3d11captureframepool.trygetnextframe) on the sender to get the next captured frame. It's possible that this method will return null, but that will be handled in the **WaitForNewFrame** helper method shown below. After the frame is retrieved, we set the **_frameEvent** so that our capture loop knows there is a new frame available.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_OnFrameArrived":::

In the **Closed** event handler, we signal the **_closedEvent** so that the capture loop will know when to stop.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_OnClosed":::

## Wait for new frames
The **WaitForNewFrame** helper method described in this section is where the heavy-lifting of the capture loop occurs. Remember, this method is called from the **OnMediaStreamSourceSampleRequested** event handler whenever the **MediaStreamSource** is ready for a new frame to be added to the video stream. At a high-level, this function simply copies each screen-captured video frame from one Direct3D surface to another so that it can be passed into the **MediaStreamSource** for encoding while a new frame is being captured. This example uses the SharpDX library to perform the actual copy operation.

Before waiting for a new frame, the method disposes of any previous frame stored in the class variable, **_currentFrame**, and resets the **_frameEvent**. Then the method waits for either the **_frameEvent** or the **_closedEvent** to be signaled. If the closed event is set, then the app calls a helper method to cleanup the capture resources. This method is shown later in this article.

If the frame event is set, then we know that the **FrameArrived** event handler defined in the previous step has been called, and we begin the process of copying the captured frame data into a Direct3D 11 surface that will be passed to the **MediaStreamSource**.

This example uses a helper class, **SurfaceWithInfo**, which simply allows us to pass the video frame and the system time of the frame - both required by the **MediaStreamSource** - as a single object. The first step of the frame copy process is to instantiate this class and set the system time.

The next steps are the part of this example that relies specifically on the SharpDX library. The helper functions used here are defined at the end of this article. First we use the **MultiThreadLock** to make sure no other threads access the video frame buffer while we are making the copy. Next, we call the helper method **CreateSharpDXTexture2D** to create a SharpDX **Texture2D** object from the video frame. This will be the source texture for the copy operation. We create a copy of the texture description to use when we create our target texture, but the description is modified, setting the **BindFlags** to **RenderTarget** so that it can be written to. Setting the **CpuAccessFlags** to **None** allows the system to optimize the copy operation.

Next, the target texture for the copy operation is created using the new texture description. A SharpDX **ResourceRegion** is created to define the area of the texture that will be copied, in this case the whole texture. **CopyResource** is called to create a resource that is the same as the **_blankTexture** resource that was created earlier during initialization. **CopySubresourceRegion** performs the actual copy operation. Finally, **CreateDirect3DSurfaceFromSharpDXTexture** is called to create the **IDirect3DSurface** object that is returned from this method.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_WaitForNewFrame":::

## Stop capture and clean up resources
The **Stop** method provides a way to stop the capture operation. It simply sets the **_closedEvent**. The **WaitForNewFrame** method defined in the previous steps looks for this event and, if set, shuts down the capture operation.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_Stop":::

The **Cleanup** method is used to properly dispose of the resources that were created during the copy operation. This includes:

- The **Direct3D11CaptureFramePool** object used by the capture session
- The **GraphicsCaptureSession** and **GraphicsCaptureItem**
- The Direct3D and SharpDX devices
- The blank SharpDX texture and the **Direct3D11CaptureFrame** used for storing the current frame.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_Cleanup":::

## Helper wrapper classes
The following helper classes were defined to help with the example code in this article.

The **MultithreadLock** helper class wraps the SharpDX **Multithread** class that makes sure that other threads don't access the texture resources while being copied.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_MultithreadLock":::

**SurfaceWithInfo** is used to associate an **IDirect3DSurface** with a **SystemRelativeTime** representing the a captured frame and the time it was captured, respectively.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/MainPage.xaml.cs" id="snippet_SurfaceWithInfo":::

## Direct3D and SharpDX helper APIs
The following helper APIs are defined to abstract out the creation of Direct3D and SharpDX resources. Detailed explanations of these technologies is outside the scope of this article but the code is provided here to allow you to implement the example code shown in the walkthrough above

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/ScreenRecorderExample/cs/Direct3D11Helpers.cs" id="snippet_Direct3D11Helpers":::