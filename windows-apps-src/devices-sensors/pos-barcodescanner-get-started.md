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


[!INCLUDE [feedback](./includes/pos-feedback.md)]