---
title: Overview of Win2D
description: Win2D is an easy-to-use Windows Runtime API for immediate-mode 2D graphics rendering with GPU acceleration.
ms.date: 10/28/2025
ms.topic: concept-article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Overview of Win2D

Win2D is an easy-to-use Windows Runtime (WinRT) API for immediate-mode 2D graphics rendering with GPU acceleration. It's ideal for creating simple games, displays such as charts, and other simple 2D graphics.

You can use Win2D in your WinUI (Windows App SDK) apps or Universal Windows Platform (UWP) apps, using either C#, C++, or VB. Win2D utilizes the power of Direct2D, and it integrates seamlessly with XAML on both WinUI (Windows App SDK) and UWP.

> [!IMPORTANT]
> Win2D for WinUI (Windows App SDK) is a work-in-progress, and some features aren't supported. This documentation refers to both the WinUI and UWP versions, which mostly share the same API surface and functionality. Whenever there's any relevant differences between the two, we'll call it out in the documentation. But otherwise, the info being presented applies to both platforms in the same way.

## Get started

Win2D is available as a NuGet package, or as source code (for the source code, see the [Win2D](https://github.com/microsoft/Win2D) repo on GitHub).

### Reference the Win2D NuGet package

In a WinUI or UWP project in Visual Studio, click **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution...** > **Browse**. Make sure that **Include prerelease** is unchecked, and type or paste into the search box:

* For a WinUI project, [*Microsoft.Graphics.Win2D*](https://www.nuget.org/packages/Microsoft.Graphics.Win2D/).
* For a UWP project, [*Win2D.uwp*](https://www.nuget.org/packages/Win2D.uwp/).

Select the correct item in search results, check your project, and click **Install** to install the package into that project. Accept the license agreement.

> [!IMPORTANT]
> If you see any error messages, then try updating the version of the Windows App SDK NuGet package that you're referencing (if appropriate). Or try going into project properties, and setting **Target OS version** to the latest version.

## Next steps

Next, to learn about creating a simple app, try out the [Win2D "Hello, World!" quickstart](./hellowin2dworld.md), or the [Build a simple Win2D app](./quick-start.md) tutorial. You can also consult the [features list](./features.md) to discover all the things Win2D can do. To learn more about advanced topics, you can refer to the collection of articles included in the documentation here as well.

## Reference

The [Win2D APIs](https://microsoft.github.io/Win2D/WinUI3/html/APIReference.htm).
