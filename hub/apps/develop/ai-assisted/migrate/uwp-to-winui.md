---
title: Migrate a UWP app to WinUI 3
description: Port a UWP app to WinUI 3 using AI assistance, with an API substitution table for namespaces, controls, threading, windowing, and notifications.
ms.topic: how-to
ms.date: 07/12/2026
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

> [!IMPORTANT]
> **`x:Bind` defaults to `OneTime` mode.** Unlike `{Binding}` (which defaults to `OneWay`), `x:Bind` only evaluates once unless you specify `Mode=OneWay` or `Mode=TwoWay`. During migration, audit all `x:Bind` expressions that bind to properties that change at runtime — missing `Mode=OneWay` causes "UI doesn't update" bugs that are invisible at compile time.

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

### Networking and HTTP

| UWP | WinUI 3 (recommended) |
|-----|------------------------|
| `Windows.Web.Http.HttpClient` | `System.Net.Http.HttpClient` (portable, no WinRT dependency) |
| `Windows.Web.Syndication.SyndicationClient` | `System.ServiceModel.Syndication.SyndicationFeed` + `HttpClient` |
| `Windows.Web.AtomPub.AtomPubClient` | `System.ServiceModel.Syndication` or direct HTTP |

> [!NOTE]
> The WinRT HTTP APIs (`Windows.Web.Http`) still work in packaged WinUI 3 apps, but the .NET equivalents are recommended for portability, simpler debugging, and broader ecosystem support (middleware, DI, mocking).

### Navigation

| UWP | WinUI 3 |
|-----|---------|
| `Frame.Navigate(typeof(MyPage))` | `Frame.Navigate(typeof(MyPage))` — unchanged |
| `SystemNavigationManager.BackRequested` | Handle via `NavigationView` or `AppWindow` |
| `Windows.UI.Core.Preview.SystemNavigationManagerPreview` | `AppWindow.Closing` event |

> [!NOTE]
> **Custom hamburger navigation (SplitView + NavMenuListView):** Many UWP samples implemented navigation using a custom `AppShell.xaml` with `SplitView` and a hand-rolled `NavMenuListView` control (~500+ lines). In WinUI 3, replace this entire pattern with [NavigationView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.navigationview), which provides the same UX with built-in accessibility, responsive behavior, and back-button support. This is typically an 80% code reduction.

### MVVM patterns

| UWP (common custom implementations) | WinUI 3 (recommended) |
|--------------------------------------|------------------------|
| Custom `BindableBase` / `ObservableObject` | `CommunityToolkit.Mvvm.ComponentModel.ObservableObject` |
| Custom `DelegateCommand` / `RelayCommand` | `CommunityToolkit.Mvvm.Input.RelayCommand` |
| Manual `SetProperty` + `OnPropertyChanged` | `[ObservableProperty]` source generator |
| Custom `INavigationService` | Built-in `Frame.Navigate` + `NavigationView` |

> [!TIP]
> The [CommunityToolkit.Mvvm](/dotnet/communitytoolkit/mvvm/) NuGet package is the recommended MVVM foundation for WinUI 3 apps. It replaces hand-rolled base classes with tested, source-generated equivalents — eliminating hundreds of lines of boilerplate.
>
> ```powershell
> dotnet add package CommunityToolkit.Mvvm
> ```

### App lifecycle

| UWP | WinUI 3 |
|-----|---------|
| `Application.Current.Suspending` | `Microsoft.Windows.AppLifecycle` (requires architectural changes — see note) |
| `Application.Current.Resuming` | `AppInstance.GetCurrent().Activated` (see note) |
| `BackgroundTaskBuilder` | [Windows App SDK background tasks](../../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) |

> [!NOTE]
> WinUI 3 app lifecycle migration is not a simple API name swap. The Windows App SDK uses a different activation and suspension model. Treat lifecycle code as requiring a dedicated rewrite rather than automated substitution. See the [Windows App SDK lifecycle documentation](../../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) for the full model.

### Settings and storage

| UWP | WinUI 3 (packaged) | WinUI 3 (unpackaged) |
|-----|---------------------|----------------------|
| `ApplicationData.Current.LocalSettings` | Unchanged | ❌ Throws — no package identity |
| `ApplicationData.Current.LocalFolder` | Unchanged | ❌ Throws — no package identity |
| `Windows.Storage.KnownFolders` | Unchanged | ❌ Throws — no package identity |

> [!WARNING]
> **Unpackaged apps** cannot use `ApplicationData.Current` — it throws at runtime because there is no package identity. Use standard .NET file APIs instead:
>
> ```csharp
> var appData = Path.Combine(
>     Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
>     "YourAppName");
> Directory.CreateDirectory(appData);
> var json = JsonSerializer.Serialize(data);
> await File.WriteAllTextAsync(Path.Combine(appData, "settings.json"), json);
> ```

> [!NOTE]
> If your UWP app used `DataContractSerializer` with `[DataMember]`/`[IgnoreDataMember]`, consider migrating to `System.Text.Json` (faster, smaller, source-gen support). The attribute mapping is:
> - `[DataMember]` → `[JsonPropertyName("name")]` (or just use property names directly)
> - `[IgnoreDataMember]` → `[JsonIgnore]`
> - `[DataContract]` → No equivalent needed (System.Text.Json serializes public properties by default)

### APIs that don't change

`Windows.Devices.*`, `Windows.Media.*`, `Windows.UI.ViewManagement.UISettings`, `Windows.UI.Color`, and most WinRT APIs outside the XAML namespace are unchanged.

### Controls without a direct equivalent

Some UWP controls don't exist in WinUI 3. Choose a replacement based on your scenario:

| UWP control | WinUI 3 replacement | Notes |
|-------------|---------------------|-------|
| `Pivot` | `TabView`, `NavigationView` (top mode), or `RadioButtons` + visibility | For 2–3 fixed tabs, `RadioButtons` with visibility switching is simplest. For dynamic/closeable tabs, use `TabView`. |
| `InkToolbar` (custom subclass) | `CommandBar` with `AppBarToggleButton` items | The built-in `InkToolbar` exists but custom subclassing patterns don't translate cleanly. Rebuild custom toolbars using `CommandBar`. |
| `RadialController` | WinRT interop with window handle | `RadialController.CreateForCurrentView()` has no direct equivalent. Use `RadialControllerInterop` with `GetForWindow(hwnd)`. |
| `SystemNavigationManager` | Custom back button or `NavigationView.IsBackEnabled` | `SystemNavigationManager.GetForCurrentView()` doesn't exist. Add your own back button or use `NavigationView`'s built-in back button. |

## Ink, Win2D, and printing

These subsystems require specific migration steps beyond namespace changes.

### Windows Ink

The `InkCanvas` and `InkPresenter` APIs move to `Microsoft.UI.Input.Inking` but are otherwise identical. The one non-obvious change is `CoreInputDeviceTypes`:

| UWP | WinUI 3 |
|-----|---------|
| `Windows.UI.Input.Inking.*` | `Microsoft.UI.Input.Inking.*` |
| `Windows.UI.Core.CoreInputDeviceTypes` | `Microsoft.UI.Core.CoreInputDeviceTypes` |

`InkStrokeContainer.SaveAsync()` and `LoadAsync()` still require `IRandomAccessStream`. Bridge from `System.IO` streams:

```csharp
// Saving ink strokes to a file using System.IO
using var memoryStream = new MemoryStream();
using var ras = memoryStream.AsRandomAccessStream();
await inkStrokeContainer.SaveAsync(ras);
await File.WriteAllBytesAsync(filePath, memoryStream.ToArray());

// Loading ink strokes from a file
var bytes = await File.ReadAllBytesAsync(filePath);
using var ms = new MemoryStream(bytes);
using var ras = ms.AsRandomAccessStream();
await inkStrokeContainer.LoadAsync(ras);
```

### Win2D

The Win2D package name changed but the API surface is identical:

| UWP | WinUI 3 |
|-----|---------|
| `Win2D.uwp` (NuGet) | `Microsoft.Graphics.Win2D` (NuGet) |

All `Microsoft.Graphics.Canvas.*` APIs (`CanvasDevice`, `CanvasBitmap`, `CanvasRenderTarget`, `DrawInk`) work the same way. Only the NuGet package reference needs updating.

### Printing

UWP printing uses `PrintManager.GetForCurrentView()`. WinUI 3 requires window-handle interop:

```csharp
// UWP
var printManager = PrintManager.GetForCurrentView();
printManager.PrintTaskRequested += OnPrintTaskRequested;
await PrintManager.ShowPrintUIAsync();

// WinUI 3 — must pass window handle
var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
var printManager = PrintManagerInterop.GetForWindow(hwnd);
printManager.PrintTaskRequested += OnPrintTaskRequested;
await PrintManagerInterop.ShowPrintUIForWindowAsync(hwnd);
```

The `PrintDocument` rendering APIs (`Paginate`, `GetPreviewPage`, `AddPages`) are unchanged.

> [!IMPORTANT]
> If you omit the window handle, `PrintManagerInterop.GetForWindow` throws a `COMException`. This is the same interop pattern as `FileOpenPicker` — any API that used `GetForCurrentView()` in UWP needs a window handle in WinUI 3.

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
- Pivot → TabView, NavigationView (top mode), or RadioButtons (no direct equivalent)
- InkToolbar custom subclasses → rebuild as CommandBar with AppBarToggleButton
- PrintManager.GetForCurrentView → PrintManagerInterop.GetForWindow(hwnd)
- Win2D.uwp NuGet → Microsoft.Graphics.Win2D NuGet
- Windows.UI.Input.Inking.* → Microsoft.UI.Input.Inking.*

Do not use any Windows.UI.Xaml.* namespaces in new code.
Do not use CoreDispatcher — use DispatcherQueue.
x:Bind defaults to Mode=OneTime. Add Mode=OneWay for any binding that should update at runtime.
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
