---
title: WinUI 2.1 Release Notes
description: Release notes for WinUI 2.1 including new features and bugfixes.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
ms.date: 04/22/2025
ms.topic: release-notes
---

# WinUI 2.1

The first open-source version of WinUI – WinUI 2.1 (released April 2019).

WinUI gives you many of the latest Windows UX platform features, including up-to-date Fluent controls and styles, available in a way you can use right away, backward-compatible to Windows 10 Anniversary Update (14393). The [WinUI 2 Gallery](https://apps.microsoft.com/detail/9MSVH128X2ZT) gives you samples to explore all the cool new features added to the library.

Download the [WinUI 2.1 NuGet package](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.1.190405004)

You can choose to use the WinUI packages in your app using the NuGet package manager: see [Get Started with WinUI 2 for UWP](../getting-started.md) for more information.

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports.

## What's new in this release

### ItemsRepeater

Use an ItemsRepeater to create custom collection experiences using a flexible layout system, custom views, and virtualization.
Unlike ListView, ItemsRepeater does not provide a comprehensive end-user experience – it has no default UI and provides no policy around focus, selection, or user interaction. Instead, it's a building block that you can use to create your own unique collection-based experiences and custom controls. It supports building of richer and more performant experiences.

![Short video showing the behavior of the Items Repeater control.](../images/ItemsRepeater%20-%20MSN%20News.gif)

[Documentation](/windows/apps/design/controls/items-repeater)

### AnimatedVisualPlayer

The AnimatedVisualPlayer hosts and controls playback of animated visuals, enabling you to add high performance custom motion graphics to your app. For instance, the AnimatedVisualPlayer is used to display and control Lottie animations.

![Short video showing the behavior of the Animated Visual Player control.](../images/AnimatedVisualPlayerUpdated.gif)

[Documentation](/windows/communitytoolkit/animations/lottie)

### TeachingTip

TeachingTip provides an engaging and Fluent way for applications to guide and inform users with non-invasive and content-rich tips. TeachingTip can bring focus to new or important features, teach users how to perform tasks, and enhance workflow by providing contextually relevant information to your task at hand.

![Short video showing the behavior of the Teaching Tip control.](../images/TeachingTipUpdated.gif)

[Documentation](/windows/apps/design/controls/dialogs-and-flyouts/teaching-tip)

### RadioMenuFlyoutItem

Includes the ability to have 'Radio Button' style options in a MenuBar. This enables groups of options with bullets that are tied together like a radio button group. The logic is handled for the developer.

![Screenshot showing the behavior of the Radio Menu Fly out Item control.](../images/RadioMenuFlyoutItem1.png)

[Documentation](/windows/apps/design/controls/menus#create-a-menu-flyout-or-a-context-menu)

### CompactDensity

Compact mode enables developers to create comfortable experiences for any number of scenarios. Simply by adding a resource dictionary your application can fit on average ~33% more UI.

![Screenshot showing the behavior of the Compact Density control.](../images/CompactDensityUpdated.png)

[Documentation](/windows/apps/design/style/spacing)

### Shadows

![Example](../images/shadow.gif)

Creating a visual hierarchy of elements in your UI makes the UI easy to scan and conveys what is important to focus on. Elevation, the act of bringing select elements of your UI forward, is often used to achieve such a hierarchy in software.

With Windows 10 May 2019 Update, many of our common controls add elevation by using z-depth and shadow by default. The NavigationView and TeachingTip controls in WinUI 2.1 will also have default shadows when running on an OS with Windows 10 May 2019 Update. For the list of controls that have default shadows and how to use additional APIs, see [Z-depth and shadow](/windows/apps/design/layout/depth-shadow).

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

## Documentation

How-to articles for WinUI controls are included with the [Controls for Windows apps](/windows/apps/design/controls/) documentation.

API reference docs are located here: [WinUI APIs](/windows/winui/api/).

## Microsoft.UI.Xaml 2.1 Version History

### Microsoft.UI.Xaml 2.1 Official release

April 2019

[GitHub release page](https://github.com/Microsoft/microsoft-ui-xaml/releases)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.1.190405004)

#### New feature (not included in earlier pre-releases)

* [CompactDensity](/windows/apps/design/style/spacing):
Compact mode enable developers to create comfortable experiences for any number of scenarios. Simply by adding a resource dictionary your application can fit on average ~33% more UI.

* Shadows:
Creating a visual hierarchy of elements in your UI makes the UI easy to scan and conveys what is important to focus on. Elevation, the act of bringing select elements of your UI forward, is often used to achieve such a hierarchy in software. Many of our common controls add elevation by using z-depth and shadow by default.  

### Microsoft.UI.Xaml 2.1.190218001-prerelease

February 2019

[GitHub release page](https://github.com/Microsoft/microsoft-ui-xaml/releases/tag/v2.1.190219001-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.1.190218001-prerelease)

New experimental features:

* [TeachingTip control](https://github.com/Microsoft/microsoft-ui-xaml/issues/21)  
  This new control provides a way for your app to guide and inform users in your application with a non-invasive and content rich notification. TeachingTip can be used for bringing focus to a new or important feature, teaching users how to perform a task, or enhancing the user workflow by providing contextually relevant information to their task at hand.

### Microsoft.UI.Xaml 2.1.190131001-prerelease

February 2019

[GitHub release page](https://github.com/Microsoft/microsoft-ui-xaml/releases/tag/v2.1.190131001-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.1.190131001-prerelease)

New experimental features:

* [AnimatedVisualPlayer](/windows/winui/api/microsoft.ui.xaml.controls.animatedvisualplayer)  
  This new control enables playing complex high performance vector animations, including [Lottie](https://github.com/airbnb/lottie) animations created using [Lottie-Windows](/windows/communitytoolkit/animations/lottie).

### Microsoft.UI.Xaml 2.1.181217001-prerelease

December 2018

[GitHub release page](https://github.com/Microsoft/microsoft-ui-xaml/releases/tag/v2.1.181217001-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.1.181217001-prerelease)

New experimental features:

* [ItemsRepeater](/windows/winui/api/microsoft.ui.xaml.controls.itemsrepeater)

* [RadioButtons](/windows/winui/api/microsoft.ui.xaml.controls.radiobuttons)

* [RadioMenuFlyoutItem](/windows/winui/api/microsoft.ui.xaml.controls.radiomenuflyoutitem)
