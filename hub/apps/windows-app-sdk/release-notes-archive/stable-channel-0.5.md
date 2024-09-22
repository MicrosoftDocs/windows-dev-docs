---
title: Stable channel release notes for the Windows App SDK 0.5
description: Provides information about the stable release channel for the Windows App SDK 0.5.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Stable channel release notes for the Windows App SDK 0.5

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Downloads for the Windows App SDK

> [!NOTE]
> The Windows App SDK Visual Studio Extensions (VSIX) are no longer distributed as a separate download. They are available in the Visual Studio Marketplace inside Visual Studio.

## Version 0.5

The latest available release of the 0.5.x lineage of the stable channel of the Windows App SDK is version [0.5.9](https://github.com/microsoft/WindowsAppSDK/discussions/1214).

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/vsixdownload)

### New features and updates

This release supports all [stable channel features](../release-channels.md#features-available-by-release-channel).

### Known issues and limitations

This release has the following limitations and known issues:

- **Desktop apps (C# or C++ desktop)**: This release is supported for use only in desktop apps (C++ or C#) that are packaged using MSIX. To use the Windows App SDK in unpackaged desktop apps, you must use the [experimental release channel](../experimental-channel.md).
- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).

[!INCLUDE [UWP migration guidance](./../includes/uwp-app-sdk-migration-pointer.md)]

## Related topics

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
