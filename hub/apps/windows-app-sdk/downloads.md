---
title: Downloads for the Windows App SDK
description: Downloads for the Windows App SDK and its runtime installer
ms.topic: article
ms.date: 5/5/2022
keywords: windows win32, windows app development, Windows App SDK
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Downloads for the Windows App SDK

## Latest versions

This page provides download links to the releases of the [Windows App SDK](index.md). To get started quickly, download the latest Visual Studio extension (VSIX) for your target programming language, as well as the runtime installer.

### Latest stable

> [!div class="button"]
> [Download 1.0 Visual Studio 2022 extension for C#](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2022-cs)

> [!div class="button"]
> [Download 1.0 Visual Studio 2022 extension for C++](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2022-cpp)

> [!div class="button"]
> [Download 1.0 runtime x64 installer](https://aka.ms/windowsappsdk/1.0/latest/windowsappruntimeinstall-1.0-x64.exe)

### Latest preview

> [!div class="button"]
> [Download 1.1 Preview 3 Visual Studio 2022 extension for C#](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2022-cs)

> [!div class="button"]
> [Download 1.1 Preview 3 Visual Studio 2022 extension for C++](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2022-cpp)

> [!div class="button"]
> [Download 1.1 Preview 3 runtime x64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-x64.exe)

For other Windows App SDK downloads, see below.

## What to download

To develop apps using Windows App SDK, developers need an SDK package on their development machine. Users intending to run apps that depend on Windows App SDK need to have runtime packages on their machine.

### SDK

| Package type  | Description  |
|:------------- |:-------------|
| Visual Studio extension | The Windows App SDK Visual Studio extension (VSIX) provides project and item templates to get started. You can choose from three versions of the extension: stable, preview, and experimental. See [Set up your development environment](/windows/apps/windows-app-sdk/set-up-your-development-environment) for more details on how to install the extension. |
| NuGet | The [**Microsoft.WindowsAppSDK** NuGet package](https://www.nuget.org/packages/Microsoft.WindowsAppSDK/) provides access to APIs provided by the Windows App SDK. The NuGet package is included with the Visual Studio extension project templates. If you have an existing project in which you want to use the Windows App SDK, you can install the latest version of the Windows App SDK NuGet package in your project directly from Visual Studio. For setup instructions, see [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md). |

### Runtime

Unpackaged or sparse-packaged apps that use the Windows App SDK can use the runtime installer (.exe) or MSIX packages to deploy the necessary runtime libraries on the end-user's machine. For setup instructions, see [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md).

| Package type  | Description  |
|:------------- |:-------------|
| Installer | The **WindowsAppRuntimeInstall.exe** installer, is available as a separate download beginning with Windows App SDK 1.0.1. It installs the Windows App SDK Runtime which includes the Framework, Main, Singleton and DDLM packages. |
| Redistributable | The Windows App Runtime Redistributable (**Microsoft.WindowsAppRuntime.Redist**) is a zip file that includes the installer and MSIX packages for all architectures (x64, x86, and ARM64).|

To learn more, see the [Windows App SDK release channels](release-channels.md) and the release notes provided below for each version.

## Current releases

### Windows App SDK 1.1

|   | Version | SDK downloads | Runtime downloads |
|---|---|---|---|
| **✅** | [1.1.0 Preview 3](preview-channel.md#version-11-preview-3-110-preview3) <br> 05/03/2022 <br> [Release notes](preview-channel.md#version-11-preview-3-110-preview3)  | [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2022-cs)<br/>[C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2022-cpp) <br/> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2019-cs)<br/>[C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2019-cpp) | [x64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-x64.exe) <br/> [x86 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-x86.exe) <br/> [ARM64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeredist-1.1.0-preview3.zip) |
| **✅** | [1.1.0 Preview 2](preview-channel.md#version-11-preview-2-110-preview2) <br> 04/19/2022 <br> [Release notes](preview-channel.md#version-11-preview-1-110-preview1)  | [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2022-cs)<br/>[C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2022-cpp) <br/> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2019-cs)<br/>[C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2019-cpp) | [x64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeinstall-1.1.0-preview2-x64.exe) <br/> [x86 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeinstall-1.1.0-preview2-x86.exe) <br/> [ARM64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeinstall-1.1.0-preview2-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeredist-1.1.0-preview2.zip) |
| **✅** | [1.1.0 Preview 1](preview-channel.md#version-11-preview-1-110-preview1) <br> 03/29/2022 <br> [Release notes](preview-channel.md#version-11-preview-1-110-preview1)  | No new Visual Studio extensions.   | [x64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x64.exe) <br/> [x86 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x86.exe) <br/> [ARM64 installer](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeredist-1.1.0-preview1.zip) |

### Windows App SDK 1.0

|   | Version | SDK downloads | Runtime downloads |
|---|---|---|---|
| **✅** | [1.0.3 (Latest)](stable-channel.md#version-103) <br> 04/18/2022 <br> [Release notes](stable-channel.md#version-103)  | No new Visual Studio extensions. | [x64 installer](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-x64.exe) <br/> [x86 installer](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-x86.exe) <br/> [ARM64 installer](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeredist-1.0.3.zip) |
| **✅** | [1.0.2](stable-channel.md#version-102) <br> 04/05/2022 <br> [Release notes](stable-channel.md#version-102)  | [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2022-cs)<br/>[C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2022-cpp) <br/> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2019-cs)<br/>[C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2019-cpp)| [x64 installer](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeinstall-1.0.2-x64.exe) <br/> [x86 installer](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeinstall-1.0.2-x86.exe) <br/> [ARM64 installer](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeinstall-1.0.2-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeredist-1.0.2.zip) |
| **✅** | [1.0.1](stable-channel.md#version-101) <br> 03/15/2022 <br> [Release notes](stable-channel.md#version-101)  | [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cs)<br/>[C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cpp) <br/> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cs)<br/>[C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cpp)| [x64 installer](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x64.exe) <br/> [x86 installer](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x86.exe) <br/> [ARM64 installer](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeredist-1.0.1.zip) |
| **✅** | [1.0.0](stable-channel.md#version-10) <br> 11/16/2021 <br> [Release notes](stable-channel.md#version-10)  | [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cs)<br/>[C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cpp) <br/> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cs)<br/>[C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cpp)| [Redistributable](https://aka.ms/windowsappsdk/1.0-stable/msix-installer) |
| **❇️** | [1.0.0 Preview 3](preview-channel.md#version-10-preview-3-100-preview3) <br> 10/27/2021 <br> [Release notes](preview-channel.md#version-10-preview-3-100-preview3)  | [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)<br/>[C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp) <br/> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp)<br/>[C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp)| [Redistributable](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer) |
| **🔄️** | [1.0.0 Experimental](experimental-channel.md#version-10-experimental-100-experimental1) <br> 08/09/2021 <br> [Release notes](experimental-channel.md#version-10-experimental-100-experimental1) | [Visual Studio extension](https://aka.ms/projectreunion/previewdownload) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/1.0.0-experimental1) |

### Windows App SDK 0.8

|   | Version | SDK downloads | Runtime downloads |
|---|---|---|---|
| **✅** | [0.8 Stable](stable-channel.md#version-08) <br> 06/24/2021 <br> [Release notes](stable-channel.md#version-08)   | [Visual Studio extension](https://aka.ms/projectreunion/vsixdownload) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0) |
| **❇️** | [0.8 Preview](experimental-channel.md#version-08-preview-080-preview) <br> 05/27/2021 <br> [Release notes](experimental-channel.md#version-08-preview-080-preview)  | [Visual Studio extension](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0-rc) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8-preview) |

## Maintenance releases

|   | Version |
|---|---|
| **✅** | [0.5 Stable](stable-channel.md#version-05) <br> 03/29/2021 |

## Out of support releases

|   | Version |
|---|---|
| **❇️** | [1.0 Preview 2](preview-channel.md#version-10-preview-2-100-preview2) <br> [Deprecated] <br> 10/5/2021 <br> [Release notes](preview-channel.md#version-10-preview-2-100-preview2)  |
| **❇️** | [1.0 Preview 1](preview-channel.md#version-10-preview-1-100-preview1) <br> [Deprecated] <br> 09/17/2021 <br> [Release notes](preview-channel.md#version-10-preview-1-100-preview1)  |
| **✅** | 0.1 Stable <br> 12/11/2020 |
