---
title: Use the Windows App SDK in an existing project
description: This article provides instructions for using the Windows App SDK in existing projects.
ms.topic: article
ms.date: 08/30/2021
keywords: windows win32, desktop development, Windows App SDK
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Use the Windows App SDK in an existing project

If you have an existing desktop project in which you want to use the Windows App SDK, you can install the latest version of the Windows App SDK NuGet package in your project. Unpackaged apps (that is, apps that do not use MSIX for their deployment technology) must follow this procedure to use the Windows App SDK, but packaged apps can do this too.

[!INCLUDE [UWP migration guidance](./includes/uwp-app-sdk-migration-pointer.md)]

> [!NOTE]
> This procedure is supported in C# .NET 5 or later and C++ desktop projects. These projects can use the NuGet package from the [stable release channel](stable-channel.md), [preview release channge](preview-channel.md) or [experimental release channel](experimental-channel.md).

## Prerequisites

- Visual Studio 2019 or Visual Studio 2022 with the required workloads and components for Windows app development. For more information, see [Install Visual Studio](set-up-your-development-environment.md).

## Instructions

1. Open an existing project in Visual Studio.

    > [!NOTE]
    > If you have a C# desktop project, make sure the **TargetFramework** element in the project file is assigned to a Windows 10-specific moniker, such as **net5.0-windows10.0.19041.0**, so that it can call Windows Runtime APIs. For more information, see [Call Windows Runtime APIs in desktop apps](../../apps/desktop/modernize/desktop-to-uwp-enhance.md#net-5-and-later-use-the-target-framework-moniker-option). Additionally, you must be targeting **18362** or higher as there is a [known issue blocking apps that target **17763**](https://github.com/microsoft/WindowsAppSDK/issues/921).

2. Make sure [package references](/nuget/consume-packages/package-references-in-project-files) are enabled:

    1. In Visual Studio, click **Tools -> NuGet Package Manager -> Package Manager Settings**.
    2. Make sure **PackageReference** is selected for **Default package management format**.

3. Right-click your project in **Solution Explorer** and choose **Manage NuGet Packages**.

4. In the **NuGet Package Manager** window, select the **Include prerelease** check box near the top of the window, select the **Browse** tab, and search for one of the following packages:

    - To install one of the [1.0 or later releases](downloads.md), search for the **Microsoft.WindowsAppSDK** package.
    - To install one of the [0.8 releases](downloads.md), search for the **Microsoft.ProjectReunion** package.

5. After the appropriate Windows App SDK NuGet package is found, select the package and then click **Install** in the right pane of the **NuGet Package Manager** window.

    [![Screenshot of the Windows App SDK NuGet package being installed](images/reunion-nuget-install.png) ](images/reunion-nuget-install.png#lightbox)

    > [!NOTE]
    > The Windows App SDK NuGet package contains other sub-packages (including **Microsoft.WindowsAppSDK.Foundation**, **Microsoft.WindowsAppSDK.WinUI**, and more) that contain the implementations for specific components in the Windows App SDK. You cannot install these sub-packages individually to reference only certain components in your project. You must install the main Windows App SDK NuGet package, which includes all of the components.

6. **For unpackaged apps only**: Before your unpackaged app can use Windows App SDK APIs and components, your app must call first call the *bootstrapper API* to initialize the Windows App SDK framework package. For more information, see [Reference the Windows App SDK framework package at run time](reference-framework-package-run-time.md) and [Tutorial: Build and deploy an unpackaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

7. Your app can now use Windows App SDK APIs and components that are available in the release channel you installed. For the list of available features, see [release channels](release-channels.md).

## Related topics

- [Windows App SDK](index.md)
- [Release channels and release notes](release-channels.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#apps-that-use-the-windows-app-sdk)
