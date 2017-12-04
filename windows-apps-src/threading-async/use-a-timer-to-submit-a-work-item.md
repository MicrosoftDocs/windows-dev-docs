---
author: normesta
ms.assetid: AAE467F9-B3C7-4366-99A2-8A880E5692BE
title: Use a timer to submit a work item
description: Learn how to create a work item that runs after a timer elapses.
ms.author: normesta
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, timer, threads
ms.localizationpriority: medium
---
# Use a timer to submit a work item


** Important APIs **

-   [**Windows.UI.Core namespace**](https://msdn.microsoft.com/library/windows/apps/BR208383)
-   [**Windows.System.Threading namespace**](https://msdn.microsoft.com/library/windows/apps/BR229642)

Learn how to create a work item that runs after a timer elapses.

## Create a single-shot timer

Use the [**CreateTimer**](https://msdn.microsoft.com/library/windows/apps/Hh967921) method to create a timer for the work item. Supply a lambda that accomplishes the work, and use the *delay* parameter to specify how long the thread pool waits before it can assign the work item to an available thread. The delay is specified using a [**TimeSpan**](https://msdn.microsoft.com/library/windows/apps/BR225996) structure.

> **Note**  You can use [**CoreDispatcher.RunAsync**](https://msdn.microsoft.com/library/windows/apps/Hh750317) to access the UI and show progress from the work item.

The following example creates a work item that runs in three minutes:

> [!div class="tabbedCodeSnippets"]
> ``` csharp
> TimeSpan delay = TimeSpan.FromMinutes(3);
>             
> ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer(
>     (source) =>
>     {
>         //
>         // TODO: Work
>         //
>         
>         //
>         // Update the UI thread by using the UI core dispatcher.
>         //
>         Dispatcher.RunAsync(
>             CoreDispatcherPriority.High,
>             () =>
>             {
>                 //
>                 // UI components can be accessed within this scope.
>                 //
>
>             });
>
>     }, delay);
> ```
> ``` cpp
> TimeSpan delay;
> delay.Duration = 3 * 60 * 10000000; // 10,000,000 ticks per second
>
> ThreadPoolTimer ^ DelayTimer = ThreadPoolTimer::CreateTimer(
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
>                     ExampleUIUpdateMethod("Timer completed.");
>
>                 }));
>
>         }), delay);
> ```

## Provide a completion handler

If needed, handle cancellation and completion of the work item with a [**TimerDestroyedHandler**](https://msdn.microsoft.com/library/windows/apps/Hh967926). Use the [**CreateTimer**](https://msdn.microsoft.com/library/windows/apps/Hh967921) overload to supply an additional lambda. This runs when the timer is cancelled or when the work item completes.

The following example creates a timer that submits the work item, and calls a method when the work item finishes or the timer is cancelled:

> [!div class="tabbedCodeSnippets"]
> ``` csharp
> TimeSpan delay = TimeSpan.FromMinutes(3);
>             
> bool completed = false;
>
> ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer(
>     (source) =>
>     {
>         //
>         // TODO: Work
>         //
>
>         //
>         // Update the UI thread by using the UI core dispatcher.
>         //
>         Dispatcher.RunAsync(
>                 CoreDispatcherPriority.High,
>                 () =>
>                 {
>                     //
>                     // UI components can be accessed within this scope.
>                     //
>
>                 });
>
>         completed = true;
>     },
>     delay,
>     (source) =>
>     {
>         //
>         // TODO: Handle work cancellation/completion.
>         //
>
>
>         //
>         // Update the UI thread by using the UI core dispatcher.
>         //
>         Dispatcher.RunAsync(
>             CoreDispatcherPriority.High,
>             () =>
>             {
>                 //
>                 // UI components can be accessed within this scope.
>                 //
>
>                 if (completed)
>                 {
>                     // Timer completed.
>                 }
>                 else
>                 {
>                     // Timer cancelled.
>                 }
>
>             });
>     });
> ```
> ``` cpp
> TimeSpan delay;
> delay.Duration = 3 * 60 * 10000000; // 10,000,000 ticks per second
>
> completed = false;
>
> ThreadPoolTimer ^ DelayTimer = ThreadPoolTimer::CreateTimer(
>         ref new TimerElapsedHandler([&](ThreadPoolTimer ^ source)
>         {
>             //
>             // TODO: Work
>             //
>
>             //
>             // Update the UI thread by using the UI core dispatcher.
>             //
>             Dispatcher->RunAsync(CoreDispatcherPriority::High,
>                 ref new DispatchedHandler([&]()
>                 {
>                     //
>                     // UI components can be accessed within this scope.
>                     //
>
>                 }));
>
>             completed = true;
>
>         }),
>         delay,
>         ref new TimerDestroyedHandler([&](ThreadPoolTimer ^ source)
>         {
>             //
>             // TODO: Handle work cancellation/completion.
>             //
>
>             Dispatcher->RunAsync(CoreDispatcherPriority::High,
>                 ref new DispatchedHandler([&]()
>                 {
>                     //
>                     // Update the UI thread by using the UI core dispatcher.
>                     //
>
>                     if (completed)
>                     {
>                         // Timer completed.
>                     }
>                     else
>                     {
>                         // Timer cancelled.
>                     }
>
>                 }));
>         }));
> ```

## Cancel the timer

If the timer is still counting down, but the work item is no longer needed, call [**Cancel**](https://msdn.microsoft.com/library/windows/apps/BR230588). The timer is cancelled and the work item won't be submitted to the thread pool.

> [!div class="tabbedCodeSnippets"]
> ``` csharp
> DelayTimer.Cancel();
> ```
> ``` cpp
> DelayTimer->Cancel();
> ```

## Remarks

Universal Windows Platform (UWP) apps can't use **Thread.Sleep** because it can block the UI thread. You can use a [**ThreadPoolTimer**](https://msdn.microsoft.com/library/windows/apps/BR230587) to create a work item instead, and this will delay the task accomplished by the work item without blocking the UI thread.

See the [thread pool sample](http://go.microsoft.com/fwlink/p/?linkid=255387) for a complete code sample that demonstrates work items, timer work items, and periodic work items. The code sample was originally written for Windows 8.1 but the code can be re-used in Windows 10.

For information about repeating timers, see [Create a periodic work item](create-a-periodic-work-item.md).

## Related topics

* [Submit a work item to the thread pool](submit-a-work-item-to-the-thread-pool.md)
* [Best practices for using the thread pool](best-practices-for-using-the-thread-pool.md)
* [Use a timer to submit a work item](use-a-timer-to-submit-a-work-item.md)
 

 
