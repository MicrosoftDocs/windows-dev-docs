---
description: Learn how to modernize your existing WPF, Windows Forms, or Win32 desktop app with the Windows App SDK, MSIX packaging, and modern Windows APIs—no rewrite needed.
title: Modernize existing desktop apps
ms.topic: concept-article
ms.date: 09/02/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Modern Windows features for desktop apps

You don't need to rewrite your WPF, Windows Forms, or Win32 app to take advantage of modern Windows features. The Windows App SDK and the broader Windows platform offer modular capabilities you can adopt incrementally, at your own pace.

This article introduces three independent modernization approaches—calling Windows Runtime (WinRT) APIs, adding the Windows App SDK, and adopting MSIX packaging—so you can decide which features to add and where to start based on your app's framework.

## UI framework migration to WinUI 3

If your goal is to fully modernize your app's UI layer, we recommend migrating to [WinUI 3](../../winui/winui3/index.md) — the native UI framework for Windows desktop apps. See [Create your first WinUI 3 app](../../get-started/start-here.md) to get started.

If you want to add Windows features to your existing app framework (WPF, Windows Forms, or C++ Win32) without changing your UI layer, use one of the [modernization approaches](#modernization-approaches) described in this article.

> [!NOTE]
> This article covers adding modern Windows features to your existing desktop app. If you want to modernize your .NET toolchain or C++ compiler, see [GitHub Copilot upgrade](/dotnet/core/porting/github-copilot-upgrade/overview) or the [Microsoft C++ porting and upgrading guide](/cpp/porting/visual-cpp-porting-and-upgrading-guide).

## Modernization approaches

The three modernization approaches are independent. You can use one, two, or all three in the same app.

| Approach | What it gives you | Requires packaging? |
|---|---|---|
| [WinRT APIs](winrt-apis-desktop-apps.md) | Direct access to Windows platform APIs (notifications, Bluetooth, share contract, sensors, and more) from any .NET or C++ desktop app | No, for most APIs |
| [Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) | A NuGet package that brings modern Windows development features (windowing, text rendering, resources, and more) to existing apps | No, for most features |
| [MSIX packaging](../../package-and-deploy/packaging/index.md) | A modern installer with clean uninstall, automatic updates, and package identity; requires no source code changes | N/A — packaging *is* the approach |

> [!TIP]
> Start with **WinRT APIs** if you want to call a specific Windows platform API. Add the **Windows App SDK** when you need its features (DWriteCore, MRT Core, windowing). Add **MSIX packaging** when you need package identity or want to modernize your installer.

### WinRT APIs

[Windows Runtime (WinRT) APIs](/uwp/api) expose Windows platform features such as app notifications, the share contract, Bluetooth, and more. You can call these APIs directly from WPF, Windows Forms, and C++ Win32 apps.

> [!div class="nextstepaction"]
> [Call Windows Runtime APIs in desktop apps](winrt-apis-desktop-apps.md)

### Windows App SDK

The [Windows App SDK](../../windows-app-sdk/index.md) delivers modern Windows development features as a NuGet package you add to your existing project. You don't need to change your UI framework.

> [!div class="nextstepaction"]
> [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)

### MSIX packaging and package identity

MSIX packaging replaces your existing installer and gives your app package identity. Some Windows features — including background tasks, startup tasks, file associations, and Windows AI Foundry — require package identity at runtime. If you want to keep your existing installer, you can still grant package identity without full MSIX packaging — an approach sometimes called *packaging with external location* or a *sparse package*.

- [Packaging overview](../../package-and-deploy/packaging/index.md)
- [Features that require package identity](modernize-packaged-apps.md)
- [Grant identity to a non-packaged app](grant-identity-to-nonpackaged-apps-overview.md)
- [Integrate your desktop app with Windows using packaging extensions](desktop-to-uwp-extensions.md)
- [Package your app using single-project MSIX](../../windows-app-sdk/single-project-msix.md)

## Framework-specific starting points

The following table lists the recommended starting point for each desktop app framework.

| Your framework | Recommended starting point |
|---|---|
| **WPF** | [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) |
| **Windows Forms** | [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) |
| **C++ Win32** | [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) |
| **C++/WinRT** | [WinRT APIs for desktop apps](winrt-apis-desktop-apps.md) |

## Feature catalog for desktop app modernization

The following tables list common modernization features and whether each one requires the Windows App SDK or package identity.

### AI

| Feature | Windows App SDK required? | Package identity required? |
|---|---|---|
| [Windows AI Foundry](/windows/ai/overview) | No | Yes |

### UI and text

| Feature | Windows App SDK required? | Package identity required? |
|---|---|---|
| [WinUI 3 components in a desktop app](../../winui/winui3/index.md) | Yes | No |
| [Render text with DWriteCore](../../windows-app-sdk/dwritecore.md) | Yes | No |
| [Apply Mica or Acrylic to Win32 apps](ui/apply-mica-win32.md) | No | No |
| [Apply rounded corners](ui/apply-rounded-corners.md) | No | No |
| [Apply Windows themes](ui/apply-windows-themes.md) | No | No |

### App lifecycle

| Feature | Windows App SDK required? | Package identity required? |
|---|---|---|
| [Background tasks (Windows App SDK)](../../windows-app-sdk/applifecycle/background-tasks.md) | Yes | Yes |
| [Startup tasks](modernize-packaged-apps.md) | No | Yes |

### Windows integration

| Feature | Windows App SDK required? | Package identity required? |
|---|---|---|
| [Share sheet integration](../../develop/windows-integration/integrate-sharesheet-overview.md) | No | No |
| [Cross Device People API](../../develop/windows-integration/cross-device-people-api.md) | No | No |
| [File associations](desktop-to-uwp-extensions.md) | No | Yes |
| [Windows Widgets](../../develop/widgets/index.md) | No | Yes |

### Security

| Feature | Windows App SDK required? | Package identity required? |
|---|---|---|
| [Credential locker](../../develop/security/credential-locker.md) | No | No |
| [OAuth 2.0](../../develop/security/oauth2.md) | No | No |
| [Fingerprint biometrics](../../develop/security/fingerprint-biometrics.md) | No | No |
| [Smart cards](../../develop/security/smart-cards.md) | No | No |
| [Cryptography](../../develop/security/cryptography.md) | No | No |

### Resources

| Feature | Windows App SDK required? | Package identity required? |
|---|---|---|
| [MRT Core resource management](../../windows-app-sdk/mrtcore/mrtcore-overview.md) | Yes | No |

For the full list of platform features available in desktop apps, see [Windows app platform overview](../../develop/features-overview.md).

## AI-assisted modernization

[GitHub Copilot upgrade](/dotnet/core/porting/github-copilot-upgrade/overview) can assess and upgrade supported .NET desktop projects. For AI-assisted Windows feature adoption and UI migration, see [AI-assisted modernization](../../windows-app-sdk/migrate-to-windows-app-sdk/ai-modernize.md).

## Related content

- [Call Windows Runtime APIs in desktop apps](winrt-apis-desktop-apps.md)
- [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- [Packaging overview](../../package-and-deploy/packaging/index.md)
- [Choose your migration path](../../windows-app-sdk/migrate-to-windows-app-sdk/migration-decision-guide.md)
- [Migration terminology](../../windows-app-sdk/migrate-to-windows-app-sdk/migration-terminology.md)
- [Call interop APIs from a .NET app](winrt-com-interop-csharp.md)
- [Build a C# .NET app with WinUI and Win32 interop](../../winui/winui3/desktop-winui3-app-with-basic-interop.md)
