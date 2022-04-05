---
title: Windows App SDK deployment guide for fx-dependent non-MSIX-packaged apps 
description: This article provides guidance about deploying non-MSIX-packaged apps (see [What is MSIX?](/windows/msix/overview)) that use the Windows App SDK. Non-MSIX-packaged apps include sparse-packaged and unpackaged apps.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for fx-dependent non-MSIX-packaged apps 

This article provides guidance about deploying framework-dependent non-MSIX-packaged apps (see [What is MSIX?](/windows/msix/overview)) that use the Windows App SDK. Non-MSIX-packaged apps include sparse-packaged and unpackaged apps.

## Overview

Non-MSIX-packaged apps use the *dynamic dependencies* support in the Windows App SDK to dynamically take a dependency on the Windows App SDK framework package and any other MSIX framework packages. Dynamic dependencies enables non-MSIX-packaged applications to keep their existing deployment mechanism, such as MSI or any installer, and be able to leverage the Windows App SDK in their application. Dynamic dependencies can be used by both packaged applications and non-MSIX-packaged apps, although it is primarily intended to be used by non-MSIX-packaged apps. 

> [!IMPORTANT]
> Before configuring your app for deployment, and to learn more about the dependencies your non-MSIX-packaged app takes when it uses the Windows App SDK, see [Deployment architecture for the Windows App SDK](deployment-architecture.md). These dependencies include the *framework*, *main*, and *Dynamic Dependency Lifetime Manager (DDLM)* packages, which are all signed and published by Microsoft.

Non-MSIX-packaged apps have two options available for deploying the Windows App SDK package dependencies:

- **The Windows App SDK runtime installer (.exe)**: The silent installer contains a copy of the MSIX packages for the Windows App SDK. By default, the installer will automatically detect the system architecture and install the MSIX packages in either the `X64`,`X86` or `ARM64` architectures.
- **Install the packages directly**: You can have your existing setup or MSI tool carry and install the MSIX packages for the Windows App SDK.

## Prerequisites

* [Download the latest installer & MSIX packages](downloads.md).
* For non-MSIX-packaged apps, the Visual C++ Redistributable is a requirement. For more info, see [Microsoft Visual C++ Redistributable latest supported downloads](/cpp/windows/latest-supported-vc-redist).
* **C#**. .NET 5 or later is required. For more info, see [.NET Downloads](https://dotnet.microsoft.com/download/dotnet/).

### Additional prerequisites details

* For downloads for other versions of the Windows App SDK, see [Downloads for the Windows App SDK](downloads.md).
* [Experimental](experimental-channel.md) and [preview](preview-channel.md) versions of the Windows App SDK require that sideloading is enabled to install the runtime.
  - Sideloading is automatically enabled on Windows 10 version 2004 and later.
  - If your development computer or the deployment computer is running **Windows 11**, confirm whether sideloading is enabled:
    - **Settings** > **Privacy & security** > **For developers**. Make sure the **Developer mode** setting is turned on.
  - If your development computer or the deployment computer is running **Windows 10 version 1909 or an earlier version**, confirm whether sideloading is enabled:
    - **Settings** > **Update & Security** > **For developers** > **Use developer features**. Confirm that **Sideload apps** or **Developer mode** is selected.
  - The **Developer mode** setting includes sideloading as well as other features

    > [!NOTE]
    > If the computer is managed in an enterprise environment, there might be a policy preventing these settings from being changed. In that case if you get an error when you or your app tries to install the Windows App SDK runtime, contact your IT Professional to enable sideloading or **Developer mode**.

## Deploy Windows App SDK using the .exe installer

You can deploy the Windows App SDK by running the Windows App SDK silent installer:

- **WindowsAppRuntimeInstall.exe** if you're using 1.0 Stable or Preview versions.
- **WindowsAppSDKInstall.exe** if you're using 1.0 Experimental.
- **ProjectReunionInstall.exe** if you're using version 0.8 and earlier. 

You should see an output similar to the following:

```console
Deploying package: Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x86__8wekyb3d8bbwe
Package deployment result : 0x0

Deploying package: MicrosoftCorporationII.WindowsAppRuntime.Main.1.0_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.Singleton_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WinAppRuntime.DDLM.0.318.928.0-x6_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WinAppRuntime.DDLM.0.318.928.0-x8_0.318.928.0_x86__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

All install operations successful.
```

You can also run the installer with no user interaction and suppress all text output with `WindowsAppRuntimeInstall.exe --quiet` or `WindowsAppSDKInstall.exe --quiet` or `ProjectReunionInstall.exe --quiet`, depending on the Windows App SDK version.

After the installation is complete, you can run your non-MSIX-packaged app. For an example of how to build and run a non-MSIX-packaged app that uses the Windows App SDK, see [Tutorial: Build and deploy a non-MSIX-packaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

### Chain the Windows App SDK installer to your app's setup

If you have a custom setup program for your app, you can chain (include) the Windows App SDK setup process in your app's setup process. The Windows App SDK installer currently does not provide a default UI so you will need to chain by using your setup's custom UI.

You can silently launch and track the Windows App SDK setup while showing your own view of the setup progress by using [ShellExecute](/windows/win32/shell/launch). The Windows App SDK .exe silently unpacks the Windows App MSIX bundle and calls the [PackageManager.AddPackageAsync](/uwp/api/windows.management.deployment.packagemanager.addpackageasync) method to complete the installation. This is very similar to other runtime installers you may have used, like .NET, Visual C++, or DirectX.

For a code example that demonstrates how to run the Windows App SDK installer from your setup program, see the **RunInstaller** function in the [installer functional test](https://aka.ms/testruninstaller) for the Windows App SDK.

### Installer sample 

For additional guidance on how to launch the WindowsAppRuntimeInstaller from a Win32 setup program without popping up a console window during setup, see the [Explore installer sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Installer).

### Troubleshooting

#### Return codes

The following table lists the most common return codes for the Windows App SDK .exe installer. The return codes are the same for all versions of the installer.

| Return code | Description                                                                         |
|-------------|-------------------------------------------------------------------------------------|
| 0x0         | Package installation or provisioning was completed successfully.                                    |
| 0x80073d06  | One or more packages failed to install.                                             |
| 0x80070005  | System-wide install or provisioning was not possible because the app is not running elevated or the user doing the installation doesn't have admin privileges.                                |

#### Installation errors

If the Windows App SDK installer returns an error during installation, it will return an error code that describes the problem.

- See the list of [common error codes](/windows/win32/appxpkg/troubleshooting#common-error-codes).
- If the error code doesn't provide enough information, you can find more diagnostic information in the [detailed event logs](/windows/win32/appxpkg/troubleshooting#get-diagnostic-information).
- Please [file an issue](https://github.com/microsoft/WindowsAppSDK/issues) with the error code and event logs so the issue can be investigated.

## Deploy Windows App SDK MSIX packages directly from your setup program

As an alternative to using the Windows App SDK installer for deployment to end users, you can manually deploy the MSIX packages through your app's program or MSI. This option can be best for developers who want more control.

For an example that demonstrates how your setup program can install the MSIX packages, see [install.cpp](https://aka.ms/testinstallpackages) in the Windows App SDK installer code.

## Deployment scenarios

- **Installing the Windows App SDK system-wide**: System-wide install alters the machine for all users, including new users that are added in the future. If the app is running elevated and the user doing the installation has admin privileges, then WindowsAppRuntimeInstall.exe will register the Windows App SDK packages system-wide by calling the [ProvisionPackageForAllUsersAsync](/uwp/api/windows.management.deployment.packagemanager.provisionpackageforallusersasync). If system-wide registration is not successful, the installation will be performed for the current user doing the installation only. In a managed Enterprise environment, the IT admin should be able to provision for everyone as usual.

- **Architectures redistributed by the Windows App SDK installer**: The Windows App SDK installer is available in the `x86`, `x64` and `ARM64` architectures. Each version of the installer also includes the MSIX packages for all architectures. For example, if you run the x86 WindowsAppRuntimeInstall.exe on an x64 or ARM64 device, the installer will deploy the packages for that device architecture. 

- **All Windows App SDK MSIX packages are already installed on the computer**: MSIX packages are installed to a system-wide location with only one copy on disk. If an app attempts installation of the Windows App SDK when all the MSIX package dependencies are already installed on the machine, then the installation is not performed.

- **One or more of the Windows App SDK MSIX packages are not installed on the computer**: When deploying the Windows App SDK, always attempt to install all the MSIX packages (framework, main, singleton, DDLM) to ensure that all dependencies are installed and you avoid disruption to the end-user experience.


## Using features at run time

- **Windows App SDK features**: Non-MSIX-packaged apps must use the *bootstrapper API* before they can use Windows App SDK features such as WinUI, App lifecycle, MRT Core, and DWriteCore. This feature enables non-MSIX-packaged apps to dynamically take a dependency on the Windows App SDK framework package at run time. For more information, see [Reference the Windows App SDK framework package at run time](reference-framework-package-run-time.md) and [Tutorial: Build and deploy a non-MSIX-packaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

- **Features in other framework packages**: In addition to the bootstrapper API, the Windows App SDK also provides a broader set of C/C++ functions and WinRT classes that implement the *dynamic dependency API*. This API is designed to be used to reference any framework package dynamically at run time. You only need to use the dynamic dependency API if you want to dynamically reference framework packages other than the Windows App SDK framework package.

For more information, see the [Use MSIX framework packages dynamically from your desktop app](../desktop/modernize/framework-packages/index.md).

## Deploy .winmd files to the target machine

Along with your app, we recommend that you go ahead and deploy Windows Metadata (`.winmd`) files. Metadata can be used by various APIs and behaviors at runtime, and its absence can limit or break functionality. For example, metadata can be used to marshal objects across apartments boundaries; and the need to marshal can be a function of machine performance. Since there's no deterministic way to know whether you need metadata, you should deploy `.winmd`s unless you're extremely concerned about size.

## Related topics

* [Deployment architecture for the Windows App SDK](deployment-architecture.md)
- [Windows App SDK deployment guide for packaged apps](deploy-packaged-apps.md)
- [Release channels](release-channels.md)
- [Tutorial: Build and deploy a non-MSIX-packaged app that uses the Windows App SDK](tutorial-unpackaged-deployment.md)
- [Check for installed versions of the Windows App SDK runtime](check-windows-app-sdk-versions.md)
- [Remove outdated Windows App SDK runtime versions from your development computer](remove-windows-app-sdk-versions.md)
