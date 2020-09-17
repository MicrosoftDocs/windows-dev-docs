---
ms.assetid: E1943DCE-833F-48AE-8402-CD48765B24FC
title: Optimize suspend/resume
description: Create Universal Windows Platform (UWP) apps that streamline their use of the process lifetime system to resume efficiently after suspension or termination.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Optimize suspend/resume


Create Universal Windows Platform (UWP) apps that streamline their use of the process lifetime system to resume efficiently after suspension or termination.

## Launch

When reactivating an app following suspend/terminate, check to see if a long time has elapsed. If so, consider returning to the main landing page of the app instead of showing the user stale data. This will also result in faster startup.

During activation, always check the PreviousExecutionState of the event args parameter (for example, for launched activations check LaunchActivatedEventArgs.PreviousExecutionState). If the value is ClosedByUser or NotRunning, don’t waste time restoring previously saved state. In this case, the right thing is to provide a fresh experience – and it will result in faster startup.

Instead of eagerly restoring previously saved state, consider keep track of that state, and only restoring it on demand. For example, consider a situation where your app was previously suspended, saved state for 3 pages, and was then terminated. Upon relaunch, if you decide to return the user to the 3rd page, do not eagerly restore the state for the first 2 pages. Instead, hold on to this state and only use it once you know you need it.

## While running

As a best practice, don’t wait for the suspend event and then persist a large amount of state. Instead, your application should incrementally persist smaller amounts of state as it runs. This is especially important for large apps that are at risk of running out of time during suspend if they try to save everything at once.

However, you need to find a good balance between incremental saving and performance of your app while running. A good tradeoff is to incrementally keep track of the data that has changed (and therefore needs to be saved) – and use the suspend event to actually save that data (which is faster than saving all data or examining the entire state of app to decide what to save).

Don’t use the window Activated or VisibilityChanged events to decide when to save state. When the user switches away from your app, the window is deactivated, but the system waits a short amount of time (about 10 seconds) before suspending the app. This is to give a more responsive experience in case the user switches back to your app rapidly. Wait for the suspend event before running suspend logic.

## Suspend

During suspend, reduce the footprint of your app. If your app uses less memory while suspended, the overall system will be more responsive and fewer suspended apps (including yours) will be terminated. However, balance this with the need for snappy resumes: don’t reduce footprint so much that resume slows down considerably while your app reloads lots of data into memory.

For managed apps, the system will run a garbage collection pass after the app’s suspend handlers complete. Make sure to take advantage of this by releasing references to objects that will help reduce the app’s footprint while suspended.

Ideally, your app will finish with suspend logic in less than 1 second. The faster you can suspend, the better – that will result in a snappier user experience for other apps and parts of the system. If you must, your suspend logic can take up to 5 seconds on desktop devices or 10 seconds on mobile devices. If those times are exceeded, your app will be abruptly terminated. You don’t want this to happen – because if it does, when the user switches back to your app, a new process will be launched and the experience will feel much slower compared to resuming a suspended app.

## Resume

Most apps don’t need to do anything special when resumed, so typically you won’t handle this event. Some apps use resume to restore connections that were closed during suspend, or to refresh data that may be stale. Instead of doing this kind of work eagerly, design your app to initiate these activities on demand. This will result in a faster experience when the user switches back to a suspended app, and ensures that you’re only doing work the user really needs.

## Avoid unnecessary termination

The UWP process lifetime system can suspend or terminate an app for a variety of reasons. This process is designed to quickly return an app to the state it was in before it was suspended or terminated. When done well, the user won’t be aware that the app ever stopped running. Here are a few tricks that your UWP app can use to help the system streamline transitions in an app’s lifetime.

An app can be suspended when the user moves it to the background or when the system enters a low power state. When the app is being suspended, it raises the suspending event and has up to 5 seconds to save its data. If the app's suspending event handler doesn't complete within 5 seconds, the system assumes the app has stopped responding and terminates it. A terminated app has to go through the long startup process again instead of being immediately loaded into memory when a user switches to it.

### Serialize only when necessary

Many apps serialize all their data on suspension. If you only need to store a small amount of app settings data, however, you should use the [**LocalSettings**](/uwp/api/windows.storage.applicationdata.localsettings) store instead of serializing the data. Use serialization for larger amounts of data and for non-settings data.

When you do serialize your data, you should avoid reserializing if it hasn't changed. It takes extra time to serialize and save the data, plus extra time to read and deserialize it when the app is activated again. Instead, we recommend that the app determine if its state has actually changed, and if so, serialize and deserialize only the data that changed. A good way to ensure that this happens is to periodically serialize data in the background after it changes. When you use this technique, everything that needs to be serialized at suspension has already been saved so there is no work to do and an app suspends quickly.

### Serializing data in C# and Visual Basic

The available choices of serialization technology for .NET apps are the [**System.Xml.Serialization.XmlSerializer**](/dotnet/api/system.xml.serialization.xmlserializer), [**System.Runtime.Serialization.DataContractSerializer**](/dotnet/api/system.runtime.serialization.datacontractserializer), and [**System.Runtime.Serialization.Json.DataContractJsonSerializer**](/dotnet/api/system.runtime.serialization.json.datacontractjsonserializer) classes.

From a performance perspective, we recommend using the [**XmlSerializer**](/dotnet/api/system.xml.serialization.xmlserializer) class. The **XmlSerializer** has the lowest serialization and deserialization times, and maintains a low memory footprint. The **XmlSerializer** has few dependencies on the .NET framework; this means that compared with the other serialization technologies, fewer modules need to be loaded into your app to use the **XmlSerializer**.

[**DataContractSerializer**](/dotnet/api/system.runtime.serialization.datacontractserializer) makes it easier to serialize custom classes, although it has a larger performance impact than **XmlSerializer**. If you need better performance, consider switching. In general, you should not load more than one serializer, and you should prefer **XmlSerializer** unless you need the features of another serializer.

### Reduce memory footprint

The system tries to keep as many suspended apps in memory as possible so that users can quickly and reliably switch between them. When an app is suspended and stays in the system's memory, it can quickly be brought to the foreground for the user to interact with, without having to display a splash screen or perform a lengthy load operation. If there aren't enough resources to keep an app in memory, the app is terminated. This makes memory management important for two reasons:

-   Freeing as much memory as possible at suspension minimizes the chances that your app is terminated because of lack of resources while it’s suspended.
-   Reducing the overall amount of memory your app uses reduces the chances that other apps are terminated while they are suspended.

### Release resources

Certain objects, such as files and devices, occupy a large amount of memory. We recommend that during suspension, an app release handles to these objects and recreate them when needed. This is also a good time to purge any caches that won’t be valid when the app is resumed. An additional step the XAML framework runs on your behalf for C# and Visual Basic apps is garbage collection if it is necessary. This ensures any objects no longer referenced in app code are released.

## Resume quickly

A suspended app can be resumed when the user moves it to the foreground or when the system comes out of a low power state. When an app is resumed from the suspended state, it continues from where it was when it was suspended. No app data is lost because it was stored in memory, even if the app was suspended for a long period of time.

Most apps don't need to handle the [**Resuming**](/uwp/api/windows.applicationmodel.core.coreapplication.resuming) event. When your app is resumed, variables and objects have the exact same state they had when the app was suspended. Handle the **Resuming** event only if you need to update data or objects that might have changed between the time your app was suspended and when it was resumed such as: content (for example, update feed data), network connections that may have gone stale, or if you need to reacquire access to a device (for example, a webcam).

## Related topics

* [Guidelines for app suspend and resume](../launch-resume/index.md)
 

 