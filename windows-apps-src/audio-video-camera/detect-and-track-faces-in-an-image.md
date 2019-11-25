---
ms.assetid: 84729E44-10E9-4D7D-8575-6A9D97467ECD
description: This topic shows how to use the FaceDetector to detect faces in an image. The FaceTracker is optimized for tracking faces over time in a sequence of video frames.
title: Detect faces in images or videos
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Detect faces in images or videos



\[Some information relates to pre-released product which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.\]

This topic shows how to use the [**FaceDetector**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceDetector) to detect faces in an image. The [**FaceTracker**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceTracker) is optimized for tracking faces over time in a sequence of video frames.

For an alternative method of tracking faces using the [**FaceDetectionEffect**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.FaceDetectionEffect), see [Scene analysis for media capture](scene-analysis-for-media-capture.md).

The code in this article was adapted from the [Basic Face Detection](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicFaceDetection) and [Basic Face Tracking](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicFaceTracking) samples. You can download these samples to see the code used in context or to use the sample as a starting point for your own app.

## Detect faces in a single image

The [**FaceDetector**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceDetector) class allows you to detect one or more faces in a still image.

This example uses APIs from the following namespaces.

[!code-cs[FaceDetectionUsing](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetFaceDetectionUsing)]

Declare a class member variable for the [**FaceDetector**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceDetector) object and for the list of [**DetectedFace**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.DetectedFace) objects that will be detected in the image.

[!code-cs[ClassVariables1](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetClassVariables1)]

Face detection operates on a [**SoftwareBitmap**](https://docs.microsoft.com/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) object which can be created in a variety of ways. In this example a [**FileOpenPicker**](https://docs.microsoft.com/uwp/api/Windows.Storage.Pickers.FileOpenPicker) is used to allow the user to pick an image file in which faces will be detected. For more information about working with software bitmaps, see [Imaging](imaging.md).

[!code-cs[Picker](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetPicker)]

Use the [**BitmapDecoder**](https://docs.microsoft.com/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) class to decode the image file into a **SoftwareBitmap**. The face detection process is quicker with a smaller image and so you may want to scale the source image down to a smaller size. This can be performed during decoding by creating a [**BitmapTransform**](https://docs.microsoft.com/uwp/api/Windows.Graphics.Imaging.BitmapTransform) object, setting the [**ScaledWidth**](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmaptransform.scaledwidth) and [**ScaledHeight**](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmaptransform.scaledheight) properties and passing it into the call to [**GetSoftwareBitmapAsync**](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapdecoder.getsoftwarebitmapasync), which returns the decoded and scaled **SoftwareBitmap**.

[!code-cs[Decode](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetDecode)]

In the current version, the **FaceDetector** class only supports images in Gray8 or Nv12. The **SoftwareBitmap** class provides the [**Convert**](/uwp/api/windows.graphics.imaging.softwarebitmap.convert) method, which converts a bitmap from one format to another. This example converts the source image into the Gray8 pixel format if it is not already in that format. If you want, you can use the [**GetSupportedBitmapPixelFormats**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.facedetector.getsupportedbitmappixelformats) and [**IsBitmapPixelFormatSupported**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.facedetector.isbitmappixelformatsupported) methods to determine at runtime if a pixel format is supported, in case the set of supported formats is expanded in future versions.

[!code-cs[Format](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetFormat)]

Instantiate the **FaceDetector** object by calling [**CreateAsync**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.facedetector.createasync) and then calling [**DetectFacesAsync**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.facedetector.detectfacesasync), passing in the bitmap that has been scaled to a reasonable size and converted to a supported pixel format. This method returns a list of [**DetectedFace**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.DetectedFace) objects. **ShowDetectedFaces** is a helper method, shown below, that draws squares around the faces in the image.

[!code-cs[Detect](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetDetect)]

Be sure to dispose of the objects that were created during the face detection process.

[!code-cs[Dispose](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetDispose)]

To display the image and draw boxes around the detected faces, add a [**Canvas**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Canvas) element to your XAML page.

[!code-xml[Canvas](./code/FaceDetection_Win10/cs/MainPage.xaml#SnippetCanvas)]

Define some member variables to style the squares that will be drawn.

[!code-cs[ClassVariables2](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetClassVariables2)]

In the **ShowDetectedFaces** helper method, a new [**ImageBrush**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.ImageBrush) is created and the source is set to a [**SoftwareBitmapSource**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource) created from the **SoftwareBitmap** representing the source image. The background of the XAML **Canvas** control is set to the image brush.

If the list of faces passed into the helper method isn't empty, loop through each face in the list and use the [**FaceBox**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.detectedface.facebox) property of the [**DetectedFace**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.DetectedFace) class to determine the position and size of the rectangle within the image that contains the face. Because the **Canvas** control is very likely to be a different size than the source image, you should multiply both the X and Y coordinates and the width and height of the **FaceBox** by a scaling value that is the ratio of the source image size to the actual size of the **Canvas** control.

[!code-cs[ShowDetectedFaces](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetShowDetectedFaces)]

## Track faces in a sequence of frames

If you want to detect faces in video, it is more efficient to use the [**FaceTracker**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceTracker) class rather than the [**FaceDetector**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceDetector) class, although the implementation steps are very similar. The **FaceTracker** uses information about previously processed frames to optimize the detection process.

[!code-cs[FaceTrackingUsing](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetFaceTrackingUsing)]

Declare a class variable for the **FaceTracker** object. This example uses a [**ThreadPoolTimer**](https://docs.microsoft.com/uwp/api/Windows.System.Threading.ThreadPoolTimer) to initiate face tracking on a defined interval. A [SemaphoreSlim](https://docs.microsoft.com/dotnet/api/system.threading.semaphoreslim) is used to make sure that only one face tracking operation is running at a time.

[!code-cs[ClassVariables3](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetClassVariables3)]

To initialize the face tracking operation, create a new **FaceTracker** object by calling [**CreateAsync**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.facetracker.createasync). Initialize the desired timer interval and then create the timer. The **ProcessCurrentVideoFrame** helper method will be called every time the specified interval elapses.

[!code-cs[TrackingInit](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetTrackingInit)]

The **ProcessCurrentVideoFrame** helper is called asynchronously by the timer, so the method first calls the semaphore's **Wait** method to see if a tracking operation is ongoing, and if it is the method returns without trying to detect faces. At the end of this method, the semaphore's **Release** method is called, which allows the subsequent call to **ProcessCurrentVideoFrame** to continue.

The [**FaceTracker**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.FaceTracker) class operates on [**VideoFrame**](https://docs.microsoft.com/uwp/api/Windows.Media.VideoFrame) objects. There are multiple ways you can obtain a **VideoFrame** including capturing a preview frame from a running [MediaCapture](capture-photos-and-video-with-mediacapture.md) object or by implementing the [**ProcessFrame**](https://docs.microsoft.com/uwp/api/windows.media.effects.ibasicaudioeffect.processframe) method of the [**IBasicVideoEffect**](https://docs.microsoft.com/uwp/api/Windows.Media.Effects.IBasicVideoEffect). This example uses an undefined helper method that returns a video frame, **GetLatestFrame**, as a placeholder for this operation. For information about getting video frames from the preview stream of a running media capture device, see [Get a preview frame](get-a-preview-frame.md).

As with **FaceDetector**, the **FaceTracker** supports a limited set of pixel formats. This example abandons face detection if the supplied frame is not in the Nv12 format.

Call [**ProcessNextFrameAsync**](https://docs.microsoft.com/uwp/api/windows.media.faceanalysis.facetracker.processnextframeasync) to retrieve a list of [**DetectedFace**](https://docs.microsoft.com/uwp/api/Windows.Media.FaceAnalysis.DetectedFace) objects representing the faces in the frame. After you have the list of faces, you can display them in the same manner described above for face detection. Note that, because the face tracking helper method is not called on the UI thread, you must make any UI updates in within a call [**CoreDispatcher.RunAsync**](https://docs.microsoft.com/uwp/api/windows.ui.core.coredispatcher.runasync).

[!code-cs[ProcessCurrentVideoFrame](./code/FaceDetection_Win10/cs/MainPage.xaml.cs#SnippetProcessCurrentVideoFrame)]

## Related topics

* [Scene analysis for media capture](scene-analysis-for-media-capture.md)
* [Basic Face Detection sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicFaceDetection)
* [Basic Face Tracking sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicFaceTracking)
* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Media playback](media-playback.md)
