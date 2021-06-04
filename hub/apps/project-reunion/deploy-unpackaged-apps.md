---
title: Deploy unpackaged apps that use Project Reunion
description: This article provides instructions for deploying unpackaged apps that use Project Reunion.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, project reunion 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Deploy unpackaged apps that use Project Reunion

This article provides guidance about deploying non-[MSIX](/windows/msix) packaged apps that use Project Reunion to other computers.

> [!IMPORTANT]
> Unpackaged app deployment is an experimental feature that is currently supported only in the [preview release channel](preview-channel.md). This feature is not supported for use by apps in production environments.

## Overview

Before configuring your apps for deployment, review [Project Reunion deployment architecture](deployment-architecture.md) to learn more about the dependencies your app takes when it uses Project Reunion, including dynamic dependencies.

You can test deployment of unpackaged apps that use Project Reunion using the Project Reunion installer (.exe). The installer contains a copy of the MSIX packages for the Project Reunion run time components, which includes the [Framework](deployment-architecture.md#framework-packages-for-packaged-and-unpackaged-apps), [Main](deployment-architecture.md#main-package) and [Dynamic Dependency Lifetime Manager (DDLM)](deployment-architecture.md#dynamic-dependency-lifetime-manager-ddlm) packages. By default, the installer will automatically detect the system architecture and install the MSIX packages in either the `X64` or `X86` architectures.

Alternatively, you can directly install the MSIX packages for the Project Reunion runtime components in your development environment or through your app's setup program.

## Prerequisites

- [Download the Project Reunion runtime Installer or MSIX packages](https://aka.ms/projectreunion/0.8preview) to your development computer.
- If your development computer or the deployment computer is running Windows 10 version 1909 or an earlier version, make sure sideloading is enabled. Sideloading is automatically enabled on Windows 10 version 2004 and later.

    > [!NOTE]
    > The Project Reunion runtime for the version 0.8 Preview requires that sideloading is enabled to install.

To confirm whether sideloading is enabled on Windows 10 version 1909 and earlier versions:

1. Open **Settings**.
2. Click **Update & Security** > **For developers**.
3. In the **Use developer features** section, make sure the **Sideload apps** or **Developer mode** setting is selected (the **Developer mode** setting includes sideloading as well as other features).

    > [!NOTE]
    > If the computer is managed in an enterprise environment, the computer may have a policy that disables the ability to modify these settings. If so, you may get an error when you or your app tries to install the Project Reunion runtime. In this case, you must contact your IT Professional to enable sideloading or **Developer mode**. 

## Run the Project Reunion installer in your development environment

You can test deployment in your development environment by running the Project Reunion Installer with this command.

```console
ProjectReunionInstall.exe
```

You can also run the installer with no user interaction and suppress all text output.

```console
ProjectReunionInstall.exe --quiet
```

After the installation is complete, you can run your unpackaged app. For an example of how to build and run an unpackaged app that uses Project Reunion, see [Tutorial: Build and deploy an unpackaged app that uses Project Reunion](tutorial-unpackaged-deployment.md).

## Launch the Project Reunion installer from your setup program

To test deployment of your unpackaged app to end users, you can launch the .exe installer from your setup program or MSI by using [ShellExecute](/windows/win32/shell/launch). This is very similar to other runtime installers you may have used, like .NET, Visual C++, or DirectX.

For a code example that demonstrates how to run the Project Reunion Installer from your setup program, see the **RunInstaller** function in [this example](https://aka.ms/testruninstaller).

## Directly deploy the MSIX packages from your setup program

As an alternative to using the Project Reunion installer for deployment to end users, you can manually deploy the MSIX packages through your app's program or MSI. This option can be best for developers who want more control or need offline installations.

For an example that demonstrates how your setup program can install the MSIX packages, see [this example](https://aka.ms/testinstallpackages).

## Using Project Reunion features at run time

Unpackaged apps must use the [dynamic dependencies API](https://github.com/microsoft/ProjectReunion/blob/main/specs/dynamicdependencies/DynamicDependencies.md) to use Project Reunion features such as WinUI, App lifecycle, MRT Core, and DWriteCore. This feature enables unpackaged apps to dynamically take a dependency on the Project Reunion framework package and any other MSIX framework packages. For more information, see [Additional requirements for unpackaged apps](deployment-architecture.md#additional-requirements-for-unpackaged-apps).

For more information about using the dynamic dependencies API in your unpackaged app, see [Tutorial: Build and deploy an unpackaged app that uses Project Reunion](tutorial-unpackaged-deployment.md).


## Related topics

- [Runtime architecture and deployment scenarios](deployment-architecture.md)
- [Tutorial: Build and deploy an unpackaged app that uses Project Reunion](tutorial-unpackaged-deployment.md)
- [Check for installed versions of the Project Reunion runtime](check-project-reunion-versions.md)
- [Remove outdated Project Reunion runtime versions from your development computer](remove-project-reunion-versions.md)
- [Release channels](release-channels.md)
- [Deploy packaged apps that use Project Reunion](deploy-packaged-apps.md)
