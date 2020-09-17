---
title: Camera Barcode Scanner Symbologies
description: View sample barcodes for each of the symbologies supported by the software barcode decoder that ships with Windows 10.
ms.date: 05/02/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Symbologies

This topic provides sample barcodes for each of the symbologies supported by the software barcode decoder that ships with Windows 10, including: UPC/EAN, Code 39, Code 128, Interleaved 2 of 5, Databar Omnidirectional, Databar Stacked, QR Code and GS1DWCode.

Windows 10 uses a standard lens camera combined with a software decoder to generate a barcode scanner. This article refers to the symbologies supported by the software decoder. Additional symbologies might be supported by dedicated barcode scanner devices that have built-in hardware decoders, please contact your barcode scanner manufacturer for details. The symbologies listed are supported in all editions of Windows 10 build 17134 or later, unless otherwise specified.

Use [GetSupportedSymbologiesAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.getsupportedsymbologiesasync) to determine the specific symbologies supported by a barcode scanner.

> [!NOTE]
> The software decoder built into Windows 10 is provided by [*Digimarc Corporation*](https://www.digimarc.com/).

## 1D Symbologies

### Code 39
![Sample Barcode - Code 39](images/pos/sample-barcode-code39.png)

### Code 128
![Sample Barcode - Code 128](images/pos/sample-barcode-code128.png)

### Databar Omnidirectional
![Sample Barcode - Databar Omnidirectional](images/pos/sample-barcode-databar-omnidirectional.png) 
### Databar Stacked
![Sample Barcode - Databar Stacked](images/pos/sample-barcode-databar-stacked.png)

### EAN-8
![Sample Barcode - EAN-8](images/pos/sample-barcode-ean8.png)

### EAN-13
![Sample Barcode - EAN-13](images/pos/sample-barcode-ean13.png)

### Interleaved 2 of 5
![Sample Barcode - Interleaved 2 of 5](images/pos/sample-barcode-interleaved-2-of-5.png)

### UPC-A
![Sample Barcode - UPC A](images/pos/sample-barcode-upca.png)

### UPC-E
![Sample Barcode - UPC E](images/pos/sample-barcode-upce.png)

## 2D Symbologies
### QR Code
![Sample Barcode - QR Code](images/pos/sample-barcode-qrcode.png)

## Digital Watermark
### GS1-DWCode

Scan the image of a package below with your camera barcode scanner application to see GS1DWCode in action.  The image is encoded with UPCA 856107006854.  Please visit http://www.digimarc.com for more information about GS1DWCode capabilities.

![Sample Barcode - GS1DWCode](images/pos/Rice-Box-V7.jpg)

## See also

### Samples

- [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
