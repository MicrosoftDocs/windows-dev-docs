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

You can schedule an app notification to appear at a later time, regardless of whether your app is running at that time. This is useful for scenarios like displaying reminders or other follow-up tasks for the user, where the time and content of the notification is known ahead-of-time.

Scheduled app notifications have a delivery window of 5 minutes. If the computer is turned off during the scheduled delivery time, and remains off for longer than 5 minutes, the notification will be "dropped" as no longer relevant to the user. If you need guaranteed delivery of notifications regardless of how long the computer was off, we recommend using a background task with a time trigger. For more information, see [Background tasks](/windows/apps/windows-app-sdk/applifecycle/background-tasks).

For more information about app notifications, see [App notifications overview](index.md).

> [!NOTE]
> The code examples in this article use the `Microsoft.Windows.AppNotifications` namespace to build notification content and the `Windows.UI.Notifications` namespace for scheduling. These two namespaces can be used together in the same app.


## Schedule the notification

To schedule a notification for a future time, use [**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) to define the notification content, then call [**AddToSchedule**](/uwp/api/windows.ui.notifications.toastnotifier.addtoschedule) with a [**ScheduledToastNotification**](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification). The following example schedules a notification to appear 10 seconds from now.

```csharp
using Microsoft.Windows.AppNotifications.Builder;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

var payload = new AppNotificationBuilder()
    .AddArgument("action", "viewItemsDueToday")
    .AddText("ASTR 170B1")
    .AddText("You have 3 items due today!")
    .BuildNotification()
    .Payload;

var doc = new XmlDocument();
doc.LoadXml(payload);

var scheduledNotification = new ScheduledToastNotification(doc, DateTimeOffset.Now.AddSeconds(10));
scheduledNotification.Tag = "18365";
scheduledNotification.Group = "ASTR 170B1";

ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledNotification);
```

The **Tag** and **Group** properties act as a composite primary key for the notification. Setting these values enables you to cancel or replace the scheduled notification later, as shown in the next section.


## Cancel scheduled notifications

To cancel a scheduled notification, call [**GetScheduledToastNotifications**](/uwp/api/windows.ui.notifications.toastnotifier.getscheduledtoastnotifications) to retrieve the list of pending notifications, then call [**RemoveFromSchedule**](/uwp/api/windows.ui.notifications.toastnotifier.removefromschedule) on the one matching the tag you specified earlier.

```csharp
var notifier = ToastNotificationManager.CreateToastNotifier();
var scheduled = notifier.GetScheduledToastNotifications();

foreach (var notification in scheduled)
{
    if (notification.Tag == "18365")
    {
        notifier.RemoveFromSchedule(notification);
    }
}
```

## See also

- [App notifications overview](index.md)