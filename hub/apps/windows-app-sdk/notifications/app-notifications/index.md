---
title: App notifications overview
description: Overview of Windows App SDK App Notifications
ms.topic: article
ms.date: 5/9/2022
keywords: toast, notification
ms.localizationpriority: medium
---


# App notifications overview

![A screen capture showing an app notification above the task bar. The notification is a reminder for an event. The app name, event name, event time, and event location are shown. A selection input displays the currently selected value, "Going". There are two buttons labeled "RSVP" and "Dismiss"](../../../design/images/shell-1x.png)

App notifications in the [Windows App SDK](../../index.md) are messages that your app can construct and deliver to your user while they are not currently inside your app. The notification content is displayed in a transient window in the bottom right corner of the screen and in the Notification Center (called Action Center in Windows 10). App notifications can be used to inform the user of application status or state changes, or to prompt the user to take an action. App notifications can either be sent locally or from a cloud service using [push notifications](../push-notifications/index.md).


## Limitations

The app notifications support in the Windows App SDK currently has these limitations:

- Notifications for an elevated (admin) app are currently not supported.

## Next steps

See [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md) to get started.
