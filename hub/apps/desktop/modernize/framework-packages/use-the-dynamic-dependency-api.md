---
title: Use the dynamic dependency API to reference MSIX packages at run time
description: Describes how to use the *dynamic dependency API* to dynamically take a dependency on different MSIX packages (other than the Windows App SDK framework package) in an unpackaged app at run time.
ms.topic: article
ms.date: 07/27/2023
ms.localizationpriority: medium
---

# Use the dynamic dependency API to reference MSIX packages at run time

## The two implementations

There are two implementations of the *dynamic dependency API* that you can choose from, depending on your target platform and scenario:

* **The Windows App SDK's dynamic dependency API**. The Windows App SDK provides C and C++ functions (in [msixdynamicdependency.h](/windows/windows-app-sdk/api/win32/msixdynamicdependency)), and Windows Runtime (WinRT) types (in the [**Microsoft.Windows.ApplicationModel.DynamicDependency**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.dynamicdependency) namespace) that implement the dynamic dependency API. You can use this implementation of the API on any version of Windows that supports the [Windows App SDK](/windows/apps/windows-app-sdk/).
* **Windows 11's dynamic dependency API**. Windows 11 also provides C and C++ functions that implement the dynamic dependency API (in [appmodel.h](/windows/win32/api/appmodel)). This implementation of the API can be used only by apps that target Windows 11, version 22H2 (10.0; Build 22621), and later.

Also see [Differences between the two implementations](#differences-between-the-two-implementations).

> [!NOTE]
> As you'll see in this topic, the Windows App SDK (C/C++) APIs have the same names as the Windows 11 (C/C++) APIs with an additional **Mdd** prefix. **Mdd** stands for *Microsoft Dynamic Dependencies*.

And there are different kinds of MSIX packages, including *framework*, *resource*, *optional*, and *main* packages. The dynamic dependency API enables unpackaged apps to reference and to use *framework* packages such as [WinUI 2](../../../winui/winui2/index.md) and the DirectX Runtime. For more info about framework package dependencies, see [MSIX framework packages and dynamic dependencies](framework-packages-overview.md).

Specifically, the dynamic dependency API provides ways to manage the *install-time references* and *run-time references* for MSIX packages. For more info, see [Servicing model for framework packages](framework-packages-overview.md#servicing-model-for-framework-packages).

## Use the dynamic dependency API

To use the dynamic dependency API in your unpackaged app to take a dependency on an MSIX package, follow this general pattern in your code:

### 1. Create an install-time reference

In your app's installer, or during the first run of your app, call one of the following functions or methods to specify a set of criteria for the MSIX package that you want to use. This informs the operating system (OS) that your app has a dependency on an MSIX package that meets the specified criteria. If one or more MSIX packages are installed that meet the criteria, then Windows ensures that at least one of them remains installed until the install-time reference is deleted.

* Windows 11 (C/C++): [TryCreatePackageDependency](/windows/win32/api/appmodel/nf-appmodel-trycreatepackagedependency)
* Windows App SDK (C/C++): [MddTryCreatePackageDependency](/windows/windows-app-sdk/api/win32/msixdynamicdependency/nf-msixdynamicdependency-mddtrycreatepackagedependency)
* Windows App SDK (WinRT): [PackageDependency.Create](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.dynamicdependency.packagedependency.create)

The criteria you specify include the package family name, minimum version, and architectures; but you can't indicate a specific MSIX package. When you add a run-time reference to the MSIX package, the API chooses the highest version that satisfies the specified criteria.

You must also specify a *lifetime artifact*, which can be the current process, a file, or a Windows Registry key that indicates to the system that the app is still available. If the specified artifact no longer exists, then the OS can assume that the dependency is no longer needed, and it can uninstall the MSIX package if no other apps have declared a dependency on it. That feature is useful for scenarios where an app neglects to remove the install-time pin when it is uninstalled.

This API returns a dependency ID that must be used in other calls to create run-time references, and to delete the install-time reference.

### 2. Add a run-time reference

When your app needs to use the MSIX package, call one of the following functions or methods to request access to the specified MSIX package, and add a run-time reference for it. Calling this API informs the OS that the MSIX package is in active use, and to handle any version updates in a side-by-side manner (effectively delay-uninstalling or otherwise servicing the older version until after the app is done using it). If successful, the app might activate classes and use content from the MSIX package.

* Windows 11 (C/C++): [AddPackageDependency](/windows/win32/api/appmodel/nf-appmodel-addpackagedependency)
* Windows App SDK (C/C++): [MddAddPackageDependency](/windows/windows-app-sdk/api/win32/msixdynamicdependency/nf-msixdynamicdependency-mddaddpackagedependency)
* Windows App SDK (WinRT): [PackageDependency.Add](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.dynamicdependency.packagedependency.add)

When you call this API, you must pass in the dependency ID that was returned when you created the install-time reference, and the desired rank to use for the MSIX package in the process's package graph. This API returns the full name of the MSIX package that was referenced, and a handle that is used to keep track of the active-use dependency. If there are multiple MSIX packages installed that meet the criteria that you specified when you created the install-time reference, then the API chooses the highest version that satisfies the criteria.

### 3. Remove the run-time reference

When your app is done using the MSIX package, call one of the following functions or methods to remove the run-time reference. Typically, your app will call this API during shutdown. This API informs the OS that it is safe to remove any unnecessary versions of the MSIX package.

* Windows 11 (C/C++): [RemovePackageDependency](/windows/win32/api/appmodel/nf-appmodel-removepackagedependency)
* Windows App SDK (C/C++): [MddRemovePackageDependency](/windows/windows-app-sdk/api/win32/msixdynamicdependency/nf-msixdynamicdependency-mddremovepackagedependency)
* Windows App SDK (WinRT): [PackageDependencyContext.Remove](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.dynamicdependency.packagedependencycontext.remove)

When you call this API, you must pass in the handle that was returned when you added the run-time reference.

### 4. Delete the install-time reference

When your app is uninstalled, call one of the following functions or methods to delete the install-time reference. This API informs the OS that it is safe to remove the MSIX package if no other apps have a dependency on it.

* Windows 11 (C/C++): [DeletePackageDependency](/windows/win32/api/appmodel/nf-appmodel-deletepackagedependency)
* Windows App SDK (C/C++): [MddDeletePackageDependency](/windows/windows-app-sdk/api/win32/msixdynamicdependency/nf-msixdynamicdependency-mdddeletepackagedependency)
* Windows App SDK (WinRT): [PackageDependency.Delete](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.dynamicdependency.packagedependency.delete)

When you call this API, you must pass in the dependency ID that was returned when you created the install-time reference.

## Differences between the two implementations

### The need for a lifetime manager (Windows App SDK limitation)

When you use the Windows App SDK's dynamic dependency API to take a dependency on an MSIX package, the API requires help via another installed package and running process to inform Windows that the MSIX package is in use, and to block servicing the framework while it is being used. That component is called a *lifetime manager*.

For its framework package, the Windows App SDK provides a lifetime manager component called the [Dynamic Dependency Lifetime Manager (DDLM)](../../../windows-app-sdk/deployment-architecture.md). However, no other framework packages currently provide a similar lifetime manager component from Microsoft.

Windows 11's dynamic dependency API doesn't have this limitation.

### Reference and use a main package (Windows App SDK limitation)

A dynamic dependency can always target a *framework* package. But only Windows 11's dynamic dependency API can reference and use *main* packages as well.

The main package must have correctly configured its app package manifest source file (the `Package.appxmanifest` file in Visual Studio). Specifically, the main package (the target, not the caller) needs to set `<uap15:DependencyTarget>true</>` (see [uap15:DependencyTarget](/uwp/schemas/appxpackage/uapmanifestschema/element-uap15-dependencytarget)). So the purpose of `<uap15::DependencyTarget>` is to enable a dynamic dependency to target a *main* package. In other words, the main package has to opt in to allow itself to be used as a dynamic dependency (whereas framework packages always implicitly allow that).

### Reference the Windows App SDK framework package (Windows App SDK limitation)

In an unpackaged app, you can't use the Windows App SDK's dynamic dependency API to reference the [Windows App SDK framework package](../../../windows-app-sdk/deployment-architecture.md#framework-package) (like you *can* reference other MSIX packages with it). Instead, you need to use the [bootstrapper API](/windows/windows-app-sdk/api/win32/_bootstrap/) provided by the Windows App SDK. The bootstrapper API is a specialized form of the dynamic dependency API that's designed to take dependencies on the Windows App SDK framework package. For more info, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../../../windows-app-sdk/use-windows-app-sdk-run-time.md).

Windows 11's dynamic dependency API doesn't have this limitation.

## Related topics

* [MSIX framework packages and dynamic dependencies](framework-packages-overview.md)
* [Dynamic dependencies specification](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/dynamicdependencies/DynamicDependencies.md)
* [Runtime architecture for the Windows App SDK](../../../windows-app-sdk/deployment-architecture.md)
* [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../../../windows-app-sdk/use-windows-app-sdk-run-time.md)
