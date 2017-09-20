---
author: adwilso
Description: Windows Push Notification Services (WNS) enables third-party developers to send toast, tile, badge, and raw updates from their own cloud service. There are many ways to send the notifications depending on the needs of your application
title: Choosing the right push notification channel type
ms.author: mijacobs
ms.date: 07/07/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Choosing the right push notification channel type

This article covers the three types of UWP push notification channels (primary, secondary, and alternate) that help you deliver content to your app. 

(For details on how to create push notifications, see the the [Windows Push Notification Services (WNS) overview](../tiles-and-notifications-windows-push-notification-services--wns--overview.md).) 

## Types of push channels 

There are three types of push channels that can be used to send notifications to a UWP app. They are: 

[Primary channel](https://docs.microsoft.com/en-us/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanagerforuser#methods_) - the "traditional" push channel. Can be used by any app in the store to send toast, tile, raw, or badge notifications (Link to descriptions of toast/tiles/badge)

[Secondary tile channel](https://docs.microsoft.com/en-us/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanagerforuser#methods_) - used to push tile updates for a secondary tile. Can only be used to send tile or badge notifications to a secondary tile pinned on the user's start screen

[Alternate channel](https://docs.microsoft.com/en-us/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanagerforuser#methods_) - A new type of channel added in the Creators Update. It allows for raw notifications to be sent to any UWP app, including those which aren't registered in the Store. 

> [!NOTE]
> No matter which push channel you use, once your app is running on the device, it will always be able to send local toast, tile, or badge notifications. It can send local notifications from the foreground app processes or from a background task. 


## Primary channels

These are the most commonly used channels on Windows right now, and are good for almost any scenario where your app is going to be distributed through the Windows Store. They allow you to send all types of notifications to the app. 

### What do primary channels enable?

-   **Sending tile or badge updates to the primary tile.** If the user has chosen to pin your tile to the start screen, this is your chance to show off. Send updates with useful information or reminders of experiences within your app. 
-   **Sending toast notifications.** Toast notifications are a chance to get some information in front of the user immediately. They are painted by the shell over top of most apps, and live in the action center so the user can go back and interact with them later. 
-   **Sending raw notifications to trigger a background task.** Sometimes you want to do some work on behalf of the user based on a notification. Raw notifications allow your app's background tasks to run 
-   **Message encryption in transit provided by Windows using TLS.** Messages are encrypted on the wire both coming into WNS and going to the user's device.  

### Limitations of primary channels

-   Requires using the WNS REST API to push notifications, which isn't standard across device vendors. 
-   Only one channel can be created per app 
-   Requires your app to be registered in the Windows Store

### Creating a primary channel 

```csharp
PushNotificationChannel channel = 
	await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
```

## Secondary tile channels

These are channels that can be used to push tile and badge updates to a secondary tile. These are used by apps to notify users of interesting actions or information that they can interact with in the app, such as new messages in a group chat or an updated sports score. 

### What do secondary tile channels enable?

-   Sending tile or badge notifications to secondary tiles. Secondary tiles are a great way to pull users back into your app. They are a deep link to information they care about, and putting relevant information on the tiles helps to bring them back again and again.
-   Separation of channels (and expiries) between various tiles. This allows you to separate the logic in the backend between the various types of secondary tiles that a user might pin to their start screen. 
-   Message encryption in transit provided by Windows using TLS. Messages are encrypted on the wire both coming into WNS and going to the user's device.  

### Limitations of secondary tile channels

-   No toast or raw notifications allowed. Toast or raw notifications sent to a secondary tile are ignored by the system.
-   Requires your app to be registered in the Windows Store


### Creating a secondary tile channel 

```csharp
PushNotificationChannel channel = 
	await PushNotificationChannelManager.CreatePushNotificationChannelForSecondaryTileAsync(tileId);
```

## Alternate channels

Alternate channels enable apps to send push notifications without registering to the Windows Store or creating push channels outside of the primary one used for the app. 
 
### What do alternate channels enable?
-   Send raw push notifications to a UWP running on any Windows device. Alternate channels only allow for raw notifications.
-   Allows apps to create multiple raw push channels for different features within the app. An app can create up to 1000 alternate channels, and each one is valid for 30 days. Each of these channels can be managed or revoked separately by the app.
-   Alternate push channels can be created without registering an app with the Windows Store. If you app is going to be installed on devices without registering it in the Windows Store, it will still be able to receive push notifications.
-   Servers can push notifications using the W3C standard REST APIs and VAPID protocol. Alternate channels use the W3C standard protocol, this allows you to simplify the server logic that needs to be maintained.
-   Full, end-to-end, message encryption. While the primary channel provides encryption while in transit, if you want to be extra secure, alternate channels enable your app to pass through encryption headers to protect a message. 

### Limitations of alternate channels
-   Apps cannot send toast, tile, or badge type notifications. The alternate channel limits your ability to send other notification types. Your app is still able to send local notifications from your background task. 
-   Requires a different REST API than either primary or secondary tile channels. Using the standard W3C REST API means that your app will need to have different logic for sending push toast or tile updates

### Creating an alternate channel 

```csharp
PushNotificationChannel webChannel = 
	await PushNotificationChannelManager.Current.CreateRawPushNotificationChannelWithAlternateKeyForApplicationAsync(applicationServerKey, appChannelId);
```

## Channel type comparison
Here is a quick comparison between the different types of channels:

<table>

<tr class="header">
<th align="left">**Type**</th>
<th align="left">**Push toast?**</th>
<th align="left">**Push tile/badge?**</th>
<th align="left">**Push raw notifications?**</th>
<th align="left">**Authentication**</th>
<th align="left">**API**</th>
<th align="left">**Store registration required?**</th>
<th align="left">**Channels**</th>
<th align="left">**Encryption**</th>
</tr>


<tr class="odd">
<td align="left">Primary</td>
<td align="left">Yes</td>
<td align="left">Yes - primary tile only</td>
<td align="left">Yes</td>
<td align="left">OAuth</td>
<td align="left">WNS REST API</td>
<td align="left">Yes</td>
<td align="left">One per app</td>
<td align="left">In Transit</td>
</tr>
<tr class="even">
<td align="left">Secondary Tile</td>
<td align="left">No</td>
<td align="left">Yes - secondary tile only</td>
<td align="left">No</td>
<td align="left">OAuth</td>
<td align="left">WNS REST API</td>
<td align="left">Yes</td>
<td align="left">One per secondary tile</td>
<td align="left">In Transit</td>
</tr>
<tr class="odd">
<td align="left">Alternate</td>
<td align="left">No</td>
<td align="left">No</td>
<td align="left">Yes</td>
<td align="left">VAPID</td>
<td align="left">WebPush W3C Standard</td>
<td align="left">No</td>
<td align="left">1,000 per app</td>
<td align="left">In transit + end to end encryption possible with header pass through (requires app code)</td>
</tr>
</table>

## Choosing the right channel

In general, we recommend using the primary channel in your app, with a few exceptions: 

1. If you are pushing a tile update to a secondary tile, use the secondary tile push channel.
2. If you are passing out channels to other services (such as in the case of a browser) use the alternate channel.
3. If you are creating an app that won't be listed in the Windows store (such as an LOB app) use an alternate channel.
4. If you have existing web push code on your server you wish to reuse or have a need for multiple channels in your backend service, use alternate channels.

## Related articles

* [Send a local tile notification](../tiles-and-notifications-sending-a-local-tile-notification.md)
* [Adaptive and interactive toast notifications](../tiles-and-notifications-adaptive-interactive-toasts.md)
* [Quickstart: Sending a push notification](https://msdn.microsoft.com/library/windows/apps/xaml/hh868252)
* [How to update a badge through push notifications](https://msdn.microsoft.com/library/windows/apps/hh465450)
* [How to request, create, and save a notification channel](https://msdn.microsoft.com/library/windows/apps/hh465412)
* [How to intercept notifications for running applications](https://msdn.microsoft.com/library/windows/apps/hh465450)
* [How to authenticate with the Windows Push Notification Service (WNS)](https://msdn.microsoft.com/library/windows/apps/hh465407)
* [Push notification service request and response headers](https://msdn.microsoft.com/library/windows/apps/hh465435)
* [Guidelines and checklist for push notifications](https://msdn.microsoft.com/library/windows/apps/hh761462)
* [Raw notifications](https://msdn.microsoft.com/library/windows/apps/hh761488)
