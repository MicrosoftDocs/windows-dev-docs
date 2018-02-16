---
title: Troubleshooting x86 apps on ARM
author: msatranjr
description: Common issues with x86 apps when running on ARM, and how to fix them.
ms.author: misatran
ms.date: 02/15/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10 s, always connected, x86 emulation on ARM, troubleshooting
ms.localizationpriority: medium
---

# Troubleshooting x86 apps on ARM
If your x86 app doesn't work on ARM the way it does on an x86 machine, here's some guidance to help you troubleshoot.

## Familiarize yourself with the platform limitations 
First, make sure that the issue isn't one of the limitations that apply to Windows 10 on ARM. For more info, see [Limitations of apps and experiences on ARM](apps-on-arm-limitations.md).

## Use the Program Compatibility Troubleshooter for Win32 apps
If your Win32 app isn't subject to any of the platform limitations noted in the preceding section, check whether the issue you're seeing is the result of x86 emulation settings. For more info, see [Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md). If changing a specific setting resolves your issue, please email *woafeedback@microsoft.com* because this may be a result of a platform issue.

## Ensure that your app calls IsWow64Process2
One common problem occurs when an app discovers that it's running under WOW and then assumes that it is on an x64 system. Having made this assumption, the app may do the following:

- Try to install the x64 version of itself, which isn't supported on ARM.
- Check for other software under the native registry view.
- Assume that a 64-bit .NET framework is available.

An app may place registry keys under the native registry view, or perform functions based on the presence of WOW. The original **IsWow64Process**  indicates only whether the app is running on an x64 machine. Apps should now use [IsWow64Process2](https://msdn.microsoft.com/en-us/library/windows/desktop/mt804318(v=vs.85).aspx) to determine whether they're running on a system with WOW support. 

Generally, an app should not make assumptions about the host system when it is determined to run under WOW. Avoid interacting with native components of the OS as much as possible.

## Debugging
To investigate your app's behavior in more depth, see [Debugging on ARM](https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/debugging-arm64) to learn more about tools and strategies for debugging on ARM.

## Performance
If your x86 UWP app runs faster when set to Single-Core by using the [Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md), you can resubmit it to the Store as an ARM32 app as a simple measure to make it run faster. To learn how to measure performance, see [Windows Performance Toolkit](https://docs.microsoft.com/en-us/windows-hardware/test/wpt/)
