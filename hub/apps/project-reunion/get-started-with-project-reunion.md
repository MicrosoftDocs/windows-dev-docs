---
description: This article provides instructions for installing the Project Reunion extension for Visual Studio 2019 on your development computer and using Project Reunion in new or existing projects.
title: Get started with Project Reunion
ms.topic: article
ms.date: 03/19/2021
keywords: windows win32, desktop development, project reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Get started with Project Reunion

This article provides instructions for installing the Project Reunion extension for Visual Studio 2019 on your development computer and using Project Reunion in new or existing projects. Before you install and use Project Reunion, see the [limitations and known issues](index.md#limitations-and-known-issues).

> [!NOTE]
> If you created a project with an earlier preview or release version of Project Reunion or WinUI 3, you can [update the project to use the latest release](update-existing-projects-to-the-latest-release.md).

## Set up your development environment

1. Ensure that your development computer has Windows 10, version 1809 (build 17763), or a later OS version installed.

2. Install [Visual Studio 2019, version 16.10 Preview](https://visualstudio.microsoft.com/vs/preview/) (or later) if you haven't done so already.

    > [!NOTE]
    > Visual Studio 2019, version 16.9 also supports Project Reunion, but does not support all WinUI 3 tooling features. For more information about WinUI 3 tooling support, see [Windows UI Library 3 - Project Reunion 0.5](../winui/winui3/index.md).

    You must include the following components when installing Visual Studio:
    - On the **Workloads** tab of the installation dialog, make sure **Universal Windows Platform development** is selected.
    - On the **Individual components** tab of the installation dialog, make sure **Windows 10 SDK (10.0.19041.0)** is selected in the **SDKs, libraries, and frameworks** section.

    To build .NET apps, you must also include the following components:
    - On the **Workloads** tab of the installation dialog, make sure **.NET Desktop Development** is selected.

    To build C++ apps, you must also include the following components:
    - On the **Workloads** tab of the installation dialog, make sure **Desktop development with C++** is selected.
    - In the **Installation details** pane on the right side of the installation dialog, make sure the **C++ (v142) Universal Windows Platform tools** optional component is selected in the **Universal Windows Platform development** section.

3. If you previously installed the [WinUI 3 Preview extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=Microsoft-WinUI.WinUIProjectTemplates), uninstall the extension. For more information about how to uninstall an extension, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

4. Make sure your system has a NuGet package source enabled for the official NuGet service index at `https://api.nuget.org/v3/index.json`. 

    1. In Visual Studio, select **Tools** -> **NuGet Package Manager** -> **Package Manager Settings** to open the **Options** dialog. 
    2. In the left pane of the **Options** dialog, select the **Package Sources** tab, and make sure there is a package source for **nuget.org** that points to `https://api.nuget.org/v3/index.json` as the source URL. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior).

5. Download and install the Project Reunion 0.5 extension for Visual Studio. There are two versions of the extension: one for desktop (C#/.NET 5 or C++/WinRT) apps and one for UWP apps.

    To use Project Reunion in desktop (C#/.NET 5 or C++/WinRT) apps:
    - In Visual Studio 2019, click **Extensions** > **Manage Extensions**, search for **Project Reunion**, and install the **Project Reunion** extension.
    - Alternatively, you can download and install the [Project Reunion 0.5 extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftProjectReunion) directly from the Visual Studio Marketplace.

    To use Project Reunion in UWP apps, you must install a preview version of the extension that is not supported for production environments:
    - Uninstall any existing versions of the Project Reunion VSIX.
    - In Visual Studio 2019, click **Extensions** > **Manage Extensions**, and click **Change your settings for Extensions** in the bottom left corner. Turn off automatic updates for packages installed for all users before installing the older version.
    - Download and install the [Project Reunion 0.5 Preview extension](https://download.microsoft.com/download/9/9/8/9981a84b-8fd8-4645-9dce-c62761601f17/ProjectReunion.Extension.vsix).

    For instructions about how to add the VSIX package to Visual Studio, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

    ![Screenshot of Project Reunion extension being installed](images/reunion-extension-install.png)

6. To use WinUI 3 tooling such as Live Visual Tree, Hot Reload, and Live Property Explorer in Visual Studio 2019 16.10 Preview, you must enable WinUI 3 tooling with Visual Studio Preview Features. For instructions, see [How to Enable UI Tooling for WinUI 3 in VS 16.9 Preview 4](https://github.com/microsoft/microsoft-ui-xaml/issues/4140).

## Create a new project that uses Project Reunion

The Project Reunion 0.5 extensions for Visual Studio 2019 (including the extension for desktop apps and the preview extension for UWP apps) provide project templates that generate projects with a WinUI 3-based UI layer and provide access to all other Project Reunion APIs. For more information about the available project templates, see [WinUI 3 project templates in Visual Studio](..\winui\winui3\winui-project-templates-in-visual-studio.md).

> [!NOTE]
> The desktop (C#/.NET 5 and C++/WinRT) project templates are supported for use in production environments. The UWP project templates are available as a developer preview only, and cannot be used to build apps for production environments.

To create a new project that uses Project Reunion 0.5:

1. Follow the instructions in the following articles:

    - [Get started with WinUI 3 for desktop apps](..\winui\winui3\get-started-winui3-for-desktop.md)
    - [Get started with WinUI 3 for UWP apps (Preview)](..\winui\winui3\get-started-winui3-for-uwp.md)
    - [Build a basic WinUI 3 desktop app](..\winui\winui3\desktop-build-basic-winui3-app.md)

2. After you create your project, you have access to the following Project Reunion APIs and components in addition to all other Windows and .NET APIs that are typically available to desktop and UWP apps.

    - [Windows UI Library 3](../winui/winui3/index.md)
    - [Manage resources MRT Core](mrtcore/mrtcore-overview.md)
    - [Render text with DWriteCore](dwritecore.md)

To confirm that your new project uses Project Reunion, expand the **Dependencies** > **Packages** node under your project in **Solution Explorer**. You should see several **Microsoft.ProjectReunion** packages listed under this node, similar to the following image.

![Screenshot of Project Reunion packages in Solution Explorer pane](images/reunion-packages.png)

## Use Project Reunion in an existing project

If you have an existing project in which you want to use Project Reunion, you can install the Project Reunion 0.5 NuGet package in your project. This scenario has [some limitations](index.md#using-the-project-reunion-nuget-package-in-existing-projects).

1. Open an existing desktop project (either C#/.NET 5 or C++/WinRT) or UWP project in Visual Studio 2019.

    > [!NOTE]
    > If you have a C#/.NET 5 desktop project, make sure the **TargetFramework** element in the project file is assigned to a Windows 10-specific .NET 5 moniker, such as **net5.0-windows10.0.19041.0**, so that it can call Windows Runtime APIs. For more information, see [this section](../../apps/desktop/modernize/desktop-to-uwp-enhance.md#net-5-use-the-target-framework-moniker-option).

2. Make sure [package references](/nuget/consume-packages/package-references-in-project-files) are enabled:

    1. In Visual Studio, click **Tools -> NuGet Package Manager -> Package Manager Settings**.
    2. Make sure **PackageReference** is selected for **Default package management format**.

3. Right-click your project in **Solution Explorer** and choose **Manage NuGet Packages**.

4. In the **NuGet Package Manager** window, select the **Browse** tab and search for `Microsoft.ProjectReunion`.

5. After the **Microsoft.ProjectReunion** package is found, in the right pane of the **NuGet Package Manager** window click **Install**.

    ![Screenshot of Project Reunion NuGet package being installed](images/reunion-nuget-install.png)

6. **For C#/.NET 5 projects only**: In order to receive all of the fixes from the latest stable release of Project Reunion 0.5, you'll need to update your project file to explicitly set your .NET SDK to the latest version. For more information, see [.NET SDK references](index.md#net-sdk-references).

7. After you install the **Microsoft.ProjectReunion** package, you can use the following Project Reunion APIs and components in your project:

    - [Manage resources MRT Core](mrtcore/mrtcore-overview.md)
    - [Render text with DWriteCore](dwritecore.md)

## Samples

The following Project Reunion samples are currently available for you to explore.

- [DWriteCore gallery sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/DWriteCore/DWriteCoreGallery): This sample application demonstrates the [DWriteCore](dwritecore.md) API.
- [MRT Core sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/MrtCore): This sample application demonstrates the [MRT Core](mrtcore/mrtcore-overview.md) API.
- [Hello World sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/HelloWorld/reunioncppdesktopsampleapp): This sample demonstrates a basic integration with the Project Reunion NuGet package.
- [Xaml Controls Gallery](https://aka.ms/winui3/xcg): This is a sample app that showcases all of the WinUI 3 controls in action. 

## Related topics

- [Build desktop Windows apps with Project Reunion](index.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)
