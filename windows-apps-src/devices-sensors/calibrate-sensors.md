---
author: muhsinking
ms.assetid: ECE848C2-33DE-46B0-BAE7-647DB62779BB
title: Calibrate sensors
description: Sensors in a device based on the magnetometer – the compass, inclinometer and orientation sensor - can become in need of calibration due to environmental factors.
ms.author: mukin
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Calibrate sensors

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

**Important APIs**

-   [**Windows.Devices.Sensors**](https://msdn.microsoft.com/library/windows/apps/BR206408)
-   [**Windows.Devices.Sensors.Custom**](https://msdn.microsoft.com/library/windows/apps/Dn895032)

Sensors in a device based on the magnetometer – the compass, inclinometer and orientation sensor - can become in need of calibration due to environmental factors. The [**MagnetometerAccuracy**](https://msdn.microsoft.com/library/windows/apps/Dn297552) enumeration can help determine a course of action when your device is in need of calibration.

## When to calibrate the magnetometer

The [**MagnetometerAccuracy**](https://msdn.microsoft.com/library/windows/apps/Dn297552) enumeration has four values that help you determine if the device your app is running on needs to be calibrated. If a device needs to be calibrated, you should let the user know that calibration is needed. However, you should not prompt the user to calibrate too frequently. We recommend no more than once every 10 minutes.

| Value           | Description                                                                                                                                                      |-----------------|-------------------|                                                                                                                                              | **Unknown**     | The sensor driver could not report the current accuracy. This does not necessarily mean the device is out of calibration. It is up to your app to decide the best course of action if **Unknown** is returned. If your app is dependant on an accurate sensor reading, you may want to prompt the user to calibrate the device. |
| **Unreliable**  | There is currently a high degree of inaccuracy in the magnetometer. Apps should always ask for a calibration from the user when this value is first returned. |
| **Approximate** | The data is accurate enough for some applications. A virtual reality app, that only needs to know if the user has moved the device up/down or left/right, can continue without calibration. Apps that need an absolute heading, like a navigation app that needs to know what direction you are driving in order to give you directions, need to ask for calibration. |
| **High**        | The data is precise. No calibration is needed, even for apps that need to know an absolute heading such as augmented reality or navigation apps. |

## How to calibrate the magnetometer

This short video gives an overview of how to calibrate the magnetometer.<iframe src="https://hubs-video.ssl.catalog.video.msn.com/embed/727bd0e3-9116-49c3-8af6-0b4339324b71/IA?csid=ux-en-us&MsnPlayerLeadsWith=html&PlaybackMode=Inline&MsnPlayerDisplayShareBar=false&MsnPlayerDisplayInfoButton=false&iframe=true&QualityOverride=HD" width="720" height="405" allowFullScreen="true" frameBorder="0" scrolling="no">One dev minute - Sensor Calibration</iframe>
