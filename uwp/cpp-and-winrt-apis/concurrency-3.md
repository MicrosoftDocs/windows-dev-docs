---
description: Shows how you can author and consume your own completion source class.
title: A completion source sample
ms.date: 05/22/2023
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, concurrency, async, asynchronous, asynchrony, TaskCompletionSource
ms.localizationpriority: medium
---

# A completion source sample

> [!NOTE]
> As an alternative to the sample code in this topic, there's the source code for a production-ready version of a task completion source implementation in the [cpp-async](https://github.com/Microsoft/cpp-async) GitHub repo.

This topic demonstrates how you can author and consume your own completion source class, similar to .NET's [**TaskCompletionSource**](/dotnet/api/system.threading.tasks.taskcompletionsource-1).

## Source code for the **completion_source** sample

The code in the listing below is offered as a sample. Its purpose is to illustrate how you could write your own version of it. For example, support for cancellation and error propagation are outside the scope of this sample.

```cppwinrt
#include <winrt/base.h>
#include <windows.h>

template <typename T>
struct completion_source
{
    completion_source()
    {
        m_signal.attach(::CreateEvent(nullptr, true, false, nullptr));
    }

    void set(T const& value)
    {
        m_value = value;
        ::SetEvent(m_signal.get());
    }

    bool await_ready() const noexcept
    {
        return ::WaitForSingleObject(m_signal.get(), 0) == 0;
    }

    void await_suspend(std::experimental::coroutine_handle<> resume)
    {
        m_wait.attach(winrt::check_pointer(::CreateThreadpoolWait(callback, resume.address(), nullptr)));
        ::SetThreadpoolWait(m_wait.get(), m_signal.get(), nullptr);
    }

    T await_resume() const noexcept
    {
        return m_value;
    }

private:

    static void __stdcall callback(PTP_CALLBACK_INSTANCE, void* context, PTP_WAIT, TP_WAIT_RESULT) noexcept
    {
        std::experimental::coroutine_handle<>::from_address(context)();
    }

    struct wait_traits
    {
        using type = PTP_WAIT;

        static void close(type value) noexcept
        {
            ::CloseThreadpoolWait(value);
        }

        static constexpr type invalid() noexcept
        {
            return nullptr;
        }
    };

    winrt::handle m_signal;
    winrt::handle_type<wait_traits> m_wait;
    T m_value{};
};
```

## Offload completion to a separate coroutine

This section demonstrates one use case for **completion_source**. Create a new project in Visual Studio based on the **Windows Console Application (C++/WinRT)** project template, and paste the following code listing into `main.cpp` (expanding the definition of **completion_source** based on the listing in the previous section).

```cppwinrt
// main.cpp
#include "pch.h"

#include <winrt/base.h>
#include <windows.h>

template <typename T>
struct completion_source
{
    ... // Paste the listing of completion_source here.
}

using namespace std::literals;
using namespace winrt;
using namespace Windows::Foundation;

fire_and_forget CompleteAfterFiveSecondsAsync(completion_source<bool>& completionSource)
{
    co_await 5s;
    completionSource.set(true);
}

IAsyncAction CompletionSourceExample1Async()
{
    completion_source<bool> completionSource;
    CompleteAfterFiveSecondsAsync(completionSource);
    co_await completionSource;
}

int main()
{
    auto asyncAction { CompletionSourceExample1Async() };
    puts("waiting");
    asyncAction.get();
    puts("done");
}
```

## Encapsulate a **completion_source** in a class, and return a value

In this next example, a simple **App** class is used to encapsulate a **completion_source**, and return a value when it completes. Create a new project in Visual Studio based on the **Windows Console Application (C++/WinRT)** project template, and paste the following code listing into `main.cpp` (expanding the definition of **completion_source** based on the listing in the previous section).

```cppwinrt
// main.cpp
#include "pch.h"

#include <winrt/base.h>
#include <windows.h>

template <typename T>
struct completion_source
{
    ... // Paste the listing of completion_source here.
}

using namespace std::literals;
using namespace winrt;
using namespace Windows::Foundation;

struct App
{
    completion_source<winrt::hstring> m_completionSource;

    IAsyncOperation<winrt::hstring> CompletionSourceExample2Async()
    {
        co_return co_await m_completionSource;
    }

    winrt::fire_and_forget CompleteAfterFiveSecondsAsync()
    {
        co_await 5s;
        m_completionSource.set(L"Hello, World!");
    }
};

int main()
{
    App app;
    auto asyncAction{ app.CompletionSourceExample2Async() };
    app.CompleteAfterFiveSecondsAsync();
    puts("waiting");
    auto message = asyncAction.get();
    printf("%ls\n", message.c_str());
}
```

## Related topics
* [Concurrency and asynchronous operations](concurrency.md)
* [Advanced concurrency and asynchrony with C++/WinRT](concurrency-2.md)
