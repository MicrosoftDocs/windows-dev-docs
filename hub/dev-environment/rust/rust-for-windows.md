---
title: Rust for Windows, and the windows crate
description: Using the *windows* crate, and calling Windows APIs.
author: stevewhims
ms.author: stwhi
manager: jken
ms.topic: article
keywords: rust, windows 10, microsoft, learning rust, rust on windows for beginners, rust with vs code, rust for windows
ms.localizationpriority: medium
ms.date: 08/11/2023
---

# Rust for Windows, and the *windows* crate

&nbsp;
> [!VIDEO https://www.youtube.com/embed/-oZrsCPKsn4]

## Introducing Rust for Windows

In the [Overview of developing on Windows with Rust](overview.md) topic, we demonstrated a simple app that outputs a *Hello, world!* message. But not only can you use Rust *on* Windows, you can also write apps *for* Windows using Rust.

You can find all of the latest updates in the [Release log of the Rust for Windows repo](https://github.com/microsoft/windows-rs/releases) on GitHub.

Rust for Windows lets you use any Windows API (past, present, and future) directly and seamlessly via [the *windows* crate](https://crates.io/crates/windows) (*crate* is Rust's term for a binary or a library, and/or the source code that builds into one).

Whether it's timeless functions such as [CreateEventW](/windows/win32/api/synchapi/nf-synchapi-createeventw) and [WaitForSingleObject](/windows/win32/api/synchapi/nf-synchapi-waitforsingleobject), powerful graphics engines such as [Direct3D](/windows/win32/direct3d12/directx-12-programming-guide), traditional windowing functions such as [CreateWindowExW](/windows/win32/api/winuser/nf-winuser-createwindowexw) and [DispatchMessageW](/windows/win32/api/winuser/nf-winuser-dispatchmessagew), or more recent user interface (UI) frameworks such as [Composition](/uwp/api/windows.ui.composition), [the *windows* crate](https://crates.io/crates/windows) has you covered.

The [win32metadata](https://github.com/microsoft/win32metadata) project aims to provide metadata for Win32 APIs. This metadata describes the API surface&mdash;strongly-typed API signatures, parameters, and types. This enables the entire Windows API to be projected in an automated and complete way for consumption by Rust (as well as languages such as C# and C++). Also see [Making Win32 APIs more accessible to more languages](https://blogs.windows.com/windowsdeveloper/2021/01/21/making-win32-apis-more-accessible-to-more-languages/).

As a Rust developer, you'll use Cargo (Rust's package management tool)&mdash;along with `https://crates.io` (the Rust community's crate registry)&mdash;to manage the dependencies in your projects. The good news is that you can reference [the *windows* crate](https://crates.io/crates/windows) from your Rust apps, and then immediately begin calling Windows APIs. You can also find Rust [documentation for the *windows* crate](https://docs.rs/windows/latest/windows/) over on `https://docs.rs`.

Similar to [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/), [Rust for Windows](https://github.com/microsoft/windows-rs) is an open source language projection developed on GitHub. Use the [Rust for Windows](https://github.com/microsoft/windows-rs) repo if you have questions about Rust for Windows, or if you wish to report issues with it.

The Rust for Windows repo also has [some simple examples](https://github.com/microsoft/windows-rs/tree/master/crates/samples) that you can follow. And there's an excellent sample app in the form of Robert Mikhayelyan's [Minesweeper](https://github.com/robmikh/minesweeper-rs).

## Contribute to Rust for Windows

[Rust for Windows](https://github.com/microsoft/windows-rs) welcomes your contributions!

## Rust documentation for the Windows API

Rust for Windows benefits from the polished toolchain that Rust developers enjoy. But if having the entire Windows API at your fingertips seems a little daunting, there's also [Rust documentation for the Windows API](https://microsoft.github.io/windows-docs-rs/doc/windows/).

This resource essentially documents how the Windows APIs and types are projected into idiomatic Rust. Use it to browse or search for the APIs you need to know about, and that you need to know how to call.

## Writing an app with Rust for Windows

The next topic is the [RSS reader tutorial](rss-reader-rust-for-windows.md), where we'll walk through writing a simple app with Rust for Windows.

## Related

* [Overview of developing on Windows with Rust](overview.md)
* [RSS reader tutorial](rss-reader-rust-for-windows.md)
* [The *windows* crate](https://crates.io/crates/windows)
* [Documentation for the *windows* crate](https://docs.rs/windows/0.3.1/windows/)
* [Win32 metadata](https://github.com/microsoft/win32metadata)
* [Making Win32 APIs more accessible to more languages](https://blogs.windows.com/windowsdeveloper/2021/01/21/making-win32-apis-more-accessible-to-more-languages/)
* [Rust documentation for the Windows API](https://microsoft.github.io/windows-docs-rs/doc/windows/)
* [Rust for Windows](https://github.com/microsoft/windows-rs)
* [Minesweeper sample app](https://github.com/robmikh/minesweeper-rs)
