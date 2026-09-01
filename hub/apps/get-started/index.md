---
title: Choose a Windows development path
description: Choose a path for a new WinUI 3 app, an existing desktop app, a UWP migration, or an app that targets multiple platforms.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 08/31/2026
keywords: windows, desktop development, windows sdk, windows app sdk, winui
ms.localizationpriority: medium
#customer intent: As a developer, I want to choose the recommended path for a new app or find guidance for my existing app.
---

# Choose a Windows development path

For a new native Windows desktop app, use [WinUI 3](../winui/winui3/index.md) with the [Windows App SDK](../windows-app-sdk/index.md). WinUI 3 is Microsoft's recommended native UI framework for new Windows apps, and the Windows App SDK is the recommended development platform.

If you maintain an existing WPF, Windows Forms, or Win32 app, you don't necessarily need to rewrite it. You can add Windows App SDK features to your current app or migrate its UI to WinUI 3. If you maintain a UWP app, follow the migration path to the Windows App SDK and WinUI 3.

## Choose your path

| Your scenario | Recommended path |
| --- | --- |
| You are starting a new native Windows desktop app | Use [WinUI 3 with the Windows App SDK](../winui/winui3/index.md). |
| You maintain a WPF or Windows Forms app | Keep your current UI framework and [add Windows App SDK features](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md), or migrate your UI to WinUI 3. |
| You maintain a C++ Win32 app | Continue to use Win32 where required and [add Windows App SDK features](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md). |
| You maintain a UWP app | [Plan a migration to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md). |
| Your app must share its UI and code across operating systems | Review the [cross-platform framework documentation](#find-documentation-for-another-framework). |
| You want to make a website installable on Windows | Review the [Progressive Web Apps documentation](/microsoft-edge/progressive-web-apps-chromium/). |

## Start a new app with WinUI 3

:::image type="content" source="images/winui-header.png" alt-text="WinUI logo.":::

WinUI 3 provides the native UI framework for Windows desktop apps. It uses XAML with C# or C++ and ships as part of the Windows App SDK.

> [!div class="nextstepaction"]
> [Build your first WinUI 3 app](start-here.md)

For more information, see the [WinUI 3 overview](../winui/winui3/index.md), [Windows App SDK overview](../windows-app-sdk/index.md), and [WinUI 3 tutorial](../tutorials/winui-notes/intro.md).

## Modernize or migrate an existing app

You can adopt Windows platform features incrementally without changing your existing UI framework. The Windows App SDK supports existing WPF, Windows Forms, and C++ Win32 apps.

- To add windowing, app lifecycle, text rendering, and other Windows App SDK features, see [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).
- To compare incremental modernization with a UI migration, see [Modern Windows features for desktop apps](../desktop/modernize/index.md).
- To move an existing UI layer to WinUI 3, see [Migrate to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md).
- If you maintain a UWP app, see [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md).

## Find documentation for another framework

Use these links when you maintain an app built with another framework or have a requirement that WinUI 3 doesn't address, such as sharing one UI across multiple operating systems.

| Technology | Documentation |
| --- | --- |
| WPF | [WPF overview](/dotnet/desktop/wpf/overview/) |
| Windows Forms | [Windows Forms overview](/dotnet/desktop/winforms/overview/) |
| Win32 and C++/WinRT | [Win32 desktop programming](/windows/win32/desktop-programming/) and [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) |
| UWP | [UWP documentation](/windows/uwp/) and [migration guidance](../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md) |
| .NET MAUI | [.NET MAUI overview](/dotnet/maui/what-is-maui) |
| React Native for Desktop | [React Native for Windows](../../dev-environment/javascript/react-native-for-windows.md) |
| Electron | [Get started with Electron on Windows](../dev-tools/winapp-cli/guides/electron-index.md) |
| Progressive Web Apps | [Progressive Web Apps on Windows](/microsoft-edge/progressive-web-apps-chromium/) |

## Understand the Windows SDKs

The [Windows App SDK](../windows-app-sdk/index.md) provides a set of APIs and tools for desktop apps, including WinUI 3, app lifecycle, windowing, and notifications. It ships through NuGet on a release cycle that is separate from Windows. You can use it when creating a WinUI 3 app or add many of its features to an existing desktop app.

The [Windows SDK](../windows-sdk/index.md) provides the headers, libraries, metadata, and tools for accessing Windows operating system APIs. Use it when your app needs Win32, Windows Runtime, DirectX, or other operating system capabilities.
