---
author: stevewhims
description: This topic shows the ways in which you can both create and consume Windows Runtime asynchronous objects with C++/WinRT.
title: Concurrency and asynchronous operations with C++/WinRT
ms.author: stwhi
ms.date: 10/03/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, concurrency, async, asynchronous, asynchrony
ms.localizationpriority: medium
---

# Concurrency and asynchronous operations with C++/WinRT

This topic shows the ways in which you can both create and consume Windows Runtime asynchronous objects with [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt).

## Asynchronous operations and Windows Runtime "Async" functions
Any Windows Runtime API that has the potential to take more than 50 milliseconds to complete is implemented as an asynchronous function (with a name ending in "Async"). The implementation of an asynchronous function initiates the work on another thread and returns immediately with an object that represents the asynchronous operation. When the asynchronous operation completes, that returned object contains any value that resulted from the work. The **Windows::Foundation** Windows Runtime namespace contains four types of asynchronous operation object.

- [**IAsyncAction**](/uwp/api/windows.foundation.iasyncaction),
- [**IAsyncActionWithProgress&lt;TProgress&gt;**](/uwp/api/windows.foundation.iasyncactionwithprogress_tprogress_),
- [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation_tresult_), and
- [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_).

Each of these asynchronous operation types is projected into a corresponding type in the **winrt::Windows::Foundation** C++/WinRT namespace. C++/WinRT also contains an internal await adapter struct. You don't use it directly but, thanks to that struct, you can write a `co_await` statement to cooperatively await the result of any function that returns one of these asychronous operation types. And you can author your own coroutines that return these types.

An example of an asynchronous Windows function is [**SyndicationClient::RetrieveFeedAsync**](https://docs.microsoft.com/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync), which returns an asynchronous operation object of type [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_). Let's look at some ways&mdash;blocking and non-blocking&mdash;of using C++/WinRT to call an API such as that.

## Block the calling thread
The code example below receives an asynchronous operation object from **RetrieveFeedAsync**, and it calls **get** on that object to block the calling thread until the results of the asynchronous operation are available.

```cppwinrt
// main.cpp

#include "pch.h"
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

void ProcessFeed()
{
    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;
    SyndicationFeed syndicationFeed = syndicationClient.RetrieveFeedAsync(rssFeedUri).get();
    // use syndicationFeed.
}

int main()
{
    winrt::init_apartment();
    ProcessFeed();
}
```

Calling **get** makes for convenient coding, and it's ideal for console apps or background threads where you may not want to use a coroutine for whatever reason. But it's not concurrent nor asynchronous, so it's not appropriate for a UI thread (and an assertion will fire in unoptimized builds if you attempt to use it on one). To avoid holding up OS threads from doing other useful work, we need a different technique.

## Write a coroutine
C++/WinRT integrates C++ coroutines into the programming model to provide a natural way to cooperatively wait for a result. You can produce your own Windows Runtime asynchronous operation by writing a coroutine. In the code example below, **ProcessFeedAsync** is the coroutine.

> [!NOTE]
> The **get** function exists on the C++/WinRT projection type **winrt::Windows::Foundation::IAsyncAction**, so you can call the function from within any C++/WinRT project. You will not find the function listed as a member of the [**IAsyncAction**](/uwp/api/windows.foundation.iasyncaction) interface, because **get** is not part of the application binary interface (ABI) surface of the actual Windows Runtime type **IAsyncAction**.

```cppwinrt
// main.cpp

#include "pch.h"
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>
#include <iostream>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

void PrintFeed(SyndicationFeed const& syndicationFeed)
{
    for (SyndicationItem const& syndicationItem : syndicationFeed.Items())
    {
        std::wcout << syndicationItem.Title().Text().c_str() << std::endl;
    }
}

IAsyncAction ProcessFeedAsync()
{
    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;
    SyndicationFeed syndicationFeed{ co_await syndicationClient.RetrieveFeedAsync(rssFeedUri) };
    PrintFeed(syndicationFeed);
}

int main()
{
    winrt::init_apartment();

    auto processOp = ProcessFeedAsync();
    // do other work while the feed is being printed.
    processOp.get(); // no more work to do; call get() so that we see the printout before the application exits.
}
```

A coroutine is a function that can be suspended and resumed. In the **ProcessFeedAsync** coroutine above, when the `co_await` statement is reached, the coroutine asynchronously initiates the **RetrieveFeedAsync** call and then it immediately suspends itself and returns control back to the caller (which is **main** in the example above). **main** can then continue to do work while the feed is being retrieved and printed. When that's done (when the **RetrieveFeedAsync** call completes), the **ProcessFeedAsync** coroutine resumes at the next statement.

You can aggregate a coroutine into other coroutines. Or you can call **get** to block and wait for it to complete (and get the result if there is one). Or you can pass it to another programming language that supports the Windows Runtime.

It's also possible to handle the completed and/or progress events of asynchronous actions and operations by using delegates. For details, and code examples, see [Delegate types for asynchronous actions and operations](handle-events.md#delegate-types-for-asynchronous-actions-and-operations).

## Asychronously return a Windows Runtime type
In this next example we wrap a call to **RetrieveFeedAsync**, for a specific URI, to give us a **RetrieveBlogFeedAsync** function that asynchronously returns a [**SyndicationFeed**](/uwp/api/windows.web.syndication.syndicationfeed).

```cppwinrt
// main.cpp

#include "pch.h"
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>
#include <iostream>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

void PrintFeed(SyndicationFeed const& syndicationFeed)
{
    for (SyndicationItem const& syndicationItem : syndicationFeed.Items())
    {
        std::wcout << syndicationItem.Title().Text().c_str() << std::endl;
    }
}

IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress> RetrieveBlogFeedAsync()
{
    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;
    return syndicationClient.RetrieveFeedAsync(rssFeedUri);
}

int main()
{
    winrt::init_apartment();

    auto feedOp = RetrieveBlogFeedAsync();
    // do other work.
    PrintFeed(feedOp.get());
}
```

In the example above, **RetrieveBlogFeedAsync** returns an **IAsyncOperationWithProgress**, which has both progress and a return value. We can do other work while **RetrieveBlogFeedAsync** is doing its thing and retrieving the feed. Then, we call **get** on that asynchronous operation object to block, wait for it to complete, and then obtain the results of the operation.

If you're asynchronously returning a Windows Runtime type, then you should return an [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation_tresult_) or an [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_). Any first- or third-party runtime class qualifies, or any type that can be passed to or from a Windows Runtime function (for example, `int`, or **winrt::hstring**). The compiler will help you with a "*must be WinRT type*" error if you try to use one of these asychronous operation types with a non-Windows Runtime type.

If a coroutine doesn't have at least one `co_await` statement then, in order to qualify as a coroutine, it must have at least one `co_return` or one `co_yield` statement. There will be cases where your coroutine can return a value without introducing any asynchrony, and therefore without blocking nor switching context. Here's an example that does that (the second and subsequent times it's called) by caching a value.

```cppwinrt
winrt::hstring m_cache;
 
IAsyncOperation<winrt::hstring> ReadAsync()
{
    if (m_cache.empty())
    {
        // Asynchronously download and cache the string.
    }
    co_return m_cache;
}
``` 

## Asychronously return a non-Windows-Runtime type
If you're asynchronously returning a type that's *not* a Windows Runtime type, then you should return a Parallel Patterns Library (PPL) [**concurrency::task**](/cpp/parallel/concrt/reference/task-class). We recommend **concurrency::task** because it gives you better performance (and better compatibility going forward) than **std::future** does.

> [!TIP]
> If you include `<pplawait.h>`, then you can use **concurrency::task** as a coroutine type.

```cppwinrt
// main.cpp

#include "pch.h"
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>
#include <iostream>
#include <ppltasks.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

concurrency::task<std::wstring> RetrieveFirstTitleAsync()
{
    return concurrency::create_task([]
    {
        Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
        SyndicationClient syndicationClient;
        SyndicationFeed syndicationFeed = syndicationClient.RetrieveFeedAsync(rssFeedUri).get();
        return std::wstring{ syndicationFeed.Items().GetAt(0).Title().Text() };
    });
}

int main()
{
    winrt::init_apartment();

    auto firstTitleOp = RetrieveFirstTitleAsync();
    // Do other work here.
    std::wcout << firstTitleOp.get() << std::endl;
}
```

## Parameter-passing
For synchronous functions, you should use `const&` parameters by default. That will avoid the overhead of copies (which involve reference counting, and that means interlocked increments and decrements).

```cppwinrt
// Synchronous function.
void DoWork(Param const& value);
```

But you can run into problems if you pass a reference parameter to a coroutine.

```cppwinrt
// NOT the recommended way to pass a value to a coroutine!
IASyncAction DoWorkAsync(Param const& value)
{
	// While it's ok to access value here...
	
	co_await DoOtherWorkAsync();

	// ...accessing value here carries no guarantees of safety.
}
```

In a coroutine, execution is synchronous up until the first suspension point, where control is returned to the caller. By the time the coroutine resumes, anything might have happened to the source value that a reference parameter references. From the coroutine's perspective, a reference parameter has uncontrolled lifetime. So, in the example above, we're safe to access *value* up until the `co_await`, but not after it. Nor can we safely pass *value* to **DoOtherWorkAsync** if there's any risk that that function will in turn suspend and then try to use *value* after it resumes. To make parameters safe to use after suspending and resuming, your coroutines should use pass-by-value by default to ensure that they capture by value and avoid lifetime issues. Cases when you can deviate from that guidance because you're certain that it's safe to do so are going to be rare.

```cppwinrt
// Coroutine
IASyncAction DoWorkAsync(Param value);
```

It's also arguable that (unless you want to move the value) passing by const value is good practice. It won't have any effect on the source value from which you're making a copy, but it makes the intent clear, and helps if you inadvertently modify the copy.
	
```cppwinrt
// coroutine with strictly unnecessary const (but arguably good practice).
IASyncAction DoWorkAsync(Param const value);
```

Also see [Standard arrays and vectors](std-cpp-data-types.md#standard-arrays-and-vectors), which deals with how to pass a standard vector into an asynchronous callee.

## Offloading work onto the Windows thread pool
Before you do compute-bound work in a coroutine, you need to return execution to the caller so that the caller isn't blocked (in other words, introduce a suspension point). If you're not already doing that by `co-await`-ing some other operation, then you can `co-await` the **winrt::resume_background** function. That returns control to the caller, and then immediately resumes execution on a thread pool thread.

The thread pool being used in the implementation is the low-level [Windows thread pool](https://msdn.microsoft.com/library/windows/desktop/ms686766), so it's optimially efficient.

```cppwinrt
IAsyncOperation<uint32_t> DoWorkOnThreadPoolAsync()
{
    co_await winrt::resume_background(); // Return control; resume on thread pool.

    uint32_t result;
    for (uint32_t y = 0; y < height; ++y)
    for (uint32_t x = 0; x < width; ++x)
    {
        // Do compute-bound work here.
    }
    co_return result;
}
```

## Programming with thread affinity in mind
This scenario expands on the previous one. You offload some work onto the thread pool, but then you want to display progress in the user interface (UI).

```cppwinrt
IAsyncAction DoWorkAsync(TextBlock textblock)
{
    co_await winrt::resume_background();
    // Do compute-bound work here.

    textblock.Text(L"Done!"); // Error: TextBlock has thread affinity.
}
```

The code above throws a [**winrt::hresult_wrong_thread**](/uwp/cpp-ref-for-winrt/hresult-wrong-thread) exception, because a **TextBlock** must be updated from the thread that created it, which is the UI thread. One solution is to capture the thread context within which our coroutine was originally called. Instantiate a **winrt::apartment_context** object, and then `co_await` it.

```cppwinrt
IAsyncAction DoWorkAsync(TextBlock textblock)
{
    winrt::apartment_context ui_thread; // Capture calling context.

    co_await winrt::resume_background();
    // Do compute-bound work here.

    co_await ui_thread; // Switch back to calling context.

    textblock.Text(L"Done!"); // Ok if we really were called from the UI thread.
}
```

As long as the coroutine above is called from the UI thread that created the **TextBlock**, then this technique works. There will be many cases in your app where you're certain of that.

For a more general solution to updating UI, which covers cases where you're uncertain about the calling thread, you can `co-await` the **winrt::resume_foreground** function to switch to a specific foreground thread. In the code example below, we specify the foreground thread by passing the dispatcher object associated with the **TextBlock** (by accessing its [**Dispatcher**](/uwp/api/windows.ui.xaml.dependencyobject.dispatcher#Windows_UI_Xaml_DependencyObject_Dispatcher) property). The implementation of **winrt::resume_foreground** calls [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) on that dispatcher object to execute the work that comes after it in the coroutine.

```cppwinrt
IAsyncAction DoWorkAsync(TextBlock textblock)
{
    co_await winrt::resume_background();
    // Do compute-bound work here.

    co_await winrt::resume_foreground(textblock.Dispatcher()); // Switch to the foreground thread associated with textblock.

    textblock.Text(L"Done!"); // Guaranteed to work.
}
```

## Canceling an asychronous operation, and cancellation callbacks

The Windows Runtime's features for asynchronous programming allow you to cancel an in-flight asynchronous action or operation. Let's begin with a simple example.

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

IAsyncAction ImplicitCancellationAsync()
{
    while (true)
    {
        std::cout << "ImplicitCancellationAsync: do some work for 1 second" << std::endl;
        co_await 1s;
    }
}

IAsyncAction MainCoroutineAsync()
{
    auto implicit_cancellation{ ImplicitCancellationAsync() };
    co_await 3s;
    implicit_cancellation.Cancel();
}

int main()
{
    winrt::init_apartment();
    MainCoroutineAsync().get();
}
```

If you run the example above, then you'll see **ImplicitCancellationAsync** print one message per second for three seconds, after which time it automatically terminates as a result of being canceled. This works because, on encountering a `co_await` expression, a coroutine checks whether it has been cancelled. If it has, then it short-circuits out; and if it hasn't, then it suspends as normal.

Cancellation can, of course, happen while the coroutine is suspended. Only when the coroutine resumes, or hits another `co_await`, will it check for cancellation. The issue is one of potentially too-coarse-grained latency in responding to cancellation.

So, another option is to explicitly poll for cancellation from within your coroutine. Update the example above with the code in the listing below. In this new example, **ExplicitCancellationAsync** retrieves the object returned by the [**winrt::get_cancellation_token**](/uwp/cpp-ref-for-winrt/get-cancellation-token) function, and uses it to periodically check whether the coroutine has been canceled. As long as it's not canceled, the coroutine loops indefinitely; once it is canceled, the loop and the function exit normally. The outcome is the same as the previous example, but here exiting happens explicitly, and under control.

```cppwinrt
...
IAsyncAction ExplicitCancellationAsync()
{
    auto cancellation_token{ co_await winrt::get_cancellation_token() };

    while (!cancellation_token())
    {
        std::cout << "ExplicitCancellationAsync: do some work for 1 second" << std::endl;
        co_await 1s;
    }
}

IAsyncAction MainCoroutineAsync()
{
    auto explicit_cancellation{ ExplicitCancellationAsync() };
    co_await 3s;
    explicit_cancellation.Cancel();
}
...
```

Waiting on **winrt::get_cancellation_token** retrieves a cancellation token with knowledge of the **IAsyncAction** that the coroutine is producing on your behalf. You can use the function call operator on that token to query the cancellation state&mdash;essentially polling for cancellation. If you're performing some compute-bound operation, or iterating through a large collection, then this is a reasonable technique.

### Register a cancellation callback
The Windows Runtime's cancellation doesn't automatically flow to other asynchronous objects. But&mdash;introduced in version 10.0.17763.0 (Windows 10, version 1809) of the Windows SDK&mdash;you can register a cancellation callback. This is a pre-emptive hook by which cancellation can be propagated, and makes it possible to integrate with existing concurrency libraries.

In this next code example, **NestedCoroutineAsync** does the work, but it has no special cancellation logic in it. **CancellationPropagatorAsync** is essentially a wrapper on the nested coroutine; the wrapper forwards cancellation pre-emptively.

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

IAsyncAction NestedCoroutineAsync()
{
    while (true)
    {
        std::cout << "NestedCoroutineAsync: do some work for 1 second" << std::endl;
        co_await 1s;
    }
}

IAsyncAction CancellationPropagatorAsync()
{
    auto cancellation_token{ co_await winrt::get_cancellation_token() };
    auto nested_coroutine{ NestedCoroutineAsync() };

    cancellation_token.callback([&]
    {
        nested_coroutine.Cancel();
    });

    co_await nested_coroutine;
}

IAsyncAction MainCoroutineAsync()
{
    auto cancellation_propagator{ CancellationPropagatorAsync() };
    co_await 3s;
    cancellation_propagator.Cancel();
}

int main()
{
    winrt::init_apartment();
    MainCoroutineAsync().get();
}
```

**CancellationPropagatorAsync** registers a lambda function for its own cancellation callback, and then it awaits (it suspends) until the nested work completes. When or if **CancellationPropagatorAsync** is canceled, it propagates the cancellation to the nested coroutine. There's no need to poll for cancellation; nor is cancellation blocked indefinitely. This mechanism is flexible enough for you to use it to interop with a coroutine or concurrency library that knows nothing of C++/WinRT.

## Reporting progress

If your coroutine returns either [**IAsyncActionWithProgress**](/uwp/api/windows.foundation.iasyncactionwithprogress_tprogress_), or [**IAsyncOperationWithProgress**](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_), then you can retrieve the object returned by the [**winrt::get_progress_token**](/uwp/cpp-ref-for-winrt/get-progress-token) function, and use it to report progress back to a progress handler. Here's a code example.

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

IAsyncOperationWithProgress<double, double> CalcPiTo5DPs()
{
    auto progress{ co_await winrt::get_progress_token() };

    co_await 1s;
    double pi_so_far{ 3.1 };
    progress(0.2);

    co_await 1s;
    pi_so_far += 4.e-2;
    progress(0.4);

    co_await 1s;
    pi_so_far += 1.e-3;
    progress(0.6);

    co_await 1s;
    pi_so_far += 5.e-4;
    progress(0.8);

    co_await 1s;
    pi_so_far += 9.e-5;
    progress(1.0);
    co_return pi_so_far;
}

IAsyncAction DoMath()
{
    auto async_op_with_progress{ CalcPiTo5DPs() };
    async_op_with_progress.Progress([](auto const& /* sender */, double progress)
    {
        std::wcout << L"CalcPiTo5DPs() reports progress: " << progress << std::endl;
    });
    double pi{ co_await async_op_with_progress };
    std::wcout << L"CalcPiTo5DPs() is complete !" << std::endl;
    std::wcout << L"Pi is approx.: " << pi << std::endl;
}

int main()
{
    winrt::init_apartment();
    DoMath().get();
}
```

> [!NOTE]
> It's not correct to implement more than one *completion handler* for an asynchronous action or operation. You can have either a single delegate for its completed event, or you can `co_await` it. If you have both, then the second will fail. Either one of the following two kinds of completion handlers is appropriate; not both for the same async object.

```cppwinrt
auto async_op_with_progress{ CalcPiTo5DPs() };
async_op_with_progress.Completed([](auto const& sender, AsyncStatus /* status */)
{
    double pi{ sender.GetResults() };
});
```

```cppwinrt
auto async_op_with_progress{ CalcPiTo5DPs() };
double pi{ co_await async_op_with_progress };
```

For more info about completion handlers, see [Delegate types for asynchronous actions and operations](handle-events.md#delegate-types-for-asynchronous-actions-and-operations).

## Fire and forget

Sometimes, you have a task that can be done concurrently with other work, and you don't need to wait for that task to complete (no other work depends on it), nor do you need it to return a value. In that case, you can fire off the task and forget it. You can do that by writing a coroutine whose return type is [**winrt::fire_and_forget**](/uwp/cpp-ref-for-winrt/fire-and-forget) (instead of one of the Windows Runtime asynchronous operation types, or **concurrency::task**).

```cppwinrt
// pch.h
#pragma once
#include <winrt/Windows.Foundation.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"
using namespace winrt;
using namespace std::chrono_literals;

winrt::fire_and_forget CompleteInFiveSeconds()
{
    co_await 5s;
}

int main()
{
    winrt::init_apartment();
    CompleteInFiveSeconds();
    // Do other work here.
}
```

## Important APIs
* [concurrency::task class](/cpp/parallel/concrt/reference/task-class)
* [IAsyncAction interface](/uwp/api/windows.foundation.iasyncaction)
* [IAsyncActionWithProgress&lt;TProgress&gt; interface](/uwp/api/windows.foundation.iasyncactionwithprogress_tprogress_)
* [IAsyncOperation&lt;TResult&gt; interface](/uwp/api/windows.foundation.iasyncoperation_tresult_)
* [IAsyncOperationWithProgress&lt;TResult, TProgress&gt; interface](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_)
* [SyndicationClient::RetrieveFeedAsync method](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync)
* [SyndicationFeed class](/uwp/api/windows.web.syndication.syndicationfeed)
* [winrt::get_cancellation_token](/uwp/cpp-ref-for-winrt/get-cancellation-token)
* [winrt::get_progress_token](/uwp/cpp-ref-for-winrt/get-progress-token)
* [winrt::fire_and_forget](/uwp/cpp-ref-for-winrt/fire-and-forget)

## Related topics
* [Handle events by using delegates in C++/WinRT](handle-events.md)
* [Standard C++ data types and C++/WinRT](std-cpp-data-types.md)