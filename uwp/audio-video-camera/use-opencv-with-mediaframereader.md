---
ms.assetid: 
description: This article shows you how to use the Open Source Computer Vision Library (OpenCV) with the MediaFrameReader class.
title: Use OpenCV with MediaFrameReader
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, openCV
ms.localizationpriority: medium
---
# Use the Open Source Computer Vision Library (OpenCV) with MediaFrameReader

This article shows you how to use the Open Source Computer Vision Library (OpenCV), a native code library that provides a wide variety of image processing algorithms, with the [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) class that can read media frames from multiple sources simultaneously. The example code in this article walks you through creating a simple app that obtains frames from a color sensor, blurs each frame using the OpenCV library, and then displays the processed image in a XAML **Image** control. 

>[!NOTE]
>OpenCV.Win.Core and OpenCV.Win.ImgProc are not regularly updated and do not pass the Store compliance checks, therefore these packages are intended for experimentation only.

This article builds on the content of two other articles:

* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md) - This article provides detailed information on using **MediaFrameReader** to obtain frames from one or more media frame sources and describes, in detail, most of the sample code in this article. In particular, **Process media frames with MediaFrameReader** provides the code listing for a helper class, **FrameRenderer**, which handles the presentation of media frames in a XAML **Image** element. The sample code in this article uses this helper class as well.

* [Process software bitmaps with OpenCV](process-software-bitmaps-with-opencv.md) - This article walks you through creating a native code Windows Runtime component, **OpenCVBridge**, which helps to convert between the **SoftwareBitmap** object, used by the **MediaFrameReader**,  and the **Mat** type used by the OpenCV library. The sample code in this article assumes you have followed the steps to add the **OpenCVBridge** component to your UWP app solution.

In addition to these articles, to view and download a full, end-to-end working sample of the scenario described in this article, see the [Camera Frames + OpenCV Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraOpenCV) in the Windows Universal Samples GitHub repo.

To get started developing quickly, you can include the OpenCV library in a UWP app project by using NuGet packages, but these packages may not pass the app certification process when you submit your app to the Store, so it is recommended that you download the OpenCV library source code and build the binaries yourself before submitting your app. Information developing with OpenCV can be found at [https://opencv.org](https://opencv.org)


## Implement the OpenCVHelper native Windows Runtime component
Follow the steps in [Process software bitmaps with OpenCV](process-software-bitmaps-with-opencv.md) to create the OpenCV helper Windows Runtime component and add a reference to the component project to your UWP app solution.

## Find available frame source groups
First, you need to find a media frame source group from which media frames will be obtained. Get the list of available source groups on the current device by calling **[MediaFrameSourceGroup.FindAllAsync](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.FindAllAsync)**. Then select the source groups that provide the sensor types required for your app scenario. For this example, we simply need a source group that provides frames from an RGB camera.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.OpenCV.xaml.cs" id="SnippetOpenCVFrameSourceGroups":::

## Initialize the MediaCapture object
Next, you need to initialize the **MediaCapture** object to use the frame source group selected in the previous step by setting the **[SourceGroup](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.SourceGroup)** property of the **MediaCaptureInitializationSettings**.

> [!NOTE] 
> The technique used by the OpenCVHelper component, described in detail in [Process software bitmaps with OpenCV](process-software-bitmaps-with-opencv.md), requires that the image data resides in CPU memory, not GPU memory. So, you should specify **MemoryPreference.CPU** for the **[MemoryPreference](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.MemoryPreference)** field of the **MediaCaptureInitializationSettings**.

After the **MediaCapture** object has been initialized, get a reference to the RGB frame source by accessing the **[MediaCapture.FrameSources](/uwp/api/windows.media.capture.mediacapture.FrameSources)** property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.OpenCV.xaml.cs" id="SnippetOpenCVInitMediaCapture":::

## Initialize the MediaFrameReader
Next, create a [**MediaFrameReader**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) for the RGB frame source retrieved in the previous step. In order to maintain a good frame rate, you may want to process frames that have a lower resolution than the resolution of the sensor. This example provides the optional **[BitmapSize](/uwp/api/windows.graphics.imaging.bitmapsize)** argument to the **[MediaCapture.CreateFrameReaderAsync](/uwp/api/windows.media.capture.mediacapture.createframereaderasync)** method to request that frames provided by the frame reader be resized to 640 x 480 pixels.

After creating the frame reader, register a handler for the **[FrameArrived](/uwp/api/windows.media.capture.frames.mediaframereader.FrameArrived)** event. Then create a new **[SoftwareBitmapSource](/uwp/api/windows.ui.xaml.media.imaging.softwarebitmapsource)** object, which the **FrameRenderer** helper class will use to present the processed image. Then call the constructor for the **FrameRenderer**. Initialize the instance of the **OpenCVHelper** class defined in the OpenCVBridge Windows Runtime component. This helper class is used in the **FrameArrived** handler to process each frame. Finally, start the frame reader by calling **[StartAsync](/uwp/api/windows.media.capture.frames.mediaframereader.StartAsync)**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.OpenCV.xaml.cs" id="SnippetOpenCVFrameReader":::


## Handle the FrameArrived event
The **FrameArrived** event is raised whenever a new frame is available from the frame reader. Call **[TryAcquireLatestFrame](/uwp/api/windows.media.capture.frames.mediaframereader.TryAcquireLatestFrame)** to get the frame, if it exists. Get the **SoftwareBitmap** from the **[MediaFrameReference](/uwp/api/windows.media.capture.frames.mediaframereference)**. Note that the **CVHelper** class used in this example requires images to use the BRGA8 pixel format with premultiplied alpha. If the frame passed into the event has a different format, convert the **SoftwareBitmap** to the correct format. Next, create a **SoftwareBitmap** to be used as the target of the blur operation. The source image properties are used as arguments to the constructor to create a bitmap with matching format. Call the helper class **Blur** method to process the frame. Finally, pass the output image of the blur operation into **PresentSoftwareBitmap**, the method of the **FrameRenderer** helper class that displays the image in the XAML **Image** control with which it was initialized.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/MainPage.OpenCV.xaml.cs" id="SnippetOpenCVFrameArrived":::

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
* [Process software bitmaps with OpenCV](process-software-bitmaps-with-opencv.md)
* [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames)
* [Camera Frames + OpenCV Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraOpenCV)
 

 
