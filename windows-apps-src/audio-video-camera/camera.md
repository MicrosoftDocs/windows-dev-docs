---
author: drewbatgit
ms.assetid: 370f2c14-4f1e-47b3-9197-24205ab255a3
description: This article lists the camera features that are available for UWP apps and links to the how-to articles that show how to use them.
title: Camera
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Camera

This section provides guidance for creating Universal Windows Platform (UWP) apps that use the camera or microphone to capture photos, video, or audio.

## Use the Windows built-in camera UI

| Topic | Description |
|---------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Capture photos and video with Windows built-in camera UI](capture-photos-and-video-with-cameracaptureui.md) | Shows how to use the [**CameraCaptureUI**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.CameraCaptureUI) class to capture photos or videos using the camera UI built into Windows. If you simply want to enable the user to capture a photo or video and return the result to your app, this is the quickest and easiest way to do it.  |

## Basic MediaCapture tasks

| Topic | Description |
|---------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Display the camera preview](simple-camera-preview-access.md) | Shows how to quickly display the camera preview stream within a XAML page in a UWP app. |
| [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md) | Shows the simplest way to capture photos and video using the [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture) class. The **MediaCapture** class exposes a robust set of APIs that provide low-level control over the capture pipeline and enable advanced capture scenarios, but this article is intended to help you add basic media capture to your app quickly and easily. |
| [Camera UI features for mobile devices](camera-ui-features-for-mobile-devices.md) | Shows you how to take advantage of special camera UI features that are only present on mobile devices.  |
                                                                                                               
## Advanced MediaCapture tasks   
                                                                                                               
| Topic                                                                                             | Description                                                                                                                                                                                                                                                                                    |
|---------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Handle device and screen orientation with MediaCapture](handle-device-orientation-with-mediacapture.md) | Shows you how to handle device orientation when capturing photos and videos by using a helper class. | 
| [Discover and select camera capabilities with camera profiles](camera-profiles.md) | Shows how to use camera profiles to discover and manage the capabilities of different video capture devices. This includes tasks such as selecting profiles that support specific resolutions or frame rates, profiles that support simultaneous access to multiple cameras, and profiles that support HDR. |
| [Set format, resolution, and frame rate for MediaCapture](set-media-encoding-properties.md) | Shows you how to use the [**IMediaEncodingProperties**](https://msdn.microsoft.com/library/windows/apps/hh701011) interface to set the resolution and frame rate of the camera preview stream and captured photos and video. It also shows how to ensure that the aspect ratio of the preview stream matches that of the captured media. |
| [HDR and low-light photo capture](high-dynamic-range-hdr-photo-capture.md) | Shows you how to use the [**AdvancedPhotoCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.AdvancedPhotoCapture) class to capture High Dynamic Range (HDR) and low-light photos. |
| [Manual camera controls for photo and video capture](capture-device-controls-for-photo-and-video-capture.md) | Shows you how to use manual device controls to enable enhanced photo and video capture scenarios including optical image stabilization and smooth zoom. |
| [Manual camera controls for video capture](capture-device-controls-for-video-capture.md) | Shows you how to use manual device controls to enable enhanced video capture scenarios including HDR video and exposure priority.  |
| [Video stabilization effect for video capture](effects-for-video-capture.md) | Shows you how to use the video stabilization effect.  |
| [Scene anlysis for MediaCapture](scene-analysis-for-media-capture.md) | Shows you how to use the [**SceneAnalysisEffect**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Core.SceneAnalysisEffect) and the [**FaceDetectionEffect**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Core.FaceDetectionEffect) to analyze the content of the media capture preview stream.  |
| [Capture a photo sequence with VariablePhotoSequence](variable-photo-sequence.md) | Shows you how to capture a variable photo sequence, which allows you to capture multiple frames of images in rapid succession and configure each frame to use different focus, flash, ISO, exposure, and exposure compensation settings.  |
| [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md) | Shows you how to use a [**MediaFrameReader**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.Frames.MediaFrameReader) with [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.MediaCapture) to get media frames from one or more available sources, including color, depth, and infrared cameras, audio devices, or even custom frame sources such as those that produce skeletal tracking frames. This feature is designed to be used by apps that perform real-time processing of media frames, such as augmented reality and depth-aware camera apps.  |
| [Get a preview frame](get-a-preview-frame.md) | Shows you how to get a single preview frame from the media capture preview stream.  |                                                                                                   


## UWP app samples for camera

* [Camera face detection sample](http://go.microsoft.com/fwlink/p/?LinkID=619486&clcid=0x409)
* [Camera preview frame sample](http://go.microsoft.com/fwlink/p/?LinkID=620516&clcid=0x409)
* [Camera HDR sample](http://go.microsoft.com/fwlink/p/?LinkID=620517&clcid=0x409)
* [Camera manual controls sample](http://go.microsoft.com/fwlink/p/?LinkID=627611&clcid=0x409)
* [Camera profile sample](http://go.microsoft.com/fwlink/p/?LinkID=620518&clcid=0x409)
* [Camera resolution sample](http://go.microsoft.com/fwlink/p/?LinkID=624252&clcid=0x409)
* [Camera starter kit](http://go.microsoft.com/fwlink/p/?LinkID=619479&clcid=0x409)
* [Camera video stabilization sample](http://go.microsoft.com/fwlink/p/?LinkID=620519&clcid=0x409)

## Related topics

* [Audio, video, and camera](index.md)
 

 




