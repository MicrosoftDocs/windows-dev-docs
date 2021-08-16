---
title: Install tools for Windows app development
description: This article provides instructions for setting up your development computer for Windows app development.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Install tools for Windows app development

To develop apps for Windows 11 and Windows 10, you'll need to configure your development computer with the required development tools.

## 1. Check system requirements

To develop apps, you'll need Visual Studio, the Windows SDK, and the Windows App SDK. For a list of the minimum system requirements for each of these tools, see [System requirements for Windows app development](system-requirements.md).

## 2. Install Visual Studio

Use the following links to install Visual Studio 2019 or Visual Studio 2022 (Preview). You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise. Whichever version you choose, the latest Windows SDK will also be installed by default.

> [!div class="button"]
> [Download Visual Studio 2019](/visualstudio/releases/2019/release-notes)

> [!div class="button"]
> [Download Visual Studio 2022 (Preview)](/visualstudio/releases/2022/release-notes-preview)

### Required workloads and components

Make sure these workloads and components are installed with Visual Studio. These are all selected by default.

- On the **Workloads** tab of the installation dialog, these workloads are required:
  - **Universal Windows Platform development**
  - **Desktop development with C++**
  - **.NET Desktop Development**

- On the **Individual components** tab of the installation dialog, **Windows 10 SDK (10.0.19041.0)** is required in the **SDKs, libraries, and frameworks** section.

- In the **Installation details** pane of the installation dialog, **C++ (v142) Universal Windows Platform tools** is required in the **Universal Windows Platform development** section.

## 3. Enable NuGet Package source

Make sure your system has a NuGet package source enabled for the official NuGet service index at `https://api.nuget.org/v3/index.json`.

 1. In Visual Studio, select **Tools** -> **NuGet Package Manager** -> **Package Manager Settings** to open the **Options** dialog.
 2. In the left pane of the **Options** dialog, select the **Package Sources** tab, and make sure there is a package source for **nuget.org** that points to `https://api.nuget.org/v3/index.json` as the source URL. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior).

## 4. Install the Windows App SDK extension for Visual Studio

The [Windows App SDK](index.md) provides a unified set of APIs, project templates, and other tools for building Windows apps. This SDK is available as a Visual Studio extension (VSIX). You can choose from two versions of the extension: stable and experimental.

> [!NOTE]
> If you previously installed the [WinUI 3 Preview extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=Microsoft-WinUI.WinUIProjectTemplates), uninstall the extension. For more information about how to uninstall an extension, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

### Install the stable release

To develop desktop (C#/.NET 5 or C++/WinRT) apps that can be used in production environments, install the latest [stable](stable-channel.md) extension. Choose one of these options to install:

- In Visual Studio, click **Extensions** > **Manage Extensions**, search for **Project Reunion**, and install the latest extension.
    > [!NOTE]
    > In the latest available stable release (0.8.2), the Windows App SDK extension for Visual Studio is named **Project Reunion**. In later releases, it has been renamed to **Windows App SDK**.

- Alternatively, you can download and install the extension directly from Visual Studio Marketplace.

    > [!div class="button"]
    > [Download latest stable release](https://aka.ms/windowsappsdk/stable-vsix)

    ![Screenshot of the Windows App SDK extension being installed](images/reunion-extension-install.png)

### Install the experimental release

To develop desktop (C#/.NET 5 or C++/WinRT) apps or UWP apps that use the latest experimental features, install the latest [experimental](experimental-channel.md) extension. This version of the Windows App SDK cannot be used by apps in production environments. Choose one of these options to install:

- In Visual Studio, click **Extensions** > **Manage Extensions**, search for **Windows App SDK (Experimental)**, and install the latest extension.
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace.

    > [!div class="button"]
    > [Download latest experimental release](https://aka.ms/windowsappsdk/experimental-vsix)

## 5. Enable your device for development

Before you can deploy apps to your development computer, you have to enable it for development. For detailed instructions, see [Enable your device for development](../get-started/enable-your-device-for-development.md).

## 6. Register as an app developer

You can start developing apps now, but you need a developer account to submit your apps to the Microsoft Store. For more information, see [Create a developer account](../get-started/sign-up.md).

## Other tools and downloads

- To enhance the developer experience for MSIX-packaged desktop applications, you can optionally install the single-project MSIX packaging tools extension for Visual Studio and combine the packaging project settings into your application project. This extension enables you to develop and build your MSIX-packaged desktop application without requiring a separate packaging project. For more information, see [Package your app using single-project MSIX](single-project-msix.md).
- If you want to customize your device and install other features or packages, check out the [developer setup scripts](https://github.com/Microsoft/windows-dev-box-setup-scripts).
- For more tools and downloads, see [Downloads and tools for Windows development](https://developer.microsoft.com/windows/downloads).

## Related topics

- [System requirements for Windows app development](system-requirements.md)
- [Get started developing apps for Windows desktop](../get-started/index.md)
- [Visual Studio project and item templates for Windows apps](../desktop/visual-studio-templates.md)
- [Build apps with the Windows App SDK](get-started.md)
- [Enable your device for development](../get-started/enable-your-device-for-development.md)