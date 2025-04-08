---
description: These extension points in C++/WinRT 2.0 allow you to defer destruction of your implementation types, to safely query during destruction, and to hook the entry into and exit from your projected methods.
title: Extension points for your implementation types
ms.date: 09/26/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, deferred destruction, safe queries
ms.localizationpriority: medium
---

# Extension points for your implementation types

The [winrt::implements struct template](/uwp/cpp-ref-for-winrt/implements) is the base from which your own [C++/WinRT](./intro-to-using-cpp-with-winrt.md) implementations (of runtime classes and activation factories) directly or indirectly derive.

This topic discusses the extension points of **winrt::implements** in C++/WinRT 2.0. You can choose to implement these extension points on your implementation types, in order to customize the default behavior of inspectable objects (*inspectable* in the sense of the [IInspectable](/windows/win32/api/inspectable/nn-inspectable-iinspectable) interface).

These extension points allow you to defer destruction of your implementation types, to safely query during destruction, and to hook the entry into and exit from your projected methods. This topic describes those features and explains more about when and how you would use them.

## Deferred destruction

In the [Diagnosing direct allocations](./diag-direct-alloc.md) topic, we mentioned that your implementation type can't have a private destructor.

The benefit of having a public destructor is that it enables deferred destruction, which is the ability to detect the final [**IUnknown::Release**](/windows/win32/api/unknwn/nf-unknwn-iunknown-release) call on your object, and then to take ownership of that object to defer its destruction indefinitely.

Recall that classic COM objects are intrinsically reference counted; the reference count is managed via the [**IUnknown::AddRef**](/windows/win32/api/unknwn/nf-unknwn-iunknown-addref) and **IUnknown::Release** functions. In a traditional implementation of **Release**, a classic COM object's C++ destructor is invoked once the reference count reaches 0.

```cppwinrt
uint32_t WINRT_CALL Release() noexcept
{
    uint32_t const remaining{ subtract_reference() };
 
    if (remaining == 0)
    {
        delete this;
    }
 
    return remaining;
}
```

The `delete this;` calls the object's destructor before freeing the memory occupied by the object. This works well enough, provided you don't need to do anything interesting in your destructor.

```cppwinrt
using namespace winrt::Windows::Foundation;
... 
struct Sample : implements<Sample, IStringable>
{
    winrt::hstring ToString() const;
 
    ~Sample() noexcept
    {
        // Too late to do anything interesting.
    }
};
```

What do we mean by *interesting*? For one thing, a destructor is inherently synchronous. You can't switch threads&mdash;perhaps to destroy some thread-specific resources in a different context. You can't reliably query the object for some other interface that you might need in order to free certain resources. The list goes on. For the cases where your destruction is non-trivial, you need a more flexible solution. Which is where C++/WinRT's **final_release** function comes in.

```cppwinrt
struct Sample : implements<Sample, IStringable>
{
    winrt::hstring ToString() const;
 
    static void final_release(std::unique_ptr<Sample> ptr) noexcept
    {
        // This is the first stop...
    }
 
    ~Sample() noexcept
    {
        // ...And this happens only when *unique_ptr* finally deletes the object.
    }
};
```

We've updated the C++/WinRT implementation of **Release** to call your **final_release** right when your object's reference count transitions to 0. In that state, the object can be confident that there are no further outstanding references, and it now has exclusive ownership of itself. For that reason, it can transfer ownership of itself to the static **final_release** function.

In other words, the object has transformed itself from one that supports shared ownership into one that is exclusively owned. The **std::unique_ptr** has exclusive ownership of the object, and so it'll naturally destroy the object as part of its semantics&mdash;hence the need for a public destructor&mdash;when the **std::unique_ptr** goes out of scope (provided that it isn't moved elsewhere before that). And that's the key. You can use the object indefinitely, provided that the **std::unique_ptr** keeps the object alive. Here's an illustration of how you might move the object elsewhere.

```cppwinrt
struct Sample : implements<Sample, IStringable>
{
    winrt::hstring ToString() const;
 
    static void final_release(std::unique_ptr<Sample> ptr) noexcept
    {
        batch_cleanup.push_back(std::move(ptr));
    }
};
```

This code saves the object in a collection named *batch_cleanup* one of whose jobs will be to clean up all the objects at some future point in the app's run-time.

Normally, the object destructs when the **std::unique_ptr** destructs, but you can hasten its destruction by calling **std::unique_ptr::reset**; or you can postpone it by saving the **std::unique_ptr** somewhere.

Perhaps more practically and more powerfully, you can turn the **final_release** function into a coroutine, and handle its eventual destruction in one place while being able to suspend and switch threads as needed.

```cppwinrt
struct Sample : implements<Sample, IStringable>
{
    winrt::hstring ToString() const;
 
    static winrt::fire_and_forget final_release(std::unique_ptr<Sample> ptr) noexcept
    {
        co_await winrt::resume_background(); // Unwind the calling thread.
 
        // Safely perform complex teardown here.
    }
};
```

A suspension will point causes the calling thread&mdash;which originally initiated the call to the **IUnknown::Release** function&mdash;to return, and thus signal to the caller that the object it once held is no longer available through that interface pointer. UI frameworks often need to ensure that objects are destroyed on the specific UI thread that originally created the object. This feature makes fulfilling such a requirement trivial, because destruction is separated from releasing the object.

Note that the object passed to **final_release** is merely a C++ object; it is no longer a COM object. For example, existing COM weak references to the object no longer resolve.

## Safe queries during destruction

Building on the notion of deferred destruction is the ability to safely query for interfaces during destruction.

Classic COM is based on two central concepts. The first is reference counting, and the second is querying for interfaces. In addition to **AddRef** and **Release**, the **IUnknown** interface provides [**QueryInterface**](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)). That method is heavily used by certain UI frameworks&mdash;such as XAML, to traverse the XAML hierarchy as it simulates its composable type system. Consider a simple example.

```cppwinrt
struct MainPage : PageT<MainPage>
{
    ~MainPage()
    {
        DataContext(nullptr);
    }
};
```

That may *appear* harmless. This XAML page wants to clear its data context in its destructor. But [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) is a property of the **FrameworkElement** base class, and it lives on the distinct **IFrameworkElement** interface. As a result, C++/WinRT must inject a call to **QueryInterface** to look up the correct vtable before being able to call the **DataContext** property. But the reason we're even in the destructor is that the reference count has transitioned to 0. Calling **QueryInterface** here temporarily bumps that reference count; and when it again returns to 0, the object destructs again.

C++/WinRT 2.0 has been hardened to support this. Here's the C++/WinRT 2.0 implementation of Release, in a simplified form.

```cppwinrt
uint32_t Release() noexcept
{
    uint32_t const remaining{ subtract_reference() };
 
    if (remaining == 0)
    {
        m_references = 1; // Debouncing!
        T::final_release(...);
    }
 
    return remaining;
}
```

As you might have predicted, it first decrements the reference count, and then acts only if there are no outstanding references. However, before calling the static **final_release** function that we described earlier in this topic, it stabilizes the reference count by setting it to 1. We refer to this as *debouncing* (borrowing a term from electrical engineering). This is critical to prevent the final reference from being release. Once that happens, the reference count is unstable, and isn't able to reliably support a call to **QueryInterface**.

Calling **QueryInterface** is dangerous after the final reference has been released, because the reference count can then conceivably grow indefinitely. It's your responsibility to call only known code paths that won't prolong the life of the object. C++/WinRT meets you halfway by ensuring that those **QueryInterface** calls can be made reliably.

It does that by stabilizing the reference count. When the final reference has been released, the actual reference count is either 0, or some wildly unpredictable value. The latter case may occur if weak references are involved. Either way, this is unsustainable if a subsequent call to **QueryInterface** occurs; because that will necessarily cause the reference count to increment temporarily&mdash;hence the reference to debouncing. Setting it to 1 ensures that a final call to **Release** will never again occur on this object. That's precisely what we want, since the **std::unique_ptr** now owns the object, but bounded calls to **QueryInterface**/**Release** pairs will be safe.

Consider a more interesting example.

```cppwinrt
struct MainPage : PageT<MainPage>
{
    ~MainPage()
    {
        DataContext(nullptr);
    }

    static winrt::fire_and_forget final_release(std::unique_ptr<MainPage> ptr)
    {
        co_await 5s;
        co_await winrt::resume_foreground(ptr->Dispatcher());
        ptr = nullptr;
    }
};
```

First, the **final_release** function is called, notifying the implementation that it's time to clean up. Here, **final_release** happens to be a coroutine. To simulate a first suspension point, it begins by waiting on the thread pool for a few seconds. It then resumes on the page's dispatcher thread. That last step involves a query, since [**Dispatcher**](/uwp/api/windows.ui.xaml.dependencyobject.dispatcher) is a property of the **DependencyObject** base class. Finally, the page is actually deleted by virtue of assigning `nullptr` to the **std::unique_ptr**. That in turn calls the page's destructor.

Inside the destructor, we clear the data context; which, as we know, requires a query for the **FrameworkElement** base class.

All of this possible because of the reference count debouncing (or reference count stabilization) provided by C++/WinRT 2.0.

## Method entry and exit hooks

A less commonly-used extension point is the **abi_guard** struct, and the **abi_enter** and **abi_exit** functions.

If your implementation type defines a function **abi_enter**, then that function is called at the entry to every one of your projected interface methods (not counting the methods of [IInspectable](/windows/win32/api/inspectable/nn-inspectable-iinspectable)).

Similarly, if you define **abi_exit**, then that will be called at the exit from every such method; but it will not be called if your **abi_enter** throws an exception. It *will* still be called if an exception is thrown by your projected interface method itself.

As an example, you might use **abi_enter** to throw a hypothetical **invalid_state_error** exception if a client tries to use an object after the object has been put into an unusable state&mdash;say after a **Shut­Down** or **Disconnect** method call. The C++/WinRT iterator classes use this feature to throw an invalid state exception in the **abi_enter** function if the underlying collection has changed.

Over and above the simple **abi_enter** and **abi_exit**functions, you can define a nested type named **abi_guard**. In that case, an instance of **abi_guard** is created on entry to each (non-**IInspectable**) of your projected interface methods, with a reference to the object as its constructor parameter. The **abi_guard** is then destructed on exit from the method. You can put whatever extra state you like into your **abi_guard** type.

If you don't define your own **abi_guard**, then there's a default one that calls **abi_enter** at construction, and **abi_exit** at destruction.

These guards are used only when a method is invoked *via the projected interface*. If you invoke methods directly on the implementation object, then those calls go straight to the implementation, without any guards.

Here's a code example.

```cppwinrt
struct Sample : SampleT<Sample, IClosable>
{
    void abi_enter();
    void abi_exit();

    void Close();
};

void example1()
{
    auto sampleObj1{ winrt::make<Sample>() };
    sampleObj1.Close(); // Calls abi_enter and abi_exit.
}

void example2()
{
    auto sampleObj2{ winrt::make_self<Sample>() };
    sampleObj2->Close(); // Doesn't call abi_enter nor abi_exit.
}

// A guard is used only for the duration of the method call.
// If the method is a coroutine, then the guard applies only until
// the IAsyncXxx is returned; not until the coroutine completes.

IAsyncAction CloseAsync()
{
    // Guard is active here.
    DoWork();

    // Guard becomes inactive once DoOtherWorkAsync
    // returns an IAsyncAction.
    co_await DoOtherWorkAsync();

    // Guard is not active here.
}
```
