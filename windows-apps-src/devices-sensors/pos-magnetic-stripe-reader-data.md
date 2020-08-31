---
title: Obtain and understand magnetic stripe data
description: Learn how to obtain and interpret the data from a magnetic stripe reader using Universal Windows Platform (UWP) Point of Service (POS) APIs.
ms.date: 10/04/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos, magnetic stripe reader
ms.localizationpriority: medium
---
# Obtain and understand magnetic stripe data

Once you've set up your magnetic stripe reader in your application using the steps outlined in [Getting started with Point of Service](pos-basics.md), you're ready to start getting data from it.

## Subscribe to *DataReceived events

Whenever the reader recognizes a swiped card, it will raise one of three events:

* [AamvaCardDataReceived Event](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.aamvacarddatareceived): Occurs when a motor vehicle card is swiped.
* [BankCardDataReceived Event](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.aamvacarddatareceived): Occurs when a bank card is swiped.
* [VendorSpecificDataReceived Event](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.vendorspecificdatareceived): Occurs when a vendor-specific card is swiped.

Your application only needs to subscribe to the events that are supported by the magnetic stripe reader. You can see what types of cards are supported with [MagneticStripeReader.SupportedCardTypes](/uwp/api/windows.devices.pointofservice.magneticstripereader.supportedcardtypes).

The following code demonstrates subscribing to the three ***DataReceived** events:

```cs
private void SubscribeToEvents(ClaimedMagneticStripeReader claimedReader, MagneticStripeReader reader)
{
    foreach (var type in reader.SupportedCardTypes)
    {
        if (item == MagneticStripeReaderCardTypes.Aamva)
        {
            claimedReader.AamvaCardDataReceived += Reader_AamvaCardDataReceived;
        }
        else if (item == MagneticStripeReaderCardTypes.Bank)
        {
            claimedReader.BankCardDataReceived += Reader_BankCardDataReceived;
        }
        else if (item == MagneticStripeReaderCardTypes.ExtendedBase)
        {
            claimedReader.VendorSpecificDataReceived += Reader_VendorSpecificDataReceived;
        }
    }
}
```

The event handler will be passed the [ClaimedMagneticStripeReader](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader) and an *args* object, whose type will vary depending on the event:

* **AamvaCardDataReceived** event: [MagneticStripeReaderAamvaCardDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.magneticstripereaderaamvacarddatareceivedeventargs)
* **BankCardDataReceived** event: [MagneticStripeReaderBankCardDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.magneticstripereaderbankcarddatareceivedeventargs)
* **VendorSpecificDataReceived** event: [MagneticStripeReaderVendorSpecificCardDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.magneticstripereadervendorspecificcarddatareceivedeventargs)

## Get the data

For the **AamvaCardDataReceived** and **BankCardDataReceived** events, you can get some of the data directly from the *args* object. The following example demonstrates getting a few properties and assigning them to member variables:

```cs
private string _accountNumber;
private string _expirationDate;
private string _firstName;

private void Reader_BankCardDataReceived(
    ClaimedMagneticStripeReader sender, 
    MagneticStripeReaderBankCardDataReceivedEventArgs args)
{
    _accountNumber = args.AccountNumber;
    _expirationDate = args.ExpirationDate;
    _firstName = args.FirstName;
    // etc...
}
```

However, some data, including all data from the **VendorSpecificDataReceived** event, must be retrieved through the **Report** object, which is a property of the *args* parameter. This is of type [MagneticStripeReaderReport](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport).

You can use the [CardType](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport.cardtype) property to figure out what type of card has been swiped, and then use that to inform how you interpret the data from [Track1](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport.track1), [Track2](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport.track2), [Track3](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport.track3), and [Track4](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport.track4).

The data from each of the tracks are represented as [MagneticStripeReaderTrackData](/uwp/api/windows.devices.pointofservice.magneticstripereadertrackdata) objects. From this class, you can get the following types of data:

* [Data](/uwp/api/windows.devices.pointofservice.magneticstripereadertrackdata.data): The raw or decoded data.
* [DiscretionaryData](/uwp/api/windows.devices.pointofservice.magneticstripereadertrackdata.discretionarydata): The discretionary data. 
* [EncryptedData](/uwp/api/windows.devices.pointofservice.magneticstripereadertrackdata.encrypteddata): The encrypted data.

The following code snippet gets the report and the track data, and then checks the card type:

```cs
private void GetTrackData(MagneticStripeReaderBankCardDataReceivedEventArgs args)
{
    MagneticStripeReaderReport report = args.Report;
    IBuffer track1 = report.Track1.Data;
    IBuffer track2 = report.Track2.Data;
    IBuffer track3 = report.Track3.Data;
    IBuffer track4 = report.Track4.Data;

    if (report.CardType == MagneticStripeReaderCardTypes.Aamva)
    {
        // Card type is AAMVA
    }
    else if (report.CardType == MagneticStripeReaderCardTypes.Bank)
    {
        // Card type is bank card
    }
    else if (report.CardType == MagneticStripeReaderCardTypes.ExtendedBase)
    {
        // Card type is vendor-specific
    }
    else if (report.CardType == MagneticStripeReaderCardTypes.Unknown)
    {
        // Card type is unknown
    }
}
```

[!INCLUDE [feedback](./includes/pos-feedback.md)]

## See also

* [Magnetic stripe reader](pos-magnetic-stripe-reader.md)
* [ClaimedMagneticStripeReader Class](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader)
* [MagneticStripeReaderAamvaCardDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.magneticstripereaderaamvacarddatareceivedeventargs)
* [MagneticStripeReaderBankCardDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.magneticstripereaderbankcarddatareceivedeventargs)
* [MagneticStripeReaderVendorSpecificCardDataReceivedEventArgs Class](/uwp/api/windows.devices.pointofservice.magneticstripereadervendorspecificcarddatareceivedeventargs)
* [MagneticStripeReaderReport](/uwp/api/windows.devices.pointofservice.magneticstripereaderreport)
* [MagneticStripeReaderTrackData](/uwp/api/windows.devices.pointofservice.magneticstripereadertrackdata)