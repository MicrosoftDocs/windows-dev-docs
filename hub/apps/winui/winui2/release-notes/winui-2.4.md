---
title: WinUI 2.4 Release Notes
description: Release notes for WinUI 2.4 including new features and bug fixes.
ms.date: 07/15/2020
ms.topic: reference
---

# Windows UI Library 2.4

WinUI 2.4 is the latest official release of the Windows UI Library (WinUI).

WinUI is an open source project hosted on GitHub at [Windows UI Library repo](https://aka.ms/winui). Please register all bug reports, feature requests, and community code contributions in this repo.

WinUI Releases: [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases)

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting Started with the Windows UI Library](../getting-started.md).

NuGet package download: [Microsoft.UI.Xaml](https://www.nuget.org/packages/Microsoft.UI.Xaml)

## New Features

### RadialGradientBrush

A RadialGradientBrush is drawn within an ellipse defined by Center, RadiusX, and RadiusY properties. Colors for the gradient start at the center of the ellipse and end at the radius.

![Radial gradient brush](../images/radialgradientbrush.gif)<br>
*Radial gradient brush*

[Usage guidelines](/windows/uwp/design/style/brushes#radial-gradient-brushes)

[API reference](/uwp/api/microsoft.ui.xaml.media.radialgradientbrush)

### ProgressRing

The ProgressRing control is used for modal interactions where the user is blocked until the ProgressRing disappears. Use this control if an operation requires that most interaction with the app be suspended until the operation is complete.

![ProgressRing control](../images/progressring.gif)<br>
*ProgressRing control*

[Usage guidelines](/windows/uwp/design/controls-and-patterns/progress-controls)

[API reference](/uwp/api/microsoft.ui.xaml.controls.progressring)

### TabView updates

The TabView control updates provide you with more control over how to render tabs.

You can set the width of unselected tabs and show just an icon to save screen space:

![TabView control tab sizes](..\images\tabview-sizing.gif)<br>
*TabView control tab sizes*

You can also hide the close button on unselected tabs until the user hovers over the tab (in previous versions it was always shown):

![TabView control hover to close](..\images\tabview-closebuttononhover.gif)<br>
*TabView control hover to close*

[Usage guidelines](/windows/uwp/design/controls-and-patterns/tab-view)

[API reference](/uwp/api/microsoft.ui.xaml.controls.tabview)

### Dark theme updates to TextBox family of controls

When dark theme is enabled, the background color of TextBox family controls remains dark by default on text insertion (in previous versions, the background color changes to white during text insertion).

| Before | After |
| - | - |
| ![TextBox dark theme updates (before)](..\images\textbox-darkthemeupdates-before1.gif)<br>*TextBox dark theme updates (before)* | ![TextBox dark theme updates (after)](..\images\textbox-darkthemeupdates-after1.gif)<br>*TextBox dark theme updates (after)* |
| ![TextBox dark theme updates (before)](..\images\textbox-darkthemeupdates-before2.gif)<br>*TextBox dark theme updates (before)* | ![TextBox dark theme updates (after)](..\images\textbox-darkthemeupdates-after2.gif)<br>*TextBox dark theme updates (after)* |

The following are some of the controls included in the TextBox family of controls:

- [TextBox](/uwp/api/windows.ui.xaml.controls.textbox)
- [RichEditBox](/uwp/api/windows.ui.xaml.controls.richtextblock)
- [PasswordBox](/uwp/api/windows.ui.xaml.controls.passwordbox)
- [Editable ComboBox](/uwp/api/windows.ui.xaml.controls.combobox)
- [AutoSuggestBox](/uwp/api/windows.ui.xaml.controls.autosuggestbox)

### Hierarchical navigation

The [NavigationView](/uwp/api/microsoft.ui.xaml.controls.navigationview?view=winui-2.4) control now supports hierarchical navigation and includes Left, Top, and LeftCompact display modes. A hierarchical NavigationView is useful for displaying categories of pages, identifying pages with related child-pages, or using within apps that have hub-style pages linking to many other pages.

![Hierarchical NavigationView control](..\images\HierarchicalNavView.gif)<br>*Hierarchical NavigationView control*

[Usage guidelines](/windows/uwp/design/controls-and-patterns/navigationview#hierarchical-navigation)

[API reference](/uwp/api/microsoft.ui.xaml.controls.navigationview)

## Samples

Examples of each of the WinUI 2.4 features described are available in the **XAML Controls Gallery**.

If you don't have the XAML Controls Gallery app installed, get it from the [Microsoft Store](https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt).

You can also view the XAML Controls Gallery source code on [GitHub](https://github.com/Microsoft/Xaml-Controls-Gallery).
