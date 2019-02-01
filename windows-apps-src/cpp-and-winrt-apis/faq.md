---
description: Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT.
title: Frequently-asked questions about C++/WinRT
ms.date: 10/26/2018
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, frequently, asked, questions, faq
ms.localizationpriority: medium
---

# Frequently-asked questions about C++/WinRT
Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt).

> [!NOTE]
> If your question is about an error message that you've seen, then also see the [Troubleshooting C++/WinRT](troubleshooting.md) topic.

## How do I retarget my C++/WinRT project to a later version of the Windows SDK?
See [How to retarget your C++/WinRT project to a later version of the Windows SDK](news.md#how-to-retarget-your-cwinrt-project-to-a-later-version-of-the-windows-sdk).

## Why won't my new project compile? I'm using Visual Studio 2017 (version 15.8.0 or higher), and SDK version 17134
If you're using Visual Studio 2017 (version 15.8.0 or higher), and targeting the Windows SDK version 10.0.17134.0 (Windows 10, version 1803), then a newly created C++/WinRT project may fail to compile with the error "*error C3861: 'from_abi': identifier not found*", and with other errors originating in *base.h*. The solution is to either target a later (more conformant) version of the Windows SDK, or set project property **C/C++** > **Language** > **Conformance mode: No** (also, if **/permissive-** appears in project property **C/C++** > **Command Line** under **Additional Options**, then delete it).

## How do I resolve the build error "The C++/WinRT VSIX no longer provides project build support.  Please add a project reference to the Microsoft.Windows.CppWinRT Nuget package"?
Install the **Microsoft.Windows.CppWinRT** NuGet package into your project. For details, see [Earlier versions of the VSIX extension](intro-to-using-cpp-with-winrt.md#earlier-versions-of-the-vsix-extension).

## What are the requirements for the C++/WinRT Visual Studio Extension (VSIX)?
For version 1.0.190128.4 of the VSIX extension and later, see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package). For other versions, see [Earlier versions of the VSIX extension](intro-to-using-cpp-with-winrt.md#earlier-versions-of-the-vsix-extension).

## What's a *runtime class*?
A runtime class is a type that can be activated and consumed via modern COM interfaces, typically across executable boundaries. However, a runtime class can also be used within the compilation unit that implements it. You declare a runtime class in Interface Definition Language (IDL), and you can implement it in standard C++ using C++/WinRT.

## What do *the projected type* and *the implementation type* mean?
If you're only *consuming* a Windows Runtime class (runtime class), then you'll be dealing exclusively with *projected types*. C++/WinRT is a *language projection*, so projected types are part of the surface of the Windows Runtime that's *projected* into C++ with C++/WinRT. For more details, see [Consume APIs with C++/WinRT](consume-apis.md).

The *implementation type* contains the implementation of a runtime class, so it's only available in the project that implements the runtime class. When you're working in a project that implements runtime classes (a Windows Runtime component project, or a project that uses XAML UI), it's important to be comfortable with the distinction between your implementation type for a runtime class, and the projected type that represents the runtime class projected into C++/WinRT. For more details, see [Author APIs with C++/WinRT](author-apis.md).

## Do I need to declare a constructor in my runtime class's IDL?
Only if the runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by Windows Runtime client apps). For full details on the purpose and consequences of declaring constructor(s) in IDL, see [Runtime class constructors](author-apis.md#runtime-class-constructors).

## Why is the linker giving me a "LNK2019: Unresolved external symbol" error?
If the unresolved symbol is an API from the Windows namespace headers for the C++/WinRT projection (in the **winrt** namespace), then the API is forward-declared in a header that you've included, but its definition is in a header that you haven't yet included. Include the header named for the API's namespace, and rebuild. For more info, see [C++/WinRT projection headers](consume-apis.md#cwinrt-projection-headers).

If the unresolved symbol is a Windows Runtime free function, such as [RoInitialize](https://msdn.microsoft.com/library/br224650), then you'll need to explicitly link the [WindowsApp.lib](/uwp/win32-and-com/win32-apis) umbrella library in your project. The C++/WinRT projection depends on some of these free (non-member) functions and entry points. If you use one of the [C++/WinRT Visual Studio Extension (VSIX)](https://aka.ms/cppwinrt/vsix) project templates for your application, then `WindowsApp.lib` is linked for you automatically. Otherwise, you can use project link settings to include it, or do it in source code.

```cppwinrt
#pragma comment(lib, "windowsapp")
```

It's important that you resolve any linker errors that you can by linking **WindowsApp.lib** instead of an alternative static-link library, otherwise your application won't pass the [Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md) tests used by Visual Studio and by the Microsoft Store to validate submissions (meaning that it consequently won't be possible for your application to be successfully ingested into the Microsoft Store).

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

#include <winrt/Windows.Foundation.h>
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

Visual Studio is the development tool that we support and recommend for C++/WinRT. See [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Why doesn't the generated implementation function for a read-only property have the `const` qualifier?
When you declare a read-only property in [MIDL 3.0](/uwp/midl-3/), you might expect the `cppwinrt.exe` tool to generate an implementation function for you that is `const`-qualified (a const function treats the *this* pointer as const).

We certainly recommend using const wherever possible, but the `cppwinrt.exe` tool itself doesn't attempt to reason about which implementation functions might conceivably be const, and which might not. You can choose to make any of your implementation functions const, as in this example.

```cppwinrt
struct MyStringable : winrt::implements<MyStringable, winrt::Windows::Foundation::IStringable>
{
    winrt::hstring ToString() const
    {
        return L"MyStringable";
    }
};
```

You can remove that `const` qualifier on **ToString** should you decide that you need to alter some object state in its implementation. But make each of your member functions either const or non-const, not both. In other words, don't overload an implementation function on `const`.

Aside from your implementation functions, another other place where const comes into the picture is in Windows Runtime function projections. Consider this code.

```cppwinrt
int main()
{
    winrt::Windows::Foundation::IStringable s{ winrt::make<MyStringable>() };
    auto result{ s.ToString() };
}
```

For the call to **ToString** above, the **Go To Declaration** command in Visual Studio shows that the projection of the Windows Runtime **IStringable::ToString** into C++/WinRT looks like this.

```
winrt::hstring ToString() const;
```

Functions on the projection are const no matter how you choose to qualify your implementation of them. Behind the scenes, the projection calls the application binary interface (ABI), which amounts to a call through a COM interface pointer. The only state that the projected **ToString** interacts with is that COM interface pointer; and it certainly has no need to modify that pointer, so the function is const. This gives you the assurance that it won't change anything about the **IStringable** reference that you're calling through, and it ensures that you can call **ToString** even with a const reference to an **IStringable**.

Understand that these examples of `const` are implementation details of C++/WinRT projections and implementations; they constitute code hygiene for your benefit. There's no such thing as `const` on the COM nor Windows Runtime ABI (for member functions).

## Do you have any recommendations for decreasing the code size for C++/WinRT binaries?
When working with Windows Runtime objects, you should avoid the coding pattern shown below because it can have a negative impact on your application by causing more binary code than necessary to be generated.

```cppwinrt
anobject.b().c().d();
anobject.b().c().e();
anobject.b().c().f();
```

In the Windows Runtime world, the compiler is unable to cache either the value of `c()` or the interfaces for each method that's called through an indirection ('.'). Unless you intervene, that results in more virtual calls and reference counting overhead. The pattern above could easily generate twice as much code as strictly needed. Instead, prefer the pattern shown below wherever you can. It generates a lot less code, and it can also dramatically improve your run time performance.

```cppwinrt
auto a{ anobject.b().c() };
a.d();
a.e();
a.f();
```

The recommended pattern shown above applies not just to C++/WinRT but to all Windows Runtime language projections.

## How do I turn a string into a type&mdash;for navigation, for example?
At the end of the [Navigation view code example](/windows/uwp/design/controls-and-patterns/navigationview#code-example) (which is mostly in C#), there's a C++/WinRT code snippet showing how to do this.

> [!NOTE]
> If this topic didn't answer your question, then you might find help by visiting the [Visual Studio C++ developer community](https://developercommunity.visualstudio.com/spaces/62/index.html), or by using the [`c++-winrt` tag on Stack Overflow](https://stackoverflow.com/questions/tagged/c%2b%2b-winrt).
