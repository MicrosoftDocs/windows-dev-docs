---
title: Point of Service Hardware Support
description: This article contains information about hardware support for each of the Point of Service device classes
ms.date: 06/13/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Supported Point of Service Peripherals

## Barcode Scanner
| Connectivity | Support |
| -------------|-------------|
| USB          | <p>Windows contains an in-box class driver for USB connected barcode scanners which is based on the HID POS Scanner Usage Table (8c) specification defined by [USB.org](https://www.usb.org/hid). See the table below for a list of known compatible devices.  Consult the manual for your barcode scanner or contact the manufacturer to determine how to configure your scanner in **USB.HID.POS Scanner** mode. </p><p>Windows also supports implementation of vendor specific drivers to support additional barcode scanners that do not support the USB.HID.POS Scanner standard. Please check with your barcode scanner manufacturer for vendor specific driver availability.</p><p>Barcode scanner manufacturers please consult the [Barcode Scanner Driver Design Guide](/windows-hardware/drivers/ddi/_pos/index) for information on creating a custom barcode scanner driver</p> |
| Bluetooth    | <p>Windows supports Serial Port Protocol - Simple Serial Interface (SPP-SSI) based Bluetooth barcode scanners. See the table below for a list of known compatible devices. Consult the manual for your barcode scanner or contact the manufacturer to determine how to configure your scanner in **SPP-SSI** mode.</p> |
| Webcam       | <p>Starting with Windows 10, version 1803, you can read barcodes through a standard camera lens from a Universal Windows Application. It is recommended that you use a camera that supports Auto Focus and a minimum resolution of 1920 x 1440.  Some lower resolution cameras can read standard barcodes if the barcode is printed large enough.  Barcodes with thinner elements may require higher resolution cameras.</p>| 
|


| Manufacturer  | Model                          | Capability | Connection    | Type         | Mode                      |
|---------------|--------------------------------|------------|--------------|--------------|---------------------------|
| Code          | Reader™ 950                    | 2D         | USB          | Handheld     | HID POS Scanner           |
| Code          | Reader™ 1021                   | 2D         | USB          | Handheld     | HID POS Scanner           |
| Code          | Reader™ 1421                   | 2D         | USB          | Handheld     | HID POS Scanner           |
| Code          | Reader™ 5000                   | 2D         | USB          | Presentation | HID POS Scanner           |
| Honeywell     | Genesis 7580g                  | 2D         | USB          | Presentation | HID POS Scanner           |
| Honeywell     | Granit 198Xi                   | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Granit 191Xi                   | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | N5680                          | 2D         | Internal     | Component    | HID POS Scanner           |
| Honeywell     | N3680                          | 2D         | Internal     | Component    | HID POS Scanner           |
| Honeywell     | Orbit 7190g                    | 2D         | USB          | Presentation | HID POS Scanner           |
| Honeywell     | Stratos 2700                   | 2D         | USB          | In Counter   | HID POS Scanner           |
| Honeywell     | Voyager 1200g                  | 1D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Voyager 1202g                  | 1D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Voyager 1202-bf                | 1D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Voyager 145Xg                  | 1D / 2D<sup>1</sup>   | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Voyager 1602g                  | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Xenon 1900g                    | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Xenon 1902g                    | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Xenon 1902g-bf                 | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Xenon 1900h                    | 2D         | USB          | Handheld     | HID POS Scanner           |
| Honeywell     | Xenon 1902h                    | 2D         | USB          | Handheld     | HID POS Scanner           |
| HP            | Value Barcode Scanner (HR2150) | 2D         | USB          | Handheld     | HID POS Scanner           |
| Intermec      | SG20                           | 2D         | USB          | Handheld     | HID POS Scanner           |
| Socket Mobile | CHS 7Ci                        | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | CHS 7Di                        | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | CHS 7Mi                        | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | CHS 7Pi                        | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | CHS 8Ci                        | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | DuraScan D700                  | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | DuraScan D730                  | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | DuraScan D740                  | 2D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | SocketScan S700                | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | SocketScan S730                | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | SocketScan S740                | 2D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | SocketScan S800                | 1D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Socket Mobile | SocketScan S850                | 2D         | Bluetooth    | Handheld     | Serial Port Profile (SPP) |
| Zebra         | DS2208<sup>2</sup>                        | 2D         | USB          | Handheld     | HID POS Scanner           |
| Zebra         | DS2278                         | 2D         | USB          | Handheld     | HID POS Scanner           |
| Zebra         | DS8108<sup>3</sup>                        | 2D         | USB          | Handheld     | HID POS Scanner           |
| Zebra         | DS8178<sup>4</sup>                         | 2D         | USB          | Handheld     | HID POS Scanner           | 


<sup>1</sup> Upgradable to support 2D barcodes through Honeywell <br/>
<sup>2</sup> Minimum firmware 009 (2018.07.09) required. Upgradable using Zebra [123Scan](http://www.zebra.com/123scan).<br/>
<sup>3</sup> Minimum firmware 016 (2018.01.18) required. Upgradable using Zebra [123Scan](http://www.zebra.com/123scan).<br/> 
<sup>4</sup> Minimum firmware 023 (2019.03.11) required. Upgradable using Zebra [123Scan](http://www.zebra.com/123scan).<br/>

<hr>

### Windows devices with built-in barcode scanner
| Manufacturer   | Model | Operating System |
|----------------|-------|------------------|
| Innowi         | ChecOut-M | Windows 10   |

### Windows Mobile devices with built-in barcode scanner
| Manufacturer   | Model | Operating System |
|----------------|-------|------------------|
| Bluebird       | EF400 | Windows Mobile   |
| Bluebird       | EF500 | Windows Mobile   |
| Bluebird       | EF500R | Windows Mobile   |
| Honeywell      | CT50   | Windows Mobile   |
| Honeywell      | D75e | Windows Mobile   |
| Janam          | XT2      | Windows Mobile   |
| Panasonic      | FZ-E1 | Windows Mobile   |
| Panasonic      | FZ-F1 |Windows Mobile   |
| PointMobile    | PM80 | Windows Mobile   |
| Zebra          | TC700j | Windows Mobile   |
| HP             | Elite X3 Jacket | Windows Mobile   |




## Cash Drawer
| Connectivity | Support |
| -------------|-------------|
| Network/Bluetooth | <p> Connection directly to the cash drawer can be made over the network or through Bluetooth, depending on the capabilities of the cash drawer unit. </p><p>APG Cash Drawer:  NetPRO, BluePRO</p> |
| DK port | <p> Cash drawers that do not have network or Bluetooth capabilities can be connected via the DK port on a supported Receipt Printer or the Star Micronics DK-AirCash accessory. </p>
| OPOS    | <p> Supports any OPOS compatible Cash Drawers via OPOS service objects provided by the manufacturer. Install the OPOS drivers as per the device manufacturers installation instructions. </p> |


## Customer Display (LineDisplay)
Supports any OPOS compatible line displays via OPOS service objects provided by the manufacturer. Install the OPOS drivers as per the device manufacturers installation instructions.

## Magnetic Stripe Reader
Windows provides support for the following magnetic stripe readers from Magtek and IDTech based on their Vendor ID and Product ID (VID/PID).

| Manufacturer | 	Model(s) |	Part Number |
|--------------|-----------|--------------|
| IDTech | SecureMag (VID:0ACD PID:2010) | IDRE-3x5xxxx |
| |	MiniMag (VID:0ACD PID:0500) |	IDMB-3x5xxxx |
| Magtek | MagneSafe (VID:0801 PID:0011) |	210730xx |
| |	Dynamag (VID:0801 PID:0002) |	210401xx |

 Windows supports implementation of additional vendor specific drivers to support additional magnetic stripe readers. Please check with your magnetic stripe reader manufacturer for availability. Magnetic stripe reader manufacturers please consult the [Magnetic Stripe Reader Driver Design Guide](/windows-hardware/drivers/ddi/_pos/index) for information on creating a custom magnetic stripe reader driver.

## Receipt Printer (POSPrinter)
| Connectivity | Support |
| -------------|-------------|
| Network and Bluetooth | <p>Windows supports network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language.  The printers listed below are discovered automatically using POSPrinter APIs. Additional receipt printers which provide an ESC/POS emulation may also work but would need to be associated using an [out of band pairing](./point-of-service.md#out-of-band-pairing) process.</p><p>Note: slip station and journal stations are not supported through this method.</p> |
| OPOS    | <p> Supports any OPOS compatible receipt printers via OPOS service objects. Install the OPOS drivers as per the device manufacturers installation instructions. </p> |

### Stationary Receipt Printers (Network/Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	TM-T88V, TM-T70, TM-T20, TM-U220 |

### Mobile Receipt Printers (Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	Mobilink P20 (TM-P20), Mobilink P60 (TM-P60), Mobilink P80 (TM-P80) |