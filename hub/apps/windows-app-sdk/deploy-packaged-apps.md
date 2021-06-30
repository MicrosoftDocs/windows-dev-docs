---
title: Deploy packaged apps that use the Windows App SDK
description: This article provides instructions for deploying packaged apps that use the Windows App SDK.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Deploy packaged apps that use the Windows App SDK 

This article provides guidance about deploying [MSIX](/windows/msix)-packaged apps that use the Windows App SDK to other computers.

## Overview

Before configuring your apps for deployment, review [the Windows App SDK deployment architecture](deployment-architecture.md) to learn more about the dependencies your app takes when it uses the Windows App SDK.

By default, when you create a project using one of the [WinUI project templates](..\winui\winui3\winui-project-templates-in-visual-studio.md) that are provided with the Windows App SDK extension for Visual Studio, your project includes a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an MSIX package. For more information about configuring this project to build an MSIX package for your app, see [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps).

After you build an MSIX package for your app, you have several options for deploying it to other computers. For more information, see [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview).

## Preview versions of the Windows App SDK

When you install a [preview release channel](preview-channel.md) version of the Windows App SDK extension for Visual Studio or the Windows App SDK NuGet package on your development computer, the preview version of the [framework package](deployment-architecture.md#framework-packages-for-packaged-and-unpackaged-apps) is deployed during build time as a NuGet package dependency.

## Stable versions of the Windows App SDK

When you install a [stable release channel](stable-channel.md) version of the Windows App SDK extension or NuGet package on your development computer and you create a project using one of the provided WinUI 3 project templates, the generated package manifest contains a [PackageDependency](/uwp/schemas/appxpackage/uapmanifestschema/element-packagedependency) element that specifies a dependency on the framework package.

```xml
<Dependencies>
    <PackageDependency Name="Microsoft.ProjectReunion.0.8-preview" MinVersion="8000.144.525.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />
</Dependencies>
```

However, if you build your app package manually, you must add this **PackageDependency** element to your package manifest yourself to declare a dependency on the Windows App SDK framework package.

## Related topics

- [Runtime architecture and deployment scenarios](deployment-architecture.md)
- [Deploy unpackaged apps that use the Windows App SDK](deploy-unpackaged-apps.md)
- [Release channels](release-channels.md)
