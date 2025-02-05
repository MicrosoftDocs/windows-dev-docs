---
title: Using background tasks in Windows apps
description: This article provides an overview of using background tasks and describes how to create a new background task in a WinUI app with the Windows App SDK BackgroundTaskBuilder APIs.
ms.date: 01/06/2025
ms.topic: concept-article
keywords: windows 11, winui, background task, app lifecycle, windows app sdk
ms.localizationpriority: medium
# Customer intent: As a Windows developer, I want to learn about using background tasks in Windows apps.
---

# Using background tasks in Windows apps

This article provides an overview of using background tasks and describes how to create a new background task in a WinUI app.

## BackgroundTaskBuilder in the Windows App SDK

Background tasks are app components that run in the background without a user interface. They can perform actions such as downloading files, syncing data, sending notifications, or updating tiles. They can be triggered by various events, such as time, system changes, user actions, or push notifications. These tasks can get executed when corresponding trigger occurs even when the app is not in running state.

The previous [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) was designed for UWP applications, and many of the background task triggers are not supported for full trust COM Components. They are supported only to be registered with Windows Runtime (WinRT) components that are to be launched with a **BackgroundTaskHost** process. Due to this, Windows App SDK desktop applications can't directly register the full trust COM components to be launched with background task triggers. They have to do a workaround of including the WinRT components in the project. This API avoids this workaround so WinUI and other desktop applications that use Windows App SDK can register the full trust COM components with the background tasks directly.

## Register a background task

The following code registers a background task for full trust COM component. You'll also need to set the **WindowsAppSDKBackgroundTask** property to **true** in the project configuration. Additionally, in the manifest file, the **EntryPoint** for **BackgroundTask** must be set to **Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task**.

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

## Related content

[Guidelines for background tasks in UWP apps](/windows/uwp/launch-resume/guidelines-for-background-tasks)
