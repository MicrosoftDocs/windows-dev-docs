---
author: stevewhims
description: C++/WinRT weak reference support is pay-for-play, in that it doesn't cost you anything unless your object is queried for IWeakReferenceSource.
title: Weak references in C++/WinRT
ms.author: stwhi
ms.date: 04/19/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, weak, reference
ms.localizationpriority: medium
---

# Weak references in [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
You should be able, more often than not, to design your own C++/WinRT APIs in such a way as to avoid the need for cyclic references and weak references. However, when it comes to the native implementation of the XAML-based UI frameworkL&mdash;because of the historic design of the framework&mdash;the weak reference mechanism in C++/WinRT is necessary to handle cyclic references. Outside of XAML, it's unlikely you'll need to use weak references (although, there’s nothing XAML-specific about them in theory).

For any given type that you declare, it's not immediately obvious to C++/WinRT whether or when weak references are needed. So, C++/WinRT provides weak reference support automatically on the struct template [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements), from which your own C++/WinRT types directly or indirectly derive. It's pay-for-play, in that it doesn't cost you anything unless your object is actually queried for [**IWeakReferenceSource**](https://msdn.microsoft.com/library/br224609). And you can choose explicitly to [opt out of that support](#opting-out-of-weak-reference-support).

## Code examples
The [**winrt::weak_ref**](/uwp/cpp-ref-for-winrt/weak-ref) struct template is one option for getting a weak reference to a class instance.

```cppwinrt
Class c;
winrt::weak_ref<Class> weak{ c };
```
Or, you can use the use the [**winrt::make_weak**](/uwp/cpp-ref-for-winrt/make-weak) helper function.

```cppwinrt
Class c;
auto weak = winrt::make_weak(c);
```

Creating a weak reference doesn't affect the reference count on the object itself; it just causes a control block to be allocated. That control block takes care of implementing the weak reference semantics. You can then try to promote the weak reference to a strong reference and, if successful, use it.

```cppwinrt
if (Class strong = weak.get())
{
    // use strong, for example strong.DoWork();
}
```

Provided that some other strong reference still exists, the [**weak_ref::get**](/uwp/cpp-ref-for-winrt/weak-ref#weakrefget-function) call increments the reference count and returns the strong reference to the caller.

## A weak reference to the *this* pointer
A C++/WinRT object directly or indirectly derives from the struct template [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements). The [**implements::get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsgetweak-function) protected member function returns a weak reference to a C++/WinRT object's *this* pointer. [**implements.get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsgetstrong-function) gets a strong reference.

## Opting out of weak reference support
Weak reference support is automatic. But you can choose explicitly to opt out of that support by passing the [**winrt::no_weak_ref**](/uwp/cpp-ref-for-winrt/no-weak-ref) marker struct as a template argument to your base class.

If you derive directly from **winrt::implements**.

```cppwinrt
struct MyImplementation: implements<MyImplementation, IStringable, no_weak_ref>
{
    ...
}
```

If you're authoring a runtime class.

```cppwinrt
struct MyRuntimeClass: MyRuntimeClassT<MyRuntimeClass, no_weak_ref>
{
    ...
}
```

It doesn't matter where in the variadic parameter pack the marker struct appears. If you request a weak reference for an opted-out type, then the compiler will help you out with "*This is only for weak ref support*".

## Important APIs
* [implements::get_weak function](/uwp/cpp-ref-for-winrt/implements#implementsgetweak-function)
* [winrt::make_weak function template](/uwp/cpp-ref-for-winrt/make-weak)
* [winrt::no_weak_ref marker struct](/uwp/cpp-ref-for-winrt/no-weak-ref)
* [winrt::weak_ref struct template](/uwp/cpp-ref-for-winrt/weak-ref)
