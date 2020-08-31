---
ms.assetid: 3b75d881-bdcf-402b-a330-23cd29d68e53
description: This article lists the DeviceInformation properties related to audio devices
title: Audio device information properties
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Audio device information properties

This article lists the device information properties related to audio devices. On Windows, each hardware device has associated [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) properties providing detailed information about a device that you can use when you need specific information about the device or when you are building a device selector. 
For general information about enumerating devices on Windows, see [Enumerate devices](../devices-sensors/enumerate-devices.md) and [Device information properties](../devices-sensors/device-information-properties.md).


|Name|Type|Description|
|------------------------------------------------------------|------------|------------------------------------------------------|
|**System.Devices.AudioDevice.Microphone.SensitivityInDbfs**|Double|Specifies the microphone sensitivity in decibels relative to full scale (dBFS) units.|
|**System.Devices.AudioDevice.Microphone.SignalToNoiseRatioInDb**|Double|Specifies the microphone signal to noise ratio (SNR) measured in decibel (dB) units.|
|**System.Devices.AudioDevice.SpeechProcessingSupported**|Boolean|Indicates whether the audio device supports speech processing.|
|**System.Devices.AudioDevice.RawProcessingSupported**|Boolean|Indicates whether the audio device supports raw processing.|
|**System.Devices.MicrophoneArray.Geometry**|unsigned char[]|Geometry data for a microphone array.|

## Related topics

* [Enumerate devices](../devices-sensors/enumerate-devices.md)
* [Device information properties](../devices-sensors/device-information-properties.md)
* [Build a device selector](../devices-sensors/build-a-device-selector.md)
* [Media playback](media-playback.md)