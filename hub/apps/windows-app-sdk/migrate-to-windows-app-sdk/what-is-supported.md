---
title: What's supported when migrating from UWP to WinUI
description: WinUI and the Windows App SDK are new technologies and, when compared to UWP, there are some features that aren't supported. This topic provides information on which features are supported before you attempt migration.
ms.topic: article
ms.date: 07/14/2025
keywords: Windows, App, SDK, port, porting, migrate, migration, support
ms.localizationpriority: medium
---

# What's supported when migrating from UWP to WinUI 3

WinUI and the Windows App SDK are new technologies and, when compared to UWP, there are some features that aren't supported. This topic provides information on which features are supported before you attempt migration.

| UWP feature | WinUI status |
| - | - |
| [Background acrylic](guides/winui3.md#acrylicbrushbackgroundsource-property) | ✅ Available via [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller). |
| [Background tasks](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task) | ✅ Supported; see [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) (introduced in 1.7). |
| Common UI controls | ✅ Supported |
| [CameraCaptureUI](https://github.com/microsoft/WindowsAppSDK/pull/4721) | ✅ Supported; see [CameraCaptureUI](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture) (introduced in 1.7). For alternative APIs, see [Using video capture](/windows/win32/multimedia/using-video-capture). |
| [Composition/DirectX interop](https://github.com/microsoft/microsoft-ui-xaml/issues/5025) | ✅ Most Composition and Drawing features are supported (global composition effects aren't supported in 1.7); see [Enhance UI with the Visual layer](../../develop/composition/visual-layer.md). |
| Distributing via Store | ✅ Supported |
| Live Tiles (on Windows 10) | ✅ Supported |
| [MapControl](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol) | ✅ Supported; see [MapControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol) (introduced in 1.5). |
| [MediaElement](/uwp/api/windows.ui.xaml.controls.mediaelement) and [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) | ✅ Use [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), which was introduced in 1.2. |
| MSAL library | ✅ Supported |
| MSIX | ✅ Supported |
| [Single-instancing](guides/applifecycle.md#single-instanced-apps) | ✅ Supported |
| [TaskbarManager](/uwp/api/windows.ui.shell.taskbarmanager) API | ✅ Supported; for details, see [TaskbarManager desktop samples](https://github.com/microsoft/Windows-classic-samples/tree/main/Samples/TaskbarManager). |
| [Toast notifications](guides/toast-notifications.md) | ✅ Supported |
| [Visual Studio App Center](https://appcenter.ms/) | ✅ Supported |
| [WebAuthenticationBroker](/windows/uwp/security/web-authentication-broker) | ✅ Supported; see [Microsoft.Security.Authentication.OAuth](/windows/windows-app-sdk/api/winrt/microsoft.security.authentication.oauth) (introduced in 1.7). |
| Best launch speed and performance | ⚠️ Slight disadvantage, see [performance considerations](#performance-considerations). |
| CoreTextServicesManager | ⚠️ Supported only on Windows 11 |
| [PrintManager](/uwp/api/windows.graphics.printing.printmanager) | ⚠️ Supported on Windows 11 (not yet available on Windows 10) |
| [CoreWindow](/uwp/api/windows.ui.core.corewindow) and related APIs | ❌ Not supported in 1.7. For alternative APIs with some of the same functionality, see [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow), and [HWND](/windows/apps/develop/ui-input/retrieve-hwnd)-based APIs. |
| [Virtual key support for gamepad input](/uwp/api/windows.system.virtualkey) | ❌ Not supported in 1.7; see [Gamepad support in WinUI](https://github.com/microsoft/microsoft-ui-xaml/issues/6891) |
| [InkCanvas](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkcanvas) | ❌ Not supported in 1.7 |
| [Single-app kiosk](https://github.com/microsoft/WindowsAppSDK/issues/3642) | ❌ Not supported in 1.7 |
| [Xbox](/windows/uwp/xbox-apps/) and HoloLens | ❌ Not supported in 1.7 |

## Performance considerations

Today in version 1.7 of the Windows App SDK, launch speeds, RAM usage, and installation size of WinUI apps are larger/slower than seen in UWP. We're actively working to improve this.

## Visual Studio

The **Design** tab of the XAML Designer in Visual Studio (and Blend for Visual Studio) doesn't currently support WinUI projects (as of version 1.7 of the Windows App SDK). For more info, see [Create a UI by using XAML Designer](/visualstudio/xaml-tools/creating-a-ui-by-using-xaml-designer-in-visual-studio).

## See Also

- [Windows App SDK and supported Windows releases](../support.md)
