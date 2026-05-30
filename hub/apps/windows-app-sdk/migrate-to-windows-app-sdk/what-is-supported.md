---
title: What's supported when migrating from UWP to WinUI
description: WinUI and the Windows App SDK are new technologies and, when compared to UWP, there are some features that aren't supported. This topic provides information on which features are supported before you attempt migration.
ms.topic: article
ms.date: 05/28/2026
keywords: Windows, App, SDK, port, porting, migrate, migration, support
ms.localizationpriority: medium
---

# What's supported when migrating from UWP to WinUI 3

WinUI and the Windows App SDK are new technologies and, when compared to UWP, there are some features that aren't supported. This topic provides information on which features are supported before you attempt migration.

| UWP feature | WinUI status |
| - | - |
| [Background acrylic](guides/winui3.md#acrylicbrushbackgroundsource-property) | ✅ Available via [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.desktopacryliccontroller). |
| [Background tasks](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task) | ✅ Supported; see [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) (introduced in 1.7). |
| [ContentDialog](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog) | ✅ Available. Preferred over `MessageDialog` in WinUI 3 (no HWND interop required; set `XamlRoot` instead). |
| [RichEditBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richeditbox) | ✅ Available. |
| [WebView](/uwp/api/windows.ui.xaml.controls.webview) (UWP) | ✅ Use [WebView2](/microsoft-edge/webview2/), which is the WinUI 3 replacement. Requires the [WebView2 Runtime](/microsoft-edge/webview2/concepts/distribution) (pre-installed on most Windows 10/11 devices via Microsoft Edge). |
| DataGrid | ❌ No first-party WinUI 3 control. The [CommunityToolkit DataGrid](https://learn.microsoft.com/windows/communitytoolkit/controls/datagrid) is UWP-only (version 7.1.0) and has not been ported to WinUI 3. Community alternative: [WinUI.TableView](https://github.com/w-ahmad/WinUI.TableView). For simpler tabular data, consider `ListView` with a `GridView` layout. |
| [CameraCaptureUI](https://github.com/microsoft/WindowsAppSDK/pull/4721) | ✅ Supported; see [CameraCaptureUI](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture) (introduced in 1.7). For alternative APIs, see [Using video capture](/windows/win32/multimedia/using-video-capture). |
| [Composition/DirectX interop](https://github.com/microsoft/microsoft-ui-xaml/issues/5025) | ✅ Most Composition and Drawing features are supported; see [Enhance UI with the Visual layer](../../develop/composition/visual-layer.md). |
| Distributing via Store | ✅ Supported |
| Live Tiles (on Windows 10) | ✅ Supported |
| [MapControl](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol) | ✅ Supported; see [MapControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol) (introduced in 1.5). |
| [MediaElement](/uwp/api/windows.ui.xaml.controls.mediaelement) and [MediaPlayerElement](/uwp/api/windows.ui.xaml.controls.mediaplayerelement) | ✅ Use [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), which was introduced in 1.2. |
| MSAL library | ✅ Supported |
| MSIX | ✅ Supported |
| [Single-instancing](guides/applifecycle.md#single-instanced-apps) | ✅ Supported |
| [TaskbarManager](/uwp/api/windows.ui.shell.taskbarmanager) API | ✅ Supported; for details, see [TaskbarManager desktop samples](https://github.com/microsoft/Windows-classic-samples/tree/main/Samples/TaskbarManager). |
| [Toast notifications](guides/toast-notifications.md) | ✅ Supported |
| [Visual Studio App Center](https://appcenter.ms/) | ❌ [Retired on March 31, 2025](https://aka.ms/azmon-migration). Analytics and Diagnostics features continue until March 31, 2027; migrate to [Azure Monitor](/azure/azure-monitor/app/mobile-center-quickstart). |
| [WebAuthenticationBroker](/windows/uwp/security/web-authentication-broker) | ✅ Supported; see [Microsoft.Security.Authentication.OAuth](/windows/windows-app-sdk/api/winrt/microsoft.security.authentication.oauth) (introduced in 1.7). |
| Best launch speed and performance | ⚠️ Slight disadvantage, see [performance considerations](#performance-considerations). |
| CoreTextServicesManager | ⚠️ Supported only on Windows 11 |
| [PrintManager](/uwp/api/windows.graphics.printing.printmanager) | ⚠️ Supported on Windows 11 (not yet available on Windows 10) |
| [CoreWindow](/uwp/api/windows.ui.core.corewindow) and related APIs | ❌ Not supported in 2.0. For alternative APIs with some of the same functionality, see [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow), and [HWND](/windows/apps/develop/ui-input/retrieve-hwnd)-based APIs. |
| [Virtual key support for gamepad input](/uwp/api/windows.system.virtualkey) | ❌ Not supported in 2.0; see [Gamepad support in WinUI](https://github.com/microsoft/microsoft-ui-xaml/issues/6891) |
| [InkCanvas](/uwp/api/windows.ui.xaml.controls.inkcanvas) | ⚠️ Experimental only (introduced in 2.0 Experimental 1). Not available in the stable 2.0 channel. [InkToolbar](/uwp/api/windows.ui.xaml.controls.inktoolbar) is not available. See [Known control gaps](#known-control-gaps). |
| [Single-app kiosk](https://github.com/microsoft/WindowsAppSDK/issues/3642) | ❌ Not supported in 2.0 |
| [Xbox](/windows/uwp/xbox-apps/) and HoloLens | ❌ Not supported in 2.0 |

## Performance considerations

Today in version 2.0 of the Windows App SDK, launch speeds, RAM usage, and installation size of WinUI apps are larger/slower than seen in UWP. We're actively working to improve this.

## Known control gaps

The following UWP controls do not have stable WinUI 3 equivalents as of Windows App SDK 2.0. This section lists the gap and available alternatives.

| UWP control or API | Status | Alternatives |
|---|---|---|
| [InkCanvas](/uwp/api/windows.ui.xaml.controls.inkcanvas) | Experimental only (2.0 Experimental 1); not in stable | [Win2D](https://github.com/Microsoft/Win2D) with pointer input handling; third-party inking libraries |
| [InkToolbar](/uwp/api/windows.ui.xaml.controls.inktoolbar) | Not available | Custom toolbar paired with Win2D inking |
| DataGrid | No first-party control | The [CommunityToolkit DataGrid](https://learn.microsoft.com/windows/communitytoolkit/controls/datagrid) is UWP-only (v7.1.0); [WinUI.TableView](https://github.com/w-ahmad/WinUI.TableView) is a WinUI 3 community alternative |
| [DisplayRequest](/uwp/api/windows.system.display.displayrequest) | Not available | Win32 [`SetThreadExecutionState`](/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate) API |

> [!NOTE]
> Controls marked "Experimental" are available in the Windows App SDK experimental channel but have not graduated to stable. Experimental APIs may change or be removed in future releases. Do not use experimental APIs in production applications.

## Visual Studio

The **Design** tab of the XAML Designer in Visual Studio (and Blend for Visual Studio) doesn't currently support WinUI 3 projects (as of version 2.0 of the Windows App SDK). For more info, see [Create a UI by using XAML Designer](/visualstudio/xaml-tools/creating-a-ui-by-using-xaml-designer-in-visual-studio). For the recommended runtime design workflow, see [XAML runtime design tools for WinUI 3](/windows/apps/develop/ui/xaml-runtime-design-tools).

## See Also

- [Windows App SDK and supported Windows releases](../support.md)
