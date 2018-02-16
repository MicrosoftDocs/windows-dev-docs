---
title: How x86 and ARM32 emulation work on ARM
author: msatranjr
description: An overview of emulation of x86 apps on ARM.
ms.author: misatran
ms.date: 02/15/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10 s, always connected, x86 emulation on ARM
ms.localizationpriority: medium
---

# How x86 emulation works on ARM
Emulation for x86 apps makes the rich ecosystem of Win32 apps available on ARM. Here's how it works.

The [WOW64](https://msdn.microsoft.com/en-us/library/windows/desktop/aa384249(v=vs.85).aspx) layer of Windows 10 allows x86 code to run on the ARM64 version of Windows 10. x86 emulation works by compiling blocks of x86 instructions into ARM64 instructions with optimizations to improve performance. A service caches these translated blocks of code to reduce the overhead of instruction translation and allow for optimization when the code runs again. The caches are produced for each module so that other apps can make use of them on first launch. 

Most x86 apps load x86 system modules that ship with Windows 10. However, on ARM, we have provided system modules that have been compiled with Compiled Hybrid Portable Executable (CHPE) technology to improve performance of the app. CHPE binaries are x86-compatible binaries that are compiled by the Microsoft Visual Studio C++ compiler to a combination of x86 and optimized native ARM64 code, improving performance by reducing runtime compilation. For more details about these technologies, see the [Windows 10 on ARM](https://channel9.msdn.com/Events/Build/2017/P4171) Channel9 video. 

There are compatibility settings that allow you to toggle performance optimizations to potentially increase compatibility. To learn more, see  [Program Compatibility Troubleshooter on ARM64](apps-on-arm-program-compat-troubleshooter.md). 