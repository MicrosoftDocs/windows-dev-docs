---
description: An introduction to C++/WinRT&mdash;a standard C++ language projection for Windows Runtime APIs.
title: Introduction to C++/WinRT
ms.date: 04/18/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, introduction
ms.localizationpriority: medium
---

# Introduction to C++/WinRT
&nbsp;
> [!VIDEO https://www.youtube.com/embed/X41j_gzSwOY]

&nbsp;
> [!VIDEO https://www.youtube.com/embed/nOFNc2uTmGs]

C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume Windows Runtime APIs using any standards-compliant C++17 compiler. The Windows SDK includes C++/WinRT; it was introduced in version 10.0.17134.0 (Windows 10, version 1803).

C++/WinRT is Microsoft's recommended replacement for the [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live) language projection, and the [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl?branch=live). The full list of [topics about C++/WinRT](index.md#topics-about-cwinrt) includes info about both interoperating with, and porting from, C++/CX and WRL.

> [!IMPORTANT]
> Some of the most important pieces of C++/WinRT to be aware of are described in the sections [SDK support for C++/WinRT](#sdk-support-for-cwinrt) and [Visual Studio support for C++/WinRT, XAML, the VSIX extension, and the NuGet package](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Language projections
The Windows Runtime is based on Component Object Model (COM) APIs, and it's designed to be accessed through *language projections*. A projection hides the COM details, and provides a more natural programming experience for a given language.

### The C++/WinRT language projection in the Windows Runtime API reference content
When you're browsing [Windows Runtime APIs](/uwp/api/), click the **Language** combo box in the upper right, and select **C++/WinRT** to view API syntax blocks as they appear in the C++/WinRT language projection.

## Visual Studio support for C++/WinRT, XAML, the VSIX extension, and the NuGet package
For Visual Studio support, you'll need Visual Studio 2019 or Visual Studio 2017 (at least version 15.6; we recommend at least 15.7). From within the Visual Studio Installer, install the **Universal Windows Platform development** workload. In **Installation Details** > **Universal Windows Platform development**, check the **C++ (v14x) Universal Windows Platform tools** option(s), if you haven't already done so. And, in Windows **Settings** > **Update \& Security** > **For developers**, choose the **Developer mode** option rather than the **Sideload apps** option.

While we recommend that you develop with the latest versions of Visual Studio and the Windows SDK, if you're using a version of C++/WinRT that shipped with the Windows SDK prior to 10.0.17763.0 (Windows 10, version 1809), then, to use the the Windows namespaces headers mentioned above, you'll need a minimum Windows SDK target version in your project of 10.0.17134.0 (Windows 10, version 1803).

You'll want to download and install the latest version of the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/).

- The VSIX extension gives you C++/WinRT project and item templates in Visual Studio, so that you can get started with C++/WinRT development.
- In addition, it gives you Visual Studio native debug visualization (natvis) of C++/WinRT projected types; providing an experience similar to C# debugging. Natvis is automatic for debug builds. You can opt into it release builds by defining the symbol WINRT_NATVIS.

The Visual Studio project templates for C++/WinRT are described in the sections below. When you create a new C++/WinRT project with the latest version of the VSIX extension installed, the new C++/WinRT project automatically installs the [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/). The **Microsoft.Windows.CppWinRT** NuGet package provides C++/WinRT build support (MSBuild properties and targets), making your project portable between a development machine and a build agent (on which only the NuGet package, and not the VSIX extension, is installed).

Alternatively, you can convert an existing project by manually installing the **Microsoft.Windows.CppWinRT** NuGet package. After installing (or updating to) the latest version of the VSIX extension, open the existing project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project. Once you've added the package, you'll get C++/WinRT MSBuild support for the project, including invoking the `cppwinrt.exe` tool.

> [!IMPORTANT]
> If you have projects that were created with (or upgraded to work with) a version of the VSIX extension earlier than 1.0.190128.4, then see [Earlier versions of the VSIX extension](#earlier-versions-of-the-vsix-extension). That section contains important info about the configuration of your projects, which you'll need to know to upgrade them to use the latest version of the VSIX extension.

- Because C++/WinRT uses features from the C++17 standard, the NuGet package sets project property **C/C++** > **Language** > **C++ Language Standard** > **ISO C++17 Standard (/std:c++17)** in Visual Studio.
- It also adds the [/bigobj](/cpp/build/reference/bigobj-increase-number-of-sections-in-dot-obj-file) compiler option.
- It adds the [/await](/cpp/build/reference/await-enable-coroutine-support) compiler option in order to enable `co_await`.
- It instructs the XAML compiler to emit C++/WinRT codegen.
- You might also want to set **Conformance mode: Yes (/permissive-)**, which further constrains your code to be standards-compliant.
- Another project property to be aware of is **C/C++** > **General** > **Treat Warnings As Errors**. Set this to **Yes(/WX)** or **No (/WX-)** to taste. Sometimes, source files generated by the `cppwinrt.exe` tool generate warnings until you add your implementation to them.

With your system set up as described above, you'll be able to create and build, or open, a C++/WinRT project in Visual Studio, and deploy it.

As of version 2.0, the **Microsoft.Windows.CppWinRT** NuGet package includes the `cppwinrt.exe` tool. You can point the `cppwinrt.exe` tool at a Windows Runtime metadata (`.winmd`) file to generate a header-file-based standard C++ library that *projects* the APIs described in the metadata for consumption from C++/WinRT code. Windows Runtime metadata (`.winmd`) files provide a canonical way of describing a Windows Runtime API surface. By pointing `cppwinrt.exe` at metadata, you can generate a library for use with any runtime class implemented in a second- or third-party Windows Runtime component, or implemented in your own application. For more info, see [Consume APIs with C++/WinRT](consume-apis.md).

With C++/WinRT, you can also implement your own runtime classes using standard C++, without resorting to COM-style programming. For a runtime class, you just describe your types in an IDL file, and `midl.exe` and `cppwinrt.exe` generate your implementation boilerplate source code files for you. You can alternatively just implement interfaces by deriving from a C++/WinRT base class. For more info, see [Author APIs with C++/WinRT](author-apis.md).

For a list of customization options for the `cppwinrt.exe` tool, set via project properties, see the Microsoft.Windows.CppWinRT NuGet package [readme](https://github.com/microsoft/cppwinrt/blob/master/nuget/readme.md#customizing).

You can identify a project that uses the C++/WinRT MSBuild support by the presence of the **Microsoft.Windows.CppWinRT** NuGet package installed within the project.

Here are the Visual Studio project templates provided by the VSIX extension.

### Blank App (C++/WinRT)
A project template for a Universal Windows Platform (UWP) app that has a XAML user-interface.

Visual Studio provides XAML compiler support to generate implementation and header stubs from the Interface Definition Language (IDL) (`.idl`) file that sits behind each XAML markup file. In an IDL file, define any local runtime classes that you want to reference in your app's XAML pages, and then build the project once to generate implementation templates in `Generated Files`, and stub type definitions in `Generated Files\sources`. Then use those stub type definitions for reference to implement your local runtime classes. See [Factoring runtime classes into Midl files (.idl)](./author-apis.md#factoring-runtime-classes-into-midl-files-idl).

The XAML design surface support in Visual Studio 2019 for C++/WinRT is close to parity with C#. In Visual Studio 2019, you can use the **Events** tab of the **Properties** window to add event handlers within a C++/WinRT project. You can also add event handlers to your code manually&mdash;see [Handle events by using delegates in C++/WinRT](handle-events.md) for more info.

### Core App (C++/WinRT)
A project template for a Universal Windows Platform (UWP) app that doesn't use XAML.

Instead, it uses the C++/WinRT Windows namespace header for the Windows.ApplicationModel.Core namespace. After building and running, click on an empty space to add a colored square; then click on a colored square to drag it.

### Windows Console Application (C++/WinRT)
A project template for a C++/WinRT client application for Windows Desktop, with a console user-interface.

### Windows Desktop Application (C++/WinRT)
A project template for a C++/WinRT client application for Windows Desktop, which displays a Windows Runtime [Windows.Foundation.Uri](/uwp/api/windows.foundation.uri) inside a Win32 **MessageBox**.

### Windows Runtime Component (C++/WinRT)
A project template for a component; typically for consumption from a Universal Windows Platform (UWP).

This template demonstrates the `midl.exe` > `cppwinrt.exe` toolchain, where Windows Runtime metadata (`.winmd`) is generated from IDL, and then implementation and header stubs are generated from the Windows Runtime metadata.

In an IDL file, define the runtime classes in your component, their default interface, and any other interfaces they implement. Build the project once to generate `module.g.cpp`, `module.h.cpp`, implementation templates in `Generated Files`, and stub type definitions in `Generated Files\sources`. Then use those the stub type definitions for reference to implement the runtime classes in your component. See [Factoring runtime classes into Midl files (.idl)](./author-apis.md#factoring-runtime-classes-into-midl-files-idl).

Bundle the built Windows Runtime component binary and its `.winmd` with the UWP app consuming them.

## Earlier versions of the VSIX extension
We recommend that you install (or update to) the latest version of the [VSIX extension](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264). It is configured to update itself by default. If you do that, and you have projects that were created with a version of the VSIX extension earlier than 1.0.190128.4, then this section contains important info about upgrading those projects to work with the new version. If you don't update, then you'll still find the info in this section useful.

In terms of supported Windows SDK and Visual Studio versions, and Visual Studio configuration, the info in the [Visual Studio support for C++/WinRT, XAML, the VSIX extension, and the NuGet package](#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package) section above applies to earlier versions of the VSIX extension. The info below describes important differences regarding the behavior and configuration of projects created with (or upgraded to work with) earlier versions.

### Created earlier than 1.0.181002.2
If your project was created with a version of the VSIX extension earlier than 1.0.181002.2, then C++/WinRT build support was built into that version of the VSIX extension. Your project has the `<CppWinRTEnabled>true</CppWinRTEnabled>` property set in the `.vcxproj` file.

```xml
<Project ...>
    <PropertyGroup Label="Globals">
        <CppWinRTEnabled>true</CppWinRTEnabled>
...
```

You can upgrade your project by manually installing the **Microsoft.Windows.CppWinRT** NuGet package. After installing (or upgrading to) the latest version of the VSIX extension, open your project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for your project.

### Created with (or upgraded to) between 1.0.181002.2 and 1.0.190128.3
If your project was created with a version of the VSIX extension between 1.0.181002.2 and 1.0.190128.3, inclusive, then the **Microsoft.Windows.CppWinRT** NuGet package was installed in the project automatically by the project template. You might also have upgraded an older project to use a version of the VSIX extension in this range. If you did, then&mdash;since build support was also still present in versions of the VSIX extension in this range&mdash;your upgraded project may or may not have the **Microsoft.Windows.CppWinRT** NuGet package installed.

To upgrade your project, follow the instructions in the previous section and ensure that your project does have the **Microsoft.Windows.CppWinRT** NuGet package installed.

### Invalid upgrade configurations
With the latest version of the VSIX extension, it's not valid for a project to have the `<CppWinRTEnabled>true</CppWinRTEnabled>` property if it doesn't also have the **Microsoft.Windows.CppWinRT** NuGet package installed. A project with this configuration produces the build error message, "The C++/WinRT VSIX no longer provides project build support.  Please add a project reference to the Microsoft.Windows.CppWinRT Nuget package."

As mentioned above, a C++/WinRT project now needs to have the NuGet package installed in it.

Since the `<CppWinRTEnabled>` element is now obsolete, you can optionally edit your `.vcxproj`, and delete the element. It's not strictly necessary, but it's an option.

Also, if your `.vcxproj` contains `<RequiredBundles>$(RequiredBundles);Microsoft.Windows.CppWinRT</RequiredBundles>`, then you can remove it so that you can build without requiring the C++/WinRT VSIX extension to be installed.

## SDK support for C++/WinRT
Although it is now present only for compatibility reasons, as of version 10.0.17134.0 (Windows 10, version 1803), the Windows SDK contains a header-file-based standard C++ library for consuming first-party Windows APIs (Windows Runtime APIs in Windows namespaces). Those headers are inside the folder `%WindowsSdkDir%Include<WindowsTargetPlatformVersion>\cppwinrt\winrt`. As of the Windows SDK version 10.0.17763.0 (Windows 10, version 1809), these headers are generated for you inside your project's *$(GeneratedFilesDir)* folder.

Again for compatibility, the Windows SDK also comes with the `cppwinrt.exe` tool. However, we recommend that you instead install and use the most recent version of `cppwinrt.exe`, which is included with the **Microsoft.Windows.CppWinRT** NuGet package. That package, and `cppwinrt.exe`, are described in the sections above.

## Custom types in the C++/WinRT projection
In your C++/WinRT programming, you can use standard C++ language features and [Standard C++ data types and C++/WinRT](std-cpp-data-types.md)&mdash;including some C++ Standard Library data types. But you'll also become aware of some custom data types in the projection, and you can choose to use them. For example, we use [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) in the quick-start code example in [Get started with C++/WinRT](get-started.md).

[**winrt::com_array**](/uwp/cpp-ref-for-winrt/com-array) is another type that you're likely to use at some point. But you're less likely to directly use a type such as [**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view). Or you may choose not to use it so that you won't have any code to change if and when an equivalent type appears in the C++ Standard Library.

> [!WARNING]
> There are also types that you might see if you closely study the C++/WinRT Windows namespace headers. An example is **winrt::param::hstring**, but there are collection examples too. These exist solely to optimize the binding of input parameters, and they yield big performance improvements and make most calling patterns "just work" for related standard C++ types and containers. These types are only ever used by the projection in cases where they add most value. They're highly optimized and they're not for general use; don't be tempted to use them yourself. Nor should you use anything from the `winrt::impl` namespace, since those are implementation types, and therefore subject to change. You should continue to use standard types, or types from the [winrt namespace](/uwp/cpp-ref-for-winrt/winrt).
>
> Also see [Passing parameters into the ABI boundary](./pass-parms-to-abi.md).

## Important APIs
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264)
* [Get started with C++/WinRT](get-started.md)
* [Standard C++ data types and C++/WinRT](std-cpp-data-types.md)
* [String handling in C++/WinRT](strings.md)
* [Windows Runtime APIs](/uwp/api/)