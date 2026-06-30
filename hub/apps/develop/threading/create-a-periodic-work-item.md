---
title: Create a periodic work item
description: Learn how to create a work item that repeats periodically in your Windows app using the CreatePeriodicTimer method of the ThreadPoolTimer API.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 06/20/2026
keywords: windows 10, windows 11, windows app sdk, winui 3, periodic work item, threading, timers
ms.localizationpriority: medium
---
# Create a periodic work item

**Important APIs**

-   [**CreatePeriodicTimer**](/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer)
-   [**ThreadPoolTimer**](/uwp/api/Windows.System.Threading.ThreadPoolTimer)

Learn how to create a work item that repeats periodically.

## Create the periodic work item

Use the [**CreatePeriodicTimer**](/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer) method to create a periodic work item. Supply a lambda that accomplishes the work, and use the *period* parameter to specify the interval between submissions. The period is specified using a [**TimeSpan**](/uwp/api/Windows.Foundation.TimeSpan) structure. The work item will be resubmitted every time the period elapses, so make sure the period is long enough for work to complete.

[**CreatePeriodicTimer**](/uwp/api/windows.system.threading.threadpooltimer.createperiodictimer) returns a [**ThreadPoolTimer**](/uwp/api/Windows.System.Threading.ThreadPoolTimer) object. Store this object in case the timer needs to be canceled.

> [!NOTE]
> Avoid specifying a value of zero (or any value less than one millisecond) for the interval. This causes the periodic timer to behave as a single-shot timer instead.

> [!NOTE]
> In WinUI 3 apps, use [`DispatcherQueue.TryEnqueue`](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) from `Microsoft.UI.Dispatching.DispatcherQueue` to access the UI thread and show progress from the work item. See the [DispatcherQueue](/windows/apps/develop/dispatcherqueue) overview for details.

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

For information about single-use timers, see [Use a timer to submit a work item](./use-a-timer-to-submit-a-work-item.md).

## Related topics

* [Submit a work item to the thread pool](./submit-a-work-item-to-the-thread-pool.md)
* [Best practices for using the thread pool](./best-practices-for-using-the-thread-pool.md)
* [Use a timer to submit a work item](./use-a-timer-to-submit-a-work-item.md)
