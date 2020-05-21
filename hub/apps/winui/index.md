---
title: Windows UI Library (WinUI)
description: WinUI Libraries for Windows app development. 
ms.topic: article
ms.date: 05/11/2020
keywords: windows 10, uwp, toolkit sdk, winui, Windows UI Library
---

# Windows UI Library (WinUI)

![Toolkits hero image](../images/logo-winui.png)

The Windows UI Library (WinUI) is the modern native user interface (UI) platform for Windows apps across both Win32 and UWP.

By incorporating the [Fluent Design System](https://www.microsoft.com/design/fluent/#/) into all controls and styles, WinUI provides consistent, intuitive, and accessible experiences using the latest UI patterns.

And with support for both Win32 and UWP apps, you can build with WinUI from the ground up or migrate your existing MFC, WinForms, or WPF apps at your own pace and using familiar languages such as C++, C#, Visual Basic, and even Javascript via [React Native for Windows](https://microsoft.github.io/react-native-windows/).

There are two versions of WinUI to be aware of, **WinUI 2.x** and **WinUI 3.0**.

## Windows UI 2.x Library

WinUI 2.x can be used right now in UWP applications and incorporated into your existing desktop applications through [XAML Islands](/windows/apps/desktop/modernize/xaml-islands).

The WinUI 2.x Library is closely coupled to the UWP SDK and provides official native Windows UI controls and other UI elements for UWP apps.

By maintaining down-level compatibility with earlier versions of Windows 10, WinUI 2.x controls work even if users don't have the latest OS.

![WinUI 2.x platform support](../images/platforms-winui2.png)

> [!NOTE]
> WinUI 2.4 is the latest WinUI 2.x release. See the [WinUI 2.5 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/10) for a list of work planned in the next release.

For installation instructions see [Getting started with the Windows UI Library](winui2/getting-started.md).

### Related links for WinUI 2.x

- [WinUI 2.x Library overview](winui2/index.md)
- [API docs](https://docs.microsoft.com/uwp/api/overview/winui/)
- [Source code](https://aka.ms/winui)
- [XAML Controls Gallery app](https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt)

## Windows UI 3.0 Library (Preview 1)

WinUI 3 is the next version of WinUI, a native Windows 10 UI platform completely decoupled from the UWP SDK.

By completely decoupling Xaml, composition, and input APIs from the UWP SDK, the Windows UI 3.0 Library can greatly expand the scope of WinUI to include the full Windows 10 native UI platform.

WinUI serves as the path forward for all Windows apps - you can use it as the UI layer on your native UWP or Win32 app, or you can use it to modernize your desktop app piece by piece with [Xaml Islands](https://docs.microsoft.com/windows/apps/desktop/modernize/xaml-islands).

> [!NOTE]
> The WinUI 3.0 Preview 1 is a pre-release build of WinUI 3.0. We welcome your feedback in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml).

All new Xaml features will ship as part of WinUI. The existing UWP Xaml APIs that ship as part of the OS will no longer receive new feature updates. However, they will continue to receive security updates and critical fixes according to the Windows 10 support lifecycle.

The Universal Windows Platform contains more than just the Xaml framework (such as application and security models, media pipeline, Xbox and Windows 10 shell integrations, broad device support) and will continue to be supported.

![WinUI 3.0 platform support](../images/platforms-winui3.png)

> [!Important]
> WinUI 3.0 Preview 1 is intended for early evaluation and to gather feedback from the developer community. It should **NOT** be used for production apps.

### Related links for WinUI 3.0

- [WinUI 3.0 Preview 1 (May 2020)](winui3/index.md)
- [XAML Controls Gallery (WinUI 3.0 Preview 1) app](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3preview)

## WinUI resources

**Github**: WinUI is an open-source project hosted on Github. On the [WinUI repository](https://github.com/microsoft/microsoft-ui-xaml), you can file feature requests or bugs, interact with the WinUI team, and view the team's plans for WinUI 3 and beyond on their [roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md).

**Website**: The [WinUI website](https://aka.ms/winui) has helpful info on product comparisons, WinUI's advantages, and ways of staying engaged with the product and the product team.
