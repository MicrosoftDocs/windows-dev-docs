---
title: WinUI 2.5 Release Notes
description: Release notes for WinUI 2.5, including new features and bug fixes.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
ms.date: 12/01/2020
ms.topic: article
---

# WinUI 2.5

WinUI 2.5 is the December 2020 release of WinUI.

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports, feature requests and community code contributions.

WinUI Releases: [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases)

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting Started with WinUI](../getting-started.md).

NuGet package download: [Microsoft.UI.Xaml](https://www.nuget.org/packages/Microsoft.UI.Xaml)

## New Features

### InfoBar

The [InfoBar](/windows/uwp/design/controls-and-patterns/infobar) control is used to display app-wide status messages that are highly visible to users, yet are also non-intrusive. The control includes a Severity property to indicate the type of message shown, and an option to specify your own call to action or hyperlink button. As the InfoBar is inline with other UI content, you can also specify whether the control is always visible or if it can be dismissed by the user.

This example shows an InfoBar in the default state with a close button and message.

:::image type="content" source="../images/infobar-default-title-message.png" alt-text="An example of an InfoBar in the default state with a close button and message.":::

This animated example shows an InfoBar with various severity states and custom messages.

:::image type="content" source="../images/infobar-severity-animated.gif" alt-text="Animated example of InfoBar severity states and custom messages.":::

[Usage guidelines](/windows/uwp/design/controls-and-patterns/infobar)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.infobar)

### Determinate ProgressRing

The determinate state for [ProgressRing](/windows/uwp/design/controls-and-patterns/progress-controls) shows the percentage completed of a task. This should be used during an operation where the duration is known and where the operation's progress should not block user interaction with the app.

The following animated image demonstrates a determinate ProgressRing control.

:::image type="content" source="../images/progressring-determinate-mode-animated.gif" alt-text="Animated example of a determinate ProgressRing control.":::<br>

[Usage guidelines](/windows/uwp/design/controls-and-patterns/progress-controls#progress-controls-best-practices)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.progressring)


### NavigationView FooterMenuItems

Use the FooterMenuItems property of the NavigationView control to place navigation items at the end of the navigation pane (compared to the MenuItems property, which places items at the beginning of the pane).

The following image shows a NavigationView with *Account*, *Your Cart*, and *Help* navigation items in the footer menu.

:::image type="content" source="../images/navigationview-footermenuitems.png" alt-text="Example of a NavigationView with Account, Your Cart, and Help navigation items in the footer menu.":::

[Usage guidelines](/windows/uwp/design/controls-and-patterns/navigationview?#footer-menu-items)

[API reference](/windows/winui/api/microsoft.ui.xaml.controls.navigationview.footermenuitems)

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

See our [Notable Changes](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.5.0) list for many of the GitHub issues addressed in this release.
