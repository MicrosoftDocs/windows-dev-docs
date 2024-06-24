---
title: Project properties
description: This topic describes the project properties that you can set in your Visual Studio project file to customize how your app is deployed.
ms.topic: article
ms.date: 11/17/2022
ms.localizationpriority: medium
---

# Project properties

This topic describes the project properties that you can set in your Visual Studio project file to customize how your app is deployed.

|Property name and description|Values|For more info|
|-|-|-|
|**AppxPackage**. Specifies whether or not a WinUI 3 app is packaged.|*false* (for an unpackaged app), or absent (for a packaged app)|[Create a new project for an unpackaged WinUI 3 desktop app](/windows/apps/winui/winui3/create-your-first-winui3-app#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)|
|**EnableMsixTooling**. Enables the single-project MSIX feature for a project.|*true* (to enable), or absent (to disable)|[Package your app using single-project MSIX](/windows/apps/windows-app-sdk/single-project-msix)|
|**UseWinUI**. Specifies whether you're using the WinUI 3 user interface framework in your app.|*true*, or absent (for *false*)|[WinUI in the Windows App SDK (WinUI 3)](/windows/apps/winui/winui3/)|
|**WindowsAppSdkBootstrapInitialize**. Determines whether or not the Windows App SDK leverages module initializers to call the bootstrapper API automatically at app startup.|*true* (the default for executables), *false* (the default for non-executables)|[Opting out of (or into) automatic module initialization](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time#opting-out-of-or-into-automatic-module-initialization)|
|**WindowsAppSdkSelfContained**. Determines whether or not an app is deployed *self-contained*.|*true*, or absent (for *false*)|[Windows App SDK deployment guide for self-contained apps](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps)|
|**WindowsAppSdkUndockedRegFreeWinRTInitialize**. Determines whether or not the Windows App SDK's implementation of undocked registration-free Windows Runtime (*UndockedRegFreeWinRT*) is enabled automatically at app startup.|*true* (the default for executables), *false* (the default for non-executables)|[Opting out of (or into) automatic UndockedRegFreeWinRT support](/windows/apps/package-and-deploy/self-contained-deploy/deploy-self-contained-apps#opting-out-of-or-into-automatic-undockedregfreewinrt-support)|
|**WindowsPackageType**. Setting `<WindowsPackageType>None</WindowsPackageType>` for an unpackaged app causes the *auto-initializer* to locate and load a version of the Windows App SDK version that's most appropriate for your app.|*None*, or absent (to disable the auto-initializer)|[Create a new project for an unpackaged WinUI 3 desktop app](/windows/apps/winui/winui3/create-your-first-winui3-app#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)<br/><br/>[Behind the scenes, and opting out of automatic module initialization](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time#behind-the-scenes-and-opting-out-of-automatic-module-initialization)|

## Example

Here's an excerpt from a typical `.csproj` file for a C# WinUI 3 project showing some of the project properties from the table above in use.

```xml
...
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    ...
    <UseWinUI>true</UseWinUI>
    <EnableMsixTooling>true</EnableMsixTooling>
  </PropertyGroup>
...
```

## Related topics

* [Deployment overview](./index.md)
