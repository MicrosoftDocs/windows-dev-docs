---
title: Coming to Windows development
description: A practical guide for developers new to Windows with paths for macOS, Linux, web, mobile, and .NET backgrounds covering tools and key concepts.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 07/22/2026
ms.localizationpriority: medium
---

# Coming to Windows development

Windows has a rich development ecosystem spanning native desktop apps, web experiences, games, and cross-platform solutions. Whether you're switching from macOS, bringing a Linux workflow to Windows, or extending an existing web or mobile codebase, this guide helps you find your footing quickly.

Pick the path that matches your background:

- [Coming from macOS](#coming-from-macos)
- [Coming from Linux](#coming-from-linux)
- [Coming from web development](#coming-from-web-development)
- [Coming from iOS or Android development](#coming-from-ios-or-android-development)
- [Coming from .NET on another platform](#coming-from-net-on-another-platform)

---

## Coming from macOS

You're comfortable with Xcode, Homebrew, the Terminal, and building apps that feel native on the platform they run on. On Windows, that same instinct for native quality has a clear path.

**Your tools map to these Windows equivalents:**

| macOS | Windows |
|-------|---------|
| Homebrew | [WinGet](../../package-manager/winget/index.md) |
| Xcode | [Visual Studio 2026](/visualstudio/) |
| iTerm2 / Terminal.app | [Windows Terminal](/windows/terminal/) |
| zsh / bash | [PowerShell](/powershell/) |
| Spotlight | Windows Search / [PowerToys Run](https://aka.ms/powertoys) |
| Finder | File Explorer |
| launchd plists | Windows Services / Task Scheduler |
| `~/Library/Application Support` | `%APPDATA%` |

**Build native Windows apps with WinUI 3**

[WinUI 3](/windows/apps/winui/winui3/) is the recommended UI framework for new Windows desktop apps—the equivalent of SwiftUI or AppKit for Windows. It uses [Fluent Design](../design/index.md) components, supports light/dark themes automatically, and runs on Windows 10 version 1809 and later.

Get started in one command:

```powershell
winget configure -f https://aka.ms/winui-config
```

This installs Visual Studio 2026 with the required workloads and enables Developer Mode. Then see [Quick start: Create your first WinUI 3 app](../get-started/start-here.md).

**Key differences to be aware of**

- File paths use backslash (`\`) by default, but PowerShell and most tools also accept forward slashes (`/`).
- File paths are **case-insensitive by default** on Windows (though per-directory case sensitivity can be enabled).
- Apps are distributed as [MSIX packages](/windows/msix/) (similar to `.app` bundles) or through the [Microsoft Store](/windows/apps/publish/).
- There is no direct equivalent of the macOS Keychain for storing secrets; use the [Windows Credential Manager](/windows/win32/secauthn/credentials-management) or [Azure Key Vault](https://azure.microsoft.com/products/key-vault/) for apps.
- In Windows Terminal or PowerShell, use `explorer .` to open the current folder in File Explorer — the equivalent of `open .` in macOS Terminal.

---

## Coming from Linux

You're at home in a terminal, comfortable with shell scripts, package managers, and build toolchains. Windows has strong support for Linux-style workflows without sacrificing access to the Windows platform.

**Run your existing Linux tools with WSL**

[Windows Subsystem for Linux (WSL)](/windows/wsl/) runs a full Linux distribution alongside Windows. Your existing bash scripts, makefiles, gcc/clang toolchains, Docker workflows, and Python environments work unchanged.

```powershell
wsl --install
```

After installation, open Ubuntu from [Windows Terminal](/windows/terminal/). Your Windows files are accessible at `/mnt/c/`. [VS Code](https://code.visualstudio.com/) connects to your WSL environment via the [WSL extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-wsl).

**Your tools map to these Windows equivalents:**

| Linux | Windows |
|-------|---------|
| apt / dnf / pacman | [WinGet](../../package-manager/winget/index.md) |
| bash / zsh | [PowerShell](/powershell/) (or bash inside WSL) |
| GNOME Terminal / Konsole | [Windows Terminal](/windows/terminal/) |
| systemd units | Windows Services / `sc.exe` |
| `/home/<user>` | `C:\Users\<username>` (`$HOME` in PowerShell) |
| `~/.config` | `%APPDATA%` |
| `/tmp` | `%TEMP%` |
| `make install` → `/usr/local/bin` | Add directory to `%PATH%` |

**Install packages with WinGet**

```powershell
winget install Git.Git
winget install Microsoft.VisualStudioCode
winget install Docker.DockerDesktop
```

**Build native Windows apps with C++ or C#**

For system-level work, [Win32 APIs](/windows/win32/) are the Windows equivalent of POSIX. For GUI apps, [WinUI 3](/windows/apps/winui/winui3/) with C++ or C# gives you native performance and platform integration. See [Quick start: Create your first WinUI 3 app](../get-started/start-here.md).

> [!TIP]
> For heavy build workloads (compiling large C++ or Rust projects), store source code on a [Dev Drive](/windows/dev-drive/) volume. Dev Drive uses the Resilient File System (ReFS) with developer-optimized settings and can significantly reduce build times.

**Line endings**

Windows uses CRLF (`\r\n`); Linux uses LF (`\n`). Add a `.gitattributes` file to your repo to avoid issues:

```
* text=auto
*.sh text eol=lf
*.ps1 text eol=crlf
```

**Tips and gotchas**

- WSL lets you run `code .` from a Linux shell to open VS Code on Windows with full access to your Linux filesystem — no copying files between environments.
- Use `pip install` in WSL for your training environment, but ship inference via the [ONNX Runtime](https://onnxruntime.ai/) NuGet package in your Windows app — same model file, two runtimes, zero rework.

---

## Coming from web development

You build with JavaScript, TypeScript, React, Node.js, or similar technologies. Windows is a great platform for web development, and you have multiple options for reaching the Windows desktop.

**Set up a web dev environment**

[WSL](/windows/wsl/) lets you run Node.js, npm, and your existing shell scripts natively. Most web developers on Windows use WSL for server-side tooling and Windows Terminal for everything else.

```powershell
wsl --install
winget install Microsoft.VisualStudioCode
```

See [Set up Node.js on Windows](/windows/dev-environment/javascript/nodejs-on-windows) for a full walkthrough.

**Reach the Windows desktop from web technologies**

You have several options for shipping a Windows app from a web codebase:

| Approach | Best for |
|----------|----------|
| [Progressive Web App (PWA)](/microsoft-edge/progressive-web-apps-chromium/) | Existing web app, light Windows presence |
| [WebView2](/microsoft-edge/webview2/) | Embedding web UI in a native app shell |
| [React Native for Windows](/windows/dev-environment/javascript/react-native-for-windows) | Shared React codebase targeting Windows and other platforms |
| [Electron on Windows](https://www.electronjs.org/docs/latest/tutorial/windows-taskbar) | Existing Electron app shipping on Windows |

**React Native for Windows** is the most direct path if you're already writing React Native for iOS and Android. Add Windows as a target platform with:

```powershell
npx react-native init MyApp
cd MyApp
npx react-native-windows-init --overwrite
```

See [React Native for Windows – Getting Started](/windows/dev-environment/javascript/react-native-for-windows) for a full guide.

**Key Windows platform features for web developers**

- [Windows Notifications](/windows/apps/windows-app-sdk/notifications/app-notifications/): Toast notifications that appear in the Action Center.
- [Share target](../develop/windows-integration/integrate-sharesheet-overview.md): Let users share content from other apps to yours.
- [File type associations](/windows/apps/develop/launch/): Open files directly in your app from File Explorer.

**Tips and gotchas**

- Use Edge DevTools (F12) to debug your WebView2 content with the same tools you already use for web development — breakpoints, network inspector, and console all work identically.
- You can keep your existing `package.json` scripts — MSBuild can invoke npm/node as a pre-build step, so your existing web build pipeline still works alongside a native app shell.

---

## Coming from iOS or Android development

You're used to a simulator, a mobile-first design mindset, and deploying through an app store. Windows has equivalents for each—and a familiar app lifecycle model.

**Your tools map to these Windows equivalents:**

| iOS / Android | Windows |
|---------------|---------|
| Xcode / Android Studio | [Visual Studio 2026](/visualstudio/) |
| Swift / Kotlin | C# (with [Windows App SDK](../windows-app-sdk/index.md)) |
| SwiftUI / Jetpack Compose | [WinUI 3](/windows/apps/winui/winui3/) (XAML + Fluent Design) |
| Simulator / Emulator | Deploy and debug directly on Windows |
| App Store / Google Play | [Microsoft Store](/windows/apps/publish/) |
| `.ipa` / `.apk` | [MSIX](/windows/msix/) package |
| `UserDefaults` / `SharedPreferences` | [ApplicationData](/uwp/api/windows.storage.applicationdata) (requires package identity) or local settings |
| Push notifications (APNs / FCM) | [Windows Push Notification Services (WNS)](/windows/apps/windows-app-sdk/notifications/push-notifications/) |

**Build cross-platform with .NET MAUI**

If you're targeting Windows, iOS, and Android from a single codebase, [.NET MAUI](/dotnet/maui/) is the recommended framework. You write C# and XAML once and deploy to all platforms.

```powershell
winget install Microsoft.DotNet.SDK.9
dotnet new maui -n MyApp
cd MyApp
dotnet run -f net9.0-windows10.0.19041.0
```

**Build a native Windows app with WinUI 3**

For a Windows-only experience that takes full advantage of Fluent Design, notifications, and platform APIs, [WinUI 3](../get-started/start-here.md) is the native choice. The development model—XAML markup, a code-behind file, an app lifecycle, and a Store packaging path—will feel familiar.

**App lifecycle on Windows**

WinUI 3 desktop apps do not have a managed lifecycle the way iOS and Android apps do. There is no `applicationDidEnterBackground` equivalent. Apps run as normal Windows processes. For background work, use [background execution in Windows App SDK](../develop/launch/background-execution.md) or [Windows Services](/windows/win32/services/services).

**Tips and gotchas**

- Use `winget` from the command line to install dev tools quickly — it's similar to using `sdkmanager` for Android SDK components. For example: `winget install Microsoft.VisualStudio.2022.Community`.

---

## Coming from .NET on another platform

You write C# or F# and use .NET on macOS or Linux. Most of your skills transfer directly—the main adjustment is learning which Windows-specific APIs and UI frameworks to use.

**Your .NET code already runs on Windows**

.NET 8 and later are cross-platform. Your class libraries, ASP.NET Core services, console apps, and worker services run on Windows without modification. The Windows-specific differences are:

- **UI frameworks**: On Windows, you have [WinUI 3](/windows/apps/winui/winui3/), [WPF](/dotnet/desktop/wpf/overview/), and [WinForms](/dotnet/desktop/winforms/overview/) in addition to [.NET MAUI](/dotnet/maui/).
- **Platform APIs**: The [Windows App SDK](../windows-app-sdk/index.md) exposes Windows-specific capabilities—notifications, windowing, file pickers, sharing, push notifications—as NuGet packages.
- **Packaging**: Windows apps are distributed as [MSIX packages](/windows/msix/), not `.deb`, `.rpm`, or `.dmg` files.

**Choose the right UI framework**

| Scenario | Recommended framework |
|----------|-----------------------|
| New Windows desktop app | [WinUI 3](../get-started/start-here.md) |
| Windows + macOS + iOS + Android | [.NET MAUI](/dotnet/maui/) |
| Existing WPF app, add new features | [WPF + Windows App SDK](/windows/apps/desktop/modernize/desktop-to-uwp-enhance) |
| Existing WinForms app | [WinForms + Windows App SDK](/windows/apps/desktop/modernize/) |
| Web app with Windows presence | [ASP.NET Core](/aspnet/core/) + [WebView2](/microsoft-edge/webview2/) |

**Get set up**

Install the .NET SDK and Visual Studio with the WinUI workload:

```powershell
winget configure -f https://aka.ms/winui-config
```

Then follow the [Quick start: Create your first WinUI 3 app](../get-started/start-here.md).

**Key Windows-specific NuGet packages**

- `Microsoft.WindowsAppSDK` — windowing, notifications, app lifecycle, push notifications
- `Microsoft.Windows.SDK.BuildTools` — Windows SDK build support
- `Microsoft.Toolkit.Uwp.Notifications` — toast notification builder (legacy; prefer `Microsoft.WindowsAppSDK` for new apps)

---

## Windows platform concepts for everyone

Regardless of your background, a few Windows-specific concepts come up early.

### Package management with WinGet

[WinGet](../../package-manager/winget/index.md) installs apps and developer tools from the command line:

```powershell
winget install Git.Git
winget install Microsoft.VisualStudioCode
winget install OpenJS.NodeJS
winget search python
```

Use a [WinGet Configuration file](../../package-manager/configuration/index.md) to reproduce your entire dev environment from a single YAML file—ideal for onboarding or setting up a new machine.

### Developer Mode

Enable [Developer Mode](/windows/advanced-settings/developer-mode) before running or sideloading apps you build. Open Windows Settings > **System** > **Advanced** and toggle **Developer Mode** to **On**, or run:

```powershell
# Requires elevation
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\AppModelUnlock" /t REG_DWORD /f /v "AllowDevelopmentWithoutDevLicense" /d "1"
```

### Packaging and distribution

Windows apps are distributed as [MSIX packages](/windows/msix/) or through traditional installers. MSIX provides clean install/uninstall, automatic updates via the Store, and app isolation.

- **Microsoft Store**: Submit packaged apps for discovery and distribution. See [Publish Windows apps and games](/windows/apps/publish/).
- **WinGet**: Publish to the [winget-pkgs community repository](https://github.com/microsoft/winget-pkgs) so users can install your app with `winget install`.

### AI-assisted development tools

Several Windows developer tools include AI features:

- **GitHub Copilot in Visual Studio / VS Code**: Inline completions, chat, and multi-file edits.
- **Intelligent Terminal**: An experimental Windows Terminal build with a built-in agent pane — see [Announcing Intelligent Terminal version 0.1](https://devblogs.microsoft.com/commandline/announcing-intelligent-terminal-version-0-1/).
- **Windows Development Skills**: Structured knowledge that lets AI agents build native Windows apps end-to-end — see [Get started with Windows Development Skills](https://aka.ms/winui-skills).
- **WinApp CLI**: A command-line tool for scaffolding, packaging, and deploying Windows apps, designed for automation and AI agent workflows.

See [AI-assisted development for Windows](../develop/ai-assisted/index.md) for more.

---

## Next steps

- [Quick start: Create your first WinUI 3 app](../get-started/start-here.md)
- [Set up your development environment](../../dev-environment/index.md)
- [Windows developer FAQ](../get-started/windows-developer-faq.md)
- [Windows developer glossary](../get-started/windows-developer-glossary.md)
- [Windows developers and their tools](./personae.md)
