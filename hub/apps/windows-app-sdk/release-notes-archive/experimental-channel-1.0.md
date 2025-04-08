---
title: Experimental channel release notes for the Windows App SDK 1.0
description: Learn about the experimental channel release notes for the Windows App SDK 1.0
ms.topic: article
ms.date: 04/19/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK 1.0

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

## Version 1.0 Experimental (1.0.0-experimental1)

This release supports all [experimental channel features](../release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](https://aka.ms/windowsappsdk/experimental-vsix)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release of WinUI 3 is focused on building towards new features for 1.0 stable and fixing bugs.

- **New features**: Support for showing a ContentDialog per window rather than per thread.
- **Bugs**: For the full list of bugs addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-exp-announcement).
- **Samples**: To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 Gallery app [from GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main), or download the app [from the Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC).

For more information or to get started developing with WinUI, see:

- [WinUI](../../winui/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)

### Push notifications (experimental feature)

This release introduces a push notifications API that can be used by packaged desktop apps with Azure app registration-based identities. To use this feature, you must [sign up for our private preview](https://aka.ms/windowsappsdk/push-private-preview).

Important limitations:

- Push notifications are only supported in MSIX packaged apps that are running on Windows 10 version 2004 (build 19041) or later releases.
- Microsoft reserves the right to disable or revoke apps from push notifications during the private preview.
- Microsoft does not guarantee the reliability or latency of push notifications.
- During the private preview, push notification volume is limited to 1 million per month.

For more information, see [Push notifications overview](../notifications/push-notifications/index.md).

### Windowing

This release includes updates to the windowing APIs. These are a set of high-level windowing APIs, centered around the AppWindow class, which allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and other apps. This is similar to, but not the same as, the UWP AppWindow.

Important limitations:

- This release of `AppWindow` is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an `AppWindow`; you're limited to using the `HWND` interop access methods.
- The Windowing API's will currently not work on Windows version 1809 and 1903 for AMD64.

For more information, see [Manage app windows (Windows App SDK)](../windowing/windowing-overview.md).

### Deployment for unpackaged apps

This release introduces updates to the *dynamic dependencies* feature, including the *bootstrapper API*.

Important limitations:

- The dynamic dependencies feature is only supported for unpackaged apps.
- Elevated callers aren't supported.

For more information, see the following articles:

- [MSIX framework packages and dynamic dependencies](../../desktop/modernize/framework-packages/framework-packages-overview.md)
- [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../use-windows-app-sdk-run-time.md)

### Other limitations and known issues

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](../use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).
- **C# apps using 1.0 Experimental must use one of the following .NET SDKs**:
  - .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
