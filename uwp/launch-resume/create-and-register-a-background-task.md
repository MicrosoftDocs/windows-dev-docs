---
title: Create and register an out-of-process background task
description: Create an out-of-process background task class and register it to run when your app is not in the foreground.
ms.assetid: 4F98F6A3-0D3D-4EFB-BA8E-30ED37AE098B
ms.date: 02/27/2019
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
dev_langs:
- csharp
- cppwinrt
- cpp
---

# Create and register an out-of-process background task

**Important APIs**

-   [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask)
-   [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder)
-   [**BackgroundTaskCompletedEventHandler**](/uwp/api/windows.applicationmodel.background.backgroundtaskcompletedeventhandler)

Create a background task class and register it to run when your app is not in the foreground. This topic demonstrates how to create and register a background task that runs in a separate process than your app's process. To do background work directly in the foreground application, see [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).

> [!NOTE]
> If you use a background task to play media in the background, see [Play media in the background](../audio-video-camera/background-audio.md) for information about improvements in Windows 10, version 1607, that make it much easier.

> [!NOTE]
> If you are implementing an out-of-process background task in a C# desktop application with .NET 6 or later, then use the [C#/WinRT authoring support](/windows/apps/develop/platform/csharp-winrt/authoring) to create a Windows Runtime Component. This applies for apps using the Windows App SDK, WinUI 3, WPF, or WinForms. See the [Background task sample](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/BgTaskComponent) for an example.

## Create the Background Task class

You can run code in the background by writing classes that implement the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface. This code runs when a specific event is triggered by using, for example, [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType) or [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger).

The following steps show you how to write a new class that implements the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface.

1.  Create a new project for background tasks and add it to your solution. To do this, right-click on your solution node in the **Solution Explorer** and select **Add** \> **New Project**. Then select the **Windows Runtime Component** project type, name the project, and click OK.
2.  Reference the background tasks project from your Universal Windows Platform (UWP) app project. For a C# or C++ app, in your app project, right-click on **References** and select **Add New Reference**. Under **Solution**, select **Projects** and then select the name of your background task project and click **Ok**.
3.  To the background tasks project, add a new class that implements the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface. The [**IBackgroundTask.Run**](/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method is a required entry point that will be called when the specified event is triggered; this method is required in every background task.

> [!NOTE]
> The background task class itself&mdash;and all other classes in the background task project&mdash;need to be **public** classes that are **sealed** (or **final**).

The following sample code shows a very basic starting point for a background task class.

```csharp
// ExampleBackgroundTask.cs
using Windows.ApplicationModel.Background;

namespace Tasks
{
    public sealed class ExampleBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            
        }        
    }
}
```

```cppwinrt
// First, add ExampleBackgroundTask.idl, and then build.
// ExampleBackgroundTask.idl
namespace Tasks
{
    [default_interface]
    runtimeclass ExampleBackgroundTask : Windows.ApplicationModel.Background.IBackgroundTask
    {
        ExampleBackgroundTask();
    }
}

// ExampleBackgroundTask.h
#pragma once

#include "ExampleBackgroundTask.g.h"

namespace winrt::Tasks::implementation
{
    struct ExampleBackgroundTask : ExampleBackgroundTaskT<ExampleBackgroundTask>
    {
        ExampleBackgroundTask() = default;

        void Run(Windows::ApplicationModel::Background::IBackgroundTaskInstance const& taskInstance);
    };
}

namespace winrt::Tasks::factory_implementation
{
    struct ExampleBackgroundTask : ExampleBackgroundTaskT<ExampleBackgroundTask, implementation::ExampleBackgroundTask>
    {
    };
}

// ExampleBackgroundTask.cpp
#include "pch.h"
#include "ExampleBackgroundTask.h"

namespace winrt::Tasks::implementation
{
    void ExampleBackgroundTask::Run(Windows::ApplicationModel::Background::IBackgroundTaskInstance const& taskInstance)
    {
        throw hresult_not_implemented();
    }
}
```

```cpp
// ExampleBackgroundTask.h
#pragma once

using namespace Windows::ApplicationModel::Background;

namespace Tasks
{
    public ref class ExampleBackgroundTask sealed : public IBackgroundTask
    {

    public:
        ExampleBackgroundTask();

        virtual void Run(IBackgroundTaskInstance^ taskInstance);
        void OnCompleted(
            BackgroundTaskRegistration^ task,
            BackgroundTaskCompletedEventArgs^ args
        );
    };
}

// ExampleBackgroundTask.cpp
#include "ExampleBackgroundTask.h"

using namespace Tasks;

void ExampleBackgroundTask::Run(IBackgroundTaskInstance^ taskInstance)
{
}
```

4.  If you run any asynchronous code in your background task, then your background task needs to use a deferral. If you don't use a deferral, then the background task process can terminate unexpectedly if the **Run** method returns before any asynchronous work has run to completion.

Request the deferral in the **Run** method before calling the asynchronous method. Save the deferral to a class data member so that it can be accessed from the asynchronous method. Declare the deferral complete after the asynchronous code completes.

The following sample code gets the deferral, saves it, and releases it when the asynchronous code is complete.

```csharp
BackgroundTaskDeferral _deferral; // Note: defined at class scope so that we can mark it complete inside the OnCancel() callback if we choose to support cancellation
public async void Run(IBackgroundTaskInstance taskInstance)
{
    _deferral = taskInstance.GetDeferral();
    //
    // TODO: Insert code to start one or more asynchronous methods using the
    //       await keyword, for example:
    //
    // await ExampleMethodAsync();
    //

    _deferral.Complete();
}
```

```cppwinrt
// ExampleBackgroundTask.h
...
private:
    Windows::ApplicationModel::Background::BackgroundTaskDeferral m_deferral{ nullptr };

// ExampleBackgroundTask.cpp
...
Windows::Foundation::IAsyncAction ExampleBackgroundTask::Run(
    Windows::ApplicationModel::Background::IBackgroundTaskInstance const& taskInstance)
{
    m_deferral = taskInstance.GetDeferral(); // Note: defined at class scope so that we can mark it complete inside the OnCancel() callback if we choose to support cancellation.
    // TODO: Modify the following line of code to call a real async function.
    co_await ExampleCoroutineAsync(); // Run returns at this point, and resumes when ExampleCoroutineAsync completes.
    m_deferral.Complete();
}
```

```cpp
void ExampleBackgroundTask::Run(IBackgroundTaskInstance^ taskInstance)
{
    m_deferral = taskInstance->GetDeferral(); // Note: defined at class scope so that we can mark it complete inside the OnCancel() callback if we choose to support cancellation.

    //
    // TODO: Modify the following line of code to call a real async function.
    //       Note that the task<void> return type applies only to async
    //       actions. If you need to call an async operation instead, replace
    //       task<void> with the correct return type.
    //
    task<void> myTask(ExampleFunctionAsync());

    myTask.then([=]() {
        m_deferral->Complete();
    });
}
```

> [!NOTE]
> In C#, your background task's asynchronous methods can be called using the **async/await** keywords. In C++/CX, a similar result can be achieved by using a task chain.

For more information about asynchronous patterns, see [Asynchronous programming](../threading-async/asynchronous-programming-universal-windows-platform-apps.md). For additional examples of how to use deferrals to keep a background task from stopping early, see the [background task sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask).

The following steps are completed in one of your app classes (for example, MainPage.xaml.cs).

> [!NOTE]
> You can also create a function dedicated to registering background tasks&mdash;see [Register a background task](register-a-background-task.md). In that case, instead of using the next three steps, you can simply construct the trigger and provide it to the registration function along with the task name, task entry point, and (optionally) a condition.

## Register the background task to run

1.  Find out whether the background task is already registered by iterating through the [**BackgroundTaskRegistration.AllTasks**](/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.alltasks) property. This step is important; if your app doesn't check for existing background task registrations, it could easily register the task multiple times, causing issues with performance and maxing out the task's available CPU time before work can complete.

The following example iterates on the **AllTasks** property and sets a flag variable to true if the task is already registered.

```csharp
var taskRegistered = false;
var exampleTaskName = "ExampleBackgroundTask";

foreach (var task in BackgroundTaskRegistration.AllTasks)
{
    if (task.Value.Name == exampleTaskName)
    {
        taskRegistered = true;
        break;
    }
}
```

```cppwinrt
std::wstring exampleTaskName{ L"ExampleBackgroundTask" };

auto allTasks{ Windows::ApplicationModel::Background::BackgroundTaskRegistration::AllTasks() };

bool taskRegistered{ false };
for (auto const& task : allTasks)
{
    if (task.Value().Name() == exampleTaskName)
    {
        taskRegistered = true;
        break;
    }
}

// The code in the next step goes here.
```

```cpp
boolean taskRegistered = false;
Platform::String^ exampleTaskName = "ExampleBackgroundTask";

auto iter = BackgroundTaskRegistration::AllTasks->First();
auto hascur = iter->HasCurrent;

while (hascur)
{
    auto cur = iter->Current->Value;

    if(cur->Name == exampleTaskName)
    {
        taskRegistered = true;
        break;
    }

    hascur = iter->MoveNext();
}
```

2.  If the background task is not already registered, use [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) to create an instance of your background task. The task entry point should be the name of your background task class prefixed by the namespace.

The background task trigger controls when the background task will run. For a list of possible triggers, see [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType).

For example, this code creates a new background task and sets it to run when the **TimeZoneChanged** trigger occurs:

```csharp
var builder = new BackgroundTaskBuilder();

builder.Name = exampleTaskName;
builder.TaskEntryPoint = "Tasks.ExampleBackgroundTask";
builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
```

```cppwinrt
if (!taskRegistered)
{
    Windows::ApplicationModel::Background::BackgroundTaskBuilder builder;
    builder.Name(exampleTaskName);
    builder.TaskEntryPoint(L"Tasks.ExampleBackgroundTask");
    builder.SetTrigger(Windows::ApplicationModel::Background::SystemTrigger{
        Windows::ApplicationModel::Background::SystemTriggerType::TimeZoneChange, false });
    // The code in the next step goes here.
}
```

```cpp
auto builder = ref new BackgroundTaskBuilder();

builder->Name = exampleTaskName;
builder->TaskEntryPoint = "Tasks.ExampleBackgroundTask";
builder->SetTrigger(ref new SystemTrigger(SystemTriggerType::TimeZoneChange, false));
```

3.  You can add a condition to control when your task will run after the trigger event occurs (optional). For example, if you don't want the task to run until the user is present, use the condition **UserPresent**. For a list of possible conditions, see [**SystemConditionType**](/uwp/api/Windows.ApplicationModel.Background.SystemConditionType).

The following sample code assigns a condition requiring the user to be present:

```csharp
builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
```

```cppwinrt
builder.AddCondition(Windows::ApplicationModel::Background::SystemCondition{ Windows::ApplicationModel::Background::SystemConditionType::UserPresent });
// The code in the next step goes here.
```

```cpp
builder->AddCondition(ref new SystemCondition(SystemConditionType::UserPresent));
```

4.  Register the background task by calling the Register method on the [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) object. Store the [**BackgroundTaskRegistration**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskRegistration) result so it can be used in the next step.

The following code registers the background task and stores the result:

```csharp
BackgroundTaskRegistration task = builder.Register();
```

```cppwinrt
Windows::ApplicationModel::Background::BackgroundTaskRegistration task{ builder.Register() };
```

```cpp
BackgroundTaskRegistration^ task = builder->Register();
```

> [!NOTE]
> Universal Windows apps must call [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) before registering any of the background trigger types.

To ensure that your Universal Windows app continues to run properly after you release an update, use the **ServicingComplete** (see [SystemTriggerType](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType)) trigger to perform any post-update configuration changes such as migrating the app's database and registering background tasks. It is best practice to unregister background tasks associated with the previous version of the app (see [**RemoveAccess**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.removeaccess)) and register background tasks for the new version of the app (see [**RequestAccessAsync**](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync)) at this time.

For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

## Handle background task completion using event handlers

You should register a method with the [**BackgroundTaskCompletedEventHandler**](/uwp/api/windows.applicationmodel.background.backgroundtaskcompletedeventhandler), so that your app can get results from the background task. When the app is launched or resumed, the marked method will be called if the background task has completed since the last time the app was in the foreground. (The OnCompleted method will be called immediately if the background task completes while your app is currently in the foreground.)

1.  Write an OnCompleted method to handle the completion of background tasks. For example, the background task result might cause a UI update. The method footprint shown here is required for the OnCompleted event handler method, even though this example does not use the *args* parameter.

The following sample code recognizes background task completion and calls an example UI update method that takes a message string.

```csharp
private void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
{
    var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
    var key = task.TaskId.ToString();
    var message = settings.Values[key].ToString();
    UpdateUI(message);
}
```

```cppwinrt
void UpdateUI(winrt::hstring const& message)
{
    MyTextBlock().Dispatcher().RunAsync(Windows::UI::Core::CoreDispatcherPriority::Normal, [=]()
    {
        MyTextBlock().Text(message);
    });
}

void OnCompleted(
    Windows::ApplicationModel::Background::BackgroundTaskRegistration const& sender,
    Windows::ApplicationModel::Background::BackgroundTaskCompletedEventArgs const& /* args */)
{
	// You'll previously have inserted this key into local settings.
    auto settings{ Windows::Storage::ApplicationData::Current().LocalSettings().Values() };
    auto key{ winrt::to_hstring(sender.TaskId()) };
    auto message{ winrt::unbox_value<winrt::hstring>(settings.Lookup(key)) };

    UpdateUI(message);
}
```

```cpp
void MainPage::OnCompleted(BackgroundTaskRegistration^ task, BackgroundTaskCompletedEventArgs^ args)
{
    auto settings = ApplicationData::Current->LocalSettings->Values;
    auto key = task->TaskId.ToString();
    auto message = dynamic_cast<String^>(settings->Lookup(key));
    UpdateUI(message);
}
```

> [!NOTE]
> UI updates should be performed asynchronously, to avoid holding up the UI thread. For an example, see the UpdateUI method in the [background task sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask).

2.  Go back to where you registered the background task. After that line of code, add a new [**BackgroundTaskCompletedEventHandler**](/uwp/api/windows.applicationmodel.background.backgroundtaskcompletedeventhandler) object. Provide your OnCompleted method as the parameter for the **BackgroundTaskCompletedEventHandler** constructor.

The following sample code adds a [**BackgroundTaskCompletedEventHandler**](/uwp/api/windows.applicationmodel.background.backgroundtaskcompletedeventhandler) to the [**BackgroundTaskRegistration**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskRegistration):

```csharp
task.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
```

```cppwinrt
task.Completed({ this, &MainPage::OnCompleted });
```

```cpp
task->Completed += ref new BackgroundTaskCompletedEventHandler(this, &MainPage::OnCompleted);
```

## Declare in the app manifest that your app uses background tasks

Before your app can run background tasks, you must declare each background task in the app manifest. If your app attempts to register a background task with a trigger that isn't listed in the manifest, the registration of the background task will fail with a "runtime class not registered" error.

1.  Open the package manifest designer by opening the file named Package.appxmanifest.
2.  Open the **Declarations** tab.
3.  From the **Available Declarations** drop-down, select **Background Tasks** and click **Add**.
4.  Select the **System event** checkbox.
5.  In the **Entry point:** textbox, enter the namespace and name of your background class which is for this example is Tasks.ExampleBackgroundTask.
6.  Close the manifest designer.

The following Extensions element is added to your Package.appxmanifest file to register the background task:

```xml
<Extensions>
  <Extension Category="windows.backgroundTasks" EntryPoint="Tasks.ExampleBackgroundTask">
    <BackgroundTasks>
      <Task Type="systemEvent" />
    </BackgroundTasks>
  </Extension>
</Extensions>
```

## Summary and next steps

You should now understand the basics of how to write a background task class, how to register the background task from within your app, and how to make your app recognize when the background task is complete. You should also understand how to update the application manifest so that your app can successfully register the background task.

> [!NOTE]
> Download the [background task sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask) to see similar code examples in the context of a complete and robust UWP app that uses background tasks.

See the following related topics for API reference, background task conceptual guidance, and more detailed instructions for writing apps that use background tasks.

## Related topics

**Detailed background task instructional topics**

* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Register a background task](register-a-background-task.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).
* [Convert an out-of-process background task to an in-process background task](convert-out-of-process-background-task.md)  

**Background task guidance**

* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](/previous-versions/hh974425(v=vs.110))

**Background Task API Reference**

* [**Windows.ApplicationModel.Background**](/uwp/api/Windows.ApplicationModel.Background)
