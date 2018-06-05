---
author: TerryWarwick
title: Getting started with Point of Service
description: This article contains information about getting started with the PointOfService UWP APIs.
ms.author: jken
ms.date: 05/1/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Getting started with Point of Service

This section contains topics that are common across all Point of Service device categories.

|Topic |Description |
|------|------------|
| [Capability declaration](pos-basics-capability.md)      | Learn how to add the **pointOfService** capability to your application manifest.  This capability is required for use of Windows.Devices.PointOfService namespace.  |
| [Enumerating devices](pos-basics-enumerating.md)        | Learn how to define a device selector that is used to query devices available to the system and use this selector to enumerate Point of Service devices.  |
| [Creating a device object](pos-basics-deviceobject.md)  | Learn how to create a PointOfService device object that will give you access to read-only properties of the peripheral and claim the peripheral for exclusive use. |
|

## See also
[Getting started with Windows.Devices.PointOfService](pos-get-started.md)


## Sample code
+ [Barcode scanner sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
+ [Cash drawer sample]( https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CashDrawer)
+ [Line display sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LineDisplay)
+ [Magnetic stripe reader sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MagneticStripeReader)
+ [POSPrinter sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/PosPrinter)

