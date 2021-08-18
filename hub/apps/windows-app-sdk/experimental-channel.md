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

The experimental channel provides releases of the Windows App SDK that include [experimental channel features](release-channels.md#features-available-by-release-channel) that are in early stages of development. 

APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel might have breaking changes in future releases, but experimental APIs are especially subject to change. Experimental features may be removed from the next release, or may never be released.

The following releases of the experimental channel are currently available:

- [Version 1.0 Experimental](#version-10-experimental-100-experimental1)
- [Version 0.8 Preview](#version-08-preview-080-preview)

If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).

## Version 1.0 Experimental (1.0.0-experimental1)

This is the latest release of the experimental channel. It supports all [experimental channel features](release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](https://aka.ms/windowsappsdk/experimental-vsix)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release of WinUI 3 is focused on building towards new features for 1.0 stable and fixing bugs.

- **New features**: Support for showing a ContentDialog per window rather than per thread.
- **Bugs**: For the full list of bugs addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-exp-announcement). 
- **Samples**: To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 Controls Gallery app [from GitHub](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3), or download the app [from the Microsoft Store](https://www.microsoft.com/en-us/p/winui-3-controls-gallery/9p3jfpwwdzrc).

For more information or to get started developing with WinUI, see:

- [Windows UI 3 Library (WinUI)](../winui/index.md)
- [Get started developing apps with WinUI 3](../winui/winui3/get-started-winui3-for-desktop.md)

### Push notifications (experimental feature)

This release introduces a push notifications API that can be used by MSIX-packaged desktop apps with Azure app registration-based identities. To use this feature, you must [sign up for our private preview](https://aka.ms/windowsappsdk/push-private-preview).

Important limitations:

- Push notifications are only supported in MSIX packaged apps that are running on Windows 10 version 2004 (build 19041) or later releases.
- Microsoft reserves the right to disable or revoke apps from push notifications during the private preview.
- Microsoft does not guarantee the reliability or latency of push notifications.
- During the private preview, push notification volume is limited to 1 million per month.

For more information, see [Push notifications](notifications/push/index.md).

### Windowing (experimental feature)

This release includes updates to the windowing APIs. These are a set of high-level windowing APIs, centered around the AppWindow class, which allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and other apps. This is similar to, but not the same as, the UWP AppWindow.

Important limitations:

- This release of `AppWindow` is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an `AppWindow`; you are limited to using the `HWND` interop access methods.
- The Windowing API's will currently not work on Windows version 1809 and 1903 for AMD64.

For more information, see [Manage app windows](windowing/windowing-overview.md).

### Deployment for unpackaged apps (experimental feature)

This release introduces updates to the *dynamic dependencies* feature, including the *bootstrapper API*.

Important limitations:

- The dynamic dependencies feature is only supported for unpackaged apps (that is, apps that do not use MSIX for their deployment technology).
- Elevated callers aren't supported.

For more information, see the following articles:

- [Use MSIX framework packages dynamically from your desktop app](../desktop/modernize/framework-packages/framework-packages-overview.md)
- [Reference the Windows App SDK framework package at run time](reference-framework-package-run-time.md)

### Other limitations and known issues

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI project templates](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](get-started.md#use-the-windows-app-sdk-in-an-existing-project) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or higher**: Your TFM must be set to `net5.0-windows10.0.18362` or higher, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or higher. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).
- **C# apps using 1.0 Experimental must use one of the following .NET SDKs**: 
	- .NET 5 SDK version 5.0.400 or later if you're using Visual Studio 2019 version 16.11
	- .NET 5 SDK version 5.0.302 or later if you're using Visual Studio 2019 version 16.10
	- .NET 5 SDK version 5.0.205 or later if you're using Visual Studio 2019 version 16.9

## Version 0.8 Preview (0.8.0-preview)

This release supports all [experimental channel features](release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/previewdownload)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release introduces critical bug fixes (including those fixed in 0.5 servicing releases) and other changes for WinUI. For a complete list, see the [WinUI 3 - Windows App SDK 0.8 release notes](../winui/winui3/release-notes/release-notes-08-preview.md).

### AppLifecycle (experimental feature)

This release introduces new experimental features related to managing the app lifecycle of your app.

- All apps (packaged and unpackaged) can use **GetActivatedEventArgs** (although packaged apps can already use the implementation of this in the platform).
- Only unpackaged apps can use the **RegisterForXXXActivation** functions.
- Packaged desktop apps can use app lifecycle instancing.
- UWP apps cannot use app lifecycle features in the current release.

For more information, see [App instancing in AppLifecycle](applifecycle/applifecycle-instancing.md) and [Rich activation in AppLifecycle](applifecycle/applifecycle-rich-activation.md).

### Deployment for unpackaged apps (experimental feature)

This release introduces new experimental deployment features for unpackaged apps (that is, apps that do not use MSIX for their deployment technology). Unpackaged apps can now dynamically take a dependency on the Windows App SDK runtime packages so you can continue using your existing MSI or setup program for app deployment. This is available through the following features:

- Standalone installer for Windows App SDK.
- MSIX package bundle that includes dynamic dependencies functionality.

For more more information, see [Deploy unpackaged apps](deploy-unpackaged-apps.md).

### DWriteCore

This release adds the following features:  

- Text decorations (underline and strikethrough in the text layout API)
- Vertical text layout
- Font face kerning API
- Experimental support for unpackaged apps

For more more information, see [Render text with DWriteCore](dwritecore.md).

### MRT Core

This release adds the following features:

- The build action for resources is now automatically set, reducing the need for manual project configuration.
- Experimental support for unpackaged apps.

For more more information, see [Manage resources with MRT Core](mrtcore/mrtcore-overview.md).

### Limitations and known issues

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI project templates](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](get-started.md#use-the-windows-app-sdk-in-an-existing-project) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or higher**: Your TFM must be set to `net5.0-windows10.0.18362` or higher, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or higher. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).

### Samples

The [Windows App SDK samples](https://github.com/microsoft/Project-Reunion-Samples) do not yet work with this release. New and updated samples, including samples that demonstrate new features such as unpackaged app deployment, are coming soon.

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)
