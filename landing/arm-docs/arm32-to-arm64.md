---
title: Update app architecture from Arm32 to Arm64
description: Guidance for changing apps with Arm32 architecture, a default for older UWP apps, to the updated Arm64 architecture using Visual Studio so that your app will work on the latest Windows on Arm devices.
ms.date: 09/27/2023
ms.topic: article
ms.prod: windows
ms.technology: arm
author: mattwojo
ms.author: mattwoj
ms.reviewer: marcs
---

# Update app architecture from Arm32 to Arm64

This guide will cover the recommended steps for changing an existing app with support for 32-bit Arm platform architecture over to the more updated 64-bit Arm architecture by adding the necessary configuration using Visual Studio. This update will help your app to run on the latest Windows on Arm devices which use 64-bit Arm (ARM64/AArch64) processors.

This topic is relevant for UWP apps that do not have an ARM64 target. Older UWP project templates generated an ARM32 (or AArch32) target, but did not include support for ARM64 (AArch64).

To check the current Solution Platform for your app to see if ARM64 is present, open your app project code in Visual Studio and in the "Solution Platforms" drop-down menu on the Standard toolbar, select **Configuration Manager...** (also available in the Build menu) where you will be able to view the list of Solution Platforms and confirm whether ARM64 is present.

> [!NOTE]
> Windows devices running on an Arm processor *(for example, Snapdragon processors from Qualcomm)* will no longer support AArch32 (Arm32). This change impacts Universal Windows Platform apps that presently target AArch32 (Arm32). [Support for 32-bit Arm versions of applications will be removed in a future release of Windows 11.](https://www.microsoft.com/windows/windows-11-specifications#table3). System binaries for ARM32 support (present in the *sysarm32* folder) will also be removed. After this change, for the small number of applications affected, app features might be different and you might notice a difference in performance. Therefore, we recommend updating your targeted platforms to AArch64 (Arm64), which is supported on all Windows on Arm devices, as soon as possible in order to ensure your customers can continue to enjoy the best possible experience. Follow the guidance on this page to update your applications to AArch64 (Arm64).

## Add an Arm64 configuration to your project

To add an ARM64 solution platform to your existing app project code:

1. Open your solution (project code) in Visual Studio (Visual Studio 2017 version 15.9 or newer is required).
2. In the "Solution Platforms" drop-down menu on the Standard toolbar (or in the "Build" menu), select **Configuration Manager...**
3. Open the "Active solution platform" drop-down menu and select **<new...>**.
4. In the "Type or select the new platform" drop-down menu, select **ARM64** and ensure that the "Copy settings from" value is set to **ARM** with the "Create new project platforms" checkbox enabled, then select **OK**.

## Build your Arm64 Solution

Once you have added the Arm64 solution platform to your existing project or solution, if you want to confirm that the Arm64 version of your app builds correctly, close the "Active solution platform" window and change the build setting from **Debug** to **Release**. In the "Build" drop-down menu, select **Rebuild Solution** and wait for the project to rebuild. You should receive a "Rebuild All succeeded" output. If not, see the [Troubleshooting](#troubleshooting) section below.

*(Optional)*: Check that your app binary is now built for Arm64 architecture by opening your project directory in PowerShell (right-click your app project in Visual Studio Solution Explorer and select **Open in Terminal**). Change directories so that your project's new `bin\ARM64\Release` directory is selected. Enter the command: `dumpbin .\<appname>.exe` (replacing `<appname>` with the name of your app). Then enter the command: `dumpbin /headers .\<appname>.exe`. Scrolling up in your terminal's output results, find the **`FILE HEADER VALUES`** section and confirm the first line is `AA64 machine (ARM64)`.

## Publish your updated app in the Microsoft Store

Once you have built an Arm64 version of your app by following the configuration steps above, you can update your existing app package in the Microsoft Store by visiting your [Partner Center dashboard](https://partner.microsoft.com/dashboard) and adding the newly built ARM64 binaries to the submission. (It is an option to also remove the previous ARM32 binaries).

(optionally) removing the previous Arm32 binaries. For more information on options, see [Publish your app in the Microsoft Store](/windows/apps/publish/publish-your-app/overview).

## Troubleshooting

If you run into issues while porting your Arm32 app to Arm64, here are a few common solutions.

### A dependency not compiled for ARM64 is blocking you from a successful build

If you can’t build due to a dependency, whether internal, from a 3rd party, or from an open-source library, you will need to either find a way to update that dependency to support ARM64 architecture or remove it.

- For internal dependencies, we recommend rebuilding the dependency for ARM64 support.

- For 3rd party dependencies, we recommend filing a request for the maintainer to rebuild with ARM64 support.

- For open source dependencies, we recommend first checking [vcpkg](https://vcpkg.io/en/index.html) to see if a newer version of the dependency that includes ARM64 support exists that you can update to. If no update exists, consider contributing the addition of ARM64 support to the package yourself. Many open source maintainers would be thankful for the contribution.

- The last choice would be to remove and/or replace the dependency on your app project.

### Need assistance? Leverage our App Assure service

[Learn more about App Assure compatibility assistance](https://www.microsoft.com/fasttrack/microsoft-365/app-assure) to help with porting your Windows app or driver to Arm64. To register and connect with App Assure, visit [aka.ms/AppAssureRequest](https://aka.ms/AppAssureRequest) or send an email to [achelp@microsoft.com](mailto:achelp@microsoft.com) to submit your request for Windows on Arm compatibility support.
