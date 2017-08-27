
---
author: muhsinking
title: Barcode scanner device support
description: This article contains information about the barcode scanner point of service family of devices
ms.author: mukin
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Barcode scanner device support

| Connectivity | Support |
| -------------|-------------|
| USB          | <p>Windows contains a in-box class driver for USB connected barcode scanners which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](http://www.usb.org/developers/hidpage/). See the table below for a list of known compatible devices.  Consult the manual for your barcode scanner or contact the manufacturer to determine if it can be configured in USB.HID.POS Scanner mode. </p><p>Windows also supports implementation of vendor specific drivers to support additional barcode scanners that do not support the USB.HID.POS Scanner standard. Please check with your barcode scanner manufacturer for vendor specific driver availability.</p>|
| Bluetooth    | <p>Windows supports SPP-SSI based Bluetooth barcode scanners. See the table below for a list of known compatible devices.</p> |

## Compatible Hardware
| Category | Connectivity | Manufacturer / Model |
|--------------|-----------|-----------|
| **1D Handheld Scanners** | **USB** |Honeywell Voyager 1200g<br/>Honeywell Voyager 1202g<br/>Honeywell Voyager 1202-bf<br/>Honeywell Voyager 145Xg (Upgradable)|
| **1D Handheld Scanners** | **Bluetooth** |Socket Mobile CHS 7Ci<br/> Socket Mobile CHS 7Di<br/> Socket Mobile CHS 7Mi<br/> Socket Mobile CHS 7Pi<br/>Socket Mobile DuraScan D700<br/> Socket Mobile DuraScan D730<br/>Socket Mobile SocketScan S800 (formerly CHS 8Ci) <br/>|
|**2D Handheld Scanners** | **USB** |Code Reader™ 950<br/>Code Reader™ 1021<br/>Code Reader™ 1421<br/> Honeywell Granit 198Xi<br/>Honeywell Granit 191Xi<br/>Honeywell Xenon 1900g<br/>Honeywell Xenon 1902g<br/>Honeywell Xenon 1902g-bf<br/>Honeywell Xenon 1900h<br/>Honeywell Xenon 1902h<br/>Honeywell Voyager 145Xg (Upgradable)<br/>Honeywell Voyager 1602g<br/>Intermec SG20|
|**2D Handheld Scanners** | **Bluetooth** |Socket Mobile SocketScan S850 (formerly CHS 8Qi)|
| **Presentation Scanners** | **USB** |Code Reader™ 5000<br/>Honeywell Genesis 7580g<br/>Honeywell Orbit 7190g|
| **In-Counter Scanners** | **USB** |Honeywell Stratos 2700|
| **Scan Engines** | **USB** | Honeywell N5680<br/>Honeywell N3680|
| **Windows Mobile Devices**| **Built-in** |Bluebird EF400<br/>Bluebird EF500<br/>Bluebird EF500R<br/>Honeywell CT50<br/>Honeywell D75e<br/>Janam XT2<br/>Panasonic FZ-E1<br/>Panasonic FZ-F1<br/>PointMobile PM80<br/>Zebra TC700j|
| **Windows Mobile Devices**| **Custom** | HP Elite X3 with Barcode Scanner Jacket |

## See also
+   [Windows.Devices.PointOfService namespace](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)
+   [BarcodeScanner class](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescanner)
+	[Barcode scanner sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)