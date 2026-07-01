---
title: Threading and async programming
description: Use the Windows thread pool and async APIs to accomplish work in parallel threads while keeping your Windows app responsive.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 06/20/2026
keywords: windows 10, windows 11, windows app sdk, winui 3, thread pool, async, threading
ms.localizationpriority: medium
---
# Threading and async programming

Threading and async programming lets your app accomplish work asynchronously in parallel threads without blocking the UI.

Your app can use the thread pool to accomplish work asynchronously in parallel threads. The thread pool manages a set of threads and uses a queue to assign work items to threads as they become available. The thread pool is similar to the asynchronous programming patterns available in the Windows Runtime because it can be used to accomplish extended work without blocking the UI, but the thread pool offers more control than the asynchronous programming patterns and you can use it to complete multiple work items in parallel. You can use the thread pool to:

- Submit work items, control their priority, and cancel work items.
- Schedule work items using timers and periodic timers.
- Set aside resources for critical work items.
- Run work items in response to named events and semaphores.

The thread pool is more efficient at managing threads because it reduces the overhead of creating and destroying threads. It has access to optimize threads across multiple CPU cores, and it can balance thread resources between apps and when background tasks are running. Using the built-in thread pool is convenient because you focus on writing code that accomplishes a task instead of the mechanics of thread management.

| Topic | Description |
|-------|-------------|
| [Best practices for using the thread pool](./best-practices-for-using-the-thread-pool.md) | Best practices for working with the thread pool. |
| [Call asynchronous APIs in C# or Visual Basic](./call-asynchronous-apis-in-csharp-or-visual-basic.md) | How to use asynchronous APIs in C# or Visual Basic in your Windows app. |
| [Create a periodic work item](./create-a-periodic-work-item.md) | How to create a work item that repeats periodically. |
| [Submit a work item to the thread pool](./submit-a-work-item-to-the-thread-pool.md) | How to do work in a separate thread by submitting a work item to the thread pool. |
| [Use a timer to submit a work item](./use-a-timer-to-submit-a-work-item.md) | How to create a work item that runs after a timer elapses. |
