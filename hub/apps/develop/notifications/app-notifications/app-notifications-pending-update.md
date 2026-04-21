---
description: Learn how to use pending update activation to create multi-step interactions in your app notifications.
title: App notification with pending update
label: App notification with pending update
template: detail.hbs
ms.date: 07/28/2025
ms.topic: how-to
keywords: windows 11, windows app sdk, winappsdk, notification, pending update, pendingupdate, multi-step interactivity, multi-step interactions
ms.localizationpriority: medium
---
# App notification with pending update

You can use **PendingUpdate** to create multi-step interactions in your app notifications. For example, you can create a series of notifications where subsequent notifications depend on responses from the previous notifications.

![Toast with pending update](images/toast-pendingupdate.gif)

For more information about app notifications, see [App notifications overview](index.md).

## Overview

To implement a notification that uses pending update as its after-activation behavior:

1. On your background activation buttons, specify an **afterActivationBehavior** of **pendingUpdate**.
2. Assign a **Tag** (and optionally **Group**) when sending your notification.
3. When the user clicks the button, your background task is activated and the notification stays on-screen in a pending update state.
4. In your background task, send a new notification with new content using the same **Tag** and **Group** to replace the pending notification.

## Set the pending update behavior

> [!NOTE]
> [**AppNotificationButton**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton) doesn't currently support `AfterActivationBehavior`. Use the XML payload directly with the [**AppNotification**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification) constructor to set `afterActivationBehavior="pendingUpdate"` on your buttons.

On your background activation buttons, set `afterActivationBehavior` to `pendingUpdate`. This only works for buttons with `activationType="background"`.

```csharp
using Microsoft.Windows.AppNotifications;

string xml = @"
<toast>
  <visual>
    <binding template='ToastGeneric'>
      <text>Would you like to order lunch today?</text>
    </binding>
  </visual>
  <actions>
    <action
      content='Yes'
      arguments='action=orderLunch'
      activationType='background'
      afterActivationBehavior='pendingUpdate'/>
    <action
      content='No'
      arguments='action=cancelLunch'
      activationType='background'/>
  </actions>
</toast>";

var notification = new AppNotification(xml);
notification.Tag = "lunch";

AppNotificationManager.Default.Show(notification);
```

## Replace the notification with new content

In response to the user clicking the button, your background task is triggered and you replace the notification by sending a new notification with the same **Tag** and **Group**. Use [**AppNotificationBuilder.MuteAudio**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.muteaudio) to mute the audio on replacements in response to a button click, since the user is already interacting with the notification.

```csharp
var notification = new AppNotificationBuilder()
    .AddText("Ordering your lunch...")
    .MuteAudio()
    .BuildNotification();

notification.Tag = "lunch";

AppNotificationManager.Default.Show(notification);
```

## See also

- [App notifications overview](index.md)