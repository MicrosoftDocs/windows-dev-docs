---
title: Troubleshooting x86 desktop apps
description: Learn how to troubleshoot and fix common issues with an x86 desktop app running on ARM64 including information about drivers, shell extensions, and debugging.
ms.date: 05/09/2018
ms.topic: article
keywords: windows 10 s, always connected, x86 emulation on ARM, troubleshooting
ms.localizationpriority: medium
---
# Troubleshooting x86 desktop apps
>[!IMPORTANT]
> The ARM64 SDK is now available as part of Visual Studio 15.8 Preview 1. We recommend that you recompile your app to ARM64 so that your app runs at full native speed. For more info, see the [Early preview of Visual Studio support for Windows 10 on ARM development](https://blogs.windows.com/buildingapps/2018/05/08/visual-studio-support-for-windows-10-on-arm-development/) blog post.

If an x86 desktop app doesn't work the way it does on an x86 machine, here's some guidance to help you troubleshoot.

|Issue|Solution|
|-----|--------|
| Your app relies on a driver that isn't designed for ARM. | Recompile your x86 driver to ARM64. See [Building ARM64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers). |
| Your app is available only for x64. | If you develop for Microsoft Store, submit an ARM version of your app. For more info, see [App package architectures](/windows/msix/package/device-architecture). If you're a Win32 developer, we recommend you recompile your app to ARM64. For more info see [Early preview of Visual Studio support for Windows 10 on ARM development](https://blogs.windows.com/buildingapps/2018/05/08/visual-studio-support-for-windows-10-on-arm-development/). |
| Your app uses an OpenGL version later than 1.1 or requires hardware-accelerated OpenGL. | Use the DirectX mode of the app, if it's available. x86 apps that use DirectX 9, DirectX 10, DirectX 11, and DirectX 12 will work on ARM. For more info, see [DirectX Graphics and Gaming](/windows/desktop/directx). |
| Your x86 app does not work as expected. | Try using the Compatibility Troubleshooter by following guidance from [Program Compatibility Troubleshooter on ARM](apps-on-arm-program-compat-troubleshooter.md). For some other troubleshooting steps, see the [Troubleshooting x86 apps on ARM](apps-on-arm-troubleshooting-x86.md) article. |

## Best practices for WOW
One common problem occurs when an app discovers that it's running under WOW and then assumes that it is on an x64 system. Having made this assumption, the app may do the following:

- Try to install the x64 version of itself, which isn't supported on ARM.
- Check for other software under the native registry view.
- Assume that a 64-bit .NET framework is available.

Generally, an app should not make assumptions about the host system when it is determined to run under WOW. Avoid interacting with native components of the OS as much as possible.

An app may place registry keys under the native registry view, or perform functions based on the presence of WOW. The original **IsWow64Process**  indicates only whether the app is running on an x64 machine. Apps should now use [IsWow64Process2](/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2) to determine whether they're running on a system with WOW support. 

## Drivers 
All kernel-mode drivers, [User-Mode Driver Framework (UMDF)](/windows-hardware/drivers/wdf/overview-of-the-umdf) drivers, and print drivers must be compiled to match the architecture of the OS. If an x86 app has a driver, then that driver must be recompiled for ARM64. The x86 app may run fine under emulation however, its driver will need to be recompiled for ARM64 and any app experience that depends on the driver will not be available. For more info about compiling your driver for ARM64, see [Building ARM64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers).

## Shell extensions 
Apps that try to hook Windows components or load their DLLs into Windows processes will need to recompile those DLLs to match the architecture of the system; i.e. ARM64. Typically, these are used by input method editors (IMEs), assistive technologies, and shell extension apps (for example, to show cloud storage icons in Explorer or a right click Context menu). To learn how to recompile your apps or DLLs to ARM64, see the [Early preview of Visual Studio support for Windows 10 on ARM development](https://blogs.windows.com/buildingapps/2018/05/08/visual-studio-support-for-windows-10-on-arm-development/) blog post. 

## Debugging
To investigate your app's behavior in more depth, see [Debugging on ARM](/windows-hardware/drivers/debugger/debugging-arm64) to learn more about tools and strategies for debugging on ARM.

## Virtual Machines
The Windows Hypervisor platform is not supported on the Qualcomm Snapdragon 835 Mobile PC Platform. Hence, running virtual machines using Hyper-V will not work. We continue to make investments in these technologies on future Qualcomm chipsets. 

## Dynamic Code Generation
X86 desktop apps are emulated on ARM64 by the system generating ARM64 instructions at runtime. This means if an x86 desktop app prevents dynamic code generation or modification in its process, that app cannot be supported to run as x86 on ARM64. 

This is a security mitigation some apps enable on their process using [SetProcessMitigationPolicy](/windows/desktop/api/processthreadsapi/nf-processthreadsapi-setprocessmitigationpolicy) API with the `ProcessDynamicCodePolicy` flag. To run successfully on ARM64 as an x86 process, this mitigation policy will have to be disabled.