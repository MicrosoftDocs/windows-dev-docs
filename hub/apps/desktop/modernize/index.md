---
description: Learn how to modernize your existing WPF, Windows Forms, or Win32 desktop app with Windows App SDK features, MSIX packaging, and modern Windows APIs — without a full rewrite.
title: Modernize your desktop apps for Windows
ms.topic: overview
ms.date: 06/16/2026
ms.localizationpriority: medium
---

# Use modern Windows features in desktop apps

You don't need to rewrite your WPF, Windows Forms, or Win32 app to take advantage of modern Windows features. The Windows App SDK and the broader Windows platform offer modular capabilities you can adopt incrementally, at your own pace.

This article provides an overview of modern features that you can add to your desktop app by using:

- Windows Runtime (WinRT) APIs in the *[Windows SDK](../../windows-sdk/index.md)*
- The *[Windows App SDK](../../windows-app-sdk/index.md)*
- MSIX [packaging or package identity](../../package-and-deploy/packaging/index.md)

> [!NOTE]
> There are other aspects of app modernization that aren't covered here, like updating to a newer version of .NET or Visual Studio tooling. For more information about those aspects of modernization, start with:
>
> - **.NET:** [GitHub Copilot modernization for .NET](/dotnet/core/porting/github-copilot-app-modernization/)
> - **C++:** [Microsoft C++ porting and upgrading guide](/cpp/porting/visual-cpp-porting-and-upgrading-guide)

## Windows Runtime APIs

Many Windows platform [features](../../develop/index.md) — app notifications, the share contract, Bluetooth, and more — are exposed through [Windows Runtime (WinRT) APIs](/uwp/api). You can call these APIs directly from WPF, Windows Forms, and C++ Win32 apps.

**The first step in adding modern features to your app is configuring your project to use WinRT APIs. For instructions, see:**

> [!div class="nextstepaction"]
> [Call Windows Runtime APIs in desktop apps](winrt-apis-desktop-apps.md)

## Windows App SDK

The [Windows App SDK](../../windows-app-sdk/index.md) is how many modern Windows platform features are shipped. You can use the Windows App SDK in your existing app without changing your UI framework.

> [!IMPORTANT]
> These APIs use the WinRT API model, so be sure you've also configured your project to use WinRT APIs.

**Add the Windows App SDK NuGet package and call its APIs alongside your existing code. For instructions, see:**

> [!div class="nextstepaction"]
> [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)

## App packaging

Packaging defines how your app is installed, updated, and integrated with Windows. Choosing between a packaged or unpackaged app affects the features you can use, the deployment model you rely on, access to the Microsoft Store and enterprise deployment pipelines, and the overall experience your customers get.

**For more information, see:**

- [Packaging overview](../../package-and-deploy/packaging/index.md)
- [Features that require package identity](modernize-packaged-apps.md)
- [Integrate your desktop app with Windows using packaging extensions](desktop-to-uwp-extensions.md)

### Package with MSIX

Packaging your app with MSIX replaces your current installer. It gives you a modern, reliable installation experience, clean uninstall, automatic updates, and package identity. MSIX packaging is separate from modernizing your app's code — you can package a WPF, WinForms, or Win32 app with MSIX without changing any source code.

**For more information, see:**

- [Package your app using single-project MSIX](../../windows-app-sdk/single-project-msix.md)
- [Create an MSIX package from a desktop installer](/windows/msix/packaging-tool/create-app-package)
- [Build an MSIX package from your code](/windows/msix/desktop/source-code-overview)

### Features that require package identity

Some Windows platform features — including background tasks, app extensions, sharing targets, Windows AI Foundry APIs, file associations, and startup tasks — require your app to have a [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) at runtime. If you want to keep your current installer, you can still grant identity to an unpackaged app without full MSIX packaging. This approach is sometimes called a *sparse package* or *packaging with external location*.

**For more information, see:**

- [Grant identity to a non-packaged app](grant-identity-to-nonpackaged-apps-overview.md)

## Add modern features

Many features in [Features for Windows app development](../../develop/index.md) are available for WPF, WinForms, and Win32 apps. You can browse that section, or use this non-comprehensive list to jump directly to some common features.

- [Windows AI Foundry](/windows/ai/overview)
- [Using background tasks in Windows apps](../../windows-app-sdk/applifecycle/background-tasks.md)
- [Cross Device People API](../../develop/windows-integration/cross-device-people-api.md)
- [Integrate Share options in your Windows app](../../develop/windows-integration/integrate-sharesheet-overview.md)
- [Render text with DWriteCore](../../windows-app-sdk/dwritecore.md)
- [Manage resources with MRT Core](../../windows-app-sdk/mrtcore/mrtcore-overview.md)
- [Develop Windows Widgets](../../develop/widgets/index.md)
- [Credential locker for Windows apps](../../develop/security/credential-locker.md)
- [Cryptography](../../develop/security/cryptography.md)
- [Fingerprint biometrics](../../develop/security/fingerprint-biometrics.md)
- [Implement OAuth 2.0 in Windows apps](../../develop/security/oauth2.md)
- [Smart cards](../../develop/security/smart-cards.md)

## Migrate to WinUI 3

If you're planning a larger modernization effort — or building new features as separate modules — consider building new components with [WinUI 3](../../winui/winui3/index.md) and the Windows App SDK. WinUI 3 is the modern native UI framework for Windows desktop apps and is the recommended path for new development.

See [Create your first WinUI 3 app](../../get-started/start-here.md) to get started.
