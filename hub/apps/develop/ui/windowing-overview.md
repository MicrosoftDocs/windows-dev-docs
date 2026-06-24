---
title: Windowing overview for WinUI and Windows App SDK
description: Overview of windowing APIs in the Windows App SDK
ms.topic: article
ms.date: 05/16/2025
keywords: windowing, window, AppWindow, Windows App SDK
ms.localizationpriority: medium
no-loc: [window handle, Effective pixels, effective pixels, Windows, Window, AppWindow]
dev_langs:
  - csharp
  - cppwinrt
appliesto:
  - ✅ <a href="https://learn.microsoft.com/en-us/windows/apps/winui/winui3/" target="_blank">WinUI</a>
  - ✅ <a href="hhttps://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/" target="_blank">Windows App SDK</a>
---

# Windowing overview for WinUI 3 and Windows App SDK

Windowing functionality in a WinUI app is provided by a combination of the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) class and the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class, both of which are based on the [Win32 HWND model](/windows/win32/winmsg/about-windows).

> [!div class="checklist"]
>
> - **Important APIs**: [Window class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window), [AppWindow class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see Windowing samples in action](winui3gallery:/category/MultipleWindows)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

**XAML Window**

In your app, the window object is an instance of the [Microsoft.UI.Xaml.Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) class (or a derived class) that represents the window in your program code. You create it directly with a call to the constructor. The XAML Window is where you attach your app content and manage the lifecycle of your app's windows.

**HWND**

The _application window_ is created by the Windows operating system and is represented by a Win32 window object. It's a system-managed container in which your content is hosted, and represents the entity that users interact with when they resize and move your app on-screen. (See [About Windows](/windows/win32/winmsg/about-windows) in the Win32 documentation for more info.)

After Windows creates the application window, the creation function returns a  _window handle_ (`HWND`) that uniquely identifies the window. A window handle has the HWND data type, although it's surfaced in C# as an [IntPtr](/dotnet/api/system.intptr). It's an opaque handle to an internal Windows data structure that corresponds to a window that's drawn by the OS and consumes system resources when present.

The HWND is created after the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) object, typically when the [Window.Activate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.activate) method is called.

**AppWindow**

The Windows App SDK provides additional windowing functionality through the [Microsoft.UI.Windowing.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class. AppWindow represents a high-level abstraction of the HWND. There's a 1:1 mapping between an AppWindow and a top-level HWND in your app. AppWindow and its related classes provide APIs that let you manage many aspects of your app's top-level windows without the need to  access the HWND directly.

The lifetime of an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) object and an HWND is the same—the AppWindow is available immediately after the window has been created; and it's destroyed when the window is closed.

**Windowing API diagram**

This diagram shows the relationship between the classes and APIs that you use to manage windows in your app, and which classes are responsible for each part of window management. In some cases, like `ExtendsContentIntoTitleBar`, the API is a member of [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) to make it available to other UI frameworks, but is also exposed on the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) class for convenience. The diagram is not comprehensive, but shows the most commonly used APIs.

:::image type="content" source="images/winui-windowing-diagram.png" lightbox="images/winui-windowing-diagram.png" alt-text="win u i windowing diagram":::

> [!NOTE]
> You can use [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) APIs with any UI framework that the Windows App SDK supports - Win32, WPF, WinForms, or WinUI. For frameworks other than WinUI, the functionality shown in the _XAML Window_ box of the diagram would be replaced by the appropriate framework-specific windowing APIs:
>
> - [WPF Window](/dotnet/api/system.windows.window)
> - [Windows Forms Form](/dotnet/api/system.windows.forms.form)
> - [Win32 windowing APIs](/windows/win32/winmsg/windows), [MFC Windows](/cpp/mfc/windows)

## Window/AppWindow API comparison

If you use WinUI XAML as your app's UI framework, both the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) and the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) APIs are available to you. Starting in Windows App SDK 1.4, you can use the [Window.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property to get an AppWindow object from an existing XAML window. With this AppWindow object you have access to the additional window management APIs.

> [!IMPORTANT]
> If you're not using WinUI 1.3 or later, use interop APIs to get the AppWindow in order to use the AppWindow APIs. For more about the interop APIs, see [Manage app windows - UI framework and HWND interop](manage-app-windows.md#ui-framework-and-hwnd-interop) and the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing).

### Lifetime management

| XAML Window | AppWindow |
|--|--|
| [Constructor](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.-ctor) | [Create](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.create), [GetFromWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.getfromwindowid) |
| [Activate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.activate) | [Show](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.show), [Hide](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.hide) |
| [Close](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.close), [Closed](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.closed) | [Destroy](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.destroy), [Destroying](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.destroying), [Closing](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.closing) |
| >>> | [Id](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.id), [OwnerWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.ownerwindowid) |

When you create a new WinUI project in Visual Studio, the project template provides a `MainWindow` class for you, which is a sub-class of [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window). If your app only needs one window, this is all you need. To learn how to create and manage additional windows, and why you might want to, see [Show multiple windows for your app](multiple-windows.md).

The [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class has APIs to create and destroy a new window; however, for a WinUI app, you won't do this in practice because there is no API to attach content to the window you create. Instead, you get an instance of AppWindow that's created by the system and associated with the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) and HWND. In WinUI 1.4 and later, you can use the [Window.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow) property to get the instance of AppWindow. In other cases, you can use the static [AppWindow.GetFromWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.getfromwindowid) method to get the AppWindow instance. See [Manage app windows](manage-app-windows.md) for more info.

### Content

| XAML Window | AppWindow |
|--|--|
| [Content](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.content)| N/A |

The [Window.Content](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.content) property is where you attach your app content to the window that displays it. You can do this in XAML or in code.


### Title, icon, and title bar

| XAML Window | AppWindow |
|--|--|
| [Title](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.title) | [Title](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.title) |
| [SetTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.settitlebar) | [TitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.titlebar) |
| >>> | [SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon), [SetTaskbarIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.settaskbaricon), [SetTitleBarIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.settitlebaricon) |
| [ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.extendscontentintotitlebar) | [AppWindow.TitleBar.ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) |

You can modify your app's title bar to varying degrees; setting the title and icon, changing the colors, or completely replacing the title bar with custom app content.

For information about using title bar APIs, including code examples, see [Title bar customization](/windows/apps/develop/title-bar).

### Size and position

| XAML Window | AppWindow |
|--|--|
| [Bounds](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.bounds) | [Position](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.position), [Size](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.size), [ClientSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.clientsize) |
| [SizeChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.sizechanged) | [Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) ([DidPositionChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didpositionchange#microsoft-ui-windowing-appwindowchangedeventargs-didpositionchange), [DidSizeChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didsizechange#microsoft-ui-windowing-appwindowchangedeventargs-didsizechange)) |
| >>> | [Move](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.move), [Resize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize), [ResizeClient](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resizeclient), [MoveAndResize](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveandresize) |
| >>> | [MoveInZOrderAtBottom](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveinzorderatbottom), [MoveInZOrderAtTop](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveinzorderattop), [MoveInZOrderBelow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.moveinzorderbelow) |

You use the [Window.Bounds](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.bounds) property and [SizeChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.sizechanged) event for managing things in your app UI, like moving elements around when the window size changes. XAML uses _[effective pixels](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design#effective-pixels-and-scale-factor)_ (epx), not actual physical pixels. Effective pixels are a virtual unit of measurement, and they're used to express layout dimensions and spacing, independent of screen density.

AppWindow, on the other hand, uses the [Window Coordinate System](/windows/win32/gdi/window-coordinate-system), where the basic unit of measurement is physical _device pixels_. You use the the AppWindow APIs for windowing actions, like resizing the window or moving it in relation to something else on the screen.

In some cases you might need to use measurements from one class in the other class, in which case you'll need to convert between effective pixels and device pixels. For example, if you set drag regions in a custom title bar, you'll need to convert measurements. For an example of how to use [XamlRoot.RasterizationScale](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.rasterizationscale) to convert measurements, see the [interactive content](/windows/apps/develop/title-bar#interactive-content) section of the _Title bar customization_ article.

### Appearance and behavior

| XAML Window | AppWindow |
|--|--|
| [SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop) | N/A |
| >>> | [Presenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.presenter), [SetPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter) |
| >>> | [IsShownInSwitchers](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.isshowninswitchers) |
| [Visible](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.visible), [VisibilityChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.visibilitychanged) | [IsVisible](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.isvisible), [Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) ([DidVisibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didvisibilitychange#microsoft-ui-windowing-appwindowchangedeventargs-didvisibilitychange)) |
| [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcherqueue) | [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.dispatcherqueue), [AssociateWithDispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.associatewithdispatcherqueue) |
| [Compositor](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.compositor) | N/A |

XAML Window APIs are generally responsible for the appearance of your app content, like the background. For more info about [SystemBackdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.systembackdrop), see [Apply Mica or Acrylic materials](system-backdrops.md).

AppWindow APIs are responsible for the _non-client_ portion of the window and your app's interaction with the Windows OS.

> [!NOTE]
> The XAML Window class has several properties that were carried over from the UWP [Windows.UI.Xaml.Window](/uwp/api/windows.ui.xaml.window) class, but are not supported in WinUI apps. These properties always have a `null` value and are not used in WinUI apps: `CoreWindow`, `Current`, and `Dispatcher`.

## Track the current window

Even though the `Current` property is not supported in WinUI apps, you still might need to access the Window APIs from other places in your app. For example, you might need to get the Window bounds or handle the [Window.SizeChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.sizechanged) event from code for a [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page). In this case, you can provide access to the Window in a similar way to the `Current` property by using a public static property on your `App` class.

To do so, modify the Window declaration in the App class as shown here.

```csharp
// App.xaml.cs in a WinUI app
public partial class App : Application
{
    ...
    public static Window Window { get { return m_window; } }
    private static Window m_window;
}
```

```cppwinrt
// App.xaml.h in a WinUI app
...
struct App : AppT<App>
{
    ...
    static winrt::Microsoft::UI::Xaml::Window Window(){ return window; };

private:
    static winrt::Microsoft::UI::Xaml::Window window;
};
...

// App.xaml.cpp
...
winrt::Microsoft::UI::Xaml::Window App::window{ nullptr };
...
```

Then, to access the Window from other places in your app, use `App.Window`, like this:

```csharp
// MainPage.xaml.cs in a WinUI app
var width = App.Window.Bounds.Width;
```

```cppwinrt
// MainPage.xaml.cpp in a WinUI app
#include <App.xaml.h>
auto width{ App::Window().Bounds().Width };
```

> [!IMPORTANT]
> Using a static `Window` property in your `App` class is useful if your app only uses a single window. If your app uses multiple windows, you should use [WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid) to track the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) instances instead, to ensure that you are accessing the correct instance of Window. See [Show multiple windows for your app](multiple-windows.md) for more info and examples.

## Related topics

- [Manage app windows](manage-app-windows.md)
- [Windowing functionality migration](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing.md)
- [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md)
- [Retrieve a window handle (HWND)](retrieve-hwnd.md)
- [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing)
