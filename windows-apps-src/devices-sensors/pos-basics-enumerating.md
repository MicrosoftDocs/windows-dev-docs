---
title: Enumerating PointOfService devices
description: Learn four methods for using a device selector to query and enumerate the PointOfService devices available to your system.
ms.date: 10/08/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Enumerating Point of Service devices
In this section you will learn how to [define a device selector](./build-a-device-selector.md) that is used to query devices available to the system and use this selector to enumerate Point of Service devices using one of the following methods:

**Method 1:** [Use a device picker](#method-1-use-a-device-picker)
<br/>
Display a device picker UI and have the user choose a connected device. This method handles updating the list when devices are attached and removed, and is simpler and safer than other methods.

**Method 2:** [Get first available device](#method-2-get-first-available-device)<br />Use [GetDefaultAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.getdefaultasync) to access the first available device in a specific Point of Service device class.

**Method 3:** [Snapshot of devices](#method-3-snapshot-of-devices)<br />Enumerate a snapshot of Point of Service devices that are present on the system at a given point in time. This is useful when you want to build your own UI or need to enumerate devices without displaying a UI to the user. [FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) will hold back results until the entire enumeration is completed.

**Method 4:** [Enumerate and watch](#method-4-enumerate-and-watch)<br />[DeviceWatcher](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) is a more powerful and flexible enumeration model that allows you to enumerate devices that are currently present, and also receive notifications when devices are added or removed from the system.  This is useful when you want to maintain a current list of devices in the background for displaying in your UI rather than waiting for a snapshot to occur.

## Define a device selector
A device selector will enable you to limit the devices you are searching through when enumerating devices.  This will allow you to only get relevant results and reduce the time it takes to enumerate the desired devices.

You can use the **GetDeviceSelector** method for the type of device that you're looking for to get the device selector for that type. For example, using [PosPrinter.GetDeviceSelector](/uwp/api/windows.devices.pointofservice.posprinter.getdeviceselector#Windows_Devices_PointOfService_PosPrinter_GetDeviceSelector) will provide you with a selector to enumerate all [PosPrinters](/uwp/api/windows.devices.pointofservice.posprinter) attached to the system, including USB, network and Bluetooth POS printers.

```Csharp
using Windows.Devices.PointOfService;

string selector = POSPrinter.GetDeviceSelector();
```

The **GetDeviceSelector** methods for the different device types are:

* [BarcodeScanner.GetDeviceSelector](/uwp/api/windows.devices.pointofservice.barcodescanner.getdeviceselector)
* [CashDrawer.GetDeviceSelector](/uwp/api/windows.devices.pointofservice.cashdrawer.getdeviceselector)
* [LineDisplay.GetDeviceSelector](/uwp/api/windows.devices.pointofservice.linedisplay.getdeviceselector)
* [MagneticStripeReader.GetDeviceSelector](/uwp/api/windows.devices.pointofservice.magneticstripereader.getdeviceselector)
* [PosPrinter.GetDeviceSelector](/uwp/api/windows.devices.pointofservice.posprinter.getdeviceselector)

Using a **GetDeviceSelector** method that takes a [PosConnectionTypes](/uwp/api/windows.devices.pointofservice.posconnectiontypes) value as a parameter, you can restrict your selector to enumerate local, network, or Bluetooth-attached POS devices, reducing the time it takes for the query to complete.  The sample below shows a use of this method to define a selector that supports only locally attached POS printers.

 ```Csharp
using Windows.Devices.PointOfService;

string selector = POSPrinter.GetDeviceSelector(PosConnectionTypes.Local);
```

> [!TIP]
> See [Build a device selector](./build-a-device-selector.md) for building more advanced selector strings.

## Method 1: Use a device picker

The [DevicePicker](/uwp/api/windows.devices.enumeration.devicepicker) class allows you to display a picker flyout that contains a list of devices for the user to choose from. You can use the [Filter](/uwp/api/windows.devices.enumeration.devicepicker.filter) property to choose which types of devices to show in the picker. This property is of type [DevicePickerFilter](/uwp/api/windows.devices.enumeration.devicepickerfilter). You can add device types to the filter using the [SupportedDeviceClasses](/uwp/api/windows.devices.enumeration.devicepickerfilter.supporteddeviceclasses) or [SupportedDeviceSelectors](/uwp/api/windows.devices.enumeration.devicepickerfilter.supporteddeviceselectors) property.

When you are ready to show the device picker, you can call the [PickSingleDeviceAsync](/uwp/api/windows.devices.enumeration.devicepicker.picksingledeviceasync) method, which will show the picker UI and return the selected device. You'll need to specify a [Rect](/uwp/api/windows.foundation.rect) that will determine where the flyout appears. This method will return a [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) object, so to use it with the Point of Service APIs, you'll need to use the **FromIdAsync** method for the particular device class that you want. You pass the [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) property as the method's *deviceId* parameter, and get an instance of the device class as the return value.

The following code snippet creates a **DevicePicker**, adds a barcode scanner filter to it, has the user pick a device, and then creates a **BarcodeScanner** object based on the device ID:

```cs
private async Task<BarcodeScanner> GetBarcodeScanner()
{
    DevicePicker devicePicker = new DevicePicker();
    devicePicker.Filter.SupportedDeviceSelectors.Add(BarcodeScanner.GetDeviceSelector());
    Rect rect = new Rect();
    DeviceInformation deviceInformation = await devicePicker.PickSingleDeviceAsync(rect);
    BarcodeScanner barcodeScanner = await BarcodeScanner.FromIdAsync(deviceInformation.Id);
    return barcodeScanner;
}
```

## Method 2: Get first available device

The simplest way to get a Point of Service device is to use **GetDefaultAsync** to get the first available device within a Point of Service device class. 

The sample below illustrates the use of [GetDefaultAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.getdefaultasync#Windows_Devices_PointOfService_BarcodeScanner_GetDefaultAsync) for [BarcodeScanner](/uwp/api/windows.devices.pointofservice.barcodescanner). The coding pattern is similar for all Point of Service device classes.

```Csharp
using Windows.Devices.PointOfService;

BarcodeScanner barcodeScanner = await BarcodeScanner.GetDefaultAsync();
```

> [!CAUTION]
> **GetDefaultAsync** must be used with care as it may return a different device from one session to the next. Many events can influence this enumeration resulting in a different first available device, including: 
> - Change in cameras attached to your computer 
> - Change in Point of Service devices attached to your computer
> - Change in network-attached Point of Service devices available on your network
> - Change in Bluetooth Point of Service devices within range of your computer 
> - Changes to the Point of Service configuration 
> - Installation of drivers or OPOS service objects
> - Installation of Point of Service extensions
> - Update to Windows operating system

## Method 3: Snapshot of devices

In some scenarios you may want to build your own UI or need to enumerate devices without displaying a UI to the user.  In these situations, you could enumerate a snapshot of devices that are currently connected or paired with the system using [DeviceInformation.FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync).  This method will hold back any results until the entire enumeration is completed.

> [!TIP]
> It is recommended to use the **GetDeviceSelector** method with the **PosConnectionTypes** parameter when using **FindAllAsync** to limit your query to the connection type desired.  Network and Bluetooth connections can delay the results as their enumerations must complete before **FindAllAsync** results are returned.

> [!CAUTION] 
> **FindAllAsync** returns an array of devices.  The order of this array can change from session to session, therefore it is not recommended to rely on a specific order by using a hardcoded index into the array.  Use [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) properties to filter your results or provide a UI for the user to choose from.

This sample uses the selector defined above to take a snapshot of devices using **FindAllAsync** then enumerates through each of the items returned by the collection and writes the device name and ID to the debug output. 

```Csharp
using Windows.Devices.Enumeration;

DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);

foreach (DeviceInformation devInfo in deviceCollection)
{
    Debug.WriteLine("{0} {1}", devInfo.Name, devInfo.Id);
}
```

> [!TIP] 
> When working with the [Windows.Devices.Enumeration](/uwp/api/Windows.Devices.Enumeration) APIs, you will frequently need to use [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) objects to obtain information about a specific device. For example, the [DeviceInformation.ID](/uwp/api/windows.devices.enumeration.deviceinformation.id) property can be used to recover and reuse the same device if it is available in a future session and the [DeviceInformation.Name](/uwp/api/windows.devices.enumeration.deviceinformation.name) property can be used for display purposes in your app.  See the [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) reference page for information about additional properties available.

## Method 4: Enumerate and watch

A more powerful and flexible method of enumerating devices is creating a [DeviceWatcher](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher).  A device watcher enumerates devices dynamically, so that the application receives notifications if devices are added, removed, or changed  after the initial enumeration is complete.  A **DeviceWatcher** will allow you to detect when a network-connected device comes online, a Bluetooth device is in range, as well as if a locally connected device is unplugged so that you can take the appropriate action within your application.

This sample uses the selector defined above to create a **DeviceWatcher** as well as defines event handlers for the [Added](/uwp/api/windows.devices.enumeration.devicewatcher.added), [Removed](/uwp/api/windows.devices.enumeration.devicewatcher.removed), and [Updated](/uwp/api/windows.devices.enumeration.devicewatcher.updated) notifications. You will need to fill in the details of the actions that you wish to take upon each notification.

```Csharp
using Windows.Devices.Enumeration;

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

> [!TIP]
> See [Enumerate and watch devices]( ./enumerate-devices.md#enumerate-and-watch-devices) for more details on the use of a **DeviceWatcher**.

## See also
* [Getting started with Point of Service](pos-basics.md)
* [DeviceInformation Class](/uwp/api/windows.devices.enumeration.deviceinformation)
* [PosPrinter Class](/uwp/api/windows.devices.pointofservice.posprinter)
* [PosConnectionTypes Enum](/uwp/api/windows.devices.pointofservice.posconnectiontypes)
* [BarcodeScanner Class](/uwp/api/windows.devices.pointofservice.barcodescanner)
* [DeviceWatcher Class](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher)

[!INCLUDE [feedback](./includes/pos-feedback.md)]