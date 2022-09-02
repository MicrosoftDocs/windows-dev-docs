---
title: Windows App SDK deployment overview
description: There are two ways in which you can deploy the Windows App SDK&mdash;framework-dependent or self-contained.
ms.topic: article
ms.date: 03/14/2022
ms.localizationpriority: medium
---

# Windows App SDK deployment overview

There are two ways in which you can deploy the Windows App SDK.

* **Framework-dependent**. Your app depends on the Windows App SDK runtime and/or Framework package being present on the target machine. Framework-dependent deployment is the default deployment mode of the Windows App SDK for its efficient use of machine resources and serviceability.
* **Self-contained**. Your app carries the Windows App SDK dependencies with it. Self-contained deployment is a deployment option that's new for Windows App SDK 1.1 Preview 1.

| | Deploy framework-dependent | Deploy self-contained |
| - | - | - |
| **Advantages** | Small deployment. Only your app and its other dependencies are distributed. The Windows App SDK runtime and Framework package are installed automatically by apps using MSIX-packaging; or as part of the Windows App SDK runtime installer by apps that use sparse-packaging or that aren't packaged.<br/><br/>Serviceable. Servicing updates to the Windows App SDK are installed automatically via the Windows App SDK Framework package without any action required of the app. | Control Windows App SDK version. You control which version of the Windows App SDK is deployed with your app. Servicing updates of the Windows App SDK won't impact your app unless you rebuild and redistribute it.<br/><br/>Isolated from other apps. Apps and users can't uninstall your Windows App SDK dependency without uninstalling your entire app.<br/><br/>Xcopy deployment. Since the Windows App SDK dependencies are carried by your app, you can simply xcopy your build output to deploy your app without any additional installation requirements. |
| **Disadvantages** | Additional installation dependencies. Requires installation of the Windows App SDK runtime and/or Framework package, which can add complexity to app installation.<br/><br/>Shared dependencies. Risk that shared dependencies are uninstalled. Apps or users uninstalling the shared components can impact the user experience of other apps that share the dependency.<br/><br/>Compatibility risk. Risk that servicing updates to the Windows App SDK introduce breaking changes. While servicing updates should provide backward compatibility, it is possible that regressions are introduced. | Larger deployments. Because your app includes the Windows App SDK, the download size and hard drive space required is greater than a framework-dependent version.<br/><br/>Performance. Slower to load and uses more memory since code pages aren't shared with other apps.<br/><br/>Not serviceable. The Windows App SDK version distributed with your app can be updated only by releasing a new version of your app. You're responsible for integrating servicing updates of the Windows App SDK into your app. |

Also see [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app) and [Use the Windows App SDK in an existing project](/windows/apps/windows-app-sdk/use-windows-app-sdk-in-existing-project)

## More info about framework-dependent deployment

Before configuring your framework-dependent app for deployment, review [Deployment architecture for the Windows App SDK](/windows/apps/windows-app-sdk/deployment-architecture) to learn more about the dependencies your app takes when it uses the Windows App SDK.

### Using MSIX-packaging

If you've chosen to go with a framework-dependent app that uses MSIX-packaging (see [Deployment overview](/windows/apps/package-and-deploy/)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for MSIX-packaged apps](/windows/apps/windows-app-sdk/deploy-packaged-apps)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

### Using sparse-packaging or no packaging

If you've chosen to go with an app that doesn't use MSIX-packaging (see [Deployment overview](/windows/apps/package-and-deploy/)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for sparse or unpackaged apps](/windows/apps/windows-app-sdk/deploy-unpackaged-apps)
* [Tutorial: Use the bootstrapper API in a non-MSIX-packaged app that uses the Windows App SDK](/windows/apps/windows-app-sdk/tutorial-unpackaged-deployment)

## More info about self-contained deployment

See [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md).

## Related topics

* [Deployment overview](/windows/apps/package-and-deploy/)
* [Deployment architecture for the Windows App SDK](/windows/apps/windows-app-sdk/deployment-architecture)
* [Windows App SDK deployment guide for MSIX-packaged apps](/windows/apps/windows-app-sdk/deploy-packaged-apps)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)
* [Windows App SDK deployment guide for sparse or unpackaged apps](/windows/apps/windows-app-sdk/deploy-unpackaged-apps)
* [Tutorial: Use the bootstrapper API in a non-MSIX-packaged app that uses the Windows App SDK](/windows/apps/windows-app-sdk/tutorial-unpackaged-deployment)
* [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md)
* [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)
* [Use the Windows App SDK in an existing project](/windows/apps/windows-app-sdk/use-windows-app-sdk-in-existing-project)
