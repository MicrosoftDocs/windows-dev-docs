---
title: Improve performance speed by updating Defender settings
description: Learn how to improve performance speed and build times by updating Windows Defender settings to exclude checking specified file types.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android, windows, windows defender, settings, configuration, exclusions, %USERPROFILE%, devenv.exe, performance, speed, build, gradle
ms.date: 04/28/2020
---

# Update Windows Defender settings to improve performance

This guide covers how to set up exclusions in your Windows Defender security settings in order to improve your build times and the overall performance speed of your Windows machine.

## Windows Defender Overview

In Windows 10, version 1703 and later, the [Windows Defender Antivirus](/windows/security/threat-protection/windows-defender-antivirus/windows-defender-security-center-antivirus) app is part of Windows Security. Windows Defender aims to keep your PC safe with built-in, real-time protection against viruses, ransomware, spyware, and other security threats.

**However**, Windows Defender's real-time protection will also dramatically slow file system access and build speed when developing Android apps.

During the Android build process, many files are created on your computer. With antivirus real-time scanning enabled, the build process will halt each time a new file is created while the antivirus scans that file.

Fortunately, Windows Defender has the capability to exclude files, project directories, or file types that you know to be secure from it's antivirus scanning process.

> [!WARNING]
> To ensure that your computer is safe from malicious software, you should not completely disable real-time scanning or your Windows Defender antivirus software.

## Add exclusions to Windows Defender

To improve your Android build speed, add exclusions in the [Windows Defender Security Center](windowsdefender://) by:

1. Select the Windows menu **Start** button
2. Enter **Windows Security**
3. Select **Virus and threat protection**
4. Select **Manage settings** under **Virus & threat protection settings**
5. Scroll to the **Exclusions** heading and select **Add or remove exclusions**
6. Select **+ Add an exclusion**. You will then need to choose whether the exclusion you wish to add is a **File**, **Folder**, **File type**, or **Process**.

![Windows Defender Add Exclusion screenshot](../images/windows-defender-exclusions.png)

## Recommended exclusions

The following list shows the default location of each Android Studio directory recommended to add as an exclusion from Windows Defender real-time scanning:

- Gradle cache: `%USERPROFILE%\.gradle`
- Android Studio projects: `%USERPROFILE%\AndroidStudioProjects`
- Android SDK: `%USERPROFILE%\AppData\Local\Android\SDK`
- Android Studio system files: `%USERPROFILE%\.AndroidStudio<version>\system`

These directory locations may not apply to your project if you have not used the default locations set by Android Studio or if you have downloaded a project from GitHub (for example). Consider adding an exclusion to the directory of your current Android development project, wherever that may be located.

Additional exclusions you may want to consider include:

- Visual Studio dev environment process: `devenv.exe`
- Visual Studio build process: `msbuild.exe`
- JetBrains directory: `%LOCALAPPDATA%\JetBrains\<Transient directory (folder)>`

For more information on adding antivirus scanning exclusions, including how to customize directory locations for Group Policy controlled environments, see the [Antivirus Impact](https://developer.android.com/studio/intro/studio-config#antivirus-impact) section of the Android Studio documentation.

> [!Note]
> Daniel Knoodle has set up a GitHub repo with recommended scripts to add [Windows Defender exclusions for Visual Studio 2017](https://gist.github.com/dknoodle/5a66b8b8a3f2243f4ca5c855b323cb7b#file-windows-defender-exclusions-vs-2017-ps1-L10).

## Additional resources

- [Develop Dual-screen apps for Android and get the Surface Duo device SDK](/dual-screen/android/)

- [Add Windows Defender exclusions to improve performance](./defender-settings.md)

- [Enable Virtualization support to improve emulator performance](./emulator.md#enable-virtualization-support)