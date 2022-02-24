---
title: Install tools for the Windows App SDK
description: Configure your development computer by installing the appropriate tools to develop apps for Windows by using the [Windows App SDK](/windows/apps/windows-app-sdk/).
ms.topic: article
ms.date: 02/15/2022
keywords: windows win32, windows app development, Windows App SDK, stable
ms.author: stwhi
author: stevewhims
ms.custom: seo-windows-dev
---

# Install tools for the Windows App SDK

Configure your development computer by installing the appropriate tools to develop apps for Windows with the [Windows App SDK](/windows/apps/windows-app-sdk/).

> [!NOTE]
> This topic is for the stable release channel of the Windows App SDK&mdash;see [Windows App SDK release channels](/windows/apps/windows-app-sdk/release-channels). There's also [Install tools for preview and experimental channels of the Windows App SDK](/windows/apps/windows-app-sdk/preview-experimental-install).

## Install Visual Studio

Use these links to install Visual Studio 2022 (recommended) or Visual Studio 2019. You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise. Also see [System requirements for Windows app development](system-requirements.md).

> [!div class="button"]
> [Download Visual Studio 2022](/visualstudio/releases/2022/release-notes)

> [!div class="button"]
> [Download Visual Studio 2019](/visualstudio/releases/2019/release-notes)

### Required workloads and components

While installing Visual Studio, select these workloads and components.

#### [Visual Studio 2022 version 17.1 and later](#tab/vs-2022-17-1-a)

* On the **Workloads** tab of the installation dialog, select:
  * **Universal Windows Platform development**
    * Then in the **Installation details** pane of the installation dialog for that workload, make sure **C++ (v143) Universal Windows Platform tools** is selected.
  * **.NET Desktop Development** for C# app development
    * Then in the **Installation details** pane of the installation dialog, select **Windows App SDK C# Templates** (at the bottom of the list).
  * **Desktop development with C++** for C++ app development
    * Then in the **Installation details** pane of the installation dialog,  select **Windows App SDK C++ Templates** (at the bottom of the list).

* On the **Individual components** tab of the installation dialog, in the **SDKs, libraries, and frameworks** section, make sure **Windows 10 SDK (10.0.19041.0)** is selected.

##### [Other Visual Studio versions](#tab/vs-other)

* On the **Workloads** tab of the installation dialog, select:
  * **Universal Windows Platform development**
    * Then in the **Installation details** pane of the installation dialog for that workload, make sure either **C++ (v143) Universal Windows Platform tools** (for Visual Studio 2022) or **C++ (v142) Universal Windows Platform tools** (for Visual Studio 2019) is selected.
  * **.NET Desktop Development** for C# app development
  * **Desktop development with C++** for C++ app development

* On the **Individual components** tab of the installation dialog, in the **SDKs, libraries, and frameworks** section, make sure **Windows 10 SDK (10.0.19041.0)** is selected.

---

## Visual Studio project and item templates

The [Windows App SDK](index.md) includes Visual Studio project and item templates for creating and developing WinUI 3 apps.

### [Visual Studio 2022 version 17.1 and later](#tab/vs-2022-17-1-b)

If you followed the instructions in [Required workloads and components](#required-workloads-and-components) above, then the templates are already installed.

### [Visual Studio 2022 version 17.0](#tab/vs-2022-17-0)

The templates are available by installing a Visual Studio extension (VSIX).

> [!NOTE]
> If you have a Windows App SDK Visual Studio extension (VSIX) already installed, then uninstall it before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

Download the extension directly, and install it:

> [!div class="button"]
> [Download latest C# stable release](https://aka.ms/windowsappsdk/stable-vsix-2022-cs)

> [!div class="button"]
> [Download latest C++ stable release](https://aka.ms/windowsappsdk/stable-vsix-2022-cpp)

### [Visual Studio 2019](#tab/vs-2019)

The templates are available by installing a Visual Studio extension (VSIX).

> [!NOTE]
> If you have a Windows App SDK Visual Studio extension (VSIX) already installed, then uninstall it before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

* You can install the latest stable release VSIX from Visual Studio. Click **Extensions** > **Manage Extensions**, search for *Windows App SDK*, and download the Windows App SDK extension. Close and reopen Visual Studio, and follow the prompts to install the extension.
* Alternatively, you can download the extension directly from Visual Studio Marketplace, and install it:

> [!div class="button"]
> [Download latest C# stable release](https://aka.ms/windowsappsdk/stable-vsix-2019-cs)

> [!div class="button"]
> [Download latest C++ stable release](https://aka.ms/windowsappsdk/stable-vsix-2019-cpp)

---

For more versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md).

## Next steps

To create your first WinUI 3 app that uses the Windows App SDK, see [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md).

Also see [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md).

## Related topics

* [Windows App SDK](/windows/apps/windows-app-sdk/)
* [Windows App SDK release channels](/windows/apps/windows-app-sdk/release-channels)
* [Install tools for preview and experimental channels of the Windows App SDK](/windows/apps/windows-app-sdk/preview-experimental-install)
* [System requirements for Windows app development](system-requirements.md)
* [Downloads for the Windows App SDK](downloads.md)
* [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
* [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
