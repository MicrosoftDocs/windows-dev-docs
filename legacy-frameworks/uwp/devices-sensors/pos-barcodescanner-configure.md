---
title: Configure a barcode scanner
description: This topic describes how barcode scanners can be configured in Universal Windows Platform (UWP) apps.
ms.date: 05/04/2023
ms.topic: article
---

# Configure a barcode scanner

This topic describes how barcode scanners can be configured in Universal Windows Platform (UWP) apps.

## Keyboard wedge mode

Many barcode scanners support **keyboard wedge** mode, which makes the barcode scanner appear as a keyboard to Windows. This enables scanning barcodes into applications that are not barcode scanner-aware such as Notepad. When you scan a barcode in this mode, the decoded data from the barcode scanner gets inserted at your insertion point as if you typed the data using your keyboard. If you want more control over your barcode scanner from your UWP application, you will need to configure it in a non-keyboard wedge mode.

## USB barcode scanner

A USB connected barcode scanner must be configured in **HID POS Scanner** mode to work with the barcode scanner driver that is included in Windows. This driver is an implementation of the **HID Point of Sale Usage Tables** specification published to [USB-HID](https://www.usb.org/hid). Please refer to your barcode scanner documentation or contact your barcode scanner manufacturer for instructions to enable the **HID POS Scanner** mode. Once configured as a **HID POS Scanner** your barcode scanner will appear in Device Manager under the **POS Barcode Scanner** node as **POS HID Barcode scanner**.

Your barcode scanner manufacturer may also have a vendor specific driver that supports the UWP Barcode Scanner APIs using a mode other than **HID POS Scanner**. If you have already installed a manufacturer-provided driver compatible with UWP Barcode Scanner APIs, you may see a vendor-specific device listed under **POS Barcode Scanner** in Device Manager.

## Bluetooth barcode scanner

A Bluetooth connected scanner must be configured in **Serial Port Protocol - Simple Serial Interface (SPP-SSI)** mode to work with the UWP Barcode Scanner APIs. Please refer to your barcode scanner documentation or contact your barcode scanner manufacturer for instructions to enable the **SPP-SSI mode**.

Before you can use your Bluetooth barcode scanner you must pair it using **Settings > Devices > Bluetooth & other devices > Add Bluetooth or other device**.

You can initiate and control the pairing process using the [Windows.Devices.Enumeration](/uwp/api/windows.devices.enumeration) namespace. See [Pair Devices](./pair-devices.md) for more information.

[!INCLUDE [feedback](./includes/pos-feedback.md)]
