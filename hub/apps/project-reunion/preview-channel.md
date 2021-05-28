---
title: Preview release channel for Project Reunion 
description: Provides information about the preview release channel for Project Reunion.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, project reunion 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Preview release channel for Project Reunion

The preview channel provides releases of Project Reunion that include experimental features. The preview channel is not supported for use in production environments, and apps that use the preview channel releases cannot be published to the Microsoft Store.

The latest release of the preview channel is Project Reunion version 0.8 Preview.

> [!div class="button"]
> [Download the latest preview release](https://aka.ms/projectreunion/previewdownload)

## Features in the latest preview release

The latest preview channel release includes the following sets of APIs and components you can explore and experiment with in your apps.

| Component | Description |
|---------|-------------|
| [Windows UI Library 3](../winui/winui3/index.md) | Windows UI Library (WinUI) 3 is the next generation of the Windows user experience (UX) platform for Windows apps. This release includes Visual Studio project templates to help get started [building apps with a WinUI-based user interface](..\winui\winui3\winui-project-templates-in-visual-studio.md), and a NuGet package that contains the WinUI libraries.  |
| [Manage resources with MRT Core](mrtcore/mrtcore-overview.md) | MRT Core provides APIs to load and manage resources used by your app. MRT Core is a streamlined version of the modern [Windows Resource Management System](/windows/uwp/app-resources/resource-management-system). |
| [Render text with DWriteCore](dwritecore.md) | DWriteCore provides access to all current DirectWrite features for text rendering, including a device-independent text layout system, hardware-accelerated text, multi-format text, and wide language support.  |
| [AppLifecycle](applifecycle/applifecycle-instancing.md) | **Experimental feature**. Apps can use AppLifecycle APIs to manage their lifecycle behavior, such as retrieving activation information and defining your app's instancing model.  |
| [Deploy unpackaged apps](deploy-unpackaged-apps.md) | **Experimental feature**. Unpackaged apps can dynamically take a dependency on the Project Reunion runtime packages so you can continue using your existing MSI or setup program for app deployment.  |

## Release notes

This section lists new features, limitations and known issues for Project Reunion version 0.8 Preview.

- **No support for Any CPU build configuration**: Project Reunion is written in native code and thus does not support **Any CPU** build configurations. The [WinUI project templates](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding Project Reunion](get-started-with-project-reunion.md#use-project-reunion-in-an-existing-project) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

#### WinUI 3

Project Reunion 0.8 Preview introduces critical bug fixes (including those fixed in Project Reunion 0.5 servicing releases) and other changes for WinUI. For a complete list, see the [WinUI 3 - Project Reunion 0.8 release notes](../winui/winui3/release-notes/release-notes-08-preview.md).

#### AppLifecycle (experimental feature)

This release introduces new experimental features related to managing the app lifecycle of your app.

- All apps (packaged and unpackaged) can use **GetActivatedEventArgs** (although packaged apps can already use the implementation of this in the platform).
- Only unpackaged apps can use the **RegisterForXXXActivation** functions.
- Packaged desktop apps can use app lifecycle instancing.
- UWP apps cannot use app lifecycle features in the current release.

For more information, see [App instancing in AppLifecycle](applifecycle/applifecycle-instancing.md) and [Rich activation in AppLifecycle](applifecycle/applifecycle-rich-activation.md).

#### Deployment for unpackaged apps (experimental feature)

This release introduces new experimental deployment features for unpackaged apps (that is, apps that are not deployed in an MSIX package). Unpackaged apps can now dynamically take a dependency on the Project Reunion runtime packages so you can continue using your existing MSI or setup program for app deployment. This is available through the following features:

- Standalone installer for Project Reunion.
- MSIX package bundle that includes dynamic dependencies functionality.

For more more information, see [Deploy unpackaged apps](deploy-unpackaged-apps.md).

#### DWriteCore

This release adds the following features:  

- Text decorations (underline and strikethrough in the text layout API)
- Vertical text layout
- Font face kerning API
- Experimental support for unpackaged apps

For more more information, see [Render text with DWriteCore](dwritecore.md).

#### MRT Core

This release adds the following features:

- The build action for resources is now automatically set, reducing the need for manual project configuration.
- Experimental support for unpackaged apps.

For more more information, see [Manage resources with MRT Core](mrtcore/mrtcore-overview.md).

#### Samples

The [Project Reunion samples](https://github.com/microsoft/Project-Reunion-Samples) do not yet work with Project Reunion version 0.8 Preview. New and updated samples, including samples that demonstrate new features such as unpackaged app deployment, are coming soon.

## Related topics

- [Stable channel](stable-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)