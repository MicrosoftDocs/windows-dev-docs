---
description: A collection that can be effectively bound to a XAML items control is known as an *observable* collection. This topic shows how to implement and consume an observable collection, and how to bind a XAML items control to it.
title: XAML items controls; bind to a C++/WinRT collection
ms.date: 04/24/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, control, binding, collection
ms.localizationpriority: medium
---

# XAML items controls; bind to a C++/WinRT collection

A collection that can be effectively bound to a XAML items control is known as an *observable* collection. This idea is based on the software design pattern known as the *observer pattern*. This topic shows how to implement observable collections in [C++/WinRT](./intro-to-using-cpp-with-winrt.md), and how to bind XAML items controls to them (for background info, see [Data binding](../data-binding/index.md)).

If you want to follow along with this topic, then we recommend that you first create the project that's described in [XAML controls; bind to a C++/WinRT property](binding-property.md). This topic adds more code to that project, and it adds to the concepts explained in that topic.

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## What does *observable* mean for a collection?

If a runtime class that represents a collection chooses to raise the [**IObservableVector&lt;T&gt;::VectorChanged**](/uwp/api/windows.foundation.collections.iobservablevector-1.vectorchanged) event whenever an element is added to it or removed from it, then the runtime class is an observable collection. A XAML items control can bind to, and handle, these events by retrieving the updated collection and then updating itself to show the current elements.

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Add a **BookSkus** collection to **BookstoreViewModel**

In [XAML controls; bind to a C++/WinRT property](binding-property.md), we added a property of type **BookSku** to our main view model. In this step, we'll use the [**winrt::single_threaded_observable_vector**](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector) factory function template to help us implement an observable collection of **BookSku** on the same view model.

Declare a new property in `BookstoreViewModel.idl`.

```idl
// BookstoreViewModel.idl
...
runtimeclass BookstoreViewModel
{
    BookSku BookSku{ get; };
    Windows.Foundation.Collections.IObservableVector<BookSku> BookSkus{ get; };
}
...
```

> [!NOTE]
> In the MIDL 3.0 listing above, note that the type of the **BookSkus** property is [**IObservableVector**](/uwp/api/windows.foundation.collections.ivector_t_) of **BookSku**. In the next section of this topic, we'll be binding the items source of a [**ListBox**](/uwp/api/windows.ui.xaml.controls.listbox) to **BookSkus**. A list box is an items control, and to correctly set the [**ItemsControl.ItemsSource**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource) property, you need to set it to a value of type **IObservableVector**, or **IVector**, or of an interoperability type such as [**IBindableObservableVector**](/uwp/api/windows.ui.xaml.interop.ibindableobservablevector).

> [!WARNING]
> The code shown in this topic applies to C++/WinRT version 2.0.190530.8 and higher. If you're using an earlier version, then you'll need to make some minor tweaks to the code shown. In the MIDL 3.0 listing above, change the **BookSkus** property to [**IObservableVector**](/uwp/api/windows.foundation.collections.ivector_t_) of [**IInspectable**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable). And then use **IInspectable** (instead of **BookSku**) in your implementation, too.

Save and build. Copy the accessor stubs from `BookstoreViewModel.h` and `BookstoreViewModel.cpp` in the `\Bookstore\Bookstore\Generated Files\sources` folder (for more details, see the previous topic, [XAML controls; bind to a C++/WinRT property](binding-property.md)). Implement those accessor stubs like this.

```cppwinrt
// BookstoreViewModel.h
...
struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel>
{
    BookstoreViewModel();

    Bookstore::BookSku BookSku();

    Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> BookSkus();

private:
    Bookstore::BookSku m_bookSku{ nullptr };
    Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> m_bookSkus;
};
...
```

```cppwinrt
// BookstoreViewModel.cpp
...
BookstoreViewModel::BookstoreViewModel()
{
    m_bookSku = winrt::make<Bookstore::implementation::BookSku>(L"Atticus");
    m_bookSkus = winrt::single_threaded_observable_vector<Bookstore::BookSku>();
    m_bookSkus.Append(m_bookSku);
}

Bookstore::BookSku BookstoreViewModel::BookSku()
{
    return m_bookSku;
}

Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> BookstoreViewModel::BookSkus()
{
    return m_bookSkus;
}
...
```

## Bind a ListBox to the **BookSkus** property

Open `MainPage.xaml`, which contains the XAML markup for our main UI page. Add the following markup inside the same **StackPanel** as the **Button**.

```xaml
<ListBox ItemsSource="{x:Bind MainViewModel.BookSkus}">
    <ItemsControl.ItemTemplate>
        <DataTemplate x:DataType="local:BookSku">
            <TextBlock Text="{x:Bind Title, Mode=OneWay}"/>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ListBox>
```

In `MainPage.cpp`, add a line of code to the **Click** event handler to append a book to the collection.

```cppwinrt
// MainPage.cpp
...
void MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
{
    MainViewModel().BookSku().Title(L"To Kill a Mockingbird");
    MainViewModel().BookSkus().Append(winrt::make<Bookstore::implementation::BookSku>(L"Moby Dick"));
}
...
```

Now build and run the project. Click the button to execute the **Click** event handler. We saw that the implementation of **Append** raises an event to let the UI know that the collection has changed; and the **ListBox** re-queries the collection to update its own **Items** value. Just as before, the title of one of the books changes; and that title change is reflected both on the button and in the list box.

## Important APIs

* [IObservableVector&lt;T&gt;::VectorChanged](/uwp/api/windows.foundation.collections.iobservablevector-1.vectorchanged)
* [winrt::make function template](/uwp/cpp-ref-for-winrt/make)

## Related topics

* [Consume APIs with C++/WinRT](consume-apis.md)
* [Author APIs with C++/WinRT](author-apis.md)