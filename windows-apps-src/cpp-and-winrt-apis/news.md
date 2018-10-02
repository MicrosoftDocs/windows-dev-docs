---
author: stevewhims
description: News and changes to C++/WinRT.
title: What's new in C++/WinRT
ms.author: stwhi
ms.date: 09/28/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, news, what's, new
ms.localizationpriority: medium
---

# What's new in C++/WinRT

The table below contains news and changes to [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) in the latest generally-available version of the Windows SDK, which is 10.0.17763.0 (Windows 10, version 1809). These changes may also be present in later SDK Insider Preview versions.

| New or changed feature | More info |
| - | - |
| The Visual Studio project system format has changed. | See [How to retarget your C++/WinRT project to a later version of the Windows SDK](#how-to-retarget-your-cwinrt-project-to-a-later-version-of-the-windows-sdk), below. |
| For it to compile, C++/WinRT doesn't depend on headers from the Windows SDK. | See [Isolation from Windows SDK header files](#isolation-from-windows-sdk-header-files), below. |
| There are new functions and base classes to help you pass a collection object to a Windows Runtime function, or to implement your own collection properties and collection types. | See [Collections with C++/WinRT](collections.md). |
| You can use the [{Binding}](/windows/uwp/xaml-platform/binding-markup-extension) markup extension with your C++/WinRT runtime classes. | For more info, and code examples, see [Data binding overview](/windows/uwp/data-binding/data-binding-quickstart). |
| Support for canceling a coroutine allows you to register a cancellation callback. | For more info, and code examples, see [Canceling an asychronous operation, and cancellation callbacks](concurrency.md#canceling-an-asychronous-operation-and-cancellation-callbacks). |
| When creating a delegate pointing to a member function, you can establish a strong or a weak reference to the current object (instead of a raw *this* pointer) at the point where the handler is registered. | For more info, and code examples, see the **If you use a member function as a delegate** sub-section in the section [Safely accessing the *this* pointer with an event-handling delegate](weak-references.md#safely-accessing-the-this-pointer-with-an-event-handling-delegate). |
| Bugs are fixed that were uncovered by Visual Studio's improved conformance to the C++ standard. The LLVM and Clang toolchain is also better leveraged to validate C++/WinRT's standards conformance. | You'll no longer encounter the issue described in [Why won't my new project compile? I'm using Visual Studio 2017 (version 15.8.0 or higher), and SDK version 17134](faq.md#why-wont-my-new-project-compile-im-using-visual-studio-2017-version-1580-or-higher-and-sdk-version-17134) |

## How to retarget your C++/WinRT project to a later version of the Windows SDK

The method for retargeting your project that's likely to result in the fewest compiler and linker issue is also the most labor-intensive. That method involves creating a new project (targeting the Windows SDK version of your choice), and then copying files over to your new project from your old. There will be sections of your old `.vcxproj` and `.vcxproj.filters` files that you can just copy over to save you adding files in Visual Studio.

However, there are two other ways to retarget your project in Visual Studio.

- Go to project property **General** \> **Windows SDK Version**, and select **All Configurations** and **All Platforms**. Set **Windows SDK Version** to the version that you want to target.
- In **Solution Explorer**, right-click the project node, click **Retarget Projects**, choose the version(s) you wish to target, and then click **OK**.

If you encounter any compiler or linker errors after using either of these two methods, then you can try cleaning the solution (**Build** > **Clean Solution** and/or manually delete all temporary folders and files) before trying to build again.

If the C++ compiler produces "*error C2039: 'IUnknown': is not a member of '\`global namespace''*", then add `#include <unknwn.h>` to the top of your `pch.h` file (before you include any C++/WinRT headers).

You may also need to add `#include <hstring.h>` after that.

If the C++ linker produces "*error LNK2019: unresolved external symbol _WINRT_CanUnloadNow@0 referenced in function _VSDesignerCanUnloadNow@0*", then you can resolve that by adding `#define _VSDESIGNER_DONT_LOAD_AS_DLL` to your `pch.h` file.

## Isolation from Windows SDK header files

For it to compile, C++/WinRT no longer depends on header files from the Windows SDK. Header files in the C run-time library (CRT) and the C++ Standard Template Library (STL) also don't include any Windows SDK headers. And that improves standards compliance, avoids inadvertent dependencies, and greatly reduces the number of macros that you have to guard against.

This independence means that C++/WinRT is now more portable and standards compliant, and it furthers the possibility of it becoming a cross-compiler and cross-platform library. It also means that the C++/WinRT headers aren't adversely affected macros.

If you previously left it to C++/WinRT to include any Windows headers in your project, then you'll now need to include them yourself. It is, in any case, always best practice to explicitly include the headers that you depend on, and not leave it to another library to include them for you.

Currently, the only exceptions to Windows SDK header file isolation are for intrinsics, and numerics. There are no known issues with these last remaining dependencies.

In your project, you can re-enable interop with the Windows SDK headers if you need to. You might, for example, want to implement a COM interface (rooted in [**IUnknown**](https://msdn.microsoft.com/library/windows/desktop/ms680509)). For that example, include `unknwn.h` before you include any C++/WinRT headers. Doing so causes the C++/WinRT base library to enable various hooks to support classic COM interfaces. For a code example, see [Author COM components with C++/WinRT](author-coclasses.md). Similarly, explicitly include any other Windows SDK headers that declare types and/or functions that you want to call.
