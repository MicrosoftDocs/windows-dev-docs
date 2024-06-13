---
title: WinUI 2.8 Release Notes
description: Release notes for WinUI 2.8, including new features and bug fixes.
ms.date: 07/18/2021
ms.topic: article
---

# WinUI 2.8

WinUI 2.8 is the latest stable release of WinUI for UWP applications (and desktop applications using [XAML Islands](../../../desktop/modernize/xaml-islands.md)).

> [!NOTE]
> For more information on building Windows desktop and UWP apps with the latest version of **WinUI 3**, see [WinUI 3](../../index.md).

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports, feature requests and community code contributions.

All stable releases (and prereleases) are available for download from our [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/tags) or from our [NuGet page](https://www.nuget.org/packages/Microsoft.UI.Xaml).

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting started with WinUI 2](../getting-started.md).

## What's New

- [WebView2](/windows/winui/api/microsoft.ui.xaml.controls.webview2) is now available in WinUI 2. WebView2 enables you to embed web content in native applications with Microsoft Edge (Chromium). Check out [Get started with WebView2 in WinUI 2 (UWP) apps](/microsoft-edge/webview2/get-started/winui2) for more info for how to use it in your app.
- The minimum supported version of WinUI 2 is now Windows 10, version 1809 - build 17763.
- Several bug fixes and improvements for a range of controls including [TabView](/windows/winui/api/microsoft.ui.xaml.controls.tabview), [NavigationView](/windows/winui/api/microsoft.ui.xaml.controls.navigationview), [InfoBadge](/windows/winui/api/microsoft.ui.xaml.controls.infobadge), and [ProgressRing](/windows/winui/api/microsoft.ui.xaml.controls.progressring).
- Improvements to accessibility and high contrast mode.
- [AnimatedVisualPlayer.AnimationOptimization](/windows/winui/api/microsoft.ui.xaml.controls.animatedvisualplayer.animationoptimization) property to configure how animations are cached in an AnimatedVisualPlayer. See [Configuring Animation Playback](/windows/communitytoolkit/animations/lottie-scenarios/playback) for info for how to use it in your app.

## Examples

> [!TIP]
> For more info, design guidance, and code examples, see [Design and code Windows apps](../../../design/index.md).
>
> The **WinUI 3 Gallery** and **WinUI 2 Gallery** apps include interactive examples of most WinUI 3 and WinUI 2 controls, features, and functionality.
>
> If installed already, open them by clicking the following links: [**WinUI 3 Gallery**](winui3gallery:/item/Webview2) or [**WinUI 2 Gallery**](winui2gallery:/item/Webview2).
>
> If they are not installed, you can download the [**WinUI 3 Gallery**](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) and the [**WinUI 2 Gallery**](https://www.microsoft.com/store/productId/9MSVH128X2ZT) from the Microsoft Store.
>
> You can also get the source code for both from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (use the *main* branch for WinUI 3 and the *winui2* branch for WinUI 2).

## Other updates

- See our [Notable Changes](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.8.0) list for many of the GitHub issues addressed in this release.
