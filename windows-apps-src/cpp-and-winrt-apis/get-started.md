---
author: stevewhims
description: To get you up to speed with using C++/WinRT, this topic walks through a simple code example.
title: Get started with C++/WinRT
ms.author: stwhi
ms.date: 10/19/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, get, getting, started
ms.localizationpriority: medium
---

# Get started with C++/WinRT

To get you up to speed with using [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt), this topic walks through a simple code example based on a new **Windows Console Application (C++/WinRT)** project. This topic also shows how to [add C++/WinRT support to a Windows Desktop application project](#modify-a-windows-desktop-application-project-to-add-cwinrt-support).

> [!IMPORTANT]
> If you're using Visual Studio 2017 (version 15.8.0 or higher), and targeting the Windows SDK version 10.0.17134.0 (Windows 10, version 1803), then a newly created C++/WinRT project may fail to compile with the error "*error C3861: 'from_abi': identifier not found*", and with other errors originating in *base.h*. The solution is to either target a later (more conformant) version of the Windows SDK, or set project property **C/C++** > **Language** > **Conformance mode: No** (also, if **/permissive-** appears in project property **C/C++** > **Language** > **Command Line** under **Additional Options**, then delete it).

## A C++/WinRT quick-start

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

Create a new **Windows Console Application (C++/WinRT)** project.

Edit `pch.h` and `main.cpp` to look like this.

```cppwinrt
// pch.h
...
#include <iostream>
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>
...
```

```cppwinrt
// main.cpp
#include "pch.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

int main()
{
    winrt::init_apartment();

    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;
    SyndicationFeed syndicationFeed = syndicationClient.RetrieveFeedAsync(rssFeedUri).get();
    for (const SyndicationItem syndicationItem : syndicationFeed.Items())
    {
        winrt::hstring titleAsHstring = syndicationItem.Title().Text();
        std::wcout << titleAsHstring.c_str() << std::endl;
    }
}
```

Let's take the short code example above piece by piece, and explain what's going on in each part.

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>
```

The headers that we include are part of the SDK, inside the folder `%WindowsSdkDir%Include<WindowsTargetPlatformVersion>\cppwinrt\winrt`. Visual Studio includes that path in its *IncludePath* macro. The headers contain Windows APIs projected into C++/WinRT. In other words, for each Windows type, C++/WinRT defines a C++-friendly equivalent (called the *projected type*). A projected type has the same fully-qualified name as the Windows type, but it's placed in the C++ **winrt** namespace. Putting these includes in your precompiled header reduces incremental build times.

> [!IMPORTANT]
> Whenever you want to use a type from a Windows namespaces, include the corresponding C++/WinRT Windows namespace header file, as shown. The *corresponding* header is the one with the same name as the type's namespace. For example, to use the C++/WinRT projection for the [**Windows::Foundation::Collections::PropertySet**](/uwp/api/windows.foundation.collections.propertyset) runtime class, `#include <winrt/Windows.Foundation.Collections.h>`.

```cppwinrt
using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;
```

The `using namespace` directives are optional, but convenient. The pattern shown above for such directives (allowing unqualified name lookup for anything in the **winrt** namespace) is suitable for when you're beginning a new project and C++/WinRT is the only language projection you're using inside of that project. If, on the other hand, you're mixing C++/WinRT code with [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and/or SDK application binary interface (ABI) code (you're either porting from, or interoperating with, one or both of those models), then see the topics [Interop between C++/WinRT and C++/CX](interop-winrt-cx.md), [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md), and [Interop between C++/WinRT and the ABI](interop-winrt-abi.md).

```cppwinrt
winrt::init_apartment();
```

The call to **winrt::init_apartment** initializes COM; by default, in a multithreaded apartment.

```cppwinrt
Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
SyndicationClient syndicationClient;
```

Stack-allocate two objects: they represent the uri of the Windows blog, and a syndication client. We construct the uri with a simple wide string literal (see [String handling in C++/WinRT](strings.md) for more ways you can work with strings).

```cppwinrt
SyndicationFeed syndicationFeed = syndicationClient.RetrieveFeedAsync(rssFeedUri).get();
```

[**SyndicationClient::RetrieveFeedAsync**](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync) is an example of an asynchronous Windows Runtime function. The code example receives an asynchronous operation object from **RetrieveFeedAsync**, and it calls **get** on that object to block the calling thread and wait for the result (which is a syndication feed, in this case). For more about concurrency, and for non-blocking techniques, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md).

```cppwinrt
for (const SyndicationItem syndicationItem : syndicationFeed.Items()) { ... }
```

[**SyndicationFeed.Items**](/uwp/api/windows.web.syndication.syndicationfeed.items) is a range, defined by the iterators returned from **begin** and **end** functions (or their constant, reverse, and constant-reverse variants). Because of this, you can enumerate **Items** with either a range-based `for` statement, or with the **std::for_each** template function.

```cppwinrt
winrt::hstring titleAsHstring = syndicationItem.Title().Text();
std::wcout << titleAsHstring.c_str() << std::endl;
```

Gets the feed's title text, as a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) object (more details in [String handling in C++/WinRT](strings.md)). The **hstring** is then output, via the **c_str** function, which reflects the pattern used with C++ Standard Library strings.

As you can see, C++/WinRT encourages modern, and class-like, C++ expressions such as `syndicationItem.Title().Text()`. This is a different, and cleaner, programming style from traditional COM programming. You don't need to directly initialize COM, work with COM pointers.

Nor do you need to handle HRESULT return codes. C++/WinRT converts error HRESULTs to exceptions such as [**winrt::hresult-error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) for a natural and modern programming style. For more info about error-handling, and code examples, see [Error handling with C++/WinRT](error-handling.md).

## Modify a Windows Desktop application project to add C++/WinRT support

This section shows you how you can add C++/WinRT support to a Windows Desktop application project that you might have. If you don't have an existing Windows Desktop application project, then you can follow along with these steps by first creating one. For example, open Visual Studio and create a **Visual C++** \> **Windows Desktop** \> **Windows Desktop Application** project.

### Set project properties

Go to project property **General** \> **Windows SDK Version**, and select **All Configurations** and **All Platforms**. Ensure that **Windows SDK Version** is set to 10.0.17134.0 (Windows 10, version 1803) or greater.

Confirm that you're not affected by [Why won't my new project compile?](/windows/uwp/cpp-and-winrt-apis/faq).

Because C++/WinRT uses features from the C++17 standard, set project property **C/C++** > **Language** > **C++ Language Standard** to *ISO C++17 Standard (/std:c++17)*.

### The precompiled header

Rename your `stdafx.h` and `stdafx.cpp` to `pch.h` and `pch.cpp`, respectively. Set project property **C/C++** > **Precompiled Headers** > **Precompiled Header File** to *pch.h*.

Find and replace all `#include "stdafx.h"` with `#include "pch.h"`.

In `pch.h`, include `winrt/base.h`.

```cppwinrt
// pch.h
...
#include <winrt/base.h>
```

## Linking

The C++/WinRT language projection depends on certain Windows Runtime free (non-member) functions, and entry points, that require linking to the [WindowsApp.lib](/uwp/win32-and-com/win32-apis) umbrella library. This section describes three ways of satisfying the linker.

The first option is to add to your Visual Studio project all of the C++/WinRT MSBuild properties and targets. Edit your `.vcxproj` file, find `<PropertyGroup Label="Globals">` and, inside that property group, set the property `<CppWinRTEnabled>true</CppWinRTEnabled>`.

Alternatively, you can use project link settings to explicitly link `WindowsApp.lib`.

Or, you can do it in source code (in `pch.h`, for example) like this.

```cppwinrt
#pragma comment(lib, "windowsapp")
```

You can now compile and link, and add C++/WinRT code to your project (for example, the code shown in the [A C++/WinRT quick-start](#a-cwinrt-quick-start) section, above)

## Important APIs
* [SyndicationClient::RetrieveFeedAsync method](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync)
* [SyndicationFeed.Items property](/uwp/api/windows.web.syndication.syndicationfeed.items)
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt::hresult-error struct](/uwp/cpp-ref-for-winrt/error-handling/hresult-error)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Error handling with C++/WinRT](error-handling.md)
* [Interop between C++/WinRT and C++/CX](interop-winrt-cx.md)
* [Interop between C++/WinRT and the ABI](interop-winrt-abi.md)
* [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md)
* [String handling in C++/WinRT](strings.md)
