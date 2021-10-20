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
- Update the thermometer runtime class to raise an event when its temperature goes below freezing.
- Update the Core App that consumes the thermometer runtime class so that it handles that event.

> [!NOTE]
> For info about installing and using the [C++/WinRT](./intro-to-using-cpp-with-winrt.md) Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Create **ThermometerWRC** and **ThermometerCoreApp**

If you want to follow along with the updates shown in this topic, so that you can build and run the code, then the first step is to follow the walkthrough in the [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md) topic. By doing so, you'll have the **ThermometerWRC** Windows Runtime component, and the **ThermometerCoreApp** Core App that consumes it.

## Update **ThermometerWRC** to raise an event

Update `Thermometer.idl` to look like the listing below. This is how to declare an event whose delegate type is [**EventHandler**](/uwp/api/windows.foundation.eventhandler-1) with an argument of a single-precision floating-point number.

```idl
// Thermometer.idl
namespace ThermometerWRC
{
    runtimeclass Thermometer
    {
        Thermometer();
        void AdjustTemperature(Single deltaFahrenheit);
        event Windows.Foundation.EventHandler<Single> TemperatureIsBelowFreezing;
    };
}
```

Save the file. The project won't build to completion in its current state, but perform a build now in any case to generate updated versions of the `\ThermometerWRC\ThermometerWRC\Generated Files\sources\Thermometer.h` and `Thermometer.cpp` stub files. Inside those files you can now see stub implementations of the **TemperatureIsBelowFreezing** event. In C++/WinRT, an IDL-declared event is implemented as a set of overloaded functions (similar to the way a property is implemented as a pair of overloaded get and set functions). One overload takes a delegate to be registered, and returns a token (a [**winrt::event_token**](/uwp/cpp-ref-for-winrt/event-token)). The other takes a token, and revokes the registration of the associated delegate.

Now open `Thermometer.h` and `Thermometer.cpp`, and update the implementation of the **Thermometer** runtime class. In `Thermometer.h`, add the two overloaded **TemperatureIsBelowFreezing** functions, as well as a private event data member to use in the implementation of those functions.

```cppwinrt
// Thermometer.h
...
namespace winrt::ThermometerWRC::implementation
{
    struct Thermometer : ThermometerT<Thermometer>
    {
        ...
        winrt::event_token TemperatureIsBelowFreezing(Windows::Foundation::EventHandler<float> const& handler);
        void TemperatureIsBelowFreezing(winrt::event_token const& token) noexcept;

    private:
        winrt::event<Windows::Foundation::EventHandler<float>> m_temperatureIsBelowFreezingEvent;
        ...
    };
}
...
```

As you can see above, an event is represented by the [**winrt::event**](/uwp/cpp-ref-for-winrt/event) struct template, parameterized by a particular delegate type (which itself can be parameterized by an args type).

In `Thermometer.cpp`, implement the two overloaded **TemperatureIsBelowFreezing** functions.

```cppwinrt
// Thermometer.cpp
...
namespace winrt::ThermometerWRC::implementation
{
    winrt::event_token Thermometer::TemperatureIsBelowFreezing(Windows::Foundation::EventHandler<float> const& handler)
    {
        return m_temperatureIsBelowFreezingEvent.add(handler);
    }

    void Thermometer::TemperatureIsBelowFreezing(winrt::event_token const& token) noexcept
    {
        m_temperatureIsBelowFreezingEvent.remove(token);
    }

    void Thermometer::AdjustTemperature(float deltaFahrenheit)
    {
        m_temperatureFahrenheit += deltaFahrenheit;
        if (m_temperatureFahrenheit < 32.f) m_temperatureIsBelowFreezingEvent(*this, m_temperatureFahrenheit);
    }
}
```

> [!NOTE]
> For details of what an auto event revoker is, see [Revoke a registered delegate](handle-events.md#revoke-a-registered-delegate). You get auto event revoker implementation for free for your event. In other words, you don't need to implement the overload for the event revoker&mdash;that's provided for you by the C++/WinRT projection.

The other overloads (the registration and manual revocation overloads) are *not* baked into the projection. That's to give you the flexibility to implement them optimally for your scenario. Calling [**event::add**](/uwp/cpp-ref-for-winrt/event#eventadd-function) and [**event::remove**](/uwp/cpp-ref-for-winrt/event#eventremove-function) as shown in these implementations is an efficient and concurrency/thread-safe default. But if you have a very large number of events, then you may not want an event field for each, but rather opt for some kind of sparse implementation instead.

You can also see above that the implementation of the **AdjustTemperature** function has been updated to raise the **TemperatureIsBelowFreezing** event if the temperature goes below freezing.

## Update **ThermometerCoreApp** to handle the event

In the **ThermometerCoreApp** project, in `App.cpp`, make the following changes to the code to register an event handler, and then cause the temperature to go below freezing.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    winrt::event_token m_eventToken;
    ...
    
    void Initialize(CoreApplicationView const &)
    {
        m_eventToken = m_thermometer.TemperatureIsBelowFreezing([](const auto &, float temperatureFahrenheit)
        {
            WINRT_ASSERT(temperatureFahrenheit < 32.f); // Put a breakpoint here.
        });
    }
    ...

    void Uninitialize()
    {
        m_thermometer.TemperatureIsBelowFreezing(m_eventToken);
    }
    ...
    
    void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
    {
        m_thermometer.AdjustTemperature(-1.f);
        ...
    }
    ...
};
```

Be aware of the change to the **OnPointerPressed** method. Now, each time you click the window, you *subtract* 1 degree Fahrenheit
from the thermometer's temperature. And now, the app is handling the event that's raised when the temperature goes below freezing. To demonstrate that the event is being raised as expected, put a breakpoint inside the lambda expression that's handling the **TemperatureIsBelowFreezing** event, run the app, and click inside the window.

## Parameterized delegates across an ABI

If your event must be accessible across an application binary interface (ABI)&mdash;such as between a component and its consuming application&mdash;then your event must use a Windows Runtime delegate type. The example above uses the [**Windows::Foundation::EventHandler\<T\>**](/uwp/api/windows.foundation.eventhandler) Windows Runtime delegate type. [**TypedEventHandler\<TSender, TResult\>**](/uwp/api/windows.foundation.eventhandler) is another example of a Windows Runtime delegate type.

The type parameters for those two delegate types have to cross the ABI, so the type parameters must be Windows Runtime types, too. That includes Windows runtime classes, third-party runtime classes, and primitive types such as numbers and strings. The compiler helps you with a "*T must be WinRT type*" error if you forget that constraint.

Below is an example in the form of code listings. Begin with the **ThermometerWRC** and **ThermometerCoreApp** projects that you created earlier in this topic, and edit the code in those projects to look like the code in these listings.

This first listing is for the **ThermometerWRC** project. After editing `ThermometerWRC.idl` as shown below, build the project, and then copy `MyEventArgs.h` and `.cpp` into the project (from the `Generated Files` folder) just like you did earlier with `Thermometer.h` and `.cpp`. Remember to delete the `static_assert` from both files.

```cppwinrt
// ThermometerWRC.idl
namespace ThermometerWRC
{
    [default_interface]
    runtimeclass MyEventArgs
    {
        Single TemperatureFahrenheit{ get; };
    }

    [default_interface]
    runtimeclass Thermometer
    {
        ...
        event Windows.Foundation.EventHandler<ThermometerWRC.MyEventArgs> TemperatureIsBelowFreezing;
        ...
    };
}

// MyEventArgs.h
#pragma once
#include "MyEventArgs.g.h"

namespace winrt::ThermometerWRC::implementation
{
    struct MyEventArgs : MyEventArgsT<MyEventArgs>
    {
        MyEventArgs() = default;
        MyEventArgs(float temperatureFahrenheit);
        float TemperatureFahrenheit();

    private:
        float m_temperatureFahrenheit{ 0.f };
    };
}

// MyEventArgs.cpp
#include "pch.h"
#include "MyEventArgs.h"
#include "MyEventArgs.g.cpp"

namespace winrt::ThermometerWRC::implementation
{
    MyEventArgs::MyEventArgs(float temperatureFahrenheit) : m_temperatureFahrenheit(temperatureFahrenheit)
    {
    }

    float MyEventArgs::TemperatureFahrenheit()
    {
        return m_temperatureFahrenheit;
    }
}

// Thermometer.h
...
struct Thermometer : ThermometerT<Thermometer>
{
...
    winrt::event_token TemperatureIsBelowFreezing(Windows::Foundation::EventHandler<ThermometerWRC::MyEventArgs> const& handler);
...
private:
    winrt::event<Windows::Foundation::EventHandler<ThermometerWRC::MyEventArgs>> m_temperatureIsBelowFreezingEvent;
...
}
...

// Thermometer.cpp
#include "MyEventArgs.h"
...
winrt::event_token Thermometer::TemperatureIsBelowFreezing(Windows::Foundation::EventHandler<ThermometerWRC::MyEventArgs> const& handler) { ... }
...
void Thermometer::AdjustTemperature(float deltaFahrenheit)
{
    m_temperatureFahrenheit += deltaFahrenheit;

    if (m_temperatureFahrenheit < 32.f)
    {
        auto args = winrt::make_self<winrt::ThermometerWRC::implementation::MyEventArgs>(m_temperatureFahrenheit);
        m_temperatureIsBelowFreezingEvent(*this, *args);
    }
}
...
```

This listing is for the **ThermometerCoreApp** project.

```cppwinrt
// App.cpp
...
void Initialize(CoreApplicationView const&)
{
    m_eventToken = m_thermometer.TemperatureIsBelowFreezing([](const auto&, ThermometerWRC::MyEventArgs args)
    {
        float degrees = args.TemperatureFahrenheit();
        WINRT_ASSERT(degrees < 32.f); // Put a breakpoint here.
    });
}
...
```

## Simple signals across an ABI

If you don't need to pass any parameters or arguments with your event, then you can define your own simple Windows Runtime delegate type. The example below shows a simpler version of the **Thermometer** runtime class. It declares a delegate type named **SignalDelegate** and then it uses that to raise a signal-type event instead of an event with a parameter.

```idl
// ThermometerWRC.idl
namespace ThermometerWRC
{
    delegate void SignalDelegate();

    runtimeclass Thermometer
    {
        Thermometer();
        event ThermometerWRC.SignalDelegate SignalTemperatureIsBelowFreezing;
        void AdjustTemperature(Single value);
    };
}
```

```cppwinrt
// Thermometer.h
...
namespace winrt::ThermometerWRC::implementation
{
    struct Thermometer : ThermometerT<Thermometer>
    {
        ...

        winrt::event_token SignalTemperatureIsBelowFreezing(ThermometerWRC::SignalDelegate const& handler);
        void SignalTemperatureIsBelowFreezing(winrt::event_token const& token);
        void AdjustTemperature(float deltaFahrenheit);

    private:
        winrt::event<ThermometerWRC::SignalDelegate> m_signal;
        float m_temperatureFahrenheit{ 0.f };
    };
}
```

```cppwinrt
// Thermometer.cpp
...
namespace winrt::ThermometerWRC::implementation
{
    winrt::event_token Thermometer::SignalTemperatureIsBelowFreezing(ThermometerWRC::SignalDelegate const& handler)
    {
        return m_signal.add(handler);
    }

    void Thermometer::SignalTemperatureIsBelowFreezing(winrt::event_token const& token)
    {
        m_signal.remove(token);
    }

    void Thermometer::AdjustTemperature(float deltaFahrenheit)
    {
        m_temperatureFahrenheit += deltaFahrenheit;
        if (m_temperatureFahrenheit < 32.f)
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
    ThermometerWRC::Thermometer m_thermometer;
    winrt::event_token m_eventToken;
    ...
    
    void Initialize(CoreApplicationView const &)
    {
        m_eventToken = m_thermometer.SignalTemperatureIsBelowFreezing([] { /* ... */ });
    }
    ...

    void Uninitialize()
    {
        m_thermometer.SignalTemperatureIsBelowFreezing(m_eventToken);
    }
    ...

    void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
    {
        m_thermometer.AdjustTemperature(-1.f);
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

## Deferrable events

A common pattern in the Windows Runtime is the deferrable event. An event handler *takes a deferral* by calling the event argument's **GetDeferral** method. Doing so indicates to the event source that post-event activities should be postponed until the deferral is completed. This allows an event handler to perform asynchronous actions in response to an event.

The [**winrt::deferrable_event_args**](/uwp/cpp-ref-for-winrt/deferrable-event-args) struct template is a helper class for implementing (producing) the Windows Runtime deferral pattern. Here's an example.

```cppwinrt
// Widget.idl
namespace Sample
{
    runtimeclass WidgetStartingEventArgs
    {
        Windows.Foundation.Deferral GetDeferral();
        Boolean Cancel;
    };

    runtimeclass Widget
    {
        event Windows.Foundation.TypedEventHandler<
            Widget, WidgetStartingEventArgs> Starting;
    };
}

// Widget.h
namespace winrt::Sample::implementation
{
    struct Widget : WidgetT<Widget>
    {
        Widget() = default;

        event_token Starting(Windows::Foundation::TypedEventHandler<
            Sample::Widget, Sample::WidgetStartingEventArgs> const& handler)
        {
            return m_starting.add(handler);
        }
        void Starting(event_token const& token) noexcept
        {
            m_starting.remove(token);
        }

    private:
        event<Windows::Foundation::TypedEventHandler<
            Sample::Widget, Sample::WidgetStartingEventArgs>> m_starting;
    };

    struct WidgetStartingEventArgs : WidgetStartingEventArgsT<WidgetStartingEventArgs>,
                                     deferrable_event_args<WidgetStartingEventArgs>
    //                               ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    {
        bool Cancel() const noexcept { return m_cancel; }
        void Cancel(bool value) noexcept { m_cancel = value; }
        bool m_cancel = false;
    };
}
```

Here's how the event recipient consumes the deferrable event pattern.

```cppwinrt
// EventRecipient.h
widget.Starting([](auto sender, auto args) -> fire_and_forget
{
    auto deferral = args.GetDeferral();
    if (!co_await CanWidgetStartAsync(sender))
    {
        // Do not allow the widget to start.
        args.Cancel(true);
    }
    deferral.Complete();
});
```

As the implementor (producer) of the event source, you derive your event args class from [**winrt::deferrable_event_args**](/uwp/cpp-ref-for-winrt/deferrable-event-args). **deferrable_event_args\<T\>** implements **T::GetDeferral** for you. It also exposes a new helper method [**deferrable_event_args::wait_for_deferrals**](/uwp/cpp-ref-for-winrt/deferrable-event-args#deferrable_event_argswait_for_deferrals-function), which completes when all outstanding deferrals have completed (if no deferrals were taken, then it completes immediately).

```cppwinrt
// Widget.h
IAsyncOperation<bool> TryStartWidget(Widget const& widget)
{
    auto args = make_self<WidgetStartingEventArgs>();
    // Raise the event to let people know that the widget is starting
    // and give them a chance to prevent it.
    m_starting(widget, *args);
    // Wait for deferrals to complete.
    co_await args->wait_for_deferrals();
    // Use the results.
    bool started = false;
    if (!args->Cancel())
    {
        widget.InsertBattery();
        widget.FlipPowerSwitch();
        started = true;
    }
    co_return started;
}
```

## Design guidelines

We recommend that you pass events, and not delegates, as function parameters. The **add** function of [**winrt::event**](/uwp/cpp-ref-for-winrt/event) is the one exception, because you must pass a delegate in that case. The reason for this guideline is because delegates can take different forms across different Windows Runtime languages (in terms of whether they support one client registration, or multiple). Events, with their multiple subscriber model, constitute a much more predictable and consistent option.

The signature for an event handler delegate should consist of two parameters: *sender* (**IInspectable**), and *args* (some event argument type, for example [**RoutedEventArgs**](/uwp/api/windows.ui.xaml.routedeventargs)).

Note that these guidelines don't necessarily apply if you're designing an internal API. Although, internal APIs often become public over time.

## Related topics
* [Author APIs with C++/WinRT](author-apis.md)
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Handle events by using delegates in C++/WinRT](handle-events.md)
* [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md)