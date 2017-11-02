---
author: TylerMSFT
title: Create and register an in-process background task
description: Create and register an in-process task that runs in the same process as your foreground app.
ms.author: twhitney
ms.date: 11/03/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: d99de93b-e33b-45a9-b19f-31417f1e9354
localizationpriority: medium
---

# Create and register an in-process background task

**Important APIs**

-   [**IBackgroundTask**](https://msdn.microsoft.com/library/windows/apps/br224794)
-   [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768)
-   [**BackgroundTaskCompletedEventHandler**](https://msdn.microsoft.com/library/windows/apps/br224781)

This topic demonstrates how to create and register a background task that runs in the same process as your app.

In-process background tasks are simpler to implement than out-of-process background tasks. However, they are less resilient. If the code running in an in-process background task crashes, it will take down your app. Also note that [DeviceUseTrigger](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.deviceusetrigger.aspx), [DeviceServicingTrigger](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.deviceservicingtrigger.aspx) and **IoTStartupTask** cannot be used with the in-process model. Activating a VoIP background task within your application is also not possible. These triggers and tasks are still supported using the out-of-process background task model.

Be aware that background activity can be terminated even when running inside the app's foreground process if it runs past execution time limits. For some purposes the resiliency of separating work into a background task that runs in a separate process is still useful. Keeping background work as a task separate from the foreground application may be the best option for work that does not require communication with the foreground application.

## Fundamentals

The in-process model enhances the application lifecycle with improved notifications for when your app is in the foreground or in the background. Two new events are available from the Application object for these transitions: [**EnteredBackground**](https://msdn.microsoft.com/library/windows/apps/Windows.ApplicationModel.Core.CoreApplication.EnteredBackground) and [**LeavingBackground**](https://msdn.microsoft.com/library/windows/apps/Windows.ApplicationModel.Core.CoreApplication.LeavingBackground). These events fit into the application lifecycle based on the visibility state of your application
Read more about these events and how they affect the application lifecycle at [App lifecycle](app-lifecycle.md).

At a high level, you will handle the **EnteredBackground** event to run your code that will execute while your app is running in the background, and handle **LeavingBackground** to know when your app has moved to the foreground.

## Register your background task trigger

In-process background activity is registered much the same as out-of-process background activity. All background triggers start with registration using the [BackgroundTaskBuilder](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.backgroundtaskbuilder.aspx?f=255&MSPPError=-2147217396). The builder makes it easy to register a background task by setting all required values in one place:

> [!div class="tabbedCodeSnippets"]
> ```cs
> var builder = new BackgroundTaskBuilder();
> builder.Name = "My Background Trigger";
> builder.SetTrigger(new TimeTrigger(15, true));
> // Do not set builder.TaskEntryPoint for in-process background tasks
> // Here we register the task and work will start based on the time trigger.
> BackgroundTaskRegistration task = builder.Register();
> ```

> [!NOTE]
> Universal Windows apps must call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485) before registering any of the background trigger types.
> To ensure that your Universal Windows app continues to run properly after you release an update, you must call [**RemoveAccess**](https://msdn.microsoft.com/library/windows/apps/hh700471) and then call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/hh700485) when your app launches after being updated. For more information, see [Guidelines for background tasks](guidelines-for-background-tasks.md).

For in-process background activities you do not set `TaskEntryPoint.` Leaving it blank enables the default entry point, a new protected method on the Application object called [OnBackgroundActivated()](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.application.onbackgroundactivated.aspx).

Once a trigger is registered, it will fire based on the type of trigger set in the [SetTrigger](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.backgroundtaskbuilder.settrigger.aspx) method. In the example above a [TimeTrigger](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.timetrigger.aspx) is used, which will fire fifteen minutes from the time it was registered.

## Add a condition to control when your task will run (optional)

You can add a condition to control when your task will run after the trigger event occurs. For example, if you don't want the task to run until the user is present, use the condition **UserPresent**. For a list of possible conditions, see [**SystemConditionType**](https://msdn.microsoft.com/library/windows/apps/br224835).

The following sample code assigns a condition requiring the user to be present:

> [!div class="tabbedCodeSnippets"]
> ```cs
> builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
> ```

## Place your background activity code in OnBackgroundActivated()

Put your background activity code in [OnBackgroundActivated](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.application.onbackgroundactivated.aspx) to respond to your background trigger when it fires. **OnBackgroundActivated** can be treated just like [IBackgroundTask.Run](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.ibackgroundtask.run.aspx?f=255&MSPPError=-2147217396). The method has a [BackgroundActivatedEventArgs](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.activation.backgroundactivatedeventargs.aspx) parameter, which contains everything that the **Run** method delivers. For example, in App.xaml.cs:

``` cs
using Windows.ApplicationModel.Background;

...

sealed partial class App : Application
{
  ...

  protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
  {
      base.OnBackgroundActivated(args);
      IBackgroundTaskInstance taskInstance = args.TaskInstance;
      DoYourBackgroundWork(taskInstance);  
  }
}
```

For a richer **OnBackgroundActivated** example, see [Convert an app service to run in the same process as its host app](convert-app-service-in-process.md).

## Handle background task progress and completion

Task progress and completion can be monitored the same way as for multi-process background tasks (see [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)) but you will likely find that you can more easily track them by using variables to track progress or completion status in your app. This is one of the advantages of having your background activity code running in the same process as your app.

## Handle background task cancellation

In-process background tasks are cancelled the same way as out-of-process background tasks are (see [Handle a cancelled background task](handle-a-cancelled-background-task.md)). Be aware that your **BackgroundActivated** event handler must exit before the cancellation occurs, or the whole process will be terminated. If your foreground app closes unexpectedly when you cancel the background task, verify that your handler exited before the cancellation occured.

## The manifest

Unlike out-of-process background tasks, you are not required to add background task information to the package manifest in order to run in-process background tasks.

## Summary and next steps

You should now understand the basics of how to write a in-process background task.

See the following related topics for API reference, background task conceptual guidance, and more detailed instructions for writing apps that use background tasks.

## Related topics

**Detailed background task instructional topics**

* [Convert an out-of-process background task to an in-process background task](convert-out-of-process-background-task.md)
* [Create and register an out-of-process background task](create-and-register-a-background-task.md)
* [Play media in the background](https://msdn.microsoft.com/windows/uwp/audio-video-camera/background-audio)
* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Register a background task](register-a-background-task.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)

**Background task guidance**

* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](http://go.microsoft.com/fwlink/p/?linkid=254345)

**Background Task API Reference**

* [**Windows.ApplicationModel.Background**](https://msdn.microsoft.com/library/windows/apps/br224847)
