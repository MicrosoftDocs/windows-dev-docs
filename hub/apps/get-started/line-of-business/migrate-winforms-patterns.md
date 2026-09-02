---
title: Windows Forms patterns and their WinUI 3 equivalents
description: Map Windows Forms controls, data-binding patterns, application lifecycle concepts, and incremental migration decisions to WinUI 3.
ms.topic: concept-article
ms.date: 09/02/2026
author: GrantMeStrength
ms.author: jken
---

# Windows Forms patterns and their WinUI 3 equivalents

Windows Forms and WinUI 3 are both .NET desktop UI frameworks, but they use different layout, binding, windowing, and application models. Use this article to translate familiar patterns when you build a new WinUI 3 screen or plan a migration.

You don't need to rewrite a working Windows Forms app solely to adopt a newer UI framework. Compare the business value, accessibility requirements, deployment constraints, test coverage, and lifetime of the existing app. You can also adopt supported non-XAML Windows App SDK capabilities without migrating every screen.

## Understand the main differences

- **UI definition:** Windows Forms commonly uses generated designer code. WinUI 3 defines the visual tree in XAML and connects it to C#.
- **Layout:** Windows Forms often uses coordinates, anchoring, docking, and layout panels. WinUI layouts are usually composed with `Grid`, `StackPanel`, and other XAML panels.
- **Binding:** WinUI supports declarative `x:Bind` and `Binding`. You can use code-behind for simple UI coordination or adopt MVVM incrementally.
- **Application model:** A WinUI 3 app uses `Application`, `Window`, pages, and the Windows App SDK lifecycle and deployment model.
- **Rendering and theming:** WinUI controls use Fluent Design resources and respond to Windows themes, text scale, and input modes.

## Map common controls and patterns

The mappings below are starting points, not automated substitutions. Re-evaluate the workflow and accessibility behavior instead of recreating the old visual tree control for control.

| Windows Forms | WinUI 3 starting point | Notes |
|---|---|---|
| `Form` | `Window` | A `Window` hosts XAML content and isn't a control in the page's visual tree. |
| `Panel` with absolute positioning | `Grid`, `StackPanel`, or another adaptive panel | Use `Canvas` only when absolute coordinates are intrinsic to the scenario. |
| `FlowLayoutPanel` | `StackPanel` or a wrapping layout | Choose based on whether items must wrap. |
| `TableLayoutPanel` | `Grid` | Use row and column definitions with fixed, automatic, or proportional sizing. |
| `DataGridView` | No first-party equivalent | Use a list control for simple records or evaluate a supported third-party grid for full grid behavior. Don't use the older UWP Community Toolkit DataGrid. |
| `ListBox` | `ListView` or `ItemsView` | Both display selectable collections; compare their interaction and layout models. |
| `Label` | `TextBlock` | Use an associated header or accessible name for form fields. |
| `TextBox`, `ComboBox`, `Button`, `CheckBox`, `RadioButton` | Controls with the same names | APIs and styling differ even when names match. |
| `MenuStrip` | `MenuBar` | Use `MenuFlyout` for contextual menus. |
| `StatusStrip` | A status area composed with layout controls | Use `InfoBar` for important, actionable status messages rather than as a permanent status strip. |
| `TabControl` | `TabView` | Useful for multiple open records or documents. |
| `ToolTip` | `ToolTipService` | Don't put essential information only in a tooltip. |
| `BindingSource` | A view model, collection, and XAML binding | `ObservableCollection<T>` reports collection changes; item properties need their own change notification. |
| `ErrorProvider` | App-defined validation presentation | WinUI 3 doesn't provide a complete equivalent. See [Build a validated form](build-validated-form.md). |
| `BackgroundWorker` | Asynchronous I/O or `Task.Run` for CPU-bound work | `async` doesn't move CPU work off the UI thread. Marshal UI updates through `DispatcherQueue` only when execution is on another thread. |
| `MessageBox.Show` | `ContentDialog` | Set the dialog's `XamlRoot` before showing it. |
| `OpenFileDialog` and `SaveFileDialog` | Windows App SDK or Windows Runtime picker APIs | Follow the current desktop picker guidance; some APIs require a window identifier or HWND initialization. |
| `NotifyIcon` | Win32 notification-area integration or a supported library | Windows App SDK doesn't include a first-party tray-icon control. |
| `ProgressBar` | `ProgressBar` or `ProgressRing` | Prefer determinate progress when the app can calculate completion. |
| `DateTimePicker` | `DatePicker` and `TimePicker` | Compose them when the workflow needs both values. |
| `NumericUpDown` | `NumberBox` | Configure validation and acceptable ranges for the business rule. |
| `SplitContainer` | A resizable layout implementation | A `Grid` supplies layout, but it doesn't by itself provide a draggable splitter. |
| `WebBrowser` | `WebView2` | Review initialization, navigation, origin, and message-handling security. |

## Translate startup and navigation

The generated WinUI 3 project provides the application entry point and creates an `App` instance. `App.OnLaunched` typically creates and activates the first `Window`.

A page or control can load data from its `Loaded` event, a navigation callback, or an explicit initialization command. Avoid starting fallible asynchronous work in a view-model constructor, because a constructor can't be awaited or report failure cleanly.

Use:

- `Frame` navigation for page-based experiences.
- `ContentDialog` for short modal interactions within a window.
- Additional `Window` instances for independent top-level views.

See [App lifecycle](../../develop/launch/app-lifecycle.md), [Multiple windows](../../develop/ui/multiple-windows.md), and [Dialogs and flyouts](../../develop/ui/controls/dialogs-and-flyouts/dialogs.md).

## Translate data binding

Windows Forms code often assigns control properties or configures a `BindingSource`. WinUI can declare the relationship in XAML:

| Scenario | Windows Forms | WinUI 3 |
|---|---|---|
| Display a property | `nameLabel.Text = customer.Name;` | `<TextBlock Text="{x:Bind ViewModel.Customer.Name, Mode=OneWay}" />` |
| Edit a property | `DataBindings.Add(...)` | `<TextBox Text="{x:Bind ViewModel.Name, Mode=TwoWay}" />` |
| Display a collection | Set `DataSource` | Bind `ItemsSource` to a collection |
| Report changes | `INotifyPropertyChanged` or reset bindings | `INotifyPropertyChanged` on the bound object |

Set an explicit `x:Bind` mode. The default is `OneTime`, which doesn't update when a property changes.

MVVM is useful when a screen has substantial state, commands, or testable business behavior, but it isn't mandatory. Keep business and data-access logic out of the visual layer regardless of whether you use a toolkit.

## Plan an incremental migration

1. Identify a bounded workflow with clear value and dependencies.
2. Separate business and data-access logic from Windows Forms controls.
3. Add automated tests around that logic and the data contract.
4. Rebuild the workflow with WinUI layout and interaction patterns rather than copying coordinates.
5. Test accessibility, text scaling, theming, deployment, and failure recovery.
6. Decide how old and new components communicate during the transition.

## Related content

- [Migration decision guide](../../windows-app-sdk/migrate-to-windows-app-sdk/migration-decision-guide.md)
- [Migration strategy overview](../../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md)
- [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- [WPF patterns and their WinUI 3 equivalents](../../windows-app-sdk/migrate-to-windows-app-sdk/wpf-patterns-winui3.md)
- [Display tabular data](display-tabular-data.md)
- [Data binding overview](../../develop/data-binding/data-binding-overview.md)
