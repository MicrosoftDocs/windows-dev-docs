---
title: "Cross-framework app considerations"
description: Build Windows apps that share business logic across WinUI 3, MAUI, and other frameworks using the Windows App SDK.
ms.topic: overview
ms.date: 05/13/2026
ms.author: jken
author: GrantMeStrength
---

# Cross-framework app considerations

You may be coming to WinUI 3 from React Native Windows, Electron, Flutter, or .NET MAUI. This page covers what to expect when targeting WinUI 3 specifically, and how to set up your AI agent for success.

The key decision: do you want to keep a cross-platform shell, or build a Windows-first app? Give your AI agent a clear boundary — which business logic stays shared, and which UI should be rewritten as native WinUI 3.

## React Native for Windows

React Native for Windows uses a WinUI rendering layer under the hood. You can integrate native Windows controls through `<WindowsXamlView>` or NativeModules.

For a fully native Windows experience, porting directly to WinUI 3 gives better performance and full Windows API access. Keep business logic and services, but rewrite UI components as XAML and C#.

Starter prompt:
```text
I have a React Native for Windows component. Rewrite it as a WinUI 3 UserControl using C# and XAML.
```

## Electron

Electron apps are web technology wrapped in a shell; WinUI 3 is native Win32. A practical incremental path is to host your existing web UI in a WinUI 3 window using WebView2, then migrate features to native controls over time.

Starter prompt:
```text
I have an Electron app. Create a WinUI 3 shell that hosts a WebView2, and show me how to call native Windows APIs from the web layer using window.chrome.webview.postMessage.
```

## .NET MAUI

.NET MAUI already targets Windows via WinUI 3. If you want a Windows-only experience with full access to WinUI 3 capabilities, remove MAUI and target WinUI 3 directly.

- ViewModels and services move across cleanly
- MAUI XAML (`Microsoft.Maui.Controls.*`) needs rewriting to WinUI 3 XAML (`Microsoft.UI.Xaml.*`)
- Keep shared .NET class libraries as-is

Starter prompt:
```text
I have a .NET MAUI ViewModel and service layer. Reuse them in a WinUI 3 project targeting Windows only. Keep the ViewModel unchanged and update only the View.
```

## Flutter

Flutter for Windows uses its own rendering engine, not WinUI 3. Platform channels let Flutter call native Win32 or WinRT APIs, but a full port replaces Flutter rendering with native XAML.

Starter prompt:
```text
I have a Flutter screen with a list and a detail view. Rewrite it as a WinUI 3 page using NavigationView and a master/detail layout.
```

## Sharing code across platforms

- Keep business logic in a separate .NET class library — it's platform-agnostic
- Share that library between WinUI 3, MAUI, Blazor, or any other .NET target
- Reuse models, services, and ViewModels before rewriting UI
- Use `#if WINDOWS` conditional compilation only as a last resort

## Related content

- [Migrate and port apps overview](index.md)
- [Migrate from WPF](wpf-to-winui.md)
- [Migrate from UWP](uwp-to-winui.md)
- [Migrate from iOS](ios-to-winui.md)
