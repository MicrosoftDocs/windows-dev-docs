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

You can author your **Midl file (.idl)** like this.

```idl
// BookSku.idl
namespace Bookstore
{
    runtimeclass BookSku { ... }
}

// BookstoreViewModel.idl
import "BookSku.idl";
...
runtimeclass BookstoreViewModel
{
    Windows.Foundation.Collections.IObservableVector<BookSku> BookSkus{ get; };
}
...

// MainPage.idl
import "BookstoreViewModel.idl";
...
runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
{
    MainPage();
    BookstoreViewModel MainViewModel{ get; };
}
...
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
// BookSku.idl
namespace Bookstore
{
    runtimeclass BookSku { ... }
}

// BookstoreViewModel.idl
import "BookSku.idl";
...
runtimeclass BookstoreViewModel
{
    // This is really an observable vector of BookSku.
    Windows.Foundation.Collections.IObservableVector<Object> BookSkus{ get; };
}
...

// MainPage.idl
import "BookstoreViewModel.idl";
...
runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
{
    MainPage();
    BookstoreViewModel MainViewModel{ get; };
}
...
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

## Important APIs
* [winrt::single_threaded_observable_vector function template](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector)
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C# tutorials](/visualstudio/get-started/csharp)
* [Author APIs with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/author-apis)
* [Data binding in depth](/windows/uwp/data-binding/data-binding-in-depth)
* [XAML items controls; bind to a C++/WinRT collection](/windows/uwp/cpp-and-winrt-apis/binding-collection)