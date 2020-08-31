---
title: Use a software trigger
description: Learn how to control the barcode scanning process programmatically using an asynchronous software trigger.
ms.date: 08/29/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Use a software trigger

It can be useful to control the act of scanning from software if you are using a barcode scanner in presentation mode or if the scanner does not have a physical trigger such as a camera-based barcode scanner. You can initiate the scan process by calling [StartSoftwareTriggerAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.startsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StartSoftwareTriggerAsync).

Depending on the value of [IsDisabledOnDataReceived](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) the scanner may scan only one barcode then stop or scan continuously until you call 
[StopSoftwareTriggerAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.stopsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StopSoftwareTriggerAsync).

Set the desired value of [IsDisabledOnDataReceived](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) to control the scanner behavior when a barcode is decoded.

| Value | Description |
| ----- | ----------- |
| True   | Scan only one barcode then stop |
| False  | Continuously scan barcodes without stopping |


> [!Important]
> Confirm that your barcode scanner supports the use of software trigger by first checking the property [IsSoftwareTriggerSupported](/uwp/api/windows.devices.pointofservice.barcodescannercapabilities.issoftwaretriggersupported#Windows_Devices_PointOfService_BarcodeScannerCapabilities_IsSoftwareTriggerSupported).

The following example shows how to initiate scanning using a software trigger, which will stop scanning once it scans one barcode:

```cs
private void SoftwareTrigger(BarcodeScanner barcodeScanner, ClaimedBarcodeScanner claimedBarcodeScanner) 
{
    if (barcodeScanner.Capabilities.IsSoftwareTriggerSupported)
    {
        claimedBarcodeScanner.IsDisabledOnDataReceived = true;
        await claimedBarcodeScanner.StartSoftwareTriggerAsync();
    }
}
```

[!INCLUDE [feedback](./includes/pos-feedback.md)]