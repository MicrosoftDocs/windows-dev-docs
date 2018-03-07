---
author: stevewhims
description: With C++/WinRT, you can call WinRT APIs using standard C++ wide string types, or you can use the winrt::hstring type.
title: Strings
ms.author: stwhi
ms.date: 03/01/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, string
ms.localizationpriority: medium
---

# Strings
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

With C++/WinRT, you can call Windows Runtime (WinRT) APIs using standard C++ wide string types. C++/WinRT does have a custom string type called [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring?branch=live) (defined in `%ProgramFiles(x86)%\Windows Kits\10\Include\<WindowsTargetPlatformVersion>\cppwinrt\winrt\base.h`). And that's the string type that WinRT constructors, functions, and properties actually take and return. But in many cases&mdash;thanks to **hstring**'s conversion constructors and conversion operators&mdash;you can choose whether or not to be aware of **hstring** in your programming.

## Using std::wstring (and optionally [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring?branch=live)) with [**Uri**](/uwp/api/windows.foundation.uri?branch=live)

[**Windows::Foundation::Uri**](/uwp/api/windows.foundation.uri?branch=live) is constructed from a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring?branch=live).

```cppwinrt
public : Uri(hstring uri) const;
```

But **hstring** has [conversion constructors](/uwp/api/windows.foundation.uri?branch=live#hstringhstring-constructor) that let you work with it without needing to be aware of it. Here's a code example showing how to make a **Uri** from a wide string literal and from a **std::wstring**.

```cppwinrt
#include "winrt/Windows.Foundation.h"

using namespace winrt;
using namespace Windows::Foundation;

int main()
{
	init_apartment();

	// You can make a Uri from a wide string literal.
	Uri contosoUri{ L"http://www.contoso.com" };

	// Or from a std::wstring.
	std::wstring wideString{ L"http://www.adventure-works.com" };
	Uri awUri{ wideString };
}
```

The property accessor [**Uri::Domain**](https://docs.microsoft.com/uwp/api/windows.foundation.uri#Windows_Foundation_Uri_Domain) is of type **hstring**.

```cppwinrt
public: hstring Domain();
```

But, again, being aware of that detail is optional thanks to **hstring**'s [conversion operator to **std::wstring_view**](/uwp/api/windows.foundation.uri?branch=live#hstringoperator-stdwstringview).

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
public: hstring ToString() const;
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

You can use the [hstring::c_str function](/uwp/api/windows.foundation.uri?branch=live#hstringcstr-function) to get a standard wide string from an **hstring**.

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
public : Uri CombineUri(hstring relativeUri) const;
```

All of the options you've just seen also apply in such cases.

```cppwinrt
std::wstring contact{ L"contact" };
contosoUri = contosoUri.CombineUri(contact);
	
std::wcout << contosoUri.ToString().c_str() << std::endl;
```

## winrt::hstring functions and operators
A host of constructors, operators, functions, and iterators are implemented for **hstring**. An **hstring** is a range, so you can use it with range-based `for`, or with `std::for_each`.

For more examples and info, see the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring?branch=live) API reference topic.

## Important APIs
[winrt::hstring (C++/WinRT)](/uwp/cpp-ref-for-winrt/hstring?branch=live)
