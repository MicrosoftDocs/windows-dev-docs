---
title: Camera barcode scanner
description: Explains what a camera barcode scanner is and provides links to various topics that describe its features.
ms.date: 05/27/2026
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Camera barcode scanner

A camera barcode scanner consists of a standard lens camera with auto-focus capabilities (attached to a computer) combined with a software decoder, which Windows dynamically pairs to create a fully functional [barcode scanner](barcodescanner.md) for WinRT apps.

> [!NOTE]
> The software decoder built into Windows 10/11 is provided by [*Digimarc Corporation*](https://www.digimarc.com/). Requires Windows 10 Version 1803 or later.

## In this section

| Title | Description |
|------|------------|
| [System requirements](camerabarcode-system-requirements.md)  | List of Windows editions that support camera barcode scanner, including the camera requirements to successfully read barcodes. |
| [Get started](camerabarcode-get-started.md)              | Learn the basics for using camera barcode scanner. |
| [Host a camera barcode scanner preview](camerabarcode-hosting-preview.md)          | Learn how to host a camera barcode scanner preview in your application. |
| [Enable or disable the software decoder](camerabarcode-enable-disable.md)         | Learn how to enable or disable the default software decoder. |
| [Camera barcode scanner symbologies](camerabarcode-symbologies.md) | Sample barcodes for each of the symbologies supported by the software barcode decoder that ships with Windows 10 Version 1803 or later (such as UPC/EAN, Code 39, Code 128, Interleaved 2 of 5, Databar Omnidirectional, Databar Stacked, QR Code, and GS1DWCode). |
| [Advanced configuration](camerabarcode-advancedconfig.md)    | Learn about advanced configuration options for camera barcode decoder. |

## See also

- [BarcodeScanner sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BarcodeScanner)
