---
title: System requirements for Windows app development
description: This article provides information about minimum system requirements for tools required for developing Windows apps.
ms.topic: article
ms.date: 06/24/2022
keywords: windows win32, windows app development, Windows App SDK
ms.author: stwhi
author: whims
ms.localizationpriority: medium
---

# System requirements for Windows app development

To develop apps for Windows 11 and Windows 10, you'll need Visual Studio, the Windows SDK, and the Windows App SDK. Before installing these tools, check the information in this article to make sure your development computer meets the minimum system requirements.

For instructions to install and set up these tools, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

## Visual Studio

Visual Studio is a comprehensive integrated development environment (IDE) that you can use to edit, debug, and build code, and then publish your app.

For the minimum system requirements, see:

- [Visual Studio 2022 system requirements](/visualstudio/releases/2022/system-requirements#visual-studio-2022-system-requirements)
- [Visual Studio 2019 system requirements](/visualstudio/releases/2019/system-requirements#visual-studio-2019-system-requirements)

## Windows SDK

The Windows SDK provides access to all the APIs and development features exposed by the Windows OS. The Windows SDK is required for building Windows apps as well as other types of components such as services and drivers. The latest Windows SDK is installed with Visual Studio 2019 and Visual Studio 2022 by default.

For the minimum system requirements, see [Windows SDK system requirements](https://developer.microsoft.com/windows/downloads/windows-10-sdk/#sysreq).

## Windows App SDK

The [Windows App SDK](index.md) is a set of developer tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 and downlevel to Windows 10, version 1809.

> [!NOTE]
> The Windows App SDK was previously known by the code name **Project Reunion**. Some SDK assets such as the VSIX extension and NuGet packages still use the code name, but these assets will be renamed in a future release. Some areas of the documentation still use **Project Reunion** when referring to an existing asset or a specified earlier release.

The Windows App SDK has these system requirements:

- Windows 10, version 1809 (build 17763) or later

- Visual Studio 2019 version 16.9 or later with the required components (see [Required workloads and components](https://docs.microsoft.com/windows/apps/windows-app-sdk/set-up-your-development-environment?tabs=vs-2022-17-1-a%2Cvs-2022-17-1-b#required-workloads-and-components))

- Windows SDK version 2004 (build 19041) or later, as included with Visual Studio 2019 and Visual Studio 2022 by default

- Building .NET apps also requires .NET 6 or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/))

### Visual Studio support for WinUI 3 tools

You can build, run, and deploy apps built with stable versions of the Windows App SDK on Visual Studio 2019 versions 16.9, 16.10, and 16.11 Preview. You can also use Visual Studio 2022 17.0 Preview 2 and later to build apps with the Windows App SDK v0.8.2 and later. However, in order to take advantage of the latest WinUI 3 tooling features such as Hot Reload, Live Visual Tree, and Live Property Explorer, you need the versions of Visual Studio 2019 with the stable versions of the Windows App SDK as listed in the following table.

|   | Visual Studio 2019 16.9  |Visual Studio 2019 16.10  |  Visual Studio 2019 16.11 Previews | Visual Studio 2022 17.0 Previews |
|---|---|---|---|---|
| **Windows App SDK 0.5** | Tools unavailable | Tools available   |  Tools unavailable   | Tools unavailable   |
| **Windows App SDK 0.8** | Tools unavailable  | Tools unavailable | Tools available starting with Visual Studio 2019 16.11 Preview 3  | Tools available starting with Visual Studio 2022 17.0 Preview 2 (Windows App SDK v0.8.2+ only) |
| **Windows App SDK 1.0 Experimental** | Tools unavailable  | Tools unavailable | Tools available starting with Visual Studio 2019 16.11 Preview 3   | Tools available starting with Visual Studio 2022 17.0 Preview 2   |
| **Windows App SDK 1.0 Preview 2** | Tools unavailable  | Tools unavailable | Tools available starting with Visual Studio 2019 16.11 Preview 3   | Tools available starting with Visual Studio 2022 17.0 Preview 2   |

## Related topics

- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Get started developing apps for Windows desktop](../get-started/index.md)
