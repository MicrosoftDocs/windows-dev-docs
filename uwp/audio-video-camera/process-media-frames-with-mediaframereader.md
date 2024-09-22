---
ms.assetid: a128edc8-8a80-4645-ac29-908ede2d1c72
description: This article shows you how to use a MediaFrameReader with MediaCapture to get media frames from one or more available sources, including color, depth, and infrared cameras, audio devices, or even custom frame sources such as those that produce skeletal tracking frames.
title: Process media frames with MediaFrameReader
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Process media frames with MediaFrameReader

This article shows you how to use a [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) with [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) to get media frames from one or more available sources, including color, depth, and infrared cameras, audio devices, or even custom frame sources such as those that produce skeletal tracking frames. This feature is designed to be used by apps that perform real-time processing of media frames, such as augmented reality and depth-aware camera apps.

If you are interested in simply capturing video or photos, such as a typical photography app, then you probably want to use one of the other capture techniques supported by [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture). For a list of available media capture techniques and articles showing how to use them, see [**Camera**](camera.md).

> [!NOTE] 
> The features discussed in this article are only available starting with Windows 10, version 1607.

> [!NOTE] 
> There is a Universal Windows app sample that demonstrates using **MediaFrameReader** to display frames from different frame sources, including color, depth, and infrared cameras. For more information, see [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames).

> [!NOTE] 
> A new set of APIs for using **MediaFrameReader** with audio data were introduced in Windows 10, version 1803. For more information, see [Process audio frames with MediaFrameReader](process-audio-frames-with-mediaframereader.md).


## Setting up your project
As with any app that uses **MediaCapture**, you must declare that your app uses the *webcam* capability before attempting to access any camera device. If your app will capture from an audio device, you should also declare the *microphone* device capability. 

**Add capabilities to the app manifest**

1.  In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2.  Select the **Capabilities** tab.
3.  Check the box for **Webcam** and the box for **Microphone**.
4.  For access to the Pictures and Videos library check the boxes for **Pictures Library** and the box for **Videos Library**.

The example code in this article uses APIs from the following namespaces, in addition to those included by the default project template.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetFramesUsing":::

## Select frame sources and frame source groups
Many apps that process media frames need to get frames from multiple sources at once, such as a device's color and depth cameras. The [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) object represents a set of media frame sources that can be used simultaneously. Call the static method [**MediaFrameSourceGroup.FindAllAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.findallasync) to get a list of all of the groups of frame sources supported by the current device.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetFindAllAsync":::

You can also create a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) using [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher) and the value returned from [**MediaFrameSourceGroup.GetDeviceSelector**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.getdeviceselector) to receive notifications when the available frame source groups on the device changes, such as when an external camera is plugged in. For more information see [**Enumerate devices**](../devices-sensors/enumerate-devices.md).

A [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) has a collection of [**MediaFrameSourceInfo**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceInfo) objects that describe the frame sources included in the group. After retrieving the frame source groups available on the device, you can select the group that exposes the frame sources you are interested in.

The following example shows the simplest way to select a frame source group. This code simply loops over all of the available groups and then loops over each item in the [**SourceInfos**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.sourceinfos) collection. Each **MediaFrameSourceInfo** is checked to see if it supports the features we are seeking. In this case, the [**MediaStreamType**](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.mediastreamtype) property is checked for the value [**VideoPreview**](/uwp/api/Windows.Media.Capture.MediaStreamType), meaning the device provides a video preview stream, and the [**SourceKind**](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.sourcekind) property is checked for the value [**Color**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceKind), indicating that the source provides color frames.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetSimpleSelect":::

This method of identifying the desired frame source group and frame sources works for simple cases, but if you want to select frame sources based on more complex criteria, it can quickly become cumbersome. Another method is to use Linq syntax and anonymous objects to make the selection. The following example uses the **Select** extension method to transform the **MediaFrameSourceGroup** objects in the *frameSourceGroups* list into an anonymous object with two fields: *sourceGroup*, representing the group itself, and *colorSourceInfo*, which represents the color frame source in the group. The *colorSourceInfo* field is set to the result of **FirstOrDefault**, which selects the first object for which the provided predicate resolves to true. In this case, the predicate is true if the stream type is **VideoPreview**, the source kind is **Color**, and if the camera is on the front panel of the device.

From the list of anonymous objects returned from the query described above, the **Where** extension method is used to select only those objects where the *colorSourceInfo* field is not null. Finally, **FirstOrDefault** is called to select the first item in the list.

Now you can use the fields of the selected object to get references to the selected **MediaFrameSourceGroup** and the **MediaFrameSourceInfo** object representing the color camera. These will be used later to initialize the **MediaCapture** object and create a **MediaFrameReader** for the selected source. Finally, you should test to see if the source group is null, meaning the current device doesn't have your requested capture sources.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetSelectColor":::

The following example uses a similar technique as described above to select a source group that contains color, depth, and infrared cameras.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetColorInfraredDepth":::

> [!NOTE]
> Starting with Windows 10, version 1803, you can use the [**MediaCaptureVideoProfile**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfile) class to select a media frame source with a set of desired capabilities. For more information, see the section **Use video profiles to select a frame source** later in this article.


## Initialize the MediaCapture object to use the selected frame source group
The next step is to initialize the **MediaCapture** object to use the frame source group you selected in the previous step.

The **MediaCapture** object is typically used from multiple locations within your app, so you should declare a class member variable to hold it.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetDeclareMediaCapture":::

Create an instance of the **MediaCapture** object by calling the constructor. Next, create a [**MediaCaptureInitializationSettings**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings) object that will be used to initialize the **MediaCapture** object. In this example, the following settings are used:

* [**SourceGroup**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.sourcegroup) - This tells the system which source group you will be using to get frames. Remember that the source group defines a set of media frame sources that can be used simultaneously.
* [**SharingMode**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.sharingmode) - This tells the system whether you need exclusive control over the capture source devices. If you set this to  [**ExclusiveControl**](/uwp/api/Windows.Media.Capture.MediaCaptureSharingMode), it means that you can change the settings of the capture device, such as the format of the frames it produces, but this means that if another app already has exclusive control, your app will fail when it tries to initialize the media capture device. If you set this to [**SharedReadOnly**](/uwp/api/Windows.Media.Capture.MediaCaptureSharingMode), you can receive frames from the frame sources even if they are in use by another app, but you can't change the settings for the devices.
* [**MemoryPreference**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.memorypreference) - If you specify [**CPU**](/uwp/api/Windows.Media.Capture.MediaCaptureMemoryPreference), the system will use CPU memory which guarantees that when frames arrive, they will be available as [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects. If you specify [**Auto**](/uwp/api/Windows.Media.Capture.MediaCaptureMemoryPreference), the system will dynamically choose the optimal memory location to store frames. If the system chooses to use GPU memory, the media frames will arrive as an [**IDirect3DSurface**](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) object and not as a  **SoftwareBitmap**.
* [**StreamingCaptureMode**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.streamingcapturemode) - Set this to [**Video**](/uwp/api/Windows.Media.Capture.StreamingCaptureMode) to indicate that audio doesn't need to be streamed.

Call [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync) to initialize the **MediaCapture** with your desired settings. Be sure to call this within a *try* block in case initialization fails.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetInitMediaCapture":::

## Set the preferred format for the frame source
To set the preferred format for a frame source, you need to get a [**MediaFrameSource**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSource) object representing the source. You get this object by accessing the [**Frames**](/previous-versions/windows/apps/phone/jj207578(v=win.10)) dictionary of the initialized **MediaCapture** object, specifying the identifier of the frame source you want to use. This is why we saved the [**MediaFrameSourceInfo**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceInfo) object when we were selecting a frame source group.

The  [**MediaFrameSource.SupportedFormats**](/uwp/api/windows.media.capture.frames.mediaframesource.supportedformats) property contains a list of [**MediaFrameFormat**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameFormat) objects describing the supported formats for the frame source. Use the **Where** Linq extension method to select a format based on desired properties. In this example, a format is selected that has a width of 1080 pixels and can supply frames in 32-bit RGB format. The **FirstOrDefault** extension method selects the first entry in the list. If the selected format is null, then the requested format is not supported by the frame source. If the format is supported, you can request that the source use this format by calling [**SetFormatAsync**](../develop/index.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetGetPreferredFormat":::

## Create a frame reader for the frame source
To receive frames for a media frame source, use a [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetDeclareMediaFrameReader":::

Instantiate the frame reader by calling [**CreateFrameReaderAsync**](/uwp/api/windows.media.capture.mediacapture.createframereaderasync) on your initialized **MediaCapture** object. The first argument to this method is the frame source from which you want to receive frames. You can create a separate frame reader for each frame source you want to use. The second argument tells the system the output format in which you want frames to arrive. This can save you from having to do your own conversions to frames as they arrive. Note that if you specify a format that is not supported by the frame source, an exception will be thrown, so be sure that this value is in the [**SupportedFormats**](/uwp/api/windows.media.capture.frames.mediaframesource.supportedformats) collection.  

After creating the frame reader, register a handler for the [**FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event which is raised whenever a new frame is available from the source.

Tell the system to start reading frames from the source by calling [**StartAsync**](/uwp/api/windows.media.capture.frames.mediaframereader.startasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetCreateFrameReader":::

## Handle the frame arrived event
The [**MediaFrameReader.FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event is raised whenever a new frame is available. You can choose to process every frame that arrives or only use frames when you need them. Because the frame reader raises the event on its own thread, you may need to implement some synchronization logic to make sure that you aren't attempting to access the same data from multiple threads. This section shows you how to synchronize drawing color frames to an image control in a XAML page. This scenario addresses the additional synchronization constraint that requires all updates to XAML controls be performed on the UI thread.

The first step in displaying frames in XAML is to create an Image control. 

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml" id="SnippetImageElementXAML":::

In your code behind page, declare a class member variable of type **SoftwareBitmap** which will be used as a back buffer that all incoming images will be copied to. Note that the image data itself isn't copied, just the object references. Also, declare a boolean to track whether our UI operation is currently running.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetDeclareBackBuffer":::

Because the frames will arrive as **SoftwareBitmap** objects, you need to create a [**SoftwareBitmapSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource) object which allows you to use a **SoftwareBitmap** as the source for a XAML **Control**. You should set the image source somewhere in your code before you start the frame reader.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetImageElementSource":::

Now it's time to implement the **FrameArrived** event handler. When the handler is called, the *sender* parameter contains a reference to the **MediaFrameReader** object which raised the event. Call [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.mediaframereader.tryacquirelatestframe) on this object to attempt to get the latest frame. As the name implies, **TryAcquireLatestFrame** may not succeed in returning a frame. So, when you access the VideoMediaFrame and then SoftwareBitmap properties, be sure to test for null. In this example the null conditional operator ? is used to access the **SoftwareBitmap** and then the retrieved object is checked for null.

The **Image** control can only display images in BRGA8 format with either pre-multiplied or no alpha. If the arriving frame is not in that format, the static method [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) is used to convert the software bitmap to the correct format.

Next, the [**Interlocked.Exchange**](/dotnet/api/system.threading.interlocked.exchange#System_Threading_Interlocked_Exchange__1___0____0_) method is used to swap the reference of to arriving bitmap with the backbuffer bitmap. This method swaps these references in an atomic operation that is thread-safe. After swapping, the old backbuffer image, now in the *softwareBitmap* variable is disposed of to clean up its resources.

Next, the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) associated with the **Image** element is used to create a task that will run on the UI thread by calling [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync). Because the asynchronous tasks will be performed within the task, the lambda expression passed to **RunAsync** is declared with the *async* keyword.

Within the task, the *_taskRunning* variable is checked to make sure that only one instance of the task is running at a time. If the task isn't already running, *_taskRunning* is set to true to prevent the task from running again. In a *while* loop, **Interlocked.Exchange** is called to copy from the backbuffer into a temporary **SoftwareBitmap** until the backbuffer image is null. For each time the temporary bitmap is populated, the **Source** property of the **Image** is cast to a **SoftwareBitmapSource**, and then [**SetBitmapAsync**](/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource.setbitmapasync) is called to set the source of the image.

Finally, the *_taskRunning* variable is set back to false so that the task can be run again the next time the handler is called.

> [!NOTE] 
> If you access the [**SoftwareBitmap**](/uwp/api/windows.media.capture.frames.videomediaframe.softwarebitmap) or [**Direct3DSurface**](/uwp/api/windows.media.capture.frames.videomediaframe.direct3dsurface) objects provided by the [**VideoMediaFrame**](/uwp/api/windows.media.capture.frames.mediaframereference.videomediaframe) property of a [**MediaFrameReference**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReference), the system creates a strong reference to these objects, which means that they will not be disposed when you call [**Dispose**](/uwp/api/windows.media.capture.frames.mediaframereference.close) on the containing **MediaFrameReference**. You must explicitly call the **Dispose** method of the **SoftwareBitmap** or **Direct3DSurface** directly for the objects to be immediately disposed. Otherwise, the garbage collector will eventually free the memory for these objects, but you can't know when this will occur, and if the number of allocated bitmaps or surfaces exceeds the maximum amount allowed by the system, the flow of new frames will stop. You can copy retrieved frames, using the [**SoftwareBitmap.Copy**](/uwp/api/windows.graphics.imaging.softwarebitmap.copy) method for example, and then release the original frames to overcome this limitation. Also, if you create the **MediaFrameReader** using the overload [CreateFrameReaderAsync(Windows.Media.Capture.Frames.MediaFrameSource inputSource, System.String outputSubtype, Windows.Graphics.Imaging.BitmapSize outputSize)](/uwp/api/windows.media.capture.mediacapture.createframereaderasync#Windows_Media_Capture_MediaCapture_CreateFrameReaderAsync_Windows_Media_Capture_Frames_MediaFrameSource_System_String_Windows_Graphics_Imaging_BitmapSize_) or [CreateFrameReaderAsync(Windows.Media.Capture.Frames.MediaFrameSource inputSource, System.String outputSubtype)](/uwp/api/windows.media.capture.mediacapture.createframereaderasync#Windows_Media_Capture_MediaCapture_CreateFrameReaderAsync_Windows_Media_Capture_Frames_MediaFrameSource_System_String_), the frames returned are copies of the original frame data and so they do not cause frame acquisition to halt when they are retained. 


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetFrameArrived":::

## Cleanup resources
When you are done reading frames, be sure to stop the media frame reader by calling [**StopAsync**](/uwp/api/windows.media.capture.frames.mediaframereader.stopasync), unregistering the **FrameArrived** handler, and disposing of the **MediaCapture** object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetCleanup":::

For more information about cleaning up media capture objects when your application is suspended, see [**Display the camera preview**](simple-camera-preview-access.md).

## The FrameRenderer helper class
The Universal Windows [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames) provides a helper class that makes it easy to display the frames from color, infrared, and depth sources in your app. Typically, you will want to do something more with depth and infrared data than just display it to the screen, but this helper class is a helpful tool for demonstrating the frame reader feature and for debugging your own frame reader implementation.

The **FrameRenderer** helper class implements the following methods.

* **FrameRenderer** constructor - The constructor initializes the helper class to use the XAML [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image) element you pass in for displaying media frames.
* **ProcessFrame** - This method displays a media frame, represented by a [**MediaFrameReference**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReference), in the **Image** element you passed into the constructor. You should typically call this method from your [**FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event handler, passing in the frame returned by [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.mediaframereader.tryacquirelatestframe).
* **ConvertToDisplayableImage** - This methods checks the format of the media frame and, if necessary, converts it to a displayable format. For color images, this means making sure that the color format is BGRA8 and that the bitmap alpha mode is premultiplied. For depth or infrared frames, each scanline is processed to convert the depth or infrared values to a psuedocolor gradient, using the **PsuedoColorHelper** class that is also included in the sample and listed below.

> [!NOTE] 
> In order to do pixel manipulation on **SoftwareBitmap** images, you must access a native memory buffer. To do this, you must use the IMemoryBufferByteAccess COM interface included in the code listing below and you must update your project properties to allow compilation of unsafe code. For more information, see [Create, edit, and save bitmap images](imaging.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/FrameRenderer.cs" id="SnippetIMemoryBufferByteAccess":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/FrameRenderer.cs" id="SnippetFrameRenderer":::

## Use MultiSourceMediaFrameReader to get time-corellated frames from multiple sources
Starting with Windows 10, version 1607, you can use [**MultiSourceMediaFrameReader**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader) to receive time-corellated frames from multiple sources. This API makes it easier to do processing that requires frames from multiple sources that were taken in close temporal proximity, such as using the [**DepthCorrelatedCoordinateMapper**](/uwp/api/windows.media.devices.core.depthcorrelatedcoordinatemapper) class. One limitation of using this new method is that frame-arrived events are only raised at the rate of the slowest capture source. Extra frames from faster sources will be dropped. Also, because the system expects frames to arrive from different sources at different rates, it does not automatically recognize if a source has stopped generating frames altogether. The example code in this section shows how to use an event to create your own timeout logic that gets invoked if correlated frames don't arrive within an app-defined time limit.

The steps for using [**MultiSourceMediaFrameReader**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader) are similar to the steps for using [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) described previously in this article. This example will use a color source and a depth source. Declare some string variables to store the media frame source IDs that will be used to select frames from each source. Next, declare a [**ManualResetEventSlim**](/dotnet/api/system.threading.manualreseteventslim), a [**CancellationTokenSource**](/dotnet/api/system.threading.cancellationtokensource), and an [**EventHandler**](/dotnet/api/system.eventhandler) that will be used to implement timeout logic for the example. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetMultiFrameDeclarations":::

Using the techniques described previously in this article, query for a [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) that includes the color and depth sources required for this example scenario. After selecting the desired frame source group, get the [**MediaFrameSourceInfo**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceInfo) for each frame source.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetSelectColorAndDepth":::

Create and initialize a **MediaCapture** object, passing the selected frame source group in the initialization settings.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetMultiFrameInitMediaCapture":::

After initializing the **MediaCapture** object, retrieve [**MediaFrameSource**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSource) objects for the color and depth cameras. Store the ID for each source so that you can select the arriving frame for the corresponding source.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetGetColorAndDepthSource":::

Create and initialize the **MultiSourceMediaFrameReader** by calling [**CreateMultiSourceFrameReaderAsync**](/uwp/api/windows.media.capture.mediacapture.createmultisourceframereaderasync) and passing an array of frame sources that the reader will use. Register an event handler for the [**FrameArrived**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader.FrameArrived) event. This example creates an instance the **FrameRenderer** helper class, described previously in this article, to render frames to an **Image** control. Start the frame reader by calling [**StartAsync**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader.StartAsync).

Register an event handler for the **CorellationFailed** event declared earlier in the example. We will signal this event if one of the media frame sources being used stops producing frames. Finally, call [**Task.Run**](/dotnet/api/system.threading.tasks.task.run#System_Threading_Tasks_Task_Run_System_Action_) to call the timeout helper method, **NotifyAboutCorrelationFailure**, on a separate thread. The implementation of this method is shown later in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetInitMultiFrameReader":::

The **FrameArrived** event is raised whenever a new frame is available from all of the media frame sources that are managed by the **MultiSourceMediaFrameReader**. This means that the event will be raised on the cadence of the slowest media source. If one source produces multiple frames in the time that a slower source produces one frame, the extra frames from the fast source will be dropped. 

Get the [**MultiSourceMediaFrameReference**](/uwp/api/windows.media.capture.frames.multisourcemediaframereference) associated with the event by calling [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader.TryAcquireLatestFrame). Get the **MediaFrameReference** associated with each media frame source by calling [**TryGetFrameReferenceBySourceId**](/uwp/api/windows.media.capture.frames.multisourcemediaframereference.trygetframereferencebysourceid), passing in the ID strings stored when the frame reader was initialized.

Call the [**Set**](/dotnet/api/system.threading.manualreseteventslim.set#System_Threading_ManualResetEventSlim_Set) method of the **ManualResetEventSlim** object to signal that frames have arrived. We will check this event in the **NotifyCorrelationFailure** method that is running in a separate thread. 

Finally, perform any processing on the time-correlated media frames. This example simply displays the frame from the depth source.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetMultiFrameArrived":::

The **NotifyCorrelationFailure** helper method was run on a separate thread after the frame reader was started. In this method, check to see if the frame received event has been signaled. Remember, in the **FrameArrived** handler, we set this event whenever a set of correlated frames arrive. If the event hasn't been signaled for some app-defined period of time - 5 seconds is a reasonable value - and the task wasn't cancelled using the **CancellationToken**, then it's likely that one of the media frame sources has stopped reading frames. In this case you typically want to shut down the frame reader, so raise the app-defined **CorrelationFailed** event. In the handler for this event you can stop the frame reader and clean up it's associated resources as shown previously in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetNotifyCorrelationFailure":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetCorrelationFailure":::

## Use buffered frame acquisition mode to preserve the sequence of acquired frames
Starting with Windows 10, version 1709, you can set the **[AcquisitionMode](/uwp/api/windows.media.capture.frames.mediaframereader.AcquisitionMode)** property of a **MediaFrameReader** or **MultiSourceMediaFrameReader** to **Buffered** to preserve the sequence of frames passed into your app from the frame source.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetSetBufferedFrameAcquisitionMode":::

In the default acquisition mode, **Realtime**, if multiple frames are acquired from the source while your app is still handling the **FrameArrived** event for a previous frame, the system will send your app the most recently acquired frame and drop additional frames waiting in the buffer. This provides your app with the most recent available frame at all times. This is typically the most useful mode for realtime computer vision applications. 

In **Buffered** acquisition mode, the system will keep all frames in the buffer and provide them to your app through the **FrameArrived** event in the order received. Note that in this mode, when system's buffer for frames is filled, the system will stop acquiring new frames until your app completes the **FrameArrived** event for previous frames, freeing up more space in the buffer.

## Use MediaSource to display frames in a MediaPlayerElement
Starting with Windows, version 1709, you can display frames acquired from a **MediaFrameReader** directly in a **[MediaPlayerElement](/uwp/api/windows.ui.xaml.controls.mediaplayerelement)** control in your XAML page. This is achieved by using the **[MediaSource.CreateFromMediaFrameSource](/uwp/api/windows.media.core.mediasource.createfrommediaframesource)** to create **[MediaSource](/uwp/api/windows.media.core.mediasource)** object that can be used directly by a **[MediaPlayer](/uwp/api/windows.media.playback.mediaplayer)** associated with a **MediaPlayerElement**. For detailed information on working with **MediaPlayer** and **MediaPlayerElement**, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md).

The following code examples show you a simple implementation that displays the frames from a front-facing and back-facing camera simultaneously in a XAML page.

First, add two **MediaPlayerElement** controls to your XAML page.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml" id="SnippetMediaPlayerElement1XAML":::

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml" id="SnippetMediaPlayerElement2XAML":::

Next, using the techniques shown in previous sections in this article, select a **MediaFrameSourceGroup** that contains **MediaFrameSourceInfo** objects for color cameras on the front panel and back panel. Note that the **MediaPlayer** does not automatically convert frames from non-color formats, such as a depth or infrared data, into color data. Using other sensor types may produce unexpected results. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetMediaSourceSelectGroup":::

Initialize the **MediaCapture** object to use the selected **MediaFrameSourceGroup**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetMediaSourceInitMediaCapture":::

Finally, call **[MediaSource.CreateFromMediaFrameSource](/uwp/api/windows.media.core.mediasource.createfrommediaframesource)** to create a **MediaSource** for each frame source by using the **[Id](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.Id)** property of the associated **MediaFrameSourceInfo** object to select one of the frame sources in the **MediaCapture** object's **[FrameSources](/uwp/api/windows.media.capture.mediacapture.FrameSources)** collection. Initialize a new **MediaPlayer** object and assign it to a **MediaPlayerElement** by calling **[SetMediaPlayer](/uwp/api/windows.ui.xaml.controls.mediaplayerelement.MediaPlayer)**. Then set the **[Source](/uwp/api/windows.media.playback.mediaplayer.Source)** property to the newly created **MediaSource** object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetMediaSourceMediaPlayer":::

## Use video profiles to select a frame source

A camera profile, represented by a [**MediaCaptureVideoProfile**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfile) object, represents a set of capabilities that a particular capture device provides, such as frame rates, resolutions, or advanced features like HDR capture. A capture device may support multiple profiles, allowing you to select the one that is optimized for your capture scenario. Starting with Windows 10, version 1803, you can use **MediaCaptureVideoProfile** to select a media frame source with particular capabilities before initializing the **MediaCapture** object. The following example method looks for a video profile that supports HDR with Wide Color Gamut (WCG) and returns a **MediaCaptureInitializationSettings** object that can be used to initialize the **MediaCapture** to use the selected device and profile.

First, call [**MediaFrameSourceGroup.FindAllAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.findallasync) to get a list of all media frame source groups available on the current device. Loop through each source group and call [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) to get a list of all of the video profiles for the current source group that support the specified profile, in this case HDR with WCG photo. If a profile that meets the criteria is found, create a new **MediaCaptureInitializationSettings** object and set the **VideoProfile** to the select profile and the **VideoDeviceId** to the **Id** property of the current media frame source group.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.xaml.cs" id="SnippetGetSettingsWithProfile":::

For more information on using camera profiles, see [Camera profiles](camera-profiles.md).

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames)
 

 
