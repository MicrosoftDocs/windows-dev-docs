---
ms.assetid: 066711E0-D5C4-467E-8683-3CC64EDBCC83
title: Call asynchronous APIs in C# or Visual Basic
description: The Universal Windows Platform (UWP) includes many asynchronous APIs to ensure that your app remains responsive when it does work that might take an extended amount of time.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, C#, Visual Basic, asynchronous
ms.localizationpriority: medium
---
# Call asynchronous APIs in C# or Visual Basic



The Universal Windows Platform (UWP) includes many asynchronous APIs to ensure that your app remains responsive when it does work that might take an extended amount of time. This topic discusses how to use asynchronous methods from the UWP in C# or Microsoft Visual Basic.

Asynchronous APIs keep your app from waiting for large operations to complete before continuing execution. For example, an app that downloads info from the Internet might spend several seconds waiting for the info to arrive. If you use a synchronous method to retrieve the info, the app is blocked until the method returns. The app won't respond to user interaction and because it seems non-responsive, the user might become frustrated. By providing asynchronous APIs, the UWP helps to ensure that your app stays responsive to the user when it's performing long operations.

Most of the asynchronous APIs in the UWP don't have synchronous counterparts, so you need to be sure to understand how to use the asynchronous APIs with C# or Visual Basic in your Universal Windows Platform (UWP) app. Here we show how to call asynchronous APIs of the UWP.

## Using asynchronous APIs


By convention, asynchronous methods are given names that end in "Async". You typically call asynchronous APIs in response to a user's action, such as when the user clicks a button. Calling an asynchronous method in an event handler is one of the simplest ways of using asynchronous APIs. Here we use the **await** operator as an example.

Suppose that you have an app that lists the titles of blog posts from a certain location. The app has a [**Button**](https://msdn.microsoft.com/library/windows/apps/BR209265) that the user clicks to get the titles. The titles are displayed in a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652). When the user clicks the button, it is important that the app remains responsive while it waits for the info from the blog's website. To ensure this responsiveness, the UWP provides an asynchronous method, [**SyndicationClient.RetrieveFeedAsync**](https://msdn.microsoft.com/library/windows/apps/BR243460), to download the feed.

The example here gets the lists of blog posts from a blog by calling the asynchronous method, [**SyndicationClient.RetrieveFeedAsync**](https://msdn.microsoft.com/library/windows/apps/BR243460), and awaiting the result.

> [!div class="tabbedCodeSnippets" data-resources="OutlookServices.Calendar"]
[!code-csharp[Main](./AsyncSnippets/csharp/MainPage.xaml.cs#SnippetDownloadRSS)]
[!code-vb[Main](./AsyncSnippets/vbnet/MainPage.xaml.vb#SnippetDownloadRSS)]

There are a couple of important things about this example. First, the line, `SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri)` uses the **await** operator with the call to the asynchronous method, [**RetrieveFeedAsync**](https://msdn.microsoft.com/library/windows/apps/BR243460). You can think of the **await** operator as telling the compiler that you are calling an asynchronous method, which causes the compiler to do some extra work so you don’t have to. Next, the declaration of the event handler includes the keyword **async**. You must include this keyword in the method declaration of any method in which you use the **await** operator.

In this topic, we won't go into a lot of the details of what the compiler does with the **await** operator, but let's examine what your app does so that it is asynchronous and responsive. Consider what happens when you use synchronous code. For example, suppose that there is a method called `SyndicationClient.RetrieveFeed` that is synchronous. (There is no such method, but imagine that there is.) If your app included the line `SyndicationFeed feed = client.RetrieveFeed(feedUri)`, instead of `SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri)`, execution of the app would stop until the return value of `RetrieveFeed` is available. And while your app waits for the method to complete, it can't respond to any other events, such another [**Click**](https://msdn.microsoft.com/library/windows/apps/BR227737) event. That is, your app would be blocked until `RetrieveFeed` returns.

But if you call `client.RetrieveFeedAsync`, the method initiates the retrieval and immediately returns. When you use **await** with [**RetrieveFeedAsync**](https://msdn.microsoft.com/library/windows/apps/BR243460), the app temporarily exits the event handler. Then it can process other events while **RetrieveFeedAsync** executes asynchronously. This keeps the app responsive to the user. When **RetrieveFeedAsync** completes and the [**SyndicationFeed**](https://msdn.microsoft.com/library/windows/apps/BR243485) is available, the app essentially reenters the event handler where it left off, after `SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri)`, and finishes the rest of the method.

The nice thing about using the **await** operator is that the code doesn't look much different from how the code looks if you used the imaginary `RetrieveFeed` method. There are ways to write asynchronous code in C# or Visual Basic without the **await** operator, but the resulting code tends to emphasize the mechanics of executing asynchronously. This makes asynchronous code hard to write, hard to understand, and hard to maintain. By using the **await** operator, you get the benefits of an asynchronous app without making your code complex.

## Return types and results of asynchronous APIs


If you followed the link to [**RetrieveFeedAsync**](https://msdn.microsoft.com/library/windows/apps/BR243460), you might have noticed that the return type of **RetrieveFeedAsync** is not a [**SyndicationFeed**](https://msdn.microsoft.com/library/windows/apps/BR243485). Instead, the return type is `IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress>`. Viewed from the raw syntax, an asynchronous API returns an object that contains the result within it. While it is common, and sometimes useful, to think of an asynchronous method as being awaitable, the **await** operator actually operates on the method’s return value, not on the method. When you apply the **await** operator, what you get back is the result of calling **GetResult** on the object returned by the method. In the example, the **SyndicationFeed** is the result of **RetrieveFeedAsync.GetResult()**.

When you use an asynchronous method, you can examine the signature to see what you’ll get back after awaiting the value returned from the method. All asynchronous APIs in the UWP return one of the following types:

-   [**IAsyncOperation&lt;TResult&gt;**](https://msdn.microsoft.com/library/windows/apps/BR206598)
-   [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](https://msdn.microsoft.com/library/windows/apps/BR206594)
-   [**IAsyncAction**](https://msdn.microsoft.com/library/windows/apps/windows.foundation.iasyncaction.aspx)
-   [**IAsyncActionWithProgress&lt;TProgress&gt;**](https://msdn.microsoft.com/library/windows/apps/br206581.aspx)

The result type of an asynchronous method is the same as the `      TResult` type parameter. Types without a `TResult` don't have a result. You can think of the result as being **void**. In Visual Basic, a [Sub](https://msdn.microsoft.com/library/windows/apps/xaml/831f9wka.aspx) procedure is equivalent to a method with a **void** return type.

The table here gives examples of asynchronous methods and lists the return type and result type of each.

| Asynchronous method                                                                           | Return type                                                                                                                                        | Result type                                       |
|-----------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------|
| [**SyndicationClient.RetrieveFeedAsync**](https://msdn.microsoft.com/library/windows/apps/BR243460)     | [**IAsyncOperationWithProgress&lt;SyndicationFeed, RetrievalProgress&gt;**](https://msdn.microsoft.com/library/windows/apps/BR206594)                                 | [**SyndicationFeed**](https://msdn.microsoft.com/library/windows/apps/BR243485) |
| [**FileOpenPicker.PickSingleFileAsync**](https://msdn.microsoft.com/library/windows/apps/JJ635275) | [**IAsyncOperation&lt;StorageFile&gt;**](https://msdn.microsoft.com/library/windows/apps/BR206598)                                                                                | [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/BR227171)          |
| [**XmlDocument.SaveToFileAsync**](https://msdn.microsoft.com/library/windows/apps/BR206284)                 | [**IAsyncAction**](https://msdn.microsoft.com/library/windows/apps/windows.foundation.iasyncaction.aspx)                                                                                                           | **void**                                          |
| [**InkStrokeContainer.LoadAsync**](https://msdn.microsoft.com/library/windows/apps/Hh701757)               | [**IAsyncActionWithProgress&lt;UInt64&gt;**](https://msdn.microsoft.com/library/windows/apps/br206581.aspx)                                                                   | **void**                                          |
| [**DataReader.LoadAsync**](https://msdn.microsoft.com/library/windows/apps/BR208135)                            | [**DataReaderLoadOperation**](https://msdn.microsoft.com/library/windows/apps/BR208120), a custom results class that implements **IAsyncOperation&lt;UInt32&gt;** | [**UInt32**](https://msdn.microsoft.com/library/windows/apps/br206598.aspx)                     |

 

Asynchronous methods that are defined in [**.NET for UWP apps**](https://msdn.microsoft.com/library/windows/apps/xaml/br230232.aspx) have the return type [**Task**](https://msdn.microsoft.com/library/windows/apps/xaml/system.threading.tasks.task.aspx) or [**Task&lt;TResult&gt;**](https://msdn.microsoft.com/library/windows/apps/xaml/dd321424.aspx). Methods that return **Task** are similar to the asynchronous methods in the UWP that return [**IAsyncAction**](https://msdn.microsoft.com/library/windows/apps/windows.foundation.iasyncaction.aspx). In each case, the result of the asynchronous method is **void**. The return type **Task&lt;TResult&gt;** is similar to [**IAsyncOperation&lt;TResult&gt;**](https://msdn.microsoft.com/library/windows/apps/BR206598) in that the result of the asynchronous method when running the task is the same type as the `TResult` type parameter. For more info about using **.NET for UWP apps** and tasks, see [.NET for Windows Runtime apps overview](https://msdn.microsoft.com/library/windows/apps/xaml/br230302.aspx).

## Handling errors


When you use the **await** operator to retrieve your results from an asynchronous method, you can use a **try/catch** block to handle errors that occur in asynchronous methods, just as you do for synchronous methods. The previous example wraps the **RetrieveFeedAsync** method and **await** operation in a **try/catch** block to handle errors when an exception is thrown.

When asynchronous methods call other asynchronous methods, any asynchronous method that results in an exception will be propagated to the outer methods. This means that you can put a **try/catch** block on the outer-most method to catch errors for the nested asynchronous methods. Again, this is similar to how you catch exceptions for synchronous methods. However, you can't use **await** in the **catch** block.

**Tip**  Starting with C# in Microsoft Visual Studio 2005, you can use **await** in the **catch** block.

## Summary and next steps

The pattern of calling an asynchronous method that we show here is the simplest one to use when you call asynchronous APIs in an event handler. You can also use this pattern when you call an asynchronous method in an overridden method that returns **void** or a **Sub** in Visual Basic.

As you encounter asynchronous methods in the UWP, it is important to remember:

-   By convention, asynchronous methods are given names that end in "Async".
-   Any method that uses the **await** operator must have its declaration marked with the **async** keyword.
-   When an app finds the **await** operator, the app remains responsive to user interaction while the asynchronous method executes.
-   Awaiting the value returned by an asynchronous method returns an object that contains the result. In most cases, the result contained within the return value is what's useful, not the return value itself. You can find the type of the value that is contained inside the result by looking at the return type of the async method.
-   Using asynchronous APIs and **async** patterns is often a way to improve the responsiveness of your app.

The example in this topic outputs text that looks like this.

``` syntax
Windows Experience Blog
PC Snapshot: Sony VAIO Y, 8/9/2011 10:26:56 AM -07:00
Tech Tuesday Live Twitter #Chat: Too Much Tech #win7tech, 8/8/2011 12:48:26 PM -07:00
Windows 7 themes: what’s new and what’s popular!, 8/4/2011 11:56:28 AM -07:00
PC Snapshot: Toshiba Satellite A665 3D, 8/2/2011 8:59:15 AM -07:00
Time for new school supplies? Find back-to-school deals on Windows 7 PCs and Office 2010, 8/1/2011 2:14:40 PM -07:00
Best PCs for blogging (or working) on the go, 8/1/2011 10:08:14 AM -07:00
Tech Tuesday – Blogging Tips and Tricks–#win7tech, 8/1/2011 9:35:54 AM -07:00
PC Snapshot: Lenovo IdeaPad U460, 7/29/2011 9:23:05 AM -07:00
GIVEAWAY: Survive BlogHer with a Sony VAIO SA and a Samsung Focus, 7/28/2011 7:27:14 AM -07:00
3 Ways to Stay Cool This Summer, 7/26/2011 4:58:23 PM -07:00
Getting RAW support in Photo Gallery & Windows 7 (…and a contest!), 7/26/2011 10:40:51 AM -07:00
Tech Tuesdays Live Twitter Chats: Photography Tips, Tricks and Essentials, 7/25/2011 12:33:06 PM -07:00
3 Tips to Go Green With Your PC, 7/22/2011 9:19:43 AM -07:00
How to: Buy a Green PC, 7/22/2011 9:13:22 AM -07:00
Windows 7 themes: the distinctive artwork of Cheng Ling, 7/20/2011 9:53:07 AM -07:00
```
