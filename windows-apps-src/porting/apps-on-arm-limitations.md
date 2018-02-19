---
title: Limitations of apps and experiences on ARM
author: msatranjr
description: Troubleshooting steps for apps that aren't working correctly on ARM.
ms.author: misatran
ms.date: 02/15/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10 s, always connected, limitations, windows 10 on ARM
ms.localizationpriority: medium
redirect_url: https://docs.microsoft.com/en-us/windows/uwp/porting/apps-on-arm-troubleshooting-x86
---

# Limitations of apps and experiences on ARM
Windows 10 on ARM has the following necessary limitations:

- **Only ARM64 drivers are supported**. As with all architectures, kernel-mode drivers, [User-Mode Driver Framework (UMDF)](https://docs.microsoft.com/en-us/windows-hardware/drivers/wdf/overview-of-the-umdf) drivers, and print drivers must be compiled to match the architecture of the OS. While ARM OS has the capabilities to emulate x86 user-mode apps, drivers implemented for other architectures (such as x64 or x86) are not currently emulated and thus not supported on this platform. Any app that works with its own custom driver would need to be ported to ARM64. In limited scenarios, the app may run as x86 under emulation but the driver portion of the app must be ported to ARM64. For more info about compiling your driver for ARM64, see [Building ARM64 Drivers with the WDK](https://review.docs.microsoft.com/en-us/windows-hardware/drivers/develop/building-arm64-drivers?branch=rs4-arm64).

- **x64 apps are not supported**. Windows 10 on ARM does not support emulation of x64 apps.

- **Certain games don’t work**. Games and apps that use a version of OpenGL later than 1.1 or that require hardware-accelerated OpenGL don’t work. In addition, games that rely on "anti-cheat" drivers are not supported on this platform.

- **Apps that customize the Windows experience may not work correctly**. Native OS components cannot load non-native components. Examples of apps that commonly do this include some input method editors (IMEs), assistive technologies, and cloud storage apps. IMEs and assistive technologies often to hook into the input stack for much of their app functionality. Cloud storage apps commonly use shell extensions (for example, icons in Explorer and additions to right-click menus); their shell extensions may fail, and if the failure is not handled gracefully, the app itself may not work at all.

- **Apps that assume that all ARM-based devices are running a mobile version of Windows may not work correctly**. Apps that make this assumption may appear in the wrong orientation, present unexpected UI layout or rendering, or failing to start altogether when they attempt to invoke mobile-only APIs without first testing the contract availability.

- **The Windows Hypervisor Platform is not supported on ARM**. Running any virtual machines using Hyper-V on an ARM device will not work.

The following table lists some common issues and offers suggestions on how to resolve them.

|Issue|Solution|
|-----|--------|
| Your app relies on a driver that isn't designed for ARM. | Recompile your x86 driver to ARM64. See [Building ARM64 Drivers with the WDK](https://docs.microsoft.com/en-us/windows-hardware/drivers/develop/building-arm64-drivers). |
| Your app is available only for x64. | If you develop for Microsoft Store, submit an ARM version of your app. For more info, see [App package architectures](../packaging/device-architecture.md). If you're a Win32 developer, distribute an x86 version of your app. |
| Your app uses an OpenGL version later than 1.1 or requires hardware-accelerated OpenGL. | x86 apps that use DirectX 9, DirectX 10, DirectX 11, and DirectX 12 will work on ARM. For more info, see [DirectX Graphics and Gaming](https://msdn.microsoft.com/en-us/library/windows/desktop/ee663274(v=vs.85).aspx). |
| Your x86 app does not work as expected. | Try using the Compatibility Troubleshooter by following guidance from [Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md). For some other troubleshooting steps, see the [Troubleshooting x86 apps on ARM](apps-on-arm-troubleshooting-x86.md) article. |
| Your x86 app does not detect that it's running on ARM. | Use [IsWow64Process2](https://msdn.microsoft.com/en-us/library/windows/desktop/mt804318(v=vs.85).aspx) to determine if your app is running on ARM. |
| Your UWP ARM32 app does not work as expected. | See [Troubleshooting ARM32 apps on ARM](apps-on-arm-troubleshooting-arm32.md) to learn how to get your app to work properly on ARM. |
