---
title: Xbox Media App Development Options
description: This section describes possible architectures and development environments for building Xbox media applications
ms.date: 04/21/2022
ms.topic: article
keywords: Xbox
---
# Xbox Media App Development Options 

### Choosing the right version of Visual Studio 

The primary IDE used for Xbox app development is Visual Studio. You should familiarize yourself with the section in this document on App Development Models before deciding which version to install. 

If you are developing your UI using mostly web technologies hosted in a WebView, it is best to use Visual Studio 2017. It is the only version with comprehensive JavaScript debugging tools which work with the original WebView control. 

[Visual Studio Older Downloads](https://visualstudio.microsoft.com/vs/older-downloads/)

If you are developing your UI primarily in XAML or using technologies like MediaFoundation and DirectX to build your UI from scratch, you can use any version from 2017 onwards. It is recommended that you use the latest release. 

[Download Visual Studio tools](https://visualstudio.microsoft.com/downloads/) 

NOTE: It can be tricky to fully downgrade your development environment after using a later version, so it's recommended that you only install the version you need. 

### Installing the right components 

Once you've chosen a Visual Studio version, follow these instructions to get it set up: 

[Getting started with UWP app development on Xbox One - UWP application](../xbox-apps/getting-started.md)

### Deploying your app to Xbox 

Follow these instructions to put your PC and Xbox into developer mode, and pair Visual Studio with your Xbox so that you can deploy your app to it: 

[Getting started with UWP app development on Xbox One - UWP application](../xbox-apps/getting-started.md)

### Custom build automation 

If you want to use a different application as your main IDE, or if you want to build your own custom build tools, apps can be built using the command line: 

[Package from the command line - MSIX](/windows/msix/package/manual-packaging-root)

You can also use Azure Pipelines to set up automated builds for your application: 

[Set up automated build for your UWP app - UWP application](../packaging/auto-build-package-uwp-apps.md)

### Manual side-loading 

If you want to side-load your application onto your Xbox manually, you will need to first generate a signed appxpackage either using the command line tools described under Custom build automation, or from Visual Studio: 

[Packaging MSIX apps - MSIX](/windows/msix/package/packaging-uwp-apps)

Then, you can use the Home tab of the Xbox Device Portal to side-load your app and its dependencies: 

[Device Portal for Xbox - UWP application](../xbox-apps/device-portal-xbox.md)