---
description: Learn about the package graph and servicing model for MSIX framework packages.
title: MSIX framework packages and dynamic dependencies
ms.topic: article
ms.date: 07/30/2021
ms.localizationpriority: medium
---

# MSIX framework packages and dynamic dependencies

This article introduces important concepts related to *MSIX framework packages*. The information in this article provides useful context to help you better understand the design and purpose of the *dynamic dependencies* feature in the Windows App SDK and in the Windows 11 OS. This feature enables your apps to reference and use MSIX framework packages at run time.

## Framework packages and the package graph

[MSIX](/windows/msix) is a package format that provides a modern packaging and deployment experience. It also provides a clean and trusted way to package redistributable libraries, content and components via *MSIX framework packages*. An MSIX framework package allows MSIX-packaged apps to access components through a single shared source on the user's device, instead of bundling them into the app package. Common framework packages include the [Windows App SDK](../../../windows-app-sdk/index.md) (including WinUI3), [WinUI2](../../../winui/winui2/index.md), [VCLibs](/troubleshoot/cpp/c-runtime-packages-desktop-bridge), and the DirectX Runtime.

Starting in Windows 8 and continuing through Windows 10 and Windows 11, every process has a *package graph* that provides the list of all the packages available to the app, including framework, resource, optional, and main packages. This graph allows the app to load DLLs, content, and run-time class declarations provided by a referenced package. Historically, this graph was fixed at process creation time, and there was no way to alter it at run time:

- For MSIX-packaged apps, the graph was initialized based on the package dependencies declared in the [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element in the app's package manifest. When building a packaged app, this was typically done for you during the build process based on your project references and dependencies.
- For unpackaged apps (that is, apps that do not use MSIX for their deployment technology), the package graph was empty and could not be changed. Therefore, unpackaged apps were limited to [standard DLL search order](/windows/win32/dlls/dynamic-link-library-search-order) and could not access framework packages.

This static package graph restriction is lifted with the introduction of the dynamic dependencies support in both the [Windows App SDK](../../../windows-app-sdk/index.md) and in Windows 11. Developers can use dynamic dependencies to reference and use MSIX framework packages from their apps at run time. Dynamic dependencies removes the static package graph restriction from apps, and developers can decide how they want to leverage framework packages.

## Primary scenarios for dynamic dependencies

Although dynamic dependencies enables any app to add a package framework dependency at run time, this feature is primarily intended to be used by unpackaged apps. Packaged apps can still continue to add static dependencies via the [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element in their package manifest.

- Most developers will only use dynamic dependencies to reference the [Windows App SDK](../../../windows-app-sdk/index.md) framework package in an unpackaged app so that the app can call APIs provided by the Windows App SDK runtime. For more information about this scenario, see [Reference the Windows App SDK framework package at run time](../../../windows-app-sdk/reference-framework-package-run-time.md).
- In some cases, developers may want to use dynamic dependencies to reference a different framework package (other than the Windows App SDK framework package) from an unpackaged app, such as the framework package for [WinUI2](../../../winui/winui2/index.md) or the DirectX Runtime. For more information about this scenario, see [Reference framework packages at run time](use-the-dynamic-dependency-api.md).

## Servicing model for framework packages

The dynamic dependencies feature preserves the integrity of the servicing pipeline for the framework package that is being referenced and used dynamically at run time.

MSIX framework packages support servicing in a side-by-side model, meaning each version is installed in its own separate versioned folder. This allows applications in use to be able to stay up and running even when a newer app installs a newer version of the framework package. The OS has uninstall logic for when to delete older versions of a given framework package, based on the presence of *install-time references* and *run-time references* for the package.

- When an app is installed, it can create an *install-time reference* to a framework package. This reference informs the OS that the app has a dependency upon the specified framework package so that the OS won't uninstall the framework package while your app is installed.
- When an app needs to use APIs or content in a framework package, it can add a *run-time reference* to the framework package. This reference informs the OS that the framework package is in active use and to handle any version updates in a side-by-side manner. If a new version of the framework package is installed, but a running app has an older version in use, the OS cannot remove the older version until all run-time references to the older version are removed.

For example, given this scenario:

- **App A** is running and using version 1.0.0.0 of a given framework package.
- **App B** is installed and has a dependency upon version 1.0.0.1 of the same framework package.

In this scenario, both versions of the framework package will be installed and in use by **App A** and **App B**. However, when **App A** is closed by the user and then restarted, it will pick up the newer version 1.0.0.1 of the framework package. At this point, the run-time reference requirement is no longer valid for version 1.0.0.0 of the framework package, and the OS can safely remove the 1.0.0.0 version. Later, when **App A** and **App B** are uninstalled by the user, then the install-time reference requirement is no longer valid and it is safe for the OS to remove the framework package entirely.  

For MSIX-packaged apps that use the [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element to specify static references to framework packages, the install-time references for framework packages are tracked by the OS when the app is installed or uninstalled. For run-time references that are managed by using the dynamic dependencies feature, the OS knows when a packaged app is running and will avoid removing its in-use framework packages when a newer one is available.

## Related topics

- [Reference the Windows App SDK framework package at run time](../../../windows-app-sdk/reference-framework-package-run-time.md)
- [Use the dynamic dependency API to reference framework packages at run time](use-the-dynamic-dependency-api.md)
- [Deploy unpackaged apps that use the Windows App SDK](../../../windows-app-sdk/deploy-unpackaged-apps.md)
- [Run time architecture and deployment scenarios for the Windows App SDK](../../../windows-app-sdk/deployment-architecture.md)
- [Tutorial: Build and deploy an unpackaged app that uses the Windows App SDK](/windows/apps/windows-app-sdk/tutorial-unpackaged-deployment)