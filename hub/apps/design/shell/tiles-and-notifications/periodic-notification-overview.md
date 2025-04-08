---
description: Periodic notifications, which are also called polled notifications, update tiles and badges at a fixed interval by downloading content from a cloud service.
title: Periodic notification overview
ms.assetid: 1EB79BF6-4B94-451F-9FAB-0A1B45B4D01C
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Periodic notification overview
 


Periodic notifications, which are also called polled notifications, update tiles and badges at a fixed interval by downloading content from a cloud service. To use periodic notifications, your client app code needs to provide two pieces of information:

-   The Uniform Resource Identifier (URI) of a web location for Windows to poll for tile or badge updates for your app
-   How often that URI should be polled

Periodic notifications enable your app to get live tile updates with minimal cloud service and client investment. Periodic notifications are a good delivery method for distributing the same content to a wide audience.

**Note**   You can learn more by downloading the [Push and periodic notifications sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Push%20and%20periodic%20notifications%20client-side%20sample%20(Windows%208)) for Windows 8.1 and re-using its source code in your Windows 10 app.

 

## How it works


Periodic notifications require that your app hosts a cloud service. The service will be polled periodically by all users who have the app installed. At each polling interval, such as once an hour, Windows sends an HTTP GET request to the URI, downloads the requested tile or badge content (as XML) that is supplied in response to the request, and displays the content on the app's tile.

Note that periodic updates cannot be used with toast notifications. Toast is best delivered through [scheduled](/previous-versions/windows/apps/hh465417(v=win.10)) or [push](/previous-versions/windows/apps/hh868252(v=win.10)) notifications.

## URI location and XML content


Any valid HTTP or HTTPS web address can be used as the URI to be polled.

The cloud server's response includes the downloaded content. The content returned from the URI must conform to the [Tile](/uwp/schemas/tiles/tilesschema/schema-root) or [Badge](/uwp/schemas/tiles/badgeschema/schema-root) XML schema specification, and must be UTF-8 encoded. You can use defined HTTP headers to specify the [expiration time](#expiration-of-tile-and-badge-notifications) or tag for the notification.

## Polling Behavior


Call one of these methods to begin polling:

-   [**StartPeriodicUpdate**](/uwp/api/Windows.UI.Notifications.TileUpdater#Windows_UI_Notifications_TileUpdater_StartPeriodicUpdate_Windows_Foundation_Uri_Windows_Foundation_DateTime_Windows_UI_Notifications_PeriodicUpdateRecurrence_) (Tile)
-   [**StartPeriodicUpdate**](/uwp/api/Windows.UI.Notifications.BadgeUpdater#Windows_UI_Notifications_BadgeUpdater_StartPeriodicUpdate_Windows_Foundation_Uri_Windows_Foundation_DateTime_Windows_UI_Notifications_PeriodicUpdateRecurrence_) (Badge)
-   [**StartPeriodicUpdateBatch**](/uwp/api/Windows.UI.Notifications.TileUpdater#Windows_UI_Notifications_TileUpdater_StartPeriodicUpdateBatch_Windows_Foundation_Collections_IIterable_1_Windows_UI_Notifications_PeriodicUpdateRecurrence_) (Tile)

When you call one of these methods, the URI is immediately polled and the tile or badge is updated with the received contents. After this initial poll, Windows continues to provide updates at the requested interval. Polling continues until you explicitly stop it (with [**TileUpdater.StopPeriodicUpdate**](/uwp/api/Windows.UI.Notifications.TileUpdater.StopPeriodicUpdate)), your app is uninstalled, or, in the case of a secondary tile, the tile is removed. Otherwise, Windows continues to poll for updates to your tile or badge even if your app is never launched again.

### The recurrence interval

You specify the recurrence interval as a parameter of the methods listed above. Note that while Windows makes a best effort to poll as requested, the interval is not precise. The requested poll interval can be delayed by up to 15 minutes at the discretion of Windows.

### The start time

You optionally can specify a particular time of day to begin polling. Consider an app that changes its tile content just once a day. In such a case, we recommend that you poll close to the time that you update your cloud service. For example, if a daily shopping site publishes the day's offers at 8 AM, poll for new tile content shortly after 8 AM.

If you provide a start time, the first call to the method polls for content immediately. Then, regular polling starts within 15 minutes of the provided start time.

### Automatic retry behavior

The URI is polled only if the device is online. If the network is available but the URI cannot be contacted for any reason, this iteration of the polling interval is skipped, and the URI will be polled again at the next interval. If the device is in an off, sleep, or hibernated state when a polling interval is reached, the URI is polled when the device returns from its off or sleep state.

### Handling app updates

If you release an app update that changes your polling URI, you should add a daily [time trigger background task](/windows/uwp/launch-resume/run-a-background-task-on-a-timer-) which calls StartPeriodicUpdate with the new URI to ensure your tiles are using the new URI. Otherwise, if users receive your app update but don't launch your app, their tiles will still be using the old URI, which may fail to display if the URI is now invalid or if the returned payload references local images that no longer exist.

## Expiration of tile and badge notifications


By default, periodic tile and badge notifications expire three days from the time they are downloaded. When a notification expires, the content is removed from the badge, tile, or queue and is no longer shown to the user. It is a best practice to set an explicit expiration time on all periodic tile and badge notifications, using a time that makes sense for your app or notification, to ensure that the content does not persist longer than it is relevant. An explicit expiration time is essential for content with a defined life span. It also assures the removal of stale content if your cloud service becomes unreachable, or if the user disconnects from the network for an extended period of time.

Your cloud service sets an expiration date and time for a notification by including the X-WNS-Expires HTTP header in the response payload. The X-WNS-Expires HTTP header conforms to the [HTTP-date format](https://www.w3.org/Protocols/rfc2616/rfc2616-sec3.html#sec3.3.1). For more information, see [**StartPeriodicUpdate**](/uwp/api/Windows.UI.Notifications.TileUpdater#Windows_UI_Notifications_TileUpdater_StartPeriodicUpdate_Windows_Foundation_Uri_Windows_Foundation_DateTime_Windows_UI_Notifications_PeriodicUpdateRecurrence_) or [**StartPeriodicUpdateBatch**](/uwp/api/Windows.UI.Notifications.TileUpdater#Windows_UI_Notifications_TileUpdater_StartPeriodicUpdateBatch_Windows_Foundation_Collections_IIterable_1_Windows_UI_Notifications_PeriodicUpdateRecurrence_).

For example, during a stock market's active trading day, you can set the expiration for a stock price update to twice that of your polling interval (such as one hour after receipt if you are polling every half-hour). As another example, a news app might determine that one day is an appropriate expiration time for a daily news tile update.

## Periodic notifications in the notification queue


You can use periodic tile updates with [notification cycling](/previous-versions/windows/apps/hh781199(v=win.10)). By default, a tile on the Start screen shows the content of a single notification until it is replaced by a new notification. When you enable cycling, up to five notifications are maintained in a queue and the tile cycles through them.

If the queue has reached its capacity of five notifications, the next new notification replaces the oldest notification in the queue. However, by setting tags on your notifications, you can affect the queue's replacement policy. A tag is an app-specific, case-insensitive string of up to 16 alphanumeric characters, specified in the [X-WNS-Tag](/previous-versions/windows/apps/hh465435(v=win.10)) HTTP header in the response payload. Windows compares the tag of an incoming notification with the tags of all notifications already in the queue. If a match is found, the new notification replaces the queued notification with the same tag. If no match is found, the default replacement rule is applied and the new notification replaces the oldest notification in the queue.

You can use notification queuing and tagging to implement a variety of rich notification scenarios. For example, a stock app could send five notifications, each about a different stock and each tagged with a stock name. This prevents the queue from ever containing two notifications for the same stock, the older of which is out of date.

For more information, see [Using the notification queue](/previous-versions/windows/apps/hh781199(v=win.10)).

### Enabling the notification queue

To implement a notification queue, first enable the queue for your tile (see [How to use the notification queue with local notifications](/archive/blogs/tiles_and_toasts/quickstart-how-to-use-the-tile-notification-queue-with-local-notifications)). The call to enable the queue needs to be done only once in your app's lifetime, but there is no harm in calling it each time your app is launched.

### Polling for more than one notification at a time

You must provide a unique URI for each notification that you'd like Windows to download for your tile. By using the [**StartPeriodicUpdateBatch**](/uwp/api/Windows.UI.Notifications.TileUpdater#Windows_UI_Notifications_TileUpdater_StartPeriodicUpdateBatch_Windows_Foundation_Collections_IIterable_1_Windows_UI_Notifications_PeriodicUpdateRecurrence_) method, you can provide up to five URIs at once for use with the notification queue. Each URI is polled for a single notification payload, at or near the same time. Each polled URI can return its own expiration and tag value.

## Related topics


* [Guidelines for periodic notifications]()
* [How to set up periodic notifications for badges](/previous-versions/windows/apps/hh761476(v=win.10))
* [How to set up periodic notifications for tiles](/previous-versions/windows/apps/hh761476(v=win.10))
 
