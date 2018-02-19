---
title: Windows 10 on ARM
author: msatranjr
description: This article provides an overview of how experiences and apps will run on ARM, what the limitations are, and where you can go to learn more.
ms.author: misatran
ms.date: 02/15/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10 s, always connected, ARM, ARM64, x86 emulation
ms.localizationpriority: medium
---

# Windows 10 on ARM
Originally Windows 10 (as distinguished from Windows 10 Mobile) could run only on PCs that were powered by x86 and x64 processors. Now, Windows 10 desktop (Pro and S editions) can run on machines that are powered by ARM64 processors with the Fall Creators Update. The power-saving nature of the ARM CPU architecture allows these PCs to have all-day battery life and support for mobile data networks. These PCs will provide great application compatibility and allow you to run your existing x86 win32 applications unmodified. E.g. Adobe reader. For more information or demo, look at the [Channel 9 video for the Always Connected PC](https://channel9.msdn.com/Events/Build/2017/P4171). 

We use the term *ARM* here as a shorthand for PCs that run the desktop version of Windows 10 on ARM64 (also commonly called *AArch64*) processors.  We use the term *ARM32* here as a shorthand for the 32-bit ARM architecture (commonly called *ARM* in other documentation).

## Apps and experiences on ARM

### Built-in Windows 10 experiences, apps and drivers
The built-in Windows 10 experiences such as Edge, Cortana, Start menu, and Explorer are all native and run as ARM64 (or ARM32). This also includes all the device drivers such as Graphics, networking or the hard disk. This ensures that you get the best user experience and battery life out of your device running at the full native speed of the Qualcomm Snapdragon processor

### Universal Windows Platform (UWP) apps
Windows 10 on ARM runs all x86 and ARM32 [UWP apps](../get-started/universal-application-platform-guide.md) from the Microsoft Store. ARM32 apps run natively without any emulation, while x86 apps run under emulation. If you are a UWP developer, please ensure that you submit an ARM package for your app as this will provide the best user experience for the device. For more information see [App package architectures](../packaging/device-architecture.md).

>[!IMPORTANT] 
> When a user downloads a UWP app from the Microsoft Store, the ARM32 version will be installed on an ARM64 device unless only an x86 version is available. For more information about architectures, see [App package architectures](../packaging/device-architecture.md).

### Win32 apps
In addition to UWP apps, Windows 10 on ARM can also run your x86 Win32 apps (e.g. Adobe Reader) unmodified, with good performance and seamless user experience, just like any PC. These x86 win32 apps don’t have to recompiled for ARM and don’t even realize they are running on ARM processor. Note that 64-bit x64 Win32 apps are not supported but the vast majority of apps all have x86 versions of their apps, so from a user perspective, just choose the 32 bit x86 installer to run on the Windows on ARM PC.

## In this section
|Topic | Description |
|-----|-----|
|[How x86 emulation works on ARM](apps-on-arm-x86-emulation.md)|An overview detailing how x86 apps are emulated on ARM.|
|[Troubleshooting x86 apps on ARM](apps-on-arm-troubleshooting-x86.md)|Common issues with x86 apps when running on ARM, and how to fix them. |
|[Troubleshooting ARM32 apps on ARM](apps-on-arm-troubleshooting-arm32.md)|Common issues with ARM32 apps when running on ARM, and how to fix them. |
|[Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md)|Guidance for adjusting compatibility settings if your app isn't working correctly on ARM. |

## Related topics
|Topic | Description |
|-----|-----|
|[Building ARM64 Drivers with the WDK](https://docs.microsoft.com/en-us/windows-hardware/drivers/develop/building-arm64-drivers)|Instructions for building an ARM64 driver. |
| [Debugging x86 apps on ARM](https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/debugging-arm64) | Guidance for debugging x86 apps on ARM. |
