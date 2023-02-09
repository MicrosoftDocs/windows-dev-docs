---
title: Camera Barcode Scanner Symbologies
description: View sample barcodes for each of the symbologies supported by the software barcode decoder that ships with Windows 10.
ms.date: 05/02/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Camera Barcode Scanner Symbologies

## Description  

A camera barcode scanner consists of a standard lens camera with auto-focus capabilities combined with a software decoder. This article refers to the symbologies supported by the software decoder. Additional symbologies might be supported by dedicated barcode scanner devices that have built-in hardware decoders, please contact your barcode scanner manufacturer for details. 

Use [GetSupportedSymbologiesAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.getsupportedsymbologiesasync) to determine the specific symbologies supported by a barcode scanner.

Below you will find:
1. A full list symbologies supported by the software barcode decoder that is built into Windows 10/11
1. The minimum build of Windows requires to support the specific symbology
1. A sample of the symbology which can be used to validate your implementation

## 1D Symbologies

| Symbology               |Min Build | Sample |
|-------------------------|----------|--------|
| Code 39                 | 17134  | ![Sample Barcode - Code 39](images/pos/sample-barcode-code39.png) |
| Code 128                | 17134  | ![Sample Barcode - Code 128](images/pos/sample-barcode-code128.png) |
| Databar Omnidirectional | 17134  | ![Sample Barcode - Databar Omnidirectional](images/pos/sample-barcode-databar-omnidirectional.png) |
| Databar Stacked         | 17134  | ![Sample Barcode - Databar Stacked](images/pos/sample-barcode-databar-stacked.png) | 
| EAN-8                   | 17134  | ![Sample Barcode - EAN-8](images/pos/sample-barcode-ean8.png) |
| EAN-13                  | 17134  | ![Sample Barcode - EAN-13](images/pos/sample-barcode-ean13.png) |
| Interleaved 2 of 5      | 17134  | ![Sample Barcode - Interleaved 2 of 5](images/pos/sample-barcode-interleaved-2-of-5.png) |
| UPC-A                   | 17134  | ![Sample Barcode - UPC A](images/pos/sample-barcode-upca.png) |
| UPC-E                   | 17134  | ![Sample Barcode - UPC E](images/pos/sample-barcode-upce.png) |

## 2D Symbologies

| Symbology               | Min Build | Sample |
|-------------------------|-----------|--------|
|  PDF417                 | 19044  | ![Sample Barcode - PDF417](images/pos/sample-barcode-pdf417.png)  |
|  QR Code                | 17134  | ![Sample Barcode - QR Code](images/pos/sample-barcode-qrcode.png) |

## Digital Watermark

| Symbology               | Min Build | Sample |
|-------------------------|-----------|--------|
| GS1-DWCode              | 17134  | Scan the image of a package below with your camera barcode scanner application to see GS1DWCode in action.  The image is encoded with UPCA 856107006854.  Please visit http://www.digimarc.com for more information about GS1DWCode capabilities. |

![Sample Barcode - GS1DWCode](images/pos/Rice-Box-V7.jpg)

## Credits

The software decoder built into Windows 10/11 is provided by [*Digimarc Corporation*](https://www.digimarc.com/).

## See also

- [Sample application](https://aka.ms/justscanit)
- [Sample code](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
