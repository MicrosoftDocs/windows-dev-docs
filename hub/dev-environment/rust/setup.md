---
title: Set up your dev environment on Windows for Rust
description: Setting up your dev environment for beginners interested in developing on Windows with Rust.
ms.topic: how-to
keywords: rust, windows 10, microsoft, learning rust, rust on windows for beginners, rust with vs code
ms.localizationpriority: medium
ms.date: 03/24/2026
---

# Set up your Rust development environment on Windows

## Step 1: Install the MSVC C++ Build Tools

Rust on Windows requires Microsoft's C++ build tools as a prerequisite. If you already have [Visual Studio](https://visualstudio.microsoft.com/downloads/) installed with the **Desktop development with C++** workload, you can skip this step.

Otherwise, install the [Microsoft C++ Build Tools](https://visualstudio.microsoft.com/visual-cpp-build-tools/) and select the **Desktop development with C++** workload.

> [!NOTE]
> Use of the Microsoft C++ Build Tools requires a valid Visual Studio license (Community, Pro, or Enterprise). The Community edition is free for students, open-source contributors, and individuals.

## Step 2: Install Rust

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
