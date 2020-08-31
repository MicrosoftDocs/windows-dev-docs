---
title: Obtain and understand barcode data
description: Learn how to obtain data from a barcode scanner in a BarcodeScannerReport object and understand its format and contents.
ms.date: 08/29/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
ms.custom: RS5
---
# Obtain and understand barcode data

Once you've set up your barcode scanner, you of course need a way of understanding the data that you scan. When you scan a barcode, the [DataReceived](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.datareceived) event is raised. The [ClaimedBarcodeScanner](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner) should subscribe to this event. The **DataReceived** event passes a [BarcodeScannerDataReceivedEventArgs](/uwp/api/windows.devices.pointofservice.barcodescannerdatareceivedeventargs) object, which you can use to access the barcode data.

## Subscribe to the DataReceived event

Once you have a **ClaimedBarcodeScanner**, have it subscribe to the **DataReceived** event:

```cs
claimedBarcodeScanner.DataReceived += ClaimedBarcodeScanner_DataReceived;
```

The event handler will be passed the **ClaimedBarcodeScanner** and a **BarcodeScannerDataReceivedEventArgs** object. You can access the barcode data through this object's [Report](/uwp/api/windows.devices.pointofservice.barcodescannerdatareceivedeventargs.report#Windows_Devices_PointOfService_BarcodeScannerDataReceivedEventArgs_Report) property, which is of type [BarcodeScannerReport](/uwp/api/windows.devices.pointofservice.barcodescannerreport).

```cs
private async void ClaimedBarcodeScanner_DataReceived(ClaimedBarcodeScanner sender, BarcodeScannerDataReceivedEventArgs args)
{
    // Parse the data
}
```

## Get the data

Once you have the **BarcodeScannerReport**, you can access and parse the barcode data. **BarcodeScannerReport** has three properties:

* [ScanData](/uwp/api/windows.devices.pointofservice.barcodescannerreport.scandata): The full, raw barcode data.
* [ScanDataLabel](/uwp/api/windows.devices.pointofservice.barcodescannerreport.scandatalabel): The decoded barcode label, which does not include the header, checksum, and other miscellaneous information.
* [ScanDataType](/uwp/api/windows.devices.pointofservice.barcodescannerreport.scandatatype): The decoded barcode label type. Possible values are defined in the [BarcodeSymbologies](/uwp/api/windows.devices.pointofservice.barcodesymbologies) class.

If you want to access either **ScanDataLabel** or **ScanDataType**, you must first set [IsDecodeDataEnabled](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdecodedataenabled#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDecodeDataEnabled) to **true**.

```cs
claimedBarcodeScanner.IsDecodeDataEnabled = true;
```

### Get the scan data type

Getting the decoded barcode label type is fairly trivial&mdash;we simply call [GetName](/uwp/api/windows.devices.pointofservice.barcodesymbologies.getname) on **ScanDataType**.

```cs
private string GetSymbology(BarcodeScannerDataReceivedEventArgs args)
{
    return BarcodeSymbologies.GetName(args.Report.ScanDataType);
}
```

### Get the scan data label

To get the decoded barcode label, there are a few things you have to be aware of. Only certain data types contain encoded text, so you should first check if the symbology can be converted to a string, and then convert the buffer we get from **ScanDataLabel** to an encoded UTF-8 string.

```cs
private string GetDataLabel(BarcodeScannerDataReceivedEventArgs args)
{
    uint scanDataType = args.Report.ScanDataType;

    // Only certain data types contain encoded text.
    // To keep this simple, we'll just decode a few of them.
    if (args.Report.ScanDataLabel == null)
    {
        return "No data";
    }

    // This is not an exhaustive list of symbologies that can be converted to a string.
    else if (scanDataType == BarcodeSymbologies.Upca ||
        scanDataType == BarcodeSymbologies.UpcaAdd2 ||
        scanDataType == BarcodeSymbologies.UpcaAdd5 ||
        scanDataType == BarcodeSymbologies.Upce ||
        scanDataType == BarcodeSymbologies.UpceAdd2 ||
        scanDataType == BarcodeSymbologies.UpceAdd5 ||
        scanDataType == BarcodeSymbologies.Ean8 ||
        scanDataType == BarcodeSymbologies.TfStd)
    {
        // The UPC, EAN8, and 2 of 5 families encode the digits 0..9
        // which are then sent to the app in a UTF8 string (like "01234").
        return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, args.Report.ScanDataLabel);
    }

    // Some other symbologies (typically 2-D symbologies) contain binary data that
    // should not be converted to text.
    else
    {
        return "Decoded data unavailable.";
    }
}
```

### Get the raw scan data

To get the full, raw data from the barcode, we simply convert the buffer we get from **ScanData** into a string.

```cs
private string GetRawData(BarcodeScannerDataReceivedEventArgs args)
{
    // Get the full, raw barcode data.
    if (args.Report.ScanData == null)
    {
        return "No data";
    }

    // Just to show that we have the raw data, we'll print the value of the bytes.
    else
    {
        return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, args.Report.ScanData);
    }
}
```

These data are, in general, in the format as delivered from the scanner. Message header and trailer information are removed, however, since they do not contain useful information for an application and are likely to be scanner-specific.

Common header information is a prefix character (such as an STX character). Common trailer information is a terminator character (such as an ETX or CR character) and a block check character if one is generated by the scanner.

This property should include a symbology character if one is returned by the scanner (for example, an **A** for UPC-A). It should also include check digits if they are present in the label and returned by the scanner. (Note that both symbology characters and check digits may or may not be present, depending upon the scanner configuration. The scanner will return them if present, but will not generate or calculate them if they are absent.)

Some merchandise may be marked with a supplemental barcode. This barcode is typically placed to the right of the main barcode, and consists of an additional two or five characters of information. If the scanner reads merchandise that contains both main and supplemental barcodes, the supplemental characters are appended to the main characters, and the result is delivered to the application as one label. (Note that a scanner may support a configuration that enables or disables the reading of supplemental codes.)

Some merchandise may be marked with multiple labels, sometimes called *multisymbol labels* or *tiered labels*. These barcodes are typically arranged vertically, and may be of the same or different symbology. If the scanner reads merchandise that contains multiple labels, each barcode is delivered to the application as a separate label. This is necessary due to the current lack of standardization of these barcode types. One is not able to determine all variations based upon the individual barcode data. Therefore, the application will need to determine when a multiple label barcode has been read based upon the data returned. (Note that a scanner may or may not support reading of multiple labels.)

This value is set prior to a **DataReceived** event being raised to the application.

[!INCLUDE [feedback](./includes/pos-feedback.md)]

## See also
* [Barcode scanner](pos-barcodescanner.md)
* [ClaimedBarcodeScanner Class](/uwp/api/windows.devices.pointofservice.barcodesymbologies.getname)
* [BarcodeScannerDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.barcodescannerdatareceivedeventargs)
* [BarcodeScannerReport Class](/uwp/api/windows.devices.pointofservice.barcodescannerreport)
* [BarcodeSymbologies Class](/uwp/api/windows.devices.pointofservice.barcodesymbologies)