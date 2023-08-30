---
title: DispatcherQueue
description: Describes the purpose and function of the Windows App SDK DispatcherQueue class, and how to program with it.
ms.date: 08/30/2023
ms.topic: article
keywords: windows 11, windows 10, dispatcherqueue, dispatcherqueuecontroller
ms.author: stwhi
author: stevewhims
ms.localizationpriority: high
---

# DispatcherQueue

## Highlights

* The [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) class in the Windows App SDK manages a prioritized queue on which the tasks for a thread execute in a serial fashion.
* It provides a means for background threads to run code on a **DispatcherQueue**'s thread (for example, the UI thread where objects with thread-affinity live).
* The class precisely integrates with arbitrary message loops. For example, it supports the common Win32 idiom of nested message loops.
* The [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class integrates with **DispatcherQueue**&mdash;when a **DispatcherQueue** for a given thread is being shut down, the **AppWindow** instances are automatically destroyed.
* It provides a means to register a delegate that's called when a timeout expires.
* It provides events that let components know when a message loop is exiting, and optionally defer that shutdown until outstanding work completes. That ensures components that use the **DispatcherQueue**, but don't own the message loop, may do cleanup on-thread as the loop exits.
* The **DispatcherQueue** is a thread singleton (there can be at most one of them running on any given thread). By default, a thread has no **DispatcherQueue**.
* A thread owner may create a [DispatcherQueueController](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuecontroller) to initialize the **DispatcherQueue** for the thread. At that point, any code can access the thread's **DispatcherQueue**; but only the **DispatcherQueueController**'s owner has access to the [DispatcherQueueController.ShutdownQueueAsync](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuecontroller.shutdownqueueasync) method, which drains the 
**DispatcherQueue**, and raises **ShutdownStarted** and **ShutdownCompleted** events.
* An outermost message loop owner must create a **DispatcherQueue** instance. Only the code in charge of running a thread's outermost message loop knows when dispatch is complete, which is the appropriate time to shut down the **DispatcherQueue**. That means that components that rely on **DispatcherQueue** mustn't create the **DispatcherQueue** unless they own the thread's message loop.

## Run-down

After a thread exits its event loop, it must shut down its **DispatcherQueue**. Doing so raises the **ShutdownStarting** and **ShutdownCompleted** events, and drains any final pending enqueued items before disabling further enqueuing.

* To shut down a **DispatcherQueue** that's running on a dedicated thread with a **DispatcherQueue**-owned message loop, call the [DispatcherQueueController.ShutdownQueueAsync](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuecontroller.shutdownqueueasync) method.
* For scenarios where the app owns an arbitrary message loop (for example, XAML Islands), call the synchronous [DispatcherQueueController.ShutdownQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuecontroller.shutdownqueue) method. That method raises shutdown events, and drains the **DispatcherQueue** synchronously on the calling thread.

When you call either **DispatcherQueueController.ShutdownQueueAsync** or **DispatcherQueueController.ShutdownQueue**, the order of events raised is the following:

* **ShutdownStarting**. Intended for apps to handle.
* **FrameworkShutdownStarting**. Intended for frameworks to handle.
* **FrameworkShutdownCompleted**. Intended for frameworks to handle.
* **ShutdownCompleted**. Intended for apps to handle.

The events are separated into application/framework categories so that orderly shutdown can be achieved. That is, by explicitly raising application shutdown ahead of framework shutdown events, there's no danger that a framework component will be in an unusable state as the application winds down.

```cppwinrt
namespace winrt 
{
    using namespace Microsoft::UI::Dispatching;
}

// App runs its own custom message loop.
void RunCustomMessageLoop()
{
    // Create a DispatcherQueue.
    auto dispatcherQueueController{winrt::DispatcherQueueController::CreateOnCurrentThread()};

    // Run a custom message loop. Runs until the message loop owner decides to stop.
    MSG msg;
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!ContentPreTranslateMessage(&msg))
        {
            TranslateMesasge(&msg);
            DispatchMessage(&msg);
        }
    }

    // Run down the DispatcherQueue. This single call also runs down the system DispatcherQueue
    // if one was created via EnsureSystemDispatcherQueue:
    // 1. Raises DispatcherQueue.ShutdownStarting event.
    // 2. Drains remaining items in the DispatcherQueue, waits for deferrals.
    // 3. Raises DispatcherQueue.FrameworkShutdownStarting event.
    // 4. Drains remaining items in the DispatcherQueue, waits for deferrals.
    // 5. Disables further enqueuing.
    // 6. Raises the DispatcherQueue.FrameworkShutdownCompleted event.
    // 7. Raises the DispatcherQueue.ShutdownCompleted event.    

    dispatcherQueueController.ShutdownQueue();
}
```

## Outermost and recursive message loops

**DispatcherQueue** supports custom message loops. However, for simple apps that don't need customization, we provide a default implementations. That removes a burden from developers, and helps ensure consistently correct behavior.

```cppwinrt
namespace winrt 
{
    using namespace Microsoft::UI::Dispatching;
}

// Simple app; doesn't need a custom message loop.
void RunMessageLoop()
{
    // Create a DispatcherQueue.
    auto dispatcherQueueController{winrt::DispatcherQueueController::CreateOnCurrentThread()};

    // Runs a message loop until a call to DispatcherQueue.EnqueueEventLoopExit or PostQuitMessage.
    dispatcherQueueController.DispatcherQueue().RunEventLoop();

    // Run down the DispatcherQueue. 
    dispatcherQueueController.ShutdownQueue();
}

// May be called while receiving a message.
void RunNestedLoop(winrt::DispatcherQueue dispatcherQueue)
{
    // Runs a message loop until a call to DispatcherQueue.EnqueueEventLoopExit or PostQuitMessage.
    dispatcherQueue.RunEventLoop();
}

// Called to break out of the message loop, returning from the RunEventLoop call lower down the
// stack.
void EndMessageLoop(winrt::DispatcherQueue dispatcherQueue)
{
    // Alternatively, calling Win32's PostQuitMessage has the same effect.
    dispatcherQueue.EnqueueEventLoopExit();
}
```

## System dispatcher management

Some Windows App SDK components (for example, [MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller)) depend on system components that in turn require a system **DispatcherQueue** (**Windows.System.DispatcherQueue**) running on the thread.

In those cases, the component that has a system **DispatcherQueue** dependency calls the [EnsureSystemDispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.ensuresystemdispatcherqueue) method, freeing your app from managing a system **DispatcherQueue**.

With that method called, the Windows App SDK **DispatcherQueue** manages the lifetime of the system **DispatcherQueue** automatically, shutting down the system **DispatcherQueue** 
alongside the Windows App SDK **DispatcherQueue**. Components might rely on both Windows App SDK and system **DispatcherQueue** shutdown events in order to ensure that they do proper cleanup after the message loop exits.

```cppwinrt
namespace winrt 
{
    using namespace Microsoft::UI::Composition::SystemBackdrops;
    using namespace Microsoft::UI::Dispatching;
}

// The Windows App SDK component calls this during its startup.
void MicaControllerInitialize(winrt::DispatcherQueue dispatcherQueue)
{
    dispatcherQueue.EnsureSystemDispatcherQueue();

    // If the component needs the system DispatcherQueue explicitly, it can now grab it off the thread.
    winrt::Windows::System::DispatcherQueue systemDispatcherQueue =
        winrt::Windows::System::DispatcherQueue::GetForCurrentThread();
}

void AppInitialize()
{
    // App doesn't need to concern itself with the system DispatcherQueue dependency.
    auto micaController = winrt::MicaController();
}
```

## AppWindow integration

The [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class has functionality that integrates it with the **DispatcherQueue**, so that **AppWindow** objects can automatically be destroyed when the [DispatcherQueueController.ShutdownQueueAsync](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuecontroller.shutdownqueueasync) or [DispatcherQueueController.ShutdownQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuecontroller.shutdownqueue) method is called.

There's also a property of **AppWindow** that allows callers to retrieve the **DispatcherQueue** associated with the **AppWindow**; aligning it with other objects in the **Composition** and **Input** namespaces.

**AppWindow** needs your explicit opt-in in order to be aware of the **DispatcherQueue**.

```cppwinrt
namespace winrt 
{
    using namespace Microsoft::UI::Dispatching;
    using namespace Microsoft::UI::Windowing;
}

void Main()
{
    // Create a Windows App SDK DispatcherQueue.
    auto dispatcherQueueController{winrt::DispatcherQueueController::CreateOnCurrentThread()};

    var appWindow = AppWindow.Create(nullptr, 0, dispatcherQueueController.DispatcherQueue());

    // Since we associated the DispatcherQueue above with the AppWindow, we're able to retreive it 
    // as a property. If we were to not associate a dispatcher, this property would be null.
    ASSERT(appWindow.DispatcherQueue() == dispatcherQueueController.DispatcherQueue());

    // Runs a message loop until a call to DispatcherQueue.EnqueueEventLoopExit or PostQuitMessage.
    dispatcherQueueController.DispatcherQueue().RunEventLoop();

    // Rundown the Windows App SDK DispatcherQueue. While this call is in progress, the AppWindow.Destoyed
    // event will be raised since the AppWindow instance is associated with the DispatcherQueue.
    dispatcherQueueController.ShutdownQueue();
}
```
