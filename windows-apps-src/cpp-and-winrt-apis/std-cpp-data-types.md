---
author: stevewhims
description: With C++/WinRT, you can call WinRT APIs using Standard C++ data types.
title: Standard C++ data types and C++/WinRT
ms.author: stwhi
ms.date: 03/01/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, data, types
ms.localizationpriority: medium
---

# Standard C++ data types and C++/WinRT
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

With C++/WinRT, you can call WinRT APIs using Standard C++ data types, including some Standard Template Library (STL) data types.

## Standard initializer lists
An initializer list (`std::initializer_list`) is a standard library construct. You can use initializer lists when you call certain WinRT constructors and methods. For example, you can call [**DataWriter::WriteBytes**](/uwp/api/windows.storage.streams.datawriter?branch=live#Windows_Storage_Streams_DataWriter_WriteBytes_System_Byte___) with one.

```cppwinrt
#include "winrt/Windows.Storage.Streams.h"

using namespace winrt::Windows::Storage::Streams;

int main()
{
	init_apartment();

	InMemoryRandomAccessStream stream;
	DataWriter dataWriter{stream};
	dataWriter.WriteBytes({ 99, 98, 97 }); // the initializer list is converted to an array_view before being passed to WriteBytes.
}
```

There are two pieces involved in making this work. First, the **DataWriter::WriteBytes** method takes a parameter of type [**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view?branch=live).

```cppwinrt
void WriteBytes(array_view<uint8_t const> value) const
```

 **array_view** is a custom C++/WinRT type that safely represents a contiguous series of values (it is defined in `%ProgramFiles(x86)%\Windows Kits\10\Include\<WindowsTargetPlatformVersion>\cppwinrt\winrt\base.h`).

Second, **array_view** has an initializer-list constructor.

```cppwinrt
template <typename T> array_view(std::initializer_list<T> value) noexcept
```

In many cases, you can choose whether or not to be aware of **array_view** in your programming. If you choose *not* to be aware of it then you won't have any code to change if and when an equivalent type appears in the standard library.

## Standard arrays and vectors
**array_view** also has conversion constructors from `std::array` and `std::vector`.

```cppwinrt
template <typename C, size_type N> array_view(std::array<C, N>& value) noexcept
template <typename C> array_view(std::vector<C>& vectorValue) noexcept
```

So, you could instead call **DataWriter::WriteBytes** with a `std::array`.

```cppwinrt
std::array<byte, 3> theArray{ 99, 98, 97 };
dataWriter.WriteBytes(theArray); // theArray is converted to an array_view before being passed to WriteBytes.
```

Or with a `std::vector`.

```cppwinrt
std::vector<byte> theVector{ 99, 98, 97 };
dataWriter.WriteBytes(theVector); // theVector is converted to an array_view before being passed to WriteBytes.
```

## Raw arrays, and pointer ranges
Bearing in mind the caveat that an equivalent type may exist in the future in the standard library, you can also work directly with **array_view** if you choose to, or need to.

**array_view** has conversion constructors from a raw array, and from a range of `T*` (pointers to the element type).

```cppwinrt
using namespace winrt;
...
byte theRawArray[]{ 99, 98, 97 };
array_view<byte const> fromRawArray{ theRawArray };
dataWriter.WriteBytes(fromRawArray); // the array_view is passed to WriteBytes.

array_view<byte const> fromRange{ theArray.data(), theArray.data() + 2 }; // just the first two elements.
dataWriter.WriteBytes(fromRange); // the array_view is passed to WriteBytes.
```

## winrt::array_view functions and operators
A host of constructors, operators, functions, and iterators are implemented for **array_view**. An **array_view** is a range, so you can use it with range-based `for`, or with `std::for_each`.

For more examples and info, see the [**winrt::array_view**](/uwp/cpp-ref-for-winrt/array-view?branch=live) API reference topic.

## Important APIs
* [winrt::array_view (C++/WinRT)](/uwp/cpp-ref-for-winrt/array-view?branch=live)
