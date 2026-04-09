---
title: App notifications overview
description: Overview of Windows App SDK App Notifications
ms.topic: concept-article
ms.date: 03/01/2026
keywords: toast, notification
ms.localizationpriority: medium
no-loc: [toast, Toast, app, App]
---
# App notifications overview

![A screen capture showing an app notification above the task bar. The notification is a reminder for an event. The app name, event name, event time, and event location are shown. A selection input displays the currently selected value, "Going". There are two buttons labeled "RSVP" and "Dismiss"](images/shell-1x.png)

App notifications are UI popups that appear outside of your app's window, delivering timely information or actions to the user. Notifications can be purely informational, can launch your app when clicked, or can trigger a background action without bringing your app to the foreground. App notifications can either be sent locally or from a cloud service using [push notifications](../push-notifications/index.md).

This section provides design and implementation guidance for app notifications in the [Windows App SDK](../../../windows-app-sdk/index.md) and WinRT.

## App notifications by framework

- [WinUI apps (quickstart)](app-notifications-quickstart.md)
- [WPF apps](app-notifications-wpf.md)
- [WinForms apps](app-notifications-winforms.md)
- [Console apps](app-notifications-console.md)
- [UWP apps](app-notifications-uwp.md)

## UX and content guidance

- [App notification UX guidance](toast-ux-guidance.md)
- [App notification content](app-notifications-content.md)
- [App notification content schema](notification-schema.md)
- [Schedule an app notification](scheduled-notification.md)

## Additional app notifications features

- [Custom audio on app notifications](custom-audio-on-notifications.md)
- [App notification progress bar and data binding](notification-progress-bar.md)
- [App notification with pending update](notification-pending-update.md)
- [Custom timestamps on app notifications](custom-timestamps-on-notifications.md)
- [App notification collections](notification-collections.md)
- [App notification headers](notification-headers.md)
- [Notification listener: Access all notifications](notification-listener.md)

## Limitations

The app notifications support in the Windows App SDK currently has these limitations:

- Notifications for an elevated (admin) app are currently not supported.

## Next steps

See [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md) to get started.



