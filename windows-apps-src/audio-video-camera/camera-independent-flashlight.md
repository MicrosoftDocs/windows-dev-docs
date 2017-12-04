---
author: drewbatgit
ms.assetid: D20C8E01-4E78-4115-A2E8-07BB3E67DDDC
description: This article shows how to access and use a device's lamp, if one is present. Lamp functionality is managed separately from the device's camera and camera flash functionality.
title: Camera-independent Flashlight
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Camera-independent Flashlight



This article shows how to access and use a device's lamp, if one is present. Lamp functionality is managed separately from the device's camera and camera flash functionality. In addition to acquiring a reference to the lamp and adjusting its settings, this article also shows you how to properly free up the lamp resource when it's not in use, and how to detect when the lamp's availability changes in case it is being used by another app.

## Get the device's default lamp

To get a device's default lamp device, call [**Lamp.GetDefaultAsync**](https://msdn.microsoft.com/library/windows/apps/dn894327). The lamp APIs are found in the [**Windows.Devices.Lights**](https://msdn.microsoft.com/library/windows/apps/dn894331) namespace. Be sure to add a using directive for this namespace before attempting to access these APIs.

[!code-cs[LightsNamespace](./code/Lamp/cs/MainPage.xaml.cs#SnippetLightsNamespace)]


[!code-cs[DeclareLamp](./code/Lamp/cs/MainPage.xaml.cs#SnippetDeclareLamp)]


[!code-cs[GetDefaultLamp](./code/Lamp/cs/MainPage.xaml.cs#SnippetGetDefaultLamp)]

If the returned object is **null**, the **Lamp** API is unsupported on the device. Some devices may not support the **Lamp** API even if there is a lamp physically present on the device.

## Get a specific lamp using the lamp selector string

Some devices may have more than one lamp. To obtain a list of lamps available on the device, get the device selector string by calling [**GetDeviceSelector**](https://msdn.microsoft.com/library/windows/apps/dn894328). This selector string can then be passed into [**DeviceInformation.FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/br225432). This method is used to enumerate many different kinds of devices and the selector string lets the method know to return only lamp devices. The [**DeviceInformationCollection**](https://msdn.microsoft.com/library/windows/apps/br225395) object returned from **FindAllAsync** is a collection of [**DeviceInformation**](https://msdn.microsoft.com/library/windows/apps/br225393) objects representing the lamps available on the device. Select one of the objects in the list and then pass the [**Id**](https://msdn.microsoft.com/library/windows/apps/br225437) property to [**Lamp.FromIdAsync**](https://msdn.microsoft.com/library/windows/apps/dn894326) to get a reference to the requested lamp. This example uses the **GetFirstOrDefault** extension method from the **System.Linq** namespace to select the **DeviceInformation** object where the [**EnclosureLocation.Panel**](https://msdn.microsoft.com/library/windows/apps/br229906) property has a value of **Back**, which selects a lamp that is on the back of the device's enclosure, if one exists.

Note that the [**DeviceInformation**](https://msdn.microsoft.com/library/windows/apps/br225393) APIs are found in the [**Windows.Devices.Enumeration**](https://msdn.microsoft.com/library/windows/apps/br225459) namespace.

[!code-cs[EnumerationNamespace](./code/Lamp/cs/MainPage.xaml.cs#SnippetEnumerationNamespace)]

[!code-cs[GetLampWithSelectionString](./code/Lamp/cs/MainPage.xaml.cs#SnippetGetLampWithSelectionString)]

## Adjust lamp settings

After you have an instance of the [**Lamp**](https://msdn.microsoft.com/library/windows/apps/dn894310) class, turn the lamp on by setting the [**IsEnabled**](https://msdn.microsoft.com/library/windows/apps/dn894330) property to **true**.

[!code-cs[LampSettingsOn](./code/Lamp/cs/MainPage.xaml.cs#SnippetLampSettingsOn)]

Turn the lamp off by setting the [**IsEnabled**](https://msdn.microsoft.com/library/windows/apps/dn894330) property to **false**.

[!code-cs[LampSettingsOff](./code/Lamp/cs/MainPage.xaml.cs#SnippetLampSettingsOff)]

Some devices have lamps that support color values. Check if a lamp supports color by checking the [**IsColorSettable**](https://msdn.microsoft.com/library/windows/apps/dn894329) property. If this value is **true**, you can set the color of the lamp with the [**Color**](https://msdn.microsoft.com/library/windows/apps/dn894322) property.

[!code-cs[LampSettingsColor](./code/Lamp/cs/MainPage.xaml.cs#SnippetLampSettingsColor)]

## Register to be notified if the lamp availability changes

Lamp access is granted to the most recent app to request access. So, if another app is launched and requests a lamp resource that your app is currently using, your app will no longer be able to control the lamp until the other app has released the resource. To receive a notification when the availability of the lamp changes, register a handler for the [**Lamp.AvailabilityChanged**](https://msdn.microsoft.com/library/windows/apps/dn894317) event.

[!code-cs[AvailabilityChanged](./code/Lamp/cs/MainPage.xaml.cs#SnippetAvailabilityChanged)]

In the handler for the event, check the [**LampAvailabilityChanged.IsAvailable**](https://msdn.microsoft.com/library/windows/apps/dn894315) property to determine if the lamp is available. In this example, a toggle switch for turning the lamp on and off is enabled or disabled based on the lamp availability.

[!code-cs[AvailabilityChangedHandler](./code/Lamp/cs/MainPage.xaml.cs#SnippetAvailabilityChangedHandler)]

## Properly dispose of the lamp resource when not in use

When you are no longer using the lamp, you should disable it and call [**Lamp.Close**](https://msdn.microsoft.com/library/windows/apps/dn894320) to release the resource and allow other apps to access the lamp. This property is mapped to the **Dispose** method if you are using C#. If you registered for the [**AvailabilityChanged**](https://msdn.microsoft.com/library/windows/apps/dn894317), you should unregister the handler when you dispose of the lamp resource. The right place in your code to dispose of the lamp resource depends on your app. To scope lamp access to a single page, release the resource in the [**OnNavigatingFrom**](https://msdn.microsoft.com/library/windows/apps/br227509) event.

[!code-cs[DisposeLamp](./code/Lamp/cs/MainPage.xaml.cs#SnippetDisposeLamp)]

## Related topics
- [Media playback](media-playback.md)

 




