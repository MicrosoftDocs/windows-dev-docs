---
title: Windows on Arm
description: This article provides an overview of how experiences and apps will run on Arm, what the limitations are, and where you can go to learn more.
ms.date: 06/25/2021
ms.topic: article
keywords: windows, always connected, Arm, Arm64, x86 emulation
ms.localizationpriority: medium
---

# Windows on Arm

Windows can run on machines that are powered by Arm64 processors. The power-saving nature of the Arm CPU architecture allows these PCs to have all-day battery life and integrated support for mobile data networks. These PCs also provide great application compatibility and allow you to run your existing x86 win32 applications unmodified.

We use the term *Arm* here as a shorthand for PCs that run the desktop version of Windows on Arm64 (also commonly called *AArch64*) processors.  We use the term *Arm32* here as a shorthand for the 32-bit Arm architecture (commonly called *Arm* in other documentation).

## Apps and experiences on Arm

### Built-in Windows experiences, apps and drivers

The built-in Windows experiences such as Edge, Start menu, File Explorer, and more are all native and run as Arm64. This also includes all the [device drivers](/windows-hardware/drivers/install/) such as graphics, networking, or the hard disk. This ensures that you get the best user experience and battery life out of your device running at the full native speed of the [Qualcomm Snapdragon processor](https://developer.qualcomm.com/hardware/windows-on-snapdragon/snapdragon-developer-kit).

### Universal Windows Platform (UWP) apps

Windows on Arm runs all x86, Arm32, and Arm64 [UWP apps](../get-started/universal-application-platform-guide.md) from the Microsoft Store. Arm32 and Arm64 apps run natively without any emulation, while x86 apps run under emulation. If you are a UWP developer, please ensure that you submit an Arm package for your app as this will provide the best user experience for the device. For more information see [App package architectures](/windows/msix/package/device-architecture).

>[!NOTE]
> To build your UWP application to natively target the Arm64 platform, you must have Visual Studio 2017 version 15.9 or later, or Visual Studio 2019. For more information, see the blog post: [Official support for Windows 10 on Arm development](https://blogs.windows.com/buildingapps/2018/11/15/official-support-for-windows-10-on-arm-development).

>[!IMPORTANT]
> Windows on Arm supports x86, Arm32, and Arm64 UWP apps from Store on Arm64 devices. When a user downloads your UWP app on an Arm64 device, the OS will automatically install the optimal version of your app that is available. If you submit x86, Arm32, and Arm64 versions of your app to the Store, the OS will automatically install the Arm64 version of your app. If you only submit x86 and Arm32 versions of your app, the OS will install the Arm32 version. If you only submit the x86 version of your app, the OS will install that version and run it under emulation. For more information about architectures, see [App package architectures](/windows/msix/package/device-architecture).

### Win32 apps

In addition to UWP apps, Windows on Arm can also run Win32 desktop apps compiled natively for Arm64 as well as your existing x86 Win32 apps unmodified, with good performance and a seamless user experience, just like any PC. These x86 Win32 apps don’t have to be recompiled for Arm and don’t even realize they are running on an Arm processor.

### x86-64 apps

Initial support for x86-64 applications was added in build 21277, and is currently being developed further. If an app's x64 Win32 version doesn't work, the vast majority of apps do also have x86 versions available. When given the choice of app architecture, just choose the 32-bit x86 version to run the app's 32-bit version on a Windows 10 on Arm PC.

## Downloads

Visual Studio 2019 provides several tools downloads for Windows on Arm. Users still using Visual Studio 2017 can use the installer to find and install comparable tools and packages. Note that to follow these steps, you must be using Visual Studio 2019.

### Visual C++ Redistributable

The Visual C++ Redist package is available for Arm apps. Visit the [Visual Studio downloads page](https://visualstudio.microsoft.com/downloads/) scroll down to **All downloads**, open **Other tools and Frameworks**, then navigate to the **Microsoft Visual C++ Redistributable for Visual Studio 2019** entry. Select the **Arm64** radio button, then **Download**.

### Remote Tools

Remote Tools for Visual Studio are available for Arm apps. Visit the [Visual Studio downloads page](https://visualstudio.microsoft.com/downloads/) scroll down to **All downloads**, open **Tools for Visual Studio 2019**, then navigate to the **Remote Tools for Visual Studio 2019** entry. Select the **Arm64* radio button, then **Download**.

## Additional resources

- [Building Arm64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers): Instructions for building an Arm64 driver.
- [Debugging x86 apps on Arm](/windows-hardware/drivers/debugger/debugging-arm64) | Guidance for debugging x86 apps on Arm.
- [Windows 10 on Arm for developers (Microsoft Build 2018 video presentation)](/events/build-2018/brk2438)
