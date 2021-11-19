---
title: Windows App SDK deployment guide for packaged apps 
description: This article provides instructions for deploying packaged apps that use the Windows App SDK.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for packaged apps 

This article provides guidance about deploying [MSIX](/windows/msix)-packaged apps that use the Windows App SDK to other computers.

By default, when you create a project using one of the [WinUI project templates](..\winui\winui3\winui-project-templates-in-visual-studio.md) that are provided with the Windows App SDK extension for Visual Studio, your project includes a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an MSIX package. For more information about configuring this project to build an MSIX package for your app, see [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps). After you build an MSIX package for your app, you have several options for deploying it to other computers. For more information, see [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview).

Before configuring your apps for deployment, review [the Windows App SDK deployment architecture](deployment-architecture.md) to learn more about the dependencies your packaged app takes when it uses the Windows App SDK. These dependencies include the **framework**, **main**, and **singleton** packages, which are all signed and published by Microsoft. 

## Deploy the Windows App SDK framework package

The Windows App SDK framework package contains the Windows App SDK binaries used at run time, and it is installed with the application. The framework has different deployment requirements for different versions of the Windows App SDK.

### Preview version

When you install a [preview release channel](preview-channel.md) version of the Windows App SDK extension for Visual Studio or the Windows App SDK NuGet package on your development computer, the preview version of the [framework package](deployment-architecture.md#framework-package) is deployed during build time as a NuGet package dependency.

### Stable version

When you install a [stable release channel](stable-channel.md) version of the Windows App SDK extension or NuGet package on your development computer and you create a project using one of the provided WinUI 3 project templates, the generated package manifest contains a [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element that specifies a dependency on the framework package.

However, if you build your app package manually, you must declare **PackageReference** in your Application (package).wapproj. For version specific instructions, see [Update existing projects to the latest release of the Windows AppSDK](update-existing-projects-to-the-latest-release.md).

 ```xml
    <ItemGroup>
        <PackageReference Include="Microsoft.ProjectReunion" Version="[0.8.0]">
        <IncludeAssets>build</IncludeAssets>
        </PackageReference>
    </ItemGroup>
```

As a result of that declared dependency, the framework package is installed when your app is deployed to another computer.

## Deploy the Windows App SDK main and singleton package

The main package contains out-of-process services that are brokered between apps, such as push notifications, and it is also needed for the framework to be serviced by the Microsoft Store. The singleton package supports a single long-running process that is brokered between apps for features like push notifications. 

**Scenarios that require these packages:** The main and singleton packages are only needed if developers want to use features not included in the framework, such as Push Notifications, and prefer to have the framework automatically updated by the Store without needing to redistribute their packaged app.  


### Use the deployment API

While the Windows application model supports framework dependencies, it does not support a packaged app (a main package) declaring a dependency on other main packages (the Windows App SDK main and singleton packages). So the framework package that is installed with your app will have the main and singleton package embedded within it, but your packaged app must use the *deployment API* in the [Microsoft.Windows.ApplicationModel.WindowsAppRuntime](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime) namespace to get those packages installed on the machine.

The deployment API is provided by the Windows App SDK framework package. You should call the API after your app's process is initialized but before your app uses Windows App SDK runtime content in the main and singleton packages. 

> [!IMPORTANT]
> In Windows App SDK version 1.0, only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the deployment API to install the main and singleton package dependencies. Support for partial trust packaged apps will be coming in later releases. 

The main methods of the deployment API are the static [GetStatus](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.getstatus) and [Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) methods of the [DeploymentManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager) class.

- The **GetStatus** method returns the current deployment status of the Windows App SDK runtime that is currently loaded. Use this method to identify if there is work required to install Windows App SDK runtime packages before the current app can use Windows App SDK features.
- The **Initialize** method verifies whether all required packages are present to a minimum version needed by the loaded Windows App SDK runtime. If any package dependencies are missing, it attempts to register those missing packages.

#### Deployment API sample 

For additional guidance on how to use the GetStatus and Initialize methods of the DeploymentManager class, explore the available sample. 

> [!div class="button"]
> [Explore Deployment API sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/DeploymentManager)

### Address installation errors

If the deployment API encounters an error during installation of the main and singleton packages, it will return an error code that describes the problem. 

For example, if your app is not full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability, you will get an **ACCESS_DENIED** error code. To review other error codes you may encounter and their possible causes, see [Troubleshooting packaging, deployment, and query of Windows apps](/windows/win32/appxpkg/troubleshooting#common-error-codes).

If the error code doesn't provide enough information, you can find more diagnostic information in the [detailed event logs](/windows/win32/appxpkg/troubleshooting#get-diagnostic-information).

If you encounter errors that you can't diagnose, [file an issue](https://github.com/microsoft/WindowsAppSDK/issues) with the error code and event logs so we can investigate the issue.

## Related topics

- [Runtime architecture](deployment-architecture.md)
- [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md)
- [Release channels](release-channels.md)
