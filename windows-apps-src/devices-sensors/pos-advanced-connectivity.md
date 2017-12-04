---
author: twarwick
title: PointOfService Advanced Connectivity
description: This article contains information about advanced connectivity for PointOfService
ms.author: twarwick
ms.date: 09/05/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# PointOfService Advanced Connectivity

## Out-of-band pairing

Out-of-band pairing allows apps to connect to a Point-of-Service peripheral without requiring discovery. Apps must use the [**Windows.Devices.PointOfService**](https://msdn.microsoft.com/library/windows/apps/windows.devices.pointofservice.aspx) namespace and pass in a specifically formatted string (out-of-band blob) to the appropriate **FromIdAsync** method for the desired peripheral. When **FromIdAsync** is executed, the host device pairs and connects to the peripheral before the operation returns to the caller.

### Out-of-band blob format

```json
    "connectionKind":"Network",
    "physicalAddress":"AA:BB:CC:DD:EE:FF",
    "connectionString":"192.168.1.1:9001",
    "peripheralKinds":"{C7BC9B22-21F0-4F0D-9BB6-66C229B8CD33}",
    "providerId":"{02FFF12E-7291-4A5D-ADFA-DA8FB7769CD2}",
    "providerName":"PrinterProtocolProvider.dll"
```

**connectionKind** - The type of connection. Valid values are "Network" and "Bluetooth".

**physicalAddress** - The MAC address of the peripheral. For example, in case of a network printer, this would be the MAC address that is provided by the printer test sheet in AA:BB:CC:DD:EE:FF format.

**connectionString** - The connection string of the peripheral. For example, in the case of a network printer, this would be the IP address provided by the printer test sheet in 192.168.1.1:9001 format. This field is omitted for all Bluetooth peripherals.

**peripheralKinds** - The GUID for the device type. Valid values are:

| Device type | GUID |
| ---- | ---- |
| *POSPrinter* | C7BC9B22-21F0-4F0D-9BB6-66C229B8CD33 |
| *Barcode Scanner* | C243FFBD-3AFC-45E9-B3D3-2BA18BC7EBC5 |
| *Cash Drawer* | 772E18F2-8925-4229-A5AC-6453CB482FDA |


**providerId** - The GUID for the protocol provider class. Valid values are:

| Protocol provider class | GUID |
| ---- | ---- |
| *POSPrinter: Network - Epson* | 9F0F8BE3-4E59-4520-BFBA-AF77614A31CE |
| *POSPrinter: Network - Generic (ESC/POS only)* | 02FFF12E-7291-4A5D-ADFA-DA8FB7769CD2 |
| *POSPrinter: Network - Star Micronics* | 1E3A32C2-F411-4B8C-AC91-CC2C5FD21996 |
| *POSPrinter: Bluetooth - Epson* | 94917594-544F-4AF8-B53B-EC6D9F8A4464 |
| *POSPrinter: Bluetooth - Generic (ESC/POS only)* | CCD5B810-95B9-4320-BA7E-78C223CAF418 |
| *Cash Drawer: Network - APG* | E619E2FE-9489-4C74-BF57-70AED670B9B0 |
| *Cash Drawer: Bluetooth - APG* | 332E6550-2E01-42EB-9401-C6A112D80185 |
| *Barcode Scanner: Bluetooth (SPP-SSI)* | 6E7C8178-A006-405E-85C3-084244885AD2 |

**providerName** - The name of the provider DLL. The default providers are:

| Provider | DLL name |
| ---- | ---- |
| POSPrinter | PrinterProtocolProvider.dll |
| Cash Drawer | CashDrawerProtocolProvider.dll |
| Barcode Scanner | BarcodeScannerProtocolProvider.dll |

**Usage example: Network Receipt Printer**

```csharp
String oobBlobNetworkPrinter =
  "{\"connectionKind\":\"Network\"," +
  "\"physicalAddress\":\"AA:BB:CC:DD:EE:FF\"," +
  "\"connectionString\":\"192.168.1.1:9001\"," +
  "\"peripheralKinds\":\"{C7BC9B22-21F0-4F0D-9BB6-66C229B8CD33}\"," +
  "\"providerId\":\"{02FFF12E-7291-4A5D-ADFA-DA8FB7769CD2}\"," +
  "\"providerName\":\"PrinterProtocolProvider.dll\"}";

printer = await PosPrinter.FromIdAsync(oobBlobNetworkPrinter);
```

**Usage example: Bluetooth Receipt Printer**

```csharp
string oobBlobBTPrinter =
    "{\"connectionKind\":\"Bluetooth\"," +
    "\"physicalAddress\":\"AA:BB:CC:DD:EE:FF\"," +
    "\"peripheralKinds\":\"{C7BC9B22-21F0-4F0D-9BB6-66C229B8CD33}\"," +
    "\"providerId\":\"{CCD5B810-95B9-4320-BA7E-78C223CAF418}\"," +
    "\"providerName\":\"PrinterProtocolProvider.dll\"}";

printer = await PosPrinter.FromIdAsync(oobBlobBTPrinter);

```
