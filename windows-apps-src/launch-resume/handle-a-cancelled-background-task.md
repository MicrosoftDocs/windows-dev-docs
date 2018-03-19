---
author: TylerMSFT
title: Handle a cancelled background task
description: Learn how to make a background task that recognizes cancellation requests and stops work, reporting the cancellation to the app using persistent storage.
ms.assetid: B7E23072-F7B0-4567-985B-737DD2A8728E
ms.author: twhitney
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Handle a cancelled background task


**Important APIs**

-   [**BackgroundTaskCanceledEventHandler**](https://msdn.microsoft.com/library/windows/apps/br224775)
-   [**IBackgroundTaskInstance**](https://msdn.microsoft.com/library/windows/apps/br224797)
-   [**ApplicationData.Current**](https://msdn.microsoft.com/library/windows/apps/br241619)

Learn how to make a background task that recognizes a cancellation request, stops work, and reports the cancellation to the app using persistent storage.

This topic assumes you have already created a background task class, including the Run method that is used as the background task entry point. To get started quickly building a background task, see [Create and register an out-of-process background task](create-and-register-a-background-task.md) or [Create and register an in-process background task](create-and-register-an-inproc-background-task.md). For more in-depth information on conditions and triggers, see [Support your app with background tasks](support-your-app-with-background-tasks.md).

This topic is also applicable to in-process background tasks. But instead of the **Run()** method, substitute **OnBackgroundActivated()**. In-process background tasks do not require you to use persistent storage to signal the cancellation because you can communicate the cancellation using app state since the background task is running in the same process as your foreground app.

## Use the OnCanceled method to recognize cancellation requests

Write a method to handle the cancellation event.

> **Note**  For all device families except desktop, if the device becomes low on memory, background tasks may be terminated. If an out of memory exception is not surfaced, or the app does not handle it, then the background task will be terminated without warning and without raising the OnCanceled event. This helps to ensure the user experience of the app in the foreground. Your background task should be designed to handle this scenario.

Create a method named **OnCanceled** as follows. This method is the entry point called by the Windows Runtime when a cancellation request is made against your background task.

> [!div class="tabbedCodeSnippets"]
> ```cs
>    private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
>    {
>        // TODO: Add code to notify the background task that it is cancelled.
>    }
> ```
> ```cpp
>    void ExampleBackgroundTask::OnCanceled(IBackgroundTaskInstance^ taskInstance, BackgroundTaskCancellationReason reason)
>    {
>        // TODO: Add code to notify the background task that it is cancelled.
>    }
> ```

Add a flag variable called **\_CancelRequested** to the background task class. This variable will be used to indicate when a cancellation request has been made.

> [!div class="tabbedCodeSnippets"]
> ```cs
>   volatile bool _CancelRequested = false;
> ```
> ```cpp
>   private:
>     volatile bool CancelRequested;
> ```

In the OnCanceled method you created in step 1, set the flag variable **\_CancelRequested** to **true**.

The full [background task sample]( http://go.microsoft.com/fwlink/p/?linkid=227509) OnCanceled method sets **\_CancelRequested** to **true** and writes potentially useful debug output:

> [!div class="tabbedCodeSnippets"]
> ```cs
>     private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
>     {
>         //
>         // Indicate that the background task is canceled.
>         //
>
>         _cancelRequested = true;
>
>         Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
>     }
> ```
> ```cpp
>     void SampleBackgroundTask::OnCanceled(IBackgroundTaskInstance^ taskInstance, BackgroundTaskCancellationReason reason)
>     {
>         //
>         // Indicate that the background task is canceled.
>         //
>
>         CancelRequested = true;
>     }
> ```

In the background task's Run method, register the **OnCanceled** event handler method before starting work. In an in-process background task, you might do this registration as part of your application initialization. For example, use the following line of code:

> [!div class="tabbedCodeSnippets"]
> ```cs
>     taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
> ```
> ```cpp
>     taskInstance->Canceled += ref new BackgroundTaskCanceledEventHandler(this, &SampleBackgroundTask::OnCanceled);
> ```

## Handle cancellation by exiting your background task

When a cancellation request is received, your method that does background work needs to stop work and exit by recognizing when **\_cancelRequested** is set to **true**. For in-process background tasks, this means returning from the **OnBackgroundActivated()** method. For out-of-process background tasks, this means returning from the **Run()** method.

Modify the code of your background task class to check the flag variable while it's working. If **\_cancelRequested** set to true, stop work from continuing.

The [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666) includes a check that stops the periodic timer callback if the background task is canceled:

> [!div class="tabbedCodeSnippets"]
> ```cs
>     if ((_cancelRequested == false) && (_progress < 100))
>     {
>         _progress += 10;
>         _taskInstance.Progress = _progress;
>     }
>     else
>     {
>         _periodicTimer.Cancel();
>
>         // TODO: Record whether the task completed or was cancelled.
>     }
> ```
> ```cpp
>     if ((CancelRequested == false) && (Progress < 100))
>     {
>         Progress += 10;
>         TaskInstance->Progress = Progress;
>     }
>     else
>     {
>         PeriodicTimer->Cancel();
>
>         // TODO: Record whether the task completed or was cancelled.
>     }
> ```

> **Note**  The code sample shown above uses the [**IBackgroundTaskInstance**](https://msdn.microsoft.com/library/windows/apps/br224797).[**Progress**](https://msdn.microsoft.com/library/windows/apps/br224800) property being used to record background task progress. Progress is reported back to the app using the [**BackgroundTaskProgressEventArgs**](https://msdn.microsoft.com/library/windows/apps/br224782) class.

Modify the Run method so that after work has stopped, it records whether the task completed or was cancelled. This step applies to out-of-process background tasks because you need a way to communicate between processes when the background task was cancelled. For in-process background tasks, you can simply share state with the application to indicate the task was cancelled.

The [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666) records status in LocalSettings:

> [!div class="tabbedCodeSnippets"]
> ```cs
>     if ((_cancelRequested == false) && (_progress < 100))
>     {
>         _progress += 10;
>         _taskInstance.Progress = _progress;
>     }
>     else
>     {
>         _periodicTimer.Cancel();
>
>         var settings = ApplicationData.Current.LocalSettings;
>         var key = _taskInstance.Task.TaskId.ToString();
>
>         //
>         // Write to LocalSettings to indicate that this background task ran.
>         //
>
>         if (_cancelRequested)
>         {
>             settings.Values[key] = "Canceled";
>         }
>         else
>         {
>             settings.Values[key] = "Completed";
>         }
>         
>         Debug.WriteLine("Background " + _taskInstance.Task.Name + (_cancelRequested ? " Canceled" : " Completed"));
>         
>         //
>         // Indicate that the background task has completed.
>         //
>
>         _deferral.Complete();
>     }
> ```
> ```cpp
>     if ((CancelRequested == false) && (Progress < 100))
>     {
>         Progress += 10;
>         TaskInstance->Progress = Progress;
>     }
>     else
>     {
>         PeriodicTimer->Cancel();
>         
>         //
>         // Write to LocalSettings to indicate that this background task ran.
>         //
>         
>         auto settings = ApplicationData::Current->LocalSettings;
>         auto key = TaskInstance->Task->Name;
>         settings->Values->Insert(key, (Progress < 100) ? "Canceled" : "Completed");
>         
>         //
>         // Indicate that the background task has completed.
>         //
>         
>         Deferral->Complete();
>     }
> ```

## Remarks

You can download the [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666) to see these code examples in the context of methods.

For illustrative purposes, the sample code shows only portions of the Run method (and callback timer) from the [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666).

## Run method example

The complete Run method, and timer callback code, from the [background task sample](http://go.microsoft.com/fwlink/p/?LinkId=618666) are shown below for context:

> [!div class="tabbedCodeSnippets"]
> ```cs
> //
> // The Run method is the entry point of a background task.
> //
> public void Run(IBackgroundTaskInstance taskInstance)
> {
>     Debug.WriteLine("Background " + taskInstance.Task.Name + " Starting...");
>
>     //
>     // Query BackgroundWorkCost
>     // Guidance: If BackgroundWorkCost is high, then perform only the minimum amount
>     // of work in the background task and return immediately.
>     //
>     var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
>     var settings = ApplicationData.Current.LocalSettings;
>     settings.Values["BackgroundWorkCost"] = cost.ToString();
>
>     //
>     // Associate a cancellation handler with the background task.
>     //
>     taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
>
>     //
>     // Get the deferral object from the task instance, and take a reference to the taskInstance;
>     //
>     _deferral = taskInstance.GetDeferral();
>     _taskInstance = taskInstance;
>
>     _periodicTimer = ThreadPoolTimer.CreatePeriodicTimer(new TimerElapsedHandler(PeriodicTimerCallback), TimeSpan.FromSeconds(1));
> }
>
> //
> // Simulate the background task activity.
> //
> private void PeriodicTimerCallback(ThreadPoolTimer timer)
> {
>     if ((_cancelRequested == false) && (_progress < 100))
>     {
>         _progress += 10;
>         _taskInstance.Progress = _progress;
>     }
>     else
>     {
>         _periodicTimer.Cancel();
>
>         var settings = ApplicationData.Current.LocalSettings;
>         var key = _taskInstance.Task.Name;
>
>         //
>         // Write to LocalSettings to indicate that this background task ran.
>         //
>         settings.Values[key] = (_progress < 100) ? "Canceled with reason: " + _cancelReason.ToString() : "Completed";
>         Debug.WriteLine("Background " + _taskInstance.Task.Name + settings.Values[key]);
>
>         //
>         // Indicate that the background task has completed.
>         //
>         _deferral.Complete();
>     }
> }
> ```
> ```cpp
> void SampleBackgroundTask::Run(IBackgroundTaskInstance^ taskInstance)
> {
>     //
>     // Query BackgroundWorkCost
>     // Guidance: If BackgroundWorkCost is high, then perform only the minimum amount
>     // of work in the background task and return immediately.
>     //
>     auto cost = BackgroundWorkCost::CurrentBackgroundWorkCost;
>     auto settings = ApplicationData::Current->LocalSettings;
>     settings->Values->Insert("BackgroundWorkCost", cost.ToString());
>
>     //
>     // Associate a cancellation handler with the background task.
>     //
>     taskInstance->Canceled += ref new BackgroundTaskCanceledEventHandler(this, &SampleBackgroundTask::OnCanceled);
>
>     //
>     // Get the deferral object from the task instance, and take a reference to the taskInstance.
>     //
>     TaskDeferral = taskInstance->GetDeferral();
>     TaskInstance = taskInstance;
>
>     auto timerDelegate = [this](ThreadPoolTimer^ timer)
>     {
>         if ((CancelRequested == false) &&
>             (Progress < 100))
>         {
>             Progress += 10;
>             TaskInstance->Progress = Progress;
>         }
>         else
>         {
>             PeriodicTimer->Cancel();
>
>             //
>             // Write to LocalSettings to indicate that this background task ran.
>             //
>             auto settings = ApplicationData::Current->LocalSettings;
>             auto key = TaskInstance->Task->Name;
>             settings->Values->Insert(key, (Progress < 100) ? "Canceled with reason: " + CancelReason.ToString() : "Completed");
>
>             //
>             // Indicate that the background task has completed.
>             //
>             TaskDeferral->Complete();
>         }
>     };
>
>     TimeSpan period;
>     period.Duration = 1000 * 10000; // 1 second
>     PeriodicTimer = ThreadPoolTimer::CreatePeriodicTimer(ref new TimerElapsedHandler(timerDelegate), period);
> }
> ```

## Related topics

- [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).
- [Create and register an out-of-process background task](create-and-register-a-background-task.md)
- [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md)
- [Guidelines for background tasks](guidelines-for-background-tasks.md)
- [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
- [Register a background task](register-a-background-task.md)
- [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
- [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
- [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
- [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)
- [Use a maintenance trigger](use-a-maintenance-trigger.md)
- [Debug a background task](debug-a-background-task.md)
- [How to trigger suspend, resume, and background events in UWP apps (when debugging)](http://go.microsoft.com/fwlink/p/?linkid=254345)