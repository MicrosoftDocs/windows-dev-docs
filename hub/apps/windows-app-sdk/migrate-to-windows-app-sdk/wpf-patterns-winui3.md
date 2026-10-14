---
title: Migrate WPF app patterns to WinUI 3
description: Compare common WPF controls, XAML, lifecycle, and tooling patterns with WinUI 3 behavior so you can assess migration work and known gaps.
ms.topic: concept-article
ms.date: 10/13/2026
author: GrantMeStrength
ms.author: jken
keywords: windows, app, sdk, wpf, winui, winui3, migration, patterns, equivalents
ms.localizationpriority: medium
---

# Migrate WPF app patterns to WinUI 3

WPF and WinUI 3 share some XAML concepts, but they use different control sets, app models, windowing APIs, resource systems, and tooling. A familiar name or pattern doesn't guarantee equivalent behavior, and some WPF features have no first-party WinUI 3 counterpart.

Use this topic to identify starting points and gaps before you estimate or implement a migration. Validate each mapping against your app's behavior, accessibility requirements, dependencies, and deployment model.

> [!TIP]
> For general WPF + Windows App SDK guidance, see [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md?pivots=dotnet).

## Controls

Some common controls have similar names, but their APIs, styling, accessibility behavior, and layout can differ. The following table highlights mappings that require a design decision or don't have a first-party equivalent.

| WPF control | WinUI 3 equivalent | Notes |
|---|---|---|
| [`DataGrid`](/dotnet/api/system.windows.controls.datagrid) | No first-party equivalent | WinUI 3 does not include a built-in DataGrid. The community-maintained [WinUI.TableView](https://github.com/w-ahmad/WinUI.TableView) is one option. Evaluate community projects based on your support and maintenance requirements. |
| [`Ribbon`](/dotnet/api/system.windows.controls.ribbon.ribbon) | `CommandBar` / Community Toolkit Labs | Consider [CommandBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.commandbar) and [CommandBarFlyout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.commandbarflyout) for toolbar-style scenarios. A [Ribbon control](https://github.com/CommunityToolkit/Labs-Windows/tree/main/components/Ribbon) is also available in Community Toolkit Labs (experimental). |
| [`StatusBar`](/dotnet/api/system.windows.controls.primitives.statusbar) | `InfoBar` + custom layout | Use [InfoBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobar) for status messaging, or add a dedicated footer area to your layout. |
| [`FlowDocumentReader`](/dotnet/api/system.windows.controls.flowdocumentreader) / [`FlowDocumentScrollViewer`](/dotnet/api/system.windows.controls.flowdocumentscrollviewer) | [`RichTextBlock`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock) | Use [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock) for read-only rich text display. WinUI 3 doesn't implement the WPF `FlowDocument` model, so document navigation, pagination, and editing require separate design work. |
| [`PasswordBox`](/dotnet/api/system.windows.controls.passwordbox) with `SecureString` | [`PasswordBox`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox) | WinUI 3's [PasswordBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox) provides password masking. `SecureString` is deprecated in .NET 5+; the recommended approach is to minimize how long credentials are held in memory using `ReadOnlySpan<char>` patterns. |
| [`WebBrowser`](/dotnet/api/system.windows.controls.webbrowser) | [WebView2](/microsoft-edge/webview2/) | `WebView2` uses the modern Microsoft Edge (Chromium) engine and is the recommended approach for embedding web content across all desktop app types. |

## XAML features

WinUI 3 XAML includes resource dictionaries, styles, data binding, and markup extensions, but the supported features and runtime behavior differ from WPF. Some WPF-specific patterns require attached behaviors, converters, view-model properties, or custom controls.

| WPF feature | WinUI 3 approach | Notes |
|---|---|---|
| [`DataTrigger`](/dotnet/api/system.windows.datatrigger) / [`MultiTrigger`](/dotnet/api/system.windows.multitrigger) | [Behaviors (Community Toolkit)](https://aka.ms/toolkit/behaviors) | WinUI 3 doesn't support WPF data and multi-triggers. The XAML Behaviors package provides `DataTriggerBehavior`, `EventTriggerBehavior`, and related building blocks, but you must rework trigger logic and verify behavior. |
| [`DynamicResource`](/dotnet/desktop/wpf/advanced/dynamicresource-markup-extension) | [`ThemeResource`](/windows/uwp/xaml-platform/themeresource-markup-extension) | `ThemeResource` provides runtime resource lookup and responds automatically to theme changes (light, dark, high contrast). Use `StaticResource` for values that never change at runtime. |
| [`MultiBinding`](/dotnet/api/system.windows.data.multibinding) / [`PriorityBinding`](/dotnet/api/system.windows.data.prioritybinding) | [Functions in `x:Bind`](../../develop/data-binding/function-bindings.md) | WinUI 3 doesn't provide the WPF `MultiBinding` or `PriorityBinding` types, and a WinUI value converter receives only one source value. To combine values, use an `x:Bind` function binding with multiple arguments and `Mode=OneWay`, or expose a computed view-model property. `PriorityBinding` fallback isn't automatic, so implement it explicitly, for example in that function or in the view model. |
| `Style` with `BasedOn` | ✅ Supported | Style inheritance with `BasedOn` works in WinUI 3. |
| Implicit styles | ✅ Supported | Resource dictionary implicit styles (styles without `x:Key`) work as expected. |
| [`AdornerLayer`](/dotnet/api/system.windows.documents.adornerlayer) | Custom overlay approach | WinUI 3 has no adorner layer equivalent. Use a `Canvas` or `Grid` overlay when that meets the scenario, and account for input, focus, clipping, scrolling, and accessibility. |

## Threading and dispatch

| WPF pattern | WinUI 3 equivalent | Notes |
|---|---|---|
| [`Dispatcher.Invoke`](/dotnet/api/system.windows.threading.dispatcher.invoke) / `BeginInvoke` | [`DispatcherQueue.TryEnqueue`](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) | WinUI 3 uses [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue). The async pattern is `await DispatcherQueue.EnqueueAsync(...)` using the Community Toolkit extension. |
| [`Application.Current.Dispatcher`](/dotnet/api/system.windows.application.current) | [`DispatcherQueue.GetForCurrentThread()`](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.getforcurrentthread) | Capture the `DispatcherQueue` at construction time on the UI thread and store it for later use on background threads. |
| [`BackgroundWorker`](/dotnet/api/system.componentmodel.backgroundworker) | `Task` / `async`-`await` | Use `Task`, `CancellationToken`, and `IProgress<T>` where they fit the workload. Review cancellation, exception handling, progress reporting, and UI-thread access instead of applying a mechanical substitution. |

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
| `.resx` resource files | `.resw` + `ResourceLoader` | WinUI 3 uses `.resw` files and the [ResourceLoader](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourceloader) API. Migrate the resource strings and update the resource lookup code as part of the UI migration. |
| `x:Static` markup extension | `x:Bind` to a static property | Use `x:Bind` with a static property or a singleton accessor. Verify initialization order and change notification when the value can change. |
| Merged resource dictionaries | ✅ Supported | `ResourceDictionary.MergedDictionaries` works in WinUI 3. |
| Theme-specific resource dictionaries | ✅ Supported | `ResourceDictionary.ThemeDictionaries` is the WinUI 3 mechanism for per-theme resources, and integrates with automatic dark/light mode switching. |

## Printing

| WPF feature | WinUI 3 status | Notes |
|---|---|---|
| [`PrintDialog`](/dotnet/api/system.windows.controls.printdialog) / [`PrintDocument`](/dotnet/api/system.drawing.printing.printdocument) | [PrintManager](/uwp/api/windows.graphics.printing.printmanager) | Printing uses a different API and workflow. A desktop app must obtain `PrintManager` for its window through the desktop interop API. See [Print from your app](/windows/apps/develop/devices-sensors/print-from-your-app). |

## Developer tooling

| WPF tooling | WinUI 3 status | Notes |
|---|---|---|
| XAML Designer (Design tab) | Not yet supported | The Visual Studio XAML Designer doesn't currently support WinUI 3 projects. [XAML Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) is supported and is the recommended way to iterate on layout and styles without stopping the debugger. |
| Blend for Visual Studio | ⚠️ Limited support | Blend ships with Visual Studio and can open WinUI 3 projects. The XAML Document Outline is functional, but Design view is not available for WinUI 3. |

## Assess gaps before migrating

Review the gaps that affect your product before you commit to a UI migration. Examples include the lack of a first-party `DataGrid`, `Ribbon`, adorner layer, WPF document model, and WinUI 3 design surface in Visual Studio. Third-party controls, custom drawing, accessibility automation peers, printing, multiple windows, and deployment can also require architecture or product decisions rather than API substitutions.

Prototype representative screens and high-risk integrations before estimating the full migration. If a required capability has no acceptable equivalent, keep that workflow in WPF, modernize it in place, or defer the UI migration.

## Use an AI assistant as an optional review aid

An AI coding assistant can inspect a bounded WPF view, identify dependencies on WPF-specific APIs, draft a control or binding mapping, and propose edits for review. It can also help create an inventory of third-party controls and platform integrations.

Verify every suggestion against current WinUI 3 documentation and the behavior your app requires. Build and test generated changes, including data binding, threading, window ownership, accessibility, localization, packaging, and error handling. An assistant can't supply missing platform features, choose acceptable product tradeoffs, or guarantee that a similar-looking control preserves the original workflow.

## See also

- [Choose your migration path](migration-decision-guide.md)
- [Windows Forms patterns and their WinUI 3 equivalents](../../get-started/line-of-business/migrate-winforms-patterns.md)
- [Migration terminology](migration-terminology.md)
- [Migrate and modernize Windows apps](index.yml)
