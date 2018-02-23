---
title: Real-Time Activity Service
author: KevinAsgari
description: Learn about the Xbox Live Real-Time Activity service.
ms.assetid: 50de262f-fc55-4301-83b5-0a8a30bc7852
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, real time activity service.
ms.localizationpriority: low
---

# Real-Time Activity Service

The Real-Time Activity (RTA) service allows an application on any device to subscribe to state data, user statistics, and presence. The system allows subscriptions to one's own data and to others' data in any title based on their privacy settings. This allows a flow of information without having to constantly poll to get the latest data.


## Developer Scenarios

There are many scenarios that RTA supports. Just a few of them are listed here, but the real power of RTA is the many scenarios that you will come up with that we haven't imagined yet. You could help define the next generation of gameplay where users often have at hand their Microsoft Surface or Apple iPad while they're interacting with your console title. RTA uses WebSocket technology, so the various subtopic walkthroughs includes an overview of the implementation using the WebSockets API provided by Windows.

The following are some simple scenarios that you can create for your title by using RTA:

-   Achievements progress app
-   Game help app
-   Squad viewer app
-   Statistics viewer
-   Presence Viewer


## Achievements progress app

Users are nearly always curious about their progress towards certain achievements, especially achievements that require performing an action a certain number of times. With real-time access to a user's statistics (which are aggregated in the Xbox Live Player Statistics service), you can present real-time progress for players and their friends towards achievements and milestones, on Xbox One or on a companion device, while the user is playing your title.


## Game help app

As users navigate through your title, real-time access to data allows you to present game help either next to the experience on Xbox One or on any companion device. Users might come up to a new map, new track, or challenging boss fight, and your Game Help companion could display user-generated or developer-generated videos and text to help the user through the experience in your title.


## Squad viewer app

In co-op multiplayer games, a player and his or her teammates work together for a shared goal. With so many players, it can be hard to keep track of the bigger picture. With access to real-time data, you could create a companion app that shows the high-level map and heat maps of where the action might be.


## Statistics viewer

While it's typical to think of companion apps when thinking about RTA, you can also use RTA in your core title. For example, you can use RTA to provide a player of a multiplayer game with a display of everyone's current statistics within the game, perhaps by the player simply pressing the View button on the controller while in the multiplayer match.


## Presence Viewer

When in a lobby, it's useful to have an up-to-date view of which friends are online and if they are playing the same title. You can subscribe to a user's friends' presence and show which friends are coming online, if they start playing your title, all in real-time.


## Subscription privacy and authorization

The latest version of RTA includes checks for privacy and authorization/content isolation. As long as privacy and authorization checks are satisfied, your app can subscribe to any statistic that is marked as RTA-enabled. (For more information on how to make a statistic RTA-enabled, see [Register for stat notifications](register-for-stat-notifications.md). There are no limitations on the kinds of statistics that can be made RTA-enabled—it's up to you, the developer. However, there is a limit to the *number* of statistics that a user can subscribe to per app session. If a user reaches that limit, he or she will receive an error on the next subscription.


## In this section

[Register for player stat change notifications](register-for-stat-notifications.md)  
Describes how to enable Real-Time Activity (RTA) for statistics or state information.

[Programming the Real-Time Activity (rta) service using winrt apis](programming-the-real-time-activity-service.md)  
Describes how to program the Real-Time Activity (RTA) service using WinRT APIs.

[Programming the Real-Time Activity (rta) service using the restful interface](programming-the-real-time-activity-service.md)  
Describes how to program the Real-Time Activity (RTA) service using the RESTful interface.

[Real Time Activity (rta) best practices](rta-best-practices.md)  
Best practices for using the Xbox services Real Time Activity (RTA) service to subscribe to statistics and state data from the Xbox data platform.
