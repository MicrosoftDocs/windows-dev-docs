---
title: Win2D
description: Win2D is an easy-to-use Windows Runtime API for immediate mode 2D graphics rendering with GPU acceleration.
ms.date: 05/25/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Win2D

> [!NOTE]
> Win2D on WinUI 3 is a work in progress. Some features are not supported, and some of the documentation still points to older WinUI2 concepts and classes. For information on using Win2D with UWP apps, see the [Win2D UWP Documentation](https://microsoft.github.io/Win2D/WinUI2/html/Introduction.htm) on GitHub.

Win2D is an easy-to-use Windows Runtime API for immediate mode 2D graphics rendering with GPU acceleration. It is available to C#, C++ and VB developers writing apps for UWP and WinAppSDK. It utilizes the power of Direct2D, and integrates seamlessly with XAML (on both UWP and WinUI 3).

It's ideal for creating simple games, displays such as charts, and other simple 2D graphics.

## Get Started

Win2D is available as a NuGet package, or as source code [on GitHub](https://github.com/microsoft/Win2D).

### Install Win2D

Add the Win2D NuGet package to your UWP or WinAppSDK app:

* From within Visual Studio, go to **Tools**, **NuGet Package Manager**, **Manage NuGet Packages for Solution..**
* Type **Win2D** into the **Search Online** box, and hit **Enter**.
  * On UWP, select the [**Win2D.uwp**](https://www.nuget.org/packages/Win2D.uwp/) package.
  * On WinAppSDK, select the [**Microsoft.Graphics.Win2D**](https://www.nuget.org/packages/Microsoft.Graphics.Win2D/) package.
* Click **Install**, then **OK**.
* Accept the license agreement.
* Click **Close**.

Next, visit [Hello Win2D World](hellowin2dworld.md) or the [Quick start](https://microsoft.github.io/Win2D/WinUI3/html/QuickStart.htm) to learn about creating a simple app.

## Reference

The [Win2D APIs](https://microsoft.github.io/Win2D/WinUI3/html/APIReference.htm).

## Articles

A list of useful [Win2D topics](https://microsoft.github.io/Win2D/WinUI3/html/Articles.htm).

## See Also

Win2D [on GitHub](https://github.com/microsoft/Win2D)