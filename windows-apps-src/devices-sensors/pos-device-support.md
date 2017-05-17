---
author: mukin
title: POS device support
description: This article contains information about device support for each POS device family
ms.author: mukin
ms.date: 05/17/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# POS device support

## Barcode scanner
| Connectivity | Support |
| -------------|-------------|
| USB          | <p>Windows contains a in-box class driver for USB connected barcode scanners which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](http://www.usb.org/developers/hidpage/). See the table below for a list of known compatible devices.  Consult the manual for your barcode scanner or contact the manufacturer to determine if it can be configured in USB.HID.POS Scanner mode. </p><p>Windows also supports implementation of vendor specific drivers to support additional barcode scanners that do not support the USB.HID.POS Scanner standard. Please check with your barcode scanner manufacturer for vendor specific driver availability.</p>|
| Bluetooth    | <p>Windows supports SPP-SSI based Bluetooth barcode scanners. See the table below for a list of known compatible devices.</p> |

### Compatible Hardware
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

## Cash drawer
| Connectivity | Support |
| -------------|-------------|
| Network/Bluetooth | <p> Connection directly to the cash drawer can be made over the network or through Bluetooth, depending on the capabilities of the cash drawer unit. </p>|
| DK port | <p> Cash drawers that don’t have network or Bluetooth capabilities can be connected via the DK port on a supported POS printer, or the Star Micronics DK-AirCash accessory. </p>
| OPOS    | <p> Supports any Cash Drawer devices that support OPOS drivers and/or provides OPOS service objects. Install the OPOS drivers as per the particular device manufacturers installation instructions. This enables USB and other connectivity based on manufacterer specifications. </p> |

**Note:**  Contact Star Micronics for more information about the DK-AirCash.

### Network/Bluetooth
| Manufacturer |	Model(s) |
|--------------|-----------|
| APG Cash Drawer |	NetPRO, BluePRO |

## Line display
Supports any line display devices that support OPOS drivers and/or provides OPOS service objects. Install the OPOS drivers as per the particular device manufacturers installation instructions.

## Magnetic stripe reader

Windows contains a in-box class driver for USB connected Magnetic stripe readers, which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](http://www.usb.org/developers/hidpage/).

### Vendor specific support
Windows provides support for the following magnetic stripe readers from Magtek and IDTech based on their Vendor ID and Product ID (VID/PID).

| Manufacturer | 	Model(s) |	Part Number |
|--------------|-----------|--------------|
| IDTech | SecureMag (VID:0ACD PID:2010) | IDRE-3x5xxxx |
| |	MiniMag (VID:0ACD PID:0500) |	IDMB-3x5xxxx |
| Magtek | MagneSafe (VID:0801 PID:0011) |	210730xx |
| |	Dynamag (VID:0801 PID:0002) |	210401xx |

### Custom vendor specific
Windows supports implementation of additional vendor specific drivers to support additional magnetic stripe readers. Please check with your magnetic stripe reader manufacturer for availability.

## POS printer
Windows supports the ability to print to network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language. For more information on ESC/POS, see [Epson ESC/POS with formatting](https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/epson-esc-pos-with-formatting).

While the classes, enumerations, and interfaces exposed in the API support receipt printer, slip printer, and journal printer, the driver interface only supports receipt printer. Attempting to use slip printer or journal printer at this time will return a status of not implemented.

| Connectivity | Support |
| -------------|-------------|
| Network/Bluetooth | <p> Connection directly to the POS printer can be made over the network or through Bluetooth, depending on the capabilities of the POS printer unit. </p>|
| OPOS    | <p> Supports any POS printer devices that support OPOS drivers and/or provides OPOS service objects. Install the OPOS drivers as per the particular device manufacturers installation instructions. This enables USB and other connectivity based on manufacterer specifications. </p> |

### Stationary POS printers (Network/Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	TM-T88V, TM-T70, TM-T20, TM-U220 |

### Mobile POS printers (Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	Mobilink P20 (TM-P20), Mobilink P60 (TM-P60), Mobilink P80 (TM-P80) |

## See also
+   [Windows.Devices.PointOfService namespace](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)