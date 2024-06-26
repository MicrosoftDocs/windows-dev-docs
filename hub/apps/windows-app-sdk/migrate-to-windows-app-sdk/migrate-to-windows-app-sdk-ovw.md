---
title: Migrate from UWP to the Windows App SDK
description: A collection of topics describing and demonstrating how to migrate your Universal Windows Platform (UWP) application to the Windows App SDK.
ms.date: 06/21/2024
ms.topic: article
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, overview, hybrid crt, hybrid, crt
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Migrate from UWP to the Windows App SDK

To migrate your app from the [Universal Windows Platform](/windows/uwp/) (UWP) to the Windows App SDK, your UI code likely needs just a few namespace changes, while much of your platform code can stay the same. You'll need to adjust some code due to differences between UWP apps and desktop apps. But we expect that for most apps (depending on codebase size, of course), migration will take on the order of days rather than weeks.

> [!NOTE]
> If you're happy with the current functionality of the UWP platform and your UWP app, then there's no need to migrate your project. [WinUI 2](../../winui/winui2/index.md) and the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/) continue to support UWP project types.

Below are the steps, at a high level, for migrating manually. But see the following section for info about using the [.NET Upgrade Assistant tool](#migrating-by-using-the-net-upgrade-assistant-tool)).

1. Create a new WinUI 3 packaged desktop project (see [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)). That could go into your project's existing solution.
2. Copy your XAML/UI code. In many cases you can simply change namespaces (for example, **Windows.UI.\*** to **Microsoft.UI.\***).
3. Copy your app logic code. Some APIs need tweaks, such as **Popup**, **Picker**s, and **SecondaryTile**s.

For full details, see the topics in the table below. They describe and demonstrate how to manually migrate your Universal Windows Platform (UWP) application to the Windows App SDK.

Take particular note of [What's supported when migrating from UWP to WinUI 3](./what-is-supported.md), which describes any functionality that's not yet supported in WinUI 3 and the Windows App SDK. If your app needs any of those features/libraries, then you might want to consider postponing migration.

> [!NOTE]
> If your UWP app's source code is written in C++/CX, then also see [Move to C++/WinRT from C++/CX](/windows/uwp/cpp-and-winrt-apis/move-to-winrt-from-cx).

## Migrating by using the .NET Upgrade Assistant tool

As a further step in assisting you to migrate your UWP apps to the Windows App SDK and WinUI 3, we've leveraged the .NET Upgrade Assistant, adding support for migrating C# UWP apps. The UWP support automates much of the migration process. For more info, see the topic [Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant](upgrade-assistant.md).

## Containerization benefits

When transitioning to the Windows App SDK, UWP applications might lose the inherent containerization benefits of their original platform. However, those benefits can be restored by using Win32 App Isolation&mdash;a new security feature that enhances protection by isolating applications within a sandbox environment. This approach offers additional security against potential threats with minimal changes to your existing code. For more info, and to begin using Win32 App Isolation, visit [Welcome to the Win32 app isolation repo](https://github.com/microsoft/win32-app-isolation) on GitHub.

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
| [Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant](upgrade-assistant.md) | The [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) is a command-line tool that can assist with migrating a C# UWP app to a [WinUI 3](../../winui/index.md) app that uses the Windows App SDK. |

## See Also

- [Windows App SDK and suppported Windows releases](../support.md)
