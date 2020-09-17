---
title: Respond to system events with background tasks
description: Learn how to create a background task that responds to SystemTrigger events.
ms.assetid: 43C21FEA-28B9-401D-80BE-A61B71F01A89
ms.date: 07/06/2018
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
dev_langs:
- csharp
- cppwinrt
- cpp
---
# Respond to system events with background tasks

**Important APIs**

- [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask)
- [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder)
- [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTrigger)

Learn how to create a background task that responds to [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType) events.

This topic assumes that you have a background task class written for your app, and that this task needs to run in response to an event triggered by the system such as when internet availability changes or the user logging in. This topic focuses on the [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType) class. More information on writing a background task class is available in [Create and register an in-process background task](create-and-register-an-inproc-background-task.md) or [Create and register an out-of-process background task](create-and-register-a-background-task.md).

## Create a SystemTrigger object

In your app code, create a new [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTrigger) object. The first parameter, *triggerType*, specifies the type of system event trigger that will activate this background task. For a list of event types, see [**SystemTriggerType**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType).

The second parameter, *OneShot*, specifies whether the background task will run only once the next time the system event occurs or every time the system event occurs until the task is unregistered.

The following code specifies that the background task runs whenever the Internet becomes available:

```csharp
SystemTrigger internetTrigger = new SystemTrigger(SystemTriggerType.InternetAvailable, false);
```

```cppwinrt
Windows::ApplicationModel::Background::SystemTrigger internetTrigger{
    Windows::ApplicationModel::Background::SystemTriggerType::InternetAvailable, false};
```

```cpp
SystemTrigger ^ internetTrigger = ref new SystemTrigger(SystemTriggerType::InternetAvailable, false);
```

## Register the background task

Register the background task by calling your background task registration function. For more information on registering background tasks, see [Register a background task](register-a-background-task.md).

The following code registers the background task for a background process that runs out-of-process. If you were calling a background task that runs in the same process as the host app, you would not set `entrypoint`:

```csharp
string entryPoint = "Tasks.ExampleBackgroundTaskClass"; // Namespace name, '.', and the name of the class containing the background task
string taskName   = "Internet-based background task";

BackgroundTaskRegistration task = RegisterBackgroundTask(entryPoint, taskName, internetTrigger, exampleCondition);
```

```cppwinrt
std::wstring entryPoint{ L"Tasks.ExampleBackgroundTaskClass" }; // don't set for in-process background tasks.
std::wstring taskName{ L"Internet-based background task" };

Windows::ApplicationModel::Background::BackgroundTaskRegistration task{
    RegisterBackgroundTask(entryPoint, taskName, internetTrigger, exampleCondition) };
```

```cpp
String ^ entryPoint = "Tasks.ExampleBackgroundTaskClass"; // don't set for in-process background tasks
String ^ taskName   = "Internet-based background task";

BackgroundTaskRegistration ^ task = RegisterBackgroundTask(entryPoint, taskName, internetTrigger, exampleCondition);
```

> [!NOTE]
> Universal Windows Platform apps must call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) before registering any of the background trigger types.

To ensure that your Universal Windows app continues to run properly after you release an update, you must call [**RemoveAccess**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.removeaccess) and then call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) when your app launches after being updated. For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

> [!NOTE]
> Background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Ensure that your app gracefully handles scenarios where background task registration fails - if instead your app depends on having a valid registration object after attempting to register a task, it may crash.
 
## Remarks

To see background task registration in action, download the [background task sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask).

Background tasks can run in response to [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTrigger) and [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger) events, but you still need to [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md). You must also call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) before registering any background task type.

Apps can register background tasks that respond to [**TimeTrigger**](/uwp/api/Windows.ApplicationModel.Background.TimeTrigger), [**PushNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger), and [**NetworkOperatorNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.NetworkOperatorNotificationTrigger) events, enabling them to provide real-time communication with the user even when the app is not in the foreground. For more information, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

## Related topics

* [Create and register an out-of-process background task](create-and-register-a-background-task.md)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md)
* [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Register a background task](register-a-background-task.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](/previous-versions/hh974425(v=vs.110))