---
ms.assetid: 1abcbb13-80f0-4bf1-a812-649ee8bd1915
title: Packaging apps
description: This section contains or links to articles about packaging for Universal Windows Platform (UWP) apps.
ms.date: 07/22/2019
ms.topic: article
keywords: windows 10, uwp, packaging
ms.localizationpriority: medium
---

# Packaging apps

This section contains or links to articles about packaging Universal Windows Platform (UWP) apps in MSIX and .appx app packages for deployment and installation. Some of these links go to relevant articles in the [MSIX documentation](/windows/msix/).

> [!NOTE]
> The original app packaging format for UWP apps in Windows 10 was .appx. Starting in Windows 10, version 1809, this packaging format was renamed to .msix and extended to support all types of Windows apps, including .NET and C++/Win32 desktop apps. Support for MSIX is also being extended to earlier Windows versions. For more information, see the [MSIX documentation](/windows/msix/).

| Topic | Description |
|-------|-------------|
| [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps) | To distribute or sell your Universal Windows Platform (UWP) app, you need to create an app package for it. |
| [Manual app packaging](/windows/msix/package/manual-packaging-root) | If you want to create and sign an app package, but you didn't use Visual Studio to develop your app, you'll need to use the manual app packaging tools. |
| [App package architectures](/windows/msix/package/device-architecture) | Learn more about which processor architecture(s) you should use when building your app package. |
| [UWP App Streaming Install](/windows/msix/package/streaming-install) | App Streaming Install enables you to specify which parts of your app you would like the Microsoft Store to download first. When the essential files of the app are downloaded first, the user can launch and interact with the app while the rest of it finishes downloading in the background. |
| [Optional packages and related set authoring](/windows/msix/package/optional-packages) | Optional packages contain content that can be integrated with a main package. These are useful for downloadable content (DLC), dividing a large app for size restraints, or for shipping any additional content for separate from your original app. |
| [Optional packages with executable code](/windows/msix/package/optional-packages-with-executable-code) | Learn how to use Visual Studio to create an optional package with executable code. |
| [Install Windows 10 apps with App Installer](/windows/msix/app-installer/app-installer-root) | App Installer allows for Windows 10 apps to be installed by double clicking the app package. |
| [Install apps with the WinAppDeployCmd.exe tool](install-universal-windows-apps-with-the-winappdeploycmd-tool.md) | Windows Application Deployment (WinAppDeployCmd.exe) is a command line tool that can use to deploy a UWP app from a Windows 10 machine to any Windows 10 Mobile device. You can use this tool to deploy an app package when the Windows 10 Mobile device is connected by USB or available on the same subnet without needing Microsoft Visual Studio or the solution for that app. This article describes how to install UWP apps using this tool. |
| [Set up automated builds for your UWP app](auto-build-package-uwp-apps.md) | If you want to package your app as part of an automated build process, this topic shows you how to use Visual Studio Team Services (VSTS) to do it. |
| [App capability declarations](app-capability-declarations.md) | Capabilities must be declared in your app's [package manifest](/uwp/schemas/appxpackage/appx-package-manifest) to access certain API or resources like pictures, music, or devices like the camera or the microphone. |
| [Download and install package updates from the Store](self-install-package-updates.md) | Your UWP app can programmatically check for package updates and install the updates. Your app can also query for packages that have been marked as mandatory in Partner Center and disable functionality until the mandatory update is installed.  |