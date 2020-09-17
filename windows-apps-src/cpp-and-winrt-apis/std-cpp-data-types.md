---
description: With C++/WinRT, you can call Windows Runtime APIs using Standard C++ data types.
title: Standard C++ data types and C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, data, types
ms.localizationpriority: medium
---

# Standard C++ data types and C++/WinRT

With [C++/WinRT](./intro-to-using-cpp-with-winrt.md), you can call Windows Runtime APIs using Standard C++ data types, including some C++ Standard Library data types. You can pass standard strings to APIs (see [String handling in C++/WinRT](strings.md)), and you can pass initializer lists and standard containers to APIs that expect a semantically equivalent collection.

Also see [Passing parameters into the ABI boundary](./pass-parms-to-abi.md).

## Standard initializer lists
An initializer list (**std::initializer_list**) is a C++ Standard Library construct. You can use initializer lists when you call certain Windows Runtime constructors and methods. For example, you can call [**DataWriter::WriteBytes**](/uwp/api/windows.storage.streams.datawriter.writebytes) with one.

```cppwinrt
#include <winrt/Windows.Storage.Streams.h>

using namespace winrt::Windows::Storage::Streams;

int main()
{
    winrt::init_apartment();

    InMemoryRandomAccessStream stream;
    DataWriter dataWriter{stream};
    dataWriter.WriteBytes({ 99, 98, 97 }); // the initializer list is converted to a winrt::array_view before being passed to WriteBytes.
}
```

There are two pieces involved in making this work. First, the **DataWriter::WriteBytes** method takes a parameter of type [**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view).

```cppwinrt
void WriteBytes(winrt::array_view<uint8_t const> value) const
```

**winrt::array_view** is a custom C++/WinRT type that safely represents a contiguous series of values (it is defined in the C++/WinRT base library, which is `%WindowsSdkDir%Include\<WindowsTargetPlatformVersion>\cppwinrt\winrt\base.h`).

Second, **winrt::array_view** has an initializer-list constructor.

```cppwinrt
template <typename T> winrt::array_view(std::initializer_list<T> value) noexcept
```

In many cases, you can choose whether or not to be aware of **winrt::array_view** in your programming. If you choose *not* to be aware of it then you won't have any code to change if and when an equivalent type appears in the C++ Standard Library.

You can pass an initializer list to a Windows Runtime API that expects a collection parameter. Take **StorageItemContentProperties::RetrievePropertiesAsync** for example.

```cppwinrt
IAsyncOperation<IMap<winrt::hstring, IInspectable>> StorageItemContentProperties::RetrievePropertiesAsync(IIterable<winrt::hstring> propertiesToRetrieve) const;
```

You can call that API with an initializer list like this.

```cppwinrt
IAsyncAction retrieve_properties_async(StorageFile const& storageFile)
{
    auto properties{ co_await storageFile.Properties().RetrievePropertiesAsync({ L"System.ItemUrl" }) };
}
```

Two factors are at work here. First, the callee constructs a **std::vector** from the initializer list (this callee is asynchronous, so it's able to own that object, which it must). Second, C++/WinRT transparently (and without introducing copies) binds **std::vector** as a Windows Runtime collection parameter.

## Standard arrays and vectors
[**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view) also has conversion constructors from **std::vector** and **std::array**.

```cppwinrt
template <typename C, size_type N> winrt::array_view(std::array<C, N>& value) noexcept
template <typename C> winrt::array_view(std::vector<C>& vectorValue) noexcept
```

So, you could instead call **DataWriter::WriteBytes** with a **std::vector**.

```cppwinrt
std::vector<byte> theVector{ 99, 98, 97 };
dataWriter.WriteBytes(theVector); // theVector is converted to a winrt::array_view before being passed to WriteBytes.
```

Or with a **std::array**.

```cppwinrt
std::array<byte, 3> theArray{ 99, 98, 97 };
dataWriter.WriteBytes(theArray); // theArray is converted to a winrt::array_view before being passed to WriteBytes.
```

C++/WinRT binds **std::vector** as a Windows Runtime collection parameter. So, you can pass a **std::vector&lt;winrt::hstring&gt;**, and it will be converted to the appropriate Windows Runtime collection of **winrt::hstring**. There's an extra detail to bear in mind if the callee is asynchronous. Due to the implementation details of that case, you'll need to provide an rvalue, so you must provide a copy or a move of the vector. In the code example below, we move ownership of the vector to the object of the parameter type accepted by the async callee (and then we're careful not to access `vecH` again after moving it). If you want to know more about rvalues, see [Value categories, and references to them](cpp-value-categories.md).

```cppwinrt
IAsyncAction retrieve_properties_async(StorageFile const storageFile, std::vector<winrt::hstring> vecH)
{
	auto properties{ co_await storageFile.Properties().RetrievePropertiesAsync(std::move(vecH)) };
}
```

But you can't pass a **std::vector&lt;std::wstring&gt;** where a Windows Runtime collection is expected. This is because, having converted to the appropriate Windows Runtime collection of **std::wstring**, the C++ language won't then coerce that collection's type parameter(s). Consequently, the following code example won't compile (and the solution is to pass a **std::vector&lt;winrt::hstring&gt;** instead, as shown above).

```cppwinrt
IAsyncAction retrieve_properties_async(StorageFile const& storageFile, std::vector<std::wstring> const& vecW)
{
    auto properties{ co_await storageFile.Properties().RetrievePropertiesAsync(std::move(vecW)) }; // error! Can't convert from vector of wstring to async_iterable of hstring.
}
```

## Raw arrays, and pointer ranges
Bearing in mind the caveat that an equivalent type may exist in the future in the C++ Standard Library, you can also work directly with **winrt::array_view** if you choose to, or need to.

**winrt::array_view** has conversion constructors from a raw array, and from a range of **T&ast;** (pointers to the element type).

```cppwinrt
using namespace winrt;
...
byte theRawArray[]{ 99, 98, 97 };
array_view<byte const> fromRawArray{ theRawArray };
dataWriter.WriteBytes(fromRawArray); // the winrt::array_view is passed to WriteBytes.

array_view<byte const> fromRange{ theArray.data(), theArray.data() + 2 }; // just the first two elements.
dataWriter.WriteBytes(fromRange); // the winrt::array_view is passed to WriteBytes.
```

## winrt::array_view functions and operators
A host of constructors, operators, functions, and iterators are implemented for **winrt::array_view**. A **winrt::array_view** is a range, so you can use it with range-based `for`, or with **std::for_each**.

For more examples and info, see the [**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view) API reference topic.

## **IVector&lt;T&gt;** and standard iteration constructs
[**SyndicationFeed.Items**](/uwp/api/windows.web.syndication.syndicationfeed.items) is an example of a Windows Runtime API that returns a collection of type [**IVector&lt;T&gt;**](/uwp/api/windows.foundation.collections.ivector_t_) (projected into C++/WinRT as **winrt::Windows::Foundation::Collections::IVector&lt;T&gt;**). You can use this type with standard iteration constructs, such as range-based `for`.

```cppwinrt
// main.cpp
#include "pch.h"
#include <winrt/Windows.Web.Syndication.h>
#include <iostream>

using namespace winrt;
using namespace Windows::Web::Syndication;

void PrintFeed(SyndicationFeed const& syndicationFeed)
{
    for (SyndicationItem const& syndicationItem : syndicationFeed.Items())
    {
        std::wcout << syndicationItem.Title().Text().c_str() << std::endl;
    }
}
```

## C++ coroutines with asynchronous Windows Runtime APIs
You can continue to use the [Parallel Patterns Library (PPL)](/cpp/parallel/concrt/parallel-patterns-library-ppl) when calling asynchronous Windows Runtime APIs. However, in many cases, C++ coroutines provide an efficient and more easily-coded idiom for interacting with asynchronous objects. For more info, and code examples, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md).

## Important APIs
* [IVector&lt;T&gt; interface](/uwp/api/windows.foundation.collections.ivector_t_)
* [winrt::array_view struct template](/uwp/cpp-ref-for-winrt/array-view)

## Related topics
* [String handling in C++/WinRT](strings.md)