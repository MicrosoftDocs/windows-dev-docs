---
title: Set up your development environment
description: This article provides instructions for installing the Windows App SDK extension for Visual Studio 2019 on your development computer.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Set up your development environment

Follow these instructions to set up your development environment so you can start creating apps for Windows 10.

## System requirements

To develop Windows apps, you'll need to install and set up Visual Studio, the Windows 10 SDK, and the Windows App SDK on a development computer. These tools have the following system requirements.

#### [Visual Studio 2019](#tab/visual-studio-2019)

Visual Studio is a comprehensive integrated development environment (IDE) that you can use to edit, debug, and build code, and then publish your app.

See [this page](/visualstudio/releases/2019/system-requirements) for Visual Studio system requirements.

#### [Windows 10 SDK](#tab/windows-sdk)

The Windows 10 SDK provides access to all the APIs and development features exposed by the Windows OS. The Windows 10 SDK is required for building Windows apps as well as other types of components such as services and drivers. The latest Windows 10 SDK is installed with Visual Studio 2019 by default.

See [this page](https://developer.microsoft.com/windows/downloads/windows-10-sdk/#sysreq) for Windows SDK system requirements.

#### [Windows App SDK](#tab/windows-app-sdk)

The [Windows App SDK](index.md) is a set of developer tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on a broad set of target Windows 10 OS versions.

> [!NOTE]
> The Windows App SDK was previously known by the code name **Project Reunion**. Some SDK assets such as the VSIX extension and NuGet packages still use the code name, but these assets will be renamed in a future release. Some areas of the documentation still use **Project Reunion** when referring to an existing asset or a specified earlier release.

The Windows App SDK has these system requirements:

- Windows 10, version 1809 (build 17763) or later.

- Visual Studio 2019 version 16.9 or later with the following workloads and components:
  - **Universal Windows Platform development**
  - **.NET Desktop Development** (needed even if you're only building C++ Win32 apps)
  - **Desktop development with C++** (needed even if you're only building .NET apps)

- Windows 10 SDK version 2004 (build 19041). This is installed with Visual Studio 2019 by default.

- Building .NET apps also requires:
  - .NET 5 SDK version 5.0.300 or later if you're using Visual Studio 2019 version 16.10
  - .NET 5 SDK version 5.0.204 if you're using Visual Studio 2019 version 16.9

##### Visual Studio support for WinUI 3 tools

You can build, run, and deploy apps built with stable versions of the Windows App SDK on Visual Studio 2019 versions 16.9, 16.10, and 16.11 Preview. However, in order to take advantage of the latest WinUI 3 tooling features such as Hot Reload, Live Visual Tree, and Live Property Explorer, you need the versions of Visual Studio 2019 with the stable versions of the Windows App SDK as listed in the following table.

|   | Visual Studio 2019 16.9  |Visual Studio 2019 16.10  |  Visual Studio 2019 16.11 Previews |
|---|---|---|---|
| **Windows App SDK 0.5** | Tools unavailable | Tools available   |  Tools unavailable   |
| **Windows App SDK 0.8** | Tools unavailable  | Tools unavailable | Tools available starting with Visual Studio 2019 16.11 Preview 3  |

---

## 1. Install Visual Studio

Use the following link to install Visual Studio 2019. You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise. Whichever version you choose, the latest Windows 10 SDK will also be installed by default.

> [!div class="button"]
> [Download Visual Studio 2019](https://developer.microsoft.com/windows/downloads)

### Required workloads and components

Make sure these workloads and components are installed with Visual Studio. These are all selected by default.

- On the **Workloads** tab of the installation dialog, these workloads are required:
  - **Universal Windows Platform development**
  - **Desktop development with C++**
  - **.NET Desktop Development**

- On the **Individual components** tab of the installation dialog, **Windows 10 SDK (10.0.19041.0)** is required in the **SDKs, libraries, and frameworks** section.

- In the **Installation details** pane of the installation dialog, **C++ (v142) Universal Windows Platform tools** is required in the **Universal Windows Platform development** section.

## 2. Enable NuGet Package source

Make sure your system has a NuGet package source enabled for the official NuGet service index at `https://api.nuget.org/v3/index.json`. 

 1. In Visual Studio, select **Tools** -> **NuGet Package Manager** -> **Package Manager Settings** to open the **Options** dialog.
 2. In the left pane of the **Options** dialog, select the **Package Sources** tab, and make sure there is a package source for **nuget.org** that points to `https://api.nuget.org/v3/index.json` as the source URL. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior).

## 3. Install the Windows App SDK Extension for Visual Studio

There are currently two [release channels](release-channels.md) of the the Windows App SDK you can choose from: the stable channel and experimental channel.

> [!NOTE]
> If you previously installed the [WinUI 3 Preview extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=Microsoft-WinUI.WinUIProjectTemplates), uninstall the extension. For more information about how to uninstall an extension, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

### Install the stable release

To develop desktop (C#/.NET 5 or C++/WinRT) apps that can be used in production environments, install the latest extension from the [stable release channel](stable-channel.md). Choose one of these options to install:

- In Visual Studio 2019, click **Extensions** > **Manage Extensions**, search for **Project Reunion**, and install the latest extension.
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace.

    > [!div class="button"]
    > [Download latest stable release](https://aka.ms/projectreunion/vsixdownload)

    ![Screenshot of the Windows App SDK extension being installed](images/reunion-extension-install.png)

### Install the experimental release

To develop desktop (C#/.NET 5 or C++/WinRT) apps or UWP apps that use the latest experimental features, install the latest extension from the [experimental release channel](experimental-channel.md). This version of the Windows App SDK cannot be used by apps in production environments. Choose one of these options to install:

- In Visual Studio 2019, click **Extensions** > **Manage Extensions**, search for **Project Reunion (Preview)**, and install the latest extension.
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace.

    > [!div class="button"]
    > [Download latest experimental release](https://aka.ms/projectreunion/previewdownload)

## 4. Enable your device for development

Before you can deploy apps to your development computer, you have to enable it for development. For detailed instructions, see [this article](../get-started/enable-your-device-for-development.md).

## 5. Register as an app developer

You can start developing apps now, but you need a developer account to submit your apps to the Microsoft Store. To get a developer account, see [this article](../get-started/sign-up.md) page.

## Other tools and downloads

- If you want to customize your device and install other features or packages, check out the [developer setup scripts](https://github.com/Microsoft/windows-dev-box-setup-scripts).
- For more tools and downloads related to Windows app development, see [this page](https://developer.microsoft.com/windows/downloads).

## Related topics

- [Build desktop Windows apps with the Windows App SDK](index.md)
- [Release channels](release-channels.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)