---
title: Overview of developing on Windows with Rust
description: An overview for beginners interested in developing on Windows with Rust.
ms.topic: concept-article
keywords: rust, windows 10, microsoft, learning rust, rust on windows for beginners, rust with vs code
ms.localizationpriority: medium
ms.date: 09/28/2026
---

# Overview of developing on Windows with Rust

[Rust](https://www.rust-lang.org/) is a systems programming language designed for performance, reliability, and memory safety — without a garbage collector. It is widely used for operating systems, command-line tools, web servers, and anywhere low-level control matters.

Rust has topped Stack Overflow's [annual developer survey](https://survey.stackoverflow.co/) as the most-admired language for many years running, and Microsoft is a founding member of the [Rust Foundation](https://foundation.rust-lang.org/).

## Key concepts

- **Cargo** — Rust's package manager and build tool. You will use it for almost everything: creating projects, adding dependencies, building, and testing.
- **crate** — the basic unit of compilation in Rust. A crate can be a binary (executable) or a library.
- **rustup** — the official installer and version manager for the Rust toolchain. It keeps your compiler and tools up to date.
- **crates.io** — the community package registry at [crates.io](https://crates.io/).

## Next steps

> [!NOTE]
> Before you install the toolchain, review the Smart App Control compatibility note in [Set up your Rust development environment on Windows](setup.md#step-2-install-rust). On a device where Smart App Control is running in enforcement mode, the unsigned Rust toolchain can be blocked from running.

- [Set up your Rust development environment on Windows](setup.md)
- [Rust for Windows, and the windows crate](rust-for-windows.md)
- [Rust in Visual Studio Code](https://code.visualstudio.com/docs/languages/rust)
