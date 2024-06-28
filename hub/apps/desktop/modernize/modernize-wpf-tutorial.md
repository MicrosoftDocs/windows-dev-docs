---
description: This tutorial demonstrates how to add UWP XAML user interfaces, create MSIX packages, and incorporate other modern components into your WPF app.
title: "Tutorial: Modernize a WPF app"
ms.topic: article
ms.date: 06/27/2019
keywords: windows 10, uwp, windows forms, wpf, xaml islands
ms.localizationpriority: medium
ms.custom: RS5, 19H1
---

# Tutorial: Modernize a WPF app 

There are many ways to [modernize](index.md) existing desktop apps by integrating the latest Windows features into the existing source code instead of rewriting the apps from scratch. In this tutorial we'll explore several ways to modernize an existing WPF line-of-business app by using these features:

* .NET Core 3
* UWP XAML controls with XAML Islands
* Adaptive Cards and Windows notifications
* MSIX deployment

This tutorial requires the following development skills:

* Experience in developing Windows desktop apps with WPF.
* Basic knowledge of C# and XAML.
* Basic knowledge of UWP.

## Overview

This tutorial provides the code for a simple WPF line-of-business app named Contoso Expenses. In the fictional scenario of the tutorial, Contoso Expenses is an internal app used by managers of Contoso Corporation to keep track of the expenses submitted by their reports. The managers are now equipped with touch-enabled devices, and they would like to use the Contoso Expenses app without a mouse or keyboard. Unfortunately, the current version of the app isn't touch friendly.

Contoso wants to modernize this app with new Windows features to enable employees to create expenses reports more efficiently. Many of the features could be easily implemented by building a new UWP app. However, the existing app is complex and is the result of many years of development by different teams. As such, rewriting it from scratch with a new technology isn't an option. The team is looking for the best approach to add new features into the existing codebase.

At the beginning of the tutorial, Contoso Expenses targets the .NET Framework 4.7.2 and uses the following 3rd party libraries:

* MVVM Light, a basic implementation for the MVVM pattern.
* Unity, a dependency injection container.
* LiteDb, an embedded NoSQL solution to store the data.
* Bogus, a tool to generate fake data.

In the tutorial, you'll enhance Contoso Expenses with new Windows features:

* Migrate an existing WPF app to .NET Core 3.0. This will open up new and important scenarios in the future.
* Use XAML Islands to host the **InkCanvas** and **MapControl** wrapped controls provided by the Windows Community Toolkit.
* Use XAML Islands to host any standard UWP XAML control (in this case, a **CalendarView**).
* Integrate Adaptive Cards and Windows notifications into the app.
* Package the app with MSIX and set up a CI/CD pipeline on Azure DevOps so that you can automatically deliver new versions of the app to testers and users as soon as it is available.

## Prerequisites

To perform this tutorial, your development computer must have these prerequisites installed:

* Windows 10, version 1903 (build 18362) or a later version.
* [Visual Studio 2019](https://www.visualstudio.com).
* [.NET Core 3 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0) (install the latest version).

Make sure you install the following workloads and optional features with Visual Studio 2019:

* .NET Desktop development
* Universal Windows Platform development
* Windows SDK (10.0.18362.0 or later)

## Get the Contoso Expenses sample app

Before you begin the tutorial, download the source code for the Contoso Expenses app and make sure you can build the code in Visual Studio.

1. Download the app source code from the **Releases** tab of the [AppConsult WinAppsModernization workshop repository](https://github.com/Microsoft/AppConsult-WinAppsModernizationWorkshop). The direct link is [https://github.com/microsoft/AppConsult-WinAppsModernizationWorkshop/releases](https://github.com/microsoft/AppConsult-WinAppsModernizationWorkshop/releases).
2. Open the zip file and extract all the content to the root of your **C:\\** drive. It will create a folder named **C:\WinAppsModernizationWorkshop**.
3. Open Visual Studio 2019 and double click on the **C:\WinAppsModernizationWorkshop\Lab\Exercise1\01-Start\ContosoExpenses\ContosoExpenses.sln** file to open the solution.
4. Verify that you can build, run, and debug the Contoso Expenses WPF project by pressing the **Start** button or CTRL + F5.

## Get started

After you have the source code for the Contoso Expenses sample app and you can confirm that you can build it in Visual Studio, you're ready to start the tutorial:

* [Part 1: Migrate the Contoso Expenses app to .NET Core 3](modernize-wpf-tutorial-1.md)
* [Part 2: Add a UWP InkCanvas control using XAML Islands](modernize-wpf-tutorial-2.md)
* [Part 3: Add a UWP CalendarView control using XAML Islands](modernize-wpf-tutorial-3.md)
* [Part 4: Add Windows 10 user activities and notifications](modernize-wpf-tutorial-4.md)
* [Part 5: Package and deploy with MSIX](modernize-wpf-tutorial-5.md)

## Important concepts

The following sections provide background for some of the key concepts discussed in this tutorial. If you're already familiar with these concepts, you can skip this section.

### Universal Windows Platform (UWP)

In Windows 8, Microsoft introduced a new API set as part of the Windows Runtime (WinRT). Unlike the .NET Framework, WinRT is a native layer of APIs which are exposed directly to apps. WinRT also introduced language projections, which are layers added on top of the runtime to allow developers to interact with it using languages such as C# and JavaScript in addition to C++. Projections enable developers to build apps on top of WinRT that leverage the same C# and XAML knowledge they acquired in building apps with the .NET Framework. 

In Windows 10, Microsoft introduced the [Universal Windows Platform (UWP)](/windows/uwp/get-started/universal-application-platform-guide), which is built on top of WinRT. The most important feature of UWP is that it offers a common set of APIs across every device platform: no matter if the app is running on a desktop, on an Xbox One or on a HoloLens, you’re able to use the same APIs.

Going forward, most new Windows features are exposed via WinRT APIs, including features such as Timeline, Project Rome, and Windows Hello.

### MSIX packaging

[MSIX](/windows/msix/) is the modern packaging model for Windows apps. MSIX supports UWP apps as well as desktop apps building using technologies such as Win32, WPF, Windows Forms, Java, Electron, and more. When you package a desktop app in an MSIX package, you can publish your app to the Microsoft Store. Your desktop app also get package identity when it is installed, which enables your desktop app to use a broader set of WinRT APIs.

For more information, see these articles:

* [Package desktop applications](/windows/uwp/porting/desktop-to-uwp-root)
* [Behind the scenes of your packaged desktop application](/windows/uwp/porting/desktop-to-uwp-behind-the-scenes)

### XAML Islands

Starting in Windows 10, version 1903, you can host UWP controls in non-UWP desktop apps using a feature called *XAML Islands*. This feature enables you to enhance the look, feel, and functionality of your existing desktop apps with the latest Windows UI features that are only available via UWP controls. This means that you can use UWP features such as Windows Ink and controls that support the Fluent Design System in your existing WPF, Windows Forms, and C++ Win32 apps.

For more information, see [UWP controls in desktop applications (XAML Islands)](/windows/uwp/xaml-platform/xaml-host-controls). This tutorial guides you through the process of using two different types of XAML Island controls:

* The [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas) and [MapControl](/windows/communitytoolkit/controls/wpf-winforms/mapcontrol) in the Windows Community Toolkit. These WPF controls wrap the interface and functionality of the corresponding UWP controls and can be used like any other WPF control in the Visual Studio designer.

* The UWP [Calendar view](/windows/uwp/design/controls-and-patterns/calendar-view) control. This is a standard UWP control that you will host by using the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control in the Windows Community Toolkit.

### .NET Core 3

[.NET Core](/dotnet/core/) is an open-source framework that implements a cross-platform, lightweight and easily extensible version of the full .NET Framework. Compared to the full .NET Framework, .NET Core startup time is much faster and many of the APIs have been optimized.

Through its first several releases, the focus of .NET Core was for supporting web or back-end apps. With .NET Core, you can easily build scalable web apps or APIs that can be hosted on Windows, Linux, or in micro-service architectures like Docker containers.

.NET Core 3 is latest release of .NET Core. The highlight of this release is support for Windows desktop apps, including Windows Forms and WPF apps. You can run new and existing Windows desktop apps on .NET Core 3 and enjoy all the benefits that .NET Core has to offer. UWP controls that are hosted in [XAML Islands](xaml-islands.md) can also be used in Windows Forms and WPF apps that target .NET Core 3.

> [!NOTE]
> WPF and Windows Forms are not becoming cross-platform, and you cannot run a WPF or Windows Forms on Linux and MacOS. The UI components of WPF and Windows Forms still have a dependency on the Windows rendering system.

For more information, see [What's new in .NET Core 3.0](/dotnet/core/whats-new/dotnet-core-3-0).
