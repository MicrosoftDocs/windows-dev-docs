---
title: PointOfService device claim and enable model
description: Use the Point of Service device claim and enable APIs to claim devices and enable them for I/O operations. 
ms.date: 06/19/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Point of Service device claim and enable model

## Claiming for exclusive use

After you have successfully created a PointOfService device object, you must claim it using the appropriate claim method for the device type before you can use the device for input or output.  Claim grants the application exclusive access to many of the device's functions to ensure that one application does not interfere with the use of the device by another application.  Only one application can claim a PointOfService device for exclusive use at a time. 

> [!Note]
> The claim action establishes an exclusive lock to a device, but does not put it into an operational state.  See [Enable device for I/O operations](#enable-device-for-io-operations) for more information.

### APIs used to claim / release

|Device|Claim | Release | 
|-|:-|:-|
|BarcodeScanner | [BarcodeScanner.ClaimScannerAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync) | [ClaimedBarcodeScanner.Close](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.close) |
|CashDrawer | [CashDrawer.ClaimDrawerAsync](/uwp/api/windows.devices.pointofservice.cashdrawer.claimdrawerasync) | [ClaimedCashDrawer.Close](/uwp/api/windows.devices.pointofservice.claimedcashdrawer.close) | 
|LineDisplay | [LineDisplay.ClaimAsync](/uwp/api/windows.devices.pointofservice.linedisplay.claimasync) |  [ClaimedineDisplay.Close](/uwp/api/windows.devices.pointofservice.claimedlinedisplay.close) | 
|MagneticStripeReader | [MagneticStripeReader.ClaimReaderAsync](/uwp/api/windows.devices.pointofservice.magneticstripereader.claimreaderasync) |  [ClaimedMagneticStripeReader.Close](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.close) | 
|PosPrinter | [PosPrinter.ClaimPrinterAsync](/uwp/api/windows.devices.pointofservice.posprinter.claimprinterasync) |  [ClaimedPosPrinter.Close](/uwp/api/windows.devices.pointofservice.claimedposprinter.close) | 
 | 

## Enable device for I/O operations

The claim action simply establishes an exclusive rights to the device, but does not put it into an operational state.  In order to receive events or perform any output operations you must enable the device using **EnableAsync**.  Conversely, you can call **DisableAsync** to stop listening to events from the device or performing output.  You can also use **IsEnabled** to determine the state of your device.

### APIs used enable / disable

| Device | Enable | Disable | IsEnabled? |
|-|:-|:-|:-|
|ClaimedBarcodeScanner | [EnableAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.enableasync) | [DisableAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.disableasync) | [IsEnabled](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isenabled) | 
|ClaimedCashDrawer | [EnableAsync](/uwp/api/windows.devices.pointofservice.claimedcashdrawer.enableasync) | [DisableAsync](/uwp/api/windows.devices.pointofservice.claimedcashdrawer.disableasync) | [IsEnabled](/uwp/api/windows.devices.pointofservice.claimedcashdrawer.isenabled) |
|ClaimedLineDisplay | Not Applicable¹ | Not Applicable¹ | Not Applicable¹ | 
|ClaimedMagneticStripeReader | [EnableAsync](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.enableasync) | [DisableAsync](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.disableasync) | [IsEnabled](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.isenabled) |  
|ClaimedPosPrinter | [EnableAsync](/uwp/api/windows.devices.pointofservice.claimedposprinter.enableasync) | [DisableAsync](/uwp/api/windows.devices.pointofservice.claimedposprinter.disableasync) | [IsEnabled](/uwp/api/windows.devices.pointofservice.claimedposprinter.isenabled) |
|

¹ Line Display does not require you to explicitly enable the device for I/O operations.  Enabling is performed automatically by the PointOfService LineDisplay APIs which perform I/O.

## Code sample: claim and enable

This sample shows how to claim a barcode scanner device after you have successfully created a barcode scanner object.

```Csharp

    BarcodeScanner barcodeScanner = await BarcodeScanner.FromIdAsync(DeviceId);

    if(barcodeScanner != null)
    {
        // after successful creation, claim the scanner for exclusive use 
        claimedBarcodeScanner = await barcodeScanner.ClaimScannerAsync();

        if(claimedBarcodeScanner != null)
        {
            // after successful claim, enable scanner for data events to fire
            await claimedBarcodeScanner.EnableAsync();
        }
        else
        {
            Debug.WriteLine("Failure to claim barcodeScanner");
        }
    }
    else
    {
        Debug.WriteLine("Failure to create barcodeScanner object");
    }
    
```

> [!Warning]
> A claim can be lost in the following circumstances:
> 1. Another app has requested a claim of the same device and your app did not issue a **RetainDevice** in response to the **ReleaseDeviceRequested** event.  (See [Claim negotiation](#claim-negotiation) below for more information.)
> 2. Your app has been suspended, which resulted in the device object being closed and as a result the claim is no longer valid. (See [Device object lifecycle](pos-basics-deviceobject.md#device-object-lifecycle) for more information.)


## Claim negotiation

Since Windows is a multi-tasking environment it is possible for multiple applications on the same computer to require access to peripherals in a cooperative manner.  The PointOfService APIs provide a negotiation model that allows for multiple applications to share peripherals connected to the computer.

When a second application on the same computer requests a Claim for a PointOfService peripheral that is already claimed by another application, a **ReleaseDeviceRequested** event notification is published. The application with the active claim must respond to the event notification by calling **RetainDevice** if the application is currently using the device to avoid losing the claim. 

If the application with the active claim does not respond with **RetainDevice** right away it is assumed that the application has been suspended or does not need the device and the claim is revoked and given to the new application. 

The first step is to create an event handler which responds to the **ReleaseDeviceRequested** event with **RetainDevice**.  

```Csharp
    /// <summary>
    /// Event handler for the ReleaseDeviceRequested event which occurs when 
    /// the claimed barcode scanner receives a Claim request from another application
    /// </summary>
    void claimedBarcodeScanner_ReleaseDeviceRequested(object sender, ClaimedBarcodeScanner myScanner)
    {
        // Retain exclusive access to the device
        myScanner.RetainDevice();
    }
```

Then register the event handler in association with your claimed device

```Csharp
    BarcodeScanner barcodeScanner = await BarcodeScanner.FromIdAsync(DeviceId);

    if(barcodeScanner != null)
    {
        // after successful creation, claim the scanner for exclusive use 
        claimedBarcodeScanner = await barcodeScanner.ClaimScannerAsync();

        if(claimedBarcodeScanner != null)
        {
            // register a release request handler to prevent loss of scanner during active use
            claimedBarcodeScanner.ReleaseDeviceRequested += claimedBarcodeScanner_ReleaseDeviceRequested;

            // after successful claim, enable scanner for data events to fire
            await claimedBarcodeScanner.EnableAsync();          
        }
        else
        {
            Debug.WriteLine("Failure to claim barcodeScanner");
        }
    }
    else
    {
        Debug.WriteLine("Failure to create barcodeScanner object");
    }
```



### APIs used for claim negotiation

|Claimed device|Release Notification| Retain Device |
|-|:-|:-|
|ClaimedBarcodeScanner | [ReleaseDeviceRequested](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.releasedevicerequested) | [RetainDevice](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.retaindevice)
|ClaimedCashDrawer | [ReleaseDeviceRequested](/uwp/api/windows.devices.pointofservice.claimedcashdrawer.releasedevicerequested) | [RetainDevice](/uwp/api/windows.devices.pointofservice.claimedcashdrawer.retaindevice)
|ClaimedLineDisplay | [ReleaseDeviceRequested](/uwp/api/windows.devices.pointofservice.claimedlinedisplay.releasedevicerequested) | [RetainDevice](/uwp/api/windows.devices.pointofservice.claimedlinedisplay.retaindevice)
|ClaimedMagneticStripeReader | [ReleaseDeviceRequested](/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader.releasedevicerequested) | [RetainDevice](/uwp/api/windows.devices.pointofservice.claimedlinedisplay.retaindevice)
|ClaimedPosPrinter | [ReleaseDeviceRequested](/uwp/api/windows.devices.pointofservice.claimedposprinter.releasedevicerequested) | [RetainDevice](/uwp/api/windows.devices.pointofservice.claimedposprinter.retaindevice)
|