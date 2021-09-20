---
description: Learn how to schedule a local toast notification to appear at a later time.
title: Schedule a toast notification
label: Schedule a toast notification
template: detail.hbs
ms.date: 04/09/2020
ms.topic: article
keywords: windows 10, uwp, scheduled toast notification, scheduledtoastnotification, how to, quickstart, getting started, code sample, walkthrough
ms.localizationpriority: medium
---

# Schedule a toast notification

Scheduled toast notifications allow you to schedule a notification to appear at a later time, regardless of whether your app is running at that time. This is useful for scenarios like displaying reminders or other follow-up tasks for the user, where the time and content of the notification is known ahead-of-time.

Note that scheduled toast notifications have a delivery window of 5 minutes. If the computer is turned off during the scheduled delivery time, and remains off for longer than 5 minutes, the notification will be "dropped" as no longer relevant to the user. If you need guaranteed delivery of notifications regardless of how long the computer was off, we recommend using a background task with a time trigger, as illustrated in [this code sample](https://github.com/WindowsNotifications/quickstart-snoozable-toasts-even-if-computer-is-off).

> [!IMPORTANT]
> Desktop applications (both MSIX/sparse packages and classic desktop) have slightly different steps for sending notifications and handling activation. Follow along with the instructions below, however replace `ToastNotificationManager` with the `DesktopNotificationManagerCompat` class from the [desktop apps](toast-desktop-apps.md) documentation.

> **Important APIs**: [ScheduledToastNotification Class](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)


## Prerequisites

To fully understand this topic, the following will be helpful...

* A working knowledge of toast notification terms and concepts. For more information, see [Toast and action center overview](/archive/blogs/tiles_and_toasts/toast-notification-and-action-center-overview-for-windows-10).
* A familiarity with Windows 10 toast notification content. For more information, see [toast content documentation](adaptive-interactive-toasts.md).
* A Windows 10 UWP app project


## Step 1: Install NuGet package

Install the [Microsoft.Toolkit.Uwp.Notifications NuGet package](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/). Our code sample will use this package. At the end of the article we'll provide the "plain" code snippets that don't use any NuGet packages. This package allows you to create toast notifications without using XML.


## Step 2: Add namespace declarations

```csharp
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
```


## Step 3: Schedule the notification

We'll use a simple text-based notification reminding a student about the homework they have due today. Construct the notification and schedule it!

```csharp
// Construct the content and schedule the toast!
new ToastContentBuilder()
    .AddArgument("action", "viewItemsDueToday")
    .AddText("ASTR 170B1")
    .AddText("You have 3 items due today!");
    .Schedule(DateTime.Now.AddSeconds(5));
```


## Provide a primary key for your toast

If you want to programmatically cancel, remove, or replace the scheduled notification, you need to use the Tag property (and optionally the Group property) to provide a primary key for your notification. Then, you can use this primary key in the future to cancel, remove, or replace the notification.

To see more details on replacing/removing already delivered toast notifications, please see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

Tag and Group combined act as a composite primary key. Group is the more generic identifier, where you can assign groups like "wallPosts", "messages", "friendRequests", etc. And then Tag should uniquely identify the notification itself from within the group. By using a generic group, you can then remove all notifications from that group by using the [RemoveGroup API](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_).

```csharp
// Construct the content and schedule the toast!
new ToastContentBuilder()
    .AddArgument("action", "viewItemsDueToday")
    .AddText("ASTR 170B1")
    .AddText("You have 3 items due today!");
    .Schedule(DateTime.Now.AddSeconds(5), toast =>
    {
        toast.Tag = "18365";
        toast.Group = "ASTR 170B1";
    });
```


## Cancel scheduled notifications

To cancel a scheduled notification, you first have to retrieve the list of all scheduled notifications.

Then, find your scheduled toast matching the tag (and optionally group) you specified earlier, and call RemoveFromSchedule().

```csharp
// Create the toast notifier
ToastNotifierCompat notifier = ToastNotificationManagerCompat.CreateToastNotifier();

// Get the list of scheduled toasts that haven't appeared yet
IReadOnlyList<ScheduledToastNotification> scheduledToasts = notifier.GetScheduledToastNotifications();

// Find our scheduled toast we want to cancel
var toRemove = scheduledToasts.FirstOrDefault(i => i.Tag == "18365" && i.Group == "ASTR 170B1");
if (toRemove != null)
{
    // And remove it from the schedule
    notifier.RemoveFromSchedule(toRemove);
}
```

> [!IMPORTANT]
> Win32 non-MSIX/sparse apps must use the ToastNotificationManagerCompat class as seen above. If you use ToastNotificationManager itself, you will receive an element not found exception. All types of apps can use the Compat class and it will work correctly.


## Activation handling

See the [send a local toast](send-local-toast.md) docs to learn more about handling activation. Activation of a scheduled toast notification is handled the same as activation of a local toast notification.


## Adding actions, inputs, and more

See the [send a local toast](send-local-toast.md) docs to learn more about advanced topics like actions and inputs. Actions and inputs work the same in local toasts as they do in scheduled toasts.


## Resources

* [Toast content documentation](adaptive-interactive-toasts.md)
* [ScheduledToastNotification Class](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)
