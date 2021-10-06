---
title: Windows App SDK deployment guide for unpackaged apps 
description: This article provides instructions for deploying unpackaged apps that use the Windows App SDK.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for unpackaged apps 

This article provides guidance about deploying non-[MSIX](/windows/msix) packaged apps that use the Windows App SDK to other computers.

> [!IMPORTANT]
> Unpackaged app deployment is currently supported in the [preview release channel](preview-channel.md) and [experimental release channel](experimental-channel.md) of the Windows App SDK. You should not deploy your unpackaged app using the methods described below in production environments until the Stable release. For more info, [see our roadmap](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/roadmap.md).

## Overview

Unpackaged apps use the *dynamic dependencies* support in the Windows App SDK to dynamically take a dependency on the Windows App SDK framework package and any other MSIX framework packages. Dynamic dependencies enables unpackaged applications to keep their existing deployment mechanism, such as MSI or any installer, and be able to leverage the Windows App SDK in their application. Dynamic dependencies can be used by both packaged applications and unpackaged apps, although it is primarily intended to be used by unpackaged apps. To learn more about the dependencies your app takes when it uses the Windows App SDK, see [the Windows App SDK deployment architecture](deployment-architecture.md).

Unpackaged apps have two options available for deploying the Windows App SDK package dependencies, which include the **[framework](deployment-architecture.md#framework-package)**, **[main](deployment-architecture.md#main-package)** and **[Dynamic Dependency Lifetime Manager (DDLM)](deployment-architecture.md#dynamic-dependency-lifetime-manager-ddlm)** packages.

- **The Windows App SDK installer (.exe)**: The silent installer contains a copy of the MSIX packages for the Windows App SDK. By default, the installer will automatically detect the system architecture and install the MSIX packages in either the `X64`,`X86` or `ARM64` architectures.
- **Install the packages directly**: You can have your existing setup or MSI tool carry and install the MSIX packages for the Windows App SDK.

## Prerequisites

- Download the latest Windows App SDK [installer and MSIX packages](https://aka.ms/windowsappsdk/1.0-preview2/msix-installer). For older versions, see [Downloads](downloads.md).

- [Experimental](experimental-channel.md) and [preview](preview-channel.md) versions of the Windows App SDK require that sideloading is enabled to install the runtime.
  - Sideloading is automatically enabled on Windows 10 version 2004 and later.
  - If your development computer or the deployment computer is running Windows 10 version 1909 or an earlier version, confirm whether sideloading is enabled:
    1. Open **Settings**.
    2. Click **Update & Security** > **For developers**.
    3. In the **Use developer features** section, make sure the **Sideload apps** or **Developer mode** setting is selected (the **Developer mode** setting includes sideloading as well as other features).

    > [!NOTE]
    > If the computer is managed in an enterprise environment, the computer may have a policy that disables the ability to modify these settings. If so, you may get an error when you or your app tries to install the the Windows App SDK runtime. In this case, you must contact your IT Professional to enable sideloading or **Developer mode**.

## Deploy Windows App SDK using the .exe installer

You can deploy the Windows App SDK by running the Windows App SDK silent installer:

- **WindowsAppRuntimeInstall.exe** if you are using version 1.0 Preview 1.
- **WindowsAppSDKInstall.exe** if you are using version 1.0 Experimental.
- **ProjectReunionInstall.exe** if you are using version 0.8 Preview.

You should see an output similar to the following:

```console
Deploying package: Microsoft.WindowsAppRuntime.1.0_0.264.801.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.1.0_0.264.801.0_x86__8wekyb3d8bbwe
Package deployment result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.Main.1.0_0.264.801.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.Singleton_0.264.801.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WinAppRuntime.DDLM.0.264.801.0-x6_0.264.801.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WinAppRuntime.DDLM.0.264.801.0-x8_0.264.801.0_x86__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0
```

You can also run the installer with no user interaction and suppress all text output with `WindowsAppRuntimeInstall.exe --quiet` or `WindowsAppSDKInstall.exe --quiet` or `ProjectReunionInstall.exe --quiet`, depending on the Windows App SDK version.

After the installation is complete, you can run your unpackaged app. For an example of how to build and run an unpackaged app that uses the Windows App SDK, see [Tutorial: Build and deploy an unpackaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

### Chain the Windows App SDK installer to your app's setup

If you have a custom setup program for your app, you can chain (include) the Windows App SDK setup process in your app's setup process. The Windows App SDK installer currently does not provide a default UI so you will need to chain by using your setup's custom UI.

You can silently launch and track the Windows App SDK setup while showing your own view of the setup progress by using [ShellExecute](/windows/win32/shell/launch). The Windows App SDK .exe silently unpacks the Windows App MSIX bundle and calls the [PackageManager.AddPackageAsync](/uwp/api/windows.management.deployment.packagemanager.addpackageasync) method to complete the installation. This is very similar to other runtime installers you may have used, like .NET, Visual C++, or DirectX.

For a code example that demonstrates how to run the Windows App SDK installer from your setup program, see the **RunInstaller** function in the [installer functional test](https://aka.ms/testruninstaller) for the Windows App SDK.

### Troubleshooting

#### Return codes

The following table lists the most common return codes for the Windows App SDK .exe installer. The return codes are the same for all versions of the installer.

| Return code | Description                                                                         |
|-------------|-------------------------------------------------------------------------------------|
| 0x0         | Package installation or provisioning was completed successfully.                                    |
| 0x80073d06  | One or more packages failed to install.                                             |
| 0x80070005  | System-wide install or provisioning was unsuccessful.                                 |

#### Installation errors

If the Windows App SDK installer returns an error during installation, it will return an error code that describes the problem.

- See the list of [common error codes](/windows/win32/appxpkg/troubleshooting#common-error-codes).
- If the error code doesn't provide enough information, you can find more diagnostic information in the [detailed event logs](/windows/win32/appxpkg/troubleshooting#get-diagnostic-information).
- Please [file an issue](https://github.com/microsoft/WindowsAppSDK/issues) with the error code and event logs so the issue can be investigated.

## Deploy Windows App SDK MSIX packages directly from your setup program

As an alternative to using the Windows App SDK installer for deployment to end users, you can manually deploy the MSIX packages through your app's program or MSI. This option can be best for developers who want more control.

For an example that demonstrates how your setup program can install the MSIX packages, see [install.cpp](https://aka.ms/testinstallpackages) in the Windows App SDK installer code.

## Deployment scenarios

- **All Windows App SDK MSIX packages are already installed on the computer**: MSIX packages are installed to a system-wide location with only one copy on disk. If an app attempts installation of the Windows App SDK when all the MSIX package dependencies are already installed on the machine, then the installation is not performed.

- **One or more of the Windows App SDK MSIX packages are not installed on the computer**: When deploying the Windows App SDK, always attempt to install all the MSIX packages (framework, main, singleton, DDLM) to ensure that all dependencies are installed and avoid disruption to the end-user experience.

- **Installing the Windows App SDK system-wide**: System-wide install alters the machine for all users. If the app is running elevated and the user has admin privileges, then WindowsAppRuntimeInstall.exe will register the Windows App SDK packages system-wide by calling the [ProvisionPackageForAllUsersAsync](/uwp/api/windows.management.deployment.packagemanager.provisionpackageforallusersasync). Otherwise, the installation will be performed for the current user doing the installation. In a managed Enterprise environment, the IT admin should be able to provision for everyone as usual.

- **Architectures redistributed by the Windows App SDK installer**: The Windows App SDK installer is available in the x86, x64, and ARM64. If you have an x86 app and include the x86 WindowsAppRuntimeInstall.exe, the installation can be performed on an `x86`, `x64` or `ARM64` device.

## Using features at run time

- **Windows App SDK features**: Unpackaged apps must use the *bootstrapper API* before they can use Windows App SDK features such as WinUI, App lifecycle, MRT Core, and DWriteCore. This feature enables unpackaged apps to dynamically take a dependency on the Windows App SDK framework package at run time. For more information, see [Reference the Windows App SDK framework package at run time](reference-framework-package-run-time.md) and [Tutorial: Build and deploy an unpackaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

- **Features in other framework packages**: In addition to the bootstrapper API, the Windows App SDK also provides a broader set of C/C++ functions and WinRT classes that implement the *dynamic dependency API*. This API is designed to be used to reference any framework package dynamically at run time. You only need to use the dynamic dependency API if you want to dynamically reference framework packages other than the Windows App SDK framework package.

For more information, see the [Use MSIX framework packages dynamically from your desktop app](../desktop/modernize/framework-packages/index.md).

## Related topics

- [Runtime architecture](deployment-architecture.md)
- [Tutorial: Build and deploy an unpackaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md)
- [Check for installed versions of the Windows App SDK runtime](check-windows-app-sdk-versions.md)
- [Remove outdated Windows App SDK runtime versions from your development computer](remove-windows-app-sdk-versions.md)
- [Release channels](release-channels.md)
- [Windows App SDK deployment guide for packaged apps](deploy-packaged-apps.md)
