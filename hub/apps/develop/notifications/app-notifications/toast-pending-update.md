---
description: Learn how to use notification with pending update activation to create multi-step interactions in your app notifications.
title: Toast with pending update activation
label: Toast with pending update activation
template: detail.hbs
ms.date: 12/14/2017
ms.topic: how-to
keywords: windows 10, windows 11, uwp, windows app sdk, winappsdk, toast, pending update, pendingupdate, multi-step interactivity, multi-step interactions
ms.localizationpriority: medium
---
# Toast with pending update activation

You can use **PendingUpdate** to create multi-step interactions in your app notifications. For example, as seen below, you can create a series of notifications where the subsequent notifications depend on responses from the previous notifications.

![Toast with pending update](images/toast-pendingupdate.gif)

> [!IMPORTANT]
> **Requires Desktop Fall Creators Update**: You must be running Desktop build 16299 or later to see pending update work. **PendingUpdate** is only supported on Desktop and will be ignored on other devices. For Windows App SDK apps, use the `AppNotificationBuilder` from the `Microsoft.Windows.AppNotifications.Builder` namespace to construct notification content.


## Prerequisites

This article assumes a working knowledge of...

- [Constructing toast content](adaptive-interactive-toasts.md)
- [Sending an app notification and handling background activation](app-notification-csharp-legacy.md)


## Overview

To implement a notification that uses pending update as its after activation behavior...

1. On your notification background activation buttons, specify an **AfterActivationBehavior** of **PendingUpdate**

2. Assign a **Tag** (and optionally **Group**) when sending your notification

3. When the user clicks your button, your background task will be activated, and the notification will be kept on-screen in a pending update state

4. In your background task, send a new notification with your new content, using the same **Tag** and **Group**


## Assign PendingUpdate

On your background activation buttons, set the **AfterActivationBehavior** to **PendingUpdate**. Note that this only works for buttons that have an **ActivationType** of **Background**.

#### [Windows App SDK](#tab/appsdk)

> [!NOTE]
> The Windows App SDK `AppNotificationButton` does not have a direct `AfterActivationBehavior` equivalent. For pending update behavior, use the raw XML approach below to set `afterActivationBehavior="pendingUpdate"` on your buttons, or handle the update logic in your background activation handler.

```csharp
new AppNotificationBuilder()
    .AddText("Would you like to order lunch today?")
    .AddButton(new AppNotificationButton("Yes")
        .AddArgument("action", "orderLunch"));
```

#### [XML](#tab/xml)

```xml
<toast>
  
  <visual>
    <binding template="ToastGeneric">
      <text>Would you like to order lunch today?</text>
    </binding>
  </visual>

  <actions>
    <action
      content="Yes"
      arguments="action=orderLunch"
      activationType="background"
      afterActivationBehavior="pendingUpdate"/>
  </actions>
  
</toast>
```

---


## Use a Tag on the notification

In order to later replace the notification, we have to assign the **Tag** (and optionally the **Group**) on the notification.

```csharp
// Create and tag the notification
var notification = builder.BuildNotification();
notification.Tag = "lunch";

// And show it
AppNotificationManager.Default.Show(notification);
```



## Replace the notification with new content

In response to the user clicking your button, your background task gets triggered and you need to replace the notification with new content. You replace the notification by simply sending a new notification with the same **Tag** and **Group**.

We strongly recommend **setting the audio to silent** on replacements in response to a button click, since the user is already interacting with your notification.

```csharp
// Generate new content with silent audio
var notification = new AppNotificationBuilder()
    .AddText("Ordering your lunch...")
    .SetAudioEvent(AppNotificationSoundEvent.Default, AppNotificationAudioLooping.None)
    .MuteAudio()
    .BuildNotification();

// Use the same Tag to replace the existing notification
notification.Tag = "lunch";

// And replace the old one with this one
AppNotificationManager.Default.Show(notification);
```



## Related topics

- [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-toast-pending-update)
- [Send a local app notification and handle activation](app-notification-csharp-legacy.md)
- [Toast content documentation](adaptive-interactive-toasts.md)
- [Toast progress bar](toast-progress-bar.md)

