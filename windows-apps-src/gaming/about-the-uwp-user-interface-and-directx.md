---
title: The app object and DirectX
description: Universal Windows Platform (UWP) with DirectX games don't use many of the Windows UI user interface elements and objects.
ms.assetid: 46f92156-29f8-d65e-2587-7ba1de5b48a6
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, directx, app object
ms.localizationpriority: medium
---

# The app object and DirectX

Universal Windows Platform (UWP) with DirectX games don't use many of the Windows UI user interface elements and objects. Rather, because they run at a lower level in the Windows Runtime stack, they must interoperate with the user interface framework in a more fundamental way: by accessing and interoperating with the app object directly. Learn when and how this interoperation occurs, and how you, as a DirectX developer, can effectively use this model in the development of your UWP app.

See the [Direct3D graphics glossary](../graphics-concepts/index.md) for information about unfamiliar graphics terms or concepts you encounter while reading.

## The important core user interface namespaces

First, let's note the Windows Runtime namespaces that you must include (with **using**) in your UWP app. We get into the details in a bit.

-   [**Windows.ApplicationModel.Core**](/uwp/api/Windows.ApplicationModel.Core)
-   [**Windows.ApplicationModel.Activation**](/uwp/api/Windows.ApplicationModel.Activation)
-   [**Windows.UI.Core**](/uwp/api/Windows.UI.Core)
-   [**Windows.System**](/uwp/api/Windows.System)
-   [**Windows.Foundation**](/uwp/api/Windows.Foundation)

> [!NOTE]
> If you're not developing a UWP app, use the user interface components provided in the JavaScript- or XAML-specific libraries and namespaces instead of the types provided in these namespaces.

## The Windows Runtime app object

In your UWP app, you want to get a window and a view provider from which you can get a view and to which you can connect your swap chain (your display buffers). You can also hook this view into the window-specific events for your running app. To get the parent window for the app object, defined by the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) type, create a type that implements [**IFrameworkViewSource**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkViewSource). For a [C++/WinRT](../cpp-and-winrt-apis/index.md) code example showing how to implement **IFrameworkViewSource**, see [Composition native interoperation with DirectX and Direct2D](../composition/composition-native-interop.md).

Here's the basic set of steps to get a window using the core user interface framework.

1.  Create a type that implements [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView). This is your view.

    In this type, define:

    -   An [**Initialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.initialize) method that takes an instance of [**CoreApplicationView**](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView) as a parameter. You can get an instance of this type by calling [**CoreApplication.CreateNewView**](/uwp/api/windows.applicationmodel.core.coreapplication.createnewview). The app object calls it when the app is launched.
    -   A [**SetWindow**](/uwp/api/windows.applicationmodel.core.iframeworkview.setwindow) method that takes an instance of [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) as a parameter. You can get an instance of this type by accessing the [**CoreWindow**](/uwp/api/windows.applicationmodel.core.coreapplicationview.corewindow) property on your new [**CoreApplicationView**](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView) instance.
    -   A [**Load**](/uwp/api/windows.applicationmodel.core.iframeworkview.load) method that takes a string for an entry point as the sole parameter. The app object provides the entry point string when you call this method. This is where you set up resources. You create your device resources here. The app object calls it when the app is launched.
    -   A [**Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) method that activates the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object and starts the window event dispatcher. The app object calls it when the app's process starts.
    -   An [**Uninitialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.uninitialize) method that cleans up the resources set up in the call to [**Load**](/uwp/api/windows.applicationmodel.core.iframeworkview.load). The app object calls this method when the app is closed.

2.  Create a type that implements [**IFrameworkViewSource**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkViewSource). This is your view provider.

    In this type, define:

    -   A method named [**CreateView**](/uwp/api/windows.applicationmodel.core.iframeworkviewsource.createview) that returns an instance of your [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView) implementation, as created in Step 1.

3.  Pass an instance of the view provider to [**CoreApplication.Run**](/uwp/api/windows.applicationmodel.core.coreapplication.run) from **main**.

With those basics in mind, let's look at more options you have to extend this approach.

## Core user interface types

Here are other core user interface types in the Windows Runtime that you might find helpful:

-   [**Windows.ApplicationModel.Core.CoreApplicationView**](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView)
-   [**Windows.UI.Core.CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow)
-   [**Windows.UI.Core.CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher)

You can use these types to access your app's view, specifically, the bits that draw the contents of the app's parent window, and handle the events fired for that window. The app window's process is an *application single-threaded apartment* (ASTA) that is isolated and that handles all callbacks.

Your app's view is generated by the view provider for your app window, and in most cases will be implemented by a specific framework package or the system itself, so you don't need to implement it yourself. For DirectX, you need to implement a thin view provider, as discussed previously. There is a specific 1-to-1 relationship between the following components and behaviors:

-   An app's view, which is represented by the [**CoreApplicationView**](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView) type, and which defines the method(s) for updating the window.
-   An ASTA, the attribution of which defines the threading behavior of the app. You cannot create instances of COM STA-attributed types on an ASTA.
-   A view provider, which your app obtains from the system or which you implement.
-   A parent window, which is represented by the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) type.
-   Sourcing for all activation events. Both views and windows have separate activation events.

In summary, the app object provides a view provider factory. It creates a view provider and instantiates a parent window for the app. The view provider defines the app's view for the parent window of the app. Now, let's discuss the specifics of the view and the parent window.

## CoreApplicationView behaviors and properties

[**CoreApplicationView**](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView) represents the current app view. The app singleton creates the app view during initialization, but the view remains dormant until it is activated. You can get the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) that displays the view by accessing the [**CoreApplicationView.CoreWindow**](/uwp/api/windows.applicationmodel.core.coreapplicationview.corewindow) property on it, and you can handle activation and deactivation events for the view by registering delegates with the [**CoreApplicationView.Activated**](/uwp/api/windows.applicationmodel.core.coreapplicationview.activated) event.

## CoreWindow behaviors and properties

The parent window, which is a [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) instance, is created and passed to the view provider when the app object initializes. If the app has a window to display, it displays it; otherwise, it simply initializes the view.

[**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) provides a number of events specific to input and basic window behaviors. You can handle these events by registering your own delegates with them.

You can also obtain the window event dispatcher for the window by accessing the [**CoreWindow.Dispatcher**](/uwp/api/windows.ui.core.corewindow.dispatcher) property, which provides an instance of [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher).

## CoreDispatcher behaviors and properties

You can determine the threading behavior of event dispatching for a window with the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) type. On this type, there's one particularly important method: the [**CoreDispatcher.ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) method, which starts window event processing. Calling this method with the wrong option for your app can lead to all sorts of unexpected event processing behaviors.

| CoreProcessEventsOption option                                                           | Description                                                                                                                                                                                                                                  |
|------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [**CoreProcessEventsOption.ProcessOneAndAllPending**](/uwp/api/Windows.UI.Core.CoreProcessEventsOption) | Dispatch all currently available events in the queue. If no events are pending, wait for the next new event.                                                                                                                                 |
| [**CoreProcessEventsOption.ProcessOneIfPresent**](/uwp/api/Windows.UI.Core.CoreProcessEventsOption)     | Dispatch one event if it is pending in the queue. If no events are pending, don't wait for a new event to be raised but instead return immediately.                                                                                          |
| [**CoreProcessEventsOption.ProcessUntilQuit**](/uwp/api/Windows.UI.Core.CoreProcessEventsOption)        | Wait for new events and dispatch all available events. Continue this behavior until the window is closed or the application calls the [**Close**](/uwp/api/windows.ui.core.corewindow.close) method on the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) instance. |
| [**CoreProcessEventsOption.ProcessAllIfPresent**](/uwp/api/Windows.UI.Core.CoreProcessEventsOption)     | Dispatch all currently available events in the queue. If no events are pending, return immediately.                                                                                                                                          |
UWP using DirectX should use the [**CoreProcessEventsOption.ProcessAllIfPresent**](/uwp/api/Windows.UI.Core.CoreProcessEventsOption) option to prevent blocking behaviors that might interrupt graphics updates.

## ASTA considerations for DirectX devs

The app object that defines the run-time representation of yourUWP and DirectX app uses a threading model called Application Single-Threaded Apartment (ASTA) to host your app’s UI views. If you are developing a UWP and DirectX app, you're familiar with the properties of an ASTA, because any thread you dispatch from your UWP and DirectX app must use the [**Windows::System::Threading**](/uwp/api/Windows.System.Threading) APIs, or use [**CoreWindow::CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher). (You can get the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object for the ASTA by calling [**CoreWindow::GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread) from your app.)

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

-   Wait primitives, such as [**CoWaitForMultipleObjects**](/windows/win32/api/combaseapi/nf-combaseapi-cowaitformultipleobjects), behave differently in an ASTA than in an STA.
-   The COM call modal loop operates differently in an ASTA. You can no longer receive unrelated calls while an outgoing call is in progress. For example, the following behavior will create a deadlock from an ASTA (and immediately crash the app):
    1.  The ASTA calls an MTA object and passes an interface pointer P1.
    2.  Later, the ASTA calls the same MTA object. The MTA object calls P1 before it returns to the ASTA.
    3.  P1 cannot enter the ASTA as it's blocked making an unrelated call. However, the MTA thread is blocked as it tries to make the call to P1.

    You can resolve this by :
    -   Using the **async** pattern defined in the Parallel Patterns Library (PPLTasks.h)
    -   Calling [**CoreDispatcher::ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) from your app's ASTA (the main thread of your app) as soon as possible to allow arbitrary calls.

    That said, you cannot rely on immediate delivery of unrelated calls to your app's ASTA. For more info about async calls, read [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

Overall, when designing your UWP app, use the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) for your app's [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) and [**CoreDispatcher::ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) to handle all UI threads rather than trying to create and manage your MTA threads yourself. When you need a separate thread that you cannot handle with the **CoreDispatcher**, use async patterns and follow the guidance mentioned earlier to avoid reentrancy issues.