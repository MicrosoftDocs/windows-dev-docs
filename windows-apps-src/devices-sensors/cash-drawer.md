---
author: mukin
title: Cash Drawer
description: This article contains information about the Cash Drawer point of service family of devices
ms.author: wdg-dev-content
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid:
---

# Cash Drawer

Enables application developers to interact with [cash drawers](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawer).

## Requirements
Applications which require this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Device support
Connection directly to the cash drawer can be made over the network or through Bluetooth, depending on the capabilities of the cash drawer unit. Additionally, cash drawers that don’t have network or Bluetooth capabilities can be connected via the DK port on a supported POS printer, or the Star Micronics DK-AirCash accessory. At this time, there is no support for cash drawers connected via USB or serial port.

**Note:**  Contact Star Micronics for more information about the DK-AirCash.

### Network/Bluetooth
| Manufacturer |	Model(s) |
|--------------|-----------|
| APG Cash Drawer |	NetPRO, BluePRO |

## Examples
See the cash drawer sample for an example implementation.
+	[Cash drawer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CashDrawer)
