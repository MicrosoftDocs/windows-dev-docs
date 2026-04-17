---
title: App Notifications Overview for Windows Apps
description: Learn how to send, manage, and respond to app notifications in Windows apps using the Windows App SDK notification APIs and Notification Center.
ms.topic: concept-article
ms.date: 03/01/2026
keywords: toast, notification
ms.localizationpriority: medium
no-loc: [toast, Toast, app, App]
---
# App notifications overview

![A screen capture showing an app notification above the task bar. The notification is a reminder for an event. The app name, event name, event time, and event location are shown. A selection input displays the currently selected value, "Going". There are two buttons labeled "RSVP" and "Dismiss"](images/shell-1x.png)

App notifications are UI popups that appear outside of your app's window, delivering timely information or actions to the user. Notifications can be purely informational, can launch your app when clicked, or can trigger a background action without bringing your app to the foreground. The [Windows App SDK](../../../windows-app-sdk/index.md) provides APIs for sending and managing app notifications, and for responding to user interaction with notifications.

This section of the documentation covers local app notifications, which are created and displayed from within your app. To send notifications from a cloud service, see [Push notifications overview](../push-notifications/index.md).

## App notifications quickstart

For a walkthrough of creating notification UI, sending a notification, and handling foreground and background activation in a WinUI app, see [Quickstart: App notifications](app-notifications-quickstart.md).

## Use app notifications with other frameworks

These articles show how to send an app notification and handle activation for other supported frameworks. For notification content, features, and design guidance, see the sections below. For a full list of supported frameworks, see [Use app notifications with other frameworks](app-notifications-other-frameworks.md).

- [WPF apps](app-notifications-wpf.md)
- [WinForms apps](app-notifications-winforms.md)
- [Console apps](app-notifications-console.md)
- [UWP apps](/windows/uwp/develop/app-notifications-uwp)

## UX and content guidance

These articles cover how to design your notifications and define their content, including text, images, buttons, inputs, and other UI elements.

- [App notification UX guidance](app-notifications-ux-guidance.md)
- [App notification content](app-notifications-content.md)
- [App notification content schema](app-notifications-schema.md)
- [Schedule an app notification](app-notifications-scheduled.md)

## [Additional app notifications features](app-notifications-additional-features.md)

These articles cover additional capabilities you can add to your app notifications, such as progress bars, custom audio, and grouping.

- [Remove app notifications](manage-app-notifications.md)
- [Custom audio on app notifications](app-notifications-custom-audio.md)
- [App notification progress bar and data binding](app-notifications-progress-bar.md)
- [App notification with pending update](app-notifications-pending-update.md)
- [Custom timestamps on app notifications](app-notifications-custom-timestamps.md)
- [App notification collections](app-notifications-collections.md)
- [App notification headers](app-notifications-headers.md)
- [Notification listener: Access all notifications](notification-listener.md)

## Limitations

The app notifications support in the Windows App SDK currently has these limitations:

- Apps running with administrator privileges (elevated) cannot send or receive app notifications.

## See also

- [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md)
- [Windows App SDK](../../../windows-app-sdk/index.md)
- [Push notifications overview](../push-notifications/index.md)

