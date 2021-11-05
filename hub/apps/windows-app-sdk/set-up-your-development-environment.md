---
title: Install tools for Windows app development
description: This article provides instructions for setting up your development computer for Windows app development.
ms.topic: article
ms.date: 10/05/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Install developer tools

To develop apps for Windows 11 and Windows 10, you'll need to configure your development computer with the required development tools.

## 1. Install Visual Studio

Use the following links to install Visual Studio 2019 or Visual Studio 2022 (Preview). You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise. Whichever version you choose, the latest Windows SDK will also be installed by default.

For a list of the minimum system requirements for Visual Studio, see [System requirements for Windows app development](system-requirements.md).

> [!div class="button"]
> [Download Visual Studio 2019](/visualstudio/releases/2019/release-notes)

<!-- > [!div class="button"]
> [Download Visual Studio 2022 (Preview)](/visualstudio/releases/2022/release-notes-preview) -->

### Required workloads and components

Make sure these workloads and components are installed with Visual Studio. These are all selected by default.

- On the **Workloads** tab of the installation dialog, these workloads are required:
  - **Universal Windows Platform development**
  - **Desktop development with C++**
  - **.NET Desktop Development**

- On the **Individual components** tab of the installation dialog, **Windows 10 SDK (10.0.19041.0)** is required in the **SDKs, libraries, and frameworks** section.

- In the **Installation details** pane of the installation dialog, make sure the following items are selected in the **Universal Windows Platform development** section:
  - For Visual Studio 2019: **C++ (v142) Universal Windows Platform tools**
  - For Visual Studio 2022 (Preview): **C++ (v143) Universal Windows Platform tools**

<!-- ## 3. Enable NuGet Package source

Make sure your system has a NuGet package source enabled for the official NuGet service index at `https://api.nuget.org/v3/index.json`.

 1. In Visual Studio, select **Tools** -> **NuGet Package Manager** -> **Package Manager Settings** to open the **Options** dialog.
 2. In the left pane of the **Options** dialog, select the **Package Sources** tab, and make sure there is a package source for **nuget.org** that points to `https://api.nuget.org/v3/index.json` as the source URL. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior). -->

## 2. Install the Windows App SDK extension for Visual Studio (VSIX)

The [Windows App SDK](index.md) provides a unified set of APIs, project templates, and other tools for building Windows apps that are available as a Visual Studio extension (VSIX). The latest extension from the stable release channel will enable you to develop modern desktop (C#/.NET 5 or C++) apps, including WinUI 3 desktop apps, that can be used in production environments. For more information about this channel and the features available in it, see [Stable channel release notes](stable-channel.md).

For a list of the minimum system requirements to use the Windows App SDK, see [System requirements for Windows app development](system-requirements.md).

### Installation options 
- You can install the latest stable release VSIX from Visual Studio: click **Extensions** > **Manage Extensions**, search for **Project Reunion**, and install the latest extension. 
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace. 

    > [!div class="button"]
    > [Download latest stable release](https://aka.ms/windowsappsdk/stable-vsix)

For all previous versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md).

## Next steps

In the next step, you will create your first WinUI 3 app that uses the Windows App SDK. 

> [!div class="nextstepaction"]
> [Create a WinUI 3 app](../winui/winui3/create-your-first-winui3-app.md)

<!-- ### [Preview release](#tab/preview)

To install a preview of the next stable release that can be used to develop desktop (C#/.NET 5 or C++) apps, install the latest extensions from the **preview** release channel. For more information about this channel and the features available in it, see [Preview release channel](preview-channel.md). The preview release channel can't be used by apps in production environments.

To download, choose one or more of the following extensions for the latest preview release. The extensions below are tailored for your programming language and version of Visual Studio.

- **Visual Studio 2019**:

    > [!div class="button"]
    > [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp)

    > [!div class="button"]
    > [C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp)

- **Visual Studio 2022**:

    > [!div class="button"]
    > [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)

    > [!div class="button"]
    > [C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp)

The extensions from the preview channel are available only from the download locations provided above. These extensions are not available via Visual Studio Marketplace or the **Manage Extensions** dialog box in Visual Studio.

> [!NOTE]
> If you install the C# version of the Windows App SDK 1.0 Preview 2 extension for Visual Studio 2019 and want to use the [single-project MSIX project template](single-project-msix.md), you must also install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) separately. The **Blank App, Packaged (WinUI 3 in Desktop)** project template has a known issue that results in a build error unless you also install the single-project packaging tools extension. This issue does not affect other versions of the Windows App SDK 1.0 Preview 2 extension.

### [Experimental release](#tab/experimental)

To develop desktop (C#/.NET 5 or C++) apps or UWP apps that use the latest experimental features, install the latest extension from the **experimental** release channel. For more information about this channel and the features available in it, see [Experimental release channel](experimental-channel.md). This release channel cannot be used by apps in production environments.

Choose one of these options to install the latest experimental release ([version 1.0 Experimental](experimental-channel.md#version-10-experimental-100-experimental1)):

- In Visual Studio, click **Extensions** > **Manage Extensions**, search for **Windows App SDK (Experimental)**, and install the latest extension.
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace.

    > [!div class="button"]
    > [Download latest experimental release](https://aka.ms/windowsappsdk/experimental-vsix) -->

<!-- ## 5. Download Windows App SDK installer and MSIX packages

Unpackaged apps can deploy the Windows App SDK package dependencies by using the Windows App SDK .exe installer or by deploying the MSIX packages directly from the app's setup program. For instructions, see the [deployment guide for unpackaged apps](deploy-unpackaged-apps.md). 

> [!div class="button"]
> [Download latest installer & MSIX packages](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer)

For downloads for all versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md).

## 6. Enable your device for development

Before you can deploy apps to your development computer, you have to enable it for development. For detailed instructions, see [Enable your device for development](../get-started/enable-your-device-for-development.md).

## 7. Register as an app developer

You can start developing apps now, but you need a developer account to submit your apps to the Microsoft Store. For more information, see [Create a developer account](../get-started/sign-up.md).

## Other tools and downloads

- To enhance the developer experience for MSIX-packaged desktop applications, you can optionally install the single-project MSIX packaging tools extension for Visual Studio. This extension enables you to develop and build your MSIX-packaged desktop application without requiring a separate packaging project. This extension is installed by default with the Windows App SDK 1.0 Preview 2 and later versions, or it can be installed separately for previous versions of the Windows App SDK. For more information, see [Package your app using single-project MSIX](single-project-msix.md).
- If you want to customize your device and install other features or packages, check out the [developer setup scripts](https://github.com/Microsoft/windows-dev-box-setup-scripts).
- For more tools and downloads, see [Downloads and tools for Windows development](https://developer.microsoft.com/windows/downloads).

## Related topics

- [System requirements for Windows app development](system-requirements.md)
- [Get started developing apps for Windows desktop](../get-started/index.md)
- [Visual Studio project and item templates for Windows apps](../desktop/visual-studio-templates.md)
- [Create a new project that uses the Windows App SDK](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Enable your device for development](../get-started/enable-your-device-for-development.md) -->


