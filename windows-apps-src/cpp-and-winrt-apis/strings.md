---
author: stevewhims
description: With C++/WinRT, you can call WinRT APIs using standard C++ wide string types, or you can use the winrt::hstring type.
title: String handling in C++/WinRT
ms.author: stwhi
ms.date: 04/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, string
ms.localizationpriority: medium
---

# String handling in C++/WinRT
With C++/WinRT, you can call Windows Runtime (WinRT) APIs using C++ Standard Library wide string types. C++/WinRT does have a custom string type called [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) (defined in `%ProgramFiles(x86)%\Windows Kits\10\Include\<WindowsTargetPlatformVersion>\cppwinrt\winrt\base.h`). And that's the string type that WinRT constructors, functions, and properties actually take and return. But in many cases&mdash;thanks to **hstring**'s conversion constructors and conversion operators&mdash;you can choose whether or not to be aware of **hstring** in your programming.

There are many string types in C++. Variants exist in many libraries in addition to **std::basic_string** from the C++ Standard Library. C++17 has string conversion utilities, and **std::basic_string_view**, to bridge the gaps between all of the string types. **hstring** provides convertibility with **std::wstring_view** to provide the interoperability that **std::basic_string_view** was designed for.

## Using **std::wstring** (and optionally [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring)) with [**Uri**](/uwp/api/windows.foundation.uri)

[**Windows::Foundation::Uri**](/uwp/api/windows.foundation.uri) is constructed from a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring).

```cppwinrt
public:
	Uri(winrt::hstring uri) const;
```

But **hstring** has [conversion constructors](/uwp/api/windows.foundation.uri#hstringhstring-constructor) that let you work with it without needing to be aware of it. Here's a code example showing how to make a **Uri** from a wide string literal, from a wide string view, and from a **std::wstring**.

```cppwinrt
#include "winrt/Windows.Foundation.h"
#include <string_view>

using namespace winrt;
using namespace Windows::Foundation;

int main()
{
	using namespace std::literals;

	init_apartment();

	// You can make a Uri from a wide string literal.
	Uri contosoUri{ L"http://www.contoso.com" };

	// Or from a wide string view.
	Uri contosoSVUri{ L"http://www.contoso.com"sv };

	// Or from a std::wstring.
	std::wstring wideString{ L"http://www.adventure-works.com" };
	Uri awUri{ wideString };
}
```

The property accessor [**Uri::Domain**](https://docs.microsoft.com/uwp/api/windows.foundation.uri.Domain) is of type **hstring**.

```cppwinrt
public:
	winrt::hstring Domain();
```

But, again, being aware of that detail is optional thanks to **hstring**'s [conversion operator to **std::wstring_view**](/uwp/api/windows.foundation.uri#hstringoperator-stdwstringview).

```cppwinrt
// Access a property of type hstring, via a conversion operator to a standard type.
std::wstring domainWstring{ contosoUri.Domain() }; // L"contoso.com"
domainWstring = awUri.Domain(); // L"adventure-works.com"

// Or, you can choose to keep the hstring unconverted.
hstring domainHstring{ contosoUri.Domain() }; // L"contoso.com"
domainHstring = awUri.Domain(); // L"adventure-works.com"
```

Similarly, [**IStringable::ToString**](https://msdn.microsoft.com/library/windows/desktop/dn302136) returns hstring.

```cppwinrt
public:
	hstring ToString() const;
```

**Uri** implements the [**IStringable**](https://msdn.microsoft.com/library/windows/desktop/dn302135) interface.

```cppwinrt
// Access hstring's IStringable::ToString, via a conversion operator to a standard type.
std::wstring tostringWstring{ contosoUri.ToString() }; // L"http://www.contoso.com/"
tostringWstring = awUri.ToString(); // L"http://www.adventure-works.com/"

// Or you can choose to keep the hstring unconverted.
hstring tostringHstring{ contosoUri.ToString() }; // L"http://www.contoso.com/"
tostringHstring = awUri.ToString(); // L"http://www.adventure-works.com/"
```

You can use the [hstring::c_str function](/uwp/api/windows.foundation.uri#hstringcstr-function) to get a standard wide string from an **hstring** (just as you can from a **std::wstring**).

```cppwinrt
#include <iostream>
std::wcout << tostringHstring.c_str() << std::endl;
```
If you have an **hstring** then you can make a **Uri** from it.

```cppwinrt
Uri awUriFromHstring{ tostringHstring };
```

Consider a method that takes an **hstring**.

```cppwinrt
public:
	Uri CombineUri(winrt::hstring relativeUri) const;
```

All of the options you've just seen also apply in such cases.

```cppwinrt
std::wstring contact{ L"contact" };
contosoUri = contosoUri.CombineUri(contact);
	
std::wcout << contosoUri.ToString().c_str() << std::endl;
```

**hstring** has a member **std::wstring_view** conversion operator, and the conversion is achieved at no cost.

```cppwinrt
void legacy_print(std::wstring_view view);

void Print(winrt::hstring const& hstring)
{
	legacy_print(hstring);
}
```

## **winrt::hstring** functions and operators
A host of constructors, operators, functions, and iterators are implemented for **hstring**.

An **hstring** is a range, so you can use it with range-based `for`, or with `std::for_each`. It also provides comparison operators for naturally and efficiently comparing against its counterparts in the C++ Standard Library. And it includes everything you need to use **hstring** as a key for associative containers. We recognize that many C++ libraries use **std::basic_string**, and work exclusively with UTF-8 text. As a convenience, we provide helpers for converting back and forth.

```cppwinrt
hstring w{ L"hello world" };
 
std::string c = to_string(w);
assert(c == "hello world");
 
w = to_hstring(c);
assert(w == L"hello world");
```

For more examples and info about **hstring** functions and operators, see the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) API reference topic.

## The rationale for **winrt::hstring** and **winrt::param::hstring**
The Windows Runtime is implemented in terms of **wchar_t** characters, but WinRT's Application Binary Interface (ABI) is not a subset of what either **std::wstring** or **std::wstring_view** provide. Using those would lead to significant inefficiency. Instead, C++/WinRT provides **winrt::hstring**, which represents an immutable string consistent with the underlying [HSTRING](https://msdn.microsoft.com/library/windows/desktop/br205775), and implemented behind an interface similar to that of **std::wstring**. 

You may notice that C++/WinRT input parameters that should logically accept **winrt::hstring** actually expect **winrt::param::hstring**. The **param** namespace contains a set of types used exclusively to optimize input parameters to naturally bind to C++ Standard Library types and avoid copies and other inefficiencies. You shouldn't use these types directly. If you want to use an optimization for your own functions then use **std::wstring_view**.

The upshot is that you can largely ignore the specifics of WinRT string management, and just work with efficiency with what you know. And that's important, given how heavily strings are used in WinRT.

## Important APIs
* [winrt::hstring (C++/WinRT)](/uwp/cpp-ref-for-winrt/hstring)
