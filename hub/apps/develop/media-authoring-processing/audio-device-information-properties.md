---
description: This article lists the DeviceInformation properties related to audio devices in Windows apps.
title: Audio device information properties
ms.date: 05/06/2026
ms.topic: article
keywords: windows, winui, audio, device, properties
ms.localizationpriority: medium
---

# Audio device information properties

This article lists the device information properties related to audio devices. On Windows, each hardware device has associated [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) properties that provide detailed information about a device. You can use these properties when you need specific information about a device or when you are building a device selector.

For general information about enumerating devices on Windows, see [Enumerate devices](/windows/uwp/devices-sensors/enumerate-devices) and [Device information properties](/windows/uwp/devices-sensors/device-information-properties).

|Name|Type|Description|
|----|------|-----------|
|**System.Devices.AudioDevice.Microphone.SensitivityInDbfs**|Double|Specifies the microphone sensitivity in decibels relative to full scale (dBFS) units.|
|**System.Devices.AudioDevice.Microphone.SensitivityInDbfs2**|Double|Specifies the microphone sensitivity in dBFS, measured after fixed hardware gain (if available). Assumes 0 dB software gain. Available starting with Windows 10, version 1803.|
|**System.Devices.AudioDevice.Microphone.SignalToNoiseRatioInDb**|Double|Specifies the microphone signal to noise ratio (SNR) measured in decibel (dB) units.|
|**System.Devices.AudioDevice.SpeechProcessingSupported**|Boolean|Indicates whether the audio device supports speech processing.|
|**System.Devices.AudioDevice.RawProcessingSupported**|Boolean|Indicates whether the audio device supports raw processing.|
|**System.Devices.MicrophoneArray.Geometry**|unsigned char[]|Geometry data for a microphone array.|

## Related topics

* [Enumerate devices](/windows/uwp/devices-sensors/enumerate-devices)
* [Device information properties](/windows/uwp/devices-sensors/device-information-properties)
* [Build a device selector](/windows/uwp/devices-sensors/build-a-device-selector)
* [Media playback](/windows/apps/develop/media-playback/media-playback)
