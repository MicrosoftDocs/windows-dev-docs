---
title: Windows UI Library (WinUI)
description: WinUI Libraries for Windows app development. 
ms.topic: article
ms.date: 07/20/2021
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, Windows UI Library
---

# Windows UI Library (WinUI)

:::row:::
    :::column:::
![WinUI logo](../images/logo-winui.png)

    :::column-end:::
    :::column span="2":::

The Windows UI Library (WinUI) is a native user experience (UX) framework for both Windows desktop and UWP applications.

By incorporating the [Fluent Design System](https://www.microsoft.com/design/fluent/#/) into all experiences, controls, and styles, WinUI provides consistent, intuitive, and accessible experiences using the latest user interface (UI) patterns.

With support for both desktop and UWP apps, you can build with WinUI from the ground up, or gradually migrate your existing MFC, WinForms, or WPF apps using familiar languages such as C++, C#, Visual Basic, and Javascript (via [React Native for Windows](https://microsoft.github.io/react-native-windows/)).

    :::column-end:::
:::row-end:::

> [!Important]
> At this time, there are two generations of the Windows UI Library (WinUI) under active development: WinUI 2 and WinUI 3. While both can be used in production-ready apps on Windows 10 and later, each have different development targets and release schedules.

## Comparison of WinUI 3 and WinUI 2

The following table highlights some of most significant differences between WinUI 3 and WinUI 2.

| WinUI 3                                                                                                                                                                                                                                                                                                        | WinUI 2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **[WinUI 3](#windows-ui-3-library)** ships with the [Windows App SDK](../windows-app-sdk/index.md). | **[WinUI 2](#windows-ui-2-library)**, the 2nd generation of WinUI, ships as a standalone [NuGet package](https://www.nuget.org/packages/Microsoft.UI.Xaml/), and is integrated with [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/). |
| UX stack and control library completely decoupled from the OS and [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/), including the core framework, composition, and input layers of the UX stack.                                                                        | UX stack and control library tightly coupled to the OS and [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/).                                                                                                                                                                                                                                                                                                                                                          |
| WinUI 3 can be used to build production-ready **desktop/Win32** Windows apps. | WinUI 2 can be used in UWP applications and incorporated into new or existing desktop applications using [XAML Islands](../desktop/modernize/xaml-islands.md) (for installation instructions, see [Getting started with the WinUI 2 Library](winui2/getting-started.md)). |
| WinUI 3 ships as a component of [the Windows App SDK](../windows-app-sdk/index.md) framework package, with Visual Studio project templates in the Windows App SDK Visual Studio Extension (VSIX). | Part of WinUI 2 ships within the operating system itself (the Windows.UI.* family of UWP WinRT APIs) and part of it ships as a library (“Windows UI Library 2”) with additional controls, elements and the latest styles on top of what’s already included in the operating system itself. With WinUI 2, these features ship in a downloadable NuGet package. However, other significant parts of the UI stack are still built-in to the OS, such as the core XAML framework, input, and composition layers. |
| WinUI 3 supports C# (.NET 5) and C++ for desktop apps. | WinUI 2 supports C# and Visual Basic (.NET Native), and C++ apps. |
| WinUI 3 support for production-ready UWP apps is currently in preview, see [WinUI 3 - Project Reunion 0.5 Preview](winui3/release-notes/winui3-project-reunion-0.5-preview.md).                                                                                                                                | WinUI 2 can be incorporated into production UWP apps by installing a NuGet package into a new or existing UWP project. WinUI controls and styles can then be referenced directly in new apps, or by updating "Windows.UI." namespace references to "Microsoft.UI." in existing apps.                                                                                                                                                                                    |
| WinUI 3 supports the Chromium-based [WebView2](/microsoft-edge/webview2/) control |  WinUI 2 supports the  [WebView](/windows/uwp/design/controls-and-patterns/web-view) control on all devices, and the [WebView2](/microsoft-edge/webview2/) control for Desktop only. |
| WinUI 3 works downlevel to Windows 10 October 2018 Update (Version 1809, OS build 17763). | WinUI 2 works downlevel to Windows 10 Creators Update (Version 1703, OS build 15063). |

## Windows UI 3 Library

WinUI 3 is the native UI platform component that ships with the [Windows App SDK](../windows-app-sdk/index.md) (completely decoupled from [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/)). The Windows App SDK provides a unified set of APIs and tools that can be used to create production desktop apps that target Windows 10 and later, and can be published to the Microsoft Store.

For more details, see the Windows App SDK [overview and release notes](../windows-app-sdk/stable-channel.md#version-08).

To provide feedback and log suggestions and issues, please use the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml).

> [!NOTE]
> The **Windows App SDK** was previously known as **Project Reunion**. Some assets, such as the VSIX extension and NuGet packages, still use this code name (these will be renamed in a future release). 
>
>**Windows App SDK** is used in all documentation except where a specific release or asset still refers to **Project Reunion**.

![WinUI 3 platform support](../images/platforms-winui3.png)

### Related links for WinUI 3

- [Windows App SDK](../windows-app-sdk/index.md)
- [Stable release channel for the Windows App SDK](../windows-app-sdk/stable-channel.md)
- [API docs](/windows/winui/api/)
- [Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples)

## Windows UI 2 Library

The WinUI 2 Library can be used in UWP applications and incorporated into new or existing desktop applications using [XAML Islands](../desktop/modernize/xaml-islands.md) (for installation instructions, see [Getting started with the WinUI 2 Library](winui2/getting-started.md)).

WinUI 2 is tightly integrated with [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/) and provides official native Windows UI controls and other UI elements for UWP apps. Maintaining down-level compatibility with earlier versions of Windows 10, enables WinUI 2 controls to work even if users don't have the latest OS.

For details on the latest version, see the WinUI 2 [overview and release notes](winui2/index.md)

For details on the work planned for the next release and to provide feedback, see the [WinUI 2.8 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/14).

![WinUI 2 platform support](../images/platforms-winui2.png)

### Related links for WinUI 2

- [WinUI 2 Library overview](winui2/index.md)
- [API docs](/windows/winui/api/)
- [Source code](https://aka.ms/winui)
- [XAML Controls Gallery app](https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt)

## WinUI resources

**Github**: WinUI is an open-source project hosted on Github. Use the [WinUI repo](https://github.com/microsoft/microsoft-ui-xaml), to file feature requests or bugs, interact with the WinUI team, and view the team's plans for WinUI 3 and beyond on their [roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md).

**Website**: The [WinUI website](https://aka.ms/winui) has product comparisons, explains the various advantages of WinUI, and provides ways to stay engaged with the product and the product team.
