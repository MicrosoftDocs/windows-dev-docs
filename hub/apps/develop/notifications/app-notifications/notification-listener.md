---
description: Learn how to use Notification Listener to access all of the user's notifications.
title: Notification listener
ms.assetid: E9AB7156-A29E-4ED7-B286-DA4A6E683638
template: detail.hbs
ms.date: 06/13/2017
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winappsdk, uwp, notification listener, usernotificationlistener, documentation, access notifications
ms.localizationpriority: medium
---
# Notification listener: Access all notifications

The notification listener allows your app to access and interact with all of the user's notifications, including notifications sent by other apps. This enables scenarios such as displaying notifications on a companion device, performing actions in response to notifications from other apps, or syncing notification state across devices.

For more information about app notifications, see [App notifications overview](index.md).


> [!NOTE]
> The code examples in this article use the `Windows.UI.Notifications.Management` namespace for notification listener functionality and the `Microsoft.Windows.AppNotifications` namespace for sending notifications. These two namespaces can be used together in the same app.


## Enable the listener by adding the User Notification capability 

To use the notification listener, you must add the User Notification Listener capability to your app manifest.

1. In Visual Studio, in the Solution Explorer, double click your `Package.appxmanifest` file to open the manifest designer.
2. Open the Capabilities tab.
3. Check the **User Notification Listener** capability.


## Request access to the listener

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

With the notification listener, you can get a list of the user's current notifications. Simply call the [GetNotificationsAsync](/uwp/api/windows.ui.notifications.management.usernotificationlistener.GetNotificationsAsync) method, and specify the type of notifications you want to get (currently, the only type of notifications supported are app notifications).

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

The content of the notification itself, such as the notification text, is contained in the [Notification](/uwp/api/windows.ui.notifications.usernotification.Notification) property. This property contains the visual portion of the notification. The [Visual](/uwp/api/windows.ui.notifications.notification.Visual) and [Visual.Bindings](/uwp/api/windows.ui.notifications.notificationvisual.Bindings) properties correspond to the content that was specified when the notification was sent.

We want to look for the toast binding (for error-proof code, you should check that the binding isn't null). From the binding, you can obtain the text elements. You can treat the text elements differently; for example, treat the first one as title text and subsequent elements as body text.

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

If your app allows the user to dismiss notifications, you can remove the actual notification so the user doesn't see it later. Provide the notification ID (obtained from the [UserNotification](/uwp/api/windows.ui.notifications.usernotification) object) of the notification you'd like to remove:

```csharp
// Remove the notification
listener.RemoveNotification(notifId);
```


## Clear all notifications

The [UserNotificationListener.ClearNotifications](/uwp/api/windows.ui.notifications.management.usernotificationlistener.ClearNotifications) method clears all the user's notifications. Use this method with caution. You should only clear all notifications if your app displays all notifications. If your app only displays certain notifications, the user may expect only those specific notifications to be removed when clicking a "Clear notifications" button. Calling [ClearNotifications](/uwp/api/windows.ui.notifications.management.usernotificationlistener.ClearNotifications) removes all notifications, including ones that your app wasn't displaying.

```csharp
// Clear all notifications. Use with caution.
listener.ClearNotifications();
```


## Background task for notification changes

You can register a background task that is triggered when a notification is added or dismissed, regardless of whether your app is currently running. Use [**UserNotificationChangedTrigger**](/uwp/api/windows.applicationmodel.background.usernotificationchangedtrigger) with the Windows App SDK [**BackgroundTaskBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) to register the task.

```csharp
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

var builder = new Microsoft.Windows.ApplicationModel.Background.BackgroundTaskBuilder();
builder.Name = "UserNotificationChanged";
builder.SetTrigger(new UserNotificationChangedTrigger(NotificationKinds.Toast));
builder.SetTaskEntryPointClsid(typeof(MyBackgroundTask).GUID);
builder.Register();
```

The background task is triggered whenever a notification is added or dismissed but doesn't provide details about which specific notification changed. When the task runs, your code should sync the current notification state by calling [**GetNotificationsAsync**](/uwp/api/windows.ui.notifications.management.usernotificationlistener.getnotificationsasync).

For complete steps on implementing a background task, including COM server registration and manifest configuration, see [Using background tasks in Windows apps](/windows/apps/windows-app-sdk/applifecycle/background-tasks).


## Determining which notifications were added and removed

In your `SyncNotifications` method, to determine which notifications have been added or removed, calculate the delta between your app's current notification state and the notifications in the platform.

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

You can also listen to notifications from an in-memory event handler using the [**NotificationChanged**](/uwp/api/windows.ui.notifications.management.usernotificationlistener.notificationchanged) event.

```csharp
// Subscribe to foreground event
listener.NotificationChanged += Listener_NotificationChanged;
 
private void Listener_NotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
{
    // Your code for handling the notification
}
```


## See also

- [App notifications overview](index.md)
