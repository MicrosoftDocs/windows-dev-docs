---
author: muhsinking
title: Cash drawer device support
description: This article contains information about the Cash Drawer point of service family of devices
ms.author: mukin
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Cash drawer device support

| Connectivity | Support |
| -------------|-------------|
| Network/Bluetooth | <p> Connection directly to the cash drawer can be made over the network or through Bluetooth, depending on the capabilities of the cash drawer unit. </p>|
| DK port | <p> Cash drawers that don’t have network or Bluetooth capabilities can be connected via the DK port on a supported POS printer, or the Star Micronics DK-AirCash accessory. </p>
| OPOS    | <p> Supports any Cash Drawer devices that support OPOS drivers and/or provides OPOS service objects. Install the OPOS drivers as per the particular device manufacturers installation instructions. This enables USB and other connectivity based on manufacterer specifications. </p> |

**Note:**  Contact Star Micronics for more information about the DK-AirCash.

## Network/Bluetooth
| Manufacturer |	Model(s) |
|--------------|-----------|
| APG Cash Drawer |	NetPRO, BluePRO |

## See also
+   [Windows.Devices.PointOfService namespace](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)
+   [CashDrawer class](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawer)
+	[Cash drawer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CashDrawer)
