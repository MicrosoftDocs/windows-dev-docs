---
title: Bluetooth LE Advertisements
description: This topic provides an overview of Bluetooth Low Energy (LE) Advertisement beacons for Universal Windows Platform (UWP) apps.
ms.date: 05/04/2023
ms.topic: article


ms.localizationpriority: medium
---

# Bluetooth LE Advertisements

This topic provides an overview of Bluetooth Low Energy (LE) Advertisement beacons for Universal Windows Platform (UWP) apps.  

> [!IMPORTANT]
> You must declare the "bluetooth" capability in *Package.appxmanifest* to use this feature.
>
> `<Capabilities> <DeviceCapability Name="bluetooth" /> </Capabilities>`

**Important APIs**

- [**Windows.Devices.Bluetooth.Advertisement**](/uwp/api/windows.devices.bluetooth.advertisement)

## Supported features

There are two main features supported by the LE Advertisement APIs:

- [Advertisement Watcher](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementwatcher): listen for nearby beacons and filter them out based on payload or proximity.  
- [Advertisement Publisher](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementpublisher): define a payload for Windows to advertise on a developers behalf.  

## Example

For a fully functional example of Bluetooth LE Advertisements, see the [Bluetooth Advertisement Sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/BluetoothAdvertisement) on Github.

### Basic Setup

To use basic Bluetooth LE functionality in a Universal Windows Platform app, you must check the Bluetooth capability in the Package.appxmanifest.

1. Open Package.appxmanifest
2. Go to the Capabilities tab
3. Find Bluetooth in the list on the left and check the box next to it.

### Publishing Advertisements

Bluetooth LE Advertisements allow your device to constantly beacon out a specific payload, called an advertisement. This advertisement can be seen by any nearby Bluetooth LE capable device, if they are set up to listen for this specific advertisement.

> [!NOTE]
> For user privacy, the lifespan of your advertisement is tied to that of your app. You can create a BluetoothLEAdvertisementPublisher and call Start in a background task for advertisement in the background. For more information about background tasks, see [Launching, resuming, and background tasks](../launch-resume/index.md).

There are many different ways to add data to an Advertisement. This example shows a common way to create a company-specific advertisement.

First, create the advertisement publisher that controls whether or not the device is beaconing out a specific advertisement.

```csharp
BluetoothLEAdvertisementPublisher publisher = new BluetoothLEAdvertisementPublisher();
```

Second, create a custom data section. This example uses an unassigned **CompanyId** value *0xFFFE* and adds the text *Hello World* to the advertisement.

```csharp
// Add custom data to the advertisement
var manufacturerData = new BluetoothLEManufacturerData();
manufacturerData.CompanyId = 0xFFFE;

var writer = new DataWriter();
writer.WriteString("Hello World");

// Make sure that the buffer length can fit within an advertisement payload (~20 bytes). 
// Otherwise you will get an exception.
manufacturerData.Data = writer.DetachBuffer();

// Add the manufacturer data to the advertisement publisher:
publisher.Advertisement.ManufacturerData.Add(manufacturerData);
```

Now that the publisher has been created and setup, you can call **Start** to begin advertising.

```csharp
publisher.Start();
```

### Watching for Advertisements

The following code demonstrates how to create a Bluetooth LE Advertisement watcher, set a callback, and start watching for all LE advertisements.

```csharp
BluetoothLEAdvertisementWatcher watcher = new BluetoothLEAdvertisementWatcher();
watcher.Received += OnAdvertisementReceived;
watcher.Start();
```	

```csharp
private async void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
{
    // Do whatever you want with the advertisement
}
```

#### Active Scanning
To receive scan response advertisements as well, set the following after creating the watcher. Note that this will cause greater power drain and is not available while in background modes.

```csharp
watcher.ScanningMode = BluetoothLEScanningMode.Active;
```

#### Watching for a Specific Advertisement Pattern

Sometimes you want to listen for a specific advertisement. In this case, listen for an advertisement containing a payload with a made up company (identified as 0xFFFE) and containing the string *Hello World* in the advertisement. This can be paired with the Basic Publishing example to have one Windows machine advertising and another watching. Be sure to set this advertisement filter before you start the watcher!

```csharp
var manufacturerData = new BluetoothLEManufacturerData();
manufacturerData.CompanyId = 0xFFFE;

// Make sure that the buffer length can fit within an advertisement payload (~20 bytes). 
// Otherwise you will get an exception.
var writer = new DataWriter();
writer.WriteString("Hello World");
manufacturerData.Data = writer.DetachBuffer();

watcher.AdvertisementFilter.Advertisement.ManufacturerData.Add(manufacturerData);
```

#### Watching for a Nearby Advertisement

Sometimes you only want to trigger your watcher when the device advertising has come in range. You can define your own range, just note that values will be clipped to between 0 and -128. 

```csharp
// Set the in-range threshold to -70dBm. This means advertisements with RSSI >= -70dBm 
// will start to be considered "in-range" (callbacks will start in this range).
watcher.SignalStrengthFilter.InRangeThresholdInDBm = -70;

// Set the out-of-range threshold to -75dBm (give some buffer). Used in conjunction 
// with OutOfRangeTimeout to determine when an advertisement is no longer 
// considered "in-range".
watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -75;

// Set the out-of-range timeout to be 2 seconds. Used in conjunction with 
// OutOfRangeThresholdInDBm to determine when an advertisement is no longer 
// considered "in-range"
watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(2000);
```

#### Gauging Distance

When your Bluetooth LE Watcher's callback is triggered, the eventArgs include an RSSI value telling you the received signal strength (how strong the Bluetooth signal is).

```csharp
private async void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
{
   // The received signal strength indicator (RSSI)
   Int16 rssi = eventArgs.RawSignalStrengthInDBm;
}
```

This can be roughly translated into distance, but should not be used to measure true distances as each individual radio is different. Different environmental factors can make distance difficult to gauge (such as walls, cases around the radio, or even air humidity).

An alternative to judging pure distance is to define "buckets". Radios tend to report 0 to -50 DBm when they are very close, -50 to -90 when they are a medium distance away, and below -90 when they are far away. Trial and error is best to determine what you want these buckets to be for your app.