---
ms.assetid: 708170E1-777A-4E4A-9F77-5AB28B88B107
description: This article shows you how to use manual device controls to enable enhanced video capture scenarios including HDR video and exposure priority.
title: Manual camera controls for video capture
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Manual camera controls for video capture



This article shows you how to use manual device controls to enable enhanced video capture scenarios, including HDR video and exposure priority.

The video device controls discussed in this article are all added to your app by using the same pattern. First, check to see if the control is supported on the current device on which your app is running. If the control is supported, set the desired mode for the control. Typically, if a particular control is unsupported on the current device, you should disable or hide the UI element that allows the user to enable the feature.

All of the device control APIs discussed in this article are members of the [**Windows.Media.Devices**](/uwp/api/Windows.Media.Devices) namespace.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetVideoControllersUsing":::

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

## HDR video

The high dynamic range (HDR) video feature applies HDR processing to the video stream of the capture device. Determine if HDR video is supported by selecting the [**HdrVideoControl.Supported**](/uwp/api/windows.media.devices.hdrvideocontrol.supported) property.

The HDR video control supports three modes: on, off, and automatic, which means that the device dynamically determines if HDR video processing would improve the media capture and, if so, enables HDR video. To determine if a particular mode is supported on the current device, check to see if the [**HdrVideoControl.SupportedModes**](/uwp/api/windows.media.devices.hdrvideocontrol.supportedmodes) collection contains the desired mode.

Enable or disable HDR video processing by setting the [**HdrVideoControl.Mode**](/uwp/api/windows.media.devices.hdrvideocontrol.mode) to the desired mode. This control requires that the stream is at a stopped state before the mode is set, see [KSPROPERTY_CAMERACONTROL_EXTENDED_VIDEOHDR](/windows-hardware/drivers/stream/ksproperty-cameracontrol-extended-videohdr).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetSetHdrVideoMode":::

## Exposure priority

The [**ExposurePriorityVideoControl**](/uwp/api/Windows.Media.Devices.ExposurePriorityVideoControl), when enabled, evaluates the video frames from the capture device to determine if the video is capturing a low-light scene. If so, the control lowers the frame rate of the captured video in order to increase the exposure time for each frame and improve the visual quality of the captured video.

Determine if the exposure priority control is supported on the current device by checking the [**ExposurePriorityVideoControl.Supported**](/uwp/api/windows.media.devices.exposurepriorityvideocontrol.supported) property.

Enable or disable the exposure priority control by setting the [**ExposurePriorityVideoControl.Enabled**](/uwp/api/windows.media.devices.exposurepriorityvideocontrol.enabled) to the desired mode.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetEnableExposurePriority":::

## Temporal denoising
Starting with Windows 10, version 1803, you can enable temporal denoising for video on devices that support it. This feature fuses the image data from multiple adjacent frames in real time to produce video frames that have less visual noise.

The [**VideoTemporalDenoisingControl**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol) allows your app to determine if temporal denoising is supported on the current device, and if so, which denoising modes are supported. The available denoising modes are [**Off**](/uwp/api/windows.media.devices.videotemporaldenoisingmode), [**On**](/uwp/api/windows.media.devices.videotemporaldenoisingmode), and [**Auto**](/uwp/api/windows.media.devices.videotemporaldenoisingmode). A device may not support all modes, but every device must support either **Auto** or **On** and **Off**.

The following example uses a simple UI to provide radio buttons allowing the user to switch between denoising modes.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml" id="SnippetDenoiseXAML":::

In the following method, the [**VideoTemporalDenoisingControl.Supported**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol.supported) property is checked to see if temporal denoising is supported at all on the current device. If so, then we check to make sure that **Off** and **Auto** or **On** is supported, in which case we make our radio buttons visible. Next, the **Auto** and **On** buttons are made visible if those methods are supported.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs" id="SnippetUpdateDenoiseCapabilities":::

In the **Checked** event handler for the radio buttons, the name of the button is checked and the corresponding mode is set by setting the [**VideoTemporalDenoisingControl.Mode**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol.mode) property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs" id="SnippetDenoiseButtonChecked":::

### Disabling temporal denoising while processing frames
Video that has been processed using temporal denoising can be more pleasing to the human eye. However, because temporal denoising can impact image consistency and decrease the amount of details in the frame, apps that perform image processing on the frames, such as registration or optical character recognition, may want to programmatically disable denoising when image processing is enabled.

The following example determines which denoising modes are supported and stores this information in some class variables.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs" id="SnippetDenoiseFrameReaderVars":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs" id="SnippetDenoiseCapabilitiesForFrameProcessing":::

When the app enables frame processing, it sets the denoising mode to **Off** if that mode is supported so that the frame processing can use raw frames that have not been denoised.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs" id="SnippetEnableFrameProcessing":::

When the app disables frame processing, it sets the denoising mode to **On** or **Auto**, depending on which mode is supported.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs" id="SnippetDisableFrameProcessing":::

For more information on obtaining video frames for image processing, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md).

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
*  [**VideoTemporalDenoisingControl**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol)
 
