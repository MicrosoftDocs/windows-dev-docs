---
title: Experimental channel release notes for the Windows App SDK 0.8
description: Learn about the experimental channel release notes for the Windows App SDK 0.8
ms.topic: article
ms.date: 04/19/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK 0.8

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel provides releases of the Windows App SDK that include [experimental channel features](../release-channels.md#features-available-by-release-channel) that are in early stages of development. APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel might have breaking changes in future releases, but experimental APIs are especially subject to change. Experimental features may be removed from the next release, or may never be released.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on experimental releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest experimental channel release:**

- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Version 0.8 Preview (0.8.0-preview)

This release supports all [experimental channel features](../release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/previewdownload)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release introduces critical bug fixes (including those fixed in 0.5 servicing releases) and other changes for WinUI. For a complete list, see the [Overview and release notes: Windows UI Library 3 - Project Reunion 0.8 Preview (May 2021)](../../winui/winui3/release-notes/release-notes-08-preview.md).

### App lifecycle (experimental feature)

This release introduces new experimental features related to managing the app lifecycle of your app.

- All apps (packaged and unpackaged) can use **GetActivatedEventArgs** (although packaged apps can already use the implementation of this in the platform).
- Only unpackaged apps can use the **RegisterForXXXActivation** functions.
- Packaged desktop apps can use app lifecycle instancing.

For more information, see [App instancing with the app lifecycle API](../applifecycle/applifecycle-instancing.md) and [Rich activation with the app lifecycle API](../applifecycle/applifecycle-rich-activation.md).

### Deployment for unpackaged apps (experimental feature)

This release introduces new experimental deployment features for unpackaged apps. Unpackaged apps can now dynamically take a dependency on the Windows App SDK runtime packages so you can continue using your existing MSI or setup program for app deployment. This is available through the following features:

- Standalone installer for Windows App SDK.
- MSIX package bundle that includes dynamic dependencies functionality.

For more info, see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../deploy-unpackaged-apps.md).

### DWriteCore

This release adds the following features:  

- Text decorations (underline and strikethrough in the text layout API)
- Vertical text layout
- Font face kerning API
- Experimental support for unpackaged apps

For more information, see [DirectWrite to DWriteCore migration](../migrate-to-windows-app-sdk/guides/dwritecore.md).

### MRT Core

This release adds the following features:

- The build action for resources is now automatically set, reducing the need for manual project configuration.
- Experimental support for unpackaged apps.

For more information, see [Manage resources with MRT Core](../mrtcore/mrtcore-overview.md).

### Limitations and known issues

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](../use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).

### Samples

The [Windows App SDK samples](https://github.com/microsoft/Project-Reunion-Samples) do not work with this release.

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
