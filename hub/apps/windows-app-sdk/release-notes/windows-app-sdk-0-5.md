---
title: What's new in Windows App SDK 0.5
description: Provides information about what's new in Windows App SDK 0.5.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# What's new in Windows App SDK 0.5

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"


## Version 0.5

<details><summary>New features and updates</summary>

>
> This release supports all [stable channel features](../../windows-app-sdk/release-channels.md#features-available-by-release-channel).
>

</details>

<details><summary>Known issues and limitations</summary>

>
> ### Known issues and limitations
>
> This release has the following limitations and known issues:
>
> - **Desktop apps (C# or C++ desktop)**: This release is supported for use only in desktop apps (C++ or C#) that are packaged using MSIX. To use the Windows App SDK in unpackaged desktop apps, you must use the experimental release channel.
> - **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).

</details>

:::zone-end

:::zone pivot="preview"

**There are no preview releases for this version.**

:::zone-end

:::zone pivot="experimental"

**There are no experimental releases for this version.**
:::zone-end