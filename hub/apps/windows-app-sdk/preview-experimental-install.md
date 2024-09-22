---
title: Install tools for preview and experimental versions of the Windows App SDK
description: Configure your development computer by installing the appropriate tools to develop apps for Windows by using the preview and experimental channels of the [Windows App SDK](./index.md).
ms.topic: article
ms.date: 08/19/2024
keywords: windows win32, windows app development, Windows App SDK, preview, experimental
ms.localizationpriority: medium
---

# Install tools for preview and experimental channels of the Windows App SDK

Configure your development computer by installing the appropriate tools to develop apps for Windows with the preview and experimental channels of the [Windows App SDK](./index.md).

> [!NOTE]
> This topic is for the preview and experimental release channels of the Windows App SDK&mdash;see [Windows App SDK release channels](./release-channels.md). For the stable channel, there's also [Install tools for the Windows App SDK](./set-up-your-development-environment.md).

## Install Visual Studio

Use these links to install Visual Studio 2022 (recommended) or Visual Studio 2019. You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise. Also see [System requirements for Windows app development](system-requirements.md).

> [!div class="button"]
> [Download Visual Studio 2022](/visualstudio/releases/2022/release-notes)

> [!div class="button"]
> [Download Visual Studio 2019](/visualstudio/releases/2019/release-notes)

### Required workloads and components

While installing Visual Studio, select these workloads and components.

* On the **Workloads** tab of the installation dialog, select as appropriate:
  * For Universal Windows Platform (UWP) app development, select **Universal Windows Platform development**
    * Then in the **Installation details** pane of the installation dialog for that workload, make sure either **C++ (v143) Universal Windows Platform tools** (for Visual Studio 2022) or **C++ (v142) Universal Windows Platform tools** (for Visual Studio 2019) is selected.
  * For C# app development, select **.NET Desktop Development**
  * For C++ app development, select **Desktop development with C++**

* On the **Individual components** tab of the installation dialog, in the **SDKs, libraries, and frameworks** section, make sure **Windows 10 SDK (10.0.19041.0)** is selected.

## Preview release

The [Windows App SDK](index.md) includes Visual Studio project and item templates for creating and developing WinUI 3 apps. For a preview of the next stable release, install the latest extension from the preview release channel. For more info, see [Preview release channel](preview-channel.md).

### [Visual Studio 2022 version 17.1 and later](#tab/vs-2022-17-1)

The templates are available by installing a Visual Studio extension (VSIX).

> [!NOTE]
> If you installed **Windows App SDK C# Templates** (an installation detail of the **.NET Desktop Development** workload) and/or **Windows App SDK C++ Templates** (an installation detail of the **Desktop development with C++** workload), then run Visual Studio Installer to uninstall them before installing the VSIX.

> [!div class="button"]
> [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)

> [!div class="button"]
> [C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp)

### [Visual Studio 2022 version 17.0](#tab/vs-2022-17)

The templates are available by installing a Visual Studio extension (VSIX).

> [!NOTE]
> If you have a Windows App SDK Visual Studio extension (VSIX) already installed, then uninstall it before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

> [!div class="button"]
> [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp)

> [!div class="button"]
> [C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp)

### [Visual Studio 2019](#tab/vs-2019)

The templates are available by installing a Visual Studio extension (VSIX).

> [!NOTE]
> If you have a Windows App SDK Visual Studio extension (VSIX) already installed, then uninstall it before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

> [!div class="button"]
> [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp)

> [!div class="button"]
> [C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp)

---

The extensions from the preview channel are available only from the download locations provided above. These extensions are not available via Visual Studio Marketplace or the **Manage Extensions** dialog box in Visual Studio.

> [!NOTE]
> If you install the C# version of the Windows App SDK 1.0 Preview 2 extension for Visual Studio 2019, and you want to use the Single-project MSIX Packaging Tools project template (see [Package your app using single-project MSIX](./single-project-msix.md)), then you must also install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) separately. The **Blank App, Packaged (WinUI 3 in Desktop)** project template has a known issue that results in a build error unless you also install the single-project packaging tools extension. This issue doesn't affect other versions of the Windows App SDK 1.0 Preview 2 extension.

## Experimental release

For experimental features, install the latest extension from the experimental release channel. For more info, see [Experimental release channel](experimental-channel.md).

Choose one of these options to install the latest experimental release ([Version 1.0 Experimental (1.0.0-experimental1)](release-notes-archive/experimental-channel-1.0.md#version-10-experimental-100-experimental1)):

* In Visual Studio, click **Extensions** > **Manage Extensions**, search for **Windows App SDK (Experimental)**, and install the latest extension.
* Alternatively, you can download and install the extension directly from Visual Studio Marketplace:

> [!div class="button"]
> [Download latest experimental release](https://aka.ms/windowsappsdk/experimental-vsix)

For more versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md).

## Next steps

To create your first WinUI 3 app that uses the Windows App SDK, see [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md).

Also see [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md).

## Related topics

* [Windows App SDK](./index.md)
* [Windows App SDK release channels](./release-channels.md)
* [Install tools for the Windows App SDK](./set-up-your-development-environment.md)
* [System requirements for Windows app development](system-requirements.md)
* [Preview release channel](preview-channel.md)
* [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions)
* [Package your app using single-project MSIX](./single-project-msix.md)
* [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools)
* [Experimental release channel](experimental-channel.md)