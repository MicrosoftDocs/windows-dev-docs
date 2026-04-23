---
description: This topic shows how to convert between application binary interface (ABI) and C++/WinRT objects.
title: Interop between C++/WinRT and the ABI
ms.date: 09/29/2023
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, interop, ABI
ms.localizationpriority: medium
---

# Interop between C++/WinRT and the ABI

This topic shows how to convert between SDK application binary interface (ABI) and [C++/WinRT](./intro-to-using-cpp-with-winrt.md) objects. You can use these techniques to interop between code that uses these two ways of programming with the Windows Runtime, or you can use them as you gradually move your code from the ABI to C++/WinRT.

In general, C++/WinRT exposes ABI types as **void\***, so that you don't need to include platform header files.

> [!NOTE]
> In the code examples, we use `reinterpret_cast` (rather than `static_cast`) in an effort to *telegraph* what are inherently unsafe casts.

## What is the Windows Runtime ABI, and what are ABI types?
A Windows Runtime class (runtime class) is really an abstraction. This abstraction defines a binary interface (the Application Binary Interface, or ABI) that allows various programming languages to interact with an object. Regardless of programming language, client code interaction with a Windows Runtime object happens at the lowest level, with client language constructs translated into calls into the object's ABI.

The Windows SDK headers in the folder "%WindowsSdkDir%Include\10.0.17134.0\winrt" (adjust the SDK version number for your case, if necessary), are the Windows Runtime ABI header files. They were produced by the MIDL compiler. Here's an example of including one of these headers.

```cpp
#include <windows.foundation.h>
```

And here's a simplified example of one of the ABI types that you'll find in that particular SDK header. Note the **ABI** namespace; **Windows::Foundation**, and all other Windows namespaces, are declared by the SDK headers within the **ABI** namespace.

```cpp
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

```cppwinrt
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
For safety and simplicity, for conversions in both directions you can simply use [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr), [**com_ptr::as**](/uwp/cpp-ref-for-winrt/com-ptr#com_ptras-function), and [**winrt::Windows::Foundation::IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function). Here's a code example (based on the **Console App** project template), which also illustrates how you can use namespace aliases for the different islands to deal with otherwise potential namespace collisions between the C++/WinRT projection and the ABI.

```cppwinrt
// pch.h
#pragma once
#include <windows.foundation.h>
#include <unknwn.h>
#include "winrt/Windows.Foundation.h"

// main.cpp
#include "pch.h"

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
    winrt::com_ptr<abi::IStringable> ptr{ uri.as<abi::IStringable>() };

    // Convert from an ABI type.
    uri = ptr.as<winrt::Uri>();
    winrt::IStringable uriAsIStringable{ ptr.as<winrt::IStringable>() };
}
```

The implementations of the **as** functions call [**QueryInterface**](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(q)). If you want lower-level conversions that only call [**AddRef**](/windows/desktop/api/unknwn/nf-unknwn-iunknown-addref), then you can use the [**winrt::copy_to_abi**](/uwp/cpp-ref-for-winrt/copy-to-abi) and [**winrt::copy_from_abi**](/uwp/cpp-ref-for-winrt/copy-from-abi) helper functions. This next code example adds these lower-level conversions to the code example above.

> [!IMPORTANT]
> When interoperating with ABI types it's critical that the ABI type used corresponds to the default interface of the C++/WinRT object. Otherwise, invocations of methods on the ABI type will actually end up calling methods in the same vtable slot on the default interface with very unexpected results. Note that [**winrt::copy_to_abi**](/uwp/cpp-ref-for-winrt/copy-from-abi) does not protect against this at compile time since it uses **void\*** for all ABI types and assumes that the caller has been careful not to mis-match the types. This is to avoid requiring C++/WinRT headers to reference ABI headers when ABI types may never be used.

```cppwinrt
int main()
{
    // The code in main() already shown above remains here.

    // Lower-level conversions that only call AddRef.

    // Convert to an ABI type.
    ptr = nullptr;
    winrt::copy_to_abi(uriAsIStringable, *ptr.put_void());

    // Convert from an ABI type.
    uri = nullptr;
    winrt::copy_from_abi(uriAsIStringable, ptr.get());
    ptr = nullptr;
}
```

Here are other similarly low-level conversions techniques but using raw pointers to ABI interface types (those defined by the Windows SDK headers) this time.

```cppwinrt
    // The code in main() already shown above remains here.

    // Copy to an owning raw ABI pointer with copy_to_abi.
    abi::IStringable* owning{ nullptr };
    winrt::copy_to_abi(uriAsIStringable, *reinterpret_cast<void**>(&owning));

    // Copy from a raw ABI pointer.
    uri = nullptr;
    winrt::copy_from_abi(uriAsIStringable, owning);
    owning->Release();
```

For the lowest-level conversions, which only copy addresses, you can use the [**winrt::get_abi**](/uwp/cpp-ref-for-winrt/get-abi), [**winrt::detach_abi**](/uwp/cpp-ref-for-winrt/detach-abi), and [**winrt::attach_abi**](/uwp/cpp-ref-for-winrt/attach-abi) helper functions.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
    // The code in main() already shown above remains here.

    // Lowest-level conversions that only copy addresses

    // Convert to a non-owning ABI object with get_abi.
    abi::IStringable* non_owning{ reinterpret_cast<abi::IStringable*>(winrt::get_abi(uriAsIStringable)) };
    WINRT_ASSERT(non_owning);

    // Avoid interlocks this way.
    owning = reinterpret_cast<abi::IStringable*>(winrt::detach_abi(uriAsIStringable));
    WINRT_ASSERT(!uriAsIStringable);
    winrt::attach_abi(uriAsIStringable, owning);
    WINRT_ASSERT(uriAsIStringable);
```

## convert_from_abi function
This helper function converts a raw ABI interface pointer to an equivalent C++/WinRT object, with minimal overhead.

```cppwinrt
template <typename T>
T convert_from_abi(::IUnknown* from)
{
    T to{ nullptr }; // `T` is a projected type.

    winrt::check_hresult(from->QueryInterface(winrt::guid_of<T>(),
        winrt::put_abi(to)));

    return to;
}
```

The function simply calls [**QueryInterface**](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(q)) to query for the default interface of the requested C++/WinRT type.

As we've seen, a helper function is not required to convert from a C++/WinRT object to the equivalent ABI interface pointer. Simply use the [**winrt::Windows::Foundation::IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) (or [**try_as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntry_as-function)) member function to query for the requested interface. The **as** and **try_as** functions return a [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) object wrapping the requested ABI type.

## Code example using convert_from_abi
Here's a code example showing this helper function in practice.

```cppwinrt
// pch.h
#pragma once
#include <windows.foundation.h>
#include <unknwn.h>
#include "winrt/Windows.Foundation.h"

// main.cpp
#include "pch.h"
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

namespace sample
{
    template <typename T>
    T convert_from_abi(::IUnknown* from)
    {
        T to{ nullptr }; // `T` is a projected type.

        winrt::check_hresult(from->QueryInterface(winrt::guid_of<T>(),
            winrt::put_abi(to)));

        return to;
    }
    inline auto put_abi(winrt::hstring& object) noexcept
    {
        return reinterpret_cast<HSTRING*>(winrt::put_abi(object));
    }
}

int main()
{
    winrt::init_apartment();

    winrt::Uri uri(L"http://aka.ms/cppwinrt");
    std::wcout << "C++/WinRT: " << uri.Domain().c_str() << std::endl;

    // Convert to an ABI type.
    winrt::com_ptr<abi::IUriRuntimeClass> ptr = uri.as<abi::IUriRuntimeClass>();
    winrt::hstring domain;
    winrt::check_hresult(ptr->get_Domain(sample::put_abi(domain)));
    std::wcout << "ABI: " << domain.c_str() << std::endl;

    // Convert from an ABI type.
    winrt::Uri uri_from_abi = sample::convert_from_abi<winrt::Uri>(ptr.get());

    WINRT_ASSERT(uri.Domain() == uri_from_abi.Domain());
    WINRT_ASSERT(uri == uri_from_abi);
}
```

## Interoperating with ABI COM interface pointers

The helper function template below illustrates how to copy an ABI COM interface pointer of a given type to its equivalent C++/WinRT projected smart pointer type.

```cppwinrt
template<typename To, typename From>
To to_winrt(From* ptr)
{
    To result{ nullptr };
    winrt::check_hresult(ptr->QueryInterface(winrt::guid_of<To>(), winrt::put_abi(result)));
    return result;
}
...
ID2D1Factory1* com_ptr{ ... };
auto cppwinrt_ptr {to_winrt<winrt::com_ptr<ID2D1Factory1>>(com_ptr)};
```

This next helper function template is equivalent, except that it copies from the smart pointer type from the [Windows Implementation Libraries (WIL)](https://github.com/Microsoft/wil).

```cppwinrt
template<typename To, typename From, typename ErrorPolicy>
To to_winrt(wil::com_ptr_t<From, ErrorPolicy> const& ptr)
{
    To result{ nullptr };
    if constexpr (std::is_same_v<typename ErrorPolicy::result, void>)
    {
        ptr.query_to(winrt::guid_of<To>(), winrt::put_abi(result));
    }
    else
    {
        winrt::check_result(ptr.query_to(winrt::guid_of<To>(), winrt::put_abi(result)));
    }
    return result;
}
```

Also see [Consume COM components with C++/WinRT](./consume-com.md).

### Unsafe interop with ABI COM interface pointers

The table that follows shows (in addition to other operations) unsafe conversions between an ABI COM interface pointer of a given type and its equivalent C++/WinRT projected smart pointer type. For the code in the table, assume these declarations.

```cppwinrt
winrt::Sample s;
ISample* p;

void GetSample(_Out_ ISample** pp);
```

Assume further that **ISample** is the default interface for **Sample**.

You can assert that at compile time with this code.

```cppwinrt
static_assert(std::is_same_v<winrt::default_interface<winrt::Sample>, winrt::ISample>);
```

| Operation | How to do it | Notes |
|-|-|-|
| Extract **ISample\*** from **winrt::Sample** | `p = reinterpret_cast<ISample*>(get_abi(s));` | *s* still owns the object. |
| Detach **ISample\*** from **winrt::Sample** | `p = reinterpret_cast<ISample*>(detach_abi(s));` | *s* no longer owns the object. |
| Transfer **ISample\*** to new **winrt::Sample** | `winrt::Sample s{ p, winrt::take_ownership_from_abi };` | *s* takes ownership of the object. |
| Set **ISample\*** into **winrt::Sample** | `*put_abi(s) = p;` | *s* takes ownership of the object. Any object previously owned by *s* is leaked (will assert in debug). |
| Receive **ISample\*** into **winrt::Sample** | `GetSample(reinterpret_cast<ISample**>(put_abi(s)));` | *s* takes ownership of the object. Any object previously owned by *s* is leaked (will assert in debug). |
| Replace **ISample\*** in **winrt::Sample** | `attach_abi(s, p);` | *s* takes ownership of the object. The object previously owned by *s* is freed. |
| Copy **ISample\*** to **winrt::Sample** | `copy_from_abi(s, p);` | *s* makes a new reference to the object. The object previously owned by *s* is freed. |
| Copy **winrt::Sample** to **ISample\*** | `copy_to_abi(s, reinterpret_cast<void*&>(p));` | *p* receives a copy of the object. Any object previously owned by *p* is leaked. |

## Interoperating with the ABI's GUID struct

**GUID** (`/previous-versions/aa373931(v%3Dvs.80)`) is projected as [**winrt::guid**](/uwp/cpp-ref-for-winrt/guid). For APIs that you implement, you must use **winrt::guid** for GUID parameters. Otherwise, there are automatic conversions between **winrt::guid** and **GUID** as long as you include `unknwn.h` (implicitly included by <windows.h> and many other header files) before you include any C++/WinRT headers.

If you don't do that, then you can hard-`reinterpret_cast` between them. For the table that follows, assume these declarations.

```cppwinrt
winrt::guid winrtguid;
GUID abiguid;
```

| Conversion | With `#include <unknwn.h>` | Without `#include <unknwn.h>` |
|-|-|-|
| From [**winrt::guid**](/uwp/cpp-ref-for-winrt/guid) to **GUID** | `abiguid = winrtguid;` | `abiguid = reinterpret_cast<GUID&>(winrtguid);` |
| From **GUID** to **winrt::guid** | `winrtguid = abiguid;` | `winrtguid = reinterpret_cast<winrt::guid&>(abiguid);` |

You can construct a **winrt::guid** like this.

```cppwinrt
winrt::guid myGuid{ 0xC380465D, 0x2271, 0x428C, { 0x9B, 0x83, 0xEC, 0xEA, 0x3B, 0x4A, 0x85, 0xC1} };
```

For a gist showing how to construct a **winrt::guid** from a string, see [make_guid.cpp](https://gist.github.com/kennykerr/6c948882de395c25b3218ad8d4daf362).

## Interoperating with the ABI's HSTRING

The table that follows shows conversions between **winrt::hstring** and [**HSTRING**](/windows/win32/winrt/hstring), and other operations. For the code in the table, assume these declarations.

```cppwinrt
winrt::hstring s;
HSTRING h;

void GetString(_Out_ HSTRING* value);
```

| Operation | How to do it | Notes |
|-|-|-|
| Extract **HSTRING** from **hstring** | `h = reinterpret_cast<HSTRING>(get_abi(s));` | *s* still owns the string. |
| Detach **HSTRING** from **hstring** | `h = reinterpret_cast<HSTRING>(detach_abi(s));` | *s* no longer owns the string. |
| Set **HSTRING** into **hstring** | `*put_abi(s) = h;` | *s* takes ownership of string. Any string previously owned by *s* is leaked (will assert in debug). |
| Receive **HSTRING** into **hstring** | `GetString(reinterpret_cast<HSTRING*>(put_abi(s)));` | *s* takes ownership of string. Any string previously owned by *s* is leaked (will assert in debug). |
| Replace **HSTRING** in **hstring** | `attach_abi(s, h);` | *s* takes ownership of string. The string previously owned by *s* is freed. |
| Copy **HSTRING** to **hstring** | `copy_from_abi(s, h);` | *s* makes a private copy of the string. The string previously owned by *s* is freed. |
| Copy **hstring** to **HSTRING** | `copy_to_abi(s, reinterpret_cast<void*&>(h));` | *h* receives a copy of the string. Any string previously owned by *h* is leaked. |

In addition, the Windows Implementation Libraries (WIL) [string helpers](https://github.com/microsoft/wil/wiki/String-helpers) perform basic string manipulations. To use the WIL string helpers, include [<wil/resource.h>](https://github.com/microsoft/wil/blob/master/include/wil/resource.h), and refer to the table below. Follow the links in the table for full details.

| Operation | WIL string helper for more info |
|-|-|
| Provide a raw Unicode or ANSI string pointer and an optional length; obtain a suitably-specialized **unique_any** wrapper | [wil::make_something_string](https://github.com/microsoft/wil/wiki/String-helpers#wilmake_something_string) |
| Unwrap a smart object until a raw null-terminated Unicode string pointer is found | [wil::str_raw_ptr](https://github.com/microsoft/wil/wiki/String-helpers#wilstr_raw_ptr) |
| Obtain the string wrapped by a smart pointer object; or the empty string `L""` if the smart pointer is empty | [wil::string_get_not_null](https://github.com/microsoft/wil/wiki/String-helpers#wilstring_get_not_null) |
| Concatenate any number of strings | [wil::str_concat](https://github.com/microsoft/wil/wiki/String-helpers#wilstr_concat) |
| Obtain a string from a printf-style format string and a corresponding parameter list | [wil::str_printf](https://github.com/microsoft/wil/wiki/String-helpers#wilstr_printf) |

## Important APIs
* [AddRef function](/windows/desktop/api/unknwn/nf-unknwn-iunknown-addref)
* [QueryInterface function](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(q))
* [winrt::attach_abi function](/uwp/cpp-ref-for-winrt/attach-abi)
* [winrt::com_ptr struct template](/uwp/cpp-ref-for-winrt/com-ptr)
* [winrt::copy_from_abi function](/uwp/cpp-ref-for-winrt/copy-from-abi)
* [winrt::copy_to_abi function](/uwp/cpp-ref-for-winrt/copy-to-abi)
* [winrt::detach_abi function](/uwp/cpp-ref-for-winrt/detach-abi)
* [winrt::get_abi function](/uwp/cpp-ref-for-winrt/get-abi)
* [winrt::Windows::Foundation::IUnknown::as member function](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function)
* [winrt::Windows::Foundation::IUnknown::try_as member function](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntry_as-function)
