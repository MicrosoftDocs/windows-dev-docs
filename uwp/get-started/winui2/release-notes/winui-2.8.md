---
title: WinUI 2.8 Release Notes
description: Release notes for WinUI 2.8, including new features and bug fixes.
ms.date: 04/22/2025
ms.topic: release-notes
---

# WinUI 2.8

WinUI 2.8 is the latest stable release of WinUI for UWP applications (and desktop applications using [XAML Islands](/windows/apps/desktop/modernize/xaml-islands/xaml-islands)).

> [!NOTE]
> For more information on building Windows desktop and UWP apps with the latest version of **WinUI 3**, see [WinUI 3](/windows/apps/winui/winui3/).

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports.

All stable releases (and prereleases) are available for download from our [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/tags) or from our [NuGet page](https://www.nuget.org/packages/Microsoft.UI.Xaml).

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Get started with WinUI 2 for UWP](../getting-started.md).

## What's New

- [WebView2](/windows/winui/api/microsoft.ui.xaml.controls.webview2) is now available in WinUI 2. WebView2 enables you to embed web content in native applications with Microsoft Edge (Chromium). Check out [Get started with WebView2 in WinUI 2 (UWP) apps](/microsoft-edge/webview2/get-started/winui2) for more info for how to use it in your app.
- The minimum supported version of WinUI 2 is now Windows 10, version 1809 - build 17763.
- Several bug fixes and improvements for a range of controls including [TabView](/windows/winui/api/microsoft.ui.xaml.controls.tabview), [NavigationView](/windows/winui/api/microsoft.ui.xaml.controls.navigationview), [InfoBadge](/windows/winui/api/microsoft.ui.xaml.controls.infobadge), and [ProgressRing](/windows/winui/api/microsoft.ui.xaml.controls.progressring).
- Improvements to accessibility and high contrast mode.
- [AnimatedVisualPlayer.AnimationOptimization](/windows/winui/api/microsoft.ui.xaml.controls.animatedvisualplayer.animationoptimization) property to configure how animations are cached in an AnimatedVisualPlayer. See [Configuring Animation Playback](/windows/communitytoolkit/animations/lottie-scenarios/playback) for info for how to use it in your app.

## Examples

> [!TIP]
> For more info, design guidance, and code examples, see [Design for Windows apps](/windows/apps/design/).
>
> The **WinUI 2 Gallery** app includes interactive examples of most WinUI 2 controls, features, and functionality.
>
> If the gallery app is installed already, click [**WinUI 2 Gallery**](winui2gallery:) to open it.
>
> If it's not installed, download the [**WinUI 2 Gallery**](https://apps.microsoft.com/detail/9MSVH128X2ZT) from the Microsoft Store.
>
> You can also get the source code from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (select the *winui2* branch).

## Other updates

- See our [Notable Changes](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.8.0) list for many of the GitHub issues addressed in this release.
