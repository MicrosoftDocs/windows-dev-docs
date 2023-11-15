---
description: C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library.
title: C++/WinRT
ms.date: 11/15/2023
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection
ms.localizationpriority: medium
---

# C++/WinRT

[C++/WinRT](./intro-to-using-cpp-with-winrt.md) is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume Windows Runtime APIs using any standards-compliant C++17 compiler. The Windows SDK includes C++/WinRT; it was introduced in version 10.0.17134.0 (Windows 10, version 1803).

C++/WinRT is for any developer interested in writing beautiful and fast code for Windows. Here's why.

## The case for C++/WinRT
&nbsp;
> [!VIDEO https://www.youtube.com/embed/rJxQnhiK4TQ]

The C++ programming language is used both in the enterprise and independent software vendor (ISV) segments for applications where high levels of correctness, quality, and performance are valued. For example: systems programming; resource-constrained embedded and mobile systems; games and graphics; device drivers; and industrial, scientific, and medical applications, to name but some.

From a language point of view, C++ has always been about authoring and consuming abstractions that are both type-rich and lightweight. But the language has changed radically since the raw pointers, raw loops, and painstaking memory allocation and releasing of C++98. Modern C++ (from C++11 onward) is about clear expression of ideas, simplicity, readability, and a much lower likelihood of introducing bugs.

For authoring and consuming Windows APIs using C++, there is C++/WinRT. This is Microsoft's recommended replacement for the [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live) language projection, and the [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl).

You use standard C++ data types, algorithms, and keywords when you use C++/WinRT. The projection does have its own custom data types, but in most cases you don't need to learn them because they provide appropriate conversions to and from standard types. That way, you can continue to use the standard C++ language features that you're accustomed to using, and the source code that you already have. C++/WinRT makes it extremely easy to call Windows APIs in any C++ application, from Win32 to the Windows AppSDK to UWP.

C++/WinRT performs better and produces smaller binaries than any other language option for the Windows Runtime. It even outperforms handwritten code using the ABI interfaces directly. That's because the abstractions use modern C++ idioms that the Visual C++ compiler is designed to optimize. This includes magic statics, empty base classes, **strlen** elision, as well as many newer optimizations in the latest version of Visual C++ targeted specifically at improving the performance of C++/WinRT.

There are ways to gradually introduce C++/WinRT into your projects. You could use [Windows Runtime components](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md), or you could interoperate with C++/CX. For more info, see [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md).

For info about porting to C++/WinRT, see these resources.

- [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md)
- [Move to C++/WinRT from WRL](./move-to-winrt-from-wrl.md)
- [Move to C++/WinRT from C#](./move-to-winrt-from-csharp.md)

Also see [Where can I find C++/WinRT sample apps?](./faq.yml#where-can-i-find-c---winrt-sample-apps-).

### Topics about C++/WinRT

| Topic | Description |
| - | - |
| [Introduction to C++/WinRT](./intro-to-using-cpp-with-winrt.md) | An introduction to C++/WinRT&mdash;a standard C++ language projection for Windows Runtime APIs. |
| [Get started with C++/WinRT](./get-started.md) | To get you up to speed with using C++/WinRT, this topic walks through a simple code example. |
| [What's new in C++/WinRT](./news.md) | News and changes to C++/WinRT. |
| [Frequently-asked questions](./faq.yml) | Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT. |
| [Troubleshooting](./troubleshooting.md) | The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app. |
| [Photo Editor C++/WinRT sample application](./photo-editor-sample.md) | Photo Editor is a UWP sample application that showcases development with the C++/WinRT language projection. The sample application allows you to retrieve photos from the **Pictures** library, and then edit the selected image with assorted photo effects. | 
| [String handling](./strings.md) | With C++/WinRT, you can call Windows Runtime APIs using standard C++ wide string types, or you can use the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) type. |
| [Standard C++ data types and C++/WinRT](./std-cpp-data-types.md) | With C++/WinRT, you can call Windows Runtime APIs using Standard C++ data types. |
| [Boxing and unboxing values to IInspectable](./boxing.md) | A scalar or array value needs to be wrapped inside a reference class object before being passed to a function that expects **IInspectable**. That wrapping process is known as *boxing* the value. |
| [Consume APIs with C++/WinRT](./consume-apis.md) | This topic shows how to consume C++/WinRT APIs, whether they're implemented by Windows, a third-party component vendor, or by yourself. |
| [Author APIs with C++/WinRT](./author-apis.md) | This topic shows how to author C++/WinRT APIs by using the **winrt::implements** base struct, either directly or indirectly. |
| [Error handling with C++/WinRT](./error-handling.md) | This topic discusses strategies for handling errors when programming with C++/WinRT. |
| [Handle events by using delegates](./handle-events.md) | This topic shows how to register and revoke event-handling delegates using C++/WinRT. |
| [Author events](./author-events.md) | This topic demonstrates how to author a Windows Runtime component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events. |
| [Collections with C++/WinRT](./collections.md) | C++/WinRT provides functions and base classes that save you a lot of time and effort when you want to implement and/or pass collections. |
| [Concurrency and asynchronous operations](./concurrency.md) | This topic shows the ways in which you can both create and consume Windows Runtime asynchronous objects with C++/WinRT. |
| [Advanced concurrency and asynchrony](./concurrency-2.md) | Advanced scenarios with concurrency and asynchrony in C++/WinRT. |
| [A completion source sample](./concurrency-3.md) | Shows how you can author and consume your own completion source class. |
| [XAML controls; bind to a C++/WinRT property](./binding-property.md) | A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it. |
| [XAML items controls; bind to a C++/WinRT collection](./binding-collection.md) | A collection that can be effectively bound to a XAML items control is known as an *observable* collection. This topic shows how to implement and consume an observable collection, and how to bind a XAML items control to it. |
| [XAML custom (templated) controls with C++/WinRT](./xaml-cust-ctrl.md) | This topic walks you through the steps of creating a simple custom control using C++/WinRT. You can build on the info here to create your own feature-rich and customizable UI controls. |
| [Passing parameters to projected APIs](./pass-parms-to-abi.md) | C++/WinRT simplifies passing parameters to projected APIs by providing automatic conversions for common cases. |
| [Consume COM components with C++/WinRT](./consume-com.md) | This topic uses a full Direct2D code example to show how to use C++/WinRT to consume COM classes and interfaces. |
| [Author COM components with C++/WinRT](./author-coclasses.md) | C++/WinRT can help you to author classic COM components, just as it helps you to author Windows Runtime classes. |
| [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md) | This topic describes the technical details involved in porting the source code in a [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) project to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md). |
| [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md) | This topic shows two helper functions that can be used to convert between [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and [C++/WinRT](./intro-to-using-cpp-with-winrt.md) objects. |
| [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md) | This is an advanced topic related to gradually porting from [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) to [C++/WinRT](./intro-to-using-cpp-with-winrt.md). It shows how Parallel Patterns Library (PPL) tasks and coroutines can exist side by side in the same project. |
| [Move to C++/WinRT from WRL](./move-to-winrt-from-wrl.md) | This topic shows how to port [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) code to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md). |
| [Porting the Clipboard sample to C++/WinRT from C#&mdash;a case study](./clipboard-to-winrt-from-csharp.md) | This topic presents a case study of porting one of the [Universal Windows Platform (UWP) app samples](https://github.com/microsoft/Windows-universal-samples) from [C#](/visualstudio/get-started/csharp) to [C++/WinRT](./intro-to-using-cpp-with-winrt.md). You can gain porting practice and experience by following along with the walkthrough and porting the sample for yourself as you go. |
| [Move to C++/WinRT from C#](./move-to-winrt-from-csharp.md) | This topic comprehensively catalogs the technical details involved in porting the source code in a [C#](/visualstudio/get-started/csharp) project to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md). |
| [Interop between C++/WinRT and the ABI](./interop-winrt-abi.md) | This topic shows how to convert between application binary interface (ABI) and C++/WinRT objects. |
| [Strong and weak references in C++/WinRT](./weak-references.md) | The Windows Runtime is a reference-counted system; and in such a system it's important for you to know about the significance of, and distinction between, strong and weak references. |
| [Agile objects](./agile-objects.md) | An agile object is one that can be accessed from any thread. Your C++/WinRT types are agile by default, but you can opt out. |
| [Diagnosing direct allocations](./diag-direct-alloc.md) | This topic goes in-depth on a C++/WinRT 2.0 feature that helps you diagnose the mistake of creating an object of implementation type on the stack, rather than using the [**winrt::make**](/uwp/cpp-ref-for-winrt/make) family of helpers, as you should. |
| [Extension points for your implementation types](./details-about-destructors.md) | These extension points in C++/WinRT 2.0 allow you to defer destruction of your implementation types, to safely query during destruction, and to hook the entry into and exit from your projected methods. |
| [A basic C++/WinRT Windows UI Library 2 example (UWP)](./simple-winui-example.md) | This topic walks you through the process of adding basic support for the [Windows UI Library (WinUI)](https://github.com/Microsoft/microsoft-ui-xaml) to your C++/WinRT UWP project. Specifically, this topic deals with WinUI 2, which is for UWP apps. |
| [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md) | This topic shows how to use C++/WinRT to create and consume a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language. |
| [Authoring a C# Windows Runtime component for use from a C++/WinRT app](../cpp-and-winrt-apis/use-csharp-component-from-cpp-winrt.md) | This topic walks you through the process of adding a simple C# component to your C++/WinRT project. |
| [Visual Studio native debug visualization (natvis) for C++/WinRT](./natvis.md) | The [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) gives you Visual Studio native debug visualization (natvis) of C++/WinRT projected types. This provides you an experience similar to C# debugging. |
| [Configuration macros](./macros.md) | This topic describes the C++/WinRT configuration macros. |
| [C++/WinRT naming conventions](./naming.md) | This topic explains naming conventions that C++/WinRT has established. |

### Topics about the C++ language

| Topic | Description |
| - | - |
| [Value categories, and references to them](./cpp-value-categories.md) | This topic describes the various categories of values that exist in C++. You will doubtless have heard of lvalues and rvalues, but there are other kinds, too. |

## Important APIs
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl)
