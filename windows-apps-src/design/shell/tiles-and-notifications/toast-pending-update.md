---
description: Learn how to use toast with pending update activation to create multi-step interactions in your toast notifications.
title: Toast with pending update activation
label: Toast with pending update activation
template: detail.hbs
ms.date: 12/14/2017
ms.topic: article
keywords: windows 10, uwp, toast, pending update, pendingupdate, multi-step interactivity, multi-step interactions
ms.localizationpriority: medium
---
# Toast with pending update activation

You can use **PendingUpdate** to create multi-step interactions in your toast notifications. For example, as seen below, you can create a series of toasts where the subsequent toasts depend on responses from the previous toasts.

![Toast with pending update](images/toast-pendingupdate.gif)

> [!IMPORTANT]
> **Requires Desktop Fall Creators Update and 2.0.0 of Notifications library**: You must be running Desktop build 16299 or higher to see pending update work. You must use version 2.0.0 or higher of the [UWP Community Toolkit Notifications NuGet library](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/) to assign **PendingUpdate** on your buttons. **PendingUpdate** is only supported on Desktop and will be ignored on other devices.


## Prerequisites

This article assumes a working knowledge of...

- [Constructing toast content](adaptive-interactive-toasts.md)
- [Sending a toast and handling background activation](send-local-toast.md)


## Overview

To implement a toast that uses pending update as its after activation behavior...

1. On your toast background activation buttons, specify an **AfterActivationBehavior** of **PendingUpdate**

2. Assign a **Tag** (and optionally **Group**) when sending your toast

3. When the user clicks your button, your background task will be activated, and the toast will be kept on-screen in a pending update state

4. In your background task, send a new toast with your new content, using the same **Tag** and **Group**


## Assign PendingUpdate

On your background activation buttons, set the **AfterActivationBehavior** to **PendingUpdate**. Note that this only works for buttons that have an **ActivationType** of **Background**.

```csharp
new ToastButton("Yes", "action=orderLunch")
{
    ActivationType = ToastActivationType.Background,

    ActivationOptions = new ToastActivationOptions()
    {
        AfterActivationBehavior = ToastAfterActivationBehavior.PendingUpdate
    }
}
```

```xml
<action
    content='Yes'
    arguments='action=orderLunch'
    activationType='background'
    afterActivationBehavior='pendingUpdate' />
```


## Use a Tag on the notification

In order to later replace the notification, we have to assign the **Tag** (and optionally the **Group**) on the notification.

```csharp
// Create the notification
var notif = new ToastNotification(content.GetXml())
{
    Tag = "lunch"
};

// And show it
ToastNotificationManager.CreateToastNotifier().Show(notif);
```


## Replace the toast with new content

In response to the user clicking your button, your background task gets triggered and you need to replace the toast with new content. You replace the toast by simply sending a new toast with the same **Tag** and **Group**.

We strongly recommend **setting the audio to silent** on replacements in response to a button click, since the user is already interacting with your toast.

```csharp
// Generate new content
ToastContent content = new ToastContent()
{
    ...

    // We disable audio on all subsequent toasts since they appear right after the user
    // clicked something, so the user's attention is already captured
    Audio = new ToastAudio() { Silent = true }
};

// Create the new notification
var notif = new ToastNotification(content.GetXml())
{
    Tag = "lunch"
};

// And replace the old one with this one
ToastNotificationManager.CreateToastNotifier().Show(notif);
```


## Related topics

- [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-toast-pending-update)
- [Send a local toast and handle activation](send-local-toast.md)
- [Toast content documentation](adaptive-interactive-toasts.md)
- [Toast progress bar](toast-progress-bar.md)