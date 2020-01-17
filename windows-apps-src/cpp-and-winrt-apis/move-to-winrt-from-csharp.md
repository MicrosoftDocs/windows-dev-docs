---
description: This topic shows how to port C# code to its equivalent in C++/WinRT.
title: Move to C++/WinRT from C#
ms.date: 07/15/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C#
ms.localizationpriority: medium
---

# Move to C++/WinRT from C#

This topic shows how to port the code in a [C#](/visualstudio/get-started/csharp) project to its equivalent in [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt).

## Register an event handler

You can register an event handler in XAML markup.

```xaml
<Button x:Name="OpenButton" Click="OpenButton_Click" />
```

In C#, your **OpenButton_Click** method can be private, and XAML will still be able to connect it to the [**ButtonBase.Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event raised by *OpenButton*.

In C++/WinRT, your **OpenButton_Click** method must be public in your [implementation type](/windows/uwp/cpp-and-winrt-apis/author-apis).

```cppwinrt
namespace winrt::MyProject::implementation
{
    struct MyPage : MyPageT<MyPage>
    {
        void OpenButton_Click(
            winrt::Windows:Foundation::IInspectable const& sender,
            winrt::Windows::UI::Xaml::RoutedEventArgs const& args);
    }
};
```

Alternatively, you can make the registering class a friend of your implementation class, and **OpenButton_Click** private.

```cppwinrt
namespace winrt::MyProject::implementation
{
    struct MyPage : MyPageT<MyPage>
    {
    private:
        friend MyPageT;
        void OpenButton_Click(
            winrt::Windows:Foundation::IInspectable const& sender,
            winrt::Windows::UI::Xaml::RoutedEventArgs const& args);
    }
};
```

## Making a class available to the {Binding} markup extension

If you intend to use the {Binding} markup extension to data bind to your data type, then see [Binding object declared using {Binding}](/windows/uwp/data-binding/data-binding-in-depth#binding-object-declared-using-binding).

## Making a data source available to XAML markup

In C++/WinRT version 2.0.190530.8 and higher, [**winrt::single_threaded_observable_vector**](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector) creates an observable vector that supports both **[IObservableVector](/uwp/api/windows.foundation.collections.iobservablevector_t_)\<T\>** and **IObservableVector\<IInspectable\>**.

You can author your **Midl file (.idl)** like this (also see [Factoring runtime classes into Midl files (.idl)](/windows/uwp/cpp-and-winrt-apis/author-apis#factoring-runtime-classes-into-midl-files-idl)).

```idl
namespace Bookstore
{
    runtimeclass BookSku { ... }

    runtimeclass BookstoreViewModel
    {
        Windows.Foundation.Collections.IObservableVector<BookSku> BookSkus{ get; };
    }

    runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
    {
        MainPage();
        BookstoreViewModel MainViewModel{ get; };
    }
}
```

And implement like this.

```cppwinrt
// BookstoreViewModel.h
...
struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel>
{
    BookstoreViewModel()
    {
        m_bookSkus = winrt::single_threaded_observable_vector<Bookstore::BookSku>();
        m_bookSkus.Append(winrt::make<Bookstore::implementation::BookSku>(L"To Kill A Mockingbird"));
    }
    
	Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> BookSkus();
    {
        return m_bookSkus;
    }

private:
    Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> m_bookSkus;
};
...
```

For more info, see [XAML items controls; bind to a C++/WinRT collection](/windows/uwp/cpp-and-winrt-apis/binding-collection).

## Making a data source available to XAML markup (prior to C++/WinRT 2.0.190530.8)

XAML data binding requires that an items source implements **[IIterable](/uwp/api/windows.foundation.collections.iiterable_t_)\<IInspectable\>**, as well as one of the following combinations of interfaces.

- **IObservableVector\<IInspectable\>**
- **IBindableVector** and **INotifyCollectionChanged**
- **IBindableVector** and **IBindableObservableVector**
- **IBindableVector** by itself (will not respond to changes)
- **IVector\<IInspectable\>**
- **IBindableIterable** (will iterate and save elements into a private collection)

A generic interface such as **IVector\<T\>** can't be detected at runtime. Each **IVector\<T\>** has a different interface identifier (IID), which is a function of **T**. Any developer can expand the set of **T** arbitrarily, so clearly the XAML binding code can never know the full set to query for. That restriction isn't a problem for C# because every CLR object that implements **IEnumerable\<T\>** automatically implements **IEnumerable**. At the ABI level, that means that every object that implements **IObservableVector\<T\>** automatically implements **IObservableVector\<IInspectable\>**.

C++/WinRT doesn't offer that guarantee. If a C++/WinRT runtime class implements **IObservableVector\<T\>**, then we can't assume that an implementation of **IObservableVector\<IInspectable\>** is somehow also provided.

Consequently, here's how the previous example will need to look.

```idl
...
runtimeclass BookstoreViewModel
{
    // This is really an observable vector of BookSku.
    Windows.Foundation.Collections.IObservableVector<Object> BookSkus{ get; };
}
```

And the implementation.

```cppwinrt
// BookstoreViewModel.h
...
struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel>
{
    BookstoreViewModel()
    {
        m_bookSkus = winrt::single_threaded_observable_vector<Windows::Foundation::IInspectable>();
        m_bookSkus.Append(winrt::make<Bookstore::implementation::BookSku>(L"To Kill A Mockingbird"));
    }
    
    // This is really an observable vector of BookSku.
	Windows::Foundation::Collections::IObservableVector<Windows::Foundation::IInspectable> BookSkus();
    {
        return m_bookSkus;
    }

private:
    Windows::Foundation::Collections::IObservableVector<Windows::Foundation::IInspectable> m_bookSkus;
};
...
```

If you need to access objects in *m_bookSkus*, then you'll need to QI them back to **Bookstore::BookSku**.

```cppwinrt
Widget MyPage::BookstoreViewModel(winrt::hstring title)
{
    for (auto&& obj : m_bookSkus)
    {
        auto bookSku = obj.as<Bookstore::BookSku>();
        if (bookSku.Title() == title) return bookSku;
    }
    return nullptr;
}
```

## ToString()

C# types provide the [Object.ToString](/dotnet/api/system.object.tostring) method.

```csharp
int i = 2;
var s = i.ToString(); // s is a System.String with value "2".
```

C++/WinRT doesn't directly provide this facility, but you can turn to alternatives.

```cppwinrt
int i{ 2 };
auto s{ std::to_wstring(i) }; // s is a std::wstring with value L"2".
```

C++/WinRT also supports [**winrt::to_hstring**](/uwp/cpp-ref-for-winrt/to-hstring) for a limited number of types. You'll need to add overloads for any additional types you want to stringify.

| Language | Stringify int | Stringify enum |
| - | - | - |
| C# | `string result = "hello, " + intValue.ToString();`<br>`string result = $"hello, {intValue}";` | `string result = "status: " + status.ToString();`<br>`string result = $"status: {status}";` |
| C++/WinRT | `hstring result = L"hello, " + to_hstring(intValue);` | `// must define overload (see below)`<br>`hstring result = L"status: " + to_hstring(status);` |

In the case of stringifying an enum, you will need to provide the implementation of **winrt::to_hstring**.

```cppwinrt
namespace winrt
{
    hstring to_hstring(StatusEnum status)
    {
        switch (status)
        {
        case StatusEnum::Success: return L"Success";
        case StatusEnum::AccessDenied: return L"AccessDenied";
        case StatusEnum::DisabledByPolicy: return L"DisabledByPolicy";
        default: return to_hstring(static_cast<int>(status));
        }
    }
}
```

These stringifications are often consumed implicitly by data binding.

```xaml
<TextBlock>
You have <Run Text="{Binding FlowerCount}"/> flowers.
</TextBlock>
<TextBlock>
Most recent status is <Run Text="{x:Bind LatestOperation.Status}"/>.
</TextBlock>
```

These bindings will perform **winrt::to_hstring** of the bound property. In the case of the second example (the **StatusEnum**), you must provide your own overload of **winrt::to_hstring**, otherwise you'll get a compiler error.

## String-building

For string building, C# has a built-in [**StringBuilder**](/dotnet/api/system.text.stringbuilder) type.

| | C# | C++/WinRT |
|-|-|-|
| Builder class | `StringBuilder builder;` | `std::wstringstream stream;` |
| Append string, preserving nulls | `builder.Append(s);` | `stream << std::wstring_view{ s };` |
| Extract result | `s = builder.ToString();` | `ws = stream.str();` |

## Boxing and unboxing

C# automatically boxes scalars into objects. C++/WinRT requires you to call the [**winrt::box_value**](/uwp/cpp-ref-for-winrt/box-value) function explicitly. Both languages require you to unbox explicitly. See [Boxing and unboxing with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/boxing).

In the tables that follows, we'll use these definitions.

| C# | C++/WinRT|
|-|-|
| `int i;` | `int i;` |
| `string s;` | `winrt::hstring s;` |
| `object o;` | `IInspectable o;`|

| Operation | C# | C++/WinRT|
|-|-|-|
| Boxing | `o = 1;`<br>`o = "string";` | `o = box_value(1);`<br>`o = box_value(L"string");` |
| Unboxing | `i = (int)o;`<br>`s = (string)o;` | `i = unbox_value<int>(o);`<br>`s = unbox_value<winrt::hstring>(o);` |

C++/CX and C# raise exceptions if you try to unbox a null pointer to a value type. C++/WinRT considers this a programming error, and it crashes. In C++/WinRT, use the [**winrt::unbox_value_or**](/uwp/cpp-ref-for-winrt/unbox-value-or) function if you want to handle the case where the object is not of the type that you thought it was.

| Scenario | C# | C++/WinRT|
|-|-|-|
| Unbox a known integer |`i = (int)o;` | `i = unbox_value<int>(o);` |
| If o is null | `System.NullReferenceException` | Crash |
| If o is not a boxed int | `System.InvalidCastException` | Crash |
| Unbox int, use fallback if null; crash if anything else | `i = o != null ? (int)o : fallback;` | `i = o ? unbox_value<int>(o) : fallback;` |
| Unbox int if possible; use fallback for anything else | `i = as int? ?? fallback;` | `i = unbox_value_or<int>(o, fallback);` |

### Boxing and unboxing a string

A string is in some ways a value type, and in other ways a reference type. C# and C++/WinRT treat strings differently.

The ABI type [**HSTRING**](/windows/win32/winrt/hstring) is a pointer to a reference-counted string. But it doesn't derive from [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable), so it's not technically an *object*. Furthermore, a null **HSTRING** represents the empty string. Boxing of things not derived from **IInspectable** is done by wrapping them inside an [**IReference\<T\>**](/uwp/api/windows.foundation.ireference_t_), and the Windows Runtime provides a standard implementation in the form of the [**PropertyValue**](/uwp/api/windows.foundation.propertyvalue) object (custom types are reported as [**PropertyType::OtherType**](/uwp/api/windows.foundation.propertytype)).

C# represents a Windows Runtime string as a reference type; while C++/WinRT projects a string as a value type. This means that a boxed null string can have different representations depending how you got there.

| Behavior | C# | C++/WinRT|
|-|-|-|
| Declarations | `object o;`<br>`string s;` | `IInspectable o;`<br>`hstring s;` |
| String type category | Reference type | Value type |
| null **HSTRING** projects as | `""` | `hstring{}` |
| Are null and `""` identical? | No | Yes |
| Validity of null | `s = null;`<br>`s.Length` raises NullReferenceException | `s = hstring{};`<br>`s.size() == 0` (valid) |
| If you assign null string to object | `o = (string)null;`<br>`o == null` | `o = box_value(hstring{});`<br>`o != nullptr` |
| If you assign `""` to object | `o = "";`<br>`o != null` | `o = box_value(hstring{L""});`<br>`o != nullptr` |

Basic boxing and unboxing.

| Operation | C# | C++/WinRT|
|-|-|-|
| Box a string | `o = s;`<br>Empty string becomes non-null object. | `o = box_value(s);`<br>Empty string becomes non-null object. |
| Unbox a known string | `s = (string)o;`<br>Null object becomes null string.<br>InvalidCastException if not a string. | `s = unbox_value<hstring>(o);`<br>Null object crashes.<br>Crash if not a string. |
| Unbox a possible string | `s = o as string;`<br>Null object or non-string becomes null string.<br><br>OR<br><br>`s = o as string ?? fallback;`<br>Null or non-string becomes fallback.<br>Empty string preserved. | `s = unbox_value_or<hstring>(o, fallback);`<br>Null or non-string becomes fallback.<br>Empty string preserved. |

## Derived classes

In order to derive from a runtime class, the base class must be *composable*. C# doesn't require that you take any special steps to make your classes composable, but C++/WinRT does. You use the [unsealed keyword](/uwp/midl-3/intro#base-classes) to indicate that you want your class to be usable as a base class.

```idl
unsealed runtimeclass BasePage : Windows.UI.Xaml.Controls.Page
{
    ...
}
runtimeclass DerivedPage : BasePage
{
    ...
}
```

In your implementation header class, you must include the base class header file before you include the autogenerated header for the derived class. Otherwise you'll get errors such as "Illegal use of this type as an expression".

```cppwinrt
// DerivedPage.h
#include "BasePage.h"       // This comes first.
#include "DerivedPage.g.h"  // Otherwise this header file will produce an error.

namespace winrt::MyNamespace::implementation
{
    struct DerivedPage : DerivedPageT<DerivedPage>
    {
        ...
    }
}
```

## Consuming objects from XAML markup

In a C# project, you can consume private members and named elements from XAML markup. But in C++/WinRT, all entities consumed by using the XAML [**{x:Bind} markup extension**](/windows/uwp/xaml-platform/x-bind-markup-extension) must be exposed publicly in IDL.

Also, binding to a Boolean displays `true` or `false` in C#, but it shows **Windows.Foundation.IReference`1\<Boolean\>** in C++/WinRT.

For more info, and code examples, see [Consuming objects from markup](/windows/uwp/cpp-and-winrt-apis/binding-property#consuming-objects-from-xaml-markup).

## Important APIs
* [winrt::single_threaded_observable_vector function template](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector)
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C# tutorials](/visualstudio/get-started/csharp)
* [Author APIs with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/author-apis)
* [Data binding in depth](/windows/uwp/data-binding/data-binding-in-depth)
* [XAML items controls; bind to a C++/WinRT collection](/windows/uwp/cpp-and-winrt-apis/binding-collection)