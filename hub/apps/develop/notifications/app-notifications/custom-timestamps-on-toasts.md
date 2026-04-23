---
title: Custom timestamps on app notifications
description: Learn how to override the default timestamp on an app notification with a custom timestamp that indicates when the message/information/content was generated.
label: Custom timestamps on an notifications
template: detail.hbs
ms.date: 12/15/2017
ms.topic: article
keywords: windows 10, uwp, toast, custom timestamp, timestamp, notification, Action Center
ms.localizationpriority: medium
---
# Custom timestamps on app notifications

By default, the timestamp on app notifications, which is visible within Notification Center, is set to the time that the notification was sent. You can optionally override the timestamp with your own custom date and time, so that the timestamp represents the time the message/information/content was actually created, rather than the time that the notification was sent. This also ensures that your notifications appear in the correct order within Notification Center, which is sorted by time. We recommend that most apps specify a custom timestamp.

This feature is available in Windows Build 15063 and later.

:::image type="content" source="images/toast-content-custom-timestamp.png" alt-text="App notification with custom timestamp":::

> [!NOTE]
> The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.


To use a custom timestamp, simply assign the **displayTimestamp** property on the **toast** element of your app notification XML payload. Starting with Windows App SDK 1.2, you can add a custom timestamp to an app notification with the [Microsoft.Windows.AppNotifications.Builder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder). For UWP apps, you can use version 1.4.0 or later of the [UWP Community Toolkit Notifications NuGet library](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/). YOu an also specify the timestamp using raw xml.


### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .SetTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc));
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .AddCustomTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc));
```

### [XML](#tab/xml)

```xml
<toast displayTimestamp='2017-04-15T12:45:00-07:00'>
    <visual>
        <binding template='ToastGeneric'>
            <text>Matt sent you a friend request</text>
            <text>Hey, wanna dress up as wizards and ride around on hoverboards?</text>
        </binding>
    </visual>
</toast>
```

---

If you are using XML, the date must be formatted in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601).

> [!NOTE]
> You can only use at most 3 decimal places on the seconds (although realistically there's no value in providing anything that granular). If you provide more, the payload will be invalid and you will receive the "New notification" notification.


## Usage guidance

In general, we recommend that most apps specify a custom timestamp. This ensures that the notification's timestamp accurately represents when the message/information/content was generated, regardless of network delays, airplane mode, or the fixed interval of periodic background tasks.

For example, a news app might run a background task every 15 minutes that checks for new articles and displays notifications. Before custom timestamps, the timestamp corresponded to when the app notification was generated (therefore always in 15 minute intervals). However, now the app can set the timestamp to the time the article was actually published. Similarly, email apps and social network apps can benefit from this feature if a similar pattern of periodic pulling is used for their notifications.

Additionally, providing a custom timestamp ensures that the timestamp is correct even if the user was disconnected from the internet. For example, when the user turns their computer on and your background task runs, you can finally ensure that the timestamp on your notifications represents the time that the messages were sent, rather than the time the user turned on their computer.


## Default timestamp

If you don't provide a custom timestamp, we use the time that your notification was sent.

If you sent a push notification through WNS, we use the time when the notification was received by WNS server (so any latency on delivering the notification to the device won't impact the timestamp).

If you sent a local notification, we use the time when the notification platform received the notification (which should be immediately).


## Related topics

- [Send a local toast](send-local-toast.md)
- [App notification content documentation](adaptive-interactive-toasts.md)