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
Originally Windows 10 (as distinguished from Windows 10 Mobile) could run only on PCs that were powered by x86 and x64 processors. Now, Windows 10 desktop (Pro and S editions) can run on machines that are powered by ARM64 processors with the Fall Creators Update. The power-saving nature of the ARM CPU architecture allows these PCs to have all-day battery life and support for mobile data networks.

We use the term *ARM* here as a shorthand for PCs that run the desktop version of Windows 10 on ARM64 (also commonly called *AArch64*) processors.  We use the term *ARM32* here as a shorthand for the 32-bit ARM architecture (commonly called *ARM* in other documentation).

## Apps and Windows 10 experiences that run on ARM
This table shows the app architecture types that are supported on ARM.

> [!div class="mx-tableFixed"]
|App type                           |x86               |x64 |ARM32             |ARM64            |
|-----------------------------------|------------------|----|------------------|-----------------|
|Win32 (native)                     |:heavy_check_mark:|:x: |N/A               |N/A              |
|Desktop Bridge (native)            |:heavy_check_mark:|:x: |N/A               |N/A              |
|Win32 and Desktop Bridge .NET      |:heavy_check_mark:|:x: |N/A               |N/A              |    
|UWP                                |:heavy_check_mark:|:x: |:heavy_check_mark:|N/A              |

>[!NOTE] 
> You can see the architecture of a particular binary by using [Dumpbin.exe](https://docs.microsoft.com/en-us/cpp/build/reference/dumpbin-reference) with the **/HEADERS** flag. Dumpbin.exe is included with [Microsoft Visual Studio](https://www.visualstudio.com/).

### Built-in Windows 10 experiences, apps and components
Most built-in Windows 10 experiences like Cortana, the Start menu, and Explorer are compiled for ARM64, so they run with native performance. Some Win32 apps that come with Windows, such as notepad.exe, have also been recompiled for ARM64. Windows 10 system components contain a mixture of ARM64, ARM32, and x86 code. Compiled Hybrid Portable Executable (CHPE) components are also included in the system to improve the performance of x86 apps that require emulation.

### Universal Windows Platform (UWP) apps
Windows 10 on ARM supports running x86 and ARM32 [UWP apps](../get-started/universal-application-platform-guide.md) from the Microsoft Store by using the Windows on Windows ([WOW64](https://msdn.microsoft.com/en-us/library/windows/desktop/aa384249.aspx)) layer. ARM32 apps run native without any emulation, while x86 apps run under emulation.

>[!IMPORTANT] 
> When a user downloads a UWP app from the Microsoft Store, the ARM32 version will be installed on an ARM64 device unless only an x86 version is available. For more info about architectures, see [App package architectures](../packaging/device-architecture.md).

### Win32 apps
In addition to x86 and ARM32 UWP apps, Windows 10 on ARM can also run x86 Win32 apps. However, x64 Win32 apps are not supported.

## In this section
|Topic | Description |
|-----|-----|
|[How x86 emulation works on ARM](apps-on-arm-x86-emulation.md)|An overview detailing how x86 apps are emulated on ARM.|
|[Limitations of apps and experiences on ARM](apps-on-arm-limitations.md)|Limitations of the platform and troubleshooting steps for apps that aren't working correctly on ARM. |
|[Troubleshooting x86 apps on ARM](apps-on-arm-troubleshooting-x86.md)|Common issues with x86 apps when running on ARM, and how to fix them. |
|[Troubleshooting ARM32 apps on ARM](apps-on-arm-troubleshooting-arm32.md)|Common issues with ARM32 apps when running on ARM, and how to fix them. |
|[Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md)|Guidance for adjusting compatibility settings if your app isn't working correctly on ARM. |

## Related topics
|Topic | Description |
|-----|-----|
|[Building ARM64 Drivers with the WDK](https://docs.microsoft.com/en-us/windows-hardware/drivers/develop/building-arm64-drivers)|Instructions for building an ARM64 driver. |
| [Debugging x86 apps on ARM](https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/debugging-arm64) | Guidance for debugging x86 apps on ARM. |
