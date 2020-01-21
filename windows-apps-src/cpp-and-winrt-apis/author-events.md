---
description: This topic demonstrates how to author a Windows Runtime component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events.
title: Author events in C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, author, event
ms.localizationpriority: medium
---

# Author events in C++/WinRT

This topic demonstrates how to author a Windows Runtime component containing a runtime class representing a bank account&mdash;a bank account that raises an event when its balance goes into debit. This topic also demonstrates a Core App that consumes the bank account runtime class, calls a function to adjust the balance, and handles any events that result.

> [!NOTE]
> For info about installing and using the [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Create a Windows Runtime component (BankAccountWRC)

Begin by creating a new project in Microsoft Visual Studio. Create a **Windows Runtime Component (C++/WinRT)** project, and name it *BankAccountWRC* (for "bank account Windows Runtime component"). Naming the project *BankAccountWRC* will give you the easiest experience with the rest of the steps in this topic. Don't built the project yet.

The newly-created project contains a file named `Class.idl`. Rename that file `BankAccount.idl` (renaming the `.idl` file automatically renames the dependent `.h` and `.cpp` files, too). Replace the contents of `BankAccount.idl` with the listing below.

```idl
// BankAccountWRC.idl
namespace BankAccountWRC
{
    runtimeclass BankAccount
    {
        BankAccount();
        event Windows.Foundation.EventHandler<Single> AccountIsInDebit;
        void AdjustBalance(Single value);
    };
}
```

Save the file. The project won't build to completion at the moment, but building now is a useful thing to do because it generates the source code files in which you'll implement the **BankAccount** runtime class. So go ahead and build now (the build errors you can expect to see at this stage have to do with `Class.h` and `Class.g.h` not being found).

During the build process, the `midl.exe` tool is run to create your component's Windows Runtime metadata file (which is `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd`). Then, the `cppwinrt.exe` tool is run (with the `-component` option) to generate source code files to support you in authoring your component. These files include stubs to get you started implementing the **BankAccount** runtime class that you declared in your IDL. Those stubs are `\BankAccountWRC\BankAccountWRC\Generated Files\sources\BankAccount.h` and `BankAccount.cpp`.

Right-click the project node and click **Open Folder in File Explorer**. This opens the project folder in File Explorer. There, copy the stub files `BankAccount.h` and `BankAccount.cpp` from the folder `\BankAccountWRC\BankAccountWRC\Generated Files\sources\` and into the folder that contains your project files, which is `\BankAccountWRC\BankAccountWRC\`, and replace the files in the destination. Now, let's open `BankAccount.h` and `BankAccount.cpp` and implement our runtime class. In `BankAccount.h`, add two private members to the implementation (*not* the factory implementation) of **BankAccount**.

```cppwinrt
// BankAccount.h
...
namespace winrt::BankAccountWRC::implementation
{
    struct BankAccount : BankAccountT<BankAccount>
    {
        ...

    private:
        winrt::event<Windows::Foundation::EventHandler<float>> m_accountIsInDebitEvent;
        float m_balance{ 0.f };
    };
}
...
```

As you can see above, the event is implemented in terms of the [**winrt::event**](/uwp/cpp-ref-for-winrt/event) struct template, parameterized by a particular delegate type.

In `BankAccount.cpp`, implement the functions as shown in the code example below. In C++/WinRT, an IDL-declared event is implemented as a set of overloaded functions (similar to the way a property is implemented as a pair of overloaded get and set functions). One overload takes a delegate to be registered, and returns a token. The other takes a token, and revokes the registration of the associated delegate.

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

You can also see above that the implementation of the **AdjustBalance** function raises the **AccountIsInDebit** event if the balance goes negative.

If any warnings prevent you from building, then either resolve them or set the project property **C/C++** > **General** > **Treat Warnings As Errors** to **No (/WX-)**, and build the project again.

## Create a Core App (BankAccountCoreApp) to test the Windows Runtime component

Now create a new project (either in your *BankAccountWRC* solution, or in a new one). Create a **Core App (C++/WinRT)** project, and name it *BankAccountCoreApp*.

> [!NOTE]
> As mentioned earlier, the Windows Runtime metadata file for your Windows Runtime component (whose project you named *BankAccountWRC*) is created in the folder `\BankAccountWRC\Debug\BankAccountWRC\`. The first segment of that path is the name of the folder that contains your solution file; the next segment is the subdirectory of that named `Debug`; and the last segment is the subdirectory of that named for your Windows Runtime component. If you didn't name your project *BankAccountWRC*, then your metadata file will be in the folder `\<YourProjectName>\Debug\<YourProjectName>\`.

Now, in your Core App project (*BankAccountCoreApp*), add a reference, and browse to `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd` (or add a project-to-project reference, if the two projects are in the same solution). Click **Add**, and then **OK**. Now build *BankAccountCoreApp*. In the unlikely event that you see an error that the payload file `readme.txt` doesn't exist, exclude that file from the Windows Runtime component project, rebuild it, then rebuild *BankAccountCoreApp*.

During the build process, the `cppwinrt.exe` tool is run to process the referenced `.winmd` file into source code files containing projected types to support you in consuming your component. The header for the projected types for your component's runtime classes&mdash;named `BankAccountWRC.h`&mdash;is generated into the folder `\BankAccountCoreApp\BankAccountCoreApp\Generated Files\winrt\`.

Include that header in `App.cpp`.

```cppwinrt
#include <winrt/BankAccountWRC.h>
```

Also in `App.cpp`, add the following code to instantiate a **BankAccount** (using the projected type's default constructor), register an event handler, and then cause the account to go into debit.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    BankAccountWRC::BankAccount m_bankAccount;
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

Each time you click the window, you subtract 1 from the bank account's balance. To demonstrate that the event is being raised as expected, put a breakpoint inside the lambda expression that's handling the **AccountIsInDebit** event, run the app, and click inside the window.

## Parameterized delegates, and simple signals, across an ABI

If your event must be accessible across an application binary interface (ABI)&mdash;such as between a component and its consuming application&mdash;then your event must use a Windows Runtime delegate type. The example above uses the [**Windows::Foundation::EventHandler\<T\>**](/uwp/api/windows.foundation.eventhandler) Windows Runtime delegate type. [**TypedEventHandler\<TSender, TResult\>**](/uwp/api/windows.foundation.eventhandler) is another example of a Windows Runtime delegate type.

The type parameters for those two delegate types have to cross the ABI, so the type parameters must be Windows Runtime types, too. That includes first- and third-party runtime classes, as well as primitive types such as numbers and strings. The compiler helps you with a "*must be WinRT type*" error if you forget that constraint.

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