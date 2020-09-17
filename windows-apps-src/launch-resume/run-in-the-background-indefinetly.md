---
title: Run in the background indefinitely
description: Use the extendedExecutionUnconstrained capability to run a background task or extended execution session in the background indefinitely.
ms.assetid: 6E48B8B6-D3BF-4AE2-85FB-D463C448C9D3
keywords: background task, extended execution, resources, limits, background task
ms.date: 10/03/2017
ms.topic: article


ms.localizationpriority: medium
---
# Run in the background indefinitely

To provide the best experience for users, Windows imposes resource limits on Universal Windows Platform (UWP) apps. Foreground apps are given the most memory and execution time; background apps get less. Users are thus protected from poor foreground app performance and heavy battery drain.

However, developers writing UWP apps for personal use (that is, side loaded apps that won't be published in the Microsoft Store), or developers writing Enterprise UWP apps, may want to use all resources available on the device without any background or extended execution throttling. Line of business and personal UWP applications can use APIs in the Windows Creators Update (version 1703) to turn off throttling. Be aware that you can't put an app into the Microsoft Store if it uses these APIs.

## Run while minimized

UWP apps move to a suspended state when they are not running in the foreground. On desktop, this occurs when a user minimizes the app. Apps use an extended execution session in order to continue running while minimized. The extended execution APIs that are accepted by the Microsoft Store are detailed in [Postpone app suspension with extended execution](./run-minimized-with-extended-execution.md).

If you are developing an app that is not intended to be submitted into the Microsoft Store, then you can use the [ExtendedExecutionForegroundSession](/uwp/api/windows.applicationmodel.extendedexecution.foreground.extendedexecutionforegroundsession) with the `extendedExecutionUnconstrained` restricted capability so that your app can continue to run while minimized, regardless of the energy state of the device.  

The `extendedExecutionUnconstrained` capability is added as a restricted capability in your app's manifest. See [App capability declarations](../packaging/app-capability-declarations.md) for more information about restricted capabilities.

> [!NOTE]
> Add the *xmlns:rescap* XML namespace declaration, and use the *rescap* prefix to declare the capability.
>
> For more information, see the Restricted Capabilities section of [App capability declarations](https://docs.microsoft.com/windows/uwp/packaging/app-capability-declarations).
>

_Package.appxmanifest_

```xml
<Package
    ...
    xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
    IgnorableNamespaces="uap mp rescap">
  ...
  <Capabilities>
    <rescap:Capability Name="extendedExecutionUnconstrained"/>
  </Capabilities>
</Package>
```

When you use the `extendedExecutionUnconstrained` capability, [ExtendedExecutionForegroundSession](/uwp/api/windows.applicationmodel.extendedexecution.foreground.extendedexecutionforegroundsession) and [ExtendedExecutionForegroundReason](/uwp/api/windows.applicationmodel.extendedexecution.foreground.extendedexecutionforegroundreason) are used rather than [ExtendedExecutionSession](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionsession) and [ExtendedExecutionReason](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionreason). The same pattern for creating the session, setting members, and requesting the extension asynchronously still applies: 

```cs
var newSession = new ExtendedExecutionForegroundSession();
newSession.Reason = ExtendedExecutionForegroundReason.Unconstrained;
newSession.Description = "Long Running Processing";
newSession.Revoked += SessionRevoked;
ExtendedExecutionResult result = await newSession.RequestExtensionAsync();
switch (result)
{
    case ExtendedExecutionResult.Allowed:
        DoLongRunningWork();
        break;

    default:
    case ExtendedExecutionResult.Denied:
        DoShortRunningWork();
        break;
}
```

You can request this extended execution session as soon as the app comes to the foreground. Unconstrained extended execution sessions are not limited by energy quotas or by the operating system battery saver. As long as a reference to the session object exists, the app will stay in the running state and not enter the suspended state. If the app is closed by the user, the session will be revoked.

Registering for the **Revoked** event will enable your app to do any cleanup work required. In the suspending state, you can create an extended execution session with   [ExtendedExecutionReason.SavingData](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionreason) to save user data before the app is terminated and removed from memory.

## Run background tasks indefinitely

In the Universal Windows Platform, background tasks are processes that run in the background without any form of user interface. Background tasks may generally run for a maximum of twenty-five seconds before they are cancelled. Some of the longer-running tasks also have a check to ensure that the background task is not sitting idle or using memory. In the Windows Creators Update (version 1703), the [extendedBackgroundTaskTime](../packaging/app-capability-declarations.md) restricted capability was introduced to remove these limits. The **extendedBackgroundTaskTime** capability is added as a restricted capability in your app's manifest file:

> [!NOTE]
> Add the *xmlns:rescap* XML namespace declaration, and use the *rescap* prefix to declare the capability.
>
> For more information, see the Restricted Capabilities section of [App capability declarations](https://docs.microsoft.com/windows/uwp/packaging/app-capability-declarations).
>

_Package.appxmanifest_

```xml
<Package
    ... 
    xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
    IgnorableNamespaces="uap mp rescap">
...
  <Capabilities>
    <rescap:Capability Name="extendedBackgroundTaskTime"/>
  </Capabilities>
</Package>
```

This capability removes execution time limitations and the idle task watchdog. Once a background task has started, whether by a trigger or an app service call, once it takes a deferral on the [BackgroundTaskInstance](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskInstance) provided by the **Run** method, it can run indefinitely. If the app is set to **Managed By Windows**, then it still may have an energy quota applied to it, and its background tasks will not activated when Battery Saver is active. This can be changed with OS settings. More information is available in [Optimizing Background Activity](../debug-test-perf/optimize-background-activity.md).

The Universal Windows Platform monitors background task execution to ensure good battery life and a smooth foreground app experience. However, personal apps and Enterprise line-of-Business apps can use extended execution and the **extendedBackgroundTaskTime** capability to create apps that will run as long as needed regardless of the device's resource availability.

Be aware that the **extendedExecutionUnconstrained** and **extendedBackgroundTaskTime** capabilities can override default policy for UWP apps and may cause significant battery drain. Before using these capabilities, first confirm that the default extended execution and background task time policies do not meet your needs and perform testing in battery-constrained conditions to understand the impact your app will have on a device.

## See also

[Remove background task resource restrictions](/windows/application-management/enterprise-background-activity-controls)