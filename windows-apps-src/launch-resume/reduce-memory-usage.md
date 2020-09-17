---
ms.assetid: 3a3ea86e-fa47-46ee-9e2e-f59644c0d1db
description: This article shows you how to reduce memory when your app moves to the background.
title: Reduce memory usage when your app moves to the background state
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Free memory when your app moves to the background

This article shows you how to reduce the amount of memory that your app uses when it moves to the background state so that it won't be suspended and possibly terminated.

## New background events

Windows 10, version 1607, introduces two new application lifecycle events, [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) and [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground). These events let your app know when it is entering and leaving the background.

When your app moves into the background, the memory constraints enforced by the system may change. Use these events to check your current memory consumption and free resources in order to stay below the limit so that your app won't be suspended and possibly terminated while it is in the background.

### Events for controlling your app's memory usage

[MemoryManager.AppMemoryUsageLimitChanging](/uwp/api/windows.system.memorymanager.appmemoryusagelimitchanging) is raised just before the limit of total memory the app can use is changed. For example, when the app moves into the background and on the Xbox the memory limit changes from 1024MB to 128MB.  
This is the most important event to handle to keep the platform from suspending or terminating the app.

[MemoryManager.AppMemoryUsageIncreased](/uwp/api/windows.system.memorymanager.appmemoryusageincreased) is raised when the app's memory consumption has increased to a higher value in the [AppMemoryUsageLevel](/uwp/api/windows.system.appmemoryusagelevel) enumeration. For example, from **Low** to **Medium**. Handling this event is optional but recommended because the application is still responsible for staying under the limit.

[MemoryManager.AppMemoryUsageDecreased](/uwp/api/windows.system.memorymanager.appmemoryusagedecreased) is raised when the app's memory consumption has decreased to a lower value in the **AppMemoryUsageLevel** enumeration. For example, from **High** to **Low**. Handling this event is optional but indicates the application may be able to allocate additional memory if needed.

## Handle the transition between foreground and background

When your app moves from the foreground to the background, the [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) event is raised. When your app returns to the foreground, the [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground) event is raised. You can register handlers for these events when your app is created. In the default project template, this is done in the **App** class constructor in App.xaml.cs.

Because running in the background will reduce the memory resources your app is allowed to retain, you should also register for the [**AppMemoryUsageIncreased**](/uwp/api/windows.system.memorymanager.appmemoryusageincreased) and [**AppMemoryUsageLimitChanging**](/uwp/api/windows.system.memorymanager.appmemoryusagelimitchanging) events which you can use to check your app's current memory usage and the current limit. The handlers for these events are shown in the following examples. For more information on the application lifecycle for UWP apps, see [App lifecycle](..//launch-resume/app-lifecycle.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetRegisterEvents":::

When the [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) event is raised, set the tracking variable to indicate that you are currently running in the background. This will be useful when you write the code for reducing memory usage.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetEnteredBackground":::

When your app transitions to the background, the system reduces the memory limit for the app to ensure that the current foreground app has sufficient resources to provide a responsive user experience

The [**AppMemoryUsageLimitChanging**](/uwp/api/windows.system.memorymanager.appmemoryusagelimitchanging) event handler lets your app know that its allotted memory has been reduced and provides the new limit in the event arguments passed into the handler. Compare the [**MemoryManager.AppMemoryUsage**](/uwp/api/windows.system.memorymanager.appmemoryusage) property, which provides your app's current usage, to the [**NewLimit**](/uwp/api/windows.system.appmemoryusagelimitchangingeventargs.newlimit) property of the event arguments, which specifies the new limit. If your memory usage exceeds the limit, you need to reduce your memory usage.

In this example, this is done in the helper method **ReduceMemoryUsage**, which is defined later in this article.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetMemoryUsageLimitChanging":::

> [!NOTE]
> Some device configurations will allow an application to continue running over the new memory limit until the system experiences resource pressure, and some will not. On Xbox, in particular, apps will be suspended or terminated if they do not reduce memory to under the new limits within 2 seconds. This means that you can deliver the best experience across the broadest range of devices by using this event to reduce resource usage below the limit within 2 seconds of the event being raised.

It is possible that although your app's memory usage is currently under the memory limit for background apps when it first transitions to the background, it may increase its memory consumption over time and begin to approach the limit. The handler the [**AppMemoryUsageIncreased**](/uwp/api/windows.system.memorymanager.appmemoryusageincreased) provides an opportunity to check your current usage when it increases and, if necessary, free memory.

Check to see if the [**AppMemoryUsageLevel**](/uwp/api/Windows.System.AppMemoryUsageLevel) is **High** or **OverLimit**, and if so, reduce your memory usage. In this example this is handled by the helper method, **ReduceMemoryUsage**. You can also subscribe to the [**AppMemoryUsageDecreased**](/uwp/api/windows.system.memorymanager.appmemoryusagedecreased) event, check to see if your app is under the limit, and if so then you know you can allocate additional resources.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetMemoryUsageIncreased":::

**ReduceMemoryUsage** is a helper method that you can implement to release memory when your app is over the usage limit while running in the background. How you release memory depends on the specifics of your app, but one recommended way to free up memory is to dispose of your UI and the other resources associated with your app view. To do so, ensure that you are running in the background state then set the [**Content**](/uwp/api/windows.ui.xaml.window.content) property of your app's window to `null` and unregister your UI event handlers and remove any other references you may have to the page. Failing to unregister your UI event handlers and clearing any other references you may have to the page will prevent the page resources from being released. Then call **GC.Collect** to reclaim the freed up memory immediately. Typically you don't force garbage collection because the system will take care of it for you. In this specific case, we are reducing the amount of memory charged to this application as it goes into the background to reduce the likelihood that the system will determine that it should terminate the app to reclaim memory.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetUnloadViewContent":::

When the window content is collected, each Frame begins its disconnection process. If there are Pages in the visual object tree under the window content, these will begin firing their Unloaded event. Pages cannot be completely cleared from memory unless all references to them are removed. In the Unloaded callback, do the following to ensure that memory is quickly freed:
* Clear and set any large data structures in your Page to `null`.
* Unregister all event handlers that have callback methods within the Page. Make sure to register those callbacks during the Loaded event handler for the Page. The Loaded event is raised when the UI has been reconstituted and the Page has been added to the visual object tree.
* Call `GC.Collect` at the end of the Unloaded callback to quickly garbage collect any of the large data structures you have just set to `null`. Again, typically you don't force garbage collection because the system will take care of it for you. In this specific case, we are reducing the amount of memory charged to this application as it goes into the background to reduce the likelihood that the system will determine that it should terminate the app to reclaim memory.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetMainPageUnloaded":::

In the [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground) event handler, set the tracking variable (`isInBackgroundMode`) to indicate that your app is no longer running in the background. Next, check to see if the [**Content**](/uwp/api/windows.ui.xaml.window.content) of the current window is `null`-- which it will be if you disposed of your app views in order to clear up memory while you were running in the background. If the window content is `null`, rebuild your app view. In this example, the window content is created in the helper method **CreateRootFrame**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetLeavingBackground":::

The **CreateRootFrame** helper method recreates the view content for your app. The code in this method is nearly identical to the [**OnLaunched**](/uwp/api/windows.ui.xaml.application.onlaunched) handler code provided in the default project template. The one difference is that the **Launching** handler determines the previous execution state from the [**PreviousExecutionState**](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs.previousexecutionstate) property of the [**LaunchActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.LaunchActivatedEventArgs) and the **CreateRootFrame** method simply gets the previous execution state passed in as an argument. To minimize duplicated code, you can refactor the default **Launching** event handler code to call **CreateRootFrame**.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/ReduceMemory/cs/App.xaml.cs" id="SnippetCreateRootFrame":::

## Guidelines

### Moving from the foreground to the background

When an app moves from the foreground to the background, the system does work on behalf of the app to free up resources that are not needed in the background. For example, the UI frameworks flush cached textures and the video subsystem frees memory allocated on behalf of the app. However, an app will still need to carefully monitor its memory usage to avoid being suspended or terminated by the system.

When an app moves from the foreground to the background it will first get an **EnteredBackground** event and then a **AppMemoryUsageLimitChanging** event.

- **Do** use the **EnteredBackground** event to free up UI resources that you know your app does not need while running in the background. For example, you could free the cover art image for a song.
- **Do** use the **AppMemoryUsageLimitChanging** event to ensure that your app is using less memory than the new background limit. Make sure that you free up resources if not. If you do not, your app may be suspended or terminated according to device specific policy.
- **Do** manually invoke the garbage collector if your app is over the new memory limit when the **AppMemoryUsageLimitChanging** event is raised.
- **Do** use the **AppMemoryUsageIncreased** event to continue to monitor your app’s memory usage while running in the background if you expect it to change. If the **AppMemoryUsageLevel** is **High** or **OverLimit** make sure that you free up resources.
- **Consider** freeing UI resources in the **AppMemoryUsageLimitChanging** event handler instead of in the **EnteredBackground** handler as a performance optimization. Use a boolean value set in the **EnteredBackground/LeavingBackground** event handlers to track whether the app is in the background or foreground. Then in the **AppMemoryUsageLimitChanging** event handler, if **AppMemoryUsage** is over the limit and the app is in the background (based on the Boolean value) you can free UI resources.
- **Do not** perform long running operations in the **EnteredBackground** event because you can cause the transition between applications to appear slow to the user.

### Moving from the background to the foreground

When an app moves from the background to the foreground, the app will first get an **AppMemoryUsageLimitChanging** event and then a **LeavingBackground** event.

- **Do** use the **LeavingBackground** event to recreate UI resources that your app discarded when moving into the background.

## Related topics

* [Background media playback sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundMediaPlayback) - shows how to free memory when your app moves to the background state.
* [Diagnostic Tools](https://devblogs.microsoft.com/devops/diagnostic-tools-debugger-window-in-visual-studio-2015/) - use the diagnostic tools to observe garbage collection events and validate that your app is releasing memory the way you expect it to.
