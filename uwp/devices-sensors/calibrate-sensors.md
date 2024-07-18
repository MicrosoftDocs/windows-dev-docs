---

title: Calibrate sensors
description: The MagnetometerAccuracy enumeration can help determine when a device is in need of calibration.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Calibrate sensors

Sensors in a device based on the magnetometer – the compass, inclinometer and orientation sensor - can require calibration due to environmental factors. The [**MagnetometerAccuracy**](/uwp/api/Windows.Devices.Sensors.MagnetometerAccuracy) enumeration can help determine when a device is in need of calibration.

**Important APIs**

- [**Windows.Devices.Sensors**](/uwp/api/Windows.Devices.Sensors)
- [**Windows.Devices.Sensors.Custom**](/uwp/api/Windows.Devices.Sensors.Custom)

## When to calibrate the magnetometer

The [**MagnetometerAccuracy**](/uwp/api/Windows.Devices.Sensors.MagnetometerAccuracy) enumeration has four values that help you determine if the device your app is running on needs to be calibrated. If a device needs to be calibrated, you should let the user know that calibration is needed. However, you should not prompt the user to calibrate too frequently. We recommend no more than once every 10 minutes.

| Value           | Description    |
| ----------------- | ------------------- |
| **Unknown**     | The sensor driver could not report the current accuracy. This does not necessarily mean the device is out of calibration. It is up to your app to decide the best course of action if **Unknown** is returned. If your app is dependent on an accurate sensor reading, you may want to prompt the user to calibrate the device. |
| **Unreliable**  | There is currently a high degree of inaccuracy in the magnetometer. Apps should always ask for a calibration from the user when this value is first returned. |
| **Approximate** | The data is accurate enough for some applications. A virtual reality app, that only needs to know if the user has moved the device up/down or left/right, can continue without calibration. Apps that need an absolute heading, like a navigation app that needs to know what direction you are driving in order to give you directions, need to ask for calibration. |
| **High**        | The data is precise. No calibration is needed, even for apps that need to know an absolute heading such as augmented reality or navigation apps. |