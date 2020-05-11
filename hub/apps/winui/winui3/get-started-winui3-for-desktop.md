---
description: This guide shows you how to get starting creating .NET and C++/Win32 desktop apps with a WinUI 3 UI.
title: Get started with WinUI 3 for desktop apps
ms.date: 05/19/2020
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, xaml islands
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: high
ms.custom: 19H1
---

# Get started with WinUI 3 for desktop apps

WinUI 3.0 Preview 1 introduces new project templates that enable you to create managed desktop C#/.NET and native C++/Win32 desktop apps with an entirely WinUI-based user interface. When you create apps using these project templates, the entire user interface of your application is implemented using windows, controls, and other UI types provided by WinUI 3.0. This is in contrast to [XAML Islands](../../desktop/modernize/xaml-islands.md), which enable you to host WinRT XAML-based UI controls in the context of .NET (Windows Forms or WPF) or C++/Win32-based user interfaces.

WinUI 3.0 Preview 1 adds the following **WinUI in Desktop** project templates in Visual Studio 2019:

* C# apps and libraries that target .NET 5:
  * Blank App (WinUI in Desktop)
  * Blank App, Packaged (WinUI in Desktop)
  * Class Library (WinUI in Desktop)

* Native C++ apps:
  * Blank App (WinUI in Desktop)
  * Blank App, Packaged (WinUI in Desktop)

> [!NOTE]
> The **Blank App** and **Blank App, Packaged** project templates are similar except the latter include a [Windows Application Packaging Project](https://docs.microsoft.com/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to package the app into an MSIX package.

## Prerequisites

WinUI 3.0 Preview 1 requires:

* Windows 10, version 1803, or a later Windows 10 release.
* Visual Studio 2019, version 16.7 Preview 1.
* .NET 5 Preview 4.

For instructions to install WinUI 3.0 Preview 1, including the project templates for managed C#/.NET and native C++/Win32 desktop apps, see [WinUI 3.0](index.md).

## Create a new WinUI 3 app for C# and .NET

1. In Visual Studio 2019, select **File** -> **New** -> **Project**.

2. In the three drop-down boxes, select **C#**, **Windows**, and **WinUI**, respectively.

3. Select the **Blank App (WinUI in Desktop)** project type and click **Next**.

4. Enter a project name, choose any other options as desired, and click **Create**.

5. In the following dialog box, choose the **Target version** and **Minimum version** values for your project. If you're just interested in creating a test project to try out this application type, select the version of Windows 10 on your development computer for both fields.

## Create a new WinUI 3 desktop app for C++/Win32

1. In Visual Studio 2019, select **File** -> **New** -> **Project**.

2. In the three drop-down boxes, select **C++**, **Windows**, and **WinUI**, respectively.

3. Select the **Blank App (WinUI in Desktop)** project type and click **Next**.

4. Enter a project name, choose any other options as desired, and click **Create**.

5. In the following dialog box, choose the **Target version** and **Minimum version** values for your project. If you're just interested in creating a test project to try out this application type, select the version of Windows 10 on your development computer for both fields.

## Known issues and limitations

The following known issues and limitations exist for WinUI 3 desktop projects in Preview 1:

* The XAML designer is not functional.
* **Live Visual Tree** and other debugging tools may not function correctly.
* When Visual Studio automatically generates a code-behind event handler from XAML markup (for example, events such as `Button.Click`), the namespace of the generated event args incorrectly start with `Windows.UI.Xaml` instead of `Microsoft.UI.Xaml`. You can fix this by manually changing the namespaces in the generated code.

## Related topics

* [WinUI 3.0](index.md)