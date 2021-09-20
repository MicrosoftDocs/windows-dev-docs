---
description: C++/WinRT simplifies passing parameters into the ABI boundary by providing automatic conversions for common cases.
title: Passing parameters into the ABI boundary
ms.date: 07/10/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, pass, parameters, ABI
ms.localizationpriority: medium
---

# Passing parameters into the ABI boundary

With the types in the **winrt::param** namespace, C++/WinRT simplifies passing parameters into the ABI boundary by providing automatic conversions for common cases. You can see more details, and code examples, in [String handling](./strings.md) and [Standard C++ data types and C++/WinRT](./std-cpp-data-types.md).

> [!IMPORTANT]
> You shouldn't use the types in the **winrt::param** namespace yourself. They're for the benefit of the projection.

Many types come in both synchronous and asynchronous versions. C++/WinRT uses the synchronous version when you're passing a parameter to a synchronous method; it uses the asynchronous version when you're passing a parameter to an asynchronous method. The asynchronous version takes extra steps to make it harder for the caller to mutate the collection before the operation has completed. Note, however, that none of the variants protect against the collection being mutated from another thread. Preventing that is your responsibility.

## String parameters

**winrt::param::hstring** simplifies passing parameters to APIs that take **HSTRING**.

|Types that you can pass|Notes|
|-|-|
|`{}`|Passes empty string.|
|**winrt::hstring**||
|**std::wstring_view**|For literals, you can write `L"Name"sv`. The view must have a null terminator after the end.|
|**std::wstring**|-|
|**wchar_t const\***|A null-terminated string.|

`nullptr` is not allowed. Use `{}` instead.

The compiler knows how to evaluate `wcslen` on string literals at compile time. So, for literals, `L"Name"sv` and `L"Name"` are equivalent.

Note that **std::wstring_view** objects are not null terminated, but C++/WinRT requires that the character after the end of the string be a null. If you pass a non-null-terminated **std::wstring_view**, then the process will terminate.

## Iterable parameters

**winrt::param::iterable\<T\>** and **winrt::param::async_iterable\<T\>** simplify passing parameters to APIs that take **IIterable\<T\>**.

Windows Runtime collections are already **IIterable**.

|Types that you can pass|Sync|Async|Notes|
|-|-|-|-|
| `nullptr` | Yes | Yes | You must verify that the underlying method supports `nullptr`.|
| **IIterable\<T\>** | Yes | Yes | Or anything convertible to it.|
| **std::vector\<T\> const&** | Yes | No ||
| **std::vector\<T\>&&** | Yes | Yes | Contents are moved into the iterator to prevent mutation.|
| **std::initializer_list\<T\>** | Yes | Yes | The async version copies the items.|
| **std::initializer_list\<U\>** | Yes | No | **U** must be convertible to **T**.|
| `{ ForwardIt begin, ForwardIt end }` | Yes | No | `*begin` must be convertible to **T**.|

Note that **IIterable\<U\>** and **std::vector\<U\>** are not permitted, even if **U** is convertible to **T**. For **std::vector\<U\>**, you can use the double-iterator version (more details below).

In some cases, the object that you have may actually implement the **IIterable** that you want. For example, the **IVectorView\<StorageFile\>** produced by [**FileOpenPicker.PickMultipleFilesAsync**](/uwp/api/windows.storage.pickers.fileopenpicker.pickmultiplefilesasync) implements **IIterable\<StorageFile\>**. But it also implements **IIterable\<IStorageItem\>**; you just have to ask for it explicitly.

```cppwinrt
IVectorView<StorageFile> pickedFiles{ co_await filePicker.PickMultipleFilesAsync() };
requestData.SetStorageItems(storageItems.as<IIterable<IStorageItem>>());
```

In other cases, you can use the double-iterator version.

```cppwinrt
std::vector<StorageFile> storageFiles;
requestData.SetStorageItems({ storageFiles.begin(), storageFiles.end() });
```

The double-iterator works more generally for the case where you have a collection that doesn't fit any of the scenarios above, as long as you can iterate over it and produce things that can be converted to **T**. We used it above to iterate over a vector of derived types. Here, we use it to iterate over a non-vector of derived types.

```cppwinrt
std::array<StorageFile, 3> storageFiles;
requestData.SetStorageItems(storageFiles); // This doesn't work.
requestData.SetStorageItems({ storageFiles.begin(), storageFiles.end() }); // But this works.
```

The implementation of [**IIterator\<T\>.GetMany(T\[\])**](/uwp/api/windows.foundation.collections.iiterator-1.getmany) is more efficient if the iterator is a `RandomAcessIt`. Otherwise, it makes several passes over the range.

|Types that you can pass|Sync|Async|Notes|
|-|-|-|-|
| `nullptr` | Yes | Yes | You must verify that the underlying method supports `nullptr`.|
| **IIterable\<IKeyValuePair\<K, V\>\>** | Yes | Yes | Or anything convertible to it.|
| **std::map\<K, V\> const&** | Yes | No ||
| **std::map\<K, V\>&&** | Yes | Yes | Contents are moved into the iterator to prevent mutation.|
| **std::unordered_map\<K, V\> const&** | Yes | No ||
| **std::unordered_map\<K, V\>&&** | Yes | Yes | Contents are moved into the iterator to prevent mutation.|
| **std::initializer_list\<std::pair\<K, V\>\>** | Yes | Yes | Types **K** and **V** must match exactly. Keys may not be duplicated. The async version copies the items.|
| `{ ForwardIt begin, ForwardIt end }` | Yes | No | `begin->first` and `begin->second` must be convertible to **K** and **V**, respectively.|

## Vector view parameters

**winrt::param::vector_view\<T\>** and **winrt::param::async_vector_view\<T\>** simplify passing parameters to APIs that take **IVectorView\<T\>**.

You can use [**IVector\<T\>.GetView**](/uwp/api/windows.foundation.collections.ivector-1.getview) to get an **IVectorView** from an **IVector**.

|Types that you can pass|Sync|Async|Notes|
|-|-|-|-|
| `nullptr` | Yes | Yes | You must verify that the underlying method supports `nullptr`.|
| **IVectorView\<T\>** | Yes | Yes | Or anything convertible to it.|
| **std::vector\<T\>const&** | Yes | No ||
| **std::vector\<T\>&&** | Yes | Yes | Contents are moved into the view to prevent mutation.|
| **std::initializer_list\<T\>** | Yes | Yes | Type must match exactly. The async version copies the items.|
| `{ ForwardIt begin, ForwardIt end }` | Yes | No | `*begin` must be convertible to **T**.|

The double-iterator version can be used to create vector views out of things that don't fit the requirements for being passed directly. Note, however, that since vectors support random access, we recommend that you pass a `RandomAcessIter`.

## Map view parameters

**winrt::param::map_view\<T\>** and **winrt::param::async_map_view\<T\>** simplify passing parameters to APIs that take **IMapView\<T\>**.

You can use **IMap::GetView** to get an **IMapView** from an **IMap**.

|Types that you can pass|Sync|Async|Notes|
|-|-|-|-|
| `nullptr` | Yes | Yes | You must verify that the underlying method supports `nullptr`.|
| **IMapView\<K, V\>** | Yes | Yes | Or anything convertible to it.|
| **std::map\<K, V\> const&** | Yes | No ||
| **std::map\<K, V\>&&** | Yes | Yes | Contents are moved into the view to prevent mutation.|
| **std::unordered_map\<K, V\> const&**  | Yes | No ||
| **std::unordered_map\<K, V\>&&** | Yes | Yes | Contents are moved into the view to prevent mutation.|
| **std::initializer_list\<std::pair\<K, V\>\>** | Yes | Yes | Both sync and async versions copy the items. You may not duplicate keys.|

## Vector parameters

**winrt::param::vector\<T\>** simplifies passing parameters to APIs that take **IVector\<T\>**.

|Types that you can pass|Notes|
|-|-|
| `nullptr` | You must verify that the underlying method supports `nullptr`.|
| **IVector\<T\>** | Or anything convertible to it.|
| **std::vector\<T\>&&** | Contents are moved into the parameter to prevent mutation. Results are not moved back.|
| **std::initializer_list\<T\>** | Contents are copied into the parameter to prevent mutation.|

If the method mutates the vector, then the only way to observe the mutation is to pass an **IVector** directly. If you pass a **std::vector**, then the method mutates the copy and not the original.

## Map parameters

**winrt::param::map\<T\>** simplifies passing parameters to APIs that take **IMap\<T\>**.

|Types that you can pass|Notes|
|-|-|
| `nullptr` | You must verify that the underlying method supports `nullptr`.|
| **IMap\<T\>** | Or anything convertible to it.|
| **std::map\<K, V\>&&** | Contents are moved into the parameter to prevent mutation. Results are not moved back.|
| **std::unordered_map\<K, V\>&&** | Contents are moved into the parameter to prevent mutation. Results are not moved back.|
| **std::initializer_list\<std::pair\<K, V\>\>** | Contents are copied into the parameter to prevent mutation.|

If the method mutates the map, then the only way to observe the mutation is to pass an **IMap** directly. If you pass a **std::map** or **std::unordered_map**, then the method mutates the copy and not the original.

## Array parameters

The **winrt::array_view\<T\>** is not in the **winrt::param** namespace, but it is used for parameters that are C-style arrays&mdash;also known as *conformant arrays*.

|Types that you can pass|Notes|
|-|-|
| `{}` | An empty array.|
| **array** | A conformant array of C (that is, `C array[N];`), where **C** is convertible to **T**, and `sizeof(C) == sizeof(T)`. |
| **std::array<C, N>** | A C++ **std::array** of **C**, where **C** is convertible to **T**, and `sizeof(C) == sizeof(T)`. |
| **std::vector<C>** | A C++ **std::vector** of **C**, where **C** is convertible to **T**, and `sizeof(C) == sizeof(T)`. |
| `{ T*, T* }` | A pair of pointers represent the range [begin, end).|
| **std::initializer_list\<T\>** ||

Also see the blog post [The various patterns for passing C-style arrays across the Windows Runtime ABI boundary](https://devblogs.microsoft.com/oldnewthing/20200205-00/?p=103398).