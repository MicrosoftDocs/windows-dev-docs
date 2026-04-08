---
title: WPF patterns and their WinUI 3 equivalents
description: WinUI 3 was designed for modern Windows from the ground up. This topic maps familiar WPF patterns to their WinUI 3 equivalents, and describes the modern approach for cases where the pattern has evolved.
ms.topic: concept-article
ms.date: 04/07/2026
keywords: windows, app, sdk, wpf, winui, winui3, migration, patterns, equivalents
ms.localizationpriority: medium
---

# WPF patterns and their WinUI 3 equivalents

WPF is a mature framework built over two decades, and WinUI 3 is a modern UI platform designed for Windows 10 and Windows 11 from the ground up. Most WPF concepts have direct equivalents in WinUI 3. In some areas, WinUI 3 introduces a better approach that replaces an older pattern. And in a few cases, features are still being actively developed.

This topic maps common WPF patterns to their WinUI 3 equivalents so you can plan your migration with confidence.

> [!TIP]
> For UWP-to-WinUI 3 migration guidance, see [What's supported when migrating from UWP to WinUI 3](what-is-supported.md). For general WPF + Windows App SDK guidance, see [Use the Windows App SDK in a WPF app](wpf-plus-winappsdk.md).

## Controls

Most WPF controls have direct equivalents in WinUI 3. The following table covers controls where the mapping is not one-to-one.

| WPF control | WinUI 3 equivalent | Notes |
|---|---|---|
| `DataGrid` | [DataGrid (Community Toolkit)](https://github.com/CommunityToolkit/Windows/tree/main/components/DataGrid) | The WinUI 3 Community Toolkit provides a DataGrid control. Most common WPF DataGrid features are supported; see the toolkit docs for specifics. |
| `InkCanvas` / `InkToolbar` | Under active development | Ink support in WinUI 3 is being actively developed. Follow [microsoft-ui-xaml #1000](https://github.com/microsoft/microsoft-ui-xaml/issues/1000) for updates. In the meantime, you can host the WPF `InkCanvas` via [XAML Islands](../../desktop/modernize/xaml-islands/xaml-islands.md). |
| `Ribbon` | `CommandBar` / `CommandBarFlyout` | Consider [CommandBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.commandbar) and [CommandBarFlyout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.commandbarflyout) for toolbar-style scenarios. For a full ribbon surface, the [Fluent UI React](https://developer.microsoft.com/fluentui) component via WebView2 is an option for hybrid apps. |
| `StatusBar` | `InfoBar` + custom layout | Use [InfoBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobar) for status messaging, or add a dedicated footer area to your layout. |
| `FlowDocumentReader` / `FlowDocumentScrollViewer` | `RichTextBlock` | Use [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock) for read-only rich text display. WinUI 3 takes a different approach to document content that is better suited to modern app scenarios. |
| `PasswordBox` with `SecureString` | `PasswordBox` | WinUI 3's [PasswordBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox) provides password masking. `SecureString` is deprecated in .NET 5+; the recommended approach is to minimize how long credentials are held in memory using `ReadOnlySpan<char>` patterns. |
| `WebBrowser` | [WebView2](/microsoft-edge/webview2/) | `WebView2` uses the modern Microsoft Edge (Chromium) engine and is the recommended approach for embedding web content across all desktop app types. |

## XAML features

WinUI 3 XAML is closely related to UWP XAML, and shares the same core engine. Some WPF-specific XAML features have evolved into more composable, testable alternatives.

| WPF feature | WinUI 3 approach | Notes |
|---|---|---|
| `DataTrigger` / `MultiTrigger` | [Behaviors (Community Toolkit)](https://aka.ms/toolkit/behaviors) | WinUI 3 uses attached behaviors rather than inline triggers. The XAML Behaviors package supports `DataTriggerBehavior`, `EventTriggerBehavior`, and more. Behaviors are more composable and unit-testable than WPF triggers. |
| `DynamicResource` | `ThemeResource` | `ThemeResource` provides runtime resource lookup and responds automatically to theme changes (light, dark, high contrast). Use `StaticResource` for values that never change at runtime. |
| `MultiBinding` / `PriorityBinding` | Converters or `x:Bind` | Use a multi-value converter with individual bindings, or use `x:Bind` with a computed property on your view model. `x:Bind` is compiled and type-safe, which makes it more performant than `Binding`. |
| `Style` with `BasedOn` | ✅ Supported | Style inheritance with `BasedOn` works in WinUI 3. |
| Implicit styles | ✅ Supported | Resource dictionary implicit styles (styles without `x:Key`) work as expected. |
| `AdornerLayer` | Custom overlay approach | For validation visuals, use the built-in built-in input validation support in text controls (see [TextBox.InputValidationErrorEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textboxinputvalidationerroreventargs)). For custom overlays, use a `Canvas` or `Grid` overlay layer in your layout — this is more explicit and easier to reason about than WPF's adorner layer. |

## Threading and dispatch

| WPF pattern | WinUI 3 equivalent | Notes |
|---|---|---|
| `Dispatcher.Invoke` / `BeginInvoke` | `DispatcherQueue.TryEnqueue` | WinUI 3 uses [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue). The async pattern is `await DispatcherQueue.EnqueueAsync(...)` using the Community Toolkit extension. |
| `Application.Current.Dispatcher` | `DispatcherQueue.GetForCurrentThread()` | Capture the `DispatcherQueue` at construction time on the UI thread and store it for later use on background threads. |
| `BackgroundWorker` | `Task` / `async`-`await` | Modern .NET async patterns are the right approach in WinUI 3. `Task`, `CancellationToken`, and `IProgress<T>` cover all `BackgroundWorker` scenarios and integrate naturally with `x:Bind`. |

## App model and lifecycle

| WPF concept | WinUI 3 equivalent | Notes |
|---|---|---|
| `Application.Startup` / `Exit` events | `App.OnLaunched` / `Window.Closed` | WinUI 3 uses `OnLaunched` in `App.xaml.cs` as the entry point. Per-window teardown is handled in `Window.Closed`. |
| `Application.Current.MainWindow` | Your `Window` instance | Hold a reference to your window instance in `App.xaml.cs` and expose it as a property. |
| `Window` subclassing | [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) customization | WinUI 3 windows are customized through `AppWindow` (title bar, presenter, overlapped/fullscreen/compact overlay modes) rather than subclassing. See [Manage app windows](../../develop/ui/manage-app-windows.md). |
| `SystemParameters` | `DisplayArea` / `UISettings` | System display properties are available through [DisplayArea](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayarea) and [UISettings](/uwp/api/windows.ui.viewmanagement.uisettings). |

## Resources and localization

| WPF pattern | WinUI 3 equivalent | Notes |
|---|---|---|
| `.resx` resource files | `.resw` + `ResourceLoader` | WinUI 3 uses `.resw` files and the [ResourceLoader](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourceloader) API. The .NET Upgrade Assistant can automate much of this conversion. |
| `x:Static` markup extension | `x:Bind` to a static property | Use `x:Bind` with a static property or a singleton accessor. `x:Bind` is compiled and produces clearer error messages than `x:Static`. |
| Merged resource dictionaries | ✅ Supported | `ResourceDictionary.MergedDictionaries` works in WinUI 3. |
| Theme-specific resource dictionaries | ✅ Supported | `ResourceDictionary.ThemeDictionaries` is the WinUI 3 mechanism for per-theme resources, and integrates with automatic dark/light mode switching. |

## Printing

| WPF feature | WinUI 3 status | Notes |
|---|---|---|
| `PrintDialog` / `PrintDocument` | [PrintManager](/uwp/api/windows.graphics.printing.printmanager) | Full printing support is available via `PrintManager`. See [Print from your app](/windows/apps/develop/devices-sensors/print-from-your-app). |

## Developer tooling

| WPF tooling | WinUI 3 status | Notes |
|---|---|---|
| XAML Designer (Design tab) | Not yet supported | The Visual Studio XAML Designer doesn't currently support WinUI 3 projects. [XAML Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) is supported and is the recommended way to iterate on layout and styles without stopping the debugger. |
| Blend for Visual Studio | Limited support | Blend can open WinUI 3 projects but Design view is not available. |

## See also

- [Use the Windows App SDK in a WPF app](wpf-plus-winappsdk.md)
- [What's supported when migrating from UWP to WinUI 3](what-is-supported.md)
- [Overall migration strategy](overall-migration-strategy.md)
- [Windows Community Toolkit](/dotnet/communitytoolkit/introduction)
- [XAML Behaviors (Community Toolkit)](https://aka.ms/toolkit/behaviors)
