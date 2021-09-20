---
title: Use a maintenance trigger
description: Learn how to use the MaintenanceTrigger class to run lightweight code in the background while the device is plugged in.
ms.assetid: 727D9D84-6C1D-4DF3-B3B0-2204EA4D76DD
ms.date: 07/06/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
- csharp
- cppwinrt
- cpp
---
# Use a maintenance trigger

**Important APIs**

- [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger)
- [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder)
- [**SystemCondition**](/uwp/api/Windows.ApplicationModel.Background.SystemCondition)

Learn how to use the [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger) class to run lightweight code in the background while the device is plugged in.

## Create a maintenance trigger object

This example assumes that you have lightweight code you can run in the background to enhance your app while the device is plugged in. This topic focuses on the [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger), which is similar to [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType).

More information on writing a background task class is available in [Create and register an in-process background task](create-and-register-an-inproc-background-task.md) or [Create and register an out-of-process background task](create-and-register-a-background-task.md).

Create a new [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger) object. The second parameter, *OneShot*, specifies whether the maintenance task will run only once or continue to run periodically. If *OneShot* is set to true, the first parameter (*FreshnessTime*) specifies the number of minutes to wait before scheduling the background task. If *OneShot* is set to false, *FreshnessTime* specifies how often the background task will run.

> [!NOTE]
> If *FreshnessTime* is set to less than 15 minutes, an exception is thrown when attempting to register the background task.

This example code creates a trigger that runs once an hour.

```csharp
uint waitIntervalMinutes = 60;
MaintenanceTrigger taskTrigger = new MaintenanceTrigger(waitIntervalMinutes, false);
```

```cppwinrt
uint32_t waitIntervalMinutes{ 60 };
Windows::ApplicationModel::Background::MaintenanceTrigger taskTrigger{ waitIntervalMinutes, false };
```

```cpp
unsigned int waitIntervalMinutes = 60;
MaintenanceTrigger ^ taskTrigger = ref new MaintenanceTrigger(waitIntervalMinutes, false);
```

## (Optional) Add a condition

- If necessary, create a background task condition to control when the task runs. A condition prevents your background task from running until the condition is met - for more information, see [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)

In this example, the condition is set to **InternetAvailable** so that maintenance runs when the Internet is available (or when it becomes available). For a list of possible background task conditions, see [**SystemConditionType**](/uwp/api/Windows.ApplicationModel.Background.SystemConditionType).

The following code adds a condition to the maintenance task builder:

```csharp
SystemCondition exampleCondition = new SystemCondition(SystemConditionType.InternetAvailable);
```

```cppwinrt
Windows::ApplicationModel::Background::SystemCondition exampleCondition{
    Windows::ApplicationModel::Background::SystemConditionType::InternetAvailable };
```

```cpp
SystemCondition ^ exampleCondition = ref new SystemCondition(SystemConditionType::InternetAvailable);
```

## Register the background task

- Register the background task by calling your background task registration function. For more information on registering background tasks, see [Register a background task](register-a-background-task.md).

The following code registers the maintenance task. Note that it assumes your background task runs in a separate process from your app because it specifies `entryPoint`. If your background task runs in the same process as your app, you do not specify `entryPoint`.

```csharp
string entryPoint = "Tasks.ExampleBackgroundTaskClass";
string taskName   = "Maintenance background task example";

BackgroundTaskRegistration task = RegisterBackgroundTask(entryPoint, taskName, taskTrigger, exampleCondition);
```

```cppwinrt
std::wstring entryPoint{ L"Tasks.ExampleBackgroundTaskClass" };
std::wstring taskName{ L"Maintenance background task example" };

Windows::ApplicationModel::Background::BackgroundTaskRegistration task{
    RegisterBackgroundTask(entryPoint, taskName, taskTrigger, exampleCondition) };
```

```cpp
String ^ entryPoint = "Tasks.ExampleBackgroundTaskClass";
String ^ taskName   = "Maintenance background task example";

BackgroundTaskRegistration ^ task = RegisterBackgroundTask(entryPoint, taskName, taskTrigger, exampleCondition);
```

> [!NOTE]
> For all device families except desktop, if the device becomes low on memory, background tasks may be terminated. If an out of memory exception is not surfaced, or the app does not handle it, then the background task will be terminated without warning and without raising the OnCanceled event. This helps to ensure the user experience of the app in the foreground. Your background task should be designed to handle this scenario.

> [!NOTE]
> Universal Windows Platform apps must call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) before registering any of the background trigger types.

To ensure that your Universal Windows app continues to run properly after you release an update to your app, you must call [**RemoveAccess**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.removeaccess) and then call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) when your app launches after being updated. For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

> [!NOTE]
> Background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Ensure that your app gracefully handles scenarios where background task registration fails - if instead your app depends on having a valid registration object after attempting to register a task, it may crash.

## Related topics

* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).
* [Create and register an out-of-process background task](create-and-register-a-background-task.md)
* [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Register a background task](register-a-background-task.md)
* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](/previous-versions/hh974425(v=vs.110))