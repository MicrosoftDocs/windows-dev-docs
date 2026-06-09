---
title: Adjust Emulation Settings on Arm
description: Guidance for adjusting compatibility settings if your app isn't working correctly on Arm.
ms.date: 12/05/2025
ms.topic: troubleshooting-general
ms.service: windows
ms.subservice: arm
ms.reviewer: marcs
---

# Adjust emulation settings on Arm

Windows on Arm PCs support running x86 and x64 applications under emulation by using [Prism](./apps-on-arm-x86-emulation.md). Prism includes many optimizations to ensure that emulation is fast and performant for a good user experience.

By default, Prism strikes the optimal balance between performance optimizations and app compatibility. In the uncommon event that an app has compatibility issues running under Prism, Windows provides optional emulation settings that you can use to tweak the behavior and optimizations that Prism uses for the app. Changing these settings can potentially increase compatibility for an application, generally at the cost of performance.

> [!WARNING]
> Changing emulation settings might cause your application to unexpectedly crash or not launch at all.

## Open emulation settings

You can reach the emulation settings by right-clicking on the executable for an app and selecting **Properties**.

When you use Windows on Arm, the **Compatibility** tab includes a section titled Windows on Arm. Select **Change emulation settings** to open an Arm emulation settings window.

![Change emulation settings screenshot](images/Capture.png)

## Restoring previous emulator behavior

Windows on Arm continues to improve and evolve over time. If an application encounters an issue but worked on a previous version of Windows on Arm, overriding Prism's behavior to match that of a previous version of Windows on Arm might resolve the issue.

### Hide x64 emulation capability

When you select this option, x86 applications see that x64 code can't run on this device. This option imitates the emulator's app support as it existed on Windows 10 on Arm. 

### Hide newer emulated CPU features

In Windows 11 24H2 and newer, Prism supports additional CPU features that previous versions of Windows on Arm didn't support. These features include AVX and AVX2, as well as BMI, FMA, F16C, and other related x86 instruction set extensions.

When you select this option for an app, the emulator returns to the level of CPU feature support that existed in the previous version of Prism.

For 32-bit x86 apps, this option is replaced with one to **Show newer emulated CPU features**. By default, Prism doesn't expose the CPU features mentioned earlier to 32-bit x86 apps. When you select this option, a 32-bit x86 app can detect and use the updated CPU feature set.

## Emulation settings

The Arm emulation settings window provides two ways to modify emulation settings. You can select a predefined group of emulation settings or select the **Use advanced settings** option to enable picking and choosing individual settings.

The four predefined groups of emulation settings are:
* Default
* Safe
* Strict
* Very strict

Moving from Default to Safe to Strict to Very strict sets additional emulation settings, trading off performance for potentially increasing compatibility.

![Change emulation settings screenshot2](images/Capture2.png)

If you select **Use advanced settings**, you can change how the application uses multiple CPU cores, selecting between Fast, Strict multi-core operation, Very strict, or Force single-core operation. 

The multi-core settings change how Prism uses memory barriers to synchronize memory accesses between cores in apps during emulation. Fast is the default mode, which is the optimal balance for majority of apps.  The strict and very strict options will increase the number of barriers, slowing down the app but reducing the risk of app errors. The single-core option removes all barriers but forces all app threads to run on a single core to avoid the need for synchronization.

![Multi-core settings screenshot](images/Capture3.png)

The remaining emulation settings are described in this table.

| Emulation setting | Result |
| ----------------- | ----------- |
| Disable application cache | The operating system caches compiled blocks of code to reduce emulation overhead on subsequent executions. This setting requires the emulator to recompile all app code at runtime. |
| Disable hybrid execution mode (x86 apps only) | Compiled Hybrid Portable Executable (CHPE) binaries are x86 compatible binaries that include native Arm64 code to improve performance, but might not be compatible with some apps. This setting disables use of these hybrid binaries in favor of pure x86-only binaries. |
| Additional lightweight emulation protections | This setting causes Prism to ignore the presence of any [volatile metadata](/cpp/build/reference/volatile) in a binary. |
| Strict self-modifying code support | Enable this setting to ensure that any self-modifying code is correctly supported in emulation. The most common self-modifying code scenarios are covered by the default emulator behavior. Selecting this option significantly reduces performance of self-modifying code during execution. |
| Disable RWX page performance optimization | This setting disables an optimization that improves performance of code on readable, writable, and executable (RWX) pages, but might be incompatible with some apps. |
| Disable floating point optimization | x87 is an x86 instruction set extension, used primarily in some older x86 software to perform floating point arithmetic, which can use a higher-precision 80-bit floating point format that is not required for most software that uses x87. Selecting this option will have Prism use the full 80-bit precision instead of a 64-bit approximation, at the cost of performance. |


