---
description: This section of the documentation provides information about APIs and features you can use while developing a Windows desktop apps.
title: Develop Windows desktop apps
ms.topic: article
ms.date: 08/17/2021
ms.localizationpriority: medium
---

# Develop Windows desktop apps

This section of the documentation provides information about APIs and features you can use while developing Windows desktop apps. Some of these features are available via the Windows App SDK. Other features are provided by the Windows OS (via the Windows SDK) and .NET, and don't require use of the Windows App SDK.

For information about setting up your development environment and getting started creating a new app, see the following articles:

- [Install tools for Windows app development](../windows-app-sdk/set-up-your-development-environment.md)
- [Get started developing apps](../get-started/index.md)

## Windows App SDK

The [Windows App SDK](../windows-app-sdk/index.md) provides a unified set of features for Windows apps. Unlike the traditional Windows SDK, which is tightly integrated with Windows OS releases, the Windows App SDK is completely decoupled from the Windows OS. This means that the Windows App SDK can be used in a consistent way by apps on Windows 11 and downlevel to Windows 10, version 1809.

The Windows App SDK provides:

- Windows UI Library (WinUI) 3. This is the premiere native user interface (UI) framework for Windows desktop apps.
- Visual Studio project templates for developing WinUI 3 apps with full access to the features in the Windows App SDK.
- Unified APIs for managing app instancing and activation, windowing, resource management, and much more.

For more information about developing WinUI 3 apps with the Windows App SDK, see these articles:

- [Introduction to the Windows App SDK](../windows-app-sdk/index.md)
- [Create your first WinUI 3 app using the Windows App SDK](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- [Manage resources with MRT Core](../windows-app-sdk/mrtcore/mrtcore-overview.md)
- [Render text with DWriteCore](../windows-app-sdk/dwritecore.md)
- [Control app activation](../windows-app-sdk/applifecycle/applifecycle-rich-activation.md)
- [Manage app windows](../windows-app-sdk/windowing/windowing-overview.md)
- [Push notifications](../windows-app-sdk/notifications/push/index.md)

## Windows UI Library

The Windows UI Library (WinUI) is the premiere native user interface (UI) framework for Windows apps. WinUI provides consistent, intuitive, and accessible experiences using the latest user interface (UI) patterns. The latest version, WinUI 3, is available with the Windows App SDK and can be used by desktop apps. The previous version, WinUI 2, is available as a standalone NuGet package and can be used by UWP apps.

For more information about WinUI, see these articles:

- [Windows UI Library (WinUI) overview](../winui/index.md)
- [Comparison of WinUI 3 and WinUI 2](../winui/winui3-winui2-comparison.md)
- [Create your first WinUI 3 app using the Windows App SDK](../winui/winui3/create-your-first-winui3-app.md)

## Modernize existing desktop apps

If you have an existing WPF, Windows Forms, or native Win32 desktop app, the Windows OS and the Windows App SDK offer many features you can use to deliver a modern experience in your app. Most of these features are available as modular components that you can adopt in your app at your own pace without having to rewrite your app for a different platform.

- Learn about the [top 11 things you can do to make your app great on Windows 11](../get-started/make-apps-great-for-windows.md).
- [Use the Windows App SDK in an existing desktop project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- [Call WinRT APIs provided by the Windows OS](../desktop/modernize/desktop-to-uwp-enhance.md) to enhance your desktop app with the latest Windows features.
- Use [package extensions](../desktop/modernize/desktop-to-uwp-extensions.md) to integrate your desktop app with modern Windows experiences. For example, point Start tiles to your app, make your app a share target, or send toast notifications from your app.
- Use [XAML Islands](../desktop/modernize/xaml-islands.md) to host WinRT XAML controls in your desktop app. Many of the latest Windows UI features are only available to WinRT XAML controls.
- Use [MSIX](/windows/msix/) to package and deploy your desktop apps. MSIX is a modern Windows app package format that provides a universal packaging experience for all Windows apps. MSIX brings together the best aspects of MSI, .appx, App-V and ClickOnce installation technologies and is built to be safe, secure, and reliable.

For more information, see [Modernize desktop apps](../desktop/modernize/index.md).

## Windows features and technologies

In addition to features provided by the Windows App SDK, Windows desktop apps have access to a broad range of features provided by the Windows 10 and Windows 11 operating systems. These features are available to apps through WinRT and Win32 (C++ and COM) APIs in the Windows SDK. WPF and Windows Forms apps also have access to additional APIs and features available via the .NET SDK. The Windows SDK and .NET SDK are installed by default in most configurations of Visual Studio 2019 and later for Windows app development.

For more information, see [Windows features and technologies](features-and-technologies.md).

## Related topics

* [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
* [Get started developing apps](../get-started/index.md)
* [Package and deploy](../package-and-deploy/index.md)