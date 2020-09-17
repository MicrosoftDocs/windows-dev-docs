---
description: With C++/WinRT, you can call Windows Runtime APIs using standard C++ wide string types, or you can use the winrt::hstring type.
title: String handling in C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, string
ms.localizationpriority: medium
---

# String handling in C++/WinRT

With [C++/WinRT](./intro-to-using-cpp-with-winrt.md), you can call Windows Runtime APIs using C++ Standard Library wide string types such as **std::wstring** (note: not with narrow string types such as **std::string**). C++/WinRT does have a custom string type called [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) (defined in the C++/WinRT base library, which is `%WindowsSdkDir%Include\<WindowsTargetPlatformVersion>\cppwinrt\winrt\base.h`). And that's the string type that Windows Runtime constructors, functions, and properties actually take and return. But in many cases&mdash;thanks to **hstring**'s conversion constructors and conversion operators&mdash;you can choose whether or not to be aware of **hstring** in your client code. If you're *authoring* APIs, then you're more likely to need to know about **hstring**.

There are many string types in C++. Variants exist in many libraries in addition to **std::basic_string** from the C++ Standard Library. C++17 has string conversion utilities, and **std::basic_string_view**, to bridge the gaps between all of the string types.  [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) provides convertibility with **std::wstring_view** to provide the interoperability that **std::basic_string_view** was designed for.

## Using **std::wstring** (and optionally **winrt::hstring**) with **Uri**
[**Windows::Foundation::Uri**](/uwp/api/windows.foundation.uri) is constructed from a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring).

```cppwinrt
public:
    Uri(winrt::hstring uri) const;
```

But **hstring** has [conversion constructors](/uwp/cpp-ref-for-winrt/hstring#hstringhstring-constructor) that let you work with it without needing to be aware of it. Here's a code example showing how to make a **Uri** from a wide string literal, from a wide string view, and from a **std::wstring**.

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <string_view>

using namespace winrt;
using namespace Windows::Foundation;

int main()
{
    using namespace std::literals;

    winrt::init_apartment();

    // You can make a Uri from a wide string literal.
    Uri contosoUri{ L"http://www.contoso.com" };

    // Or from a wide string view.
    Uri contosoSVUri{ L"http://www.contoso.com"sv };

    // Or from a std::wstring.
    std::wstring wideString{ L"http://www.adventure-works.com" };
    Uri awUri{ wideString };
}
```

The property accessor [**Uri::Domain**](/uwp/api/windows.foundation.uri.Domain) is of type **hstring**.

```cppwinrt
public:
    winrt::hstring Domain();
```

But, again, being aware of that detail is optional thanks to **hstring**'s [conversion operator to **std::wstring_view**](/uwp/cpp-ref-for-winrt/hstring#hstringoperator-stdwstring_view).

```cppwinrt
// Access a property of type hstring, via a conversion operator to a standard type.
std::wstring domainWstring{ contosoUri.Domain() }; // L"contoso.com"
domainWstring = awUri.Domain(); // L"adventure-works.com"

// Or, you can choose to keep the hstring unconverted.
hstring domainHstring{ contosoUri.Domain() }; // L"contoso.com"
domainHstring = awUri.Domain(); // L"adventure-works.com"
```

Similarly, [**IStringable::ToString**](/windows/desktop/api/windows.foundation/nf-windows-foundation-istringable-tostring) returns hstring.

```cppwinrt
public:
    hstring ToString() const;
```

**Uri** implements the [**IStringable**](/windows/desktop/api/windows.foundation/nn-windows-foundation-istringable) interface.

```cppwinrt
// Access hstring's IStringable::ToString, via a conversion operator to a standard type.
std::wstring tostringWstring{ contosoUri.ToString() }; // L"http://www.contoso.com/"
tostringWstring = awUri.ToString(); // L"http://www.adventure-works.com/"

// Or you can choose to keep the hstring unconverted.
hstring tostringHstring{ contosoUri.ToString() }; // L"http://www.contoso.com/"
tostringHstring = awUri.ToString(); // L"http://www.adventure-works.com/"
```

You can use the [**hstring::c_str function**](/uwp/cpp-ref-for-winrt/hstring#hstringc_str-function) to get a standard wide string from an **hstring** (just as you can from a **std::wstring**).

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
A host of constructors, operators, functions, and iterators are implemented for  [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring).

An **hstring** is a range, so you can use it with range-based `for`, or with `std::for_each`. It also provides comparison operators for naturally and efficiently comparing against its counterparts in the C++ Standard Library. And it includes everything you need to use **hstring** as a key for associative containers.

We recognize that many C++ libraries use **std::string**, and work exclusively with UTF-8 text. As a convenience, we provide helpers, such as [**winrt::to_string**](/uwp/cpp-ref-for-winrt/to-string) and [**winrt::to_hstring**](/uwp/cpp-ref-for-winrt/to-hstring), for converting back and forth.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
winrt::hstring w{ L"Hello, World!" };

std::string c = winrt::to_string(w);
WINRT_ASSERT(c == "Hello, World!");

w = winrt::to_hstring(c);
WINRT_ASSERT(w == L"Hello, World!");
```

For more examples and info about **hstring** functions and operators, see the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) API reference topic.

## The rationale for **winrt::hstring** and **winrt::param::hstring**
The Windows Runtime is implemented in terms of **wchar_t** characters, but the Windows Runtime's Application Binary Interface (ABI) is not a subset of what either **std::wstring** or **std::wstring_view** provide. Using those would lead to significant inefficiency. Instead, C++/WinRT provides **winrt::hstring**, which represents an immutable string consistent with the underlying [HSTRING](/windows/desktop/WinRT/hstring), and implemented behind an interface similar to that of **std::wstring**. 

You may notice that C++/WinRT input parameters that should logically accept **winrt::hstring** actually expect **winrt::param::hstring**. The **param** namespace contains a set of types used exclusively to optimize input parameters to naturally bind to C++ Standard Library types and avoid copies and other inefficiencies. You shouldn't use these types directly. If you want to use an optimization for your own functions then use **std::wstring_view**. Also see [Passing parameters into the ABI boundary](./pass-parms-to-abi.md).

The upshot is that you can largely ignore the specifics of Windows Runtime string management, and just work with efficiency with what you know. And that's important, given how heavily strings are used in the Windows Runtime.

## Formatting strings
One option for string-formatting is **std::wostringstream**. Here's an example that formats and displays a simple debug trace message.

```cppwinrt
#include <sstream>
#include <winrt/Windows.UI.Input.h>
#include <winrt/Windows.UI.Xaml.Input.h>
...
void MainPage::OnPointerPressed(winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs const& e)
{
    winrt::Windows::Foundation::Point const point{ e.GetCurrentPoint(nullptr).Position() };
    std::wostringstream wostringstream;
    wostringstream << L"Pointer pressed at (" << point.X << L"," << point.Y << L")" << std::endl;
    ::OutputDebugString(wostringstream.str().c_str());
}
```

## The correct way to set a property

You set a property by passing a value to a setter function. Here's an example.

```cppwinrt
// The right way to set the Text property.
myTextBlock.Text(L"Hello!");
```

The code below is incorrect. It compiles, but all it does is to modify the temporary **winrt::hstring** returned by the **Text()** accessor function, and then to throw the result away.

```cppwinrt
// *Not* the right way to set the Text property.
myTextBlock.Text() = L"Hello!";
```

## Important APIs
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt::to_hstring function](/uwp/cpp-ref-for-winrt/to-hstring)
* [winrt::to_string function](/uwp/cpp-ref-for-winrt/to-string)