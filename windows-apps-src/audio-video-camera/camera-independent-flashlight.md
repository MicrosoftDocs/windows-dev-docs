---
ms.assetid: D20C8E01-4E78-4115-A2E8-07BB3E67DDDC
description: This article shows how to access and use a device's lamp, if one is present. Lamp functionality is managed separately from the device's camera and camera flash functionality.
title: Camera-independent Flashlight
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Camera-independent Flashlight



This article shows how to access and use a device's lamp, if one is present. Lamp functionality is managed separately from the device's camera and camera flash functionality. In addition to acquiring a reference to the lamp and adjusting its settings, this article also shows you how to properly free up the lamp resource when it's not in use, and how to detect when the lamp's availability changes in case it is being used by another app.

## Get the device's default lamp

To get a device's default lamp device, call [**Lamp.GetDefaultAsync**](/uwp/api/windows.devices.lights.lamp.getdefaultasync). The lamp APIs are found in the [**Windows.Devices.Lights**](/uwp/api/Windows.Devices.Lights) namespace. Be sure to add a using directive for this namespace before attempting to access these APIs.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetLightsNamespace":::


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetDeclareLamp":::


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetGetDefaultLamp":::

If the returned object is **null**, the **Lamp** API is unsupported on the device. Some devices may not support the **Lamp** API even if there is a lamp physically present on the device.

## Get a specific lamp using the lamp selector string

Some devices may have more than one lamp. To obtain a list of lamps available on the device, get the device selector string by calling [**GetDeviceSelector**](/uwp/api/windows.devices.lights.lamp.getdeviceselector). This selector string can then be passed into [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync). This method is used to enumerate many different kinds of devices and the selector string lets the method know to return only lamp devices. The [**DeviceInformationCollection**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationCollection) object returned from **FindAllAsync** is a collection of [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) objects representing the lamps available on the device. Select one of the objects in the list and then pass the [**Id**](/uwp/api/windows.devices.enumeration.deviceinformation.id) property to [**Lamp.FromIdAsync**](/uwp/api/windows.devices.lights.lamp.fromidasync) to get a reference to the requested lamp. This example uses the **GetFirstOrDefault** extension method from the **System.Linq** namespace to select the **DeviceInformation** object where the [**EnclosureLocation.Panel**](/uwp/api/windows.devices.enumeration.enclosurelocation.panel) property has a value of **Back**, which selects a lamp that is on the back of the device's enclosure, if one exists.

Note that the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) APIs are found in the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) namespace.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetEnumerationNamespace":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetGetLampWithSelectionString":::

## Adjust lamp settings

After you have an instance of the [**Lamp**](/uwp/api/Windows.Devices.Lights.Lamp) class, turn the lamp on by setting the [**IsEnabled**](/uwp/api/windows.devices.lights.lamp.isenabled) property to **true**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetLampSettingsOn":::

Turn the lamp off by setting the [**IsEnabled**](/uwp/api/windows.devices.lights.lamp.isenabled) property to **false**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetLampSettingsOff":::

Some devices have lamps that support color values. Check if a lamp supports color by checking the [**IsColorSettable**](/uwp/api/windows.devices.lights.lamp.iscolorsettable) property. If this value is **true**, you can set the color of the lamp with the [**Color**](/uwp/api/windows.devices.lights.lamp.color) property.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetLampSettingsColor":::

## Register to be notified if the lamp availability changes

Lamp access is granted to the most recent app to request access. So, if another app is launched and requests a lamp resource that your app is currently using, your app will no longer be able to control the lamp until the other app has released the resource. To receive a notification when the availability of the lamp changes, register a handler for the [**Lamp.AvailabilityChanged**](/uwp/api/windows.devices.lights.lamp.availabilitychanged) event.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetAvailabilityChanged":::

In the handler for the event, check the [**LampAvailabilityChanged.IsAvailable**](/uwp/api/windows.devices.lights.lampavailabilitychangedeventargs.isavailable) property to determine if the lamp is available. In this example, a toggle switch for turning the lamp on and off is enabled or disabled based on the lamp availability.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetAvailabilityChangedHandler":::

## Properly dispose of the lamp resource when not in use

When you are no longer using the lamp, you should disable it and call [**Lamp.Close**](/uwp/api/windows.devices.lights.lamp.close) to release the resource and allow other apps to access the lamp. This property is mapped to the **Dispose** method if you are using C#. If you registered for the [**AvailabilityChanged**](/uwp/api/windows.devices.lights.lamp.availabilitychanged), you should unregister the handler when you dispose of the lamp resource. The right place in your code to dispose of the lamp resource depends on your app. To scope lamp access to a single page, release the resource in the [**OnNavigatingFrom**](/uwp/api/windows.ui.xaml.controls.page.onnavigatingfrom) event.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Lamp/cs/MainPage.xaml.cs" id="SnippetDisposeLamp":::

## Related topics
- [Media playback](media-playback.md)

 
