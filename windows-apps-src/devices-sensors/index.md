---
ms.assetid: 0b891f63-02fa-4c30-b307-9fbcccac5caa
title: Devices, sensors, and power
description: In order to provide a rich experience for your users, you may find it necessary to integrate external devices or sensors into your app.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Devices, sensors, and power


In order to provide a rich experience for your users, you may find it necessary to integrate external devices or sensors into your app. Here are some examples of features that you can add to your app using the technology described in this section.

-   Providing an enhanced print experience
-   Integrating motion and orientation sensors into your game
-   Connecting to a device directly or through a network protocol

| Topic | Description |
|-------|-------------|
| [Enable device capabilities](enable-device-capabilities.md) | This tutorial describes how to declare device capabilities in Microsoft Visual Studio. This enables your app to use cameras, microphones, location sensors, and other devices. | 
| [Enable usermode access for Windows IoT](enable-usermode-access.md) | This tutorial describes how to enable usermode access to GPIO, I2C, SPI, and UART on Windows 10 IoT Core. |
| [Enumerate devices](enumerate-devices.md) | The enumeration namespace enables you to find devices that are internally connected to the system, externally connected, or detectable over wireless or networking protocols. |
| [Pair devices](pair-devices.md) | Some devices need to be paired before they can be used. The [<strong>Windows.Devices.Enumeration</strong>](/uwp/api/Windows.Devices.Enumeration) namespace supports three different ways to pair devices. |
| [Point of Service](point-of-service.md) | This section describes how to interact with point of service peripherals, such as barcode scanners, receipt printers, cash drawers, etc. | 
| [Sensors](sensors.md) | Sensors let your app know the relationship between a device and the physical world around it. Sensors can tell your app the direction, orientation, and movement of the device. |
| [Bluetooth](bluetooth.md) | This section contains articles on how to integrate Bluetooth into Universal Windows Platform (UWP) apps, including how to use RFCOMM, GATT, and Low Energy (LE) Advertisements. | 
| [Printing and scanning](printing-and-scanning.md) | This section describes how to print and scan from your Universal Windows app. | 
| [3D printing](3d-printing.md) | This section describes how to utilize 3D printing functionality in your Universal Windows app. |
| [Create an NFC Smart Card app](host-card-emulation.md) | Windows Phone 8.1 supported NFC card emulation apps using a SIM-based secure element, but that model required secure payment apps to be tightly coupled with mobile-network operators (MNO). This limited the variety of possible payment solutions by other merchants or developers that are not coupled with MNOs. In Windows 10 Mobile, we have introduced a new card emulation technology called, Host Card Emulation (HCE). HCE technology allows your app to directly communicate with an NFC card reader. This topic illustrates how Host Card Emulation (HCE) works on Windows 10 Mobile devices and how you can develop an HCE app so that your customers can access your services through their phone instead of a physical card without collaborating with an MNO. |
| [Get battery information](get-battery-info.md) | Learn how to get detailed battery information using APIs in the [<strong>Windows.Devices.Power</strong>](/uwp/api/Windows.Devices.Power) namespace. |