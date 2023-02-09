---
title: Camera Barcode Scanner System Requirements
description: This article lists the requirements for using camera barcode scanner from a UWP app.
ms.date: 05/02/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Camera barcode scanner system requirements

## Operating system requirements

| OS         | Version
|------------|-----------------------------|
| Windows 11 | All                         |
| Windows 10 | 1803 (build 17134) or later |
Starting with Windows 10, version 1803, you can read barcodes through a standard camera lens from a Universal Windows Application.

## Webcam Requirements
| Category      | Recommendation           | Comments |
| ------------- | ------------------------ | -------- |
| Focus         | Auto Focus               | Fixed focus is not recommended |
| Resolution    | 1920 x 1440 or higher    | We have had best experience with cameras that are capable of 1920 x 1440 resolution or higher.  Some lower resolution cameras can read standard barcodes if the barcode is printed large enough. Barcodes with thinner elements may require higher resolution cameras. |
|

## Credits

The software decoder built into Windows 10/11 is provided by [*Digimarc Corporation*](https://www.digimarc.com/).

## See also

- [Sample application](https://aka.ms/justscanit)
- [Sample code](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
