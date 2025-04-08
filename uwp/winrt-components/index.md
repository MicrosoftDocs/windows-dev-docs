---
title: Windows Runtime components
description: Windows Runtime components are self-contained objects that you can instantiate and use from any language, including C#, Visual Basic, JavaScript, and C++.
ms.assetid: 55887622-828b-4318-87f2-25592268f7c1
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Windows Runtime components

A Windows Runtime component is a self-contained software module that you can author, reference, and use with any Windows Runtime language (including C#, C++/WinRT, Visual Basic, JavaScript, and C++/CX). You can use Visual Studio to create a Windows Runtime component that can be consumed by either an app that uses the [Windows App SDK](/windows/apps/windows-app-sdk/) or by a Universal Windows Platform (UWP) app.

> [!NOTE]
> For C++ developers, we recommend that you use [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) for new applications. C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. To learn how to create a Windows Runtime component using C++/WinRT, see [Windows Runtime components with C++/WinRT](./create-a-windows-runtime-component-in-cppwinrt.md).

> [!NOTE]
> For C# developers writing desktop apps in .NET 6 or later, use C#/WinRT to author a Windows Runtime component. See [Author Windows Runtime components with C#/WinRT](/windows/apps/develop/platform/csharp-winrt/authoring).

| Topic | Description |
|-------|-------------|
| [Windows Runtime components with C++/WinRT](./create-a-windows-runtime-component-in-cppwinrt.md) | This topic shows how to use C++/WinRT to create and consume a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language. |
| [Windows Runtime components with C++/CX](creating-windows-runtime-components-in-cpp.md) | This topic shows how to use C++/CX to create a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language. |
| [Walkthrough of creating a C++/CX Windows Runtime component, and calling it from JavaScript or C#](walkthrough-creating-a-basic-windows-runtime-component-in-cpp-and-calling-it-from-javascript-or-csharp.md) | This walkthrough shows how to create a basic Windows Runtime component DLL that's callable from JavaScript, C#, or Visual Basic. Before you begin this walkthrough, make sure that you understand concepts such as the Abstract Binary Interface (ABI), ref classes, and the Visual C++ Component Extensions that make working with ref classes easier. For more information, see [Creating Windows Runtime components in C++](creating-windows-runtime-components-in-cpp.md) and [Visual C++ Language Reference (C++/CX)](/cpp/cppcx/visual-c-language-reference-c-cx). |
| [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md) | You can use managed code to create your own Windows Runtime types, packaged in a Windows Runtime component. You can use your component in Universal Windows Platform (UWP) apps with C++, JavaScript, Visual Basic, or C#. This topic outlines the rules for creating a component, and discusses some aspects of .NET support for the Windows Runtime. In general, that support is designed to be transparent to the .NET programmer. However, when you create a component to use with JavaScript or C++, you need to be aware of differences in the way those languages support the Windows Runtime. |
| [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md) | This walkthrough shows how you can use .NET with Visual Basic or C# to create your own Windows Runtime types, packaged in a Windows Runtime component, and how to call the component from your Universal Windows app built for Windows using JavaScript. |
| [Raising Events in Windows Runtime components](raising-events-in-windows-runtime-components.md) | If your Windows Runtime component raises an event of a user-defined delegate type on a background thread (worker thread) and you want JavaScript to be able to receive the event, you can implement and/or raise it in one of these ways: | 
