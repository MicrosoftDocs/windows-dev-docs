---
title: Universal Windows Platform (UWP) application lifecycle
description: This topic describes the lifecycle of a Universal Windows Platform (UWP) application from the time it is activated until it is closed.
keywords: app lifecycle suspended resume launch activate
ms.assetid: 6C469E77-F1E3-4859-A27B-C326F9616D10
ms.date: 09/13/2023
ms.topic: article
ms.localizationpriority: medium
---

# Universal Windows Platform (UWP) app lifecycle

This topic describes the lifecycle of a Universal Windows Platform (UWP) app from the time it is launched until it is closed.

## A little history

Before Windows 8, apps had a simple lifecycle. Win32 and .NET apps are either running or not running. When a user minimizes them, or switches away from them, they continue to run. This was fine until portable devices and power management became increasingly important.

Windows 8 introduced a new application model with UWP apps. At a high level, a new suspended state was added. A UWP app is suspended shortly after the user minimizes it or switches to another app. This means that the app's threads are stopped and the app is left in memory unless the operating system needs to reclaim resources. When the user switches back to the app, it can be quickly restored to a running state.

There are various ways for apps that need to continue to run when they are in the background such as [background tasks](support-your-app-with-background-tasks.md), [extended execution](/uwp/api/windows.applicationmodel.extendedexecution), and activity sponsored execution (for example, the **BackgroundMediaEnabled** capability which allows an app to continue to [play media in the background](../audio-video-camera/background-audio.md)). Also, background transfer operations can continue even if your app is suspended or even terminated. For more info, see [How to download a file](/previous-versions/windows/apps/jj152726(v=win.10)).

By default, apps that are not in the foreground are suspended. This results in power savings and more resources available for the app currently in the foreground.

The suspended state adds new requirements for you as a developer because the operating system may elect to terminate a suspended app in order to free up resources. The terminated app will still appear in the task bar. When the user click on it, the app must restore the state that it was in before it was terminated because the user will not be aware that the system closed the app. They will think that it has been waiting in the background while they were doing other things and will expect it to be in the same state it was in when they left it. In this topic we will look at how to accomplish that.

Windows 10, version 1607, introduced two more app model states: **Running in foreground** and **Running in background**. We will look at these additional states in the sections that follow.

## App execution state

This illustration represents the possible app model states starting in Windows 10, version 1607. Let's walk through the typical lifecycle of a UWP app.

![state diagram showing transitions between app execution states](images/updated-lifecycle.png)

Apps enter the running in background state when they are launched or activated. If the app needs to move into the foreground due to a foreground app launch, the app then gets the [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground) event.

Although "launched" and "activated" may seem like similar terms, they refer to different ways the operating system may start your app. Let's first look at launching an app.

## App launch

The [**OnLaunched**](/uwp/api/windows.ui.xaml.application.onlaunched) method is called when an app is launched. It is passed a [**LaunchActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.LaunchActivatedEventArgs) parameter which provides, among other things, the arguments passed to the app, the identifier of the tile that launched the app, and the previous state that the app was in.

Get the previous state of your app from [LaunchActivatedEventArgs.PreviousExecutionState](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs.previousexecutionstate) which returns an [ApplicationExecutionState](/uwp/api/windows.applicationmodel.activation.applicationexecutionstate). Its values and the appropriate action to take due to that state are as follows:

| ApplicationExecutionState | Explanation | Action to take |
|-------|-------------|----------------|
| **NotRunning** | An app could be in this state because it hasn't been launched since the last time the user rebooted or logged in. It can also be in this state if it was running but then crashed, or because the user closed it earlier.| Initialize the app as if it is running for the first time in the current user session. |
|**Suspended** | The user either minimized or switched away from your app and didn't return to it within a few seconds. | When the app was suspended, its state remained in memory. You only need to reacquire any file handles or other resources you released when the app was suspended. |
| **Terminated** | The app was previously suspended but was then shutdown at some point because the system needed to reclaim memory. | Restore the state that the app was in when the user switched away from it.|
|**ClosedByUser** | The user closed the app with the system close button, or with Alt+F4. When the user closes the app, it is first suspended and then terminated. | Because the app has essentially gone through the same steps that lead to the Terminated state, handle this the same way you would the Terminated state.|
|**Running** | The app was already open when the user tried to launch it again. | Nothing. Note that another instance of your app is not launched. The already running instance is simply activated. |

> [!NOTE]
> _Current user session_ is based on Windows logon. As long as the current user hasn't logged off, shut down, or restarted Windows, the current user session persists across events such as lock screen authentication, switch-user, and so on.

One important circumstance to be aware of is that if the device has sufficient resources, the operating system will prelaunch frequently used apps that have opted in for that behavior in order to optimize responsiveness. Apps that are prelaunched are launched in the background and then quickly suspended so that when the user switches to them, they can be resumed which is faster than launching the app.

Because of prelaunch, the app's **OnLaunched()** method may be initiated by the system rather than by the user. Because the app is prelaunched in the background you may need to take different action in **OnLaunched()**. For example, if your app starts playing music when launched, they will not know where it is coming from because the app is prelaunched in the background. Once your app is prelaunched in the background, it is followed by a call to **Application.Suspending**. Then, when the user does launch the app, the resuming event is invoked as well as the **OnLaunched()** method. See [Handle app prelaunch](handle-app-prelaunch.md) for additional information about how to handle the prelaunch scenario. Only apps that opt-in are prelaunched.

Windows displays a splash screen for the app when it is launched. To configure the splash screen, see [Adding a splash screen](/previous-versions/windows/apps/hh465331(v=win.10)).

While the splash screen is displayed, your app should register event handlers and set up any custom UI it needs for the initial page. See that these tasks running in the application's constructor and **OnLaunched()** are completed within a few seconds or the system may think your app is unresponsive and terminate it. If an app needs to request data from the network or needs to retrieve large amounts of data from disk, these activities should be completed outside of launch. An app can use its own custom loading UI or an extended splash screen while it waits for long running operations to finish. See [Display a splash screen for more time](create-a-customized-splash-screen.md) and the [Splash screen sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/SplashScreen) for more info.

After the app completes launching, it enters the **Running** state and the splash screen disappears and all splash screen resources and objects are cleared.

## App activation

In contrast to being launched by the user, an app can be activated by the system. An app may be activated by a contract such as the share contract. Or it may be activated to handle a custom URI protocol or a file with an extension that your app is registered to handle. For a list of ways your app can be activated, see [**ActivationKind**](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind).

The [**Windows.UI.Xaml.Application**](/uwp/api/Windows.UI.Xaml.Application) class defines methods you can override to handle the various ways your app may be activated.
[**OnActivated**](/uwp/api/windows.ui.xaml.application.onactivated) can handle all possible activation types. However, it's more common to use specific methods to handle the most common activation types, and use **OnActivated** as the fallback method for the less common activation types. These are the additional methods for specific activations:

[**OnCachedFileUpdaterActivated**](/uwp/api/windows.ui.xaml.application.oncachedfileupdateractivated)  
[**OnFileActivated**](/uwp/api/windows.ui.xaml.application.onfileactivated)  
[**OnFileOpenPickerActivated**](/uwp/api/windows.ui.xaml.application.onfileopenpickeractivated)  [**OnFileSavePickerActivated**](/uwp/api/windows.ui.xaml.application.onfilesavepickeractivated)  
[**OnSearchActivated**](/uwp/api/windows.ui.xaml.application.onsearchactivated)  
[**OnShareTargetActivated**](/uwp/api/windows.ui.xaml.application.onsharetargetactivated)

The event data for these methods includes the same  [**PreviousExecutionState**](/uwp/api/windows.applicationmodel.activation.iactivatedeventargs.previousexecutionstate) property that we saw above, which tells you which state your app was in before it was activated. Interpret the state and what you should do it the same way as described above in the [App launch](#app-launch) section.

**Note** If you log on using the computer's Administrator account, you can't activate UWP apps.

## Running in the background

Starting with Windows 10, version 1607, apps can run background tasks within the same process as the app itself. Read more about it in [Background activity with the Single Process Model](https://blogs.windows.com/buildingapps/2016/06/07/background-activity-with-the-single-process-model/#tMmI7wUuYu5CEeRm.99). We won't go into in-process background processing in this article, but how this impacts the app lifecycle is that two new events have been added related to when your app is in the background. They are: [**EnteredBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.enteredbackground) and [**LeavingBackground**](/uwp/api/windows.applicationmodel.core.coreapplication.leavingbackground).

These events also reflect whether the user can see your app's UI.

Running in the background is the default state that an application is launched, activated, or resumed into. In this state your application UI is not visible yet.

## Running in the foreground

Running in the foreground means that your app's UI is visible.

The **LeavingBackground** event is fired just before your application UI is visible and before entering the running in foreground state. It also fires when the user switches back to your app.

Previously, the best location to load UI assets was in the **Activated** or **Resuming** event handlers. Now **LeavingBackground** is the best place to verify that your UI is ready.

It is important to check that visual assets are ready by this time because this is the last opportunity to do work before your application is visible to the user. All UI work in this event handler should complete quickly, as it impacts the launch and resume time that the user experiences. **LeavingBackground** is the time to ensure the first frame of UI is ready. Then, long-running storage or network calls should be handled asynchronously so that the event handler may return.

When the user switches away from your application, your app reenters the running in background state.

## Reentering the background state

The **EnteredBackground** event indicates that your app is no longer visible in the foreground. On the desktop **EnteredBackground** fires when your app is minimized; on phone, when switching to the home screen or another app.

### Reduce your app's memory usage

Since your app is no longer visible to the user, this is the best place to stop UI rendering work and animations. You can use **LeavingBackground** to start that work again.

If you are going to do work in the background, this is the place to prepare for it. It is best to check [MemoryManager.AppMemoryUsageLevel](/uwp/api/windows.system.memorymanager.appmemoryusagelevel) and, if needed, reduce the amount of memory being used by your app when it is running in the background so that your app doesn't risk being terminated by the system to free up resources.

See [Reduce memory usage when your app moves to the background state](reduce-memory-usage.md) for more details.

### Save your state

The suspending event handler is the best place to save your app state. However, if you are doing work in the background (for example, audio playback, using an extended execution session or in-proc background task), it is also a good practice to save your data asynchronously from your **EnteredBackground** event handler. This is because it is possible for your app to be terminated while it is at a lower priority in the background. And because the app will not have gone through the suspended state in that case, your data will be lost.

Saving your data in your **EnteredBackground** event handler, before background activity begins, ensures a good user experience when the user brings your app back to the foreground. You can use the application data APIs to save data and settings. For more info, see [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data).

After you save your data, if you are over your memory usage limit, then you can release your data from memory since you can reload it later. That will free memory that can be used by the assets needed for background activity.

Be aware that if your app has background activity in progress that it can move from the running in the background state to the running in the foreground state without ever reaching the suspended state.

> [!NOTE]
> When your app is being closed by the user, it is possible for the **OnSuspending** event to be fired before the **EnteredBackground** event. In some cases, the **EnteredBackground** event may not be fired before the app is terminated. It is important to save your data in the **OnSuspending** event handler.

### Asynchronous work and Deferrals

If you make an asynchronous call within your handler, control returns immediately from that asynchronous call. That means that execution can then return from your event handler and your app will move to the next state even though the asynchronous call hasn't completed yet. Use the [**GetDeferral**](/uwp/api/windows.applicationmodel.suspendingoperation.getdeferral) method on the [**EnteredBackgroundEventArgs**](/uwp/api/Windows.ApplicationModel) object that is passed to your event handler to delay suspension until after you call the [**Complete**](/uwp/api/windows.foundation.deferral.complete) method on the returned [**Windows.Foundation.Deferral**](/uwp/api/windows.foundation.deferral) object.

A deferral doesn't increase the amount of time you have to run your code before your app is terminated. It only delays termination until either the deferral's _Complete_ method is called, or the deadline passes-_whichever comes first_.

If you need more time to save your state, investigate ways to save your state in stages before your app enters the background state so that there is less to save in your **OnSuspending** event handler. Or you may request an [ExtendedExecutionSession](/archive/msdn-magazine/2015/windows-10-special-issue/app-lifecycle-keep-apps-alive-with-background-tasks-and-extended-execution) to get more time. There is no guarantee that the request will be granted, however, so it is best to find ways to minimize the amount of time you need to save your state.

## App suspend

When the user minimizes an app Windows waits a few seconds to see whether the user will switch back to it. If they do not switch back within this time window, and no extended execution, background task, or activity sponsored execution is active, Windows suspends the app. An app is also suspended when the lock screen appears as long as no extended execution session, etc. is active in that app.

When an app is suspended, it invokes the [**Application.Suspending**](/uwp/api/windows.ui.xaml.application.suspending) event. Visual Studio's UWP project templates provide a handler for this event called **OnSuspending** in **App.xaml.cs**. You should put the code to save your application state here.

You should release exclusive resources and file handles so that other apps can access them while your app is suspended. Examples of exclusive resources include cameras, I/O devices, external devices, and network resources. Explicitly releasing exclusive resources and file handles helps to ensure that other apps can access them while your app is suspended. When the app is resumed, it should reacquire  its exclusive resources and file handles.

### Be aware of the deadline

In order to ensure a fast and responsive device, there is a limit for the amount of time you have to run your code in your suspending event handler. It is different for each device, and you can find out what it is using a property of the [**SuspendingOperation**](/uwp/api/Windows.ApplicationModel.SuspendingOperation) object called the deadline.

As with the **EnteredBackground** event handler, if you make an asynchronous call from your handler, control returns immediately from that asynchronous call. That means that execution can then return from your event handler and your app will move to the suspend state even though the asynchronous call hasn't completed yet. Use the [**GetDeferral**](/uwp/api/windows.applicationmodel.suspendingoperation.getdeferral) method on the [**SuspendingOperation**](/uwp/api/Windows.ApplicationModel.SuspendingOperation) object (available via the event args) to delay entering the suspended state until after you call the [**Complete**](/uwp/api/windows.applicationmodel.suspendingdeferral.complete) method on the returned [**SuspendingDeferral**](/uwp/api/Windows.ApplicationModel.SuspendingDeferral) object.

If you need more time, you may request an [ExtendedExecutionSession](/archive/msdn-magazine/2015/windows-10-special-issue/app-lifecycle-keep-apps-alive-with-background-tasks-and-extended-execution). There is no guarantee that the request will be granted, however, so it is best to find ways to minimize the amount of time you need in your **Suspended** event handler.

### App terminate

The system attempts to keep your app and its data in memory while it's suspended. However, if the system does not have the resources to keep your app in memory, it will terminate your app. Apps don't receive a notification that they are being terminated, so the only opportunity you have to save your app's data is in your **OnSuspending** event handler.

When your app determines that it has been activated after being terminated, it should load the application data that it saved so that the app is in the same state it was in before it was terminated. When the user switches back to a suspended app that has been terminated, the app should restore its application data in its [**OnLaunched**](/uwp/api/windows.ui.xaml.application.onlaunched) method. The system doesn't notify an app when it is terminated, so your app must save its application data and release exclusive resources and file handles before it is suspended, and restore them when the app is activated after termination.

**A note about debugging using Visual Studio:** Visual Studio prevents Windows from suspending an app that is attached to the debugger. This is to allow the user to view the Visual Studio debug UI while the app is running. When you're debugging an app, you can send it a suspend event using Visual Studio. Make sure the **Debug Location** toolbar is being shown, then click the **Suspend** icon.

## App resume

A suspended app is resumed when the user switches to it or when it is the active app when the device comes out of a low power state.

When an app is resumed from the **Suspended** state, it enters the **Running in background** state and the system restores the app where it left off so that it appears to the user as if it has been running all along. No app data stored in memory is lost. Therefore, most apps don't need to restore state when they are resumed though they should reacquire any file or device handles that they released when they were suspended, and restore any state that was explicitly released when the app was suspended.

You app may be suspended for hours or days. If your app has content or network connections that may have gone stale, these should be refreshed when the app resumes. If an app registered an event handler for the [**Application.Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event, it is called when the app is resumed from the **Suspended** state. You can refresh your app content and data in this event handler.

If a suspended app is activated to participate in an app contract or extension, it receives the **Resuming** event first, then the **Activated** event.

If the suspended app was terminated, there is no **Resuming** event and instead **OnLaunched()** is called with an **ApplicationExecutionState** of **Terminated**. Because you saved your state when the app was suspended, you can restore that state during **OnLaunched()** so that your app appears to the user as it was when they switched away from it.

While an app is suspended, it does not receive any network events that it registered to receive. These network events are not queued--they are simply missed. Therefore, your app should test the network status when it is resumed.

**Note**  Because the [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event is not raised from the UI thread, a dispatcher must be used if the code in your resume handler communicates with your UI. See [Update the UI thread from a background thread](https://github.com/Microsoft/Windows-task-snippets/blob/master/tasks/UI-thread-access-from-background-thread.md) for a code example of how to do this.

For general guidelines, see [Guidelines for app suspend and resume](./index.md).

## App close

Generally, users don't need to close apps, they can let Windows manage them. However, users can choose to close an app using the close gesture or by pressing Alt+F4 or by using the task switcher on Windows Phone.

There is not an event to indicate that the user closed the app. When an app is closed by the user, it is first suspended to give you an opportunity to save its state. In Windows 8.1 and later, after an app has been closed by the user, the app is removed from the screen and switch list but not explicitly terminated.

**Closed-by-user behavior:**  If your app needs to do something different when it is closed by the user than when it is closed by Windows, you can use the activation event handler to determine whether the app was terminated by the user or by Windows. See the descriptions of **ClosedByUser** and **Terminated** states in the reference for the [**ApplicationExecutionState**](/uwp/api/Windows.ApplicationModel.Activation.ApplicationExecutionState) enumeration.

We recommend that apps not close themselves programmatically unless absolutely necessary. For example, if an app detects a memory leak, it can close itself to ensure the security of the user's personal data.

## App crash

The system crash experience is designed to get users back to what they were doing as quickly as possible. You shouldn't provide a warning dialog or other notification because that will delay the user.

If your app crashes, stops responding, or generates an exception, a problem report is sent to Microsoft per the user's feedback and diagnostics settings. See [Diagnostics, feedback, and privacy in Windows](https://support.microsoft.com/windows/diagnostics-feedback-and-privacy-in-windows-28808a2b-a31b-dd73-dcd3-4559a5199319) for more information. Microsoft provides a subset of the error data in the problem report to you so that you can use it to improve your app. You'll be able to see this data in your app's Quality page in your Dashboard.

When the user activates an app after it crashes, its activation event handler receives an [**ApplicationExecutionState**](/uwp/api/Windows.ApplicationModel.Activation.ApplicationExecutionState) value of **NotRunning**, and should display its initial UI and data. After a crash, don't routinely use the app data you would have used for **Resuming** with **Suspended** because that data could be corrupt; see [Guidelines for app suspend and resume](./index.md).

## App removal

When a user deletes your app, the app is removed, along with all its local data. Removing an app doesn't affect the user's data that was stored in common locations such as the Documents or Pictures libraries.

## App lifecycle and the Visual Studio project templates

The basic code that is relevant to the app lifecycle is provided in the Visual Studio project templates. The basic app handles launch activation, provides a place for you to restore your app data, and displays the primary UI even before you've added any of your own code. For more info, see [C#, VB, and C++ project templates for apps](/previous-versions/windows/apps/hh768232(v=win.10)).

## Key application lifecycle APIs

- [**Windows.ApplicationModel**](/uwp/api/Windows.ApplicationModel) namespace
- [**Windows.ApplicationModel.Activation**](/uwp/api/Windows.ApplicationModel.Activation) namespace
- [**Windows.ApplicationModel.Core**](/uwp/api/Windows.ApplicationModel.Core) namespace
- [**Windows.UI.Xaml.Application**](/uwp/api/Windows.UI.Xaml.Application) class (XAML)
- [**Windows.UI.Xaml.Window**](/uwp/api/Windows.UI.Xaml.Window) class (XAML)

## Related topics

- [**ApplicationExecutionState**](/uwp/api/Windows.ApplicationModel.Activation.ApplicationExecutionState)
- [Guidelines for app suspend and resume](./index.md)
- [Handle app prelaunch](handle-app-prelaunch.md)
- [Handle app activation](activate-an-app.md)
- [Handle app suspend](suspend-an-app.md)
- [Handle app resume](resume-an-app.md)
- [Background activity with the Single Process Model](https://blogs.windows.com/buildingapps/2016/06/07/background-activity-with-the-single-process-model/#tMmI7wUuYu5CEeRm.99)
- [Play media in the Background](../audio-video-camera/background-audio.md)
