---
description: This topic uses a full Direct2D code example to show how to use C++/WinRT to consume COM classes and interfaces.
title: Consume COM components with C++/WinRT
ms.date: 04/24/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, COM, component, class, interface
ms.localizationpriority: medium
---

# Consume COM components with C++/WinRT

You can use the facilities of the [C++/WinRT](./intro-to-using-cpp-with-winrt.md) library to consume COM components, such as the high-performance 2-D and 3-D graphics of the DirectX APIs. C++/WinRT is the simplest way to use DirectX without compromising performance. This topic uses a Direct2D code example to show how to use C++/WinRT to consume COM classes and interfaces. You can, of course, mix COM and Windows Runtime programming within the same C++/WinRT project.

At the end of this topic, you'll find a full source code listing of a minimal Direct2D application. We'll lift excerpts from that code and use them to illustrate how to consume COM components using C++/WinRT using various facilities of the C++/WinRT library.

## COM smart pointers ([**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr))

When you program with COM, you work directly with interfaces rather than with objects (that's also true behind the scenes for Windows Runtime APIs, which are an evolution of COM). To call a function on a COM class, for example, you activate the class, get an interface back, and then you call functions on that interface. To access the state of an object, you don't access its data members directly; instead, you call accessor and mutator functions on an interface.

To be more specific, we're talking about interacting with interface *pointers*. And for that, we benefit from the existence of the COM smart pointer type in C++/WinRT&mdash;the [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) type.

```cppwinrt
#include <d2d1_1.h>
...
winrt::com_ptr<ID2D1Factory1> factory;
```

The code above shows how to declare an uninitialized smart pointer to a [**ID2D1Factory1**](/windows/desktop/api/d2d1_1/nn-d2d1_1-id2d1factory1) COM interface. The smart pointer is uninitialized, so it's not yet pointing to a **ID2D1Factory1** interface belonging to any actual object (it's not pointing to an interface at all). But it has the potential to do so; and (being a smart pointer) it has the ability via COM reference counting to manage the lifetime of the owning object of the interface that it points to, and to be the medium by which you call functions on that interface.

## COM functions that return an interface pointer as **void**

You can call the [**com_ptr::put_void**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput_void-function) function to write to an uninitialized smart pointer's underlying raw pointer.

```cppwinrt
D2D1_FACTORY_OPTIONS options{ D2D1_DEBUG_LEVEL_NONE };
D2D1CreateFactory(
    D2D1_FACTORY_TYPE_SINGLE_THREADED,
    __uuidof(factory),
    &options,
    factory.put_void()
);
```

The code above calls the [**D2D1CreateFactory**](/windows/desktop/api/d2d1/nf-d2d1-d2d1createfactory) function, which returns an **ID2D1Factory1** interface pointer via its last parameter, which has **void\*\*** type. Many COM functions return a **void\*\***. For such functions, use [**com_ptr::put_void**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput_void-function) as shown.

## COM functions that return a specific interface pointer

The [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) function returns an [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device) interface pointer via its third-from-last parameter, which has **ID3D11Device\*\*** type. For functions that return a specific interface pointer like that, use [**com_ptr::put**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptr_put-function).

```cppwinrt
winrt::com_ptr<ID3D11Device> device;
D3D11CreateDevice(
    ...
    device.put(),
    ...);
```

The code example in the section before this one shows how to call the raw **D2D1CreateFactory** function. But in fact, when the code example for this topic calls **D2D1CreateFactory**, it uses a helper function template that wraps the raw API, and so the code example actually uses [**com_ptr::put**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptr_put-function).

```cppwinrt
winrt::com_ptr<ID2D1Factory1> factory;
D2D1CreateFactory(
    D2D1_FACTORY_TYPE_SINGLE_THREADED,
    options,
    factory.put());
```

## COM functions that return an interface pointer as **IUnknown**

The [**DWriteCreateFactory**](/windows/desktop/api/dwrite/nf-dwrite-dwritecreatefactory) function returns a DirectWrite factory interface pointer via its last parameter, which has [**IUnknown**](/windows/desktop/api/unknwn/nn-unknwn-iunknown) type. For such a function, use [**com_ptr::put**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptr_put-function), but reinterpret cast that to **IUnknown**.

```cppwinrt
DWriteCreateFactory(
    DWRITE_FACTORY_TYPE_SHARED,
    __uuidof(dwriteFactory2),
    reinterpret_cast<IUnknown**>(dwriteFactory2.put()));
```

## Re-seat a **winrt::com_ptr**

> [!IMPORTANT]
> If you have a [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) that's already seated (its internal raw pointer already has a target) and you want to re-seat it to point to a different object, then you first need to assign `nullptr` to it&mdash;as shown in the code example below. If you don't, then an already-seated **com_ptr** will draw the issue to your attention (when you call [**com_ptr::put**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptr_put-function) or [**com_ptr::put_void**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput_void-function)) by asserting that its internal pointer is not null.

```cppwinrt
winrt::com_ptr<ID2D1SolidColorBrush> brush;
...
    brush.put()
...
brush = nullptr; // Important because we're about to re-seat
target->CreateSolidColorBrush(
    color_orange,
    D2D1::BrushProperties(0.8f),
    brush.put()));
```

## Handle HRESULT error codes

To check the value of a HRESULT returned from a COM function, and throw an exception in the event that it represents an error code, call [**winrt::check_hresult**](/uwp/cpp-ref-for-winrt/error-handling/check-hresult).

```cppwinrt
winrt::check_hresult(D2D1CreateFactory(
    D2D1_FACTORY_TYPE_SINGLE_THREADED,
    __uuidof(factory),
    options,
    factory.put_void()));
```

## COM functions that take a specific interface pointer

You can call the [**com_ptr::get**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrget-function) function to pass your **com_ptr** to a function that takes a specific interface pointer of the same type.

```cppwinrt
... ExampleFunction(
    winrt::com_ptr<ID2D1Factory1> const& factory,
    winrt::com_ptr<IDXGIDevice> const& dxdevice)
{
    ...
    winrt::check_hresult(factory->CreateDevice(dxdevice.get(), ...));
    ...
}
```

## COM functions that take an **IUnknown** interface pointer

You can use [**com_ptr::get**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrget-function) to pass your **com_ptr** to a function that takes an **IUnknown** interface pointer.

You can use the [**winrt::get_unknown**](/uwp/cpp-ref-for-winrt/get-unknown) free function to return the address of (in other words, a pointer to) the underlying raw [IUnknown interface](/windows/win32/api/unknwn/nn-unknwn-iunknown) of an object of a projected type. You can then pass that address to a function that takes an **IUnknown** interface pointer.

For info about *projected types*, see [Consume APIs with C++/WinRT](./consume-apis.md).

For a code example of **get_unknown**, see [**winrt::get_unknown**](/uwp/cpp-ref-for-winrt/get-unknown), or the [Full source code listing of a minimal Direct2D application](#full-source-code-listing-of-a-minimal-direct2d-application) in this topic.

## Passing and returning COM smart pointers

A function taking a COM smart pointer in the form of a **winrt::com_ptr** should do so by constant reference, or by reference.

```cppwinrt
... GetDxgiFactory(winrt::com_ptr<ID3D11Device> const& device) ...

... CreateDevice(..., winrt::com_ptr<ID3D11Device>& device) ...
```

A function that returns a **winrt::com_ptr** should do so by value.

```cppwinrt
winrt::com_ptr<ID2D1Factory1> CreateFactory() ...
```

## Query a COM smart pointer for a different interface

You can use the [**com_ptr::as**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptras-function) function to query a COM smart pointer for a different interface. The function throws an exception if the query doesn't succeed.

```cppwinrt
void ExampleFunction(winrt::com_ptr<ID3D11Device> const& device)
{
    ...
    winrt::com_ptr<IDXGIDevice> const dxdevice{ device.as<IDXGIDevice>() };
    ...
}
```

Alternatively, use [**com_ptr::try_as**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrtry_as-function), which returns a value that you can check against `nullptr` to see whether the query succeeded.

## Full source code listing of a minimal Direct2D application

> [!NOTE]
> For info about setting up Visual Studio for C++/WinRT development&mdash;including installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support)&mdash;see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

If you want to build and run this source code example then first install (or update to) the latest version of the C++/WinRT Visual Studio Extension (VSIX); see the note above. Then, in Visual Studio, create a new **Core App (C++/WinRT)**. `Direct2D` is a reasonable name for the project, but you can name it anything you like. Target the latest generally-available (that is, not preview) version of the Windows SDK.

### Step 1. Edit `pch.h`

Open `pch.h`, and add `#include <unknwn.h>` immediately after including `windows.h`. This is because we're using [**winrt::get_unknown**](/uwp/cpp-ref-for-winrt/get-unknown). It's a good idea to `#include <unknwn.h>` explicitly whenever you use **winrt::get_unknown**, even if that header has been included by another header.

> [!NOTE]
> If you omit this step then you'll see the build error *'get_unknown': identifier not found*.

### Step 2. Edit `App.cpp`

Open `App.cpp`, delete its entire contents, and paste in the listing below.

The code below uses the [winrt::com_ptr::capture function](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrcapture-function) where possible. `WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
#include "pch.h"
#include <d2d1_1.h>
#include <d3d11.h>
#include <dxgi1_2.h>
#include <winrt/Windows.Graphics.Display.h>

using namespace winrt;

using namespace Windows;
using namespace Windows::ApplicationModel::Core;
using namespace Windows::UI;
using namespace Windows::UI::Core;
using namespace Windows::Graphics::Display;

namespace
{
    winrt::com_ptr<ID2D1Factory1> CreateFactory()
    {
        D2D1_FACTORY_OPTIONS options{};

#ifdef _DEBUG
        options.debugLevel = D2D1_DEBUG_LEVEL_INFORMATION;
#endif

        winrt::com_ptr<ID2D1Factory1> factory;

        winrt::check_hresult(D2D1CreateFactory(
            D2D1_FACTORY_TYPE_SINGLE_THREADED,
            options,
            factory.put()));

        return factory;
    }

    HRESULT CreateDevice(D3D_DRIVER_TYPE const type, winrt::com_ptr<ID3D11Device>& device)
    {
        WINRT_ASSERT(!device);

        return D3D11CreateDevice(
            nullptr,
            type,
            nullptr,
            D3D11_CREATE_DEVICE_BGRA_SUPPORT,
            nullptr, 0,
            D3D11_SDK_VERSION,
            device.put(),
            nullptr,
            nullptr);
    }

    winrt::com_ptr<ID3D11Device> CreateDevice()
    {
        winrt::com_ptr<ID3D11Device> device;
        HRESULT hr{ CreateDevice(D3D_DRIVER_TYPE_HARDWARE, device) };

        if (DXGI_ERROR_UNSUPPORTED == hr)
        {
            hr = CreateDevice(D3D_DRIVER_TYPE_WARP, device);
        }

        winrt::check_hresult(hr);
        return device;
    }

    winrt::com_ptr<ID2D1DeviceContext> CreateRenderTarget(
        winrt::com_ptr<ID2D1Factory1> const& factory,
        winrt::com_ptr<ID3D11Device> const& device)
    {
        WINRT_ASSERT(factory);
        WINRT_ASSERT(device);

        winrt::com_ptr<IDXGIDevice> const dxdevice{ device.as<IDXGIDevice>() };

        winrt::com_ptr<ID2D1Device> d2device;
        winrt::check_hresult(factory->CreateDevice(dxdevice.get(), d2device.put()));

        winrt::com_ptr<ID2D1DeviceContext> target;
        winrt::check_hresult(d2device->CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS_NONE, target.put()));
        return target;
    }

    winrt::com_ptr<IDXGIFactory2> GetDxgiFactory(winrt::com_ptr<ID3D11Device> const& device)
    {
        WINRT_ASSERT(device);

        winrt::com_ptr<IDXGIDevice> const dxdevice{ device.as<IDXGIDevice>() };

        winrt::com_ptr<IDXGIAdapter> adapter;
        winrt::check_hresult(dxdevice->GetAdapter(adapter.put()));

        winrt::com_ptr<IDXGIFactory2> factory;
        factory.capture(adapter, &IDXGIAdapter::GetParent);
        return factory;
    }

    void CreateDeviceSwapChainBitmap(
        winrt::com_ptr<IDXGISwapChain1> const& swapchain,
        winrt::com_ptr<ID2D1DeviceContext> const& target)
    {
        WINRT_ASSERT(swapchain);
        WINRT_ASSERT(target);

        winrt::com_ptr<IDXGISurface> surface;
        surface.capture(swapchain, &IDXGISwapChain1::GetBuffer, 0);

        D2D1_BITMAP_PROPERTIES1 const props{ D2D1::BitmapProperties1(
            D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW,
            D2D1::PixelFormat(DXGI_FORMAT_B8G8R8A8_UNORM, D2D1_ALPHA_MODE_IGNORE)) };

        winrt::com_ptr<ID2D1Bitmap1> bitmap;

        winrt::check_hresult(target->CreateBitmapFromDxgiSurface(surface.get(),
            props,
            bitmap.put()));

        target->SetTarget(bitmap.get());
    }

    winrt::com_ptr<IDXGISwapChain1> CreateSwapChainForCoreWindow(winrt::com_ptr<ID3D11Device> const& device)
    {
        WINRT_ASSERT(device);

        winrt::com_ptr<IDXGIFactory2> const factory{ GetDxgiFactory(device) };

        DXGI_SWAP_CHAIN_DESC1 props{};
        props.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
        props.SampleDesc.Count = 1;
        props.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
        props.BufferCount = 2;
        props.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL;

        winrt::com_ptr<IDXGISwapChain1> swapChain;

        winrt::check_hresult(factory->CreateSwapChainForCoreWindow(
            device.get(),
            winrt::get_unknown(CoreWindow::GetForCurrentThread()),
            &props,
            nullptr, // all or nothing
            swapChain.put()));

        return swapChain;
    }

    constexpr D2D1_COLOR_F color_white{ 1.0f,  1.0f,  1.0f,  1.0f };
    constexpr D2D1_COLOR_F color_orange{ 0.92f,  0.38f,  0.208f,  1.0f };
}

struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    winrt::com_ptr<ID2D1Factory1> m_factory;
    winrt::com_ptr<ID2D1DeviceContext> m_target;
    winrt::com_ptr<IDXGISwapChain1> m_swapChain;
    winrt::com_ptr<ID2D1SolidColorBrush> m_brush;
    float m_dpi{};

    IFrameworkView CreateView()
    {
        return *this;
    }

    void Initialize(CoreApplicationView const&)
    {
    }

    void Load(hstring const&)
    {
        CoreWindow const window{ CoreWindow::GetForCurrentThread() };

        window.SizeChanged([&](auto&&...)
        {
            if (m_target)
            {
                ResizeSwapChainBitmap();
                Render();
            }
        });

        DisplayInformation const display{ DisplayInformation::GetForCurrentView() };
        m_dpi = display.LogicalDpi();

        display.DpiChanged([&](DisplayInformation const& display, IInspectable const&)
        {
            if (m_target)
            {
                m_dpi = display.LogicalDpi();
                m_target->SetDpi(m_dpi, m_dpi);
                CreateDeviceSizeResources();
                Render();
            }
        });

        m_factory = CreateFactory();
        CreateDeviceIndependentResources();
    }

    void Uninitialize()
    {
    }

    void Run()
    {
        CoreWindow const window{ CoreWindow::GetForCurrentThread() };
        window.Activate();

        Render();
        CoreDispatcher const dispatcher{ window.Dispatcher() };
        dispatcher.ProcessEvents(CoreProcessEventsOption::ProcessUntilQuit);
    }

    void SetWindow(CoreWindow const&) {}

    void Draw()
    {
        m_target->Clear(color_white);

        D2D1_SIZE_F const size{ m_target->GetSize() };
        D2D1_RECT_F const rect{ 100.0f, 100.0f, size.width - 100.0f, size.height - 100.0f };
        m_target->DrawRectangle(rect, m_brush.get(), 100.0f);

        char buffer[1024];
        (void)snprintf(buffer, sizeof(buffer), "Draw %.2f x %.2f @ %.2f\n", size.width, size.height, m_dpi);
        ::OutputDebugStringA(buffer);
    }

    void Render()
    {
        if (!m_target)
        {
            winrt::com_ptr<ID3D11Device> const device{ CreateDevice() };
            m_target = CreateRenderTarget(m_factory, device);
            m_swapChain = CreateSwapChainForCoreWindow(device);

            CreateDeviceSwapChainBitmap(m_swapChain, m_target);

            m_target->SetDpi(m_dpi, m_dpi);

            CreateDeviceResources();
            CreateDeviceSizeResources();
        }

        m_target->BeginDraw();
        Draw();
        m_target->EndDraw();

        HRESULT const hr{ m_swapChain->Present(1, 0) };

        if (S_OK != hr && DXGI_STATUS_OCCLUDED != hr)
        {
            ReleaseDevice();
        }
    }

    void ReleaseDevice()
    {
        m_target = nullptr;
        m_swapChain = nullptr;

        ReleaseDeviceResources();
    }

    void ResizeSwapChainBitmap()
    {
        WINRT_ASSERT(m_target);
        WINRT_ASSERT(m_swapChain);

        m_target->SetTarget(nullptr);

        if (S_OK == m_swapChain->ResizeBuffers(0, // all buffers
            0, 0, // client area
            DXGI_FORMAT_UNKNOWN, // preserve format
            0)) // flags
        {
            CreateDeviceSwapChainBitmap(m_swapChain, m_target);
            CreateDeviceSizeResources();
        }
        else
        {
            ReleaseDevice();
        }
    }

    void CreateDeviceIndependentResources()
    {
    }

    void CreateDeviceResources()
    {
        winrt::check_hresult(m_target->CreateSolidColorBrush(
            color_orange,
            D2D1::BrushProperties(0.8f),
            m_brush.put()));
    }

    void CreateDeviceSizeResources()
    {
    }

    void ReleaseDeviceResources()
    {
        m_brush = nullptr;
    }
};

int __stdcall wWinMain(HINSTANCE, HINSTANCE, PWSTR, int)
{
    CoreApplication::Run(winrt::make<App>());
}
```

## Working with COM types, such as BSTR and VARIANT

As you can see, C++/WinRT provides support for both implementing and calling COM interfaces. For using COM types, such as BSTR and VARIANT, we recommend that you use wrappers provided by the [Windows Implementation Libraries (WIL)](https://github.com/Microsoft/wil), such as **wil::unique_bstr** and **wil::unique_variant** (which manage resources lifetimes).

[WIL](https://github.com/Microsoft/wil) supersedes frameworks such as the Active Template Library (ATL), and the Visual C++ compiler's COM Support. And we recommend it over writing your own wrappers, or using COM types such as BSTR and VARIANT in their raw form (together with the appropriate APIs).

## Avoiding namespace collisions

It's common practice in C++/WinRT&mdash;as the code listing in this topic demonstrates&mdash;to use using-directives liberally. In some cases, though, that can lead to the problem of importing colliding names into the global namespace. Here's an example.

C++/WinRT contains a type named [**winrt::Windows::Foundation::IUnknown**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown); while COM defines a type named [**::IUnknown**](/windows/desktop/api/unknwn/nn-unknwn-iunknown). So consider the following code, in a C++/WinRT project that consumes COM headers.

```cppwinrt
using namespace winrt::Windows::Foundation;
...
void MyFunction(IUnknown*); // error C2872:  'IUnknown': ambiguous symbol
```

The unqualified name *IUnknown* collides in the global namespace, hence the *ambiguous symbol* compiler error. Instead, you can isolate the C++/WinRT version of the name into the **winrt** namespace, like this.

```cppwinrt
namespace winrt
{
    using namespace Windows::Foundation;
}
...
void MyFunctionA(IUnknown*); // Ok.
void MyFunctionB(winrt::IUnknown const&); // Ok.
```

Or, if you want the convenience of `using namespace winrt`, then you can. You just need to qualify the global version of *IUnknown*, like this.

```cppwinrt
using namespace winrt;
namespace winrt
{
    using namespace Windows::Foundation;
}
...
void MyFunctionA(::IUnknown*); // Ok.
void MyFunctionB(winrt::IUnknown const&); // Ok.
```

Naturally, this works with any C++/WinRT namespace.

```cppwinrt
namespace winrt
{
    using namespace Windows::Storage;
    using namespace Windows::System;
}
```

You can then refer to **winrt::Windows::Storage::StorageFile**, for example, as just **winrt::StorageFile**.

## Important APIs
* [winrt::check_hresult function](/uwp/cpp-ref-for-winrt/error-handling/check-hresult)
* [winrt::com_ptr struct template](/uwp/cpp-ref-for-winrt/com-ptr)
* [winrt::Windows::Foundation::IUnknown struct](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown)