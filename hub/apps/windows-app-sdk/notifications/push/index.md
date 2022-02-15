---
title: Push notifications overview
description: Overview of Windows App SDK Push Notifications
ms.topic: article
ms.date: 7/26/2021
keywords: push, notification
ms.author: hickeys
author: hickeys
ms.localizationpriority: medium
---

# Push notifications overview

Push notifications in the Windows App SDK use [Windows Notification Push Service](/windows/apps/design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview) to send rich notifications to Windows apps using Azure App Registration identities.

## Prerequisites

> [!IMPORTANT]
> Push notifications are currently experimental and supported only in the [experimental release channel](../../experimental-channel.md) of the Windows App SDK. This feature is not supported for use by apps in production environments, and the API surface is subject to change.

To use push notifications in the Windows App SDK:

1. Download and install the latest experimental release of the Windows App SDK. For more information, see [Install developer tools](../../set-up-your-development-environment.md).
2. Follow the instructions to [create a new project that uses the Windows App SDK](../../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../../use-windows-app-sdk-in-existing-project.md).


## Push notification types and usage scenarios

Push notifications can be used to enable several distinct features. The content and effect of a push notification will vary based on the way it is being used.

### Raw notifications

Raw notifications are consumed by the app itself and are not communicated to the user. They can be used to control application behavior or notify applications of state changes remotely.

| Scenario | Description  | Example |
|----------|--------------|---------|
| Application Wake Up | Push notifications can be used to wake up your application instead of it having to be constantly running which frees up user resources. | *Without push notifications*: The Contoso Chat app runs in the background while waiting for a VOIP call.<br/><br/>*With push notifications*: The Contoso Chat app can sleep until awoken by a push notification indicating a VOIP call has been initiated.
| Real Time Sync | Push notifications can replace polling scenarios for your applications by sending push notifications to your clients and notifying them to sync with your web service | *Without push notifications*: The Contoso Chat app polls the Contoso cloud service every 30 mins to check for content updates, and initiates a sync if updates are available.<br/><br/>*With push notifications*: The Contoso Chat app is notified immediately when new content is available, and syncs that content right away.

### Toast notifications

Toast notifications are used to communicate with the user. The notification content is displayed in Action Center and in a transient window in the bottom right corner of the screen. Toast notifications can be used to inform the user of application status or state changes, or to prompt the user to take an action.

## Limitations

The push notifications support in the Windows App SDK currently has these limitations:

- Push notifications are only supported in MSIX packaged apps that are running on Windows 10 version 2004 (build 19041) or later releases.
- Microsoft reserves the right to disable or revoke apps from push notifications during the private preview.
- Microsoft does not guarantee the reliability or latency of push notifications.
- During the private preview, push notification volume is limited to 1 million per month.

## Related topics

- [Windows Push Notification Service](/windows/apps/design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview)
- [Send a push notification using the Windows App SDK](push-quickstart.md)
- [Troubleshooting Windows App SDK](/troubleshooting.md)
- [Toast UX Guidance](../../../design/shell/tiles-and-notifications/toast-ux-guidance.md)