---
title: C++/WinRT configuration macros
description: This topic describes the C++/WinRT configuration macros.
ms.date: 03/29/2022
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, configuration, macros
ms.localizationpriority: medium
---

# C++/WinRT configuration macros

This topic describes the C++/WinRT configuration macros. Unless otherwise noted, these rules apply to all of the C++/WinRT configuration macros:

* All files that are linked together to form a single module (`.exe` or `.dll`) must have identical macro settings. That includes static libraries.
* All macro settings must be complete before including any C++/WinRT header file.
* You may not change any macro setting after including any C++/WinRT header file.

## WINRT_LEAN_AND_MEAN
If defined, disables these rarely-used features (in order to reduce compile times):

* The ability to implement exclusive interfaces outside the component.
* **std::hash** specializations for interface and runtime class smart pointers.
* Support for directly outputting an hstring or IStringable to a C++ stream, since version 2.0.221101.3.

You're allowed to combine files with different settings for **WINRT_LEAN_AND_MEAN**.

Files that don't define **WINRT_LEAN_AND_MEAN** gain access to the rarely-used features.

## WINRT_NO_MODULE_LOCK
If defined, disables object counts for the current module. The module never unloads from the process. Defining this macro is customary for executables (which can never unload), or for `.dll`s that you intend to leave pinned. May not be combined with **WINRT_CUSTOM_MODULE_LOCK**.

## WINRT_CUSTOM_MODULE_LOCK
If defined, allows you to provide your own implementation of **winrt::get_module_lock**. May not be combined with **WINRT_NO_MODULE_LOCK**.

Your custom implementation of **winrt::get_module_lock** must support the following operations:

* `++winrt::get_module_lock()`: Increment the reference count on the module lock.
* `--winrt::get_module_lock()`: Decrement the reference count on the module lock.
* `if (winrt::get_module_lock())`: Check whether the reference count is nonzero. (Needed if you're building a DLL.)

## WINRT_ASSERT, WINRT_VERIFY
These macros allow you to customize assertion handling. **WINRT_ASSERT** doesn't require the argument to be evaluated. **WINRT_VERIFY** requires that the argument be evaluated, even in non-debug builds.

If you don't customize these macros, and **_DEBUG** is defined, then C++/WinRT makes them equivalent to **_ASSERTE**.

If you don't customize these macros, and **_DEBUG** is not defined, then C++/WinRT defines **WINRT_ASSERT** to discard the expression unevaluated, and defines **WINRT_VERIFY** to discard the expression after evaluating it.

## WINRT_NO_MAKE_DETECTION
If defined, disables the default C++/WinRT diagnostic that detects that you mistakenly constructed an implementation class without using [**winrt::make**](/uwp/cpp-ref-for-winrt/make).

We strongly recommended that you don't define this symbol, because doing so masks a common source of programming errors.

## WINRT_DIAGNOSTICS
If defined, enables internal statistics to track various operations:
* The number of times each interface was queried.
* The number of times each factory was requested (and whether the factory is agile).

## WINRT_NATVIS
If defined, includes helper functions to assist in native debug visualizations in Visual Studio. The code isn't used at runtime; it exists only for debugging.

If you don't customize this macro, then visualization support functions are enabled if **_DEBUG** is defined. For more details, see [Visual Studio native debug visualization (natvis) for C++/WinRT](natvis.md).

You're allowed to combine files with different settings for **WINRT_NATVIS**.

If any file is compiled with **WINRT_NATVIS** support, then the resulting module will have native debug visualizations enabled.

## WINRT_EXPORT, WINRT_FAST_ABI_SIZE

Don't use these macros.
