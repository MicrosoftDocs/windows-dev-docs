---
description: Overview of Windowing APIs in the Windows App SDK
title: Manage app windows (Windows App SDK)
ms.topic: article
ms.date: 08/04/2021
keywords: windowing, window, Windows App SDK
ms.author: rokarman
author: rkarman
ms.localizationpriority: medium
---

# Manage app windows

> [!IMPORTANT]
> Windowing APIs are an experimental feature that is currently supported only in the [experimental](../experimental-channel.md) release channel of the Windows App SDK, and the API surface is subject to change. This feature is not supported for use by apps in production environments.

The Windows App SDK provides an `AppWindow` class that evolves the previous easy-to-use [AppWindow preview](/uwp/api/windows.ui.windowmanagement.appwindow) class and makes it available to all Windows apps, including Win32, WPF, and WinForms.

For this version of `AppWindow`, we're taking the opportunity to address some of the major feedback we've gotten for the previous AppWindow preview - the most significant one being that `AppWindow` in the Windows App SDK does not rely on async patterns, and it provides immediate feedback to your app as to whether or not API calls succeeded.

The Windows App SDK windowing APIs will be the focus for introducing new features, integration with Windows UX, and enabling new windowing scenarios going forward. We therefore recommend all apps to start leveraging these APIs for windowing operations.

## AppWindow

`AppWindow` is a high-level windowing API that allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and with other apps.

`AppWindow` represents a high-level abstraction of a system-managed container of the content of an app. This is the container in which your content is hosted, and represents the entity that users interact with when they resize and move your app on screen. For developers familiar with Win32, the `AppWindow` can be seen as a high-level abstraction of the `HWND`. For developers familiar with UWP, the `AppWindow` can be seen as a replacement for `CoreWindow`/`ApplicationView`/`AppWindow`.

For this version of `AppWindow` we're only supporting top-level `HWND`s (there's a 1:1 mapping between an `AppWindow` and a top-level `HWND`).

The lifetime of an `AppWindow` is the same as for an `HWND`, meaning that the `AppWindow` object is available immediately after the window has been created, and is destroyed when the window gets closed.

## Presenters

Each `AppWindow` has an `AppWindowPresenter` (Presenter) applied to it. To the UWP developer who has worked with AppWindow before, this will be familiar but not a 1:1 mapping of functionality and behavior. As a new concept to the Win32 application model they are akin to, but not the same, as a combination of window state and styles. Some Presenters also have UX behaviors defined in them that are not inspectable from classic window state and style properties (such as auto-hiding titlebar, for example).

By default, a Presenter will be created by the system and applied to an `AppWindow` at creation time. In Windows App SDK 1.0, on Windows Desktop, this is the `OverlappedPresenter`. There is no need for apps to stash it or keep a reference to it in order to "go back" to the default Presenter for a window after having applied another Presenter. The system will keep the same instance of this Presenter around for the lifetime of the `AppWindow` for which it was created and the app can reapply it by calling the `TrySetPresenter` method with `AppWindowPresenterKind.Default` as a parameter.

A Presenter can only be applied to a single window at a time. Trying to apply a Presenter to a second window will throw an exception. This means that if you have multiple windows and want to switch each one into a specific presentation mode, you need to create multiple Presenters of the same kind and then apply each to its own window.

Some Presenters have functionality that allows a user to make changes outside of the apps own control. When such a change happens the app will be notified by a `Changed` event on the affected `AppWindow` with the `DidPresenterChange` event arg property set to `true`. The app will then have to inspect the property of the applied presenter to see what changed.

The applied presenter is a live object. Changing any property of the `AppWindow.Presenter` object will take effect immediately.

A Presenter cannot be destroyed while it is applied to a window. In order to destroy a Presenter object, you must first apply another presenter to the window so that the Presenter you intend to destroy is removed from the window. This can be done by either applying another specific Presenter to the window, or by calling the `TryApplyPresenter` method with `AppWindowPresenterKind.Default` as an argument, which will reapply the default system-created Presenter to the window. If you kept a reference to the system-created Presenter for the window, it will be valid at this point (i.e. the same instance as was first created for the window will have been re-applied).

### Available Presenters

The following Presenters are provided in the current release and they are available on all the supported OS versions for this release.

* `OverlappedPresenter` - the system created "default" Presenter which allows apps to request and react to Minimize/Maximize/Restore operations and state changes.
* `FullScreenPresenter` - allows a window to go into a FullScreen UX.
* `CompactOverlayPresenter` - creates an "always on top" window of a fixed size, with a 16:9 aspect ratio to allow for Picture-in-Picture like experiences.

## UI framework and HWND interop

The `AppWindow` class is available for any top-level `HWND` in your app. This means that when you are working with a UI framework you can continue to use that framework's entry point for creating a window and attaching its content, and once you have created a window you can use the windowing interop methods provided in the Windows App SDK to access the corresponding `AppWindow` and its methods, properties, and events. For examples on how to do this for a WinUI 3 window, see the [samples](#samples) section in this article.

Some of the benefits of using the `AppWindow` even when working with a UI framework are:

* Easy TitleBar customization that by default maintains the Windows 11 UX experience (rounded corners, snap group flyout);
* System provided FullScreen and CompactOverlay (Picture-in-Picture) experiences;
* WinRT API surface for some of the core Win32 windowing concepts.

## Limitations

- This release of `AppWindow` is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an `AppWindow`. You are limited to using the `HWND` interop access methods demonstrated in the [samples](#samples) section in this article.
- The Windowing APIs currently do not work on Windows 10 version 1809 and Windows 10 version 1903 for AMD64.

## Samples

The following code examples demonstrate how to retrieve an `AppWindow` from a WinUI 3 Window. For more details on how to work with `AppWindow`, see the [windowing samples](https://github.com/microsoft/WindowsAppSDK-Samples).

### C++ Sample for getting an AppWindow for a WinUI 3 window

```cpp
#using namespace winrt;
#using namespace Microsoft::UI::Xaml;

// The include- and using-statements below are in addition to the ones you need for WinUI 3
// You can add these to your pch.cpp file in order to not have to include them in each xaml.cpp file
// wghere you need to access windowing APIs.
#include <winrt/Microsoft.UI.h>
#include <winrt/Microsoft.UI.Windowing.h>
#include "microsoft.ui.windowing.core.interop.h"
// For access to WindowId
#include <Microsoft.UI.h>
// For access to hwnd interop methods
#include <Microsoft.UI.Interop.h>
// For the WinRT windowing APIs
#include <Microsoft.UI.Windowing.h>

// This include file is needed for the XAML Native Window Interop.
#include "microsoft.ui.xaml.window.h"

namespace winrt
{
    using namespace Microsoft::UI::Windowing;
    using namespace Microsoft::UI;
}

namespace winrt::SampleApp::implementation
{

    MainWindow::MainWindow()
    {
        InitializeComponent();
        m_appWindow = GetAppWindowForCurrentWindow();
    }

    winrt::AppWindow MainWindow::GetAppWindowForCurrentWindow()
    {
        winrt::AppWindow appWindow = nullptr;
        
        //Get the HWND for the XAML Window
        HWND hWnd;
        Window window = this->try_as<Window>();
        window.as<IWindowNative>()->get_WindowHandle(&hWnd);

        // Get the WindowId for the HWND
        winrt::WindowId windowId;
        if(SUCCEEDED(GetWindowIdFromWindowHandle(hWnd, &windowId))
        {
            // Get the AppWindow for the WindowId
            appWindow = winrt::AppWindow::GetFromWindowId(windowId);
        }
        return appWindow;
    }

    void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
    {
        winrt::AppWindow appWindow = GetAppWindowForCurrentWindow();

        // Check to see that we indeed got an AppWindow.
        if(appWindow)
        {
            // You now have an AppWindow object and can call its methods to manipulate the window.
            // Just to do something here, let's change the title of the window...
            appWindow.Title("WinUI ❤️ AppWindow");
        }
    }
}
```

### C# Sample for getting an AppWindow for a WinUI 3 window

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;
// Needed for WindowId
using Microsoft.UI;
// Needed for AppWindow
using Microsoft.UI.Windowing;
// Needed for XAML hwnd interop
using WinRT.Interop;

namespace SampleApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        // For the simplicity of this code snippet we import the DLL and declare
        // the methods in the MainWindow class here. It is recommended that you
        // break this out into a support class that you use wherever needed instead.
        // See the Windows App SDK windowing sample for more details.
        [DllImport("Microsoft.UI.Windowing.Core.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowHandleFromWindowId(WindowId windowId, out IntPtr result);

        [DllImport("Microsoft.UI.Windowing.Core.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowIdFromWindowHandle(IntPtr hwnd, out WindowId result);

        private AppWindow m_appWindow;

        public MainWindow()
        {
            this.InitializeComponent();
            // Get the AppWindow for our XAML Window
            m_appWindow = GetAppWindowForCurrentWindow();
            if (m_appWindow != null)
            {
                // You now have an AppWindow object and can call its methods to manipulate the window.
                // Just to do something here, let's change the title of the window...
                m_appWindow.Title = "WinUI ❤️ AppWindow";
            }
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            GetWindowIdFromWindowHandle(hWnd, out WindowId myWndId);
            return AppWindow.GetFromWindowId(myWndId);
        }
   }
}
```
