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

Use the following links to install Visual Studio 2022 version 17.0 (recommended) or Visual Studio 2019. You can choose between the free Visual Studio Community edition, Visual Studio Professional, or Visual Studio Enterprise.

For a list of the minimum system requirements for Visual Studio, see [System requirements for Windows app development](system-requirements.md).

> [!div class="button"]
> [Download Visual Studio 2022 version 17.0](/visualstudio/releases/2022/release-notes)

> [!div class="button"]
> [Download Visual Studio 2019](/visualstudio/releases/2019/release-notes)

### Required workloads and components

While installing Visual Studio 2022 or 2019, select the following workloads and components.

- On the **Workloads** tab of the installation dialog, select:
  - **Universal Windows Platform development**
  - **.NET Desktop Development**
  - **Desktop development with C++**

- On the **Individual components** tab of the installation dialog, make sure **Windows 10 SDK (10.0.19041.0)** is selected in the **SDKs, libraries, and frameworks** section.

- In the **Installation details** pane of the installation dialog, make sure the following items are selected in the **Universal Windows Platform development** section:
  - For Visual Studio 2019: **C++ (v142) Universal Windows Platform tools**
  - For Visual Studio 2022: **C++ (v143) Universal Windows Platform tools**

## 2. Install the Windows App SDK extension for Visual Studio (VSIX)

The [Windows App SDK](index.md) includes project and item templates for creating and developing WinUI 3 apps. These project templates are available as Visual Studio extensions (VSIX) for Visual Studio 2022 / 2019. 

For a list of the minimum system requirements to use the Windows App SDK, see [System requirements for Windows app development](system-requirements.md).

### Installation options 

#### [Visual Studio 2022](#tab/vs-2022)

You can download and install the latest C# or C++ VSIX extension directly from the links below.

    > [!div class="button"]
    > [Download latest C# stable release](https://aka.ms/windowsappsdk/stable-vsix-2022-cs)

    > [!div class="button"]
    > [Download latest C++ stable release](https://aka.ms/windowsappsdk/stable-vsix-2022-cpp)

#### [Visual Studio 2019](#tab/vs-2019)

- You can install the latest stable release VSIX from Visual Studio: click **Extensions** > **Manage Extensions**, search for **Windows App SDK**, and install the latest C# or C++ extension depending on what language you want to use. 
- Alternatively, you can download and install the extension directly from Visual Studio Marketplace. 

    > [!div class="button"]
    > [Download latest stable release](https://aka.ms/windowsappsdk/stable-vsix)

    > [!div class="button"]
    > [Download latest stable release](https://aka.ms/windowsappsdk/stable-vsix-cpp)

---

For all other versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md) and [Install tools for Preview and Experimental](preview-experimental-install.md).

## Next steps

In the next step, you will create your first WinUI 3 app that uses the Windows App SDK. 

> [!div class="nextstepaction"]
> [Create a WinUI 3 app](../winui/winui3/create-your-first-winui3-app.md)


## Related topics

- [System requirements for Windows app development](system-requirements.md)
- [Install tools for Preview and Experimental channels](preview-experimental-install.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)


