---
title: Run a background task on a timer
description: Learn how to schedule a one-time background task, or run a periodic background task.
ms.assetid: 0B7F0BFF-535A-471E-AC87-783C740A61E9
ms.date: 07/06/2018
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
---
# Run a background task on a timer

Learn how to use the [**TimeTrigger**](/uwp/api/Windows.ApplicationModel.Background.TimeTrigger) to schedule a one-time background task, or run a periodic background task.

See **Scenario4** in the [Background activation sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundActivation) to see an example of how to implement the time triggered background task described in this topic.

This topic assumes that you have a background task that needs to run periodically, or at a specific time. If you don't already have a background task, there is a sample background task at [BackgroundActivity.cs](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/BackgroundActivation/cs/BackgroundActivity.cs). Or, follow the steps in [Create and register an in-process background task](create-and-register-an-inproc-background-task.md) or [Create and register an out-of-process background task](create-and-register-a-background-task.md) to create one.

## Create a time trigger

Create a new [**TimeTrigger**](/uwp/api/Windows.ApplicationModel.Background.TimeTrigger). The second parameter, *OneShot*, specifies whether the background task will run only once or keep running periodically. If *OneShot* is set to true, the first parameter (*FreshnessTime*) specifies the number of minutes to wait before scheduling the background task. If *OneShot* is set to false, *FreshnessTime* specifies the frequency at which the background task will run.

The built-in timer for Universal Windows Platform (UWP) apps that target the desktop or mobile device family runs background tasks in 15-minute intervals. (The timer runs in 15-minute intervals so that the system only needs to wake once every 15 minutes to wake up the apps that have requested TimerTriggers--which saves power.)

- If *FreshnessTime* is set to 15 minutes and *OneShot* is true, the task will be scheduled to run once starting between 15 and 30 minutes from the time it is registered. If it is set to 25 minutes and *OneShot* is true, the task will be scheduled to run once starting between 25 and 40 minutes from the time it is registered.

- If *FreshnessTime* is set to 15 minutes and *OneShot* is false, the task will be scheduled to run every 15 minutes starting between 15 and 30 minutes from the time it is registered. If it is set to n minutes and *OneShot* is false, the task will be scheduled to run every n minutes starting between n and n + 15 minutes after it is registered.

> [!NOTE]
> If *FreshnessTime* is set to less than 15 minutes, an exception is thrown when attempting to register the background task.

For example, this trigger will cause a background task to run once an hour.

```cs
TimeTrigger hourlyTrigger = new TimeTrigger(60, false);
```

```cppwinrt
Windows::ApplicationModel::Background::TimeTrigger hourlyTrigger{ 60, false };
```

```cpp
TimeTrigger ^ hourlyTrigger = ref new TimeTrigger(60, false);
```

## (Optional) Add a condition

You can create a background task condition to control when the task runs. A condition prevents the background task from running until the condition is met. For more information, see [Set conditions for running a background task](set-conditions-for-running-a-background-task.md).

In this example the condition is set to **UserPresent** so that, once triggered, the task only runs once the user is active. For a list of possible conditions, see [**SystemConditionType**](/uwp/api/Windows.ApplicationModel.Background.SystemConditionType).

```cs
SystemCondition userCondition = new SystemCondition(SystemConditionType.UserPresent);
```

```cppwinrt
Windows::ApplicationModel::Background::SystemCondition userCondition{
    Windows::ApplicationModel::Background::SystemConditionType::UserPresent };
```

```cpp
SystemCondition ^ userCondition = ref new SystemCondition(SystemConditionType::UserPresent);
```

For more in-depth information on conditions and types of background triggers, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

##  Call RequestAccessAsync()

Before registering the **ApplicationTrigger** background task, call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) to determine the level of background activity the user allows because the user may have disabled background activity for your app. See [Optimize background activity](../debug-test-perf/optimize-background-activity.md) for more information about the ways users can control the settings for background activity.

```cs
var requestStatus = await Windows.ApplicationModel.Background.BackgroundExecutionManager.RequestAccessAsync();
if (requestStatus != BackgroundAccessStatus.AlwaysAllowed)
{
    // Depending on the value of requestStatus, provide an appropriate response
    // such as notifying the user which functionality won't work as expected
}
```

## Register the background task

Register the background task by calling your background task registration function. For more information on registering background tasks, and to see the definition of the **RegisterBackgroundTask()** method in the sample code below, see [Register a background task](register-a-background-task.md).

> [!IMPORTANT]
> For background tasks that run in the same process as your app, do not set `entryPoint`. For background tasks that run in a separate process from your app, set `entryPoint` to be the namespace, '.', and the name of the class that contains your background task implementation.

```cs
string entryPoint = "Tasks.ExampleBackgroundTaskClass";
string taskName   = "Example hourly background task";

BackgroundTaskRegistration task = RegisterBackgroundTask(entryPoint, taskName, hourlyTrigger, userCondition);
```

```cppwinrt
std::wstring entryPoint{ L"Tasks.ExampleBackgroundTaskClass" };
std::wstring taskName{ L"Example hourly background task" };

Windows::ApplicationModel::Background::BackgroundTaskRegistration task{
    RegisterBackgroundTask(entryPoint, taskName, hourlyTrigger, userCondition) };
```

```cpp
String ^ entryPoint = "Tasks.ExampleBackgroundTaskClass";
String ^ taskName   = "Example hourly background task";

BackgroundTaskRegistration ^ task = RegisterBackgroundTask(entryPoint, taskName, hourlyTrigger, userCondition);
```

Background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Ensure that your app gracefully handles scenarios where background task registration fails - if instead your app depends on having a valid registration object after attempting to register a task, it may crash.

## Manage resources for your background task

Use [BackgroundExecutionManager.RequestAccessAsync](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager) to determine if the user has decided that your app’s background activity should be limited. Be aware of your battery usage and only run in the background when it is necessary to complete an action that the user wants. See [Optimize background activity](../debug-test-perf/optimize-background-activity.md) for more information about the ways users can control the settings for background activity.

- Memory: Tuning your app's memory and energy use is key to ensuring that the operating system will allow your background task to run. Use the [Memory Management APIs](/uwp/api/windows.system.memorymanager) to see how much memory your background task is using. The more memory your background task uses, the harder it is for the OS to keep it running when another app is in the foreground. The user is ultimately in control of all background activity that your app can perform and has visibility on the impact your app has on battery use.  
- CPU time: Background tasks are limited by the amount of wall-clock usage time they get based on trigger type.

See [Support your app with background tasks](support-your-app-with-background-tasks.md) for the resource constraints applied to background tasks.

## Remarks

Starting with Windows 10, it is no longer necessary for the user to add your app to the lock screen in order to utilize background tasks.

A background task will only run using a **TimeTrigger** if you have called [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) first.

## Related topics

* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Background task code sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md)
* [Create and register an out-of-process background task](create-and-register-a-background-task.md)
* [Debug a background task](debug-a-background-task.md)
* [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md)
* [Free memory when your app moves to the background](reduce-memory-usage.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](/previous-versions/hh974425(v=vs.110))
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Postpone app suspension with extended execution](run-minimized-with-extended-execution.md)
* [Register a background task](register-a-background-task.md)
* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)