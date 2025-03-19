---
title: Windows Package Manager
description: Windows Package Manager is a comprehensive package manager solution that consists of a command line tool and set of services for installing applications on Windows.
ms.date: 02/13/2025
ms.topic: overview
---

# Windows Package Manager

Windows Package Manager is a comprehensive [package manager solution](#understanding-package-managers) that includes:

- [WinGet](./winget/index.md): The command line tool and client interface for the Windows Package Manager service. You can view the packages available using the command [`winget list`](./winget/list.md), find more winget commands: [Use the WinGet tool to install and manage applications](./winget/index.md).
- [Submit packages to Windows Package Manager](./package/index.md): The packaging services for hosting and installing applications on Windows devices.
- [WinGet Configuration files](./configuration/index.md): Create a set of instructions for Windows Package Manager to consolidate the steps for manually setting up a device and onboarding to a new project to a single command that is reliable and repeatable. WinGet Configuration files utilize PowerShell Desired State Configuration (DSC) in combination with YAML formatted instructions and WinGet packages to handle your machine set up.

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

Find the [latest Windows Package Manager announcements and version updates](https://devblogs.microsoft.com/commandline/author/denelon/) in the [Windows Command Line Blog](https://devblogs.microsoft.com/commandline/).

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

## Additional Group Policy settings for Windows Package Manager

Windows Package Manager provides additional configuration options through Group Policy, allowing IT administrators to manage and control functionality across multiple devices. These settings are particularly beneficial for enterprise environments where compliance and consistency are critical.

Beginning in Windows 11, additional Group Policy templates for Windows Package Manager are included with each release. These templates are divided into several subcategories, enabling IT administrators to configure key aspects of the tool's behavior, such as:

- **Source Control**: Specify which sources are allowed or blocked.
- **Local Development**: Control whether users are allowed to enable experimental features or local manifest installations.
- **Execution Policies**: Set policies for the command line interface and proxy options.

To download the Group Policy templates:

1. Visit [Windows Package Manage GitHub releases](https://github.com/microsoft/winget-cli/releases).
2. Locate the release version you wish to use.
3. Download the `DesktopAppInstallerPolicies.zip` file included in the release assets.

The ZIP file contains the necessary `.admx` and `.adml` files for deploying the policies. Once you've downloaded the `DesktopAppInstallerPolicies.zip` file:

1. Extract the contents of the ZIP file on your local machine.
2. Copy the `.admx` file to the `C:\Windows\PolicyDefinitions` folder on the target device.
3. Copy the corresponding language-specific `.adml` file to the appropriate subdirectory, such as `C:\Windows\PolicyDefinitions\en-US`.
4. Open the Group Policy Management Console (GPMC) to configure the policies.

> [!NOTE]
> When working on a Windows Domain Controller, you can store the Group Policy templates in the Central Store. For detailed instructions, see [How to create and manage the Central Store for Group Policy Administrative Templates in Windows](/troubleshoot/windows-client/group-policy/create-and-manage-central-store).

New Group Policy settings may be introduced with each release of Windows Package Manager. To ensure your environment is always up to date:

- Regularly check for updates on the [Windows Package Manager GitHub repository](https://github.com/microsoft/winget-cli/releases) page.
- Review the release notes for changes or additions to the policy templates.
