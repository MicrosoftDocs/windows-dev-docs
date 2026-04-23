---
title: App notifications overview
description: Overview of Windows App SDK App Notifications
ms.topic: concept-article
ms.date: 07/28/2025
keywords: toast, notification
ms.localizationpriority: medium
no-loc: [toast, Toast, app, App]
---
# App notifications overview

![A screen capture showing an app notification above the task bar. The notification is a reminder for an event. The app name, event name, event time, and event location are shown. A selection input displays the currently selected value, "Going". There are two buttons labeled "RSVP" and "Dismiss"](images/shell-1x.png)

App notifications (also called _toast_ notifications) are messages that your app can construct and deliver to your user while they are not currently inside your app. The notification content is displayed in a transient window in the bottom right corner of the screen and in the Notification Center (called Action Center in Windows 10). App notifications can be used to inform the user of application status or state changes, or to prompt the user to take an action. App notifications can either be sent locally or from a cloud service using [push notifications](../push-notifications/index.md).

> [!NOTE]
> The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.

This section provides design and implementation guidance for app notifications in the [Windows App SDK](../../../windows-app-sdk/index.md) and WinRT.

## App notification UX and implementation guidance

- [App notification UX guidance](toast-ux-guidance.md)
- [Send a local app notification from C# apps](send-local-toast.md)
- [Send a local app notification from C++ UWP apps](send-local-toast-cpp-uwp.md)
- [Send a local app notification from Win32 C++ WRL apps](send-local-toast-desktop-cpp-wrl.md)
- [Send a local app notification from other types of unpackaged apps](send-local-toast-other-apps.md)
- [App notification content](adaptive-interactive-toasts.md)
- [App notification content schema](toast-schema.md)
- [Schedule an app notification](scheduled-toast.md)

## Additional app notifications features

- [Custom audio on app notifications](custom-audio-on-toasts.md)
- [App notification progress bar and data binding](toast-progress-bar.md)
- [App notification with pending update activation](toast-pending-update.md)
- [Custom timestamps on app notifications](custom-timestamps-on-toasts.md)
- [Grouping app notifications with collections](toast-collections.md)
- [App notification headers](toast-headers.md)
- [Notification listener: Access all notifications](notification-listener.md)

## Limitations

The app notifications support in the Windows App SDK currently has these limitations:

- Notifications for an elevated (admin) app are currently not supported.

## Next steps

See [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md) to get started.
