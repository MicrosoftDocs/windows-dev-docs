---
title: Bluetooth GATT Client
description: This article demonstrates how to use the Bluetooth Generic Attribute (GATT) Client APIs for Universal Windows Platform (UWP) apps.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Bluetooth GATT Client

This article demonstrates how to use the Bluetooth Generic Attribute (GATT) Client APIs for Universal Windows Platform (UWP) apps.

> [!IMPORTANT]
> You must declare the "bluetooth" capability in *Package.appxmanifest*.
>
> `<Capabilities> <DeviceCapability Name="bluetooth" /> </Capabilities>`

> **Important APIs**
>
> - [**Windows.Devices.Bluetooth**](/uwp/api/Windows.Devices.Bluetooth)
> - [**Windows.Devices.Bluetooth.GenericAttributeProfile**](/uwp/api/Windows.Devices.Bluetooth.GenericAttributeProfile)

## Overview

Developers can use the APIs in the [**Windows.Devices.Bluetooth.GenericAttributeProfile**](/uwp/api/Windows.Devices.Bluetooth.GenericAttributeProfile) namespace to access Bluetooth LE devices. Bluetooth LE devices expose their functionality through a collection of:

- Services
- Characteristics
- Descriptors

Services define the functional contract of the LE device and contain a collection of characteristics that define the service. Those characteristics, in turn, contain descriptors that describe the characteristics. These 3 terms are generically known as the attributes of a device.

The Bluetooth LE GATT APIs expose objects and functions, rather than access to the raw transport. The GATT APIs also enable developers to work with Bluetooth LE devices with the ability to perform the following tasks:

- Perform attribute discovery
- Read and Write attribute values
- Register a callback for Characteristic ValueChanged event

To create a useful implementation a developer must have prior knowledge of the GATT services and characteristics the application intends to consume and to process the specific characteristic values such that the binary data provided by the API is transformed into useful data before being presented to the user. The Bluetooth GATT APIs expose only the basic primitives required to communicate with a Bluetooth LE device. To interpret the data, an application profile must be defined, either by a Bluetooth SIG standard profile, or a custom profile implemented by a device vendor. A profile creates a binding contract between the application and the device, as to what the exchanged data represents and how to interpret it.

For convenience the Bluetooth SIG maintains a [list of public profiles](https://www.bluetooth.com/specifications/adopted-specifications#gattspec) available.

## Examples

For a complete sample, see [Bluetooth Low Energy sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BluetoothLE).

### Query for nearby devices

There are two main methods to query for nearby devices:

- [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) in [Windows.Devices.Enumeration](/uwp/api/windows.devices.enumeration)
- [BluetoothLEAdvertisementWatcher](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementwatcher) in [Windows.Devices.Bluetooth.Advertisement](/uwp/api/windows.devices.bluetooth.advertisement)

The 2nd method is discussed at length in the [Advertisement](ble-beacon.md) documentation so it won't be discussed much here but the basic idea is to find the Bluetooth address of nearby devices that satisfy the particular [Advertisement Filter](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementwatcher.advertisementfilter). Once you have the address, you can call [BluetoothLEDevice.FromBluetoothAddressAsync](/uwp/api/windows.devices.bluetooth.bluetoothledevice.frombluetoothaddressasync) to get a reference to the device.

Now, back to the DeviceWatcher method. A Bluetooth LE device is just like any other device in Windows and can be queried using the [Enumeration APIs](/uwp/api/Windows.Devices.Enumeration). Use the [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) class and pass a query string specifying the devices to look for:

```csharp
// Query for extra properties you want returned
string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

DeviceWatcher deviceWatcher =
            DeviceInformation.CreateWatcher(
                    BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                    requestedProperties,
                    DeviceInformationKind.AssociationEndpoint);

// Register event handlers before starting the watcher.
// Added, Updated and Removed are required to get all nearby devices
deviceWatcher.Added += DeviceWatcher_Added;
deviceWatcher.Updated += DeviceWatcher_Updated;
deviceWatcher.Removed += DeviceWatcher_Removed;

// EnumerationCompleted and Stopped are optional to implement.
deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
deviceWatcher.Stopped += DeviceWatcher_Stopped;

// Start the watcher.
deviceWatcher.Start();
```

Once you've started the DeviceWatcher, you will receive [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) for each device that satisfies the query in the handler for the [Added](/uwp/api/windows.devices.enumeration.devicewatcher.added) event for the devices in question. For a more detailed look at DeviceWatcher see the complete sample [on Github](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/DeviceEnumerationAndPairing).

### Connecting to the device

Once a desired device is discovered, use the [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) to get the Bluetooth LE Device object for the device in question:

```csharp
async void ConnectDevice(DeviceInformation deviceInfo)
{
    // Note: BluetoothLEDevice.FromIdAsync must be called from a UI thread because it may prompt for consent.
    BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);
    // ...
}
```

On the other hand, disposing of all references to a BluetoothLEDevice object for a device (and if no other app on the system has a reference to the device) will trigger an automatic disconnect after a small timeout period.

```csharp
bluetoothLeDevice.Dispose();
```

If the app needs to access the device again, simply re-creating the device object and accessing a characteristic (discussed in the next section) will trigger the OS to re-connect when necessary. If the device is nearby, you'll get access to the device otherwise it will return w/ a DeviceUnreachable error.  

> [!NOTE]
> Creating a [BluetoothLEDevice](/uwp/api/windows.devices.bluetooth.bluetoothledevice) object by calling this method alone doesn't (necessarily) initiate a connection. To initiate a connection, set [GattSession.MaintainConnection](/uwp/api/windows.devices.bluetooth.genericattributeprofile.gattsession.maintainconnection) to `true`, or call an uncached service discovery method on **BluetoothLEDevice**, or perform a read/write operation against the device.
>
> - If **GattSession.MaintainConnection** is set to true, then the system waits indefinitely for a connection, and it will connect when the device is available. There's nothing for your application to wait on, since **GattSession.MaintainConnection** is a property.
> - For service discovery and read/write operations in GATT, the system waits a finite but variable time. Anything from instantaneous to a matter of minutes. Factors include the traffic on the stack, and how queued up the request is. If there are no other pending request, and the remote device is unreachable, then the system will wait for seven (7) seconds before it times out. If there are other pending requests, then each of the requests in the queue can take seven (7) seconds to process, so the further yours is toward the back of the queue, the longer you'll wait.
>
> Currently, you can't cancel the connection process.

### Enumerating supported services and characteristics

Now that you have a BluetoothLEDevice object, the next step is to discover what data the device exposes. The first step to do this is to query for services:

```csharp
GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

if (result.Status == GattCommunicationStatus.Success)
{
    var services = result.Services;
    // ...
}
```

Once the service of interest has been identified, the next step is to query for characteristics.

```csharp
GattCharacteristicsResult result = await service.GetCharacteristicsAsync();

if (result.Status == GattCommunicationStatus.Success)
{
    var characteristics = result.Characteristics;
    // ...
}
```

The OS returns a ReadOnly list of [GattCharacteristic](/uwp/api/windows.devices.bluetooth.genericattributeprofile.gattcharacteristic) objects that you can then perform operations on.

### Perform Read/Write operations on a characteristic

The characteristic is the fundamental unit of GATT based communication. It contains a value that represents a distinct piece of data on the device. For example, the battery level characteristic has a value that represents the battery level of the device.

Read the characteristic properties to determine what operations are supported:

```csharp
GattCharacteristicProperties properties = characteristic.CharacteristicProperties

if(properties.HasFlag(GattCharacteristicProperties.Read))
{
    // This characteristic supports reading from it.
}
if(properties.HasFlag(GattCharacteristicProperties.Write))
{
    // This characteristic supports writing to it.
}
if(properties.HasFlag(GattCharacteristicProperties.Notify))
{
    // This characteristic supports subscribing to notifications.
}
```

If read is supported, you can read the value:

```csharp
GattReadResult result = await selectedCharacteristic.ReadValueAsync();
if (result.Status == GattCommunicationStatus.Success)
{
    var reader = DataReader.FromBuffer(result.Value);
    byte[] input = new byte[reader.UnconsumedBufferLength];
    reader.ReadBytes(input);
    // Utilize the data as needed
}
```

Writing to a characteristic follows a similar pattern:

```csharp
var writer = new DataWriter();
// WriteByte used for simplicity. Other common functions - WriteInt16 and WriteSingle
writer.WriteByte(0x01);

GattCommunicationStatus result = await selectedCharacteristic.WriteValueAsync(writer.DetachBuffer());
if (result == GattCommunicationStatus.Success)
{
    // Successfully wrote to device
}
```

> [!TIP]
> [DataReader](/uwp/api/windows.storage.streams.datareader) and [DataWriter](/uwp/api/windows.storage.streams.datawriter) are indispensable when working with the raw buffers you get from many of the Bluetooth APIs.

### Subscribing for notifications

Make sure the characteristic supports either Indicate or Notify (check the characteristic properties to make sure).

Indicate is considered more reliable because each value changed event is coupled with an acknowledgement from the client device. Notify is more prevalent because most GATT transactions would rather conserve power rather than be extremely reliable. In any case, all of that is handled at the controller layer so the app does not get involved. We'll collectively refer to them as simply "notifications".

There are two things to take care of before getting notifications:

- Write to Client Characteristic Configuration Descriptor (CCCD)
- Handle the Characteristic.ValueChanged event

Writing to the CCCD tells the Server device that this client wants to know each time that particular characteristic value changes. To do this:

```csharp
GattCommunicationStatus status = await selectedCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                        GattClientCharacteristicConfigurationDescriptorValue.Notify);
if(status == GattCommunicationStatus.Success)
{
    // Server has been informed of clients interest.
}
```

Now, the GattCharacteristic's ValueChanged event will get called each time the value gets changed on the remote device. All that's left is to implement the handler:

```csharp
characteristic.ValueChanged += Characteristic_ValueChanged;

...

void Characteristic_ValueChanged(GattCharacteristic sender,
                                    GattValueChangedEventArgs args)
{
    // An Indicate or Notify reported that the value has changed.
    var reader = DataReader.FromBuffer(args.CharacteristicValue)
    // Parse the data however required.
}
```

