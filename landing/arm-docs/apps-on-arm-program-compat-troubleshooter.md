---
title: Program Compatibility Troubleshooter on Arm
description: Guidance for adjusting compatibility settings if your app isn't working correctly on Arm
ms.date: 06/25/2021
ms.topic: article
ms.service: windows
ms.subservice: arm
author: mattwojo
ms.author: mattwoj
ms.reviewer: marcs
---

# Program Compatibility Troubleshooter on Arm

Emulation to support x86 apps is a new feature created for Windows on Arm64. Sometimes the emulation performs optimizations that don't result in the best experience. You can use the Program Compatibility Troubleshooter to toggle emulation settings for your x86 app, reducing the default optimizations and potentially increasing compatibility.

## Start the Program Compatibility Troubleshooter

You start the [Program Compatibility Troubleshooter](https://support.microsoft.com/help/15078/windows-make-older-programs-compatible) manually in the same way on any Windows PC: right-click an executable (.exe) file and select **Troubleshoot compatibility**. This screen appears.

![Screenshot of the Troubleshoot compatibility options.](images/Capture4.png)

If you click on **Troubleshoot program** you will be presented with the following options.

![Screenshot of the What problems do you notice options.](images/Capture5.png)

All options enable the settings that are applicable and applied on Windows Desktop PCs. In addition, the first, second, and fourth options apply the [Disable application cache](#disable-app-cache) and [Disable hybrid execution mode](#disable-hybrid-exec-mode) emulation settings.

## Toggling emulation settings

> [!WARNING]
> Changing emulation settings may result in your application unexpectedly crashing or not launching at all.

You can toggle emulation settings by right-clicking the executable and selecting **Properties**.

On ARM, a section titled **Windows 10 on ARM** or **Windows 11 on ARM** will be available in the **Compatibility** tab. Click **Change emulation settings** to launch a second window as here.

![Change emulation settings screenshot](images/Capture.png)

This window provides two ways to modify emulation settings. You may select a pre-defined group of emulation settings, or you may click the **Use advanced settings** option to enable choosing individual settings.

The grouped emulation settings reduce performance optimizations in favor of quality. Below are some grouped settings that you can select.

![Change emulation settings screenshot2](images/Capture2.png)

Select **Use advanced settings** to choose individual settings as described in this table.

| Emulation setting | Result |
| ----------------- | ----------- |
| <p id="disable-app-cache">Disable application cache</p> | The operating system will cache compiled blocks of code to reduce emulation overhead on subsequent executions. This setting requires the emulator to recompile all app code at runtime. |
| <p id="disable-hybrid-exec-mode">Disable hybrid execution mode</p> | Compiled Hybrid Portable Executable (CHPE), binaries are x86 compatible binaries that include native Arm64 code to improve performance, but that may not be compatible with some apps. This setting forces use of x86-only binaries. |
| Strict self-modifying code support | Enable this to ensure that any self-modifying code is correctly supported in emulation. The most common self-modifying code scenarios are covered by the default emulator behavior. Enabling this option significantly reduces performance of self-modifying  code during execution. |
| Disable RWX page performance optimization | This optimization improves the performance of code on readable, writable, and executable (RWX) pages, but may be incompatible with some apps. |

You can also select multi-core settings, as shown here.

![Multi-core settings screenshot](images/Capture3.png)

These settings change the number of memory barriers used to synchronize memory accesses between cores in apps during emulation. **Fast** is the default mode, but the **strict** and **very strict** options will increase the number of barriers. This slows down the app, but reduces the risk of app errors. The **single-core** option removes all barriers but forces all app threads to run on a single core.

If changing a specific setting resolves your issue, please email *woafeedback@microsoft.com* with details, so that we may incorporate your feedback.
