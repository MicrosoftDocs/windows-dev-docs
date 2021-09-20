---
title: Working with barcode scanner symbologies
description: Use the UWP barcode scanner APIs to process barcode symbologies without manually configuring the scanner.
ms.date: 08/29/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Working with symbologies
A [barcode symbology](/uwp/api/windows.devices.pointofservice.barcodesymbologies) is the mapping of data to a specific barcode format. Some common symbologies include UPC, Code 128, QR Code, and so on.  The Universal Windows Platform barcode scanner APIs allow an application to control how the scanner processes these symbologies without manually configuring the scanner. 

## Determine which symbologies are supported 
Since your application may be used with different barcode scanner models from multiple manufacturers, you may want to query the scanner to determine the list of symbologies that it supports.  This can be useful if your application requires a specific symbology that may not be supported by all scanners or you need to enable symbologies that have been either manually or programmatically disabled on the scanner.

Once you have a [BarcodeScanner](/uwp/api/windows.devices.pointofservice.barcodescanner) object by using [BarcodeScanner.FromIdAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.fromidasync), call [GetSupportedSymbologiesAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.getsupportedsymbologiesasync#Windows_Devices_PointOfService_BarcodeScanner_GetSupportedSymbologiesAsync) to obtain a list of symbologies supported by the device.

The following example gets a list of the supported symbologies of the barcode scanner, and displays them in a text block:

```cs
private void DisplaySupportedSymbologies(BarcodeScanner barcodeScanner, TextBlock textBlock) 
{
    var supportedSymbologies = await barcodeScanner.GetSupportedSymbologiesAsync();

    foreach (uint item in supportedSymbologies)
    {
        string symbology = BarcodeSymbologies.GetName(item);
        textBlock.Text += (symbology + "\n");
    }
}
```

## Determine if a specific symbology is supported
To determine if the scanner supports a specific symbology you can call [IsSymbologySupportedAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.issymbologysupportedasync#Windows_Devices_PointOfService_BarcodeScanner_IsSymbologySupportedAsync_System_UInt32_).

The following example checks if the barcode scanner supports the **Code32** symbology:

```cs
bool symbologySupported = await barcodeScanner.IsSymbologySupportedAsync(BarcodeSymbologies.Code32);
```

## Change which symbologies are recognized
In some cases, you may want to use a subset of symbologies that the barcode scanner supports.  This is particularly useful to block symbologies that you do not intend to use in your application. For example, to ensure a user scans the right barcode, you could constrain scanning to UPC or EAN when acquiring item SKUs and constrain scanning to Code 128 when acquiring serial numbers.

Once you know the symbologies that your scanner supports, you can set the symbologies that you want it to recognize.  This can be done after you have established a 
[ClaimedBarcodeScanner](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner) object using [ClaimScannerAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync#Windows_Devices_PointOfService_BarcodeScanner_ClaimScannerAsync). You can call [SetActiveSymbologiesAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setactivesymbologiesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_SetActiveSymbologiesAsync_Windows_Foundation_Collections_IIterable_System_UInt32__) to enable a specific set of symbologies while those omitted from your list are disabled.

The following example sets the active symbologies of a claimed barcode scanner to [Code39](/uwp/api/windows.devices.pointofservice.barcodesymbologies.code39#Windows_Devices_PointOfService_BarcodeSymbologies_Code39) and [Code39Ex](/uwp/api/windows.devices.pointofservice.barcodesymbologies.code39ex):

```cs
private async void SetSymbologies(ClaimedBarcodeScanner claimedBarcodeScanner) 
{
    var symbologies = new List<uint>{ BarcodeSymbologies.Code39, BarcodeSymbologies.Code39Ex };
    await claimedBarcodeScanner.SetActiveSymbologiesAsync(symbologies);
}
```

## Barcode symbology attributes
Different barcode symbologies can have different attributes, such as supporting multiple decode lengths, transmitting the check digit to the host as part of the raw data, and check digit validation. With the [BarcodeSymbologyAttributes](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes) class, you can get and set these attributes for a given [ClaimedBarcodeScanner](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner) and barcode symbology.

You can get the attributes of a given symbology with [GetSymbologyAttributesAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.getsymbologyattributesasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_GetSymbologyAttributesAsync_System_UInt32_). The following code snippet gets the attributes of the Upca symbology for a **ClaimedBarcodeScanner**.

```cs
BarcodeSymbologyAttributes barcodeSymbologyAttributes = 
    await claimedBarcodeScanner.GetSymbologyAttributesAsync(BarcodeSymbologies.Upca);
```

When you've finished modifying the attributes and are ready to set them, you can call [SetSymbologyAttributesAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.setsymbologyattributesasync). This method returns a **bool**, which is **true** if the attributes were successfully set.

```cs
bool success = await claimedBarcodeScanner.SetSymbologyAttributesAsync(
    BarcodeSymbologies.Upca, barcodeSymbologyAttributes);
```

### Restrict scan data by data length
Some symbologies are variable length such as Code 39 or Code 128.  Barcodes of these symbologies can be located near each other containing different data often of specific length. Setting the specific length of the data you require can prevent invalid scans.

Before setting the decode length, check whether the barcode symbology supports multiple lengths with [IsDecodeLengthSupported](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.isdecodelengthsupported#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_IsDecodeLengthSupported). Once you know that it's supported, you can set the [DecodeLengthKind](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.decodelengthkind#Windows_Devices_PointOfService_BarcodeSymbologyAttributes_DecodeLengthKind), which is of type [BarcodeSymbologyDecodeLengthKind](/uwp/api/windows.devices.pointofservice.barcodesymbologydecodelengthkind). This property can be any of the following values:

* **AnyLength**: Decode lengths of any number.
* **Discrete**: Decode lengths of either [DecodeLength1](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.decodelength1) or [DecodeLength2](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.decodelength2) single-byte characters.
* **Range**: Decode lengths between **DecodeLength1** and **DecodeLength2** single-byte characters. The order of **DecodeLength1** and **DecodeLength2** do not matter (either can be higher or lower than the other).

Finally, you can set the values of **DecodeLength1** and **DecodeLength2** to control the length of the data you require.

The following code snippet demonstrates setting the decode length:

```cs
private async Task<bool> SetDecodeLength(
    ClaimedBarcodeScanner scanner,
    uint symbology, 
    BarcodeSymbologyDecodeLengthKind kind, 
    uint decodeLength1, 
    uint decodeLength2)
{
    bool success = false;
    BarcodeSymbologyAttributes attributes = await scanner.GetSymbologyAttributesAsync(symbology);

    if (attributes.IsDecodeLengthSupported)
    {
        attributes.DecodeLengthKind = kind;
        attributes.DecodeLength1 = decodeLength1;
        attributes.DecodeLength2 = decodeLength2;
        success = await scanner.SetSymbologyAttributesAsync(symbology, attributes);
    }

    return success;
}
```

### Check digit transmission

Another attribute you can set on a symbology is whether the check digit will be transmitted to the host as part of the raw data. Before setting this, make sure that the symbology supports check digit transmission with [IsCheckDigitTransmissionSupported](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigittransmissionsupported). Then, set whether check digit transmission is enabled with [IsCheckDigitTransmissionEnabled](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigittransmissionenabled).

The following code snippet demonstrates setting check digit transmission:

```cs
private async Task<bool> SetCheckDigitTransmission(ClaimedBarcodeScanner scanner, uint symbology, bool isEnabled)
{
    bool success = false;
    BarcodeSymbologyAttributes attributes = await scanner.GetSymbologyAttributesAsync(symbology);

    if (attributes.IsCheckDigitTransmissionSupported)
    {
        attributes.IsCheckDigitTransmissionEnabled = isEnabled;
        success = await scanner.SetSymbologyAttributesAsync(symbology, attributes);
    }

    return success;
}
```

### Check digit validation

You can also set whether the barcode check digit will be validated. Before setting this, make sure that the symbology supports check digit validation with [IsCheckDigitValidationSupported](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigitvalidationsupported). Then, set whether check digit validation is enabled with [IsCheckDigitValidationEnabled](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes.ischeckdigitvalidationenabled).

The following code snippet demonstrates setting check digit validation:

```cs
private async Task<bool> SetCheckDigitValidation(ClaimedBarcodeScanner scanner, uint symbology, bool isEnabled)
{
    bool success = false;
    BarcodeSymbologyAttributes attributes = await scanner.GetSymbologyAttributesAsync(symbology);

    if (attributes.IsCheckDigitValidationSupported)
    {
        attributes.IsCheckDigitValidationEnabled = isEnabled;
        success = await scanner.SetSymbologyAttributesAsync(symbology, attributes);
    }

    return success;
}
```

[!INCLUDE [feedback](./includes/pos-feedback.md)]

## See also

* [Barcode scanner](pos-barcodescanner.md)
* [BarcodeSymbologies Class](/uwp/api/windows.devices.pointofservice.barcodesymbologies)
* [BarcodeScanner Class](/uwp/api/windows.devices.pointofservice.barcodescanner)
* [ClaimedBarcodeScanner Class](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner)
* [BarcodeSymbologyAttributes Class](/uwp/api/windows.devices.pointofservice.barcodesymbologyattributes)
* [BarcodeSymbologyDecodeLengthKind Enum](/uwp/api/windows.devices.pointofservice.barcodesymbologydecodelengthkind)