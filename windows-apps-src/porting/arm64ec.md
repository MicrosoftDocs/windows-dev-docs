---
title: ARM64EC for Windows 11 apps on ARM
description: Learn how ARM64EC empowers you to build and incrementally update apps that benefit from native performance on ARM devices, without interrupting your current x64 functionality.
ms.date: 06/25/2021
ms.topic: article
keywords: ARM64EC, windows 11, ARM, ARM64, x86, emulation
author: marswe
ms.author: marcs
ms.localizationpriority: medium
---

# Using ARM64EC to build apps for Windows 11 on ARM devices

ARM64EC (“Emulation Compatible”) is a new application binary interface (ABI) for building apps for Windows 11 on ARM. With ARM64EC, you can build new native apps or incrementally transition existing apps to native performance on ARM. You can read more about ARM64EC on the [Windows Developer blog](https://aka.ms/arm64ecannounceblog). 

## Get started building Win32 apps as ARM64EC

To start building Win32 apps as ARM64EC, you'll need to install these prerequisites:

- The latest [Windows Insider SDK build](https://aka.ms/windowsinsidersdk) available through the Windows Insider program.
- [Visual Studio Preview (version 16.11 preview 2 or later)](https://visualstudio.microsoft.com/vs/preview/).

Once the Windows Insider SDK and Visual Studio Preview have been installed, follow these steps to add the ARM64EC platform.

1. In the Visual Studio Installer, add the ARM64EC tools by searching under **Individual components** and selecting the **MSVC v142 - VS 2019 C++ ARM64EC build tools** checkbox, currently marked as experimental.

    ![Visual Studio Installer ARM64EC checkbox screenshot](images/arm/arm64ec-vs-installer.png)

2. With the tools and SDK installed, create a new C++ project or open an existing one.

    > [!NOTE]
    > If your project is using an older SDK or older version of MSVC, you'll need to retarget the solution to use the latest version of each.

3. To add the ARM64EC platform:
    - In the **Build** menu, select **Configuration Manager**.
    - In the **Active solution platform** box, select **`<New…>`** to create a new platform.
    - Select **ARM64EC**, Copy settings from **x64**, and check the **Create new project platforms** checkbox.

    ![Visual Studio Installer New ARM64EC Platform screenshot](images/arm/arm64ec-vs-new-platform.png)

    You can choose to leave parts of the solution as x64 as needed. However, the more code built as ARM64EC, the more code that will run with native performance on Windows 11 on ARM. For any external dependencies, ensure that your project links against the x64 or ARM64EC versions of those projects.

4. With the new solution platform in place, select **Build** in Visual Studio to start building ARM64EC binaries.  
