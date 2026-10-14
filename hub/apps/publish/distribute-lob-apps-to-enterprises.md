---
description: Compare current options for distributing signed MSIX line-of-business apps with Intune, Configuration Manager, App Installer, or sideloading.
title: Distribute LOB apps to enterprises
ms.assetid: 2050126E-CE49-4DE3-AC2B-A572AC895158
ms.date: 10/14/2026
ms.topic: article
keywords: windows 11, windows 10, lob, line-of-business, enterprise apps, enterprise, intune, configuration manager
ms.localizationpriority: medium
---

# Distribute LOB apps to enterprises

You have several options for distributing line-of-business (LOB) apps to your organization's users with [MSIX packages](/windows/msix/) without making the apps broadly available to the public. For managed devices, use Microsoft Intune or Microsoft Configuration Manager. You can also use App Installer or distribute signed packages directly.

Every MSIX package installed outside the Microsoft Store must be signed with a certificate that the target device trusts. Sideloading is enabled by default on Windows 10, version 2004 and later, and on Windows 11.

## Choose a distribution method

| Method | Use when |
|---|---|
| Microsoft Intune | You manage cloud-connected devices and want to assign the app to Microsoft Entra user or device groups. |
| Microsoft Configuration Manager | You manage devices on-premises or through co-management and want centralized deployment and reporting. |
| App Installer | You host the package and an optional `.appinstaller` file on a web server and want an interactive installation and update experience. |
| Direct sideloading | You distribute a signed package through a file share, download, script, or other controlled channel. |

## Deploy with Microsoft Intune

In Intune, add the package as a Windows **Line-of-business app**, configure its assignments, and deploy it to user or device groups. You can make the app required or make it available for users to install from Company Portal.

For the current package requirements and deployment procedure, see [Add a Windows line-of-business app to Microsoft Intune](/intune/app-management/deployment/add-lob-windows).

## Deploy with Microsoft Configuration Manager

Configuration Manager can read an MSIX package's identity, publisher, and version and configure its installation and detection settings. Deploy the signing certificate to devices before the app if the package uses a certificate that the devices don't already trust.

For the complete deployment procedure, see [Deploy MSIX apps with Microsoft Configuration Manager](/windows/msix/desktop/managing-your-msix-deployment-configmgr).

## Distribute with App Installer

App Installer enables a user to install a signed MSIX package by opening the package or an `.appinstaller` file. An `.appinstaller` file can specify related packages and update settings, including update checks and required updates.

Host the package and `.appinstaller` file on an HTTPS web server, and link directly to the `.appinstaller` file for users to download and open. For more information, see [App Installer file overview](/windows/msix/app-installer/app-installer-file-overview).

> [!IMPORTANT]
> The `ms-appinstaller:` URI protocol for one-click installation from a web page is disabled by default. Enterprise administrators can enable it through policy on managed devices. See [Current status of Windows app distribution features](../package-and-deploy/distribution-feature-status.md).

## Distribute packages directly

Users can install a signed MSIX package by opening it with App Installer. Administrators can also install packages with PowerShell, deployment scripts, provisioning packages, or Windows images.

The target devices must trust the package's signing certificate. For certificate and package requirements, see [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps) and [Sign an app package using SignTool](/windows/msix/package/sign-app-package-using-signtool).

## Microsoft Store and Company Portal

Microsoft Store for Business and Microsoft Store for Education retired on March 31, 2023. The enterprise association, private store, and offline Store licensing workflow that those services provided is no longer a distribution path for new LOB apps.

Company Portal is the private app repository for organizations that use Intune. Add an internal app directly to Intune as a LOB app, or add an eligible public Microsoft Store app from the Store catalog in Intune. For more information, see:

- [Use the Company Portal app for your private app repository](/windows/application-management/private-app-repository-mdm-company-portal-windows-11)
- [Add Microsoft Store apps to Microsoft Intune](/intune/app-management/deployment/add-microsoft-store)

For internal apps that must remain private to an organization, use Intune, Configuration Manager, App Installer, or direct sideloading. Partner Center also provides a **Private audience** option for limited testing. Private audiences contain individually named personal Microsoft accounts, not Microsoft Entra work or school accounts, so they aren't a replacement for enterprise app deployment. See [Choose visibility options for MSIX apps](publish-your-app/msix/visibility-options.md#private-audience).

<span id="organizational"></span>

## Organizational licensing options

Partner Center still shows **Organizational licensing** settings on the [Pricing and availability](publish-your-app/msix/price-and-availability.md#organizational-licensing) page of an app submission. These settings apply to apps that you publish publicly in Microsoft Store. They don't create a private catalog for your organization, and they don't replace the retired Microsoft Store for Business and Microsoft Store for Education volume-acquisition workflow described earlier in this article.

> [!NOTE]
> Selections for each of your apps are configured independently from each other. You can change your preferences for an app at any time by creating a new submission. Your changes take effect after the submission completes the [certification process](publish-your-app/msix/app-certification-process.md).

> [!IMPORTANT]
> Submissions that use the [Microsoft Store submission API](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services) aren't made available to Microsoft Store for volume acquisition by organizations. To use these settings, create and submit your submission in Partner Center.

### Organization-managed licensing setting

The **Make my app available to organizations with Store-managed (online) licensing and distribution** option is a legacy setting. Because Microsoft Store for Business and Microsoft Store for Education are retired, don't rely on it to distribute an internal app to your organization. Use Intune, Configuration Manager, App Installer, or direct sideloading instead.

If you uncheck this box, the change takes effect after the app completes the certification process. Licenses that organizations previously acquired remain valid, and people who already have the app can continue to use it.

### Offline licensing setting

The **Allow organization-managed (offline) licensing and distribution for organizations** option is unchecked by default. It applies to apps published in Microsoft Store and doesn't provide a workflow for distributing a private LOB app.

> [!IMPORTANT]
> Offline licensing isn't supported for .xap packages.
