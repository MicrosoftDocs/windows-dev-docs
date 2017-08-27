---
author: muhsinking
title: POS printer device support
description: This article contains information about the point of service printer family of devices
ms.author: mukin
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# POS printer device support

Windows supports the ability to print to network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language. For more information on ESC/POS, see [Epson ESC/POS with formatting](https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/epson-esc-pos-with-formatting).

While the classes, enumerations, and interfaces exposed in the API support receipt printer, slip printer, and journal printer, the driver interface only supports receipt printer. Attempting to use slip printer or journal printer at this time will return a status of not implemented.

## Device support

| Connectivity | Support |
| -------------|-------------|
| Network/Bluetooth | <p> Connection directly to the POS printer can be made over the network or through Bluetooth, depending on the capabilities of the POS printer unit. </p>|
| OPOS    | <p> Supports any POS printer devices that support OPOS drivers and/or provides OPOS service objects. Install the OPOS drivers as per the particular device manufacturers installation instructions. This enables USB and other connectivity based on manufacterer specifications. </p> |

## Stationary POS printers (Network, Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	TM-T88V, TM-T70, TM-T20, TM-U220 |

## Mobile POS printers (Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	Mobilink P20 (TM-P20), Mobilink P60 (TM-P60), Mobilink P80 (TM-P80) |

## See also
+   [Windows.Devices.PointOfService namespace](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)
+   [POSPrinter class](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinter)
+   [POS printer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/PosPrinter)

