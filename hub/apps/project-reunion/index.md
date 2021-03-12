---
description: Learn about Project Reunion, benefits it provides to developers, what is ready for developers now, and how to give feedback.
title: Project Reunion
ms.topic: article
ms.date: 03/09/2021
keywords: windows win32, desktop development, project reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Build desktop Windows apps with Project Reunion 0.5 Preview (March 2021)

Project Reunion is a new set of developer components and tools that represents the next evolution in the Windows app development platform. Project Reunion provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on a broad set of target Windows 10 OS versions. 

Project Reunion does not replace the existing desktop Windows app platforms and frameworks such as .NET (including Windows Forms and WPF) and C++/Win32. Instead, it complements these existing platforms with a common set of APIs and tools that developers can rely on across these platforms.

> [!NOTE]
> Project Reunion 0.5 Preview is a developer preview. You are encouraged to try this release in your development environment. However, be aware that Project Reunion will change in many ways between now and the 1.0 release. Project Reunion 0.5 Preview is not supported for apps that are used in production environments. **Project Reunion** is a code name that may change in a future release.

## Benefits of Project Reunion for Windows app developers

Project Reunion provides a broad set of Windows APIs with implementations that are decoupled from the OS and released to developers via NuGet packages. Project Reunion is not meant to replace the Windows SDK. The Windows SDK will continue to work as is, and there are many core components of Windows that will continue to evolve via APIs that are delivered via OS and Windows SDK releases. Developers are encouraged to adopt Project Reunion at their own pace.

#### Unified API surface across desktop app platforms

Developers who want to create desktop Windows apps must choose between several app platforms and frameworks. Although each platform provides many features and APIs that can be used by apps that are built using other platforms, some features and APIs can only be used by specific platforms. Project Reunion will unify access to Windows APIs for all desktop Windows 10 apps. No matter which app model you choose, you will have access to the same set of Windows APIs that are available in Project Reunion.

Over time, we plan to make further investments in Project Reunion that remove more distinctions between the different app models. Project Reunion will include both WinRT APIs and native C APIs.

#### Consistent support across Windows 10 versions

As the Windows APIs continue to evolve with new OS versions, developers must use techniques such as [version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code) to account for all the differences in versions to reach their application audience. This can add complexity to the code and the development experience.

Project Reunion APIs will work on Windows 10, version 1809, and all later versions of Windows 10. This means that as long as your customers are on Windows 10, version 1809, or any later version, you can use new Project Reunion APIs and features as soon as they are released, and without having to write version adaptive code.

#### Faster release cadence

New Windows APIs and features have typically been tied to OS releases that happen on a once or twice a year release cadence. Project Reunion will ship updates on a faster cadence, enabling you to get earlier and more rapid access to innovations in the Windows development platform as soon as they are created.

## Components available with Project Reunion

Project Reunion 0.5 Preview includes the following components.

| Component | Description |
|---------|-------------|
| [Windows UI Library 3](../winui/winui3/index.md) | Windows UI Library (WinUI) 3 is the next generation of the Windows user experience (UX) platform for both desktop (.NET and C++/Win32) and UWP apps. This release includes Visual Studio project templates to help get started [building apps with a WinUI-based user interface](..\winui\winui3\index.md#create-winui-projects), and a NuGet package that contains the WinUI libraries.  |
| [Manage resources with MRT Core](mrtcore/mrtcore-overview.md) | MRT Core provides APIs to load and manage resources used by your app. MRT Core is a streamlined version of the modern [Windows Resource Management System](/windows/uwp/app-resources/resource-management-system). |
| [Render text with DWriteCore](dwritecore.md) | DWriteCore provides access to all current DirectWrite features for text rendering, including a device-independent text layout system, hardware-accelerated text, multi-format text, and wide language support.  |

You can learn more about the future plans to bring other components into Project Reunion [here](https://github.com/microsoft/ProjectReunion/blob/master/docs/README.md).

## Set up your development environment

1. Ensure that your development computer has Windows 10, version 1809 (build 17763), or a later OS version installed.

2. Install [Visual Studio 2019, version 16.10 Preview](https://visualstudio.microsoft.com/vs/preview/) (or later) if you haven't done so already. 

    > [!NOTE]
    > Visual Studio 2019, version 16.9 also supports Project Reunion, but does not support WinUI 3 tooling features. For more information on WinUI 3 tooling support, see Windows UI Library 3 - Project Reunion 0.5 Preview (March 2021).

    You must include the following components when installing Visual Studio:
    - On the **Workloads** tab, make sure **Universal Windows Platform development** is selected.
    - On the **Individual components** tab, make sure **Windows 10 SDK (10.0.19041.0)** is selected in the **SDKs, libraries, and frameworks** section.

    To build .NET apps, you must also include the following components:
    - **.NET Desktop Development** workload.

    To build C++ apps, you must also include the following components:
    - **Desktop development with C++** workload.
    - The **C++ (v142) Universal Windows Platform tools** optional component for the **Universal Windows Platform development** workload.

3. If you previously installed the [WinUI 3 Preview extension](https://marketplace.visualstudio.com/items?itemName=Microsoft-WinUI.WinUIProjectTemplates) from an earlier WinUI 3 preview release, uninstall the extension. For more information about how to uninstall an extension, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

4. Make sure your system has a NuGet package source enabled for **nuget.org**. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior).

5. In Visual Studio 2019, click **Extensions** > **Manage Extensions**, search for **Project Reunion**, and install the **Project Reunion** extension. Alternatively, you can download and install the [Project Reunion extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftProjectReunion) directly from the Visual Studio Marketplace. For instructions on how to add the VSIX package to Visual Studio, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

6. To use WinUI 3 tooling such as Live Visual Tree, Hot Reload, and Live Property Explorer, you must enable WinUI 3 tooling with Visual Studio Preview Features as described in the [instructions here](https://github.com/microsoft/microsoft-ui-xaml/issues/4140).

## Get started developing with Project Reunion

You can use Project Reunion 0.5 Preview in new apps created by using the WinUI 3 project templates that are included with the Project Reunion extension, or you can use Project Reunion components in existing projects by installing a NuGet package.

> [!NOTE]
> Project Reunion 0.5 Preview only supports MSIX-packaged apps.

#### Create a new project that uses Project Reunion

The Project Reunion 0.5 Preview extension for Visual Studio 2019 includes project templates that generate projects with a WinUI 3-based UI layer and provide access to all other Project Reunion APIs. You can use these templates to build MSIX-packaged desktop apps (either C#/.NET 5 or C++/WinRT), or UWP apps that use Project Reunion. For more information about the available project templates, see [Create WinUI projects](..\winui\winui3\index.md#create-winui-projects).

To create a new project that uses Project Reunion 0.5 Preview:

1. Follow the instructions in the following articles:

    - [Get started with WinUI 3 for desktop apps](..\winui\winui3\get-started-winui3-for-desktop.md)
    - [Get started with WinUI 3 for UWP apps](..\winui\winui3\get-started-winui3-for-uwp.md)

2. After you create your project, you have access to the following Project Reunion APIs and components in addition to all other Windows and .NET APIs that are typically available to desktop and UWP apps.

    - [Windows UI Library 3](../winui/winui3/index.md)
    - [Manage resources MRT Core](mrtcore/mrtcore-overview.md)
    - [Render text with DWriteCore](dwritecore.md)

To confirm that your new project uses Project Reunion, expand the **Dependencies** > **Packages** node under your project in **Solution Explorer**. You should see several **Microsoft.ProjectReunion** packages listed under this node, similar to the following image.

![Screenshot of Project Reunion packages in Solution Explorer pane](images/reunion-packages.png)

#### Use Project Reunion in an existing project

If you have an existing desktop project (either C#/.NET 5 or C++/WinRT) in which you want to use Project Reunion, you can install the Project Reunion 0.5 Preview NuGet package in your project. 

> [!NOTE]
> In this scenario, you can only use non-WinUI 3 components that are part of Project Reunion in your project. To use WinUI 3, you must create a new project using one of the WinUI 3 project templates as described in the previous section.

1. Open an existing desktop project (either C#/.NET 5 or C++/WinRT) or UWP project in Visual Studio 2019.

2. Make sure [package references](/nuget/consume-packages/package-references-in-project-files) are enabled:

    1. In Visual Studio, click **Tools -> NuGet Package Manager -> Package Manager Settings**.
    2. Make sure **PackageReference** is selected for **Default package management format**.

3. Right-click your project in **Solution Explorer** and choose **Manage NuGet Packages**.

4. In the **NuGet Package Manager** window, select the **Browse** tab and search for `Microsoft.ProjectReunion`.

5. After the **Microsoft.ProjectReunion** package is found, in the right pane of the **NuGet Package Manager** window click **Install**.

6. After you install the package, you can use the following Project Reunion APIs and components in your project:

    - [Manage resources MRT Core](mrtcore/mrtcore-overview.md)
    - [Render text with DWriteCore](dwritecore.md)

#### Deploying apps that use Project Reunion

Currently, apps that use Project Reunion must be packaged using [MSIX](/windows/msix). By default, when you create a project using one of the WinUI project templates that are provided with the Project Reunion extension for Visual Studio, your project includes a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an MSIX package. For more information about configuring this project to build an MSIX package for your app, see [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps).

After you build an MSIX package for your app, you have several options for deploying it to other computers. For more information, see [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview).

> [!NOTE]
> Apps that use Project Reunion 0.5 Preview cannot be published to the Store.

## Samples

The following Project Reunion samples are currently available for you to explore.

- [DWriteCore gallery sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/DWriteCore/DWriteCoreGallery): This sample application demonstrates the [DWriteCore](dwritecore.md) API.
- [MRT Core sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/MrtCore): This sample application demonstrates the [MRT Core](mrtcore/mrtcore-overview.md) API.
- [Hello World sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/HelloWorld/reunioncppdesktopsampleapp): This sample demonstrates a basic integration with the Project Reunion NuGet package.

## Limitations and known issues

- This release is not supported for apps that are used in production environments. Expect bugs, limitations, and other issues.
- This release can only be used in MSIX-packaged desktop apps (C#/.NET 5 or C++/Win32). It cannot be used in unpackaged desktop apps.
- The [tools limitations for WinUI 3](..\winui\winui3\index.md#developer-tools) also apply to any project that uses Project Reunion 0.5 Preview.

## Developer roadmap

For the latest Project Reunion plans, see our [Github page](https://github.com/microsoft/ProjectReunion).

## Give feedback and contribute

We are building Project Reunion as an open source project. We have a lot more information on our [Github page](https://github.com/microsoft/ProjectReunion) about how we're planning on making Project Reunion a reality, and we're inviting you to be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/ProjectReunion/blob/master/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that Project Reunion brings the biggest benefits to developers like you.
