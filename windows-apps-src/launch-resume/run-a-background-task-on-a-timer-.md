---
author: TylerMSFT
title: Run a background task on a timer
description: Learn how to schedule a one-time background task, or run a periodic background task.
ms.assetid: 0B7F0BFF-535A-471E-AC87-783C740A61E9
ms.author: twhitney
ms.date: 03/31/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Run a background task on a timer

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

**Important APIs**

-   [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768)
-   [**TimeTrigger**](https://msdn.microsoft.com/library/windows/apps/br224843)
-   [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700494)

Learn how to schedule a one-time background task, or run a periodic background task.

-   This example assumes that you have a background task that needs to run periodically, or at a specific time, to support your app. A background task will only run using a [**TimeTrigger**](https://msdn.microsoft.com/library/windows/apps/br224843) if you have called [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485).
-   This topic assumes you have already created a background task class. To get started quickly building a background task, see [Create and register an in-process background task](create-and-register-an-inproc-background-task.md) or [Create and register an out-of-process background task](create-and-register-a-background-task.md). For more in-depth information on conditions and triggers, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

## Create a time trigger

-   Create a new [**TimeTrigger**](https://msdn.microsoft.com/library/windows/apps/br224843). The second parameter, *OneShot*, specifies whether the background task will run only once or keep running periodically. If *OneShot* is set to true, the first parameter (*FreshnessTime*) specifies the number of minutes to wait before scheduling the background task. If *OneShot* is set to false, *FreshnessTime* specifies the frequency at which the background task will run.

    The built-in timer for Universal Windows Platform (UWP) apps that target the desktop or mobile device family runs background tasks in 15-minute intervals.

    -   If *FreshnessTime* is set to 15 minutes and *OneShot* is true, the task will be scheduled to run once starting between 15 and 30 minutes from the time it is registered. If it is set to 25 minutes and *OneShot* is true, the task will be scheduled to run once starting between 25 and 40 minutes from the time it is registered.

    -   If *FreshnessTime* is set to 15 minutes and *OneShot* is false, the task will be scheduled to run every 15 minutes starting between 15 and 30 minutes from the time it is registered. If it is set to n minutes and *OneShot* is false, the task will be scheduled to run every n minutes starting between n and n + 15 minutes after it is registered.

    **Note**  If *FreshnessTime* is set to less than 15 minutes, an exception is thrown when attempting to register the background task.
 

For example, this trigger will cause a background task to run once an hour:

> [!div class="tabbedCodeSnippets"]
> ```cs
> TimeTrigger hourlyTrigger = new TimeTrigger(60, false);
> ```
> ```cpp
> TimeTrigger ^ hourlyTrigger = ref new TimeTrigger(60, false);
> ```

## (Optional) Add a condition

-   If necessary, create a background task condition to control when the task runs. A condition prevents the background task from running until the condition is met - for more information, see [Set conditions for running a background task](set-conditions-for-running-a-background-task.md).

    In this example the condition is set to **UserPresent** so that, once triggered, the task only runs once the user is active. For a list of possible conditions, see [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835).

> [!div class="tabbedCodeSnippets"]
> ```cs
> SystemCondition userCondition = new SystemCondition(SystemConditionType.UserPresent);
> ```
> ```cpp
> SystemCondition ^ userCondition = ref new SystemCondition(SystemConditionType::UserPresent)
> ```

##  Call RequestAccessAsync()

-   Before trying to register the [**TimeTrigger**](https://msdn.microsoft.com/library/windows/apps/br224843) background task, call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700494).

> [!div class="tabbedCodeSnippets"]
> ```cs
> await Windows.ApplicationModel.Background.BackgroundExecutionManager.RequestAccessAsync();
> ```

## Register the background task

-   Register the background task by calling your background task registration function. For more information on registering background tasks, see [Register a background task](register-a-background-task.md).

> [!Important]
> For background tasks that run in the same process as your app, do not set `entryPoint`
> For background tasks that run in a separate process from your app, set `entryPoint` to be the namespace, '.', and name of the class that contains your background task implementation.

    The following code registers a background task that runs out-of-process:

> [!div class="tabbedCodeSnippets"]
> ```cs
> string entryPoint = "Tasks.ExampleBackgroundTaskClass";
> string taskName   = "Example hourly background task";
>
> BackgroundTaskRegistration task = RegisterBackgroundTask(entryPoint, taskName, hourlyTrigger, userCondition);
> ```
> ```cpp
> String ^ entryPoint = "Tasks.ExampleBackgroundTaskClass";
> String ^ taskName   = "Example hourly background task";
>
> BackgroundTaskRegistration ^ task = RegisterBackgroundTask(entryPoint, taskName, hourlyTrigger, userCondition);
> ```

> **Note**  Background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Ensure that your app gracefully handles scenarios where background task registration fails - if instead your app depends on having a valid registration object after attempting to register a task, it may crash.


## Remarks

> **Note**  Starting with Windows 10, it is no longer necessary for the user to add your app to the lock screen in order to utilize background tasks. For guidance on the types of background task triggers, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

> **Note**  This article is for Windows 10 developers writing Universal Windows Platform (UWP) apps. If you’re developing for Windows 8.x or Windows Phone 8.x, see the [archived documentation](http://go.microsoft.com/fwlink/p/?linkid=619132).

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
* [Use a maintenance trigger](use-a-maintenance-trigger.md)
* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in Windows Store apps (when debugging)](http://go.microsoft.com/fwlink/p/?linkid=254345)
