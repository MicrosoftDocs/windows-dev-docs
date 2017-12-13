---
author: TylerMSFT
title: Create and register an out-of-process background task
description: Create an out-of-process background task class and register it to run when your app is not in the foreground.
ms.assetid: 4F98F6A3-0D3D-4EFB-BA8E-30ED37AE098B
ms.author: twhitney
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Create and register an out-of-process background task


**Important APIs**

-   [**IBackgroundTask**](https://msdn.microsoft.com/library/windows/apps/br224794)
-   [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768)
-   [**BackgroundTaskCompletedEventHandler**](https://msdn.microsoft.com/library/windows/apps/br224781)

Create a background task class and register it to run when your app is not in the foreground. This topic demonstrates how to create and register a background task that runs in a separate process than your app's process. To do background work directly in the foreground application, see [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).

> [!Note]
> If you use a background task to play media in the background, see [Play media in the background](https://msdn.microsoft.com/windows/uwp/audio-video-camera/background-audio) for information about improvements in Windows 10, version 1607, that make it much easier.

## Create the Background Task class

You can run code in the background by writing classes that implement the [**IBackgroundTask**](https://msdn.microsoft.com/library/windows/apps/br224794) interface. This code will run when a specific event is triggered by using, for example, [**SystemTrigger**](https://msdn.microsoft.com/library/windows/apps/br224839) or [**MaintenanceTrigger**](https://msdn.microsoft.com/library/windows/apps/hh700517).

The following steps show you how to write a new class that implements the [**IBackgroundTask**](https://msdn.microsoft.com/library/windows/apps/br224794) interface. Before getting started, create a new project in your solution for background tasks. Add a new empty class for your background task and import the [Windows.ApplicationModel.Background](https://msdn.microsoft.com/library/windows/apps/br224847) namespace.

1.  Create a new project for background tasks and add it to your solution. To do this, right-click on your solution node in the **Solution Explorer** and select Add-&gt;New Project. Then select the **Windows Runtime Component (Universal Windows)** project type, name the project, and click OK.
2.  Reference the background tasks project from your Universal Windows Platform (UWP) app project. For a C# or C++ app, in your app project, right click on **References** and select **Add New Reference**. Under **Solution**, select **Projects** and then select the name of your background task project and click **Ok**.
3.  Create a new class that implements the [**IBackgroundTask**](https://msdn.microsoft.com/library/windows/apps/br224794) interface. The [**Run**](https://msdn.microsoft.com/library/windows/apps/br224811) method is a required entry point that will be called when the specified event is triggered; this method is required in every background task.

    > [!NOTE]
    > The background task class itself - and all other classes in the background task project - need to be **public** classes that are **sealed**.

    The following sample code shows a very basic starting point for a background task class:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     //
    >     // ExampleBackgroundTask.cs
    >     //
    >
    >     using Windows.ApplicationModel.Background;
    >
    >     namespace Tasks
    >     {
    >         public sealed class ExampleBackgroundTask : IBackgroundTask
    >         {
    >             public void Run(IBackgroundTaskInstance taskInstance)
    >             {
    >                 
    >             }        
    >         }
    >     }
    > ```
    > ```cpp
    >     //
    >     // ExampleBackgroundTask.h
    >     //
    >
    >     #pragma once
    >
    >     using namespace Windows::ApplicationModel::Background;
    >
    >     namespace RuntimeComponent1
    >     {
    >         public ref class ExampleBackgroundTask sealed : public IBackgroundTask
    >         {
    >
    >         public:
    >             ExampleBackgroundTask();
    >
    >             virtual void Run(IBackgroundTaskInstance^ taskInstance);
    >             void OnCompleted(
    >                     BackgroundTaskRegistration^ task,
    >                     BackgroundTaskCompletedEventArgs^ args
    >                     );
    >         };
    >     }
    >
    >     //
    >     // ExampleBackgroundTask.cpp
    >     //
    >
    >     #include "ExampleBackgroundTask.h"
    >
    >     using namespace Tasks;
    >
    >     void ExampleBackgroundTask::Run(IBackgroundTaskInstance^ taskInstance)
    >     {
    >
    >     }
    >  ```

4.  If you run any asynchronous code in your background task, then your background task needs to use a deferral. If you don't use a deferral, then the background task process can terminate unexpectedly if the Run method completes before your asynchronous method call has completed.

    Request the deferral in the Run method before calling the asynchronous method. Save the deferral to a global variable so it can be accessed from the asynchronous method. Declare the deferral complete after the asynchronous code completes.

    The following sample code gets the deferral, saves it, and releases it when the asynchronous code is complete:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     BackgroundTaskDeferral _deferral; // Note: defined at class scope so we can mark it complete inside the OnCancel() callback if we choose to support cancellation
    >     public async void Run(IBackgroundTaskInstance taskInstance)
    >     {
    >         _deferral = taskInstance.GetDeferral()
    >         //
    >         // TODO: Insert code to start one or more asynchronous methods using the
    >         //       await keyword, for example:
    >         //
    >         // await ExampleMethodAsync();
    >         //
    >         
    >         _deferral.Complete();
    >     }
    > ```
    > ```cpp
    >     BackgroundTaskDeferral^ deferral = taskInstance->GetDeferral(); // Note: defined at class scope so we can mark it complete inside the OnCancel() callback if we choose to support cancellation
    >     void ExampleBackgroundTask::Run(IBackgroundTaskInstance^ taskInstance)
    >     {
    >         //
    >         // TODO: Modify the following line of code to call a real async function.
    >         //       Note that the task<void> return type applies only to async
    >         //       actions. If you need to call an async operation instead, replace
    >         //       task<void> with the correct return type.
    >         //
    >         task<void> myTask(ExampleFunctionAsync());
    >         
    >         myTask.then([=] () {
    >             deferral->Complete();
    >         });
    >     }
    > ```

> [!NOTE]
> In C#, your background task's asynchronous methods can be called using the **async/await** keywords. In C++, a similar result can be achieved by using a task chain.

For more information about asynchronous patterns, see [Asynchronous programming](https://msdn.microsoft.com/library/windows/apps/mt187335). For additional examples of how to use deferrals to keep a background task from stopping early, see the [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666).

The following steps are completed in one of your app classes (for example, MainPage.xaml.cs).

> [!NOTE]
> You can also create a function dedicated to registering background tasks - see [Register a background task](register-a-background-task.md). In that case, instead of using the next 3 steps, you can simply construct the trigger and provide it to the registration function along with the task name, task entry point, and (optionally) a condition.

## Register the background task to run

1.  Find out if the background task is already registered by iterating through the [**BackgroundTaskRegistration.AllTasks**](https://msdn.microsoft.com/library/windows/apps/br224787) property. This step is important; if your app doesn't check for existing background task registrations, it could easily register the task multiple times, causing issues with performance and maxing out the task's available CPU time before work can complete.

    The following example iterates on the AllTasks property and sets a flag variable to true if the task is already registered:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     var taskRegistered = false;
    >     var exampleTaskName = "ExampleBackgroundTask";
    >
    >     foreach (var task in BackgroundTaskRegistration.AllTasks)
    >     {
    >         if (task.Value.Name == exampleTaskName)
    >         {
    >             taskRegistered = true;
    >             break;
    >         }
    >     }
    > ```
    > ```cpp
    >     boolean taskRegistered = false;
    >     Platform::String^ exampleTaskName = "ExampleBackgroundTask";
    >
    >     auto iter = BackgroundTaskRegistration::AllTasks->First();
    >     auto hascur = iter->HasCurrent;
    >
    >     while (hascur)
    >     {
    >         auto cur = iter->Current->Value;
    >
    >         if(cur->Name == exampleTaskName)
    >         {
    >             taskRegistered = true;
    >             break;
    >         }
    >
    >         hascur = iter->MoveNext();
    >     }
    > ```

2.  If the background task is not already registered, use [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) to create an instance of your background task. The task entry point should be the name of your background task class prefixed by the namespace.

    The background task trigger controls when the background task will run. For a list of possible triggers, see [**SystemTrigger**](https://msdn.microsoft.com/library/windows/apps/br224839).

    For example, this code creates a new background task and sets it to run when the **TimeZoneChanged** trigger is fired:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     var builder = new BackgroundTaskBuilder();
    >
    >     builder.Name = exampleTaskName;
    >     builder.TaskEntryPoint = "RuntimeComponent1.ExampleBackgroundTask";
    >     builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
    > ```
    > ```cpp
    >     auto builder = ref new BackgroundTaskBuilder();
    >
    >     builder->Name = exampleTaskName;
    >     builder->TaskEntryPoint = "RuntimeComponent1.ExampleBackgroundTask";
    >     builder->SetTrigger(ref new SystemTrigger(SystemTriggerType::TimeZoneChange, false));
    > ```

3.  You can add a condition to control when your task will run after the trigger event occurs (optional). For example, if you don't want the task to run until the user is present, use the condition **UserPresent**. For a list of possible conditions, see [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835).

    The following sample code assigns a condition requiring the user to be present:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
    > ```
    > ```cpp
    >     builder->AddCondition(ref new SystemCondition(SystemConditionType::UserPresent));
    > ```

4.  Register the background task by calling the Register method on the [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) object. Store the [**BackgroundTaskRegistration**](https://msdn.microsoft.com/library/windows/apps/br224786) result so it can be used in the next step.

    The following code registers the background task and stores the result:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     BackgroundTaskRegistration task = builder.Register();
    > ```
    > ```cpp
    >     BackgroundTaskRegistration^ task = builder->Register();
    > ```

> [!NOTE]
> Universal Windows apps must call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485) before registering any of the background trigger types.

To ensure that your Universal Windows app continues to run properly after you release an update, use the **ServicingComplete** (see [SystemTriggerType](https://msdn.microsoft.com/library/windows/apps/br224839)) trigger to perform any post-update configuration changes such as migrating the app's database and registering background tasks. It is best practice to unregister background tasks associated with the previous version of the app (see [**RemoveAccess**](https://msdn.microsoft.com/library/windows/apps/hh700471)) and register background tasks for the new version of the app (see [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485)) at this time.

For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

## Handle background task completion using event handlers

You should register a method with the [**BackgroundTaskCompletedEventHandler**](https://msdn.microsoft.com/library/windows/apps/br224781), so that your app can get results from the background task. When the app is launched or resumed, the mark method will be called if the background task has completed since the last time the app was in the foreground. (The OnCompleted method will be called immediately if the background task completes while your app is currently in the foreground.)

1.  Write an OnCompleted method to handle the completion of background tasks. For example, the background task result might cause a UI update. The method footprint shown here is required for the OnCompleted event handler method, even though this example does not use the *args* parameter.

    The following sample code recognizes background task completion and calls an example UI update method that takes a message string.

     > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     private void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
    >     {
    >         var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
    >         var key = task.TaskId.ToString();
    >         var message = settings.Values[key].ToString();
    >         UpdateUI(message);
    >     }
    > ```
    > ```cpp
    >     void ExampleBackgroundTask::OnCompleted(BackgroundTaskRegistration^ task, BackgroundTaskCompletedEventArgs^ args)
    >     {
    >         auto settings = ApplicationData::Current->LocalSettings->Values;
    >         auto key = task->TaskId.ToString();
    >         auto message = dynamic_cast<String^>(settings->Lookup(key));
    >         UpdateUI(message);
    >     }
    > ```

    > [!NOTE]
    > UI updates should be performed asynchronously, to avoid holding up the UI thread. For an example, see the UpdateUI method in the [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666).


2.  Go back to where you registered the background task. After that line of code, add a new [**BackgroundTaskCompletedEventHandler**](https://msdn.microsoft.com/library/windows/apps/br224781) object. Provide your OnCompleted method as the parameter for the **BackgroundTaskCompletedEventHandler** constructor.

    The following sample code adds a [**BackgroundTaskCompletedEventHandler**](https://msdn.microsoft.com/library/windows/apps/br224781) to the [**BackgroundTaskRegistration**](https://msdn.microsoft.com/library/windows/apps/br224786):

     > [!div class="tabbedCodeSnippets"]
    > ```cs
    >     task.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
    > ```
    > ```cpp
    >     task->Completed += ref new BackgroundTaskCompletedEventHandler(this, &ExampleBackgroundTask::OnCompleted);
    > ```

## Declare that your app uses background tasks in the app manifest

Before your app can run background tasks, you must declare each background task in the app manifest. If your app attempts to register a background task with a trigger that isn't listed in the manifest, the registration will fail.

1.  Open the package manifest designer by opening the file named Package.appxmanifest.
2.  Open the **Declarations** tab.
3.  From the **Available Declarations** drop-down, select **Background Tasks** and click **Add**.
4.  Select the **System event** checkbox.
5.  In the **Entry point:** textbox, enter the namespace and name of your background class which is for this example is RuntimeComponent1.ExampleBackgroundTask.
6.  Close the manfiest designer.

    The following Extensions element is added to your Package.appxmanifest file to register the background task:

    ```xml
    <Extensions>
      <Extension Category="windows.backgroundTasks" EntryPoint="RuntimeComponent1.ExampleBackgroundTask">
        <BackgroundTasks>
          <Task Type="systemEvent" />
        </BackgroundTasks>
      </Extension>
    </Extensions>
    ```

## Summary and next steps

You should now understand the basics of how to write a background task class, how to register the background task from within your app, and how to make your app recognize when the background task is complete. You should also understand how to update the application manifest so that your app can successfully register the background task.

> [!NOTE]
> Download the [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666) to see similar code examples in the context of a complete and robust UWP app that uses background tasks.

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
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](http://go.microsoft.com/fwlink/p/?linkid=254345)

**Background Task API Reference**

* [**Windows.ApplicationModel.Background**](https://msdn.microsoft.com/library/windows/apps/br224847)
