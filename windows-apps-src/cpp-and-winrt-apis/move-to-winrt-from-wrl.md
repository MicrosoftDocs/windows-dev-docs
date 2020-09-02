---
description: This topic shows how to port WRL code to its equivalent in C++/WinRT.
title: Move to C++/WinRT from WRL
ms.date: 05/30/2018
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, WRL
ms.localizationpriority: medium
---

# Move to C++/WinRT from WRL
This topic shows how to port [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) code to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md).

The first step in porting to C++/WinRT is to manually add C++/WinRT support to your project (see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)). To do that, install the [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) into your project. Open the project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project. One effect of that change is that support for [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) is turned off in the project. If you're using C++/CX in the project, then you can leave support turned off and update your C++/CX code to C++/WinRT as well (see [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md)). Or you can turn support back on (in project properties, **C/C++** \> **General** \> **Consume Windows Runtime Extension** \> **Yes (/ZW)**), and first focus on porting your WRL code. C++/CX and C++/WinRT code can coexist in the same project, with the exception of XAML compiler support, and Windows Runtime components (see [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md)).

Set project property **General** \> **Target Platform Version** to 10.0.17134.0 (Windows 10, version 1803) or greater.

In your precompiled header file (usually `pch.h`), include `winrt/base.h`.

```cppwinrt
#include <winrt/base.h>
```

If you include any C++/WinRT projected Windows API headers (for example, `winrt/Windows.Foundation.h`), then you don't need to explicitly include `winrt/base.h` like this because it will be included automatically for you.

## Porting WRL COM smart pointers ([Microsoft::WRL::ComPtr](/cpp/windows/comptr-class))
Port any code that uses **Microsoft::WRL::ComPtr\<T\>** to use [**winrt::com_ptr\<T\>**](/uwp/cpp-ref-for-winrt/com-ptr). Here's a before-and-after code example. In the *after* version, the [**com_ptr::put**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput-function) member function retrieves the underlying raw pointer so that it can be set.

```cpp
ComPtr<IDXGIAdapter1> previousDefaultAdapter;
DX::ThrowIfFailed(m_dxgiFactory->EnumAdapters1(0, &previousDefaultAdapter));
```

```cppwinrt
winrt::com_ptr<IDXGIAdapter1> previousDefaultAdapter;
winrt::check_hresult(m_dxgiFactory->EnumAdapters1(0, previousDefaultAdapter.put()));
```

> [!IMPORTANT]
> If you have a [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) that's already seated (its internal raw pointer already has a target) and you want to re-seat it to point to a different object, then you first need to assign `nullptr` to it&mdash;as shown in the code example below. If you don't, then an already-seated **com_ptr** will draw the issue to your attention (when you call [**com_ptr::put**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput-function) or [**com_ptr::put_void**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput_void-function)) by asserting that its internal pointer is not null.

```cppwinrt
winrt::com_ptr<IDXGISwapChain1> m_pDXGISwapChain1;
...
// We execute the code below each time the window size changes.
m_pDXGISwapChain1 = nullptr; // Important because we're about to re-seat 
winrt::check_hresult(
    m_pDxgiFactory->CreateSwapChainForHwnd(
        m_pCommandQueue.get(), // For Direct3D 12, this is a pointer to a direct command queue, and not to the device.
        m_hWnd,
        &swapChainDesc,
        nullptr,
        nullptr,
        m_pDXGISwapChain1.put())
);
```

In this next example (in the *after* version), the [**com_ptr::put_void**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrput_void-function) member function retrieves the underlying raw pointer as a pointer to a pointer to void.

```cpp
ComPtr<ID3D12Debug> debugController;
if (SUCCEEDED(D3D12GetDebugInterface(IID_PPV_ARGS(&debugController))))
{
    debugController->EnableDebugLayer();
}
```

```cppwinrt
winrt::com_ptr<ID3D12Debug> debugController;
if (SUCCEEDED(D3D12GetDebugInterface(__uuidof(debugController), debugController.put_void())))
{
    debugController->EnableDebugLayer();
}
```

Replace **ComPtr::Get** with [**com_ptr::get**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptrget-function).

```cpp
m_d3dDevice->CreateDepthStencilView(m_depthStencil.Get(), &dsvDesc, m_dsvHeap->GetCPUDescriptorHandleForHeapStart());
```

```cppwinrt
m_d3dDevice->CreateDepthStencilView(m_depthStencil.get(), &dsvDesc, m_dsvHeap->GetCPUDescriptorHandleForHeapStart());
```

When you want to pass the underlying raw pointer to a function that expects a pointer to **IUnknown**, use the [**winrt::get_unknown**](/uwp/cpp-ref-for-winrt/get-unknown) free function, as shown in this next example.

```cpp
ComPtr<IDXGISwapChain1> swapChain;
DX::ThrowIfFailed(
    m_dxgiFactory->CreateSwapChainForCoreWindow(
        m_commandQueue.Get(),
        reinterpret_cast<IUnknown*>(m_window.Get()),
        &swapChainDesc,
        nullptr,
        &swapChain
    )
);
```

```cppwinrt
winrt::agile_ref<winrt::Windows::UI::Core::CoreWindow> m_window; 
winrt::com_ptr<IDXGISwapChain1> swapChain;
winrt::check_hresult(
    m_dxgiFactory->CreateSwapChainForCoreWindow(
        m_commandQueue.get(),
        winrt::get_unknown(m_window.get()),
        &swapChainDesc,
        nullptr,
        swapChain.put()
    )
);
```

## Porting a WRL module (Microsoft::WRL::Module)
You can gradually add C++/WinRT code to an existing project that uses WRL to implement a component, and your existing WRL classes will continue to be supported. This section shows how.

If you create a new **Windows Runtime Component (C++/WinRT)** project type in Visual Studio, and build, then the file `Generated Files\module.g.cpp` is generated for you. That file contains the definitions of two useful C++/WinRT functions (listed out below), which you can copy and add to your project. Those function are **WINRT_CanUnloadNow** and **WINRT_GetActivationFactory** and, as you can see, they conditionally call WRL in order to support you whatever stage of porting you're at.

```cppwinrt
HRESULT WINRT_CALL WINRT_CanUnloadNow()
{
#ifdef _WRL_MODULE_H_
    if (!::Microsoft::WRL::Module<::Microsoft::WRL::InProc>::GetModule().Terminate())
    {
        return S_FALSE;
    }
#endif

    if (winrt::get_module_lock())
    {
        return S_FALSE;
    }

    winrt::clear_factory_cache();
    return S_OK;
}

HRESULT WINRT_CALL WINRT_GetActivationFactory(HSTRING classId, void** factory)
{
    try
    {
        *factory = nullptr;
        wchar_t const* const name = WINRT_WindowsGetStringRawBuffer(classId, nullptr);

        if (0 == wcscmp(name, L"MoveFromWRLTest.Class"))
        {
            *factory = winrt::detach_abi(winrt::make<winrt::MoveFromWRLTest::factory_implementation::Class>());
            return S_OK;
        }

#ifdef _WRL_MODULE_H_
        return ::Microsoft::WRL::Module<::Microsoft::WRL::InProc>::GetModule().GetActivationFactory(classId, reinterpret_cast<::IActivationFactory**>(factory));
#else
        return winrt::hresult_class_not_available().to_abi();
#endif
    }
    catch (...) { return winrt::to_hresult(); }
}
```

Once you have these functions in your project, instead of calling [**Module::GetActivationFactory**](/cpp/windows/module-getactivationfactory-method) directly, call **WINRT_GetActivationFactory** (which calls the WRL function internally). Here's a before-and-after code example.

```cpp
HRESULT WINAPI DllGetActivationFactory(_In_ HSTRING activatableClassId, _Out_ ::IActivationFactory **factory)
{
    auto & module = Microsoft::WRL::Module<Microsoft::WRL::InProc>::GetModule();
    return module.GetActivationFactory(activatableClassId, factory);
}
```

```cppwinrt
HRESULT __stdcall WINRT_GetActivationFactory(HSTRING activatableClassId, void** factory);
HRESULT WINAPI DllGetActivationFactory(_In_ HSTRING activatableClassId, _Out_ ::IActivationFactory **factory)
{
    return WINRT_GetActivationFactory(activatableClassId, reinterpret_cast<void**>(factory));
}
```

Instead of calling [**Module::Terminate**](/cpp/windows/module-terminate-method) directly, call **WINRT_CanUnloadNow** (which calls the WRL function internally). Here's a before-and-after code example.

```cpp
HRESULT __stdcall DllCanUnloadNow(void)
{
    auto &module = Microsoft::WRL::Module<Microsoft::WRL::InProc>::GetModule();
    HRESULT hr = (module.Terminate() ? S_OK : S_FALSE);
    if (hr == S_OK)
    {
        hr = ...
    }
    return hr;
}
```

```cppwinrt
HRESULT __stdcall WINRT_CanUnloadNow();
HRESULT __stdcall DllCanUnloadNow(void)
{
    HRESULT hr = WINRT_CanUnloadNow();
    if (hr == S_OK)
    {
        hr = ...
    }
    return hr;
}
```

## Important APIs
* [winrt::com_ptr struct template](/uwp/cpp-ref-for-winrt/com-ptr)
* [winrt::Windows::Foundation::IUnknown struct](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown)

## Related topics
* [Introduction to C++/WinRT](intro-to-using-cpp-with-winrt.md)
* [Move to C++/WinRT from C++/CX](move-to-winrt-from-cx.md)
* [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl)