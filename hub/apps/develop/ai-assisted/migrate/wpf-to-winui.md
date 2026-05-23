---
title: "Migrate a WPF app to WinUI 3"
description: Use the winui-wpf-migration AI skill to port a WPF app to WinUI 3, updating XAML namespaces, controls, and project structure.
ms.topic: overview
ms.date: 05/13/2026
ms.author: jken
author: GrantMeStrength
---

# Migrate a WPF app to WinUI 3

WPF apps run on .NET but use the Windows Presentation Foundation XAML stack. WinUI 3 is the modern replacement. The core challenge for AI migration is that WPF uses `System.Windows.*` namespaces while WinUI 3 uses `Microsoft.UI.Xaml.*`, and many controls and windowing APIs need targeted substitutions rather than simple search-and-replace.

## Install the WPF migration skill

```powershell
gh copilot plugin install winui@awesome-copilot
```

## API substitution table

### Namespaces

| WPF | WinUI 3 |
|-----|---------|
| `System.Windows.*` | `Microsoft.UI.Xaml.*` |
| `System.Windows.Controls.*` | `Microsoft.UI.Xaml.Controls.*` |
| `System.Windows.Media.*` | `Microsoft.UI.Xaml.Media.*` |
| `System.Windows.Data.*` | `Microsoft.UI.Xaml.Data.*` |
| `System.Windows.Input.*` | `Microsoft.UI.Input.*` |

### Controls

| WPF | WinUI 3 | Notes |
|-----|---------|-------|
| `Window` | `Microsoft.UI.Xaml.Window` | Different API surface |
| `Grid`, `StackPanel`, `Canvas` | Unchanged | Same names |
| `TextBox`, `Button`, `CheckBox` | Unchanged | Same names, WinUI styling |
| `ListBox` / `ListView` | `ListView` | Use `ItemsView` for new code |
| `DataGrid` | `DataGrid` (CommunityToolkit) | Add `CommunityToolkit.WinUI.Controls.DataGrid` |
| `TabControl` | `TabView` | Different API |
| `Menu` / `MenuItem` | `MenuBar` / `MenuBarItem` | |
| `ToolBar` | `CommandBar` | |
| `RichTextBox` | `RichEditBox` | |
| `WebBrowser` | `WebView2` | Different API, async |

### Threading

| WPF | WinUI 3 |
|-----|---------|
| `Dispatcher.Invoke(...)` | `DispatcherQueue.TryEnqueue(...)` |
| `Dispatcher.BeginInvoke(...)` | `DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, ...)` |
| `Application.Current.Dispatcher` | `this.DispatcherQueue` |

### Windowing and DPI

| WPF | WinUI 3 |
|-----|---------|
| `Window.WindowState` | `AppWindow.Presenter` (use `OverlappedPresenter`) |
| `SystemParameters.WorkArea` | `DisplayArea.GetFromWindowId(...)` |
| `PresentationSource.FromVisual()` | `WinRT.Interop.WindowNative.GetWindowHandle(window)` |

### Data binding

| WPF | WinUI 3 |
|-----|---------|
| `INotifyPropertyChanged` | Unchanged |
| `ObservableCollection<T>` | Unchanged |
| `{Binding}` | `{x:Bind}` preferred (compile-time) |
| `DependencyProperty` | Unchanged |
| `IValueConverter` | Unchanged |

### Resources and styles

| WPF | WinUI 3 |
|-----|---------|
| `ResourceDictionary` | Unchanged |
| `StaticResource` | Unchanged |
| `DynamicResource` | `{ThemeResource}` for system colors |
| `SystemColors.WindowBrush` | `{ThemeResource SystemFillColorSolidNeutralBrush}` |

## Starter prompt

```text
I'm migrating a WPF app to WinUI 3 using the Windows App SDK.

Apply these substitutions:
- System.Windows.* → Microsoft.UI.Xaml.*
- Dispatcher.Invoke / BeginInvoke → DispatcherQueue.TryEnqueue
- Window.WindowState → AppWindow with OverlappedPresenter
- PresentationSource → WinRT.Interop.WindowNative.GetWindowHandle
- DynamicResource for system colors → ThemeResource
- {Binding} → {x:Bind} where possible (compile-time binding)
- ListBox → ListView or ItemsView
- TabControl → TabView
- WebBrowser → WebView2
- DataGrid → CommunityToolkit.WinUI.Controls.DataGrid

Do not use any System.Windows.* namespaces in new code.
Do not use Dispatcher.Invoke — use DispatcherQueue.TryEnqueue.
Flag APIs without a direct WinUI 3 equivalent rather than guessing.
```

## Project file changes

```xml
<!-- Before (WPF) -->
<TargetFramework>net10.0-windows</TargetFramework>
<UseWPF>true</UseWPF>

<!-- After (WinUI 3) -->
<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
<WindowsSdkPackageVersion>10.0.19041.31</WindowsSdkPackageVersion>
```

```powershell
dotnet add package Microsoft.WindowsAppSDK
```

## APIs that don't migrate directly

Tell the agent to flag these rather than guess:

- WPF `Adorner` layer — no equivalent in WinUI 3
- WPF `FlowDocument` / `DocumentViewer` — use `RichEditBox` for editable content; no viewer equivalent
- WPF `Viewport3D` — use Win2D or DirectX interop
- Air-space / HWND hosting — use `SwapChainPanel` or Win32 interop patterns

## Related content

- [Migrate from UWP](uwp-to-winui.md)
- [Migrate from iOS](ios-to-winui.md)
- [WinUI agent plugin](../winui-agent-plugin.md)
- [Windows App SDK migration guide](../../../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md)
