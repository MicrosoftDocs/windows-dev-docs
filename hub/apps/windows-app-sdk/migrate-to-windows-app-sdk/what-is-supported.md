---
title: What is supported when migrating from UWP to WinUI 3
description: Learn what features are currently available in WinUI 3 Desktop to evaluate whether you should attempt migrating your UWP app today.
ms.topic: article
ms.date: 10/01/2021
keywords: Windows, App, SDK, port, porting, migrate, migration, support
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# What is supported when migrating from UWP to WinUI 3

WinUI 3 and the Windows App SDK are new technologies and, when compared to UWP, there are some features that still aren't supported. This topic provides information to ensure that you know whether all the features you need for your are supported before you attempt migration.

| UWP feature | WinUI 3 status |
| - | - |
| Common UI controls | ✅ Supported |
| MSIX | ✅ Supported |
| Toast notifications | ✅ Supported |
| Live Tiles (on Windows 10) | ✅ Supported |
| Distributing via Store | ✅ Supported |
| MSAL library | ✅ Supported |
| [Visual Studio App Center](https://appcenter.ms/) | ✅ Supported |
| [Single-instancing](guides/applifecycle.md#single-instanced-apps) | ✅ Supported |
| Background tasks | ⚠️ Not documented for C# apps |
| [CameraCaptureUI](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/49-support-cameracaptureui) | ❌ Not supported in 1.0 |
| CoreTextServicesManager | ⚠️ Supported only on Windows 11 |
| [InkCanvas](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/31-inking-controls) | ❌ Not supported in 1.0 |
| [MapControl](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/27-map-control) | ❌ Not supported in 1.0 |
| [MediaElement](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/30-media-controls) | ❌ Not supported in 1.0 |
| [PrintManager](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/50-support-printmanager-api) | ❌ Not supported in 1.0 |
| WebAuthenticationBroker | ❌ Not supported in 1.0 |
| [Background acrylic](guides/winui3.md#acrylicbrushbackgroundsource-property) | ❌ Not supported in 1.0 |
| DisplayRequest API | ❌ Not supported in 1.0 |
| [Single-app kiosk](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/62-support-single-app-kiosk) | ❌ Not supported in 1.0 |
| [TaskbarManager](/uwp/api/windows.ui.shell.taskbarmanager) API | ❌ Not supported in 1.0 |
| Full containerization of your app | ❌ Not supported in 1.0 |
| Best launch speed and performance | ⚠️ Slight disadvantage, see [performance considerations](#performance-considerations) |

## Performance considerations

Today in version 1.0 of Windows App SDK, launch speeds, RAM usage, and installation size of WinUI 3 apps are larger/slower than seen in UWP. We are actively working to improve this. We will have more information to share in the future.
