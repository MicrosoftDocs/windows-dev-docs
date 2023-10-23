---
title: Push notifications overview
description: Overview of Windows App SDK Push Notifications
ms.topic: article
ms.date: 7/26/2021
keywords: push, notification
ms.localizationpriority: medium
---

# Push notifications overview

Push notifications in the [Windows App SDK](../../index.md) use [Windows Push Notifications Service (WNS)](https://aka.ms/wns) to send rich notifications to Windows apps using Azure App Registration identities.

## Push notification types and usage scenarios

Push notifications can be used to enable several distinct features. The content and effect of a push notification will vary based on the way it is being used.

### Raw notifications

*Raw* notifications are consumed by the app itself and are not communicated to the user. They can be used to control application behavior or notify applications of state changes remotely.

| Scenario | Description  | Example |
|----------|--------------|---------|
| Application Wake Up | Raw notifications can be used by app developers to wake up their application instead of it constantly running, which frees up user resources. | *Without raw notifications*: The Contoso Chat app runs in the background while waiting for a VOIP call.<br/><br/>*With raw notifications*: The Contoso app process can be in a terminated state until a raw notification signals it and brings the process up, indicating a VOIP call has been initiated.
| Real Time Sync | Raw notifications can replace polling scenarios by allowing the app developer to send payloads from their App Service to the App Client on the local device. These payloads notify the App Client to sync with the App Service. | *Without raw notifications*: The Contoso Chat app polls the Contoso cloud service every 30 mins to check for content updates, and initiates a sync if updates are available.<br/><br/>*With raw notifications*: The Contoso Chat app is notified immediately when new content is available, and syncs that content right away.

### App notifications from the cloud

*App* notifications are used to communicate with the user. The notification content is displayed in a transient window in the bottom right corner of the screen and in the Notification Center (called Action Center in Windows 10). App notifications can be used to inform the user of application status or state changes, or to prompt the user to take an action. App notifications can be either push (sent from the cloud) or sent locally. Sending a cloud-sourced app notification is similar to sending a raw notification, except the *X-WNS-Type* header is `toast`, *Content-Type* is `text/xml`, and the content contains the app notification XML payload, which you can learn more about [here](../app-notifications/app-notifications-quickstart.md).

## Limitations

The push notifications support in the Windows App SDK currently has these limitations:

- If your app is published as [*self-contained*](../../../package-and-deploy/self-contained-deploy/deploy-self-contained-apps.md) or is running with the elevated (admin) privilege, this feature may not be supported. In your app, use the `winrt::PushNotificationManager::IsSupported()` check as demonstrated in [Quickstart: Push notifications in the Windows App SDK](push-quickstart.md) and implement a custom socket if the feature is unsupported.
- Microsoft reserves the right to disable or revoke apps from using push notifications.

## Next steps

See [Quickstart: Push notifications in the Windows App SDK](push-quickstart.md) to get started.

## Related topics

- [Windows Push Notification Service (WNS)](https://aka.ms/wns)
- [Quickstart: Push notifications in the Windows App SDK](push-quickstart.md)
- [Troubleshooting Windows App SDK](troubleshooting.md)
- [Notifications UX Guidance](../../../design/shell/tiles-and-notifications/toast-ux-guidance.md)

