
---
author: mukin
title: Barcode Scanner
description: This article contains information about the barcode scanner point of service family of devices
ms.author: wdg-dev-content
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Barcode Scanner
Enables application developers to access [barcode scanners](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescanner) to retrieve decoded data from a variety of barcode symbologies such as UPC and QR Codes depending on support from the hardware. See the [BarcodeSymbologies](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodesymbologies) class for a full list of supported symbologies.

## Requirements
Applications which utilize this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Examples
See the barcode scanner sample for an example implementation.
+	[Barcode scanner sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)

## Device support
| Connectivity | Support |
| -------------|-------------|
| USB          | <p>Windows contains a in-box class driver for USB connected barcode scanners which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](http://www.usb.org/developers/hidpage/). See the table below for a list of known compatible devices.  Consult the manual for your barcode scanner or contact the manufacturer to determine if it can be configured in USB.HID.POS Scanner mode. </p><p>Windows also supports implementation of vendor specific drivers to support additional barcode scanners that do not support the USB.HID.POS Scanner standard. Please check with your barcode scanner manufacturer for vendor specific driver availability.</p>|
| Bluetooth    | <p>Windows supports SPP-SSI based Bluetooth barcode scanners. See the table below for a list of known compatible devices.</p> |

## Compatible Hardware
| Category | Connectivity | Manufacturer / Model |
|--------------|-----------|-----------|
| **1D Handheld Scanners** | **USB** |Honeywell Voyager 1200g<br/>Honeywell Voyager 1202g<br/>Honeywell Voyager 1202-bf<br/>Honeywell Voyager 145Xg (Upgradable)|
| **1D Handheld Scanners** | **Bluetooth** |Socket Mobile CHS 7Ci<br/> Socket Mobile CHS 7Di<br/> Socket Mobile CHS 7Mi<br/> Socket Mobile CHS 7Pi<br/>Socket Mobile DuraScan D700<br/> Socket Mobile DuraScan D730<br/>Socket Mobile SocketScan S800 (formerly CHS 8Ci) <br/>|
|**2D Handheld Scanners** | **USB** |Code Reader™ 1021<br/>Code Reader™ 1421<br/> Honeywell Granit 198Xi<br/>Honeywell Granit 191Xi<br/>Honeywell Xenon 1900g<br/>Honeywell Xenon 1902g<br/>Honeywell Xenon 1902g-bf<br/>Honeywell Xenon 1900h<br/>Honeywell Xenon 1902h<br/>Honeywell Voyager 145Xg (Upgradable)<br/>Honeywell Voyager 1602g<br/>Intermec SG20|
|**2D Handheld Scanners** | **Bluetooth** |Socket Mobile SocketScan S850 (formerly CHS 8Qi)|
| **Presentation Scanners** | **USB** |Code Reader™ 5000<br/>Honeywell Genesis 7580g<br/>Honeywell Orbit 7190g|
| **In-Counter Scanners** | **USB** |Honeywell Stratos 2700|
| **Scan Engines** | **USB** | Honeywell N5680<br/>Honeywell N3680|
| **Windows Mobile Devices**| **Custom** | HP Elite X3 with Barcode Scanner Jacket |
