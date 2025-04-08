---
description: Learn how to use extended execution to keep your app running while it is minimized
title: Postpone app suspension with extended execution
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, extended execution, minimized, ExtendedExecutionSession, background task, application lifecycle, lock screen
ms.assetid: e6a6a433-5550-4a19-83be-bbc6168fe03a
ms.localizationpriority: medium
---
# Postpone app suspension with extended execution

This article shows you how to use extended execution to postpone when your app is suspended so that it can run while minimized or under the lock screen.

When the user minimizes or switches away from an app it is put into a suspended state.  Its memory is maintained, but its code does not run. This is true across all OS Editions with a visual user interface. For more details about when your app is suspended, see [Application Lifecycle](app-lifecycle.md). 

There are cases where an app may need to keep running, rather than be suspended, when the user navigates away from the app, or while it is minimized. For example, a step counting app needs to keep running and tracking steps even when the user navigates away to use other apps. 

If an app needs to keep running, either the OS can keep it running, or it can request to keep running. For example, when playing audio in the background, the OS can keep an app running longer if you follow these steps for [Background Media Playback](../audio-video-camera/background-audio.md). Otherwise, you must manually request more time. The amount of time you may get to perform background execution may be several minutes but you must be prepared to handle the session being revoked at any time. These application lifecycle time constraints are disabled while the app is running under a debugger. For this reason it is important to test Extended Execution and other tools for postponing app suspension while not running under a debugger or by using the Lifecycle Events available in Visual Studio. 
 
Create an [ExtendedExecutionSession](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionsession) to request more time to complete an operation in the background. The kind of **ExtendedExecutionSession** you create is determined by the  [ExtendedExecutionReason](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionreason) that you provide when you create it. There are three **ExtendedExecutionReason** enum values: **Unspecified, LocationTracking** and **SavingData**. Only one **ExtendedExecutionSession** can be requested at any time; attempting to create another session while an approved session request is currently active will cause exception 0x8007139F to be thrown from the **ExtendedExecutionSession** constructor stating that the group or resource is not in the correct state to perform the requested operation. Do not use [ExtendedExecutionForegroundSession](/uwp/api/windows.applicationmodel.extendedexecution.foreground.extendedexecutionforegroundsession) and [ExtendedExecutionForegroundReason](/uwp/api/windows.applicationmodel.extendedexecution.foreground.extendedexecutionforegroundreason); they require restricted capabilities and are not available for use in Store applications.

## Run while minimized

There are two cases where extended execution can be used:
- At any point during regular foreground execution, while the application is in the running state.
- After the application has received a suspending event (the OS is about to move the app to the suspended state) in the application’s suspending event handler.

The code for these two cases is the same, but the application behaves a little differently in each. In the first case, the application stays in the running state, even if an event that normally would trigger suspension occurs (for example, the user navigating away from the application). The application will never receive a suspending event while the execution extension is in effect. When the extension is disposed, the application becomes eligible for suspension again.

In the second case, if the application is transitioning to the suspended state, it will stay in a suspending state for the period of the extension. Once the extension expires, the application enters the suspended state without further notification.

Use **ExtendedExecutionReason.Unspecified** when you create an **ExtendedExecutionSession** to request additional time before your app moves into the background for scenarios such as media processing, project compilation, or keeping a network connection alive. On desktop devices running Windows 10 for desktop editions (Home, Pro, Enterprise, and Education), this is the approach to use if an app needs to avoid being suspended while it is minimized.

Request the extension when starting a long running operation in order to defer the **Suspending** state transition that otherwise occurs when the app moves into the background. On desktop devices, extended execution sessions created with **ExtendedExecutionReason.Unspecified** have a battery-aware time limit. If the device is connected to wall power, there is no limit to the length of the extended execution time period. If the device is on battery power, the extended execution time period can run up to ten minutes in the background.

A tablet or laptop user can get the same long running behavior--at the expense of battery life--when the **Allow the app to run background tasks** option is selected in **Battery usage by app** settings. (To find this option on a laptop, go to **Settings** > **System** > **Battery** > **Battery usage by App** (the link under the percent of battery power remaining) > select an app > turn off **Managed By Windows** > select **Allow app to run background tasks**.  

On all OS editions this kind of extended execution session stops when the device enters Connected Standby. On mobile devices running Windows 10 Mobile, this kind of extended execution session will run as long as the screen is on. When the screen turns off, the device immediately attempts to enter the low-power Connected-Standby mode. On desktop devices, the session will continue running if the lock screen appears. The device does not enter Connected Standby for a period of time after the screen turns off. On the Xbox OS Edition, the device enters Connect Standby after one hour unless the user changes the default.

## Track the user's location

Specify **ExtendedExecutionReason.LocationTracking** when you create an **ExtendedExecutionSession** if your app needs to regularly log the location from the [GeoLocator](/uwp/api/windows.devices.geolocation.geolocator). Apps for fitness tracking and navigation that need to regularly monitor the user's location and should use this reason.

A location tracking extended execution session can run as long as needed, including while the screen is locked on a mobile device. However, there can only be one such session running per device. A location tracking extended execution session can only be requested in the foreground, and the app must be in the **Running** state. This ensures that the user is aware that the app has initiated an extended location tracking session. It is still possible to use the GeoLocator while the app is in the background by using a background task, or an app service, without requesting a location tracking extended execution session.

## Save Critical Data Locally

Specify **ExtendedExecutionReason.SavingData** when you create an **ExtendedExecutionSession** to save user data in the case where not saving the data before the app is terminated will result in data loss and a negative user experience.

Don't use this kind of session to extend the lifetime of an app to upload or download data. If you need to upload data, request a [background transfer](../networking/background-transfers.md) or register a **MaintenanceTrigger** to handle the transfer when AC power is available. An **ExtendedExecutionReason.SavingData** extended execution session can be requested either when the app is in the foreground and in the **Running** state, or in the background and in the **Suspending** state.

The **Suspending** state is the last opportunity during the app lifecycle that an app can do work before the app is terminated. **ExtendedExecutionReason.SavingData** is the only type of **ExtendedExecutionSession** that can be requested in the **Suspending** state. Requesting an **ExtendedExecutionReason.SavingData** extended execution session while the app is in the **Suspending** state creates a potential issue that you should be aware of. If an extended execution session is requested while in the **Suspending** state, and the user requests the app be launched again, it may appear to take a long time to launch. This is because the extended execution session time period must complete before the old instance of the app can be closed and a new instance of the app can be launched. Launch performance time is sacrificed in order to guarantee that user state is not lost.

## Request, disposal, and revocation

There are three fundamental interactions with an extended execution session: the request, disposal, and revocation.  Making the request is modeled in the following code snippet.

### Request

```csharp
var newSession = new ExtendedExecutionSession();
newSession.Reason = ExtendedExecutionReason.Unspecified;
newSession.Revoked += SessionRevoked;
ExtendedExecutionResult result = await newSession.RequestExtensionAsync();

switch (result)
{
    case ExtendedExecutionResult.Allowed:
        DoLongRunningWork();
        break;

    default:
    case ExtendedExecutionResult.Denied:
        DoShortRunningWork();
        break;
}
```
[See code sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/ExtendedExecution/cs/Scenario1_UnspecifiedReason.xaml.cs#L81-L110)  

Calling **RequestExtensionAsync** checks with the operating system to see if the user has approved background activity for the app and whether the system has the available resources to enable background execution. Only one session will be approved for an app at any time, causing additional calls to **RequestExtensionAsync** to result in the session being denied.

You can check the [BackgroundExecutionManager](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager) beforehand to determine the [BackgroundAccessStatus](/uwp/api/windows.applicationmodel.background.backgroundaccessstatus?f=255&MSPPError=-2147217396), which is the user setting that indicates whether your app can run in the background or not. To learn more about these user settings see [Background Activity and Energy Awareness](https://blogs.windows.com/buildingapps/2016/08/01/battery-awareness-and-background-activity/#XWK8mEgWD7JHvC10.97).

The **ExtendedExecutionReason** indicates the operation your app is performing in the background. The **Description** string is a human-readable string that explains why your app needs to perform the operation. This string is not presented to the user, but may be made available in a future release of Windows. The **Revoked** event handler is required so that an extended execution session can halt gracefully if the user, or the system, decides that the app can no longer run in the background.

### Revoked

If an app has an active extended execution session and the system requires background activity to halt because a foreground application requires the resources, then the session is revoked. An extended execution session time period is never terminated without first firing the **Revoked** event handler.

When the **Revoked** event is fired for an **ExtendedExecutionReason.SavingData** extended execution session, the app has one second to complete the operation it was performing and finish **Suspending**.

Revocation can occur for many reasons: an execution time limit was reached, a background energy quota was reached, or memory needs to be reclaimed in order for the user to open a new app in the foreground.

Here is an example of a Revoked event handler:

```cs
private async void SessionRevoked(object sender, ExtendedExecutionRevokedEventArgs args)
{
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        switch (args.Reason)
        {
            case ExtendedExecutionRevokedReason.Resumed:
                rootPage.NotifyUser("Extended execution revoked due to returning to foreground.", NotifyType.StatusMessage);
                break;

            case ExtendedExecutionRevokedReason.SystemPolicy:
                rootPage.NotifyUser("Extended execution revoked due to system policy.", NotifyType.StatusMessage);
                break;
        }

        EndExtendedExecution();
    });
}
```
[See code sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/ExtendedExecution/cs/Scenario1_UnspecifiedReason.xaml.cs#L124-L141)

### Dispose

The final step is to dispose of the extended execution session. You want to dispose of the session, and any other memory intensive assets, because otherwise the energy used by the app while it is waiting for the session to close will be counted against the app's energy quota. To preserve as much of the energy quota for the app as possible, it is important to dispose of the session when you are done with your work for the session so that the app can move into the **Suspended** state more quickly.

Disposing of the session yourself, rather than waiting for the revocation event, reduces your app's energy quota usage. This means that your app will be permitted to run in the background longer in future sessions because you'll have more energy quota available to do so. You must maintain a reference to the **ExtendedExecutionSession** object until the end of the operation so that you can call its **Dispose** method.

A snippet that disposes an extended execution session follows:

```cs
void ClearExtendedExecution(ExtendedExecutionSession session)
{
    if (session != null)
    {
        session.Revoked -= SessionRevoked;
        session.Dispose();
        session = null;
    }
}
```
[See code sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/ExtendedExecution/cs/Scenario1_UnspecifiedReason.xaml.cs#L49-L63)

An app can only have one **ExtendedExecutionSession** active at a time. Many apps use asynchronous tasks in order to complete complex operations that require access to resources such as storage, network, or network-based services. If an operation requires multiple asynchronous tasks to complete, then the state of each of these tasks must be accounted for before disposing the **ExtendedExecutionSession** and allowing the app to be suspended. This requires reference counting the number of tasks that are still running, and not disposing of the session until that value reaches zero.

Here is some example code for managing multiple tasks during an extended execution session period. For more information on how to use this in your app please see the code sample linked below:

```cs
static class ExtendedExecutionHelper
{
    private static ExtendedExecutionSession session = null;
    private static int taskCount = 0;

    public static bool IsRunning
    {
        get
        {
            if (session != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static async Task<ExtendedExecutionResult> RequestSessionAsync(ExtendedExecutionReason reason, TypedEventHandler<object, ExtendedExecutionRevokedEventArgs> revoked, String description)
    {
        // The previous Extended Execution must be closed before a new one can be requested.       
        ClearSession();

        var newSession = new ExtendedExecutionSession();
        newSession.Reason = reason;
        newSession.Description = description;
        newSession.Revoked += SessionRevoked;

        // Add a revoked handler provided by the app in order to clean up an operation that had to be halted prematurely
        if(revoked != null)
        {
            newSession.Revoked += revoked;
        }

        ExtendedExecutionResult result = await newSession.RequestExtensionAsync();

        switch (result)
        {
            case ExtendedExecutionResult.Allowed:
                session = newSession;
                break;
            default:
            case ExtendedExecutionResult.Denied:
                newSession.Dispose();
                break;
        }
        return result;
    }

    public static void ClearSession()
    {
        if (session != null)
        {
            session.Dispose();
            session = null;
        }

        taskCount = 0;
    }

    public static Deferral GetExecutionDeferral()
    {
        if (session == null)
        {
            throw new InvalidOperationException("No extended execution session is active");
        }

        taskCount++;
        return new Deferral(OnTaskCompleted);
    }

    private static void OnTaskCompleted()
    {
        if (taskCount > 0)
        {
            taskCount--;
        }
        
        //If there are no more running tasks than end the extended lifetime by clearing the session
        if (taskCount == 0 && session != null)
        {
            ClearSession();
        }
    }

    private static void SessionRevoked(object sender, ExtendedExecutionRevokedEventArgs args)
    {
        //The session has been prematurely revoked due to system constraints, ensure the session is disposed
        if (session != null)
        {
            session.Dispose();
            session = null;
        }
        
        taskCount = 0;
    }
}
```
[See code sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/ExtendedExecution/cs/Scenario4_MultipleTasks.xaml.cs)

## Ensure that your app uses resources well

Tuning your app's memory and energy use is key to ensuring that the operating system will allow your app to continue to run when it is no longer the foreground app. Use the [Memory Management APIs](/uwp/api/windows.system.memorymanager) to see how much memory your app is using. The more memory your app uses, the harder it is for the OS to keep your app running when another app is in the foreground. The user is ultimately in control of all background activity that your app can perform and has visibility on the impact your app has on battery use.

Use [BackgroundExecutionManager.RequestAccessAsync](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager) to determine if the user has decided that your app’s background activity should be limited. Be aware of your battery usage and only run in the background when it is necessary to complete an action that the user wants.

## See also

[Extended Execution Sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ExtendedExecution)  
[Application Lifecycle](./app-lifecycle.md)  
[App Lifecycle - Keep Apps Alive with Background Tasks and Extended Execution](/archive/msdn-magazine/2015/windows-10-special-issue/app-lifecycle-keep-apps-alive-with-background-tasks-and-extended-execution)
[Background Memory Management](./reduce-memory-usage.md)  
[Background Transfers](../networking/background-transfers.md)  
[Battery Awareness and Background Activity](https://blogs.windows.com/buildingapps/2016/08/01/battery-awareness-and-background-activity/#I2bkQ6861TRpbRjr.97)  
[MemoryManager class](/uwp/api/windows.system.memorymanager)  
[Play Media in the Background](../audio-video-camera/background-audio.md)