---
author: TylerMSFT
title: Set conditions for running a background task
description: Learn how to set conditions that control when your background task will run.
ms.assetid: 10ABAC9F-AA8C-41AC-A29D-871CD9AD9471
ms.author: twhitney
ms.date: 08/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Set conditions for running a background task


**Important APIs**

-   [**SystemCondition**](https://msdn.microsoft.com/library/windows/apps/br224834)
-   [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835)
-   [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768)

Learn how to set conditions that control when your background task will run.

Sometimes, background tasks require certain conditions to be met for the background task to succeed. You can specify one or more of the conditions specified by [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835) when registering your background task. The condition will be checked after the trigger has been fired. The background task will then be queued, but it will not run until all the required conditions are satisfied.

Putting conditions on background tasks saves battery life and CPU by preventing tasks from running unnecessarily. For example, if your background task runs on a timer and requires Internet connectivity, add the **InternetAvailable** condition to the [**TaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) before registering the task. This will help prevent the task from using system resources and battery life unnecessarily by only running the background task when the timer has elapsed *and* the Internet is available.

It is also possible to combine multiple conditions by calling **AddCondition** multiple times on the same [**TaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768). Take care not to add conflicting conditions, such as **UserPresent** and **UserNotPresent**.

## Create a SystemCondition object

This topic assumes that you have a background task already associated with your app, and that your app already includes code that creates a [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) object named **taskBuilder**.  See [Create and register an in-process background task](create-and-register-an-inproc-background-task.md) or [Create and register an out-of-process background task](create-and-register-a-background-task.md) if you need to create a background task first.

This topic applies to background tasks that run out-of-process as well as those that run in the same process as the foreground app.

Before adding the condition, create a [**SystemCondition**](https://msdn.microsoft.com/library/windows/apps/br224834) object to represent the condition that must be in effect for a background task to run. In the constructor, specify the condition that must be met with a [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835) enumeration value.

The following code creates a [**SystemCondition**](https://msdn.microsoft.com/library/windows/apps/br224834) object that specifies the **InternetAvailable** condition:

> [!div class="tabbedCodeSnippets"]
> ```cs
> SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);
> ```
> ```cpp
> SystemCondition ^ internetCondition = ref new SystemCondition(SystemConditionType::InternetAvailable);
> ```

## Add the SystemCondition object to your background task

To add the condition, call the [**AddCondition**](https://msdn.microsoft.com/library/windows/apps/br224769) method on the [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) object, and pass it the [**SystemCondition**](https://msdn.microsoft.com/library/windows/apps/br224834) object.

The following code uses **taskBuilder** to add the **InternetAvailable** condition.

> [!div class="tabbedCodeSnippets"]
> ```cs
> taskBuilder.AddCondition(internetCondition);
> ```
> ```cpp
> taskBuilder->AddCondition(internetCondition);
> ```

## Register your background task

Now you can register your background task with the [**Register**](https://msdn.microsoft.com/library/windows/apps/br224772) method, and the background task will not start until the specified condition is met.

The following code registers the task and stores the resulting BackgroundTaskRegistration object:

> [!div class="tabbedCodeSnippets"]
> ```cs
> BackgroundTaskRegistration task = taskBuilder.Register();
> ```
> ```cpp
> BackgroundTaskRegistration ^ task = taskBuilder->Register();
> ```

> **Note**  Universal Windows apps must call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485) before registering any of the background trigger types.

To ensure that your Universal Windows app continues to run properly after you release an update, you must call [**RemoveAccess**](https://msdn.microsoft.com/library/windows/apps/hh700471) and then call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485) when your app launches after being updated. For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

> **Note**  Background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Ensure that your app gracefully handles scenarios where background task registration fails - if instead your app depends on having a valid registration object after attempting to register a task, it may crash.

## Place multiple conditions on your background task

To add multiple conditions, your app makes multiple calls to the [**AddCondition**](https://msdn.microsoft.com/library/windows/apps/br224769) method. These calls must come before task registration to be effective.

> **Note**  Take care not to add conflicting conditions to a background task.
 

The following snippet shows multiple conditions in the context of creating and registering a background task:

> [!div class="tabbedCodeSnippets"]
```cs
> //
> // Set up the background task.
> //
>
> TimeTrigger hourlyTrigger = new TimeTrigger(60, false);
>
> var recurringTaskBuilder = new BackgroundTaskBuilder();
>
> recurringTaskBuilder.Name           = "Hourly background task";
> recurringTaskBuilder.TaskEntryPoint = "Tasks.ExampleBackgroundTaskClass";
> recurringTaskBuilder.SetTrigger(hourlyTrigger);
>
> //
> // Begin adding conditions.
> //
>
> SystemCondition userCondition     = new SystemCondition(SystemConditionType.UserPresent);
> SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);
>
> recurringTaskBuilder.AddCondition(userCondition);
> recurringTaskBuilder.AddCondition(internetCondition);
>
> //
> // Done adding conditions, now register the background task.
> //
>
> BackgroundTaskRegistration task = recurringTaskBuilder.Register();
```
```cpp
> //
> // Set up the background task.
> //
>
> TimeTrigger ^ hourlyTrigger = ref new TimeTrigger(60, false);
>
> auto recurringTaskBuilder = ref new BackgroundTaskBuilder();
>
> recurringTaskBuilder->Name           = "Hourly background task";
> recurringTaskBuilder->TaskEntryPoint = "Tasks.ExampleBackgroundTaskClass";
> recurringTaskBuilder->SetTrigger(hourlyTrigger);
>
> //
> // Begin adding conditions.
> //
>
> SystemCondition ^ userCondition     = ref new SystemCondition(SystemConditionType::UserPresent);
> SystemCondition ^ internetCondition = ref new SystemCondition(SystemConditionType::InternetAvailable);
>
> recurringTaskBuilder->AddCondition(userCondition);
> recurringTaskBuilder->AddCondition(internetCondition);
>
> //
> // Done adding conditions, now register the background task.
> //
>
> BackgroundTaskRegistration ^ task = recurringTaskBuilder->Register();
```

## Remarks


> **Note**  Choose conditions for your background task so that it only runs when it's needed, and doesn't run when it shouldn't. See [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835) for descriptions of the different background task conditions.


## Related topics

****

* [Create and register an out-of-process background task](create-and-register-a-background-task.md)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md)
* [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Register a background task](register-a-background-task.md)
* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](http://go.microsoft.com/fwlink/p/?linkid=254345)

 

 
