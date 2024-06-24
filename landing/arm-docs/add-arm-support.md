---
title: Add support Arm devices to your Windows app
description: Guidance for adding Arm64 support to your app. Optimize your x64 app to perform better on Windows devices powered by Arm processors so that CPU, GPU, and NPU performance is accelerated, less power is consumed to preserve battery life, and wi-fi and mobile data network connections are supported.
ms.date: 05/21/2024
ms.topic: article
ms.service: windows
ms.subservice: arm
author: mattwojo
ms.author: mattwoj
ms.reviewer: marcs
---

# Add Arm support to your Windows app

Arm-based devices are becoming increasingly popular due to their power-frugal nature, longer battery life, and impressive processing power, in addition to the Windows on Arm support for Neural Processing Units (NPUs) tuned for the increasingly popular AI and Machine Learning workloads. 

This guide will cover the steps for adding support to your Windows app(s) for devices powered by Arm64 processors. Guidance will also cover ways to address any potential issues or blockers (such as 3rd party dependencies or plug-ins) that may interfere with creating an Arm64-based version of your app.

## Emulation on Arm-based devices for x86 or x64 Windows apps

Arm versions of Windows 10 include emulation technology that enables existing unmodified x86 apps to run on Arm devices. Windows 11 extends that emulation to run unmodified x64 Windows apps on Arm-powered devices.

While the ability to emulate x64 and x86 on Arm devices is a great step forward, this guide will help you to add Arm-native support, so that your app can take advantage of native performance gains and the unique qualities of Arm64-powered devices, including:

- Optimizing the power-consumption of your app to extend device battery life.
- Optimizing performance for CPU, GPU, and NPUs to accelerate workflows, particularly when working with AI.

Additionally, [Kernel drivers](/windows-hardware/drivers/kernel/) are required to be built as native Arm64. There is no emulation present in the kernel. This primarily impacts virtualization scenarios. For apps that utilize device drivers requiring direct access to the internals of the OS or hardware running in kernel mode, rather than user mode, and that have not yet been updated to support Arm64 processors, see [Building Arm64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers).

> [!NOTE]
> Progressive Web Apps (PWAs) will already execute with native Arm64 performance.

## Prerequisites

If you are updating your app using an Arm-based device (native compiling - generating the code for the same platform on which you're running), you can use:

- [Introducing Visual Studio 17.10 – Preview 1 (Feb 2024)](https://devblogs.microsoft.com/visualstudio/introducing-visual-studio-17-10-preview-1-is-here/)
- [Visual Studio 2022 v17.4](https://devblogs.microsoft.com/visualstudio/visual-studio-2022-17-4/#arm64) or later. This is the first GA release of Visual Studio that natively supports building and debugging Arm64 apps on Arm-based processors. Both Visual Studio 2022 17.4 and Microsoft Visual C++ (MSVC) native Arm64 versions provide significantly better performance  compared with previous emulated versions.

- (Optional) [LLVM (Clang) v12+](https://releases.llvm.org/12.0.0/tools/clang/docs/ReleaseNotes.html#windows-support) or later. LLVM 12 adds official binary release hosted on Windows on Arm64, including a Clang compiler, LLD Linker, and compiler-rt runtime libraries.

If you are updating your Windows app to support Arm using an x64 or x86 Intel-based device (cross compiling), you can use:

- [Visual Studio 2022 v17.10​](/visualstudio/releases/2022/release-notes-v17.0) (Recommended)
- [Visual Studio 2019 v16.x​](/visualstudio/releases/2019/history#prior-release-notes)
- [Visual Studio 2017 v15.9 onwards (UWP, Desktop bridge, win32 C++)​](/visualstudio/releasenotes/vs2017-relnotes)
- [LLVM (Clang) v12+](https://releases.llvm.org/12.0.0/tools/clang/docs/ReleaseNotes.html#windows-support)

There are several factors to consider when choosing between cross compilation or native compilation such as available hardware and simplicity of test execution.

> [!NOTE]
> [GCC, the GNU Compiler Collection](https://gcc.gnu.org/) support is targeted for the near future.

## Steps to add Arm64 native support

To update your app to run natively on Arm64:

1. [Add an Arm64 configuration to your project in Visual Studio](#step-1---add-an-arm64-configuration-to-your-project-in-visual-studio)
2. [Test and debug the newly built Arm64 app](#step-2---test-and-debug-the-newly-built-arm64-app)
3. [Build and test your app on Arm devices](#step-3---build-and-test-your-app-on-arm-devices)

Once you've confirmed that your app has successfully been optimized for Arm devices:

4. [Update your installer and publish your updated app](#step-4---update-your-installer-and-publish-your-updated-app)
5. [Plan for ongoing updates](#step-5---plan-for-ongoing-updates)

## Step 1 - Add an Arm64 configuration to your project in Visual Studio

To add a new ARM64 solution platform with debug and release targets to your existing x64 or x86 app project:

1. Open your solution (project code) in Visual Studio (see [prerequisites](#prerequisites) for the supported versions).
2. In the "Solution Platforms" drop-down menu on the Standard toolbar (or in the "Build" menu), select **Configuration Manager...**
3. Open the "Active solution platform" drop-down menu and select **<New...>**.
4. In the "Type or select the new platform" drop-down menu, select **ARM64** and ensure that the "Copy settings from" value is set to **x64** with the "Create new project platforms" checkbox enabled, then select **OK**.

Congratulations! You have started adding Arm support to your app. Next, check to see whether your Arm64 Solution builds successfully.

If your Solution does not build successfully, you will need to resolve the issues that are causing the build to fail.  The most likely reason is that a dependency is not available for ARM64, which is covered in [Troubleshooting](#troubleshooting) below.

*(Optional)*: If you want to verify first-hand that your app binary is now built for Arm64, you can open your project directory in PowerShell (right-click your app project in Visual Studio Solution Explorer and select **Open in Terminal**). Change directories so that your project's new `bin\ARM64\Debug` or Release directory is selected. Enter the command: `dumpbin /headers .\<appname>.exe` (replacing `<appname>` with the name of your app). Scroll up in your terminal's output results to find the `FILE HEADER VALUES` section and confirm the first line is `AA64 machine (ARM64)`.

## Step 2 - Test and debug the newly built Arm64 app

To check whether your Arm64 Solution builds successfully after adding the Arm64 solution platform to your project in Visual Studio:

1. Close the "Active solution platform" window.
2. Change the build setting from **Debug** to **Release**.
3. In the "Build" drop-down menu, select **Rebuild Solution** and wait for the project to rebuild.
4. You will receive a "Rebuild All succeeded" output. If not, see the [Troubleshooting](#troubleshooting) section below.

Once the binaries are built for your app to support Arm64, you’ll want to test them. That will require having a device or a virtual machine running Windows on Arm.  

If you are doing development on a Windows on Arm device, then you have an easy setup with Visual Studio local debugging. If cross-compiling (using a device that is not running on an Arm-processor), then you will want to use remote debugging on a Windows on Arm device or a virtual machine to enable your development experience in Visual Studio while running the Arm64 app on another device.

### Windows on Arm hardware or virtual machines available for testing

If you are looking for hardware to use for Continuous Integration (CI) and testing, these are a few of the Windows devices with an Arm64-based processor:

- [Windows Dev Kit 2023](/windows/arm/dev-kit)
- [Surface Pro 9 5G](https://www.microsoft.com/en-us/d/surface-pro-9/93vkd8np4fvk)
- [Lenovo x13s](https://www.lenovo.com/us/en/p/laptops/thinkpad/thinkpadx/thinkpad--x13s-(13-inch-snapdragon)/len101t0019)

For help setting up a virtual machine (VM) running Windows on Arm to support CI and testing, see [Quickstart: Create a Windows on Arm virtual machine in the Azure portal](./create-arm-vm.md).

- Read the Azure blog announcement of general availability for [Azure Virtual Machines with Ampere Altra Arm-based processors](https://azure.microsoft.com/blog/azure-virtual-machines-with-ampere-altra-arm-based-processors-generally-available/) with the ability to run Arm64-based versions of Windows 11 Pro and Enterprise.

- Learn more about the [Windows 11 on Arm Insider Preview (VHDX)](https://www.microsoft.com/en-us/software-download/windowsinsiderpreviewARM64) for creating a local Windows on Arm VM using Hyper-V and the Windows Insider VHDX. *Arm64 VMs are only supported on devices that meet the prerequisites. Creating Arm64 VMs is not supported on x64 hardware - you will need to host the VM in the cloud, see the quickstart link above.

-  Check out the video ["Ask the Expert: Create Apps with Ampere-based Azure VMs"](/shows/ask-the-expert/ask-the-expert-create-apps-with-ampere-based-azure-vms).

## Step 3 - Build and test your app on Arm devices

Adding a test automation pass is an important considering for your Continuous Integrations and Continuous Delivery (CI/CD) strategy. For Arm64 solutions running on Windows, it’s important to run your test suite on Arm64 architecture -- this could be actual Windows on Arm hardware, using one of the Arm devices listed above, or a Virtual Machine, from the VMs listed above.

Compiling the app is more convenient when done on the same machine as the tests, but in many cases is not required. Instead, you can consider extending the existing build infrastructure to produce a cross-compiled output for Arm64.

## Step 4 - Update your installer and publish your updated app

If you publish to the Microsoft Store, once you have built an Arm64 version of your app by following the steps above, you can update your existing app package in the Microsoft Store by visiting your [Partner Center dashboard](https://partner.microsoft.com/dashboard) and adding the newly built ARM64 binaries to the submission.

If your app is not already published in the Microsoft Store, you can follow the instructions to [create an app submission](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msi-exe) based on whether you want to submit an MSI or EXE, MSIX package, PWA, or App add-on.

If you build your own installer, you should ensure it is able to install your new Arm64 version successfully.  Most installer frameworks, such as [WiX](https://wixtoolset.org/docs/releasenotes/), [Squirrel](https://github.com/Squirrel/Squirrel.Windows), [InnoSetup](https://github.com/jrsoftware/issrc), [InstallAware](https://www.installaware.com/mh52/desktop/set64bitmode.htm), and others support Windows on Arm without issue.

If you offer your app's installer from a webpage, you can use [User-Agent Client Hints](/microsoft-edge/web-platform/how-to-detect-win11) to detect when your customer is visiting from a Windows on Arm device and offer them the updated Arm-native version of your app. Unlike the user-agent string, User-Agent Client Hints allows you to differentiate customers on Arm from customers on x86 devices.

## Step 5 - Plan for ongoing updates

Now that you have an Arm64 version of your app published, you’ll want to ensure that it stays updated the same way that other versions of your app do. It’s best to keep versions and features aligned across architectures to avoid customer confusion in the future.

## Troubleshooting

Common issues that may interfere with or block you from adding an Arm64 version of your existing x64 or x86 Windows app include:

- [A dependency not compiled for ARM64 is blocking you from a successful build.](#a-dependency-not-compiled-for-arm64-is-blocking-you-from-a-successful-build)
- [Code is written for a specific architecture other than Arm64.](#code-is-written-for-a-specific-architecture-other-than-arm64)
- [Your app relies on a kernel driver.](#your-app-relies-on-a-kernel-driver)
- [You're stuck and need assistance.](#need-assistance-leverage-our-app-assure-service)

### A dependency not compiled for ARM64 is blocking you from a successful build

If you can’t build due to a dependency, whether internal, from a 3rd party, or from an open-source library, you will need to either find a way to update that dependency to support ARM64 architecture or remove it.

- For internal dependencies, we recommend rebuilding the dependency for ARM64 support.

- For 3rd party dependencies, we recommend filing a request for the maintainer to rebuild with ARM64 support.

- For open source dependencies, consider checking [vcpkg](https://vcpkg.io/en/packages.html) to see if a newer version of the dependency that includes ARM64 support exists that you can update to. If no update exists, consider contributing the addition of ARM64 support to the package yourself. Many open source maintainers would be thankful for the contribution.

- The Linaro organization also works with businesses and open source communities to develop software on Arm-based technology. You can [file a request with the Linaro Service Desk to help update package support](https://linaro-servicedesk.atlassian.net/servicedesk/customer/portal/22) for any missing dependencies related to Windows on Arm.

- Consider using [Arm64EC](arm64ec.md). Arm64EC versions of dependencies can be used to rebuild an application while still utilizing x64 versions of dependencies. Any x64 code, including  code from dependencies, in an Arm64EC process will run under emulation in your app. (Arm64 versions of dependencies won't be usable in this case.)

- The last choice would be to remove and/or replace the dependency on your app project.

### Code is written for a specific architecture other than Arm64

- CPU specific assembly or inline intrinsic function calls will need to be modified to match available instructions and functions on the Arm CPU. For guidance, see: [Using Assembly and Intrinsics in C or C++ Code](https://developer.arm.com/documentation/100748/0620/Using-Assembly-and-Intrinsics-in-C-or-C---Code).

### Your app relies on a kernel driver

[Kernel drivers](/windows-hardware/drivers/kernel/) are required to be built as native Arm64. There is no emulation present in the kernel. This primarily impacts virtualization scenarios. For apps that utilize device drivers requiring direct access to the internals of the OS or hardware running in kernel mode, rather than user mode, and that have not yet been updated to support Arm64 processors, see [Building Arm64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers).

Additionally, [drivers on Windows](/windows-hardware/drivers/gettingstarted/) are required to be built as Arm64 and can't be emulated.  For apps that rely on software drivers that have not yet been updated to support Arm64 processors, see [Building Arm64 Drivers with the WDK](/windows-hardware/drivers/develop/building-arm64-drivers).

<!-- ### When to rebuild as Arm64EC

When is [Arm64EC](arm64ec.md) a good fit for updating your app?

- Arm64EC is considered native Arm code which can mean better power consumption, battery life, and accelerated AI & ML workloads.

- Arm64EC is only supported for apps running on Arm-powered Windows 11 devices. It is a Windows 11 feature that requires the use of the Windows 11 SDK and is not available on Windows 10 on Arm.

- Arm64EC is specific to recompiling x64 apps piece by piece. Enabling you to leave assembly or stubborn dependencies as x64 and/or retaining 3rd party x64 plugin compatibility. For x86 applications, you will want to recompile everything (code, dependencies, plugins) for Arm64.

- Arm64EC brings AArch64 instructions into the x64 calling convention. It is NOT possible to directly load classic Arm64 native code in this scenario.

- Arm64EC currently supports only C++ projects and does not support .NET projects.

- Arm64EC is ideal for plug-in models and x64 plug-in extensions. Check out [Arm64X PE files](./arm64x-pe.md).

- Arm64EC support was introduced in [Visual Studio 17.3](/visualstudio/releases/2022/release-notes-v17.3). Clang-cl support is coming soon. Linker *must* be MSVC linker.

### Need assistance? Leverage our App Assure service

[Learn more about App Assure compatibility assistance](https://www.microsoft.com/fasttrack/microsoft-365/app-assure) to help with porting your Windows app or driver to Arm64. To register and connect with App Assure, visit [aka.ms/AppAssureRequest](https://aka.ms/AppAssureRequest) or send an email to [achelp@microsoft.com](mailto:achelp@microsoft.com) to submit your request for Windows on Arm compatibility support. -->

## Toolchain for Windows on Arm

In addition to support for Visual Studio and LLVM (CLANG) as shared in the [Prerequisites](#prerequisites) section of this guide, the following tools and frameworks are also supported for Arm64:

- [.NET 7](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7-rc-1/#arm64)
- [.NET 6 (LTS)](/dotnet/core/whats-new/dotnet-6#arm64-support)
- [.NET 5.0.8+](https://devblogs.microsoft.com/dotnet/net-july-2021/)
- [.NET Framework 4.8.1](/dotnet/framework/whats-new/#whats-new-in-net-framework-481)
- [clang-cl](https://clang.llvm.org/docs/MSVCCompatibility.html)  compiles C++ code for Windows and can serve as a drop-in replacement for the MSVC compiler and linker. It still uses headers and libraries from MSVC and is ABI-compatible with MSVC.

As well as 3rd-party frameworks, including:

- [Qt for Windows](https://doc.qt.io/qt-6/windows.html), [Boost C++ Library](https://www.boost.org/doc/libs/1_81_0/more/getting_started/windows.html), [Bazel, an open-source build and test tool](https://bazel.build/configure/windows).
- Support for GCC and Mingw / GNU Toolchain for Windows on Arm is [in-progress over at Linaro](https://linaro.atlassian.net/wiki/spaces/WOAR/pages/28802842658/GNU+Toolchain+for+Windows+on+Arm).
- For a more complete list, see [Windows On Arm (WOA) - Confluence (atlassian.net)](https://linaro.atlassian.net/wiki/spaces/WOAR/overview).

## Need assistance? Leverage our App Assure service

The App Assure Arm Advisory Service is available to help developers build Arm-optimized apps. This service is in addition to our existing promise: your apps will run on Windows on Arm, and if you encounter any issues, Microsoft will help you remediate them. [Learn more](https://blogs.windows.com/windowsdeveloper/2023/10/16/windows-launching-arm-advisory-service-for-developers/).

[Sign up for Windows Arm Advisory Services](https://forms.office.com/Pages/ResponsePage.aspx?id=v4j5cvGGr0GRqy180BHbR0hAZezl6y5Om22d_0SBAstUOU9OSlBDQ0dBNkUwTU0ySlNZRklSMFJMViQlQCN0PWcu).
