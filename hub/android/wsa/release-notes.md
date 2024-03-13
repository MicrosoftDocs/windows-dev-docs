---
title: Windows Subsystem for Android™️ Release Notes
description: Release notes for Windows Subsystem for Android.
author: mattwojo
ms.author: mattwoj
ms.reviewer: mousma
manager: jken
ms.topic: article
ms.date: 03/05/2024
---

# Release Notes for Windows Subsystem for Android™️

> [!IMPORTANT]
> Windows Subsystem for Android™️ and the Amazon Appstore will no longer be available in the Microsoft Store after March 5, 2025. [Learn more](index.md).

These release notes are based on updates to the Windows Subsystem for Android™️. For basic information on how to install and run Android™️ apps on Windows, see the Support article: [Installing the Amazon Appstore and Android™️ Apps](https://support.microsoft.com/windows/mobile-apps-and-the-windows-subsystem-for-android-f8d0abb5-44ad-47d8-b9fb-ad6b1459ff6c).

## Build 2304.40000.3.0

June 1, 2023
- Package verification for apps on WSA: Android apps are scanned using anti-virus software installed on Windows prior to app installation.
- Ability for users to configure how much memory to assign to Android
- Android apps will be launched when a user opens the supported app link from any app (Android AppLink support)
- Linux kernel updated to 5.15.94
- Improvements to platform reliability and performance

## Build 2303.40000.3.0

April 11, 2023

- Picture-in-picture mode now supported
- A new "Partially running" system setting added to WSA Settings app, which runs the subsystem with minimal resources but apps launch quicker than "As needed" mode
- Linux kernel updated to 5.15.78
- Improvements to platform reliability
- Android 13 security updates

## Build 2302.4000

March 15, 2023

- Stability improvements to graphics card selection
- Updates to the Windows Subsystem for Android Settings app to include performance options for graphics cards
- Docking and undocking with external monitors issues fixed with the subsystem
- Fixes to apps with audio buffer issues
- Android 13 security updates

## Build 2301.40000.4.0

February 9, 2023

- Improved audio input latency and reliability
- Improvements to camera experience (camera metadata now exposed to camera apps)
- Improvements to framerate performance: certain benchmarks have improved by 10%-20% on ARM and 40%-50% on x64
- Fixed zooming out in apps using touchpad or mouse
- Improvements to platform reliability
- Using latest Chromium WebView to version 108
- Synchronizing global microphone and camera privacy toggles between Windows and Android apps
- Android 13 security updates

## Build 2211.40000.11.0

January 10, 2023

- Windows Subsystem for Android updated to Android 13
- Improvements in boot performance
- Improvements to mouse click input
- Improvements in clipboard stability
- Improvements to application resizing
- Reliability improvements to media files opening in Windows
- Jumplist entries for applications supporting app shortcuts

## Build 2210.40000.7.0

November 17, 2022

- Enhancement of audio recording quality
- Enhancement of OAuth scenarios
- Support for MPEG2 decoding
- Improvements to the camera experience when the device is not equipped with a camera
- Improvements in input reliability
- Chromium update to 106

## Build 2209.40000.26.0

October 20, 2022

- Improvements to the Camera HAL
- Improvements to clipboard stability
- Improvements to multi-threaded (>8 core) performance
- Improved security for graphic streaming
- Reliability improvements for package launches
- Security updates for ANGLE and GSK
- Annotated telemetry with package installation sources
- Window with legal information has been fixed
- Security updates to the Linux kernel
- Enhancements to platform stability
- Updated to Chromium WebView 105

## Build 2208.40000.4.0

September 15, 2022

- Reliability fixes for App Not Responding (ANR) errors
- Improvements to input compatibility shims
- Improvements to scrolling (smoothness) in apps
- Usability Improvements to the Windows Subsystem for Android Settings app
- Startup performance improvements
- Fixed crashes when copying and pasting extremely large content
- UX improvements for the game controls dialog
- Improvements to networking
- General graphics improvements
- Improvements for gamepad when using multiple apps
- Improved performance of uninstalling apps
- Fixed video playback issue for apps
- Updated to Chromium WebView 104
- Linux kernel security updates

## Build 2207.40000.8.0

August 31, 2022

- New compatibility shim to allow apps to maintain aspect ratio but still support resize
- Accessibility improvements to the Windows Subsystem for Android Settings app
- New compatibility shims in the Windows Subsystem for Android Settings app
- Fixed problems with restarting apps
- Apps that update toast notifications instead of using progress toasts have better behavior
- Game controls user education dialog for apps with compatibility shims enabled
- Improvements with handling VPN
- Scrollbar fix for Windows Subsystem for Android Settings compatibility page
- User crash data and system app crash data is now being reported
- “No internet available” toast notification is now suppressed
- Custom Android toasts now render correctly
- Amazon Appstore 60.09 update
- Android security update
- Improved reliability

## Build 2206.40000.15.0

August 2, 2022

- New suite of shims available to toggle in the Windows Subsystem for Android Settings app which enables better experiences in several apps
- Compatibility for games with joysticks (mapped to WASD)
- Compatibility for gamepad in games
- Compatibility for aiming in games with arrow keys
- Compatibility for sliding in games with arrow keys
- Scrolling improvements
- Networking improvements
- Android minimum window size defaulted to 220dp
- Improved dialog when unsupported VPN is detected
- New toggle to view/save diagnostic data in the Windows Subsystem for Android Settings app
- Security updates
- General reliability fixes, including improvements to diagnostic sizes
- Graphics improvements

## Build 2205.40000.14.0

July 6, 2022

- Enabled Advanced Networking functionality, including app access to local network devices for ARM
- VM IP address removed from Settings app. With Advanced Networking, now the IP address of the VM is the same as the host/computer IP
- Fixes for non-resizable app content on maximize or resizing
- Fixes for scrolling with mouse and trackpad in apps
- Android May Kernel patches
- Android windows marked secure can no longer be screenshotted
- Improve web browser launching
- Enable doze and app standby while charging for improved power saving
- ADB debug prompts redirected to Windows for improved security
- Updated to Chromium WebView 101
- Fixes for graphics including app flickering and graphics corruption
- Fixes for video playback
- AV1 Codec support
- Enabled IPv6 and VPN Connectivity
- Increased the performance and reliability connecting to virtual WIFI in the container
- Video playback apps can now prevent the screen from turning off in Windows

Known Issues:
- Some VPNs may not work with Advanced Networking. If you use a VPN and find Android apps do not have network connectivity, please disable Advanced Networking in the Windows Subsystem for Android Settings app

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
