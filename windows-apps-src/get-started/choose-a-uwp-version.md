---
author: QuinnRadich
title: Choose a UWP version
description: When writing a UWP app in Microsoft Visual Studio, you can choose which version to target. Learn about the difference between different UWP versions, and how to configure your choices in new and existing projects.
redirect_url: ../updates-and-versions/choose-a-uwp-version/
---

# Choose a UWP version

**This page has been relocated to ../updates-and-versions/choose-a-uwp-version/**

When writing a UWP app in Microsoft Visual Studio, you can choose which version to target. Learn about the difference between different UWP versions, and how to configure your choices in new and existing projects.

## What's different in each UWP version?

New and changed APIs for UWP are available in every successive version of Windows 10. For specific information about what features were added in which version, see [What's new for developers in Windows 10](../whats-new/windows-10-version-1607.md).

For reference topics that enumerate all device families and their versions, and all API contrats and their versions, see [Device families](https://msdn.microsoft.com/library/windows/apps/dn706137.aspx) and [API contracts](https://msdn.microsoft.com/library/windows/apps/dn706135.aspx).

## Choose which version to use for your app

In the **New Universal Windows Project** dialog in Visual Studio, you can choose a version for **Target Version** and for **Minimum Version**.

* **Target Version**. This sets the *TargetPlatformVersion* setting in your project file. It also determines the value of the *TargetDeviceFamily@MaxVersionTested* attribute in your app package manifest. The value you choose specifies the version of the UWP platform that your project is targeting—and therefore the set of APIs available to your app—so we recommend that you choose the most recent version possible. For more info about your app package manifest, and some guidelines around configuring TargetDeviceFamily manually, see [TargetDeviceFamily](https://msdn.microsoft.com/library/windows/apps/dn986903).
* **Minimum Version**. This sets the *TargetPlatformMinVersion* setting in your project file. It also determines the value of the *TargetDeviceFamily@MinVersion* attribute in your app package manifest. The value you choose specifies the minimum version of the UWP platform that your project can work with.

Be aware that you're declaring that your app works on any version of Windows in the range from **Minimum Version** to **Target Version**. If those two are the same version then you don't need to do anything special. If they're different then here are some things to be aware of.

* In your code, you can freely (that is, without conditional checks) call any API that exists in the version specified by **Minimum Version**.
* The value of **Target Version** is used to identify all the references (contract winmds) used to compile your project. But those references will enable you to compile your code with calls to APIs that won't necessarily exist on devices that you've declared that you support (via **Minimum Version**). Therefore, any API that was introduced after **Minimum Version** will need to be called via adaptive code. For more information about adaptive code, see [Guide to Universal Windows Platform (UWP) apps](universal-application-platform-guide.md).