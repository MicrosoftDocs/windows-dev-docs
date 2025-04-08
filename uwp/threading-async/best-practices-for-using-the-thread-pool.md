---
ms.assetid: 95CF7F3D-9E3A-40AC-A083-D8A375272181
title: Best practices for using the thread pool
description: Learn best practices for working with the thread pool to accomplish work asynchronously in parallel threads.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, thread, thread pool
ms.localizationpriority: medium
---
# Best practices for using the thread pool

This topic describes best practices for working with the thread pool.

## Do's


-   Use the thread pool to do parallel work in your app.

-   Use work items to accomplish extended tasks without blocking the UI thread.

-   Create work items that are short-lived and independent. Work items run asynchronously and they can be submitted to the pool in any order from the queue.

-   Dispatch updates to the UI thread with the [**Windows.UI.Core.CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher).

-   Use [**ThreadPoolTimer.CreateTimer**](/uwp/api/windows.system.threading.threadpooltimer.createtimer) instead of the **Sleep** function.

-   Use the thread pool instead of creating your own thread management system. The thread pool runs at the OS level with advanced capability and is optimized to dynamically scale according to device resources and activity within the process and across the system.

-   In C++, ensure that work item delegates use the agile threading model (C++ delegates are agile by default).

-   Use pre-allocated work items when you can't tolerate a resource allocation failure at time of use.

## Don'ts


-   Don't create periodic timers with a *period* value of &lt;1 millisecond (including 0). This will cause the work item to behave as a single-shot timer.

-   Don't submit periodic work items that take longer to complete than the amount of time you specified in the *period* parameter.

-   Don't try to send UI updates (other than toasts and notifications) from a work item dispatched from a background task. Instead, use background task progress and completion handlers - for example, [**IBackgroundTaskInstance.Progress**](/uwp/api/windows.applicationmodel.background.ibackgroundtaskinstance.progress).

-   When you use work-item handlers that use the **async** keyword, don't assume that all code in the handler has executed when the complete state has been set on the work item. The thread pool work item may be set to the complete state before all of the code in the handler has executed. Code following an **await** keyword within the handler may execute after the work item has been set to the complete state.

-   Don't try to run a pre-allocated work item more than once without reinitializing it. [Create a periodic work item](create-a-periodic-work-item.md)

## Related topics


* [Create a periodic work item](create-a-periodic-work-item.md)
* [Submit a work item to the thread pool](submit-a-work-item-to-the-thread-pool.md)
* [Use a timer to submit a work item](use-a-timer-to-submit-a-work-item.md)
