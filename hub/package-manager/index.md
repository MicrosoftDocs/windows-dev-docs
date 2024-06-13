---
title: Windows Package Manager
description: Windows Package Manager is a comprehensive package manager solution that consists of a command line tool and set of services for installing applications on Windows.
ms.date: 10/30/2023
ms.topic: overview
---

# Windows Package Manager

Windows Package Manager is a comprehensive [package manager solution](#understanding-package-managers) that consists of a command line tool (WinGet) and set of services for installing applications on Windows devices.

Windows Package Manager is a helpful tool for:

- [Developers](#windows-package-manager-for-developers) who want to manage their software applications using the command line.
- [Independent Software Vendors (ISVs)](#windows-package-manager-for-isv-software-distribution) who want to distribute software.
- [Enterprise organizations](#windows-package-manager-for-enterprise-security) who want to automate device set up and maintain a secure work environment.

## Understanding package managers

A package manager is a system or set of tools used to automate installing, upgrading, configuring and using software. Most package managers are designed for discovering and installing developer tools.

Ideally, developers use a package manager to specify the prerequisites for the tools they need to develop solutions for a given project. The package manager then follows the declarative instructions to install and configure the tools. The package manager reduces the time spent getting an environment ready, and it helps ensure the same versions of packages are installed on their machine.

Third party package managers can leverage the [Microsoft Community Package Manifest Repository](package/repository.md) to increase the size of their software catalog.

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

## Windows Package Manager for ISV software distribution

Independent Software Vendors (ISVs) can use Windows Package Manager as a distribution channel for software packages containing their tools and applications. To submit software packages (containing .msix, .msi, or .exe installers) to Windows Package Manager, we provide the open source **Microsoft Community Package Manifest Repository** on GitHub where ISVs can upload [package manifests](package/manifest.md) to have their software packages considered for inclusion with Windows Package Manager. Manifests are automatically validated and may also be reviewed manually.

For more information, see [Submit packages to Windows Package Manager](package/repository.md).

## Windows Package Manager for Enterprise Security

The WinGet client can be used in the command line to install and manage applications across multiple machines. Those responsible for setting up enterprise work environments, such as IT Administrators or Security Analysts,  with the goal of maintaining a consistent level of security settings across everyone’s work machine may also be using [Microsoft Intune](/mem/intune/) to manage security using “Group Policy” settings.

To maintain ongoing security updates, the WinGet client is released using the Microsoft Store and installs applications from the Microsoft Store using the [“msstore” source](./winget/source.md) and applying  “certificate pinning” to ensure that the connection is secure and established with the proper endpoint.

The Group Policy applied by your enterprise organization may be using SSL inspection via a firewall between the WinGet client and the Microsoft Store source that causes a connection error to appear in the WinGet client. 

For this reason, the Windows Package Manager desktop installer supports a policy setting called: “BypassCertificatePinningForMicrosoftStore”.  This policy controls whether the Windows Package Manager will validate the Microsoft Store certificate hash matches to a known Microsoft Store certificate when initiating a connection to the Microsoft Store Source. The options for this policy include:

- **Not configured (default)**: If you do not configure this policy, the Windows Package Manager administrator settings will be adhered to. We recommend leaving this policy in the not configured default unless you have a specific need to change it.
- **Enable**: If you enable this policy, the Windows Package Manager will bypass the Microsoft Store certificate validation.
- **Disable**: If you disable this policy, the Windows Package Manager will validate the Microsoft Store certificate used is valid and belongs to the Microsoft Store before communicating with the Microsoft Store source.

“Certificate Pinning” ensures that the package manager connection to the Microsoft Store is secure, helping to avoid risks associated with attacks such as Man-in-the-Middle (MITM) attacks involving a third party inserting themselves between a client (user) and server (application) to secretly intercept communication flows to steal sensitive data such as login credentials, etc. Disabling “Certificate Pinning” (enabling the bypass) can expose your organization to risk in this area and should be avoided.

To learn more about setting up Group Policy for your enterprise organization, see the [Microsoft Intune documentation](/mem/intune/).

## Related topics

- [Use the winget tool to install and manage software packages](winget/index.md)
- [Submit packages to Windows Package Manager](package/index.md)
