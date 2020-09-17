---
title: Windows 10 on ARM
description: This article provides an overview of how experiences and apps will run on ARM, what the limitations are, and where you can go to learn more.
ms.date: 05/22/2020
ms.topic: article
keywords: windows 10 s, always connected, ARM, ARM64, x86 emulation
ms.localizationpriority: medium
---

# Windows 10 on ARM
Originally Windows 10 (as distinguished from Windows 10 Mobile) could run only on PCs that were powered by x86 and x64 processors. Now, Windows 10 desktop can run on machines that are powered by ARM64 processors with the Fall Creators Update or newer. The power-saving nature of the ARM CPU architecture allows these PCs to have all-day battery life and support for mobile data networks. These PCs will provide great application compatibility and allow you to run your existing x86 win32 applications unmodified. For more information or a demo, look at the [Channel 9 video for the Always Connected PC](https://channel9.msdn.com/Events/Build/2017/P4171).

We use the term *ARM* here as a shorthand for PCs that run the desktop version of Windows 10 on ARM64 (also commonly called *AArch64*) processors.  We use the term *ARM32* here as a shorthand for the 32-bit ARM architecture (commonly called *ARM* in other documentation).

## Apps and experiences on ARM

### Built-in Windows 10 experiences, apps and drivers
The built-in Windows 10 experiences such as Edge, Cortana, Start menu, and Explorer are all native and run as ARM64. This also includes all the device drivers such as graphics, networking, or the hard disk. This ensures that you get the best user experience and battery life out of your device running at the full native speed of the Qualcomm Snapdragon processor.

### Universal Windows Platform (UWP) apps
Windows 10 on ARM runs all x86, ARM32, and ARM64 [UWP apps](../get-started/universal-application-platform-guide.md) from the Microsoft Store. ARM32 and ARM64 apps run natively without any emulation, while x86 apps run under emulation. If you are a UWP developer, please ensure that you submit an ARM package for your app as this will provide the best user experience for the device. For more information see [App package architectures](/windows/msix/package/device-architecture).

>[!NOTE]
> To build your UWP application to natively target the ARM64 platform, you must have Visual Studio 2017 version 15.9 or later, or Visual Studio 2019. For more information, see [this blog post](https://blogs.windows.com/buildingapps/2018/11/15/official-support-for-windows-10-on-arm-development).


>[!IMPORTANT]
> Windows 10 on ARM supports x86, ARM32, and ARM64 UWP apps from Store on ARM64 devices. When a user downloads your UWP app on an ARM64 device, the OS will automatically install the optimal version of your app that is available. If you submit x86, ARM32, and ARM64 versions of your app to the Store, the OS will automatically install the ARM64 version of your app. If you only submit x86 and ARM32 versions of your app, the OS will install the ARM32 version. If you only submit the x86 version of your app, the OS will install that version and run it under emulation. For more information about architectures, see [App package architectures](/windows/msix/package/device-architecture).

### Win32 apps
In addition to UWP apps, Windows 10 on ARM can also run your x86 Win32 apps unmodified, with good performance and a seamless user experience, just like any PC. These x86 Win32 apps don’t have to recompiled for ARM and don’t even realize they are running on an ARM processor. Note that 64-bit x64 Win32 apps are not supported, but the vast majority of apps have x86 versions available.  When given the choice of app architecture, just choose the 32-bit x86 version to run the app on a Windows 10 on ARM PC.

## Downloads

Visual Studio 2019 provides several tools downloads for Windows 10 on ARM. Users stil using Visual Studio 2017 can use the installer to find and install comparable tools and packages. Note that to follow these steps, you must be using Visual Studio 2019.

### Visual C++ Redistributable

The Visual C++ Redist package is available for ARM apps. Visit the [Visual Studio downloads page](https://visualstudio.microsoft.com/downloads/) scroll down to **All downloads**, open **Other tools and Frameworks**, then navigate to the **Microsoft Visual C++ Redistributable for Visual Studio 2019** entry. Select the **ARM64** radio button, then **Download**.

### Remote Tools

Remote Tools for Visual Studio are available for ARM apps. Visit the [Visual Studio downloads page](https://visualstudio.microsoft.com/downloads/) scroll down to **All downloads**, open **Tools for Visual Studio 2019**, then navigate to the **Remote Tools for Visual Studio 2019** entry. Select the **ARM64* radio button, then **Download**.


## In this section
|Topic | Description |
|-----|-----|
|[How x86 emulation works on ARM](apps-on-arm-x86-emulation.md)|An overview detailing how x86 apps are emulated on ARM.|
|[Troubleshooting x86 apps on ARM](apps-on-arm-troubleshooting-x86.md)|Common issues with x86 apps when running on ARM, and how to fix them. |
|[Troubleshooting ARM apps on ARM](apps-on-arm-troubleshooting-arm32.md)|Common issues with ARM32 and ARM64 apps when running on ARM, and how to fix them. |
|[Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md)|Guidance for adjusting compatibility settings if your app isn't working correctly on ARM. |

## Related topics
|Topic | Description |
|-----|-----|
|[Building ARM64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers)|Instructions for building an ARM64 driver. |
| [Debugging x86 apps on ARM](/windows-hardware/drivers/debugger/debugging-arm64) | Guidance for debugging x86 apps on ARM. |