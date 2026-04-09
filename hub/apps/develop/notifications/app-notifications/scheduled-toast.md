---
description: Learn how to schedule a local app notification to appear at a later time.
title: Schedule an app notification
label: Schedule an app notification
template: detail.hbs
ms.date: 07/28/2025
ms.topic: how-to
keywords: windows 10, windows 11, windows app sdk, winappsdk, uwp, scheduled toast notification, scheduledtoastnotification, how to, quickstart, getting started, code sample, walkthrough
ms.localizationpriority: medium
no-loc: [toast, Toast, app, App]
---

# Schedule an app notification

Scheduled app notifications allow you to schedule a notification to appear at a later time, regardless of whether your app is running at that time. This is useful for scenarios like displaying reminders or other follow-up tasks for the user, where the time and content of the notification is known ahead-of-time.

Note that scheduled app notifications have a delivery window of 5 minutes. If the computer is turned off during the scheduled delivery time, and remains off for longer than 5 minutes, the notification will be "dropped" as no longer relevant to the user. If you need guaranteed delivery of notifications regardless of how long the computer was off, we recommend using a background task with a time trigger, as illustrated in [this code sample](https://github.com/WindowsNotifications/quickstart-snoozable-toasts-even-if-computer-is-off).

> [!NOTE]
> The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.

> [!NOTE]
> For Windows App SDK apps, you can use `AppNotificationManager` from the `Microsoft.Windows.AppNotifications` namespace.

> **Important APIs**: [AppNotificationManager.Show](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show), [ScheduledToastNotification Class](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)

## Prerequisites

To fully understand this topic, the following will be helpful...

* A working knowledge of app notification terms and concepts. For more information, see [Toast and action center overview](/archive/blogs/tiles_and_toasts/toast-notification-and-action-center-overview-for-windows-10).
* A familiarity with Windows 10 app notification content. For more information, see [App notification content](app-notifications-content.md) documentation.
* A Windows desktop app project (WinUI 3, WPF, or other)

## Step 1: Install NuGet package

If you're using a WinUI 3 or Windows App SDK project, the notification APIs are included in the `Microsoft.WindowsAppSDK` NuGet package, which is already referenced by default.


## Step 2: Add namespace declarations

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
```


## Step 3: Schedule the notification

We'll use a simple text-based notification reminding a student about the homework they have due today. Construct the notification and schedule it!

The Windows App SDK doesn't have a direct scheduling method. You can use `AppNotificationManager.Default.Show()` with an expiration time, or use the platform `ScheduledToastNotification` API via the `Windows.UI.Notifications` namespace for true scheduling.

```csharp
// Construct the notification
var notification = new AppNotificationBuilder()
    .AddArgument("action", "viewItemsDueToday")
    .AddText("ASTR 170B1")
    .AddText("You have 3 items due today!")
    .BuildNotification();

notification.Expiration = DateTimeOffset.Now.AddSeconds(5);
AppNotificationManager.Default.Show(notification);
```

To schedule a notification for a future time, use the platform `ScheduledToastNotification` API:

```csharp
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

var xml = new AppNotificationBuilder()
    .AddArgument("action", "viewItemsDueToday")
    .AddText("ASTR 170B1")
    .AddText("You have 3 items due today!")
    .BuildNotification()
    .Payload;

var doc = new XmlDocument();
doc.LoadXml(xml);

var scheduledNotification = new ScheduledToastNotification(doc, DateTimeOffset.Now.AddSeconds(5));
ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledNotification);
```


## Provide a primary key for your notification

If you want to programmatically cancel, remove, or replace the scheduled notification, you need to use the Tag property (and optionally the Group property) to provide a primary key for your notification. Then, you can use this primary key in the future to cancel, remove, or replace the notification.

To see more details on replacing/removing already delivered app notifications, please see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

Tag and Group combined act as a composite primary key. Group is the more generic identifier, where you can assign groups like "wallPosts", "messages", "friendRequests", etc. And then Tag should uniquely identify the notification itself from within the group. By using a generic group, you can then remove all notifications from that group by using the [RemoveGroup API](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_).

```csharp
var notification = new AppNotificationBuilder()
    .AddArgument("action", "viewItemsDueToday")
    .AddText("ASTR 170B1")
    .AddText("You have 3 items due today!")
    .BuildNotification();

notification.Tag = "18365";
notification.Group = "ASTR 170B1";

AppNotificationManager.Default.Show(notification);
```


## Cancel scheduled notifications

To cancel a scheduled notification, you first have to retrieve the list of all scheduled notifications.

Then, find your scheduled app notification matching the tag (and optionally group) you specified earlier, and remove it.

For Windows App SDK apps, you can remove displayed notifications by tag and group using `AppNotificationManager`:

```csharp
await AppNotificationManager.Default.RemoveByTagAndGroupAsync("18365", "ASTR 170B1");
```

To remove all notifications for a group:

```csharp
await AppNotificationManager.Default.RemoveByGroupAsync("ASTR 170B1");
```


## Activation handling

See the [send a local app notification](app-notifications-csharp-legacy.md) docs to learn more about handling activation. Activation of a scheduled app notification is handled the same as activation of a local app notification.

## Adding actions, inputs, and more

See the [send a local app notification](app-notifications-csharp-legacy.md) docs to learn more about advanced topics like actions and inputs. Actions and inputs work the same in local app notifications as they do in scheduled app notifications.

## Resources

* [App notification content documentation](app-notifications-content.md)
* [AppNotificationManager class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager)
* [AppNotificationBuilder class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder)
* [ScheduledToastNotification Class](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)


