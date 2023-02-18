---
title: Windows Package Manager
description: Windows Package Manager is a comprehensive package manager solution that consists of a command line tool and set of services for installing applications on Windows.
ms.date: 02/17/2023
ms.topic: overview
---

# Windows Package Manager

Windows Package Manager is a comprehensive [package manager solution](#understanding-package-managers) that consists of a command line tool and set of services for installing applications on Windows 10 and Windows 11.

## Windows Package Manager for developers

Developers use the **winget** command line tool to discover, install, upgrade, remove and configure a curated set of applications. After it is installed, developers can access **winget** via the Windows Terminal, PowerShell, or the Command Prompt.

For more information, see [Use the winget tool to install and manage applications](winget/index.md).

For a video demo of winget, see [Intro to Windows Package Manager](/shows/open-at-microsoft/intro-to-windows-package-manager).

For the latest announcements and version updates, see the [Windows Command Line Blog](https://devblogs.microsoft.com/commandline/), including:

- [Windows Package Manager 1.4](https://devblogs.microsoft.com/commandline/windows-package-manager-1-4/)
- [Windows Package Manager 1.3](https://devblogs.microsoft.com/commandline/windows-package-manager-1-3/)
- [Windows Package Manager 1.2](https://devblogs.microsoft.com/commandline/windows-package-manager-1-2/)
- [Windows Package Manager 1.1](https://devblogs.microsoft.com/commandline/windows-package-manager-1-1/)
- [Windows Package Manager 1.0](https://devblogs.microsoft.com/commandline/windows-package-manager-1-0/)

## Windows Package Manager for ISVs

Independent Software Vendors (ISVs) can use Windows Package Manager as a distribution channel for software packages containing their tools and applications. To submit software packages (containing .msix, .msi, or .exe installers) to Windows Package Manager, we provide the open source **Microsoft Community Package Manifest Repository** on GitHub where ISVs can upload [package manifests](package/manifest.md) to have their software packages considered for inclusion with Windows Package Manager. Manifests are automatically validated and may also be reviewed manually.

For more information, see [Submit packages to Windows Package Manager](package/repository.md).

## Understanding package managers

A package manager is a system or set of tools used to automate installing, upgrading, configuring and using software. Most package managers are designed for discovering and installing developer tools.

Ideally, developers use a package manager to specify the prerequisites for the tools they need to develop solutions for a given project. The package manager then follows the declarative instructions to install and configure the tools. The package manager reduces the time spent getting an environment ready, and it helps ensure the same versions of packages are installed on their machine.

Third party package managers can leverage the [Microsoft Community Package Manifest Repository](package/repository.md) to increase the size of their software catalog.

## Related topics

* [Use the winget tool to install and manage software packages](winget/index.md)
* [Submit packages to Windows Package Manager](package/index.md)
