---
Description: With the package resource indexing (PRI) APIs, you can develop a custom build system for your UWP app's resources. The build system will be able to create, version, and dump PRI files to whatever level of complexity your UWP app needs.
title: Package resource indexing and custom build systems
template: detail.hbs
ms.date: 05/07/2018
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# Package resource indexing (PRI) APIs and custom build systems
With the [package resource indexing (PRI) APIs](/windows/desktop/menurc/pri-indexing-reference), you can develop a custom build system for your UWP app's resources. The build system will be able to create, version, and dump (as XML) package resource index (PRI) files to whatever level of complexity your UWP app needs. If you have a custom build system that currently uses the MakePri.exe command-line tool (see [Compile resources manually with MakePri.exe](makepri-exe-command-options.md)) then, for increased performance and control, we recommend that you switch over to calling the PRI APIs instead of calling MakePri.exe.

The PRI APIs were introduced in the Windows SDK for Windows 10, version 1803. The APIs take the form of Win32 Windows APIs, which means that you have a few options for calling them. You can call them directly from a Win32 app, or you can call them via [platform invoke](/dotnet/framework/interop/consuming-unmanaged-dll-functions?branch=live) from a .NET app or even from a UWP app.

The scenarios in this topic demonstrate calling PRI APIs from a Win32 Visual C++ Windows Console Application project. For background info, see [Resource Management System](resource-management-system.md).

> [!NOTE]
> This caveat is unlikely to be an issue, since you probably won't want to submit your custom build system app to the Microsoft Store. But, if you choose the option to develop your custom build system in the form of a UWP app, then it will be an unusual UWP app in that you won't be able to submit it to the Microsoft Store. That's because a UWP app that uses platform invoke fails Microsoft Store certification. Note that, in this case, platform invoke calls *will exist only in your custom build system*; *not* in your shipping UWP app (the one you're building PRI files for).

## Scenario walkthroughs
|Topic|Description|
|-|-|
|[Scenario 1: Generate a PRI file from string resources and asset files](pri-apis-scenario-1.md)|In this scenario, we'll make a new app to represent our custom build system. We'll create a resource indexer and add strings and other kinds of resources to it. Then we'll generate and dump a PRI file.|

## Important APIs
* [Package resource indexing (PRI) reference](/windows/desktop/menurc/pri-indexing-reference)

## Related topics
* [Compile resources manually with MakePri.exe](makepri-exe-command-options.md)
* [Consuming Unmanaged DLL Functions](/dotnet/framework/interop/consuming-unmanaged-dll-functions?branch=live)
* [Resource Management System](resource-management-system.md)