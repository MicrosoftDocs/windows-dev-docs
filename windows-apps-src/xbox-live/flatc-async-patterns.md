---
title: Asynchronous C API calling patterns
author: aablackm
description: Learn the asynchronous C API calling patterns for the XSAPI C API
ms.author: aablackm
ms.date: 06/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, developer program,
ms.localizationpriority: low
---
# Calling pattern for XSAPI flat C layer async calls

An **asynchronous API** is an API that returns quickly but starts an **asynchronous task** and the result is returned after the task is finished.

Traditionally games have little control over which thread executes the **asynchronous task** and which thread returns the results when using a **completion callback**.  Some games are designed so that a section of the heap is only touched by a single thread to avoid any need for thread synchronization. If the **completion callback** isn't called from a thread the game controls, updating shared state with the result of an **asynchronous task** will require thread synchronization.

The XSAPI C API exposes a new asynchronous C API that gives developers direct thread control when
making an **asynchronous API** call, such as **XblSocialGetSocialRelationshipsAsync()**, **XblProfileGetUserProfileAsync()** and **XblAchievementsGetAchievementsForTitleIdAsync()**.

Here is a basic example calling the **XblProfileGetUserProfileAsync** API.

```cpp
AsyncBlock* asyncBlock = new AsyncBlock {};
asyncBlock->queue = asyncQueue;
asyncBlock->context = customDataForCallback;
asyncBlock->callback = [](AsyncBlock* asyncBlock)
{
    XblUserProfile profile;
    if( SUCCEEDED( XblProfileGetUserProfileResult(asyncBlock, &profile) ) )
    {
        printf("Profile retrieved successfully\r\n");
    }
    delete asyncBlock;
};
XblProfileGetUserProfileAsync(asyncBlock, xboxLiveContext, xuid);
```

To understand this calling pattern, you will need to understand how to use the **AsyncBlock** and the **AsyncQueue**.

* The **AsyncBlock** carries all of the information pertaining to the **asynchronous task** and **completion callback**.

* The **AsyncQueue** allows you to determine which thread executes the **asynchronous task** and which thread calls the AsyncBlock's **completion callback**.

## The **AsyncBlock**

Let's take a look at the **AsyncBlock** in detail. It is a struct defined as follows:

```cpp
typedef struct AsyncBlock
{
    AsyncCompletionRoutine* callback;
    void* context;
    async_queue_handle_t queue;
} AsyncBlock;
```

The **AsyncBlock** contains:

* *callback* - an optional callback function that will be called after the asynchronous work has been done.  If you don't specify a callback, you can wait for the **AsyncBlock** to complete with **GetAsyncStatus** and then get the results.
* *context* - allows you to pass data to the callback function.
* *queue* - an async_queue_handle_t which is a handle designating an **AsyncQueue**. If this is not set, a default queue will be used.

You should create a new AsyncBlock on the heap for each async API you call.  The AsyncBlock must live until the AsyncBlock's completion callback is called and then it can be deleted.

> [!IMPORTANT]
> An **AsyncBlock** must remain in memory until the **asynchronous task** completes. If it is dynamically allocated, it can be deleted inside the AsyncBlock's **completion callback**.

### Waiting for **asynchronous task**

You can tell an **asynchronous task** is complete a number of different ways:

* the AsyncBlock's **completion callback** is called
* Call **GetAsyncStatus** with true to wait until it completes.
* Set a waitEvent in the **AsyncBlock** and wait for the event to be signaled

With **GetAsyncStatus** and waitEvent, the **asynchronous task** is considered complete after the the AsyncBlock's **completion callback** executes however the AsyncBlock's **completion callback** is optional.

Once the **asynchronous task** is complete, you can get the results.

### Getting the result of the **asynchronous task**

To get the result, most **asynchronous API** functions have a corresponding \[Name of Function\]Result function to receive the result of the asynchronous call. In our example code, **XblProfileGetUserProfileAsync** has a corresponding **XblProfileGetUserProfileResult** function. You can use this function to return the result of the function and act accordingly.  See the documention of each **asynchronous API** function for full details on retrieving results.

## The **AsyncQueue**

The **AsyncQueue** allows you to determine which thread executes the **asynchronous task** and which thread calls the AsyncBlock's **completion callback**.

You can control which thread performs these operation by setting a *dispatch mode*. There are three dispatch modes available:

* *Manual* - The manual queue are not automatically dispatched.  It is up to the developer to dispatch them on any thread they want. This can be used to assign either the work or callback side of an async call to a specific thread.  This is discussed in more detail below.

* *Thread Pool* - Dispatches using a thread pool.  The thread pool invokes the calls in parallel, taking a call to execute from the queue in turn as threadpool threads become available.  This is the easist to use but gives you the least amount of control over which thread is used.

* *Fixed Thread* - Dispatches using QueueUserAPC on the thread that created the async queue. When a user-mode APC is queued, the thread is not directed to call the APC function unless it is in an alertable state. A thread enters an alertable state by using **SleepEx**, **SignalObjectAndWait**, **WaitForSingleObjectEx**, **WaitForMultipleObjectsEx**, or **MsgWaitForMultipleObjectsEx** to perform an alertable wait operation

To create a new **AsyncQueue** you will need to call **CreateAsyncQueue**.

```cpp
STDAPI CreateAsyncQueue(
    _In_ AsyncQueueDispatchMode workDispatchMode,
    _In_ AsyncQueueDispatchMode completionDispatchMode,
    _Out_ async_queue_handle_t* queue);
```

where AsyncQueueDispatchMode contains the three dispatch modes introduced earlier:

```cpp
typedef enum AsyncQueueDispatchMode
{
    /// <summary>
    /// Async callbacks are invoked manually by DispatchAsyncQueue
    /// </summary>
    AsyncQueueDispatchMode_Manual,

    /// <summary>
    /// Async callbacks are queued to the thread that created the queue
    /// and will be processed when the thread is alertable.
    /// </summary>
    AsyncQueueDispatchMode_FixedThread,

    /// <summary>
    /// Async callbacks are queued to the system thread pool and will
    /// be processed in order by the threadpool.
    /// </summary>
    AsyncQueueDispatchMode_ThreadPool

} AsyncQueueDispatchMode;
```

**workDispatchMode** determines the dispatch mode for the thread which handles the async work, while **completionDispatchMode** determines the dispatch mode for the thread which handles the completion of the async operation.

You can also call **CreateSharedAsyncQueue** to create an **AsyncQueue** with the same queue type by specifying an ID for the queue.

```cpp
STDAPI CreateSharedAsyncQueue(
    _In_ uint32_t id,
    _In_ AsyncQueueDispatchMode workerMode,
    _In_ AsyncQueueDispatchMode completionMode,
    _Out_ async_queue_handle_t* aQueue);
```

> [!NOTE]
> If there is already a queue with this ID and dispatch  modes, it will be referenced.  Otherwise a new queue will be created.

Once you have created your **AsyncQueue** simply add it to the **AsyncBlock** to control threading on your work and completion functions.
When you are finished using the **AsyncQueue**, typically when the game is ending, you can close it with **CloseAsyncQueue**:

```cpp
STDAPI_(void) CloseAsyncQueue(
    _In_ async_queue_handle_t aQueue);
```

### Manually dispatching an **AsyncQueue**

If you used the manual queue dispatch mode for an **AsyncQueue** work or completion queue, you will need to manually dispatch.
Let us say that an **AsyncQueue** was created where both the work queue and the completion queue are set to dispatch manually like so:

```cpp
CreateAsyncQueue(
    AsyncQueueDispatchMode_Manual,
    AsyncQueueDispatchMode_Manual,
    &queue);
```

In order to dispatch work that has been assigned **AsyncQueueDispatchMode_Manual** you will have to dispatch it with the **DispatchAsyncQueue** function.

```cpp
STDAPI_(bool) DispatchAsyncQueue(
    _In_ async_queue_handle_t queue,
    _In_ AsyncQueueCallbackType type,
    _In_ uint32_t timeoutInMs);
```

* *queue* - which queue to dispatch work on
* *type* - an instance of the **AsyncQueueCallbackType** enum
* *timeoutInMs* - a uint32_t for the timeout in milliseconds.

There are two callback types defined by the **AsyncQueueCallbackType** enum:

```cpp
typedef enum AsyncQueueCallbackType
{
    /// <summary>
    /// Async work callbacks
    /// </summary>
    AsyncQueueCallbackType_Work,

    /// <summary>
    /// Completion callbacks after work is done
    /// </summary>
    AsyncQueueCallbackType_Completion
} AsyncQueueCallbackType;
```

### When to call **DispatchAsyncQueue**

In order to check when the queue has received a new item you can call **AddAsyncQueueCallbackSubmitted** to set an event handler to let your code know that either work or completions are ready to be dispatched.

```cpp
STDAPI AddAsyncQueueCallbackSubmitted(
    _In_ async_queue_handle_t queue,
    _In_opt_ void* context,
    _In_ AsyncQueueCallbackSubmitted* callback,
    _Out_ uint32_t* token);
```

**AddAsyncQueueCallbackSubmitted** takes the following parameters:

* *queue* - the async queue you are submitting the callback for.
* *context* - a pointer to data that should be passed to the submit callback.
* *callback* - the function that will be invoked when a new callback is submitted to the queue.
* *token* - a token that will be used in a later call to **RemoveAsynceCallbackSubmitted** to remove the callback.

For example, here is a call to **AddAsyncQueueCallbackSubmitted**:

`AddAsyncQueueCallbackSubmitted(queue, nullptr, HandleAsyncQueueCallback, &m_callbackToken);`

The corresponding **AsyncQueueCallbackSubmitted** callback might be implemented as follows:

```cpp
void CALLBACK HandleAsyncQueueCallback(
    _In_ void* context,
    _In_ async_queue_handle_t queue,
    _In_ AsyncQueueCallbackType type)
{
    switch (type)
    {
    case AsyncQueueCallbackType::AsyncQueueCallbackType_Work:
        ReleaseSemaphore(g_workReadyHandle, 1, nullptr);
        break;
    }
}
```

Then in a background thread you can listen for this semaphore to wake up and call **DispatchAsyncQueue**.

```cpp
DWORD WINAPI BackgroundWorkThreadProc(LPVOID lpParam)
{
    HANDLE hEvents[2] =
    {
        g_workReadyHandle.get(),
        g_stopRequestedHandle.get()
    };

    async_queue_handle_t queue = static_cast<async_queue_handle_t>(lpParam);

    bool stop = false;
    while (!stop)
    {
        DWORD dwResult = WaitForMultipleObjectsEx(2, hEvents, false, INFINITE, false);
        switch (dwResult)
        {
        case WAIT_OBJECT_0: 
            // Background work is ready to be dispatched
            DispatchAsyncQueue(queue, AsyncQueueCallbackType_Work, 0);
            break;

        case WAIT_OBJECT_0 + 1:
        default:
            stop = true;
            break;
        }
    }

    CloseAsyncQueue(queue);
    return 0;
}
```

It is best practice to use implement with Win32 Semaphore object.  If instead you implement using a Win32 Event object, then you'll need to ensure don't miss any events with code such as:

```cpp
    case WAIT_OBJECT_0: 
        // Background work is ready to be dispatched
        DispatchAsyncQueue(queue, AsyncQueueCallbackType_Work, 0);        
        
        if (!IsAsyncQueueEmpty(queue, AsyncQueueCallbackType_Work))
        {
            // If there's more pending work, then set the event to process them
            SetEvent(g_workReadyHandle.get());
        }
        break;
```


You can view an example of the best practices for async integration at [Social C Sample AsyncIntegration.cpp](https://github.com/Microsoft/xbox-live-api/blob/master/InProgressSamples/Social/Xbox/C/AsyncIntegration.cpp)

