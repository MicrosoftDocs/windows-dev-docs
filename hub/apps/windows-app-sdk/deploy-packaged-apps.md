---
title: Windows App SDK deployment guide for MSIX-packaged apps 
description: This article provides guidance about deploying framework-dependent MSIX-packaged apps (see [What is MSIX?](/windows/msix/overview)) that use the Windows App SDK.
ms.topic: article
ms.date: 04/04/2022
keywords: windows win32, windows app development, Windows App SDK 
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for framework-dependent MSIX-packaged apps 

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This article provides guidance about deploying framework-dependent MSIX-packaged apps (see [What is MSIX?](/windows/msix/overview)) that use the Windows App SDK.

## Overview

By default, when you create a project using one of the [WinUI 3 templates in Visual Studio](..\winui\winui3\winui-project-templates-in-visual-studio.md), your project is configured to build the app into an MSIX package using either single-project MSIX (see [Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)) or a Windows Application Packaging project (see [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net)). You can then build an MSIX package for your app by using the instructions in [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps). After you build an MSIX package for your app, you have several options to [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview).

To learn more about the packages your MSIX-packaged app may need when it uses the Windows App SDK, see [Deployment architecture for the Windows App SDK](deployment-architecture.md). These include the *Framework*, *Main*, and *Singleton* packages, which are all signed and published by Microsoft. There are two main requirements for deploying MSIX-packaged apps:

1. [Deploy the Windows App SDK framework package](#deploy-the-windows-app-sdk-framework-package)
2. [Call the Deployment API](#call-the-deployment-api)

## Prerequisites

* For MSIX-packaged apps, the VCLibs framework package dependency is a requirement. For more info, see [C++ Runtime framework packages for Desktop Bridge](/troubleshoot/cpp/c-runtime-packages-desktop-bridge).
* **C#**. .NET 5 or later is required. For more info, see [.NET Downloads](https://dotnet.microsoft.com/download/dotnet/).

## Deploy the Windows App SDK framework package

The Windows App SDK framework package contains the Windows App SDK binaries used at run time, and it is installed with the application. The framework has different deployment requirements for different channels of the Windows App SDK.

### Stable version

When you install a [stable release](stable-channel.md) version of the Windows App SDK NuGet package on your development computer and you create a project using one of the provided WinUI 3 project templates, the generated package manifest contains a [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element that specifies a dependency on the framework package.

However, if you build your app package manually using a separate Windows Application Packaging Project, you must declare a **PackageReference** in your `Application (package).wapproj`, like the following:

 ```xml
    <ItemGroup>
        <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.0.1">
            <IncludeAssets>build</IncludeAssets>
        </PackageReference>
    </ItemGroup>
```

This package dependency ensures the framework package is installed when your app is deployed to another computer.

### Preview version

When you install a [preview release](preview-channel.md) version of the Windows App SDK NuGet package on your development computer, a preview version of the Windows App SDK framework package is deployed during build time as a NuGet package dependency.

## Call the Deployment API

 The Deployment API is provided by the Windows App SDK framework package and is available in the [Microsoft.Windows.ApplicationModel.WindowsAppRuntime](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime) namespace. The Windows application model does not support declaring a dependency on the Main and Singleton packages. The Deployment API is therefore required for two main reasons:

1. To deploy the Singleton package for features not in the Framework package (e.g., Push Notifications).
2. To deploy the Main package, which enables automatic updates to the Framework package from the Store.

For MSIX-packaged apps *not* distributed through the Store, the developer is responsible for distributing the Framework package. It is recommended to call the Deployment API so that any critical servicing updates are delivered. Note that for using features outside the Framework package (e.g., Push Notifications), the Singleton package must be deployed (this can be done with the Deployment API, or by redistributing the MSIX packages using your own install method). 

> [!IMPORTANT]
> In Windows App SDK version 1.0, only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the Deployment API to install the main and singleton package dependencies. Support for partial trust packaged apps will be coming in later releases. 

You should call the Deployment API after your app's process is initialized, but before your app uses Windows App SDK runtime features that use the Singleton package (e.g., Push Notifications). The main methods of the Deployment API are the static [GetStatus](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.getstatus) and [Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) methods of the [DeploymentManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager) class.

- The **GetStatus** method returns the current deployment status of the Windows App SDK runtime that is currently loaded. Use this method to identify if there is work required to install Windows App SDK runtime packages before the current app can use Windows App SDK features.
- The **Initialize** method verifies whether all required packages are present to a minimum version needed by the Windows App SDK runtime that's currently loaded. If any package dependencies are missing, the method attempts to register those missing packages. In Windows App SDK 1.1 Preview 2 and later, the **Initialize** method also supports the option to force-deploy the Windows App SDK runtime packages. That shuts down any processes for the *Main* and *Singleton* runtime packages, and thus interrupts their services (for example, Push Notifications will not deliver notifications during this time).

### Deployment API sample 

For additional guidance on how to use the GetStatus and Initialize methods of the DeploymentManager class, explore the available sample. 

> [!div class="button"]
> [Explore Deployment API sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/DeploymentManager)

### Address installation errors

If the Deployment API encounters an error during installation of the Windows App SDK runtime packages, it will return an error code that describes the problem. 

For example, if your app is not full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability, you will get an **ACCESS_DENIED** error code. To review other error codes you may encounter and their possible causes, see [Troubleshooting packaging, deployment, and query of Windows apps](/windows/win32/appxpkg/troubleshooting#common-error-codes).

If the error code doesn't provide enough information, you can find more diagnostic information in the [detailed event logs](/windows/win32/appxpkg/troubleshooting#get-diagnostic-information).

If you encounter errors that you can't diagnose, [file an issue](https://github.com/microsoft/WindowsAppSDK/issues) with the error code and event logs so we can investigate the issue.

## Related topics

* [Deployment architecture for the Windows App SDK](deployment-architecture.md)
* [Deploying non-MSIX-packaged apps](deploy-unpackaged-apps.md)
* [Release channels](release-channels.md)
* [Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)
