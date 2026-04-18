---
description: Learn how to modernize your existing WPF, Windows Forms, or Win32 desktop app with Windows App SDK features, MSIX packaging, and modern Windows APIs — without a full rewrite.
title: Modernize your desktop apps for Windows
ms.topic: overview
ms.date: 04/07/2026
ms.localizationpriority: medium
---

# Modernize your existing desktop apps

You don't need to rewrite your WPF, Windows Forms, or Win32 app to take advantage of modern Windows features. The Windows App SDK and the broader Windows platform offer modular capabilities you can adopt incrementally, at your own pace.

Use the table below to find the right starting point for your situation.

## Choose your modernization path

| I want to... | Recommended approach |
|---|---|
| Add modern UI controls (Fluent, rounded corners, dark mode) to my WPF or Win32 app | [Host WinUI controls in a WPF app (XAML Islands)](xaml-islands/xaml-islands.md) |
| Use Windows platform features (notifications, sharing, file pickers) in my WPF or WinForms app | [Call Windows Runtime APIs in desktop apps](desktop-to-uwp-enhance.md) |
| Package my app for the Microsoft Store or enterprise deployment | [Package a desktop app with MSIX](/windows/msix/desktop/source-code-overview) |
| Unlock features that require package identity (push notifications, background tasks, app extensions, Windows AI APIs, share targets, and more) | [Grant identity to an unpackaged app](grant-identity-to-nonpackaged-apps-overview.md) |
| Integrate my app with Windows 11 shell features (snap layouts, context menus, taskbar) | [Integrate with Windows 11 features](desktop-to-uwp-extensions.md) |
| Move to a fully modern app with WinUI 3 over time | [Migrate to WinUI 3](../../winui/winui3/create-your-first-winui3-app.md) |
| Add on-device AI capabilities to my desktop app | [Windows AI Foundry](/windows/ai/overview) |

## What is the Windows App SDK?

The [Windows App SDK](../../windows-app-sdk/index.md) is the recommended way to access modern Windows platform features from any desktop app — WPF, Windows Forms, Win32, or WinUI 3. It provides a consistent, versioned set of APIs that work across Windows 10 and Windows 11, decoupled from the OS release cycle.

You can use the Windows App SDK in your existing app without changing your UI framework. Add it as a NuGet package and call its APIs alongside your existing code.

## Add modern UI without a full rewrite

You can host [WinUI 3 controls](../../winui/index.md) inside existing WPF or Win32 app windows using the Windows App SDK. This lets you modernize your UI incrementally — one window or dialog at a time — without migrating the entire app.

For guidance on hosting WinUI 3 controls in your existing app, see [Host WinRT XAML controls in desktop apps (XAML Islands)](xaml-islands/xaml-islands.md). For low-level visual effects and animations, see [Modernize your desktop app using the Visual layer](ui/visual-layer-in-desktop-apps.md).

## Call Windows Runtime APIs

Many Windows platform features — push notifications, the share contract, file pickers, Bluetooth, and more — are exposed through Windows Runtime (WinRT) APIs. You can call these APIs directly from WPF, Windows Forms, and C++ Win32 apps.

For more information, see [Call Windows Runtime APIs in desktop apps](desktop-to-uwp-enhance.md).

## Package with MSIX

Packaging your app with MSIX gives you a modern, reliable installation experience, clean uninstall, automatic updates, and access to the Microsoft Store and enterprise deployment pipelines. MSIX packaging is separate from modernizing your app's code — you can package a WPF or Win32 app with MSIX without changing any source code.

For more information, see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview).

## Features that require package identity

Some Windows platform features — including push notifications, background tasks, app extensions, sharing targets, Windows AI Foundry APIs, file associations, and startup tasks — require your app to have a [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) at runtime. You can grant identity to an unpackaged app without full MSIX packaging. This approach is sometimes called a *sparse package* or *packaging with external location*.

For more information, see:
- [Package identity overview](package-identity-overview.md)
- [Grant identity to a non-packaged app](grant-identity-to-nonpackaged-apps-overview.md)
- [Features that require package identity](modernize-packaged-apps.md)

## Integrate with Windows 11 shell features

Windows 11 introduces new shell integration points — snap layouts, updated context menus, rounded window corners, and taskbar integration. Many of these are available to unpackaged desktop apps with no code changes. Others require packaging extensions.

For more information, see [Integrate your desktop app with Windows using packaging extensions](desktop-to-uwp-extensions.md).

## Migrate to WinUI 3

If you're planning a larger modernization effort — or building new features as separate modules — consider building new components with [WinUI 3](../../winui/index.md) and the Windows App SDK. WinUI 3 is the modern native UI framework for Windows desktop apps and is the recommended path for new development.

See [Create your first WinUI 3 app](../../get-started/start-here.md) to get started.