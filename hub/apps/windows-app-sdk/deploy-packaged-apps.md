---
title: Windows App SDK deployment guide for framework-dependent packaged apps
description: This article provides guidance about deploying framework-dependent packaged apps (see [What is MSIX?](/windows/msix/overview)) that use the Windows App SDK.
ms.topic: article
ms.date: 08/19/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for framework-dependent packaged apps

This article provides guidance about deploying framework-dependent packaged apps (see [What is MSIX?](/windows/msix/overview)) that use the Windows App SDK. The equivalent topic for other framework-dependent packaging options is [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md).

## Overview

By default, when you create a project using one of the [WinUI 3 templates in Visual Studio](..\winui\winui3\winui-project-templates-in-visual-studio.md), your project is configured to build the app into an MSIX package using either single-project MSIX (see [Package your app using single-project MSIX](./single-project-msix.md)) or a Windows Application Packaging project (see [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net)). You can then build an MSIX package for your app by using the instructions in [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps). After you build an MSIX package for your app, you have several options to [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview).

To learn more about the packages that your packaged app might need when it uses the Windows App SDK, see [Deployment architecture for the Windows App SDK](deployment-architecture.md). Those include the *Framework*, *Main*, and *Singleton* packages; which are all signed and published by Microsoft. There are two main requirements for deploying a packaged app:

1. [Deploy the Windows App SDK framework package](#deploy-the-windows-app-sdk-framework-package).
2. [Call the Deployment API](#call-the-deployment-api).

## Prerequisites

* For packaged apps, the VCLibs framework package dependency is a requirement. For more info, see [C++ Runtime framework packages for Desktop Bridge](/troubleshoot/cpp/c-runtime-packages-desktop-bridge).
* **C#**. .NET 6 or later is required. For more info, see [.NET Downloads](https://dotnet.microsoft.com/download/dotnet/).

## Deploy the Windows App SDK framework package

The Windows App SDK framework package contains the Windows App SDK binaries used at run time, and it is installed with your application. The framework has different deployment requirements for different channels of the Windows App SDK.

### Stable version

When you install a stable release version (see [Stable channel release notes](stable-channel.md)) of the Windows App SDK NuGet package on your development computer, and you create a project using one of the provided WinUI 3 project templates, the generated package manifest contains a [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element that specifies a dependency on the framework package.

However, if you build your app package manually using a separate Windows Application Packaging Project, then you must declare a **PackageReference** in your `Application (package).wapproj` file, like the following:

 ```xml
<ItemGroup>
    <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.0.1">
        <IncludeAssets>build</IncludeAssets>
    </PackageReference>
</ItemGroup>
```

That package dependency ensures that the Framework package is installed when your app is deployed to another computer.

### Preview version

When you install a preview release version (see [Preview channel release notes](preview-channel.md)) of the Windows App SDK NuGet package on your development computer, a preview version of the Windows App SDK framework package is deployed during build time as a NuGet package dependency.

## Call the Deployment API

Also see [Initialize the Windows App SDK](../package-and-deploy/deploy-overview.md#initialize-the-windows-app-sdk).

The Deployment API is provided by the Windows App SDK framework package, and is available in the [Microsoft.Windows.ApplicationModel.WindowsAppRuntime](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime) namespace. The Windows application model doesn't support declaring a dependency on the Main and Singleton packages. The Deployment API is therefore required for these reasons:

1. To deploy the Singleton package for features not in the Framework package (for example, push notifications).
2. To deploy the Main package, which enables automatic updates to the Framework package from the Microsoft Store.

For packaged apps that are *not* distributed through the Store, you as the developer are responsible for distributing the Framework package. We recommended that you call the Deployment API so that any critical servicing updates are delivered. Note that for using features outside the Framework package (for example, push notifications), the Singleton package must be deployed (this can be done with the Deployment API, or by redistributing the MSIX packages using your own install method).

> [!IMPORTANT]
> In Windows App SDK version 1.0, only packaged apps that are full trust or that have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the Deployment API to install the Main and Singleton package dependencies. Support for partial trust packaged apps will be coming in later releases. 

You should call the Deployment API after your app's process is initialized, but before your app uses Windows App SDK runtime features that use the Singleton package (for example, push notifications). The main methods of the Deployment API are the static [GetStatus](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.getstatus) and [Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) methods of the [DeploymentManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager) class.

* The **GetStatus** method returns the current deployment status of the Windows App SDK runtime that's currently loaded. Use this method to identify whether there's work required to install Windows App SDK runtime packages before the current app can use Windows App SDK features.
* The **Initialize** method verifies whether all required packages are present to a minimum version needed by the Windows App SDK runtime that's currently loaded. If any package dependencies are missing, then the method attempts to register those missing packages. Beginning in Windows App SDK 1.1, the **Initialize** method also supports the option to force-deploy the Windows App SDK runtime packages. That shuts down any processes for the *Main* and *Singleton* runtime packages, and thus interrupts their services (for example, push notifications won't deliver notifications during this time).

### Deployment API sample app

For additional guidance on how to use the **GetStatus** and **Initialize** methods of the **DeploymentManager** class, explore the available sample app. 

> [!div class="button"]
> [Explore Deployment API sample app](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/DeploymentManager)

### Address installation errors

If the Deployment API encounters an error during installation of the Windows App SDK runtime packages, it returns an error code that describes the problem. 

For example, if your app is not full trust, or doesn't have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability, then you'll get an **ACCESS_DENIED** error code. To review other error codes that you may encounter and their possible causes, see [Troubleshooting packaging, deployment, and query of Windows apps](/windows/win32/appxpkg/troubleshooting#common-error-codes).

If the error code doesn't provide enough information, then you can find more diagnostic information in the detailed event logs (see [Get diagnostic information](/windows/win32/appxpkg/troubleshooting#get-diagnostic-information)).

If you encounter errors that you can't diagnose, then file an issue in the [WindowsAppSDK GitHub repo](https://github.com/microsoft/WindowsAppSDK/issues) with the error code and event logs so that we can investigate the issue.

## Related topics

* [Deployment architecture for the Windows App SDK](deployment-architecture.md)
* [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md)
* [Release channels](release-channels.md)
* [Package your app using single-project MSIX](./single-project-msix.md)