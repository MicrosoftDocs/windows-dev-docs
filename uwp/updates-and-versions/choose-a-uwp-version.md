---
title: Choose a UWP version
description: When writing a UWP app in Microsoft Visual Studio, you can choose which version to target. Learn about the difference between different UWP versions, and how to configure your choices in new and existing projects.
ms.date: 5/12/2019
ms.topic: article
keywords: windows 10, uwp, version, build, versions, windows, choose, update, updates
ms.assetid: a8b7830f-4929-44c6-90be-91f38be5f364
ms.localizationpriority: medium
f1_keywords:
 - UniversalProjects.TargetPlatformWizardPicker 
dev_langs:
 - csharp
 - cppwinrt
 - cpp
 - vb
---
# Choose a UWP version

Each version of Windows 10 and Windows 11 have brought new and improved features to the UWP platform. When creating a UWP app in Microsoft Visual Studio, you can choose which version to target. Projects using [.NET Standard 2.0](/dotnet/standard/net-standard) must have a **Minimum Version** of Build 16299 or later.

> [!WARNING]
> UWP projects created in current versions of Visual Studio cannot be opened in Visual Studio 2015.

The following table describes the available versions of Windows 10 and Windows 11. Please note that this table only applies for building UWP apps, which are only supported on Windows 10 and Windows 11. You cannot develop UWP apps for older versions of Windows, and you must have [installed the appropriate build of the SDK](https://developer.microsoft.com/windows/downloads#_blank) in order to target that version.

| Version | Description |
| --- | --- |
| Build 19041 (version 2004) | This is the latest version of Windows 10, released in May 2020. Highlighted features of this release include: </br> \* **WSL2:** Windows Subsystem for Linux has been updated with a new architectural model, and now runs an actual Linux kernel on Windows. Learn more at [about WSL2](/windows/wsl/wsl2-about). </br> \*  **MSIX:** New features within Windows provide further support for the modern MSIX app packaging format, including the ability to create packages with included services, creation of hosted apps, and the ability to include features that require package identity in unpackaged apps. Learn more in the [MSIX docs](/windows/msix/overview). </br> For more information on these and the many other features added in this release of Windows, visit [the Dev Center](https://developer.microsoft.com/windows/windows-10-for-developers) or the more in-depth page on [What's new in Windows 10 for developers](../whats-new/windows-10-build-19041.md)
| Build 18362 (version 1903) | This version of Windows 10 was released in April 2019. Some highlighted features from this release include: </br> \* **XAML Islands:** Windows 10 now enables you to use UWP controls in non-UWP desktop applications. If you're developing for WPF, Windows Forms, or C++ Win32, [check out how you can add the latest Windows 10 UI features to your existing app](/windows/apps/desktop/modernize/xaml-islands). </br> \* **Windows Subsystem for Linux:** You can now access Linux files directly from within Windows, and use several new command line options. See the latest at [about WSL](/windows/wsl/about). </br> For information on these and many other features added in this release of Windows, visit [What's new in build 18362](../whats-new/windows-10-build-18362.md)
| Build 17763 (version 1809) | This version of Windows 10 was released in October 2018. **Please note that you _must_ be using Visual Studio 2017 or Visual Studio 2019 in order to target this version of Windows.** Some highlighted features from this release include: </br> \* **Windows Machine Learning:** Windows Machine Learning has now officially launched, providing features like faster evaluation and support for cutting-edge machine learning models. To learn more about the platform, see [Windows Machine Learning](/windows/ai/). </br> \* **Fluent Design:** New features such as menu bar, command bar flyout, and XAML property animations have been added to Windows 10. See the latest at the [Fluent design overview](/windows/apps/fluent-design-system). </br> For information on these and many other features added in this release of Windows, visit [What's new in build 17763](../whats-new/windows-10-build-17763.md)
| Build 17134 (version 1803) | This is version of Windows 10 was released in April 2018. **Please note that you _must_ be using Visual Studio 2017 or Visual Studio 2019 in order to target this version of Windows.** Some highlighted features from this release include: </br> \* **Fluent Design:** New features such as tree view, pull-to-refresh, and navigation view have been added to Windows 10. See the latest at the [Fluent design overview](/windows/apps/fluent-design-system). </br> \* **Console UWP apps:** You can now write C++ /WinRT or /CX UWP console apps that run in a console window such as a DOS or PowerShell console window. </br> For information on these and many other features added in this release of windows, visit [What's new in build 17134](../whats-new/windows-10-build-17134.md)
| Build 16299 (Fall Creators Update, version 1709) | This version of Windows 10 was released in October 2017. **Please note that you _must_ be using Visual Studio 2017 or Visual Studio 2019 in order to target this version of Windows.** Some highlighted features from this release include: </br> \* **.NET Standard 2.0:** Enjoy a massive increase in the number of .NET APIs and incorporate your favorite NuGet packages and third party libraries into .NET Standard. See more details and explore the documentation [here](/dotnet/standard/net-standard). Please note that you must set your **minimum version** to Build 16299 to access these new APIs. </br> \* **Fluent Design:** Use light, depth, perspective, and movement to enhance your app and help users focus on important UI elements. </br> \* **Conditional XAML:** Easily set properties and instantiate objects based on the presence of an API at runtime, enabling your apps to run seamlessly across devices and versions. </br> For information on these and many other features added in this release of windows, visit [What's new in Windows 10 for developers](../whats-new/windows-10-build-16299.md)
| Build 15063 (Creators Update, version 1703) | This version of Windows 10 was released in March 2017. **Please note that you _must_ be using Visual Studio 2017 or Visual Studio 2019 in order to target this version of Windows**. Some highlighted features from this release include:  </br> \* **Ink Analysis:** Windows Ink can now categorize ink strokes into either writing or drawing strokes, and recognized text, shapes, and basid layout structures. </br> \* **Windows.Ui.Composition APIs:** Easily combine and apply animations across your app. </br> \* **Live Editing:** Edit XAML while your app is running, and see the changes applied in real-time. </br> For information on these and many other features added in this release of windows, visit [What's new in build 15063](../whats-new/windows-10-build-15063.md)  |
| Build 14393 (Anniversary Update, version 1607) | This version of Windows 10 was released in July 2016. Some highlighted features from this release include: </br> \* **Windows Ink:** New InkCanvas and InkToolbar controls. </br> \* **Cortana APIs:** Use new Cortana Actions to integrate Cortana support with specific functions of your app. </br> \* **Windows Hello:** Microsoft Edge now supports Windows Hello, giving web developers access to biometric authentication. </br> For information on these and many other features added in this release of windows, visit [What's new in build 14393](../whats-new/windows-10-build-14393.md)  |
| Build 10586 (November Update, version 1511) | This version of Windows 10 was released in November 2015. Highlighted features include the introduction of ORTC (object real-time communications) APIs for video communication in Microsoft Edge and Providers APIs to enable apps to use Windows Hello face authentication. [More information on features introduced in this build.](../whats-new/windows-10-build-10586.md) |
| Build 10240 (Windows 10, version 1507) | This is the initial release version of Windows 10, from July 2015. [More information on features introduced in this build.](../whats-new/windows-10-build-10240.md) |

We highly recommend that new developers and developers writing code for a general audience always use the latest build of Windows (19041). Developers writing Enterprise apps should strongly consider supporting an older **Minimum Version**.

## What's different in each UWP version?

New and changed APIs for UWP are available in every successive version of Windows 10 and Windows 11. For specific information about what features were added in which version, see [What's new for developers in Windows 10/11](../whats-new/windows-10-build-19041.md).

For reference topics that enumerate all device families and their versions, and all API contracts and their versions, see [Device families](/uwp/extension-sdks/) and [API contracts](/uwp/extension-sdks/).

## .NET API availability in UWP versions

UWP supports a limited subset of .NET APIs, which are available regardless of the **Target Version** or **Minimum Version** of your project. [This page provides more information on the types available](/dotnet/api/index?view=dotnet-uwp-10.0&preserve-view=true).

If you wish to create reusable cross-platform libraries, .NET Standard is supported on UWP. The [.NET Standard documentation](/dotnet/standard/net-standard) provides information on which .NET Standard is supported in which UWP versions.

If you are developing a Desktop app, see instead [.NET Framework versions and dependencies](/dotnet/framework/migration-guide/versions-and-dependencies) for detailed information on .NET framework availability.

## Choose which version to use for your app

In the **New Universal Windows Project** dialog in Visual Studio, you can choose a version for **Target Version** and for **Minimum Version**. Additionally, you can change the **Target Version** and **Minimum Version** of your UWP app in the *application* section of the app's **Properties**.

* **Target Version**. The version of Windows 10 or Windows 11 that your app is intended to run on. This sets the *TargetPlatformVersion* setting in your project file. It also determines the value of the *TargetDeviceFamily@MaxVersionTested* attribute in your app package manifest. The value you choose specifies the version of the UWP platform that your project is targeting—and therefore the set of APIs available to your app—so we recommend that you choose the most recent version possible. For more info about your app package manifest, and some guidelines around configuring TargetDeviceFamily manually, see [TargetDeviceFamily](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily).
* **Minimum Version**. The earliest version of Windows 10 or Windows 11 needed to support the basic functions of your app. This sets the *TargetPlatformMinVersion* setting in your project file. It also determines the value of the *TargetDeviceFamily@MinVersion* attribute in your app package manifest. The value you choose specifies the minimum version of the UWP platform that your project can work with.

Be aware that you're declaring that your app works on any version of Windows in the range from **Minimum Version** to **Target Version**. If those two are the same version then you don't need to do anything special. If they're different, then here are some things to be aware of.

* In your code, you can freely (that is, without conditional checks) call any API that exists in the version specified by **Minimum Version**.
* Ensure that you test your code on a device running the **Minimum Version**, to be sure that it works without requiring APIs only present in the **Target Version**.
* The value of **Target Version** is used to identify all the references (contract winmds) used to compile your project. But those references will enable you to compile your code with calls to APIs that won't necessarily exist on devices that you've declared that you support (via **Minimum Version**). Therefore, any API that was introduced after **Minimum Version** will need to be called via adaptive code. For more information about adaptive code, see [Version adaptive code](../debug-test-perf/version-adaptive-code.md).
