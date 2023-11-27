---
title: How x86 emulation works on Arm
description: Learn how emulation for x86 apps makes the rich ecosystem of existing Win32 apps available on Arm devices.
ms.date: 10/23/2023
ms.topic: article
ms.prod: windows
ms.technology: arm
author: mattwojo
ms.author: mattwoj
ms.reviewer: marcs
---

# How x86 emulation works on Arm

Emulation for x86 apps makes the rich ecosystem of Win32 apps available on Arm. This provides the user the magical experience of running an existing x86 win32 app without any modifications to the app.

Arm versions of Windows 10 include emulation technology that enables existing unmodified x86 apps to run on Arm devices. Windows 11 extends that emulation to run unmodified x64 Windows apps on Arm-powered devices.

## WOW64 APIs

An x86 (or x64) app doesn’t even know that it is running on a Windows on Arm PC, unless it calls specific APIs ([IsWoW64Process2](/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2)).

The [WOW64](/windows/desktop/WinProg64/running-32-bit-applications) layer of Windows allows x86 code to run on the Arm64 version of Windows. x86 emulation works by compiling blocks of x86 instructions into Arm64 instructions with optimizations to improve performance. A service caches these translated blocks of code to reduce the overhead of instruction translation and allow for optimization when the code runs again. The caches are produced for each module so that other apps can make use of them on first launch.

## Updating to support an Arm64 version of your app

While the ability to emulate x64 and x86 on Arm devices is a great step forward, your app may be able to take advantage of native performance gains and the unique qualities of Arm64-powered devices by updating to support an Arm64 version of the app.

See [Add Arm support to your Windows app](add-arm-support.md) for guidance on how to create an Arm64 version of your apps and what sort of advantages, challenges, and tooling may be involved, as well as available support for creating an Arm64 version of your app (and any related dependencies).
