---
title: Windows App SDK deployment architecture and overview
description: This article provides a high level explanation of the Windows App SDK deployment architecture and scenarios.
ms.topic: article
ms.date: 11/16/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Deployment architecture and overview for framework-dependent apps

This article explains a high-level architecture of Windows App SDK deployment. The concepts below apply primarily to Windows App SDK framework-dependent apps. A framework-dependent app depends on the Windows App SDK runtime being present on the target machine. 

There are two main options to distribute a framework-dependent app:

| App&nbsp;deployment method  | Requirements |
|------------------------|---------|
| MSIX-packaged | - Must declare dependency on Framework package in the package manifest. <br> - Deployment API is required for Microsoft Store distributed apps and recommended for non-Store distributed apps to ensure runtime dependencies are installed. | 
| Non-MSIX-packaged | - Must distribute runtime either using the Installer or by installing required MSIX packages directly. <br> - Additional runtime requirements: Must initialize access to the Windows App SDK runtime via the Bootstrap API. | 

For more details on these requirements, see the following articles:
- [Windows App SDK deployment guide for MSIX-packaged apps](deploy-packaged-apps.md) 
- [Windows App SDK deployment guide for non-MSIX-packaged apps](deploy-unpackaged-apps.md)

## Key terms

The following sections define key terms for Windows App SDK deployment and additional details on some of these packages.

| Term | Definition |
|------|------------|
| **Windows App SDK runtime** | The MSIX packages required by an app to use the Windows App SDK. These packages include: Framework, Main, Singleton, and DDLM. Depending on the features used and your app deployment method, you will need a certain set of these packages on the target machine. |
| **Framework package** | Contains binaries used at run time by apps (most Windows App SDK features). The framework includes a bootstrapper component that enables apps to automatically install the latest version of the Windows App SDK, which will be updated on a regular release cadence. |
| **Main package** | Package that contains background tasks to keep track of dynamic dependencies, and enables automatic updates to the Framework package from the Microsoft Store. |
| **Singleton package** | Contains background tasks, services, app extensions, and other components not included in the Framework package such as Push Notifications. This is generally a single long-running process that is brokered between apps. |
| **Dynamic Dependency Lifetime Manager (DDLM) package** | Prevents the OS from performing servicing updates to the MSIX packages while a non-MSIX-packaged app is in use. |
| **Bootstrapper** | An app-local binary used by non-MSIX-packaged apps to locate and load the best Windows App SDK version match as needed by the app.  |
| **Provisioning** | The process of installing and registering packages (including files and registry keys) system-wide to eliminate the need for repeated installation by the other users. It can be done either as part of the OS or done during installation of an app. |
| **Installer** | Refers to the .exe installer which deploys the Framework, Main, Singleton, and DDLM packages. |
| **MSIX** | Modern installer technology that enables users to safely install an app per user, directly from the Microsoft Store or a web site. On Enterprise or shared PCs, apps can be installed for all users via PowerShell and MDM. |

### Framework package

When you build an app that uses the Windows App SDK, your app references a set of Windows App SDK runtime components that are distributed to end users via a *framework package*. The framework package allows apps to access Windows App SDK components through a single shared source on the user's device, instead of bundling them into the app package. The framework package also carries its own resources, such as DLLs and API definitions (COM and Windows Runtime registrations). These resources run in the context of your app, so they inherit the capabilities and privileges of your app, and don't assert any capabilities or privileges of their own. For more information about framework package dependencies, see [MSIX framework packages and dynamic dependencies](../desktop/modernize/framework-packages/framework-packages-overview.md).

The Windows App SDK framework package is an MSIX package that is deployed to end users through the Microsoft Store. It can be easily and quickly updated with servicing releases, which may include security and reliability fixes. All framework-dependent apps that use the Windows App SDK have a dependency on a shared instance of the framework package, as illustrated in the following diagram.

[![Diagram of how apps access the Windows App SDK framework package](images/framework.png) ](images/framework.png#lightbox)

When a new version of the Windows App SDK framework package is serviced, all framework-dependent apps are updated to the new version without themselves having to redistribute a copy. Windows updates to the newest version of frameworks as they are released, and apps will automatically reference the latest framework package version during relaunch. Older framework package versions will not be removed from the system until they are no longer running or being actively used by apps on the system.

[![Diagram of how apps get updates to the Windows App SDK framework package](images/framework-update.png) ](images/framework-update.png#lightbox)

Because app compatibility is important to Microsoft and to apps that depend on the Windows App SDK, the Windows App SDK framework package follows [Semantic Versioning 2.0.0](https://semver.org/) rules. This means that after we release version 1.0 of the Windows App SDK, the Windows App SDK framework package will guarantee compatibility between minor and patch version changes, and breaking changes will occur only between major version updates.

### Singleton package

The **singleton package** ensures that a single long-running process can handle services that are used across multiple apps, which may be running on different versions of the Windows App SDK. 

The Windows App SDK singleton is needed to enable [push notifications](notifications/push/index.md) for unpackaged apps and packaged Win32 applications using Windows versions below 20H1, which cannot be supported by the existing UWP [PushNotificationTrigger](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger) and [ToastNotificationActionTrigger](/uwp/api/windows.applicationmodel.background.toastnotificationactiontrigger) class. Future Windows App SDK features that cannot be supported by the Framework package will be added to the Singleton package.

## Related topics

* [Windows App SDK deployment guide for MSIX-packaged apps](deploy-packaged-apps.md)
* [Windows App SDK deployment guide for non-MSIX-packaged apps](deploy-unpackaged-apps.md) 
