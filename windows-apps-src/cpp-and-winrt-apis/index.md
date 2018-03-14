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
| [Frequently-asked questions about C++/WinRT](faq.md) | Answers to questions that you're likely to have about authoring and consuming WinRT APIs with C++/WinRT. |
| [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md) | This topic describes essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT. |
| [String handling in C++/WinRT](strings.md) | With C++/WinRT, you can call WinRT APIs using standard C++ wide string types, or you can use the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring?branch=live) type. |
| [Standard C++ data types and C++/WinRT](std-cpp-data-types.md) | With C++/WinRT, you can call WinRT APIs using Standard C++ data types. |
| [Events; how to author and handle them in C++/WinRT](events-author-handle.md) | This topic demonstrates how to author a Windows Runtime Component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events. |
| [XAML controls; binding to a C++/WinRT property](binding-property.md) | A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it. |
| [XAML items controls; binding to a C++/WinRT collection](binding-collection.md) | A collection that can be effectively bound to a XAML items control is known as an *observable* collection. This topic shows how to implement and consume an observable collection, and how to bind a XAML items control to it. |
| [Boxing and unboxing scalar values to IInspectable with C++/WinRT](boxing.md) | A scalar value needs to be wrapped inside a reference class object before being passed to a function that expects **IInspectable**. That wrapping process is known as *boxing* the value. |
| [Troubleshooting C++/WinRT issues](troubleshooting.md) | The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app. |

## Important APIs
[winrt namespace (C++/WinRT)](/uwp/cpp-ref-for-winrt/winrt?branch=live)
