---
author: mijacobs
Description: Learn how to use tiles, badges, toasts, and notifications to provide entry points into your app and keep users up-to-date.
title: Tiles, badges, and notifications
ms.assetid: 48ee4328-7999-40c2-9354-7ea7d488c538
label: Tiles, badges, and notifications
template: detail.hbs
ms.author: mijacobs
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Tiles, badges, and notifications for UWP apps
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css"> 


Learn how to use tiles, badges, toasts, and notifications to provide entry points into your app and keep users up-to-date.

<p><img style="float: left; margin: 0px 15px 15px 0px;" src="images/tile-and-live-tile.png" />
A tile is an app's representation on the Start menu. Every UWP app has a tile. You can enable different tile sizes (small, medium, wide, and large).</p>

<p>You can use a <em>tile notification</em> to update the tile to communicate new information to the user, such as news headlines, or the subject of the most recent unread message.</p>

<p>You can use a <em>badge</em> to provide status or summary info in the form of a system-provided glyph or a number from 1-99. Badges also appear on the task bar icon for an app. </p>

<p>A <em>toast notification</em> is a notification that your app sends to the user via a pop-up UI element called a <em>toast</em> (or <em>banner</em>). The notification can be seen whether the user is in your app or not.</p>
<p>A <em>push notification</em> or <em>raw notification</em> is a notification sent to your app either from Windows Push Notification Services (WNS) or from a background task. Your app can respond to these notifications either by notifying the user that something of interest happened (via badge update, tile update, or toast) or it can respond in any way of your choice.</p>

 
## Tiles 
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>[Create tiles](tiles-and-notifications-creating-tiles.md)</p></td>
<td align="left"><p>Customize the default tile for your app and provide assets for different screen sizes.</p></td>
</tr>
<tr>
<td align="left"><p>[Primary tile API's](tiles-and-notifications-primary-tile-apis.md)</p></td>
<td align="left"><p>Request to pin your app's primary tile, and check if the primary tile is currently pinned.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Create adaptive tiles](tiles-and-notifications-create-adaptive-tiles.md)</p></td>
<td align="left"><p>Adaptive tile templates are a new feature in Windows 10, allowing you to design your own tile notification content using a simple and flexible markup language that adapts to different screen densities. This article tells you how to create adaptive live tiles for your Universal Windows Platform (UWP) app.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Adaptive tiles schema](tiles-and-notifications-adaptive-tiles-schema.md)</p></td>
<td align="left"><p>Here are the elements and attributes you use to create adaptive tiles.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Special tile templates](tiles-and-notifications-special-tile-templates-catalog.md)</p></td>
<td align="left"><p>Special tile templates are unique templates that are either animated, or just allow you to do things that aren't possible with adaptive tiles.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[App icon assets](tiles-and-notifications-app-assets.md)</p></td>
<td align="left"><p>App icon assets, which appear in a variety of forms throughout the Windows 10 operating system, are the calling cards for your Universal Windows Platform (UWP) app. These guidelines detail where app icon assets appear in the system, and provide in-depth design tips on how to create the most polished icons.</p></td>
</tr>
</tbody>
</table>

## Notifications


<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>[Adaptive and interactive toast notifications](tiles-and-notifications-adaptive-interactive-toasts.md)</p></td>
<td align="left"><p>Adaptive and interactive toast notifications let you create flexible pop-up notifications with more content, optional inline images, and optional user interaction.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Notifications Visualizer](tiles-and-notifications-notifications-visualizer.md)</p></td>
<td align="left"><p>Notifications Visualizer is a new Universal Windows Platform (UWP) app in [the Store](https://www.microsoft.com/store/apps/notifications-visualizer/9nblggh5xsl1) that helps developers design adaptive live tiles for Windows 10.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Choose a notification delivery method](tiles-and-notifications-choosing-a-notification-delivery-method.md)</p></td>
<td align="left"><p>This article covers the four notification options—local, scheduled, periodic, and push—that deliver tile and badge updates and toast notification content.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Send a local tile notification](tiles-and-notifications-sending-a-local-tile-notification.md)</p></td>
<td align="left"><p>This article describes how to send a local tile notification to a primary tile and a secondary tile using adaptive tile templates.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Periodic notification overview](tiles-and-notifications-periodic-notification-overview.md)</p></td>
<td align="left"><p>Periodic notifications, which are also called polled notifications, update tiles and badges at a fixed interval by downloading content from a cloud service.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Windows Push Notification Services (WNS) overview](tiles-and-notifications-windows-push-notification-services--wns--overview.md)</p></td>
<td align="left"><p>The Windows Push Notification Services (WNS) enables third-party developers to send toast, tile, badge, and raw updates from their own cloud service. This provides a mechanism to deliver new updates to your users in a power-efficient and dependable way.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Code generated by the push notification wizard](tiles-and-notifications-the-code-generated-by-the-push-notification-wizard.md)</p></td>
<td align="left"><p>By using a wizard in Visual Studio, you can generate push notifications from a mobile service that was created with Azure Mobile Services. The Visual Studio wizard generates code to help you get started. This topic explains how the wizard modifies your project, what the generated code does, how to use this code, and what you can do next to get the most out of push notifications. See [Windows Push Notification Services (WNS) overview](tiles-and-notifications-windows-push-notification-services--wns--overview.md).</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Raw notification overview](tiles-and-notifications-raw-notification-overview.md)</p></td>
<td align="left"><p>Raw notifications are short, general purpose push notifications. They are strictly instructional and do not include a UI component. As with other push notifications, the WNS feature delivers raw notifications from your cloud service to your app.</p></td>
</tr>
</tbody>
</table>