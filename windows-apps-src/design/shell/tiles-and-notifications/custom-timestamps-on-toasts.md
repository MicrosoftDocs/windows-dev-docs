---
title: Custom timestamps on toasts
description: Learn how to override the default timestamp on a toast notification with a custom timestamp that indicates when the message/information/content was generated.
label: Custom timestamps on toasts
template: detail.hbs
ms.date: 12/15/2017
ms.topic: article
keywords: windows 10, uwp, toast, custom timestamp, timestamp, notification, Action Center
ms.localizationpriority: medium
---
# Custom timestamps on toasts

By default, the timestamp on toast notifications (visible within Action Center) is set to the time that the notification was sent.

<img alt="Toast with custom timestamp" src="images/toast-customtimestamp.jpg" width="396"/>

You can optionally override the timestamp with your own custom date and time, so that the timestamp represents the time the message/information/content was actually created, rather than the time that the notification was sent. This also ensures that your notifications appear in the correct order within Action Center (which are sorted by time). We recommend that most apps specify a custom timestamp.

> [!IMPORTANT]
> **Requires Creators Update and 1.4.0 of Notifications library**: You must be running build 15063 or higher to see custom timestamps. You must use version 1.4.0 or higher of the [UWP Community Toolkit Notifications NuGet library](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/) to assign the timestamp on your toast's content.

To use a custom timestamp, simply assign the **DisplayTimestamp** property on your **ToastContent**.

```csharp
ToastContent toastContent = new ToastContent()
{
    DisplayTimestamp = new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc),
    ...
};
```

```xml
<toast displayTimestamp="2017-04-15T19:45:00Z">
  ...
</toast>
```

If you are using XML, the date must be formatted in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601).

> [!NOTE]
> You can only use at most 3 decimal places on the seconds (although realistically there's no value in providing anything that granular). If you provide more, the payload will be invalid and you will receive the "New notification" notification.


## Usage guidance

In general, we recommend that most apps specify a custom timestamp. This ensures that the notification's timestamp accurately represents when the message/information/content was generated, regardless of network delays, airplane mode, or the fixed interval of periodic background tasks.

For example, a news app might run a background task every 15 minutes that checks for new articles and displays notifications. Before custom timestamps, the timestamp corresponded to when the toast notification was generated (therefore always in 15 minute intervals). However, now the app can set the timestamp to the time the article was actually published. Similarly, email apps and social network apps can benefit from this feature if a similar pattern of periodic pulling is used for their notifications.

Additionally, providing a custom timestamp ensures that the timestamp is correct even if the user was disconnected from the internet. For example, when the user turns their computer on and your background task runs, you can finally ensure that the timestamp on your notifications represents the time that the messages were sent, rather than the time the user turned on their computer.


## Default timestamp

If you don't provide a custom timestamp, we use the time that your notification was sent.

If you sent a push notification through WNS, we use the time when the notification was received by WNS server (so any latency on delivering the notification to the device won't impact the timestamp).

If you sent a local notification, we use the time when the notification platform received the notification (which should be immediately).


## Related topics

- [Send a local toast](send-local-toast.md)
- [Toast content documentation](adaptive-interactive-toasts.md)