---
ms.assetid: 4311D293-94F0-4BBD-A22D-F007382B4DB8
title: Enumerate devices
description: The enumeration namespace enables you to find devices that are internally connected to the system, externally connected, or detectable over wireless or networking protocols.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Enumerate devices


## Samples

The simplest way to enumerate all available devices is to take a snapshot with the [**FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) command (explained further in a section below).

```CSharp
async void enumerateSnapshot(){
  DeviceInformationCollection collection = await DeviceInformation.FindAllAsync();
}
```

To download a sample showing the more advanced usages of the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs, click [here](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/DeviceEnumerationAndPairing).

## Enumeration APIs

The enumeration namespace enables you to find devices that are internally connected to the system, externally connected, or detectable over wireless or networking protocols. The APIs that you use to enumerate through the possible devices are the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) namespace. Some reasons for using these APIs include the following.

-   Finding a device to connect to with your application.
-   Getting information about devices connected to or discoverable by the system.
-   Have an app receive notifications when devices are added, connect, disconnect, change online status, or change other properties.
-   Have an app receive background triggers when devices connect, disconnect, change online status, or change other properties.

These APIs can enumerate devices over any of the following protocols and buses, provided the individual device and the system running the app support that technology. This is not an exhaustive list, and other protocols may be supported by a specific device.

-   Physically connected buses. This includes PCI and USB. For example, anything that you can see in the **Device Manager**.
-   [UPnP](/windows/desktop/UPnP/universal-plug-and-play-start-page)
-   Digital Living Network Alliance (DLNA)
-   [**Discovery and Launch (DIAL)**](/uwp/api/Windows.Media.DialProtocol)
-   [**DNS Service Discovery (DNS-SD)**](/uwp/api/Windows.Networking.ServiceDiscovery.Dnssd)
-   [Web Services on Devices (WSD)](/windows/desktop/WsdApi/wsd-portal)
-   [Bluetooth](/windows/desktop/Bluetooth/bluetooth-start-page)
-   [**Wi-Fi Direct**](/uwp/api/Windows.Devices.WiFiDirect)
-   WiGig
-   [**Point of Service**](/uwp/api/Windows.Devices.PointOfService)

In many cases, you will not need to worry about using the enumeration APIs. This is because many APIs that use devices will automatically select the appropriate default device or provide a more streamlined enumeration API. For example, [**MediaElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaElement) will automatically use the default audio renderer device. As long as your app can use the default device, there is no need to use the enumeration APIs in your application. The enumeration APIs provide a general and flexible way for you to discover and connect to available devices. This topic provides information about enumerating devices and describes the four common ways to enumerate devices.

-   Using the [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker) UI
-   Enumerating a snapshot of devices currently discoverable by the system
-   Enumerating devices currently discoverable and watch for changes
-   Enumerating devices currently discoverable and watch for changes in a background task

## DeviceInformation objects


Working with the enumeration APIs, you will frequently need to use [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) objects. These objects contain most of the available information about the device. The following table explains some of the **DeviceInformation** properties you will be interested in. For a complete list, see the reference page for **DeviceInformation**.

| Property                         | Comments                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
|----------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **DeviceInformation.Id**         | This is the unique identifier of the device and is provided as a string variable. In most cases, this is an opaque value you will just pass from one method to another to indicate the specific device you are interested in. You can also use this property and the **DeviceInformation.Kind** property after closing down your app and reopening it. This will ensure that you can recover and reuse the same [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object. |
| **DeviceInformation.Kind**       | This indicates the kind of device object represented by the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object. This is not the device category or type of device. A single device can be represented by several different **DeviceInformation** objects of different kinds. The possible values for this property are listed in [**DeviceInformationKind**](/uwp/api/windows.devices.enumeration.deviceinformationkind) as well as how they relate to one another.                           |
| **DeviceInformation.Properties** | This property bag contains information that is requested for the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object. The most common properties are easily referenced as properties of the **DeviceInformation** object, such as with [**DeviceInformation.Name**](/uwp/api/windows.devices.enumeration.deviceinformation.name). For more information, see [Device information properties](device-information-properties.md).                                                                |

 

## DevicePicker UI


The [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker) is a control provided by Windows that creates a small UI that enables the user to select a device from a list. You can customize the **DevicePicker** window in a few ways.

-   You can control the devices that are displayed in the UI by adding a [**SupportedDeviceSelectors**](/uwp/api/windows.devices.enumeration.devicepickerfilter.supporteddeviceselectors), a [**SupportedDeviceClasses**](/uwp/api/windows.devices.enumeration.devicepickerfilter.supporteddeviceclasses), or both to the [**DevicePicker.Filter**](/uwp/api/windows.devices.enumeration.devicepicker.filter). In most cases, you only need to add one selector or class, but if you do need more than one you can add multiple. If you do add multiple selectors or classes, they are conjoined using an OR logic function.
-   You can specify the properties you want to retrieve for the devices. You can do this by adding properties to [**DevicePicker.RequestedProperties**](/uwp/api/windows.devices.enumeration.devicepicker.requestedproperties).
-   You can alter the appearance of the [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker) using [**Appearance**](/uwp/api/windows.devices.enumeration.devicepicker.appearance).
-   You can specify the size and location of the [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker) when it is displayed.

While the [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker) is displayed, the contents of the UI will be automatically updated if devices are added, removed, or updated.

**Note**  You cannot specify the [**DeviceInformationKind**](/uwp/api/windows.devices.enumeration.deviceinformationkind) using the [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker). If you want to have devices of a specific **DeviceInformationKind**, you will need to build a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) and provide your own UI.

 

Casting media content and DIAL also each provide their own pickers if you want to use them. They are [**CastingDevicePicker**](/uwp/api/Windows.Media.Casting.CastingDevicePicker) and [**DialDevicePicker**](/uwp/api/Windows.Media.DialProtocol.DialDevicePicker), respectively.

## Enumerate a snapshot of devices


In some scenarios, the [**DevicePicker**](/uwp/api/Windows.Devices.Enumeration.DevicePicker) will not be suitable for your needs and you need something more flexible. Perhaps you want to build your own UI or need to enumerate devices without displaying a UI to the user. In these situations, you could enumerate a snapshot of devices. This involves looking through the devices that are currently connected to or paired with the system. However, you need to be aware that this method only looks at a snapshot of devices that are available, so you will not be able to find devices that connect after you enumerate through the list. You also will not be notified if a device is updated or removed. Another potential downside to be aware of is that this method will hold back any results until the entire enumeration is completed. For this reason, you should not use this method when you are interested in **AssociationEndpoint**, **AssociationEndpointContainer**, or **AssociationEndpointService** objects since they are found over a network or wireless protocol. This can take up to 30 seconds to complete. In that scenario, you should use a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) object to enumerate through the possible devices.

To enumerate through a snapshot of devices, use the [**FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) method. This method waits until the entire enumeration process is complete and returns all the results as one [**DeviceInformationCollection**](/uwp/api/windows.devices.enumeration.deviceinformationcollection) object. This method is also overloaded to provide you with several options for filtering your results and limiting them to the devices that you are interested in. You can do this by providing a [**DeviceClass**](/uwp/api/Windows.Devices.Enumeration.DeviceClass) or passing in a device selector. The device selector is an AQS string that specifies the devices you want to enumerate. For more information, see [Build a device selector](build-a-device-selector.md).

An example of a device enumeration snapshot is provided below:



In addition to limiting the results, you can also specify the properties that you want to retrieve for the devices. If you do, the specified properties will be available in the property bag for each of the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) objects returned in the collection. It is important to note that not all properties are available for all device kinds. To see what properties are available for which device kinds, see [Device information properties](device-information-properties.md).



## Enumerate and watch devices


A more powerful and flexible method of enumerating devices is creating a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher). This option provides the most flexibility when you are enumerating devices. It allows you to enumerate devices that are currently present, and also receive notifications when devices that match your device selector are added, removed, or properties change. When you create a **DeviceWatcher**, you provide a device selector. For more information about device selectors, see [Build a device selector](build-a-device-selector.md). After creating the watcher, you will receive the following notifications for any device that matches your provided criteria.

-   Add notification when a new device is added.
-   Update notification when a property you are interested in is changed.
-   Remove notification when a device is no longer available or no longer matches your filter.

In most cases where you are using a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher), you are maintaining a list of devices and adding to it, removing items from it, or updating items as your watcher receives updates from the devices that you are watching. When you receive an update notification, the updated information will be available as a [**DeviceInformationUpdate**](/uwp/api/windows.devices.enumeration.deviceinformationupdate) object. In order to update your list of devices, first find the appropriate [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) that changed. Then call the [**Update**](/uwp/api/windows.devices.enumeration.deviceinformation.update) method for that object, providing the **DeviceInformationUpdate** object. This is a convenience function that will automatically update your **DeviceInformation** object.

Since a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) sends notifications as devices arrive and when they change, you should use this method of enumerating devices when you are interested in **AssociationEndpoint**, **AssociationEndpointContainer**, or **AssociationEndpointService** objects since they are enumerated over networking or wireless protocols.

To create a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher), use one of the [**CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher) methods. These methods are overloaded to enable you to specify the devices that you are interested in. You can do this by providing a [**DeviceClass**](/uwp/api/Windows.Devices.Enumeration.DeviceClass) or passing in a device selector. The device selector is an AQS string that specifies the devices you want to enumerate. For more information, see [Build a device selector](build-a-device-selector.md). You can also specify the properties that you want to retrieve for the devices and are interested in. If you do, the specified properties will be available in the property bag for each of the [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) objects returned in the collection. It is important to note that not all properties are available for all device kinds. To see what properties are available for which device kinds, see [Device information properties](device-information-properties.md)

## Watch devices as a background task


Watching devices as a background task is very similar to creating a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) as described above. In fact, you will still need to create a normal **DeviceWatcher** object first as described in the previous section. Once you create it, you call [**GetBackgroundTrigger**](/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted) instead of [**DeviceWatcher.Start**](/uwp/api/windows.devices.enumeration.devicewatcher.start). When you call **GetBackgroundTrigger**, you must specify which of the notifications you are interested in: add, remove, or update. You cannot request update or remove without requesting add as well. Once you register the trigger, the **DeviceWatcher** will start running immediately in the background. From this point forward, whenever it receives a new notification for your application that matches your criteria, the background task will trigger and it will provide you the latest changes since it last triggered your application.

**Important**  The first time that a [**DeviceWatcherTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceWatcherTrigger) triggers your application will be when the watcher reaches the **EnumerationCompleted** state. This means it will contain all of the initial results. Any future times it triggers your application, it will only contain the add, update, and remove notifications that have occurred since the last trigger. This is slightly different from a foreground [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) object because the initial results do not come in one at a time and are only delivered in a bundle after the **EnumerationCompleted** is reached.

 

Some wireless protocols behave differently if they are scanning in the background versus the foreground, or they may not support scanning in the background at all. There are three possibilities with relation to background scanning. The following table lists the possibilities and the effects this may have on your application. For example, Bluetooth and Wi-Fi Direct do not support background scans, so by extension, they do not support a [**DeviceWatcherTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceWatcherTrigger).

| Behavior                                  | Impact                                                                                                                                  |
|-------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| Same behavior in background               | None                                                                                                                                    |
| Only passive scans possible in background | Device may take longer to discover while waiting for a passive scan to occur.                                                           |
| Background scans not supported            | No devices will be detectable by the [**DeviceWatcherTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceWatcherTrigger), and no updates will be reported. |

 

If your [**DeviceWatcherTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceWatcherTrigger) includes a protocol that does not support scanning in as a background task, your trigger will still work. However, you will not be able to get any updates or results over that protocol. The updates for other protocols or devices will still be detected normally.

## Using DeviceInformationKind


In most scenarios, you will not need to worry about the [**DeviceInformationKind**](/uwp/api/windows.devices.enumeration.deviceinformationkind) of a [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object. This is because the device selector returned by the device API you're using will often guarantee you are getting the correct kinds of device objects to use with their API. However, in some scenarios you will want to get the **DeviceInformation** for devices, but there is not a corresponding device API to provide a device selector. In these cases you will need to build your own selector. For example, Web Services on Devices does not have a dedicated API, but you can discover those devices and get information about them using the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs and then use them using the socket APIs.

If you are building your own device selector to enumerate through device objects, [**DeviceInformationKind**](/uwp/api/windows.devices.enumeration.deviceinformationkind) will be important for you to understand. All of the possible kinds, as well as how they relate to one another, are described on the reference page for **DeviceInformationKind**. One of the most common uses of **DeviceInformationKind** is to specify what kind of devices you are searching for when submitting a query in conjunction with a device selector. By doing this, it makes sure that you only enumerate over devices that match the provided **DeviceInformationKind**. For example, you could find a **DeviceInterface** object and then run a query to get the information for the parent **Device** object. That parent object may contain additional information.

It is important to note that the properties available in the property bag for a [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object will vary depending on the [**DeviceInformationKind**](/uwp/api/windows.devices.enumeration.deviceinformationkind) of the device. Certain properties are only available with certain kinds. For more information about which properties are available for which kinds, see [Device information properties](device-information-properties.md). Hence, in the above example, searching for the parent **Device** will give you access to more information that was not available from the **DeviceInterface** device object. Because of this, when you create your AQS filter strings, it is important to ensure that the requested properties are available for the **DeviceInformationKind** objects you are enumerating. For more information about building a filter, see [Build a device selector](build-a-device-selector.md).

When enumerating **AssociationEndpoint**, **AssociationEndpointContainer**, or **AssociationEndpointService** objects, you are enumerating over a wireless or network protocol. In these situations, we recommend that you don't use [**FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) and instead use [**CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher). This is because searching over a network often results in search operations that won't timeout for 10 or more seconds before generating [**EnumerationCompleted**](/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted). **FindAllAsync** doesn't complete its operation until **EnumerationCompleted** is triggered. If you are using a [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher), you'll get results closer to real time regardless of when **EnumerationCompleted** is called.

## Save a device for later use


Any [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object is uniquely identified by a combination of two pieces of information: [**DeviceInformation.Id**](/uwp/api/windows.devices.enumeration.deviceinformation.id) and [**DeviceInformation.Kind**](/uwp/api/windows.devices.enumeration.deviceinformation.kind). If you keep these two pieces of information, you can recreate a **DeviceInformation** object after it is lost by supplying this information to [**CreateFromIdAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.createfromidasync). If you do this, you can save user preferences for a device that integrates with your app.


 

 