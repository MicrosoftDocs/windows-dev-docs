---
title: System requirements for Windows app development
description: The minimum system requirements for the tools required to develop Windows apps.
ms.topic: article
ms.date: 10/12/2022
keywords: windows win32, windows app development, Windows App SDK
ms.localizationpriority: medium
---

# System requirements for Windows app development

To develop apps for Windows 10 and 11, you'll need *Visual Studio*, the *Windows SDK*, and the *Windows App SDK*. Before installing these tools, make sure your development computer meets the minimum system requirements.

[Install the tools for the Windows App SDK](./set-up-your-development-environment.md) to get started.

## Visual Studio

Visual Studio is a comprehensive *Integrated Development Environment (IDE)* that's used to *edit*, *debug*, *build*, and *publish* apps.

For the minimum system requirements, see:

- [Visual Studio 2022 system requirements](/visualstudio/releases/2022/system-requirements#visual-studio-2022-system-requirements)
- [Visual Studio 2019 system requirements](/visualstudio/releases/2019/system-requirements#visual-studio-2019-system-requirements)

## Windows SDK

The Windows SDK provides access to all of the APIs and development features exposed by the Windows OS. The Windows SDK is required for building Windows apps as well as other types of components (such as services and drivers). The latest Windows SDK is installed with *Visual Studio 2019* and *Visual Studio 2022* by default.

For the minimum system requirements, see [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/).

## Windows App SDK

The [Windows App SDK](./index.md) is a set of developer tools that represent the next evolution in the Windows app development platform. It provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 (and it's backward-compatible for Windows 10, version 1809).

> [!NOTE]
> The Windows App SDK was previously known by the code name *Project Reunion*. Some SDK assets (such as the VSIX extension and certain NuGet packages) still use this name, but these assets will be renamed in a future release. Some documentation still uses *Project Reunion* when referring to an existing asset or a specified earlier release.

The Windows App SDK has the following minimum system requirements:

- Windows 10, version 1809 (build 17763) or later.
- Visual Studio 2022, version 17.0 or later, with the [required workloads and components](/windows/apps/windows-app-sdk/set-up-your-development-environment#required-workloads-and-components).
- Visual Studio 2019, version 16.9 or later, with the [required workloads and components](./set-up-your-development-environment.md#required-workloads-and-components).
- Windows SDK, version 2004 (build 19041) or later (included with Visual Studio 2019 and 2022 by default).
- If you plan to build .NET apps, you'll also need .NET 6 or later (see [Download .NET](https://dotnet.microsoft.com/en-us/download)).

## Visual Studio support for WinUI 3 tools

You can build, run, and deploy apps built with stable versions of the Windows App SDK on Visual Studio 2019 versions *16.9*, *16.10*, and *16.11 Preview*. You can also use Visual Studio 2022 *17.0 Preview 2 and later* to build apps with the Windows App SDK *v0.8.2 and later*. However, in order to take advantage of the latest WinUI 3 tooling features such as *hot reload*, *live visual tree*, and *live property explorer*, you'll need the version of Visual Studio 2019 with a stable version of the Windows App SDK as shown in the following table:

| | Visual Studio 2019 16.9 | Visual Studio 2019 16.10 | Visual Studio 2019 16.11 Preview | Visual Studio 2022 17.0 Preview |
|-|-|-|-|-|
| **Windows App SDK 0.5** | Tools unavailable | Tools available | Tools unavailable | Tools unavailable |
| **Windows App SDK 0.8** | Tools unavailable | Tools unavailable | Tools available (starting with Visual Studio 2019 16.11 Preview). | Tools available (starting with Visual Studio 2022 17.0 Preview 2). *Requires Windows App SDK v0.8.2 or later*. |
| **Windows App SDK 1.0 Experimental** | Tools unavailable | Tools unavailable | Tools available (starting with Visual Studio 2019 16.11 Preview 3). | Tools available (starting with Visual Studio 2022 17.0 Preview 2). |
| **Windows App SDK 1.0 Preview 2** | Tools unavailable | Tools unavailable | Tools available (starting with Visual Studio 2019 16.11 Preview 3). | Tools available (starting with Visual Studio 2022 17.0 Preview 2). |

## See Also

- [Windows App SDK and supported Windows releases](support.md)
