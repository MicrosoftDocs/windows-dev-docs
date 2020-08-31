---
title: Troubleshooting ARM32 UWP apps
description: Common issues with ARM32 apps when running on ARM, and how to fix them.
ms.date: 01/03/2019
ms.topic: article
keywords: windows 10 s, always connected, ARM32 apps on ARM, windows 10 on ARM, troubleshooting
ms.localizationpriority: medium
---

# Troubleshooting ARM UWP apps

If your ARM32 or ARM64 UWP app isn't working correctly on ARM, here's some guidance that may help.

>[!NOTE]
> To build your UWP application to natively target the ARM64 platform, you must have Visual Studio 2017 version 15.9 or later, or Visual Studio 2019. For more information, see [this blog post](https://blogs.windows.com/buildingapps/2018/11/15/official-support-for-windows-10-on-arm-development).


## Common issues
Here are some common issues to keep in mind when troubleshooting ARM32 and ARM64 apps.

### Using Windows 10 Mobile-only APIs on ARM-based processors
ARM apps may run into problems when using mobile-only APIs (for example, **HardwareButtons**). To mitigate this, you can dynamically detect whether your app is running on Windows 10 Mobile before calling these APIs. Follow the guidance in the blog post, [Dynamically detecting features with API contracts](https://blogs.windows.com/buildingapps/2015/09/15/dynamically-detecting-features-with-api-contracts-10-by-10/).

### Including dependencies not supported by UWP apps
Universal Windows Platform (UWP) apps that aren't properly built with Visual Studio and the UWP SDK may have dependencies on OS components that aren't available to ARM apps running on an ARM64 system. Examples of these dependencies include:

- Expecting parts of the .NET Framework to be available.
- Referencing third-party .NET components that aren't compatible with UWP.

These issues can be resolved by: removing the unavailable dependencies and rebuilding the app by using the latest Microsoft Visual Studio and UWP SDK versions; or as a last resort, removing the ARM app from the Microsoft Store, so that the x86 version of the app (if available) is downloaded to users’ PCs.

For more info on .NET APIs available for UWP apps, see [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0)

### Compiling an app with an older version of Visual Studio and SDK
If you're running into issues, be sure to use the latest versions of Microsoft Visual Studio and the Windows SDK to compile your app. Apps compiled with an earlier version of Visual Studio and the SDK may have issues that have been fixed in later versions.

## Debugging
You can use existing tools for developing apps for the ARM platform. Here are some helpful resources.

- Visual Studio 15.5 Preview 1 and later supports running ARM32 apps by using Universal Authentication mode. This automatically bootstraps the necessary remote debugging tools.
- See [Debugging on ARM64](/windows-hardware/drivers/debugger/debugging-arm64) to learn more about tools and strategies for debugging on ARM.