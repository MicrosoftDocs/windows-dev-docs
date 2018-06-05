---
title: Troubleshooting ARM32 UWP apps
author: msatranjr
description: Common issues with ARM32 apps when running on ARM, and how to fix them. 
ms.author: misatran
ms.date: 05/09/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10 s, always connected, ARM32 apps on ARM, windows 10 on ARM, troubleshooting
ms.localizationpriority: medium
---

# Troubleshooting ARM32 UWP apps
>[!IMPORTANT]
> The ARM64 SDK is now available as part of Visual Studio 15.8 Preview 1. We recommend that you recompile your app to ARM64 so that your app runs at full native speed. For more info, see the [Early preview of Visual Studio support for Windows 10 on ARM development](https://blogs.windows.com/buildingapps/2018/05/08/visual-studio-support-for-windows-10-on-arm-development/) blog post.

If your ARM32 UWP app isn't working correctly on ARM, here's some guidance that may help. 

## Common issues
Here are some common issues to keep in mind when troubleshooting ARM32 apps.

### Using Windows 10 Mobile-only APIs on ARM-based processors 
ARM32 apps may run into problems when using mobile-only APIs (for example, **HardwareButtons**). To mitigate this, you can dynamically detect whether your app is running on Windows 10 Mobile before calling these APIs. Follow the guidance in the blog post, [Dynamically detecting features with API contracts](https://blogs.windows.com/buildingapps/2015/09/15/dynamically-detecting-features-with-api-contracts-10-by-10/).

### Including dependencies not supported by UWP apps
Universal Windows Platform (UWP) apps that aren't properly built with Visual Studio and the UWP SDK may have dependencies on OS components that aren't available to ARM32 apps running on an ARM64 system. Examples of these dependencies include:

- Expecting parts of the .NET Framework to be available.
- Referencing third-party .NET components that aren't compatible with UWP.

These issues can be resolved by: removing the unavailable dependencies and rebuilding the app by using the latest Microsoft Visual Studio and UWP SDK versions; or as a last resort, removing the ARM32 app from the Microsoft Store, so that the x86 version of the app (if available) is downloaded to users’ PCs. 

For more info on .NET APIs available for UWP apps, see [.NET for UWP apps](https://msdn.microsoft.com/library/windows/apps/mt185501.aspx)

### Compiling an app with an older version of Visual Studio and SDK
If you're running into issues, be sure to use the latest versions of Microsoft Visual Studio and the Windows SDK to compile your app. Apps compiled with an earlier version of Visual Studio and the SDK may have issues that have been fixed in later versions.

## Debugging
You can use existing tools for developing ARM32 apps for the ARM platform. Here are some helpful resources.

- Visual Studio 15.5 Preview 1 and later supports running ARM32 apps by using Universal Authentication mode. This automatically bootstraps the necessary remote debugging tools.
- See [Debugging on ARM64](https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/debugging-arm64) to learn more about tools and strategies for debugging on ARM.