---
title: Troubleshooting x86 desktop apps
description: Learn how to troubleshoot and fix common issues with an x86 desktop app running on Arm64 including information about drivers, shell extensions, and debugging.
ms.date: 11/06/2025
ms.topic: troubleshooting-general
ms.service: windows
ms.subservice: arm
ms.reviewer: marcs
---

# Troubleshooting x86 desktop apps

>[!IMPORTANT]
> With Visual Studio 2017 or later, you can recompile your app to Arm64 or Arm64EC so that your app runs at full native speed. For more info about compiling as Arm64, see the blog post: [Official support for Windows 10 on Arm development](https://blogs.windows.com/buildingapps/2018/11/15/official-support-for-windows-10-on-arm-development). For information about Arm64EC, see [Announcing Arm64EC: Building Native and Interoperable Apps for Windows 11 on Arm](https://aka.ms/arm64ecannounceblog).

If an x86 desktop app doesn't work the way it does on an x86 machine, use the following guidance to help you troubleshoot.

|Issue|Solution|
|-----|--------|
| Your app relies on a driver that isn't designed for Arm. | Recompile your x86 driver to Arm64. See [Building Arm64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers). |
| Your app is available only for x64. | If you develop for Microsoft Store, submit an Arm version of your app. For more info, see [App package architectures](/windows/msix/package/device-architecture). If you're a Win32 developer, recompile your app to Arm64. For more info see [Early preview of Visual Studio support for Windows 10 on Arm development](https://blogs.windows.com/buildingapps/2018/05/08/visual-studio-support-for-windows-10-on-arm-development/). |
| Your app uses an OpenGL version later than 1.1 or requires hardware-accelerated OpenGL. | Use the DirectX mode of the app, if it's available. x86 apps that use DirectX 9, DirectX 10, DirectX 11, and DirectX 12 work on Arm. For more info, see [DirectX Graphics and Gaming](/windows/desktop/directx). |
| Your x86 app doesn't work as expected. | Try using the Compatibility Troubleshooter by following guidance from [Program Compatibility Troubleshooter on Arm](apps-on-arm-program-compat-troubleshooter.md). For some other troubleshooting steps, see the [Troubleshooting x86 apps on Arm](apps-on-arm-troubleshooting-x86.md) article. |

## Best practices for WOW

One common problem occurs when an app discovers that it's running under WOW and then assumes that it is on an x64 system. With this assumption, the app might do the following actions:

- Try to install the x64 version of itself, which isn't supported on Arm.
- Check for other software under the native registry view.
- Assume that a 64-bit .NET framework is available.

Generally, an app shouldn't make assumptions about the host system when it is determined to run under WOW. Avoid interacting with native components of the OS as much as possible.

An app might place registry keys under the native registry view, or perform functions based on the presence of WOW. The original **IsWow64Process**  indicates only whether the app is running on an x64 machine. Apps should now use [IsWow64Process2](/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2) to determine whether they're running on a system with WOW support. 

## Drivers

All kernel-mode drivers, [User-Mode Driver Framework (UMDF)](/windows-hardware/drivers/wdf/overview-of-the-umdf) drivers, and print drivers must be compiled to match the architecture of the OS. If an x86 app has a driver, then you must recompile that driver for Arm64. The x86 app might run fine under emulation; however, you need to recompile its driver for Arm64 and any app experience that depends on the driver isn't available. For more info about compiling your driver for Arm64, see [Building Arm64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers).

## Shell extensions

Apps that try to hook Windows components or load their DLLs into Windows processes need to recompile those DLLs to match the architecture of the system, such as Arm64. Typically, input method editors (IMEs), assistive technologies, and shell extension apps use these DLLs. For example, these apps show cloud storage icons in Explorer or a right-click context menu. To learn how to recompile your apps or DLLs to Arm64, see the [Early preview of Visual Studio support for Windows 10 on Arm development](https://blogs.windows.com/buildingapps/2018/05/08/visual-studio-support-for-windows-10-on-arm-development/) blog post.

## Debugging

To investigate your app's behavior in more depth, see [Debugging on Arm](/windows-hardware/drivers/debugger/debugging-arm64) to learn more about tools and strategies for debugging on Arm.

## Virtual Machines

The Windows Hypervisor platform isn't supported on the Qualcomm Snapdragon 835 Mobile PC Platform. Hence, running virtual machines by using Hyper-V doesn't work. We continue to make investments in these technologies on future Qualcomm chipsets. 

## Dynamic Code Generation

The system emulates x86 desktop apps on Arm64 by generating Arm64 instructions at runtime. This emulation doesn't support an x86 desktop app that prevents dynamic code generation or modification in its process. 

This security mitigation is enabled on some apps' processes by using the [SetProcessMitigationPolicy](/windows/desktop/api/processthreadsapi/nf-processthreadsapi-setprocessmitigationpolicy) API with the `ProcessDynamicCodePolicy` flag. To run successfully on Arm64 as an x86 process, you need to disable this mitigation policy.
