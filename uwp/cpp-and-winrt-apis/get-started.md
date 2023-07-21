---
description: To get you up to speed with using C++/WinRT, this topic walks through a simple code example.
title: Get started with C++/WinRT
ms.date: 01/28/2022
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, get, getting, started
ms.localizationpriority: medium
---

# Get started with C++/WinRT

> [!IMPORTANT]
> For info about setting up Visual Studio for C++/WinRT development&mdash;including installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support)&mdash;see [Visual Studio support for C++/WinRT](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

To get you up to speed with using [C++/WinRT](./intro-to-using-cpp-with-winrt.md), this topic walks through a simple code example based on a new **Windows Console Application (C++/WinRT)** project. This topic also shows how to [add C++/WinRT support to a Windows Desktop application project](#modify-a-windows-desktop-application-project-to-add-cwinrt-support).

> [!NOTE]
> While we recommend that you develop with the latest versions of Visual Studio and the Windows SDK, if you're using Visual Studio 2017 (version 15.8.0 or later), and targeting the Windows SDK version 10.0.17134.0 (Windows 10, version 1803), then a newly created C++/WinRT project may fail to compile with the error "*error C3861: 'from_abi': identifier not found*", and with other errors originating in *base.h*. The solution is to either target a later (more conformant) version of the Windows SDK, or set project property **C/C++** > **Language** > **Conformance mode: No** (also, if **/permissive-** appears in project property **C/C++** > **Language** > **Command Line** under **Additional Options**, then delete it).

## A C++/WinRT quick-start

Create a new **Windows Console Application (C++/WinRT)** project.

Edit `pch.h` and `main.cpp` to look like this.

```cppwinrt
// pch.h
#pragma once
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.Web.Syndication.h>
#include <iostream>
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
    syndicationClient.SetRequestHeader(L"user-agent", L"C++/WinRT Test Agent");
    SyndicationFeed syndicationFeed = syndicationClient.RetrieveFeedAsync(rssFeedUri).get();
    for (const SyndicationItem syndicationItem : syndicationFeed.Items())
    {
        winrt::hstring titleAsHstring = syndicationItem.Title().Text();
        
        // A workaround to remove the trademark symbol from the title string, because it causes issues in this case.
        std::wstring titleAsStdWstring{ titleAsHstring.c_str() };
        titleAsStdWstring.erase(remove(titleAsStdWstring.begin(), titleAsStdWstring.end(), L'™'), titleAsStdWstring.end());
        titleAsHstring = titleAsStdWstring;

        std::wcout << titleAsHstring.c_str() << std::endl;
    }
}
```

Let's take the short code example above piece by piece, and explain what's going on in each part.

```cppwinrt
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.Web.Syndication.h>
```

With the default project settings, the included headers come from the Windows SDK, inside the folder `%WindowsSdkDir%Include<WindowsTargetPlatformVersion>\cppwinrt\winrt`. Visual Studio includes that path in its *IncludePath* macro. But there's no strict dependency on the Windows SDK, because your project (via the `cppwinrt.exe` tool) generates those same headers into your project's *$(GeneratedFilesDir)* folder. They'll be loaded from that folder if they can't be found elsewhere, or if you change your project settings.

The headers contain Windows APIs projected into C++/WinRT. In other words, for each Windows type, C++/WinRT defines a C++-friendly equivalent (called the *projected type*). A projected type has the same fully-qualified name as the Windows type, but it's placed in the C++ **winrt** namespace. Putting these includes in your precompiled header reduces incremental build times.

> [!IMPORTANT]
> Whenever you want to use a type from a Windows namespaces, you must `#include` the corresponding C++/WinRT Windows namespace header file, as shown above. The *corresponding* header is the one with the same name as the type's namespace. For example, to use the C++/WinRT projection for the [**Windows::Foundation::Collections::PropertySet**](/uwp/api/windows.foundation.collections.propertyset) runtime class, include the `winrt/Windows.Foundation.Collections.h` header.
> 
> It's usual for a C++/WinRT projection header to automatically include its parent namespace header file. So, for example, `winrt/Windows.Foundation.Collections.h` includes `winrt/Windows.Foundation.h`. But you shouldn't rely on this behavior, since it's an implementation detail that changes over time. You must explicitly include any headers that you need.

```cppwinrt
using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;
```

The `using namespace` directives are optional, but convenient. The pattern shown above for such directives (allowing unqualified name lookup for anything in the **winrt** namespace) is suitable for when you're beginning a new project and C++/WinRT is the only language projection you're using inside of that project. If, on the other hand, you're mixing C++/WinRT code with [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and/or SDK application binary interface (ABI) code (you're either porting from, or interoperating with, one or both of those models), then see the topics [Interop between C++/WinRT and C++/CX](interop-winrt-cx.md), [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md), and [Interop between C++/WinRT and the ABI](interop-winrt-abi.md).

```cppwinrt
winrt::init_apartment();
```

The call to **winrt::init_apartment** initializes the thread in the Windows Runtime; by default, in a multithreaded apartment. The call also initializes COM.

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

[**SyndicationFeed.Items**](/uwp/api/windows.web.syndication.syndicationfeed.items) is a range, defined by the iterators returned from **begin** and **end** functions (or their constant, reverse, and constant-reverse variants). Because of this, you can enumerate **Items** with either a range-based `for` statement, or with the **std::for_each** template function. Whenever you iterate over a Windows Runtime collection like this, you'll need to `#include <winrt/Windows.Foundation.Collections.h>`.

```cppwinrt
winrt::hstring titleAsHstring = syndicationItem.Title().Text();

// Omitted: there's a little bit of extra work here to remove the trademark symbol from the title text.

std::wcout << titleAsHstring.c_str() << std::endl;
```

Gets the feed's title text, as a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) object (more details in [String handling in C++/WinRT](strings.md)). The **hstring** is then output, via the **c_str** function, which reflects the pattern used with C++ Standard Library strings.

As you can see, C++/WinRT encourages modern, and class-like, C++ expressions such as `syndicationItem.Title().Text()`. This is a different, and cleaner, programming style from traditional COM programming. You don't need to directly initialize COM, nor work with COM pointers.

Nor do you need to handle HRESULT return codes. C++/WinRT converts error HRESULTs to exceptions such as [**winrt::hresult-error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) for a natural and modern programming style. For more info about error-handling, and code examples, see [Error handling with C++/WinRT](error-handling.md).

## Modify a Windows Desktop application project to add C++/WinRT support

Some desktop projects (for example, the [WinUI 3 templates in Visual Studio](/windows/apps/winui/winui3/winui-project-templates-in-visual-studio)) have C++/WinRT support built in.

But this section shows you how you can add C++/WinRT support to any Windows Desktop application project that you might have. If you don't have an existing Windows Desktop application project, then you can follow along with these steps by first creating one. For example, open Visual Studio and create a **Visual C++** \> **Windows Desktop** \> **Windows Desktop Application** project.

You can optionally install the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) and the NuGet package. For details, see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

### Set project properties

Go to project property **General** \> **Windows SDK Version**, and select **All Configurations** and **All Platforms**. Ensure that **Windows SDK Version** is set to 10.0.17134.0 (Windows 10, version 1803) or greater.

Confirm that you're not affected by [Why won't my new project compile?](./faq.yml).

Because C++/WinRT uses features from the C++17 standard, set project property **C/C++** > **Language** > **C++ Language Standard** to *ISO C++17 Standard (/std:c++17)*.

### The precompiled header

The default project template creates a precompiled header for you, named either `framework.h`, or `stdafx.h`. Rename that to `pch.h`. If you have a `stdafx.cpp` file, then rename that to `pch.cpp`. Set project property **C/C++** > **Precompiled Headers** > **Precompiled Header** to *Create (/Yc)*, and **Precompiled Header File** to *pch.h*.

Find and replace all `#include "framework.h"` (or `#include "stdafx.h"`) with `#include "pch.h"`.

In `pch.h`, include `winrt/base.h`.

```cppwinrt
// pch.h
...
#include <winrt/base.h>
```

### Linking

The C++/WinRT language projection depends on certain Windows Runtime free (non-member) functions, and entry points, that require linking to the [WindowsApp.lib](/uwp/win32-and-com/win32-apis) umbrella library. This section describes three ways of satisfying the linker.

The first option is to add to your Visual Studio project all of the C++/WinRT MSBuild properties and targets. To do this, install the [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) into your project. Open the project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project.

You can also use project link settings to explicitly link `WindowsApp.lib`. Or, you can do it in source code (in `pch.h`, for example) like this.

```cppwinrt
#pragma comment(lib, "windowsapp")
```

You can now compile and link, and add C++/WinRT code to your project (for example, code similar to that shown in the [A C++/WinRT quick-start](#a-cwinrt-quick-start) section, above).

## The three main scenarios for C++/WinRT

As you use and become familiar with C++/WinRT, and work through the rest of the documentation here, you'll likely notice that there are three main scenarios, as described in the following sections.

### Consuming Windows APIs and types

In other words, *using*, or *calling* APIs. For example, making API calls to communicate using Bluetooth; to stream and present video; to integrate with the Windows shell; and so on. C++/WinRT fully and uncompromisingly supports this category of scenario. For more info, see [Consume APIs with C++/WinRT](./consume-apis.md).

### Authoring Windows APIs and types

In other words, *producing* APIs and types. For example, producing the kinds of APIs described in the section above; or the graphics APIs; the storage and file system APIs; the networking APIs, and so on. For more info, see [Author APIs with C++/WinRT](./author-apis.md).

Authoring APIs with C++/WinRT is a little more involved than consuming them, because you must use IDL to define the shape of the API before you can implement it. There's a walkthrough of doing that in [XAML controls; bind to a C++/WinRT property](./binding-property.md).

### XAML applications

This scenario is about building applications and controls on the XAML UI framework. Working in a XAML application amounts to a combination of consuming and authoring. But since XAML is the dominant UI framework on Windows today, and its influence over the Windows Runtime is proportionate to that, it deserves its own category of scenario.

Be aware that XAML works best with programming languages that offer reflection. In C++/WinRT, you sometimes have to do a little extra work in order to interoperate with the XAML framework. All of those cases are covered in the documentation. Good places to start are [XAML controls; bind to a C++/WinRT property](./binding-property.md) and [XAML custom (templated) controls with C++/WinRT](./xaml-cust-ctrl.md).

## Sample apps written in C++/WinRT

See [Where can I find C++/WinRT sample apps?](./faq.yml#where-can-i-find-c---winrt-sample-apps-).

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
