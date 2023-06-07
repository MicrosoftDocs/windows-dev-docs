---
title: Win2D
description: Win2D is an easy-to-use Windows Runtime API for immediate mode 2D graphics rendering with GPU acceleration.
ms.date: 05/25/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Win2D

Win2D is an easy-to-use Windows Runtime API for immediate mode 2D graphics rendering with GPU acceleration. It is available to C#, C++ and VB developers writing apps for UWP and WinAppSDK. It utilizes the power of Direct2D, and integrates seamlessly with XAML (on both UWP and WinAppSDK, including WinUI 3).

It's ideal for creating simple games, displays such as charts, and other simple 2D graphics.

> [!NOTE]
> Win2D on WinAppSDK (including WinUI 3) is a work in progress, and some features are not supported. The documentation refers to both the UWP and WinAppSDK versions, which mostly share the same API surface and functionality. Whenever there's any relevant differences between the two, it will be called out in the docs. When not specified, you can assume that the topics being discussed apply to both frameworks in the same way.

## Get Started

Win2D is available as a NuGet package, or as source code [on GitHub](https://github.com/microsoft/Win2D).

### Install Win2D

Add the Win2D NuGet package to your UWP or WinAppSDK app:

* From within Visual Studio, go to **Tools**, **NuGet Package Manager**, **Manage NuGet Packages for Solution**.
* Type **Win2D** into the **Search Online** box, and hit **Enter**.
  * On UWP, select the [**Win2D.uwp**](https://www.nuget.org/packages/Win2D.uwp/) package.
  * On WinAppSDK, select the [**Microsoft.Graphics.Win2D**](https://www.nuget.org/packages/Microsoft.Graphics.Win2D/) package.
* Click **Install**, then **OK**.
* Accept the license agreement.
* Click **Close**.

Next, visit [Hello Win2D World](hellowin2dworld.md) or the [quick start](./quick-start.md) to learn about creating a simple app. You can also consult the [features list](./features.md) to discover all the things Win2D can do. To learn more about advanced topics, you can refer to the collection of articles included in this docs section as well.

## Reference

The [Win2D APIs](https://microsoft.github.io/Win2D/WinUI3/html/APIReference.htm).