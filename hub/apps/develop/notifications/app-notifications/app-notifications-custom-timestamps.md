---
title: Custom timestamps on app notifications
description: Learn how to override the default timestamp on an app notification with a custom timestamp that indicates when the message or content was generated.
label: Custom timestamps on app notifications
template: detail.hbs
ms.date: 07/28/2025
ms.topic: article
keywords: windows 11, windows app sdk, winappsdk, notification, custom timestamp, timestamp, Notification Center
ms.localizationpriority: medium
---
# Custom timestamps on app notifications

By default, the timestamp on app notifications in Notification Center is set to the time that the notification was sent. You can optionally override the timestamp with your own custom date and time so that the timestamp represents the time the message or content was actually created, rather than the time the notification was sent. This also ensures that your notifications appear in the correct order within Notification Center, which is sorted by time.

:::image type="content" source="images/toast-content-custom-timestamp.png" alt-text="App notification with custom timestamp":::

For more information about app notifications, see [App notifications overview](index.md).

## Set a custom timestamp

Use [**AppNotificationBuilder.SetTimeStamp**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.settimestamp) to override the default timestamp on your notification.

```csharp
using Microsoft.Windows.AppNotifications.Builder;

var notification = new AppNotificationBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .SetTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc))
    .BuildNotification();

AppNotificationManager.Default.Show(notification);
```

## Usage guidance

We recommend that most apps specify a custom timestamp. This ensures that the notification's timestamp accurately represents when the message or content was generated, regardless of network delays, airplane mode, or the fixed interval of periodic background tasks.

For example, a news app might run a background task every 15 minutes that checks for new articles and displays notifications. Without a custom timestamp, the timestamp corresponds to when the notification was generated (always in 15-minute intervals). With a custom timestamp, the app can set it to the time the article was actually published. Similarly, email apps and social network apps can benefit from this feature if a similar pattern of periodic pulling is used for their notifications.

Providing a custom timestamp also ensures that the timestamp is correct even if the user was disconnected from the internet. For example, when the user turns their computer on and your background task runs, you can ensure that the timestamp represents the time the messages were sent, rather than the time the user turned on their computer.

## Default timestamp

If you don't provide a custom timestamp, the platform uses the time that your notification was sent.

- For push notifications sent through WNS, the timestamp is set to the time when the notification was received by the WNS server.
- For local notifications, the timestamp is set to the time when the notification platform received the notification.

## See also

- [App notifications overview](index.md)