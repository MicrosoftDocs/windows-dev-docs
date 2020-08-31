---
ms.assetid: 34C00F9F-2196-46A3-A32F-0067AB48291B
description: This article describes the recommended way to consume asynchronous methods in Visual C++ component extensions (C++/CX) by using the task class defined in the concurrency namespace in ppltasks.h.
title: Asynchronous programming in C++
ms.date: 05/14/2018
ms.topic: article
keywords: windows 10, uwp, threads, asynchronous, C++
ms.localizationpriority: medium
---
# Asynchronous programming in C++/CX
> [!NOTE]
> This topic exists to help you maintain your C++/CX application. But we recommend that you use [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) for new applications. C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API.

This article describes the recommended way to consume asynchronous methods in Visual C++ component extensions (C++/CX) by using the `task` class that's defined in the `concurrency` namespace in ppltasks.h.

## Universal Windows Platform (UWP) asynchronous types
The Universal Windows Platform (UWP) features a well-defined model for calling asynchronous methods and provides the types that you need to consume such methods. If you are not familiar with the UWP asynchronous model, read [Asynchronous Programming][AsyncProgramming] before you read the rest of this article.

Although you can consume the asynchronous Windows Runtime APIs directly in C++, the preferred approach is to use the [**task class**][task-class] and its related types and functions, which are contained in the [**concurrency**][concurrencyNamespace] namespace and defined in `<ppltasks.h>`. The **concurrency::task** is a general-purpose type, but when the **/ZW** compiler switch—which is required for Universal Windows Platform (UWP) apps and components—is used, the task class encapsulates the UWP asynchronous types so that it's easier to:

-   chain multiple asynchronous and synchronous operations together

-   handle exceptions in task chains

-   perform cancellation in task chains

-   ensure that individual tasks run in the appropriate thread context or apartment

This article provides basic guidance about how to use the **task** class with the UWP asynchronous APIs. For more complete documentation about **task** and its related methods including [**create\_task**][createTask], see [Task Parallelism (Concurrency Runtime)][taskParallelism]. 

## Consuming an async operation by using a task
The following example shows how to use the task class to consume an **async** method that returns an [**IAsyncOperation**][IAsyncOperation] interface and whose operation produces a value. Here are the basic steps:

1.  Call the `create_task` method and pass it the **IAsyncOperation^** object.

2.  Call the member function [**task::then**][taskThen] on the task and supply a lambda that will be invoked when the asynchronous operation completes.

``` cpp
#include <ppltasks.h>
using namespace concurrency;
using namespace Windows::Devices::Enumeration;
...
void App::TestAsync()
{    
    //Call the *Async method that starts the operation.
    IAsyncOperation<DeviceInformationCollection^>^ deviceOp =
        DeviceInformation::FindAllAsync();

    // Explicit construction. (Not recommended)
    // Pass the IAsyncOperation to a task constructor.
    // task<DeviceInformationCollection^> deviceEnumTask(deviceOp);

    // Recommended:
    auto deviceEnumTask = create_task(deviceOp);

    // Call the task's .then member function, and provide
    // the lambda to be invoked when the async operation completes.
    deviceEnumTask.then( [this] (DeviceInformationCollection^ devices )
    {       
        for(int i = 0; i < devices->Size; i++)
        {
            DeviceInformation^ di = devices->GetAt(i);
            // Do something with di...          
        }       
    }); // end lambda
    // Continue doing work or return...
}
```

The task that's created and returned by the [**task::then**][taskThen] function is known as a *continuation*. The input argument (in this case) to the user-provided lambda is the result that the task operation produces when it completes. It's the same value that would be retrieved by calling [**IAsyncOperation::GetResults**](/uwp/api/Windows.Foundation.IAsyncOperation_TResult_#Windows_Foundation_IAsyncOperation_1_GetResults) if you were using the **IAsyncOperation** interface directly.

The [**task::then**][taskThen] method returns immediately, and its delegate doesn't run until the asynchronous work completes successfully. In this example, if the asynchronous operation causes an exception to be thrown, or ends in the canceled state as a result of a cancellation request, the continuation will never execute. Later, we’ll describe how to write continuations that execute even if the previous task was cancelled or failed.

Although you declare the task variable on the local stack, it manages its lifetime so that it is not deleted until all of its operations complete and all references to it go out of scope, even if the method returns before the operations complete.

## Creating a chain of tasks
In asynchronous programming, it's common to define a sequence of operations, also known as *task chains*, in which each continuation executes only when the previous one completes. In some cases, the previous (or *antecedent*) task produces a value that the continuation accepts as input. By using the [**task::then**][taskThen] method, you can create task chains in an intuitive and straightforward manner; the method returns a **task<T>** where **T** is the return type of the lambda function. You can compose multiple continuations into a task chain: `myTask.then(…).then(…).then(…);`

Task chains are especially useful when a continuation creates a new asynchronous operation; such a task is known as an asynchronous task. The following example illustrates a task chain that has two continuations. The initial task acquires the handle to an existing file, and when that operation completes, the first continuation starts up a new asynchronous operation to delete the file. When that operation completes, the second continuation runs, and outputs a confirmation message.

``` cpp
#include <ppltasks.h>
using namespace concurrency;
...
void App::DeleteWithTasks(String^ fileName)
{    
    using namespace Windows::Storage;
    StorageFolder^ localFolder = ApplicationData::Current->LocalFolder;
    auto getFileTask = create_task(localFolder->GetFileAsync(fileName));

    getFileTask.then([](StorageFile^ storageFileSample) ->IAsyncAction^ {       
        return storageFileSample->DeleteAsync();
    }).then([](void) {
        OutputDebugString(L"File deleted.");
    });
}
```

The previous example illustrates four important points:

-   The first continuation converts the [**IAsyncAction^**][IAsyncAction] object to a **task<void>** and returns the **task**.

-   The second continuation performs no error handling, and therefore takes **void** and not **task<void>** as input. It is a value-based continuation.

-   The second continuation doesn't execute until the [**DeleteAsync**][deleteAsync] operation completes.

-   Because the second continuation is value-based, if the operation that was started by the call to [**DeleteAsync**][deleteAsync] throws an exception, the second continuation doesn't execute at all.

**Note**  Creating a task chain is just one of the ways to use the **task** class to compose asynchronous operations. You can also compose operations by using join and choice operators **&&** and **||**. For more information, see [Task Parallelism (Concurrency Runtime)][taskParallelism].

## Lambda function return types and task return types
In a task continuation, the return type of the lambda function is wrapped in a **task** object. If the lambda returns a **double**, then the type of the continuation task is **task<double>**. However, the task object is designed so that it doesn't produce needlessly nested return types. If a lambda returns an **IAsyncOperation<SyndicationFeed^>^**, the continuation returns a **task<SyndicationFeed^>**, not a **task<task<SyndicationFeed^>>** or **task<IAsyncOperation<SyndicationFeed^>^>^**. This process is known as *asynchronous unwrapping* and it also ensures that the asynchronous operation inside the continuation completes before the next continuation is invoked.

In the previous example, notice that the task returns a **task<void>** even though its lambda returned an [**IAsyncInfo**][IAsyncInfo] object. The following table summarizes the type conversions that occur between a lambda function and the enclosing task:

| | |
|--------------------------------------------------------|---------------------|
| lambda return type                                     | `.then` return type |
| TResult                                                | task<TResult> |
| IAsyncOperation<TResult>^                        | task<TResult> |
| IAsyncOperationWithProgress<TResult, TProgress>^ | task<TResult> |
|IAsyncAction^                                           | task<void>    |
| IAsyncActionWithProgress<TProgress>^             |task<void>     |
| task<TResult>                                    |task<TResult>  |


## Canceling tasks
It is often a good idea to give the user the option to cancel an asynchronous operation. And in some cases you might have to cancel an operation programmatically from outside the task chain. Although each \***Async** return type has a [**Cancel**][IAsyncInfoCancel] method that it inherits from [**IAsyncInfo**][IAsyncInfo], it's awkward to expose it to outside methods. The preferred way to support cancellation in a task chain is to use a [**cancellation\_token\_source**](/cpp/parallel/concrt/reference/cancellation-token-source-class) to create a [**cancellation\_token**](/cpp/parallel/concrt/reference/cancellation-token-class), and then pass the token to the constructor of the initial task. If an asynchronous task is created with a cancellation token, and [**cancellation\_token\_source::cancel**](/cpp/parallel/concrt/reference/cancellation-token-source-class?view=vs-2017) is called, the task automatically calls **Cancel** on the **IAsync\*** operation and passes the cancellation request down its continuation chain. The following pseudocode demonstrates the basic approach.

``` cpp
//Class member:
cancellation_token_source m_fileTaskTokenSource;

// Cancel button event handler:
m_fileTaskTokenSource.cancel();

// task chain
auto getFileTask2 = create_task(documentsFolder->GetFileAsync(fileName),
                                m_fileTaskTokenSource.get_token());
//getFileTask2.then ...
```

When a task is canceled, a [**task\_canceled**][taskCanceled] exception is propagated down the task chain. Value-based continuations will simply not execute, but task-based continuations will cause the exception to be thrown when [**task::get**][taskGet] is called. If you have an error-handling continuation, make sure that it catches the **task\_canceled** exception explicitly. (This exception is not derived from [**Platform::Exception**](/cpp/cppcx/platform-exception-class).)

Cancellation is cooperative. If your continuation does some long-running work beyond just invoking a UWP method, then it is your responsibility to check the state of the cancellation token periodically and stop execution if it is canceled. After you clean up all resources that were allocated in the continuation, call [**cancel\_current\_task**](/cpp/parallel/concrt/reference/concurrency-namespace-functions?view=vs-2017) to cancel that task and propagate the cancellation down to any value-based continuations that follow it. Here's another example: you can create a task chain that represents the result of a [**FileSavePicker**](/uwp/api/Windows.Storage.Pickers.FileSavePicker) operation. If the user chooses the **Cancel** button, the [**IAsyncInfo::Cancel**][IAsyncInfoCancel] method is not called. Instead, the operation succeeds but returns **nullptr**. The continuation can test the input parameter and call **cancel\_current\_task** if the input is **nullptr**.

For more information, see [Cancellation in the PPL](/cpp/parallel/concrt/cancellation-in-the-ppl)

## Handling errors in a task chain
If you want a continuation to execute even if the antecedent was canceled or threw an exception, then make the continuation a task-based continuation by specifying the input to its lambda function as a **task<TResult>** or **task<void>** if the lambda of the antecedent task returns an [**IAsyncAction^**][IAsyncAction].

To handle errors and cancellation in a task chain, you don't have to make every continuation task-based or enclose every operation that might throw within a `try…catch` block. Instead, you can add a task-based continuation at the end of the chain and handle all errors there. Any exception—this includes a [**task\_canceled**][taskCanceled] exception—will propagate down the task chain and bypass any value-based continuations, so that you can handle it in the error-handling task-based continuation. We can rewrite the previous example to use an error-handling task-based continuation:

``` cpp
#include <ppltasks.h>
void App::DeleteWithTasksHandleErrors(String^ fileName)
{    
    using namespace Windows::Storage;
    using namespace concurrency;

    StorageFolder^ documentsFolder = KnownFolders::DocumentsLibrary;
    auto getFileTask = create_task(documentsFolder->GetFileAsync(fileName));

    getFileTask.then([](StorageFile^ storageFileSample)
    {       
        return storageFileSample->DeleteAsync();
    })

    .then([](task<void> t)
    {

        try
        {
            t.get();
            // .get() didn' t throw, so we succeeded.
            OutputDebugString(L"File deleted.");
        }
        catch (Platform::COMException^ e)
        {
            //Example output: The system cannot find the specified file.
            OutputDebugString(e->Message->Data());
        }

    });
}
```

In a task-based continuation, we call the member function [**task::get**][taskGet] to get the results of the task. We still have to call **task::get** even if the operation was an [**IAsyncAction**][IAsyncAction] that produces no result because **task::get** also gets any exceptions that have been transported down to the task. If the input task is storing an exception, it is thrown at the call to **task::get**. If you don't call **task::get**, or don't use a task-based continuation at the end of the chain, or don't catch the exception type that was thrown, then an **unobserved\_task\_exception** is thrown when all references to the task have been deleted.

Only catch the exceptions that you can handle. If your app encounters an error that you can't recover from, it's better to let the app crash than to let it continue to run in an unknown state. Also, in general, don't attempt to catch the **unobserved\_task\_exception** itself. This exception is mainly intended for diagnostic purposes. When **unobserved\_task\_exception** is thrown, it usually indicates a bug in the code. Often the cause is either an exception that should be handled, or an unrecoverable exception that's caused by some other error in the code.

## Managing the thread context
The UI of a UWP app runs in a single-threaded apartment (STA). A task whose lambda returns either an [**IAsyncAction**][IAsyncAction] or [**IAsyncOperation**][IAsyncOperation] is apartment-aware. If the task is created in the STA, then all of its continuations will run also run in it by default, unless you specify otherwise. In other words, the entire task chain inherits apartment-awareness from the parent task. This behavior helps simplify interactions with UI controls, which can only be accessed from the STA.

For example, in a UWP app, in the member function of any class that represents a XAML page, you can populate a [**ListBox**](/uwp/api/Windows.UI.Xaml.Controls.ListBox) control from within a [**task::then**][taskThen] method without having to use the [**Dispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) object.

``` cpp
#include <ppltasks.h>
void App::SetFeedText()
{    
    using namespace Windows::Web::Syndication;
    using namespace concurrency;
    String^ url = "http://windowsteamblog.com/windows_phone/b/wmdev/atom.aspx";
    SyndicationClient^ client = ref new SyndicationClient();
    auto feedOp = client->RetrieveFeedAsync(ref new Uri(url));

    create_task(feedOp).then([this]  (SyndicationFeed^ feed)
    {
        m_TextBlock1->Text = feed->Title->Text;
    });
}
```

If a task doesn't return an [**IAsyncAction**][IAsyncAction] or [**IAsyncOperation**][IAsyncOperation], then it's not apartment-aware and, by default, its continuations are run on the first available background thread.

You can override the default thread context for either kind of task by using the overload of [**task::then**][taskThen] that takes a [**task\_continuation\_context**](/cpp/parallel/concrt/reference/task-continuation-context-class). For example, in some cases, it might be desirable to schedule the continuation of an apartment-aware task on a background thread. In such a case, you can pass [**task\_continuation\_context::use\_arbitrary**][useArbitrary] to schedule the task’s work on the next available thread in a multi-threaded apartment. This can improve the performance of the continuation because its work doesn't have to be synchronized with other work that's happening on the UI thread.

The following example demonstrates when it's useful to specify the [**task\_continuation\_context::use\_arbitrary**][useArbitrary] option, and it also shows how the default continuation context is useful for synchronizing concurrent operations on non-thread-safe collections. In this code, we loop through a list of URLs for RSS feeds, and for each URL, we start up an async operation to retrieve the feed data. We can’t control the order in which the feeds are retrieved, and we don't really care. When each [**RetrieveFeedAsync**](/uwp/api/windows.web.syndication.isyndicationclient.retrievefeedasync) operation completes, the first continuation accepts the [**SyndicationFeed^**](/uwp/api/Windows.Web.Syndication.SyndicationFeed) object and uses it to initialize an app-defined `FeedData^` object. Because each of these operations is independent from the others, we can potentially speed things up by specifying the **task\_continuation\_context::use\_arbitrary** continuation context. However, after each `FeedData` object is initialized, we have to add it to a [**Vector**](/cpp/cppcx/platform-collections-vector-class), which is not a thread-safe collection. Therefore, we create a continuation and specify [**task\_continuation\_context::use\_current**](/cpp/parallel/concrt/reference/task-continuation-context-class?view=vs-2017) to ensure that all the calls to [**Append**](/uwp/api/windows.foundation.collections.ivector_t_.append) occur in the same Application Single-Threaded Apartment (ASTA) context. Because [**task\_continuation\_context::use\_default**](/cpp/parallel/concrt/reference/task-continuation-context-class?view=vs-2017) is the default context, we don’t have to specify it explicitly, but we do so here for the sake of clarity.

``` cpp
#include <ppltasks.h>
void App::InitDataSource(Vector<Object^>^ feedList, vector<wstring> urls)
{
                using namespace concurrency;
    SyndicationClient^ client = ref new SyndicationClient();

    std::for_each(std::begin(urls), std::end(urls), [=,this] (std::wstring url)
    {
        // Create the async operation. feedOp is an
        // IAsyncOperationWithProgress<SyndicationFeed^, RetrievalProgress>^
        // but we don't handle progress in this example.

        auto feedUri = ref new Uri(ref new String(url.c_str()));
        auto feedOp = client->RetrieveFeedAsync(feedUri);

        // Create the task object and pass it the async operation.
        // SyndicationFeed^ is the type of the return value
        // that the feedOp operation will eventually produce.

        // Then, initialize a FeedData object by using the feed info. Each
        // operation is independent and does not have to happen on the
        // UI thread. Therefore, we specify use_arbitrary.
        create_task(feedOp).then([this]  (SyndicationFeed^ feed) -> FeedData^
        {
            return GetFeedData(feed);
        }, task_continuation_context::use_arbitrary())

        // Append the initialized FeedData object to the list
        // that is the data source for the items collection.
        // This all has to happen on the same thread.
        // By using the use_default context, we can append
        // safely to the Vector without taking an explicit lock.
        .then([feedList] (FeedData^ fd)
        {
            feedList->Append(fd);
            OutputDebugString(fd->Title->Data());
        }, task_continuation_context::use_default())

        // The last continuation serves as an error handler. The
        // call to get() will surface any exceptions that were raised
        // at any point in the task chain.
        .then( [this] (task<void> t)
        {
            try
            {
                t.get();
            }
            catch(Platform::InvalidArgumentException^ e)
            {
                //TODO handle error.
                OutputDebugString(e->Message->Data());
            }
        }); //end task chain

    }); //end std::for_each
}
```

Nested tasks, which are new tasks that are created inside a continuation, don't inherit apartment-awareness of the initial task.

## Handing progress updates
Methods that support [**IAsyncOperationWithProgress**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_) or [**IAsyncActionWithProgress**](/uwp/api/Windows.Foundation.IAsyncActionWithProgress_TProgress_) provide progress updates periodically while the operation is in progress, before it completes. Progress reporting is independent from the notion of tasks and continuations. You just supply the delegate for the object’s [**Progress**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_) property. A typical use of the delegate is to update a progress bar in the UI.

## Related topics
* [Creating Asynchronous Operations in C++/CX for UWP apps](/cpp/parallel/concrt/creating-asynchronous-operations-in-cpp-for-windows-store-apps)
* [Visual C++ Language Reference](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Asynchronous Programming][AsyncProgramming]
* [Task Parallelism (Concurrency Runtime)][taskParallelism]
* [concurrency::task](/cpp/parallel/concrt/reference/task-class)

<!-- LINKS -->
[AsyncProgramming]: ./asynchronous-programming-universal-windows-platform-apps.md "AsyncProgramming"
[concurrencyNamespace]: /cpp/parallel/concrt/reference/concurrency-namespace "Concurrency Namespace"
[createTask]: /cpp/parallel/concrt/reference/concurrency-namespace-functions#create_task "CreateTask"
[deleteAsync]: /uwp/api/Windows.Storage.StorageFile "DeleteAsync"
[IAsyncAction]: /uwp/api/Windows.Foundation.IAsyncAction "IAsyncAction"
[IAsyncOperation]: /uwp/api/windows.foundation.iasyncoperation-1 "IAsyncOperation"
[IAsyncInfo]: /uwp/api/Windows.Foundation.IAsyncInfo "IAsyncInfo"
[IAsyncInfoCancel]: /uwp/api/Windows.Foundation.IAsyncInfo "IAsyncInfoCancel"
[taskCanceled]: /cpp/parallel/concrt/reference/task-canceled-class "TaskCancelled"
[task-class]: /cpp/parallel/concrt/reference/task-class#get "Task Class"
[taskGet]: /cpp/parallel/concrt/reference/task-class "TaskGet"
[taskParallelism]: /cpp/parallel/concrt/task-parallelism-concurrency-runtime "Task Parallelism"
[taskThen]: /cpp/parallel/concrt/reference/task-class#then "TaskThen"
[useArbitrary]: /cpp/parallel/concrt/reference/task-continuation-context-class "UseArbitrary"