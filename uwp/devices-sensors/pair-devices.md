---
ms.assetid: F8A741B4-7A6A-4160-8C5D-6B92E267E6EA
title: Pair devices
description: Some devices need to be paired before they can be used. The Windows.Devices.Enumeration namespace supports three different ways to pair devices.
ms.date: 04/19/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: 19H1
---
# Pair devices



**Important APIs**

- [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration)

Some devices need to be paired before they can be used. The [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) namespace supports three different ways to pair devices.

-   Automatic pairing
-   Basic pairing
-   Custom pairing

**Tip**  Some devices do not need to be paired in order to be used. This is covered under the section on automatic pairing.

 

## Automatic pairing


Sometimes you want to use a device in your application, but do not care whether or not the device is paired. You simply want to be able to use the functionality associated with a device. For example, if your app wants to simply capture an image from a webcam, you are not necessarily interested in the device itself, just the image capture. If there are device APIs available for the device you are interested in, this scenario would fall under automatic pairing.

In this case, you simply use the APIs associated with the device, making the calls as necessary and trusting the system to handle any pairing that might be necessary. Some devices do not need to be paired in order for you to use their functionality. If the device does need to be paired, then the device APIs will handle the pairing action behind the scenes so you do not need to integrate that functionality into your app. Your app will have no knowledge about whether or not a given device is paired or needs to be, but you will still be able to access the device and use its functionality.

## Basic pairing


Basic pairing is when your application uses the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs in order to attempt to pair the device. In this scenario, you are letting Windows attempt the pairing process and handle it. If any user interaction is necessary, it will be handled by Windows. You would use basic pairing if you need to pair with a device and there is not a relevant device API that will attempt automatic pairing. You just want to be able to use the device and need to pair with it first.

In order to attempt basic pairing, you first need to obtain the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object for the device you are interested in. Once you receive that object, you will interact with the [**DeviceInformation.Pairing**](/uwp/api/windows.devices.enumeration.deviceinformation.pairing) property, which is a [**DeviceInformationPairing**](/uwp/api/windows.devices.enumeration.deviceinformation.pairing) object. To attempt to pair, simply call [**DeviceInformationPairing.PairAsync**](/uwp/api/windows.devices.enumeration.deviceinformationpairing.pairasync). You will need to **await** the result in order to give your app time to attempt to complete the pairing action. The result of the pairing action will be returned, and as long as no errors are returned, the device will be paired.

If you are using basic pairing, you also have access to additional information about the pairing status of the device. For example you know the pairing status ([**IsPaired**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationPairing.IsPaired)) and whether the device can pair ([**CanPair**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationPairing.CanPair)). Both of these are properties of the [**DeviceInformationPairing**](/uwp/api/windows.devices.enumeration.deviceinformation.pairing) object. If you are using automatic pairing, you might not have access to this information unless you obtain the relevant [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) objects.

## Custom pairing


Custom pairing enables your app to participate in the pairing process. This allows your app to specify the [**DevicePairingKinds**](/uwp/api/Windows.Devices.Enumeration.DevicePairingKinds) that are supported for the pairing process. You will also be responsible for creating your own user interface to interact with the user as needed. Use custom pairing when you want your app to have a little more influence over how the pairing process proceeds or to display your own pairing user interface.

In order to implement custom pairing, you will need to obtain the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object for the device you are interested in, just like with basic pairing. However, the specific property your are interested in is [**DeviceInformation.Pairing.Custom**](/uwp/api/windows.devices.enumeration.deviceinformationpairing.custom). This will give you a [**DeviceInformationCustomPairing**](/uwp/api/windows.devices.enumeration.deviceinformationcustompairing) object. All of the [**DeviceInformationCustomPairing.PairAsync**](/uwp/api/windows.devices.enumeration.deviceinformationcustompairing.pairasync) methods require you to include a [**DevicePairingKinds**](/uwp/api/Windows.Devices.Enumeration.DevicePairingKinds) parameter. This indicates the actions that the user will need to take in order to attempt to pair the device. See the **DevicePairingKinds** reference page for more information about the different kinds and what actions the user will need to take. Just like with basic pairing, you will need to **await** the result in order to give your app time to attempt to complete the pairing action. The result of the pairing action will be returned, and as long as no errors are returned, the device will be paired.

To support custom pairing, you will need to create a handler for the [**PairingRequested**](/uwp/api/windows.devices.enumeration.deviceinformationcustompairing.pairingrequested) event. This handler needs to make sure to account for all the different [**DevicePairingKinds**](/uwp/api/Windows.Devices.Enumeration.DevicePairingKinds) that might be used in a custom pairing scenario. The appropriate action to take will depend on the **DevicePairingKinds** provided as part of the event arguments.

It is important to be aware that custom pairing is always a system-level operation. Because of this, when you are operating on Desktop or Windows Phone, a system dialog will always be shown to the user when pairing is going to happen. This is because both of those platforms posses a user experience that requires user consent. Since that dialog is automatically generated, you will not need to create your own dialog when you are opting for a [**DevicePairingKinds**](/uwp/api/Windows.Devices.Enumeration.DevicePairingKinds) of **ConfirmOnly** when operating on these platforms. For the other **DevicePairingKinds**, you will need to perform some special handling depending on the specific **DevicePairingKinds** value. See the sample for examples of how to handle custom pairing for different **DevicePairingKinds** values.

Starting with Windows 10, version 1903, a new **DevicePairingKinds** is supported, **ProvidePasswordCredential**. This value means that the app must request a user name and password from the user in order to authenticate with the paired device. To handle this case, call the [**AcceptWithPasswordCredential**](/uwp/api/windows.devices.enumeration.devicepairingrequestedeventargs.acceptwithpasswordcredential?branch=release-19h1#Windows_Devices_Enumeration_DevicePairingRequestedEventArgs_AcceptWithPasswordCredential_Windows_Security_Credentials_PasswordCredential_) method of the event args of the **PairingRequested** event handler to accept the pairing. Pass in a [**PasswordCredential**](/uwp/api/windows.security.credentials.passwordcredential) object that encapsulates the user name and password as a parameter. Note that the username and password for the remote device are distinct from and often not the same as the credentials for the locally signed-in user.

## Unpairing


Unpairing a device is only relevant in the basic or custom pairing scenarios described above. If you are using automatic pairing, your app remains oblivious to the pairing status of the device and there is no need to unpair it. If you do choose to unpair a device, the process is identical whether you implement basic or custom pairing. This is because there is no need to provide additional information or interact in the unpairing process.

The first step to unpairing a device is obtaining the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object for the device that you want to unpair. Then you need to retrieve the [**DeviceInformation.Pairing**](/uwp/api/windows.devices.enumeration.deviceinformation.pairing) property and call [**DeviceInformationPairing.UnpairAsync**](/uwp/api/windows.devices.enumeration.deviceinformationpairing.unpairasync). Just like with pairing, you will want to **await** the result. The result of the unpairing action will be returned, and as long as no errors are returned, the device will be unpaired.

## Sample


To download a sample showing how to use the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs, click [here](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/DeviceEnumerationAndPairing).

 

 