---
title: Overview of feature area guides
description: A collection of migration guidance topics, each focusing on a specific feature area.
ms.topic: concept-article
ms.date: 05/28/2026
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting
ms.localizationpriority: medium
---

# Overview of feature area guides

A collection of migration guidance topics, each focusing on a specific feature area.

## Feature area guides in this section

| Topic | Description |
| - | - |
| [User interface migration (including WinUI)](winui3.md) | This topic shows how to migrate your user interface (UI) code, including migrating to [WinUI](../../../winui/index.md). |
| [Windowing functionality migration](windowing.md) | This topic contains guidance related to window management, including migrating from UWP's [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**CoreWindow**](/uwp/api/windows.ui.core.corewindow) or [**AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow) to the Window App SDK [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow). |
| [Application lifecycle functionality migration](applifecycle.md) | This topic contains migration guidance in the application lifecycle feature area. |
| [Toast notifications functionality migration](toast-notifications.md) | This topic contains migration guidance in the toast notifications feature area. |
| [Push notifications functionality migration](notifications.md) | This topic contains migration guidance in the push notifications feature area. |
| [DirectWrite to DWriteCore migration](dwritecore.md) | DWriteCore is the [Windows App SDK](../../index.md) implementation of [DirectWrite](/windows/win32/directwrite/direct-write-portal). |
| [MRT to MRT Core migration](mrtcore.md) | This topic contains guidance for migrating from UWP's [Resource Management System](/windows/uwp/app-resources/resource-management-system) (also known as MRT) to the Windows App SDK's MRT Core. |
| [Threading functionality migration](threading.md) | This topic shows how to migrate your threading code. |
| [Background task migration strategy](background-task-migration-strategy.md) | This topic contains considerations and strategies for approaching the migration process, and how to migrate your UWP background tasks to use the Windows App SDK background task APIs. |

## See Also

* [Windows App SDK and supported Windows releases](../../support.md)
