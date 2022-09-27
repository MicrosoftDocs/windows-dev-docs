---
title: Migrate from UWP to the Windows App SDK
description: A collection of topics describing and demonstrating how to migrate your Universal Windows Platform (UWP) application to the Windows App SDK.
ms.topic: article
ms.date: 10/08/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, overview, hybrid crt, hybrid, crt
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Migrate from UWP to the Windows App SDK

If you're happy with your current functionality in the Universal Windows Platform (UWP), then there's no need to migrate your project type. WinUI 2.x, and the Windows SDK, support UWP project types.

But if you've decided to migrate your app from UWP to the Windows App SDK, then in most cases your UI code needs just a few namespace changes. Much of your platform code can stay the same. You'll need to adjust some code due to differences between desktop apps and UWP apps. But we expect that for most apps (depending on codebase size, of course), migration will take on the order of days rather than weeks. At a high level, these are the steps:

1. Create a WinUI 3 packaged desktop project (see [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)). That could go into your existing solution.
2. Copy your XAML/UI code. In many cases you can simply change namespaces (for example, **Windows.UI.\*** to **Microsoft.UI.\***).
3. Copy your app logic code. Some APIs need tweaks, such as **Popup**, **Picker**s, and **SecondaryTile**s.

For full details, see the topics below. They describe and demonstrate how to migrate your Universal Windows Platform (UWP) application to the Windows App SDK.

Take particular note of [What's supported when migrating from UWP to WinUI 3](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported). That topic describes any functionality that's not yet supported in WinUI 3 and the Windows App SDK. If your app needs any of those features/libraries, then consider waiting to migrate.

## Migrating by using the .NET Upgrade Assistant tool

As a next step in assisting you to migrate your Universal Windows Platform (UWP) apps to the Windows App SDK and WinUI 3, we've leveraged the .NET Upgrade Assistant, adding support for migrating C# UWP apps. The UWP support automates much of the migration process. A preview is now available&mdash;for more info, see [Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant](upgrade-assistant.md).

Our roadmap for UWP support in the .NET Upgrade Assistant includes further tooling improvements, and adding migration support for new features.

The [Upgrade Assistant GitHub repository](https://github.com/dotnet/upgrade-assistant) documents troubleshooting tips and known issues. If you find any issues while using the tool, please report them in that same GitHub repository, tagging them with an area tag of `UWP`. We appreciate it!

## Topics in this section

| Topic | Description |
| - | - |
| [Overall migration strategy](overall-migration-strategy.md) | Considerations and strategies for approaching the migration process, and how to set up your development environment for migrating. |
| [Mapping UWP features to the Windows App SDK](feature-mapping-table.md) | This topic compares major feature areas in the different forms in which they appear in UWP and in the Windows App SDK. |
| [What's supported](what-is-supported.md) | Learn what features are currently available in WinUI 3 Desktop to evaluate whether you should attempt migrating your UWP app today. |
| [Mapping UWP APIs and libraries to Windows App SDK](api-mapping-table.md) | This topic provides a mapping of UWP APIs and libraries to their Windows App SDK equivalents. |
| [Feature area guides](guides/feature-area-guides-ovw.md) | A collection of migration guidance topics, each focusing on a specific feature area. |
| [Case study 1—PhotoLab (C#)](case-study-1.md) | This topic is a case study of taking the C# [UWP PhotoLab sample app](/samples/microsoft/windows-appsample-photo-lab/photolab-sample/), and migrating it to the Windows App SDK. |
| [Case study 2—Photo Editor (C++/WinRT)](case-study-2.md) | This topic is a case study of taking the C++/WinRT [UWP Photo Editor sample app](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/), and migrating it to the Windows App SDK. |
| [Additional migration guidance](misc-info.md) | This topic contains additional migration guidance not categorized into a feature area in the [feature area guides](guides/feature-area-guides-ovw.md). |
| [Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant](upgrade-assistant.md) | The [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) is a command-line tool that can assist with migrating a C# UWP app to a [Windows UI Library (WinUI) 3](/windows/apps/winui/) app that uses the Windows App SDK. |
