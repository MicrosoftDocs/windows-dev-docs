---
author: stevewhims
description: This topic shows how to convert between application binary interface (ABI) and C++/WinRT objects.
title: Interop between C++/WinRT and the ABI
ms.author: stwhi
ms.date: 04/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, interop, ABI
ms.localizationpriority: medium
---

# Interop between [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) and the ABI
This topic shows how to convert between application binary interface (ABI) and C++/WinRT objects. You can use it to interop between code that uses these two ways of programming with the Windows Runtime, or you can use the function as you gradually move your code from the ABI to C++/WinRT.

## What are Windows Runtime ABI types?
The Windows SDK headers in the folder "%WindowsSdkDir%Include\10.0.17134.0\winrt" (adjust the SDK version number for your case, if necessary), are the Windows Runtime ABI header files. They were produced by the MIDL compiler. Here's an example of including one of these headers.

```
#include <windows.foundation.h>
```

And here's a simplified example of one of the ABI types that you'll find in that particular header.

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

IUriRuntimeClass is a COM interface. But more than that&mdash;since its base is IInspectable&mdash;IUriRuntimeClass is a Windows Runtime interface. Note the **HRESULT** return type, rather than the raising of exceptions. And the use of artifacts such as the **HSTRING** handle (it's good practice to set that handle back to `nullptr` when you're finished with it). This gives a taste of what the Windows Runtime looks like at the application binary level; in other words, at the COM programming level.

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
		hstring AbsoluteUri() const { ... }
		...
	};
}
```

The interface here is modern, standard C++. It does away with **HRESULT**s (C++/WinRT raises exceptions if necessary). And the accessor function returns a simple string object, which is cleaned up at the end of its scope.

## convert_from_abi function
This helper function converts an ABI interface pointer (defined by the Windows SDK headers) to an equivalent C++/WinRT object.

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

This function simply calls [**QueryInterface**](https://msdn.microsoft.com/library/windows/desktop/ms682521) to query for the default interface of the requested C++/WinRT type.

A helper function is not required to convert from a C++/WinRT object to the equivalent ABI interface pointer. Simply use the [**IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) (or [**try_as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntryas-function)) member function to query for the requested interface. **as** returns a [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) object wrapping the requested ABI type.

## Code example
Here's a code example (based on the **Console App** project template) showing this helper in practice. It also illustrates how you can deal with namespace collisions between the ABI and the projection.

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

	// Convert from a C++/WinRT type to a Windows SDK type.
	winrt::com_ptr<abi::IUriRuntimeClass> ptr = uri.as<abi::IUriRuntimeClass>();
	winrt::hstring domain;
	winrt::check_hresult(ptr->get_Domain(put_abi(domain)));
	std::wcout << "ABI: " << domain.c_str() << std::endl;

	// Convert from a Windows SDK type to a C++/WinRT type.
	winrt::Uri uri_from_abi = convert_from_abi<winrt::Uri>(ptr.get());

	WINRT_ASSERT(uri.Domain() == uri_from_abi.Domain());
	WINRT_ASSERT(uri == uri_from_abi);
}
```

## Important APIs
* [QueryInterface](https://msdn.microsoft.com/library/windows/desktop/ms682521)
* [winrt::com_ptr struct template](/uwp/cpp-ref-for-winrt/com-ptr)
