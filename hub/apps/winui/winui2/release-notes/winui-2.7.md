---
title: WinUI 2.7 Release Notes
description: Release notes for WinUI 2.7, including new features and bug fixes.
ms.date: 09/13/2021
ms.topic: article
---

# Windows UI Library 2.7

WinUI 2.7 is the latest stable release of the Windows UI Library (WinUI) for UWP applications (and desktop applications using [XAML Islands](../../../desktop/modernize/xaml-islands.md)).

> [!NOTE]
> For more information on building Windows desktop and UWP apps with the latest version of **WinUI 3**, see [Windows UI Library 3](../../winui3/index.md).

WinUI is hosted on GitHub in our [Windows UI Library repo](https://github.com/microsoft/microsoft-ui-xaml). As an open source project, you can file your WinUI 2 bug reports, feature requests, and community code contributions there.

All stable releases (and prereleases) are available for download from our [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/tags) or from our [NuGet page](https://www.nuget.org/packages/Microsoft.UI.Xaml).

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting started with the Windows UI 2 Library](../getting-started.md).

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

The **XAML Controls Gallery** sample app includes examples of each of these WinUI features and controls.

If you have the **XAML Controls Gallery** app installed and updated to the latest version, [see the controls in action](xamlcontrolsgallery:).

If you don't have the XAML Controls Gallery app installed, get it from the [Microsoft Store](https://aka.ms/xamlgalleryapp).

You can also view, clone, and build the XAML Controls Gallery source code from [GitHub](https://github.com/Microsoft/Xaml-Controls-Gallery).

## Other updates

- See our [Notable Changes](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.7.0) list for many of the GitHub issues addressed in this release.
- Check out the [Figma design toolkit](https://aka.ms/winui/2.7-figma-toolkit) for the WinUI 2.7 control and layout templates.
