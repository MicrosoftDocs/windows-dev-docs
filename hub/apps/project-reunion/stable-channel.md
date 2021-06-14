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

The following releases of the stable channel are currently available:

- [Version 0.8 RC (release candidate)](#version-08-rc)
- [Version 0.5](#version-05)

## Version 0.8 RC

This is a release candidate of the upcoming release of Project Reunion 0.8. This build is available for developers to try out and provide feedback before the final release. 

For more information and download details, see the [GitHub announcement](https://aka.ms/projectreunion/0.8-release-candidate).

## Version 0.5

The latest available servicing release is [0.5.7](https://github.com/microsoft/ProjectReunion/discussions/820).

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/vsixdownload)

#### New features and updates

This release supports all [stable channel features](release-channels.md#features-by-release-channel). 

#### Known issues and limitations

This release has the following limitations and known issues:

- **Desktop apps (C# .NET 5 or C++ desktop)**: This release is supported for use only in desktop apps (C++ or C# with .NET 5) that are packaged using MSIX. To use Project Reunion in unpackaged desktop apps, you must use the [preview channel](preview-channel.md).
- **UWP apps**: This release is not supported for UWP apps that are used in production environments. To use Project Reunion in UWP apps, you must use a release from the [preview release channel](preview-channel.md). For more information about installing the preview extension, see [Set up your development environment](set-up-your-development-environment.md).
- **WinUI 3**: For a list of WinUI changes in this release, see the [WinUI 3 - Project Reunion 0.5 release notes](../winui/winui3/index.md).
- The [WinUI 3 developer tool limitations](..\winui\winui3\index.md#developer-tools) also apply to any project that uses this release.

## Related topics

- [Preview channel](preview-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)
