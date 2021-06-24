---
description: Overview of Windowing APIs in the Windows App SDK
title: Manage app windows (Windows App SDK)
ms.topic: article
ms.date: 03/31/2021
keywords: windowing, window, Windows App SDK
ms.author: rokarman
author: rkarman
ms.localizationpriority: medium
---

# Manage app windows

> [!IMPORTANT]
> Windowing APIs are an experimental feature that is currently supported only in the [experimental release channel](../experimental-channel.md) of the Windows App SDK, and the API surface is subject to change. This feature is not supported for use by apps in production environments.

The Windows App SDK provides an **AppWindow** class that is similar to, but not the same as, the UWP **AppWindow** - it is a high-level windowing API that allows for easy-to-use windowing scenarios that integrates well with the Windows and other apps. Our goal with the Windows App SDK is to have a windowing API that lifts app developers from system specific APIs/currencies, to bridge the gap between the Win32 and UWP world, all while maintaining a way for Win32 developers who have a lot of investment in their current apps around windowing to not have to rewrite that code.

For the Windows App SDK version of **AppWindow**, we're taking the opportunity to address some of the major feedback we've gotten for the UWP **AppWindow** - the most significant one in this release being that **AppWindow** in the Windows App SDK does not rely on async patterns, and it provides immediate feedback to your app as to whether or not API calls succeeded.

## Description

**AppWindow** represents a high-level abstraction of a system-managed container of the content of an app. This is the container in which your content is hosted, and represents the entity that users interact with when they resize and move your app on screen. For developers familiar with Win32, the **AppWindow** can be seen as a high-level abstraction of the HWND. For developers familiar with UWP, the **AppWindow** can be seen as a replacement for CoreWindow/ApplicationView/AppWindow.

For this version of **AppWindow** we're only supporting top-level HWNDs (there's a 1:1 mapping between an **AppWindow** and a top-level HWND). We might change this in the future or introduce other API surfaces to create, manipulate, and manage child windows.

## Limitations

The version of **AppWindow** in this release is currently available only to Win32 apps (both packaged and unpackaged).

We do not have samples and documentation ready for this feature area yet, they will follow the API spec which is targeted for June. Until then, please see below for a very limited code snippet that shows how to interop with WinUI 3. The same interop-pattern can also be used for any other existing top-level HWNDs in your app.

### Sample for getting an AppWindow for a WinUI 3 window

```c++
// The include- and using-statements below are in addition to the ones you need for WinUI3
#include <winrt/Microsoft.UI.Windowing.h>

#include <microsoft.ui.windowing.core.interop.h>
#include <windowingcorefeatureapi.h>

namespace abi
{
    using namespace ABI::Microsoft::UI::Windowing;
}

namespace winrt
{
    using namespace Microsoft::UI::Windowing;
}

void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
{
    HWND hWnd;
    abi::WindowId windowId;
    winrt::AppWindow appWindow = nullptr;

    // Get the HWND for the XAML Window.
    Window window = this->try_as<Window>();
    window.as<IWindowNative>()->get_WindowHandle(&hWnd);

    // Get the WindowId for the HWND.
    if(SUCCEEDED(TryGetWindowIdFromDesktopWindow(hWnd, &windowId)))
    {
        // Get the AppWindow for the WindowId.
        appWindow = winrt::AppWindow::GetFromWindowId({ windowId.Value });
        if(appWindow)
        {
            // You now have an AppWindow object and can call its methods to manipulate the window.
            // Just to do something here, let's change the size of the window...
            appWindow.SetSize({200,200});
        }
    }
}
```
