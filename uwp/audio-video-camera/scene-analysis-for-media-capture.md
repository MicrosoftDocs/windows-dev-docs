---
ms.assetid: B5D915E4-4280-422C-BA0E-D574C534410B
description: This article describes how to use the SceneAnalysisEffect and the FaceDetectionEffect to analyze the content of the media capture preview stream.
title: Effects for analyzing camera frames
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Effects for analyzing camera frames



This article describes how to use the [**SceneAnalysisEffect**](/uwp/api/Windows.Media.Core.SceneAnalysisEffect) and the [**FaceDetectionEffect**](/uwp/api/Windows.Media.Core.FaceDetectionEffect) to analyze the content of the media capture preview stream.

## Scene analysis effect

The [**SceneAnalysisEffect**](/uwp/api/Windows.Media.Core.SceneAnalysisEffect) analyzes the video frames in the media capture preview stream and recommends processing options to improve the capture result. Currently, the effect supports detecting whether the capture would be improved by using High Dynamic Range (HDR) processing.

If the effect recommends using HDR, you can do this in the following ways:

-   Use the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) class to capture photos using the Windows built-in HDR processing algorithm. For more information, see [High Dynamic Range (HDR) photo capture](high-dynamic-range-hdr-photo-capture.md).

-   Use the [**HdrVideoControl**](/uwp/api/Windows.Media.Devices.HdrVideoControl) to capture video using the Windows built-in HDR processing algorithm. For more information, see [Capture device controls for video capture](capture-device-controls-for-video-capture.md).

-   Use the [**VariablePhotoSequenceControl**](/uwp/api/Windows.Media.Devices.Core.VariablePhotoSequenceController) to capture a sequence of frames that you can then composite using a custom HDR implementation. For more information, see [Variable photo sequence](variable-photo-sequence.md).

### Scene analysis namespaces

To use scene analysis, your app must include the following namespaces in addition to the namespaces required for basic media capture.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetSceneAnalysisUsing":::

### Initialize the scene analysis effect and add it to the preview stream

Video effects are implemented using two APIs, an effect definition, which provides settings that the capture device needs to initialize the effect, and an effect instance, which can be used to control the effect. Since you may want to access the effect instance from multiple places within your code, you should typically declare a member variable to hold the object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetDeclareSceneAnalysisEffect":::

In your app, after you have initialized the **MediaCapture** object, create a new instance of [**SceneAnalysisEffectDefinition**](/uwp/api/Windows.Media.Core.SceneAnalysisEffectDefinition).

Register the effect with the capture device by calling [**AddVideoEffectAsync**](/uwp/api/windows.media.capture.mediacapture.addvideoeffectasync) on your **MediaCapture** object, providing the **SceneAnalysisEffectDefinition** and specifying [**MediaStreamType.VideoPreview**](/uwp/api/Windows.Media.Capture.MediaStreamType) to indicate that the effect should be applied to the video preview stream, as opposed to the capture stream. **AddVideoEffectAsync** returns an instance of the added effect. Because this method can be used with multiple effect types, you must cast the returned instance to a [**SceneAnalysisEffect**](/uwp/api/Windows.Media.Core.SceneAnalysisEffect) object.

To receive the results of the scene analysis, you must register a handler for the [**SceneAnalyzed**](/uwp/api/windows.media.core.sceneanalysiseffect.sceneanalyzed) event.

Currently, the scene analysis effect only includes the high dynamic range analyzer. Enable HDR analysis by setting the effect's [**HighDynamicRangeControl.Enabled**](/uwp/api/windows.media.core.highdynamicrangecontrol.enabled) to true.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCreateSceneAnalysisEffectAsync":::

### Implement the SceneAnalyzed event handler

The results of the scene analysis are returned in the **SceneAnalyzed** event handler. The [**SceneAnalyzedEventArgs**](/uwp/api/Windows.Media.Core.SceneAnalyzedEventArgs) object passed into the handler has a [**SceneAnalysisEffectFrame**](/uwp/api/Windows.Media.Core.SceneAnalysisEffectFrame) object which has a [**HighDynamicRangeOutput**](/uwp/api/Windows.Media.Core.HighDynamicRangeOutput) object. The [**Certainty**](/uwp/api/windows.media.core.highdynamicrangeoutput.certainty) property of the high dynamic range output provides a value between 0 and 1.0 where 0 indicates that HDR processing would not help improve the capture result and 1.0 indicates that HDR processing would help. You can decide the threshold point at which you want to use HDR or show the results to the user and let the user decide.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetSceneAnalyzed":::

The [**HighDynamicRangeOutput**](/uwp/api/Windows.Media.Core.HighDynamicRangeOutput) object passed into the handler also has a [**FrameControllers**](/uwp/api/windows.media.core.highdynamicrangeoutput.framecontrollers) property which contains suggested frame controllers for capturing a variable photo sequence for HDR processing. For more information, see [Variable photo sequence](variable-photo-sequence.md).

### Clean up the scene analysis effect

When your app is done capturing, before disposing of the **MediaCapture** object, you should disable the scene analysis effect by setting the effect's [**HighDynamicRangeAnalyzer.Enabled**](/uwp/api/windows.media.core.highdynamicrangecontrol.enabled) property to false and unregister your [**SceneAnalyzed**](/uwp/api/windows.media.core.sceneanalysiseffect.sceneanalyzed) event handler. Call [**MediaCapture.ClearEffectsAsync**](/uwp/api/windows.media.capture.mediacapture.cleareffectsasync), specifying the video preview stream since that was the stream to which the effect was added. Finally, set your member variable to null.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCleanUpSceneAnalysisEffectAsync":::

## Face detection effect

The [**FaceDetectionEffect**](/uwp/api/Windows.Media.Core.FaceDetectionEffect) identifies the location of faces within the media capture preview stream. The effect allows you to receive a notification whenever a face is detected in the preview stream and provides the bounding box for each detected face within the preview frame. On supported devices, the face detection effect also provides enhanced exposure and focus on the most important face in the scene.

### Face detection namespaces

To use face detection, your app must include the following namespaces in addition to the namespaces required for basic media capture.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetFaceDetectionUsing":::

### Initialize the face detection effect and add it to the preview stream

Video effects are implemented using two APIs, an effect definition, which provides settings that the capture device needs to initialize the effect, and an effect instance, which can be used to control the effect. Since you may want to access the effect instance from multiple places within your code, you should typically declare a member variable to hold the object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetDeclareFaceDetectionEffect":::

In your app, after you have initialized the **MediaCapture** object, create a new instance of [**FaceDetectionEffectDefinition**](/uwp/api/Windows.Media.Core.FaceDetectionEffectDefinition). Set the [**DetectionMode**](/uwp/api/windows.media.core.facedetectioneffectdefinition.detectionmode) property to prioritize faster face detection or more accurate face detection. Set [**SynchronousDetectionEnabled**](/uwp/api/windows.media.core.facedetectioneffectdefinition.synchronousdetectionenabled) to specify that incoming frames are not delayed waiting for face detection to complete as this can result in a choppy preview experience.

Register the effect with the capture device by calling [**AddVideoEffectAsync**](/uwp/api/windows.media.capture.mediacapture.addvideoeffectasync) on your **MediaCapture** object, providing the **FaceDetectionEffectDefinition** and specifying [**MediaStreamType.VideoPreview**](/uwp/api/Windows.Media.Capture.MediaStreamType) to indicate that the effect should be applied to the video preview stream, as opposed to the capture stream. **AddVideoEffectAsync** returns an instance of the added effect. Because this method can be used with multiple effect types, you must cast the returned instance to a [**FaceDetectionEffect**](/uwp/api/Windows.Media.Core.FaceDetectionEffect) object.

Enable or disable the effect by setting the [**FaceDetectionEffect.Enabled**](/uwp/api/windows.media.core.facedetectioneffect.enabled) property. Adjust how often the effect analyzes frames by setting the [**FaceDetectionEffect.DesiredDetectionInterval**](/uwp/api/windows.media.core.facedetectioneffect.desireddetectioninterval) property. Both of these properties can be adjusted while media capture is ongoing.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCreateFaceDetectionEffectAsync":::

### Receive notifications when faces are detected

If you want to perform some action when faces are detected, such as drawing a box around detected faces in the video preview, you can register for the [**FaceDetected**](/uwp/api/windows.media.core.facedetectioneffect.facedetected) event.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetRegisterFaceDetectionHandler":::

In the handler for the event, you can get a list of all faces detected in a frame by accessing the [**FaceDetectionEffectFrame.DetectedFaces**](/uwp/api/windows.media.core.facedetectioneffectframe.detectedfaces) property of the [**FaceDetectedEventArgs**](/uwp/api/Windows.Media.Core.FaceDetectedEventArgs). The [**FaceBox**](/uwp/api/windows.media.faceanalysis.detectedface.facebox) property is a [**BitmapBounds**](/uwp/api/Windows.Graphics.Imaging.BitmapBounds) structure that describes the rectangle containing the detected face in units relative to the preview stream dimensions. To view sample code that transforms the preview stream coordinates into screen coordinates, see the [face detection UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFaceDetection).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetFaceDetected":::

### Clean up the face detection effect

When your app is done capturing, before disposing of the **MediaCapture** object, you should disable the face detection effect with [**FaceDetectionEffect.Enabled**](/uwp/api/windows.media.core.facedetectioneffect.enabled) and unregister your [**FaceDetected**](/uwp/api/windows.media.core.facedetectioneffect.facedetected) event handler if you previously registered one. Call [**MediaCapture.ClearEffectsAsync**](/uwp/api/windows.media.capture.mediacapture.cleareffectsasync), specifying the video preview stream since that was the stream to which the effect was added. Finally, set your member variable to null.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCleanUpFaceDetectionEffectAsync":::

### Check for focus and exposure support for detected faces

Not all devices have a capture device that can adjust its focus and exposure based on detected faces. Because face detection consumes device resources, you may only want to enable face detection on devices that can use the feature to enhance capture. To see if face-based capture optimization is available, get the [**VideoDeviceController**](/uwp/api/Windows.Media.Devices.VideoDeviceController) for your initialized [MediaCapture](./index.md) and then get the video device controller's [**RegionsOfInterestControl**](/uwp/api/Windows.Media.Devices.RegionsOfInterestControl). Check to see if the [**MaxRegions**](/uwp/api/windows.media.devices.regionsofinterestcontrol.maxregions) supports at least one region. Then check to see if either [**AutoExposureSupported**](/uwp/api/windows.media.devices.regionsofinterestcontrol.autoexposuresupported) or [**AutoFocusSupported**](/uwp/api/windows.media.devices.regionsofinterestcontrol.autofocussupported) are true. If these conditions are met, then the device can take advantage of face detection to enhance capture.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetAreFaceFocusAndExposureSupported":::

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
 

 
