---
description: C++/WinRT provides functions and base classes that save you a lot of time and effort when you want to implement and/or pass collections.
title: Collections with C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, collection
ms.localizationpriority: medium
ms.custom: RS5
---

# Collections with C++/WinRT

Internally, a Windows Runtime collection has a lot of complicated moving parts. But when you want to pass a collection object to a Windows Runtime function, or to implement your own collection properties and collection types, there are functions and base classes in [C++/WinRT](./intro-to-using-cpp-with-winrt.md) to support you. These features take the complexity out of your hands, and save you a lot of overhead in time and effort.

[**IVector**](/uwp/api/windows.foundation.collections.ivector_t_) is the Windows Runtime interface implemented by any random-access collection of elements. If you were to implement **IVector** yourself, you'd also need to implement [**IIterable**](/uwp/api/windows.foundation.collections.iiterable_t_), [**IVectorView**](/uwp/api/windows.foundation.collections.ivectorview_t_), and [**IIterator**](/uwp/api/windows.foundation.collections.iiterator_t_). Even if you *need* a custom collection type, that's a lot of work. But if you have data in a **std::vector** (or a **std::map**, or a **std::unordered_map**) and all you want to do is pass that to a Windows Runtime API, then you'd want to avoid doing that level of work, if possible. And avoiding it *is* possible, because C++/WinRT helps you to create collections efficiently and with little effort.

Also see [XAML items controls; bind to a C++/WinRT collection](binding-collection.md).

## Helper functions for collections

### General-purpose collection, empty

This section covers the scenario where you wish to create a collection that's initially empty; and then populate it *after* creation.

To retrieve a new object of a type that implements a general-purpose collection, you can call the [**winrt::single_threaded_vector**](/uwp/cpp-ref-for-winrt/single-threaded-vector) function template. The object is returned as an [**IVector**](/uwp/api/windows.foundation.collections.ivector_t_), and that's the interface via which you call the returned object's functions and properties.

If you want to copy-paste the following code examples directly into the main source code file of a **Windows Console Application (C++/WinRT)** project, then first set **Not Using Precompiled Headers** in project properties.

```cppwinrt
// main.cpp
#include <winrt/Windows.Foundation.Collections.h>
#include <iostream>
using namespace winrt;

int main()
{
    winrt::init_apartment();

    Windows::Foundation::Collections::IVector<int> coll{ winrt::single_threaded_vector<int>() };
    coll.Append(1);
    coll.Append(2);
    coll.Append(3);

    for (auto const& el : coll)
    {
        std::cout << el << std::endl;
    }

    Windows::Foundation::Collections::IVectorView<int> view{ coll.GetView() };
}
```

As you can see in the code example above, after creating the collection you can append elements, iterate over them, and generally treat the object as you would any Windows Runtime collection object that you might have received from an API. If you need an immutable view over the collection, then you can call [**IVector::GetView**](/uwp/api/windows.foundation.collections.ivector-1.getview), as shown. The pattern shown above&mdash;of creating and consuming a collection&mdash;is appropriate for simple scenarios where you want to pass data into, or get data out of, an API. You can pass an **IVector**, or an **IVectorView**, anywhere an [**IIterable**](/uwp/api/windows.foundation.collections.iiterable_t_) is expected.

In the code example above, the call to **winrt::init_apartment** initializes the thread in the Windows Runtime; by default, in a multithreaded apartment. The call also initializes COM.

### General-purpose collection, primed from data

This section covers the scenario where you wish to create a collection and populate it at the same time.

You can avoid the overhead of the calls to **Append** in the previous code example. You may already have the source data, or you may prefer to populate the source data in advance of creating the Windows Runtime collection object. Here's how to do that.

```cppwinrt
auto coll1{ winrt::single_threaded_vector<int>({ 1,2,3 }) };

std::vector<int> values{ 1,2,3 };
auto coll2{ winrt::single_threaded_vector<int>(std::move(values)) };

for (auto const& el : coll2)
{
    std::cout << el << std::endl;
}
```

You can pass a temporary object containing your data to **winrt::single_threaded_vector**, as with `coll1`, above. Or you can move a **std::vector** (assuming you won't be accessing it again) into the function. In both cases, you're passing an *rvalue* into the function. That enables the compiler to be efficient and to avoid copying the data. If you want to know more about *rvalues*, see [Value categories, and references to them](cpp-value-categories.md).

If you want to bind a XAML items control to your collection, then you can. But be aware that to correctly set the [**ItemsControl.ItemsSource**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource) property, you need to set it to a value of type **IVector** of **IInspectable** (or of an interoperability type such as [**IBindableObservableVector**](/uwp/api/windows.ui.xaml.interop.ibindableobservablevector)).

Here's a code example that produces a collection of a type suitable for binding, and appends an element to it. You can find the context for this code example in [XAML items controls; bind to a C++/WinRT collection](binding-collection.md).

```cppwinrt
auto bookSkus{ winrt::single_threaded_vector<Windows::Foundation::IInspectable>() };
bookSkus.Append(winrt::make<Bookstore::implementation::BookSku>(L"Moby Dick"));
```

You can create a Windows Runtime collection from data, and get a view on it ready to pass to an API, all without copying anything.

```cppwinrt
std::vector<float> values{ 0.1f, 0.2f, 0.3f };
Windows::Foundation::Collections::IVectorView<float> view{ winrt::single_threaded_vector(std::move(values)).GetView() };
```

In the examples above, the collection we create *can* be bound to a XAML items control; but the collection isn't observable.

### Observable collection

To retrieve a new object of a type that implements an *observable* collection, call the [**winrt::single_threaded_observable_vector**](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector) function template with any element type. But to make an observable collection suitable for binding to a XAML items control, use **IInspectable** as the element type.

The object is returned as an [**IObservableVector**](/uwp/api/windows.foundation.collections.iobservablevector_t_), and that's the interface via which you (or the control to which it's bound) call the returned object's functions and properties.

```cppwinrt
auto bookSkus{ winrt::single_threaded_observable_vector<Windows::Foundation::IInspectable>() };
```

For more details, and code examples, about binding your user interface (UI) controls to an observable collection, see [XAML items controls; bind to a C++/WinRT collection](binding-collection.md).

### Associative collection (map)

There are associative collection versions of the two functions that we've looked at.

- The [**winrt::single_threaded_map**](/uwp/cpp-ref-for-winrt/single-threaded-map) function template returns a non-observable associative collection as an [**IMap**](/uwp/api/windows.foundation.collections.imap_k_v_).
- The [**winrt::single_threaded_observable_map**](/uwp/cpp-ref-for-winrt/single-threaded-observable-map) function template returns an observable associative collection as an [**IObservableMap**](/uwp/api/windows.foundation.collections.iobservablemap_k_v_).

You can optionally prime these collections with data by passing to the function an *rvalue* of type **std::map** or **std::unordered_map**.

```cppwinrt
auto coll1{
    winrt::single_threaded_map<winrt::hstring, int>(std::map<winrt::hstring, int>{
        { L"AliceBlue", 0xfff0f8ff }, { L"AntiqueWhite", 0xfffaebd7 }
    })
};

std::map<winrt::hstring, int> values{
    { L"AliceBlue", 0xfff0f8ff }, { L"AntiqueWhite", 0xfffaebd7 }
};
auto coll2{ winrt::single_threaded_map<winrt::hstring, int>(std::move(values)) };
```

### Single-threaded

The "single-threaded" in the names of these functions indicates that they don't provide any concurrency&mdash;in other words, they're not thread-safe. The mention of threads is unrelated to apartments, because the objects returned from these functions are all agile (see [Agile objects in C++/WinRT](agile-objects.md)). It's just that the objects are single-threaded. And that's entirely appropriate if you just want to pass data one way or the other across the application binary interface (ABI).

## Base classes for collections

If, for complete flexibility, you want to implement your own custom collection, then you'll want to avoid doing that the hard way. For example, this is what a custom vector view would look like *without the assistance of C++/WinRT's base classes*.

```cppwinrt
...
using namespace winrt;
using namespace Windows::Foundation::Collections;
...
struct MyVectorView :
    implements<MyVectorView, IVectorView<float>, IIterable<float>>
{
    // IVectorView
    float GetAt(uint32_t const) { ... };
    uint32_t GetMany(uint32_t, winrt::array_view<float>) const { ... };
    bool IndexOf(float, uint32_t&) { ... };
    uint32_t Size() { ... };

    // IIterable
    IIterator<float> First() const { ... };
};
...
IVectorView<float> view{ winrt::make<MyVectorView>() };
```

Instead, it's much easier to derive your custom vector view from the [**winrt::vector_view_base**](/uwp/cpp-ref-for-winrt/vector-view-base) struct template, and just implement the **get_container** function to expose the container holding your data.

```cppwinrt
struct MyVectorView2 :
    implements<MyVectorView2, IVectorView<float>, IIterable<float>>,
    winrt::vector_view_base<MyVectorView2, float>
{
    auto& get_container() const noexcept
    {
        return m_values;
    }

private:
    std::vector<float> m_values{ 0.1f, 0.2f, 0.3f };
};
```

The container returned by **get_container** must provide the **begin** and **end** interface that **winrt::vector_view_base** expects. As shown in the example above, **std::vector** provides that. But you can return any container that fulfils the same contract, including your own custom container.

```cppwinrt
struct MyVectorView3 :
    implements<MyVectorView3, IVectorView<float>, IIterable<float>>,
    winrt::vector_view_base<MyVectorView3, float>
{
    auto get_container() const noexcept
    {
        struct container
        {
            float const* const first;
            float const* const last;

            auto begin() const noexcept
            {
                return first;
            }

            auto end() const noexcept
            {
                return last;
            }
        };

        return container{ m_values.data(), m_values.data() + m_values.size() };
    }

private:
    std::array<float, 3> m_values{ 0.2f, 0.3f, 0.4f };
};
```

These are the base classes that C++/WinRT provides to help you implement custom collections.

### [winrt::vector_view_base](/uwp/cpp-ref-for-winrt/vector-view-base)

See the code examples above.

### [winrt::vector_base](/uwp/cpp-ref-for-winrt/vector-base)

```cppwinrt
struct MyVector :
    implements<MyVector, IVector<float>, IVectorView<float>, IIterable<float>>,
    winrt::vector_base<MyVector, float>
{
    auto& get_container() const noexcept
    {
        return m_values;
    }

    auto& get_container() noexcept
    {
        return m_values;
    }

private:
    std::vector<float> m_values{ 0.1f, 0.2f, 0.3f };
};
```

### [winrt::observable_vector_base](/uwp/cpp-ref-for-winrt/observable-vector-base)

```cppwinrt
struct MyObservableVector :
    implements<MyObservableVector, IObservableVector<float>, IVector<float>, IVectorView<float>, IIterable<float>>,
    winrt::observable_vector_base<MyObservableVector, float>
{
    auto& get_container() const noexcept
    {
        return m_values;
    }

    auto& get_container() noexcept
    {
        return m_values;
    }

private:
    std::vector<float> m_values{ 0.1f, 0.2f, 0.3f };
};
```

### [winrt::map_view_base](/uwp/cpp-ref-for-winrt/map-view-base)

```cppwinrt
struct MyMapView :
    implements<MyMapView, IMapView<winrt::hstring, int>, IIterable<IKeyValuePair<winrt::hstring, int>>>,
    winrt::map_view_base<MyMapView, winrt::hstring, int>
{
    auto& get_container() const noexcept
    {
        return m_values;
    }

private:
    std::map<winrt::hstring, int> m_values{
        { L"AliceBlue", 0xfff0f8ff }, { L"AntiqueWhite", 0xfffaebd7 }
    };
};
```

### [winrt::map_base](/uwp/cpp-ref-for-winrt/map-base)

```cppwinrt
struct MyMap :
    implements<MyMap, IMap<winrt::hstring, int>, IMapView<winrt::hstring, int>, IIterable<IKeyValuePair<winrt::hstring, int>>>,
    winrt::map_base<MyMap, winrt::hstring, int>
{
    auto& get_container() const noexcept
    {
        return m_values;
    }

    auto& get_container() noexcept
    {
        return m_values;
    }

private:
    std::map<winrt::hstring, int> m_values{
        { L"AliceBlue", 0xfff0f8ff }, { L"AntiqueWhite", 0xfffaebd7 }
    };
};
```

### [winrt::observable_map_base](/uwp/cpp-ref-for-winrt/observable-map-base)

```cppwinrt
struct MyObservableMap :
    implements<MyObservableMap, IObservableMap<winrt::hstring, int>, IMap<winrt::hstring, int>, IMapView<winrt::hstring, int>, IIterable<IKeyValuePair<winrt::hstring, int>>>,
    winrt::observable_map_base<MyObservableMap, winrt::hstring, int>
{
    auto& get_container() const noexcept
    {
        return m_values;
    }

    auto& get_container() noexcept
    {
        return m_values;
    }

private:
    std::map<winrt::hstring, int> m_values{
        { L"AliceBlue", 0xfff0f8ff }, { L"AntiqueWhite", 0xfffaebd7 }
    };
};
```

## Important APIs
* [ItemsControl.ItemsSource property](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource)
* [IObservableVector interface](/uwp/api/windows.foundation.collections.iobservablevector_t_)
* [IVector interface](/uwp/api/windows.foundation.collections.ivector_t_)
* [winrt::map_base struct template](/uwp/cpp-ref-for-winrt/map-base)
* [winrt::map_view_base struct template](/uwp/cpp-ref-for-winrt/map-view-base)
* [winrt::observable_map_base struct template](/uwp/cpp-ref-for-winrt/observable-map-base)
* [winrt::observable_vector_base struct template](/uwp/cpp-ref-for-winrt/observable-vector-base)
* [winrt::single_threaded_observable_map function template](/uwp/cpp-ref-for-winrt/single-threaded-observable-map)
* [winrt::single_threaded_map function template](/uwp/cpp-ref-for-winrt/single-threaded-map)
* [winrt::single_threaded_observable_vector function template](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector)
* [winrt::single_threaded_vector function template](/uwp/cpp-ref-for-winrt/single-threaded-vector)
* [winrt::vector_base struct template](/uwp/cpp-ref-for-winrt/vector-base)
* [winrt::vector_view_base struct template](/uwp/cpp-ref-for-winrt/vector-view-base)

## Related topics
* [Value categories, and references to them](cpp-value-categories.md)
* [XAML items controls; bind to a C++/WinRT collection](binding-collection.md)