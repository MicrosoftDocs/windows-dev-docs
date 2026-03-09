---
title: Windows 11 Arm ISO files
description: Details on the three main ways to use Arm64 ISO files
ms.date: 11/06/2025
ms.topic: concept-article
ms.service: windows
ms.subservice: arm
ms.reviewer: marcs
#customer intent: As a developer, I have questions about using Arm64 ISOs.
---

# Windows 11 Arm ISO files overview

Windows 11 disk images (ISO files) are now available for Windows 11 on Arm.  You can download the latest Windows 11 Arm64 ISO at [Download Windows 11](https://www.microsoft.com/software-download/windows11arm64).  Just as with x64 ISO files, you can use Arm64 ISO files to create virtual machines, run Windows Setup from within a running copy of Windows, or create bootable media for installing Windows.

## Creating virtual machines

The primary use for Windows 11 Arm64 ISO files is to create virtual machines on local devices for development. 

**Using a Windows on Arm device**: Arm64 ISO files can be used to create a VM in Hyper-V on Windows 11 Arm-based PCs by following the instructions found at [Create a virtual machine with Hyper-V on Windows 11](/virtualization/hyper-v-on-windows/quick-start/create-virtual-machine).

**Using an x64-based Windows device**: Hyper-V on x64 hardware doesn't support Arm64 VMs. To run a Windows on Arm VM from an x64-based device, create an Arm64 VM in the cloud using Azure. Find guidance here: [Quickstart - Create a Windows on Arm VM in the Azure portal](/windows/arm/create-arm-vm).

**Using an Arm-based Apple device**: Arm64 VMs can be created using Mac computers built with Arm-based Apple Silicon. [Learn more](https://support.microsoft.com/windows/options-for-using-windows-11-with-mac-computers-with-apple-m1-m2-and-m3-chips-cd15fd62-9b34-4b78-b0bc-121baa3c568c) about the options available and some of the limitations that apply.

## Mounting and installing Windows

You can install Windows 11 on Arm directly from the ISO file by mounting the ISO file in an already running operating system and running [Windows Setup](/windows-hardware/manufacture/desktop/windows-setup-installation-process). 

To mount the ISO file, right-click the ISO file and select **Mount**. This action creates a virtual bootable disc. Double-click the bootable disc to view the files within. Double-click `setup.exe` to start Windows 11 setup.

## Creating bootable media

Windows on Arm ISOs are designed for creating virtual machines, but you can also use them to create bootable media for installing Windows 11 on an Arm device. Depending on the device, you might need to include drivers from the device manufacturer for the installation media to boot successfully.

To create a bootable USB drive from the Arm64 ISO, you can use a tool such as [Rufus](https://rufus.ie/) (a free, open-source utility). Select the downloaded ISO file and your USB drive in Rufus, then click **Start**. Note that the Windows Media Creation Tool does not currently support creating Arm64 bootable media.

> [!NOTE]
> Use recovery media from your device’s manufacturer instead of creating your own. Recovery media includes the correct drivers that are already tested for your specific device. You can find recovery media for Surface devices at [Surface Recovery Image Download](https://support.microsoft.com/surface-recovery-image).

For devices with a Snapdragon X Series processor, you can boot from an ISO to install Windows without needing extra drivers from device manufacturers. However, while these devices can boot Windows media and install Windows successfully, they don't have full functionality until you install the remaining drivers. To install those drivers and restore functionality, use an ethernet connection via dongle or dock to connect to Windows Update to download the drivers. Once you install the drivers and reboot the device, all subsystems in the device are functional.

Devices with previous generations of Snapdragon processors must have drivers from the device manufacturer injected in the image (see [Add and Remove Driver packages to an offline Windows Image](/windows-hardware/manufacture/desktop/add-and-remove-drivers-to-an-offline-windows-image)). Otherwise, the device might fail to boot or boot to a state where input is non-functional.
