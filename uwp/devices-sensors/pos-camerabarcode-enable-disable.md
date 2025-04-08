---
title: Enable or disable the software decoder for the Camera Barcode Scanner
description: Learn how to set a system registry key in Windows 10 to enable or disable the software decoder for the Camera Barcode Scanner.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Enable or disable the software decoder for the camera barcode scanner

This topic explains how to enable or disable the software decoder for the camera barcode scanner.

The software decoder can be disabled if you do not want to use camera barcode scanner or if you have acquired a 3rd party decoder that works with the [**BarcodeScanner**](/uwp/api/windows.devices.pointofservice.barcodescanner) APIs.

**In Windows 10 Version 1803 or later, the software decoder is installed and enabled by default.**  

> [!NOTE]
> The software decoder built into Windows 10/11 is provided by [*Digimarc Corporation*](https://www.digimarc.com/).

## Enable or disable using the system registry

The software decoder that ships with Windows can be enabled or disabled through the system registry. Just add the registry key *InboxDecoder* under *HKLM\Software\Microsoft\PointOfService\BarcodeScanner* and set the *Enable* value as described below.

| Value name  | Value Type | Value |
| ----------- | --------- | -------|
| Enable      | DWORD     | 1 (default) - Enables the software decoder that ships with Windows <br/>0 - Disables the software decoder that ships with Windows |

Here is an example Registration Entries (.reg) file that disables the software decoder that ships with Windows:

```text
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PointOfService\BarcodeScanner\InboxDecoder]
"Enable"=dword:0000000
```  

Here is an example Registration Entries (.reg) file that enables the software decoder that ships with Windows:

```text
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PointOfService\BarcodeScanner\InboxDecoder]
"Enable"=dword:0000001
```  

> [!CAUTION]
> Serious problems might occur if you modify the registry incorrectly.  For added protection, back up the registry before you modify it.  Then, you can restore the registry if a problem occurs.  For more information about how to back up and restore the registry, click the following article number to view the article in the Microsoft Knowledge Base: <br/><br/> [322756](https://support.microsoft.com/help/322756/how-to-back-up-and-restore-the-registry-in-windows) How to back up and restore the registry in Windows.

## See also

- [JustScanIt - Windows Store app](https://aka.ms/justscanit)
- [BarcodeScanner sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BarcodeScanner)
