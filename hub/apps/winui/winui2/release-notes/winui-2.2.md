---
title: WinUI 2.2 Release Notes
description: Release notes for WinUI 2.2 including new features and bugfixes.
ms.date: 07/15/2020
ms.topic: article
---

# Windows UI Library 2.2

WinUI 2.2 is the latest official release of the Windows UI Library.

You can add WinUI packages to your app using the NuGet package manager: see [Getting Started with the Windows UI Library](../getting-started.md) for more information.

WinUI is an open source project hosted on GitHub. We welcome bug reports, feature requests and community code contributions in the [Windows UI Library repo](https://aka.ms/winui).

## Microsoft.UI.Xaml 2.2 Version History

### Windows UI Library 2.2 Official Release

AUGUST 2019

[GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml)

### New Features

#### TabView

![Example](../images/tabview-gif.gif)

#### Description

The TabView control is a collection of tabs that each represents a new page or document in your app. TabView is useful when your app has several pages of content and the user expects to be able to add, close, and rearrange the tabs. The new [Windows Terminal](https://github.com/Microsoft/Terminal) uses TabView to show multiple command line interfaces.

#### Documentation

https://docs.microsoft.com/uwp/api/microsoft.ui.xaml.controls.tabview?view=winui-2.2

#### NavigationView Updates

##### a) NavigationView's Back Button update

![Example](../images/navigationview-back-button.gif)

##### Description

In NavigationView's minimal mode, the back button no longer disappears. When opening and closing the pane, users no longer need to move their cursor to click the hamburger button. This feature will work by default. You don't need to make any code changes to make this work.

##### b) NavigationView - No Auto Padding

![Example](../images/navigationview-no-auto-padding.png)

##### Description

App developers can now reclaim all pixels within their app window when they use the NavigationView control and extend into the title bar area.

##### Documentation

https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/navigationview#top-whitespace

#### Visual Style Updates

##### a) Corner Radius Update

![Example](../images/corner-radius.png)

##### Description

CornerRadius attribute was added. Default controls were updated to use slightly rounded corners. Developers can easily customize the corner radius to give your app a unique look if desired.

##### GitHub Spec Link

https://github.com/microsoft/microsoft-ui-xaml/issues/524

##### b) Border Thickness Update

![Example](../images/border-thickness.png)

##### Description

BorderThickness property was made easier to customize. Default controls were updated to reduce the outlines to be thinner for a cleaner and familiar look.

##### GitHub Spec Link

https://github.com/microsoft/microsoft-ui-xaml/issues/835

##### c) Button Visual Update

![Example](../images/button-hover-visual-update.png)

##### Description: 
Default Button's visual was updated to remove outline that appeared during hover to give it a cleaner look.

##### GitHub Spec Link:  
https://github.com/microsoft/microsoft-ui-xaml/issues/953

##### d) SplitButton Visual Update

![Example](../images/splitbutton-visual-update.png)

##### Description: 
Default SplitButton's visual was updated to make it more distinct from DropDownButton.

##### GitHub Spec Link: 
https://github.com/microsoft/microsoft-ui-xaml/issues/986

##### e) ToggleSwitch Visual Update

![Example](../images/toggleswitch-update.png)

##### Description: 
Default ToggleSwitch's width was reduced from 44px to 40px so it is balanced visually while retaining usability.

##### GitHub Spec Link: 
https://github.com/microsoft/microsoft-ui-xaml/issues/836

##### f) CheckBox and RadioButton Visual Update

![Example](../images/checkbox-radiobutton.png)

##### Description: 
CheckBox and RadioButton visuals were updated to be consistent with the rest of the visual style change.

##### GitHub Spec Link: 
https://github.com/microsoft/microsoft-ui-xaml/issues/839

## Examples

The Xaml Controls Gallery sample app includes interactive demos and sample code for using WinUI controls.

* Install the XAML Controls Gallery app from the [Microsoft Store](
https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt)

* The Xaml Controls Gallery is also [open source on GitHub](
https://github.com/Microsoft/Xaml-Controls-Gallery)

## Documentation

How-to articles for Windows UI Library controls are included with the [Universal Windows Platform controls documentation](/windows/uwp/design/controls-and-patterns/).

API reference docs are located here: [Windows UI Library APIs](/uwp/api/overview/winui/).

## Microsoft.UI.Xaml 2.2 Version History

### Microsoft.UI.Xaml 2.2.190702001-prerelease

July 2019

[GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases/tag/v2.2.190702001-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.2.190702001-prerelease)

### Experimental Feature

* [TabView](/uwp/api/microsoft.ui.xaml.controls.tabview?view=winui-2.2)

### Microsoft.UI.Xaml 2.2.20190416001-prerelease

April 2019

[GitHub release page](https://github.com/Microsoft/microsoft-ui-xaml/releases/tag/v2.2.190416008-prerelease)

[NuGet package download](https://www.nuget.org/packages/Microsoft.UI.Xaml/2.2.190416008-prerelease)

#### Experimental features

* [FlowLayout](/uwp/api/microsoft.ui.xaml.controls.flowlayout)

* [LayoutPanel](/uwp/api/microsoft.ui.xaml.controls.layoutpanel)

* [RadioButtons](/uwp/api/microsoft.ui.xaml.controls.radiobuttons)

* [ScrollViewer](/uwp/api/microsoft.ui.xaml.controls.scrollviewer)