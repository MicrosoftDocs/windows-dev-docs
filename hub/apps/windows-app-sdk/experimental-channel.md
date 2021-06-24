---
title: Experimental release channel for the Windows App SDK
description: Learn about the latest experimental releases of the Windows App SDK.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Experimental release channel for the Windows App SDK

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel provides releases of the Windows App SDK that include experimental features that are in early stages of development. 

APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel might have breaking changes in future releases, but experimental APIs are especially subject to change. Experimental features may be removed from the next release, or may never be released.

The following releases of the experimental channel are currently available:

- [Version 0.8 Preview](#version-08-preview-080-preview)

If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md). 

## Version 0.8 Preview (0.8.0-preview)

This is the latest release of the experimental channel

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/previewdownload)

### New features and updates

This release supports all [experimental channel features](release-channels.md#features-available-by-release-channel).

## Release notes

This section lists new features, limitations and known issues for this release.

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI project templates](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](get-started.md#use-the-windows-app-sdk-in-an-existing-project) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or higher**: Your TFM must be set to `net5.0-windows10.0.18362` or higher, and your packaging project's <TargetPlatformVersion> must be set to 18362 or higher. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).

#### WinUI 3

This release introduces critical bug fixes (including those fixed in 0.5 servicing releases) and other changes for WinUI. For a complete list, see the [WinUI 3 - Windows App SDK 0.8 release notes](../winui/winui3/release-notes/release-notes-08-preview.md).

#### AppLifecycle (experimental feature)

This release introduces new experimental features related to managing the app lifecycle of your app.

- All apps (packaged and unpackaged) can use **GetActivatedEventArgs** (although packaged apps can already use the implementation of this in the platform).
- Only unpackaged apps can use the **RegisterForXXXActivation** functions.
- Packaged desktop apps can use app lifecycle instancing.
- UWP apps cannot use app lifecycle features in the current release.

For more information, see [App instancing in AppLifecycle](applifecycle/applifecycle-instancing.md) and [Rich activation in AppLifecycle](applifecycle/applifecycle-rich-activation.md).

#### Deployment for unpackaged apps (experimental feature)

This release introduces new experimental deployment features for unpackaged apps (that is, apps that are not deployed in an MSIX package). Unpackaged apps can now dynamically take a dependency on the Windows App SDK runtime packages so you can continue using your existing MSI or setup program for app deployment. This is available through the following features:

- Standalone installer for Windows App SDK.
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

The [Windows App SDK samples](https://github.com/microsoft/Project-Reunion-Samples) do not yet work with this release. New and updated samples, including samples that demonstrate new features such as unpackaged app deployment, are coming soon.

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)