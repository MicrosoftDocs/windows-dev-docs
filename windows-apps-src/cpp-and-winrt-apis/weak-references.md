---
description: The Windows Runtime is a reference-counted system; and in such a system it's important for you to know about the significance of, and distinction between, strong and weak references.
title: Strong and weak references in C++/WinRT
ms.date: 05/16/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, strong, weak, reference
ms.localizationpriority: medium
ms.custom: RS5
---

# Strong and weak references in C++/WinRT

The Windows Runtime is a reference-counted system; and in such a system it's important for you to know about the significance of, and distinction between, strong and weak references (and references that are neither, such as the implicit *this* pointer). As you'll see in this topic, knowing how to manage these references correctly can mean the difference between a reliable system that runs smoothly, and one that crashes unpredictably. By providing helper functions that have deep support in the language projection, [C++/WinRT](./intro-to-using-cpp-with-winrt.md) meets you halfway in your work of building more complex systems simply and correctly.

## Safely accessing the *this* pointer in a class-member coroutine

For more info about coroutines, and code examples, see [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md).

The code listing below shows a typical example of a coroutine that's a member function of a class. You can copy-paste this example into the specified files in a new **Windows Console Application (C++/WinRT)** project.

```cppwinrt
// pch.h
#pragma once
#include <iostream>
#include <winrt/Windows.Foundation.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace std::chrono_literals;

struct MyClass : winrt::implements<MyClass, IInspectable>
{
    winrt::hstring m_value{ L"Hello, World!" };

    IAsyncOperation<winrt::hstring> RetrieveValueAsync()
    {
        co_await 5s;
        co_return m_value;
    }
};

int main()
{
    winrt::init_apartment();

    auto myclass_instance{ winrt::make_self<MyClass>() };
    auto async{ myclass_instance->RetrieveValueAsync() };

    winrt::hstring result{ async.get() };
    std::wcout << result.c_str() << std::endl;
}
```

**MyClass::RetrieveValueAsync** spends some time working, and eventually it returns a copy of the `MyClass::m_value` data member. Calling **RetrieveValueAsync** causes an asynchronous object to be created, and that object has an implicit *this* pointer (through which, eventually, `m_value` is accessed).

Remember that, in a coroutine, execution is synchronous up until the first suspension point, where control is returned to the caller. In **RetrieveValueAsync**, the first `co_await` is the first suspension point. By the time the coroutine resumes (around five seconds later, in this case), anything might have happened to the implicit *this* pointer through which we access `m_value`.

Here's the full sequence of events.

1. In **main**, an instance of **MyClass** is created (`myclass_instance`).
2. The `async` object is created, pointing (via its *this*) to `myclass_instance`.
3. The **winrt::Windows::Foundation::IAsyncAction::get** function hits its first suspension point, blocks for a few seconds, and then returns the result of **RetrieveValueAsync**.
4. **RetrieveValueAsync** returns the value of `this->m_value`.

Step 4 is safe only as long as *this* remains valid.

But what if the class instance is destroyed before the async operation completes? There are all kinds of ways the class instance could go out of scope before the asynchronous method has completed. But we can simulate it by setting the class instance to `nullptr`.

```cppwinrt
int main()
{
    winrt::init_apartment();

    auto myclass_instance{ winrt::make_self<MyClass>() };
    auto async{ myclass_instance->RetrieveValueAsync() };
    myclass_instance = nullptr; // Simulate the class instance going out of scope.

    winrt::hstring result{ async.get() }; // Behavior is now undefined; crashing is likely.
    std::wcout << result.c_str() << std::endl;
}
```

After the point where we destroy the class instance, it looks like we don't directly refer to it again. But of course the asynchronous object has a *this* pointer to it, and tries to use that to copy the value stored inside the class instance. The coroutine is a member function, and it expects to be able to use its *this* pointer with impunity.

With this change to the code, we run into a problem in step 4, because the class instance has been destroyed, and *this* is no longer valid. As soon as the asynchronous object attempts to access the variable inside the class instance, it will crash (or do something entirely undefined).

The solution is to give the asynchronous operation&mdash;the coroutine&mdash;its own strong reference to the class instance. As currently written, the coroutine effectively holds a raw *this* pointer to the class instance; but that's not enough to keep the class instance alive.

To keep the class instance alive, change the implementation of **RetrieveValueAsync** to that shown below.

```cppwinrt
IAsyncOperation<winrt::hstring> RetrieveValueAsync()
{
    auto strong_this{ get_strong() }; // Keep *this* alive.
    co_await 5s;
    co_return m_value;
}
```

A C++/WinRT class directly or indirectly derives from the [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) template. Because of that, the C++/WinRT object can call its [**implements::get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsget_strong-function) protected member function to retrieve a strong reference to its *this* pointer. Note that there's no need to actually use the `strong_this` variable in the code example above; simply calling **get_strong** increments the C++/WinRT object's reference count, and keeps its implicit *this* pointer valid.

> [!IMPORTANT]
> Because **get_strong** is a member function of the **winrt::implements** struct template, you can call it only from a class that directly or indirectly derives from **winrt::implements**, such as a C++/WinRT class. For more info about deriving from **winrt::implements**, and examples, see [Author APIs with C++/WinRT](./author-apis.md).

This resolves the problem that we previously had when we got to step 4. Even if all other references to the class instance disappear, the coroutine has taken the precaution of guaranteeing that its dependencies are stable.

If a strong reference isn't appropriate, then you can instead call [**implements::get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsget_weak-function) to retrieve a weak reference to *this*. Just confirm that you can retrieve a strong reference before accessing *this*. Again, **get_weak** is a member function of the **winrt::implements** struct template.

```cppwinrt
IAsyncOperation<winrt::hstring> RetrieveValueAsync()
{
    auto weak_this{ get_weak() }; // Maybe keep *this* alive.

    co_await 5s;

    if (auto strong_this{ weak_this.get() })
    {
        co_return m_value;
    }
    else
    {
        co_return L"";
    }
}
```

In the example above, the weak reference doesn't keep the class instance from being destroyed when no strong references remain. But it gives you a way of checking whether a strong reference can be acquired before accessing the member variable.

## Safely accessing the *this* pointer with an event-handling delegate

### The scenario

For general info about event-handling, see [Handle events by using delegates in C++/WinRT](handle-events.md).

The previous section highlighted potential lifetime issues in the areas of coroutines and concurrency. But, if you handle an event with an object's member function, or from within a lambda function inside an object's member function, then you need to think about the relative lifetimes of the event recipient (the object handling the event) and the event source (the object raising the event). Let's look at some code examples.

The code listing below first defines a simple **EventSource** class, which raises a generic event that's handled by any delegates that have been added to it. This example event happens to use the [**Windows::Foundation::EventHandler**](/uwp/api/windows.foundation.eventhandler) delegate type, but the issues and remedies here apply to any and all delegate types.

Then, the **EventRecipient** class provides a handler for the **EventSource::Event** event in the form of a lambda function.

```cppwinrt
// pch.h
#pragma once
#include <iostream>
#include <winrt/Windows.Foundation.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"

using namespace winrt;
using namespace Windows::Foundation;

struct EventSource
{
    winrt::event<EventHandler<int>> m_event;

    void Event(EventHandler<int> const& handler)
    {
        m_event.add(handler);
    }

    void RaiseEvent()
    {
        m_event(nullptr, 0);
    }
};

struct EventRecipient : winrt::implements<EventRecipient, IInspectable>
{
    winrt::hstring m_value{ L"Hello, World!" };

    void Register(EventSource& event_source)
    {
        event_source.Event([&](auto&& ...)
        {
            std::wcout << m_value.c_str() << std::endl;
        });
    }
};

int main()
{
    winrt::init_apartment();

    EventSource event_source;
    auto event_recipient{ winrt::make_self<EventRecipient>() };
    event_recipient->Register(event_source);
    event_source.RaiseEvent();
}
```

The pattern is that the event recipient has a lambda event handler with dependencies on its *this* pointer. Whenever the event recipient outlives the event source, it outlives those dependencies. And in those cases, which are common, the pattern works well. Some of these cases are obvious, such as when a UI page handles an event raised by a control that's on the page. The page outlives the button&mdash;so, the handler also outlives the button. This holds true any time the recipient owns the source (as a data member, for example), or any time the recipient and the source are siblings and directly owned by some other object.

When you're sure you have a case where the handler won't outlive the *this* that it depends on, then you can capture *this* normally, without consideration for strong or weak lifetime.

But there are still cases where *this* doesn't outlive its use in a handler (including handlers for completion and progress events raised by asynchronous actions and operations), and it's important to know how to deal with them.

- When an event source raises its events *synchronously*, you can revoke your handler and be confident that you won't receive any more events. But for asynchronous events, even after revoking (and especially when revoking within the destructor), an in-flight event might reach your object after it has started destructing. Finding a place to unsubscribe prior to destruction might mitigate the issue, but continue reading for a robust solution.
- If you're authoring a coroutine to implement an asynchronous method, then it's possible.
- In rare cases with certain XAML UI framework objects ([**SwapChainPanel**](/uwp/api/windows.ui.xaml.controls.swapchainpanel), for example), then it's possible, if the recipient is finalized without unregistering from the event source.

### The issue

This next version of the **main** function simulates what happens when the event recipient is destroyed (perhaps it goes out of scope) while the event source is still raising events.

```cppwinrt
int main()
{
    winrt::init_apartment();

    EventSource event_source;
    auto event_recipient{ winrt::make_self<EventRecipient>() };
    event_recipient->Register(event_source);
    event_recipient = nullptr; // Simulate the event recipient going out of scope.
    event_source.RaiseEvent(); // Behavior is now undefined within the lambda event handler; crashing is likely.
}
```

The event recipient is destroyed, but the lambda event handler within it is still subscribed to the **Event** event. When that event is raised, the lambda attempts to dereference the *this* pointer, which is at that point invalid. So, an access violation results from code in the handler (or in a coroutine's continuation) attempting to use it.

> [!IMPORTANT]
> If you encounter a situation like this, then you'll need to think about the lifetime of the *this* object; and whether or not the captured *this* object outlives the capture. If it doesn't, then capture it with a strong or a weak reference, as we'll demonstrate below.
>
> Or&mdash;if it makes sense for your scenario, and if threading considerations make it even possible&mdash;then another option is to revoke the handler after the recipient is done with the event, or in the recipient's destructor. See [Revoke a registered delegate](handle-events.md#revoke-a-registered-delegate).

This is how we're registering the handler.

```cppwinrt
event_source.Event([&](auto&& ...)
{
    std::wcout << m_value.c_str() << std::endl;
});
```

The lambda automatically captures any local variables by reference. So, for this example, we could equivalently have written this.

```cppwinrt
event_source.Event([this](auto&& ...)
{
    std::wcout << m_value.c_str() << std::endl;
});
```

In both cases, we're just capturing the raw *this* pointer. And that has no effect on reference-counting, so nothing is preventing the current object from being destroyed.

### The solution

The solution is to capture a strong reference (or, as we'll see, a weak reference if that's more appropriate). A strong reference *does* increment the reference count, and it *does* keep the current object alive. You just declare a capture variable (called `strong_this` in this example), and initialize it with a call to [**implements::get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsget_strong-function), which retrieves a strong reference to our *this* pointer.

> [!IMPORTANT]
> Because **get_strong** is a member function of the **winrt::implements** struct template, you can call it only from a class that directly or indirectly derives from **winrt::implements**, such as a C++/WinRT class. For more info about deriving from **winrt::implements**, and examples, see [Author APIs with C++/WinRT](./author-apis.md).

```cppwinrt
event_source.Event([this, strong_this { get_strong()}](auto&& ...)
{
    std::wcout << m_value.c_str() << std::endl;
});
```

You can even omit the automatic capture of the current object, and access the data member through the capture variable instead of via the implicit *this*.

```cppwinrt
event_source.Event([strong_this { get_strong()}](auto&& ...)
{
    std::wcout << strong_this->m_value.c_str() << std::endl;
});
```

If a strong reference isn't appropriate, then you can instead call [**implements::get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsget_weak-function) to retrieve a weak reference to *this*. A weak reference *does not* keep the current object alive. So, just confirm that you can still retrieve a strong reference from the weak reference before accessing members.

```cppwinrt
event_source.Event([weak_this{ get_weak() }](auto&& ...)
{
    if (auto strong_this{ weak_this.get() })
    {
        std::wcout << strong_this->m_value.c_str() << std::endl;
    }
});
```

If you capture a raw pointer, then you'll need to make sure you keep the pointed-to object alive.

### If you use a member function as a delegate

As well as lambda functions, these principles also apply to using a member function as your delegate. The syntax is different, so let's look at some code. First, here's the potentially unsafe member function event handler, using a raw *this* pointer.

```cppwinrt
struct EventRecipient : winrt::implements<EventRecipient, IInspectable>
{
    winrt::hstring m_value{ L"Hello, World!" };

    void Register(EventSource& event_source)
    {
        event_source.Event({ this, &EventRecipient::OnEvent });
    }

    void OnEvent(IInspectable const& /* sender */, int /* args */)
    {
        std::wcout << m_value.c_str() << std::endl;
    }
};
```

This is the standard, conventional way to refer to an object and its member function. To make this safe, you can&mdash;as of version 10.0.17763.0 (Windows 10, version 1809) of the Windows SDK&mdash;establish a strong or a weak reference at the point where the handler is registered. At that point, the event recipient object is known to be still alive.

For a strong reference, just call [**get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsget_strong-function) in place of the raw *this* pointer. C++/WinRT ensures that the resulting delegate holds a strong reference to the current object.

```cppwinrt
event_source.Event({ get_strong(), &EventRecipient::OnEvent });
```

Capturing a strong reference means that your object will become eligible for destruction only after the handler has been unregistered and all outstanding callbacks have returned. However, that guarantee is valid only at the time the event is raised. If your event handler is asynchronous, then you'll have to give your coroutine a strong reference to the class instance before the first suspension point (for details, and code, see the [Safely accessing the *this* pointer in a class-member coroutine](#safely-accessing-the-this-pointer-in-a-class-member-coroutine) section earlier in this topic). But that creates a circular reference between the event source and your object, so you need to explicitly break that by revoking your event.

For a weak reference, call [**get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsget_weak-function). C++/WinRT ensures that the resulting delegate holds a weak reference. At the last minute, and behind the scenes, the delegate attempts to resolve the weak reference to a strong one, and only calls the member function if it's successful.

```cppwinrt
event_source.Event({ get_weak(), &EventRecipient::OnEvent });
```

If the delegate *does* call your member function, then C++/WinRT will keep your object alive until your handler returns. However, if your handler is asynchronous, then it returns at suspension points, and so you'll have to give your coroutine a strong reference to the class instance before the first suspension point. Again, for more info, see [Safely accessing the *this* pointer in a class-member coroutine](#safely-accessing-the-this-pointer-in-a-class-member-coroutine) section earlier in this topic.

### A weak reference example using **SwapChainPanel::CompositionScaleChanged**

In this code example, we use the [**SwapChainPanel::CompositionScaleChanged**](/uwp/api/windows.ui.xaml.controls.swapchainpanel.compositionscalechanged) event by way of another illustration of weak references. The code registers an event handler using a lambda that captures a weak reference to the recipient.

```cppwinrt
winrt::Windows::UI::Xaml::Controls::SwapChainPanel m_swapChainPanel;
winrt::event_token m_compositionScaleChangedEventToken;

void RegisterEventHandler()
{
    m_compositionScaleChangedEventToken = m_swapChainPanel.CompositionScaleChanged([weak_this{ get_weak() }]
        (Windows::UI::Xaml::Controls::SwapChainPanel const& sender,
        Windows::Foundation::IInspectable const& object)
    {
        if (auto strong_this{ weak_this.get() })
        {
            strong_this->OnCompositionScaleChanged(sender, object);
        }
    });
}

void OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel const& sender,
    Windows::Foundation::IInspectable const& object)
{
    // Here, we know that the "this" object is valid.
}
```

In the lamba capture clause, a temporary variable is created, representing a weak reference to *this*. In the body of the lambda, if a strong reference to *this* can be obtained, then the **OnCompositionScaleChanged** function is called. That way, inside **OnCompositionScaleChanged**, *this* can safely be used.

## Weak references in C++/WinRT

Above, we saw weak references being used. In general, they're good for breaking cyclic references. For example, for the native implementation of the XAML-based UI framework&mdash;because of the historical design of the framework&mdash;the weak reference mechanism in C++/WinRT is necessary to handle cyclic references. Outside of XAML, though, you likely won't need to use weak references (not that there's anything inherently XAML-specific about them). Rather you should, more often than not, be able to design your own C++/WinRT APIs in such a way as to avoid the need for cyclic references and weak references. 

For any given type that you declare, it's not immediately obvious to C++/WinRT whether or when weak references are needed. So, C++/WinRT provides weak reference support automatically on the struct template [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements), from which your own C++/WinRT types directly or indirectly derive. It's pay-for-play, in that it doesn't cost you anything unless your object is actually queried for [**IWeakReferenceSource**](/windows/desktop/api/weakreference/nn-weakreference-iweakreferencesource). And you can choose explicitly to [opt out of that support](#opting-out-of-weak-reference-support).

### Code examples
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

Provided that some other strong reference still exists, the [**weak_ref::get**](/uwp/cpp-ref-for-winrt/weak-ref#weak_refget-function) call increments the reference count and returns the strong reference to the caller.

### Opting out of weak reference support
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
* [implements::get_weak function](/uwp/cpp-ref-for-winrt/implements#implementsget_weak-function)
* [winrt::make_weak function template](/uwp/cpp-ref-for-winrt/make-weak)
* [winrt::no_weak_ref marker struct](/uwp/cpp-ref-for-winrt/no-weak-ref)
* [winrt::weak_ref struct template](/uwp/cpp-ref-for-winrt/weak-ref)