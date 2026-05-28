---
title: Project properties and auto-initializers
description: Describes the project properties that you can set in your Visual Studio project file to customize how your app is deployed, including configuring auto-initializers.
ms.topic: article
ms.date: 05/28/2026
ms.localizationpriority: medium
---

# Project properties and auto-initializers

This topic describes the project properties that you can set in your Visual Studio project file (such as `.csproj` or `.vcxproj`) in order to customize how your app is deployed, including configuring auto-initializers.

## Auto-initializers in the Windows App SDK

In the Windows App SDK, there are several routines whose job it is to ensure that the Windows App Runtime is properly initialized. These routines are known as auto-initializers, because they run automatically before your application's entry point, and do initialization work for you.

> [!TIP]
> In case you're curious about technical details. In C++, an auto-initializer is implemented with a static class constructor. In C#, an auto-initializer is implemented with a .NET module initializer. So you might sometimes hear *module initializer* used when the proper term is *auto-initializer*.

All of the auto-initializers are conditionally enabled by default, based on your app's packaging and deployment configuration. Here are details about them:

* Bootstrapper (also known as dynamic dependencies) auto-initializer. This auto-initializer calls the bootstrapper API automatically at app startup. It's required for framework-dependent unpackaged apps, in order to ensure that the Windows App Runtime is added to the app's package graph. For info about framework-dependent (and self-contained) apps, see [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview). For info about unpackaged (and packaged) apps, see [Windows apps: packaging, deployment, and process](/windows/apps/get-started/intro-pack-dep-proc).
  * For a packaged app, you don't need the bootstrapper/dynamic dependencies auto-initializer because the `appxmanifest.xml` file expresses the framework dependency. And for a self-contained app, you don't need the bootstrapper/dynamic dependencies auto-initializer because those apps don't use the framework.
  * You can opt out of the bootstrapper/dynamic dependencies auto-initializer in your `.csproj` or `.vcxproj` file via `<WindowsAppSdkBootstrapInitialize>false</WindowsAppSdkBootstrapInitialize>`.
* Deployment Manager auto-initializer. This is required for framework-dependent packaged apps that make use of main/singleton functionality (for example, push notifications), because the `appxmanifest.xml` file can't express those dependencies.
  * For a self-contained app, you don't need the Deployment Manager auto-initializer because those apps don't support main/singleton functionality.
  * For more important info, see the section [The Deployment Manager auto-initializer](#the-deployment-manager-auto-initializer) later in this topic.
* Registration-free activation auto-initializer. This is required for a self-contained app to use manifest-based undocked registration-free Windows Runtime (WinRT) activation (*UndockedRegFreeWinRT*), if the app is running downlevel on an operating system version earlier than Windows 10 May 2019 Update (version 1903; codenamed "19H1").
  * For framework-dependent apps, and for self-contained apps that target Windows 10, version 1903, or later, you don't need the registration-free activation auto-initializer. Those apps can opt out via `<WindowsAppSdkUndockedRegFreeWinRTInitialize>false</WindowsAppSdkUndockedRegFreeWinRTInitialize>`.
* Compatibility auto-initializer. This is required for an app to use A/B containment facilities to control servicing release behavior. For more info, see [RuntimeCompatibilityOptions](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions).
  * For apps that don't use A/B containment (which is the default), you don't need the compatibility auto-initializer.

## The Deployment Manager auto-initializer

When an app that uses the Windows App SDK 1.8 or later starts up, the Deployment Manager auto-initializer runs *by default*. But you can opt out of that happening. This section explains the benefits and caveats of allowing the Deployment Manager auto-initializer to run, and it helps you decide whether or not to opt out.

For your app to make use of functionality in the Main/Singleton packages (for example, push notifications):
	1. You must use the Deployment API to ensure that those packages are deployed (because the Main/Singleton packages aren't frameworks, but "main" packages, like apps; so they can't be registered as dependencies in your app's appx manifest. Instead, the Deployment API provides the functionality to deploy those packages).
	2. Because of 1), your app needs to initialize the Deployment Manager by causing [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) to be called. Your app can do that either automatically, or explicitly, as we'll see.
	3. You app needs to be a framework-dependent packaged app so that it takes a dependency on the Main/Singleton packages.

One way of initializing the Deployment Manager is to allow the Deployment Manager auto-initializer to run (see the section [Auto-initializers in the Windows App SDK](#auto-initializers-in-the-windows-app-sdk) earlier in this topic). The Deployment Manager auto-initializer calls [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) for you. The other way of initializing the Deployment Manager is to explicitly call **DeploymentManager.Initialize** yourself.

If your app (using the Windows App SDK 1.8 or later) doesn't need the Main/Singleton packages, then you should opt out of the Deployment Manager auto-initializer by setting the **WindowsAppSdkDeploymentManagerInitialize** property to *false* in your app's project file.

If your app (using the Windows App SDK 1.8 or later) *does* need the Main/Singleton packages, then you can either:
* Allow the Deployment Manager auto-initializer to run (which it does by default),
* or opt out of the Deployment Manager auto-initializer by setting the **WindowsAppSdkDeploymentManagerInitialize** property to *false* in your app's project file. You should then explicitly call [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) yourself.

> [!IMPORTANT]
> For any process running in the AppContainer, if you cause [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) to be called, then your app needs to declare the `packageManagement` [restricted capability](/windows/uwp/packaging/app-capability-declarations#restricted-capabilities) in your [package manifest](/uwp/schemas/appxpackage/appx-package-manifest).

## Project properties

In the table below are the properties that you can set in your app's project file. See the previous section (above) for details about the auto-initializers in the Windows App SDK.

|Property name and description|Values|For more info|
|-|-|-|
|**AppxPackage**. Specifies whether or not a WinUI app is packaged.|*false* (for an unpackaged app), or absent (for a packaged app)|[Unpackage a WinUI app](unpackage-winui-app.md)|
|**EnableMsixTooling**. Enables the single-project MSIX feature for a project.|*true* (to enable), or absent (to disable)|[Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)|
|**UseCrtSDKReferenceStaticWarning**. Suppresses the build warning that fires when a C++ packaged app links the CRT statically (which is required for self-contained deployment using the hybrid CRT). Set to *false* in packaged app projects that opt into the [hybrid CRT](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/Coding-Guidelines/HybridCRT.md).|*false* (to suppress the warning in packaged self-contained apps), or absent (default, warning enabled)|[Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md)|
|**UseWinUI**. Specifies whether you're using the WinUI user interface framework in your app.|*true*, or absent (for *false*)|[WinUI in the Windows App SDK (WinUI)](/windows/apps/winui/winui3/)|
|**WindowsAppSDKSingleFileVerifyConfiguration**. Controls whether the SDK runs a build-time validation target when `PublishSingleFile` is set. When enabled (the default), the target emits **errors** if required properties are missing (`EnableMsixTooling`, `WindowsPackageType=None`, `IncludeAllContentForSelfExtract`) and **warnings** if self-contained settings are absent (`WindowsAppSDKSelfContained`, `SelfContained`). Set to *false* only to suppress all checks — for example, in CI pipelines where you have verified compliance manually.|*true* (the default — validation runs), *false* (suppress all checks)|Defined in `Microsoft.WindowsAppSDK.SingleFile.targets` (included automatically by the NuGet package when `PublishSingleFile` is set)|
|**WindowsAppSdkBootstrapInitialize**. Determines whether or not the Windows App SDK leverages the bootstrapper/dynamic dependencies auto-initializer.|*true* (the default for executables), *false* (the default for non-executables)|[Opting out of (or into) auto-initializers](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time#opting-out-of-or-into-automatic-module-initialization)|
|**WindowsAppSdkDeploymentManagerInitialize**. Determines whether or not the Windows App SDK leverages the Deployment Manager auto-initializer.|*true* (the default), *false*||
|**WindowsAppSDKRuntimePatchLevel1**, **WindowsAppSDKRuntimePatchLevel2**, and **WindowsAppSDKDisabledChanges**. Determines whether or not the Windows App SDK leverages the compatibility auto-initializer, and configures any desired compatibility options for Windows App Runtime behavior of changes added in servicing updates.|Various, or absent (to disable the auto-initializer)|[RuntimeCompatibilityOptions](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions)|
|**WindowsAppSdkSelfContained**. Determines whether or not an app is deployed *self-contained*.|*true*, or absent (for *false*)|[Windows App SDK deployment guide for self-contained apps](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps)|
|**WindowsAppSdkUndockedRegFreeWinRTInitialize**. Determines whether or not the Windows App SDK leverages the Registration-free activation auto-initializer.|*true* (the default for executables), *false* (the default for non-executables)|[Opting out of (or into) automatic UndockedRegFreeWinRT support](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps#opting-out-of-or-into-automatic-undockedregfreewinrt-support)|
|**WindowsPackageType**. Setting `<WindowsPackageType>None</WindowsPackageType>` for an unpackaged app causes the bootstrapper/dynamic dependencies auto-initializer to locate and load a version of the Windows App SDK version that's most appropriate for your app.|*None*, or absent (to disable the auto-initializer)|[Unpackage a WinUI app](unpackage-winui-app.md)<br/><br/>[Behind the scenes, and opting out of auto-initializers](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time#behind-the-scenes-and-opting-out-of-automatic-module-initialization)|

## Example

Here's an excerpt from a typical `.csproj` file for a C# WinUI 3 project, showing some of the project properties from the table above in use.

```xml
...
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    ...
    <UseWinUI>true</UseWinUI>
    <EnableMsixTooling>true</EnableMsixTooling>
  </PropertyGroup>
...
```

## Related topics

* [Deployment overview](/windows/apps/package-and-deploy/)
* [Create your first WinUI (Windows App SDK) project](/windows/apps/winui/winui3/create-your-first-winui3-app)
* [Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)
* [RuntimeCompatibilityOptions](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions)
* [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time)
* [Windows App SDK deployment guide for self-contained apps](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps)
* [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
* [Windows apps: packaging, deployment, and process](/windows/apps/get-started/intro-pack-dep-proc)
* [WinUI in the Windows App SDK (WinUI)](/windows/apps/winui/winui3/)
