---
title: Background task migration strategy
description: Considerations for approaching the migration process and how to migrate your UWP background tasks to use the Windows App SDK BackgroundTaskBuilder.
ms.topic: concept-article
ms.date: 05/28/2026
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn about the considerations and strategies for migrating my UWP background tasks to use the Windows App SDK background task APIs.
---

# Background task migration strategy

Background tasks are used heavily in UWP apps for performing jobs when a particular trigger is invoked on the machine. As these don’t require any service to be running listening for the triggers, it's very power efficient. So, while migrating UWP apps to WinUI 3 and other desktop apps that use Windows App SDK, developers may require support for implementing background tasks on the platform.

Background tasks offered in the UWP app model can either be out-of-proc or in-proc. This article describes the migration strategy for each of these types when moving to [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) APIs in Windows App SDK.

## Out-of-proc background tasks

In the case of out-of-proc background tasks in UWP, background tasks will be written as a Windows Runtime (WinRT) component, and the component is set as the [TaskEntryPoint](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder.taskentrypoint) on [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) during registration. While migrating to Windows App SDK, developers can keep this as-is and package the WinRT component together with the desktop project. In this case, the broker infrastructure will be launching `backgroundtaskhost` process when the trigger is invoked, and the WinRT component background task would be executed in the process. In this approach, the background task would be running in a Low Integrity Level (IL) process (`backgroundtaskhost.exe`) while the main desktop project would be running in a Medium IL process.

If the application needs to run a background task in a medium IL process itself, the full trust COM component needs to be used for background tasks. In that scenario, developers must use the Windows App SDK [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) to register the full trust COM component. Note that the [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) API from the **Windows.ApplicationModel.Background** namespace will throw exceptions while registering of some of the triggers.

A full WinUI background task registration sample can be found on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/BackgroundTask).

More details on the implementation are available in [Create and register a Win32 background task](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task). The only required change would be to replace the WinRT [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) API with the Windows App SDK API [Microsoft.Windows.ApplicationModel.Background.BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder). For registration code examples, see the [Windows App SDK BackgroundTaskBuilder API](#windows-app-sdk-backgroundtaskbuilder-api) section below.

## In-proc background tasks

For in-proc background tasks in UWP, background task routines are implemented in the [OnBackgroundActivated](/uwp/api/windows.ui.xaml.application.onbackgroundactivated) callback which runs as part of the foreground process. This won't be possible in a WinUI application as the **OnBackgroundActivated** callbacks aren't available. The application needs to move the background task implementations to full trust COM tasks as described above and define the COM server in the package manifest to handle the COM activation of the task. When the trigger occurs, COM activation will happen on the corresponding [COM Coclass](/windows/uwp/cpp-and-winrt-apis/author-coclasses#implement-the-coclass-and-class-factory) registered for the trigger.

A full WinUI background task registration sample can be found on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/BackgroundTask).

More details on the implementation are available in [Create and register a Win32 background task](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task). The only change would be to replace the WinRT [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) API with the Windows App SDK APIs in [Microsoft.Windows.ApplicationModel.Background.BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder).

## Windows App SDK BackgroundTaskBuilder API

There are two different versions of the **BackgroundTaskBuilder** API. The [Windows.ApplicationModel.BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) API was designed for UWP applications, and many of the background task triggers are not supported for full trust COM Components. They are supported only when registered with WinRT components that are launched with a `backgroundtaskhost` process. Due to this, Windows App SDK desktop applications can't directly register the full trust COM components to be launched with background task triggers. They require a workaround of including the WinRT components in the project. The [Microsoft.Windows.ApplicationModel.BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) API included in the Windows App SDK avoids this workaround so WinUI 3 and other desktop applications that use Windows App SDK can register the full trust COM components directly with background tasks.


The following code shows how to register background task using Windows App SDK [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) APIs:

```cpp
//Using Windows App SDK API for BackgroundTaskBuilder
winrt::Microsoft::Windows::ApplicationModel::Background::BackgroundTaskBuilder builder;
SystemTrigger trigger = SystemTrigger(SystemTriggerType::TimeZoneChange, false);
auto backgroundTrigger = trigger.as<IBackgroundTrigger>();
builder.SetTrigger(backgroundTrigger);
builder.AddCondition(SystemCondition(SystemConditionType::InternetAvailable));
builder.SetTaskEntryPointClsid(classGuid);
builder.Register();
```

The following is the equivalent C# code:

```csharp
// Using Windows App SDK API for BackgroundTaskBuilder
var builder = new Microsoft.Windows.ApplicationModel.Background.BackgroundTaskBuilder();
var trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, false);
builder.SetTrigger(trigger);
builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
builder.SetTaskEntryPointClsid(classGuid);
builder.Register();
```

To use this API, add the tag below to the project file to enable Windows App SDK background tasks:

``` xml
<WindowsAppSDKBackgroundTask>true</WindowsAppSDKBackgroundTask>
```

Also, in the manifest file, the **EntryPoint** for **BackgroundTask** is set to `Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task`:

``` xml
<Extension Category="windows.backgroundTasks" EntryPoint="Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task">
    <BackgroundTasks>
        <Task Type="general"/>
    </BackgroundTasks>
</Extension>
```

For C# applications, an **ActivatableClass** registration should also be added to the manifest file:

```xml
<Extension Category="windows.activatableClass.inProcessServer">
    <InProcessServer>
        <Path>Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.dll</Path>
        <ActivatableClass ActivatableClassId="Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task" ThreadingModel="both"/>
    </InProcessServer>
</Extension>
```

## Leveraging TaskScheduler for background task migration

[Task Scheduler](/windows/win32/api/_taskschd/) helps desktop apps achieve the same functionality that is provided by [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) in UWP apps. More details on implementations using **TaskScheduler** are available in the [Task Scheduler API reference](/windows/win32/api/taskschd/).

## ApplicationTrigger use in Windows App SDK applications

[ApplicationTrigger](/uwp/api/windows.applicationmodel.background.applicationtrigger) is supported in UWP applications due to the lifetime management scenario where the application process can be suspended. This scenario does not occur for WinUI and other Windows App SDK desktop applications, so this trigger is not supported in WinUI applications. All logic related to **ApplicationTrigger** will need to be rewritten to execute by launching another process or by running in a thread pool thread. For more information, see [CreateThread](/windows/win32/api/processthreadsapi/nf-processthreadsapi-createthread) and [CreateProcess](/windows/win32/api/processthreadsapi/nf-processthreadsapi-createprocessa).

## Related content

- [Using background tasks in Windows apps](/windows/apps/windows-app-sdk/applifecycle/background-tasks)
- [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder)
