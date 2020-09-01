---
title: Bluetooth developer FAQ
description: This article contains answers to commonly asked questions related to the UWP bluetooth APIs.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: e7dee32d-3756-430d-a026-32c1ee288a85
ms.localizationpriority: medium
---
# Bluetooth Developer FAQ

This article contains answers to commonly asked UWP Bluetooth API questions.

## What APIs do I use? Bluetooth Classic (RFCOMM) or Bluetooth Low Energy (GATT)?
There are various discussions online around this general topic so let's keep this answer squarely on the difference with respect to Windows. Here are some general guidelines:

### Bluetooth LE (Windows.Devices.Bluetooth.GenericAttributeProfile)

Use the GATT APIs when you are communicating with a device that supports Bluetooth Low Energy. If your use case is infrequent, low bandwidth, or requires low power, Bluetooth Low Energy is the answer. The main namespace that includes this functionality is [Windows.Devices.Bluetooth.GenericAttributeProfile](/uwp/api/Windows.Devices.Bluetooth.GenericAttributeProfile). 

**When not to use Bluetooth LE**
- High bandwidth, high frequency scenarios. If you need to constantly keep sync with large amounts of data, consider using Bluetooth classic or maybe even WiFi. 

### Bluetooth Classic (Windows.Devices.Bluetooth.Rfcomm)

The RFCOMM APIs give developers a socket to perform bidirectional serial port-style communication. Once you've got a socket, the methods for writing and reading are fairly standard. An implementation of this is presented in the [Rfcomm Chat sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/BluetoothRfcommChat). 

**When not to use Bluetooth Rfcomm** 
- Notifications. The Bluetooth GATT protocol has a specific command for this and will result in significantly less power draw and faster response times. 
- Checking for proximity or presence detection. Better to use the [Advertisement APIs](/uwp/api/windows.devices.bluetooth.advertisement) and connect over Bluetooth LE. 


## Why does my Bluetooth LE Device stop responding after a disconnect?

The most common reason this occurs is because the remote device has lost pairing information. A large number of older Bluetooth devices don't require authentication. To protect the user, all pairing transactions performed from the Settings application will require authentication, and some devices were not designed with this in mind. 

Starting with Windows 10 release 1511, developers have control over the pairing handshake. The [Device Enumeration and Pairing Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/DeviceEnumerationAndPairing) details the various aspects of associating new devices.

In this example, we initiate pairing with a device using no encryption. Note, this will only work if the remote device does not require encryption or authentication to function.

```csharp
// Get ceremony type and protection level selections
// You must select at least ConfirmOnly or the pairing attempt will fail
    DevicePairingKinds ceremonySelected = DevicePairingKinds.ConfirmOnly;

//  Workaround remote devices losing pairing information
    DevicePairingProtectionLevel protectionLevel = DevicePairingProtectionLevel.None

    DeviceInformationCustomPairing customPairing = deviceInfoDisp.DeviceInformation.Pairing.Custom;

// Declare an event handler - you don't need to do much in PairingRequestedHandler since the ceremony is "None"
    customPairing.PairingRequested += PairingRequestedHandler;
    DevicePairingResult result = await customPairing.PairAsync(ceremonySelected, protectionLevel);
```

## Do I have to pair Bluetooth devices before using them?

You don't have to pair devices before using them if leveraging Bluetooth RFCOMM (classic). Starting with Windows 10 release 1607, you can simply query for nearby devices and connect to them. The updated [RFCOMM Chat Sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/BluetoothRfcommChat) shows this functionality. 

**(14393 and below)** This feature is not available for Bluetooth Low Energy (GATT Client), so you will still have to pair either through the Settings page or using the [Windows.Devices.Enumeration](/uwp/api/windows.devices.enumeration) APIs in order to access these devices.

**(15030 and above)** Pairing Bluetooth devices is no longer needed. Use the new Async APIs like GetGattServicesAsync and GetCharacteristicsAsync in order to query the current state of the remote device. See the [Client docs](gatt-client.md) for more details. 

## When should I pair with a device before communicating with it?
Generally, if you require a trusted, long-term bond with a device, pair with it by either directing the user to the settings page or using the Device Enumeration and Pairing APIs. If you simply need to read information from the device that is exposed publicly (a temperature sensor or beacon), then connect or listen for advertisements without making any effort to pair with the device. This will prevent interoperability problems in the long run, because a large number of devices do not support pairing. 

## Do all Windows devices support Peripheral Role?

No. This is a hardware-dependent feature, but a method is provided, BluetoothAdapter.IsPeripheralRoleSupported, to query whether it is supported or not.  Currently supported devices include Windows Phone on 8992+ and RPi3 (Windows IoT). 

## Can I access these APIs from Win32?

Yes, all these APIs should work. This blog details the way to call [Windows APIs from Desktop applications](https://blogs.windows.com/buildingapps/2017/01/25/calling-windows-10-apis-desktop-application/). 
## Is this functionality supposed to exist on *-Insert SKU here-*?

**Bluetooth LE**: Yes, all functionality is in OneCore and should be available on most recent devices w/ a functioning Bluetooth LE stack. 
> Caveat: Peripheral Role is hardware-dependent and some Windows Server Editions don't support Bluetooth. 

**Bluetooth BR/EDR (Classic)**: Some variations exist but mostly, they have very similar profile level support. See the docs on [RFCOMM](send-or-receive-files-with-rfcomm.md) and these supported profile documents for [PC](https://support.microsoft.com/help/10568/windows-10-supported-bluetooth-profiles) and [Phone](https://support.microsoft.com/help/10569/windows-10-mobile-supported-bluetooth-profiles)