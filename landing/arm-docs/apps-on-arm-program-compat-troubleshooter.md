---
title: Program Compatibility Troubleshooter on Arm
description: Guidance for adjusting compatibility settings if your app isn't working correctly on Arm
ms.date: 02/23/2024
ms.topic: article
ms.service: windows
ms.subservice: arm
author: mattwojo
ms.author: mattwoj
ms.reviewer: marcs
---

# Program Compatibility Troubleshooter on Arm

PCs powered by Arm provide great application compatibility and allow you to run your existing unmodified x86 win32 applications. Arm apps run natively without any emulation, while x86 and x64 apps run under emulation on Arm devices.

However, sometimes the emulation performs optimizations that don't result in the best user experience. You can use the **Program Compatibility Troubleshooter** to toggle emulation settings for your x86 or x64 app, reducing the default optimizations and potentially increasing compatibility.

## Start the Program Compatibility Troubleshooter

You start the [Program Compatibility Troubleshooter](https://support.microsoft.com/help/15078/windows-make-older-programs-compatible) manually in the same way on any Windows PC: right-click an executable (.exe) file and select **Troubleshoot compatibility**. You will then have the option to *Try recommended settings* to test run the program using recommended compatibility settings or *Troubleshoot program* to choose compatibility settings based on specific problems that you've noticed.

![Screenshot of the Troubleshoot compatibility options.](images/Capture4.png)

If you select **Troubleshoot program**, you will have the option to select:

- The program worked in earlier versions of Windows but won't install or run now
- The program opens but doesn't display correctly
- The program requires additional permissions
- I don't see my problem listed

![Screenshot of the What problems do you notice options.](images/Capture5.png)

All options enable the settings that are applicable and applied on Windows Desktop PCs. In addition, the first, second, and fourth options apply the `Disable application cache` and `Disable hybrid execution mode` emulation settings. (See the emulation settings table below for descriptions.)

## Toggling emulation settings

> [!WARNING]
> Changing emulation settings may result in your application unexpectedly crashing or not launching at all.

You can toggle emulation settings by right-clicking the executable and selecting **Properties**.

On ARM, a section titled **Windows 10 on ARM** or **Windows 11 on ARM** will be available in the **Compatibility** tab. Select **Change emulation settings** to launch an Emulations Properties window.

![Change emulation settings screenshot](images/Capture.png)

This Emulations Properties window provides two ways to modify emulation settings. You may select a pre-defined group of emulation settings or select the **Use advanced settings** option to enable choosing individual settings.

The following emulation settings reduce performance optimizations in favor of quality and can be used to experiment with testing your x86 or x64 app's compatibility when running in Windows on Arm.

![Change emulation settings screenshot2](images/Capture2.png)

Select **Use advanced settings** to choose individual settings as described in this table.

| Emulation setting | Result |
| ----------------- | ----------- |
| Disable application cache | The operating system will cache compiled blocks of code to reduce emulation overhead on subsequent executions. This setting requires the emulator to recompile all app code at runtime. |
| Disable hybrid execution mode | Compiled Hybrid Portable Executable (CHPE), binaries are x86 compatible binaries that include native Arm64 code to improve performance, but that may not be compatible with some apps. This setting forces use of x86-only binaries. |
| Additional lightweight emulation protections | A catch-all update affecting things like volatile metadata which can impact performance when running an x86 or x64 app in emulation. |
| Strict self-modifying code support | Enable this to ensure that any self-modifying code is correctly supported in emulation. The most common self-modifying code scenarios are covered by the default emulator behavior. Enabling this option significantly reduces performance of self-modifying  code during execution. |
| Disable RWX page performance optimization | This optimization improves the performance of code on readable, writable, and executable (RWX) pages, but may be incompatible with some apps. |
| Disable JIT optimization (x64 apps only) | This is no longer used and will be removed in future versions of the Troubleshooter. |
| Disable floating point optimization (x64 apps only) | Check to emulate x87 floating point at a full 80-bit precision, but at  a performance cost. x87 is a floating-point coprocessor used in some older x86 processors to perform floating-point arithmetic using an 80-bit floating point format with higher precision than the 32-bit or 64-bit format. |

You can also change how the application uses multiple CPU cores, selecting between Fast, Strict multi-core operation, Very strict, or Force single-core operation. Test your apps emulation when running Windows on Arm with these settings if you notice compatibility issues.

![Multi-core settings screenshot](images/Capture3.png)

These settings change the number of memory barriers used to synchronize memory accesses between cores in apps during emulation. **Fast** is the default mode, but the **strict** and **very strict** options will increase the number of barriers. This slows down the app, but reduces the risk of app errors. The **single-core** option removes all barriers but forces all app threads to run on a single core.

If changing a specific setting resolves your issue, please email *woafeedback@microsoft.com* with details, so that we may incorporate your feedback.
