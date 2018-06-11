---
author: TerryWarwick
title: Enumerating PointOfService devices
description: Learn how to enumerate PointOfService devices
ms.author: jken
ms.date: 06/8/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Enumerating Point of Service devices
In this section you will learn how to [**define a device selector**](https://docs.microsoft.com/windows/uwp/devices-sensors/build-a-device-selector) that is used to query devices available to the system and use this selector to enumerate Point of Service devices using one of the following methods:

**Method 1:** [**Get first available device**](#Method-1:-get-first-available-device)<br />In this section you will learn how to use **GetDefaultAsync** to access the first available device in a specific PointOfService device class.

**Method 2:** [**Snapshot of devices**](#Method-2:-Snapshot-of-devices)<br />In this section you will learn how to enumerate a snapshot of PointOfService devices that are present on the system at a given point in time. This is useful when you want to build your own UI or need to enumerate devices without displaying a UI to the user. FindAllAsync will hold back results until the entire enumeration is completed.

**Method 3:** [**Enumerate and watch**](#Method-3:-Enumerate-and-watch)<br />In this section you will learn about a more powerful and flexible enumeration model that allows you to enumerate devices that are currently present, and also receive notifications when devices are added or removed from the system.  This is useful when you want to maintain a current list of devices in the background for displaying in your UI rather than waiting for a snapshot to occur.
 

---
## Define a device selector
A device selector will enable you to limit the devices you are searching through when enumerating devices.  This will enable you to only get relevant results and reduce the time it takes to enumerate the desired devices.  

Using [**GetDeviceSelector**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.posprinter.getdeviceselector#Windows_Devices_PointOfService_PosPrinter_GetDeviceSelector) will provide you with a selector to enumerate all POSPrinters attached to the system, including USB, network and Bluetooth POSPrinters.

```Csharp
using Windows.Devices.PointOfService;

string selector = POSPrinter.GetDeviceSelector();   

```

Using the [**GetDeviceSelector**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.posprinter.getdeviceselector#Windows_Devices_PointOfService_PosPrinter_GetDeviceSelector_Windows_Devices_PointOfService_PosConnectionTypes_) method that takes [**PosConnectionTypes**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.posconnectiontypes) as a parameter, you can restrict your selector to enumerate local, network or Bluetooth attached POSPrinters reducing the time it takes for the query to complete.  The sample below shows the use of this method to define a selctor that support only locally attached POSPrinters.

 ```Csharp
using Windows.Devices.PointOfService;

string selector = POSPrinter.GetDeviceSelector(PosConnectionTypes.Local);   

```
> [!TIP]
> See [**Build a device selector**](https://docs.microsoft.com/windows/uwp/devices-sensors/build-a-device-selector) for building more advanced selector strings.

---

## Method 1: Get first available device

The simplest way to get a PointOfService device is to use **GetDefaultAsync** to get the first available device within a PointOfService device class. 

The sample below illustrates the use of [**GetDefaultAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.getdefaultasync#Windows_Devices_PointOfService_BarcodeScanner_GetDefaultAsync) for BarcodeScanner. The coding pattern is similar for all PointOfService device classes.

```Csharp

using Windows.Devices.PointOfService;

BarcodeScanner barcodeScanner = await BarcodeScanner.GetDefaultAsync();

```

> [!CAUTION]
> GetDefaultAsync must be used with care as it may return a different device from one session to the next. Many events can influence this enumeration resulting in a different first available device, including: 
> - Change in cameras attached to your computer 
> - Change in PointOfService devices attached to your computer
> - Change in network attached PointOfService devices available on your network
> - Change in Bluetooth PointOfService devices within range of your computer 
> - Changes to the PointOfService configuration 
> - Installation of drivers or OPOS service objects
> - Installation of PointOfService extensions
> - Update to Windows operating system

---

## Method 2: Snapshot of devices

In some scenarios you may want to build your own UI or need to enumerate devices without displaying a UI to the user.  In these situations, you could enumerate a snapshot of devices that are currently connected or paired with the system using [**DeviceInformation.FindAllAsync**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformation.findallasync).  This method will hold back any results until the entire enumeration is completed.

> [!TIP]
> It is recommended to use GetDeviceSelector method with the PosConnectionTypes parameter when using FindAllAsync to limit your query to the connection type desired.  Network and Bluetooth connections can delay the results as their enumerations must complete before FindAllAsync results are returned.

>[!CAUTION] 
>FindAllAsync returns an array of devices.  The order of this array can change from session to session, therefore it is not recommended to rely on a specific order by using a hardcoded index into the array.  Use DeviceInformation properties to filter your results or provide an UI for the user to choose from.

This sample uses the selector defined above to take a snapshot of devices using FindAllAsync then enumerates through each of the items returned by the collection and writes the device name and ID to the debug output. 

```Csharp
using Windows.Devices.Enumeration;

DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);

foreach (DeviceInformation devInfo in deviceCollection)
{
    Debug.WriteLine("{0} {1}", devInfo.Name, devInfo.Id);
}
```

> [!TIP] 
> When working with the [**Windows.Devices.Enumeration**](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration) APIs, you will frequently need to use [**DeviceInformation**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformation) objects to obtain information about a specific device. For example, [**DeviceInformation.ID**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformation.id) property can be used to recover and reuse the same device if it is available in a future session and [**DeviceInformation.Name**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformation.name) property can be used for display purposes in your app.  See the [**DeviceInformation**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformation) reference page for information about additional properties available.

---

## Method 3: Enumerate and watch

A more powerful and flexible method of enumerating devices is creating a [**DeviceWatcher**](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration.DeviceWatcher).  A device watcher enumerates devices dynamically, so that the application receives notifications if devices are added, removed, or changed  after the initial enumeration is complete.  A DeviceWatcher will allow you to detect when a network connected device comes online, a Bluetooth device is in range, as well as if a locally connected device is unplugged so that you can take the appropriate action within your application.

This sample uses the selector defined above to create a DeviceWatcher as well as defines event handlers for the Added, Removed and Updated notifications. You will need to fill in the details of the actions that you wish to take upon each notification.

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
> See [**Enumerate and watch devices**]( https://docs.microsoft.com/windows/uwp/devices-sensors/enumerate-devices#enumerate-and-watch-devices) for more details on the use of a DeviceWatcher
