---
title: Windows Dev Kit 2023 (Project Volterra)
description: Powered by Arm64 and running Windows 11, this desktop device enables you to develop Windows apps for Arm, on Arm. Find device specifications, set up instructions, Arm-native developer tools, Support, and FAQs. 
ms.date: 10/21/2022
ms.topic: article
ms.prod: windows
ms.technology: arm
author: mattwojo
ms.author: mattwoj
---

# Windows Dev Kit 2023

Windows Dev Kit 2023 (code name “Project Volterra”) is the latest Arm device built for Windows developers with a Neural Processing Unit (NPU) that provides best-in-class AI computing capacity, multiple ports, and a stackable design for desktops and rack deployment. Purpose-built with everything you need to develop, debug, and test native Windows apps for Arm.

> [!VIDEO https://www.youtube.com/embed/WMdc5tk_Wp0]

## Device specifics

- 32GB LPDDR4x RAM and 512GB fast NVMe storage
- Snapdragon® 8cx Gen 3 compute platform
- Ports: 3x USB-A, 2x USB-C, Mini-Display (HBR2 support), Ethernet (RJ45)
- Made with 20% recycled ocean plastic
- Available in the [Microsoft Store](https://aka.ms/windowsdevkit2023)

![Windows Dev Kit 2023](./images/windowsdevkit2023-side.png)

## Device set up

When the device is turned on and connects to the Internet for the first time, follow the getting started prompts and configuration for Windows Update to ensure the latest software is running on the device.

### Identify buttons and external ports

With the device flat on the table, the 3 buttons on the left side of the device, from left to right, are:

- **Boot to USB button**: Hold the Power button + the Boot to USB button to boot to the USB thumb drive. This method can be used to re-image the device with latest operating system build.
- **UEFI button**: Hold the Power button + the UEFI button to boot into UEFI menu.
- **Power button**

All external ports will be available after the device boots into Windows 11 including:

- RJ45 for ethernet
- 3 x USB-A ports
- 2 x USB-C ports
- Bluetooth & WiFi

The device supports up to three displays by using the mDP port and the two USB-C ports.

> [!NOTE]
> Unified Extensible Firmware Interface (UEFI) replaces the standard basic input/output system (BIOS) with new features including faster startup and improved security. You can use UEFI to manage the firmware features on your device.

### Set up power

The dev kit includes a 90W power supply. Attach the power supply to the back on the far left of the device.

- The device will default into "Connected Standby Mode" when not in use. You can choose to [hibernate the device](/windows-hardware/customize/power-settings/adaptive-hibernate) using OS controls.
- Fan control is supported and controlled by firmware. The Fan will come on as needed to manage thermal load.
- There is no battery on the device, hence the system will only run on AC. There is no DC mode to test against.

### Set up display

Use the **mDP port** as your main display for setting up this device. The UEFI output defaults to mDP port.

- If the display is connected to USB-C only, you won't see the UEFI startup screen output when you turn on the device. It takes  ~25 seconds for the display to light up after power button is pressed.
- If connecting an HDMI monitor to the mDP port, an active mini-DP to HDMI adapter is required. Use the Surface Mini DisplayPort to HDMI 2.0 Adapter provided with your device.
- When connecting an external keyboard or mouse, use the USB-A ports, not USB-C. Using USB-C to connect a keyboard or mouse will only work intermittently.

|Ports | Transmission Mode | Max Data Speed | Supported Displays (max resolution) | Comments |
|---|---|---|---|---|
|mDP| HBR2| 4 lane x 5.4 Gbps/lane | SST: 3840 x 2160 @ 60Hz, MST: (x2) 2560 x 1600 @ 60Hz | Default monitor port to boot with UEFI menu |
|USB-C (x2) | HBR3 |4 lane x 8.1 Gbps/lane | SST: 5120x2880 @ 60Hz, SST: 4096x2160 @ 60Hz, MST: (x2) 3840x2160 @ 60Hz (RB2), MST: (x2) 2560x1600 @ 60Hz (CVT, RB) | Default monitor port to boot without UEFI menu |

## Install Arm-native developer tools

A fully Arm-native suite of developer tools are available for installing on Windows 11, including:

- **[Visual Studio 2022 17.4 Preview 2 for Arm64](/visualstudio/install/visual-studio-on-arm-devices)**

    This is the first native Arm64 version of Visual Studio available with workloads enabled for desktop development with C++ (for MSBuild-based projects), .NET desktop development, and web development. Additionally our latest preview includes support for Windows SDK and Win App SDK components (Win UI).

- **[.NET 7 Preview Arm64 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)**

    Native support for Arm64 is [available in .NET 6](/dotnet/core/install/windows?tabs=net60#arm-based-windows-pcs), along with the [.NET Framework 4.8.1 runtime](https://devblogs.microsoft.com/dotnet/announcing-dotnet-framework-481/) and SDK, but that support has been extended in .NET 7. Read more about [Arm64 performance improvements in .NET 7](https://devblogs.microsoft.com/dotnet/arm64-performance-improvements-in-dotnet-7/).

- **[Visual Studio Code for Arm](https://code.visualstudio.com/download)**

    VS Code has supported an Arm64 architecture since the September 2020 version 1.50 release, including [extensions for Remote Development](https://code.visualstudio.com/docs/remote/remote-overview).

Bringing together local compute on the CPU, GPU, and NPU and cloud compute with Azure, including:

- **[Qualcomm Neural Processing SDK for AI](https://developer.qualcomm.com/qualcomm-robotics-rb5-kit/software-reference-manual/machine-learning/snpe)**

    Formerly known as the Snapdragon Neural Processing Engine (SNPE), this software accelerated, inference-only runtime engine supports the execution of deep neural networks. Build apps that unlock the power of integrated NPU hardware to accelerate AI/ML workloads without compromising app performance.

- **[SNPE Execution Provider for ONNX Runtime](https://onnxruntime.ai/docs/execution-providers/SNPE-ExecutionProvider.html)**

    This execution provider makes use of the [Qualcomm Snapdragon Neural Processing Engine SDK](https://developer.qualcomm.com/sites/default/files/docs/snpe/overview.html) to enable hardware accelerated execution of deep neural networks.

- **[Azure Virtual Machines with Ampere Altra Arm-based processors](https://azure.microsoft.com/blog/now-in-preview-azure-virtual-machines-with-ampere-altra-armbased-processors/)**

    Engineered to efficiently run scale-out workloads, web servers, application servers, open-source databases, cloud-native as well as rich .NET applications, Java applications, gaming servers, media servers, and more.

Support for building Arm-native apps and porting existing x64 apps is also available, including:

- **[Arm64EC](./arm64ec.md)**

    Arm64EC (“Emulation Compatible”) is a new application binary interface (ABI) enabling you to build new native apps or incrementally transition existing x64 apps to take advantage of the native speed and performance possible with Arm-powered devices, including better power consumption, battery life, and accelerated AI & ML workloads.

- **[Arm64X](./arm64x-pe.md)**

    Arm64X is a new type of binary that can contain both the classic Arm64 code and Arm64EC code together, making it a particularly good fit for middleware or plugins that may be used by both ABIs.

Additional developer tools supported by Windows 11 on Arm, include:

- **[Windows Subsystem for Linux](/windows/wsl)**

    Enabling Linux distributions to be installed on Windows without the overhead of a traditional virtual machine or dual-boot setup.

- **[Windows Terminal](/windows/terminal)**

    A modern way to run multiple command lines side-by-side in tabs or panes, fully customizable with a GPU-accelerated text rendering engine and command palette.

- **[Windows Package Manager](/windows/package-manager)**

    Offering a comprehensive package manager solution that consists of a command line tool (winget) and set of services for installing applications that will choose the best available package based on your hardware architecture.

- **[Microsoft PowerToys](/windows/powertoys)**

    A set of utilities for power users to tune and streamline their Windows experience for greater productivity, including the FancyZones window manager, a keyboard manager, mouse utilities, PowerRename, and more.

- **[Windows Subsystem for Android](/windows/android/wsa)**

    Enabling Windows 11 to run Android applications that are available in the Amazon Appstore.

## Support

For hardware or warranty support with your Windows on Arm developer kit, open a support request on the **[Support for business](https://support.serviceshub.microsoft.com/supportforbusiness/onboarding?origin=/supportforbusiness/create)** services hub page.

## FAQs

- **How do I set up a recovery drive?**

    To create a USB recovery drive in order to capture the default device state to return to as needed, you will need an empty 16gb USB drive. (This process will erase any data already stored on the drive.)

    1. In the search box on the task bar, search for **Create a recovery drive**. After selecting, you may be asked to enter an admin password or confirm your choice.

    2. When the tool opens, ensure **Back up system files to the recovery drive** is selected. Select Next.

    3. Connect a USB drive, select it, select Next, and then Create. Many files will be copied to the recovery drive, so this will take some time.

    To boot your dev kit device from a recovery drive:

    1. Connect your USB drive and then hold the Power button + the UEFI button to boot into UEFI menu.

    2. Once in UEFI, use the external USB-A keyboard or mouse to navigate to the Boot Configuration menu.

    3. Double-click on **USB Storage** to boot to the USB key.

- **How do I update a driver to work on a Windows 11 Arm-based PC?**

    Drivers for hardware, games, and apps may only work if they're designed for a Windows 11 Arm-based PC. Check directly with the organization that developed the driver to find relevant Arm64 updates.

- **Does this device support assistive technology?**

    Windows 11 provides [built-in accessibility features](https://www.microsoft.com/Accessibility/windows) that help you do more on your device, in addition to assistive technology apps in the Microsoft Store, such as the [OneStep Reader](https://apps.microsoft.com/store/detail/onestep-reader/9NBLGGH6HQKK?hl=en-us&gl=us) or the [Read &Write](https://microsoftedge.microsoft.com/addons/detail/readwrite-for-microsoft-/bjglhpoliipklkfjcahfefdlfpifcinb) extension for Microsoft Edge. NVDA also offers a Windows 11 Arm-based screen reader (see the [NV Access download site](https://www.nvaccess.org/download/)). Check the [Microsoft Store](https://apps.microsoft.com/store/apps) or contact your assistive software vendor to see if your preferred apps are available for a Windows 11 Arm-based PC.

To learn more, see **[FAQs for Windows Arm-based PCs](https://support.microsoft.com/en-us/windows/windows-arm-based-pcs-faq-477f51df-2e3b-f68f-31b0-06f5e4f8ebb5)**.
