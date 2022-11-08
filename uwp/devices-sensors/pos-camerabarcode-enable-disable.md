---
title: Camera Barcode Scanner Configuration
description: Learn how to set a system registry key in Windows 10 to enable or disable the software decoder for the Camera Barcode Scanner.
ms.date: 04/08/2019
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Enable or disable the software decoder that ships with Windows

In Windows 10, version 1803, the software decoder is installed and enabled by default.  You can disable the software decoder that ships with Windows if you do not want to use Camera Barcode Scanner or if you have acquired a 3rd party decoder that works with Windows.Devices.PointOfService.BarcodeScanner APIs and do not want to use both.

## Enable or disable using the system registry

The software decoder that ships with Windows can be enabled or disabled via the system registry by adding the registry key *InboxDecoder* under *HKLM\Software\Microsoft\PointOfService\BarcodeScanner* and setting the *Enable* value as described below.

| Value name  | Value Type | Value | Status |
| ----------- | --------- | -------|--------|
| Enable      | DWORD     | 1 (default)<br/>0 |  Enables the software decoder that ships with Windows <br/> Disables the software decoder that ships with Windows |

Here is an example registry file that you can use to **disable** the software decoder that ships with Windows:

```text
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PointOfService\BarcodeScanner\InboxDecoder]
"Enable"=dword:0000000
```  

Use this example registry file to **enable** the software decoder that ships with Windows:

```text
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PointOfService\BarcodeScanner\InboxDecoder]
"Enable"=dword:0000001
```  

> [!Warning]
> Serious problems might occur if you modify the registry incorrectly.  For added protection, back up the registry before you modify it.  Then, you can restore the registry if a problem occurs.  For more information about how to back up and restore the registry, click the following article number to view the article in the Microsoft Knowledge Base: <br/><br/> [322756](https://support.microsoft.com/help/322756/how-to-back-up-and-restore-the-registry-in-windows) How to back up and restore the registry in Windows.

> [!NOTE]
> The software decoder built into Windows 10 is provided courtesy of  [**Digimarc Corporation**](https://www.digimarc.com/).

## See also

### Samples

- [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
