---
author: laurenhughes
title: Install UWP apps with App Installer
description: This section contains or links to articles about App Installer and how to use the features of App Installer.
ms.author: lahugh
ms.date: 06/05/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, app installer, AppInstaller, sideload, related set, optional packages
ms.localizationpriority: medium
---

# Install UWP apps with App Installer

## Purpose
This section contains or links to articles about App Installer and how to use the features of App Installer. 

App Installer allows for UWP apps to be installed by double clicking the app package. This means that users don't need to use PowerShell or other developer tools to deploy UWP apps. The App Installer can also install an app from the web, optional packages, and related sets. To learn how to use the App Installer to install your app, see the topics in the table.

| Topic | Description |
|-------|-------------|
| [Create App Installer file with Visual Studio](create-appinstallerfile-vs.md)| Learn how to use Visual Studio to enable automatic updates using the .appinstaller file. |
| [Install UWP apps from a web page](installing-UWP-apps-web.md) | In this section, we will review the steps you need to take to allow users to install your apps directly from the web page. |
| [Install a related set using an App Installer file](install-related-set.md) | In this section, learn how to allow the installation of a related set via App Installer. We will also go through the steps to construct an App Installer file that will define your related set. |
| [Troubleshoot installation issues with the App Installer file](troubleshoot-appinstaller-issues.md) | Common issues and solutions when sideloading applications with the App Installer file. |
| [App Installer file (.appinstaller) reference](https://docs.microsoft.com/uwp/schemas/appinstallerschema/app-installer-file) | View the full XML schema for the App Installer file. |

## Tutorials 

Follow these tutorials and learn how to host and install a UWP app from various distribution platforms. These tutorials are useful for enterprises and developers that don't want or need to publish their apps to the Store, but still want to take advantage of the Windows 10 packaging and deployment platform.

| Tutorial | Description |
|----------|-------------|
| [Install a UWP app from an Azure Web App](web-install-azure.md) | Create an Azure Web App and use it to host and distribute your UWP app package. |
| [Install a UWP app from an IIS server](web-install-IIS.md) | Set up an IIS server, verify that your web app can host app packages, and use App Installer effectively. |
| [Hosting UWP app packages on AWS for web install](web-install-aws.md) | Learn how to set up Amazon Simple Storage Service to host your UWP app package from a web site. |

