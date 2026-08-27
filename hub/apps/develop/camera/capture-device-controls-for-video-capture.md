---
description: Learn how to use manual device controls to enable enhanced video capture scenarios including HDR video and exposure priority in a WinUI app.
title: Manual camera controls for video capture
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, winui 3
ms.localizationpriority: medium
---

# Manual camera controls for video capture

This article shows you how to use manual device controls to enable enhanced video capture scenarios, including HDR video and exposure priority.

The video device controls discussed in this article are all added to your app by using the same pattern. First, check to see if the control is supported on the current device on which your app is running. If the control is supported, set the desired mode for the control. Typically, if a particular control is unsupported on the current device, you should disable or hide the UI element that allows the user to enable the feature.

> [!NOTE]
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of **MediaCapture** that has been properly initialized.

## HDR video

The high dynamic range (HDR) video feature applies HDR processing to the video stream of the capture device. Determine if HDR video is supported by selecting the [**HdrVideoControl.Supported**](/uwp/api/windows.media.devices.hdrvideocontrol.supported) property.

The HDR video control supports three modes: on, off, and automatic, which means that the device dynamically determines if HDR video processing would improve the media capture and, if so, enables HDR video. To determine if a particular mode is supported on the current device, check to see if the [**HdrVideoControl.SupportedModes**](/uwp/api/windows.media.devices.hdrvideocontrol.supportedmodes) collection contains the desired mode.

Enable or disable HDR video processing by setting the [**HdrVideoControl.Mode**](/uwp/api/windows.media.devices.hdrvideocontrol.mode) to the desired mode. This control requires that the stream is at a stopped state before the mode is set, see [KSPROPERTY_CAMERACONTROL_EXTENDED_VIDEOHDR](/windows-hardware/drivers/stream/ksproperty-cameracontrol-extended-videohdr).

```csharp
private void SetHdrVideoMode(HdrVideoMode mode)
{
    if (!m_mediaCapture.VideoDeviceController.HdrVideoControl.Supported)
    {
        tbStatus.Text = "HDR Video not available";
        return;
    }

    var hdrVideoModes = m_mediaCapture.VideoDeviceController.HdrVideoControl.SupportedModes;

    if (!hdrVideoModes.Contains(mode))
    {
        tbStatus.Text = "HDR Video setting not supported";
        return;
    }

    m_mediaCapture.VideoDeviceController.HdrVideoControl.Mode = mode;
}
```

## Exposure priority

The [**ExposurePriorityVideoControl**](/uwp/api/Windows.Media.Devices.ExposurePriorityVideoControl), when enabled, evaluates the video frames from the capture device to determine if the video is capturing a low-light scene. If so, the control lowers the frame rate of the captured video in order to increase the exposure time for each frame and improve the visual quality of the captured video.

Determine if the exposure priority control is supported on the current device by checking the [**ExposurePriorityVideoControl.Supported**](/uwp/api/windows.media.devices.exposurepriorityvideocontrol.supported) property.

Enable or disable the exposure priority control by setting the [**ExposurePriorityVideoControl.Enabled**](/uwp/api/windows.media.devices.exposurepriorityvideocontrol.enabled) to the desired mode.

```csharp
if (!m_mediaCapture.VideoDeviceController.ExposurePriorityVideoControl.Supported)
{
    tbStatus.Text = "Exposure priority not available";
    return;
}
m_mediaCapture.VideoDeviceController.ExposurePriorityVideoControl.Enabled = true;
```

## Temporal denoising

Starting with Windows 10, version 1803, you can enable temporal denoising for video on devices that support it. This feature fuses the image data from multiple adjacent frames in real time to produce video frames that have less visual noise.

The [**VideoTemporalDenoisingControl**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol) allows your app to determine if temporal denoising is supported on the current device, and if so, which denoising modes are supported. The available denoising modes are [**Off**](/uwp/api/windows.media.devices.videotemporaldenoisingmode), [**On**](/uwp/api/windows.media.devices.videotemporaldenoisingmode), and [**Auto**](/uwp/api/windows.media.devices.videotemporaldenoisingmode). A device may not support all modes, but every device must support either **Auto** or **On** and **Off**.

The following example uses a simple UI to provide radio buttons allowing the user to switch between denoising modes.

```xml
<StackPanel Orientation="Vertical" HorizontalAlignment="Right" >
    <StackPanel x:Name="spDenoise" Visibility="Collapsed">
        <TextBlock>Temporal Denoising</TextBlock>
        <RadioButton x:Name="rbDenoiseOff" Checked="rbDenoise_Checked"
            GroupName="Denoise Group" Content="Off"/>
        <RadioButton x:Name="rbDenoiseOn" Checked="rbDenoise_Checked"
            GroupName="Denoise Group" Content="On" Visibility="Collapsed"/>
        <RadioButton x:Name="rbDenoiseAuto" Checked="rbDenoise_Checked"
            GroupName="Denoise Group" Content="Auto" Visibility="Collapsed"/>
    </StackPanel>
</StackPanel>
```

In the following method, the [**VideoTemporalDenoisingControl.Supported**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol.supported) property is checked to see if temporal denoising is supported at all on the current device. If so, then we check to make sure that **Off** and **Auto** or **On** is supported, in which case we make our radio buttons visible. Next, the **Auto** and **On** buttons are made visible if those methods are supported.

```csharp
private void bUpdateDenoiseCapabilities_Click(object sender, RoutedEventArgs e)
{
    if (m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Supported)
    {
         IReadOnlyList<VideoTemporalDenoisingMode> modes = m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.SupportedModes;
        if(modes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.Off) &&
           (modes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.On) ||
           modes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.Auto)))
        {
            spDenoise.Visibility = Visibility.Visible;

            if (modes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.On))
            {
                rbDenoiseOn.Visibility = Visibility.Visible;
            }
            if (modes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.Auto))
            {
                rbDenoiseAuto.Visibility = Visibility.Visible;
            }
        }
    }
}
```

In the **Checked** event handler for the radio buttons, the name of the button is checked and the corresponding mode is set by setting the [**VideoTemporalDenoisingControl.Mode**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol.mode) property.

```csharp
private void rbDenoise_Checked(object sender, RoutedEventArgs e)
{
    var button = sender as RadioButton;
    if(button.Name == "denoiseOffButton")
    {
        m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = Windows.Media.Devices.VideoTemporalDenoisingMode.Off;
    }
    else if (button.Name == "denoiseOnButton")
    {
        m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = Windows.Media.Devices.VideoTemporalDenoisingMode.On;
    }
    else if (button.Name == "denoiseAutoButton")
    {
        m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = Windows.Media.Devices.VideoTemporalDenoisingMode.Auto;
    }
}
```

### Disabling temporal denoising while processing frames

Video that has been processed using temporal denoising can be more pleasing to the human eye. However, because temporal denoising can impact image consistency and decrease the amount of details in the frame, apps that perform image processing on the frames, such as registration or optical character recognition, may want to programmatically disable denoising when image processing is enabled.

The following example determines which denoising modes are supported and stores this information in some class variables.

```csharp
private bool _isVideoTemporalDenoisingOffSupported = false;
private bool _isProcessing = false;
private Windows.Media.Devices.VideoTemporalDenoisingMode? _videoDenoisingEnabledMode = null;
```

```csharp
private void ConfigureDenoiseForFrameProcessing()
{
    if (m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Supported)
    {
        // Query support for the VideoTemporalDenoising control Off mode
        _isVideoTemporalDenoisingOffSupported = m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.SupportedModes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.Off);

        // Query support for a mode that would enable VideoTemporalDenoising (On or Auto) and toggle it if supported
        if (m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.SupportedModes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.On))
        {
            _videoDenoisingEnabledMode = Windows.Media.Devices.VideoTemporalDenoisingMode.On;
            m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = (Windows.Media.Devices.VideoTemporalDenoisingMode)_videoDenoisingEnabledMode;
        }
        else if (m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.SupportedModes.Contains(Windows.Media.Devices.VideoTemporalDenoisingMode.Auto))
        {
            _videoDenoisingEnabledMode = Windows.Media.Devices.VideoTemporalDenoisingMode.Auto;
            m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = (Windows.Media.Devices.VideoTemporalDenoisingMode)_videoDenoisingEnabledMode;
        }
    }

}
```

When the app enables frame processing, it sets the denoising mode to **Off** if that mode is supported so that the frame processing can use raw frames that have not been denoised.

```csharp
public void EnableFrameProcessing()
{
    // Toggle Off VideoTemporalDenoising
    if (_isVideoTemporalDenoisingOffSupported)
    {
        m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = Windows.Media.Devices.VideoTemporalDenoisingMode.Off;
    }

    _isProcessing = true;
}
```

When the app disables frame processing, it sets the denoising mode to **On** or **Auto**, depending on which mode is supported.

```csharp
public void DisableFrameProcessing()
{
    _isProcessing = false;

    // If a VideoTemporalDenoising mode to enable VideoTemporalDenoising is supported, toggle it
    if (_videoDenoisingEnabledMode != null)
    {
        m_mediaCapture.VideoDeviceController.VideoTemporalDenoisingControl.Mode = (Windows.Media.Devices.VideoTemporalDenoisingMode)_videoDenoisingEnabledMode;
    }
}
```

For more information on obtaining video frames for image processing, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md).

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
* [**VideoTemporalDenoisingControl**](/uwp/api/windows.media.devices.videotemporaldenoisingcontrol)
 
