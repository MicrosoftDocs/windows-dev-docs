---
author: msatranjr
title: Bluetooth developer FAQ
description: This article contains answers to commonly asked questions related to the UWP bluetooth APIs.
ms.author: misatran
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: e7dee32d-3756-430d-a026-32c1ee288a85
---
# Bluetooth Developer FAQ

This article contains answers to commonly asked UWP Bluetooth API questions.

## Why does my Bluetooth LE Device stop responding after a disconnect?

The common reason this happens is because the remote device has lost pairing information. A lot of earlier Bluetooth devices don't require authentication. To protect the user, all pairing ceremonies performed from the Settings app will require authentication and some devices don't know how to deal with that. 

Starting with Windows 10 release 1511, developers have control over the pairing ceremony. The [Device Enumeration and Pairing Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/DeviceEnumerationAndPairing) details the various aspects of associating new devices.

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

You don't have to for Bluetooth RFCOMM (classic) devices. Starting with Windows 10 release 1607, you can simply query for nearby devices and connect to them. The updated [RFCOMM Chat Sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/BluetoothRfcommChat) shows this functionality. 

This feature is not available for Bluetooth Low Energy (GATT Client), so you will still have to pair either through the Settings page or using the [Windows.Devices.Enumeration](https://msdn.microsoft.com/en-us/library/windows/apps/windows.devices.enumeration.aspx) APIs in order access these devices.

