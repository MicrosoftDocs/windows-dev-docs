---
author: stevewhims
description: News and changes to C++/WinRT.
title: What's new in C++/WinRT
ms.author: stwhi
ms.date: 10/03/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, news, what's, new
ms.localizationpriority: medium
---

# What's new in C++/WinRT

The table below contains news and changes to [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) in the latest generally-available version of the Windows SDK, which is 10.0.17763.0 (Windows 10, version 1809). These changes may also be present in later SDK Insider Preview versions.

## News, and changes, in Windows SDK version 10.0.17763.0 (Windows 10, version 1809)

| New or changed feature | More info |
| - | - |
| **Breaking change**. For it to compile, C++/WinRT doesn't depend on headers from the Windows SDK. | See [Isolation from Windows SDK header files](#isolation-from-windows-sdk-header-files), below. |
| The Visual Studio project system format has changed. | See [How to retarget your C++/WinRT project to a later version of the Windows SDK](#how-to-retarget-your-cwinrt-project-to-a-later-version-of-the-windows-sdk), below. |
| There are new functions and base classes to help you pass a collection object to a Windows Runtime function, or to implement your own collection properties and collection types. | See [Collections with C++/WinRT](collections.md). |
| You can use the [{Binding}](/windows/uwp/xaml-platform/binding-markup-extension) markup extension with your C++/WinRT runtime classes. | For more info, and code examples, see [Data binding overview](/windows/uwp/data-binding/data-binding-quickstart). |
| Support for canceling a coroutine allows you to register a cancellation callback. | For more info, and code examples, see [Canceling an asychronous operation, and cancellation callbacks](concurrency.md#canceling-an-asychronous-operation-and-cancellation-callbacks). |
| When creating a delegate pointing to a member function, you can establish a strong or a weak reference to the current object (instead of a raw *this* pointer) at the point where the handler is registered. | For more info, and code examples, see the **If you use a member function as a delegate** sub-section in the section [Safely accessing the *this* pointer with an event-handling delegate](weak-references.md#safely-accessing-the-this-pointer-with-an-event-handling-delegate). |
| Bugs are fixed that were uncovered by Visual Studio's improved conformance to the C++ standard. The LLVM and Clang toolchain is also better leveraged to validate C++/WinRT's standards conformance. | You'll no longer encounter the issue described in [Why won't my new project compile? I'm using Visual Studio 2017 (version 15.8.0 or higher), and SDK version 17134](faq.md#why-wont-my-new-project-compile-im-using-visual-studio-2017-version-1580-or-higher-and-sdk-version-17134) |

Other changes.

- **Breaking change**. [**winrt::get_abi(winrt::hstring const&)**](/uwp/cpp-ref-for-winrt/get-abi) now returns `void*` instead of `HSTRING`. You can use `static_cast<HSTRING>(get_abi(my_hstring));` to get an HSTRING.
- **Breaking change**. [**winrt::put_abi(winrt::hstring&)**](/uwp/cpp-ref-for-winrt/put-abi) now returns `void**` instead of `HSTRING*`. You can use `reinterpret_cast<HSTRING*>(put_abi(my_hstring));` to get an HSTRING*.
- **Breaking change**. HRESULT is now projected as **winrt::hresult**. If you need an HRESULT (to do type checking, or to support type traits), then you can `static_cast` a **winrt::hresult**. Otherwise, **winrt::hresult** converts to HRESULT, as long as you include `unknwn.h` before you include any C++/WinRT headers.
- **Breaking change**. GUID is now projected as **winrt::guid**. For APIs that you implement, you must use **winrt::guid** for GUID parameters. Otherwise, **winrt::hresult** converts to GUID, as long as you include `unknwn.h` before you include any C++/WinRT headers.
- **Breaking change**. The [**winrt::handle_type constructor**](/uwp/cpp-ref-for-winrt/handle-type#handletypehandletype-constructor) has been hardened by making it explicit (it's now harder to write incorrect code with it). If you need to assign a raw handle value, call the [**handle_type::attach function**](/uwp/cpp-ref-for-winrt/handle-type#handletypeattach-function) instead.
- **Breaking change**. The signatures of **WINRT_CanUnloadNow** and **WINRT_GetActivationFactory** have changed. You mustn't declare these functions at all. Instead, include `winrt/base.h` (which is automatically included if you include any C++/WinRT Windows namespace header files) to include the declarations of these functions.
- For the [**winrt::clock struct**](/uwp/cpp-ref-for-winrt/clock), **from_FILETIME/to_FILETIME** are deprecated in favor of **from_file_time/to_file_time**.
- APIs that expect **IBuffer** parameters are simplified. Although most APIs prefer collections or arrays, enough APIs rely on **IBuffer** that it needed to be easier to use such APIs from C++. This update provides direct access to the data behind an **IBuffer** implementation, using the same data naming convention used by the C++ Standard Library containers. This also avoids colliding with metadata names that conventionally begin with an uppercase letter.
- Improved code generation: various improvements to reduce code size, improve inlining, and optimize factory caching.
- Removed unnecessary recursion. When the command-line refers to a folder, rather than to a specific `.winmd`, the `cppwinrt.exe` tool no longer searches recursively for `.winmd` files. The `cppwinrt.exe` tool also now handles duplicates more intelligently, making it more resilient to user error, and to poorly-formed `.winmd` files.
- Hardened smart pointers. Formerly, the event revokers failed to revoke when move-assigned a new value. This helped uncover an issue where smart pointer classes weren't reliably handling self-assignment; rooted in the [**winrt::com_ptr struct template**](/uwp/cpp-ref-for-winrt/com-ptr). **winrt::com_ptr** has been fixed, and the event revokers fixed to handle move semantics correctly so that they revoke upon assignment.

> [!NOTE]
> With version 1.0.181002.2 (or later) of the [C++/WinRT Visual Studio Extension (VSIX)](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix) installed, creating a new C++/WinRT project automatically installs the [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) for that project. The Microsoft.Windows.CppWinRT NuGet package provides improved C++/WinRT project build support, making your project portable between a development machine and a build agent (on which only the NuGet package, and not the VSIX, is installed).
>
> For an existing project&mdash;after you've installed version 1.0.181002.2 (or later) of the VSIX&mdash;we recommend that you open the project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project.


## Isolation from Windows SDK header files

This is potentially a breaking change for your code.

For it to compile, C++/WinRT no longer depends on header files from the Windows SDK. Header files in the C run-time library (CRT) and the C++ Standard Template Library (STL) also don't include any Windows SDK headers. And that improves standards compliance, avoids inadvertent dependencies, and greatly reduces the number of macros that you have to guard against.

This independence means that C++/WinRT is now more portable and standards compliant, and it furthers the possibility of it becoming a cross-compiler and cross-platform library. It also means that the C++/WinRT headers aren't adversely affected macros.

If you previously left it to C++/WinRT to include any Windows headers in your project, then you'll now need to include them yourself. It is, in any case, always best practice to explicitly include the headers that you depend on, and not leave it to another library to include them for you.

Currently, the only exceptions to Windows SDK header file isolation are for intrinsics, and numerics. There are no known issues with these last remaining dependencies.

In your project, you can re-enable interop with the Windows SDK headers if you need to. You might, for example, want to implement a COM interface (rooted in [**IUnknown**](https://msdn.microsoft.com/library/windows/desktop/ms680509)). For that example, include `unknwn.h` before you include any C++/WinRT headers. Doing so causes the C++/WinRT base library to enable various hooks to support classic COM interfaces. For a code example, see [Author COM components with C++/WinRT](author-coclasses.md). Similarly, explicitly include any other Windows SDK headers that declare types and/or functions that you want to call.

## How to retarget your C++/WinRT project to a later version of the Windows SDK

The method for retargeting your project that's likely to result in the fewest compiler and linker issue is also the most labor-intensive. That method involves creating a new project (targeting the Windows SDK version of your choice), and then copying files over to your new project from your old. There will be sections of your old `.vcxproj` and `.vcxproj.filters` files that you can just copy over to save you adding files in Visual Studio.

However, there are two other ways to retarget your project in Visual Studio.

- Go to project property **General** \> **Windows SDK Version**, and select **All Configurations** and **All Platforms**. Set **Windows SDK Version** to the version that you want to target.
- In **Solution Explorer**, right-click the project node, click **Retarget Projects**, choose the version(s) you wish to target, and then click **OK**.

If you encounter any compiler or linker errors after using either of these two methods, then you can try cleaning the solution (**Build** > **Clean Solution** and/or manually delete all temporary folders and files) before trying to build again.

If the C++ compiler produces "*error C2039: 'IUnknown': is not a member of '\`global namespace''*", then add `#include <unknwn.h>` to the top of your `pch.h` file (before you include any C++/WinRT headers).

You may also need to add `#include <hstring.h>` after that.

If the C++ linker produces "*error LNK2019: unresolved external symbol _WINRT_CanUnloadNow@0 referenced in function _VSDesignerCanUnloadNow@0*", then you can resolve that by adding `#define _VSDESIGNER_DONT_LOAD_AS_DLL` to your `pch.h` file.
