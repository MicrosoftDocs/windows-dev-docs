---
title: Manage app windows
description: Use AppWindow APIs in the Windows App SDK
ms.topic: article
ms.date: 07/22/2025
keywords: windowing, window, AppWindow, Windows App SDK
ms.localizationpriority: medium
no-loc: [AppWindow, Window]
dev_langs:
  - csharp
  - cppwinrt
appliesto:
  - ✅ <a href="/windows/apps/winui/winui3/" target="_blank">WinUI 3</a>
  - ✅ <a href="h/windows/apps/windows-app-sdk/" target="_blank">Windows App SDK</a>
---

# Manage app windows

The Windows App SDK provides the [Microsoft.UI.Windowing.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class, which represents a high-level abstraction of the HWND. There's a 1:1 mapping between an AppWindow and a top-level HWND in your app. AppWindow and its related classes provide APIs that let you manage many aspects of your app's top-level windows without the need to access the HWND directly.

> [!NOTE]
> This article demonstrates how to use [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) APIs in your app. As a prerequisite, we recommend that you read and understand the AppWindow information in [Windowing overview for WinUI and Windows App SDK](../ui-input/windowing-overview.md), which is applicable whether you use WinUI or another UI framework.

> [!div class="checklist"]
>
> - **Important APIs**: [AppWindow class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow), [OverlappedPresenter class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see AppWindow in action](winui3gallery:/item/AppWindow)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

You can use [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) APIs with any UI framework that the Windows App SDK supports - WinUI 3, WPF, WinForms, or Win32. AppWindow APIs work alongside the framework-specific windowing APIs:

- [XAML Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window)
- [WPF Window](/dotnet/api/system.windows.window)
- [Windows Forms Form](/dotnet/api/system.windows.forms.form)
- [Win32 windowing APIs](/windows/win32/winmsg/windows), [MFC Windows](/cpp/mfc/windows)

You typically use [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) APIs to:

- Manage the size and position of your app's window.
- Manage the window title, icon and title bar color; or create a fully custom title bar with [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) APIs.

    See [Title bar customization](../title-bar.md) for more info and examples.
- Manage the appearance and behavior of the window with [AppWindowPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter)-derived APIs.

## Respond to AppWindow changes

You respond to changes to the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) by handling the single [Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event, then checking the event args ([AppWindowChangedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs)) to determine what kind of change happened. If the change that you're interested in happened, you can respond to it. Potential changes include position, size, presenter, visibility, and z-order.

Here's an example of an [AppWindow.Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event handler.

```csharp
private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
{
    // ConfigText and SizeText are TextBox controls defined in XAML for the page.
    if (args.DidPresenterChange == true)
    {
        ConfigText.Text = sender.Presenter.Kind.ToString();
    }

    if (args.DidSizeChange == true)
    {
        SizeText.Text = sender.Size.Width.ToString() + ", " + sender.Size.Height.ToString();
    }
}

```

## Window size and placement

The [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class has several properties and methods you can use to manage the size and placement of the window.

| Category | Properties |
|--|--|
| Read-only properties | [Position](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.position), [Size](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.size), [ClientSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.clientsize) |
| Events | [Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) ([DidPositionChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didpositionchange#microsoft-ui-windowing-appwindowchangedeventargs-didpositionchange), [DidSizeChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didsizechange#microsoft-ui-windowing-appwindowchangedeventargs-didsizechange)) |
| Size and position methods | [Move](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.move), [Resize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize), [ResizeClient](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resizeclient), [MoveAndResize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveandresize) |
| Z-order methods | [MoveInZOrderAtBottom](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveinzorderatbottom), [MoveInZOrderAtTop](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveinzorderattop), [MoveInZOrderBelow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveinzorderbelow) |

Call [**Resize**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize) to specify a new window size.

In this example, the code is in `MainWindow.xaml.cs`, so you can use the [Window.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property to get the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) instance.

```csharp
public MainWindow()
{
    InitializeComponent();
    AppWindow.Resize(new Windows.Graphics.SizeInt32(1200, 800));
}
```

Call the [**Move**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.move) method to change the position of a window.

This example moves the window to be centered on the screen when the user clicks a button.

This occurs in the code file for a [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) class, so you don't automatically have access to the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) or [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) objects. You have a few options for getting the AppWindow.

- If you keep a reference to the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) as described in [Track the current window](/windows/apps/develop/ui-input/windowing-overview#track-the-current-window) or [Track instances of Window](/windows/apps/develop/ui-input/multiple-windows#track-instances-of-window), you can get the Window, then get [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) from the [Window.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property.
- Or, you can call the static [AppWindow.GetFromWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.getfromwindowid) method to get the AppWindow instance, as shown here. (See [Determining the window that's hosting a visual element](/windows/apps/develop/ui-input/retrieve-hwnd#determining-the-window-thats-hosting-a-visual-element).)

```csharp
private void MoveWindowButton_Click(object sender, RoutedEventArgs e)
{
    AppWindow appWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId);
    RectInt32? area = DisplayArea.GetFromWindowId(appWindow.Id, DisplayAreaFallback.Nearest)?.WorkArea;
    if (area == null) return;
    appWindow.Move(new PointInt32((area.Value.Width - appWindow.Size.Width) / 2, (area.Value.Height - appWindow.Size.Height) / 2));
}
```

## The AppWindowPresenter class, and subclasses

Each [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) has an [AppWindowPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter) (presenter) applied to it. A presenter is created by the system and applied to an AppWindow at creation time. Each sub-class of AppWindowPresenter provides a pre-defined configuration appropriate for the purpose of the window. These [AppWindowPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter)-derived presenters are provided, and they're available on all of the supported OS versions.

- [**CompactOverlayPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.compactoverlaypresenter)

  Configures an *always-on-top* window of a fixed size, with a 16:9 aspect ratio to allow for *picture-in-picture*-like experiences. By default, the [InitialSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.compactoverlaypresenter.initialsize) is [CompactOverlaySize.Small](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.compactoverlaysize), but you can change it to `Medium` or `Large`. You can also call [AppWindow.Resize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize) to override the 16:9 aspect ratio and make the window any desired size.
- [**FullScreenPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.fullscreenpresenter)

  Configures a window to provide a full-screen experience suitable for watching video. The window does not have a border or title bar, and hides the system task bar.
- [**OverlappedPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter)

    The standard window configuration, which, by default, provides a border with resize handles and a title bar with minimize/maximize/restore buttons.

> [!NOTE]
> As a new concept to the Win32 application model, a presenter is similar to (but not the same as) a combination of window state and [styles](/windows/win32/winmsg/window-styles). Some presenters also have behaviors defined in them that can't be inspected from classic window state and style properties (such as an auto-hiding title bar).

### The default presenter

The default presenter applied when an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is created is an instance of [OverlappedPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter) with default property settings. There's no need to keep a reference to it in order to go back to the default presenter for a window after having applied another presenter. That's because the system keeps the same instance of this presenter around for the lifetime of the AppWindow for which it was created; and you can re-apply it by calling the [AppWindow.SetPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter) method with [AppWindowPresenterKind.Default](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenterkind) as a parameter.

> [!IMPORTANT]
> Calling `SetPresenter(AppWindowPresenterKind.Default)` always re-applies the default presenter instance that is created with the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow). If you create and apply another presenter, and want to re-apply it later, you need to keep a reference to your presenter.

You can also get a reference to the default presenter instance and modify it. If you've applied a new presenter, first ensure that the default presenter is applied, as shown here:

```csharp
appWindow.SetPresenter(AppWindowPresenterKind.Default);
OverlappedPresenter defaultPresenter = (OverlappedPresenter)appWindow.Presenter;
defaultPresenter.IsMaximizable = false;
defaultPresenter.IsMinimizable = false;
```

### Modify an OverlappedPresenter

The [OverlappedPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter) is a flexible presenter that you can configure in a variety of ways.

The `Create`* methods let you create an overlapped presenter with default property settings, or one with property settings pre-configured for a particular use.

This table shows how configuration properties are set when you create an OverlappedPresenter object from each method.

| Property | [**Create**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.create) | [**CreateForContextMenu**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.createforcontextmenu) | [**CreateForDialog**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.createfordialog) | [**CreateForToolWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.createfortoolwindow) |
| -- | -- | -- | -- | -- |
| [HasBorder](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.hasborder) | `true` | `true` | `true` | `true` |
| [HasTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.hastitlebar) | `true` | `false` | `true` | `true` |
| [IsAlwaysOnTop](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.isalwaysontop) | `false` | `false` | `false` | `false` |
| [IsMaximizable](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.ismaximizable) | `true` | `false` | `false` | `true` |
| [IsMinimizable](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.isminimizable) | `true` | `false` | `false` | `true` |
| [IsModal](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.ismodal) | `false` | `false` | `false` | `false` |
| [IsResizable](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.isresizable) | `true` | `false` | `false` | `true` |

The applied presenter is a live object. A change to any property of the [AppWindow.Presenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.presenter) object takes effect immediately. There are no events to notify you of these changes, but you can check the properties for current values at any time.

The [**HasBorder**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.hasborder) and [**HasTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.hastitlebar) properties are read-only. You can set these values by calling the [**SetBorderAndTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.setborderandtitlebar) method (`SetBorderAndTitleBar(bool hasBorder, bool hasTitleBar)`). An [OverlappedPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter) cannot have a title bar without a border. That is, if the `hasTitleBar` parameter is `true`, then the `hasBorder` parameter must also be `true`. Otherwise, an exception is thrown with this message:

```text
The parameter is incorrect.
Invalid combination: Border=false, TitleBar=true.
```

Set [**IsMaximizable**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.ismaximizable) to `false` to hide the maximize button in the toolbar. We recommend you do this if you set the `PreferredMaximumHeight` or `PreferredMaximumWidth` properties, as these properties constrain the window size even in the maximized state. This does not affect calls to the [Maximize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.maximize) method.

Set [**IsMinimizable**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.isminimizable) to `false` to hide the minimize button in the toolbar. This does not affect calls to the [Minimize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.minimize) method.

Set [**IsResizable**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.isresizable) to `false` to hide the resizing controls and prevent the user from resizing the window. This does not affect calls to the [AppWindow.Resize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize) method.

Set [**IsAlwaysOnTop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.isalwaysontop) to `true` to keep this window on top of other windows. If you call any of the `AppWindow.MoveInZOrder*` methods, they still take effect to change the z-order of the window even if this property is `true`.

Set [**PreferredMaximumHeight**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.preferredmaximumheight) and [**PreferredMaximumWidth**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.preferredmaximumwidth) to constrain the maximum size that the user can stretch the window to. We recommend that you set `IsMaximizable` to `false` if you set the maximum size properties, as these properties constrain the window size even in the maximized state. These properties also affect calls to [AppWindow.Resize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize); the window will not be resized larger than the specified maximum height and width.

Set [**PreferredMinimumHeight**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.preferredminimumheight) and [**PreferredMinimumWidth**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.preferredminimumwidth) to set the minimum size that the user can shrink the window to. These properties also affect calls to [AppWindow.Resize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize); the window will not be resized smaller than the specified minimum height and width.

#### Modal windows

You can set [**IsModal**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.overlappedpresenter.ismodal) to `true` to create a _modal window_. A modal window is a separate window that blocks interaction with its _owner window_ until it is closed. However, to create a modal window, you must also set the owner window; otherwise, an exception is thrown with this message:

```text
The parameter is incorrect.

The window should have an owner when IsModal=true.
```

To set the owner window in a WinUI app requires Win32 interop. For more information and example code, see the AppWindow page in the WinUI Gallery sample app.

- [Launch the WinUI Gallery app](winui3gallery:/item/AppWindow)
- [Open ModalWindow.xaml.cs on GitHub](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/Samples/SamplePages/ModalWindow.xaml.cs)

### Apply a presenter

A presenter can be applied to only a single window at a time. Trying to apply the same presenter to a second window throws an exception. That means that if you have multiple windows, and you want to switch each one into a specific presentation mode, then you need to create multiple presenters of the same kind, and then apply each to its own window.

When a new presenter is applied (the AppWindow.Presenter property changes), your app is notified by an [AppWindow.Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event on the affected AppWindow, with the [AppWindowChangedEventArgs.DidPresenterChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didpresenterchange) property set to `true`.

> [!TIP]
> If you apply a modified presenter, and allow changing between presenters, be sure to keep a reference to your modified presenter so that it can be re-applied to the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow).

This example shows how to do the following:

- Use the [AppWindow.Presenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.presenter) property to get the current presenter.
- Use the [AppWindowPresenter.Kind](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter.kind) property to check what kind of configuration is currently applied.
- Call [AppWindow.SetPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter) to change the current configuration.

Here, a presenter is created, modified, and applied in the constructor for the window.

```csharp
OverlappedPresenter presenter = OverlappedPresenter.Create();
presenter.PreferredMinimumWidth = 420;
presenter.PreferredMinimumHeight = 550;
AppWindow.SetPresenter(presenter);
```

In the [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) that is the window content, you can get a reference to the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) and the applied presenter.

```csharp
AppWindow appWindow;
OverlappedPresenter modifiedPresenter;

private void AppWindowPage_Loaded(object sender, RoutedEventArgs e)
{
    appWindow = AppWindow.GetFromWindowId(XamlRoot.ContentIslandEnvironment.AppWindowId);
    modifiedPresenter = (OverlappedPresenter)appWindow.Presenter;

    appWindow.Changed += AppWindow_Changed;
}

private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
{
    if (args.DidPresenterChange)
    {
        // ConfigText is a TextBox control defined in XAML for the page.
        ConfigText.Text = appWindow.Presenter.Kind.ToString();
    }
}

private void CompactOverlayButton_Click(object sender, RoutedEventArgs e)
{
    if (appWindow.Presenter.Kind != AppWindowPresenterKind.CompactOverlay)
    {
        appWindow.SetPresenter(CompactOverlayPresenter.Create());
        fullScreenButton.IsChecked = false;
    }
    else
    {
        appWindow.SetPresenter(modifiedPresenter);
    }
}

private void FullScreenButton_Click(object sender, RoutedEventArgs e)
{
    if (appWindow.Presenter.Kind != AppWindowPresenterKind.FullScreen)
    {
        appWindow.SetPresenter(FullScreenPresenter.Create());
        compactOverlayButton.IsChecked = false;
    }
    else
    {
        appWindow.SetPresenter(modifiedPresenter);
    }
}
```

## UI framework and HWND interop

The [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class is available for *any* top-level HWND in your app. That means that when you're working with a desktop UI framework (including WinUI 3), you can continue to use that framework's entry point for creating a window, and attaching its content. And once you've created a window with that UI framework, you can use the windowing interop functions (see below) provided in the Windows App SDK to access the corresponding AppWindow and its methods, properties, and events.

Some of the benefits of using [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) (even when working with a UI framework) are:

* Easy title bar customization; which by default maintains the Windows 11 UI (rounded corners, snap group flyout).
* System-provided full-screen and compact overlay (picture-in-picture) experiences.
* Windows Runtime (WinRT) API surface for some of the core Win32 windowing concepts.

## Get the AppWindow for versions of Windows App SDK prior to 1.3 (or other desktop app frameworks)

The [Window.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property is available in Windows App SDK version 1.3 and later. For earlier versions, you can use the functionally equivalent code example in this section.

**C#.** .NET wrappers for the windowing interop functions are implemented as methods of the [Microsoft.UI.Win32Interop](../../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.md) class. Also see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

**C++.** The interop functions are defined in the [winrt/Microsoft.ui.interop.h](/windows/windows-app-sdk/api/win32/winrt-microsoft.ui.interop/) header file.

The code example section below shows actual source code; but here's the recipe for retrieving an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) object given an existing window:

1. Retrieve the HWND for your existing window object (for your UI framework), if you don't already have it.
1. Pass that HWND to the [GetWindowIdFromWindow](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowidfromwindow) interop function to retrieve a [WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid).
1. Pass that WindowId to the static [AppWindow.GetFromWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.getfromwindowid) method to retrieve the AppWindow.

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

For more examples of how to work with AppWindow, see the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing).

## Limitations

The Windows App SDK doesn't currently provide methods for attaching UI framework content to an AppWindow.

## Related topics

- [Windowing overview](../ui-input/windowing-overview.md)
- [Title bar customization](../title-bar.md)
- [Windowing functionality migration](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing.md)
- [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md)
- [Retrieve a window handle (HWND)](../ui-input/retrieve-hwnd.md)
- [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing)
