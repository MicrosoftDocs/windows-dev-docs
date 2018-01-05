---
author: mtoepke
title: The app object and DirectX
description: Universal Windows Platform (UWP) with DirectX games don't use many of the Windows UI user interface elements and objects.
ms.assetid: 46f92156-29f8-d65e-2587-7ba1de5b48a6
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, directx, app object
ms.localizationpriority: medium
---

# The app object and DirectX



Universal Windows Platform (UWP) with DirectX games don't use many of the Windows UI user interface elements and objects. Rather, because they run at a lower level in the Windows Runtime stack, they must interoperate with the user interface framework in a more fundamental way: by accessing and interoperating with the app object directly. Learn when and how this interoperation occurs, and how you, as a DirectX developer, can effectively use this model in the development of your UWP app.

## The important core user interface namespaces


First, let's note the Windows Runtime namespaces that you must include (with **using**) in your UWP app. We get into the details in a bit.

-   [**Windows.ApplicationModel.Core**](https://msdn.microsoft.com/library/windows/apps/br205865)
-   [**Windows.ApplicationModel.Activation**](https://msdn.microsoft.com/library/windows/apps/br224766)
-   [**Windows.UI.Core**](https://msdn.microsoft.com/library/windows/apps/br208383)
-   [**Windows.System**](https://msdn.microsoft.com/library/windows/apps/br241814)
-   [**Windows.Foundation**](https://msdn.microsoft.com/library/windows/apps/br226021)

> **Note**   If you are not developing a UWP app, use the user interface components provided in the JavaScript- or XAML-specific libraries and namespaces instead of the types provided in these namespaces.

 

## The Windows Runtime app object


In your UWP app, you want to get a window and a view provider from which you can get a view and to which you can connect your swap chain (your display buffers). You can also hook this view into the window-specific events for your running app. To get the parent window for the app object, defined by the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) type, create a type that implements [**IFrameworkViewSource**](https://msdn.microsoft.com/library/windows/apps/hh700482), as we did in the previous code snippet.

Here's the basic set of steps to get a window using the core user interface framework:

1.  Create a type that implements [**IFrameworkView**](https://msdn.microsoft.com/library/windows/apps/hh700478). This is your view.

    In this type, define:

    -   An [**Initialize**](https://msdn.microsoft.com/library/windows/apps/hh700495) method that takes an instance of [**CoreApplicationView**](https://msdn.microsoft.com/library/windows/apps/br225017) as a parameter. You can get an instance of this type by calling [**CoreApplication.CreateNewView**](https://msdn.microsoft.com/library/windows/apps/dn297278). The app object calls it when the app is launched.
    -   A [**SetWindow**](https://msdn.microsoft.com/library/windows/apps/hh700509) method that takes an instance of [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) as a parameter. You can get an instance of this type by accessing the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br225019) property on your new [**CoreApplicationView**](https://msdn.microsoft.com/library/windows/apps/br225017) instance.
    -   A [**Load**](https://msdn.microsoft.com/library/windows/apps/hh700501) method that takes a string for an entry point as the sole parameter. The app object provides the entry point string when you call this method. This is where you set up resources. You create your device resources here. The app object calls it when the app is launched.
    -   A [**Run**](https://msdn.microsoft.com/library/windows/apps/hh700505) method that activates the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) object and starts the window event dispatcher. The app object calls it when the app's process starts.
    -   An [**Uninitialize**](https://msdn.microsoft.com/library/windows/apps/hh700523) method that cleans up the resources set up in the call to [**Load**](https://msdn.microsoft.com/library/windows/apps/hh700501). The app object calls this method when the app is closed.

2.  Create a type that implements [**IFrameworkViewSource**](https://msdn.microsoft.com/library/windows/apps/hh700482). This is your view provider.

    In this type, define:

    -   A method named [**CreateView**](https://msdn.microsoft.com/library/windows/apps/hh700491) that returns an instance of your [**IFrameworkView**](https://msdn.microsoft.com/library/windows/apps/hh700478) implementation, as created in Step 1.

3.  Pass an instance of the view provider to [**CoreApplication.Run**](https://msdn.microsoft.com/library/windows/apps/hh700469) from **main**.

With those basics in mind, let's look at more options you have to extend this approach.

## Core user interface types


Here are other core user interface types in the Windows Runtime that you might find helpful:

-   [**Windows.ApplicationModel.Core.CoreApplicationView**](https://msdn.microsoft.com/library/windows/apps/br225017)
-   [**Windows.UI.Core.CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225)
-   [**Windows.UI.Core.CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211)

You can use these types to access your app's view, specifically, the bits that draw the contents of the app's parent window, and handle the events fired for that window. The app window's process is an *application single-threaded apartment* (ASTA) that is isolated and that handles all callbacks.

Your app's view is generated by the view provider for your app window, and in most cases will be implemented by a specific framework package or the system itself, so you don't need to implement it yourself. For DirectX, you need to implement a thin view provider, as discussed previously. There is a specific 1-to-1 relationship between the following components and behaviors:

-   An app's view, which is represented by the [**CoreApplicationView**](https://msdn.microsoft.com/library/windows/apps/br225017) type, and which defines the method(s) for updating the window.
-   An ASTA, the attribution of which defines the threading behavior of the app. You cannot create instances of COM STA-attributed types on an ASTA.
-   A view provider, which your app obtains from the system or which you implement.
-   A parent window, which is represented by the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) type.
-   Sourcing for all activation events. Both views and windows have separate activation events.

In summary, the app object provides a view provider factory. It creates a view provider and instantiates a parent window for the app. The view provider defines the app's view for the parent window of the app. Now, let's discuss the specifics of the view and the parent window.

## CoreApplicationView behaviors and properties


[**CoreApplicationView**](https://msdn.microsoft.com/library/windows/apps/br225017) represents the current app view. The app singleton creates the app view during initialization, but the view remains dormant until it is activated. You can get the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) that displays the view by accessing the [**CoreApplicationView.CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br225019) property on it, and you can handle activation and deactivation events for the view by registering delegates with the [**CoreApplicationView.Activated**](https://msdn.microsoft.com/library/windows/apps/br225018) event.

## CoreWindow behaviors and properties


The parent window, which is a [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) instance, is created and passed to the view provider when the app object initializes. If the app has a window to display, it displays it; otherwise, it simply initializes the view.

[**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) provides a number of events specific to input and basic window behaviors. You can handle these events by registering your own delegates with them.

You can also obtain the window event dispatcher for the window by accessing the [**CoreWindow.Dispatcher**](https://msdn.microsoft.com/library/windows/apps/br208264) property, which provides an instance of [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211).

## CoreDispatcher behaviors and properties


You can determine the threading behavior of event dispatching for a window with the [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) type. On this type, there's one particularly important method: the [**CoreDispatcher.ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208215) method, which starts window event processing. Calling this method with the wrong option for your app can lead to all sorts of unexpected event processing behaviors.

| CoreProcessEventsOption option                                                           | Description                                                                                                                                                                                                                                  |
|------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [**CoreProcessEventsOption.ProcessOneAndAllPending**](https://msdn.microsoft.com/library/windows/apps/br208217) | Dispatch all currently available events in the queue. If no events are pending, wait for the next new event.                                                                                                                                 |
| [**CoreProcessEventsOption.ProcessOneIfPresent**](https://msdn.microsoft.com/library/windows/apps/br208217)     | Dispatch one event if it is pending in the queue. If no events are pending, don't wait for a new event to be raised but instead return immediately.                                                                                          |
| [**CoreProcessEventsOption.ProcessUntilQuit**](https://msdn.microsoft.com/library/windows/apps/br208217)        | Wait for new events and dispatch all available events. Continue this behavior until the window is closed or the application calls the [**Close**](https://msdn.microsoft.com/library/windows/apps/br208260) method on the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) instance. |
| [**CoreProcessEventsOption.ProcessAllIfPresent**](https://msdn.microsoft.com/library/windows/apps/br208217)     | Dispatch all currently available events in the queue. If no events are pending, return immediately.                                                                                                                                          |

 

UWP using DirectX should use the [**CoreProcessEventsOption.ProcessAllIfPresent**](https://msdn.microsoft.com/library/windows/apps/br208217) option to prevent blocking behaviors that might interrupt graphics updates.

## ASTA considerations for DirectX devs


The app object that defines the run-time representation of yourUWP and DirectX app uses a threading model called Application Single-Threaded Apartment (ASTA) to host your app’s UI views. If you are developing a UWP and DirectX app, you're familiar with the properties of an ASTA, because any thread you dispatch from your UWP and DirectX app must use the [**Windows::System::Threading**](https://msdn.microsoft.com/library/windows/apps/br229642) APIs, or use [**CoreWindow::CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211). (You can get the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) object for the ASTA by calling [**CoreWindow::GetForCurrentThread**](https://msdn.microsoft.com/library/windows/apps/hh701589) from your app.)

The most important thing for you to be aware of, as a developer of a UWP DirectX app, is that you must enable your app thread to dispatch MTA threads by setting **Platform::MTAThread** on **main()**.

```cpp
[Platform::MTAThread]
int main(Platform::Array<Platform::String^>^)
{
    auto myDXAppSource = ref new MyDXAppSource(); // your view provider factory 
    CoreApplication::Run(myDXAppSource);
    return 0;
}
```

When the app object for your UWP DirectX app activates, it creates the ASTA that will be used for the UI view. The new ASTA thread calls into your view provider factory, to create the view provider for your app object, and as a result, your view provider code will run on that ASTA thread.

Also, any thread that you spin off from the ASTA must be in an MTA. Be aware that any MTA threads that you spin off can still create reentrancy issues and result in a deadlock.

If you're porting existing code to run on the ASTA thread, keep these considerations in mind:

-   Wait primitives, such as [**CoWaitForMultipleObjects**](https://msdn.microsoft.com/library/windows/desktop/hh404144), behave differently in an ASTA than in an STA.
-   The COM call modal loop operates differently in an ASTA. You can no longer receive unrelated calls while an outgoing call is in progress. For example, the following behavior will create a deadlock from an ASTA (and immediately crash the app):
    1.  The ASTA calls an MTA object and passes an interface pointer P1.
    2.  Later, the ASTA calls the same MTA object. The MTA object calls P1 before it returns to the ASTA.
    3.  P1 cannot enter the ASTA as it's blocked making an unrelated call. However, the MTA thread is blocked as it tries to make the call to P1.

    You can resolve this by :
    -   Using the **async** pattern defined in the Parallel Patterns Library (PPLTasks.h)
    -   Calling [**CoreDispatcher::ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208215) from your app's ASTA (the main thread of your app) as soon as possible to allow arbitrary calls.

    That said, you cannot rely on immediate delivery of unrelated calls to your app's ASTA. For more info about async calls, read [Asynchronous programming in C++](https://msdn.microsoft.com/library/windows/apps/mt187334).

Overall, when designing your UWP app, use the [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) for your app's [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) and [**CoreDispatcher::ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208215) to handle all UI threads rather than trying to create and manage your MTA threads yourself. When you need a separate thread that you cannot handle with the **CoreDispatcher**, use async patterns and follow the guidance mentioned earlier to avoid reentrancy issues.

 

 




