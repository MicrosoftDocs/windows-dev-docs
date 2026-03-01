---
title: WinUI 2.2 Release Notes
description: Release notes for WinUI 2.2 including new features and bugfixes.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui
ms.date: 04/22/2025
ms.topic: release-notes
---

# WinUI 2.2

WinUI 2.2 is the August 2019 release of WinUI.

You can add WinUI packages to your app using the NuGet package manager: see [Get Started with WinUI 2 for UWP](../getting-started.md) for more information.

WinUI is hosted on [GitHub](https://github.com/microsoft/microsoft-ui-xaml) where we encourage you to file bug reports.

## Microsoft.UI.Xaml 2.2 Version History

### WinUI 2.2 Official Release

AUGUST 2019

[GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml)

### New Features

#### TabView

![Short video showing the behavior of the Tab View control.](../images/tabview-gif.gif)

The TabView control is a collection of tabs that each represents a new page or document in your app. TabView is useful when your app has several pages of content and the user expects to be able to add, close, and rearrange the tabs. The new [Windows Terminal](https://github.com/Microsoft/Terminal) uses TabView to show multiple command line interfaces.

**Documentation:** [API reference](/windows/winui/api/microsoft.ui.xaml.controls.tabview)

#### NavigationView Updates

##### a) NavigationView Back Button update

![Short video showing the updated behavior of the Navigation View control Back Button.](../images/navigationview-back-button.gif)

In NavigationView's minimal mode, the back button no longer disappears. When opening and closing the pane, users no longer need to move their cursor to click the hamburger button. This feature will work by default. You don't need to make any code changes to make this work.

##### b) NavigationView - No Auto Padding

![Screenshot showing the behavior of the Navigation View control with No Auto Padding.](../images/navigationview-no-auto-padding.png)

App developers can now reclaim all pixels within their app window when they use the NavigationView control and extend into the title bar area.

**Documentation:** [Usage guidance](/windows/apps/design/controls/navigationview#top-whitespace)

#### Visual Style Updates

##### a) Corner Radius Update

![Screenshot showing the updated style of the Corner Radius.](../images/corner-radius.png)

CornerRadius attribute was added. Default controls were updated to use slightly rounded corners. Developers can easily customize the corner radius to give your app a unique look if desired.

**GitHub Spec Link:** [https://github.com/microsoft/microsoft-ui-xaml/issues/524](https://github.com/microsoft/microsoft-ui-xaml/issues/524)

##### b) Border Thickness Update

![Screenshot showing the updated style of the Borer Thickness.](../images/border-thickness.png)

BorderThickness property was made easier to customize. Default controls were updated to reduce the outlines to be thinner for a cleaner and familiar look.

**GitHub Spec Link:** [https://github.com/microsoft/microsoft-ui-xaml/issues/835](https://github.com/microsoft/microsoft-ui-xaml/issues/835)

##### c) Button Visual Update

![Screenshot showing the updated style of the Button control.](../images/button-hover-visual-update.png)

Default Button's visual was updated to remove outline that appeared during hover to give it a cleaner look.

**GitHub Spec Link:** [https://github.com/microsoft/microsoft-ui-xaml/issues/953](https://github.com/microsoft/microsoft-ui-xaml/issues/953)

##### d) SplitButton Visual Update

![Screenshot showing the updated style of the Split Button control.](../images/splitbutton-visual-update.png)

Default SplitButton's visual was updated to make it more distinct from DropDownButton.

**GitHub Spec Link:** [https://github.com/microsoft/microsoft-ui-xaml/issues/986](https://github.com/microsoft/microsoft-ui-xaml/issues/986)

##### e) ToggleSwitch Visual Update

![Screenshot showing the updated style of the Toggle Switch control.](../images/toggleswitch-update.png)

Default ToggleSwitch's width was reduced from 44px to 40px so it is balanced visually while retaining usability.

**GitHub Spec Link:** [https://github.com/microsoft/microsoft-ui-xaml/issues/836](https://github.com/microsoft/microsoft-ui-xaml/issues/836)

##### f) CheckBox and RadioButton Visual Update

![Screenshot showing the updated style of the Check Box and Radio Button controls](../images/checkbox-radiobutton.png)

CheckBox and RadioButton visuals were updated to be consistent with the rest of the visual style change.

**GitHub Spec Link:** [https://github.com/microsoft/microsoft-ui-xaml/issues/839](https://github.com/microsoft/microsoft-ui-xaml/issues/839)

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

## Microsoft.UI.Xaml 2.2-prerelease Version History

### Microsoft.UI.Xaml 2.2.190702001-prerelease

July 2019

[GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.2.190702001-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.2.190702001-prerelease)

### Experimental Feature

* [TabView](/windows/winui/api/microsoft.ui.xaml.controls.tabview)

### Microsoft.UI.Xaml 2.2.20190416001-prerelease

April 2019

[GitHub release page](https://github.com/Microsoft/microsoft-ui-xaml/releases/tag/v2.2.190416008-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.2.190416008-prerelease)

#### Experimental features

* [FlowLayout](/windows/winui/api/microsoft.ui.xaml.controls.flowlayout)

* [LayoutPanel](/windows/winui/api/microsoft.ui.xaml.controls.layoutpanel)

* [RadioButtons](/windows/winui/api/microsoft.ui.xaml.controls.radiobuttons)

* [ScrollView](/windows/winui/api/microsoft.ui.xaml.controls.scrollview)
