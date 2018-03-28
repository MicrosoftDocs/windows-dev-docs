---
author: stevewhims
description: The Windows SDK includes C++/WinRT. This is a standard C++ language projection for WinRT APIs, implemented solely in header files, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume WinRT APIs using any standards-compliant C++ compiler.
title: C++/WinRT
ms.author: stwhi
ms.date: 03/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection
ms.localizationpriority: medium
---

# C++/WinRT
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

Introduced in Windows 10, version 1803, the Windows SDK now includes C++/WinRT. C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented solely in header files, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume WinRT APIs using any standards-compliant C++17 compiler.

C++/WinRT is for any developer interested in writing beautiful and fast code for Windows. Here's why.

## The case for C++/WinRT
The C++ programming language is used both in the enterprise/commercial *and* independent software vendor (ISV) segments for applications where high levels of correctness, quality, and performance are valued. For example: systems programming; resource-constrained embedded and mobile systems; games and graphics; device drivers; and industrial, scientific, and medical applications, to name but some.

From a language point of view, C++ has always been about authoring and consuming abstractions that are both type-rich and lightweight. But the language has changed radically since the raw pointers, raw loops, and painstaking memory allocation and releasing of C++98. Modern C++ (from C++11 onward) is about clear expression of ideas, simplicity, readability, and a much lower likelihood of introducing bugs.

For authoring and consuming Windows Runtime APIs using C++, there is C++/WinRT. This is Microsoft's recommended replacement for the [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl?branch=live) and [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live).

You use standard C++ data types, algorithms, and keywords when you use C++/WinRT. The projection does have its own custom data types, but in most cases you don't need to learn them because they provide appropriate conversions to and from standard types. That way, you can continue to use the standard C++ language features that you're accustomed to using, and the source code that you already have. C++/WinRT makes it extremely easy to call WinRT APIs in any C++ application, from WinForms to UWP.

C++/WinRT performs better and produces smaller binaries than any other language option for WinRT. It even outperforms handwritten code using the ABI interfaces directly. That's because the abstractions use modern C++ idioms that the Visual C++ compiler is designed to optimize. This includes magic statics, empty base classes, **strlen** elision, as well as many newer optimizations in the latest version of Visual C++ targeted specifically at improving the performance of C++/WinRT.

| Topic | Description |
| - | - |
| [Introduction to C++/WinRT](intro-to-using-cpp-with-winrt.md) | An introduction to C++/WinRT&mdash;a standard C++ language projection for WinRT APIs. |
| [Frequently-asked questions about C++/WinRT](faq.md) | Answers to questions that you're likely to have about authoring and consuming WinRT APIs with C++/WinRT. |
| [String handling in C++/WinRT](strings.md) | With C++/WinRT, you can call WinRT APIs using standard C++ wide string types, or you can use the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) type. |
| [Standard C++ data types and C++/WinRT](std-cpp-data-types.md) | With C++/WinRT, you can call WinRT APIs using Standard C++ data types. |
| [Boxing and unboxing scalar values to IInspectable with C++/WinRT](boxing.md) | A scalar value needs to be wrapped inside a reference class object before being passed to a function that expects **IInspectable**. That wrapping process is known as *boxing* the value. |
| [Interfaces; how to implement them in C++/WinRT](implement-an-interface.md) | This topic shows how to implement a Windows Runtime interface in C++/WinRT. |
| [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md) | This topic describes essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT. |
| [Events; how to author and handle them in C++/WinRT](events-author-handle.md) | This topic demonstrates how to author a Windows Runtime Component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events. |
| [Concurrency and asynchronous operations with C++/WinRT](concurrency.md) | This topic shows the ways in which you can both create and consume Windows Runtime asynchronous objects with C++/WinRT. |
| [XAML controls; binding to a C++/WinRT property](binding-property.md) | A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it. |
| [XAML items controls; binding to a C++/WinRT collection](binding-collection.md) | A collection that can be effectively bound to a XAML items control is known as an *observable* collection. This topic shows how to implement and consume an observable collection, and how to bind a XAML items control to it. |
| [Troubleshooting C++/WinRT issues](troubleshooting.md) | The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app. |

## Important APIs
[winrt namespace (C++/WinRT)](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl)
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
