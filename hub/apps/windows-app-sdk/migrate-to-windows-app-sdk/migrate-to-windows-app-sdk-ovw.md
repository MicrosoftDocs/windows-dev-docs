---
title: Migrate from UWP to the Windows App SDK
description: Plan a UWP migration to the Windows App SDK and WinUI 3 by assessing support, mapping features and APIs, and using feature-area guidance.
ms.date: 10/13/2026
ms.topic: upgrade-and-migration-article
author: GrantMeStrength
ms.author: jken
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, overview, hybrid crt, hybrid, crt
ms.localizationpriority: medium
---

# Migrate from UWP to the Windows App SDK

This section covers moving a C# or C++/WinRT Universal Windows Platform (UWP) app to the [Windows App SDK](/windows/apps/windows-app-sdk) and [WinUI 3](../../winui/winui3/index.md). The guidance includes migration strategy, feature comparisons, and step-by-step feature-area guides.

The amount of work depends on the app's UI, app-model dependencies, packaging, background activity, windowing, and use of UWP-only features. Some XAML and platform code can transfer with namespace or API changes, but other areas require redesign or have no direct WinUI 3 equivalent. Assess support and test representative features before you estimate the migration.

> [!NOTE]
> Your existing UWP app will continue to function as expected. However, to take advantage of modern features in [WinUI 3](../../winui/winui3/index.md) and the [Windows App SDK](/windows/apps/windows-app-sdk) we recommend migrating your app.

Here are the high-level steps for migrating manually:

1. Create a new WinUI packaged desktop project (see [Create and run a WinUI 3 project](../../get-started/start-here.md)). That could go into your project's existing solution.
2. Move a representative UI workflow and update its XAML namespaces, controls, resources, and app-model assumptions.
3. Move the related app logic and replace or adapt APIs that behave differently in a desktop app, such as popups, pickers, windowing, and secondary tiles.

The topics in the table below describe and demonstrate how to manually migrate your UWP application to the Windows App SDK.

> [!IMPORTANT]
> Before you begin, read [What's supported when migrating from UWP to WinUI](./what-is-supported.md). This topic lists features that aren't yet supported in WinUI 3 and the Windows App SDK. If your app depends on any of those features, consider postponing migration until support is available.

> [!NOTE]
> If your UWP app's source code is written in C++/CX, then also see [Move to C++/WinRT from C++/CX](/windows/uwp/cpp-and-winrt-apis/move-to-winrt-from-cx).

## Legacy .NET Upgrade Assistant guidance

> [!IMPORTANT]
> .NET Upgrade Assistant is deprecated. Don't use it to start a new migration. GitHub Copilot upgrade doesn't currently document UWP-to-WinUI 3 as a supported upgrade path.

If you're maintaining a C# UWP migration that already uses .NET Upgrade Assistant, see the [legacy .NET Upgrade Assistant guidance](upgrade-assistant.md). For a new migration, use the manual guidance on this page. An AI assistant can optionally support the bounded review tasks described below.

## Use an AI assistant as an optional review aid

An AI coding assistant can help you inventory namespace usage, locate APIs that need desktop interop, draft a mapping for a bounded feature, or propose edits for one migration step. Treat those results as suggestions, not verified substitutions.

Review every proposed API and project change against the feature mapping, API mapping, and current API reference. Build and test each bounded change, including packaging, activation, window ownership, threading, accessibility, and failure paths. An assistant can't determine whether a missing WinUI 3 feature is acceptable for your product or guarantee that generated code preserves behavior.

## Containerization benefits

When transitioning to the Windows App SDK, UWP applications might lose the inherent containerization benefits of their original platform. However, those benefits can be restored by using Win32 App Isolation&mdash;a new security feature that enhances protection by isolating applications within a sandbox environment. This approach offers additional security against potential threats with minimal changes to your existing code. For more info, and to begin using Win32 App Isolation, see [Win32 app isolation overview](/windows/win32/secauthz/app-isolation-overview).

## Topics in this section

| Topic | Description |
| - | - |
| [UWP migration strategy](overall-migration-strategy.md) | Considerations and strategies for approaching the migration process, and how to set up your development environment for migrating. |
| [Mapping UWP features to the Windows App SDK](feature-mapping-table.md) | This topic compares major feature areas in the different forms in which they appear in UWP and in the Windows App SDK. |
| [What's supported](what-is-supported.md) | Learn what features are currently available in WinUI Desktop to evaluate whether you should attempt migrating your UWP app today. |
| [Mapping UWP APIs and libraries to Windows App SDK](api-mapping-table.md) | This topic provides a mapping of UWP APIs and libraries to their Windows App SDK equivalents. |
| [Feature area guides](guides/feature-area-guides-ovw.md) | A collection of migration guidance topics, each focusing on a specific feature area. |
| [Case study 1—PhotoLab (C#)](case-study-1.md) | This topic is a case study of taking the C# [UWP PhotoLab sample app](https://github.com/microsoft/Windows-appsample-photo-lab), and migrating it to the Windows App SDK. |
| [Case study 2—Photo Editor (C++/WinRT)](case-study-2.md) | This topic is a case study of taking the C++/WinRT [UWP Photo Editor sample app](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/), and migrating it to the Windows App SDK. |
| [Additional migration guidance](misc-info.md) | This topic contains additional migration guidance not categorized into a feature area in the [feature area guides](guides/feature-area-guides-ovw.md). |
| [Legacy .NET Upgrade Assistant guidance for UWP](upgrade-assistant.md) | Reference information for maintaining an existing C# UWP migration that used the deprecated .NET Upgrade Assistant. |

## See also

- [Choose your migration path](migration-decision-guide.md)
- [Migration terminology](migration-terminology.md)
- [Windows App SDK and supported Windows releases](../support.md)

> [!div class="nextstepaction"]
> [UWP migration strategy](overall-migration-strategy.md)
