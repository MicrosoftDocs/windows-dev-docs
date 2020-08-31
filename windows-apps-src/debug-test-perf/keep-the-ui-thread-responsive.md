---
ms.assetid: FA25562A-FE62-4DFC-9084-6BD6EAD73636
title: Keep the UI thread responsive
description: Users expect an app to remain responsive while it does computation, regardless of the type of machine.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Keep the UI thread responsive


Users expect an app to remain responsive while it does computation, regardless of the type of machine. This means different things for different apps. For some, this translates to providing more realistic physics, loading data from disk or the web faster, quickly presenting complex scenes and navigating between pages, finding directions in a snap, or rapidly processing data. Regardless of the type of computation, users want their app to act on their input and eliminate instances where it appears unresponsive while it "thinks".

Your app is event-driven, which means that your code performs work in response to an event and then it sits idle until the next. Platform code for UI (layout, input, raising events, etc.) and your app’s code for UI all are executed on the same UI thread. Only one instruction can execute on that thread at a time so if your app code takes too long to process an event then the framework can’t run layout or raise new events representing user interaction. The responsiveness of your app is related to the availability of the UI thread to process work.

You need to use the UI thread to make almost all changes to the UI thread, including creating UI types and accessing their members. You can't update the UI from a background thread but you can post a message to it with [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) to cause code to be run there.

> **Note**  The one exception is that there's a separate render thread that can apply UI changes that won't affect how input is handled or the basic layout. For example many animations and transitions that don’t affect layout can run on this render thread.

## Delay element instantiation

Some of the slowest stages in an app can include startup, and switching views. Don't do more work than necessary to bring up the UI that the user sees initially. For example, don't create the UI for progressively-disclosed UI and the contents of popups.

-   Use [x:Load attribute](../xaml-platform/x-load-attribute.md) or [x:DeferLoadStrategy](../xaml-platform/x-deferloadstrategy-attribute.md) to delay-instantiate elements.
-   Programmatically insert elements into the tree on-demand.

[**CoreDispatcher.RunIdleAsync**](/uwp/api/windows.ui.core.coredispatcher.runidleasync) queues work for the UI thread to process when it's not busy.

## Use asynchronous APIs

To help keep your app responsive, the platform provides asynchronous versions of many of its APIs. An asynchronous API ensures that your active execution thread never blocks for a significant amount of time. When you call an API from the UI thread, use the asynchronous version if it's available. For more info about programming with **async** patterns, see [Asynchronous programming](../threading-async/asynchronous-programming-universal-windows-platform-apps.md) or [Call asynchronous APIs in C# or Visual Basic](../threading-async/call-asynchronous-apis-in-csharp-or-visual-basic.md).

## Offload work to background threads

Write event handlers to return quickly. In cases where a non-trivial amount of work needs to be performed, schedule it on a background thread and return.

You can schedule work asynchronously by using the **await** operator in C#, the **Await** operator in Visual Basic, or delegates in C++. But this doesn't guarantee that the work you schedule will run on a background thread. Many of the Universal Windows Platform (UWP) APIs schedule work in the background thread for you, but if you call your app code by using only **await** or a delegate, you run that delegate or method on the UI thread. You have to explicitly say when you want to run your app code on a background thread. In C# and Visual Basic you can accomplish this by passing code to [**Task.Run**](/dotnet/api/system.threading.tasks.task.run).

Remember that UI elements may only be accessed from the UI thread. Use the UI thread to access UI elements before launching the background work and/or use [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) or [**CoreDispatcher.RunIdleAsync**](/uwp/api/windows.ui.core.coredispatcher.runidleasync) on the background thread.

An example of work that can be performed on a background thread is the calculating of computer AI in a game. The code that calculates the computer's next move can take a lot of time to execute.

```csharp
public class AsyncExample
{
    private async void NextMove_Click(object sender, RoutedEventArgs e)
    {
        // The await causes the handler to return immediately.
        await System.Threading.Tasks.Task.Run(() => ComputeNextMove());
        // Now update the UI with the results.
        // ...
    }

    private async System.Threading.Tasks.Task ComputeNextMove()
    {
        // Perform background work here.
        // Don't directly access UI elements from this method.
    }
}
```

> [!div class="tabbedCodeSnippets"]
> ```csharp
> public class Example
> {
>     // ...
>     private async void NextMove_Click(object sender, RoutedEventArgs e)
>     {
>         await Task.Run(() => ComputeNextMove());
>         // Update the UI with results
>     }
> 
>     private async Task ComputeNextMove()
>     {
>         // ...
>     }
>     // ...
> }
> ```
> ```vb
> Public Class Example
>     ' ...
>     Private Async Sub NextMove_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
>         Await Task.Run(Function() ComputeNextMove())
>         ' update the UI with results
>     End Sub
> 
>     Private Async Function ComputeNextMove() As Task
>         ' ...
>     End Function
>     ' ...
> End Class
> ```

In this example, the `NextMove_Click` handler returns at the **await** in order to keep the UI thread responsive. But execution picks up in that handler again after `ComputeNextMove` (which executes on a background thread) completes. The remaining code in the handler updates the UI with the results.

> **Note**  There's also a [**ThreadPool**](/uwp/api/Windows.System.Threading.ThreadPool) and [**ThreadPoolTimer**](/uwp/api/windows.system.threading.threadpooltimer) API for the UWP, which can be used for similar scenarios. For more info, see [Threading and async programming](../threading-async/index.md).

## Related topics

* [Custom user interactions](../design/layout/index.md)