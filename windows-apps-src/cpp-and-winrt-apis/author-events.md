---
description: This topic demonstrates how to author a Windows Runtime component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events.
title: Author events in C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, author, event
ms.localizationpriority: medium
---

# Author events in C++/WinRT

This topic builds on the Windows Runtime component, and the consuming application, that the [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md) topic shows you how to build.

Here are the new features that this topic adds.
- Update the bank account runtime class to raise an event when its balance goes into debit.
- Update the Core App that consumes the bank account runtime class so that it handles that event.

> [!NOTE]
> For info about installing and using the [C++/WinRT](./intro-to-using-cpp-with-winrt.md) Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Create **BankAccountWRC** and **BankAccountCoreApp**

If you want to follow along with the updates shown in this topic, so that you can build and run the code, then the first step is to follow the walkthrough in the [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md) topic. By doing so, you'll have the **BankAccountWRC** Windows Runtime component, and the **BankAccountCoreApp** Core App that consumes it.

## Update **BankAccountWRC** to raise an event

Update `BankAccount.idl` to look like the listing below. This is how to declare an event whose delegate type is [**EventHandler**](/uwp/api/windows.foundation.eventhandler-1) with an argument of a single-precision floating-point number.

```idl
// BankAccountWRC.idl
namespace BankAccountWRC
{
    runtimeclass BankAccount
    {
        BankAccount();
        void AdjustBalance(Single value);
        event Windows.Foundation.EventHandler<Single> AccountIsInDebit;
    };
}
```

Save the file. The project won't build to completion in its current state, but perform a build now in any case to generate updated versions of the `\BankAccountWRC\BankAccountWRC\Generated Files\sources\BankAccount.h` and `BankAccount.cpp` stub files. Inside those files you can now see stub implementations of the **AccountIsInDebit** event. In C++/WinRT, an IDL-declared event is implemented as a set of overloaded functions (similar to the way a property is implemented as a pair of overloaded get and set functions). One overload takes a delegate to be registered, and returns a token (a [**winrt::event_token**](/uwp/cpp-ref-for-winrt/event-token)). The other takes a token, and revokes the registration of the associated delegate.

Now open `BankAccount.h` and `BankAccount.cpp`, and update the implementation of the **BankAccount** runtime class. In `BankAccount.h`, add the two overloaded **AccountIsInDebit** functions, as well as a private event data member to use in the implementation of those functions.

```cppwinrt
// BankAccount.h
...
namespace winrt::BankAccountWRC::implementation
{
    struct BankAccount : BankAccountT<BankAccount>
    {
        ...
        winrt::event_token AccountIsInDebit(Windows::Foundation::EventHandler<float> const& handler);
        void AccountIsInDebit(winrt::event_token const& token) noexcept;

    private:
        winrt::event<Windows::Foundation::EventHandler<float>> m_accountIsInDebitEvent;
        ...
    };
}
...
```

As you can see above, an event is represented by the [**winrt::event**](/uwp/cpp-ref-for-winrt/event) struct template, parameterized by a particular delegate type (which itself can be parameterized by an args type).

In `BankAccount.cpp`, implement the two overloaded **AccountIsInDebit** functions.

```cppwinrt
// BankAccount.cpp
...
namespace winrt::BankAccountWRC::implementation
{
    winrt::event_token BankAccount::AccountIsInDebit(Windows::Foundation::EventHandler<float> const& handler)
    {
        return m_accountIsInDebitEvent.add(handler);
    }

    void BankAccount::AccountIsInDebit(winrt::event_token const& token) noexcept
    {
        m_accountIsInDebitEvent.remove(token);
    }

    void BankAccount::AdjustBalance(float value)
    {
        m_balance += value;
        if (m_balance < 0.f) m_accountIsInDebitEvent(*this, m_balance);
    }
}
```

> [!NOTE]
> For details of what an auto event revoker is, see [Revoke a registered delegate](handle-events.md#revoke-a-registered-delegate). You get auto event revoker implementation for free for your event. In other words, you don't need to implement the overload for the event revoker&mdash;that's provided for you by the C++/WinRT projection.

The other overloads (the registration and manual revocation overloads) are *not* baked into the projection. That's to give you the flexibility to implement them optimally for your scenario. Calling [**event::add**](/uwp/cpp-ref-for-winrt/event#eventadd-function) and [**event::remove**](/uwp/cpp-ref-for-winrt/event#eventremove-function) as shown in these implementations is an efficient and concurrency/thread-safe default. But if you have a very large number of events, then you may not want an event field for each, but rather opt for some kind of sparse implementation instead.

You can also see above that the implementation of the **AdjustBalance** function has been updated to raise the **AccountIsInDebit** event if the balance goes negative.

## Update **BankAccountCoreApp** to handle the event

In the **BankAccountCoreApp** project, in `App.cpp`, make the following changes to the code to register an event handler, and then cause the account to go into debit.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    winrt::event_token m_eventToken;
    ...
    
    void Initialize(CoreApplicationView const &)
    {
        m_eventToken = m_bankAccount.AccountIsInDebit([](const auto &, float balance)
        {
            WINRT_ASSERT(balance < 0.f); // Put a breakpoint here.
        });
    }
    ...

    void Uninitialize()
    {
        m_bankAccount.AccountIsInDebit(m_eventToken);
    }
    ...
    
    void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
    {
        m_bankAccount.AdjustBalance(-1.f);
        ...
    }
    ...
};
```

Be aware of the change to the **OnPointerPressed** method. Now, each time you click the window, you *subtract* 1 from the bank account's balance. And now, the app is handling the event that's raised when the balance goes negative. To demonstrate that the event is being raised as expected, put a breakpoint inside the lambda expression that's handling the **AccountIsInDebit** event, run the app, and click inside the window.

## Parameterized delegates across an ABI

If your event must be accessible across an application binary interface (ABI)&mdash;such as between a component and its consuming application&mdash;then your event must use a Windows Runtime delegate type. The example above uses the [**Windows::Foundation::EventHandler\<T\>**](/uwp/api/windows.foundation.eventhandler) Windows Runtime delegate type. [**TypedEventHandler\<TSender, TResult\>**](/uwp/api/windows.foundation.eventhandler) is another example of a Windows Runtime delegate type.

The type parameters for those two delegate types have to cross the ABI, so the type parameters must be Windows Runtime types, too. That includes Windows runtime classes, third-party runtime classes, and primitive types such as numbers and strings. The compiler helps you with a "*must be WinRT type*" error if you forget that constraint.

Below is an example in the form of code listings. Begin with the **BankAccountWRC** and **BankAccountCoreApp** projects that you created earlier in this topic, and edit the code in those projects to look like the code in these listings.

This first listing is for the **BankAccountWRC** project. After editing `BankAccountWRC.idl` as shown below, build the project, and then copy `MyEventArgs.h` and `.cpp` into the project (from the `Generated Files` folder) just like you did earlier with `BankAccount.h` and `.cpp`.

```cppwinrt
// BankAccountWRC.idl
namespace BankAccountWRC
{
    [default_interface]
    runtimeclass MyEventArgs
    {
        Single Balance{ get; };
    }

    [default_interface]
    runtimeclass BankAccount
    {
        ...
        event Windows.Foundation.EventHandler<BankAccountWRC.MyEventArgs> AccountIsInDebit;
        ...
    };
}

// MyEventArgs.h
#pragma once
#include "MyEventArgs.g.h"

namespace winrt::BankAccountWRC::implementation
{
    struct MyEventArgs : MyEventArgsT<MyEventArgs>
    {
        MyEventArgs() = default;
        MyEventArgs(float balance);
        float Balance();

    private:
        float m_balance{ 0.f };
    };
}

// MyEventArgs.cpp
#include "pch.h"
#include "MyEventArgs.h"
#include "MyEventArgs.g.cpp"

namespace winrt::BankAccountWRC::implementation
{
    MyEventArgs::MyEventArgs(float balance) : m_balance(balance)
    {
    }

    float MyEventArgs::Balance()
    {
        return m_balance;
    }
}

// BankAccount.h
...
struct BankAccount : BankAccountT<BankAccount>
{
...
    winrt::event_token AccountIsInDebit(Windows::Foundation::EventHandler<BankAccountWRC::MyEventArgs> const& handler);
...
private:
    winrt::event<Windows::Foundation::EventHandler<BankAccountWRC::MyEventArgs>> m_accountIsInDebitEvent;
...
}
...

// BankAccount.cpp
#include "MyEventArgs.h"
...
winrt::event_token BankAccount::AccountIsInDebit(Windows::Foundation::EventHandler<BankAccountWRC::MyEventArgs> const& handler) { ... }
...
void BankAccount::AdjustBalance(float value)
{
    m_balance += value;

    if (m_balance < 0.f)
    {
        auto args = winrt::make_self<winrt::BankAccountWRC::implementation::MyEventArgs>(m_balance);
        m_accountIsInDebitEvent(*this, *args);
    }
}
...
```

This listing is for the **BankAccountCoreApp** project.

```cppwinrt
// App.cpp
...
void Initialize(CoreApplicationView const&)
{
    m_eventToken = m_bankAccount.AccountIsInDebit([](const auto&, BankAccountWRC::MyEventArgs args)
    {
        float balance = args.Balance();
        WINRT_ASSERT(balance < 0.f); // Put a breakpoint here.
    });
}
...
```

## Simple signals across an ABI

If you don't need to pass any parameters or arguments with your event, then you can define your own simple Windows Runtime delegate type. The example below shows a simpler version of the **BankAccount** runtime class. It declares a delegate type named **SignalDelegate** and then it uses that to raise a signal-type event instead of an event with a parameter.

```idl
// BankAccountWRC.idl
namespace BankAccountWRC
{
    delegate void SignalDelegate();

    runtimeclass BankAccount
    {
        BankAccount();
        event BankAccountWRC.SignalDelegate SignalAccountIsInDebit;
        void AdjustBalance(Single value);
    };
}
```

```cppwinrt
// BankAccount.h
...
namespace winrt::BankAccountWRC::implementation
{
    struct BankAccount : BankAccountT<BankAccount>
    {
        ...

        winrt::event_token SignalAccountIsInDebit(BankAccountWRC::SignalDelegate const& handler);
        void SignalAccountIsInDebit(winrt::event_token const& token);
        void AdjustBalance(float value);

    private:
        winrt::event<BankAccountWRC::SignalDelegate> m_signal;
        float m_balance{ 0.f };
    };
}
```

```cppwinrt
// BankAccount.cpp
...
namespace winrt::BankAccountWRC::implementation
{
    winrt::event_token BankAccount::SignalAccountIsInDebit(BankAccountWRC::SignalDelegate const& handler)
    {
        return m_signal.add(handler);
    }

    void BankAccount::SignalAccountIsInDebit(winrt::event_token const& token)
    {
        m_signal.remove(token);
    }

    void BankAccount::AdjustBalance(float value)
    {
        m_balance += value;
        if (m_balance < 0.f)
        {
            m_signal();
        }
    }
}
```

```cppwinrt
// App.cpp
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    BankAccountWRC::BankAccount m_bankAccount;
    winrt::event_token m_eventToken;
    ...
    
    void Initialize(CoreApplicationView const &)
    {
        m_eventToken = m_bankAccount.SignalAccountIsInDebit([] { /* ... */ });
    }
    ...

    void Uninitialize()
    {
        m_bankAccount.SignalAccountIsInDebit(m_eventToken);
    }
    ...

    void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
    {
        m_bankAccount.AdjustBalance(-1.f);
        ...
    }
    ...
};
```

## Parameterized delegates, simple signals, and callbacks within a project

If you need events that are internal to your Visual Studio project (not across binaries), where those events are not limited to Windows Runtime types, then you can still use the [**winrt::event**](/uwp/cpp-ref-for-winrt/event)\<Delegate\> class template. Simply use [**winrt::delegate**](/uwp/cpp-ref-for-winrt/delegate) instead of an actual Windows Runtime delegate type, since **winrt::delegate** also supports non Windows Runtime parameters.

The example below first shows a delegate signature that doesn't take any parameters (essentially a simple signal), and then one that takes a string.

```cppwinrt
winrt::event<winrt::delegate<>> signal;
signal.add([] { std::wcout << L"Hello, "; });
signal.add([] { std::wcout << L"World!" << std::endl; });
signal();

winrt::event<winrt::delegate<std::wstring>> log;
log.add([](std::wstring const& message) { std::wcout << message.c_str() << std::endl; });
log.add([](std::wstring const& message) { Persist(message); });
log(L"Hello, World!");
```

Notice how you can add to the event as many subscribing delegates as you wish. However, there is some overhead associated with an event. If all you need is a simple callback with only a single subscribing delegate, then you can use [**winrt::delegate&lt;... T&gt;**](/uwp/cpp-ref-for-winrt/delegate) on its own.

```cppwinrt
winrt::delegate<> signalCallback;
signalCallback = [] { std::wcout << L"Hello, World!" << std::endl; };
signalCallback();

winrt::delegate<std::wstring> logCallback;
logCallback = [](std::wstring const& message) { std::wcout << message.c_str() << std::endl; }f;
logCallback(L"Hello, World!");
```

If you're porting from a C++/CX codebase where events and delegates are used internally within a project, then **winrt::delegate** will help you to replicate that pattern in C++/WinRT.

## Design guidelines

We recommend that you pass events, and not delegates, as function parameters. The **add** function of [**winrt::event**](/uwp/cpp-ref-for-winrt/event) is the one exception, because you must pass a delegate in that case. The reason for this guideline is because delegates can take different forms across different Windows Runtime languages (in terms of whether they support one client registration, or multiple). Events, with their multiple subscriber model, constitute a much more predictable and consistent option.

The signature for an event handler delegate should consist of two parameters: *sender* (**IInspectable**), and *args* (some event argument type, for example [**RoutedEventArgs**](/uwp/api/windows.ui.xaml.routedeventargs)).

Note that these guidelines don't necessarily apply if you're designing an internal API. Although, internal APIs often become public over time.

## Related topics
* [Author APIs with C++/WinRT](author-apis.md)
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Handle events by using delegates in C++/WinRT](handle-events.md)
* [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md)