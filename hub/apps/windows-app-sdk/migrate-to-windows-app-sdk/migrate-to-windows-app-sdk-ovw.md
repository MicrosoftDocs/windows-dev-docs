---
title: Migrate from UWP to the Windows App SDK
description: How to migrate a UWP app to the Windows App SDK and WinUI 3, including strategy, feature mapping, supported features, and step-by-step feature area guides.
ms.date: 07/09/2026
ms.topic: upgrade-and-migration-article
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, overview, hybrid crt, hybrid, crt
ms.localizationpriority: medium
---

# Migrate from UWP to the Windows App SDK

This section of the documentation covers moving a C# or C++/WinRT Universal Windows Platform (UWP) app to the [Windows App SDK](/windows/apps/windows-app-sdk) and [WinUI 3](../../winui/winui3/index.md). The guidance includes migration strategy, feature comparisons, and step-by-step feature area guides.

To migrate your app from UWP to the Windows App SDK, your UI code likely needs just a few namespace changes, while much of your platform code can stay the same. You'll need to adjust some code due to differences between UWP apps and desktop apps. For most apps (depending on codebase size), migration takes on the order of days rather than weeks.

> [!NOTE]
> Your existing UWP app will continue to function as expected. However, to take advantage of modern features in [WinUI 3](../../winui/winui3/index.md) and the [Windows App SDK](/windows/apps/windows-app-sdk) we recommend migrating your app.

> [!TIP]
> You can also use AI tools to accelerate your migration. See [Migrate a UWP app to WinUI 3 with AI assistance](../../develop/ai-assisted/migrate/uwp-to-winui.md) for a quick-reference API substitution table and a starter prompt you can use with GitHub Copilot.

Here are the high-level steps for migrating manually. See also the [.NET Upgrade Assistant tool](#migrating-by-using-the-net-upgrade-assistant-tool) if you're migrating a C# UWP app.

1. Create a new WinUI packaged desktop project (see [Create your first WinUI project](../../get-started/start-here.md)). That could go into your project's existing solution.
2. Copy your XAML/UI code. In many cases you can simply change namespaces (for example, **Windows.UI.\*** to **Microsoft.UI.\***).
3. Copy your app logic code. Some APIs need tweaks, such as **Popup**, **Picker**s, and **SecondaryTile**s.

The topics in the table below describe and demonstrate how to manually migrate your UWP application to the Windows App SDK.

> [!IMPORTANT]
> Before you begin, read [What's supported when migrating from UWP to WinUI](./what-is-supported.md). This topic lists features that aren't yet supported in WinUI 3 and the Windows App SDK. If your app depends on any of those features, consider postponing migration until support is available.

> [!NOTE]
> If your UWP app's source code is written in C++/CX, then also see [Move to C++/WinRT from C++/CX](/windows/uwp/cpp-and-winrt-apis/move-to-winrt-from-cx).

## Migrating by using the .NET Upgrade Assistant tool

As a further step in assisting you to migrate your UWP apps to the Windows App SDK and WinUI, we've leveraged the .NET Upgrade Assistant, adding support for migrating C# UWP apps. The UWP support automates much of the migration process. For more info, see the topic [Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant](upgrade-assistant.md).

## Containerization benefits

When transitioning to the Windows App SDK, UWP applications might lose the inherent containerization benefits of their original platform. However, those benefits can be restored by using Win32 App Isolation&mdash;a new security feature that enhances protection by isolating applications within a sandbox environment. This approach offers additional security against potential threats with minimal changes to your existing code. For more info, and to begin using Win32 App Isolation, see [Win32 app isolation overview](/windows/win32/secauthz/app-isolation-overview).

## Topics in this section

| Topic | Description |
| - | - |
| [Overall migration strategy](overall-migration-strategy.md) | Considerations and strategies for approaching the migration process, and how to set up your development environment for migrating. |
| [Mapping UWP features to the Windows App SDK](feature-mapping-table.md) | This topic compares major feature areas in the different forms in which they appear in UWP and in the Windows App SDK. |
| [What's supported](what-is-supported.md) | Learn what features are currently available in WinUI Desktop to evaluate whether you should attempt migrating your UWP app today. |
| [Mapping UWP APIs and libraries to Windows App SDK](api-mapping-table.md) | This topic provides a mapping of UWP APIs and libraries to their Windows App SDK equivalents. |
| [Feature area guides](guides/feature-area-guides-ovw.md) | A collection of migration guidance topics, each focusing on a specific feature area. |
| [Case study 1—PhotoLab (C#)](case-study-1.md) | This topic is a case study of taking the C# [UWP PhotoLab sample app](/samples/microsoft/windows-appsample-photo-lab/photolab-sample/), and migrating it to the Windows App SDK. |
| [Case study 2—Photo Editor (C++/WinRT)](case-study-2.md) | This topic is a case study of taking the C++/WinRT [UWP Photo Editor sample app](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/), and migrating it to the Windows App SDK. |
| [Additional migration guidance](misc-info.md) | This topic contains additional migration guidance not categorized into a feature area in the [feature area guides](guides/feature-area-guides-ovw.md). |
| [Migrate from UWP to the Windows App SDK with the .NET Upgrade Assistant](upgrade-assistant.md) | The [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) is a command-line tool that can assist with migrating a C# UWP app to a [WinUI](../../winui/index.md) app that uses the Windows App SDK. |
| [Use AI to help modernize your app](./ai-modernize.md) | How to use GitHub Copilot to accelerate modernization of an existing desktop app to Windows App SDK and WinUI 3. |

## See also

- [Choose your migration path](migration-decision-guide.md)
- [Migration terminology](migration-terminology.md)
- [Windows App SDK and supported Windows releases](../support.md)

> [!div class="nextstepaction"]
> [Overall migration strategy](overall-migration-strategy.md)
