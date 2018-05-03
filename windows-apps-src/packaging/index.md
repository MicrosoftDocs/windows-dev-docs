---
author: laurenhughes
ms.assetid: 1abcbb13-80f0-4bf1-a812-649ee8bd1915
title: Packaging apps
description: This section contains or links to articles about packaging for Universal Windows Platform (UWP) apps.
ms.author: lahugh
ms.date: 10/10/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, packaging
ms.localizationpriority: medium
---
# Packaging apps


## Purpose

This section contains or links to articles about packaging for Universal Windows Platform (UWP) apps.

| Topic | Description |
|-------|-------------|
| [Package a UWP app with Visual Studio](packaging-uwp-apps.md) | To distribute or sell your Universal Windows Platform (UWP) app, you need to create an app package for it. |
| [Manual app packaging](manual-packaging-root.md) | If you want to create and sign an app package, but you didn't use Visual Studio to develop your app, you'll need to use the manual app packaging tools. |
| [App package architectures](device-architecture.md) | Learn more about which processor architecture(s) you should use when building your UWP app package. |
| [UWP App Streaming Install](streaming-install.md) | Universal Windows Platform (UWP) App Streaming Install enables you to specify which parts of your app you would like the Microsoft Store to download first. When the essential files of the app are downloaded first, the user can launch and interact with the app while the rest of it finishes downloading in the background. |
| [Optional packages and related set authoring](optional-packages.md) | Optional packages contain content that can be integrated with a main package. These are useful for downloadable content (DLC), dividing a large app for size restraints, or for shipping any additional content for separate from your original app. |
| [Optional packages with executable code](optional-packages-with-executable-code.md) | Learn how to use Visual Studio to create an optional package with executable code. |
| [Install UWP apps with App Installer](appinstaller-root.md) | App Installer allows for UWP apps to be installed by double clicking the app package. |
| [Install apps with the WinAppDeployCmd.exe tool](install-universal-windows-apps-with-the-winappdeploycmd-tool.md) | Windows Application Deployment (WinAppDeployCmd.exe) is a command line tool that can use to deploy a UWP app from a Windows 10 machine to any Windows 10 Mobile device. You can use this tool to deploy an .appx package when the Windows 10 Mobile device is connected by USB or available on the same subnet without needing Microsoft Visual Studio or the solution for that app. This article describes how to install UWP apps using this tool. |
| [Set up automated builds for your UWP app](auto-build-package-uwp-apps.md) | If you want to package your app as part of an automated build process, this topic shows you how to use Visual Studio Team Services (VSTS) to do it. |
| [App capability declarations](app-capability-declarations.md) | Capabilities must be declared in your UWP app's [package manifest](https://msdn.microsoft.com/library/windows/apps/BR211474) to access certain API or resources like pictures, music, or devices like the camera or the microphone. |
| [Download and install package updates from the Store](self-install-package-updates.md) | Your UWP app can programmatically check for package updates and install the updates. Your app can also query for packages that have been marked as mandatory on the Windows Dev Center dashboard and disable functionality until the mandatory update is installed.  |
