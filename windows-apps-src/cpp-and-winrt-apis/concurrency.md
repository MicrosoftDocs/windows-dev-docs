---
description: This topic shows the ways in which you can both create and consume Windows Runtime asynchronous objects with C++/WinRT.
title: Concurrency and asynchronous operations with C++/WinRT
ms.date: 07/08/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, concurrency, async, asynchronous, asynchrony
ms.localizationpriority: medium
---

# Concurrency and asynchronous operations with C++/WinRT

> [!IMPORTANT]
> This topic introduces the concepts of *coroutines* and `co_await`, which we recommend that you use in both your UI *and* in your non-UI applications. For simplicity, most of the code examples in this introductory topic show **Windows Console Application (C++/WinRT)** projects. The later code examples in this topic do use coroutines, but for convenience the console application examples also continue to use the blocking **get** function call just before exiting, so that the application doesn't exit before finishing printing its output. You won't do that (call the blocking **get** function) from a UI thread. Instead, you'll use the `co_await` statement. The techniques that you'll use in your UI applications are described in the topic [More advanced concurrency and asynchrony](concurrency-2.md).

This introductory topic shows some of the ways in which you can both create and consume Windows Runtime asynchronous objects with [C++/WinRT](./intro-to-using-cpp-with-winrt.md). After reading this topic, especially for techniques you'll use in your UI applications, also see [More advanced concurrency and asynchrony](concurrency-2.md).

## Asynchronous operations and Windows Runtime "Async" functions

Any Windows Runtime API that has the potential to take more than 50 milliseconds to complete is implemented as an asynchronous function (with a name ending in "Async"). The implementation of an asynchronous function initiates the work on another thread, and returns immediately with an object that represents the asynchronous operation. When the asynchronous operation completes, that returned object contains any value that resulted from the work. The **Windows::Foundation** Windows Runtime namespace contains four types of asynchronous operation object.

- [**IAsyncAction**](/uwp/api/windows.foundation.iasyncaction),
- [**IAsyncActionWithProgress&lt;TProgress&gt;**](/uwp/api/windows.foundation.iasyncactionwithprogress-1),
- [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation-1), and
- [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress-2).

Each of these asynchronous operation types is projected into a corresponding type in the **winrt::Windows::Foundation** C++/WinRT namespace. C++/WinRT also contains an internal await adapter struct. You don't use it directly but, thanks to that struct, you can write a `co_await` statement to cooperatively await the result of any function that returns one of these asynchronous operation types. And you can author your own coroutines that return these types.

An example of an asynchronous Windows function is [**SyndicationClient::RetrieveFeedAsync**](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync), which returns an asynchronous operation object of type [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress-2).

Let's look at some ways&mdash;first blocking, and then non-blocking&mdash;of using C++/WinRT to call an API such as that. Just for illustration of the basic ideas, we'll be using a **Windows Console Application (C++/WinRT)** project in the next few code examples. Techniques that are more appropriate for a UI application are discussed in [More advanced concurrency and asynchrony](concurrency-2.md).

## Block the calling thread

The code example below receives an asynchronous operation object from **RetrieveFeedAsync**, and it calls **get** on that object to block the calling thread until the results of the asynchronous operation are available.

If you want to copy-paste this example directly into the main source code file of a **Windows Console Application (C++/WinRT)** project, then first set **Not Using Precompiled Headers** in project properties.

```cppwinrt
// main.cpp
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

void ProcessFeed()
{
    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;
    SyndicationFeed syndicationFeed{ syndicationClient.RetrieveFeedAsync(rssFeedUri).get() };
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
#include <iostream>
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.Web.Syndication.h>

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

    auto processOp{ ProcessFeedAsync() };
    // do other work while the feed is being printed.
    processOp.get(); // no more work to do; call get() so that we see the printout before the application exits.
}
```

A coroutine is a function that can be suspended and resumed. In the **ProcessFeedAsync** coroutine above, when the `co_await` statement is reached, the coroutine asynchronously initiates the **RetrieveFeedAsync** call and then it immediately suspends itself and returns control back to the caller (which is **main** in the example above). **main** can then continue to do work while the feed is being retrieved and printed. When that's done (when the **RetrieveFeedAsync** call completes), the **ProcessFeedAsync** coroutine resumes at the next statement.

You can aggregate a coroutine into other coroutines. Or you can call **get** to block and wait for it to complete (and get the result if there is one). Or you can pass it to another programming language that supports the Windows Runtime.

It's also possible to handle the completed and/or progress events of asynchronous actions and operations by using delegates. For details, and code examples, see [Delegate types for asynchronous actions and operations](handle-events.md#delegate-types-for-asynchronous-actions-and-operations).

As you can see, in the code example above, we continue to use the blocking **get** function call just before exiting **main**. But that's only so that the application doesn't exit before finishing printing its output.

## Asynchronously return a Windows Runtime type

In this next example we wrap a call to **RetrieveFeedAsync**, for a specific URI, to give us a **RetrieveBlogFeedAsync** function that asynchronously returns a [**SyndicationFeed**](/uwp/api/windows.web.syndication.syndicationfeed).

```cppwinrt
// main.cpp
#include <iostream>
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.Web.Syndication.h>

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

    auto feedOp{ RetrieveBlogFeedAsync() };
    // do other work.
    PrintFeed(feedOp.get());
}
```

In the example above, **RetrieveBlogFeedAsync** returns an **IAsyncOperationWithProgress**, which has both progress and a return value. We can do other work while **RetrieveBlogFeedAsync** is doing its thing and retrieving the feed. Then, we call **get** on that asynchronous operation object to block, wait for it to complete, and then obtain the results of the operation.

If you're asynchronously returning a Windows Runtime type, then you should return an [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation-1) or an [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress-2). Any first- or third-party runtime class qualifies, or any type that can be passed to or from a Windows Runtime function (for example, `int`, or **winrt::hstring**). The compiler will help you with a "*must be WinRT type*" error if you try to use one of these asynchronous operation types with a non-Windows Runtime type.

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

## Asynchronously return a non-Windows-Runtime type

If you're asynchronously returning a type that's *not* a Windows Runtime type, then you should return a Parallel Patterns Library (PPL) [**concurrency::task**](/cpp/parallel/concrt/reference/task-class). We recommend **concurrency::task** because it gives you better performance (and better compatibility going forward) than **std::future** does.

> [!TIP]
> If you include `<pplawait.h>`, then you can use **concurrency::task** as a coroutine type.

```cppwinrt
// main.cpp
#include <iostream>
#include <ppltasks.h>
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.Web.Syndication.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

concurrency::task<std::wstring> RetrieveFirstTitleAsync()
{
    return concurrency::create_task([]
        {
            Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
            SyndicationClient syndicationClient;
            SyndicationFeed syndicationFeed{ syndicationClient.RetrieveFeedAsync(rssFeedUri).get() };
            return std::wstring{ syndicationFeed.Items().GetAt(0).Title().Text() };
        });
}

int main()
{
    winrt::init_apartment();

    auto firstTitleOp{ RetrieveFirstTitleAsync() };
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

    co_await DoOtherWorkAsync(); // (this is the first suspension point)...

    // ...accessing value here carries no guarantees of safety.
}
```

In a coroutine, execution is synchronous up until the first suspension point, where control is returned to the caller and the calling frame goes out of scope. By the time the coroutine resumes, anything might have happened to the source value that a reference parameter references. From the coroutine's perspective, a reference parameter has uncontrolled lifetime. So, in the example above, we're safe to access *value* up until the `co_await`, but not after it. In the event that *value* is destructed by the caller, attempting to access it inside the coroutine after that results in a memory corruption. Nor can we safely pass *value* to **DoOtherWorkAsync** if there's any risk that that function will in turn suspend and then try to use *value* after it resumes.

To make parameters safe to use after suspending and resuming, your coroutines should use pass-by-value by default to ensure that they capture by value, and avoid lifetime issues. Cases when you can deviate from that guidance because you're certain that it's safe to do so are going to be rare.

```cppwinrt
// Coroutine
IASyncAction DoWorkAsync(Param value); // not const&
```

Passing by value requires that the argument be inexpensive to move or copy; and that's typically the case for a smart pointer.

It's also arguable that (unless you want to move the value) passing by const value is good practice. It won't have any effect on the source value from which you're making a copy, but it makes the intent clear, and helps if you inadvertently modify the copy.

```cppwinrt
// coroutine with strictly unnecessary const (but arguably good practice).
IASyncAction DoWorkAsync(Param const value);
```

Also see [Standard arrays and vectors](std-cpp-data-types.md#standard-arrays-and-vectors), which deals with how to pass a standard vector into an asynchronous callee.

If you can't change your coroutine's signature, but you can change the implementation, then you can make a local copy before the first `co_await`.

```cppwinrt
IASyncAction DoWorkAsync(Param const& value)
{
    auto safe_value = value;
    // It's ok to access both safe_value and value here.

    co_await DoOtherWorkAsync();

    // It's ok to access only safe_value here (not value).
}
```

If `Param` is expensive to copy, then extract just the pieces you need before the first `co_await`.

```cppwinrt
IASyncAction DoWorkAsync(Param const& value)
{
    auto safe_data = value.data;
    // It's ok to access safe_data, value.data, and value here.

    co_await DoOtherWorkAsync();

    // It's ok to access only safe_data here (not value.data, nor value).
}
```

## Safely accessing the *this* pointer in a class-member coroutine

See [Strong and weak references in C++/WinRT](./weak-references.md#safely-accessing-the-this-pointer-in-a-class-member-coroutine).

## Important APIs
* [concurrency::task class](/cpp/parallel/concrt/reference/task-class)
* [IAsyncAction interface](/uwp/api/windows.foundation.iasyncaction)
* [IAsyncActionWithProgress&lt;TProgress&gt; interface](/uwp/api/windows.foundation.iasyncactionwithprogress-1)
* [IAsyncOperation&lt;TResult&gt; interface](/uwp/api/windows.foundation.iasyncoperation-1)
* [IAsyncOperationWithProgress&lt;TResult, TProgress&gt; interface](/uwp/api/windows.foundation.iasyncoperationwithprogress-2)
* [SyndicationClient::RetrieveFeedAsync method](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync)
* [SyndicationFeed class](/uwp/api/windows.web.syndication.syndicationfeed)

## Related topics
* [More advanced concurrency and asynchrony](concurrency-2.md)
* [Handle events by using delegates in C++/WinRT](handle-events.md)
* [Standard C++ data types and C++/WinRT](std-cpp-data-types.md)