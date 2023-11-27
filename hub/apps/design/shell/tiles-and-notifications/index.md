---
description: Learn how to use tiles, badges, toasts, and notifications to provide entry points into your app and keep users up-to-date.
title: Tiles, badges, and notifications
ms.assetid: 48ee4328-7999-40c2-9354-7ea7d488c538
label: Tiles, badges, and notifications
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Tiles, badges, and notifications for Windows apps
 

Learn how to use tiles, badges, toasts, and notifications to provide entry points into your app and keep users up-to-date.

> **Important APIs**: [UWP Community Toolkit Notifications nuget package](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/)

<p><img src="images/tile-and-live-tile.png" alt="Screenshot of a static tile and a live tile displaying a notification and a badge." />
A tile is an app's representation on the Start menu. Every Windows app has a tile. You can enable different tile sizes (small, medium, wide, and large).</p>

<p>You can use a <em>tile notification</em> to update the tile to communicate new information to the user, such as news headlines, or the subject of the most recent unread message.</p>

<p>You can use a <em>badge</em> to provide status or summary info in the form of a system-provided glyph or a number from 1-99. Badges also appear on the task bar icon for an app. </p>

<p>A <em>toast notification</em> is a notification that your app sends to the user via a pop-up UI element called a <em>toast</em> (or <em>banner</em>). The notification can be seen whether the user is in your app or not.</p>
<p>A <em>push notification</em> or <em>raw notification</em> is a notification sent to your app either from Windows Push Notification Services (WNS) or from a background task. Your app can respond to these notifications either by notifying the user that something of interest happened (via badge update, tile update, or toast) or it can respond in any way of your choice.</p>

 
## Tiles
| Article | Description |
| --- | --- |
| [Create tiles](creating-tiles.md) | Customize the default tile for your app and provide assets for different screen sizes. |
| [App icon assets](/windows/apps/design/style/iconography/overview) | App icon assets, which appear in a variety of forms throughout the Windows 10 operating system, are the calling cards for your Windows app. These guidelines detail where app icon assets appear in the system, and provide in-depth design tips on how to create the most polished icons. |
| [Primary tile API's](primary-tile-apis.md) | Request to pin your app's primary tile, and check if the primary tile is currently pinned. |
| [Tile content](create-adaptive-tiles.md) | Tile notification content is specified using adaptive, a new feature in Windows 10, allowing you to design your own tile notification content using a simple and flexible markup language that adapts to different screen densities. This article tells you how to create adaptive live tiles for your Windows app. |
| [Tile content schema](../tiles-and-notifications/tile-schema.md) | Here are the elements and attributes you use to create adaptive tiles. |
| [Special tile templates](special-tile-templates-catalog.md) | Special tile templates are unique templates that are either animated, or just allow you to do things that aren't possible with adaptive tiles. |
| [Send local tile notification](sending-a-local-tile-notification.md) | Learn how to send a local tile notification, adding rich dynamic content to your Live Tile. |


## Notifications

| Article | Description |
| --- | --- |
| [Toast notifications](adaptive-interactive-toasts.md) | Adaptive and interactive toast notifications let you create flexible pop-up notifications with more content, optional inline images, and optional user interaction. |
| [Send a local toast notification](send-local-toast.md) | Learn how to send an interactive toast notification. |
| [Notifications Visualizer](notifications-visualizer.md) | Notifications Visualizer is a new Windows app in [the Store](https://www.microsoft.com/store/apps/notifications-visualizer/9nblggh5xsl1) that helps developers design adaptive live tiles for Windows 10. |
| [Choose a notification delivery method](choosing-a-notification-delivery-method.md) | This article covers the four notification options—local, scheduled, periodic, and push—that deliver tile and badge updates and toast notification content. |
| [Periodic notification overview](periodic-notification-overview.md) | Periodic notifications, which are also called polled notifications, update tiles and badges at a fixed interval by downloading content from a cloud service. |
| [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md) | The Windows Push Notification Services (WNS) enables third-party developers to send toast, tile, badge, and raw updates from their own cloud service. This provides a mechanism to deliver new updates to your users in a power-efficient and dependable way. |
| [Code generated by the push notification wizard](the-code-generated-by-the-push-notification-wizard.md) | By using a wizard in Visual Studio, you can generate push notifications from a mobile service that was created with Azure Mobile Services. The Visual Studio wizard generates code to help you get started. This topic explains how the wizard modifies your project, what the generated code does, how to use this code, and what you can do next to get the most out of push notifications. See [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md). |
| [Raw notification overview](raw-notification-overview.md) | Raw notifications are short, general purpose push notifications. They are strictly instructional and do not include a UI component. As with other push notifications, the WNS feature delivers raw notifications from your cloud service to your app. |