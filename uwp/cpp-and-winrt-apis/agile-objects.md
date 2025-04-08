---
description: An agile object is one that can be accessed from any thread. Your C++/WinRT types are agile by default, but you can opt out.
title: Agile objects with C++/WinRT
ms.date: 04/24/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, agile, object, agility, IAgileObject
ms.localizationpriority: medium
---

# Agile objects in C++/WinRT

In the vast majority of cases, an instance of a Windows Runtime class can be accessed from any thread (just like most standard C++ objects can). Such a Windows Runtime class is *agile*. Only a small number of Windows Runtime classes that ship with Windows are non-agile, but when you consume them you need to take into consideration their threading model and marshaling behavior (marshaling is passing data across an apartment boundary). It's a good default for every Windows Runtime object to be agile, so your own [C++/WinRT](./intro-to-using-cpp-with-winrt.md) types are agile by default.

But you can opt out. You might have a compelling reason to require an object of your type to reside, for example, in a given single-threaded apartment. This typically has to do with reentrancy requirements. But increasingly, even user interface (UI) APIs offer agile objects. In general, agility is the simplest and most performant option. Also, when you implement an activation factory, it must be agile even if your corresponding runtime class isn't.

> [!NOTE]
> The Windows Runtime is based on COM. In COM terms, an agile class is registered with `ThreadingModel` = *Both*. For more info about COM threading models, and apartments, see [Understanding and Using COM Threading Models](/previous-versions/ms809971(v=msdn.10)).

## Code examples

Let's use an example implementation of a runtime class to show how C++/WinRT supports agility.

```cppwinrt
#include <winrt/Windows.Foundation.h>

using namespace winrt;
using namespace Windows::Foundation;

struct MyType : winrt::implements<MyType, IStringable>
{
    winrt::hstring ToString(){ ... }
};
```

Because we haven't opted out, this implementation is agile. The [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) base struct implements [**IAgileObject**](/windows/desktop/api/objidl/nn-objidl-iagileobject) and [**IMarshal**](/windows/desktop/api/objidl/nn-objidl-imarshal). The **IMarshal** implementation uses **CoCreateFreeThreadedMarshaler** to do the right thing for legacy code that doesn't know about **IAgileObject**.

This code checks an object for agility. The call to [**IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) throws an exception if `myimpl` is not agile.

```cppwinrt
winrt::com_ptr<MyType> myimpl{ winrt::make_self<MyType>() };
winrt::com_ptr<IAgileObject> iagileobject{ myimpl.as<IAgileObject>() };
```

Rather than handle an exception, you can call [**IUnknown::try_as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntry_as-function) instead.

```cppwinrt
winrt::com_ptr<IAgileObject> iagileobject{ myimpl.try_as<IAgileObject>() };
if (iagileobject) { /* myimpl is agile. */ }
```

**IAgileObject** has no methods of its own, so you can't do much with it. This next variant, then, is more typical.

```cppwinrt
if (myimpl.try_as<IAgileObject>()) { /* myimpl is agile. */ }
```

**IAgileObject** is a *marker interface*. The mere success or failure of querying for **IAgileObject** is the extent of the information and utility you get from it.

## Opt out of agile object support

You can choose explicitly to opt out of agile object support by passing the [**winrt::non_agile**](/uwp/cpp-ref-for-winrt/non-agile) marker struct as a template argument to your base class.

If you derive directly from **winrt::implements**.

```cppwinrt
struct MyImplementation: implements<MyImplementation, IStringable, winrt::non_agile>
{
    ...
}
```

If you're authoring a runtime class.

```cppwinrt
struct MyRuntimeClass: MyRuntimeClassT<MyRuntimeClass, winrt::non_agile>
{
    ...
}
```

It doesn't matter where in the variadic parameter pack the marker struct appears.

Whether or not you opt out of agility, you can implement **IMarshal** yourself. For example, you can use the **winrt::non_agile** marker to avoid the default agility implementation, and implement **IMarshal** yourself&mdash;perhaps to support marshal-by-value semantics.

## Agile references (winrt::agile_ref)

If you're consuming an object that isn't agile, but you need to pass it around in some potentially agile context, then one option is to use the [**winrt::agile_ref**](/uwp/cpp-ref-for-winrt/agile-ref) struct template to get an agile reference to an instance of a non-agile type, or to an interface of a non-agile object.

```cppwinrt
NonAgileType nonagile_obj;
winrt::agile_ref<NonAgileType> agile{ nonagile_obj };
```

Or, you can use the use the [**winrt::make_agile**](/uwp/cpp-ref-for-winrt/make-agile) helper function.

```cppwinrt
NonAgileType nonagile_obj;
auto agile{ winrt::make_agile(nonagile_obj) };
```

In either case, `agile` may now be freely passed to a thread in a different apartment, and used there.

```cppwinrt
co_await resume_background();
NonAgileType nonagile_obj_again{ agile.get() };
winrt::hstring message{ nonagile_obj_again.Message() };
```

The [**agile_ref::get**](/uwp/cpp-ref-for-winrt/agile-ref#agile_refget-function) call returns a proxy that may safely be used within the thread context in which **get** is called.

## Important APIs

* [IAgileObject interface](/windows/desktop/api/objidl/nn-objidl-iagileobject)
* [IMarshal interface](/windows/desktop/api/objidl/nn-objidl-imarshal)
* [winrt::agile_ref struct template](/uwp/cpp-ref-for-winrt/agile-ref)
* [winrt::implements struct template](/uwp/cpp-ref-for-winrt/implements)
* [winrt::make_agile function template](/uwp/cpp-ref-for-winrt/make-agile)
* [winrt::non_agile marker struct](/uwp/cpp-ref-for-winrt/non-agile)
* [winrt::Windows::Foundation::IUnknown::as function](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function)
* [winrt::Windows::Foundation::IUnknown::try_as function](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntry_as-function)

## Related topics

* [Understanding and Using COM Threading Models](/previous-versions/ms809971(v=msdn.10))