---
title: PointOfService device objects
description: Learn how to create a PointOfService device object and learn about the device object lifecycle in the Universal Windows Platform (UWP) application model.
ms.date: 06/19/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# PointOfService device objects

## Creating a device object

Once you have identified the PointOfService device that you want to use, either from a fresh enumeration or a stored DeviceID, you just call [**FromIdAsync**](/uwp/api/windows.devices.pointofservice.barcodescanner.fromidasync) with the[**DeviceID**](/uwp/api/windows.devices.enumeration.deviceinformation.id) that you have chosen programmatically or the user has selected to create a new Point of Service device object.

This sample attempts to create a new BarcodeScanner object with FromIdAsync using a DeviceID. If there is a failure creating the object a debug message is written.

```Csharp

    BarcodeScanner barcodeScanner = await BarcodeScanner.FromIdAsync(DeviceId);

    if(barcodeScanner != null)
    {
        // after successful creation, claim the scanner for exclusive use and enable it to exchange data
    }
    else
    {
        Debug.WriteLine("Failure to create barcodeScanner object");
    }
    
```

Once you have a device object, you can then access the device's methods, properties and events.  

## Device object lifecycle

Before Windows 8, apps had a simple lifecycle. Win32 and .NET apps are either running or not running and PointOfService peripherals were usually claimed for the full app lifecycle. When a user minimizes them, or switches away from them, they continue to run. This was fine until portable devices and power management became increasingly important.

Windows 8 introduced a new application model with UWP apps. At a high level, a new suspended state was added. A UWP app is suspended shortly after the user minimizes it or switches to another app. This means that the app's threads are stopped, the app is left in memory unless the operating system needs to reclaim resources, and any device objects representing PointOfService peripherals are automatically closed to allow other applications access to the peripherals. When the user switches back to the app, it can be quickly restored to a running state and restore PointOfService peripherals connections provided they are still available on resume.

You can detect when an object is closed for any reason with a \<DeviceObject\>.Closed event handler then make note of the device ID for re-establishing the connection in the future.   Alternatively, you may wish to handle this on an App Suspend notification to save the device ID's for re-establishing the device connections on App Resume notification.  Make sure that you do not double up on the event handlers and duplicate actions for the device object on both \<DeviceObject\>.Closed and App Suspend.

> [!TIP]
> Please refer to the following topics for more information about Windows 10 Universal Windows Platform (UWP) application lifecycle:
> - [Windows 10 Universal Windows Platform (UWP) app lifecycle](../launch-resume/app-lifecycle.md)
> - [Handle app suspension](../launch-resume/suspend-an-app.md)
> - [Handle app resume](../launch-resume/resume-an-app.md)