---
Description: Learn how to use Notification Listener to access all of the user's notifications.
title: Notification listener
ms.assetid: E9AB7156-A29E-4ED7-B286-DA4A6E683638
label: Chaseable tiles
template: detail.hbs
ms.date: 06/13/2017
ms.topic: article
keywords: windows 10, uwp, notification listener, usernotificationlistener, documentation, access notifications
ms.localizationpriority: medium
---
# Notification listener: Access all notifications

The notification listener provides access to a user's notifications. Smartwatches and other wearables can use the notification listener to send the phone's notifications to the wearable device. Home automation apps can use notification listener to perform specific actions when notifications are received, such as making the lights blink when you receive a call. 

> [!IMPORTANT]
> **Requires Anniversary Update**: You must target SDK 14393 and be running build 14393 or higher to use Notification Listener.


> **Important APIs**: [UserNotificationListener class](/uwp/api/Windows.UI.Notifications.Management.UserNotificationListener), [UserNotificationChangedTrigger class](/uwp/api/Windows.ApplicationModel.Background.UserNotificationChangedTrigger)


## Enable the listener by adding the User Notification capability 

To use the notification listener, you must add the User Notification Listener capability to your app manifest.

1. In Visual Studio, in the Solution Explorer, double click your `Package.appxmanifest` file to open the manifest designer.
2. Open the Capabilities tab.
3. Check the **User Notification Listener** capability.


## Check whether the listener is supported

If your app supports older versions of Windows 10, you need to use the [ApiInformation class](/uwp/api/Windows.Foundation.Metadata.ApiInformation) to check whether the listener is supported.  If the listener isn't supported, avoid executing any calls to the listener APIs.

```csharp
if (ApiInformation.IsTypePresent("Windows.UI.Notifications.Management.UserNotificationListener"))
{
    // Listener supported!
}
 
else
{
    // Older version of Windows, no Listener
}
```


## Requesting access to the listener

Since the listener allows access to the user's notifications, users must give your app permission to access their notifications. During your app's first-run experience, you should request access to use the notification listener. If you want, you can show some preliminary UI that explains why your app needs access to the user's notifications before you call [RequestAccessAsync](/uwp/api/windows.ui.notifications.management.usernotificationlistener.RequestAccessAsync), so that the user understands why they should allow access.

```csharp
// Get the listener
UserNotificationListener listener = UserNotificationListener.Current;
 
// And request access to the user's notifications (must be called from UI thread)
UserNotificationListenerAccessStatus accessStatus = await listener.RequestAccessAsync();
 
switch (accessStatus)
{
    // This means the user has granted access.
    case UserNotificationListenerAccessStatus.Allowed:
 
        // Yay! Proceed as normal
        break;
 
    // This means the user has denied access.
    // Any further calls to RequestAccessAsync will instantly
    // return Denied. The user must go to the Windows settings
    // and manually allow access.
    case UserNotificationListenerAccessStatus.Denied:
 
        // Show UI explaining that listener features will not
        // work until user allows access.
        break;
 
    // This means the user closed the prompt without
    // selecting either allow or deny. Further calls to
    // RequestAccessAsync will show the dialog again.
    case UserNotificationListenerAccessStatus.Unspecified:
 
        // Show UI that allows the user to bring up the prompt again
        break;
}
```

The user can revoke access at any time via Windows Settings. Therefore, your app should always check the access status via the [GetAccessStatus](/uwp/api/windows.ui.notifications.management.usernotificationlistener.GetAccessStatus) method before executing code that uses the notification listener. If the user revokes access, the APIs will silently fail rather than throwing an exception (for example, the API to get all notifications will simply return an empty list).


## Access the user's notifications

With the notification listener, you can get a list of the user's current notifications. Simply call the [GetNotificationsAsync](/uwp/api/windows.ui.notifications.management.usernotificationlistener.GetNotificationsAsync) method, and specify the type of notifications you want to get (currently, the only type of notifications supported are toast notifications).

```csharp
// Get the toast notifications
IReadOnlyList<UserNotification> notifs = await listener.GetNotificationsAsync(NotificationKinds.Toast);
```


## Displaying the notifications

Each notification is represented as a [UserNotification](/uwp/api/windows.ui.notifications.usernotification), which provides information about the app that the notification is from, the time the notification was created, the notification's ID, and the notification itself.

```csharp
public sealed class UserNotification
{
    public AppInfo AppInfo { get; }
    public DateTimeOffset CreationTime { get; }
    public uint Id { get; }
    public Notification Notification { get; }
}
```

The [AppInfo](/uwp/api/windows.ui.notifications.usernotification.AppInfo) property provides the info you need to display the notification.

> [!NOTE]
> We recommend surrounding all your code for processing a single notification in a try/catch, in case an unexpected exception occurs when you are capturing a single notification. You shouldn't completely fail to display other notifications just because of an issue with one specific notification.

```csharp
// Select the first notification
UserNotification notif = notifs[0];
 
// Get the app's display name
string appDisplayName = notif.AppInfo.DisplayInfo.DisplayName;
 
// Get the app's logo
BitmapImage appLogo = new BitmapImage();
RandomAccessStreamReference appLogoStream = notif.AppInfo.DisplayInfo.GetLogo(new Size(16, 16));
await appLogo.SetSourceAsync(await appLogoStream.OpenReadAsync());
```

The content of the notification itself, such as the notification text, is contained in the [Notification](/uwp/api/windows.ui.notifications.usernotification.Notification) property. This property contains the visual portion of the notification. (If you are familiar with sending notifications on Windows, you will notice that the [Visual](/uwp/api/windows.ui.notifications.notification.Visual) and [Visual.Bindings](/uwp/api/windows.ui.notifications.notificationvisual.Bindings) properties in the [Notification](/uwp/api/windows.ui.notifications.notification) object correspond to what developers send when popping a notification.)

We want to look for the toast binding (for error-proof code, you should check that the binding isn't null). From the binding, you can obtain the text elements. You can choose to display as many text elements as you would like. (Ideally, you should display them all.) You can choose to treat the text elements differently; for example, treat the first one as title text, and subsequent elements as body text.

```csharp
// Get the toast binding, if present
NotificationBinding toastBinding = notif.Notification.Visual.GetBinding(KnownNotificationBindings.ToastGeneric);
 
if (toastBinding != null)
{
    // And then get the text elements from the toast binding
    IReadOnlyList<AdaptiveNotificationText> textElements = toastBinding.GetTextElements();
 
    // Treat the first text element as the title text
    string titleText = textElements.FirstOrDefault()?.Text;
 
    // We'll treat all subsequent text elements as body text,
    // joining them together via newlines.
    string bodyText = string.Join("\n", textElements.Skip(1).Select(t => t.Text));
}
```


## Remove a specific notification

If your wearable or service allows the user to dismiss notifications, you can remove the actual notification so the user doesn't see it later on their phone or PC. Simply provide the notification ID (obtained from the [UserNotification](/uwp/api/windows.ui.notifications.usernotification) object) of the notification you'd like to remove: 

```csharp
// Remove the notification
listener.RemoveNotification(notifId);
```


## Clear all notifications

The [UserNotificationListener.ClearNotifications](/uwp/api/windows.ui.notifications.management.usernotificationlistener.ClearNotifications) method clears all the user's notifications. Use this method with caution. You should only clear all notifications if your wearable or service displays ALL notifications. If your wearable or service only displays certain notifications, when the user clicks your "Clear notifications" button, the user is only expecting those specific notifications to be removed; however, calling the [ClearNotifications](/uwp/api/windows.ui.notifications.management.usernotificationlistener.ClearNotifications) method would actually cause all the notifications, including ones that your wearable or service wasn't displaying, to be removed.

```csharp
// Clear all notifications. Use with caution.
listener.ClearNotifications();
```


## Background task for notification added/dismissed

A common way to enable an app to listen to notifications is to set up a background task, so that you can know when a notification was added or dismissed regardless of whether your app is currently running.

Thanks to the [single process model](../../../launch-resume/create-and-register-an-inproc-background-task.md) added in the Anniversary Update, adding background tasks is fairly simple. In your main app's code, after you have obtained the user's access to Notification Listener and obtained access to run background tasks, simply register a new background task, and set the [UserNotificationChangedTrigger](/uwp/api/windows.applicationmodel.background.usernotificationchangedtrigger) using the [Toast notification kind](/uwp/api/windows.ui.notifications.notificationkinds).

```csharp
// TODO: Request/check Listener access via UserNotificationListener.Current.RequestAccessAsync
 
// TODO: Request/check background task access via BackgroundExecutionManager.RequestAccessAsync
 
// If background task isn't registered yet
if (!BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals("UserNotificationChanged")))
{
    // Specify the background task
    var builder = new BackgroundTaskBuilder()
    {
        Name = "UserNotificationChanged"
    };
 
    // Set the trigger for Listener, listening to Toast Notifications
    builder.SetTrigger(new UserNotificationChangedTrigger(NotificationKinds.Toast));
 
    // Register the task
    builder.Register();
}
```

Then, in your App.xaml.cs, override the [OnBackgroundActivated](/uwp/api/windows.ui.xaml.application.OnBackgroundActivated) method if you haven't yet, and use a switch statement on the task name to determine which of your many background task triggers was invoked.

```csharp
protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
{
    var deferral = args.TaskInstance.GetDeferral();
 
    switch (args.TaskInstance.Task.Name)
    {
        case "UserNotificationChanged":
            // Call your own method to process the new/removed notifications
            // The next section of documentation discusses this code
            await MyWearableHelpers.SyncNotifications();
            break;
    }
 
    deferral.Complete();
}
```

The background task is simply a "shoulder tap": it doesn't provide any information about which specific notification was added or removed. When your background task is triggered, you should sync the notifications on your wearable so that they reflect the notifications in the platform. This ensures that if your background task fails, notifications on your wearable can still be recovered the next time your background task executes.

`SyncNotifications` is a method you implement; the next section shows how. 


## Determining which notifications were added and removed

In your `SyncNotifications` method, to determine which notifications have been added or removed (syncing notifications with your wearable), you have to calculate the delta between your current notification collection, and the notifications in the platform.

```csharp
// Get all the current notifications from the platform
IReadOnlyList<UserNotification> userNotifications = await listener.GetNotificationsAsync(NotificationKinds.Toast);
 
// Obtain the notifications that our wearable currently has displayed
IList<uint> wearableNotificationIds = GetNotificationsOnWearable();
 
// Copy the currently displayed into a list of notification ID's to be removed
var toBeRemoved = new List<uint>(wearableNotificationIds);
 
// For each notification in the platform
foreach (UserNotification userNotification in userNotifications)
{
    // If we've already displayed this notification
    if (wearableNotificationIds.Contains(userNotification.Id))
    {
        // We want to KEEP it displayed, so take it out of the list
        // of notifications to remove.
        toBeRemoved.Remove(userNotification.Id);
    }
 
    // Otherwise it's a new notification
    else
    {
        // Display it on the Wearable
        SendNotificationToWearable(userNotification);
    }
}
 
// Now our toBeRemoved list only contains notification ID's that no longer exist in the platform.
// So we will remove all those notifications from the wearable.
foreach (uint id in toBeRemoved)
{
    RemoveNotificationFromWearable(id);
}
```


## Foreground event for notification added/dismissed

> [!IMPORTANT] 
> Known issue: In builds before Build 17763 / October 2018 Update / Version 1809, The foreground event will cause a CPU loop and/or didn't work. If you need support on those earlier builds, use the background task instead.

You can also listen to notifications from an in-memory event handler...

```csharp
// Subscribe to foreground event
listener.NotificationChanged += Listener_NotificationChanged;
 
private void Listener_NotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
{
    // Your code for handling the notification
}
```


## How to fix delays in the background task

When testing your app, you might notice that the background task is sometimes delayed and doesn't trigger for several minutes. To fix the delay, prompt the user to go to the system settings -> System -> Battery -> Battery usage by app, find your app in the list, select it, and set it to be "Always allowed in background." After this, the background task should always be triggered within around a second of the notification being received.