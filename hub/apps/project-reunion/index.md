---
description: Learn about Project Reunion, benefits it provides to developers, what is ready for developers now, and how to give feedback.
title: Build desktop Windows apps with Project Reunion 0.5
ms.topic: article
ms.date: 03/19/2021
keywords: windows win32, desktop development, project reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Build desktop Windows apps with Project Reunion 0.5

Project Reunion is a set of new developer components and tools that represent the next evolution in the Windows app development platform. Project Reunion provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on a broad set of target Windows 10 OS versions.

Project Reunion does not replace the existing desktop Windows app platforms and frameworks such as .NET (including Windows Forms and WPF) and C++/Win32. Instead, it complements these existing platforms with a common set of APIs and tools that developers can rely on across these platforms. For more details, see [Benefits of Project Reunion](#benefits-of-project-reunion-for-windows-developers).

> [!NOTE]
> Project Reunion 0.5 is supported for use in MSIX-packaged desktop apps (C#/.NET 5 or C++/Win32) in production environments. Packaged desktop apps that use Project Reunion 0.5 can be published to the Microsoft Store. For UWP apps, Project Reunion 0.5 is available only as a preview. This release is not supported for UWP apps that are used in production environments.
>
>**Project Reunion** is a code name that may change in a future release.

## Overview

Project Reunion provides an extension for Visual Studio 2019 that includes project templates configured to use Project Reunion components in new projects. The Project Reunion libraries are also available via a NuGet package that you can install in existing projects. For more information, see [Get started with Project Reunion](get-started-with-project-reunion.md).

After you build an app that uses Project Reunion, you can deploy it to other computers. For more information, see [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md).

Project Reunion 0.5 includes the following sets of APIs and components you can use in your apps. You can learn more about the future plans to bring other components into Project Reunion [here](https://github.com/microsoft/ProjectReunion/blob/master/docs/README.md).

| Component | Description |
|---------|-------------|
| [Windows UI Library 3](../winui/winui3/index.md) | Windows UI Library (WinUI) 3 is the next generation of the Windows user experience (UX) platform for Windows apps. This release includes Visual Studio project templates to help get started [building apps with a WinUI-based user interface](..\winui\winui3\winui-project-templates-in-visual-studio.md), and a NuGet package that contains the WinUI libraries.  |
| [Manage resources with MRT Core](mrtcore/mrtcore-overview.md) | MRT Core provides APIs to load and manage resources used by your app. MRT Core is a streamlined version of the modern [Windows Resource Management System](/windows/uwp/app-resources/resource-management-system). |
| [Render text with DWriteCore](dwritecore.md) | DWriteCore provides access to all current DirectWrite features for text rendering, including a device-independent text layout system, hardware-accelerated text, multi-format text, and wide language support.  |

## Benefits of Project Reunion for Windows developers

Project Reunion provides a broad set of Windows APIs with implementations that are decoupled from the OS and released to developers via NuGet packages. Project Reunion is not meant to replace the Windows SDK. The Windows SDK will continue to work as is, and there are many core components of Windows that will continue to evolve via APIs that are delivered via OS and Windows SDK releases. Developers are encouraged to adopt Project Reunion at their own pace.

### Unified API surface across desktop app platforms

Developers who want to create desktop Windows apps must choose between several app platforms and frameworks. Although each platform provides many features and APIs that can be used by apps that are built using other platforms, some features and APIs can only be used by specific platforms. Project Reunion will unify access to Windows APIs for all desktop Windows 10 apps. No matter which app model you choose, you will have access to the same set of Windows APIs that are available in Project Reunion.

Over time, we plan to make further investments in Project Reunion that remove more distinctions between the different app models. Project Reunion will include both WinRT APIs and native C APIs.

### Consistent support across Windows 10 versions

As the Windows APIs continue to evolve with new OS versions, developers must use techniques such as [version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code) to account for all the differences in versions to reach their application audience. This can add complexity to the code and the development experience.

Project Reunion APIs will work on Windows 10, version 1809, and all later versions of Windows 10. This means that as long as your customers are on Windows 10, version 1809, or any later version, you can use new Project Reunion APIs and features as soon as they are released, and without having to write version adaptive code.

### Faster release cadence

New Windows APIs and features have typically been tied to OS releases that happen on a once or twice a year release cadence. Project Reunion will ship updates on a faster cadence, enabling you to get earlier and more rapid access to innovations in the Windows development platform as soon as they are created.

## Limitations and known issues

- **Desktop apps (C#/.NET 5 or C++/Win32)**: Project Reunion 0.5 cannot be used in unpackaged desktop apps (C#/.NET 5 or C++/Win32). This release is supported for use only in MSIX-packaged desktop apps.
- **UWP apps**: Project Reunion 0.5 is not supported for UWP apps that are used in production environments. To use Project Reunion in UWP apps, you must use a preview version of the Project Reunion 0.5 extension that is not supported for production environments. For more information about installing the preview extension, see [Set up your development environment](get-started-with-project-reunion.md#set-up-your-development-environment).
- The [WinUI 3 developer tool limitations](..\winui\winui3\index.md#developer-tools) also apply to any project that uses Project Reunion 0.5.
- There are [some limitations](get-started-with-project-reunion.md#limitations-for-using-project-reunion-in-existing-projects) for installing the Project Reunion 0.5 NuGet package in existing projects.

#### ASTA to STA threading model

If you're migrating code from an existing UWP app to a new C#/.NET 5 or C++/Win32 WinUI 3 project that uses Project Reunion, be aware that the new project uses the [single-threaded apartment (STA)](/windows/win32/com/single-threaded-apartments) threading model instead of the [Application STA (ASTA)](https://devblogs.microsoft.com/oldnewthing/20210224-00/?p=104901) threading model used by UWP apps. If your code assumes the non re-entrant behavior of the ASTA threading model, your code may not behave as expected.

## Developer roadmap

For the latest Project Reunion plans, see our [roadmap](https://github.com/microsoft/ProjectReunion/blob/main/docs/roadmap.md).

## Give feedback and contribute

We are building Project Reunion as an open source project. We have a lot more information on our [Github page](https://github.com/microsoft/ProjectReunion) about how we're building Project Reunion, and how you can be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/ProjectReunion/blob/master/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that Project Reunion brings the biggest benefits to developers like you.

## Related topics

- [Build desktop Windows apps with Project Reunion](index.md)
- [Get started with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)
- [Windows UI Library 3](../winui/winui3/index.md)
- [Manage resources with MRT Core](mrtcore/mrtcore-overview.md)
- [Render text with DWriteCore](dwritecore.md)
