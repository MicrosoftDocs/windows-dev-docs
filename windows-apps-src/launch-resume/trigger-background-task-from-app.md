---
title: Trigger a background task from within your app
description: Learn how to use an application trigger to run a background task that you want to activate from within your app.
ms.date: 07/06/2018
ms.topic: article
keywords: background task trigger, background task
ms.localizationpriority: medium
---
# Trigger a background task from within your app

Learn how to use the [ApplicationTrigger](/uwp/api/Windows.ApplicationModel.Background.ApplicationTrigger) to activate a background task from within your app.

For an example of how to create an Application trigger, see this [example](https://github.com/Microsoft/Windows-universal-samples/blob/v2.0.0/Samples/BackgroundTask/cs/BackgroundTask/Scenario5_ApplicationTriggerTask.xaml.cs).

This topic assumes that you have a background task that you want to activate from your application. If you don't already have a background task, there is a sample background task at [BackgroundActivity.cs](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/BackgroundActivation/cs/BackgroundActivity.cs). Or, follow the steps in [Create and register an out-of-process background task](create-and-register-a-background-task.md) to create one.

## Why use an application trigger

Use an **ApplicationTrigger** to run code in a separate process from the foreground app. An **ApplicationTrigger** is appropriate if your app has work that needs to be done in the background--even if the user closes the foreground app. If background work should halt when the app is closed, or should be tied to the state of the foreground process, then [Extended Execution](run-minimized-with-extended-execution.md) should be used, instead.

## Create an application trigger

Create a new [ApplicationTrigger](/uwp/api/Windows.ApplicationModel.Background.ApplicationTrigger). You could store it in a field as is done in the snippet below. This is for convenience so that we don't have to create a new instance later when we want to signal the trigger. But you can use any **ApplicationTrigger** instance to signal the trigger.

```csharp
// _AppTrigger is an ApplicationTrigger field defined at a scope that will keep it alive
// as long as you need to trigger the background task.
// Or, you could create a new ApplicationTrigger instance and use that when you want to
// trigger the background task.
_AppTrigger = new ApplicationTrigger();
```

```cppwinrt
// _AppTrigger is an ApplicationTrigger field defined at a scope that will keep it alive
// as long as you need to trigger the background task.
// Or, you could create a new ApplicationTrigger instance and use that when you want to
// trigger the background task.
Windows::ApplicationModel::Background::ApplicationTrigger _AppTrigger;
```

```cpp
// _AppTrigger is an ApplicationTrigger field defined at a scope that will keep it alive
// as long as you need to trigger the background task.
// Or, you could create a new ApplicationTrigger instance and use that when you want to
// trigger the background task.
ApplicationTrigger ^ _AppTrigger = ref new ApplicationTrigger();
```

## (Optional) Add a condition

You can create a background task condition to control when the task runs. A condition prevents the background task from running until the condition is met. For more information, see [Set conditions for running a background task](set-conditions-for-running-a-background-task.md).

In this example the condition is set to **InternetAvailable** so that, once triggered, the task only runs once internet access is available. For a list of possible conditions, see [**SystemConditionType**](/uwp/api/Windows.ApplicationModel.Background.SystemConditionType).

```csharp
SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);
```

```cppwinrt
Windows::ApplicationModel::Background::SystemCondition internetCondition{
    Windows::ApplicationModel::Background::SystemConditionType::InternetAvailable };
```

```cpp
SystemCondition ^ internetCondition = ref new SystemCondition(SystemConditionType::InternetAvailable)
```

For more in-depth information on conditions and types of background triggers, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

##  Call RequestAccessAsync()

Before registering the **ApplicationTrigger** background task, call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) to determine the level of background activity the user allows because the user may have disabled background activity for your app. See [Optimize background activity](../debug-test-perf/optimize-background-activity.md) for more information about the ways users can control the settings for background activity.

```csharp
var requestStatus = await Windows.ApplicationModel.Background.BackgroundExecutionManager.RequestAccessAsync();
if (requestStatus != BackgroundAccessStatus.AlwaysAllowed)
{
   // Depending on the value of requestStatus, provide an appropriate response
   // such as notifying the user which functionality won't work as expected
}
```

## Register the background task

Register the background task by calling your background task registration function. For more information on registering background tasks, and to see the definition of the **RegisterBackgroundTask()** method in the sample code below, see [Register a background task](register-a-background-task.md).

If you are considering using an Application Trigger to extend the lifetime of your foreground process, consider using [Extended Execution](run-minimized-with-extended-execution.md) instead. The Application Trigger is designed for creating a separately hosted process to do work in. The following code snippet registers an out-of-process background trigger.

```csharp
string entryPoint = "Tasks.ExampleBackgroundTaskClass";
string taskName   = "Example application trigger";

BackgroundTaskRegistration task = RegisterBackgroundTask(entryPoint, taskName, appTrigger, internetCondition);
```

```cppwinrt
std::wstring entryPoint{ L"Tasks.ExampleBackgroundTaskClass" };
std::wstring taskName{ L"Example application trigger" };

Windows::ApplicationModel::Background::BackgroundTaskRegistration task{
    RegisterBackgroundTask(entryPoint, taskName, appTrigger, internetCondition) };
```

```cpp
String ^ entryPoint = "Tasks.ExampleBackgroundTaskClass";
String ^ taskName   = "Example application trigger";

BackgroundTaskRegistration ^ task = RegisterBackgroundTask(entryPoint, taskName, appTrigger, internetCondition);
```

Background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Ensure that your app gracefully handles scenarios where background task registration fails - if instead your app depends on having a valid registration object after attempting to register a task, it may crash.

## Trigger the background task

Before you trigger the background task, use [BackgroundTaskRegistration](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskRegistration) to verify that the background task is registered. A good time to verify that all of your background tasks are registered is during app launch.

Trigger the background task by calling [ApplicationTrigger.RequestAsync](/uwp/api/windows.applicationmodel.background.applicationtrigger). Any **ApplicationTrigger** instance will do.

Note that **ApplicationTrigger.RequestAsync** can't be called from the background task itself, or when the app is in the background running state (see [App lifecycle](app-lifecycle.md) for more information about application states).
It may return [DisabledByPolicy](/uwp/api/windows.applicationmodel.background.applicationtriggerresult) if the user has set energy or privacy policies that prevent the app from performing background activity.
Also, only one AppTrigger can run at a time. If you attempt to run an AppTrigger while another is already running, the function will return [CurrentlyRunning](/uwp/api/windows.applicationmodel.background.applicationtriggerresult).

```csharp
var result = await _AppTrigger.RequestAsync();
```

## Manage resources for your background task

Use [BackgroundExecutionManager.RequestAccessAsync](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager) to determine if the user has decided that your app’s background activity should be limited. Be aware of your battery usage and only run in the background when it is necessary to complete an action that the user wants. See [Optimize background activity](../debug-test-perf/optimize-background-activity.md) for more information about the ways users can control the settings for background activity.  

- Memory: Tuning your app's memory and energy use is key to ensuring that the operating system will allow your background task to run. Use the [Memory Management APIs](/uwp/api/windows.system.memorymanager) to see how much memory your background task is using. The more memory your background task uses, the harder it is for the OS to keep it running when another app is in the foreground. The user is ultimately in control of all background activity that your app can perform and has visibility on the impact your app has on battery use.  
- CPU time: Background tasks are limited by the amount of wall-clock usage time they get based on trigger type. Background tasks triggered by the Application trigger are limited to about 10 minutes.

See [Support your app with background tasks](support-your-app-with-background-tasks.md) for the resource constraints applied to background tasks.

## Remarks

Starting with Windows 10, it is no longer necessary for the user to add your app to the lock screen in order to utilize background tasks.

A background task will only run using an **ApplicationTrigger** if you have called [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) first.

## Related topics

* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Background task code sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).
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