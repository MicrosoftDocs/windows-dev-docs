---
title: What's supported when migrating from UWP to WinUI 3
description: WinUI 3 and the Windows App SDK are new technologies and, when compared to UWP, there are some features that aren't supported. This topic provides information on which features are supported before you attempt migration.
ms.topic: article
ms.date: 03/27/2023
keywords: Windows, App, SDK, port, porting, migrate, migration, support
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# What's supported when migrating from UWP to WinUI 3

WinUI 3 and the Windows App SDK are new technologies and, when compared to UWP, there are some features that aren't supported. This topic provides information on which features are supported before you attempt migration.

| UWP feature | WinUI 3 status |
| - | - |
| [Background acrylic](guides/winui3.md#acrylicbrushbackgroundsource-property) | ✅ Available via [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller) |
| Common UI controls | ✅ Supported |
| Distributing via Store | ✅ Supported |
| Live Tiles (on Windows 10) | ✅ Supported |
| [MediaElement](/uwp/api/windows.ui.xaml.controls.mediaelement) and [MediaPlayerElement](/uwp/api/windows.ui.xaml.controls.mediaplayerelement) | ✅ Use [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), which was introduced in 1.2 |
| MSAL library | ✅ Supported |
| MSIX | ✅ Supported |
| [Single-instancing](guides/applifecycle.md#single-instanced-apps) | ✅ Supported |
| [Toast notifications](guides/toast-notifications.md) | ✅ Supported |
| [Visual Studio App Center](https://appcenter.ms/) | ✅ Supported |
| [Background tasks](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task) | ✅ Supported for C++ <br> ⚠️ Partially supported for C# (OOP background tasks supported)|
| Best launch speed and performance | ⚠️ Slight disadvantage, see [performance considerations](#performance-considerations) |
| CoreTextServicesManager | ⚠️ Supported only on Windows 11 |
| [PrintManager](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/50-support-printmanager-api) | ⚠️ Supported on Windows 11 (not yet available on Windows 10) |
| [CameraCaptureUI](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/49-support-cameracaptureui) | ❌ Not supported in 1.3. For alternative APIs, see [Using video capture](/windows/win32/multimedia/using-video-capture). |
| Full containerization of your app | ❌ Not supported in 1.3 |
| [InkCanvas](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/31-inking-controls) | ❌ Not supported in 1.3 |
| [MapControl](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/27-map-control) | ❌ Not supported in 1.3 |
| [Single-app kiosk](https://portal.productboard.com/winappsdk/1-windows-app-sdk/c/62-support-single-app-kiosk) | ❌ Not supported in 1.3 |
| [TaskbarManager](/uwp/api/windows.ui.shell.taskbarmanager) API | ❌ Not supported in 1.3 |
| WebAuthenticationBroker | ❌ Not supported in 1.3 |
| [Visual Designer](https://learn.microsoft.com/en-us/visualstudio/xaml-tools/creating-a-ui-by-using-xaml-designer-in-visual-studio?view=vs-2022) | ❌ Not supported in 1.3 |

## Performance considerations

Today in version 1.3 of the Windows App SDK, launch speeds, RAM usage, and installation size of WinUI 3 apps are larger/slower than seen in UWP. We're actively working to improve this.

## Development tools

Today in version 1.3 of the Windows App SDK, Visual Designer does not implement yet
