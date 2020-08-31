---
title: How x86 and ARM32 emulation work on ARM

description: Learn how emulation for x86 apps makes the rich ecosystem of existing Win32 apps available on ARM devices.
ms.date: 02/15/2018
ms.topic: article
keywords: windows 10 s, always connected, x86 emulation on ARM
ms.localizationpriority: medium
---
# How x86 emulation works on ARM
Emulation for x86 apps makes the rich ecosystem of Win32 apps available on ARM. This provides the user the magical experience of running an existing x86 win32 app without any modifications to the app. The app doesn’t even know that it is running on a Windows on ARM PC, unless it calls specific APIs ([IsWoW64Process2](/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2)).

The [WOW64](/windows/desktop/WinProg64/running-32-bit-applications) layer of Windows 10 allows x86 code to run on the ARM64 version of Windows 10. x86 emulation works by compiling blocks of x86 instructions into ARM64 instructions with optimizations to improve performance. A service caches these translated blocks of code to reduce the overhead of instruction translation and allow for optimization when the code runs again. The caches are produced for each module so that other apps can make use of them on first launch. 

For more details about these technologies, see the [Windows 10 on ARM](https://channel9.msdn.com/Events/Build/2017/P4171) Channel9 video.