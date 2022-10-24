---
ms.assetid: E0B9532F-1195-4927-99BE-F41565D891AD
title: Enumerate devices over a network
description: In addition to discovering locally connected devices, you can use the Windows.Devices.Enumeration APIs to enumerate devices over wireless and networked protocols.
ms.date: 04/19/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: 19H1
---
# Enumerate devices over a network



**Important APIs**

- [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration)

In addition to discovering locally connected devices, you can use the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs to enumerate devices over wireless and networked protocols.

## Enumerating devices over networked or wireless protocols

Sometimes you need to enumerate devices that are not locally connected and can only be discovered over a wireless or networking protocols. In order to do so, the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs have three different kinds of device objects: the **AssociationEndpoint** (AEP), the **AssociationEndpointContainer** (AEP Container), and the **AssociationEndpointService** (AEP Service). As a group these are referred to as AEPs or AEP objects.

Some device APIs provide a selector string that you can use to enumerate through the available AEP objects. This could include both devices that are paired and are not paired with the system. Some of the devices might not require pairing. Those device APIs may attempt to pair the device if pairing it is necessary before interacting with it. Wi-Fi Direct is an example of APIs that follow this pattern. If those device APIs do not automatically pair the device, you can pair it using the [**DeviceInformationPairing**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationPairing) object available from [**DeviceInformation.Pairing**](/uwp/api/windows.devices.enumeration.deviceinformation.pairing).

However, there may be cases where you want to manually discover devices on your own without using a pre-defined selector string. For example, you may just need to gather information about AEP devices without interacting with them or you may want to find more AEP objects than will be discovered with the pre-defined selector string. In this case, you will build your own selector string and use it following the instructions under [Build a device selector](build-a-device-selector.md).

When you build your own selector, it is strongly recommended that you limit your scope of enumeration to the protocols that you are interested in. For example, you don't want to have the Wi-Fi radio search for Wi-Fi Direct devices if you are particularly interested in UPnP devices. Windows has defined an identity for each protocol that you can use to scope your enumeration. The following table lists the protocol types and identifiers.

| Protocol or network device type              | Id                                         |
|----------------------------------------------|--------------------------------------------|
| UPnP (including DIAL and DLNA)               | **{0e261de4-12f0-46e6-91ba-428607ccef64}** |
| Web services on devices (WSD)                | **{782232aa-a2f9-4993-971b-aedc551346b0}** |
| Wi-Fi Direct                                 | **{0407d24e-53de-4c9a-9ba1-9ced54641188}** |
| DNS service discovery (DNS-SD)               | **{4526e8c1-8aac-4153-9b16-55e86ada0e54}** |
| Point of service                             | **{d4bf61b3-442e-4ada-882d-fa7B70c832d9}** |
| Network printers (active directory printers) | **{37aba761-2124-454c-8d82-c42962c2de2b}** |
| Windows connect now (WNC)                    | **{4c1b1ef8-2f62-4b9f-9bc5-b21ab636138f}** |
| WiGig docks                                  | **{a277f3a5-8764-4f88-8045-4c5e962640b1}** |
| Wi-Fi provisioning for HP printers           | **{c85ef710-f344-4792-bb6d-85a4346f1e69}** |
| Bluetooth                                    | **{e0cbf06c-cd8b-4647-bb8a-263b43f0f974}** |
| Bluetooth LE                                 | **{bb7bb05e-5972-42b5-94fc-76eaa7084d49}** |
| Network Camera                               | **{b8238652-b500-41eb-b4f3-4234f7f5ae99}** |

 

## AQS examples

Each AEP kind has a property you can use to constrain your enumeration to a specific protocol. Keep in mind you can use the OR operator in an AQS filter to combine multiple protocols. Here are some examples of AQS filter strings that show how to query for AEP devices.

This AQS queries for all UPnP **AssociationEndpoint** objects when the [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) is set to **AsssociationEndpoint**.

``` syntax
System.Devices.Aep.ProtocolId:="{0e261de4-12f0-46e6-91ba-428607ccef64}"
```

This AQS queries for all UPnP and WSD **AssociationEndpoint** objects when the [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) is set to **AsssociationEndpoint**.

``` syntax
System.Devices.Aep.ProtocolId:="{782232aa-a2f9-4993-971b-aedc551346b0}" OR
System.Devices.Aep.ProtocolId:="{0e261de4-12f0-46e6-91ba-428607ccef64}"
```

This AQS queries for all UPnP **AssociationEndpointService** objects if the [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) is set to **AsssociationEndpointService**.

``` syntax
System.Devices.AepService.ProtocolId:="{0e261de4-12f0-46e6-91ba-428607ccef64}"
```

This AQS queries **AssociationEndpointContainer** objects when the [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) is set to **AssociationEndpointContainer**, but only finds them by enumerating the UPnP protocol. Typically, it wouldn't be useful to enumerate containers that only come from one protocol. However, this might be useful by limiting your filter to protocols where you know your device can be discovered.

``` syntax
System.Devices.AepContainer.ProtocolIds:~~"{0e261de4-12f0-46e6-91ba-428607ccef64}"
```

 

 