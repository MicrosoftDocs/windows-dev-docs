---
author: stevewhims
description: C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library.
title: C++/WinRT
ms.author: stwhi
ms.date: 05/14/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection
ms.localizationpriority: medium
---

# [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume Windows Runtime APIs using any standards-compliant C++17 compiler. The Windows SDK includes C++/WinRT; it was introduced in version 10.0.17134.0 (Windows 10, version 1803).

C++/WinRT is for any developer interested in writing beautiful and fast code for Windows. Here's why.

## The case for C++/WinRT
&nbsp;
> [!VIDEO https://www.youtube.com/embed/TLSul1XxppA]

The C++ programming language is used both in the enterprise *and* independent software vendor (ISV) segments for applications where high levels of correctness, quality, and performance are valued. For example: systems programming; resource-constrained embedded and mobile systems; games and graphics; device drivers; and industrial, scientific, and medical applications, to name but some.

From a language point of view, C++ has always been about authoring and consuming abstractions that are both type-rich and lightweight. But the language has changed radically since the raw pointers, raw loops, and painstaking memory allocation and releasing of C++98. Modern C++ (from C++11 onward) is about clear expression of ideas, simplicity, readability, and a much lower likelihood of introducing bugs.

For authoring and consuming Windows Runtime APIs using C++, there is C++/WinRT. This is Microsoft's recommended replacement for the [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl?branch=live) and [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live).

You use standard C++ data types, algorithms, and keywords when you use C++/WinRT. The projection does have its own custom data types, but in most cases you don't need to learn them because they provide appropriate conversions to and from standard types. That way, you can continue to use the standard C++ language features that you're accustomed to using, and the source code that you already have. C++/WinRT makes it extremely easy to call Windows Runtime APIs in any C++ application, from Win32 to UWP.

C++/WinRT performs better and produces smaller binaries than any other language option for the Windows Runtime. It even outperforms handwritten code using the ABI interfaces directly. That's because the abstractions use modern C++ idioms that the Visual C++ compiler is designed to optimize. This includes magic statics, empty base classes, **strlen** elision, as well as many newer optimizations in the latest version of Visual C++ targeted specifically at improving the performance of C++/WinRT.

| Topic | Description |
| - | - |
| [Introduction to C++/WinRT](intro-to-using-cpp-with-winrt.md) | An introduction to C++/WinRT&mdash;a standard C++ language projection for Windows Runtime APIs. |
| [Get started with C++/WinRT](get-started.md) | To get you up to speed with using C++/WinRT, this topic walks through a simple code example. |
| [Frequently-asked questions](faq.md) | Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT. |
| [Troubleshooting](troubleshooting.md) | The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app. |
| [String handling](strings.md) | With C++/WinRT, you can call Windows Runtime APIs using standard C++ wide string types, or you can use the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) type. |
| [Standard C++ data types and C++/WinRT](std-cpp-data-types.md) | With C++/WinRT, you can call Windows Runtime APIs using Standard C++ data types. |
| [Boxing and unboxing scalar values to IInspectable](boxing.md) | A scalar value needs to be wrapped inside a reference class object before being passed to a function that expects **IInspectable**. That wrapping process is known as *boxing* the value. |
| [Consume APIs with C++/WinRT](consume-apis.md) | This topic shows how to consume C++/WinRT APIs, whether they're implemented by Windows, a third-party component vendor, or by yourself. |
| [Author APIs with C++/WinRT](author-apis.md) | This topic shows how to author C++/WinRT APIs by using the **winrt::implements** base struct, either directly or indirectly. |
| [Error handling with C++/WinRT](error-handling.md) | This topic discusses strategies for handling errors when programming with C++/WinRT. |
| [Handle events by using delegates](handle-events.md) | This topic shows how to register and revoke event-handling delegates using C++/WinRT. |
| [Author events](author-events.md) | This topic demonstrates how to author a Windows Runtime Component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events. |
| [Concurrency and asynchronous operations](concurrency.md) | This topic shows the ways in which you can both create and consume Windows Runtime asynchronous objects with C++/WinRT. |
| [XAML controls; bind to a C++/WinRT property](binding-property.md) | A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it. |
| [XAML items controls; bind to a C++/WinRT collection](binding-collection.md) | A collection that can be effectively bound to a XAML items control is known as an *observable* collection. This topic shows how to implement and consume an observable collection, and how to bind a XAML items control to it. |
| [Interop between C++/WinRT and C++/CX](interop-winrt-cx.md) | This topic shows two helper functions that can be used to convert between [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live) and C++/WinRT objects. |
| [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md) | This topic shows how to port C++/CX code to its equivalent in C++/WinRT. |
| [Interop between C++/WinRT and the ABI](interop-winrt-abi.md) | This topic shows how to convert between application binary interface (ABI) and C++/WinRT objects. |
| [Move to C++/WinRT from WRL](move-to-winrt-from-wrl.md) | This topic shows how to port [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) code to its equivalent in C++/WinRT. |
| [Weak references](weak-references.md) | C++/WinRT weak reference support is pay-for-play, in that it doesn't cost you anything unless your object is queried for [**IWeakReferenceSource**](https://msdn.microsoft.com/library/br224609). |
| [Agile objects](agile-objects.md) | An agile object is one that can be accessed from any thread. Your C++/WinRT types are agile by default, but you can opt out. |

## Important APIs
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl)
