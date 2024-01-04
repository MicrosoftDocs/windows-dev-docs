---
title: Manage app windows
description: Overview of windowing APIs in the Windows App SDK
ms.topic: article
ms.date: 01/04/2024
keywords: windowing, window, AppWindow, Windows App SDK
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# Manage app windows (Windows App SDK)

This topic contains a [Code example](#code-example) section.

The Windows App SDK provides the easy-to-use [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class. **AppWindow** is framework-agnostic, and available to all Windows apps including Win32, WPF, and WinForms. You can contrast the framework-agnostic nature of **AppWindow** to [**Microsoft.UI.Xaml.Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window), which is the window class specifically for the WinUI 3 framework. **AppWindow** is also an evolution of the Universal Windows Platform's (UWP's) [**Windows.UI.WindowManagement.AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow).

The Windows App SDK version of **Microsoft.UI.Windowing.AppWindow** doesn't rely on asynchronous patterns; and it provides immediate feedback to your app about whether API calls have succeeded.

Also see [Install tools for the Windows App SDK](../set-up-your-development-environment.md), [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md), and [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

## The AppWindow class

[**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is a high-level windowing API that allows for easy-to-use windowing scenarios. **AppWindow** integrates well with the Windows UI/UX, and with other apps.

**AppWindow** represents a high-level abstraction of a system-managed container for the content of an app. It's the container in which your content is hosted; and it represents the entity that users interact with when they resize and move your app on-screen. If you're familiar with Win32, the *app window* can be seen as a high-level abstraction of the [**HWND**](/windows/win32/winprog/windows-data-types). If you're familiar with UWP, then the *app window* can be seen as a replacement for [**CoreWindow**](/uwp/api/windows.ui.core.corewindow)/[**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**Windows.UI.WindowManagement.AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow).

For the Windows App SDK version of **Microsoft.UI.Windowing.AppWindow** we're supporting only top-level **HWND**s. There's a 1:1 mapping between an **AppWindow** and a top-level **HWND**.

The lifetime of an **AppWindow** object and an **HWND** is the same&mdash;the **AppWindow** is available immediately after the window has been created; and it's destroyed when the window is closed.

## The AppWindowPresenter class, and subclasses

Each [**AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) has an [**AppWindowPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter) (presenter) applied to it. If you're a UWP developer who's worked with [**Windows.UI.WindowManagement.AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow), then this will be familiar; even if it's not a 1:1 mapping of functionality and behavior. Also see See [Windowing functionality migration](../migrate-to-windows-app-sdk/guides/windowing.md).

As a new concept to the Win32 application model, a presenter is akin to (but not the same as) a combination of window state and styles. Some presenters also have UI/UX behaviors defined in them that aren't inspectable from classic window state and style properties (such as an auto-hiding titlebar). 

By default, a presenter is created by the system, and applied to an **AppWindow** at creation time. In the Windows App SDK 1.0, on Windows desktop, the type of presenter is [**OverlappedPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter), which is a subclass of **AppWindowPresenter**. There's no need for your app to stash it, nor to keep a reference to it in order to go back to the default presenter for a window after having applied another presenter. That's because the system keeps the same instance of this presenter around for the lifetime of the **AppWindow** for which it was created; and your app can reapply it by calling the [**AppWindow.SetPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter) method with [**AppWindowPresenterKind.Default**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenterkind) as a parameter.

A presenter can be applied to only a single window at a time. Trying to apply the same presenter to a second window throws an exception. That means that if you have multiple windows, and you want to switch each one into a specific presentation mode, then you need to create multiple presenters of the same kind, and then apply each to its own window.

Some presenters have functionality that allows a user to make changes outside of your app's own control. When such a change happens, your app is notified by an [**AppWindow.Changed**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event on the affected **AppWindow**, with the [**AppWindowChangedEventArgs.DidPresenterChange**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didpresenterchange) property set to `true`. Your app should then inspect the property of the applied presenter to see what changed.

The applied presenter is a live object. A change to any property of the [**AppWindow.Presenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.presenter) object takes effect immediately.

A presenter can't be destroyed while it's applied to a window. To destroy a presenter object, first apply a different presenter object to the window; that way, the presenter that you intend to destroy is removed from the window. You can do that either by applying another specific presenter to the window, or by calling the [**AppWindow.SetPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter) method with [**AppWindowPresenterKind.Default**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenterkind) as an argument, which will reapply the default system-created presenter to the window. If you happened to keep a reference to the system-created presenter for the window, then it will be valid at this point (that is, the instance that was first created for the window is re-applied).

### Available presenters

These [**AppWindowPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter)-derived presenters are provided, and they're available on all of the supported OS versions.

* [**CompactOverlayPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.compactoverlaypresenter). Creates an *always-on-top* window of a fixed size, with a 16:9 aspect ratio to allow for *picture-in-picture*-like experiences.
* [**FullScreenPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.fullscreenpresenter). Allows a window to go into a full-screen experience.
* [**OverlappedPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter). The system-created default presenter, which allows you to request and react to minimize/maximize/restore operations and state changes.

## UI framework and HWND interop

The [**AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class is available for *any* top-level **HWND** in your app. That means that when you're working with a desktop UI framework (including WinUI 3), you can continue to use that framework's entry point for creating a window, and attaching its content. And once you've created a window with that UI framework, you can use the windowing interop functions (see below) provided in the Windows App SDK to access the corresponding **AppWindow** and its methods, properties, and events.

Some of the benefits of using **AppWindow** (even when working with a UI framework) are:

* Easy title bar customization; which by default maintains the Windows 11 UI (rounded corners, snap group flyout).
* System-provided full-screen and compact overlay (picture-in-picture) experiences.
* Windows Runtime (WinRT) API surface for some of the core Win32 windowing concepts.

## Code example

This code example demonstrates how to retrieve a [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) from a WinUI 3 window by using the [**Microsoft.UI.Xaml.Window.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property. To use the example, create a new **Blank App, Packaged (WinUI 3 in Desktop)** project, and paste the code in.

For additional details on how to work with **AppWindow**, see the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing).

```csharp
// MainWindow.xaml.cs
private void myButton_Click(object sender, RoutedEventArgs e)
{
    // Retrieve the AppWindow for the current (XAML) WinUI 3 window.
    Microsoft.UI.Windowing.AppWindow appWindow = this.AppWindow;

    if (appWindow != null)
    {
        // With a non-null AppWindow object, you can call its methods
        // to manipulate the window. As an example, let's change the title
        // text of the window.
        appWindow.Title = "Title text updated via AppWindow!";
    }
}
```

```cppwinrt
// pch.h
#include <winrt/Microsoft.UI.Windowing.h> // For the AppWindow class.

// mainwindow.xaml.cpp
void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
{
    // Retrieve the AppWindow for the current (XAML) WinUI 3 window.
    Microsoft::UI::Windowing::AppWindow appWindow = this->AppWindow();

    if (appWindow)
    {
        // With a non-null AppWindow object, you can call its methods
        // to manipulate the window. As an example, let's change the title
        // text of the window.
        appWindow.Title(L"Title text updated via AppWindow!");
    }
}
```

## Code example for versions of Windows App SDK prior to 1.3 (or other desktop app frameworks)

The [**Microsoft.UI.Xaml.Window.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property (used in the code example above) is available in Windows App SDK version 1.3 and later. For earlier versions, you can use the functionally equivalent code example in this section.

**C#**. .NET wrappers for the windowing interop functions are implemented as methods of the [**Microsoft.UI.Win32Interop**](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.md) class. Also see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

**C++**. The interop functions are defined in the [winrt/Microsoft.ui.interop.h](/windows/windows-app-sdk/api/win32/winrt-microsoft.ui.interop/) header file.

The [Code example](#code-example) section below shows actual source code; but here's the recipe for retrieving an **AppWindow** object given an existing window:

1. Retrieve the **HWND** for your existing window object (for your UI framework), if you don't already have it.
2. Pass that **HWND** to the [**GetWindowIdFromWindow**](/windows/windows-app-sdk/api/win32/winrt-microsoft.ui.interop/nf-winrt-microsoft-ui-interop-getwindowidfromwindow) interop function to retrieve a [**WindowId**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid).
3. Pass that **WindowId** to the static [**AppWindow.GetFromWindowId**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.getfromwindowid) method to retrieve the **AppWindow**.


```csharp
// MainWindow.xaml.cs
private void myButton_Click(object sender, RoutedEventArgs e)
{
    // Retrieve the window handle (HWND) of the current (XAML) WinUI 3 window.
    var hWnd =
        WinRT.Interop.WindowNative.GetWindowHandle(this);

    // Retrieve the WindowId that corresponds to hWnd.
    Microsoft.UI.WindowId windowId =
        Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);

    // Lastly, retrieve the AppWindow for the current (XAML) WinUI 3 window.
    Microsoft.UI.Windowing.AppWindow appWindow =
        Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

    if (appWindow != null)
    {
        // You now have an AppWindow object, and you can call its methods to manipulate the window.
        // As an example, let's change the title text of the window.
        appWindow.Title = "Title text updated via AppWindow!";
    }
}
```

```cppwinrt
// pch.h
#include "microsoft.ui.xaml.window.h" // For the IWindowNative interface.
#include <winrt/Microsoft.UI.Interop.h> // For the WindowId struct and the GetWindowIdFromWindow function.
#include <winrt/Microsoft.UI.Windowing.h> // For the AppWindow class.

// mainwindow.xaml.cpp
void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
{
    // Retrieve the window handle (HWND) of the current (XAML) WinUI 3 window.
    auto windowNative{ this->m_inner.as<::IWindowNative>() };
    HWND hWnd{ 0 };
    windowNative->get_WindowHandle(&hWnd);

    // Retrieve the WindowId that corresponds to hWnd.
    Microsoft::UI::WindowId windowId = 
        Microsoft::UI::GetWindowIdFromWindow(hWnd);

    // Lastly, retrieve the AppWindow for the current (XAML) WinUI 3 window.
    Microsoft::UI::Windowing::AppWindow appWindow = 
        Microsoft::UI::Windowing::AppWindow::GetFromWindowId(windowId);

    if (appWindow)
    {
        // You now have an AppWindow object, and you can call its methods to manipulate the window.
        // As an example, let's change the title text of the window.
        appWindow.Title(L"Title text updated via AppWindow!");
    }
}
```

## Limitations

- [**AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is a WinUI 3 API. That means that it's available only to desktop apps (both packaged and unpackaged); and isn't available to UWP apps.
- The Windows App SDK doesn't currently provide methods for attaching UI framework content to an **AppWindow**. But see the [Code example](#code-example) section.
- Title bar customization is supported in Windows 11 and later; and in Windows 10 for version 1.2 and later of the Windows App SDK. For details, see [Title bar customization](/windows/apps/develop/title-bar).

## Related topics

* [Windowing functionality migration](../migrate-to-windows-app-sdk/guides/windowing.md)
* [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md)
* [Retrieve a window handle (HWND)](../../develop/ui-input/retrieve-hwnd.md)
* [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing)
