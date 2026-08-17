---
title: WinRT projections for C# and C++
description: Learn how Windows Runtime language projections let C++ and C# apps consume and author WinRT APIs and reusable components.
ms.topic: concept-article
ms.date: 08/14/2026

#customer intent: As a Windows developer, I want to understand how WinRT language projections work so that I can decide whether to consume or author WinRT APIs and components in C# or C++.
---

# Windows Runtime (WinRT) language projections

Windows Runtime (WinRT) is a language-neutral application architecture for building Windows APIs and components. A *language projection* exposes WinRT APIs according to the conventions of a specific programming language, so your C# or C++ code can call them like other APIs in your language.

Understanding how projections work helps you decide whether to consume WinRT APIs in your app or author your own reusable WinRT components. This article explains how a language projection maps WinRT metadata to C# and C++, which projections Microsoft supports, and when you need the projection documentation.

## How language projections work

Windows Metadata (`.winmd`) files define WinRT APIs. A language projection reads that metadata and exposes the APIs according to the conventions of a specific programming language. When you create a project from a standard Windows App SDK or WinUI template, the template already configures the language projection, and your C# or C++ code can call WinRT APIs directly without working with the underlying metadata or application binary interface (ABI).

## Supported language projections

Microsoft supports C# and C++ language projections for Windows App SDK and WinUI apps. Community projects provide projections for other languages, such as Rust, but Microsoft doesn't officially support them.

| Projection | Language | What it provides |
|---|---|---|
| [C++/WinRT](../cpp-winrt/index.md) | C++17 or later | A standard C++ language projection for consuming and authoring WinRT APIs and components. |
| [C#/WinRT](csharp-winrt/index.md) | C# (.NET) | A .NET language projection for consuming WinRT APIs and authoring WinRT components. |

## When to use the language projection documentation

For most app development, you can use projected WinRT APIs like other APIs in your chosen language. Use the projection documentation when you:

- **Author a Windows Runtime component** that other apps or languages consume.
- **Distribute a library** as a NuGet package that wraps WinRT APIs.
- **Work with low-level COM or ABI interfaces** from C++ code.
- **Troubleshoot build errors** related to generated interop code or `.winmd` files.

## Related content

- [Get started with C++/WinRT](../cpp-winrt/get-started.md)
- [Learn about C#/WinRT](csharp-winrt/index.md)
- [Create a C#/WinRT component](csharp-winrt/create-windows-runtime-component-cswinrt.md)
