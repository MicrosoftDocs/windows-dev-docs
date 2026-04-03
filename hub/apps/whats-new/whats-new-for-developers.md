---
title: What's new for Windows app developers
description: Learn what's new for developers in Windows 11 and tools
keywords: what's new, Windows 11, Windows, developers, WinUI, sdk, tools
ms.date: 04/03/2026
ms.topic: whats-new
ms.localizationpriority: medium
---

# What's new for developers

This section curates the latest platform capabilities, SDK and API additions, AI integration options, performance and diagnostics improvements, design guidance updates, and productivity tooling enhancements. Bookmark it and check back regularly: we refresh the highlights so you can focus on what moves your app forward.

---

## Latest releases

Find the latest downloads, release notes, and updates for the Windows SDK, Windows App SDK, and WinUI 3.

:::row:::
    :::column:::
        ![Windows App SDK icon](images/wasdk-hero.png)<br>
        **Windows App SDK**</br>
        Discover what's new</br>
        [View release notes](../windows-app-sdk/release-notes/windows-app-sdk-2-0.md)</br>
        [View downloads](../windows-app-sdk/downloads.md)
    :::column-end:::
    :::column:::
        ![Windows SDK icon](images/wsdk-hero.png)<br>
        **Windows SDK**<br>
        Discover what's new<br>
        [View release notes](../windows-sdk/release-notes.md)<br>
        [View downloads](../windows-sdk/downloads.md)
    :::column-end:::
:::row-end:::

---

## Highlights – March 2026

- **Agentic AI tools for Windows development**: New guide on enhancing AI coding agents with Windows-specific context — including the [Microsoft Learn MCP Server](/training/support/mcp) for live documentation access, and the [WinUI 3 development plugin for GitHub Copilot](https://github.com/github/awesome-copilot/tree/main/plugins/winui3-development) to generate accurate, modern WinUI 3 code — [Agentic AI tools for Windows](../dev-tools/agentic-tools.md).
- **WinApp CLI cross-framework guides**: The Windows App Development CLI (public preview) now has step-by-step guides for adding Windows capabilities to apps built with **.NET, C++, Rust, Flutter, Electron, and Tauri** — including packaging, identity, AI integration, and notifications — [WinApp CLI guides](../dev-tools/winapp-cli/guides/index.md).
- **MapControl for WinUI**: New documentation for the [MapControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol), an interactive map powered by Azure Maps with support for pins, layers, and user interaction — [MapControl guide](../develop/ui/controls/map-control.md).
- **Materials documentation**: New dedicated pages covering how to use Mica and Acrylic in your WinUI apps — [Materials overview](../develop/ui/materials.md) and [In-app acrylic](../develop/ui/in-app-acrylic.md).
- **Store API updates**: New sections documenting how to check if an unpackaged app is installed and how to open the Store product detail page for a product — [Send requests to the Store](/windows/uwp/monetize/send-requests-to-the-store).
- **Iconography documentation reorganized**: App icon and iconography docs have been consolidated into a dedicated [Iconography hub](../design/iconography/index.md) for easier navigation.
- **Windows notifications overview**: Confused by `AppNotificationManager` vs `ToastNotificationManager`? New overview page explains which notification API to use for your app type, with a feature comparison table and links to samples — [Windows notifications overview](../develop/notifications/index.md).
- **Choose a packaging model**: New scenario-based decision guide for choosing between packaged (MSIX), unpackaged, and sparse-package / external-location approaches — covering distribution options, feature requirements, and migration paths — [Choose a packaging model](../package-and-deploy/choose-packaging-model.md).
- **Performance docs for WinUI**: The full set of Windows app performance documentation — startup, memory, XAML layout, animations, ListView/GridView optimization, and more — is now available under the WinUI developer section at [Windows app performance](../develop/performance/index.md).
- **Visual layer and Composition docs for WinUI**: Documentation for the Visual layer (`Windows.UI.Composition`) — including visuals, animations, effects, brushes, lighting, and shadows — is now linked from the WinUI/Windows App SDK developer section.
- **Command Palette extension toolkit**: New API reference documentation for the PowerToys Command Palette extension toolkit, covering built-in commands (`CopyPathCommand`, `OpenFileCommand`, `OpenInConsoleCommand`, and more) and layout types — [Command Palette extension toolkit](/windows/powertoys/command-palette/microsoft-commandpalette-extensions-toolkit/).
- **Java getting started for Windows**: New guide for setting up a Java development environment on Windows, covering JDK installation, `JAVA_HOME` configuration, editor options, and WSL considerations — [Java on Windows](../dev-environment/java.md).
- **App features overview**: New landing page for the Features section of the Windows developer docs, with entry points to accessibility, AI, files, notifications, UI, and more — [Features for Windows app development](../develop/features-overview.md).

Previous highlights:

- **Segoe Fluent Icons Font updated**: The Segoe Fluent Icons Font documentation has been refreshed with the latest icon additions and usage guidance — [View icons](/windows/apps/design/style/segoe-fluent-icons-font).
- **WinUI terminology clarification**: Updated terminology across documentation for clarity — "WinUI 2" is now referred to as "WinUI for UWP" and "WinUI 3" is now simply "WinUI" to reflect current naming conventions.
- **Windows Developer Support hub**: New centralized support page with quick help actions, community channels, and Microsoft support contacts — [Get support](/windows/apps/develop/support).
- **PowerToys** 0.97.2 release with new features and improvements to existing tools — [Microsoft PowerToys: Utilities to customize Windows](/windows/powertoys/).
- **Windows App SDK release notes**: Refactored and consolidated history from 0.5 through 2.0 — find the latest fixes and APIs in one place ([release notes hub](../windows-app-sdk/release-notes/windows-app-sdk-2-0.md)).
- **Windows SDK updates**: New overview and detailed release notes to track SDK changes ([overview](/windows/apps/windows-sdk/) · [release notes](/windows/apps/windows-sdk/release-notes)).
- **WinAppCLI public preview**: The Windows App Development CLI is a command-line interface for managing Windows SDKs, packaging, generating app identity, manifests, certificates, and using build tools with any app framework — ([GitHub repo](https://github.com/microsoft/WinAppCli)).
- **Cross Device Resume (XDR) overview**: Introduces Windows app continuity across devices and technologies available to enable XDR scenarios ([overview](../develop/windows-integration/cross-device-resume-overview.md)).
- **Implement XDR using WNS raw notifications**: Step-by-step guide to integrate app continuity via WNS, including prerequisites and Python/JavaScript examples ([how-to](../develop/windows-integration/integrate-app-continuity.md)).
- **AI on Windows**: Fresh overview for building AI‑powered experiences on Windows, with entry points to APIs and tooling ([Windows AI overview](/windows/ai/)).
- **Packaged vs. unpackaged guidance**: Updated guidance for choosing and configuring packaged or unpackaged apps, including WinUI scenarios ([decision guide](../get-started/intro-pack-dep-proc.md)).
- **UWP capability declarations**: Revised topic clarifying capability types, privacy‑sensitive capabilities, and Store submission considerations ([declare capabilities](/windows/uwp/packaging/app-capability-declarations)).
- **Microsoft Store**: The latest news from the [Microsoft Store](/windows/apps/publish/whats-new-individual-developer) including waived fees and updated analytics.


### Documentation

| Feature | Description |
| :------ | :------ |
| [Start developing Windows apps](/windows/apps/get-started/start-here) | Comprehensive starting point for Windows app development. |
| [Win32 app isolation overview](/windows/win32/secauthz/app-isolation-overview) | Security and reliability benefits of isolating Win32 apps. |
| [AppWindow.SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon) | API reference for setting a window icon (Windows App SDK). |
| [Get started with Windows AI APIs](/windows/ai/apis/get-started) | Quickstart building apps using Windows AI. |
| [Agentic AI tools for Windows](../dev-tools/agentic-tools.md) | Connect AI coding agents to Windows docs and WinUI 3 best practices. |
| [WinApp CLI overview](../dev-tools/winapp-cli/index.md) | Command-line tool for packaging, identity, and SDK management across frameworks. |
| [MapControl](../develop/ui/controls/map-control.md) | Interactive Azure Maps-powered map control for WinUI apps. |
| [Materials in Windows apps](../develop/ui/materials.md) | Overview of Mica and Acrylic materials for WinUI. |
| [Windows notifications overview](../develop/notifications/index.md) | Which notification API to use: AppNotificationManager vs ToastNotificationManager. |
| [Choose a packaging model](../package-and-deploy/choose-packaging-model.md) | Scenario-based guide for choosing packaged, unpackaged, or sparse packaging. |
| [Windows app performance](../develop/performance/index.md) | Performance docs for WinUI apps: startup, memory, XAML layout, and more. |
| [Command Palette extension toolkit](/windows/powertoys/command-palette/microsoft-commandpalette-extensions-toolkit/) | API reference for building PowerToys Command Palette extensions. |
| [Java on Windows](../dev-environment/java.md) | Set up a Java development environment on Windows. |
| [Features for Windows app development](../develop/features-overview.md) | Landing page for Windows platform features: accessibility, AI, files, notifications, and more. |
