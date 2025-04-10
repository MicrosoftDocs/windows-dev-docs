---
title: Overview of developing on Windows with Rust
description: An overview for beginners interested in developing on Windows with Rust.
ms.topic: article
keywords: rust, windows 10, microsoft, learning rust, rust on windows for beginners, rust with vs code
ms.localizationpriority: medium
ms.date: 03/04/2021
---

# Overview of developing on Windows with Rust

It's not hard to get started with [Rust](https://www.rust-lang.org/). If you're a beginner who's interested in learning Rust using Windows, then we recommend that you follow each detail of this step-by-step guide. It shows you what to install, and how to set up your development environment.

> [!TIP]
> If you're already sold on Rust and you have your Rust environment already set up, and you just want to start calling Windows APIs, then feel free to jump forward to the [Rust for Windows, and the windows crate](rust-for-windows.md) topic.

## What is Rust?

Rust is a systems programming language, so it's used for writing systems (such as operating systems). But it can also be used for applications where performance and trustworthiness are important. The Rust language syntax is comparable to that of C++, provides performance on par with modern C++, and for many experienced developers Rust hits all the right notes when it comes to compilation and runtime model, type system, and deterministic finalization.

In addition, Rust is designed around the promise of guaranteed memory safety, without the need for garbage collection.

So why did we choose Rust for the latest language projection for Windows? One factor is that Stack Overflow's [annual developer survey](https://insights.stackoverflow.com/survey) shows Rust to be the best-loved programming language by far, year after year. While you might find that the language has a steep learning curve, once you're over the hump it's hard not to fall in love.

Furthermore, Microsoft is a founding member of the [Rust Foundation](https://foundation.rust-lang.org/). The Foundation is an independent non-profit organization, with a new approach to sustaining and growing a large, participatory, open source ecosystem.

## The pieces of the Rust development toolset/ecosystem

We'll introduce some Rust tools and terms in this section. You can refer back here to refresh yourself on any of the descriptions.

* A *crate* is a Rust unit of compilation and linking. A crate can exist in source code form, and from there it can be processed into a crate in the form of either a binary executable (*binary* for short), or a binary library (*library* for short).
* A Rust project is known as a *package*. A package contains one or more crates, together with a `Cargo.toml` file that describes how to build those crates.
* `rustup` is the installer and updater for the Rust toolchain.
* *Cargo* is the name of Rust's package management tool.
* `rustc` is the compiler for Rust. Most of the time, you won't invoke `rustc` directly; you'll invoke it indirectly via Cargo.
* *crates.io* (`https://crates.io/`) is the Rust community's crate registry.

## Setting up your development environment

In the next topic, we'll see how to [set up your dev environment on Windows for Rust](setup.md).

## Related

* [The Rust website](https://www.rust-lang.org/)
* [Rust for Windows, and the windows crate](rust-for-windows.md)
* [Stack Overflow annual developer survey](https://insights.stackoverflow.com/survey)
* [Rust Foundation](https://foundation.rust-lang.org/)
* [crates.io](https://crates.io/)
* [Set up your dev environment on Windows for Rust](setup.md)
