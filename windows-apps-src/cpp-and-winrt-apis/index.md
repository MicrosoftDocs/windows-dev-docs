---
author: stevewhims
description: The Windows SDK includes C++/WinRT. This is a standard C++ language projection for WinRT APIs, implemented solely in header files, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume WinRT APIs using any standards-compliant C++ compiler.
title: C++/WinRT
ms.author: stwhi
ms.date: 03/01/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection
ms.localizationpriority: medium
---

# C++/WinRT
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

Introduced in Windows 10, version 1803, the Windows SDK includes C++/WinRT.

C++/WinRT is a standard C++ language projection for Windows Runtime (WinRT) APIs, implemented solely in header files, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume WinRT APIs using any standards-compliant C++ compiler.

| Topic | Description |
| - | - |
| [Introduction to C++/WinRT](intro-to-using-cpp-with-winrt.md) | An introduction to C++/WinRT&mdash;a standard C++ language projection for WinRT APIs. |
| [String handling in C++/WinRT](strings.md) | With C++/WinRT, you can call WinRT APIs using standard C++ wide string types, or you can use the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring?branch=live) type. |
| [Standard C++ data types and C++/WinRT](std-cpp-data-types.md) | With C++/WinRT, you can call WinRT APIs using Standard C++ data types. |
| [C++/WinRT runtime class instantiation, activation, and construction](ctors-runtimeclass-activation.md) | This topic describes two different ways to instantiate a runtime class with C++/WinRT. The way you go depends on whether the runtime class is implemented in the same compilation unit as the consuming code, or in a different one. |
| [Events; how to author and handle them in C++/WinRT](events-author-handle.md) | This topic demonstrates how to author a Windows Runtime Component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events. |
| [XAML; binding a control to C++/WinRT properties and collections](binding-prop-collection.md) | A property or a collection that can be effectively bound to a XAML control is known as an *observable* property or collection. This topic shows how to implement and consume observable properties and collections. |
| [Troubleshooting C++/WinRT issues](troubleshooting.md) | The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app. |

## Important APIs
[winrt namespace (C++/WinRT)](/uwp/cpp-ref-for-winrt/winrt?branch=live)
