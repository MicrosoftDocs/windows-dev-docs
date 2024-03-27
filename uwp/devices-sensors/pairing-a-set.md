---
title: Pairing a set with the DeviceInformationPairing.Custom property
description: Windows supports pairing a collection of devices as an automatically-discovered set or as an explicit set.
ms.date: 03/27/2024
ms.topic: article
ms.localizationpriority: medium
---

# Pairing a set with the DeviceInformationPairing.Custom property

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

Use the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs to pair devices as a set.

Windows supports pairing a collection of devices as a set. The platform supports two types of set.

* **Automatically-discovered set**. This happens when a protocol (such as Bluetooth LE) automatically discovers other endpoints that belong to the set while pairing a primary endpoint. For example, while a user is paring the right earbud over Bluetooth LE, the protocol stack might discover the left earbud; so both can be paired together as a set.
* **Explicit set**. These are useful when a device is discovered over more than one protocol. For example, Internet Printing Protocol (IPP) printers are typically discovered over three protocols: IPP, WSD, and eSCL. When multiple endpoints for the same device are discovered, then they can be explicitly paired together as a set.

## Code example for an automatically-discovered set (Bluetooth-style)

This code example implements automatically-discovered (Bluetooth-style) set pairing using a custom pairing object. As with a typical implementation of custom pairing, a *pairing-requested handler* is required to handle the pairing ceremony. In this case, the code example implements only the *confirm only* pairing ceremony. The new and interesting part is adding a *pairing-set-member-requested* handler.

The set-member-handler enables the platform to attempt pairing the passed-in device as a set. Without that handler, the protocol stack won't attempt to enumerate the members of a pairing set. The Bluetooth protocol discovers set members as part of finalizing the pairing, so the set handler is called sometime after its ceremony handler returns. In this flow, the set handler generally can expect to be called once after handling the ceremony with a fixed list of set members; meaning that the protocol discovers all of the endpoints that it can during pairing, and it won't discover more later.

This code example synchronously pairs all of the discovered set members with the same **BtSetPairing** routine used to pair the primary device/endpoint. Pairing them in parallel is also supported, and might be more efficient for your scenario. But for simplicity, that's not shown in the code example. Since we're also pairing the set members with a set handler, they can potentially recursively produce more set members to pair. But typically, the set handler for the discovered set members will probably only see *discovery completed* with the set member vector being empty.

> [!NOTE]
> This code example is without the context of a larger scenario or app; but an app would probably need to track what devices it's pairing, and the pairing results. That's in order for the app to get a sense of whether the whole set-pairing operation was successful or not.

> [!NOTE]
> These APIs are generally asynchronous. Pairing operations have their own worker threads, and handlers are called on different threads. Your code doesn't have to block as often as this code example does.

### Code example in C++/WinRT

```cppwinrt
void PairingTests::BtSetPairing(DeviceInformation device)
{
    DevicePairingKinds ceremonies = DevicePairingKinds::ConfirmOnly;
    auto customPairing = device.Pairing().Custom();
    event_revoker ceremonyEventToken = customPairing.PairingRequested(
        { this, &PairingTests::PairingRequestedHandler });
    event_revoker setEventToken = customPairing.PairingSetMembersRequested(
        { this, &PairingTests::PairingSetMembersRequestedHandler });

    DevicePairingResult result = customPairing.PairAsync(ceremonies).get();

    if (DevicePairingResultStatus::Paired == result.Status()) // Pairing worked.
    else // Handle pairing failure.
}

void PairingTests::PairingRequestedHandler(DeviceInformationCustomPairing const&,
    DevicePairingRequestedEventArgs const& args)
{
    switch (args.PairingKind())
    {
    case DevicePairingKinds::ConfirmOnly:
        args.Accept();
        break;
    default:
        // This code example doesn't implement other ceremonies.
        // The handler wouldn't be called for ceremonies that the app didn't register.
    }
}

void PairingTests::PairingSetMembersRequestedHandler(DeviceInformationCustomPairing
    const&, DevicePairingSetMembersRequestedEventArgs const& args)
{
    switch (args.Status())
    {
    case DevicePairingAddPairingSetMemberStatus::SetDiscoveryCompletedByProtocol:
        // This is the expected result if we started set pairing 
        // a Bluetooth device. Note: there still might be no set members.
        break;
    case DevicePairingAddPairingSetMemberStatus::SetDiscoveryPartiallyCompletedByProtocol:
        // The protocol enumerated some but not all set members.
        break;
    case DevicePairingAddPairingSetMemberStatus::SetDiscoveryNotAttemptedByProtocol:
        // Not expected for Bluetooth.
        // This constant implies that the protocol likely doesn't support set-pairing.
    default:
        // The other constants aren't expected in an auto-discovered Bluetooth set scenario.
        // Error handling can go here.
    }

    for (auto setMember : args.PairingSetMembers())
    {
        BtSetPairing(setMember);
    }
}
```

## Code example for an explicit set (IPP-style)

This code example implements explicit set pairing using a custom pairing object. As with a typical implementation of custom pairing, a *pairing-requested handler* is required to handle the pairing ceremony. In this case, the code example implements only the *confirm only* pairing ceremony. As with the Bluetooth code example, the new and interesting part is adding a *pairing-set-member-requested* handler. The set member handler enables the platform to attempt pairing devices as a set. 

As compared to the Bluetooth-style set-pairing scenario, this code example explicitly adds devices to the set. And the set handler implies something a bit different for the protocols related to pairing IPP printers. It implies that clients are handling device discovery over the various protocols, and the PnP state and print queues created as a result of pairing all of the set members should be synchronized.

To keep the implementation of the code example simple, it assumes that a vector of set member endpoints were discovered beforehand, and passed in as a parameter along with the primary device. For example, in a typical IPP scenario, endpoints are discovered in arbitrary order. So the primary device could have been discovered over WSD for instance; and then the vector would contain devices representing endpoints discovered over IPP, and eSCL. But any combination is possible and valid. It adds set members to the primary device's custom pairing object on the app's main thread, and then calls [PairAsync](/uwp/api/windows.devices.enumeration.deviceinformationcustompairing.pairasync).

> [!NOTE]
> In practice, set members might be added to a custom pairing object at any time on any thread. Operations over a protocol can take a long time, or might even be blocked until they time out, so overlapping them is useful. Consider taking advantage of the parallelism of the API to add and pair devices concurrently. This is possible even while you're still enumerating devices over the wire. The advantages of pairing them as a set still generally apply.

With this implementation, the primary set member will be paired concurrently with the set members. The set members are paired one at a time synchronously in the handler. But again, they can be paired in parallel for better efficiency.

Device node objects in PnP are often created as a result of pairing. In the case of IPP, device nodes are always created for each endpoint after pairing. This set-pairing API implicitly synchronizes device node creation between endpoints in the set. In this code example's flow, all device nodes will be synchronized because all set members are added before pairing starts. For more detail on how this API synchronizes device nodes in PnP, see the [General commentary](#general-commentary) section in this topic.

> [!NOTE]
> This code example is without the context of a larger scenario or app; but an app would probably need to track what devices it's pairing, and the pairing results. That's in order for the app to get a sense of whether the whole set-pairing operation was successful or not.

### Code example in C++/WinRT

```cppwinrt
void PairingTests::IppSetPairing(DeviceInformation device,
    std::vector<DeviceInformation> const& setMemberDevices)
{
    DevicePairingKinds ceremonies = DevicePairingKinds::ConfirmOnly;
    auto customPairing = device.Pairing().Custom();
    event_revoker ceremonyEventToken = customPairing.PairingRequested({ this,
                     &PairingTests::PairingRequestedHandler });
    event_revoker setEventToken = customPairing.PairingSetMembersRequested({ this,
                  &PairingTests::PairingSetMembersRequestedHandler });

    if (setMemberDevices)
    {
        for (auto setDevice : setMemberDevices)
        {
            customPairing.AddPairingSetMember(setDevice);
        }
    }

    DevicePairingResult result = customPairing.PairAsync(ceremonies).get();

    if (DevicePairingResultStatus::Paired == result.Status()) // Pairing worked.
    else // Handle pairing failure.
}

void PairingTests::PairingRequestedHandler(DeviceInformationCustomPairing const&,
    DevicePairingRequestedEventArgs const& args)
{
    switch (args.PairingKind())
    {
    case DevicePairingKinds::ConfirmOnly:
        args.Accept();
        break;
    }
}

void PairingTests::PairingSetMembersRequestedHandler(DeviceInformationCustomPairing const&,
    DevicePairingSetMembersRequestedEventArgs args)
{
    switch (args.Status())
    {
    case DevicePairingAddPairingSetMemberStatus::AddedToSet:
        // This is the expected result of adding a set member(s)
        // by calling the AddPairingSetMember method.
        break;
    case DevicePairingAddPairingSetMemberStatus::CouldNotBeAddedToSet:
        // Means we failed to add set member(s).
        break;
    case DevicePairingAddPairingSetMemberStatus::SetDiscoveryNotAttemptedByProtocol:
    default:
        // The other constants aren't expected in an explicit set scenario.
        // Error handling can go here.
    }

    for (auto setMember : args.PairingSetMembers())
    {
        IppSetPairing(setMember, nullptr);
    }
}
```

## General commentary

### Automatically-discovered (Bluetooth-style) set pairing

This API is necessary in order to accomplish Bluetooth-style set pairing where set members are discovered by the protocol after the primary endpoint is paired. A simple example might be a set of wireless earbuds. As the first earbud's pairing is finalized, the device informs the PC that there's a second device to pair as part of the set. The custom pairing API is extended to enable apps to handle set operations through the new *add set member status* handler.

### Explicit (IPP-style) set pairing

Similarly, you can also use this API to pair any group of association endpoint (AEP) devices as a set. With a custom pairing object, your app can add other endpoints to the pairing set at any time on any thread. That's by design because device discovery and pairing over a network can take a long time for each device, so we don't want to serialize these operations when we can avoid it.

It's especially useful to pair in sets when discovering devices over many protocols where the protocol and device stacks aren't easily coordinated. For example, a modern network printer will likely be discovered by Windows with three different network protocols, each of which produces association endpoints. In that case, pairing all three endpoints as a set is very useful for a couple of reasons: it avoids wasteful rediscovery over the wire, and it creates a single streamlined print queue.

Even if a network printer isn't paired as a set, the print spooler still attempts to create a single print queue per user regardless of whether it can be paired over several protocols. If a printer is initially paired over one protocol, then the operating system (OS) will attempt to rediscover it over the other supported protocols, and associate over all of them in order to avoid duplicate print queues. Typically, the OS can do that quickly and successfully and produce one streamlined print queue.

However, if the app has already discovered all the endpoints for the printer, then this rediscovery step is wasteful. And worse, it can add long delays before the printer is ready to use. Also, if protocols are too out-of-sync or delayed, then the spooler might have to create extra print queues for the same printer, which is confusing to end users.

Pairing all endpoints at once as a set avoids potentially slow rediscovery. It ensures that the PnP state is synchronized, and produces optimal streamlined print queues.

#### Device node synchronization

When the devices are paired as a set with this API, the resulting PnP device state is conveniently synchronized. The API doesn't constrain *when* an app can add set member; but there are limits on when the platform can synchronize device nodes. Device node synchronization blocks until all endpoints in the set have their pairing finalized. After that, all device nodes for all endpoints are created at once. You can add more set members to the set after that point, but no subsequent device node creation is blocked, but is instead created right away.

* Device node creation *is* synchronized when:
  * Set members are added before pairing starts.
  * Set members are added while at least one of the set members is not finalized.
* Device node creation is *not* synchronized:
  * Any time after all added set members have been finalized.

As a practical matter, the API doesn't allow an app to control when the ceremony finalization completes in order to influence this behavior on how device nodes are synchronized. The closest approximation would be when the app chooses to complete the ceremony. A pairing can't finalize until the app's ceremony handler returns; so that's the app's last chance to influence when all of the set members are finalized.
