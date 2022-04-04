---
title: Windows App SDK deployment guide for self-contained apps
description: A Windows App SDK project is framework-dependent by default. To switch to self-contained deployment, follow the steps in this article (the terms *framework-dependent* and *self-contained* are described in [Windows App SDK deployment overview](../deploy-overview.md)).
ms.topic: article
ms.date: 03/22/2022
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for self-contained apps

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

> [!IMPORTANT]
> The self-contained feature described in this topic is available in Windows App SDK 1.1 Preview 1.

A Windows App SDK project is framework-dependent by default. To switch to self-contained deployment, follow the steps below (the terms *framework-dependent* and *self-contained* are described in [Windows App SDK deployment overview](../deploy-overview.md)).

* In Visual Studio, right-click the project node, and click **Edit Project File** to open the project file for editing. For a C++ project, first click **Unload Project**.
* In the project file, inside the main `PropertyGroup`, add `<WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>` as shown in the screenshot below. Save and close.
    * If you're using a **Windows Application Packaging Project** (rather than the single-project MSIX that you get with **Blank App, Packaged (WinUI 3 in Desktop)**), then set this property both in the app project and in the packaging project.
* Click **Reload Project**.

![Screenshot showing the WindowsAppSDKSelfContained property set in a project file.](../../images/winappsdk-self-contained.png)

> [!NOTE]
> Don't set this property in a library project. Set it only in an app project (and, where applicable, in a **Windows Application Packaging Project**).

For sample apps, see [Windows App SDK self-contained deployment samples](https://github.com/microsoft/WindowsAppSDK-Samples/tree/mikebattista/selfcontained/Samples/SelfContainedDeployment).

Having set the `WindowsAppSDKSelfContained` property to `true` in your project file, the contents of the Windows App SDK Framework package will be extracted to your build output, and deployed as part of your application.

> [!NOTE]
> .NET apps need to be [published as self-contained](/dotnet/core/deploying/#publish-self-contained) as well to be fully self-contained. See [this sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/f1a30c2524c785739fee842d02a1ea15c1362f8f/Samples/SelfContainedDeployment/cs-winui-unpackaged/SelfContainedDeployment.csproj#L12) for how to configure .NET self-contained with publish profiles. `dotnet publish` is not yet supported with Windows App SDK 1.1.

> [!NOTE]
> C++ apps need to use the [hybrid CRT](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/Coding-Guidelines/HybridCRT.md#what-is-the-hybrid-crt) as well to be fully self-contained. Add [HybridCRT.props](https://github.com/microsoft/WindowsAppSDK/blob/main/HybridCRT.props) to the root of your project as well as a [Directory.Build.props](https://github.com/microsoft/WindowsAppSDK-Samples/blob/mikebattista/selfcontained/Samples/SelfContainedDeployment/cpp-winui-unpackaged/Directory.Build.props) file that imports it (see an example in [Directory.Build.props](https://github.com/microsoft/WindowsAppSDK-Samples/blob/43404afcc4e72294b3e2706d2eff12418dbb815a/Samples/SelfContainedDeployment/cpp-winui-unpackaged/Directory.Build.props#L3)). See the [Self-contained deployment](https://github.com/microsoft/WindowsAppSDK-Samples/tree/mikebattista/selfcontained/Samples/SelfContainedDeployment/cpp-winui-unpackaged) sample app for how to use the hybrid CRT.

If your app is MSIX-packaged (for more info, see [Deployment overview](/windows/apps/package-and-deploy/)), then the Windows App SDK dependencies will be included as content inside the MSIX package. Deploying the app still requires registering the MSIX package like any other MSIX-packaged app.

If your app is not MSIX-packaged, then the Windows App SDK dependencies are copied next to the `.exe` in your build output. You can xcopy-deploy the resulting files, or include them in a custom installer.

## Dependencies on additional MSIX packages

A small set of APIs in the Windows App SDK rely on additional MSIX packages that represent critical operating system (OS) functionality.

* As of the Windows App SDK 1.1, push notifications depends on additional MSIX packages (see [Deployment architecture for the Windows App SDK](/windows/apps/windows-app-sdk/deployment-architecture)).

Consider these options when you're considering using those APIs in a self-contained app:

1. Light up the functionality as optional in your app using the API's **IsSupported** design pattern.
    * Doing so enables optional use of the API without compromising the simplicity of self-contained deployment.
    * Only if the OS services are installed outside of your app deployment will your app light up the appropriate functionality.
2. Deploy the required MSIX packages as part of your app installation.
    * Doing so allows you to depend on the API in all scenarios. But requiring MSIX package deployment of dependencies as part of your app deployment can compromise the simplicity of self-contained deployment.
3. Don't use the API.
    * Consider alternative APIs that would provide similar functionality without additional deployment requirements.

## Related topics

* [Windows App SDK deployment overview](../deploy-overview.md)
* [Windows App SDK self-contained deployment samples](https://github.com/microsoft/WindowsAppSDK-Samples/tree/mikebattista/selfcontained/Samples/SelfContainedDeployment)
* [Deployment overview](/windows/apps/package-and-deploy/)
* [Deployment architecture for the Windows App SDK](/windows/apps/windows-app-sdk/deployment-architecture)
