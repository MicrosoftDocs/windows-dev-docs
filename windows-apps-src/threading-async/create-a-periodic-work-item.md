---
ms.assetid: 1B077801-0A58-4A34-887C-F1E85E9A37B0
title: Create a periodic work item
description: Learn how to create a work item that repeats periodically using the CreatePeriodicTimer method of the Universal Windows Platform (UWP) ThreadPoolTimer API.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, periodic work item, threading, timers
ms.localizationpriority: medium
---
# Create a periodic work item


<b>Important APIs</b>

-   [**CreatePeriodicTimer**](/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer)
-   [**ThreadPoolTimer**](/uwp/api/Windows.System.Threading.ThreadPoolTimer)

Learn how to create a work item that repeats periodically.

## Create the periodic work item

Use the [**CreatePeriodicTimer**](/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer) method to create a periodic work item. Supply a lambda that accomplishes the work, and use the *period* parameter to specify the interval between submissions. The period is specified using a [**TimeSpan**](/uwp/api/Windows.Foundation.TimeSpan) structure. The work item will be resubmitted every time the period elapses, so make sure the period is long enough for work to complete.

[**CreateTimer**](/uwp/api/windows.system.threading.threadpooltimer.createtimer) returns a [**ThreadPoolTimer**](/uwp/api/Windows.System.Threading.ThreadPoolTimer) object. Store this object in case the timer needs to be canceled.

> **Note**  Avoid specifying a value of zero (or any value less than one millisecond) for the interval. This causes the periodic timer to behave as a single-shot timer instead.

> **Note**  You can use [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) to access the UI and show progress from the work item.

The following example creates a work item that runs once every 60 seconds:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> TimeSpan period = TimeSpan.FromSeconds(60);
>
> ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
>     {
>         //
>         // TODO: Work
>         //
>         
>         //
>         // Update the UI thread by using the UI core dispatcher.
>         //
>         Dispatcher.RunAsync(CoreDispatcherPriority.High,
>             () =>
>             {
>                 //
>                 // UI components can be accessed within this scope.
>                 //
>
>             });
>
>     }, period);
> ```
> ``` cpp
> TimeSpan period;
> period.Duration = 60 * 10000000; // 10,000,000 ticks per second
>
> ThreadPoolTimer ^ PeriodicTimer = ThreadPoolTimer::CreatePeriodicTimer(
>         ref new TimerElapsedHandler([this](ThreadPoolTimer^ source)
>         {
>             //
>             // TODO: Work
>             //
>             
>             //
>             // Update the UI thread by using the UI core dispatcher.
>             //
>             Dispatcher->RunAsync(CoreDispatcherPriority::High,
>                 ref new DispatchedHandler([this]()
>                 {
>                     //
>                     // UI components can be accessed within this scope.
>                     //
>                         
>                 }));
>
>         }), period);
> ```

## Handle cancellation of the periodic work item (optional)

If needed, you can handle cancellation of the periodic timer with a [**TimerDestroyedHandler**](/uwp/api/windows.system.threading.timerdestroyedhandler). Use the [**CreatePeriodicTimer**](/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer) overload to supply an additional lambda that handles cancellation of the periodic work item.

The following example creates a periodic work item that repeats every 60 seconds and it also supplies a cancellation handler:

> [!div class="tabbedCodeSnippets"]
> ``` csharp
> using Windows.System.Threading;
>
>     TimeSpan period = TimeSpan.FromSeconds(60);
>
>     ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
>     {
>         //
>         // TODO: Work
>         //
>         
>         //
>         // Update the UI thread by using the UI core dispatcher.
>         //
>         Dispatcher.RunAsync(CoreDispatcherPriority.High,
>             () =>
>             {
>                 //
>                 // UI components can be accessed within this scope.
>                 //
>
>             });
>     },
>     period,
>     (source) =>
>     {
>         //
>         // TODO: Handle periodic timer cancellation.
>         //
>
>         //
>         // Update the UI thread by using the UI core dispatcher.
>         //
>         Dispatcher->RunAsync(CoreDispatcherPriority.High,
>             ()=>
>             {
>                 //
>                 // UI components can be accessed within this scope.
>                 //                 
>
>                 // Periodic timer cancelled.
>
>             }));
>     });
> ```
> ``` cpp
> using namespace Windows::System::Threading;
> using namespace Windows::UI::Core;
>
> TimeSpan period;
> period.Duration = 60 * 10000000; // 10,000,000 ticks per second
>
> ThreadPoolTimer ^ PeriodicTimer = ThreadPoolTimer::CreatePeriodicTimer(
>         ref new TimerElapsedHandler([this](ThreadPoolTimer^ source)
>         {
>             //
>             // TODO: Work
>             //
>                 
>             //
>             // Update the UI thread by using the UI core dispatcher.
>             //
>             Dispatcher->RunAsync(CoreDispatcherPriority::High,
>                 ref new DispatchedHandler([this]()
>                 {
>                     //
>                     // UI components can be accessed within this scope.
>                     //
>
>                 }));
>
>         }),
>         period,
>         ref new TimerDestroyedHandler([&](ThreadPoolTimer ^ source)
>         {
>             //
>             // TODO: Handle periodic timer cancellation.
>             //
>
>             Dispatcher->RunAsync(CoreDispatcherPriority::High,
>                 ref new DispatchedHandler([&]()
>                 {
>                     //
>                     // UI components can be accessed within this scope.
>                     //
>
>                     // Periodic timer cancelled.
>
>                 }));
>         }));
> ```

## Cancel the timer

When necessary, call the [**Cancel**](/uwp/api/windows.system.threading.threadpooltimer.cancel) method to stop the periodic work item from repeating. If the work item is running when the periodic timer is cancelled it is allowed to complete. The [**TimerDestroyedHandler**](/uwp/api/windows.system.threading.timerdestroyedhandler) (if provided) is called when all instances of the periodic work item have completed.

> [!div class="tabbedCodeSnippets"]
> ``` csharp
> PeriodicTimer.Cancel();
> ```
> ``` cpp
> PeriodicTimer->Cancel();
> ```

## Remarks

For information about single-use timers, see [Use a timer to submit a work item](use-a-timer-to-submit-a-work-item.md).

## Related topics

* [Submit a work item to the thread pool](submit-a-work-item-to-the-thread-pool.md)
* [Best practices for using the thread pool](best-practices-for-using-the-thread-pool.md)
* [Use a timer to submit a work item](use-a-timer-to-submit-a-work-item.md)
 