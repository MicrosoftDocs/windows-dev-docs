---
title: Deployment architecture for the Windows App SDK
description: This article provides a high level explanation of the Windows App SDK deployment architecture and scenarios.
ms.topic: article
ms.date: 11/16/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Deployment architecture for the Windows App SDK

This article explains the basic building blocks and high-level architecture of Windows App SDK deployment. For instructions to deploy apps that use the Windows App SDK, see these articles:

- [Deploy MSIX-packaged apps that use the Windows App SDK](deploy-packaged-apps.md)
- [Deploy non-MSIX packaged apps that use the Windows App SDK](deploy-unpackaged-apps.md) (experimental feature)

## Key terms

| Term | Definition |
|------|------------|
| **Windows App SDK runtime** | All the MSIX packages required by an app to be able to use the Windows App SDK. These packages include: Framework, Main, and DDLM. |
| **Framework package** | Contains binaries used at run time by apps. The framework includes a bootstrapper component that enables apps to automatically install the latest version of the Windows App SDK, which will be updated on a regular release cadence. |
| **Main package** | Contains the background tasks, services, app extensions, and other components not included in the framework package. These are generally out-of-process services that are brokered between apps, such as push notifications and the Clipboard. |
| **Singleton package** | Contains services and other components not included in the Framework package. This is generally a single long-running process that is brokered between apps, such as push notifications. |
| **Dynamic Dependency Lifetime Manager (DDLM)** | A main package that prevents the OS from performing servicing updates to the MSIX packages while an unpackaged app is in use. |
| **Bootstrapper** | An app-local binary used by unpackaged apps to locate and load the best Windows App SDK version match as needed by the app.  |
| **Provisioning** | The process of installing and registering packages (including files and registry keys) system-wide to eliminate the need for repeated installation by the other users. It can be done either as part of the OS or done during installation of an app. |
| **Installer** | An installation technology for installing an app, such an MSI, App Installer, or .exe setup. |
| **MSIX** | Modern installer technology that enables users to safely install an app per user, directly from the Microsoft Store or a web site. On Enterprise or shared PCs, apps can be installed for all users via PowerShell and MDM. |

## Requirements for packaged and unpackaged apps

### Framework package

When you build an app that uses the Windows App SDK, your app references a set of Windows App SDK runtime components that are distributed to end users via a *framework package*. The framework package allows packaged apps to access Windows App SDK components through a single shared source on the user's device, instead of bundling them into the app package. The framework package also carries its own resources, such as DLLs and API definitions (COM and Windows Runtime registrations). These resources run in the context of your app, so they inherit the capabilities and privileges of your app, and don't assert any capabilities or privileges of their own. For more information about framework package dependencies, see [MSIX framework packages and dynamic dependencies](../desktop/modernize/framework-packages/framework-packages-overview.md).

The Windows App SDK framework package is an MSIX package that is deployed to end users through the Microsoft Store. It can be easily and quickly updated with the latest releases, in addition to security and reliability fixes. All apps that use the Windows App SDK on a computer have a dependency on a shared instance of the framework package, as illustrated in the following diagram.

[![Diagram of how apps access the Windows App SDK framework package](images/framework.png) ](images/framework.png#lightbox)

When a new version of the Windows App SDK framework package is released, all apps are updated to the new version without themselves having to redistribute a copy. Windows updates to the newest version of frameworks as they are released, and apps will automatically reference the latest framework package version during relaunch. Older framework package versions will not be removed from the system until they are no longer running or being actively used by apps on the system.

[![Diagram of how apps get updates to the Windows App SDK framework package](images/framework-update.png) ](images/framework-update.png#lightbox)

Because app compatibility is important to Microsoft and to apps that depend on the Windows App SDK, the Windows App SDK framework package follows [Semantic Versioning 2.0.0](https://semver.org/) rules. This means that after we release version 1.0 of the Windows App SDK, the Windows App SDK framework package will guarantee compatibility between minor and patch version changes, and breaking changes will occur only between major version updates.

### Main package

The **main package** contains a short-lived server process that keeps track of dynamic dependencies that have been added, and it enables the Store to update the framework package.

### Singleton package

The **singleton package** ensures that a single long-running process can handle services that are used across multiple apps, which may be running on different versions of the Windows App SDK. 

The Windows App SDK singleton is needed to enable [push notifications](notifications/push/index.md) for unpackaged apps and packaged Win32 applications using Windows versions below 20H1, which cannot be supported by the existing UWP [PushNotificationTrigger](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger) and [ToastNotificationActionTrigger](/uwp/api/windows.applicationmodel.background.toastnotificationactiontrigger) class.

## Additional requirements for unpackaged apps

### Bootstrapper

The bootstrapper is a library that must be included with your unpackaged app. It provides the [bootstrapper API](reference-framework-package-run-time.md), which enables unpackaged apps to perform these important tasks:

- Initialize the Dynamic Dependency Lifetime Manager (DDLM) for the Windows App SDK framework package.
- Find and load the Windows App SDK framework package to the app's package graph.

To accomplish these tasks, the nuget package leverages module initializers to wire up the bootstrapper for you. Simply set `<WindowsPackageType>None</WindowsPackageType>` in your project file. In advanced scenarios, if you want control over the initialization, you may [call the bootstrapper API directly in your app's startup code](tutorial-unpackaged-deployment.md) so it can properly initialize the system for the unpackaged app. Your unpackaged app must use the bootstrapper API before it can use Windows App SDK features such as WinUI, App lifecycle, MRT Core, and DWriteCore.

The bootstrapper library in the Windows App SDK 1.0 Stable release includes:

- **Microsoft.WindowsAppRuntime.Bootstrap.dll** (C++ and C#) 
- **Microsoft.WindowsAppRuntime.Bootstrap.Net.dll** (C# wrapper)

### Dynamic Dependency Lifetime Manager (DDLM)

The purpose of the DDLM is to prevent servicing of the Windows App SDK framework package while it is in use by an unpackaged app. It contains a server that must be initialized by the bootstrapper early in an app's startup to provide that functionality.

There is one DDLM for each version and architecture of the Windows App SDK framework package. This means on an `x64` computer, you may have both an `x86` and an `x64` version of the DDLM to support apps of both architectures.

## Related topics

- [Windows App SDK deployment guide for packaged apps](deploy-packaged-apps.md)
- [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md)

