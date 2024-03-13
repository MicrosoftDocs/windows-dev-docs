---
title: Troubleshooting Arm32 UWP apps
description: Common issues with Arm32 apps when running on Arm, and how to fix them.
ms.date: 06/25/2021
ms.topic: article
ms.service: windows
ms.subservice: arm
author: mattwojo
ms.author: mattwoj
ms.reviewer: marcs
---

# Troubleshooting Arm UWP apps

If your Arm32 or Arm64 UWP app isn't working correctly on Arm, here's some guidance that may help.

>[!NOTE]
> To build your UWP application to natively target the Arm64 platform, you must have Visual Studio 2017 version 15.9 or later, or Visual Studio 2019. For more information, see [this blog post](https://blogs.windows.com/buildingapps/2018/11/15/official-support-for-windows-10-on-arm-development).

## Common issues

Here are some common issues to keep in mind when troubleshooting Arm32 and Arm64 apps.

### Using Windows 10 Mobile-only APIs on Arm-based processors

Arm apps may run into problems when using mobile-only APIs (for example, **HardwareButtons**). To mitigate this, you can dynamically detect whether your app is running on Windows 10 Mobile before calling these APIs. For more info, see [Dynamically detecting features with API contracts](/windows/uwp/debug-test-perf/version-adaptive-apps#api-contracts).

### Including dependencies not supported by UWP apps

Universal Windows Platform (UWP) apps that aren't properly built with Visual Studio and the UWP SDK may have dependencies on OS components that aren't available to Arm apps running on an Arm64 system. Examples of these dependencies include:

- Expecting parts of the .NET Framework to be available.
- Referencing third-party .NET components that aren't compatible with UWP.

These issues can be resolved by: removing the unavailable dependencies and rebuilding the app by using the latest Microsoft Visual Studio and UWP SDK versions; or as a last resort, removing the Arm app from the Microsoft Store, so that the x86 version of the app (if available) is downloaded to users' PCs.

For more info on .NET APIs available for UWP apps, see [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0&preserve-view=true)

### Compiling an app with an older version of Visual Studio and SDK

If you're running into issues, be sure to use the latest versions of Microsoft Visual Studio and the Windows SDK to compile your app. Apps compiled with an earlier version of Visual Studio and the SDK may have issues that have been fixed in later versions.

## Debugging

You can use existing tools for developing apps for the Arm platform. Here are some helpful resources.

- Visual Studio 15.5 Preview 1 and later supports running Arm32 apps by using Universal Authentication mode. This automatically bootstraps the necessary remote debugging tools.
- See [Debugging on Arm64](/windows-hardware/drivers/debugger/debugging-arm64) to learn more about tools and strategies for debugging on Arm.
