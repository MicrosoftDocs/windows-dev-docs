---
author: TerryWarwick
title: Getting Started with Barcode Scanner
description: Learn how to interact with a barcode scanner from a Universal Windows Application
ms.author: jken
ms.date: 05/1/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Getting started with barcode scanner[s]

Learn how to interact with a barcode scanner from a Universal Windows Application.  This topic provides information about barcode scanner specific functionality.

## Configuring your barcode scanner
Barcode scanners can be configured in several different modes.  It is important for your barcode scanner to be configured properly for the intended application.  Many barcode scanners can be configured in **keyboard wedge** mode which makes the barcode scanner appear as a keyboard to Windows.  This allows you to scan barcodes into applications that are not barcode scanner aware such as Notepad.  When you scan a barcode in this mode, the decoded data from the barcode scanner gets inserted at your insertion point as if you typed the data using your keyboard.  If you want more control over your barcode scanner from your UWP application, you will need to configure it in a non-keyboard wedge mode.

### USB barcode scanner
A USB connected barcode scanner must be configured in **HID POS Scanner** mode to work with the barcode scanner driver that is included in Windows. This driver is an implementation of **HID Point of Sale Usage Tables** specification published to [**USB-HID**](http://www.usb.org/developers/hidpage/).  Please refer to your barcode scanner documentation or contact your barcode scanner manufacturer for instructions to enable the **HID POS Scanner** mode.  Once configured as a **HID POS Scanner** your barcode scanner will appear in Device Manager under the **POS Barcode Scanner** node as **POS HID Barcode scanner**.
Your barcode scanner manufacturer may also have a vendor specific driver that supports the UWP Barcode Scanner APIs using a mode other than **HID POS Scanner**.  If you have already installed a manufacturer provided driver compatible with UWP Barcode Scanner APIs, you may see a vendor specific device listed under **POS Barcode Scanner** in Device Manager.

### Bluetooth barcode scanner
A Bluetooth connected scanner must be configured in **Serial Port Protocol - Simple Serial Interface (SPP-SSI)** mode to work with the UWP Barcode Scanner APIs.  Please refer to your barcode scanner documentation or contact your barcode scanner manufacturer for instructions to enable the **SPP-SSI mode**.  
Before you can use your Bluetooth barcode scanner you must pair it using Settings - Devices - Bluetooth & other devices - Add Bluetooth or other device.  
You can initiate and control the pairing ceremony using **Windows.Devices.Enumeration** namespace.  See [**Pair Devices**](https://docs.microsoft.com/windows/uwp/devices-sensors/pair-devices) for more information.

## Working with symbologies
A [**barcode symbology**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologies) is the mapping of data to a specific barcode format. Some common symbologies include UPC, Code 128, QR Code, etc.  The UWP barcode scanner APIs allow an application to control how the scanner processes these symbologies without manually configuring the scanner. 

### Determine which symbologies are supported 
Since your application may be used with different barcode scanner models from multiple manufacturers, you may want to query the scanner to determine the list of symbologies that it supports.  This can be useful if your application requires a specific symbology that may not be supported by all scanners or you need to enable symbologies that have been either manually or programmatically disabled on the scanner.
Once you have a [**BarcodeScanner**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner) object by using [**BarcodeScanner.FromIdAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.fromidasync), call [**GetSupportedSymbologiesAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.getsupportedsymbologiesasync#Windows_Devices_PointOfService_BarcodeScanner_GetSupportedSymbologiesAsync) to obtain a list of symbologies supported by the device.

### Determine if a specific symbology is supported
To determine if the scanner supports a specific symbology you can call [**IsSymbologySupportedAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.issymbologysupportedasync#Windows_Devices_PointOfService_BarcodeScanner_IsSymbologySupportedAsync_System_UInt32_)

### Changing which symbologies are recognized
In some cases, you may want to use a subset of symbologies that the barcode scanner supports.  This is particularly useful to block symbologies that you do not intend to use in your application. For example, to ensure a user scans the right barcode, you could constrain scanning to UPC or EAN when acquiring item SKUs and constrain scanning to Code 128 when acquiring serial numbers.
Once you know the symbologies that your scanner supports, you can set the symbologies that you want it to recognize.  This can be done after you have established a 
[**ClaimedBarcodeScanner**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner) object using [**ClaimScannerAsyc**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync#Windows_Devices_PointOfService_BarcodeScanner_ClaimScannerAsync). You can call [**SetActiveSymbologiesAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setactivesymbologiesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_SetActiveSymbologiesAsync_Windows_Foundation_Collections_IIterable_System_UInt32__) to enable a specific set of symbologies while those omitted from your list are disabled.

### Restricting scan data by data length
Some symbologies are variable length such as Code 39 or Code 128.  Barcodes of this symbology can be located near each containing different data often of specific length. Setting the specific length of the data you require can prevent invalid scans.

| Method    | Description |
| :-------- | :---------- |
| [**SetSymbologyAttributesAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_SetSymbologyAttributesAsync_System_UInt32_Windows_Devices_PointOfService_BarcodeSymbologyAttributes_) | Allows you to configure a desired length range of the decoded data and how the scanner handles the check digit. |
| [**GetSymbologyAttributesAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.getsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_GetSymbologyAttributesAsync_System_UInt32_) | Allows you to retrieve the current length and check digit configurations. |

> [!Important] 
> Confirm that your barcode scanner supports the use of symbology attributes by first checking the following properties: [**SetSymbologyAttributesAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_SetSymbologyAttributesAsync_System_UInt32_Windows_Devices_PointOfService_BarcodeSymbologyAttributes_) or [**GetSymbologyAttributesAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.getsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_GetSymbologyAttributesAsync_System_UInt32_) | Allows you to retrieve the current length and check digit configurations. :
> - [**IsDecodeLengthSupported**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.isdecodelengthsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsDecodeLengthSupported)
> - [**ICheckDigitTransmissionSupported**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigittransmissionsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsCheckDigitTransmissionSupported)
> - [**IsCheckDigitValidationSupported**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigitvalidationsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsCheckDigitValidationSupported)

## Using software trigger with barcode scanners
### Initiate scan from software
It can be useful to control the act of scanning from software if you are using a barcode scanner in presentation mode or if the scanner does not have a physical trigger such as a camera-based barcode scanner. You can initiate the scan process by calling [**StartSoftwareTriggerAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.startsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StartSoftwareTriggerAsync).  
Depending on the value of [**IsDisabledOnDataReceived**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) the scanner may scan only one barcode then stop or scan continuously until you call 
[**StopSoftwareTriggerAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.stopsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StopSoftwareTriggerAsync).

Set the desired value of [**IsDisabledOnDataReceived**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) to control the scanner behavior when a barcode is decoded.

| Value | Description |
| ----- | ----------- |
| True   | Scan only one barcode then stop |
| False  | Continuously scan barcodes without stopping |


> [!Important]
> Confirm that your barcode scanner supports the use of software trigger by first checking the property [**IsSoftwareTriggerSupported**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescannercapabilities.issoftwaretriggersupported#Windows_Devices_PointOfService_BarcodeScannerCapabilities_IsSoftwareTriggerSupported).
