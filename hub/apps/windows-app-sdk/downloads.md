---
title: Downloads for the Windows App SDK 
description: Downloads for the Windows App SDK, including the VSIX, Installer and MSIX packages 
ms.topic: article
ms.date: 08/30/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Downloads for the Windows App SDK

This page provides download links to the various releases of the [Windows App SDK](index.md).

Depending on your development scenario, you may require the following:

- **VSIX:** The Windows App SDK provides a unified set of APIs, project templates, and other tools for building Windows apps, which is available as a Visual Studio extension (VSIX). You can choose from three versions of the extension: stable, preview, and experimental. For setup instructions, see [Install tools for the Windows App SDK](set-up-your-development-environment.md). 
- **Nuget Package:** If you have an existing project in which you want to use the Windows App SDK, you can install the latest version of the Windows App SDK NuGet package in your project directly from Visual Studio. For setup instructions, see [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md).
- **Installer and MSIX packages:** Unpackaged apps that use the Windows App SDK can use the standalone .exe installer or MSIX packages to deploy the Windows App SDK package dependencies with their app. For setup instructions, see [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md).

To learn more, see the [Windows App SDK release channels](release-channels.md) and the release notes provided below for each version.

## Current releases

#### Windows App SDK 1.0

|   | Version | Release date | SDK Downloads (VSIX) | Runtime&nbsp;Downloads (Installer&nbsp;and&nbsp;MSIX&nbsp;packages) |
|---|---|---|---|---|
| **✅** | [1.0.1 (Latest)](stable-channel.md#version-10) | 3/15/2022 <br> [Release&nbsp;notes](stable-channel.md#version-10)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0/1.0.1/vsix-2019-cpp)| [Runtime&nbsp;installer&nbsp;(x64)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x64.exe) <br/> [Runtime&nbsp;installer&nbsp;(x86)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-x86.exe) <br/> [Runtime&nbsp;installer&nbsp;(arm64)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeinstall-1.0.1-arm64.exe) <br/> [Windows App Runtime Redist (Installer and MSIX packages)](https://aka.ms/windowsappsdk/1.0/1.0.1/windowsappruntimeredist-1.0.1.zip) | 
| **✅** | [1.0.0](stable-channel.md#version-10) | 11/16/2021 <br> [Release&nbsp;notes](stable-channel.md#version-10)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2022-cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cs)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/stable-vsix-2019-cpp)| [Download Installer and MSIX packages](https://aka.ms/windowsappsdk/1.0-stable/msix-installer) |
| **❇️** | [1.0.0&nbsp;Preview 3&nbsp;](preview-channel.md#version-10-preview-3-100-preview3) | 10/27/2021 <br> [Release&nbsp;notes](preview-channel.md#version-10-preview-3-100-preview3)&nbsp; | [C#&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2022&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp) <br/> [C#&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp)<br/>[C++&nbsp;Visual&nbsp;Studio&nbsp;2019&nbsp;extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp)| [Download Installer and MSIX packages](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer) |
| **🔄️** | [1.0.0&nbsp;Experimental&nbsp;](experimental-channel.md#version-10-experimental-100-experimental1) | 8/09/2021 <br> [Release&nbsp;notes&nbsp;](experimental-channel.md#version-10-experimental-100-experimental1) | [Download VSIX from Marketplace](https://aka.ms/projectreunion/previewdownload) | [Download Installer and MSIX packages from announcement](https://github.com/microsoft/WindowsAppSDK/releases/tag/1.0.0-experimental1) |

#### Windows App SDK 0.8

|   | Version | Release date | Download extension (VSIX) | Installer and MSIX packages |
|---|---|---|---|---|
| **✅** | [0.8&nbsp;Stable&nbsp;](stable-channel.md#version-08)  | 6/24/2021 <br> [Release&nbsp;notes&nbsp;](stable-channel.md#version-08)&nbsp;  | [Download&nbsp;VSIX&nbsp;from&nbsp;Marketplace](https://aka.ms/projectreunion/vsixdownload) | [Download Installer and MSIX packages from announcement](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0) |
| **❇️** | [0.8&nbsp;Preview&nbsp;](experimental-channel.md#version-08-preview-080-preview)  | 5/27/2021 <br> [Release&nbsp;notes&nbsp;](experimental-channel.md#version-08-preview-080-preview)&nbsp; | [Download&nbsp;VSIXfrom&nbsp;announcement](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8.0-rc) | [Download Installer and MSIX packages from announcement](https://github.com/microsoft/WindowsAppSDK/releases/tag/v0.8-preview) |

## Maintenance releases 

|   | Version | Release date |  
|---|---|---|
| **✅** | [0.5 Stable](stable-channel.md#version-05) | 3/29/2021 |


## Out of support releases 

|   | Version | Release date |
|---|---|---|
| **❇️** | [1.0&nbsp;Preview 2&nbsp;](preview-channel.md#version-10-preview-2-100-preview2) <br> [Deprecated] | 10/5/2021 <br> [Release&nbsp;notes&nbsp;](preview-channel.md#version-10-preview-2-100-preview2)&nbsp; | 
| **❇️** | [1.0&nbsp;Preview 1&nbsp;](preview-channel.md#version-10-preview-1-100-preview1) <br> [Deprecated] | 9/17/2021 <br> [Release&nbsp;notes&nbsp;](preview-channel.md#version-10-preview-1-100-preview1)&nbsp; | 
| **✅** | 0.1 Stable | 12/11/2020 |
