---
title: Downloads for the Windows App SDK 
description: Downloads for the Windows App SDK, including the VSIX, installer, and MSIX packages 
ms.topic: article
ms.date: 05/23/2022
keywords: windows win32, windows app development, Windows App SDK 
ms.author: stwhi
author: whims
ms.localizationpriority: medium
---

# Downloads for the Windows App SDK

This page provides download links to the various releases of the [Windows App SDK](index.md). To get started quickly, download the latest Visual Studio extensions (VSIX) and installer below:

**Latest Stable 1.1**

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.1 Visual Studio 2022 Extension (C#)](https://aka.ms/windowsappsdk/1.1/latest/WindowsAppSDK.Cs.Extension.Dev17.Standalone.vsix) 

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.1 Visual Studio 2022 Extension (C++)](https://aka.ms/windowsappsdk/1.1/latest/WindowsAppSDK.Cpp.Extension.Dev17.Standalone.vsix)

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.1 Installer (x64)](https://aka.ms/windowsappsdk/1.1/latest/windowsappruntimeinstall-x64.exe) 

**Latest Stable 1.0**

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.0 Visual Studio 2022 Extension (C#)](https://aka.ms/windowsappsdk/1.0/1.0.4/vsix-2022-cs) 

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.0 Visual Studio 2022 Extension (C++)](https://aka.ms/windowsappsdk/1.0/1.0.4/vsix-2022-cpp)

> [!div class="button" style="text-align: left;" width="150px;"] 
> [Download 1.0 Installer (x64)](https://aka.ms/windowsappsdk/1.0/latest/windowsappruntimeinstall-1.0-x64.exe) 

For all Windows App SDK downloads, refer to the tables and links below. Depending on your development scenario, you may require the following:

**SDK downloads**

| Tool&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | Description | 
|:------------- |:-------------|
| Visual Studio extension | The Windows App SDK Visual Studio extension (VSIX) provides project and item templates to get started. You can choose from three versions of the extension: stable, preview, and experimental. See [Set up your development environment](/windows/apps/windows-app-sdk/set-up-your-development-environment) for more details on how to install the extension. |
| NuGet package | The [**Microsoft.WindowsAppSDK** NuGet package](https://www.nuget.org/packages/Microsoft.WindowsAppSDK/) provides access to APIs provided by the Windows App SDK. The NuGet package is included with the Visual Studio extension project templates. If you have an existing project in which you want to use the Windows App SDK, you can install the latest version of the Windows App SDK NuGet package in your project directly from Visual Studio. For setup instructions, see [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md).  |

**Runtime downloads**

Unpackaged or sparse-packaged apps that use the Windows App SDK can use the standalone .exe Runtime installer or MSIX packages to deploy the Windows App SDK package dependencies with their app. For setup instructions, see [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md).

| Tool&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | Description | 
|:------------- |:-------------|
| Installer | The standalone .exe installer, **WindowsAppRuntimeInstall.exe**, is available as a separate download beginning with Windows App SDK 1.0.1. It installs the Windows App SDK Runtime which includes the Framework, Main, Singleton and DDLM packages.  |
| Redistributable |  The Windows App Runtime Redistributable (**Microsoft.WindowsAppRuntime.Redist**) is a zip file that includes the installer and MSIX packages for all architectures (x64, x86, and ARM64).|

To learn more, see the [Windows App SDK release channels](release-channels.md) and the release notes provided below for each version.

## Current releases

#### Windows App SDK 1.2
|   | Version | SDK downloads | Runtime&nbsp;downloads |
|---|---|---|---|
| **🔄️** | [1.2&nbsp;Experimental](/windows/apps/windows-app-sdk/experimental-channel#version-12-experimental-120-experimental1)<br>08/15/2021<br>[Release&nbsp;notes&nbsp;](experimental-channel.md#version-12-experimental-120-experimental1) | No new Visual Studio extensions. | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.2/1.2.220727.1-experimental1/windowsappruntimeinstall-x64.exe)<br>[Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.2/1.2.220727.1-experimental1/windowsappruntimeinstall-x86.exe)<br>[Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.2/1.2.220727.1-experimental1/windowsappruntimeinstall-arm64.exe)<br>[Redistributable](https://aka.ms/windowsappsdk/1.2/1.2.220727.1-experimental1/Microsoft.WindowsAppRuntime.Redist.1.2.220727.1-experimental1.zip) |


#### Windows App SDK 1.1
|   | Version | SDK downloads | Runtime&nbsp;downloads |
|---|---|---|---|
| **✅** | [1.1.4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](stable-channel.md) <br> 08/11/2022 <br> [Release&nbsp;notes](stable-channel.md#version-114)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.4/WindowsAppSDK.Cs.Extension.Dev17.Standalone.vsix)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.4/WindowsAppSDK.Cpp.Extension.Dev17.Standalone.vsix) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.4/WindowsAppSDK.Cs.Extension.Dev16.vsix)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.4/WindowsAppSDK.Cpp.Extension.Dev16.vsix) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.4/windowsappruntimeinstall-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.4/windowsappruntimeinstall-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.4/windowsappruntimeinstall-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.4/Microsoft.WindowsAppRuntime.Redist.1.1.4.zip) |
| **✅** | [1.1.3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](stable-channel.md) <br> 07/20/2022 <br> [Release&nbsp;notes](stable-channel.md#version-113)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.3/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.3/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.3/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.3/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.3/windowsappruntimeinstall-1.1.3-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.3/windowsappruntimeinstall-1.1.3-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.3/windowsappruntimeinstall-1.1.3-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.3/windowsappruntimeredist-1.1.3.zip) | 
| **✅** | [1.1.2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](stable-channel.md) <br> 06/28/2022 <br> [Release&nbsp;notes](stable-channel.md#version-112)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.2/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.2/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.2/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.2/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.2/windowsappruntimeinstall-1.1.2-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.2/windowsappruntimeinstall-1.1.2-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.2/windowsappruntimeinstall-1.1.2-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.2/windowsappruntimeredist-1.1.2.zip) | 
| **✅** | [1.1.1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](stable-channel.md) <br> 06/14/2022 <br> [Release&nbsp;notes](stable-channel.md#version-111)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.1/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.1/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.1/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.1/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.1/windowsappruntimeinstall-1.1.1-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.1/windowsappruntimeinstall-1.1.1-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.1/windowsappruntimeinstall-1.1.1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.1/windowsappruntimeredist-1.1.1.zip) | 
| **✅** | [1.1.0&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](stable-channel.md) <br> 05/24/2022 <br> [Release&nbsp;notes](stable-channel.md)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.0/windowsappruntimeinstall-1.1.0-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.0/windowsappruntimeinstall-1.1.0-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.0/windowsappruntimeinstall-1.1.0-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.1/windowsappruntimeredist-1.1.1.zip) | 
| **❇️** | [1.1.0&nbsp;Preview&nbsp;3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](preview-channel.md#version-11-preview-3-110-preview3) <br> 05/03/2022 <br> [Release&nbsp;notes](preview-channel.md#version-11-preview-3-110-preview3)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeinstall-1.1.0-preview3-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview3/windowsappruntimeredist-1.1.0-preview3.zip) | 
| **❇️** | [1.1.0&nbsp;Preview&nbsp;2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](preview-channel.md#version-11-preview-2-110-preview2) <br> 04/19/2022 <br> [Release&nbsp;notes](preview-channel.md#version-11-preview-1-110-preview1)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeinstall-1.1.0-preview2-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeinstall-1.1.0-preview2-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeinstall-1.1.0-preview2-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview2/windowsappruntimeredist-1.1.0-preview2.zip) | 
| **❇️**| [1.1.0&nbsp;Preview&nbsp;1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;](preview-channel.md#version-11-preview-1-110-preview1) <br> 03/29/2022 <br> [Release&nbsp;notes](preview-channel.md#version-11-preview-1-110-preview1)&nbsp; | No new Visual Studio extensions.&nbsp;&nbsp; | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeinstall-1.1.0-preview1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.1/1.1.0-preview1/windowsappruntimeredist-1.1.0-preview1.zip) | 

#### Windows App SDK 1.0

|   | Version | SDK downloads | Runtime&nbsp;downloads |
|---|---|---|---|
| **✅** | [1.0.4](stable-channel.md#version-104) <br> 06/14/2022 <br> [Release&nbsp;notes](stable-channel.md#version-104)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.4/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.4/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.4/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.4/vsix-2019-cpp) | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.0/1.0.4/windowsappruntimeinstall-1.0.4-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.0/1.0.4/windowsappruntimeinstall-1.0.4-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.0/1.0.4/windowsappruntimeinstall-1.0.4-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.4/windowsappruntimeredist-1.0.4.zip) | 
| **✅** | [1.0.3](stable-channel.md#version-103) <br> 04/18/2022 <br> [Release&nbsp;notes](stable-channel.md#version-103)&nbsp; | No new Visual Studio extensions. | [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeredist-1.0.3.zip) | 
| **✅** | [1.0.2](stable-channel.md#version-102) <br> 04/05/2022 <br> [Release&nbsp;notes](stable-channel.md#version-102)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.2/vsix-2019-cpp)| [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeinstall-1.0.2-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeinstall-1.0.2-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeinstall-1.0.2-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.2/windowsappruntimeredist-1.0.2.zip) | 
| **✅** | [1.0.1](stable-channel.md#version-101) <br> 03/15/2022 <br> [Release&nbsp;notes](stable-channel.md#version-101)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cpp)| [Installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x64.exe) <br/> [Installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x86.exe) <br/> [Installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-arm64.exe) <br/> [Redistributable](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeredist-1.0.1.zip) | 
| **✅** | [1.0.0](stable-channel.md#version-10) <br> 11/16/2021 <br> [Release&nbsp;notes](stable-channel.md#version-10)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cpp)| [Redistributable](https://aka.ms/windowsappsdk/1.0-stable/msix-installer) |
| **❇️** | [1.0.0&nbsp;Preview 3&nbsp;](preview-channel.md#version-10-preview-3-100-preview3) <br> 10/27/2021 <br> [Release&nbsp;notes](preview-channel.md#version-10-preview-3-100-preview3)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp)| [Redistributable](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer) |
| **🔄️** | [1.0.0&nbsp;Experimental&nbsp;](experimental-channel.md#version-10-experimental-100-experimental1) <br> 08/09/2021 <br> [Release&nbsp;notes&nbsp;](experimental-channel.md#version-10-experimental-100-experimental1) | [Visual&nbsp;Studio&nbsp;extension&nbsp;](https://aka.ms/projectreunion/previewdownload) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/1.0.0-experimental1) |

#### Windows App SDK 0.8

|   | Version | SDK downloads | Runtime downloads |
|---|---|---|---|
| **✅** | [0.8&nbsp;Stable&nbsp;](stable-channel.md#version-08) <br> 06/24/2021 <br> [Release&nbsp;notes&nbsp;](stable-channel.md#version-08)&nbsp;  | [Visual&nbsp;Studio&nbsp;extension&nbsp;](https://aka.ms/projectreunion/vsixdownload) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0) |
| **❇️** | [0.8&nbsp;Preview&nbsp;](experimental-channel.md#version-08-preview-080-preview) <br> 05/27/2021 <br> [Release&nbsp;notes&nbsp;](experimental-channel.md#version-08-preview-080-preview)&nbsp; | [Visual&nbsp;Studio&nbsp;extension&nbsp;](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0-rc) | [Redistributable](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8-preview) |

## Maintenance releases 

|   | Version |  
|---|---|
| **✅** | [0.5 Stable](stable-channel.md#version-05) <br> 03/29/2021 |


## Out of support releases 

|   | Version |
|---|---|
| **❇️** | [1.0&nbsp;Preview 2&nbsp;](preview-channel.md#version-10-preview-2-100-preview2) <br> [Deprecated] <br> 10/5/2021 <br> [Release&nbsp;notes&nbsp;](preview-channel.md#version-10-preview-2-100-preview2)&nbsp; | 
| **❇️** | [1.0&nbsp;Preview 1&nbsp;](preview-channel.md#version-10-preview-1-100-preview1) <br> [Deprecated] <br> 09/17/2021 <br> [Release&nbsp;notes&nbsp;](preview-channel.md#version-10-preview-1-100-preview1)&nbsp; | 
| **✅** | 0.1 Stable <br> 12/11/2020 |
