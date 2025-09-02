---
title: Project properties and auto-initializers
description: Describes the project properties that you can set in your Visual Studio project file to customize how your app is deployed, including configuring auto-initializers.
ms.topic: article
ms.date: 08/29/2025
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
* Registration-free activation auto-initializer. This is required for a self-contained app to use manifest-based undocked registration-free Windows Runtime (WinRT) activation (*UndockedRegFreeWinRT*), if the app is running downlevel on an operating system version earlier than Windows 10 May 2019 Update (version 1903; codenamed "19H1").
  * For framework-dependent apps, and for self-contained apps that target Windows 10, version 1903, or later, you don't need the registration-free activation auto-initializer. Those apps can opt out via `<WindowsAppSdkUndockedRegFreeWinRTInitialize>false</WindowsAppSdkUndockedRegFreeWinRTInitialize>`.
* Compatibility auto-initializer. This is required for an app to use A/B containment facilities to control servicing release behavior. For more info, see [RuntimeCompatibilityOptions](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions).
  * For apps that don't use A/B containment (which is the default), you don't need the compatibility auto-initializer.

## Project properties

These are the properties you can set You can use your app's project file to specify the patch level and disabled changes. Here's an example of how to specify the patch level and disabled changes in your project file :

See the previous section (above) for details about the auto-initializers in the Windows App SDK.

|Property name and description|Values|For more info|
|-|-|-|
|**AppxPackage**. Specifies whether or not a WinUI 3 app is packaged.|*false* (for an unpackaged app), or absent (for a packaged app)|[Create a new project for an unpackaged WinUI 3 desktop app](/windows/apps/winui/winui3/create-your-first-winui3-app#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)|
|**EnableMsixTooling**. Enables the single-project MSIX feature for a project.|*true* (to enable), or absent (to disable)|[Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)|
|**UseWinUI**. Specifies whether you're using the WinUI 3 user interface framework in your app.|*true*, or absent (for *false*)|[WinUI in the Windows App SDK (WinUI 3)](/windows/apps/winui/winui3/)|
|**WindowsAppSdkBootstrapInitialize**. Determines whether or not the Windows App SDK leverages the bootstrapper/dynamic dependencies auto-initializer.|*true* (the default for executables), *false* (the default for non-executables)|[Opting out of (or into) auto-initializers](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time#opting-out-of-or-into-automatic-module-initialization)|
|**WindowsAppSdkSelfContained**. Determines whether or not an app is deployed *self-contained*.|*true*, or absent (for *false*)|[Windows App SDK deployment guide for self-contained apps](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps)|
|**WindowsAppSdkUndockedRegFreeWinRTInitialize**. Determines whether or not the Windows App SDK leverages the Registration-free activation auto-initializer.|*true* (the default for executables), *false* (the default for non-executables)|[Opting out of (or into) automatic UndockedRegFreeWinRT support](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps#opting-out-of-or-into-automatic-undockedregfreewinrt-support)|
|**WindowsPackageType**. Setting `<WindowsPackageType>None</WindowsPackageType>` for an unpackaged app causes the bootstrapper/dynamic dependencies auto-initializer to locate and load a version of the Windows App SDK version that's most appropriate for your app.|*None*, or absent (to disable the auto-initializer)|[Create a new project for an unpackaged WinUI 3 desktop app](/windows/apps/winui/winui3/create-your-first-winui3-app#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)<br/><br/>[Behind the scenes, and opting out of auto-initializers](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time#behind-the-scenes-and-opting-out-of-automatic-module-initialization)|
|**WindowsAppSDKRuntimePatchLevel1**, **WindowsAppSDKRuntimePatchLevel2**, and **WindowsAppSDKDisabledChanges**. Determines whether or not the Windows App SDK leverages the compatibility auto-initializer, and configures any desired compatibility options for Windows App Runtime behavior of changes added in servicing updates.|Various, or absent (to disable the auto-initializer)|[RuntimeCompatibilityOptions](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions)|

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
* [Create your first WinUI 3 (Windows App SDK) project](/windows/apps/winui/winui3/create-your-first-winui3-app)
* [Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)
* [RuntimeCompatibilityOptions](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions)
* [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time)
* [Windows App SDK deployment guide for self-contained apps](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps)
* [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
* [Windows apps: packaging, deployment, and process](/windows/apps/get-started/intro-pack-dep-proc)
* [WinUI in the Windows App SDK (WinUI 3)](/windows/apps/winui/winui3/)
