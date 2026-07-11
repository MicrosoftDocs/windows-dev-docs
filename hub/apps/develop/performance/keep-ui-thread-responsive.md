---
title: Keep the UI thread responsive
description: Learn how to keep the UI thread responsive in your Windows App SDK application by using asynchronous APIs and background threads.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Keep the UI thread responsive

Users expect your app to remain responsive while it performs computation, regardless of the type of machine. This means different things for different apps — providing realistic physics, loading data from disk or the web, presenting complex scenes, navigating between pages, or processing data. Regardless of the type of computation, users want your app to act on their input and eliminate instances where it appears unresponsive.

Your app is event-driven: your code performs work in response to an event and then sits idle until the next event. Platform code for UI (layout, input, raising events) and your app's code for UI all run on the same UI thread. Only one instruction executes on that thread at a time, so if your app code takes too long to process an event, the framework can't run layout or raise new events. The responsiveness of your app is related to the availability of the UI thread to process work.

You need to use the UI thread to make almost all changes to UI elements. You can't update the UI from a background thread, but you can post a message to it with [DispatcherQueue.TryEnqueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) to cause code to run there.

> [!IMPORTANT]
> In WinUI 3 desktop apps, use `DispatcherQueue` instead of the UWP `CoreDispatcher`. Access the dispatcher queue through your window's `DispatcherQueue` property or `DispatcherQueue.GetForCurrentThread()`.

> [!NOTE]
> A separate render thread can apply UI changes that don't affect how input is handled or the basic layout. For example, many animations and transitions that don't affect layout can run on this render thread.

## Delay element instantiation

Some of the slowest stages in an app include startup and switching views. Don't do more work than necessary to bring up the UI that the user sees initially. For example, don't create the UI for progressively-disclosed content or popup contents.

- Use the [x:Load attribute](/windows/uwp/xaml-platform/x-load-attribute) to delay-instantiate elements.
- Programmatically insert elements into the tree on-demand.

Use `DispatcherQueue.TryEnqueue` with a low priority to queue work for the UI thread to process when it's not busy.

## Use asynchronous APIs

To keep your app responsive, use asynchronous versions of APIs when they are available. An asynchronous API ensures that your active execution thread never blocks for a significant amount of time. When you call an API from the UI thread, always use the asynchronous version if one exists.

## Offload work to background threads

Write event handlers to return quickly. When a non-trivial amount of work needs to be performed, schedule it on a background thread and return.

You can schedule work asynchronously by using the `await` operator in C#. However, `await` doesn't guarantee that the work runs on a background thread. Many Windows App SDK APIs schedule work on a background thread for you, but if you call your app code by using only `await`, you run that method on the UI thread. You have to explicitly run your app code on a background thread. In C#, accomplish this by passing code to [Task.Run](/dotnet/api/system.threading.tasks.task.run).

Remember that you can only access UI elements from the UI thread. Use the UI thread to access UI elements before launching background work, or use `DispatcherQueue.TryEnqueue` on the background thread to post UI updates back to the UI thread.

```csharp
public sealed partial class MainWindow : Window
{
    // Declared in MainWindow.xaml as x:Name="statusText".
    private TextBlock statusText;

    private async void NextMove_Click(object sender, RoutedEventArgs e)
    {
        // The await causes the handler to return immediately.
        await Task.Run(() => ComputeNextMove());

        // Now update the UI with the results.
        // This code runs on the UI thread after ComputeNextMove completes.
        statusText.Text = "Move computed.";
    }

    private void ComputeNextMove()
    {
        // Perform background work here.
        // Don't directly access UI elements from this method.
    }
}
```

In this example, the `NextMove_Click` handler returns at the `await` to keep the UI thread responsive. Execution picks up in that handler again after `ComputeNextMove` (which runs on a background thread) completes, and the remaining code updates the UI with the results.

## Related content

- [Asynchronous programming](/dotnet/csharp/asynchronous-programming/)
- [Planning and measuring performance](planning-measuring-performance.md)
- [Optimize your XAML markup](optimize-xaml-loading.md)
