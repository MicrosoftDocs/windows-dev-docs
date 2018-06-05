---
author: stevewhims
description: Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT.
title: Frequently-asked questions about C++/WinRT
ms.author: stwhi
ms.date: 05/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, frequently, asked, questions, faq
ms.localizationpriority: medium
---

# Frequently-asked questions about [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT.

> [!NOTE]
> If your question is about an error message that you've seen, then also see the [Troubleshooting C++/WinRT](troubleshooting.md) topic.

## What are the requirements for the [C++/WinRT Visual Studio Extension (VSIX)](https://aka.ms/cppwinrt/vsix)?
The [VSIX](https://aka.ms/cppwinrt/vsix) enforces a minimum Windows SDK target version of 10.0.17134.0 (Windows 10, version 1803). You'll also need Visual Studio 2017 (at least version 15.6; we recommend at least 15.7). You can identify a project that uses the VSIX by the presence of `<CppWinRTEnabled>true</CppWinRTEnabled>` in `<PropertyGroup Label="Globals">` in the `.vcxproj` file. For more info, see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

## What's a *runtime class*?
A runtime class is a type that can be activated and consumed via modern COM interfaces, typically across executable boundaries. However, a runtime class can also be used within the compilation unit that implements it. You declare a runtime class in Interface Definition Language (IDL), and you can implement it in standard C++ using C++/WinRT.

## What do *the projected type* and *the implementation type* mean?
If you're only *consuming* a Windows Runtime class (runtime class), then you'll be dealing exclusively with *projected types*. C++/WinRT is a *language projection*, so projected types are part of the surface of the Windows Runtime that's *projected* into C++ with C++/WinRT. For more details, see see [Consume APIs with C++/WinRT](consume-apis.md).

The *implementation type* contains the implementation of a runtime class, so it's only available in the project that implements the runtime class. When you're working in a project that implements runtime classes (a Windows Runtime component project, or a project that uses XAML UI), it's important to be comfortable with the distinction between your implementation type for a runtime class, and the projected type that represents the runtime class projected into C++/WinRT. For more details, see [Author APIs with C++/WinRT](author-apis.md).

## Do I need to declare a constructor in my runtime class's IDL?
Only if the runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by Windows Runtime client apps). For full details on the purpose and consequences of declaring constructor(s) in IDL, see [Runtime class constructors](author-apis.md#runtime-class-constructors).

## Why is the linker giving me a "LNK2019: Unresolved external symbol" error?
If the unresolved symbol is an API from the Windows namespace headers for the C++/WinRT projection (in the **winrt** namespace), then the API is forward-declared in a header that you've included, but its definition is in a header that you haven't yet included. Include the header named for the API's namespace, and rebuild. For more info, see [C++/WinRT projection headers](consume-apis.md#cwinrt-projection-headers).

If the unresolved symbol is a Windows Runtime free function, such as [RoInitialize](https://msdn.microsoft.com/library/br224650), then you'll need to explicitly include the [WindowsApp.lib](/uwp/win32-and-com/win32-apis) umbrella library in your project. The C++/WinRT projection depends on some of these free (non-member) functions and entry points. If you use one of the [C++/WinRT Visual Studio Extension (VSIX)](https://aka.ms/cppwinrt/vsix) project templates for your application, then `WindowsApp.lib` is linked for you automatically. Otherwise, you can use project link settings to include it, or do it in source code.

```cppwinrt
#pragma comment(lib, "windowsapp")
```

## Should I implement [**Windows::Foundation::IClosable**](/uwp/api/windows.foundation.iclosable) and, if so, how?
If you have a runtime class that frees resources in its destructor, and that runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by Windows Runtime client apps), then we recommend that you also implement **IClosable** in order to support the consumption of your runtime class by languages that lack deterministic finalization. Make sure that your resources are freed whether the destructor, [**IClosable::Close**](/uwp/api/windows.foundation.iclosable.Close), or both are called. **IClosable::Close** may be called an arbitrary number of times.

## Do I need to call [**IClosable::Close**](/uwp/api/windows.foundation.iclosable#Windows_Foundation_IClosable_Close_) on runtime classes that I consume?
**IClosable** exists to support languages that lack deterministic finalization. So, you shouldn't call **IClosable::Close** from C++/WinRT, except in very rare cases involving shutdown races or semi-deadly embraces. If you're using **Windows.UI.Composition** types, as an example, then you may encounter cases where you want to dispose objects in a set sequence, as an alternative to allowing the destruction of the C++/WinRT wrapper do the work for you.

## Can I use LLVM/Clang to compile with C++/WinRT?
We don't support the LLVM and Clang toolchain for C++/WinRT, but we do make use of it internally to validate C++/WinRT's standards conformance. For example, if you wanted to emulate what we do internally, then you could try an experiment such as the one described below.

Go to the [LLVM Download Page](https://releases.llvm.org/download.html), look for **Download LLVM 6.0.0** > **Pre-Built Binaries**, and download **Clang for Windows (64-bit)**. During installation, opt to add LLVM to the PATH system variable so that you'll be able to invoke it from a command prompt. For the purposes of this experiment, you can ignore any "Failed to find MSBuild toolsets directory" and/or "MSVC integration install failed" errors, if you see them. There are a variety of ways to invoke LLVM/Clang; the example below shows just one way.

```
C:\ExperimentWithLLVMClang>type main.cpp
// main.cpp
#pragma comment(lib, "windowsapp")
#pragma comment(lib, "ole32")

#include "winrt/Windows.Foundation.h"
#include <stdio.h>
#include <iostream>

using namespace winrt;

int main()
{
    winrt::init_apartment();
    Windows::Foundation::Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    std::wcout << rssFeedUri.Domain().c_str() << std::endl;
}

C:\ExperimentWithLLVMClang>clang-cl main.cpp /EHsc /I ..\.. -Xclang -std=c++17 -Xclang -Wno-delete-non-virtual-dtor -o app.exe

C:\ExperimentWithLLVMClang>app
windows.com
```

Because C++/WinRT uses features from the C++17 standard, you'll need to use whatever compiler flags are necessary to get that support; such flags differ from one compiler to another.

Visual Studio is the development tool that we support and recommend for C++/WinRT. See [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

> [!NOTE]
> If this topic didn't answer your question, you might find help by using the [`c++-winrt` tag on Stack Overflow](https://stackoverflow.com/questions/tagged/c%2b%2b-winrt).
