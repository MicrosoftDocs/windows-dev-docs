---
author: stevewhims
description: To get you up to speed with using C++/WinRT, this topic walks through a simple code example. We also describe how to use the C++/WinRT projection headers.
title: Get started with C++/WinRT
ms.author: stwhi
ms.date: 05/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, get, getting, started
ms.localizationpriority: medium
---

# Get started with [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
To get you up to speed with using C++/WinRT, this topic walks through a simple code example. We also describe how to use the C++/WinRT projection headers.

## A C++/WinRT quick-start
> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

Create a new **Windows Console Application (C++/WinRT)** project. Edit `main.cpp` to look like this.

```cppwinrt
// main.cpp
#include "pch.h"
#include <iostream>
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>

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
        hstring titleAsHstring = syndicationItem.Title().Text();
        std::wcout << titleAsHstring.c_str() << std::endl;
    }
}
```

The included headers `winrt/Windows.Foundation.h` and `winrt/Windows.Web.Syndication.h` are in the SDK, inside the folder `%WindowsSdkDir%Include<WindowsTargetPlatformVersion>\cppwinrt\winrt`. Visual Studio includes that path in its *IncludePath* macro. The headers contain Windows APIs projected into C++/WinRT. In other words, for each Windows type, C++/WinRT defines a C++-friendly equivalent (called the *projected type*). A projected type has the same fully-qualified name as the Windows type, but it's placed in the C++ **winrt** namespace. The `using namespace` directives are optional, but convenient.

> [!NOTE]
> Both [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and the Windows SDK declare types in the root namespace **Windows**. These distinct namespaces let you migrate from C++/CX to C++/WinRT at your own pace.

> [!IMPORTANT]
> Whenever you want to use a type from a Windows namespaces, include the corresponding C++/WinRT Windows namespace header file as shown above. The *corresponding* header is the one with the same name as the type's namespace. For example, to use the C++/WinRT projection for the [**Windows::Foundation::Collections::PropertySet**](/uwp/api/windows.foundation.collections.propertyset) runtime class, `#include <winrt/Windows.Foundation.Collections.h>`.

[**SyndicationClient::RetrieveFeedAsync**](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync) is an example of an asynchronous Windows Runtime function. The code example receives an asynchronous operation object from **RetrieveFeedAsync**, and it calls **get** on that object to block the calling thread and wait for the results. For more about concurrency, and for non-blocking techniques, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md).

[**SyndicationFeed.Items**](/uwp/api/windows.web.syndication.syndicationfeed.items) is a range, defined by the iterators returned from **begin** and **end** functions (or their constant, reverse, and constant-reverse variants). Because of this, you can enumerate **Items** with either a range-based `for` statement, or with the **std::for_each** template function.

The code then gets the feed's title text, as a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) object (see [String handling in C++/WinRT](strings.md)). The **hstring** is then output, via the **c_str** function, which will be familiar to you if you've used strings from the C++ Standard Library.

As you can see, C++/WinRT encourages modern, and class-like, C++ expressions such as `syndicationItem.Title().Text()`. This is a different, and cleaner programming style from traditional COM programming. You don't need to explicitly initialize COM (**winrt::init_apartment** does that for you), work with COM pointers, nor handle HRESULT return codes. C++/WinRT converts error HRESULTs to exceptions such as [**winrt::hresult-error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) for a natural and modern programming style.

## C++/WinRT projection headers
The headers that you include are inside the `%WindowsSdkDir%Include<WindowsTargetPlatformVersion>\cppwinrt\winrt` folder. It's common for a type in a subordinate namespace to reference types in its immediate parent namespace. Consequently, each C++/WinRT projection header automatically includes its parent namespace header file; so you don't *need* to explicitly include it. Although, if you do, there will be no error.

For example, for the [**Windows::Security::Cryptography::Certificates**](/uwp/api/windows.security.cryptography.certificates) namespace, the equivalent C++/WinRT type definitions reside in `winrt/Windows.Security.Cryptography.Certificates.h`. Types in **Windows::Security::Cryptography::Certificates** require types in the parent **Windows::Security::Cryptography** namespace; and types in that namespace could require types in its own parent, **Windows::Security**.

So, when you include `winrt/Windows.Security.Cryptography.Certificates.h`, that file in turn includes `winrt/Windows.Security.Cryptography.h`; and `winrt/Windows.Security.Cryptography.h` includes `winrt/Windows.Security.h`. That's where the trail stops, since there is no `winrt/Windows.h`. This transitive inclusion process stops at the second-level namespace.

This process transitively includes the header files that provide the necessary *declarations* and *implementations* for the classes defined in parent namespaces.

A member of a type in one namespace can reference one or more types in other, unrelated, namespaces. In order for the compiler to compile these member definitions successfully, the compiler needs to see the type declarations for the closure of all these types. Consequently, each C++/WinRT projection header includes the namespace headers it needs to *declare* any dependent types. Unlike for parent namespaces, this process does *not* pull in the *implementations* for referenced types.

> [!IMPORTANT]
> When you want to actually *use* a type (instantiate, call methods, etc.) declared in an unrelated namespace, you must include the appropriate namespace header file for that type. Only *declarations*, not *implementations*, are automatically included.

For example, if you only include `winrt/Windows.Security.Cryptography.Certificates.h`, then that causes declarations to be pulled in from these namespaces (and so on, transitively).

- Windows.Foundation
- Windows.Foundation.Collections
- Windows.Networking
- Windows.Storage.Streams
- Windows.Security.Cryptography

In other words, some APIs are forward-declared in a header that you've included. But their definitions are in a header that you haven't yet included. So, if you then call [**Windows::Foundation::Uri::RawUri**](/uwp/api/windows.foundation.uri.rawuri), then you'll receive a linker error indicating that the member is undefined. The solution is to explicitly `#include <winrt/Windows.Foundation.h>`. In general, when you see a linker error such as this, include the header named for the API's namespace, and rebuild.

## Important APIs
* [SyndicationClient::RetrieveFeedAsync](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync)
* [SyndicationFeed.Items](/uwp/api/windows.web.syndication.syndicationfeed.items)
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
