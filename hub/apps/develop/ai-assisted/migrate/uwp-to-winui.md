---
title: Migrate a UWP app to WinUI 3
description: Port a UWP app to WinUI 3 and the Windows App SDK using AI assistance — with a complete API substitution table covering namespaces, controls, threading, windowing, notifications, and more.
ms.topic: how-to
ms.date: 05/14/2026
ms.author: jken
author: GrantMeStrength
---

# Migrate a UWP app to WinUI 3

UWP is no longer under active development. WinUI 3 and the Windows App SDK are its successors — and AI tools can automate most of the migration. The main challenge is that AI models were trained on years of UWP samples, so without guidance they reproduce the patterns you're trying to move away from. This page gives your agent the context it needs to get it right.

## Install the WinUI agent plugin

The `winui-uwp-migration` skill handles the common substitutions automatically:

```powershell
gh copilot plugin install winui@awesome-copilot
```

See the [WinUI agent plugin](../winui-agent-plugin.md) for full details.

## API substitution table

The following tables summarize the most common API substitutions. For the full detailed mapping — including members, properties, and less common APIs — see [Mapping UWP APIs and libraries to the Windows App SDK](../../../windows-app-sdk/migrate-to-windows-app-sdk/api-mapping-table.md).

### Namespaces

| UWP | WinUI 3 |
|-----|---------|
| `Windows.UI.Xaml.*` | `Microsoft.UI.Xaml.*` |
| `Windows.UI.Xaml.Controls.*` | `Microsoft.UI.Xaml.Controls.*` |
| `Windows.UI.Xaml.Media.*` | `Microsoft.UI.Xaml.Media.*` |
| `Windows.UI.Composition` | `Microsoft.UI.Composition` |

### Threading

| UWP | WinUI 3 |
|-----|---------|
| `CoreDispatcher` | `DispatcherQueue` |
| `Dispatcher.RunAsync(...)` | `DispatcherQueue.TryEnqueue(...)` |
| `CoreApplication.MainView.CoreWindow.Dispatcher` | `this.DispatcherQueue` (from a `Window` or `Page`) |

### Windowing

| UWP | WinUI 3 |
|-----|---------|
| `ApplicationView` | `AppWindow` |
| `ApplicationView.GetForCurrentView()` | `AppWindow.GetFromWindowId(...)` |
| `ApplicationViewTitleBar` | `AppWindowTitleBar` |
| `CoreWindow` | `Microsoft.UI.Xaml.Window` |
| `SystemNavigationManager` | Back button via `AppWindowTitleBar` |

### Dialogs and pickers

| UWP | WinUI 3 |
|-----|---------|
| `MessageDialog` | `ContentDialog` (set `XamlRoot`) |
| `FileOpenPicker` | `FileOpenPicker` + `InitializeWithWindow` |
| `FileSavePicker` | `FileSavePicker` + `InitializeWithWindow` |
| `FolderPicker` | `FolderPicker` + `InitializeWithWindow` |

> [!IMPORTANT]
> **Pickers** require `InitializeWithWindow` before calling `PickSingleFileAsync` (or similar):
>
> ```csharp
> var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
> WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
> ```
>
> **ContentDialog** requires `XamlRoot` (not `InitializeWithWindow`):
>
> ```csharp
> var dialog = new ContentDialog { XamlRoot = this.Content.XamlRoot, ... };
> await dialog.ShowAsync();
> ```

### Notifications

| UWP | WinUI 3 |
|-----|---------|
| `Windows.UI.Notifications.ToastNotificationManager` | `Microsoft.Windows.AppNotifications.AppNotificationManager` |
| `Windows.UI.Notifications.BadgeUpdateManager` | `Microsoft.Windows.BadgeNotifications.BadgeNotificationManager` |
| `Windows.UI.Notifications.TileUpdateManager` | Tiles are deprecated — use notifications or widgets |

### Navigation

| UWP | WinUI 3 |
|-----|---------|
| `Frame.Navigate(typeof(MyPage))` | `Frame.Navigate(typeof(MyPage))` — unchanged |
| `SystemNavigationManager.BackRequested` | Handle via `NavigationView` or `AppWindow` |
| `Windows.UI.Core.Preview.SystemNavigationManagerPreview` | `AppWindow.Closing` event |

### App lifecycle

| UWP | WinUI 3 |
|-----|---------|
| `Application.Current.Suspending` | `Microsoft.Windows.AppLifecycle` (requires architectural changes — see note) |
| `Application.Current.Resuming` | `AppInstance.GetCurrent().Activated` (see note) |
| `BackgroundTaskBuilder` | [Windows App SDK background tasks](../../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) |

> [!NOTE]
> WinUI 3 app lifecycle migration is not a simple API name swap. The Windows App SDK uses a different activation and suspension model. Treat lifecycle code as requiring a dedicated rewrite rather than automated substitution. See the [Windows App SDK lifecycle documentation](../../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) for the full model.

### Settings and storage

| UWP | WinUI 3 |
|-----|---------|
| `ApplicationData.Current.LocalSettings` | Unchanged |
| `ApplicationData.Current.LocalFolder` | Unchanged |
| `Windows.Storage.KnownFolders` | Unchanged |

### APIs that don't change

`Windows.Devices.*`, `Windows.Media.*`, `Windows.UI.ViewManagement.UISettings`, `Windows.UI.Color`, and most WinRT APIs outside the XAML namespace are unchanged.

## Starter prompt

```
I'm migrating a UWP app to WinUI 3 using the Windows App SDK.

Apply these substitutions:
- Windows.UI.Xaml.* → Microsoft.UI.Xaml.*
- CoreDispatcher / Dispatcher.RunAsync → DispatcherQueue.TryEnqueue
- ApplicationView → AppWindow + AppWindowTitleBar
- CoreWindow → Microsoft.UI.Xaml.Window
- MessageDialog → ContentDialog (set XamlRoot, not InitializeWithWindow)
- FileOpenPicker / FileSavePicker / FolderPicker → add InitializeWithWindow
- Windows.UI.Notifications → Microsoft.Windows.AppNotifications
- SystemNavigationManager.BackRequested → NavigationView back handling

Do not use any Windows.UI.Xaml.* namespaces in new code.
Do not use CoreDispatcher — use DispatcherQueue.
Flag any APIs without a direct WinUI 3 equivalent rather than guessing.
```

## Project file changes

Replace the UWP target framework:

```xml
<!-- Before (UWP) -->
<TargetPlatformVersion>10.0.19041.0</TargetPlatformVersion>
<TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>

<!-- After (WinUI 3) -->
<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
<WindowsSdkPackageVersion>10.0.19041.31</WindowsSdkPackageVersion>
```

Add the Windows App SDK package:

```powershell
dotnet add package Microsoft.WindowsAppSDK
```

## Related content

- [Windows App SDK migration guide](../../../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md) — full manual migration walkthrough
- [Mapping UWP APIs and libraries to the Windows App SDK](../../../windows-app-sdk/migrate-to-windows-app-sdk/api-mapping-table.md) — comprehensive API mapping table
- [What's supported when migrating from UWP to WinUI](../../../windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported.md) — feature support status
- [Migrate from WPF](wpf-to-winui.md)
