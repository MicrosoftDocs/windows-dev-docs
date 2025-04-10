---
title: Camera barcode scanner system requirements
description: Lists the requirements for using camera barcode scanner from a UWP app.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Camera barcode scanner system requirements

[Camera barcode scanner](pos-camerabarcode.md) consists of a standard lens camera with auto-focus capabilities (attached to a computer) combined with a software decoder and is supported by Universal Windows Platform (UWP) apps on the following versions of Windows.

| OS         | Version
|------------|-----------------------------|
| Windows 11 | All                         |
| Windows 10 | 1803 (build 17134) or later |

> [!NOTE]
> The software decoder built into Windows 10/11 is provided by [*Digimarc Corporation*](https://www.digimarc.com/).

## Webcam Requirements

| Category      | Recommendation           | Comments |
| ------------- | ------------------------ | -------- |
| Focus         | Auto Focus               | Fixed focus is not recommended as it will have difficulty getting a clear view of the barcode due to dithering between the black and white elements of the barcode. |
| Resolution    | 1920 x 1440 or higher    | We recommend cameras that are capable of 1920 x 1440 resolution or higher.  Some lower resolution cameras can read standard barcodes if the barcode is printed large enough. Barcodes with thinner elements may require higher resolution cameras. |

## See also

- [JustScanIt - Windows Store app](https://aka.ms/justscanit)
- [BarcodeScanner sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BarcodeScanner)
