---
title: Access sensors and devices from a background task
description: DeviceUseTrigger lets your Universal Windows app access sensors and peripheral devices in the background, even when your foreground app is suspended.
ms.assetid: B540200D-9FF2-49AF-A224-50877705156B
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
---
# Access sensors and devices from a background task




[**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) lets your Universal Windows app access sensors and peripheral devices in the background, even when your foreground app is suspended. For example, depending on where your app is running, it could use a background task to synchronize data with devices or monitor sensors. To help preserve battery life and ensure the appropriate user consent, the use of [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) is subject to policies that are described in this topic.

To access sensors or peripheral devices in the background, create a background task that uses the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger). For an example that shows how this is done on a PC, see the [Custom USB device sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CustomUsbDeviceAccess). For an example on a phone, see the [Background Sensors sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundSensors).

> [!Important]
> **DeviceUseTrigger** cannot be used with in-process background tasks. The info in this topic only applies to background tasks that run out-of-process.

## Device background task overview

When your app is no longer visible to the user, Windows will suspend or terminate your app to reclaim memory and CPU resources. This allows other apps to run in the foreground and reduces battery consumption. When this happens, without the help of a background task, any ongoing data events will be lost. Windows provides the background task trigger, [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger), to let your app perform long running sync and monitoring operations on devices and sensors safely in the background, even if your app is suspended. For more info about app lifecycle, see [Launching, resuming, and background tasks](index.md). For more about background tasks, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

**Note**  In a Universal Windows app, syncing a device in the background requires that your user has approved background syncing by your app. The device must also be connected to or paired with the PC, with active I/O, and is allowed a maximum of 10 minutes of background activity. More detail on policy enforcement is described later in this topic.

### Limitation: critical device operations

Some critical device operations, such as long running firmware updates, cannot be performed with the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger). Such operations can be performed only on the PC, and only by a privileged app that uses the [**DeviceServicingTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceServicingTrigger). A *privileged app* is an app that the device's manufacturer has authorized to perform those operations. Device metadata is used to specify which app, if any, has been designated as the privileged app for a device. For more info, see [Device sync and update for Microsoft Store device apps](/windows-hardware/drivers/devapps/device-sync-and-update-for-uwp-device-apps).

## Protocols/APIs supported in a DeviceUseTrigger background task

Background tasks that use [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) let your app communicate over many protocols/APIs, most of which aren't supported by system-triggered background tasks. The following are supported on a Universal Windows app.

| Protocol         | DeviceUseTrigger in a Universal Windows app                                                                                                                                                    |
|------------------|:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| USB              | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| HID              | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| Bluetooth RFCOMM | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| Bluetooth GATT   | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| MTP              | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| Network wired    | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| Network Wi-Fi    | ![this protocol is supported.](images/ap-tools.png)                                                                                                                                            |
| IDeviceIOControl | ![deviceservicingtrigger supports ideviceiocontrol](images/ap-tools.png)                                                                                                                       |
| Sensors API      | ![deviceservicingtrigger supports universal sensors apis](images/ap-tools.png) (limited to sensors in the [universal device family](../get-started/universal-application-platform-guide.md)) |

## Registering background tasks in the app package manifest

Your app will perform sync and update operations in code that runs as part of a background task. This code is embedded in a Windows Runtime class that implements [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) (or in a dedicated JavaScript page for JavaScript apps). To use a [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task, your app must declare it in the app manifest file of a foreground app, like it does for system-triggered background tasks.

In this example of an app package manifest file, **DeviceLibrary.SyncContent** is the entry point for a background task that uses the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger).

```xml
<Extensions>
  <Extension Category="windows.backgroundTasks" EntryPoint="DeviceLibrary.SyncContent">
    <BackgroundTasks>
      <m2:Task Type="deviceUse" />
    </BackgroundTasks>
  </Extension>
</Extensions>
```

## Introduction to using DeviceUseTrigger

To use the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger), follow these basic steps. For more info about background tasks, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

1.  Your app registers its background task in the app manifest and embeds the background task code in a Windows Runtime class that implements IBackgroundTask or in a dedicated JavaScript page for JavaScript apps.
2.  When your app starts, it will create and configure a trigger object of type [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger), and store the trigger instance for future use.
3.  Your app checks whether the background task has been previously registered and, if not, registers it against the trigger. Note that your app isn't allowed to set conditions on the task associated with this trigger.
4.  When your app needs to trigger the background task, it must first call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) to check if the app is able to request a background task.
5.  If the app can request the background task, it calls the [**RequestAsync**](/uwp/api/windows.applicationmodel.background.deviceusetrigger.requestasync) activation method on the device trigger object.
6.  Your background task isn’t throttled like other system background tasks (there's no CPU time quota) but will run with reduced priority to keep foreground apps responsive.
7.  Windows will then validate, based on the trigger type, that the necessary policies have been met, including requesting user consent for the operation before starting the background task.
8.  Windows monitors system conditions and task runtime and, if necessary, cancels the task if the required conditions are no longer met.
9.  When the background tasks reports progress or completion, your app will receive these events through progress and completed events on the registered task.

**Important**  
Consider these important points when using the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger):

-   The ability to programmatically trigger background tasks that use the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) was first introduced in Windows 8.1 and Windows Phone 8.1.

-   Certain policies are enforced by Windows to ensure user consent when updating peripheral devices on the PC.

-   Additional polices are enforced to preserve user battery life when syncing and updating peripheral devices.

-   Background tasks that use [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) might be canceled by Windows when certain policy requirements are no longer met, including a maximum amount of background time (wall clock time). It's important to consider these policy requirements when using these background tasks to interact with your peripheral device.

**Tip**  To see how these background tasks work, download a sample. For an example that shows how this is done on a PC, see the [Custom USB device sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CustomUsbDeviceAccess). For an example on a phone, see the [Background Sensors sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundSensors).
 
## Frequency and foreground restrictions

There is no restriction on the frequency with which your app can initiate operations, but your app can run only one [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task operation at a time (this does not affect other types of background tasks), and can initiate a background task only while your app is in the foreground. When your app isn't in the foreground, it is unable to initiate a background task with **DeviceUseTrigger**. Your app can't initiate a second **DeviceUseTrigger** background task before the first background task has completed.

## Device restrictions

While each app is limited to registering and running only one [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task, the device (on which your app is running) may allow multiple apps to register and run **DeviceUseTrigger** background tasks. Depending on the device, there may be a limit on the total number of **DeviceUseTrigger** background tasks from all apps. This helps preserve battery on resource-constrained devices. See the following table for more details.

From a single [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task, your app can access an unlimited number of peripheral devices or sensors - limited only by the supported APIs and protocols that were listed in the previous table.

## Background task policies

Windows enforces policies when your app uses a [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task. If these policies aren't met, the background task might be canceled. It's important to consider these policy requirements when using this type of background task to interact with devices or sensors.

### Task initiation policies

This table indicates which task initiation policies apply to a Universal Windows app.

| Policy | DeviceUseTrigger in a Universal Windows app |
|--------|---------------------------------------------|
| Your app is in the foreground when triggering the background task. | ![policy applies](images/ap-tools.png) |
| The device is attached to the system (or in range for a wireless device). | ![policy applies](images/ap-tools.png) |
| The device is accessible to the app using the supported device peripheral APIs (the Windows Runtime APIs for USB, HID, Bluetooth, Sensors, and so on). If your app can't access the device or sensor, access to the background task is denied. | ![policy applies](images/ap-tools.png) |
| Background task entry point provided by the app is registered in the app package manifest. | ![policy applies](images/ap-tools.png) |
| Only one [DeviceUseTrigger](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task is running per app. | ![policy applies](images/ap-tools.png) |
| The maximum number of [DeviceUseTrigger](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background tasks has not yet been reached on the device (on which your app is running). | **Desktop device family**: An unlimited number of tasks can be registered and run in parallel. **Mobile device family**: 1 task on a 512 MB device; otherwise, 2 tasks can be registered and run in parallel. |
| The maximum number of peripheral devices or sensors that your app can access from a single [DeviceUseTrigger](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task, when using the supported APIs/protocols. | unlimited |
| Your background task consumes 400ms of CPU time (assuming a 1GHz CPU) every minute when the screen is locked, or every 5 minutes when the screen is not locked. Failure to meet this policy can result in cancellation of your task. | ![policy applies](images/ap-tools.png) |

### Runtime policy checks

Windows enforces the following runtime policy requirements while your task is running in the background. If any of the runtime requirements stops being true, Windows will cancel your device background task.

This table indicates which runtime policies apply to a Universal Windows app.

| Policy check | DeviceUseTrigger in a Universal Windows app |
|--------------|:-------------------------------------------:|
| The device is attached to the system (or in range for a wireless device). | ![policy check applies](images/ap-tools.png) |
| Task is performing regular I/O to the device (1 I/O every 5 seconds). | ![policy check applies](images/ap-tools.png) |
| App has not canceled the task. | ![policy check applies](images/ap-tools.png) |
| Wall-clock time limit – the total amount of time your app’s task can run in the background. | **Desktop device family**: 10 minutes. **Mobile device family**: No time limit. To conserve resources, no more than 1 or 2 tasks can execute at once. |
| App has not exited. | ![policy check applies](images/ap-tools.png) |

## Best practices

The following are best practices for apps that use the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background tasks.

### Programming a background task

Using the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task from your app ensures that any sync or monitoring operations started from your foreground app continue to run in the background if your users switch apps and your foreground app is suspended by Windows. We recommend that you follow this overall model for registering, triggering, and unregistering your background tasks:

1.  Call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) to check if the app is able to request a background task. This must be done before registering a background task.

2.  Register the background task before requesting the trigger.

3.  Connect progress and completion event handlers to your trigger. When your app returns from suspension, Windows will provide your app with any queued progress or completion events that can be used to determine the status of your background tasks.

4.  Close any open device or sensor objects when you trigger your [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task so that those devices or sensors are free to be opened and used by your background task.

5.  Register the trigger.

6.  Carefully consider the battery impact of accessing a device or senor from a background task. For example, having the report interval of a sensor run too frequently could cause the task to run so often that it quickly drains a phone's battery.

7.  When your background task completes, unregister it.

8.  Register for cancellation events from your background task class. Registering for cancellation events will allow your background task code to cleanly stop your running background task when canceled by Windows or your foreground app.

9.  On app exit (not suspension), unregister and cancel any running tasks if your app no longer needs them. On resource-constrained systems, such as low-memory phones, this will allow other apps to use a [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task.

    -   When your app exits, unregister and cancel any running tasks.

    -   When your app exits, your background tasks will be canceled and any existing event handlers will be disconnected from your existing background tasks. This prevents you from determining the state of your background tasks. Unregistering and canceling the background task will allow your cancellation code to cleanly stop your background tasks.

### Cancelling a background task

To cancel a task running in the background from your foreground app, use the Unregister method on the [**BackgroundTaskRegistration**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskRegistration) object you use in your app to register the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) background task. Unregistering your background task by using the [**Unregister**](/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.unregister) method on **BackgroundTaskRegistration** will cause the background task infrastructure to cancel your background task.

The [**Unregister**](/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.unregister) method additionally takes a Boolean true or false value to indicate if currently running instances of your background task should be canceled without allowing them to finish. For more info, see the API reference for **Unregister**.

In addition to [**Unregister**](/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.unregister), your app will also need to call [**BackgroundTaskDeferral.Complete**](/uwp/api/windows.applicationmodel.background.backgroundtaskdeferral.complete). This informs the system that the asynchronous operation associated with a background task has finished.