---
title: About migrating from UWP to the Windows App SDK
description: High-level steps for migrating your app from UWP to the Windows App SDK
ms.topic: article
ms.date: 02/10/2022
keywords: windows win32, desktop development
ms.localizationpriority: medium
---

# About migrating from UWP to the Windows App SDK

If you're happy with your current functionality in the Universal Windows Platform (UWP), then there's no need to migrate your project type. WinUI 2.x, and the Windows SDK, support UWP project types.

But if you've decided to migrate your app from UWP to the Windows App SDK, then in most cases your UI code needs just a few namespace changes. Much of your platform code can stay the same. You'll need to adjust some code due to differences between desktop apps and UWP apps. But we expect that for most apps (depending on codebase size, of course), migration will take on the order of days rather than weeks. At a high level, these are the steps:

1. [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md?pivots=winui3-packaged-csharp) packaged desktop project. That could go into your existing solution.
2. Copy your XAML/UI code. In many cases you can simply change namespaces (for example, **Windows.UI.\*** to **Microsoft.UI.\***).
3. Copy your app logic code. Some APIs need tweaks, such as **Popup**, **Picker**s, and **SecondaryTile**s.

For full details, see [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md). That migration documentation covers how to migrate across any differences.

Within that content, take particular note of [What's supported when migrating from UWP to WinUI 3](../windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported.md). That topic describes any functionality that's not yet supported in WinUI 3 and the Windows App SDK. If your app needs any of those features/libraries, then consider waiting to migrate.

## Related topics

* [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md)
* [What's supported when migrating from UWP to WinUI 3](../windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported.md)
* [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md?pivots=winui3-packaged-csharp)