---
description: An introduction to C++/WinRT&mdash;a standard C++ language projection for Windows Runtime APIs.
title: Introduction to C++/WinRT
ms.date: 05/07/2018
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, introduction
ms.localizationpriority: medium
---
# Introduction to C++/WinRT
&nbsp;
> [!VIDEO https://www.youtube.com/embed/nOFNc2uTmGs]

C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume Windows Runtime APIs using any standards-compliant C++17 compiler. The Windows SDK includes C++/WinRT; it was introduced in version 10.0.17134.0 (Windows 10, version 1803).

C++/WinRT is Microsoft's recommended replacement for the [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live) language projection, and the [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl?branch=live). The full list of [topics about C++/WinRT](index.md#topics-about-cwinrt) includes info about both interoperating with, and porting from, C++/CX and WRL.

> [!IMPORTANT]
> Two of the most important pieces of C++/WinRT to be aware of are described in the sections [SDK support for C++/WinRT](#sdk-support-for-cwinrt) and [Visual Studio support for C++/WinRT, and the VSIX](#visual-studio-support-for-cwinrt-and-the-vsix).

## Language projections
The Windows Runtime is based on Component Object Model (COM) APIs, and it's designed to be accessed through *language projections*. A projection hides the COM details, and provides a more natural programming experience for a given language.

### The C++/WinRT language projection in the Windows UWP API reference content
When you're browsing [Windows UWP APIs](https://docs.microsoft.com/uwp/api/), click the **Language** combo box in the upper right, and select **C++/WinRT** to view API syntax blocks as they appear in the C++/WinRT language projection.

## SDK support for C++/WinRT
As of version 10.0.17134.0 (Windows 10, version 1803), the Windows SDK contains a header-file-based standard C++ library for consuming first-party Windows APIs (Windows Runtime APIs in Windows namespaces). C++/WinRT also comes with the `cppwinrt.exe` tool, which you can point at a Windows Runtime metadata (`.winmd`) file to generate a header-file-based standard C++ library that *projects* the APIs described in the metadata for consumption from C++/WinRT code. Windows Runtime metadata (`.winmd`) files provide a canonical way of describing a Windows Runtime API surface. By pointing `cppwinrt.exe` at metadata, you can generate a library for use with any runtime class implemented in a second- or third-party Windows Runtime component, or implemented in your own application. For more info, see [Consume APIs with C++/WinRT](consume-apis.md).

With C++/WinRT, you can also implement your own runtime classes using standard C++, without resorting to COM-style programming. For a runtime class, you just describe your types in an IDL file, and `midl.exe` and `cppwinrt.exe` generate your implementation boilerplate source code files for you. You can alternatively just implement interfaces by deriving from a C++/WinRT base class. For more info, see [Author APIs with C++/WinRT](author-apis.md).

## Visual Studio support for C++/WinRT, and the VSIX
For C++/WinRT project templates in Visual Studio, as well as C++/WinRT MSBuild properties and targets, download and install the [C++/WinRT Visual Studio Extension (VSIX)](https://aka.ms/cppwinrt/vsix) from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/).

> [!NOTE]
> With version 1.0.181002.2 (or later) of the VSIX installed, creating a new C++/WinRT project automatically installs the [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) for that project. The Microsoft.Windows.CppWinRT NuGet package provides improved C++/WinRT project build support, making your project portable between a development machine and a build agent (on which only the NuGet package, and not the VSIX, is installed).
>
> For an existing project&mdash;after you've installed version 1.0.181002.2 (or later) of the VSIX&mdash;we recommend that you open the project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project.

You'll need Visual Studio 2017 (you'll need at least version 15.6, but we recommend at least 15.7), and Windows SDK version 10.0.17134.0 (Windows 10, version 1803). If you haven't already installed it, you'll need to install the **C++ Universal Windows Platform tools** option from within the Visual Studio Installer. And, in Windows **Settings** > **Update \& Security** > **For developers**, choose the **Developer mode** option  rather than the **Sideload apps** option.

You'll then be able to create and build, or open, a C++/WinRT project in Visual Studio, and deploy it. Alternatively, you can convert an existing project by adding the `<CppWinRTEnabled>true</CppWinRTEnabled>` property to its `.vcxproj` file.

```xml
<Project ...>
    <PropertyGroup Label="Globals">
        <CppWinRTEnabled>true</CppWinRTEnabled>
...
```

Once you've added that property, you'll get C++/WinRT MSBuild support for the project, including invoking the `cppwinrt.exe` tool.

Because C++/WinRT uses features from the C++17 standard, it needs project property **C/C++** > **Language** > **C++ Language Standard** > **ISO C++17 Standard (/std:c++17)**. You might also want to set **Conformance mode: Yes (/permissive-)**, which further constrains your code to be standards-compliant.

Another project property to be aware of is **C/C++** > **General** > **Treat Warnings As Errors**. Set this to **Yes(/WX)** or **No (/WX-)** to taste. Sometimes, source files generated by the `cppwinrt.exe` tool generate warnings until you add your implementation to them.

The VSIX also gives you Visual Studio native debug visualization (natvis) of C++/WinRT projected types; providing an experience similar to C# debugging. Natvis is automatic for debug builds. You can opt into it release builds by defining the symbol WINRT_NATVIS.

Here are the Visual Studio project templates provided by the VSIX.

### Windows Console Application (C++/WinRT)
A project template for a C++/WinRT client application for Windows Desktop, with a console user-interface.

### Blank App (C++/WinRT)
A project template for a Universal Windows Platform (UWP) app that has a XAML user-interface.

Visual Studio provides XAML compiler support to generate implementation and header stubs from the Interface Definition Language (IDL) (`.idl`) file that sits behind each XAML markup file. In an IDL file, define any local runtime classes that you want to reference in your app's XAML pages, and then build the project once to generate implementation templates in `Generated Files`, and stub type definitions in `Generated Files\sources`. Then use those the stub type definitions for reference to implement your local runtime classes. We recommend that you declare each runtime class in its own IDL file.

### Core App (C++/WinRT)
A project template for a Universal Windows Platform (UWP) app that doesn't use XAML.

Instead, it uses the C++/WinRT Windows namespace header for the Windows.ApplicationModel.Core namespace. After building and running, click on an empty space to add a colored square; then click on a colored square to drag it.

### Windows Runtime Component (C++/WinRT)
A project template for a component; typically for consumption from a Universal Windows Platform (UWP).

This template demonstrates the `midl.exe` > `cppwinrt.exe` toolchain, where Windows Runtime metadata (`.winmd`) is generated from IDL, and then implementation and header stubs are generated from the Windows Runtime metadata.

In an IDL file, define the runtime classes in your component, their default interface, and any other interfaces they implement. Build the project once to generate `module.g.cpp`, `module.h.cpp`, implementation templates in `Generated Files`, and stub type definitions in `Generated Files\sources`. Then use those the stub type definitions for reference to implement the runtime classes in your component. We recommend that you declare each runtime class in its own IDL file.

Bundle the built Windows Runtime Component binary and its `.winmd` with the UWP app consuming them.

## Custom types in the C++/WinRT projection
In your C++/WinRT programming, you can use standard C++ language features and [Standard C++ data types and C++/WinRT](std-cpp-data-types.md)&mdash;including some C++ Standard Library data types. But you'll also become aware of some custom data types in the projection, and you can choose to use them. For example, we use [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) in the quick-start code example in [Get started with C++/WinRT](get-started.md).

[**winrt::com_array**](/uwp/cpp-ref-for-winrt/com-array) is another type that you're likely to use at some point. But you're less likely to directly use a type such as [**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view). Or you may choose not to use it so that you won't have any code to change if and when an equivalent type appears in the C++ Standard Library.

> [!WARNING]
> There are also types that you might see if you closely study the C++/WinRT Windows namespace headers. An example is **winrt::param::hstring**, but there are collection examples too. These exist solely to optimize the binding of input parameters, and they yield big performance improvements and make most calling patterns "just work" for related standard C++ types and containers. These types are only ever used by the projection in cases where they add most value. They're highly optimized and they're not for general use; don't be tempted to use them yourself. Nor should you use anything from the `winrt::impl` namespace, since those are implementation types, and therefore subject to change. You should continue to use standard types, or types from the [winrt namespace](/uwp/cpp-ref-for-winrt/winrt).

## Important APIs
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [C++/WinRT Visual Studio Extension (VSIX)](https://aka.ms/cppwinrt/vsix)
* [Get started with C++/WinRT](get-started.md)
* [Standard C++ data types and C++/WinRT](std-cpp-data-types.md)
* [String handling in C++/WinRT](strings.md)
* [Windows UWP APIs](https://docs.microsoft.com/uwp/api/)
