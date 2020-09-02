---
ms.assetid: 23FE28F1-89C5-4A17-A732-A722648F9C5E
title: Asynchronous programming
description: This topic describes asynchronous programming in the Universal Windows Platform (UWP) and its representation in C#, Microsoft Visual Basic .NET, C++, and JavaScript.
ms.date: 05/14/2018
ms.topic: article
keywords: windows 10, uwp, asynchronous
ms.localizationpriority: medium
---
# Asynchronous programming
This topic describes asynchronous programming in the Universal Windows Platform (UWP) and its representation in C#, Microsoft Visual Basic .NET, C++, and JavaScript.

Using asynchronous programming helps your app stay responsive when it does work that might take an extended amount of time. For example, an app that downloads content from the Internet might spend several seconds waiting for the content to arrive. If you used a synchronous method on the UI thread to retrieve the content, the app is blocked until the method returns. The app won't respond to user interaction, and because it seems non-responsive, the user might become frustrated. A much better way is to use asynchronous programming, where the app continues to run and respond to the UI while it waits for an operation to complete.

For methods that might take a long time to complete, asynchronous programming is the norm and not the exception in the UWP. JavaScript, C#, Visual Basic, and C++ each provide language support for asynchronous methods.

## Asynchronous programming in the UWP
Many UWP features, such as the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) APIs and [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) APIs, are exposed as asynchronous APIs. By convention, the names of asynchronous APIs end with "Async" to indicate that part of their execution is likely to take place after control has returned to the caller.

When you use asynchronous APIs in your Universal Windows Platform (UWP) app, your code makes non-blocking calls in a consistent way. When you implement these asynchronous patterns in your own API definitions, callers can understand and use your code in a predictable way.

Here are some common tasks that require calling asynchronous Windows Runtime APIs.

-   Displaying a message dialog

-   Working with the file system, displaying a file picker

-   Sending and receiving data to and from the Internet

-   Using sockets, streams, connectivity

-   Working with appointments, contacts, calendar

-   Working with file types, such as opening Portable Document Format (PDF) files or decoding image or media formats

-   Interacting with a device or a service

With UWP asynchronous pattern, you may be able to avoid explicitly manage threads at all. Each programming language supports the asynchronous pattern for the UWP in its own way:

| Programming language | Asynchronous representation           |
|----------------------|---------------------------------------|
| C#                   | **async** keyword, **await** operator |
| Visual Basic         | **Async** keyword, **Await** operator |
| C++/WinRT            | coroutine, and **co_await** operator  |
| C++/CX               | **task** class, **.then** method      |
| JavaScript           | promise object, **then** function     |

## Asynchronous patterns in UWP using C# and Visual Basic
A typical segment of code written in C# or Visual Basic executes synchronously, meaning that when a line executes, it finishes before the next line executes. There have been previous Microsoft .NET programming models for asynchronous execution, but the resulting code tends to emphasize the mechanics of executing asynchronous code instead of focusing on the task that the code is trying to accomplish. The UWP, .NET framework, and C# and Visual Basic compilers have added features that abstract the asynchronous mechanics out of your code. For .NET and the UWP you can write asynchronous code that focuses on what your code does instead of how and when to do it. Your asynchronous code will look reasonably similar to synchronous code. For more info, see [Call asynchronous APIs in C# or Visual Basic](call-asynchronous-apis-in-csharp-or-visual-basic.md).

## Asynchronous patterns in UWP with C++/WinRT
With C++/WinRT, you use coroutines, and the **co_await** operator. For more info, and code examples, see [Asynchronous programming in C++/WinRT](../cpp-and-winrt-apis/concurrency.md).

## Asynchronous patterns in UWP with C++/CX
In C++/CX, asynchronous programming is based on the [**task class**](/cpp/parallel/concrt/reference/task-class), and its [**then method**](/cpp/parallel/concrt/reference/task-class?view=vs-2017). The syntax is similar to that of JavaScript promises. The **task class** and its related types also provide the capability for cancellation and management of the thread context. For more info, see [Asynchronous programming in C++/CX](asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

The [**create\_async function**](/cpp/parallel/concrt/reference/concurrency-namespace-functions?view=vs-2017) provides support for producing asynchronous APIs that can be consumed from JavaScript or any other language that supports the UWP. For more info, see [Creating Asynchronous Operations in C++/CX](/cpp/parallel/concrt/creating-asynchronous-operations-in-cpp-for-windows-store-apps).

## Asynchronous patterns in UWP using JavaScript
In JavaScript, asynchronous programming follows the [Common JS Promises/A](https://wiki.commonjs.org/wiki/Promises/A) proposed standard by having asynchronous methods return promise objects. Promises are used in both the UWP and Windows Library for JavaScript.

A promise object represents a value that will be fulfilled in the future. In the UWP you get a promise object from a factory function, which by convention has a name that ends with "Async".

In many cases, calling an asynchronous function is almost as simple as calling a conventional function. The difference is that you use the [**then**](/previous-versions/windows/apps/br229728(v=win.10)) or the [**done**](/previous-versions/windows/apps/hh701079(v=win.10)) method to assign the handlers for results or errors and to start the operation.

## Related topics
* [Call asynchronous APIs in C# or Visual Basic](call-asynchronous-apis-in-csharp-or-visual-basic.md)
* [Asynchronous Programming with Async and Await (C# and Visual Basic)](/previous-versions/visualstudio/visual-studio-2012/hh191443(v=vs.110))
* [Reversi sample feature scenarios: asynchronous code](/previous-versions/windows/apps/jj712233(v=win.10))