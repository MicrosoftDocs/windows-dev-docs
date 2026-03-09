---
title: Windows SDK downloads archive
description: Older downloads for the Windows SDK, including emulators
ms.topic: article
ms.date: 12/15/2025
keywords: windows win32, windows app development, Windows App SDK, windows sdk
ms.localizationpriority: medium
---

# Windows SDK downloads archive

This page contains SDK releases and updates for earlier Windows and Windows Phone platform versions, as well as emulator releases supporting development and UX testing for mobile device experiences that are **no longer being serviced**. For the latest editions of the Windows SDK, see the [Windows SDK overview](./index.md).

> [!WARNING]
> These unserviced installers may still be susceptible to [CVE-2024-29187](https://msrc.microsoft.com/update-guide/vulnerability/CVE-2024-29187), use at your own risk. Installation of legacy SDKs and emulators is not recommended.

#### Windows 11

| Release  | Downloads |
| ----- | -------------------------------------- |
| **Windows SDK for Windows 11 (10.0.26100.1742)** <br/> September 2024. | [Installer](//go.microsoft.com/fwlink/?linkid=2286561) <br/> [ISO](//go.microsoft.com/fwlink/?linkid=2286663) |
| **Windows SDK for Windows 11 (10.0.22621.2428)** <br/> Released in October 2023. | [Installer](//go.microsoft.com/fwlink/?linkid=2250105) <br/> [ISO](//go.microsoft.com/fwlink/?linkid=2249825) |
| **Windows SDK for Windows 11 (10.0.22621.1778)** <br/> May 2023. | [Installer](//go.microsoft.com/fwlink/?linkid=2237387) <br/> [ISO](//go.microsoft.com/fwlink/?linkid=2237510) |
| **Windows SDK for Windows 11 (10.0.22621.755)** <br/> Released as part of Windows 11, version 22H2. Includes servicing update 10.0.22000.755 on October 25, 2022: <ul><li>Includes ARM64 support for the Visual Studio 17.4 release</li></ul> | [Installer](//go.microsoft.com/fwlink/p/?linkid=2196241) <br/> [ISO](//go.microsoft.com/fwlink/p/?linkid=2196240) |
| **Windows SDK for Windows 11 (10.0.22000.194)** <br/> Released in conjunction with Windows 11. Includes servicing update 10.0.22000.832 on July 29, 2022: <ul><li>Critical updates for developers building Arm64EC applications</li></ul> | [Installer](https://go.microsoft.com/fwlink/?linkid=2173743) <br/> [ISO](https://go.microsoft.com/fwlink/?linkid=2173746) |


#### Windows 10

| Release | Description | Downloads |
| ----- | ------------| ---------------------------------------------------------------- |
| Windows 10 SDK version 2104 (10.0.20348.0) | Primarily intended for Windows Server development. For desktop development, see the [release notes](./release-notes.md) for changes you may benefit from by updating. | [Installer](https://go.microsoft.com/fwlink/?linkid=2164145) <br/> [ISO](https://go.microsoft.com/fwlink/?linkid=2164360) |
| Windows 10 SDK, version 2004 (10.0.19041.0) | Released in conjunction with Windows 10, version 2004. Includes servicing updates 10.0.19041.685. Updated 12/16/20 <ul><li>Resolved unpredictable and hard to diagnose crashes when linking both umbrella libraries and native OS libraries (for example, onecoreuap.lib and kernel32.lib)</li><li>Resolved issue that prevented AppVerifier from working</li><li>Resolved issue that caused WACK to fail with “Task failed to enable HighVersionLie”</li></ul>| [Installer](https://go.microsoft.com/fwlink/?linkid=2120843) <br/> [ISO](https://go.microsoft.com/fwlink/?linkid=2120735) |
| Windows 10 SDK, version 1903 (10.0.18362.1) | Released in conjunction with Windows 10, version 1903. | [Installer](https://go.microsoft.com/fwlink/?linkid=2083338) <br/> [ISO](https://go.microsoft.com/fwlink/?linkid=2083448) |
| Windows 10 SDK, version 1809 (10.0.17763.0) | Released in conjunction with Windows 10, version 1809. Includes servicing updates 10.0.17763.132.<ul><li>Addressed issue where Windows App Certification Kits crashes for any app that declares more than one Device Family in manifest</li><li>Addressed issue where Windows App Certification Kit failed to deploy MSIX bundle</li><li>Addressed issue where UWP projects that used multiple MinTargetPlatformVersions would fail with a build error related to XAML.</li><li>Addressed issue where deriving from SelectorAutomationPeer in IDL raises MIDL error "Unsupported array pattern detected."</li></ul>| [Installer](https://go.microsoft.com/fwlink/p/?LinkID=2033908) <br/> [ISO](https://go.microsoft.com/fwlink/p/?LinkID=2033686) |
| Windows 10 SDK, version 1803 (10.0.17134.12) | Released in conjunction with the Windows 10 April Update (version 1803). | [Installer](https://go.microsoft.com/fwlink/p/?linkid=870807) |
| Windows 10 SDK (10.0.16299.91) and Microsoft Emulator for Windows 10 mobile (10.0.15254.1) | Released in conjunction with the Windows 10 Fall Creators Update (version 1709). | [Installer](https://go.microsoft.com/fwlink/p/?linkid=864422) <br/> [Emulator](https://go.microsoft.com/fwlink/p/?linkid=615095) |
| Windows 10 SDK (10.0.15063.468) and Microsoft Emulator for Windows 10 mobile (10.0.15254.1) | Released in conjunction with the Windows 10 Creators Update (version 1703).<ul><li>Addressed issue where build errors were encountered when including events.h</li><li>Back ported tests to App Certification Kit</li><li>Addressed issue where WinAppDeploycmd tool fails to connect to phone via USB</li><li>Addressed issue where UWP Remote Deployment Pipeline silently swallows SMB exceptions.</li></ul> | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=845298) <br/> [Emulator](https://go.microsoft.com/fwlink/p/?LinkId=846267) |
| Windows 10 SDK (10.0.14393.795) and Microsoft Emulator for Windows 10 mobile (10.0.14393.0) | Released in conjunction with the Windows 10 Anniversary Edition (version 1607).<ul><li>Addressed issue where developers could not build UWP apps on Windows 7 because MRMSupport.dll failed to load.</li><li>Addressed issue where MidlRT and MDMerge failed to run on Windows 7</li><li>Addressed issue where SDK setup failed to install on Windows</li><li>Addressed issue where deploying a legacy Store app to a 8.1 Phone caused Visual Studio to crash</li><li>Addressed issue where application data was not preserved across remote debugging sessions when apps were getting un-registered.</li></ul> | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=838916) <br/> [Emulator](https://go.microsoft.com/fwlink/p/?LinkId=822928) |
| Windows 10 SDK (10.0.10586.212) and Microsoft Emulator for Windows 10 mobile (10.0.10586.11) | Released in conjunction with Windows 10, version 1511 | [Installer](https://go.microsoft.com/fwlink/p/?LinkID=698771) <br/> |
| Windows 10 SDK (10.0.10240) and Microsoft Emulator for Windows 10 mobile (10.0.10240). | Released in conjunction with Windows 10, version 1507. <br/>**Note:** The version will display as 10.0.26624 during setup. | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=619296) <br/> |
| Microsoft HoloLens Emulator | Run apps on Windows Holographic in a virtual machine without a HoloLens. This installation also includes holographic DirectX project templates for Visual Studio | [Emulator](/windows/mixed-reality/develop/advanced-concepts/using-the-hololens-emulator) |

#### Earlier releases

| Release  | Description | Downloads |
| ----- | ------------| ---------------------------------------------------------------- |
| Windows 8.1 SDK | Released in October 2013, this SDK can be used to create Windows apps (for Windows 8.1 or later)<br/> using web technologies, native, and managed code; or desktop apps that use the native or managed programming model. | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=323507) |
| Windows Phone 8.1 development tools | The Windows Phone 8.1 development tools are installed with Visual Studio Community 2015 with Update 2.<br/> Features introduced in Update 2 include new emulators and universal app templates. | [Install Visual Studio](https://go.microsoft.com/fwlink/p/?LinkId=534599) |
| Windows Phone 8.1 Emulators | The Windows Phone 8.1 Emulators package adds six emulator images to an existing installation of Visual Studio 2013<br/> so you can test how apps will work on phones running Windows Phone 8.1. (Requires Visual Studio 2013 with Update 2 or later.) | [Emulators](https://go.microsoft.com/fwlink/p/?LinkId=517214) |
| Windows Phone 8.1 Update and Emulators | Supports use of emulators in test scenarios for phones running Windows Phone 8.1 Update 1.<br/> (Requires Visual Studio 2013 with Update 2 or later.)| [Emulators](https://www.microsoft.com/download/details.aspx?id=43719) |
| Windows 8 SDK | Released in November 2012, this SDK can be used to create Windows apps (for Windows 8 or earlier)<br/> using web technologies, native, and managed code; or desktop apps that use the native or managed programming model. | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=226658) |
| Windows Phone SDK 8.0 | Included in Visual Studio Community 2015 to support developing apps for Windows Phone 8 devices. | [Install Visual Studio](https://go.microsoft.com/fwlink/p/?LinkId=534599) <br/> [More languages](https://visualstudio.microsoft.com/vs/older-downloads/) |
| Windows Phone SDK 8.0 Update 3 Emulators | Adds five new emulator images to an existing installation of Windows Phone SDK 8.0. With this update installed,<br/> you can test how your app will run on devices that have Update 3 (version 8.0.10492 or later) of Windows Phone 8.<br/> This update requires either Visual Studio 2012 with Windows Phone SDK 8.0 and Update 4 or later, or Visual Studio 2013<br/> with the optional Windows Phone SDK 8.0 option selected during setup. | [Emulators](https://go.microsoft.com/fwlink/p/?LinkId=389663) <br/> [More languages](https://go.microsoft.com/fwlink/p/?LinkId=389659) |
| Windows Phone SDK 8.0 Update for Windows Phone 8.0.10322 | Adds four new emulator images to an existing installation of Windows Phone SDK 8.0. This update requires<br/> either Visual Studio 2012 with Windows Phone SDK 8.0 and Update 4 or later, or Visual Studio 2013<br/> with the optional Windows Phone SDK 8.0 option selected during setup. | [Emulators](https://go.microsoft.com/fwlink/p/?LinkId=389663) <br/> [More languages](https://go.microsoft.com/fwlink/p/?LinkId=310186) |
 Windows Phone SDK Update for Windows Phone 7.8 | Adds two new emulator images to an existing Windows Phone SDK installation. This update supports Windows Phone SDK 7.1<br/> and Windows Phone SDK 8.0. With this update, use Windows Phone 8 Start screen experience in your Windows Phone 7.5 apps.<br/> You also can test how your apps will run on Windows Phone 7.8 devices. | [Emulators](https://go.microsoft.com/fwlink/p/?LinkId=276663) <br/> [More languages](https://go.microsoft.com/fwlink/p/?LinkId=276662) |
| Windows Phone SDK 7.1 | Tools to help you develop apps for Windows Phone 7.5 and Windows Phone 7.0 devices. | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=258412) <br/> [More languages](https://go.microsoft.com/fwlink/p/?LinkId=226403) |
| Windows Phone SDK 7.1.1 Update | Brings additional functionality to Windows Phone SDK 7.1. With this update, it’s easier to develop apps and<br/> games that are optimized to run on 256-MB devices. | [Installer](https://go.microsoft.com/fwlink/p/?LinkId=258413) <br/> [More languages](https://go.microsoft.com/fwlink/p/?LinkId=242824) |
| Windows SDK for Windows 7 and .NET Framework 4 | Released in June 2010, this SDK can be used to develop applications for Windows 7, Windows XP, Windows Server 2003,<br/> Windows Vista, Windows Server 2008, and .NET Framework versions 2.0, 3.0, 3.5 SP1, and 4.0. | [ISO](https://go.microsoft.com/fwlink/?LinkID=191424) |
