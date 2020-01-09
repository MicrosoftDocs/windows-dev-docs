---
title: Native Android development on Windows
description: Get started developing Android native apps on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.localizationpriority: medium
ms.date: 02/19/2020
---

# Native Android development on Windows

The following is a guide to the various workflows available to you for doing Android development using the Windows operating system.

## Install Android Studio

Android Studio is the official integrated development environment for Google's Android operating system, built on JetBrain's ItelliJ IDEA software. Download the latest version of Android Studio at https://developer.android.com/studio.

- If you downloaded an .exe file (recommended), double-click to launch it.
- If you downloaded a .zip file, unpack the ZIP, copy the android-studio folder into your Program Files folder, and then open the android-studio > bin folder and launch studio64.exe (for 64-bit machines) or studio.exe (for 32-bit machines).

Follow the setup wizard in Android Studio and install any SDK packages that it recommends. As new tools and other APIs become available, Android Studio tells you with a pop-up, or you can check for updates by selecting **Help** > **Check for Update**.


## Java or Kotlin

## Minimum API Level

## Emulating an Android device

## 

Where to find the configuration file...

Both configuration files are stored in the configuration folder for Android Studio. The name of the folder depends on your Studio version. For example, Android Studio 3.3 has the folder name AndroidStudio3.3. The location of this folder depends on your operating system:

Windows: %USERPROFILE%\.CONFIGURATION_FOLDER

You can also use the following environment variables to point to specific override files elsewhere:

STUDIO_VM_OPTIONS: set the name and location of the .vmoptions file
STUDIO_PROPERTIES: set the name and location of the .properties file
STUDIO_JDK: set the JDK with which to run Studio