---
author: stevewhims
description: This topic shows the ways in which you can both create and consume WinRT asynchronous objects with C++/WinRT.
title: Concurrency and asynchronous operations with C++/WinRT
ms.author: stwhi
ms.date: 03/20/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection
ms.localizationpriority: medium
---

# Concurrency and asynchronous operations with C++/WinRT
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic shows the ways in which you can both create and consume WinRT asynchronous objects with C++/WinRT. Any WinRT API that has the potential to take more than 50 milliseconds to complete is implemented as an asynchronous function (with a name ending in "Async"). The implementation of an asynchronous function kicks off the work on another thread and returns immediately with an object that represents the asynchronous operation. When the asynchronous operation completes, that returned object contains any value that resulted from the work.

An example of an asynchronous WinRT function is [**SyndicationClient::RetrieveFeedAsync**](https://docs.microsoft.com/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync), which returns an asynchronous operation object of type [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_). Let's look at some ways of calling that API using C++/WinRT.

## Block the calling thread
The code example below receives an asynchronous operation object from **RetrieveFeedAsync**, and it calls **get** on that object to block the calling thread until the results of the asynchronous operation are available.

```cppwinrt
// main.cpp

#include "pch.h"
#include "winrt/Windows.Foundation.h"
#include "winrt/Windows.Web.Syndication.h"

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
    init_apartment();
	ProcessFeed();
}
```

Calling **get** makes for convenient coding, but it's not what you'd call concurrency nor asynchrony. To avoid holding up OS threads from doing other useful work, we need a different technique.

## Write a coroutine
C++/WinRT integrates C++ coroutines very deeply into the programming model to provide a natural way to cooperatively wait for a result. You can produce your own WinRT asynchronous operation by writing a coroutine. In the code example below, **ProcessFeedAsync** is the coroutine.

```cppwinrt
// main.cpp

#include "pch.h"
#include "winrt/Windows.Foundation.h"
#include "winrt/Windows.Web.Syndication.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

IAsyncAction ProcessFeedAsync()
{
	Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
	SyndicationClient syndicationClient;
	SyndicationFeed syndicationFeed = co_await syndicationClient.RetrieveFeedAsync(rssFeedUri);
	// use syndicationFeed.
}

int main()
{
	init_apartment();
	auto asyncAction = ProcessFeedAsync();
	// do other work.
}
```

The returned **winrt::Windows::Foundation::IAsync­Action** is equivalent to [**Windows::Foundation::IAsync­Action**](/uwp/api/windows.foundation.iasyncaction). You can aggregate it into other coroutines. Or you can call **get** to block and wait for the result. Or you can pass it to another programming language that supports WinRT.

## Important APIs
* [SyndicationClient::RetrieveFeedAsync](https://docs.microsoft.com/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync)
