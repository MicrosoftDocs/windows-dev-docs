---
title: Working with background tasks in Windows apps
description: Learn how to create and register a background task in your app with the Windows Runtime (WinRT) BackgroundTaskBuilder class.
ms.date: 02/11/2025
ms.topic: concept-article
keywords: windows 10, uwp, windows 11, winui, winrt
ms.localizationpriority: medium
dev_langs:
  - csharp
  - vb
  - cppwinrt
  - cpp
# customer-intent: As a Windows developer, I want to learn how to create and register a background task in my app.
---

# Working with background tasks in Windows apps

Learn how to create and register a background task in your app with the Windows Runtime (WinRT) [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) class.

## Register a background task

See the [BackgroundTask sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask/cs/BackgroundTask) for a complete example of registering a background task in a Universal Windows Platform (UWP) app.

The following example shows the registration of a Win32 COM task that runs on a recurring 15 minute timer.

To register a background task, you must first create a new instance of the [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) class. The `BackgroundTaskBuilder` class is used to create and register background tasks in your app. The following code example demonstrates how to create a new instance of the `BackgroundTaskBuilder` class:

```csharp
using System;
using Windows.ApplicationModel.Background;

public IBackgroundTaskRegistration RegisterBackgroundTaskWithSystem(IBackgroundTrigger trigger, Guid entryPointClsid, string taskName)
{
    BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

    builder.SetTrigger(trigger);
    builder.SetTaskEntryPointClsid(entryPointClsid);

    BackgroundTaskRegistration registration;
    if (builder.Validate())
    {
        registration = builder.Register(taskName);
    }
    else
    {
        registration = null;
    }

    return registration;
}

RegisterBackgroundTaskWithSystem(new TimeTrigger(15, false), typeof(TimeTriggeredTask).GUID, typeof(TimeTriggeredTask).Name);
```

The `RegisterBackgroundTaskWithSystem` method takes three parameters:

- `trigger`: The trigger that will start the background task.
- `entryPointClsid`: The class ID of the background task entry point.
- `taskName`: The name of the background task.

The `RegisterBackgroundTaskWithSystem` method creates a new instance of the `BackgroundTaskBuilder` class and sets the trigger and entry point class ID for the background task. The method then registers the background task with the system.

> [!NOTE]
> This class is not agile, which means that you need to consider its threading model and marshaling behavior. For more info, see [Threading and Marshaling (C++/CX)](/cpp/cppcx/threading-and-marshaling-c-cx) and [Using Windows Runtime objects in a multithreaded environment (.NET)](/windows/uwp/threading-async/using-windows-runtime-objects-in-a-multithreaded-environment).

## Handle modern standby in a background task

The **BackgroundTaskBuilder** and related APIs already allow packaged desktop applications to run background tasks. The API now extends these APIs to enable these appls to execute code in modern standby. The update also adds properties that can be queried by an app to determine if the system will throttle background tasks for the application in modern standby to conserve battery life. This enables scenarios like apps receiving VoIP calls or other push notifications from modern standby.

> [!NOTE]
> "Packaged desktop applications" in this section refers to Win32 applications that have package identity (i.e., are Desktop Bridge or Sparse Signed Packaged applications) and have a main (or wmain) function as their entry point.

The following example shows how an app developer can use the **BackgroundTaskBuilder** API to register at most one task with the specified task name. The sample also shows how to check and opt in the task registration to run in modern standby for the application's most critical tasks.

```csharp
// The following namespace is required for BackgroundTaskBuilder APIs. 
using Windows.ApplicationModel.Background; 

// The following namespace is required for API version checks. 
using Windows.Foundation.Metadata; 

// The following namespace is used for showing Toast Notifications. This 
// namespace requires the Microsoft.Toolkit.Uwp.Notifications NuGet package 
// version 7.0 or greater. 
using Microsoft.Toolkit.Uwp.Notifications; 

// Incoming calls are considered to be critical tasks to the operation of the app. 
const string IncomingCallTaskName = "IncomingCallTask"; 
const string NotificationTaskName = "NotificationTask"; 
const string PrefetchTaskName = "PrefetchTask"; 

public static bool IsAllowedInBackground(BackgroundAccessStatus status) { 
    return ((status != BackgroundAccessStatus.Denied) && 
            (status != BackgroundAccessStatus.DeniedBySystemPolicy) && 
            (status != BackgroundAccessStatus.DeniedByUser) && 
            (status != BackgroundAccessStatus.Unspecified)); 
} 

public async void RegisterTask(IBackgroundTrigger trigger, 
                               Guid entryPointClsid, 
                               string taskName, 
                               bool isRunInStandbyRequested) 
{ 
    var taskBuilder = new BackgroundTaskBuilder(); 
    taskBuilder.SetTrigger(trigger); 
    taskBuilder.SetTaskEntryPointClsid(entryPointClsid); 

    // Only the most critical background work should be allowed to proceed in 
    // modern standby. Additionally, some platforms may not support modern 
    // or running background tasks in modern standby at all. Only attempt to 
    // request modern standby execution if both are true. Requesting network 
    // is necessary when running in modern standby to handle push notifications. 
    if (IsRunInStandbyRequested && taskBuilder.IsRunningTaskInStandbySupported) 
    { 
        var accessStatus = BackgroundExecutionManager.GetAccessStatusForModernStandby(); 
        if (!IsAllowedInBackground(accessStatus) 
        { 
            await BackgroundExecutionManager.RequestAccessKindForModernStandby( 
                    BackgroundAccessRequestKind.AllowedSubjectToSystemPolicy, 
                    "This app wants to receive incoming notifications while your device is asleep"); 
        } 

        accessStatus = BackgroundExecutionManager.GetAccessStatusForModernStandby(); 

        if (IsAllowedInBackground(accessStatus) 
        { 
            taskBuilder.IsRunningTaskInStandbyRequested = true; 
            taskBuilder.IsNetworkRequested = true; 
        } 
    } 

    // Check that the registration is valid before attempting to register. 
    if (taskBuilder.IsRegistrationValid) 
    { 
        // If a task with the specified name already exists, it is unregistered 
        // before a new one is registered. Note this API may still fail from 
        // catastrophic failure (e.g., memory allocation failure). 
        taskBuilder.Register(taskName); 
    } 

    return; 
} 

RegisterTask(new PushNotificationTrigger(), "{INSERT-YOUR-GUID-HERE}", IncomingCallTaskName, true); 
```

## Check if background tasks have exceeded their budget in modern standby

The following sample code shows how an app developer may use the **BackgroundWorkCost.WasApplicationThrottledInStandby** and **BackgroundWorkCost.ApplicationEnergyUseLevel** to monitor and react to having their background tasks exhaust its app budget. The app developer may react by reducing lower priority work being done in modern standby. Note this relies on the code from the previous sample.

```csharp
public async void ReduceBackgroundCost() 
{ 
    BackgroundTaskRegistration callTask; 
    BackgroundTaskRegistration notificationTask; 
    BackgroundTaskRegistration prefetchTask; 

    // Nothing to do if the app was not or will not be throttled. 
    if (!BackgroundWorkCost.WasApplicationThrottledInStandby && 
        (BackgroundWorkCost.ApplicationEnergyUseLevel != StandbyEnergyUseLevel.OverBudget)) 
    { 
        return; 
    } 

    foreach (var task in BackgroundTaskRegistration.AllTasks) 
    { 
        switch (task.Value.Name) { 
        case IncomingCallTaskName: 
            callTask = task.Value; 
            break; 

        case NotificationTaskName: 
            notificationTask = task.Value; 
            break; 

        case PrefetchTaskName: 
            prefetchTask = task.Value; 
            break; 

        default: 
        } 
    } 

    if (callTask.WasTaskThrottledInStandby) 
    { 
        // Unset the throttle flag after acknowledging it so the app can 
        // react to the same task being throttled again in the future. 
        task.Value.WasTaskThrottledInStandby = false; 

        // Notify the user that the notification was missed. 
        new ToastContentBuilder() 
            .AddText("You missed a call") 
            .AddText(task.Value.Name) 
            .Show(); 

        // Because the incoming calls were not activated, demote less notifications 
        // tasks so the calls can be delivered promptly in the future. 
        RegisterTask(notificationTask.Value.Trigger, 
                     typeof(TimeTriggeredTask).GUID, 
                     notificationTask.Value.Name, 
                     false); 
    } 

    // Note that if incoming call tasks were throttled in some previous modern 
    // standby session, the application energy use was over budget for some period. 
    // Demote unimportant tasks like prefetch work to avoid calls and notifications 
    // from being throttled.
    if (callTask.WasTaskThrottledInStandby) ||
        (BackgroundWorkCost.ApplicationEnergyUseLevel == StandbyEnergyUseLevel.OverBudget))
    {
        RegisterTask(prefetchTask.Value.Trigger,
                     typeof(TimeTriggeredTask).GUID,
                     prefetchTask.Value.Name,
                     false);
    }

    return;
}
```

## Monitor background task energy use trends

The following is an incremental end to end update to the following C++WinRT/C# sample code on [GitHub](https://github.com/microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/BackgroundTaskWinMainComSample).

The example shows how you can use the **BackgroundWorkCost.ApplicationEnergyUseTrend** to monitor how your background tasks trend towards exhausting their budget. You can also stop the most expensive background tasks from running in modern standby and prevent background tasks from running in modern standby if their app is using its budget too quickly. This sample relies on code from previous samples.

```csharp
public async void ReduceBackgroundCostPreemptively() 
{ 
    BackgroundTaskRegistration mostExpensiveTask = null; 

    // We can't do anything preemptively since the trend isn't known. 
    if (!BackgroundWorkCost.IsApplicationEnergyUseTrendKnown) 
    { 
        return; 
    } 

    // The app is not trending towards being over budget, so this method can 
    // return early. 
    if ((BackgroundWorkCost.ApplicationEnergyUseTrend != EnergyUseTrend.OverBudget) && 
        (BackgroundWorkCost.ApplicationEnergyUseTrend != EnergyUseTrend.OverHalf)) 
    { 
        return; 
    } 

    // The application is going exceeding its budget very quickly. Demote the 
    // most expensive task that is not the call task before call tasks start being 
    // throttled. 
    if (BackgroundWorkCost.ApplicationEnergyUseTrend == EnergyUseTrend.OverBudget) 
    { 
        foreach (var task in BackgroundTaskRegistration.AllTasks) 
        { 
            if ((task.Value.Name != IncomingCallTaskName) && 
                ((mostExpensiveTask == null) || 
                 (mostExpensiveTask.ApplicationEnergyUseTrendContributionPercentage < 
                  task.Value.ApplicationEnergyUseTrendContributionPercentage))) 
            { 
                mostExpensiveTask = task.Value; 
            } 
        } 
    } 

    if (mostExpensiveTask != null) 
    { 
        RegisterTask(mostExpensiveTask.Trigger, 
                     typeof(TimeTriggeredTask).GUID, 
                     mostExpensiveTask.Name, 
                     false); 
    } 

    // The application is trending toward eventually exceeding its budget. Demote the 
    // least important prefetch task before calls and notifications are throttled. 
    foreach (var task in BackgroundTaskRegistration.AllTasks) 
    { 
        if (task.Value.Name == PrefetchTaskName) { 
            RegisterTask(task.Value.Trigger, 
                         typeof(TimeTriggeredTask).GUID, 
                         task.Value.Name, 
                         false); 
        } 
    } 

    return; 
} 
```

## Background tasks and network connectivity

If your background task requires network connectivity, be aware of the following considerations.

### Network related triggers

- Use a [SocketActivityTrigger](/uwp/api/windows.applicationmodel.background.socketactivitytrigger) to activate the background task when a packet is received and you need to perform a short-lived task. After performing the task, the background task should terminate to save power.
- Use a [ControlChannelTrigger](/uwp/api/windows.networking.sockets.controlchanneltrigger) to activate the background task when a packet is received and you need to perform a long-lived task.

### Network related conditions and flags

- Add the InternetAvailable condition ([BackgroundTaskBuilder.AddCondition](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder.addcondition)) to your background task to delay triggering the background task until the network stack is running. This condition saves power because the background task won't execute until network access is available. This condition does not provide real-time activation.
- Regardless of the trigger you use, set [IsNetworkRequested](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder.isnetworkrequested) on your background task to ensure that the network stays up while the background task runs. This tells the background task infrastructure to keep the network up while the task is executing, even if the device has entered Connected Standby mode. If your background task does not use **IsNetworkRequested**, then your background task will not be able to access the network when in Connected Standby mode.

## Related content

- [BackgroundTaskBuilder class](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder)
- [BackgroundTask sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask/cs/BackgroundTask)
- [Launching Windows apps and managing background tasks](index.md)
