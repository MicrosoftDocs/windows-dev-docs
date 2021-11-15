---
title: Install tools for Preview and Experimental versions of Windows App SDK 
description: This article provides instructions for setting up your development computer for Preview and Experimental versions of Windows App SDK
ms.topic: article
ms.date: 10/05/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Install tools for Preview and Experimental channels of the Windows App SDK

To develop apps for Windows 11 and Windows 10, you'll need to configure your development computer with the required development tools.

**Using the Windows App SDK Stable version**: To set up your environment using the stable version of the Windows App SDK, see [Install developer tools](set-up-your-development-environment.md).

[!INCLUDE [UWP migration guidance](./includes/uwp-app-sdk-migration-pointer.md)]

## 1. Install Visual Studio

Use the following links to install Visual Studio 2022 version 17.0 (recommended), Visual Studio 2022 version 17.1 (Preview), or Visual Studio 2019. You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise. Whichever version you choose, the latest Windows SDK will also be installed.

> [!div class="button"]
> [Download Visual Studio 2022 version 17.0](/visualstudio/releases/2022/release-notes)

> [!div class="button"]
> [Download Visual Studio 2022 version 17.1 (Preview)](/visualstudio/releases/2022/release-notes-preview)

> [!div class="button"]
> [Download Visual Studio 2019](/visualstudio/releases/2019/release-notes)


### Required workloads and components

While installing Visual Studio, select the following workloads and components...

#### [Visual Studio 2022 / 2019](#tab/vs)

- On the **Workloads** tab of the installation dialog, select:
  - **Universal Windows Platform development**
  - **.NET Desktop Development**
  - **Desktop development with C++**

- On the **Individual components** tab of the installation dialog, make sure **Windows 10 SDK (10.0.19041.0)** is selected in the **SDKs, libraries, and frameworks** section.

- In the **Installation details** pane of the installation dialog, make sure the following items are selected in the **Universal Windows Platform development** section:
  - For Visual Studio 2019: **C++ (v142) Universal Windows Platform tools**
  - For Visual Studio 2022: **C++ (v143) Universal Windows Platform tools**

#### [Visual Studio 2022 version 17.1 (Preview)](#tab/vs-preview)

On the **Workloads** tab of the installation dialog, select one or both of:

- **.NET Desktop Development** for C# app development
  - Then in the **Installation details** pane of the installation dialog, select **Windows App SDK C# Templates** (at the bottom of the list).

- **Desktop development with C++** for C++ app development
  - Then in the **Installation details** pane of the installation dialog,  select **Windows App SDK C++ Templates** (at the bottom of the list).

---

## 2. Install the Windows App SDK extension for Visual Studio (VSIX)

The Windows App SDK includes project and item templates for creating and developing WinUI 3 apps. These project templates are built into Visual Studio 2022 version 17.1 (Preview), and available as Visual Studio extensions (VSIX) for Visual Studio 2022 / 2019. There are three versions of the extension: **stable**, **preview**, and **experimental**. For more information about the differences between these versions, see [Release channels](release-channels.md).

> [!NOTE]
> If you have Windows App SDK Visual Studio extensions (VSIX) already installed, then uninstall them before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

To download the latest Windows App SDK extensions for Visual Studio, see the installation instructions in the following tabs. For downloads for all versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md).

### [Preview release](#tab/preview/vs)

To install a preview of the next stable release that can be used to develop desktop (C# or C++) apps, install the latest extensions from the **preview** release channel. For more information about this channel and the features available in it, see [Preview release channel](preview-channel.md). The preview release channel can't be used by apps in production environments.

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

### [Preview release](#tab/preview/vs-preview)

Visual Studio 2022 version 17.1 (Preview) already includes the latest Preview release of the Windows App SDK project templates as part of the individual components when installing Visual Studio as described in step #2. If you didn't select the **Windows App SDK C# Templates** and/or the **Windows App SDK C++ Templates** when installing Visual Studio 2022 version 17.1 (Preview), modify your installation and follow step #2.

### [Experimental release](#tab/experimental/vs)

To develop desktop (C# or C++) apps that use the latest experimental features, install the latest extension from the **experimental** release channel. For more information about this channel and the features available in it, see [Experimental release channel](experimental-channel.md). This release channel shouldn't be used by apps in production environments.

Choose one of these options to install the latest experimental release ([version 1.0 Experimental](experimental-channel.md#version-10-experimental-100-experimental1)):

- In Visual Studio, click **Extensions** > **Manage Extensions**, search for **Windows App SDK (Experimental)**, and install the latest extension.
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace.

    > [!div class="button"]
    > [Download latest experimental release](https://aka.ms/windowsappsdk/experimental-vsix)

### [Experimental release](#tab/experimental/vs-preview)

Visual Studio 2022 version 17.1 (Preview) only supports the latest 1.0 Preview 3 release. To use the experimental release, install Visual Studio 2022 version 17.0 or Visual Studio 2019.

---

## Related topics

- [System requirements for Windows app development](system-requirements.md)
- [Overview of app development options](../get-started/index.md)
- [Create a WinUI 3 app](preview-experimental-create-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)

