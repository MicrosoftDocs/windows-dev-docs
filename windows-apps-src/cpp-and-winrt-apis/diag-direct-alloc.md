---
description: This topic goes in-depth on a C++/WinRT 2.0 feature that helps you diagnose the mistake of creating an object of implementation type on the stack, rather than using the [**winrt::make**](/uwp/cpp-ref-for-winrt/make) family of helpers, as you should.
title: Diagnosing direct allocations
ms.date: 07/19/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, direct, stack, allocations, projected, implementation
ms.localizationpriority: medium
---

# Diagnosing direct allocations

As explained in [Author APIs with C++/WinRT](./author-apis.md), when you create an object of implementation type, you should use the [**winrt::make**](/uwp/cpp-ref-for-winrt/make) family of helpers to do so. This topic goes in-depth on a C++/WinRT 2.0 feature that helps you to diagnose the mistake of directly allocating an object of implementation type on the stack.

Such mistakes can turn into mysterious crashes or corruptions that are difficult and time-consuming to debug. So this is an important feature, and it's worth understanding the background.

## Setting the scene, with **MyStringable**

First, let's consider a simple implementation of [**IStringable**](/uwp/api/windows.foundation.istringable).

```cppwinrt
struct MyStringable : implements<MyStringable, IStringable>
{
    winrt::hstring ToString() const { return L"MyStringable"; }
};
```

Now imagine that you need to call a function (from within your implementation) that expects an **IStringable** as an argument.

```cppwinrt
void Print(IStringable const& stringable)
{
    printf("%ls\n", stringable.ToString().c_str());
}
```

The trouble is that our **MyStringable** type is *not* an **IStringable**.

- Our **MyStringable** type is an implementation of the **IStringable** interface.
- The **IStringable** type is a projected type.

> [!IMPORTANT]
> It's important to understand the distinction between an *implementation type* and a *projected type*. For essential concepts and terms, be sure to read [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

The space between an implementation and the projection can be subtle to grasp. And in fact, to try to make the implementation feel a bit more like the projection, the implementation provides implicit conversions to each of the projected types that it implements. That doesn't mean we can simply do this.

```cppwinrt
struct MyStringable : implements<MyStringable, IStringable>
{
    winrt::hstring ToString() const;
 
    void Call()
    {
        Print(this);
    }
};
```

Instead, we need to get a reference so that conversion operators may be used as candidates for resolving the call.

```cppwinrt
void Call()
{
    Print(*this);
}
```

That works. An implicit conversion provides a (very efficient) conversion from the implementation type to the projected type, and that's very convenient for many scenarios. Without that facility, a lot of implementation types would prove very cumbersome to author. Provided that you only use the [**winrt::make**](/uwp/cpp-ref-for-winrt/make) function template (or [**winrt::make_self**](/uwp/cpp-ref-for-winrt/make-self)) to allocate the implementation, then all is well.

```cppwinrt
IStringable stringable{ winrt::make<MyStringable>() };
```

## Potential pitfalls with C++/WinRT 1.0

Still, implicit conversions can land you in trouble. Consider this unhelpful helper function.

```cppwinrt
IStringable MakeStringable()
{
    return MyStringable(); // Incorrect.
}
```

Or even just this apparently harmless statement.

```cppwinrt
IStringable stringable{ MyStringable() }; // Also incorrect.
```

Unfortunately, code like that *did* compile with C++/WinRT 1.0, because of that implicit conversion. The (very serious) problem is that we're potentially returning a projected type that points to a reference-counted object whose backing memory is on the ephemeral stack.

Here's something else that compiled with C++/WinRT 1.0.

```cppwinrt
MyStringable* stringable{ new MyStringable() }; // Very inadvisable.
```

Raw pointers are dangerous and labor-intensive source of bugs. Don't use them if you don't need to. C++/WinRT goes out of its way to make everything efficient without ever forcing you into using raw pointers. Here's something else that compiled with C++/WinRT 1.0.

```cppwinrt
auto stringable{ std::make_shared<MyStringable>(); } // Also very inadvisable.
```

This is a mistake on several levels. We have two different reference counts for the same object. The Windows Runtime (and classic COM before it) is based on an intrinsic reference count that's not compatible with **std::shared_ptr**. **std::shared_ptr** has, of course, many valid applications; but it's entirely unnecessary when you're sharing Windows Runtime (and classic COM) objects. Finally, this also compiled with C++/WinRT 1.0.

```cppwinrt
auto stringable{ std::make_unique<MyStringable>() }; // Highly dubious.
```

This is again rather questionable. The unique ownership is in opposition to the shared lifetime of the **MyStringable**'s intrinsic reference count.

## The solution with C++/WinRT 2.0

With C++/WinRT 2.0, all of these attempts to directly allocate implementation types leads to a compiler error. That's the best kind of error, and infinitely better than a mysterious runtime bug.

Whenever you need to make an implementation, you can simply use [**winrt::make**](/uwp/cpp-ref-for-winrt/make) or [**winrt::make_self**](/uwp/cpp-ref-for-winrt/make-self), as shown above. And now, if you forget to do so, then you'll be greeted with a compiler error alluding to this with a reference to an abstract function named **use_make_function_to_create_this_object**. It's not exactly a `static_assert`; but it's close. Still, this is the most reliable way of detecting all of the mistakes described.

It does mean that we need to place a few minor constraints on the implementation. Given that we're relying on the absence of an override to detect direct allocation, the **winrt::make** function template must somehow satisfy the abstract virtual function with an override. It does so by deriving from the implementation with a `final` class that provides the override. There are a few things to observe about this process.

First, the virtual function is only present in debug builds. Which means that detection isn't going to affect the size of the vtable in your optimized builds.

Second, since the derived class that **winrt::make** uses is `final`, it means that any devirtualization that the optimizer can possibly deduce will happen even if you previously chose not to mark your implementation class as `final`. So that's an improvement. The converse is that your implementation *can't* be `final`. Again, that's of no consequence because the instantiated type will always be `final`.

Third, nothing prevents you from marking any virtual functions in your implementation as `final`. Of course, C++/WinRT is very different from classic COM and implementations such as WRL, where everything about your implementation tends to be virtual. In C++/WinRT, the virtual dispatch is limited to the application binary interface (ABI) (which is always `final`), and your implementation methods rely on compile-time or static polymorphism. That avoids unnecessary runtime polymorphism, and also means that there's precious little reason for virtual functions in your C++/WinRT implementation. Which is a very good thing, and leads to far more predictable inlining.

Fourth, since **winrt::make** injects a derived class, your implementation can't have a private destructor. Private destructors were popular with classic COM implementations because, again, everything was virtual, and it was common to deal directly with raw pointers and thus was easy to accidentally call `delete` instead of [**Release**](/windows/win32/api/unknwn/nf-unknwn-iunknown-release). C++/WinRT goes out of its way to make it hard for you to deal directly with raw pointers. And you'd have to *really* go out of your way to get a raw pointer in C++/WinRT that you could potentially call `delete` on. Value semantics means that you're dealing with values and references; and rarely with pointers.

So, C++/WinRT challenges our preconceived notions of what it means to write classic COM code. And that's perfectly reasonable because WinRT is not classic COM. Classic COM is the assembly language of the Windows Runtime. It shouldn't be the code you write every day. Instead, C++/WinRT gets you to write code that's more like modern C++, and far less like classic COM.

## Important APIs
* [winrt::make function template](/uwp/cpp-ref-for-winrt/make)
* [winrt::make_self function template](/uwp/cpp-ref-for-winrt/make-self)

## Related topics
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Author APIs with C++/WinRT](./author-apis.md)