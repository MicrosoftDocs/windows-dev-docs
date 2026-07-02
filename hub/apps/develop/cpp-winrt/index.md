---
title: C++/WinRT overview
description: An overview of C++/WinRT, a standard C++17 language projection for Windows Runtime APIs, for use with the Windows App SDK and WinUI 3.
ms.date: 07/02/2026
ms.topic: overview
keywords: windows app sdk, winui 3, c++, cpp, winrt, projection, language projection
ms.localizationpriority: medium
---

# C++/WinRT

C++/WinRT is a standard C++17 language projection for Windows Runtime (WinRT) APIs. It's implemented as a header-file-based library and designed to provide first-class access to the modern Windows API from any standards-compliant C++17 compiler.

C++/WinRT lets you both consume and author Windows Runtime APIs using standard C++. It's the recommended replacement for [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and the [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl).

## Get started

- [Introduction to C++/WinRT](./intro-to-using-cpp-with-winrt.md) &mdash; an overview of what C++/WinRT is and why it exists.
- [Get started with C++/WinRT](./get-started.md) &mdash; set up your development environment and write your first C++/WinRT app.

## Core concepts

- [Concurrency and asynchronous operations](./concurrency.md) &mdash; author and consume asynchronous operations with coroutines.
- [A completion source sample](./concurrency-3.md) &mdash; reporting progress, timeouts, and other advanced patterns.
- [Collections with C++/WinRT](./collections.md) &mdash; create and consume Windows Runtime collection types.
- [Boxing and unboxing values](./boxing.md) &mdash; wrap scalar and array values for APIs that expect **IInspectable**.
- [Standard C++ data types and C++/WinRT](./std-cpp-data-types.md) &mdash; use standard C++ types with Windows Runtime APIs.
- [Value categories and references](./cpp-value-categories.md) &mdash; understand lvalues, rvalues, and how C++/WinRT uses them.
- [Agile objects](./agile-objects.md) &mdash; how agility works in C++/WinRT.
- [String handling](./strings.md) &mdash; work with **winrt::hstring** and standard string types.
- [Error handling](./error-handling.md) &mdash; handle and produce errors with C++/WinRT.

## Authoring and interop

- [Author COM components](./author-coclasses.md) &mdash; implement classic COM coclasses with C++/WinRT.
- [Consume APIs](./consume-apis.md) &mdash; call Windows Runtime APIs from C++/WinRT.
- [Author APIs](./author-apis.md) &mdash; define and implement your own Windows Runtime types.
- [Interop between C++/WinRT and the ABI](./interop-winrt-abi.md) &mdash; convert between ABI and C++/WinRT objects.
- [Passing parameters into the ABI boundary](./pass-parms-to-abi.md) &mdash; efficiently pass values across the ABI.
- [Use a C# component from C++/WinRT](./use-csharp-component-from-cpp-winrt.md) &mdash; consume a C# Windows Runtime component.

## XAML and UI

- [XAML controls; bind to a C++/WinRT property](./binding-property.md) &mdash; data-bind a XAML control to a C++/WinRT property.
- [XAML items controls; bind to a collection](./binding-collection.md) &mdash; bind to an observable collection.
- [XAML custom controls](./xaml-cust-ctrl.md) &mdash; create a custom (templated) control.

## Samples

- [Photo Editor C++/WinRT sample application](./photo-editor-sample.md)
- [A simple WinUI example](./simple-winui-example.md)

## Reference and diagnostics

- [Naming conventions](./naming.md) &mdash; C++/WinRT naming rules and conventions.
- [Native debug visualization (natvis)](./natvis.md) &mdash; use Visual Studio natvis to debug C++/WinRT types.
- [Macros](./macros.md) &mdash; C++/WinRT preprocessor macros.
- [Diagnosing direct allocations](./diag-direct-alloc.md) &mdash; detect and fix direct allocations of implementation types.
- [Details about destructors](./details-about-destructors.md) &mdash; understand destructor behavior in C++/WinRT.

## Migration guides

- [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md)
- [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md)
- [Move to C++/WinRT from WRL](./move-to-winrt-from-wrl.md)
- [Move to C++/WinRT from C#](./move-to-winrt-from-csharp.md)
