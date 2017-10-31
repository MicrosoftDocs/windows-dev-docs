---
author: laurenhughes
title: App package architectures
description: Learn more about which processor architecture(s) you should use when building your UWP app package.
ms.author: lahugh
ms.date: 7/13/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, packaging, architecture, package configuration
localizationpriority: medium
---

# App package architectures

App packages are configured to run on a specific processor architecture. By selecting an architecture, you are specifying which device(s) you want your app to run on. Universal Windows Platform (UWP) apps can be configured to run on the following architectures:
- x86
- x64
- ARM

It is **highly** recommended that you build your app package to target all architectures. By deselecting a device architecture, you are limiting the number of devices your app can run on, which in turn will limit the amount of people who can use your app!

## Windows 10 devices and architectures

> [!div class="mx-tableFixed"]
| UWP Architecture | Desktop (x86)      | Desktop (x64)      | Desktop (ARM)      | Mobile             | HoloLens           | Xbox               | IoT Core (Device dependent) | Surface Hub        |
|------------------|--------------------|--------------------|--------------------|--------------------|--------------------|--------------------|-----------------------------|--------------------|
| x86              | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :x:                | :heavy_check_mark: | :x:                | :heavy_check_mark:          | :heavy_check_mark: |
| x64              | :x:                | :heavy_check_mark: | :x:                | :x:                | :x:                | :heavy_check_mark: | :heavy_check_mark:          | :heavy_check_mark: |
| ARM              | :x:                | :x:                | :heavy_check_mark: | :heavy_check_mark: | :x:                | :x:                | :heavy_check_mark:          | :x:                |
 

Let’s talk about these architectures in more detail. 

### x86
Choosing x86 is generally the safest configuration for an app package since it will run on nearly every device. On some devices, an app package with the x86 configuration won't run, such as the Xbox or some IoT Core devices. However, for a PC, an x86 package is the safest choice and has the largest reach for device deployment. A substantial portion of Windows 10 devices continue to run the x86 version of Windows. 

### x64
This configuration is used less frequently than the x86 configuration. It should be noted that this configuation is reserved for desktops using 64-bit versions of Windows 10, [UWP apps on Xbox](https://docs.microsoft.com/windows/uwp/xbox-apps/system-resource-allocation), and Windows 10 IoT Core on the Intel Joule.

### ARM
The Windows 10 on ARM configuration includes desktop PCs, mobile devices, and some IoT Core devices (Rasperry Pi 2, Raspberry Pi 3, and DragonBoard). Windows 10 on ARM desktop PCs are a new addition to the Windows family, so if you are UWP app developer, you should submit ARM packages to the Store for the best experience on these PCs. 

Check out this //Build talk to see a demo of [Windows 10 on ARM](https://channel9.msdn.com/Events/Build/2017/P4171) and learn more about how it works. 

For more information IoT specific topics, see [Deploying an App with Visual Studio](https://developer.microsoft.com/windows/iot/Docs/AppDeployment).
