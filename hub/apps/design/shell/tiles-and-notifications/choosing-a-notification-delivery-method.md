---
description: This article covers the four notification options&\#8212;local, scheduled, periodic, and push&\#8212;that deliver tile and badge updates and toast notification content.
title: Choose a notification delivery method
ms.assetid: FDB43EDE-C5F2-493F-952C-55401EC5172B
label: Choose a notification delivery method
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Choose a notification delivery method

 


This article covers the four notification options—local, scheduled, periodic, and push—that deliver tile and badge updates and toast notification content. A tile or a toast notification can get information to your user even when the user is not directly engaged with your app. The nature and content of your app and the information that you want to deliver can help you determine which notification method or methods is best for your scenario.

## Notification delivery methods overview


There are four mechanisms that an app can use to deliver a notification:

-   **Local**
-   **Scheduled**
-   **Periodic**
-   **Push**

This table summarizes the notification delivery types.

<table>
<colgroup>
<col width="25%" />
<col width="25%" />
<col width="25%" />
<col width="25%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Delivery method</th>
<th align="left">Use with</th>
<th align="left">Description</th>
<th align="left">Examples</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">Local</td>
<td align="left">Tile, Badge, Toast</td>
<td align="left">A set of API calls that send notifications while your app is running, directly updating the tile or badge, or sending a toast notification.</td>
<td align="left"><ul>
<li>A music app updates its tile to show what's &quot;Now Playing&quot;.</li>
<li>A game app updates its tile with the user's high score when the user leaves the game.</li>
<li>A badge whose glyph indicates that there's new info int the app is cleared when the app is activated.</li>
</ul></td>
</tr>
<tr class="even">
<td align="left">Scheduled</td>
<td align="left">Tile, Toast</td>
<td align="left">A set of API calls that schedule a notification in advance, to update at the time you specify.</td>
<td align="left"><ul>
<li>A calendar app sets a toast notification reminder for an upcoming meeting.</li>
</ul></td>
</tr>
<tr class="odd">
<td align="left">Periodic</td>
<td align="left">Tile, Badge</td>
<td align="left">Notifications that update tiles and badges regularly at a fixed time interval by polling a cloud service for new content.</td>
<td align="left"><ul>
<li>A weather app updates its tile, which shows the forecast, at 30-minute intervals.</li>
<li>A &quot;daily deals&quot; site updates its deal-of-the-day every morning.</li>
<li>A tile that displays the days until an event updates the displayed countdown each day at midnight.</li>
</ul></td>
</tr>
<tr class="even">
<td align="left">Push</td>
<td align="left">Tile, Badge, Toast, Raw</td>
<td align="left">Notifications sent from a cloud server, even if your app isn't running.</td>
<td align="left"><ul>
<li>A shopping app sends a toast notification to let a user know about a sale on an item that they're watching.</li>
<li>A news app updates its tile with breaking news as it happens.</li>
<li>A sports app keeps its tile up-to-date during an ongoing game.</li>
<li>A communication app provides alerts about incoming messages or phone calls.</li>
</ul></td>
</tr>
</tbody>
</table>

 

## Local notifications


Updating the app tile or badge or raising a toast notification while the app is running is the simplest of the notification delivery mechanisms; it only requires local API calls. Every app can have useful or interesting information to show on the tile, even if that content only changes after the user launches and interacts with the app. Local notifications are also a good way to keep the app tile current, even if you also use one of the other notification mechanisms. For instance, a photo app tile could show photos from a recently added album.

We recommended that your app update its tile locally on first launch, or at least immediately after the user makes a change that your app would normally reflect on the tile. That update isn't seen until the user leaves the app, but by making that change while the app is being used ensures that the tile is already up-to-date when the user departs.

While the API calls are local, the notifications can reference web images. If the web image is not available for download, is corrupted, or doesn't meet the image specifications, tiles and toast respond differently:

-   Tiles: The update is not shown
-   Toast: The notification is displayed, but your image is dropped

By default, local toast notifications expire in three days, and local tile notifications never expire. We recommend overriding these defaults with an explicit expiration time that makes sense for your notifications (toasts have a max of three days). 

For more information, see these topics:

-   [Send a local tile notification](/windows/uwp/launch-resume/sending-a-local-tile-notification)
-   [Send a local toast notification](send-local-toast.md)
-   [Windows app notifications code samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)

## Scheduled notifications


Scheduled notifications are the subset of local notifications that can specify the precise time when a tile should be updated or a toast notification should be shown. Scheduled notifications are ideal in situations where the content to be updated is known in advance, such as a meeting invitation. If you don't have advance knowledge of the notification content, you should use a push or periodic notification.

Note that scheduled notifications cannot be used for badge notifications; badge notifications are best served by local, periodic, or push notifications.

By default, scheduled notifications expire three days from the time they are delivered. You can override this default expiration time on scheduled tile notifications, but you cannot override the expiration time on scheduled toasts.

For more information, see these topics:

-   [Scheduling a toast notification](scheduled-toast.md)
-   [Windows app notifications code samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)

## Periodic notifications


Periodic notifications give you live tile updates with a minimal cloud service and client investment. They are also an excellent method of distributing the same content to a wide audience. Your client code specifies the URL of a cloud location that Windows polls for tile or badge updates, and how often the location should be polled. At each polling interval, Windows contacts the URL to download the specified XML content and display it on the tile.

Periodic notifications require the app to host a cloud service, and this service will be polled at the specified interval by all users who have the app installed. Note that periodic updates cannot be used for toast notifications; toast notifications are best served by scheduled or push notifications.

By default, periodic notifications expire three days from the time polling occurs. If needed, you can override this default with an explicit expiration time.

For more information, see these topics:

-   [Periodic notification overview](periodic-notification-overview.md)
-   [Windows app notifications code samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)

## Push notifications


Push notifications are ideal to communicate real-time data or data that is personalized for your user. Push notifications are used for content that is generated at unpredictable times, such as breaking news, social network updates, or instant messages. Push notifications are also useful in situations where the data is time-sensitive in a way that would not suit periodic notifications, such as sports scores during a game.

Push notifications require a cloud service that manages push notification channels and chooses when and to whom to send notifications.

By default, push notifications expire three days from the time they are received by the device. If needed, you can override this default with an explicit expiration time (toasts have a max of three days).

For more information, see:

-   [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md)
-   [Guidelines for push notifications](./windows-push-notification-services--wns--overview.md)
-   [Windows app notifications code samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)


## Related topics


* [Send a local tile notification](/windows/uwp/launch-resume/sending-a-local-tile-notification)
* [Send a local toast notification](send-local-toast.md)
* [Guidelines for push notifications](./windows-push-notification-services--wns--overview.md)
* [Guidelines for toast notifications](./index.md)
* [Periodic notification overview](periodic-notification-overview.md)
* [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md)
* [Windows app notifications code samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)
 

 
