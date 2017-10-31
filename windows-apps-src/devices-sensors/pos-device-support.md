---
author: muhsinking
title: POS device support
description: This article contains information about device support for each POS device family
ms.author: mukin
ms.date: 05/17/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Supported Point of Service Peripherals

## Barcode Scanner
| Connectivity | Support |
| -------------|-------------|
| USB          | <p>Windows contains a in-box class driver for USB connected barcode scanners which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](http://www.usb.org/developers/hidpage/). See the table below for a list of known compatible devices.  Consult the manual for your barcode scanner or contact the manufacturer to determine how to configure your scanner in **USB.HID.POS Scanner** mode. </p><p>Windows also supports implementation of vendor specific drivers to support additional barcode scanners that do not support the USB.HID.POS Scanner standard. Please check with your barcode scanner manufacturer for vendor specific driver availability.</p><p>Barcode scanner manufacturers please consult the [Barcode Scanner Driver Design Guide](https://aka.ms/pointofservice-drv) for information on creating a custom barcode scanner driver</p> |
| Bluetooth    | <p>Windows supports Serial Port Protocol - Simple Serial Interface (SPP-SSI) based Bluetooth barcode scanners. See the table below for a list of known compatible devices. Consult the manual for your barcode scanner or contact the manufacturer to determine how to configure your scanner in **SPP-SSI** mode.</p> |

### Compatible Barcode Scanners
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


## Cash Drawer
| Connectivity | Support |
| -------------|-------------|
| Network/Bluetooth | <p> Connection directly to the cash drawer can be made over the network or through Bluetooth, depending on the capabilities of the cash drawer unit. </p><p>APG Cash Drawer:  NetPRO, BluePRO</p> |
| DK port | <p> Cash drawers that do not have network or Bluetooth capabilities can be connected via the DK port on a supported Receipt Printer or the Star Micronics DK-AirCash accessory. </p>
| OPOS    | <p> Supports any OPOS compatible Cash Drawers via OPOS service objects provided by the manufacturer. Install the OPOS drivers as per the particular device manufacturers installation instructions. </p> |


## Customer Display (LineDisplay)
Supports any OPOS compatible line displays via OPOS service objects provided by the manufacturer. Install the OPOS drivers as per the particular device manufacturers installation instructions.

## Magnetic Stripe Reader
Windows provides support for the following magnetic stripe readers from Magtek and IDTech based on their Vendor ID and Product ID (VID/PID).

| Manufacturer | 	Model(s) |	Part Number |
|--------------|-----------|--------------|
| IDTech | SecureMag (VID:0ACD PID:2010) | IDRE-3x5xxxx |
| |	MiniMag (VID:0ACD PID:0500) |	IDMB-3x5xxxx |
| Magtek | MagneSafe (VID:0801 PID:0011) |	210730xx |
| |	Dynamag (VID:0801 PID:0002) |	210401xx |

<p>Windows supports implementation of additional vendor specific drivers to support additional magnetic stripe readers. Please check with your magnetic stripe reader manufacturer for availability.</p><p>Magnetic stripe reader manufacturers please consult the [Magnetic Stripe Reader Driver Design Guide](https://aka.ms/pointofservice-drv) for information on creating a custom magnetic stripe reader driver</p>

## Receipt Printer (POSPrinter)
| Connectivity | Support |
| -------------|-------------|
| Network and Bluetooth | <p>Windows supports network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language.  The printers listed below are discovered automatically using POSPrinter APIs. Additional receipt printers which provide an ESC/POS emulation may also work, but would need to be associated using an [out of band pairing](https://aka.ms/pointofservice-oobpairing) process.</p><p>Note: slip station and journal stations are not supported through this method.</p> |
| OPOS    | <p> Supports any OPOS compatible receipt printers via OPOS service objects. Install the OPOS drivers as per the particular device manufacturers installation instructions. </p> |

### Stationary Receipt Printers (Network/Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	TM-T88V, TM-T70, TM-T20, TM-U220 |

### Mobile Receipt Printers (Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	Mobilink P20 (TM-P20), Mobilink P60 (TM-P60), Mobilink P80 (TM-P80) |
