---
title: WinUI 2.7 Release Notes
description: Release notes for WinUI 2.7, including new features and bug fixes.
ms.date: 09/13/2021
ms.topic: article
---

# WinUI 2.7

WinUI 2.7 is the September 2021 release of WinUI for UWP applications (and desktop applications using [XAML Islands](../../../desktop/modernize/xaml-islands.md)).

> [!NOTE]
> For more information on building Windows desktop and UWP apps with the latest version of **WinUI 3**, see [WinUI 3](../../index.md).

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports, feature requests and community code contributions.

All stable releases (and prereleases) are available for download from our [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/tags) or from our [NuGet page](https://www.nuget.org/packages/Microsoft.UI.Xaml).

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting started with WinUI 2](../getting-started.md).

New or updated features for WinUI 2.7 include:

## InfoBadge

Badging is a non-intrusive and intuitive way to indicate notifications, display alerts, highlight new content, or draw focus to an area within an app. An [InfoBadge](/uwp/api/microsoft.ui.xaml.controls.infobadge) is a small piece of UI that can be added into an app and customized to display a number, icon, or a simple dot.

:::image type="content" source="../../../design/controls/images/infobadge/infobadge-types.png" alt-text="Dot, icon, and numeric InfoBadges":::

InfoBadge is built into the [NavigationView](../../../design/controls/navigationview.md) control. It can also be specified as a standalone element in the XAML tree, letting you place InfoBadge into any control or piece of UI that you choose.

:::image type="content" source="../../../design/controls/images/infobadge/infobadge-example-1.png" alt-text="Example of an InfoBadge in NavigationView.":::

[Usage guidelines](../../../design/controls/info-badge.md)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.infobadge)

## Horizontal Orientation in ColorPicker

Use the Orientation property of the ColorPicker control to specify whether the editing controls should align vertically or horizontally, relative to the color spectrum.

:::image type="content" source="../../../design/controls/images/color-picker-horizontal.png" alt-text="Example of a horizontally aligned ColorPicker.":::

[Usage guidelines](../../../design/controls/color-picker.md?#specify-the-layout-direction)

[API reference](/uwp/api/microsoft.ui.xaml.controls.colorpicker.orientation)

## Examples

> [!TIP]
> For more info, design guidance, and code examples, see [Design and code Windows apps](../../../design/index.md).
>
> The **WinUI 3 Gallery** and **WinUI 2 Gallery** apps include interactive examples of most WinUI 3 and WinUI 2 controls, features, and functionality.
>
> If installed already, open them by clicking the following links: [**WinUI 3 Gallery**](winui3gallery:/item/AnimatedIcon) or [**WinUI 2 Gallery**](winui2gallery:/item/AnimatedIcon).
>
> If they are not installed, you can download the [**WinUI 3 Gallery**](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) and the [**WinUI 2 Gallery**](https://www.microsoft.com/store/productId/9MSVH128X2ZT) from the Microsoft Store.
>
> You can also get the source code for both from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (use the *main* branch for WinUI 3 and the *winui2* branch for WinUI 2).

## Other updates

- See our [Notable Changes](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.7.0) list for many of the GitHub issues addressed in this release.
