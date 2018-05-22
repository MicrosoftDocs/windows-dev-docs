---
author: stevewhims
description: This topic shows how to convert between application binary interface (ABI) and C++/WinRT objects.
title: Interop between C++/WinRT and the ABI
ms.author: stwhi
ms.date: 05/21/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, interop, ABI
ms.localizationpriority: medium
---

# Interop between [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) and the ABI
This topic shows how to convert between SDK application binary interface (ABI) and C++/WinRT objects. You can use these techniques to interop between code that uses these two ways of programming with the Windows Runtime, or you can use them as you gradually move your code from the ABI to C++/WinRT.

## What is the Windows Runtime ABI, and what are ABI types?
A Windows Runtime class (runtime class) is really an abstraction. This abstraction defines a binary interface (the Application Binary Interface, or ABI) that allows various programming languages to interact with an object. Regardless of programming language, client code interaction with a Windows Runtime object happens at the lowest level, with client language constructs translated into calls into the object's ABI.

The Windows SDK headers in the folder "%WindowsSdkDir%Include\10.0.17134.0\winrt" (adjust the SDK version number for your case, if necessary), are the Windows Runtime ABI header files. They were produced by the MIDL compiler. Here's an example of including one of these headers.

```
#include <windows.foundation.h>
```

And here's a simplified example of one of the ABI types that you'll find in that particular SDK header. Note the **ABI** namespace; **Windows::Foundation**, and all other Windows namespaces, are declared by the SDK headers within the **ABI** namespace.

```
namespace ABI::Windows::Foundation
{
    IUriRuntimeClass : public IInspectable
    {
    public:
        /* [propget] */ virtual HRESULT STDMETHODCALLTYPE get_AbsoluteUri(/* [retval, out] */__RPC__deref_out_opt HSTRING * value) = 0;
        ...
    }
}
```

**IUriRuntimeClass** is a COM interface. But more than that&mdash;since its base is **IInspectable**&mdash;**IUriRuntimeClass** is a Windows Runtime interface. Note the **HRESULT** return type, rather than the raising of exceptions. And the use of artifacts such as the **HSTRING** handle (it's good practice to set that handle back to `nullptr` when you're finished with it). This gives a taste of what the Windows Runtime looks like at the application binary level; in other words, at the COM programming level.

The Windows Runtime is based on Component Object Model (COM) APIs. You can access the Windows Runtime that way, or you can access it through *language projections*. A projection hides the COM details, and provides a more natural programming experience for a given language.

For example, if you look in the folder "%WindowsSdkDir%Include\10.0.17134.0\cppwinrt\winrt" (again, adjust the SDK version number for your case, if necessary), then you'll find the C++/WinRT language projection headers. There's a header for each Windows namespace, just like there's one ABI header per Windows namespace. Here's an example of including one of the C++/WinRT headers.

```cppwinrt
#include <winrt/Windows.Foundation.h>
```

And, from that header, here (simplified) is the C++/WinRT equivalent of that ABI type we just saw.

```
namespace winrt::Windows::Foundation
{
    struct Uri : IUriRuntimeClass, ...
    {
        winrt::hstring AbsoluteUri() const { ... }
        ...
    };
}
```

The interface here is modern, standard C++. It does away with **HRESULT**s (C++/WinRT raises exceptions if necessary). And the accessor function returns a simple string object, which is cleaned up at the end of its scope.

This topic is for cases when you want to interop with, or port, code that works at the Application Binary Interface (ABI) layer.

## Converting to and from ABI types in code
For safety and simplicity, for conversions in both directions you can simply use [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr), [**com_ptr::as**](/uwp/cpp-ref-for-winrt/com-ptr#comptras-function), and [**winrt::Windows::Foundation::IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function). Here's a code example (based on the **Console App** project template), which also illustrates how you can use namespace aliases for the different islands to deal with otherwise potential namespace collisions between the C++/WinRT projection and the ABI.

```cppwinrt
// main.cpp
#include "pch.h"

#include <windows.foundation.h>
#include <winrt/Windows.Foundation.h>

namespace winrt
{
    using namespace Windows::Foundation;
}

namespace abi
{
    using namespace ABI::Windows::Foundation;
};

int main()
{
    winrt::init_apartment();

    winrt::Uri uri(L"http://aka.ms/cppwinrt");

    // Convert to an ABI type.
    winrt::com_ptr<abi::IStringable> ptr = uri.as<abi::IStringable>();

    // Convert from an ABI type.
    uri = ptr.as<winrt::Uri>();
    winrt::IStringable uriAsIStringable = ptr.as<winrt::IStringable>();
}
```

The implementations of the **as** functions call [**QueryInterface**](https://msdn.microsoft.com/library/windows/desktop/ms682521). If you want lower-level conversions that only call [**AddRef**](https://msdn.microsoft.com/library/windows/desktop/ms691379), then you can use the [**winrt::copy_to_abi**](/uwp/cpp-ref-for-winrt/copy-to-abi) and [**winrt::copy_from_abi**](/uwp/cpp-ref-for-winrt/copy-from-abi) helper functions. This next code example adds these lower-level conversions to the code example above.

```cppwinrt
int main()
{
    ...

    // Lower-level conversions that only call AddRef.

    // Convert to an ABI type.
    ptr = nullptr;
    winrt::copy_to_abi(uri, *ptr.put_void());

    // Convert from an ABI type.
    uri = nullptr;
    winrt::copy_from_abi(uri, ptr.get());
    ptr = nullptr;
}
```

Here are other similarly low-level conversions techniques but using raw pointers to ABI interface types (those defined by the Windows SDK headers) this time.

```cppwinrt
    ...

    // Copy to an owning raw ABI pointer with copy_to_abi.
    abi::IStringable* owning = nullptr;
    winrt::copy_to_abi(uri, *reinterpret_cast<void**>(&owning));

    // Copy from a raw ABI pointer.
    uri = nullptr;
    winrt::copy_from_abi(uri, owning);
    owning->Release();
```

For the lowest-level conversions, which only copy addresses, you can use the [**winrt::get_abi**](/uwp/cpp-ref-for-winrt/get-abi), [**winrt::detach_abi**](/uwp/cpp-ref-for-winrt/detach-abi), and [**winrt::attach_abi**](/uwp/cpp-ref-for-winrt/attach-abi) helper functions.

```cppwinrt
    ...

    // Lowest-level conversions that only copy addresses

    // Convert to a non-owning ABI object with get_abi.
    abi::IStringable* non_owning = static_cast<abi::IStringable*>(winrt::get_abi(uri));
    WINRT_ASSERT(non_owning);

    // Avoid interlocks this way.
    owning = static_cast<abi::IStringable*>(winrt::detach_abi(uri));
    WINRT_ASSERT(!uri);
    winrt::attach_abi(uri, owning);
    WINRT_ASSERT(uri);
```

## convert_from_abi function
This helper function converts a raw ABI interface pointer to an equivalent C++/WinRT object, with minimal overhead.

```cppwinrt
template <typename T>
T convert_from_abi(::IUnknown* from)
{
    T to{ nullptr };

    winrt::check_hresult(from->QueryInterface(winrt::guid_of<T>(),
        reinterpret_cast<void**>(winrt::put_abi(to))));

    return to;
}
```

The function simply calls [**QueryInterface**](https://msdn.microsoft.com/library/windows/desktop/ms682521) to query for the default interface of the requested C++/WinRT type.

As we've seen, a helper function is not required to convert from a C++/WinRT object to the equivalent ABI interface pointer. Simply use the [**winrt::Windows::Foundation::IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) (or [**try_as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntryas-function)) member function to query for the requested interface. The **as** and **try_as** functions return a [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) object wrapping the requested ABI type.

## Code example using convert_from_abi
Here's a code example showing this helper function in practice.

```cppwinrt
// main.cpp
#include "pch.h"

#include <windows.foundation.h>
#include <winrt/Windows.Foundation.h>
#include <iostream>

using namespace winrt;
using namespace Windows::Foundation;

namespace winrt
{
    using namespace Windows::Foundation;
}

namespace abi
{
    using namespace ABI::Windows::Foundation;
};

template <typename T>
T convert_from_abi(::IUnknown* from)
{
    T to{ nullptr };

    winrt::check_hresult(from->QueryInterface(winrt::guid_of<T>(),
        reinterpret_cast<void**>(winrt::put_abi(to))));

    return to;
}

int main()
{
    winrt::init_apartment();

    winrt::Uri uri(L"http://aka.ms/cppwinrt");
    std::wcout << "C++/WinRT: " << uri.Domain().c_str() << std::endl;

    // Convert to an ABI type.
    winrt::com_ptr<abi::IUriRuntimeClass> ptr = uri.as<abi::IUriRuntimeClass>();
    winrt::hstring domain;
    winrt::check_hresult(ptr->get_Domain(put_abi(domain)));
    std::wcout << "ABI: " << domain.c_str() << std::endl;

    // Convert from an ABI type.
    winrt::Uri uri_from_abi = convert_from_abi<winrt::Uri>(ptr.get());

    WINRT_ASSERT(uri.Domain() == uri_from_abi.Domain());
    WINRT_ASSERT(uri == uri_from_abi);
}
```

## Important APIs
* [AddRef](https://msdn.microsoft.com/library/windows/desktop/ms691379)
* [QueryInterface](https://msdn.microsoft.com/library/windows/desktop/ms682521)
* [winrt::attach_abi](/uwp/cpp-ref-for-winrt/attach-abi)
* [winrt::com_ptr struct template](/uwp/cpp-ref-for-winrt/com-ptr)
* [winrt::copy_from_abi](/uwp/cpp-ref-for-winrt/copy-from-abi)
* [winrt::copy_to_abi](/uwp/cpp-ref-for-winrt/copy-to-abi)
* [winrt::detach_abi](/uwp/cpp-ref-for-winrt/detach-abi)
* [winrt::get_abi](/uwp/cpp-ref-for-winrt/get-abi)
* [winrt::Windows::Foundation::IUnknown::as member function](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function)
* [winrt::Windows::Foundation::IUnknown::try_as member function](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntryas-function)
