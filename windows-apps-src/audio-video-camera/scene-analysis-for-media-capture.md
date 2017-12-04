---
author: drewbatgit
ms.assetid: B5D915E4-4280-422C-BA0E-D574C534410B
description: This article describes how to use the SceneAnalysisEffect and the FaceDetectionEffect to analyze the content of the media capture preview stream.
title: Effects for analyzing camera frames
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Effects for analyzing camera frames



This article describes how to use the [**SceneAnalysisEffect**](https://msdn.microsoft.com/library/windows/apps/dn948902) and the [**FaceDetectionEffect**](https://msdn.microsoft.com/library/windows/apps/dn948776) to analyze the content of the media capture preview stream.

## Scene analysis effect

The [**SceneAnalysisEffect**](https://msdn.microsoft.com/library/windows/apps/dn948902) analyzes the video frames in the media capture preview stream and recommends processing options to improve the capture result. Currently, the effect supports detecting whether the capture would be improved by using High Dynamic Range (HDR) processing.

If the effect recommends using HDR, you can do this in the following ways:

-   Use the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/mt181386) class to capture photos using the Windows built-in HDR processing algorithm. For more information, see [High Dynamic Range (HDR) photo capture](high-dynamic-range-hdr-photo-capture.md).

-   Use the [**HdrVideoControl**](https://msdn.microsoft.com/library/windows/apps/dn926680) to capture video using the Windows built-in HDR processing algorithm. For more information, see [Capture device controls for video capture](capture-device-controls-for-video-capture.md).

-   Use the [**VariablePhotoSequenceControl**](https://msdn.microsoft.com/library/windows/apps/dn640573) to capture a sequence of frames that you can then composite using a custom HDR implementation. For more information, see [Variable photo sequence](variable-photo-sequence.md).

### Scene analysis namespaces

To use scene analysis, your app must include the following namespaces in addition to the namespaces required for basic media capture.

[!code-cs[SceneAnalysisUsing](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetSceneAnalysisUsing)]

### Initialize the scene analysis effect and add it to the preview stream

Video effects are implemented using two APIs, an effect definition, which provides settings that the capture device needs to initialize the effect, and an effect instance, which can be used to control the effect. Since you may want to access the effect instance from multiple places within your code, you should typically declare a member variable to hold the object.

[!code-cs[DeclareSceneAnalysisEffect](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetDeclareSceneAnalysisEffect)]

In your app, after you have initialized the **MediaCapture** object, create a new instance of [**SceneAnalysisEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn948903).

Register the effect with the capture device by calling [**AddVideoEffectAsync**](https://msdn.microsoft.com/library/windows/apps/dn878035) on your **MediaCapture** object, providing the **SceneAnalysisEffectDefinition** and specifying [**MediaStreamType.VideoPreview**](https://msdn.microsoft.com/library/windows/apps/br226640) to indicate that the effect should be applied to the video preview stream, as opposed to the capture stream. **AddVideoEffectAsync** returns an instance of the added effect. Because this method can be used with multiple effect types, you must cast the returned instance to a [**SceneAnalysisEffect**](https://msdn.microsoft.com/library/windows/apps/dn948902) object.

To receive the results of the scene analysis, you must register a handler for the [**SceneAnalyzed**](https://msdn.microsoft.com/library/windows/apps/dn948920) event.

Currently, the scene analysis effect only includes the high dynamic range analyzer. Enable HDR analysis by setting the effect's [**HighDynamicRangeControl.Enabled**](https://msdn.microsoft.com/library/windows/apps/dn948827) to true.

[!code-cs[CreateSceneAnalysisEffectAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCreateSceneAnalysisEffectAsync)]

### Implement the SceneAnalyzed event handler

The results of the scene analysis are returned in the **SceneAnalyzed** event handler. The [**SceneAnalyzedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn948922) object passed into the handler has a [**SceneAnalysisEffectFrame**](https://msdn.microsoft.com/library/windows/apps/dn948907) object which has a [**HighDynamicRangeOutput**](https://msdn.microsoft.com/library/windows/apps/dn948830) object. The [**Certainty**](https://msdn.microsoft.com/library/windows/apps/dn948833) property of the high dynamic range output provides a value between 0 and 1.0 where 0 indicates that HDR processing would not help improve the capture result and 1.0 indicates that HDR processing would help. Your can decide the threshold point at which you want to use HDR or show the results to the user and let the user decide.

[!code-cs[SceneAnalyzed](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetSceneAnalyzed)]

The [**HighDynamicRangeOutput**](https://msdn.microsoft.com/library/windows/apps/dn948830) object passed into the handler also has a [**FrameControllers**](https://msdn.microsoft.com/library/windows/apps/dn948834) property which contains suggested frame controllers for capturing a variable photo sequence for HDR processing. For more information, see [Variable photo sequence](variable-photo-sequence.md).

### Clean up the scene analysis effect

When your app is done capturing, before disposing of the **MediaCapture** object, you should disable the scene analysis effect by setting the effect's [**HighDynamicRangeAnalyzer.Enabled**](https://msdn.microsoft.com/library/windows/apps/dn948827) property to false and unregister your [**SceneAnalyzed**](https://msdn.microsoft.com/library/windows/apps/dn948920) event handler. Call [**MediaCapture.ClearEffectsAsync**](https://msdn.microsoft.com/library/windows/apps/br226592), specifying the video preview stream since that was the stream to which the effect was added. Finally, set your member variable to null.

[!code-cs[CleanUpSceneAnalysisEffectAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCleanUpSceneAnalysisEffectAsync)]

## Face detection effect

The [**FaceDetectionEffect**](https://msdn.microsoft.com/library/windows/apps/dn948776) identifies the location of faces within the media capture preview stream. The effect allows you to receive a notification whenever a face is detected in the preview stream and provides the bounding box for each detected face within the preview frame. On supported devices, the face detection effect also provides enhanced exposure and focus on the most important face in the scene.

### Face detection namespaces

To use face detection, your app must include the following namespaces in addition to the namespaces required for basic media capture.

[!code-cs[FaceDetectionUsing](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetFaceDetectionUsing)]

### Initialize the face detection effect and add it to the preview stream

Video effects are implemented using two APIs, an effect definition, which provides settings that the capture device needs to initialize the effect, and an effect instance, which can be used to control the effect. Since you may want to access the effect instance from multiple places within your code, you should typically declare a member variable to hold the object.

[!code-cs[DeclareFaceDetectionEffect](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetDeclareFaceDetectionEffect)]

In your app, after you have initialized the **MediaCapture** object, create a new instance of [**FaceDetectionEffectDefinition**](https://msdn.microsoft.com/library/windows/apps/dn948778). Set the [**DetectionMode**](https://msdn.microsoft.com/library/windows/apps/dn948781) property to prioritize faster face detection or more accurate face detection. Set [**SynchronousDetectionEnabled**](https://msdn.microsoft.com/library/windows/apps/dn948786) to specify that incoming frames are not delayed waiting for face detection to complete as this can result in a choppy preview experience.

Register the effect with the capture device by calling [**AddVideoEffectAsync**](https://msdn.microsoft.com/library/windows/apps/dn878035) on your **MediaCapture** object, providing the **FaceDetectionEffectDefinition** and specifying [**MediaStreamType.VideoPreview**](https://msdn.microsoft.com/library/windows/apps/br226640) to indicate that the effect should be applied to the video preview stream, as opposed to the capture stream. **AddVideoEffectAsync** returns an instance of the added effect. Because this method can be used with multiple effect types, you must cast the returned instance to a [**FaceDetectionEffect**](https://msdn.microsoft.com/library/windows/apps/dn948776) object.

Enable or disable the effect by setting the [**FaceDetectionEffect.Enabled**](https://msdn.microsoft.com/library/windows/apps/dn948818) property. Adjust how often the effect analyzes frames by setting the [**FaceDetectionEffect.DesiredDetectionInterval**](https://msdn.microsoft.com/library/windows/apps/dn948814) property. Both of these properties can be adjusted while media capture is ongoing.

[!code-cs[CreateFaceDetectionEffectAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCreateFaceDetectionEffectAsync)]

### Receive notifications when faces are detected

If you want to perform some action when faces are detected, such as drawing a box around detected faces in the video preview, you can register for the [**FaceDetected**](https://msdn.microsoft.com/library/windows/apps/dn948820) event.

[!code-cs[RegisterFaceDetectionHandler](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetRegisterFaceDetectionHandler)]

In the handler for the event, you can get a list of all faces detected in a frame by accessing the [**FaceDetectionEffectFrame.DetectedFaces**](https://msdn.microsoft.com/library/windows/apps/dn948792) property of the [**FaceDetectedEventArgs**](https://msdn.microsoft.com/library/windows/apps/dn948774). The [**FaceBox**](https://msdn.microsoft.com/library/windows/apps/dn974126) property is a [**BitmapBounds**](https://msdn.microsoft.com/library/windows/apps/br226169) structure that describes the rectangle containing the detected face in units relative to the preview stream dimensions. To view sample code that transforms the preview stream coordinates into screen coordinates, see the [face detection UWP sample](http://go.microsoft.com/fwlink/?LinkId=619486).

[!code-cs[FaceDetected](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetFaceDetected)]

### Clean up the face detection effect

When your app is done capturing, before disposing of the **MediaCapture** object, you should disable the face detection effect with [**FaceDetectionEffect.Enabled**](https://msdn.microsoft.com/library/windows/apps/dn948818) and unregister your [**FaceDetected**](https://msdn.microsoft.com/library/windows/apps/dn948820) event handler if you previously registered one. Call [**MediaCapture.ClearEffectsAsync**](https://msdn.microsoft.com/library/windows/apps/br226592), specifying the video preview stream since that was the stream to which the effect was added. Finally, set your member variable to null.

[!code-cs[CleanUpFaceDetectionEffectAsync](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCleanUpFaceDetectionEffectAsync)]

### Check for focus and exposure support for detected faces

Not all devices have a capture device that can adjust its focus and exposure based on detected faces. Because face detection consumes device resources, you may only want to enable face detection on devices that can use the feature to enhance capture. To see if face-based capture optimization is available, get the [**VideoDeviceController**](https://msdn.microsoft.com/library/windows/apps/br226825) for your initialized [MediaCapture](capture-photos-and-video-with-mediacapture.md) and then get the video device controller's [**RegionsOfInterestControl**](https://msdn.microsoft.com/library/windows/apps/dn279064). Check to see if the [**MaxRegions**](https://msdn.microsoft.com/library/windows/apps/dn279069) supports at least one region. Then check to see if either [**AutoExposureSupported**](https://msdn.microsoft.com/library/windows/apps/dn279065) or [**AutoFocusSupported**](https://msdn.microsoft.com/library/windows/apps/dn279066) are true. If these conditions are met, then the device can take advantage of face detection to enhance capture.

[!code-cs[AreFaceFocusAndExposureSupported](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetAreFaceFocusAndExposureSupported)]

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
 

 




