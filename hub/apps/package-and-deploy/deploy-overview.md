---
title: Windows App SDK deployment overview
description: There are two ways in which you can deploy the Windows App SDK&mdash;framework-dependent or self-contained.
ms.topic: article
ms.date: 11/02/2023
ms.localizationpriority: medium
---

# Windows App SDK deployment overview

There are two ways in which you can deploy the Windows App SDK:

* **Framework-dependent**. Your app depends on the Windows App SDK runtime and/or Framework package being present on the target machine. Framework-dependent deployment is the default deployment mode of the Windows App SDK for its efficient use of machine resources and serviceability.
* **Self-contained**. Your app carries the Windows App SDK dependencies with it. Self-contained deployment is a deployment option that was introduced in Windows App SDK 1.1 Preview 1.

This topic also uses the terms *packaged app*, *packaged app with external location*, and *unpackaged app*. For explanations of those terms, see the [Deployment overview](./index.md).

| | Deploy framework-dependent | Deploy self-contained |
| - | - | - |
| **Advantages** | *Small deployment*. Only your app and its other dependencies are distributed. The Windows App SDK runtime and Framework package are installed automatically by framework-dependent apps that are packaged; or as part of the Windows App SDK runtime installer by framework-dependent apps that are either packaged with external location or unpackaged.<br/><br/>*Serviceable*. Servicing updates to the Windows App SDK are installed automatically via the Windows App SDK Framework package without any action required of the app. | *Control Windows App SDK version*. You control which version of the Windows App SDK is deployed with your app. Servicing updates of the Windows App SDK won't impact your app unless you rebuild and redistribute it.<br/><br/>*Isolated from other apps*. Apps and users can't uninstall your Windows App SDK dependency without uninstalling your entire app.<br/><br/>*Xcopy deployment*. Since the Windows App SDK dependencies are carried by your app, you can deploy your app by simply xcopy-ing your build output, without any additional installation requirements. |
| **Disadvantages** | *Additional installation dependencies*. Requires installation of the Windows App SDK runtime and/or Framework package, which can add complexity to app installation.<br/><br/>*Shared dependencies*. Risk that shared dependencies are uninstalled. Apps or users uninstalling the shared components can impact the user experience of other apps that share the dependency.<br/><br/>*Compatibility risk*. Risk that servicing updates to the Windows App SDK introduce breaking changes. While servicing updates should provide backward compatibility, it's possible that regressions are introduced. | *Larger deployments (unpackaged apps only)*. Because your app includes the Windows App SDK, the download size and hard drive space required are greater than would be the case for a framework-dependent version.<br/><br/>*Performance (unpackaged apps only)*. Slower to load, and uses more memory since code pages aren't shared with other apps.<br/><br/>*Not serviceable*. The Windows App SDK version distributed with your app can be updated only by releasing a new version of your app. You're responsible for integrating servicing updates of the Windows App SDK into your app. |

Also see [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md), and [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

## More info about framework-dependent deployment

Before configuring your framework-dependent app for deployment, to learn more about the dependencies your app takes when it uses the Windows App SDK, review [Deployment architecture for the Windows App SDK](../windows-app-sdk/deployment-architecture.md).

### Packaged apps

If you've chosen to go with a framework-dependent packaged app (see [Deployment overview](./index.md)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for framework-dependent packaged apps](../windows-app-sdk/deploy-packaged-apps.md)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

### Packaged with external location or unpackaged apps

If you've chosen to go with a framework-dependent packaged app with external location, or a framework-dependent unpackaged app (see [Deployment overview](./index.md)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../windows-app-sdk/deploy-unpackaged-apps.md)
* [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](../windows-app-sdk/tutorial-unpackaged-deployment.md)

## More info about self-contained deployment

See [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md).

## Initialize the Windows App SDK

The way that you should initialize the Windows App SDK depends on whether, and how, you package your app; and on the way in which you deploy relative to the Windows App SDK runtime. Use the section below that applies to your app.

### Packaged apps

|How your app deploys|How to initialize|
|-|-|
|Framework-dependent|See [Call the Deployment API](../windows-app-sdk/deploy-packaged-apps.md#call-the-deployment-api).|
|Self-contained|No initialization necessary.|

### Unpackaged apps, and apps packaged with external location

|How your app deploys|How to initialize|
|-|-|
|Framework-dependent|See [Use the bootstrapper API in an app packaged with external location or unpackaged](../windows-app-sdk/tutorial-unpackaged-deployment.md).|
|Self-contained|See [Opting out of (or into) automatic UndockedRegFreeWinRT support](./self-contained-deploy/deploy-self-contained-apps.md#opting-out-of-or-into-automatic-undockedregfreewinrt-support).|

## Related topics

* [Deployment overview](./index.md)
* [Deployment architecture for the Windows App SDK](../windows-app-sdk/deployment-architecture.md)
* [Windows App SDK deployment guide for framework-dependent packaged apps](../windows-app-sdk/deploy-packaged-apps.md)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)
* [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../windows-app-sdk/deploy-unpackaged-apps.md)
* [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](../windows-app-sdk/tutorial-unpackaged-deployment.md)
* [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md)
* [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
* [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
