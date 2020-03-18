---
title: Improve performance speed by updating Defender settings
description: Learn how to improve performance speed and build times by updating Windows Defender settings to exclude checking specified file types.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.date: 02/19/2020
---

# Improve performance speed and build time by updating Windows Defender security settings

In Windows 10, version 1703 and later, the [Windows Defender Antivirus](https://docs.microsoft.com/windows/security/threat-protection/windows-defender-antivirus/windows-defender-security-center-antivirus) app is part of Windows Security. Windows Defender aims to keep your PC safe with built-in, real-time protection against viruses, ransomware, spyware, and other security threats.

**However**, Windows Defender's real-time protection will also dramatically slow file system access and build speed when developing Android apps.

During the Android build process, many files are created on your computer. With antivirus real-time scanning enabled, the build process will halt each time a new file is created while the antivirus scans that file.

Fortunately, Windows Defender has the capability to exclude files, project directories, or file types that you know to be secure from it's antivirus scanning process.

> [WARNING]
> To ensure that your computer is safe from malicious software, you should not completely disable real-time scanning or your Windows Defender antivirus software.

![Windows Defender Add Exclusion screenshot](../images/windows-defender-exclusions.png)

To improve your Android build speed, add exclusions in the [Windows Defender Security Center](windowsdefender://) by:

1. Select the Windows menu **Start** button
2. Enter **Windows Security**
3. Select **Virus and threat protection**
4. Select **Manage settings** under **Virus & threat protection settings**
5. Scroll to the **Exclusions** heading and select **Add or remove exclusions**
6. Select **+ Add an exclusion**. You will then need to choose whether the exclusion you wish to add is a **File**, **Folder**, **File type**, or **Process**.

The following list shows the default location of each Android Studio directory that you should exclude from real-time scanning:

- Gradle cache: `%USERPROFILE%\.gradle`
- Android Studio projects: `%USERPROFILE%\AndroidStudioProjects`
- Android SDK: `%USERPROFILE%\AppData\Local\Android\SDK`
- Android Studio system files: `%USERPROFILE%\.AndroidStudio<version>\system`

For more information on adding antivirus scanning exclusions, including how to customize directory locations for Group Policy controlled environments, see the [Antivirus Impact](https://developer.android.com/studio/intro/studio-config#antivirus-impact) section of the Android Studio documentation.

<!-- Todo: Do these Windows Defender exclusions apply for the Xamarin or React cross-plat build speeds for Android development?  -->

<!-- Should we include any other recommended exclusions?? For example, Aaron Stannard was very excited about the improvements ReSharper stuggested to exclude R#  and VS: https://twitter.com/Aaronontheweb/status/1227654898843516928?s=20 -->
