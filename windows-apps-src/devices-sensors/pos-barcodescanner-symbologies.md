---
author: TerryWarwick
title: Working with barcode scanner symbologies
description: This article contains information about barcode scanner symbologies.
ms.author: jken
ms.date: 05/3/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Working with symbologies
A [barcode symbology](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologies) is the mapping of data to a specific barcode format. Some common symbologies include UPC, Code 128, QR Code, etc.  The UWP barcode scanner APIs allow an application to control how the scanner processes these symbologies without manually configuring the scanner. 

## Determine which symbologies are supported 
Since your application may be used with different barcode scanner models from multiple manufacturers, you may want to query the scanner to determine the list of symbologies that it supports.  This can be useful if your application requires a specific symbology that may not be supported by all scanners or you need to enable symbologies that have been either manually or programmatically disabled on the scanner.
Once you have a [BarcodeScanner](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner) object by using [BarcodeScanner.FromIdAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.fromidasync), call [GetSupportedSymbologiesAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.getsupportedsymbologiesasync#Windows_Devices_PointOfService_BarcodeScanner_GetSupportedSymbologiesAsync) to obtain a list of symbologies supported by the device.

## Determine if a specific symbology is supported
To determine if the scanner supports a specific symbology you can call [IsSymbologySupportedAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.issymbologysupportedasync#Windows_Devices_PointOfService_BarcodeScanner_IsSymbologySupportedAsync_System_UInt32_)

## Changing which symbologies are recognized
In some cases, you may want to use a subset of symbologies that the barcode scanner supports.  This is particularly useful to block symbologies that you do not intend to use in your application. For example, to ensure a user scans the right barcode, you could constrain scanning to UPC or EAN when acquiring item SKUs and constrain scanning to Code 128 when acquiring serial numbers.
Once you know the symbologies that your scanner supports, you can set the symbologies that you want it to recognize.  This can be done after you have established a 
[ClaimedBarcodeScanner](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner) object using [ClaimScannerAsyc](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync#Windows_Devices_PointOfService_BarcodeScanner_ClaimScannerAsync). You can call [SetActiveSymbologiesAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setactivesymbologiesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_SetActiveSymbologiesAsync_Windows_Foundation_Collections_IIterable_System_UInt32__) to enable a specific set of symbologies while those omitted from your list are disabled.

## Restricting scan data by data length
Some symbologies are variable length such as Code 39 or Code 128.  Barcodes of this symbology can be located near each containing different data often of specific length. Setting the specific length of the data you require can prevent invalid scans.

| Method    | Description |
| :-------- | :---------- |
| [SetSymbologyAttributesAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_SetSymbologyAttributesAsync_System_UInt32_Windows_Devices_PointOfService_BarcodeSymbologyAttributes_) | Allows you to configure a desired length range of the decoded data and how the scanner handles the check digit. |
| [GetSymbologyAttributesAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.getsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_GetSymbologyAttributesAsync_System_UInt32_) | Allows you to retrieve the current length and check digit configurations. |

> [!Important] 
> Confirm that your barcode scanner supports the use of symbology attributes by first checking the following properties: 
> - [IsDecodeLengthSupported](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.isdecodelengthsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsDecodeLengthSupported)
> - [ICheckDigitTransmissionSupported](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigittransmissionsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsCheckDigitTransmissionSupported)
> - [IsCheckDigitValidationSupported](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigitvalidationsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsCheckDigitValidationSupported)
