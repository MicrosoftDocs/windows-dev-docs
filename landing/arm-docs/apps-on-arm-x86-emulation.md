---
title: How emulation works on Arm
description: Learn how emulation for x86 and x64 apps makes the rich ecosystem of existing Win32 apps available on Arm devices.
ms.date: 10/23/2023
ms.topic: article
ms.service: windows
ms.subservice: arm
ms.reviewer: marcs
---

# How emulation works on Arm

Emulation makes the rich ecosystem of Windows apps available on Arm, allowing users to run the apps they care about without any modifications to the app.  Emulation is transparent to the user and is part of Windows; it does not require any additional components to be installed.

Windows 11 on Arm supports emulation of both x86 and x64 apps and performance has been enhanced with the introduction of the new emulator Prism in Windows 11 24H2.  Windows 10 on Arm also supports emulation, but only for x86 apps. 

## Prism

Prism is the new emulator included with Windows 11 24H2.  Relative to previous emulation technology included in Windows, it includes significant optimizations that improve the performance and lower CPU usage of apps under emulation.

Prism is optimized and tuned specifically for Qualcomm Snapdragon processors.  Some performance features within Prism require hardware features only available in the Snapdragon X series, but Prism is available for all supported Windows 11 on Arm devices with Windows 11 24H2.

## How emulation works

Emulation works as a software simulator, just-in-time compiling blocks of x86 instructions into Arm64 instructions with optimizations to improve performance of the emitted Arm64 code.   

A service caches these translated blocks of code to reduce the overhead of instruction translation and allow for optimization when the code runs again. The caches are produced for each module so that other apps can make use of them on first launch.

For x86 apps, the [WOW64](/windows/desktop/WinProg64/running-32-bit-applications) layer of Windows allows x86 code to run on the Arm64 version of Windows, just as it allows x86 code to run on the x64 version of Windows.  This means that x86 apps on Arm are protected with filesystem and registry redirection.

For x64 apps, there is no WOW64 layer and no separate registry or folder of Windows system binaries.  Instead, system binaries are compiled as [Arm64X PE files](./arm64x-pe.md) that can be loaded into both x64 and Arm64 processes from the same location without the need for filesystem redirection.  This means that x64 applications can access the entire OS, both filesystem and registry, without the need for special code. 

Note that emulation only supports user mode code and does not support drivers; any kernel mode components must be compiled as Arm64.

## Detecting emulation

An x86 or x64 app will not know that it is running on a Windows on Arm PC, unless it calls specific APIs that are designed to convey knowledge of the Arm64 host, such as [IsWoW64Process2](/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2).  Apps under emulation that query for processor details including metadata or feature capabilities will receive details corresponding to the emulated virtual processor.  For compatibility reasons, the API [GetNativeSystemInfo](/windows/desktop/api/sysinfoapi/nf-sysinfoapi-getnativesysteminfo) will also return emulated processor details when run from an app under emulation. 
 
For apps looking to detect the emulation capabilities of the operating system, use the API [GetMachineTypeAttributes](/windows/win32/api/processthreadsapi/nf-processthreadsapi-getmachinetypeattributes).

## Updating to support an Arm version of your app

While having your app run under emulation on Arm devices is a great place to start, your app will benefit from native performance gains and the unique qualities of Arm-powered devices if you rebuild to add Arm support to your app.

See [Add Arm support to your Windows app](add-arm-support.md) for guidance on how to create an Arm version of your apps and what sort of advantages, challenges, and tooling may be involved, as well as available support for creating an Arm version of your app (and any related dependencies).
