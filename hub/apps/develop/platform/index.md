---
title: Platform overview
description: Learn about the language projections and UI framework that Windows App SDK apps are built on, including C++/WinRT, C#/WinRT, and XAML.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 07/08/2026
---

# Windows App SDK platform overview

The Windows App SDK supports two languages for app development: **C#** and **C++**. Each language uses a *language projection* to call Windows Runtime (WinRT) APIs, and both use **XAML** (specifically WinUI 3 XAML, in the `Microsoft.UI.Xaml` namespace) to define the user interface. Together, these projections and the UI framework form the platform layer.

> [!NOTE]
> C# and C++ are the only languages with supported projections for Windows App SDK and WinUI 3. Community projects exist for other languages (such as Rust), but they are not officially supported.

## Do you need to know about this?

If you create a project from the standard Visual Studio templates for Windows App SDK or WinUI 3, the language projection is already configured for you. Your C# or C++ code can call WinRT APIs directly, and you don't need to think about how the projection works.

You typically need to look deeper into the projection layer when you:

- **Author a Windows Runtime component** that other apps or languages consume.
- **Distribute a library** as a NuGet package that wraps WinRT APIs.
- **Interop with low-level COM or ABI interfaces** from C++ code.
- **Troubleshoot build errors** related to generated interop code or WinMD files.

For everyday app development, the details below are background knowledge — useful if you're curious, but not required to get started.

## Language projections

Windows Runtime APIs are defined in a language-neutral way using Windows Metadata (`.winmd`) files. A language projection reads that metadata and generates idiomatic bindings so you can call the APIs naturally from C++ or C#.

| Projection | Language | What it is | Namespace pattern |
|---|---|---|---|
| **[C++/WinRT](../cpp-winrt/index.md)** | C++17 | A header-only library that provides first-class access to WinRT APIs. It replaces C++/CX and the Windows Runtime C++ Template Library (WRL). | `winrt::Microsoft::UI::Xaml` |
| **[C#/WinRT](csharp-winrt/index.md)** | C# (.NET) | A NuGet-packaged toolkit that generates .NET interop assemblies from `.winmd` files. It enables you to consume, and author, WinRT components from C#. | `Microsoft.UI.Xaml` |

## UI framework

| Framework | What it is | Namespace |
|---|---|---|
| **[XAML (WinUI 3)](xaml/index.md)** | The declarative XML-based markup language for WinUI 3 user interfaces. Handles layout, styling, data binding, and animations. Code-behind is written in C# or C++. | `Microsoft.UI.Xaml` (not `Windows.UI.Xaml`, which is UWP) |

> [!IMPORTANT]
> WinUI 3 XAML uses the `Microsoft.UI.Xaml` namespace. If you see code or documentation referencing `Windows.UI.Xaml`, that is UWP XAML, which is a different framework. Do not mix the two namespaces in a WinUI 3 project.

## Next steps

Choose the area that matches what you want to learn:

- [Get started with C++/WinRT](../cpp-winrt/get-started.md)
- [Create a C#/WinRT component](csharp-winrt/create-windows-runtime-component-cswinrt.md)
- [XAML overview](xaml/xaml-overview.md)
