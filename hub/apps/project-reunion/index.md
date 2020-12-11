---
description: Learn about Project Reunion, benefits it provides to developers, what is ready for developers now, and how to give feedback.
title: Project Reunion
ms.topic: article
ms.date: 11/17/2020
keywords: windows win32, desktop development, project reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Build Windows apps with Project Reunion (Prerelease)

Project Reunion 0.1 Prerelease is a preview of a new set of developer components and tools that represents the next evolution in the Windows app development platform. Project Reunion provides a unified set of APIs and tools that can be used in a consistent way by any app on a broad set of target Windows 10 OS versions. Project Reunion does not replace the existing Windows app platforms and frameworks such as UWP and native Win32, and .NET. Instead, it complements these existing platforms with a common set of APIs and tools that developers can rely on across these platforms.

> [!NOTE]
> Project Reunion 0.1 Prerelease is an early developer preview. You are encouraged to try this release in your development environment. However, be aware that Project Reunion will change in many ways between now and the final release. Project Reunion 0.1 Prerelease is not supported for apps that are used in production environments. **Project Reunion** is a temporary code name.

## Goals of Project Reunion

Project Reunion provides a broad set of Windows APIs with implementations that are decoupled from the OS and released to developers via NuGet packages. Project Reunion is not meant to replace the Windows SDK. The Windows SDK will continue to work as is, and there are many core components of Windows that will continue to evolve via APIs that are delivered via OS and Windows SDK releases. Developers are encouraged to adopt Project Reunion at their own pace.

Project Reunion was created to support the following goals.

#### Unified API surface across different types of Windows apps

Developers who want to create Windows apps must choose between several app platforms and frameworks. Although each platform provides many features and APIs that can be used by apps that are built using other platforms, some features and APIs can only be used by specific platforms. Project Reunion will unify access to Windows APIs for all Windows 10 apps. No matter which app model you choose, you will have access to the same set of Windows APIs that are available in Project Reunion.

Over time, we plan to make further investments in Project Reunion that remove more distinctions between the different app models. Project Reunion will include both WinRT APIs and native C APIs.

#### Consistent support across Windows 10 versions

As the Windows APIs continue to evolve with new OS versions, developers must use techniques such as [version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code) to account for all the differences in versions to reach their application audience. This can add complexity to the code and the development experience. Project Reunion will enable us to unify access to Project Reunion APIs across different OS versions and all Windows 10 devices, allowing us to opportunistically make updates available to more developers and not just the newest version of Windows. The current plan is for Project Reunion to support Windows 10, version 1809, and all later versions of Windows 10.

#### Faster release cadence

New Windows APIs and features have typically been tied to OS releases that happen on a once or twice a year release cadence. Project Reunion will enable us to release new production-quality APIs and features in a more frequent and agile manner.

## Get started

Project Reunion 0.1 Prerelease includes new APIs for the following feature areas.

| Feature | Description |
|---------|-------------|
| [MRT Core (Modern Resource Management System)](mrtcore/mrtcore-overview.md) | MRT Core is a streamlined version of the modern [Windows Resource Management System](/windows/uwp/app-resources/resource-management-system) that is distributed as part of Project Reunion. |
| [DWriteCore](dwritecore.md) | DWriteCore is a form of DirectWrite that runs on versions of Windows down to Windows 8, and opens the door for you to use it cross-platform. |

You can learn more about the future plans to bring other components into Project Reunion [here](https://github.com/microsoft/ProjectReunion/blob/master/docs/README.md).

> [!NOTE]
> Certain other existing components, including the Windows UI Library (WinUI), MSIX, and WebView2, are already decoupled from the OS and follow Project Reunion guidelines (for example, they are supported on Windows 10, version 1809, and later OS versions). However, these other components are currently not part of the Project Reunion NuGet package.  

### Set up your development environment

If you want to try out Project Reunion 0.1 Prerelease, you must start with one of the provided C++ samples. These samples are pre-configured to use the Project Reunion NuGet package. This preview does not support installing the  Project Reunion NuGet package in your own projects. Follow these steps to set up your development environment to use one of the samples.

1. Ensure that your development computer has Windows 10, version 1809 (build 17763), or a later OS version installed.

2. Install [Visual Studio 2019, version 16.9 Preview 2 (or later)](https://visualstudio.microsoft.com/vs/preview/). You must include the following workloads when installing Visual Studio:
    - .NET Desktop Development
    - Universal Windows Platform development
    - Desktop development with C++
    - The **C++ (v142) Universal Windows Platform tools** optional component for the Universal Windows Platform workload (see **Installation Details** under the **Universal Windows Platform development** section, on the right pane of the installer)

3. Install the latest version of the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) from the Visual Studio Marketplace.

4. Make sure your system has a NuGet package source enabled for **nuget.org**. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior).

5. Download and install the [WinUI 3 Preview 3 VSIX package](https://aka.ms/winui3/preview3-download). This step is only required for the Hello World and MRT Core samples, which are already configured to use WinUI 3. For instructions on how to add the VSIX package to Visual Studio, see [Finding and Using Visual Studio Extensions](/visualstudio/ide/finding-and-using-visual-studio-extensions#install-without-using-the-manage-extensions-dialog-box).

6. Clone and explore the following samples:
    - [DWriteCore gallery sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/DWriteCore/DWriteCoreGallery): This sample application demonstrates the [DWriteCore](dwritecore.md) API.
    - [MRT Core sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/MrtCore): This sample application demonstrates the [MRT Core](mrtcore/mrtcore-overview.md) API.
    - [Hello World sample](https://github.com/microsoft/Project-Reunion-Samples/tree/main/HelloWorld/reunioncppdesktopsampleapp): This sample demonstrates a basic integration with the Project Reunion NuGet package.

## Known issues

Project Reunion 0.1 Prerelease has the following limitations:

 - This release is only supported for MSIX-packaged C++/Win32 apps.
 - This release does not support C#.
 - This release does not support .NET 5.

## Developer roadmap

For the latest Project Reunion plans, see our [Github page](https://github.com/microsoft/ProjectReunion).

## Give feedback and contribute

We are building Project Reunion as an open source project. We have a lot more information on our [Github page](https://github.com/microsoft/ProjectReunion) about how we're planning on making Project Reunion a reality, and we're inviting you to be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/ProjectReunion/blob/master/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that Project Reunion brings the biggest benefits to developers like you.
