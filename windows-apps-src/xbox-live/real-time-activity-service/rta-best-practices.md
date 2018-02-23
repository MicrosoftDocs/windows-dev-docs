---
title: RTA Best Practices
author: KevinAsgari
description: Learn about the best practices when using the Xbox Live Real-Time Acitivity service.
ms.assetid: 543b78e3-d06b-4969-95db-2cb996a8bbd3
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, real time activity
ms.localizationpriority: low
---

# Real Time Activity (RTA) Best Practices
These best practices will help you make the most out of your title's use of RTA.


## The Basics

RTA uses WebSocket sessions to create a persistent connection with the client. That's how the service will deliver statistics to you. A client needs to send an authenticated connection request, and RTA will use the token provided on the request to verify the connection can be made and then establish it.

Once the connection has been established, your app can make requests to subscribe to particular statistics. On a successful subscription, RTA will return the current value and some additional metadata, like type of statistic, as part of the response payload. Then, RTA will forward any updates that happen to the statistic while the client is subscribed.

When your title doesn't require real-time updates on a statistic, it should terminate its subscription to that statistic.


## Handling Disconnects

Titles should be aware that when the authentication token for the user expires, their session will be terminated by the service. The title needs to be capable of detecting when such event happens, reconnect and re-subscribe to all the statistics it was previously subscribed to.

A client could also get disconnected due to a user's ISP having issues or when the process for the title is suspended. When this happens, a WebSocket event is raised to let the client know. In general, it is best practice to be able to handle disconnects from the service.


## Managing Subscriptions

In relation to managing subscriptions when a token expires, your title should track at all times which subscriptions have been made. Upon successfully subscribing, RTA returns a unique identifier for each subscribed statistic. In all future updates to that statistic, the identifier will be used instead of the name of the statistic. This is done to save bandwidth. Clients are responsible for maintaining their own mapping of statistics to RTA subscription IDs.


## Unsubscribing

Having unused subscriptions is not recommended. The service limits the number of subscriptions a user can have per title at a given time. If you are subscribing to everything and anything, you may hit that limit, and this may prevent subscription to some important statistics. (For more information about subscription limits, see **Throttles**, later in this topic.)

For example, your title might only need a subscription to a certain scene. When the user enters that scene, your title should subscribe. When the user leaves that scene, those statistics should be unsubscribed. Similarly, there is an unsubscribe-all message that can be used if no subscriptions are needed.

After unsubscribing, the subscription identifier for that statistic will no longer be used.


## Awareness of Latent Items in the Queue

When unsubscribing from a statistic, it's possible that there is an update for the statistic still in the process of reaching your client. So, even if your title has just unsubscribed from a statistic, be aware that it may still get an update or two related to that statistic.

The recommendation is to ignore the updates for statistics when the subscription identifier is not recognized.


## Ignore Messages You do not Understand

It's possible that the message protocol will get updated. To keep your app agnostic of any new messages, we recommend that your title simply discard unknown message types.


## Throttles

RTA enforces certain throttles on the service:

-   UserStats: 1000
-   Presence: 2500

If a client hits the throttling limit it will either receive an error as part of a subscribe/unsubscribe call, or it will be disconnected. In either case, more information about the throttling limitation that was hit will be available to the client along with the error message or disconnect message.

When developing your title, keep these concepts in mind. If you are doing something extreme, you may have a degraded app experience because the service could be throttling your calls. Right now, the service allows 1000 subscriptions to statistics per instance of an application. In addition to that, an instance of an application can also subscribe to a user's entire people list length for presence updates. This number is being tuned, so it may change in future releases.
