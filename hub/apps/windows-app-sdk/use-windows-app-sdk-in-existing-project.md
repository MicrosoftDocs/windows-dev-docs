---
title: Use the Windows App SDK in an existing project
description: Install the Windows App SDK NuGet package in an existing WPF, WinForms, or Win32 project and initialize the runtime for unpackaged deployment.
ms.topic: how-to
ms.date: 11/18/2026
keywords: windows win32, desktop development, Windows App SDK
ms.localizationpriority: medium
zone_pivot_groups: desktop-framework
---

# Use the Windows App SDK in an existing project

If you have a WPF, WinForms, or Win32 desktop project in which you want to use features of the Windows App SDK, then you can install the Windows App SDK NuGet package in your project.

## Prerequisites

Before you install and use the Windows App SDK NuGet package in your app, be sure these requirements are met:

- Visual Studio is installed and configured for Windows app development.
  
  > [!div class="nextstepaction"]
  > [Set up your development environment](../get-started/start-here.md#set-up-your-development-environment).

- Your WPF, WinForms, or Win32 project is configured to call WinRT APIs.

  > [!div class="nextstepaction"]
  > [Call Windows Runtime APIs in desktop apps](../desktop/modernize/winrt-apis-desktop-apps.md).

## Instructions

1. Open an existing WPF, WinForms, or Win32 project in Visual Studio. Ensure that it's configured to [Call Windows Runtime APIs](../desktop/modernize/winrt-apis-desktop-apps.md).

1. In Visual Studio, open the **NuGet Package Manager**:

    1. Click **Tools > NuGet Package Manager > Manage NuGet Packages for Solution...**.
    <br/>– OR –
    1. Right-click your project in **Solution Explorer**, and choose **Manage NuGet Packages for Solution...**.

1. In the **NuGet Package Manager** window select the **Browse** tab and search for the following package:

    - **Microsoft.WindowsAppSDK**.

1. After you've found the appropriate Windows App SDK NuGet package, select the package, check the box in the right-hand pane of the **NuGet Package Manager** window next to the project where you want to install the package, then click **Install**.

    [![Screenshot of the Windows App SDK NuGet package being installed](images/reunion-nuget-install.png) ](images/reunion-nuget-install.png#lightbox)

    > [!NOTE]
    > The Windows App SDK NuGet package contains other sub-packages (including **Microsoft.WindowsAppSDK.Foundation**, **Microsoft.WindowsAppSDK.WinUI**, and others) that contain the implementations for specific components in the Windows App SDK. In general, we recommend that you install the main Windows App SDK NuGet package, which includes all of the components. In some cases, you can install a sub-package individually in order to reference only certain components in your project. For example, see [Install and deploy Windows ML](/windows/ai/new-windows-ml/distributing-your-app).

### Additional steps for framework-dependent unpackaged apps

If your app is unpackaged and framework-dependent, follow the additional steps below to install the runtime and establish a dynamic Framework dependency.

Packaged apps, including apps packaged with external location (sparse packages), can declare the Framework dependency statically with `<PackageDependency>` in `AppxManifest.xml`; they don't need Bootstrapper initialization for that dependency. Self-contained apps carry the Framework binaries app-locally and don't need a Framework MSIX dependency or Bootstrapper initialization for those binaries. See [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md).

For more info about the terms *packaged* and *unpackaged*, see [Packaging overview](/windows/apps/package-and-deploy/packaging/).


#### 1. Install the Windows App SDK Runtime

The Windows App SDK Runtime needs to be installed on any machine where the app will run.

For your development machine, we recommend that you visit [Latest Windows App SDK downloads](/windows/apps/windows-app-sdk/downloads), then download, unzip, and run either:

-	The latest stable release under **Runtime downloads**.
-	A version and release channel of the runtime that matches the version and release channel of the **Microsoft.WindowsAppSDK** NuGet package that you installed.

Choose the appropriate Installer option for your machine’s architecture.

> [!IMPORTANT]
> When your app is deployed, you will be responsible for deploying required Windows App SDK runtime packages to your end users. For more information, see [Windows App SDK deployment guide for framework-dependent unpackaged apps](deploy-unpackaged-apps.md#prerequisites).

#### 2. Initialize the Windows App SDK Runtime

An unpackaged framework-dependent app must add the Framework package to its process package graph before using APIs from that package.

You can do that automatically when your app starts via *auto-initialization*.

:::zone pivot="dotnet"

1. In **Solution Explorer**, right-click your project, and choose **Edit Project File**.

2. Inside the `PropertyGroup` element, add a `WindowsPackageType` element set to `None`. 

```xml
<WindowsPackageType>None</WindowsPackageType>
```
When you build your project, these files are added to your project in Visual Studio:
-	MddBootstrapAutoInitializer.cs
-	WindowsAppSDK-VersionInfo.cs

:::zone-end

:::zone pivot="win32"

1. Manually edit your `.vcxproj` file.

2. Inside the `<PropertyGroup Label="Globals">` element, add a `WindowsPackageType` element set to `None`. 

```xml
<WindowsPackageType>None</WindowsPackageType>
```

:::zone-end

> [!NOTE]
> If you have advanced needs (such as custom error handling, or to load a specific version of the Windows App SDK), then instead of *auto-initialization* you can call the bootstrapper API explicitly&mdash;for more info, see [Initialize the Windows App SDK Framework package dynamically](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time) and [Tutorial: Use the bootstrapper API in an unpackaged app](tutorial-unpackaged-deployment.md).

## Further info

If you encounter a *Class not registered* error when using a Windows App SDK component, check that your deployment configuration supplies that component. A framework-dependent app must have a Framework dependency in its process package graph, either statically through its package manifest or dynamically through Bootstrapper initialization. A self-contained app must deploy the required app-local binaries. For more info, see [Initialize the Windows App SDK](../package-and-deploy/deploy-overview.md#initialize-the-windows-app-sdk).

## Next steps

> [!div class="nextstepaction"]
> [Deploy apps that use the Windows App SDK](../package-and-deploy/index.yml)

## Related content

- [Windows App SDK](index.md)
- [Release channels and release notes](release-channels.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.yml)
- [Windows App SDK and supported Windows releases](support.md)
