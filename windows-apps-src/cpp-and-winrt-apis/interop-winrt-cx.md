---
description: This topic shows two helper functions that can be used to convert between [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and [C++/WinRT](./intro-to-using-cpp-with-winrt.md) objects.
title: Interop between C++/WinRT and C++/CX
ms.date: 10/09/2018
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, interop, C++/CX
ms.localizationpriority: medium
---

# Interop between C++/WinRT and C++/CX

Before reading this topic, you'll need the info in the topic [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md). That topic introduces two main strategy options for porting your [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) project to [C++/WinRT](./intro-to-using-cpp-with-winrt.md).

- Port the entire project in one pass. The simplest option for a project that's not too large. If you have a Windows Runtime component project, then this strategy is your only option.
- Port the project gradually (the size or complexity of your codebase might make this necessary). But this strategy calls for you to follow a porting process in which for a time C++/CX and C++/WinRT code exists side by side in the same project. For a XAML project, at any given time, your XAML page types must be *either* all C++/WinRT *or* all C++/CX.

This interop topic is relevant for that *second* strategy&mdash;for cases when you need to port your project gradually. This topic shows you two helper functions that you can use to convert a C++/CX object into a C++/WinRT object (and vice versa) within the same project.

These helper functions will be very useful as you port your code gradually from C++/CX to C++/WinRT. Or you might just choose to use both the C++/WinRT and C++/CX language projections in the same project, whether you're porting or not, and use these helper functions to interoperate between the two.

After reading this topic, for info and code examples showing how to support PPL tasks and coroutines side by side in the same project (for example, calling coroutines from task chains), see the more advanced topic [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md).

## The **from_cx** and **to_cx** functions

Here's a source code listing of a header file named `interop_helpers.h`, containing two conversion helper functions. As you gradually port your project, there will be parts still in C++/CX, and parts that you've ported to C++/WinRT. You can use these helper functions to convert objects to and from C++/CX and C++/WinRT in your project at the boundary points between those two parts.

The sections that follow the code listing explain the two functions, and how to create and use the header file in your project.

```cppwinrt
// interop_helpers.h
#pragma once

template <typename T>
T from_cx(Platform::Object^ from)
{
    T to{ nullptr };

    winrt::check_hresult(reinterpret_cast<::IUnknown*>(from)
        ->QueryInterface(winrt::guid_of<T>(), winrt::put_abi(to)));

    return to;
}

template <typename T>
T^ to_cx(winrt::Windows::Foundation::IUnknown const& from)
{
    return safe_cast<T^>(reinterpret_cast<Platform::Object^>(winrt::get_abi(from)));
}
```

### The **from_cx** function

The **from_cx** helper function converts a C++/CX object to an equivalent C++/WinRT object. The function casts a C++/CX object to its underlying [**IUnknown**](/windows/win32/api/unknwn/nn-unknwn-iunknown) interface pointer. It then calls [**QueryInterface**](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(q_)) on that pointer to query for the default interface of the C++/WinRT object. **QueryInterface** is the Windows Runtime application binary interface (ABI) equivalent of the C++/CX `safe_cast` extension. And, the [**winrt::put_abi**](/uwp/cpp-ref-for-winrt/put-abi) function retrieves the address of a C++/WinRT object's underlying **IUnknown** interface pointer so that it can be set to another value.

### The **to_cx** function

The **to_cx** helper function converts a C++/WinRT object to an equivalent C++/CX object. The [**winrt::get_abi**](/uwp/cpp-ref-for-winrt/get-abi) function retrieves a pointer to a C++/WinRT object's underlying **IUnknown** interface. The function casts that pointer to a C++/CX object before using the C++/CX `safe_cast` extension to query for the requested C++/CX type.

### The `interop_helpers.h` header file

To use the two helper functions in your project, follow these steps.

- Add a new **Header File (.h)** item to your project and name it `interop_helpers.h`.
- Replace the contents of `interop_helpers.h` with the code listing above.
- Add these includes to `pch.h`.

```cppwinrt
// pch.h
...
#include <unknwn.h>
// Include C++/WinRT projected Windows API headers here.
...
#include <interop_helpers.h>
```

## Taking a C++/CX project and adding C++/WinRT support

This section describes what to do if you've decided to take your existing C++/CX project, add C++/WinRT support to it, and do your porting work there. Also see [Visual Studio support for C++/WinRT](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

To mix C++/CX and C++/WinRT in a C++/CX project&mdash;including using the **from_cx** and **to_cx** helper functions in the project&mdash;you'll need to manually add C++/WinRT support to the project.

First, open your C++/CX project in Visual Studio and confirm that project property **General** \> **Target Platform Version** is set to 10.0.17134.0 (Windows 10, version 1803) or greater.

### Install the C++/WinRT NuGet package

The [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) provides C++/WinRT build support (MSBuild properties and targets). To install it, click the menu item **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project.

> [!IMPORTANT]
> Installing the C++/WinRT NuGet package causes support for C++/CX to be turned off in the project. If you're going to port in one pass, then it's a good idea to leave that support turned off so that build messages will help you find (and port) all of your dependencies on C++/CX (eventually turning what was a pure C++/CX project into a pure C++/WinRT project). But see the next section for info about turning it back on.

### Turn C++/CX support back on

If you're porting in one pass, then you don't need to do this. But if you need to port gradually, then at this point you'll need to turn C++/CX support back on in your project. In project properties, **C/C++** \> **General** \> **Consume Windows Runtime Extension** \> **Yes (/ZW)**).

Alternatively (or, for a XAML project, in addition), you can add C++/CX support by using the C++/WinRT project property page in Visual Studio. In project properties, **Common Properties** \> **C++/WinRT** \> **Project Language** \> **C++/CX**. Doing that will add the following property to your `.vcxproj` file.

```xml
  <PropertyGroup Label="Globals">
    <CppWinRTProjectLanguage>C++/CX</CppWinRTProjectLanguage>
  </PropertyGroup>
```

> [!IMPORTANT]
> Whenever you need to build to process the contents of a **Midl File (.idl)** into stub files, you'll need to change **Project Language** back to **C++/WinRT**. After the build has generated those stubs, change **Project Language** back to **C++/CX**.

For a list of similar customization options (which fine-tune the behavior of the `cppwinrt.exe` tool), see the Microsoft.Windows.CppWinRT NuGet package [readme](https://github.com/microsoft/cppwinrt/blob/master/nuget/readme.md#customizing).

### Include C++/WinRT header files

The least you should do is, in your precompiled header file (usually `pch.h`), include `winrt/base.h` as shown below.

```cppwinrt
// pch.h
...
#include <winrt/base.h>
...
```

But you'll almost certainly need the types in the **winrt::Windows::Foundation** namespace. And you might already know of other namespaces that you'll need. So include the C++/WinRT projected Windows API headers that correspond to those namespaces like this (you don't need to explicitly include `winrt/base.h` now because it will be included automatically for you).

```cppwinrt
// pch.h
...
#include <winrt/Windows.Foundation.h>
// Include any other C++/WinRT projected Windows API headers here.
...
```

Also see the code example in the following section (*Taking a C++/WinRT project and adding C++/CX support*) for a technique using the namespace aliases `namespace cx` and `namespace winrt`. That technique lets you deal with otherwise potential namespace collisions between the C++/WinRT projection and the C++/CX projection.

### Add `interop_helpers.h` to the project

You'll now be able to add the **from_cx** and **to_cx** functions to your C++/CX project. For instructions on doing that, see the [**from_cx** and **to_cx** functions](#the-from_cx-and-to_cx-functions) section above.

## Taking a C++/WinRT project and adding C++/CX support

This section describes what to do if you've decided to create a new C++/WinRT project, and do your porting work there.

To mix C++/WinRT and C++/CX in a C++/WinRT project&mdash;including using the **from_cx** and **to_cx** helper functions in the project&mdash;you'll need to manually add C++/CX support to the project.

- Create a new C++/WinRT project in Visual Studio using one of the C++/WinRT project templates (see [Visual Studio support for C++/WinRT](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)).
- Turn on project support for C++/CX. In project properties, **C/C++** \> **General** \> **Consume Windows Runtime Extension** \> **Yes (/ZW)**.

### An example C++/WinRT project showing the two helper functions in use

In this section, you can create an example C++/WinRT project that demonstrates how to use **from_cx** and **to_cx**. It also illustrates how you can use namespace aliases for the different islands of code, in order to deal with otherwise potential namespace collisions between the C++/WinRT projection and the C++/CX projection.

- Create a **Visual C++** \> **Windows Universal** \> **Core App (C++/WinRT)** project.
- In project properties, **C/C++** \> **General** \> **Consume Windows Runtime Extension** \> **Yes (/ZW)**.
- Add `interop_helpers.h` to the project. For instructions on doing that, see the [**from_cx** and **to_cx** functions](#the-from_cx-and-to_cx-functions) section above.
- Replace the contents of `App.cpp` with the code listing below, build, and run.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
// App.cpp
#include "pch.h"
#include <sstream>

namespace cx
{
    using namespace Windows::Foundation;
}

namespace winrt
{
    using namespace Windows;
    using namespace Windows::ApplicationModel::Core;
    using namespace Windows::Foundation;
    using namespace Windows::Foundation::Numerics;
    using namespace Windows::UI;
    using namespace Windows::UI::Core;
    using namespace Windows::UI::Composition;
}

struct App : winrt::implements<App, winrt::IFrameworkViewSource, winrt::IFrameworkView>
{
    winrt::CompositionTarget m_target{ nullptr };
    winrt::VisualCollection m_visuals{ nullptr };
    winrt::Visual m_selected{ nullptr };
    winrt::float2 m_offset{};

    winrt::IFrameworkView CreateView()
    {
        return *this;
    }

    void Initialize(winrt::CoreApplicationView const &)
    {
    }

    void Load(winrt::hstring const&)
    {
    }

    void Uninitialize()
    {
    }

    void Run()
    {
        winrt::CoreWindow window = winrt::CoreWindow::GetForCurrentThread();
        window.Activate();

        winrt::CoreDispatcher dispatcher = window.Dispatcher();
        dispatcher.ProcessEvents(winrt::CoreProcessEventsOption::ProcessUntilQuit);
    }

    void SetWindow(winrt::CoreWindow const & window)
    {
        winrt::Compositor compositor;
        winrt::ContainerVisual root = compositor.CreateContainerVisual();
        m_target = compositor.CreateTargetForCurrentView();
        m_target.Root(root);
        m_visuals = root.Children();

        window.PointerPressed({ this, &App::OnPointerPressed });
        window.PointerMoved({ this, &App::OnPointerMoved });

        window.PointerReleased([&](auto && ...)
        {
            m_selected = nullptr;
        });
    }

    void OnPointerPressed(IInspectable const &, winrt::PointerEventArgs const & args)
    {
        winrt::float2 const point = args.CurrentPoint().Position();

        for (winrt::Visual visual : m_visuals)
        {
            winrt::float3 const offset = visual.Offset();
            winrt::float2 const size = visual.Size();

            if (point.x >= offset.x &&
                point.x < offset.x + size.x &&
                point.y >= offset.y &&
                point.y < offset.y + size.y)
            {
                m_selected = visual;
                m_offset.x = offset.x - point.x;
                m_offset.y = offset.y - point.y;
            }
        }

        if (m_selected)
        {
            m_visuals.Remove(m_selected);
            m_visuals.InsertAtTop(m_selected);
        }
        else
        {
            AddVisual(point);
        }
    }

    void OnPointerMoved(IInspectable const &, winrt::PointerEventArgs const & args)
    {
        if (m_selected)
        {
            winrt::float2 const point = args.CurrentPoint().Position();

            m_selected.Offset(
            {
                point.x + m_offset.x,
                point.y + m_offset.y,
                0.0f
            });
        }
    }

    void AddVisual(winrt::float2 const point)
    {
        winrt::Compositor compositor = m_visuals.Compositor();
        winrt::SpriteVisual visual = compositor.CreateSpriteVisual();

        static winrt::Color colors[] =
        {
            { 0xDC, 0x5B, 0x9B, 0xD5 },
            { 0xDC, 0xED, 0x7D, 0x31 },
            { 0xDC, 0x70, 0xAD, 0x47 },
            { 0xDC, 0xFF, 0xC0, 0x00 }
        };

        static unsigned last = 0;
        unsigned const next = ++last % _countof(colors);
        visual.Brush(compositor.CreateColorBrush(colors[next]));

        float const BlockSize = 100.0f;

        visual.Size(
        {
            BlockSize,
            BlockSize
        });

        visual.Offset(
        {
            point.x - BlockSize / 2.0f,
            point.y - BlockSize / 2.0f,
            0.0f,
        });

        m_visuals.InsertAtTop(visual);

        m_selected = visual;
        m_offset.x = -BlockSize / 2.0f;
        m_offset.y = -BlockSize / 2.0f;
    }
};

int __stdcall wWinMain(HINSTANCE, HINSTANCE, PWSTR, int)
{
    winrt::init_apartment();

    winrt::Uri uri(L"http://aka.ms/cppwinrt");
    std::wstringstream wstringstream;
    wstringstream << L"C++/WinRT: " << uri.Domain().c_str() << std::endl;

    // Convert from a C++/WinRT type to a C++/CX type.
    cx::Uri^ cx = to_cx<cx::Uri>(uri);
    wstringstream << L"C++/CX: " << cx->Domain->Data() << std::endl;
    ::OutputDebugString(wstringstream.str().c_str());

    // Convert from a C++/CX type to a C++/WinRT type.
    winrt::Uri uri_from_cx = from_cx<winrt::Uri>(cx);
    WINRT_ASSERT(uri.Domain() == uri_from_cx.Domain());
    WINRT_ASSERT(uri == uri_from_cx);

    winrt::CoreApplication::Run(winrt::make<App>());
}
```

## Important APIs
* [IUnknown interface](/windows/win32/api/unknwn/nn-unknwn-iunknown)
* [QueryInterface function](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(q_))
* [winrt::get_abi function](/uwp/cpp-ref-for-winrt/get-abi)
* [winrt::put_abi function](/uwp/cpp-ref-for-winrt/put-abi)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md)
* [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md)