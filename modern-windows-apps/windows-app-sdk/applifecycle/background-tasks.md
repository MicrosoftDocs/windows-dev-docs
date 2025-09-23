---
title: Using background tasks in Windows apps
description: Get an overview of using background tasks and learn how to create a new background task in an app with the BackgroundTaskBuilder in Windows App SDK.
ms.date: 07/14/2025
ms.topic: concept-article
keywords: windows 11, winui, background task, app lifecycle, windows app sdk
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn about using background tasks in Windows apps.
---

# Using background tasks in Windows apps

This article provides an overview of using background tasks and describes how to create a new background task in a WinUI 3 app. For information about migrating your UWP apps with background tasks to WinUI, see the Windows App SDK [Background task migration strategy](../migrate-to-windows-app-sdk/guides/background-task-migration-strategy.md).

## BackgroundTaskBuilder in the Windows App SDK

Background tasks are app components that run in the background without a user interface. They can perform actions such as downloading files, syncing data, sending notifications, or updating tiles. They can be triggered by various events, such as time, system changes, user actions, or push notifications. These tasks can get executed when corresponding trigger occurs even when the app is not in running state.

The Windows Runtime (WinRT) [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) was designed for UWP applications, and many of the background task triggers are not supported for full trust COM Components. They are supported only when registered with WinRT components that are launched with a `backgroundtaskhost` process. Due to this, Windows App SDK desktop applications can't directly register the full trust COM components to be launched with background task triggers. They require a workaround of including the WinRT components in the project. The [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) in Windows App SDK API avoids this workaround so WinUI 3 and other desktop applications that use Windows App SDK can register the full trust COM components directly with background tasks.

## Register a background task

The following example registers a background task for a full trust COM component using the Windows App SDK [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder). See the [Background task migration strategy](../migrate-to-windows-app-sdk/guides/background-task-migration-strategy.md) guide for more information.

The C++ code to create and register a background task is as follows:

```cpp
//Using the Windows App SDK API for BackgroundTaskBuilder
winrt::Microsoft::Windows::ApplicationModel::Background::BackgroundTaskBuilder builder;
SystemTrigger trigger = SystemTrigger(SystemTriggerType::TimeZoneChange, false);
auto backgroundTrigger = trigger.as<IBackgroundTrigger>();
builder.SetTrigger(backgroundTrigger);
builder.AddCondition(SystemCondition(SystemConditionType::InternetAvailable));
builder.SetTaskEntryPointClsid(classGuid);
builder.Register(); 
```

To create and register the background task in C#, the code is as follows:

```csharp
//Using the Windows App SDK API for BackgroundTaskBuilder
var builder = new Microsoft.Windows.ApplicationModel.Background.BackgroundTaskBuilder();
var trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, false);
var backgroundTrigger = trigger as IBackgroundTrigger;
builder.SetTrigger(backgroundTrigger);
builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
builder.SetTaskEntryPointClsid(classGuid);
builder.Register();
```

The corresponding package manifest entry for the background task is as follows:

```xml
<Extension Category="windows.backgroundTasks" EntryPoint="Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task">
    <BackgroundTasks>
        <Task Type="general"/>
    </BackgroundTasks>
</Extension>
```

A full WinUI 3 background task registration sample can be found on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/release/experimental/Samples/BackgroundTask).

## Related content

- [Guidelines for background tasks in UWP apps](/windows/uwp/launch-resume/guidelines-for-background-tasks)
- [Background task migration strategy](../migrate-to-windows-app-sdk/guides/background-task-migration-strategy.md)
