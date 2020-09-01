---
title: WinUI 2.3 Release Notes
description: Release notes for WinUI 2.3 including new features and bug fixes.
ms.date: 07/15/2020
ms.topic: article
---

# Windows UI Library 2.3

WinUI 2.3 is the latest official release of the Windows UI Library (WinUI).

WinUI is an open source project hosted on GitHub at [Windows UI Library repo](https://aka.ms/winui). Please register all bug reports, feature requests, and community code contributions in this repo.

WinUI Releases: [GitHub release page](https://github.com/microsoft/microsoft-ui-xaml/releases)

WinUI packages can be added to Visual Studio projects through the NuGet package manager. For more information, see [Getting Started with the Windows UI Library](../getting-started.md).

NuGet package download: [Microsoft.UI.Xaml](https://www.nuget.org/packages/Microsoft.UI.Xaml)

## New Features

### Progress Bar Visual Refresh

The **ProgressBar** has two different visual represetations.

#### Indeterminate Progress Bar

Shows that a task is ongoing, but doesn't block user interaction.

![Indeterminate Progress Bar](../images/IndeterminateProgressBar.gif)

#### Determinate Progress Bar

Shows how much progress has been made on a known amount of work. 

![Determinate Progress Bar](../images/DeterminateProgressBar.gif)

[Doc link](/windows/uwp/design/controls-and-patterns/progress-controls)

[Sample link](/windows/uwp/design/controls-and-patterns/progress-controls#examples)

### NumberBox

A **NumberBox** represents a control that can be used to display and edit numbers. This supports validation, increment stepping, and computing inline calculations of basic equations, such as multiplication, division, addition, and subtraction.

![NumberBox](../images/NumberBoxGif.gif)

[Doc and sample link](/windows/uwp/design/controls-and-patterns/number-box)

### RadioButtons

**RadioButtons** is a new container control that enables you to create related groups of RadioButton elements easily, while also correctly supporting keyboarding and narrator/screen reader functionality

![RadioButtons](../images/RadioButtons.png)

[Doc and sample link](https://github.com/microsoft/microsoft-ui-xaml-specs/blob/c8d3d3668af546091656dfc37436b13cd062f52d/active/radiobuttons/RadioButtons_Spec.md)

## Examples

The Xaml Controls Gallery sample app includes interactive demos and sample code for using WinUI controls.

* Install the XAML Controls Gallery app from the [Microsoft Store](
https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt)

* The Xaml Controls Gallery is also [open source on GitHub](
https://github.com/Microsoft/Xaml-Controls-Gallery)

## Documentation

How-to articles for Windows UI Library controls are included with the [Universal Windows Platform controls documentation](/windows/uwp/design/controls-and-patterns/).

API reference docs are located here: [Windows UI Library APIs](/uwp/api/overview/winui/).