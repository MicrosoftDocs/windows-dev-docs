---
author: muhsinking
title: Getting started with point of service
description: This article contains information about getting started with the point of service UWP APIs.
ms.author: mukin
ms.date: 08/2/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, point of service, pos
---

# Getting started with point of service

Point of service, point of sale, or POS devices are computer peripherals used to facilitate retail transactions. Examples of POS devices include electronic cash registers, barcode scanners, magnetic stripe readers, and receipt printers.

Here you’ll learn the basics of interfacing with POS devices by using the Universal Windows Platform (UWP) POS APIs. We’ll cover device enumeration, checking device capabilities, claiming devices, and device sharing. We use a barcode scanner device as an example, but almost all the guidance here applies to any UWP-compatible POS device. (For a list of supported devices, see [POS Device Support](pos-device-support.md)).

## Finding and connecting to POS peripherals

Before a POS device can be used by an app, it must be paired with the PC where the app is running. There are several ways to connect to POS devices, either programmatically or through the Settings app.

### Connecting to devices by using the Settings app
When you plug a POS device like a barcode scanner into a PC, it shows up just like any other device. You can find it in the **Devices > Bluetooth & other devices** section of the Settings app. There you can pair with a POS device by selecting **Add Bluetooth or other device**.

Some POS devices may not appear in the Settings app until they are programmatically enumerated by using the POS APIs.

### Getting a single POS device with GetDefaultAsync
In a simple use case, you may have just one POS peripheral connected to the PC where the app is running, and want to set it up as quickly as possible. To do that, retrieve the “default” device with the **GetDefaultAsync** method as shown here.

```Csharp
using Windows.Devices.PointOfService;

BarcodeScanner barcodeScanner = await BarcodeScanner.GetDefaultAsync();
```

If the default device is found, the device object retrieved is ready to be claimed. “Claiming” a device gives an application exclusive access to it, preventing conflicting commands from multiple processes.

> [!NOTE] 
> If more than one POS device is connected to the PC, **GetDefaultAsync** returns the first device it finds. For this reason, use **FindAllAsync** unless you’re sure that only one POS device is visible to the application.

### Enumerating a collection of devices with FindAllAsync

When connected to more than one device, you must enumerate the collection of **PointOfService** device objects to find the one you want to claim. For example, the following code creates a collection of all the barcode scanners currently connected, and then searches the collection for a scanner with a specific name.

```Csharp
using Windows.Devices.Enumeration;
using Windows.Devices.PointOfService;

string selector = BarcodeScanner.GetDeviceSelector();       
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);

foreach (DeviceInformation devInfo in deviceCollection)
{
    Debug.WriteLine("{0} {1}", devInfo.Name, devInfo.Id);
    if (devInfo.Name.Contains("1200G"))
    {
        Debug.WriteLine(" Found one");
    }
}
```

### Scoping the device selection
When connecting to a device, you may want to limit your search to a subset of POS peripherals that your app has access to. Using the **GetDeviceSelector** method, you can scope the selection to retrieve devices connected only by a certain method (Bluetooth, USB, etc.). You can create a selector that searches for devices over **Bluetooth**, **IP**, **Local**, or **All connection types**. This can be useful, as wireless device discovery takes a long time compared to local (wired) discovery. You can ensure a deterministic wait time for local device connection by limiting **FindAllAsync** to **Local** connection types. For example, this code retrieves all barcode scanners accessible via a local connection. 

```Csharp
string selector = BarcodeScanner.GetDeviceSelector(PosConnectionTypes.Local);
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

### Reacting to device connection changes with DeviceWatcher

As your app runs, sometimes devices will be disconnected or updated, or new devices will need to be added. You can use the **DeviceWatcher** class to access device-related events, so your app can respond accordingly. Here’s example of how to use **DeviceWatcher**, with method stubs to be called if a device is added, removed, or updated.

```Csharp
DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(selector);
deviceWatcher.Added += DeviceWatcher_Added;
deviceWatcher.Removed += DeviceWatcher_Removed;
deviceWatcher.Updated += DeviceWatcher_Updated;

void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    // TODO: Add the DeviceInformation object to your collection
}

void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
{
    // TODO: Remove the item in your collection associated with DeviceInformationUpdate
}

void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
{
    // TODO: Update your collection with information from DeviceInformationUpdate
}
```

## Checking the capabilities of a POS device
Even within a device class, such as barcode scanners, the attributes of each device may vary considerably between models. If your app requires a specific device attribute, you may need to inspect each connected device object to determine whether the attribute is supported. For example, perhaps your business requires that labels be created using a specific barcode printing pattern. Here’s how you could check to see whether a connected barcode scanner supports a particular symbology. 

> [!NOTE]
> A symbology is the language mapping that a barcode uses to encode messages.

```Csharp
try
{
    BarcodeScanner barcodeScanner = await BarcodeScanner.FromIdAsync(deviceId);
    if (await barcodeScanner.IsSymbologySupportedAsync(BarcodeSymbologies.Code32))
    {
        Debug.WriteLine("Has symbology");
    }
}
catch (Exception ex)
{
    Debug.WriteLine("FromIdAsync() - " + ex.Message);
}
```

### Using the Device.Capabilities class
The **Device.Capabilities** class is an attribute of all POS device classes and can be used to get general information about each device. For example, this example determines whether a device supports statistics reporting and, if it does, retrieves statistics for any types supported.

```Csharp
try
{
    if (barcodeScanner.Capabilities.IsStatisticsReportingSupported)
    {
        Debug.WriteLine("Statistics reporting is supported");

        string[] statTypes = new string[] {""};
        IBuffer ibuffer = await barcodeScanner.RetrieveStatisticsAsync(statTypes);
    }
}
catch (Exception ex)
{
    Debug.WriteLine("EX: RetrieveStatisticsAsync() - " + ex.Message);
}
```

## Claiming a POS device
Before you can use a POS device for active input or output, you must claim it, granting the application exclusive access to many of its functions. This code shows how to claim a barcode scanner device, after you’ve found the device by using one of the methods described earlier.

```Csharp
try
{
    claimedBarcodeScanner = await barcodeScanner.ClaimScannerAsync();
}
catch (Exception ex)
{
    Debug.WriteLine("EX: ClaimScannerAsync() - " + ex.Message);
}
```

### Retaining the device
When using a POS device over a network or Bluetooth connection, you may wish to share the device with other apps on the network. (For more info about this, see Sharing Devices[link].) In other cases, you may want to hold on to the device for prolonged use. This example shows how to retain a claimed barcode scanner after another app has requested that the device be released.

```Csharp
claimedBarcodeScanner.ReleaseDeviceRequested += claimedBarcodeScanner_ReleaseDeviceRequested;

void claimedBarcodeScanner_ReleaseDeviceRequested(object sender, ClaimedBarcodeScanner e)
{
    e.RetainDevice();  // Retain exclusive access to the device
}
```

## Input and output

After you’ve claimed a device, you’re almost ready to use it. To receive input from the device, you must set up and enable a delegate to receive data. In the example below, we claim a barcode scanner device, set its decode property, and then call **EnableAsync** to enable decoded input from the device. This process varies between device classes, so for guidance about how to set up a delegate for non-barcode devices, refer to the relevant [UWP app sample](https://github.com/Microsoft/Windows-universal-samples#devices-and-sensors).

```Csharp
try
{
    claimedBarcodeScanner = await barcodeScanner.ClaimScannerAsync();
    if (claimedBarcodeScanner != null)
    {
        claimedBarcodeScanner.DataReceived += claimedBarcodeScanner_DataReceived;
        claimedBarcodeScanner.IsDecodeDataEnabled = true;
        await claimedBarcodeScanner.EnableAsync();
    }
}
catch (Exception ex)
{
    Debug.WriteLine("EX: ClaimScannerAsync() - " + ex.Message);
}


void claimedBarcodeScanner_DataReceived(ClaimedBarcodeScanner sender, BarcodeScannerDataReceivedEventArgs args)
{
    string symbologyName = BarcodeSymbologies.GetName(args.Report.ScanDataType);
    var scanDataLabelReader = DataReader.FromBuffer(args.Report.ScanDataLabel);
    string barcode = scanDataLabelReader.ReadString(args.Report.ScanDataLabel.Length);
}
```

## Sharing a device between apps

POS devices are often used in cases where more than one app will need to access them in a short period.  A device can be shared when connected to multiple apps locally (USB or other wired connection), or through a Bluetooth or IP network. Depending on the needs of each app, one process may need to dispose of its claim on the device. This code disposes of our claimed barcode scanner device, allowing other apps to claim and use it.

```Csharp
if (claimedBarcodeScanner != null)
{
    claimedBarcodeScanner.Dispose();
    claimedBarcodeScanner = null;
}
```

> [!NOTE]
> Both the claimed and unclaimed POS device classes implement the [IClosable interface](https://docs.microsoft.com/en-us/uwp/api/windows.foundation.iclosable). If a device is connected to an app via network or bluetooth, both the claimed and unclaimed objects must disposed of before another app can connect.

## See also
+ [Barcode scanner sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)
+ [Cash drawer sample]( https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CashDrawer)
+ [Line display sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LineDisplay)
+ [Magnetic stripe reader sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MagneticStripeReader)
+ [POS printer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/PosPrinter)

