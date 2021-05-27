---
title: Stable release channel for Project Reunion 
description: Provides information about the stable release channel for Project Reunion.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, project reunion 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Stable release channel for Project Reunion

The stable channel provides releases of Project Reunion that are supported for use by apps in production environments. Apps that use the release channel of Project Reunion can also be published to the Microsoft Store.

The latest release of the stable channel is Project Reunion [version 0.5.7](https://github.com/microsoft/ProjectReunion/discussions/820). 

> [!div class="button"]
> [Download the latest stable release](https://aka.ms/projectreunion/vsixdownload)

## Features in the latest stable release

The latest stable channel release includes the following sets of APIs and components you can use in your apps.

| Component | Description |
|---------|-------------|
| [Windows UI Library 3](../winui/winui3/index.md) | Windows UI Library (WinUI) 3 is the next generation of the Windows user experience (UX) platform for Windows apps. This release includes Visual Studio project templates to help get started [building apps with a WinUI-based user interface](..\winui\winui3\winui-project-templates-in-visual-studio.md), and a NuGet package that contains the WinUI libraries.  |
| [Manage resources with MRT Core](mrtcore/mrtcore-overview.md) | MRT Core provides APIs to load and manage resources used by your app. MRT Core is a streamlined version of the modern [Windows Resource Management System](/windows/uwp/app-resources/resource-management-system). |
| [Render text with DWriteCore](dwritecore.md) | DWriteCore provides access to all current DirectWrite features for text rendering, including a device-independent text layout system, hardware-accelerated text, multi-format text, and wide language support.  |

## Release notes

This section lists limitations and known issues for Project Reunion version 0.5.x.

- **Desktop apps (C# .NET 5 or C++ desktop)**: This release cannot be used in unpackaged desktop apps (C# .NET 5 or C++ desktop). This release is supported for use only in MSIX-packaged desktop apps.
- **UWP apps**: This release is not supported for UWP apps that are used in production environments. To use Project Reunion in UWP apps, you must use a release from the [preview release channel](preview-channel.md). For more information about installing the preview extension, see [Set up your development environment](set-up-your-development-environment.md).
- **WinUI 3**: For a list of WinUI changes in this release, see the [WinUI 3 - Project Reunion 0.5 release notes](../winui/winui3/index.md).
- The [WinUI 3 developer tool limitations](..\winui\winui3\index.md#developer-tools) also apply to any project that uses this release.

## Related topics

- [Preview channel](preview-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)