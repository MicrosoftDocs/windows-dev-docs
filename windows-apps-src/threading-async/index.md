---
ms.assetid: beac6333-655a-4bcf-9caf-bba15f715ea5
title: Threading and async programming
description: Threading and async programming enables your app to accomplish work asynchronously in parallel threads.
ms.date: 05/14/2018
ms.topic: article
keywords: windows 10, uwp, asynchronous, threads, threading
ms.localizationpriority: medium
---
# Threading and async programming
Threading and async programming enables your app to accomplish work asynchronously in parallel threads.

Your app can use the thread pool to accomplish work asynchronously in parallel threads. The thread pool manages a set of threads and uses a queue to assign work items to threads as they become available. The thread pool is similar to the asynchronous programming patterns available in the Windows Runtime because it can be used to accomplish extended work without blocking the UI, but the thread pool offers more control than the asynchronous programming patterns and you can use it to complete multiple work items in parallel. You can use the thread pool to:

-   Submit work items, control their priority, and cancel work items.

-   Schedule work items using timers and periodic timers.

-   Set aside resources for critical work items.

-   Run work items in response to named events and semaphores.

The thread pool is more efficient at managing threads because it reduces the overhead of creating and destroying threads. The means it has access to optimize threads across multiple CPU cores, and it can balance thread resources between apps and when background tasks are running. Using the built-in thread pool is convenient because you focus on writing code that accomplishes a task instead of the mechanics of thread management.

| Topic                                                                                                          | Description                         |
|----------------------------------------------------------------------------------------------------------------|-------------------------------------|
| [Asynchronous programming (UWP apps)](asynchronous-programming-universal-windows-platform-apps.md)              | This topic describes asynchronous programming in the Universal Windows Platform (UWP) and its representation in C#, Microsoft Visual Basic .NET, Visual C++ component extensions (C++/CX), and JavaScript. |
| [Asynchronous programming in C++/CX (UWP apps)](asynchronous-programming-in-cpp-universal-windows-platform-apps.md)| This article describes the recommended way to consume asynchronous methods in C++/CX by using the <code>task</code> class that's defined in the <code>concurrency</code> namespace in ppltasks.h. |
| [Best practices for using the thread pool](best-practices-for-using-the-thread-pool.md)                         | This topic describes best practices for working with the thread pool. |
| [Call asynchronous APIs in C# or Visual Basic](call-asynchronous-apis-in-csharp-or-visual-basic.md)             | The Universal Windows Platform (UWP) includes many asynchronous APIs to ensure that your app remains responsive when it does work that might take an extended amount of time. This topic discusses how to use asynchronous methods from the UWP in C# or Microsoft Visual Basic. |
| [Create a periodic work item](create-a-periodic-work-item.md)                                                   | Learn how to create a work item that repeats periodically. |
| [Submit a work item to the thread pool](submit-a-work-item-to-the-thread-pool.md)                               | Learn how to do work in a separate thread by submitting a work item to the thread pool. |
| [Use a timer to submit a work item](use-a-timer-to-submit-a-work-item.md)                                       | Learn how to create a work item that runs after a timer elapses. |
