---
author: laurenhughes
ms.assetid: ee51eae3-ed55-419e-ad74-6adf1e1fb8b9
title: Manual app packaging
description: This section contains or links to articles about manually packaging Universal Windows Platform (UWP) apps.
ms.author: lahugh
ms.date: 04/30/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, packaging
ms.localizationpriority: medium
---

# Manual app packaging

If you want to create and sign an app package, but you didn't use Visual Studio to develop your app, you'll need to use the manual app packaging tools.

> [!IMPORTANT] 
> If you used Visual Studio to develop your app, it's recommended that you use the Visual Studio wizard to create and sign your app package. For more information, see [Package a UWP app with Visual Studio](https://msdn.microsoft.com/windows/uwp/packaging/packaging-uwp-apps).

## Purpose

This section contains or links to articles about manually packaging Universal Windows Platform (UWP) apps.

| Topic | Description |
|-------|-------------|
| [Create an app package with the MakeAppx.exe tool](create-app-package-with-makeappx-tool.md) | MakeAppx.exe creates, encrypts, decrypts, and extracts files from app packages and bundles. |
| [Create a certificate for package signing](create-certificate-package-signing.md) | Create and export a certificate for app package signing with PowerShell tools. |
| [Sign an app package using SignTool](sign-app-package-using-signtool.md) | Use SignTool to manually sign an app package with a certificate. |

### Advanced topics

This section contains more advanced topics for componentizing a large and/or complex app for more efficient packaging and installation. 

> [!IMPORTANT]
> If you intend to submit your app to the Store, you need to contact [Windows developer support](https://developer.microsoft.com/windows/support) and get approval to use any of the advanced features in this section.


| Topic | Description |
|-------|-------------|
| [Introduction to asset packages](asset-packages.md) | Asset packages are a type of package that act as a centralized location for an application’s common files – effectively eliminating the necessity for duplicated files throughout its architecture packages. |
| [Developing with asset packages and package folding](package-folding.md) | Learn how to efficiently organize your app with asset packages and package folding. |
| [Flat bundle app packages](flat-bundles.md) | Describes how to create a flat bundle to bundle your app’s .appx package files with references to app packages. |
| [Package creation with the packaging layout](packaging-layout.md) | The packaging layout is a single document that describes packaging structure of the app. It specifies the bundles of an app (primary and optional), the packages in the bundles, and the files in the packages. |
