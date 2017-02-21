author: mukin
title: Barcode Scanner
description: This article contains information about the barcode scanner point of service family of devices
ms.author: mukin
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid:

# Barcode Scanner
<!-- ADD LINK -->
Enables application developers to access barcode scanners to retrieve decoded data from a variety of barcode symbologies such as UPC and QR Codes depending on support from the hardware. See the [BarcodeSymbologies](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodesymbologies) class for a full list of supported symbologies.

This topic covers the following:
+	Members
+	Requirements
+ Device support

## Members
The barcode scanner device type has these types of members:
+	Classes
+	Enumerations

### Classes
<!-- ADD LINKS -->
| Class | Description |
|-------|-------------|
| [BarcodeScanner](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescanner) | Represents the barcode scanner device.
| [BarcodeScannerCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannercapabilities) | Represents the barcode scanner capabilities. |
| [BarcodeScannerDataReceivedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannerdatareceivedeventargs) | Provides the barcode data from the DataReceived event. |
| [BarcodeScannerErrorOccurredEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannererroroccurredeventargs) | Provides data for the ErrorOccurred event. |
| [BarcodeScannerImagePreviewReceivedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannerimagepreviewreceivedeventargs) | Provides the data from the ImagePreviewReceived event. |
| [BarcodeScannerReport](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannerreport) | Contains the barcode scanner data. |
| [BarcodeScannerStatusUpdatedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannerstatusupdatedeventargs) | Provides information about an operation status change. |
| [BarcodeSymbologies](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodesymbologies) | Contains the barcode symbology. |
| [ClaimedBarcodeScanner](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedbarcodescanner) | Represents the claimed barcode scanner.

### Enumerations
| Enumeration | Description |
|-------------|-------------|
| [BarcodeScannerStatus](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.barcodescannerstatus) | Defines the constants that indicates the barcode scanner status. |

## Requirements
Applications which require this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Device support

### USB

#### HID.Scanner
Windows contains a barcode scanner class driver which is based on the HID.Scanner (8C) usage page defined by USB.org. This class driver will support any barcode scanner which implements this standard, such as:
Manufacturer	Model(s)
Honeywell	1900GSR-2, 1200g-2
Intermec	SG20

Consult the manual for your barcode scanner or contact the manufacturer to determine if it can be configured as a USB.HID.Scanner.

#### HID.Vendor specific
Windows supports implementation of vendor specific drivers to support additional barcode scanners. Please check with your barcode scanner manufacturer for availability if the device is not supported with the in-box USB.HID.Scanner.

### Bluetooth
#### Serial Port Protocol (SPP) – Simple Serial Interface (SSI)
Windows supports SPP-SSI based Bluetooth barcode scanners.

| Manufacturer |	Model(s) |
|--------------|-----------|
| Socket Mobile |	**CHS 7 Series:** <br/> 7 Ci 1D Imager Barcode Scanner <br/> 7Di 1D Durable, Imager Barcode Scanner <br/> 7Mi 1D Laser Barcode Scanner <br/> 7Pi 1D Durable, LaserBarcode Scanner <br/> **DuraScan 700 Series:** <br/> D700 1D Imager Barcode Scanner <br/> D730 1D Laser Barcode Scanner <br/> **SocketScan 800 Series** <br/> S800 1D Imager Barcode Scanner (formerly CHS 8Ci) <br/> S850 2D Imager Barcode Scanner (formerly CHS 8Qi)

## Examples
See the barcode scanner sample for an example implementation.
+	[Barcode scanner sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
