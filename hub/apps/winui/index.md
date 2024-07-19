---
title: WinUI
description: WinUI Libraries for Windows app development. 
ms.topic: article
ms.date: 07/19/2024
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
---

# WinUI

:::row:::
    :::column:::
![WinUI logo](../images/logo-winui.png)

    :::column-end:::
    :::column span="2":::

> [!IMPORTANT]
> At this time, there are two generations of WinUI: [WinUI 2 for UWP](winui2/index.md) and [WinUI in the Windows App SDK (WinUI 3)](winui3/index.md). While both can be used in production-ready apps on Windows 10 and later, each have different development targets.
>
> See [Comparison of WinUI 3 and WinUI 2](#comparison-of-winui-3-and-winui-2).

    :::column-end:::
:::row-end:::

WinUI is a native user experience (UX) framework for both Windows desktop and UWP applications.

By incorporating the [Fluent Design System](https://fluent2.microsoft.design/) into all experiences, controls, and styles, WinUI provides consistent, intuitive, and accessible experiences using the latest user interface (UI) patterns.

With support for both desktop and UWP apps, you can build with WinUI from the ground up, or gradually migrate your existing MFC, WinForms, or WPF apps using familiar languages such as C++, C#, Visual Basic, and JavaScript (using [React Native for Windows](https://microsoft.github.io/react-native-windows/)).

> The WinUI libraries are hosted in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml) where you can file feature requests or bugs, and interact with the WinUI where you can file feature requests or bugs, and interact with the WinUI team.

## Comparison of WinUI 3 and WinUI 2

The following table highlights some of most significant differences between WinUI 3 in the Windows App SDK and WinUI 2 for UWP.

| WinUI 3 in the Windows App SDK | WinUI 2 for UWP |
|-|-|
| **[WinUI 3](winui3/index.md)** ships with the [Windows App SDK](../windows-app-sdk/index.md). | **[WinUI 2](winui2/index.md)**, the 2nd generation of WinUI, ships as a standalone [NuGet package](https://www.nuget.org/packages/Microsoft.UI.Xaml/), and is integrated with [Windows SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/). |
| UX stack and control library completely decoupled from the OS and [Windows SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/), including the core framework, composition, and input layers of the UX stack. | UX stack and control library tightly coupled to the OS and [Windows SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/). |
| WinUI 3 can be used to build production-ready **desktop/Win32** Windows apps. | WinUI 2 can be used in UWP applications and incorporated into desktop applications using [XAML Islands](../desktop/modernize/xaml-islands.md) (for installation instructions, see [Getting started with the WinUI 2 Library](winui2/getting-started.md)). |
| WinUI 3 ships as a component of [the Windows App SDK](../windows-app-sdk/index.md) framework package, with Visual Studio project templates in the Windows App SDK Visual Studio Extension (VSIX). | Part of WinUI 2 ships within the operating system itself (the Windows.UI.* family of UWP WinRT APIs) and part of it ships as a library ("WinUI 2") with additional controls, elements and the latest styles on top of what's already included in the operating system itself. With WinUI 2, these features ship in a downloadable NuGet package. However, other significant parts of the UI stack are still built-in to the OS, such as the core XAML framework, input, and composition layers. |
| WinUI 3 supports C# (.NET 6 and later) and C++ for desktop apps. | WinUI 2 supports C# and Visual Basic (.NET Native), and C++ apps. |
| WinUI 3 is supported only in desktop-based projects. To use WinUI 3, UWP projects can migrate their project type to desktop (see [how to migrate your UWP app to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md)). | WinUI 2 can be incorporated into production UWP apps by installing a NuGet package into a new or existing UWP project. WinUI controls and styles can then be referenced directly in new apps, or by updating "Windows.UI." namespace references to "Microsoft.UI." in existing apps. |
| WinUI 3 supports the Chromium-based [WebView2](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.webview2) control | WinUI 2 supports the [WebView](/uwp/api/windows.ui.xaml.controls.webview) control on all devices, and starting with WinUI 2.8, the [WebView2](/windows/winui/api/microsoft.ui.xaml.controls.webview2) control on Desktop. |
| WinUI 3 works downlevel to Windows 10 October 2018 Update (Version 1809, OS build 17763). | WinUI 2.0 - 2.7 works downlevel to Windows 10 Creators Update (Version 1703, OS build 15063). WinUI 2.8 and later works downlevel to Windows 10 October 2018 Update (Version 1809, OS build 17763). |

### See also

- [Windows App SDK](../windows-app-sdk/index.md)
- [Stable channel release notes for the Windows App SDK](../windows-app-sdk/stable-channel.md)
- [Windows App SDK and supported Windows releases](../windows-app-sdk/support.md)
- [Windows App SDK API docs](/windows/windows-app-sdk/api/winrt/)
- [WinUI 2 API docs](/windows/winui/api/)
- [Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples)
