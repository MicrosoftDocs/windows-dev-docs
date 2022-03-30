---
title: Downloads for the Windows App SDK 
description: Downloads for the Windows App SDK, including the VSIX, Installer and MSIX packages 
ms.topic: article
ms.date: 3/29/2022
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Downloads for the Windows App SDK

This page provides download links to the various releases of the [Windows App SDK](index.md). To get started quickly, download the latest Visual Studio extensions (VSIX) and installer below:

**Latest Stable**

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.0 Visual Studio 2022 Extension (C#)](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cs) 

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.0 Visual Studio 2022 Extension (C++)](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cpp)

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.0 Installer (x64)](https://aka.ms/windowsappsdk/1.0/latest/windowsappruntimeinstall-1.0-x64.exe) 

**Latest Preview**

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.1 Preview 1 Installer (x64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x64.exe) 

For all Windows App SDK downloads, refer to the tables and links below. Depending on your development scenario, you may require the following:

**SDK downloads**

| Tool&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | Description | 
|:------------- |:-------------|
| Visual Studio Extension | The Windows App SDK Visual Studio Extension (VSIX) provides project and item templates to get started. You can choose from three versions of the extension: stable, preview, and experimental. See [Set up your development environment](/windows/apps/windows-app-sdk/set-up-your-development-environment) for more details on how to install the extension. |
| NuGet Package | The [**Microsoft.WindowsAppSDK** NuGet package](https://www.nuget.org/packages/Microsoft.WindowsAppSDK/) provides access to APIs provided by the Windows App SDK. The NuGet package is included with the Visual Studio Extension project templates. If you have an existing project in which you want to use the Windows App SDK, you can install the latest version of the Windows App SDK NuGet package in your project directly from Visual Studio. For setup instructions, see [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md).  |

**Runtime downloads**

Unpackaged or sparse-packaged apps that use the Windows App SDK can use the standalone .exe Runtime installer or MSIX packages to deploy the Windows App SDK package dependencies with their app. For setup instructions, see [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md).

| Tool&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | Description | 
|:------------- |:-------------|
| Installer | The standalone .exe installer, **WindowsAppRuntimeInstall.exe**, is available as a separate download beginning with Windows App SDK 1.0.1. It installs the Windows App SDK Runtime which includes the Framework, Main, Singleton and DDLM packages.  |
| Redistributable |  The Windows App Runtime Redistributable (**Microsoft.WindowsAppRuntime.Redist**) is a zip file that includes the installer and MSIX packages for all architectures (x64, x86, and ARM64).|

To learn more, see the [Windows App SDK release channels](release-channels.md) and the release notes provided below for each version.

## Current releases

#### Windows App SDK 1.1
|   | Version | SDK downloads | Runtime&nbsp;downloads |
|---|---|---|---|
| **✅** | [1.1.0&nbsp;Preview&nbsp;1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](preview-channel.md#version-11-preview-1-110-preview1) <br> 3/29/2022 <br> [Release&nbsp;notes](preview-channel.md#version-11-preview-1-110-preview1)&nbsp; | No new Visual Studio extensions.&nbsp;&nbsp; | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeredist-1.1.0-preview1.zip) | 

#### Windows App SDK 1.0

|   | Version | SDK downloads | Runtime&nbsp;downloads |
|---|---|---|---|
| **✅** | [1.0.1 (Latest)](stable-channel.md#version-10) <br> 3/15/2022 <br> [Release&nbsp;notes](stable-channel.md#version-10)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cpp)| [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeredist-1.0.1.zip) | 
| **✅** | [1.0.0](stable-channel.md#version-10) <br> 11/16/2021 <br> [Release&nbsp;notes](stable-channel.md#version-10)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cpp)| [Redistributable](https://aka.ms/windowsappsdk/1.0-stable/msix-installer) |
| **❇️** | [1.0.0&nbsp;Preview 3&nbsp;](preview-channel.md#version-10-preview-3-100-preview3) <br> 10/27/2021 <br> [Release&nbsp;notes](preview-channel.md#version-10-preview-3-100-preview3)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp)| [Redistributable](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer) |
| **🔄️** | [1.0.0&nbsp;Experimental&nbsp;](experimental-channel.md#version-10-experimental-100-experimental1) <br> 8/09/2021 <br> [Release&nbsp;notes&nbsp;](experimental-channel.md#version-10-experimental-100-experimental1) | [Visual&nbsp;Studio&nbsp;extension&nbsp;](https://aka.ms/projectreunion/previewdownload) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/1.0.0-experimental1) |

#### Windows App SDK 0.8

|   | Version | SDK Downloads | Runtime Downloads |
|---|---|---|---|
| **✅** | [0.8&nbsp;Stable&nbsp;](stable-channel.md#version-08) <br> 6/24/2021 <br> [Release&nbsp;notes&nbsp;](stable-channel.md#version-08)&nbsp;  | [Visual&nbsp;Studio&nbsp;extension&nbsp;](https://aka.ms/projectreunion/vsixdownload) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0) |
| **❇️** | [0.8&nbsp;Preview&nbsp;](experimental-channel.md#version-08-preview-080-preview) <br> 5/27/2021 <br> [Release&nbsp;notes&nbsp;](experimental-channel.md#version-08-preview-080-preview)&nbsp; | [Visual&nbsp;Studio&nbsp;extension&nbsp;](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0-rc) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8-preview) |

## Maintenance releases 

|   | Version |  
|---|---|
| **✅** | [0.5 Stable](stable-channel.md#version-05) <br> 3/29/2021 |


## Out of support releases 

|   | Version |
|---|---|
| **❇️** | [1.0&nbsp;Preview 2&nbsp;](preview-channel.md#version-10-preview-2-100-preview2) <br> [Deprecated] <br> 10/5/2021 <br> [Release&nbsp;notes&nbsp;](preview-channel.md#version-10-preview-2-100-preview2)&nbsp; | 
| **❇️** | [1.0&nbsp;Preview 1&nbsp;](preview-channel.md#version-10-preview-1-100-preview1) <br> [Deprecated] <br> 9/17/2021 <br> [Release&nbsp;notes&nbsp;](preview-channel.md#version-10-preview-1-100-preview1)&nbsp; | 
| **✅** | 0.1 Stable <br> 12/11/2020 |
