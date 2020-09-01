---
title: Support your app with background tasks
description: The topics in this section show you how to make lightweight code run in the background in response to triggers.
ms.assetid: EFF7CBFB-D309-4ACB-A2A5-28E19D447E32
ms.date: 08/21/2017
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
---
# Support your app with background tasks


The topics in this section show you how to make lightweight code run in the background in response to triggers. You can use background tasks to provide functionality when your app is suspended or not running. You can also use background tasks for real-time communication apps like VOIP, mail, and IM.

## Playing media in the background

Starting in Windows 10, version 1607, playing audio in the background is much easier. See [Play media in the background](../audio-video-camera/background-audio.md) for more information.

## In-process and out-of-process background tasks

There are two approaches to implementing background tasks:

* In-process: the app and its background process run in the same process
* Out-of-process: the app and the background process run in separate processes.

In-process background support was introduced in Windows 10, version 1607, to simplify writing background tasks. But you can still write out-of-process background tasks. See [Guidelines for background tasks](guidelines-for-background-tasks.md) for recommendations on when to write an in-process versus out-of-process background task.

Out-of-process background tasks are more resilient because the background process can't bring down your app process if something goes wrong. But the resiliency comes at the price of greater complexity to manage the cross-process communication between the app and the background task.

Out-of-process background tasks are implemented as lightweight classes that implement the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface that the OS runs in a separate process (backgroundtaskhost.exe). Register a background task by using the [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) class. The class name is used to specify the entry point when you registering the background task.

In Windows 10, version 1607, you can enable background activity without creating a background task. You can instead run your background code directly inside the foreground application's process.

To get started quickly with in-process background tasks, see [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).

To get started quickly with out-of-process background tasks, see [Create and register an out-of-process background task](create-and-register-a-background-task.md).

> [!TIP]
> Starting with Windows 10, you no longer need to place an app on the lock screen as a prerequisite for registering a background task for it.

## Background tasks for system events

Your app can respond to system-generated events by registering a background task with the [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTrigger) class. An app can use any of the following system event triggers (defined in [**SystemTriggerType**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType))

| Trigger name                     | Description                                                                                                    |
|----------------------------------|----------------------------------------------------------------------------------------------------------------|
| **InternetAvailable**            | The Internet becomes available.                                                                                |
| **NetworkStateChange**           | A network change such as a change in cost or connectivity occurs.                                              |
| **OnlineIdConnectedStateChange** | Online ID associated with the account changes.                                                                 |
| **SmsReceived**                  | A new SMS message is received by an installed mobile broadband device.                                         |
| **TimeZoneChange**               | The time zone changes on the device (for example, when the system adjusts the clock for daylight saving time). |

For more info see [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md).

## Conditions for background tasks

You can control when the background task runs, even after it is triggered, by adding a condition. Once triggered, a background task will not run until all of its conditions are met. The following conditions (represented by the [**SystemConditionType**](/uwp/api/Windows.ApplicationModel.Background.SystemConditionType) enumeration) can be used.

| Condition name           | Description                       |
|--------------------------|-----------------------------------|
| **InternetAvailable**    | The Internet must be available.   |
| **InternetNotAvailable** | The Internet must be unavailable. |
| **SessionConnected**     | The session must be connected.    |
| **SessionDisconnected**  | The session must be disconnected. |
| **UserNotPresent**       | The user must be away.            |
| **UserPresent**          | The user must be present.         |

Add the **InternetAvailable** condition to your background task [BackgroundTaskBuilder.AddCondition](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) to delay triggering the background task until the network stack is running. This condition saves power because the background task won't execute until the network is available. This condition does not provide real-time activation.

If your background task requires network connectivity, set [IsNetworkRequested](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) to ensure that the network stays up while the background task runs. This tells the background task infrastructure to keep the network up while the task is executing, even if the device has entered Connected Standby mode. If your background task does not set **IsNetworkRequested**, then your background task will not be able to access the network when in Connected Standby mode (for example, when a phone's screen is turned off.)
 
For more info about background task conditions, see [Set conditions for running a background task](set-conditions-for-running-a-background-task.md).

## Application manifest requirements

Before your app can successfully register a background task that runs out-of-process, it must be declared in the application manifest. Background tasks that run in the same process as their host app do not need to be declared in the application manifest. For more info see [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md).

## Background tasks

The following real-time triggers can be used to run lightweight custom code in the background:

| Real-time trigger  | Description |
|--------------------|-------------|
| **Control Channel** | Background tasks can keep a connection alive, and receive messages on the control channel, by using the [**ControlChannelTrigger**](/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). If your app is listening to a socket, you can use the Socket Broker instead of the **ControlChannelTrigger**. For more details on using the Socket Broker, see [SocketActivityTrigger](/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger). The **ControlChannelTrigger** is not supported on Windows Phone. |
| **Timer** | Background tasks can run as frequently as every 15 minutes, and they can be set to run at a certain time by using the [**TimeTrigger**](/uwp/api/Windows.ApplicationModel.Background.TimeTrigger). For more info see [Run a background task on a timer](run-a-background-task-on-a-timer-.md). |
| **Push Notification** | Background tasks respond to the [**PushNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger) to receive raw push notifications. |

**Note**  

Universal Windows apps must call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) before registering any of the background trigger types.

To ensure that your Universal Windows app continues to run properly after you release an update, call [**RemoveAccess**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.removeaccess) and then call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) when your app launches after being updated. For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

**Limits on the number of trigger instances:** There are limits to how many instances of some triggers an app can register. An app can only register   [ApplicationTrigger](/uwp/api/Windows.ApplicationModel.Background.ApplicationTrigger), [MediaProcessingTrigger](/uwp/api/windows.applicationmodel.background.mediaprocessingtrigger) and [DeviceUseTrigger](/uwp/api/windows.applicationmodel.background.deviceusetrigger?f=255&MSPPError=-2147217396) once per instance of the app. If an app goes over this limit, registration will throw an exception.

## System event triggers

The [**SystemTriggerType**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType) enumeration represents the following system event triggers:

| Trigger name            | Description                                                       |
|-------------------------|-------------------------------------------------------------------|
| **UserPresent**         | The background task is triggered when the user becomes present.   |
| **UserAway**            | The background task is triggered when the user becomes absent.    |
| **ControlChannelReset** | The background task is triggered when a control channel is reset. |
| **SessionConnected**    | The background task is triggered when the session is connected.   |

   
The following system event triggers signal when the user has moved an app on or off the lock screen.

| Trigger name                     | Description                                  |
|----------------------------------|----------------------------------------------|
| **LockScreenApplicationAdded**   | An app tile is added to the lock screen.     |
| **LockScreenApplicationRemoved** | An app tile is removed from the lock screen. |

 
## Background task resource constraints

Background tasks are lightweight. Keeping background execution to a minimum ensures the best user experience with foreground apps and battery life. This is enforced by applying resource constraints to background tasks.

Background tasks are limited to 30 seconds of wall-clock usage.

### Memory constraints

Due to the resource constraints for low-memory devices, background tasks may have a memory limit that determines the maximum amount of memory the background task can use. If your background task attempts an operation that would exceed this limit, the operation will fail and may generate an out-of-memory exception--which the task can handle. If the task does not handle the out-of-memory exception, or the nature of the attempted operation is such that an out-of-memory exception was not generated, then the task will be terminated immediately.  

You can use the [**MemoryManager**](/uwp/api/Windows.System.MemoryManager) APIs to query your current memory usage and limit in order to discover your cap (if any), and to monitor your background task's ongoing memory usage.

### Per-device limit for apps with background tasks for low-memory devices

On memory-constrained devices, there is a limit to the number of apps that can be installed on a device and use background tasks at any given time. If this number is exceeded, the call to [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync), which is required to register all background tasks, will fail.

### Battery Saver

Unless you exempt your app so that it can still run background tasks and receive push notifications when Battery Saver is on, the Battery Saver feature, when enabled, will prevent background tasks from running when the device is not connected to external power and the battery goes below a specified amount of power remaining. This will not prevent you from registering background tasks.

However, for enterprise apps, and apps that will not be published in the Microsoft Store, see [Run in the background indefinitely](run-in-the-background-indefinetly.md) to learn how to use a capabilities to run a background task or extended execution session in the background indefinitely.

## Background task resource guarantees for real-time communication

To prevent resource quotas from interfering with real-time communication functionality, background tasks using the [**ControlChannelTrigger**](/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) and [**PushNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger) receive guaranteed CPU resource quotas for every running task. The resource quotas are as mentioned above, and remain constant for these background tasks.

Your app doesn't have to do anything differently to get the guaranteed resource quotas for [**ControlChannelTrigger**](/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) and [**PushNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger) background tasks. The system always treats these as critical background tasks.

## Maintenance trigger

Maintenance tasks only run when the device is plugged in to AC power. For more info see [Use a maintenance trigger](use-a-maintenance-trigger.md).

## Background tasks for sensors and devices

Your app can access sensors and peripheral devices from a background task with the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) class. You can use this trigger for long-running operations such as data synchronization or monitoring. Unlike tasks for system events, a **DeviceUseTrigger** task can only be triggered while your app is running in the foreground and no conditions can be set on it.

> [!IMPORTANT]
> The **DeviceUseTrigger** and **DeviceServicingTrigger** cannot be used with in-process background tasks.

Some critical device operations, such as long running firmware updates, cannot be performed with the [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger). Such operations can be performed only on the PC, and only by a privileged app that uses the [**DeviceServicingTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceServicingTrigger). A *privileged app* is an app that the device's manufacturer has authorized to perform those operations. Device metadata is used to specify which app, if any, has been designated as the privileged app for a device. For more info, see [Device sync and update for Microsoft Store device apps](/windows-hardware/drivers/devapps/device-sync-and-update-for-uwp-device-apps)

## Managing background tasks

Background tasks can report progress, completion, and cancellation to your app using events and local storage. Your app can also catch exceptions thrown by a background task, and manage background task registration during app updates. For more info see:

[Handle a cancelled background task](handle-a-cancelled-background-task.md)  
[Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)

Check your background task registration during app launch. Ensure that your app's ungrouped background tasks are present in BackgroundTaskBuilder.AllTasks. Re-register the ones that are not present. Unregister any tasks that are no longer needed. This ensures that all background tasks registrations are up-to-date every time the app is launched.

## Related topics

**Conceptual guidance for multitasking in Windows 10**

* [Launching, resuming, and multitasking](index.md)

**Related background task guidance**

* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Access sensors and devices from a background task](access-sensors-and-devices-from-a-background-task.md)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md)
* [Create and register an out-of-process background task](create-and-register-a-background-task.md)
* [Convert an out-of-process background task to an in-process background task](convert-out-of-process-background-task.md)
* [Debug a background task](debug-a-background-task.md)
* [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md)
* [Group background task registration](group-background-tasks.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](/visualstudio/debugger/how-to-trigger-suspend-resume-and-background-events-for-windows-store-apps-in-visual-studio)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Play media in the background](../audio-video-camera/background-audio.md)
* [Register a background task](register-a-background-task.md)
* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
* [Run a background task when your UWP app is updated](run-a-background-task-during-updatetask.md)
* [Run in the background indefinitely](run-in-the-background-indefinetly.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Trigger a background task from your app](trigger-background-task-from-app.md)
* [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)