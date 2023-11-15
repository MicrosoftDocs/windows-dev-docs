---
description: C++/WinRT simplifies passing parameters to projected APIs by providing automatic conversions for common cases.
title: Passing parameters to projected APIs
ms.date: 07/10/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, pass, parameters, ABI
ms.localizationpriority: medium
---

# Passing parameters to projected APIs

For certain types,
C++/WinRT provides alternative methods for passing a parameter to a projected API.
These parameter-accepting classes are placed in the **winrt::param** namespace.
Only C++/WinRT-generated code should use these classes;
don't use them in your own functions and methods.

> [!IMPORTANT]
> You shouldn't use the types in the **winrt::param** namespace yourself. They're for the benefit of the projection.

Some of these alternative distinguish between synchronous and asynchronous calls.
The version for asynchronous calls typically takes ownership of the parameter data
to ensure that the values remain valid and unchanged until the asynchronous call completes.
Note, however, that this protection does not extend to changes to the collection from
another thread. Preventing that is your responsibility.

## Alternatives for string parameters

**winrt::param::hstring** simplifies passing parameters as **winrt::hstring**. In addition to **winrt::hstring**, these alternatives are also accepted:

| Alternative  | Notes |
|--------------|-------|
|`{}`| An empty string. |
|**std::wstring_view**| The view must be followed by a null terminator. |
|**std::wstring**||
|**wchar_t const\***|A null-terminated string.|

You can't pass `nullptr` to represent the empty string. Instead, use `L""` or `{}`.

The compiler knows how to evaluate `wcslen` on string literals at compile time. So, for literals, `L"Name"sv` and `L"Name"` are equivalent.

Note that **std::wstring_view** objects are not null-terminated, but C++/WinRT requires that the character after the end of the view be a null. If you pass a non-null-terminated **std::wstring_view**, then the process will terminate.

## Alternatives for iterable parameters

**winrt::param::iterable\<T\>** and **winrt::param::async_iterable\<T\>** simplify passing parameters as **IIterable\<T\>**.

The Windows Runtime collections **IVector\<T\>** and **IVectorView\<T\>** already support **IIterable\<T\>**.
The Windows Runtime collections **IMap\<K, V\>** and **IMapView\<K, V\>** already support **IIterable\<IKeyValuePair\<K, V\>\>**.

In addition to **IIterable\<T\>**, the following alternatives are also accepted. Note that some alternatives are available only for synchronous methods.

| Alternative | Sync | Async | Notes |
|-------------|------|-------|-------|
|**std::vector\<T\> const&** | Yes | No | |
|**std::vector\<T\>&&** | Yes | Yes | Contents are moved into a temporary iterable. |
|**std::initializer_list\<T\>** | Yes | Yes | Async version copies the items. |
|**std::initializer_list\<U\>** | Yes | No | **U** must be convertible to **T**. |
|`{ begin, end }` | Yes | No | `begin` and `end` must be [forward iterators](https://en.cppreference.com/w/cpp/named_req/ForwardIterator), and `*begin` must be convertible to **T**. |

The double-iterator works more generally for the case where you have a collection that doesn't fit any of the scenarios above,
as long as you can iterate over it and produce things that can be converted to **T**.
For example, you may have a **IVector\<U\>** or **std::vector\<U\>**, where **U** is convertible to **T*.

In the following example, the **SetStorageItems** method expects an **IIterable\<IStorageItem\>**. The double-iterator pattern lets us pass other types of collections.

```cppwinrt
// Vector of derived types.
std::vector<winrt::StorageFile> storageFiles;
dataPackage.SetStorageItems(storageFiles); // doesn't work
dataPackage.SetStorageItems({ storageFiles.begin(), storageFiles.end() }); // works

// Array of derived types.
std::array<StorageFile, 3> storageFiles;
dataPackage.SetStorageItems(storageFiles); // doesn't work
dataPackage.SetStorageItems({ storageFiles.begin(), storageFiles.end() }); // works
```

For the case of **IIterable\<IKeyValuePair\<K, V\>\>**, the following alternatives are accepted. Note that some alternatives are available only for synchronous methods.

| Alternative  | Sync | Async | Notes |
|--------------|------|-------|-------|
|**std::map\<K, V\> const&** | Yes | No | |
|**std::map\<K, V\>&&** | Yes | Yes | Contents are moved into into a temporary iterable. |
|**std::unordered_map\<K, V\> const&** | Yes | No | |
|**std::unordered_map\<K, V\>&&** | Yes | Yes | Contents are moved into into a temporary iterable. |
|**std::initializer_list\<std::pair\<K, V\>\>** | Yes | Yes | Async version copies the list into a temporary iterable. |
|`{ begin, end }` | Yes | No | `begin` and `end` must be [forward iterators](https://en.cppreference.com/w/cpp/named_req/ForwardIterator), and `begin->first` and `begin->second` must be convertible to **K** and **V**, respectively. |

## Alternatives for vector view parameters

**winrt::param::vector_view\<T\>** and **winrt::param::async_vector_view\<T\>** simplify passing parameters as **IVectorView\<T\>**.

You can use **IVector\<T\>::GetView()** to get an **IVectorView\<T\>** from an **IVector\<T\>**.

In addition to **IVectorView\<T\>**, the following alternatives are also accepted. Note that some alternatives are available only for synchronous methods.

| Alternative  | Sync | Async | Notes |
|--------------|------|-------|-------|
|**std::vector\<T\> const&** | Yes | No | |
|**std::vector\<T\>&&** | Yes | Yes | Contents are moved into a temporary view. |
|**std::initializer_list\<T\>** | Yes | Yes | Async version copies the list into a temporary view. |
|`{ begin, end }` | Yes | No | `begin` and `end` must be [forward iterators](https://en.cppreference.com/w/cpp/named_req/ForwardIterator), and `*begin` must be convertible to **T**. |

Again, the double-iterator version can be used to create vector views out of things that don't fit an existing alternative.
The temporary view is more efficient if the `begin` and `end` iterators are random-access iterators.

## Alternatives for map view parameters

**winrt::param::map_view\<T\>** and **winrt::param::async_map_view\<T\>** simplify passing parameters as **IMapView\<T\>**.

You can use **IMap\<K, V\>::GetView()** to get an **IMapView\<K, V\>** from an **IMap\<K, V\>**.

In addition to **IMapView\<K, V\>**, the following alternatives are also accepted. Note that some alternatives are available only for synchronous methods.

| Alternative  | Sync | Async | Notes |
|--------------|------|-------|-------|
|**std::map\<K, V\> const&** | Yes | No | |
|**std::map\<K, V\>&&** | Yes | Yes | Contents are moved into a temporary view. |
|**std::unordered_map\<K, V\> const&** | Yes | No | |
|**std::unordered_map\<K, V\>&&** | Yes | Yes | Contents are moved into a temporary view. |
|**std::initializer_list\<std::pair\<K, V\>\>** | Yes | Yes | Contents are copied into a temporary view. Keys may not be duplicated. |

## Alternatives for vector parameters

**winrt::param::vector\<T\>** simplifies passing parameters as **IVector\<T\>**. In addition to **IVector\<T\>**, these alternatives are also accepted:

| Alternative  | Notes |
|--------------|-------|
|**std::vector\<T\>&&** | Contents are moved into a temporary vector. Results are *not* moved back. |
|**std::initializer_list\<T\>** | |

If the method mutates the temporary vector, then those changes are not reflected in the original parameters. To observe the changes, pass an **IVector\<T\>**.

## Alternatives for map parameters

**winrt::param::map\<K, V\>** simplifies passing parameters as **IMap\<K, V\>**. In addition to **IMap\<K, V\>**, these alternatives are also accepted:

| You can pass | Notes |
|--------------|-------|
|**std::map\<K, V\>&&** | Contents are moved into a temporary map. Results are *not* moved back. |
|**std::unordered_map\<K, V\>&&** | Contents are moved into a temporary map. Results are *not* moved back. |
|**std::initializer_list\<std::pair\<K, V\>\>** | |

If the method mutates the temporary map, then those changes are not reflected in the original parameters. To observe the changes, pass an **IMap\<K, V\>**.

## Alternatives for array parameters

**winrt::array_view\<T\>** is not in the **winrt::param** namespace, but it is used for parameters that are C-style arrays.
In addition to an explicit **array_view\<T\>**, these alternatives are also accepted:

| Alternative  | Notes |
|--------------|-------|
|`{}` | Empty array. |
|**U[]** | A C-style array, where **U** is convertible to **T**, and `sizeof(U) == sizeof(T)`. |
|**std::array\<U, N\>** | Where **U** is convertible to **T**, and `sizeof(U) == sizeof(T)`. |
|**std::vector\<U\>** | Where **U** is convertible to **T**, and `sizeof(U) == sizeof(T)`. |
|`{ begin, end }` |  `begin` and `end` must be of type **T\***, representing the range [`begin`, `end`).|
|**std::initializer_list\<T\>** | |
|**std::span\<U, N\>** | Where **U** is convertible to **T**, and `sizeof(U) == sizeof(T)`. |

Also see the blog post [The various patterns for passing C-style arrays across the Windows Runtime ABI boundary](https://devblogs.microsoft.com/oldnewthing/20200205-00/?p=103398).