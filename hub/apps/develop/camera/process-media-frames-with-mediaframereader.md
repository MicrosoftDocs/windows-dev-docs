---
description: Learn how to use a MediaFrameReader with MediaCapture to get media frames from one or more available sources, including color, depth, and infrared cameras, audio devices, or even custom frame sources such as those that produce skeletal tracking frames.
title: Process media frames with MediaFrameReader
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, windows 11, winui3, camera
ms.localizationpriority: medium
---

# Process media frames with MediaFrameReader

This article shows you how to use a [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) with [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) to get media frames from one or more available sources, including color, depth, and infrared cameras, audio devices, or even custom frame sources such as those that produce skeletal tracking frames. This feature is designed to be used by apps that perform real-time processing of media frames, such as augmented reality and depth-aware camera apps.

If you are interested in simply capturing video or photos, such as a typical photography app, then you probably want to use one of the other capture techniques supported by [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture). For a list of available media capture techniques and articles showing how to use them, see [**Camera**](camera.md).

> [!NOTE] 
> The features discussed in this article are only available starting with Windows 10, version 1607.

> [!NOTE] 
> There is a Universal Windows app sample that demonstrates using **MediaFrameReader** to display frames from different frame sources, including color, depth, and infrared cameras. For more information, see [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/CameraFrames).

> [!NOTE] 
> A new set of APIs for using **MediaFrameReader** with audio data were introduced in Windows 10, version 1803. For more information, see [Process audio frames with MediaFrameReader](process-audio-frames-with-mediaframereader.md).


## Select frame sources and frame source groups

Many apps that process media frames need to get frames from multiple sources at once, such as a device's color and depth cameras. The [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) object represents a set of media frame sources that can be used simultaneously. Call the static method [**MediaFrameSourceGroup.FindAllAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.findallasync) to get a list of all of the groups of frame sources supported by the current device.

```csharp
var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();
```

You can also create a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) using [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher) and the value returned from [**MediaFrameSourceGroup.GetDeviceSelector**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.getdeviceselector) to receive notifications when the available frame source groups on the device changes, such as when an external camera is plugged in. For more information see [**Enumerate devices**](/windows/apps/develop/devices-sensors/enumerate-devices).

A [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) has a collection of [**MediaFrameSourceInfo**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceInfo) objects that describe the frame sources included in the group. After retrieving the frame source groups available on the device, you can select the group that exposes the frame sources you are interested in.

The following example shows the simplest way to select a frame source group. This code simply loops over all of the available groups and then loops over each item in the [**SourceInfos**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.sourceinfos) collection. Each **MediaFrameSourceInfo** is checked to see if it supports the features we are seeking. In this case, the [**MediaStreamType**](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.mediastreamtype) property is checked for the value [**VideoPreview**](/uwp/api/Windows.Media.Capture.MediaStreamType), meaning the device provides a video preview stream, and the [**SourceKind**](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.sourcekind) property is checked for the value [**Color**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceKind), indicating that the source provides color frames.

```csharp
var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();

MediaFrameSourceGroup selectedGroup = null;
MediaFrameSourceInfo colorSourceInfo = null;

foreach (var sourceGroup in frameSourceGroups)
{
    foreach (var sourceInfo in sourceGroup.SourceInfos)
    {
        if (sourceInfo.MediaStreamType == MediaStreamType.VideoPreview
            && sourceInfo.SourceKind == MediaFrameSourceKind.Color)
        {
            colorSourceInfo = sourceInfo;
            break;
        }
    }
    if (colorSourceInfo != null)
    {
        selectedGroup = sourceGroup;
        break;
    }
}
```

This method of identifying the desired frame source group and frame sources works for simple cases, but if you want to select frame sources based on more complex criteria, it can quickly become cumbersome. Another method is to use Linq syntax and anonymous objects to make the selection. The following example uses the **Select** extension method to transform the **MediaFrameSourceGroup** objects in the *frameSourceGroups* list into an anonymous object with two fields: *sourceGroup*, representing the group itself, and *colorSourceInfo*, which represents the color frame source in the group. The *colorSourceInfo* field is set to the result of **FirstOrDefault**, which selects the first object for which the provided predicate resolves to true. In this case, the predicate is true if the stream type is **VideoPreview**, the source kind is **Color**, and if the camera is on the front panel of the device.

From the list of anonymous objects returned from the query described above, the **Where** extension method is used to select only those objects where the *colorSourceInfo* field is not null. Finally, **FirstOrDefault** is called to select the first item in the list.

Now you can use the fields of the selected object to get references to the selected **MediaFrameSourceGroup** and the **MediaFrameSourceInfo** object representing the color camera. These will be used later to initialize the **MediaCapture** object and create a **MediaFrameReader** for the selected source. Finally, you should test to see if the source group is null, meaning the current device doesn't have your requested capture sources.

```csharp
var selectedGroupObjects = frameSourceGroups.Select(group =>
   new
   {
       sourceGroup = group,
       colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
       {
           // On Xbox/Kinect, omit the MediaStreamType and EnclosureLocation tests
           return sourceInfo.MediaStreamType == MediaStreamType.VideoPreview
           && sourceInfo.SourceKind == MediaFrameSourceKind.Color
           && sourceInfo.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front;
       })

   }).Where(t => t.colorSourceInfo != null)
   .FirstOrDefault();

MediaFrameSourceGroup selectedGroup = selectedGroupObjects?.sourceGroup;
MediaFrameSourceInfo colorSourceInfo = selectedGroupObjects?.colorSourceInfo;

if (selectedGroup == null)
{
    return;
}
```

The following example uses a similar technique as described above to select a source group that contains color, depth, and infrared cameras.

```csharp
var allGroups = await MediaFrameSourceGroup.FindAllAsync();
var eligibleGroups = allGroups.Select(g => new
{
    Group = g,

    // For each source kind, find the source which offers that kind of media frame,
    // or null if there is no such source.
    SourceInfos = new MediaFrameSourceInfo[]
    {
        g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Color),
        g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Depth),
        g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Infrared),
    }
}).Where(g => g.SourceInfos.Any(info => info != null)).ToList();

if (eligibleGroups.Count == 0)
{
    System.Diagnostics.Debug.WriteLine("No source group with color, depth or infrared found.");
    return;
}

var selectedGroupIndex = 0; // Select the first eligible group
MediaFrameSourceGroup selectedGroup = eligibleGroups[selectedGroupIndex].Group;
MediaFrameSourceInfo colorSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[0];
MediaFrameSourceInfo infraredSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[1];
MediaFrameSourceInfo depthSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[2];
```

> [!NOTE]
> Starting with Windows 10, version 1803, you can use the [**MediaCaptureVideoProfile**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfile) class to select a media frame source with a set of desired capabilities. For more information, see the section **Use video profiles to select a frame source** later in this article.


## Initialize the MediaCapture object to use the selected frame source group

The next step is to initialize the **MediaCapture** object to use the frame source group you selected in the previous step. Create an instance of the **MediaCapture** object by calling the constructor. Next, create a [**MediaCaptureInitializationSettings**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings) object that will be used to initialize the **MediaCapture** object. In this example, the following settings are used:

* [**SourceGroup**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.sourcegroup) - This tells the system which source group you will be using to get frames. Remember that the source group defines a set of media frame sources that can be used simultaneously.
* [**SharingMode**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.sharingmode) - This tells the system whether you need exclusive control over the capture source devices. If you set this to  [**ExclusiveControl**](/uwp/api/Windows.Media.Capture.MediaCaptureSharingMode), it means that you can change the settings of the capture device, such as the format of the frames it produces, but this means that if another app already has exclusive control, your app will fail when it tries to initialize the media capture device. If you set this to [**SharedReadOnly**](/uwp/api/Windows.Media.Capture.MediaCaptureSharingMode), you can receive frames from the frame sources even if they are in use by another app, but you can't change the settings for the devices.
* [**MemoryPreference**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.memorypreference) - If you specify [**CPU**](/uwp/api/Windows.Media.Capture.MediaCaptureMemoryPreference), the system will use CPU memory which guarantees that when frames arrive, they will be available as [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects. If you specify [**Auto**](/uwp/api/Windows.Media.Capture.MediaCaptureMemoryPreference), the system will dynamically choose the optimal memory location to store frames. If the system chooses to use GPU memory, the media frames will arrive as an [**IDirect3DSurface**](/uwp/api/Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface) object and not as a  **SoftwareBitmap**.
* [**StreamingCaptureMode**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.streamingcapturemode) - Set this to [**Video**](/uwp/api/Windows.Media.Capture.StreamingCaptureMode) to indicate that audio doesn't need to be streamed.

Call [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync) to initialize the **MediaCapture** with your desired settings. Be sure to call this within a *try* block in case initialization fails.

```csharp
m_mediaCapture = new MediaCapture();

var settings = new MediaCaptureInitializationSettings()
{
    SourceGroup = selectedGroup,
    SharingMode = MediaCaptureSharingMode.ExclusiveControl,
    MemoryPreference = MediaCaptureMemoryPreference.Cpu,
    StreamingCaptureMode = StreamingCaptureMode.Video
};
try
{
    await m_mediaCapture.InitializeAsync(settings);
}
catch (Exception ex)
{
    System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed: " + ex.Message);
    return;
}
```

## Set the preferred format for the frame source

To set the preferred format for a frame source, you need to get a [**MediaFrameSource**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSource) object representing the source. You get this object by accessing the [**Frames**](/previous-versions/windows/apps/phone/jj207578(v=win.10)) dictionary of the initialized **MediaCapture** object, specifying the identifier of the frame source you want to use. This is why we saved the [**MediaFrameSourceInfo**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceInfo) object when we were selecting a frame source group.

The  [**MediaFrameSource.SupportedFormats**](/uwp/api/windows.media.capture.frames.mediaframesource.supportedformats) property contains a list of [**MediaFrameFormat**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameFormat) objects describing the supported formats for the frame source. In this example, a format is selected that has a width of 1080 pixels and can supply frames in 32-bit RGB format. The **FirstOrDefault** extension method selects the first entry in the list. If the selected format is null, then the requested format is not supported by the frame source. If the format is supported, you can request that the source use this format by calling [**SetFormatAsync**](/uwp/api/windows.media.capture.frames.mediaframesource.setformatasync).

```csharp
var colorFrameSource = m_mediaCapture.FrameSources[colorSourceInfo.Id];
var preferredFormat = colorFrameSource.SupportedFormats.Where(format =>
{
    return format.VideoFormat.Width >= 1080
    && format.Subtype == MediaEncodingSubtypes.Argb32;

}).FirstOrDefault();

if (preferredFormat == null)
{
    // Our desired format is not supported
    return;
}

await colorFrameSource.SetFormatAsync(preferredFormat);
```

## Create a frame reader for the frame source

To receive frames for a media frame source, use a [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader).

```csharp
MediaFrameReader m_mediaFrameReader;
```

Instantiate the frame reader by calling [**CreateFrameReaderAsync**](/uwp/api/windows.media.capture.mediacapture.createframereaderasync) on your initialized **MediaCapture** object. The first argument to this method is the frame source from which you want to receive frames. You can create a separate frame reader for each frame source you want to use. The second argument tells the system the output format in which you want frames to arrive. This can save you from having to do your own conversions to frames as they arrive. Note that if you specify a format that is not supported by the frame source, an exception will be thrown, so be sure that this value is in the [**SupportedFormats**](/uwp/api/windows.media.capture.frames.mediaframesource.supportedformats) collection.  

After creating the frame reader, register a handler for the [**FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event which is raised whenever a new frame is available from the source.

Tell the system to start reading frames from the source by calling [**StartAsync**](/uwp/api/windows.media.capture.frames.mediaframereader.startasync).

```csharp
m_mediaFrameReader = await m_mediaCapture.CreateFrameReaderAsync(colorFrameSource, MediaEncodingSubtypes.Argb32);
m_mediaFrameReader.FrameArrived += ColorFrameReader_FrameArrived;
await m_mediaFrameReader.StartAsync();
```

## Handle the frame arrived event

The [**MediaFrameReader.FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event is raised whenever a new frame is available. You can choose to process every frame that arrives or only use frames when you need them. Because the frame reader raises the event on its own thread, you may need to implement some synchronization logic to make sure that you aren't attempting to access the same data from multiple threads. This section shows you how to synchronize drawing color frames to an image control in a XAML page. This scenario addresses the additional synchronization constraint that requires all updates to XAML controls be performed on the UI thread.

The first step in displaying frames in XAML is to create an Image control. 

```xml
<Image x:Name="iFrameReaderImageControl" MaxWidth="300" MaxHeight="200"/>
```

In your code behind page, declare a class member variable of type **SoftwareBitmap** which will be used as a back buffer that all incoming images will be copied to. Note that the image data itself isn't copied, just the object references. Also, declare a boolean to track whether our UI operation is currently running.

```csharp
private SoftwareBitmap backBuffer;
private bool taskRunning = false;
```

Because the frames will arrive as **SoftwareBitmap** objects, you need to create a [**SoftwareBitmapSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource) object which allows you to use a **SoftwareBitmap** as the source for a XAML **Control**. You should set the image source somewhere in your code before you start the frame reader.

```csharp
iFrameReaderImageControl.Source = new SoftwareBitmapSource();
```

Now it's time to implement the **FrameArrived** event handler. When the handler is called, the *sender* parameter contains a reference to the **MediaFrameReader** object which raised the event. Call [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.mediaframereader.tryacquirelatestframe) on this object to attempt to get the latest frame. As the name implies, **TryAcquireLatestFrame** may not succeed in returning a frame. So, when you access the VideoMediaFrame and then SoftwareBitmap properties, be sure to test for null. In this example the null conditional operator ? is used to access the **SoftwareBitmap** and then the retrieved object is checked for null.

The **Image** control can only display images in BRGA8 format with either pre-multiplied or no alpha. If the arriving frame is not in that format, the static method [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) is used to convert the software bitmap to the correct format.

Next, the [**Interlocked.Exchange**](/dotnet/api/system.threading.interlocked.exchange) method is used to swap the reference of to arriving bitmap with the backbuffer bitmap. This method swaps these references in an atomic operation that is thread-safe. After swapping, the old backbuffer image, now in the *softwareBitmap* variable is disposed of to clean up its resources.

Next, the [**DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) associated with the UI thread is used to queue work that will run on the UI thread by calling [**TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue). Because asynchronous operations are performed within the queued callback, the lambda expression passed to **TryEnqueue** is declared with the *async* keyword.

Within the queued callback, the *_taskRunning* variable is checked to make sure that only one instance of the task is running at a time. If the task isn't already running, *_taskRunning* is set to true to prevent the task from running again. In a *while* loop, **Interlocked.Exchange** is called to copy from the backbuffer into a temporary **SoftwareBitmap** until the backbuffer image is null. For each time the temporary bitmap is populated, the **Source** property of the **Image** is cast to a **SoftwareBitmapSource**, and then [**SetBitmapAsync**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource.setbitmapasync) is called to set the source of the image.

Finally, the *_taskRunning* variable is set back to false so that the task can be run again the next time the handler is called.

> [!NOTE] 
> If you access the [**SoftwareBitmap**](/uwp/api/windows.media.capture.frames.videomediaframe.softwarebitmap) or [**Direct3DSurface**](/uwp/api/windows.media.capture.frames.videomediaframe.direct3dsurface) objects provided by the [**VideoMediaFrame**](/uwp/api/windows.media.capture.frames.mediaframereference.videomediaframe) property of a [**MediaFrameReference**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReference), the system creates a strong reference to these objects, which means that they will not be disposed when you call [**Dispose**](/uwp/api/windows.media.capture.frames.mediaframereference.close) on the containing **MediaFrameReference**. You must explicitly call the **Dispose** method of the **SoftwareBitmap** or **Direct3DSurface** directly for the objects to be immediately disposed. Otherwise, the garbage collector will eventually free the memory for these objects, but you can't know when this will occur, and if the number of allocated bitmaps or surfaces exceeds the maximum amount allowed by the system, the flow of new frames will stop. You can copy retrieved frames, using the [**SoftwareBitmap.Copy**](/uwp/api/windows.graphics.imaging.softwarebitmap.copy) method for example, and then release the original frames to overcome this limitation. Also, if you create the **MediaFrameReader** using the overload [CreateFrameReaderAsync(Windows.Media.Capture.Frames.MediaFrameSource inputSource, System.String outputSubtype, Windows.Graphics.Imaging.BitmapSize outputSize)](/uwp/api/windows.media.capture.mediacapture.createframereaderasync) or [CreateFrameReaderAsync(Windows.Media.Capture.Frames.MediaFrameSource inputSource, System.String outputSubtype)](/uwp/api/windows.media.capture.mediacapture.createframereaderasync), the frames returned are copies of the original frame data and so they do not cause frame acquisition to halt when they are retained. 


```csharp
private void ColorFrameReader_FrameArrived(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
{
    var mediaFrameReference = sender.TryAcquireLatestFrame();
    var videoMediaFrame = mediaFrameReference?.VideoMediaFrame;
    var softwareBitmap = videoMediaFrame?.SoftwareBitmap;

    if (softwareBitmap != null)
    {
        if (softwareBitmap.BitmapPixelFormat != Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8 ||
            softwareBitmap.BitmapAlphaMode != Windows.Graphics.Imaging.BitmapAlphaMode.Premultiplied)
        {
            softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
        }

        // Swap the processed frame to _backBuffer and dispose of the unused image.
        softwareBitmap = Interlocked.Exchange(ref backBuffer, softwareBitmap);
        softwareBitmap?.Dispose();

        // Changes to XAML ImageElement must happen on UI thread through Dispatcher
        var task = iFrameReaderImageControl.DispatcherQueue.TryEnqueue(
            async () =>
            {
                // Don't let two copies of this task run at the same time.
                if (taskRunning)
                {
                    return;
                }
                taskRunning = true;

                // Keep draining frames from the backbuffer until the backbuffer is empty.
                SoftwareBitmap latestBitmap;
                while ((latestBitmap = Interlocked.Exchange(ref backBuffer, null)) != null)
                {
                    var imageSource = (SoftwareBitmapSource)iFrameReaderImageControl.Source;
                    await imageSource.SetBitmapAsync(latestBitmap);
                    latestBitmap.Dispose();
                }

                taskRunning = false;
            });
    }

    if (mediaFrameReference != null)
    {
        mediaFrameReference.Dispose();
    }
}
```

## Cleanup resources

When you are done reading frames, be sure to stop the media frame reader by calling [**StopAsync**](/uwp/api/windows.media.capture.frames.mediaframereader.stopasync), unregistering the **FrameArrived** handler, and disposing of the **MediaCapture** object.

```csharp
await m_mediaFrameReader.StopAsync();
m_mediaFrameReader.FrameArrived -= ColorFrameReader_FrameArrived;
m_mediaCapture.Dispose();
m_mediaCapture = null;
```

For more information about cleaning up media capture objects when your application is suspended, see [**Show the camera preview in a WinUI app**](camera-quickstart-winui3.md).

## The FrameRenderer helper class

This section provides the full code listing for a provides a helper class that makes it easy to display the frames from color, infrared, and depth sources in your app. Typically, you will want to do something more with depth and infrared data than just display it to the screen, but this helper class is a helpful tool for demonstrating the frame reader feature and for debugging your own frame reader implementation. The code in this section is adapted from the [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/CameraFrames).

The **FrameRenderer** helper class implements the following methods.

* **FrameRenderer** constructor - The constructor initializes the helper class to use the XAML [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image) element you pass in for displaying media frames.
* **ProcessFrame** - This method displays a media frame, represented by a [**MediaFrameReference**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReference), in the **Image** element you passed into the constructor. You should typically call this method from your [**FrameArrived**](/uwp/api/windows.media.capture.frames.mediaframereader.framearrived) event handler, passing in the frame returned by [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.mediaframereader.tryacquirelatestframe).
* **ConvertToDisplayableImage** - This methods checks the format of the media frame and, if necessary, converts it to a displayable format. For color images, this means making sure that the color format is BGRA8 and that the bitmap alpha mode is premultiplied. For depth or infrared frames, each scanline is processed to convert the depth or infrared values to a psuedocolor gradient, using the **PsuedoColorHelper** class that is also included in the sample and listed below.

> [!NOTE] 
> In order to do pixel manipulation on **SoftwareBitmap** images, you must access a native memory buffer. To do this, you must use the IMemoryBufferByteAccess COM interface included in the code listing below and you must update your project properties to allow compilation of unsafe code. For more information, see [Create, edit, and save bitmap images](../media-authoring-processing/imaging.md).

```csharp
[GeneratedComInterface]
[Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
unsafe partial interface IMemoryBufferByteAccess
{
    void GetBuffer(out byte* buffer, out uint capacity);
}
```

```csharp
class FrameRenderer
{
    private Image m_imageElement;
    private SoftwareBitmap m_backBuffer;
    private bool _taskRunning = false;

    public FrameRenderer(Image imageElement)
    {
        m_imageElement = imageElement;
        m_imageElement.Source = new SoftwareBitmapSource();
    }

    // Processes a MediaFrameReference and displays it in a XAML image control
    public void ProcessFrame(MediaFrameReference frame)
    {
        var softwareBitmap = FrameRenderer.ConvertToDisplayableImage(frame?.VideoMediaFrame);
        if (softwareBitmap != null)
        {
            // Swap the processed frame to m_backBuffer and trigger UI thread to render it
            softwareBitmap = Interlocked.Exchange(ref m_backBuffer, softwareBitmap);

            // UI thread always reset m_backBuffer before using it.  Unused bitmap should be disposed.
            softwareBitmap?.Dispose();

            // Changes to xaml ImageElement must happen in UI thread through Dispatcher
            var task = m_imageElement.DispatcherQueue.TryEnqueue(
                async () =>
                {
                    // Don't let two copies of this task run at the same time.
                    if (_taskRunning)
                    {
                        return;
                    }
                    _taskRunning = true;

                    // Keep draining frames from the backbuffer until the backbuffer is empty.
                    SoftwareBitmap latestBitmap;
                    while ((latestBitmap = Interlocked.Exchange(ref m_backBuffer, null)) != null)
                    {
                        var imageSource = (SoftwareBitmapSource)m_imageElement.Source;
                        await imageSource.SetBitmapAsync(latestBitmap);
                        latestBitmap.Dispose();
                    }

                    _taskRunning = false;
                });
        }
    }



    // Function delegate that transforms a scanline from an input image to an output image.
    private unsafe delegate void TransformScanline(int pixelWidth, byte* inputRowBytes, byte* outputRowBytes);
    /// <summary>
    /// Determines the subtype to request from the MediaFrameReader that will result in
    /// a frame that can be rendered by ConvertToDisplayableImage.
    /// </summary>
    /// <returns>Subtype string to request, or null if subtype is not renderable.</returns>

    public static string GetSubtypeForFrameReader(MediaFrameSourceKind kind, MediaFrameFormat format)
    {
        // Note that media encoding subtypes may differ in case.
        // https://docs.microsoft.com/en-us/uwp/api/Windows.Media.MediaProperties.MediaEncodingSubtypes

        string subtype = format.Subtype;
        switch (kind)
        {
            // For color sources, we accept anything and request that it be converted to Bgra8.
            case MediaFrameSourceKind.Color:
                return Windows.Media.MediaProperties.MediaEncodingSubtypes.Bgra8;

            // The only depth format we can render is D16.
            case MediaFrameSourceKind.Depth:
                return String.Equals(subtype, Windows.Media.MediaProperties.MediaEncodingSubtypes.D16, StringComparison.OrdinalIgnoreCase) ? subtype : null;

            // The only infrared formats we can render are L8 and L16.
            case MediaFrameSourceKind.Infrared:
                return (String.Equals(subtype, Windows.Media.MediaProperties.MediaEncodingSubtypes.L8, StringComparison.OrdinalIgnoreCase) ||
                    String.Equals(subtype, Windows.Media.MediaProperties.MediaEncodingSubtypes.L16, StringComparison.OrdinalIgnoreCase)) ? subtype : null;

            // No other source kinds are supported by this class.
            default:
                return null;
        }
    }

    /// <summary>
    /// Converts a frame to a SoftwareBitmap of a valid format to display in an Image control.
    /// </summary>
    /// <param name="inputFrame">Frame to convert.</param>

    public static unsafe SoftwareBitmap ConvertToDisplayableImage(VideoMediaFrame inputFrame)
    {
        SoftwareBitmap result = null;
        using (var inputBitmap = inputFrame?.SoftwareBitmap)
        {
            if (inputBitmap != null)
            {
                switch (inputFrame.FrameReference.SourceKind)
                {
                    case MediaFrameSourceKind.Color:
                        // XAML requires Bgra8 with premultiplied alpha.
                        // We requested Bgra8 from the MediaFrameReader, so all that's
                        // left is fixing the alpha channel if necessary.
                        if (inputBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8)
                        {
                            System.Diagnostics.Debug.WriteLine("Color frame in unexpected format.");
                        }
                        else if (inputBitmap.BitmapAlphaMode == BitmapAlphaMode.Premultiplied)
                        {
                            // Already in the correct format.
                            result = SoftwareBitmap.Copy(inputBitmap);
                        }
                        else
                        {
                            // Convert to premultiplied alpha.
                            result = SoftwareBitmap.Convert(inputBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                        }
                        break;

                    case MediaFrameSourceKind.Depth:
                        // We requested D16 from the MediaFrameReader, so the frame should
                        // be in Gray16 format.
                        if (inputBitmap.BitmapPixelFormat == BitmapPixelFormat.Gray16)
                        {
                            // Use a special pseudo color to render 16 bits depth frame.
                            var depthScale = (float)inputFrame.DepthMediaFrame.DepthFormat.DepthScaleInMeters;
                            var minReliableDepth = inputFrame.DepthMediaFrame.MinReliableDepth;
                            var maxReliableDepth = inputFrame.DepthMediaFrame.MaxReliableDepth;
                            result = TransformBitmap(inputBitmap, (w, i, o) => PseudoColorHelper.PseudoColorForDepth(w, i, o, depthScale, minReliableDepth, maxReliableDepth));
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Depth frame in unexpected format.");
                        }
                        break;

                    case MediaFrameSourceKind.Infrared:
                        // We requested L8 or L16 from the MediaFrameReader, so the frame should
                        // be in Gray8 or Gray16 format.
                        switch (inputBitmap.BitmapPixelFormat)
                        {
                            case BitmapPixelFormat.Gray16:
                                // Use pseudo color to render 16 bits frames.
                                result = TransformBitmap(inputBitmap, PseudoColorHelper.PseudoColorFor16BitInfrared);
                                break;

                            case BitmapPixelFormat.Gray8:
                                // Use pseudo color to render 8 bits frames.
                                result = TransformBitmap(inputBitmap, PseudoColorHelper.PseudoColorFor8BitInfrared);
                                break;
                            default:
                                System.Diagnostics.Debug.WriteLine("Infrared frame in unexpected format.");
                                break;
                        }
                        break;
                }
            }
        }

        return result;
    }



    /// <summary>
    /// Transform image into Bgra8 image using given transform method.
    /// </summary>
    /// <param name="softwareBitmap">Input image to transform.</param>
    /// <param name="transformScanline">Method to map pixels in a scanline.</param>

    private static unsafe SoftwareBitmap TransformBitmap(SoftwareBitmap softwareBitmap, TransformScanline transformScanline)
    {
        // XAML Image control only supports premultiplied Bgra8 format.
        var outputBitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8,
            softwareBitmap.PixelWidth, softwareBitmap.PixelHeight, BitmapAlphaMode.Premultiplied);

        using (var input = softwareBitmap.LockBuffer(BitmapBufferAccessMode.Read))
        using (var output = outputBitmap.LockBuffer(BitmapBufferAccessMode.Write))
        {
            // Get stride values to calculate buffer position for a given pixel x and y position.
            int inputStride = input.GetPlaneDescription(0).Stride;
            int outputStride = output.GetPlaneDescription(0).Stride;
            int pixelWidth = softwareBitmap.PixelWidth;
            int pixelHeight = softwareBitmap.PixelHeight;

            using (var outputReference = output.CreateReference())
            using (var inputReference = input.CreateReference())
            {
                // Get input and output byte access buffers.
                byte* inputBytes;
                uint inputCapacity;
                ((IMemoryBufferByteAccess)inputReference).GetBuffer(out inputBytes, out inputCapacity);
                byte* outputBytes;
                uint outputCapacity;
                ((IMemoryBufferByteAccess)outputReference).GetBuffer(out outputBytes, out outputCapacity);

                // Iterate over all pixels and store converted value.
                for (int y = 0; y < pixelHeight; y++)
                {
                    byte* inputRowBytes = inputBytes + y * inputStride;
                    byte* outputRowBytes = outputBytes + y * outputStride;

                    transformScanline(pixelWidth, inputRowBytes, outputRowBytes);
                }
            }
        }

        return outputBitmap;
    }



    /// <summary>
    /// A helper class to manage look-up-table for pseudo-colors.
    /// </summary>

    private static class PseudoColorHelper
    {

        private const int TableSize = 1024;   // Look up table size
        private static readonly uint[] PseudoColorTable;
        private static readonly uint[] InfraredRampTable;

        // Color palette mapping value from 0 to 1 to blue to red colors.
        private static readonly Color[] ColorRamp =
        {
            Color.FromArgb(a:0xFF, r:0x7F, g:0x00, b:0x00),
            Color.FromArgb(a:0xFF, r:0xFF, g:0x00, b:0x00),
            Color.FromArgb(a:0xFF, r:0xFF, g:0x7F, b:0x00),
            Color.FromArgb(a:0xFF, r:0xFF, g:0xFF, b:0x00),
            Color.FromArgb(a:0xFF, r:0x7F, g:0xFF, b:0x7F),
            Color.FromArgb(a:0xFF, r:0x00, g:0xFF, b:0xFF),
            Color.FromArgb(a:0xFF, r:0x00, g:0x7F, b:0xFF),
            Color.FromArgb(a:0xFF, r:0x00, g:0x00, b:0xFF),
            Color.FromArgb(a:0xFF, r:0x00, g:0x00, b:0x7F),
        };

        static PseudoColorHelper()
        {
            PseudoColorTable = InitializePseudoColorLut();
            InfraredRampTable = InitializeInfraredRampLut();
        }

        /// <summary>
        /// Maps an input infrared value between [0, 1] to corrected value between [0, 1].
        /// </summary>
        /// <param name="value">Input value between [0, 1].</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  // Tell the compiler to inline this method to improve performance

        private static uint InfraredColor(float value)
        {
            int index = (int)(value * TableSize);
            index = index < 0 ? 0 : index > TableSize - 1 ? TableSize - 1 : index;
            return InfraredRampTable[index];
        }

        /// <summary>
        /// Initializes the pseudo-color look up table for infrared pixels
        /// </summary>

        private static uint[] InitializeInfraredRampLut()
        {
            uint[] lut = new uint[TableSize];
            for (int i = 0; i < TableSize; i++)
            {
                var value = (float)i / TableSize;
                // Adjust to increase color change between lower values in infrared images

                var alpha = (float)Math.Pow(1 - value, 12);
                lut[i] = ColorRampInterpolation(alpha);
            }

            return lut;
        }



        /// <summary>
        /// Initializes pseudo-color look up table for depth pixels
        /// </summary>
        private static uint[] InitializePseudoColorLut()
        {
            uint[] lut = new uint[TableSize];
            for (int i = 0; i < TableSize; i++)
            {
                lut[i] = ColorRampInterpolation((float)i / TableSize);
            }

            return lut;
        }



        /// <summary>
        /// Maps a float value to a pseudo-color pixel
        /// </summary>
        private static uint ColorRampInterpolation(float value)
        {
            // Map value to surrounding indexes on the color ramp
            int rampSteps = ColorRamp.Length - 1;
            float scaled = value * rampSteps;
            int integer = (int)scaled;
            int index =
                integer < 0 ? 0 :
                integer >= rampSteps - 1 ? rampSteps - 1 :
                integer;

            Color prev = ColorRamp[index];
            Color next = ColorRamp[index + 1];

            // Set color based on ratio of closeness between the surrounding colors
            uint alpha = (uint)((scaled - integer) * 255);
            uint beta = 255 - alpha;
            return
                ((prev.A * beta + next.A * alpha) / 255) << 24 | // Alpha
                ((prev.R * beta + next.R * alpha) / 255) << 16 | // Red
                ((prev.G * beta + next.G * alpha) / 255) << 8 |  // Green
                ((prev.B * beta + next.B * alpha) / 255);        // Blue
        }


        /// <summary>
        /// Maps a value in [0, 1] to a pseudo RGBA color.
        /// </summary>
        /// <param name="value">Input value between [0, 1].</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        private static uint PseudoColor(float value)
        {
            int index = (int)(value * TableSize);
            index = index < 0 ? 0 : index > TableSize - 1 ? TableSize - 1 : index;
            return PseudoColorTable[index];
        }


        /// <summary>
        /// Maps each pixel in a scanline from a 16 bit depth value to a pseudo-color pixel.
        /// </summary>
        /// <param name="pixelWidth">Width of the input scanline, in pixels.</param>
        /// <param name="inputRowBytes">Pointer to the start of the input scanline.</param>
        /// <param name="outputRowBytes">Pointer to the start of the output scanline.</param>
        /// <param name="depthScale">Physical distance that corresponds to one unit in the input scanline.</param>
        /// <param name="minReliableDepth">Shortest distance at which the sensor can provide reliable measurements.</param>
        /// <param name="maxReliableDepth">Furthest distance at which the sensor can provide reliable measurements.</param>

        public static unsafe void PseudoColorForDepth(int pixelWidth, byte* inputRowBytes, byte* outputRowBytes, float depthScale, float minReliableDepth, float maxReliableDepth)
        {
            // Visualize space in front of your desktop.
            float minInMeters = minReliableDepth * depthScale;
            float maxInMeters = maxReliableDepth * depthScale;
            float one_min = 1.0f / minInMeters;
            float range = 1.0f / maxInMeters - one_min;

            ushort* inputRow = (ushort*)inputRowBytes;
            uint* outputRow = (uint*)outputRowBytes;

            for (int x = 0; x < pixelWidth; x++)
            {
                var depth = inputRow[x] * depthScale;

                if (depth == 0)
                {
                    // Map invalid depth values to transparent pixels.
                    // This happens when depth information cannot be calculated, e.g. when objects are too close.
                    outputRow[x] = 0;
                }
                else
                {
                    var alpha = (1.0f / depth - one_min) / range;
                    outputRow[x] = PseudoColor(alpha * alpha);
                }
            }
        }



        /// <summary>
        /// Maps each pixel in a scanline from a 8 bit infrared value to a pseudo-color pixel.
        /// </summary>
        /// /// <param name="pixelWidth">Width of the input scanline, in pixels.</param>
        /// <param name="inputRowBytes">Pointer to the start of the input scanline.</param>
        /// <param name="outputRowBytes">Pointer to the start of the output scanline.</param>

        public static unsafe void PseudoColorFor8BitInfrared(
            int pixelWidth, byte* inputRowBytes, byte* outputRowBytes)
        {
            byte* inputRow = inputRowBytes;
            uint* outputRow = (uint*)outputRowBytes;

            for (int x = 0; x < pixelWidth; x++)
            {
                outputRow[x] = InfraredColor(inputRow[x] / (float)Byte.MaxValue);
            }
        }

        /// <summary>
        /// Maps each pixel in a scanline from a 16 bit infrared value to a pseudo-color pixel.
        /// </summary>
        /// <param name="pixelWidth">Width of the input scanline.</param>
        /// <param name="inputRowBytes">Pointer to the start of the input scanline.</param>
        /// <param name="outputRowBytes">Pointer to the start of the output scanline.</param>

        public static unsafe void PseudoColorFor16BitInfrared(int pixelWidth, byte* inputRowBytes, byte* outputRowBytes)
        {
            ushort* inputRow = (ushort*)inputRowBytes;
            uint* outputRow = (uint*)outputRowBytes;

            for (int x = 0; x < pixelWidth; x++)
            {
                outputRow[x] = InfraredColor(inputRow[x] / (float)UInt16.MaxValue);
            }
        }
    }


    // Displays the provided softwareBitmap in a XAML image control.
    public void PresentSoftwareBitmap(SoftwareBitmap softwareBitmap)
    {
        if (softwareBitmap != null)
        {
            // Swap the processed frame to m_backBuffer and trigger UI thread to render it
            softwareBitmap = Interlocked.Exchange(ref m_backBuffer, softwareBitmap);

            // UI thread always reset m_backBuffer before using it.  Unused bitmap should be disposed.
            softwareBitmap?.Dispose();

            // Changes to the XAML Image element must happen on the UI thread through DispatcherQueue.
            m_imageElement.DispatcherQueue.TryEnqueue(
                async () =>
                {
                    // Don't let two copies of this task run at the same time.
                    if (_taskRunning)
                    {
                        return;
                    }
                    _taskRunning = true;

                    // Keep draining frames from the backbuffer until the backbuffer is empty.
                    SoftwareBitmap latestBitmap;
                    while ((latestBitmap = Interlocked.Exchange(ref m_backBuffer, null)) != null)
                    {
                        var imageSource = (SoftwareBitmapSource)m_imageElement.Source;
                        await imageSource.SetBitmapAsync(latestBitmap);
                        latestBitmap.Dispose();
                    }

                    _taskRunning = false;
                });
        }
    }
}
```

## Use MultiSourceMediaFrameReader to get time-correlated frames from multiple sources

Starting with Windows 10, version 1607, you can use [**MultiSourceMediaFrameReader**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader) to receive time-correlated frames from multiple sources. This API makes it easier to do processing that requires frames from multiple sources that were taken in close temporal proximity, such as using the [**DepthCorrelatedCoordinateMapper**](/uwp/api/windows.media.devices.core.depthcorrelatedcoordinatemapper) class. One limitation of using this new method is that frame-arrived events are only raised at the rate of the slowest capture source. Extra frames from faster sources will be dropped. Also, because the system expects frames to arrive from different sources at different rates, it does not automatically recognize if a source has stopped generating frames altogether. The example code in this section shows how to use an event to create your own timeout logic that gets invoked if correlated frames don't arrive within an app-defined time limit.

The steps for using [**MultiSourceMediaFrameReader**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader) are similar to the steps for using [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) described previously in this article. This example will use a color source and a depth source. Declare some string variables to store the media frame source IDs that will be used to select frames from each source. Next, declare a [**ManualResetEventSlim**](/dotnet/api/system.threading.manualreseteventslim), a [**CancellationTokenSource**](/dotnet/api/system.threading.cancellationtokensource), and an [**EventHandler**](/dotnet/api/system.eventhandler) that will be used to implement timeout logic for the example. 

```csharp
private MultiSourceMediaFrameReader m_multiFrameReader = null;
private string m_colorSourceId = null;
private string m_depthSourceId = null;


private readonly ManualResetEventSlim m_frameReceived = new ManualResetEventSlim(false);
private readonly CancellationTokenSource m_tokenSource = new CancellationTokenSource();
public event EventHandler CorrelationFailed;
```

Using the techniques described previously in this article, query for a [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) that includes the color and depth sources required for this example scenario. After selecting the desired frame source group, get the [**MediaFrameSourceInfo**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceInfo) for each frame source.

```csharp
var allGroups = await MediaFrameSourceGroup.FindAllAsync();
var eligibleGroups = allGroups.Select(g => new
{
    Group = g,

    // For each source kind, find the source which offers that kind of media frame,
    // or null if there is no such source.
    SourceInfos = new MediaFrameSourceInfo[]
    {
        g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Color),
        g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Depth)
    }
}).Where(g => g.SourceInfos.Any(info => info != null)).ToList();

if (eligibleGroups.Count == 0)
{
    System.Diagnostics.Debug.WriteLine("No source group with color, depth or infrared found.");
    return;
}

var selectedGroupIndex = 0; // Select the first eligible group
MediaFrameSourceGroup selectedGroup = eligibleGroups[selectedGroupIndex].Group;
MediaFrameSourceInfo colorSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[0];
MediaFrameSourceInfo depthSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[1];
```

Create and initialize a **MediaCapture** object, passing the selected frame source group in the initialization settings.

```csharp
m_mediaCapture = new MediaCapture();

var settings = new MediaCaptureInitializationSettings()
{
    SourceGroup = selectedGroup,
    SharingMode = MediaCaptureSharingMode.ExclusiveControl,
    MemoryPreference = MediaCaptureMemoryPreference.Cpu,
    StreamingCaptureMode = StreamingCaptureMode.Video
};

await m_mediaCapture.InitializeAsync(settings);
```

After initializing the **MediaCapture** object, retrieve [**MediaFrameSource**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSource) objects for the color and depth cameras. Store the ID for each source so that you can select the arriving frame for the corresponding source.

```csharp
MediaFrameSource colorSource =
    m_mediaCapture.FrameSources.Values.FirstOrDefault(
        s => s.Info.SourceKind == MediaFrameSourceKind.Color);

MediaFrameSource depthSource =
    m_mediaCapture.FrameSources.Values.FirstOrDefault(
        s => s.Info.SourceKind == MediaFrameSourceKind.Depth);

if (colorSource == null || depthSource == null)
{
    System.Diagnostics.Debug.WriteLine("MediaCapture doesn't have the Color and Depth streams");
    return;
}

m_colorSourceId = colorSource.Info.Id;
m_depthSourceId = depthSource.Info.Id;
```

Create and initialize the **MultiSourceMediaFrameReader** by calling [**CreateMultiSourceFrameReaderAsync**](/uwp/api/windows.media.capture.mediacapture.createmultisourceframereaderasync) and passing an array of frame sources that the reader will use. Register an event handler for the [**FrameArrived**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader.FrameArrived) event. This example creates an instance the **FrameRenderer** helper class, described previously in this article, to render frames to an **Image** control. Start the frame reader by calling [**StartAsync**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader.StartAsync).

Register an event handler for the **CorellationFailed** event declared earlier in the example. We will signal this event if one of the media frame sources being used stops producing frames. Finally, call [**Task.Run**](/dotnet/api/system.threading.tasks.task.run) to call the timeout helper method, **NotifyAboutCorrelationFailure**, on a separate thread. The implementation of this method is shown later in this article.

```csharp
m_multiFrameReader = await m_mediaCapture.CreateMultiSourceFrameReaderAsync(
    new[] { colorSource, depthSource });

m_multiFrameReader.FrameArrived += MultiFrameReader_FrameArrived;

m_frameRenderer = new FrameRenderer(iFrameReaderImageControl);

MultiSourceMediaFrameReaderStartStatus startStatus =
    await m_multiFrameReader.StartAsync();

if (startStatus != MultiSourceMediaFrameReaderStartStatus.Success)
{
    throw new InvalidOperationException(
        "Unable to start reader: " + startStatus);
}

this.CorrelationFailed += MainWindow_CorrelationFailed;
Task.Run(() => NotifyAboutCorrelationFailure(m_tokenSource.Token));
```

The **FrameArrived** event is raised whenever a new frame is available from all of the media frame sources that are managed by the **MultiSourceMediaFrameReader**. This means that the event will be raised on the cadence of the slowest media source. If one source produces multiple frames in the time that a slower source produces one frame, the extra frames from the fast source will be dropped. 

Get the [**MultiSourceMediaFrameReference**](/uwp/api/windows.media.capture.frames.multisourcemediaframereference) associated with the event by calling [**TryAcquireLatestFrame**](/uwp/api/windows.media.capture.frames.multisourcemediaframereader.TryAcquireLatestFrame). Get the **MediaFrameReference** associated with each media frame source by calling [**TryGetFrameReferenceBySourceId**](/uwp/api/windows.media.capture.frames.multisourcemediaframereference.trygetframereferencebysourceid), passing in the ID strings stored when the frame reader was initialized.

Call the [**Set**](/dotnet/api/system.threading.manualreseteventslim.set) method of the **ManualResetEventSlim** object to signal that frames have arrived. We will check this event in the **NotifyCorrelationFailure** method that is running in a separate thread. 

Finally, perform any processing on the time-correlated media frames. This example simply displays the frame from the depth source.

```csharp
private void MultiFrameReader_FrameArrived(MultiSourceMediaFrameReader sender, MultiSourceMediaFrameArrivedEventArgs args)
{
    using (MultiSourceMediaFrameReference muxedFrame =
        sender.TryAcquireLatestFrame())
    using (MediaFrameReference colorFrame =
        muxedFrame.TryGetFrameReferenceBySourceId(m_colorSourceId))
    using (MediaFrameReference depthFrame =
        muxedFrame.TryGetFrameReferenceBySourceId(m_depthSourceId))
    {
        // Notify the listener thread that the frame has been received.
        m_frameReceived.Set();
        m_frameRenderer.ProcessFrame(depthFrame);
    }
}
```

The **NotifyCorrelationFailure** helper method was run on a separate thread after the frame reader was started. In this method, check to see if the frame received event has been signaled. Remember, in the **FrameArrived** handler, we set this event whenever a set of correlated frames arrive. If the event hasn't been signaled for some app-defined period of time - 5 seconds is a reasonable value - and the task wasn't cancelled using the **CancellationToken**, then it's likely that one of the media frame sources has stopped reading frames. In this case you typically want to shut down the frame reader, so raise the app-defined **CorrelationFailed** event. In the handler for this event you can stop the frame reader and clean up it's associated resources as shown previously in this article.

```csharp
private void NotifyAboutCorrelationFailure(CancellationToken token)
{
    // If in 5 seconds the token is not cancelled and frame event is not signaled,
    // correlation is most likely failed.
    if (WaitHandle.WaitAny(new[] { token.WaitHandle, m_frameReceived.WaitHandle }, 5000)
            == WaitHandle.WaitTimeout)
    {
        CorrelationFailed?.Invoke(this, EventArgs.Empty);
    }
}
```

```csharp
private async void MainWindow_CorrelationFailed(object sender, EventArgs e)
{
    await m_multiFrameReader.StopAsync();
    m_multiFrameReader.FrameArrived -= MultiFrameReader_FrameArrived;
    m_mediaCapture.Dispose();
    m_mediaCapture = null;
}
```

## Use buffered frame acquisition mode to preserve the sequence of acquired frames

Starting with Windows 10, version 1709, you can set the **[AcquisitionMode](/uwp/api/windows.media.capture.frames.mediaframereader.AcquisitionMode)** property of a **MediaFrameReader** or **MultiSourceMediaFrameReader** to **Buffered** to preserve the sequence of frames passed into your app from the frame source.

```csharp
m_mediaFrameReader.AcquisitionMode = MediaFrameReaderAcquisitionMode.Buffered;
```

In the default acquisition mode, **Realtime**, if multiple frames are acquired from the source while your app is still handling the **FrameArrived** event for a previous frame, the system will send your app the most recently acquired frame and drop additional frames waiting in the buffer. This provides your app with the most recent available frame at all times. This is typically the most useful mode for realtime computer vision applications. 

In **Buffered** acquisition mode, the system will keep all frames in the buffer and provide them to your app through the **FrameArrived** event in the order received. Note that in this mode, when system's buffer for frames is filled, the system will stop acquiring new frames until your app completes the **FrameArrived** event for previous frames, freeing up more space in the buffer.

## Use MediaSource to display frames in a MediaPlayerElement

Starting with Windows, version 1709, you can display frames acquired from a **MediaFrameReader** directly in a **[MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement)** control in your XAML page. This is achieved by using the **[MediaSource.CreateFromMediaFrameSource](/uwp/api/windows.media.core.mediasource.createfrommediaframesource)** to create **[MediaSource](/uwp/api/windows.media.core.mediasource)** object that can be used directly by a **[MediaPlayer](/uwp/api/windows.media.playback.mediaplayer)** associated with a **MediaPlayerElement**. For detailed information on working with **MediaPlayer** and **MediaPlayerElement**, see [Play audio and video with MediaPlayer](/windows/uwp/audio-video-camera/play-audio-and-video-with-mediaplayer).

The following code examples show you a simple implementation that displays the frames from a front-facing and back-facing camera simultaneously in a XAML page.

First, add two **MediaPlayerElement** controls to your XAML page.

```xml
<MediaPlayerElement x:Name="mediaPlayerElement1" Width="320" Height="240"/>
```

```xml
<MediaPlayerElement x:Name="mediaPlayerElement2" Width="320" Height="240"/>
```

Next, using the techniques shown in previous sections in this article, select a **MediaFrameSourceGroup** that contains **MediaFrameSourceInfo** objects for color cameras on the front panel and back panel. Note that the **MediaPlayer** does not automatically convert frames from non-color formats, such as a depth or infrared data, into color data. Using other sensor types may produce unexpected results. 

```csharp
var allGroups = await MediaFrameSourceGroup.FindAllAsync();
var eligibleGroups = allGroups.Select(g => new
{
    Group = g,

    // For each source kind, find the source which offers that kind of media frame,
    // or null if there is no such source.
    SourceInfos = new MediaFrameSourceInfo[]
    {
        g.SourceInfos.FirstOrDefault(info => info.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front
            && info.SourceKind == MediaFrameSourceKind.Color),
        g.SourceInfos.FirstOrDefault(info => info.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back
            && info.SourceKind == MediaFrameSourceKind.Color)
    }
}).Where(g => g.SourceInfos.Any(info => info != null)).ToList();

if (eligibleGroups.Count == 0)
{
    System.Diagnostics.Debug.WriteLine("No source group with front and back-facing camera found.");
    return;
}

var selectedGroupIndex = 0; // Select the first eligible group
MediaFrameSourceGroup selectedGroup = eligibleGroups[selectedGroupIndex].Group;
MediaFrameSourceInfo frontSourceInfo = selectedGroup.SourceInfos[0];
MediaFrameSourceInfo backSourceInfo = selectedGroup.SourceInfos[1];
```

Initialize the **MediaCapture** object to use the selected **MediaFrameSourceGroup**.

```csharp
m_mediaCapture = new MediaCapture();

var settings = new MediaCaptureInitializationSettings()
{
    SourceGroup = selectedGroup,
    SharingMode = MediaCaptureSharingMode.ExclusiveControl,
    MemoryPreference = MediaCaptureMemoryPreference.Cpu,
    StreamingCaptureMode = StreamingCaptureMode.Video
};
try
{
    await m_mediaCapture.InitializeAsync(settings);
}
catch (Exception ex)
{
    System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed: " + ex.Message);
    return;
}
```

Finally, call **[MediaSource.CreateFromMediaFrameSource](/uwp/api/windows.media.core.mediasource.createfrommediaframesource)** to create a **MediaSource** for each frame source by using the **[Id](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.Id)** property of the associated **MediaFrameSourceInfo** object to select one of the frame sources in the **MediaCapture** object's **[FrameSources](/uwp/api/windows.media.capture.mediacapture.FrameSources)** collection. Initialize a new **MediaPlayer** object and assign it to a **MediaPlayerElement** by calling **[SetMediaPlayer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.setmediaplayer)**. Then set the **[Source](/uwp/api/windows.media.playback.mediaplayer.Source)** property to the newly created **MediaSource** object.

```csharp
var frameMediaSource1 = MediaSource.CreateFromMediaFrameSource(m_mediaCapture.FrameSources[frontSourceInfo.Id]);
mediaPlayerElement1.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer());
mediaPlayerElement1.MediaPlayer.Source = frameMediaSource1;
mediaPlayerElement1.AutoPlay = true;

var frameMediaSource2 = MediaSource.CreateFromMediaFrameSource(m_mediaCapture.FrameSources[backSourceInfo.Id]);
mediaPlayerElement2.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer());
mediaPlayerElement2.MediaPlayer.Source = frameMediaSource2;
mediaPlayerElement2.AutoPlay = true;
```

## Use video profiles to select a frame source

A camera profile, represented by a [**MediaCaptureVideoProfile**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfile) object, represents a set of capabilities that a particular capture device provides, such as frame rates, resolutions, or advanced features like HDR capture. A capture device may support multiple profiles, allowing you to select the one that is optimized for your capture scenario. Starting with Windows 10, version 1803, you can use **MediaCaptureVideoProfile** to select a media frame source with particular capabilities before initializing the **MediaCapture** object. The following example code looks for a video profile that supports HDR with Wide Color Gamut (WCG) and returns a **MediaCaptureInitializationSettings** object that can be used to initialize the **MediaCapture** to use the selected device and profile.

First, call [**MediaFrameSourceGroup.FindAllAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.findallasync) to get a list of all media frame source groups available on the current device. Loop through each source group and call [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) to get a list of all of the video profiles for the current source group that support the specified profile, in this case HDR with WCG photo. If a profile that meets the criteria is found, create a new **MediaCaptureInitializationSettings** object and set the **VideoProfile** to the select profile and the **VideoDeviceId** to the **Id** property of the current media frame source group.

```csharp
IReadOnlyList<MediaFrameSourceGroup> sourceGroups = await MediaFrameSourceGroup.FindAllAsync();
MediaCaptureInitializationSettings settings = null;

foreach (MediaFrameSourceGroup sourceGroup in sourceGroups)
{
    // Find a device that support AdvancedColorPhoto
    IReadOnlyList<MediaCaptureVideoProfile> profileList = MediaCapture.FindKnownVideoProfiles(
                                  sourceGroup.Id,
                                  KnownVideoProfile.HdrWithWcgPhoto);

    if (profileList.Count > 0)
    {
        settings = new MediaCaptureInitializationSettings();
        settings.VideoProfile = profileList[0];
        settings.VideoDeviceId = sourceGroup.Id;
        break;
    }
}
```

For more information on using camera profiles, see [Camera profiles](camera-profiles.md).

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
* [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/CameraFrames)
 

 
