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

> [!Important]
> Two generations of WinUI are under active development, each with their own release schedule. Depending on your requirements, you can choose from:
>
> - **[WinUI 3](#windows-ui-3-library)**, the newest generation of the native Windows UX stack that ships with the [Windows App SDK](../windows-app-sdk/index.md) (decoupled from [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/)).
> - **[WinUI 2](#windows-ui-2-library)**, the 2nd generation of WinUI, ships as a standalone [NuGet package](https://www.nuget.org/packages/Microsoft.UI.Xaml/), and is integrated with [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/).

    :::column-end:::
:::row-end:::

By incorporating the [Fluent Design System](https://www.microsoft.com/design/fluent/#/) into all experiences, controls, and styles, WinUI provides consistent, intuitive, and accessible experiences using the latest user interface (UI) patterns.

With support for both desktop and UWP apps, you can build with WinUI from the ground up, or gradually migrate your existing MFC, WinForms, or WPF apps using familiar languages such as C++, C#, Visual Basic, and Javascript (via [React Native for Windows](https://microsoft.github.io/react-native-windows/)).

## Windows UI 3 Library

WinUI 3 is the native UI platform component that ships with the [Windows App SDK](../windows-app-sdk/index.md) (completely decoupled from [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/)). The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on a broad set of target Windows 10 and later OS versions.

> [!Important]
> WinUI 3 - Windows App SDK 0.8 is the latest stable, supported version of WinUI 3. With this version of WinUI 3, you can create production apps and publish them to the Microsoft Store. For more details, see the [overview and release notes](../windows-app-sdk/stable-channel.md#version-08).
>
> Please use the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml) to provide feedback and log suggestions and issues.

> [!NOTE]
> The Windows App SDK was previously called **Project Reunion**, which was a temporary code name. Some assets such as the VSIX extension and NuGet packages still use the code name, but these files will be renamed in a future release. Most of the documentation now uses the name **Windows App SDK** except when referring to a specific release or asset that still uses the previous code name.

![WinUI 3 platform support](../images/platforms-winui3.png)

### Related links for WinUI 3

- [Windows App SDK](../windows-app-sdk/index.md)
- [Stable release channel for the Windows App SDK](../windows-app-sdk/stable-channel.md)
- [API docs](/windows/winui/api/)
- [Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples)

## Windows UI 2 Library

WinUI 2 can be used in UWP applications and incorporated into new or existing desktop applications using [XAML Islands](../desktop/modernize/xaml-islands.md) (for installation instructions, see [Getting started with the WinUI 2 Library](winui2/getting-started.md)).

> [!NOTE]
> WinUI 2.6 is the latest release. For details, see the [overview and release notes](winui2/index.md)
>
> For details on the work planned for the next release, see the [WinUI 2.7 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/12).

The WinUI 2 Library is tightly integrated with [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/) and provides official native Windows UI controls and other UI elements for UWP apps.

By maintaining down-level compatibility with earlier versions of Windows 10, WinUI 2 controls work even if users don't have the latest OS.

![WinUI 2 platform support](../images/platforms-winui2.png)

### Related links for WinUI 2

- [WinUI 2 Library overview](winui2/index.md)
- [API docs](/windows/winui/api/)
- [Source code](https://aka.ms/winui)
- [XAML Controls Gallery app](https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt)

## WinUI resources

**Github**: WinUI is an open-source project hosted on Github. Use the [WinUI repo](https://github.com/microsoft/microsoft-ui-xaml), to file feature requests or bugs, interact with the WinUI team, and view the team's plans for WinUI 3 and beyond on their [roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md).

**Website**: The [WinUI website](https://aka.ms/winui) has product comparisons, explains the various advantages of WinUI, and provides ways to stay engaged with the product and the product team.
