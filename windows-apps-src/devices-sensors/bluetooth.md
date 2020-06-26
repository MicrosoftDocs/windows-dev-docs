---
ms.assetid: 404783BA-8859-4BFB-86E3-3DD2042E66F5
title: Bluetooth
description: This section contains articles on how to integrate Bluetooth into Universal Windows Platform (UWP) apps, including how to use RFCOMM, GATT, and Low Energy (LE) Advertisements.
ms.date: 06/26/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Bluetooth
This section contains articles on how to integrate Bluetooth into Universal Windows Platform (UWP) apps. There are two different bluetooth technologies that you can choose to implement in your app.

> [!Important]
> You must declare the "bluetooth" capability in *Package.appxmanifest*.
>
> `<Capabilities> <DeviceCapability Name="bluetooth" /> </Capabilities>`

## Classic Bluetooth (RFCOMM)
Before Bluetooth LE, devices commonly used this protocol to communicate using Bluetooth. This protocol is simple and useful for device-to-device communication without the need of energy savings. For more information about this protocol, including code samples, see the [Bluetooth RFCOMM](send-or-receive-files-with-rfcomm.md) topic.

## Bluetooth Low-Energy (LE)
Bluetooth Low Energy (LE) is a specification that defines protocols for discovery and communication between devices that have an efficient energy usage requirement. For more information including code samples, see the [Bluetooth Low Energy](bluetooth-low-energy-overview.md) topic.

## See Also
- [Bluetooth developer FAQ](bluetooth-dev-faq.md)