---
author: mukin
title: POS Printer
description: This article contains information about the point of service printer family of devices
ms.author: wdg-dev-content
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# POS Printer

Enables application developers to print to network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language.

This topic covers the following:
+	Members
+	Remarks
+	Requirements

## Members
The cash drawer device type has these types of members:
+	Classes
+	Enumerations
+	Interfaces

### Classes

| Class |	Description |
|-------|-------------|
| [ClaimedJournalPrinter](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedjournalprinter) | Represents a journal printer station that has been claimed for use. |
| [ClaimedPosPrinter](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedposprinter) | Represent a point-of-service printer that has been claimed for use. |
| [ClaimedReceiptPrinter](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedreceiptprinter) | Represents a receipt printer station that has been claimed for use. |
| [ClaimedSlipPrinter](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedslipprinter) | Represents a slip printer station that has been claimed for use. |
| [JournalPrinterCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.journalprintercapabilities) | Represents the capabilities of journal station of a point-of-service printer. |
| [JournalPrintJob](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.journalprintjob) | Represents a set of printing instructions that you want to run on the journal printer station. |
| [PosPrinter](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinter) | Represents a point-of-service printer. |
| [PosPrinterCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintercapabilities) | Represents the capabilities of the point-of-service printer. |
| [PosPrinterCharacterSetIds](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintercharactersetids) | Represents the set of identifiers for the character sets that a point-of-service printer can use. |
| [PosPrinterReleaseDeviceRequestedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterreleasedevicerequestedeventargs) | Provides information about the [ClaimedPosPrinter.ReleaseDeviceRequested](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedposprinter#Windows_Devices_PointOfService_ClaimedPosPrinter_ReleaseDeviceRequested) event that occurs when a point-of-service printer gets a request to release its exclusive claim. |
| [PosPrinterStatus](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterstatus) | Provides information about the status of a point-of-service printer, such as the power state of the printer. |
| [PosPrinterStatusUpdatedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterstatusupdatedeventargs) | Provides information about the [PosPrinter.StatusUpdated](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinter#Windows_Devices_PointOfService_PosPrinter_StatusUpdated) event that occurs when the status of a point-of-service printer changes. |
| [ReceiptPrinterCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.receiptprintercapabilities) | Represents the capabilities of receipt station of a point-of-service printer. |
| [ReceiptPrintJob](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.receiptprintjob) | Represents a set of printing instructions that you want to run on the receipt printer. |
| [SlipPrinterCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.slipprintercapabilities) | Represents the capabilities of slip station of a point-of-service printer. |
| [SlipPrintJob](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.slipprintjob) | Represents a set of printing instructions that you want to run on the slip printer station. |

### Enumerations

| Enumeration |	Description |
|-------------|-------------|
| [PosPrinterAlignment](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinteralignment) | Describes the possible horizontal alignments of the text that a point-of-service printer prints on the page. |
| [PosPrinterBarcodeTextPosition](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterbarcodetextposition) | Describes the possible vertical positions in which a point-of-service printer prints the barcode text relative to the barcode. |
| [PosPrinterCartridgeSensors](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintercartridgesensors) | Describes the possible sensors available for a printer station of a point-of-service printer to use to report the status of the printer. |
| [PosPrinterColorCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintercolorcapabilities) | Describes the possible color cartridges that a point-of-service printer can support. |
| [PosPrinterColorCartridge](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintercolorcartridge) | Describes the color cartridges that the point-of-service printer can use for printing. |
| [PosPrinterLineDirection](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterlinedirection) | Describes the possible directions that a receipt or slip printer station can use to print a ruled line. |
| [PosPrinterLineStyle](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterlinestyle) | Describes the line styles that a receipt or slip printer station can use to print a ruled line. |
| [PosPrinterMapMode](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintermapmode) | Describes the valid units of measure for point-of-service printers. |
| [PosPrinterMarkFeedCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintermarkfeedcapabilities) | Describes the capabilities of a receipt printer station for handling mark-sensed paper. |
| [PosPrinterMarkFeedKind](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprintermarkfeedkind) | Describes the ways the receipt printer station should feed the mark-sensed paper when you call the ReceiptPrintJob.MarkFeed method. |
| [PosPrinterPrintSide](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterprintside) | Describes the sides of the sheet of paper on which the point-of-service printer prints. |
| [PosPrinterRotation](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterrotation) | Describes the possible ways that a point-of-service printer can rotate the text or image on the page. |
| [PosPrinterRuledLineCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterruledlinecapabilities) | Describes the capabilities of the point-of-service printer to draw ruled lines. |
| [PosPrinterStatusKind](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.posprinterstatuskind) | Describes the power state for a point-of-service printer. |

### Interfaces
| Interface |	Description |
|-----------|-------------|
| [ICommonClaimedPosPrinterStation](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.icommonclaimedposprinterstation) | Represents properties and actions common to all type of claimed stations for a point-of-service printer. |
| [ICommonPosPrintStationCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.icommonposprintstationcapabilities) | Represents the capabilities common to all types of stations for point-of-service printers. |
| [ICommonReceiptSlipCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.icommonreceiptslipcapabilities) | Represents the capabilities common to receipt and slip printer stations. |
| [IPosPrinterJob](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.iposprinterjob) | Represents actions common to jobs for all types of stations for a point-of-service printer. |
| [IReceiptOrSlipJob](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.ireceiptorslipjob) | Represents actions common to jobs for receipt and slip printer stations. |

## Requirements
Applications which require this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Device support
Windows supports the ability to print to network and Bluetooth connected receipt printers using the Epson ESC/POS printer control language. For more information on ESC/POS, see [Epson ESC/POS with formatting](https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/epson-esc-pos-with-formatting).

While the classes, enumerations, and interfaces exposed in the API support receipt printer, slip printer as well as journal printer, the driver interface only supports receipt printer. Attempting to use slip printer or journal printer at this time will return a status of not implemented.

Support is currently limited to the Network and Bluetooth device models listed in the tables below. USB connected printers are currently not supported. Please check back for additional support to be added in the future.

### Stationary POS printers (Network, Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	TM-T88V, TM-T70, TM-T20, TM-U220 |

### Mobile POS printers (Bluetooth)
| Manufacturer |	Model(s) |
|--------------|-----------|
| Epson |	Mobilink P20 (TM-P20), Mobilink P60 (TM-P60), Mobilink P80 (TM-P80) |

## Examples
See the POS printer sample for an example implementation.
+ [POS printer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/PosPrinter)
