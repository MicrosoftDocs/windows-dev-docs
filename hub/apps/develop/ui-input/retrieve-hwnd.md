---
title: Retrieve a window handle (HWND)
description: This topic shows you how, in a desktop app, to retrieve the window handle for a window.
ms.topic: article
ms.date: 08/28/2024
keywords: Windows, App, SDK, desktop, C#, C++, cpp, window, handle, HWND, WinUI
ms.localizationpriority: medium
---

# Retrieve a window handle (HWND)

This topic shows you how, in a desktop app, to retrieve the window handle for a window. The scope covers [WinUI 3](../../winui/winui3/index.md), [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/), and [Windows Forms (WinForms)](/dotnet/desktop/winforms/) apps; code examples are presented in C# and [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/).

The development and UI frameworks listed above are (behind the scenes) built on the [Win32 API](/windows/win32/). In Win32, a [window](/windows/win32/winmsg/about-windows) object is identified by a value known as a [window handle](/windows/win32/winmsg/about-windows#window-handle). And the type of a window handle is an **[HWND](/windows/win32/winprog/windows-data-types)** (although it surfaces in C# as an [**IntPtr**](/dotnet/api/system.intptr)). In any case, you'll hear the term **HWND** used as a shorthand for *window handle*.

There are several reasons to retrieve the **HWND** for a window in your WinUI 3, WPF, or WinForms desktop app. One example is to use the **HWND** to interoperate with certain Windows Runtime (WinRT) objects that depend on a **CoreWindow** to display a user-interface (UI). For more info, see [Display WinRT UI objects that depend on CoreWindow](./display-ui-objects.md).

## WinUI 3 with C#

The C# code below shows how to retrieve the window handle (HWND) for a WinUI 3 [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) object. This example calls the **GetWindowHandle** method on the **WinRT.Interop.WindowNative** C# interop class. For more info about the C# interop classes, see [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

```csharp
// MainWindow.xaml.cs
private async void myButton_Click(object sender, RoutedEventArgs e)
{
    // Retrieve the window handle (HWND) of the current WinUI 3 window.
    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
}
```

## WinUI 3 with C++

The C++/WinRT code below shows how to retrieve the window handle (HWND) for a WinUI 3 [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) object. This example calls the [**IWindowNative::get_WindowHandle**](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nf-microsoft-ui-xaml-window-iwindownative-get_windowhandle) method.

```cppwinrt
// pch.h
...
#include <microsoft.ui.xaml.window.h>

// MainWindow.xaml.cpp
void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
{
    // Retrieve the window handle (HWND) of the current WinUI 3 window.
    auto windowNative{ this->m_inner.as<::IWindowNative>() };
    HWND hWnd{ 0 };
    windowNative->get_WindowHandle(&hWnd);
}
```

## WPF with C#

The C# code below shows how to retrieve the window handle (HWND) for a WPF window object. This example uses the [**WindowInteropHelper**](/dotnet/api/system.windows.interop.windowinterophelper) class.

```csharp
// MainWindow.xaml.cs
private void Button_Click(object sender, RoutedEventArgs e)
{
    var wih = new System.Windows.Interop.WindowInteropHelper(this);
    var hWnd = wih.Handle;
}
```

## WinForms with C#

The C# code below shows how to retrieve the window handle (HWND) for a WinForms form object. This example uses the [**NativeWindow.Handle**](/dotnet/api/system.windows.forms.nativewindow.handle) property.

```csharp
// Form1.cs
private void button1_Click(object sender, EventArgs e)
{
    var hWnd = this.Handle;
}
```

## Determining the window that's hosting a visual element

From a visual element, you can access [UIElement.XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.xamlroot); then [XamlRoot.ContentIslandEnvironment](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.contentislandenvironment); then the [ContentIslandEnvironment.AppWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentislandenvironment.appwindowid) property contains the ID of the top-level Win32 HWND.

## Related topics

* [Display WinRT UI objects that depend on CoreWindow](./display-ui-objects.md)
* [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md)
* [WinUI 3](../../winui/winui3/index.md)
* [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
* [Windows Forms (WinForms)](/dotnet/desktop/winforms/)
* [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/)
* [Win32 API](/windows/win32/)