---
title: Windows Subsystem for Android™️ Release Notes
description: Release notes for Windows Subsystem for Android.
author: mattwojo
ms.author: mattwoj
ms.reviewer: mousma
manager: jken
ms.topic: article
ms.date: 04/27/2022
---

# Release Notes for Windows Subsystem for Android™️

These release notes are based on updates to the Windows Subsystem for Android™️. For basic information on how to install and run Android™️ apps on Windows, see the Support article: [Installing the Amazon Appstore and Android™️ Apps](https://support.microsoft.com/windows/mobile-apps-and-the-windows-subsystem-for-android-f8d0abb5-44ad-47d8-b9fb-ad6b1459ff6c).

## Build 2204.40000.19.0
May 20, 2022

- Windows Subsystem for Android updated to Android 12.1
- Advanced networking on by default for newer x64 Windows builds
- Updated Windows Subsystem for Android Settings app: redesigned UX and diagnostics data viewer added
- Simpleperf CPU profiler recording now works with Windows Subsystem for Android 
- Windows taskbar now shows which Android apps are using microphone and location
- Improvements to Android app notifications appearing as Windows notifications
- Reduced flicker when apps are restored from minimized state
- Apps are not restarted when devices come out of connected standby on recent Windows builds
- New video hardware decoding (VP8 and VP9)
- Fixes for on-screen keyboard in apps
- Fixes for full screen Android apps and auto-hidden Windows taskbar
- Windows Subsystem for Android updated with Chromium WebView 100
- Added support for Android NetworkLocationProvider in addition to GpsLocationProvider
- Improved general stability, performance, and reliability

Known Issues:

- Instability with camera on ARM devices 
- Instability printing via Android apps
- Some apps rendered at lower resolutions may lay out incorrectly
- Some VPNs may not work with Advanced Networking. If you use a VPN and find Android apps do not have network connectivity, please disable Advanced Networking in the Windows Subsystem for Android Settings app
- Some apps that were previously available might be missing from the experience, fail to launch, or function incorrectly for various known issues. We’re working with our partners to address these issues as soon as possible. 


## Build 2203.40000.3.0
March 22, 2022.

- H.264 Video Hardware Decoding
- Networking changes enabling future improvements in the platform
- Mail Integration with Windows email clients and Android apps
- Disabled force-enabling MSAA (Multisample anti-aliasing)
- Improved scrolling in the Amazon Appstore and Kindle apps

Known Issues:

- Video playback in some apps may be choppy on certain systems.
- Coming out of connected standby, apps may be restarted.


## Public Preview Build 1.8.32837.0
February 15, 2022. Windows Subsystem for Android is available for **public preview**.

- General improvements to performance and reliability
- Full screen support for apps (via F11) 
- Taking pictures with camera metadata and encoding fixed 
- Improvements to the OAuth experience 
- Improved screen reader support for the Amazon Appstore
- Fix for issue with file attachments not displaying an email dialog correctly
- When Windows Subsystem for Android™️ Settings subsystem resources are configured for continuous mode, the subsystem will restart at Windows boot up
- Windows Subsystem for Android™️ Settings App now includes the ability to choose GPU
- Copy and paste clipboard reliability improvements 
- Fixes to camera rotation and aspect ratio
- Fixes for onscreen keyboard in apps

Known Issues:

- Apps coming out of modern standby may encounter issues
- Performance may vary when running multiple concurrent apps
