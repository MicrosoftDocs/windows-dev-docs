---
title: WinUI 2.6 Release Notes
description: Release notes for WinUI 2.6, including new features and bug fixes.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
ms.date: 06/24/2021
ms.topic: article
---

# WinUI 2.6

WinUI 2.6 is the June 2021 release of WinUI for UWP applications (and desktop applications using [XAML Islands](../../../desktop/modernize/xaml-islands/xaml-islands.md)).

> [!NOTE]
> For more information on building Windows desktop and UWP apps with the latest version of **WinUI 3**, see [WinUI 3](../../index.md).

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports, feature requests and community code contributions.

All stable releases (and prereleases) are available for download from our [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/tags) or from our [NuGet page](https://www.nuget.org/packages/Microsoft.UI.Xaml).

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting started with WinUI 2](../getting-started.md).

New or updated features for WinUI 2.6 include:

## Mica

Mica is a new material that incorporates theme and desktop wallpaper to paint the background of long-lived windows such as apps and settings. You can apply Mica to your application backdrop to delight users and create visual hierarchy, aiding productivity, by increasing clarity about which window is in focus. Mica is specifically designed for app performance as it only samples the desktop wallpaper once to create its visualization.

:::image type="content" source="../../../design/style/images/materials/mica-light-theme.png" alt-text="Mica in light theme":::

[Usage guidelines](../../../design/style/mica.md)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.backdropmaterial)

## Expander

The Expander control is composed of a header of primary content that is always visible, paired with a toggle button used to show or hide an expandable content area containing secondary content related to the header.

As the user expands or collapses the content area, adjacent UI elements are shifted and adjusted to accommodate the content area. The content area of the expander does not overlay those elements.

This animated example shows an Expander in the default state with just basic text in the content area.

:::image type="content" source="../../../design/controls/images/expander-default.gif" alt-text="Expander in the default state with basic text in the content area.":::

You can use complex, interactive UI in the content area of the Expander, including nested Expander controls as shown here.

:::image type="content" source="../../../design/controls/images/expander-nested.png" alt-text="Expander with complex, interactive UI in the content area.":::

[Usage guidelines](../../../design/controls/expander.md)

[API reference](/uwp/api/microsoft.ui.xaml.controls.expander)

## BreadcrumbBar

A BreadcrumbBar is a hierarchical navigation element that provides a direct path of links to pages or folders leading to the user's current location. It is often used near the top of a page in situations where the user's navigation trail (in a file system or menu system) needs to be persistently visible and provide the user with the ability to quickly go back to a previous location.

This animated example shows a BreadcrumbBar in the default state with eight levels of navigation depth.

:::image type="content" source="../../../design/controls/images/breadcrumbbar-default.gif" alt-text="BreadcrumbBar in default state with eight levels of navigation depth.":::

If the app is resized so that there is not enough space to show all levels in the BreadcrumbBar, the control automatically collapses, substituting an ellipsis for the leftmost nodes. Clicking the ellipsis opens a flyout menu that displays the collapsed nodes in hierarchical order, as shown here.

:::image type="content" source="../../../design/controls/images/breadcrumb-bar-flyout.png" alt-text="BreadcrumbBar condensed with flyout menu.":::

[Usage guidelines](../../../design/controls/breadcrumbbar.md)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.breadcrumbbar)

## ImageIcon

ImageIcon adds support for using an [Image](/windows/winui/api/microsoft.ui.xaml.controls.imageicon) control as an icon in your application UI.

The following image file formats are supported:

- Bitmap (BMP)
- Graphics Interchange Format (GIF)
- Joint Photographic Experts Group (JPEG)
- Portable Network Graphics (PNG)
- JPEG XR (WDP)
- Tagged Image File Format (TIFF)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.imageicon)

## AnimatedIcon

An AnimatedIcon control plays animated images in response to user interaction and visual state changes, such as when a user hovers over a button or clicks it.

This animated example shows an AnimatedIcon added to a NavigationViewItem control.

:::image type="content" source="../../../design/controls/images/animated-settings.gif" alt-text="AnimatedIcon added to a NavigationViewItem control.":::

Defining an animation requires that you create, or obtain, a Lottie file for the icon you want to add (custom animations can be created with [Adobe AfterEffects](https://www.adobe.com/products/aftereffects.html) and rendered with the [Lottie-Windows](/windows/communitytoolkit/animations/lottie) library) and run that file through [LottieGen](/windows/communitytoolkit/animations/lottie-scenarios/getting_started_codegen). LottieGen generates code for a C++/WinRT class that you can then instantiate and use with an AnimatedIcon.

[Usage guidelines](../../../design/controls/animated-icon.md)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.animatedicon)

## PipsPager

The PipsPager control helps users navigate within linearly paginated content using a configurable collection of glyphs, each of which represents a single "page" within a potentially limitless range. The glyphs highlight the current page, and indicate the availability of both preceding and succeeding pages. The control relies on current context and does not support explicit page numbering or a non-linear organization.

This example shows a PipsPager in the default state with five visible pips, oriented horizontally, with the first pip selected.

:::image type="content" source="../../../design/controls/images/pipspager-default.png" alt-text="A default PipsPager with five horizontal dots, and the first selected.":::

If the content consists of a large number of pages, you can set the number of visible, interactive pips. If the number of pages exceeds the number of visible pips, the pips automatically scroll in order to center the selected page in the control.

This animated example shows a PipsPager with horizontally scrolling pips for a large item collection.

:::image type="content" source="../../../design/controls/images/pipspager-max-visible-pips.gif" alt-text="PipsPager with horizontally scrolling pips for a large item collection.":::

[Usage guidelines](../../../design/controls/pipspager.md)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.pipspager)

## Visual style updates

Most WinUI controls now support the latest Windows 11 styles.

A new versioning system has also been introduced that lets you revert to the previous control styles. However, we strongly encourage using the new styles, if possible, as they align with the current design direction of Windows.

[XAML styles](../../../design/style/xaml-styles.md)

## SplitButton styles for CommandBar

A new `SplitButtonCommandBarStyle` provides the ability to apply the look and feel of an [AppBarButton](/uwp/api/windows.ui.xaml.controls.appbarbutton) to a [SplitButton](/windows/winui/api/microsoft.ui.xaml.controls.splitbutton) control.

## Samples

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

- See our [Notable Changes](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.6.0) list for many of the GitHub issues addressed in this release.
- Check out the [Figma design toolkit](https://aka.ms/winui/2.6-figma-toolkit) for the WinUI 2.6 control and layout templates.
