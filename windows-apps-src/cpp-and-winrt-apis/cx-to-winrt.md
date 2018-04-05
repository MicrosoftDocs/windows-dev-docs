---
author: stevewhims
description: This topic shows two helper functions that can be used to convert between C++/CX and C++/WinRT objects.
title: Move from C++/CX to C++/WinRT
ms.author: stwhi
ms.date: 04/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, interop, C++/CX
ms.localizationpriority: medium
---

# Move from C++/CX to C++/WinRT
This topic shows two helper functions that can be used to convert between [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx?branch=live) and C++/WinRT objects. You can use them to interop between code that uses the two language projections, or you can use the functions as you gradually move your code from C++/CX to C++/WinRT.

## from_cx and to_cx functions
This helper function converts a C++/CX object to an equivalent C++/WinRT object.

```cppwinrt
template <typename T>
T from_cx(Platform::Object^ from)
{
    T to{ nullptr };
 
    winrt::check_hresult(reinterpret_cast<::IUnknown*>(from)
        ->QueryInterface(winrt::guid_of<T>(),
                         reinterpret_cast<void**>(winrt::put_abi(to))));
 
    return to;
}
```

The **winrt::put_abi** function retrieves the address of a C++/WinRT object's underlying [IUnknown interface](https://msdn.microsoft.com/library/windows/desktop/ms680509) pointer so that it can be set to another value.

The helper function below converts a C++/WinRT object to an equivalent C++/CX object.

```cppwinrt
template <typename T>
T^ to_cx(winrt::Windows::Foundation::IUnknown const& from)
{
    return safe_cast<T^>(reinterpret_cast<Platform::Object^>(winrt::get_abi(from)));
}
```

The [**winrt::get_abi**](/uwp/cpp-ref-for-winrt/get-abi) function retrieves a pointer to a C++/WinRT object's underlying [IUnknown interface](https://msdn.microsoft.com/library/windows/desktop/ms680509) pointer. The function casts that pointer to a C++/CX object before using the C++/CX safe_cast extension to query for the requested C++/CX type.

Here's a code example showing the two helper functions in use.

```cppwinrt
namespace cx
{
    using namespace Windows::Foundation;
}

namespace winrt
{
    using namespace Windows::Foundation;
}

void sample()
{
    winrt::Uri uri{ L"https://blogs.windows.com/feed" };

    cx::Uri^ uri2 = to_cx<cx::Uri>(uri);

    winrt::Uri uri3 = from_cx<winrt::Uri>(uri2);

    assert(uri == uri3); // identity
    assert(uri2->Domain == L"blogs.windows.com");
    assert(uri3.Domain() == L"blogs.windows.com");
}
```

## Important APIs
[winrt::get_abi (C++/WinRT)](/uwp/cpp-ref-for-winrt/get-abi)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
