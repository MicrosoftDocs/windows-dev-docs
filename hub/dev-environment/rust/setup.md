---
title: Set up your dev environment on Windows for Rust
description: Setting up your dev environment for beginners interested in developing on Windows with Rust.
ms.topic: how-to
keywords: rust, windows 10, microsoft, learning rust, rust on windows for beginners, rust with vs code
ms.localizationpriority: medium
ms.date: 09/28/2026
---

# Set up your Rust development environment on Windows

## Step 1: Install the MSVC C++ Build Tools

Rust on Windows requires Microsoft's C++ build tools as a prerequisite. If you already have [Visual Studio](https://visualstudio.microsoft.com/downloads/) installed with the **Desktop development with C++** workload, you can skip this step.

Otherwise, install the [Microsoft C++ Build Tools](https://visualstudio.microsoft.com/visual-cpp-build-tools/) and select the **Desktop development with C++** workload.

> [!NOTE]
> Use of the Microsoft C++ Build Tools requires a valid Visual Studio license (Community, Pro, or Enterprise). The Community edition is free for students, open-source contributors, and individuals.

## Step 2: Install Rust

> [!IMPORTANT]
> [Smart App Control](/windows/apps/develop/smart-app-control/overview) runs only code that it predicts to be safe or that is signed by a certificate authority in the Microsoft Trusted Root Program. The Rust toolchain for Windows is not currently Authenticode signed, so on a device that has Smart App Control turned on, the installed `cargo` and `rustc` binaries, and binaries produced by your build scripts, can be blocked from running. The block surfaces as `Application Control policy has blocked this file.` and is recorded in the **Applications and Services Logs > Microsoft > Windows > CodeIntegrity > Operational** event log; there's no other in-product signal, so the failure can look like a broken installer. Developer Mode and locally trusted certificates aren't exemptions. To check whether Smart App Control is on, go to **Settings > Privacy & security > Windows Security > App & browser control > Smart App Control settings**. For background, see [Smart App Control overview](/windows/apps/develop/smart-app-control/overview) and [Code signing options for Windows app developers](/windows/apps/package-and-deploy/code-signing-options).

The official Rust installer, `rustup`, handles everything — the compiler, Cargo, and future updates.

#### [WinGet](#tab/winget)

```powershell
winget install Rustlang.Rustup
```

#### [rustup.rs](#tab/rustup)

Download and run the installer from [rustup.rs](https://rustup.rs/). Accept the defaults to install the MSVC toolchain, which is recommended for Windows development.

---

After installation, open a new terminal and verify your setup:

```powershell
cargo --version
rustc --version
```

> [!TIP]
> To keep Rust up to date, run `rustup update` periodically.

## Step 3: Install Visual Studio Code and the Rust extension

1. Download and install [Visual Studio Code](https://code.visualstudio.com).
2. Install the [rust-analyzer extension](https://marketplace.visualstudio.com/items?itemName=rust-lang.rust-analyzer) from the VS Code Marketplace. This adds code completion, inline errors, go-to-definition, and debugging support.

## Next steps

> [!div class="nextstepaction"]
> [Learn Rust](https://www.rust-lang.org/learn)

- [Rust for Windows, and the windows crate](rust-for-windows.md) — call Windows APIs directly from Rust
- [Rust in Visual Studio Code](https://code.visualstudio.com/docs/languages/rust) — detailed VS Code Rust workflow
- [The Rust Programming Language book](https://doc.rust-lang.org/book/) — the official free Rust book
