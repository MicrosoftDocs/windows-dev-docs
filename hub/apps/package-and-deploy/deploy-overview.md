---
title: Windows App SDK deployment overview
description: There are two ways in which you can deploy the Windows App SDK&mdash;framework-dependent or self-contained.
ms.topic: concept-article
ms.date: 11/18/2026
ms.localizationpriority: medium
keywords: windows app sdk deployment, framework-dependent, self-contained, deploy winui 3, windows app sdk self-contained
---

# Windows App SDK deployment overview

There are two ways in which you can deploy the Windows App SDK:

* **Framework-dependent**. Your app depends on the Windows App SDK runtime and/or Framework package being present on the target machine. Framework-dependent deployment is the default deployment mode of the Windows App SDK for its efficient use of machine resources and serviceability.
* **Self-contained**. Your app carries the Windows App SDK dependencies with it, eliminating the need for a separate runtime installation on the target machine.

This topic also uses the terms *packaged app*, *packaged app with external location*, and *unpackaged app*. For explanations of those terms, see the [Deployment overview](./index.md).

| | Deploy framework-dependent | Deploy self-contained |
| - | - | - |
| **Advantages** | *Small deployment*. Only your app and its other dependencies are distributed. The Windows App SDK runtime and Framework package are installed automatically by framework-dependent apps that are packaged; or as part of the Windows App SDK runtime installer by framework-dependent apps that are either packaged with external location or unpackaged.<br/><br/>*Serviceable*. Servicing updates to the Windows App SDK are installed automatically via the Windows App SDK Framework package without any action required of the app. | *Control Windows App SDK version*. You control which version of the Windows App SDK is deployed with your app. Servicing updates of the Windows App SDK won't impact your app unless you rebuild and redistribute it.<br/><br/>*Isolated from other apps*. Apps and users can't uninstall your Windows App SDK dependency without uninstalling your entire app.<br/><br/>*Xcopy deployment*. Since the Windows App SDK dependencies are carried by your app, you can deploy your app by simply xcopy-ing your build output, without any additional installation requirements. |
| **Disadvantages** | *Additional installation dependencies*. Requires installation of the Windows App SDK runtime and/or Framework package, which can add complexity to app installation.<br/><br/>*Shared dependencies*. Risk that shared dependencies are uninstalled. Apps or users uninstalling the shared components can impact the user experience of other apps that share the dependency.<br/><br/>*Compatibility risk*. Risk that servicing updates to the Windows App SDK introduce breaking changes. While servicing updates should provide backward compatibility, it's possible that regressions are introduced. | *Larger deployments (unpackaged apps only)*. Because your app includes the Windows App SDK, the download size and hard drive space required are greater than would be the case for a framework-dependent version.<br/><br/>*Performance (unpackaged apps only)*. Slower to load, and uses more memory since code pages aren't shared with other apps.<br/><br/>*Not serviceable*. The Windows App SDK version distributed with your app can be updated only by releasing a new version of your app. You're responsible for integrating servicing updates of the Windows App SDK into your app. |

Also see [Create your first WinUI 3 project](/windows/apps/get-started/start-here), and [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

> [!NOTE]
> **`PublishSingleFile` (single-file EXE)** is supported for **unpackaged, self-contained** WinUI 3 apps (Windows App SDK 1.5 and later). Packaged apps and framework-dependent apps do not support `PublishSingleFile`. See [Single-file EXE](./unpackage-winui-app.md#single-file-exe) for required MSBuild properties.

## More info about framework-dependent deployment

Before configuring your framework-dependent app for deployment, to learn more about the dependencies your app takes when it uses the Windows App SDK, review [Deployment architecture for the Windows App SDK](../windows-app-sdk/deployment-architecture.md).

### Packaged apps

If you've chosen to go with a framework-dependent packaged app (see [Deployment overview](./index.md)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for framework-dependent packaged apps](../windows-app-sdk/deploy-packaged-apps.md)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

### Packaged with external location or unpackaged apps

For framework-dependent apps packaged with external location or unpackaged apps, you can deploy the required Windows App SDK runtime packages using your existing installer. This shared installation guidance doesn't mean that both packaging options require Bootstrapper initialization: apps packaged with external location can declare a static Framework dependency in their package manifest.

* [Windows App SDK deployment guide for framework-dependent unpackaged apps](../windows-app-sdk/deploy-unpackaged-apps.md)
* For unpackaged apps that need explicit initialization, see [Tutorial: Use the bootstrapper API in an unpackaged app](../windows-app-sdk/tutorial-unpackaged-deployment.md).

## More info about self-contained deployment

See [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md).

> [!NOTE]
> `PublishSingleFile` (single-file EXE) requires the app to be both **unpackaged** and **self-contained**. See [Single-file EXE](../package-and-deploy/unpackage-winui-app.md#single-file-exe) for the full list of required MSBuild properties.

## Initialize the Windows App SDK

Framework-dependent apps need the Framework package in their process package graph. Packaged apps can declare it statically in their package manifest; unpackaged apps use Bootstrapper initialization to add it dynamically. Self-contained apps carry the Framework binaries app-locally and don't need a Framework MSIX dependency or Bootstrapper initialization for those binaries. Initialization for additional Main/Singleton functionality is separate.

### Packaged apps, including apps packaged with external location

|How your app deploys|How to initialize|
|-|-|
|Framework-dependent|Declare a Framework `<PackageDependency>` in `AppxManifest.xml`. Bootstrapper initialization isn't required for that dependency. If the app needs Main/Singleton functionality, see [Call the Deployment API](../windows-app-sdk/deploy-packaged-apps.md#call-the-deployment-api).|
|Self-contained|No Framework package dependency or Bootstrapper initialization is necessary for the app-local Framework binaries. Check API-specific requirements for other initialization or dependencies.|

### Unpackaged apps

|How your app deploys|How to initialize|
|-|-|
|Framework-dependent|Use the Bootstrapper auto-initializer or explicit Bootstrapper calls to add the Framework dependency dynamically. See [Initialize the Windows App SDK Framework package dynamically](../windows-app-sdk/use-windows-app-sdk-run-time.md).|
|Self-contained|No Framework package dependency or Bootstrapper initialization is necessary. For registration-free WinRT activation, see [Opting out of (or into) automatic UndockedRegFreeWinRT support](./self-contained-deploy/deploy-self-contained-apps.md#opting-out-of-or-into-automatic-undockedregfreewinrt-support).|

## Related topics

* [Deployment overview](./index.md)
* [Deployment architecture for the Windows App SDK](../windows-app-sdk/deployment-architecture.md)
* [Windows App SDK deployment guide for framework-dependent packaged apps](../windows-app-sdk/deploy-packaged-apps.md)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)
* [Windows App SDK deployment guide for framework-dependent unpackaged apps](../windows-app-sdk/deploy-unpackaged-apps.md)
* [Tutorial: Use the bootstrapper API in an unpackaged app](../windows-app-sdk/tutorial-unpackaged-deployment.md)
* [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md)
* [Create your first WinUI project](/windows/apps/get-started/start-here)
* [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
