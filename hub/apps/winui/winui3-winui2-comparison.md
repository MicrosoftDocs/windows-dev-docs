---
title: Comparison of WinUI 3 and WinUI 2
description: Quick comparison of WinUI 3 and WinUI 2.
ms.topic: article
ms.date: 03/19/2021
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, Windows UI Library
---

# Comparison of WinUI 3 and WinUI 2

:::image type="content" source="../images/logo-winui2-vs-winui3.png" alt-text="Win UI 2 vs Win UI 3 logos":::

At this time, there are two unique generations of the Windows UI Library (WinUI): WinUI 2 and WinUI 3. While both provide user interfaces and experiences based on the latest [Fluent Design System](https://www.microsoft.com/design/fluent) principles and processes, each have different development targets and scope with different development tracks and release schedules.

Both [WinUI 2](winui2/index.md) and [WinUI 3](winui3/index.md) support development of production-ready apps on Windows 10 and later OS versions, and both support using C# or C++ to build your app.

Both are under active development with updates released on a regular basis.

## The major differences

| WinUI 3                                                                                                                                                                                                                                                                                                        | WinUI 2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| UX stack and control library completely decoupled from the OS and [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/), including the core framework, composition, and input layers of the UX stack.                                                                        | UX stack and control library tightly coupled to the OS and [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/).                                                                                                                                                                                                                                                                                                                                                          |
| WinUI 3 can be used to build production-ready Windows **desktop/Win32** apps. | WinUI 2 cannot be used to build  Windows **desktop/Win32** apps. |
| WinUI 3 ships as a component of [the Windows App SDK](../windows-app-sdk/index.md) framework package, with Visual Studio project templates in the Windows App SDK Visual Studio Extension (VSIX). | Part of WinUI 2 ships within the operating system itself (the Windows.UI.* family of UWP WinRT APIs) and part of it ships as a library (“Windows UI Library 2”) with additional controls, elements and the latest styles on top of what’s already included in the operating system itself. With WinUI 2, these features ship in a downloadable NuGet package. However, other significant parts of the UI stack are still built-in to the OS, such as the core XAML framework, input, and composition layers. |
| WinUI 3 supports C# and .NET 5 for desktop apps. | WinUI 2 supports C# and .NET Native apps only. |
| WinUI 3 support for production-ready UWP apps is currently in preview, see [WinUI 3 - Project Reunion 0.5 Preview](winui3/release-notes/winui3-project-reunion-0.5-preview.md).                                                                                                                                | WinUI 2 can be incorporated into production UWP apps by installing a NuGet package into a new or existing UWP project. WinUI controls and styles can then be referenced directly in new apps, or by updating "windows.ui." namespace references to "microsoft.ui." in existing apps.                                                                                                                                                                                    |
| WinUI 3 supports the Chromium-based [WebView2](/microsoft-edge/webview2/) control |  WinUI 2 supports the  [WebView](/windows/uwp/design/controls-and-patterns/web-view) control |
| WinUI 3 works downlevel to Windows 10 October 2018 Update (Version 1809, OS build 17763). | WinUI 2 works downlevel to Windows 10 Creators Update (Version 1703, OS build 15063). |

## See also

- [Windows App SDK](../windows-app-sdk/index.md)
- [Stable release channel for the Windows App SDK](../windows-app-sdk/stable-channel.md)
- [API docs](/windows/winui/api/)
- [Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples)
