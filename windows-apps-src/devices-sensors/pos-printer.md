---
author: mukin
title: POS Printer
description: This article contains information about the point of service printer family of devices
ms.author: wdg-dev-content
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# POS Printer

Enables application developers to print to network and Bluetooth connected [receipt printers](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinter) using the Epson ESC/POS printer control language.

## Requirements
Applications which require this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Device support
Windows supports the ability to print to network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language. For more information on ESC/POS, see [Epson ESC/POS with formatting](https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/epson-esc-pos-with-formatting).

While the classes, enumerations, and interfaces exposed in the API support receipt printer, slip printer as well as journal printer, the driver interface only supports receipt printer. Attempting to use slip printer or journal printer at this time will return a status of not implemented.

Support is currently limited to the Network and Bluetooth device models listed in the tables below. USB connected printers are currently not supported. Please check back for additional support to be added in the future.

### Stationary POS printers (Network, Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	TM-T88V, TM-T70, TM-T20, TM-U220 |

### Mobile POS printers (Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	Mobilink P20 (TM-P20), Mobilink P60 (TM-P60), Mobilink P80 (TM-P80) |

## Examples
See the POS printer sample for an example implementation.
+ [POS printer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/PosPrinter)
