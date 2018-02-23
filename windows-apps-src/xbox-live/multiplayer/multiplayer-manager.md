---
title: Multiplayer Manager
author: KevinAsgari
description: Learn about Xbox Live multiplayer manager, a high level API designed to make it easier to implement multiplayer.
ms.assetid: f3a6c8bc-4f73-4b99-ac51-aadee73c8cfa
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Multiplayer Manager

Xbox Live provides extensive support for adding multiplayer functionality to your titles, allowing your game to connect Xbox Live members across the world.  This includes rich matchmaking scenarios, the ability for a player to join a friend's game in progress, and more. However, implementing Xbox Live multiplayer by using the Multiplayer 2015 APIs directly can be a complex task, requiring a large degree of design and testing to verify that you are following the best practices, and meeting certification requirements.

Multiplayer Manager makes it easy to add multiplayer functionality to your game by managing sessions and matchmaking, and by providing a state and event based programming model. It is a set of APIs designed to make it easy to implement multiplayer scenarios for your Xbox Live game. It provides an API that is oriented around common multiplayer scenarios such as playing multiplayer games with friends, handling game invites, handling join in progress, matchmaking, and more. It supports multiple local users and makes it easier for your title to integrate with Multiplayer Session Directory if you are using a third party matchmaking service. Many of these scenarios can be accomplished with just a few API calls.

## Key Features
These are the main features of the Multiplayer Manager API:

* Easy session management and Xbox Live Matchmaking
* State and event based programming model.
* Ensures Xbox Live Best Practices  along with being Multiplayer XR compliant.
* Supports both Xbox One XDK and UWP titles.
* Implements [Multiplayer 2015 flowcharts](https://developer.xboxlive.com/en-us/platform/development/education/Documents/Xbox%20One%20Multiplayer%202015%20Developer%20Flowcharts.aspx).
* Works alongside the traditional Multiplayer 2015 APIs.

>**Important** - Your game must still implement required events for online multiplayer in order to pass certification.

## Overview
Multiplayer Manager is oriented around a few key concepts:
* `lobby_session` : A persistent session used for managing users that are local to this device and your invited friends that wish to play together. The group may play multiple rounds, maps, levels, etc., of the game, and the lobby session tracks this core group of friends (including players local to the device). Typically, this group is formed while the host may be navigating through the menus, chatting with the group members to decide what game mode they want to play.

* `game_session` : Tracks players who are playing a specific instance of the game. For example, a race, map, or level. You can create a new game session via `join_game_from_lobby` that includes the members in the lobby session.  When a member accepts an invite, they will be added to the lobby and the game session (if there is room). Additional players may be added to the game session if matchmaking is enabled, but those additional players are not added to the lobby session. This means that once the game ends, the players in the lobby session are still together, while the extra players from matchmaking are not.

* `multiplayer_member` : Represents an individual user signed in on a local or remote device.

* `do_work` : Ensures proper game state updates are maintained between the title and the Xbox Live Multiplayer Service. To ensure best performance, do_work() must be called frequently, such as once per frame. It provides you with a list of `multiplayer_event` callback events for the game to handle.

## State Machine
The `do_work()` call is necessary to ensure your state is kept fresh.  For Multiplayer Manager to do its work, developers must call the `do_work()` method regularly. The most reliable way to do this is to call at least once per frame. `do_work()` returns quickly when it has no work to do, so there is no reason for concern about calling it too often.

All objects returned by the Multiplayer Manager API should not be considered thread-safe. However, it gives you control to do thread synchronization if you are calling it from multiple threads. The library has internal multithreading protection, but you will still need to implement your own locking if you require one thread to access any values – for example, walking the members() list while another thread might be invoking `do_work()`.

Multiplayer Manager maintains a state based model updating the sessions in the background as players join, leave, or are when sessions are updated. In order to help avoid thread synchronization problems between the UI thread and your game thread, Multiplayer Manager does not update the app visible state of the sessions until you call the `do_work()` method. Traditionally you would receive notifications about events such as session changes on a background thread, and then have to synchronize it with a UI thread to display these changes. With Multiplayer Manager, this work is done for you behind the scenes.  You can call `do_work()` on your main thread at the time of your choosing, to get the latest snapshot of the state which Multiplayer Manager is buffering for you behind the scenes.

## Events and Notifications
Multiplayer Manager defines a set of significant events (see `multiplayer_event_type`) and notifies the title via the `do_work()` method when they happen. Events such as remote players joining or leaving, member properties changing, or session state changing. All Multiplayer Manager APIs are synchronous. The `do_work()` method returns a list of events when these asynchronous operations are completed. Your title should handle these events as you see appropriate. Please see the `multiplayer_event` class documentation for more details.

For each event, depending on the event type, you must cast the `event_args` to the appropriate event arg class to get the event properties. The following example demonstrates using `do_work()` to handle events:

```cpp
auto eventQueue = mpInstance.do_work();
for (auto& event : eventQueue)
{
    switch (event.event_type())
    {
      case multiplayer_event_type::member_joined:
      {
        auto memberJoinedArgs =  std::dynamic_pointer_cast<member_joined_event_args>(event.event_args());
        HandleMemberJoined(memberJoinedArgs);
        break;
      }
      case multiplayer_event_type::session_property_changed:
      {
        auto sessionPropertyChangedArgs =  std::dynamic_pointer_cast<session_property_changed_event_args>(event.event_args());
        HandlePropertiesChanged(sessionPropertyChangedArgs);
        break;
      }
      . . .
    }
}

```

## Scenarios

In this section we will go through some common scenarios, and the APIs you would call in each scenario.  Some information on what Multiplayer Manager is doing behind the scenes is also provided.

* [Play with friends](multiplayer-manager/play-multiplayer-with-friends.md)
* [Find a match](multiplayer-manager/play-multiplayer-with-matchmaking.md)
* [Send game invites](multiplayer-manager/send-game-invites.md)
* [Handle protocol activation](multiplayer-manager/handle-protocol-activation.md)

A high level overview of the API can be found at [Multiplayer Manager API overview](multiplayer-manager/multiplayer-manager-api-overview.md).

## What Multiplayer Manager does not do
While Multiplayer Manager makes it much easier to implement multiplayer scenarios and abstracts some  of the data from the developer, there are a few things it does not handle, or is not best suited for.

* Persistent online server games, such as MMOs, or other game types that require large sessions (over 100 players in a session).
* Server to server session management

>Multiplayer Manager is not tied to any specific network technology, and should work with any network layer.

## Next Steps

Please see either the C++ or WinRT *Multiplayer* sample for a working example of the API.

The API documentation can be found in the C++ or WinRT guides in the Microsoft::Xbox::Services::Multiplayer::Manager namespace.  You can also see the `multiplayer_manager.h` header.

If you have any questions, feedback, or run into any issues using the Multiplayer Manager, please contact your DAM or post a support thread on the forums at [https://forums.xboxlive.com](https://forums.xboxlive.com).
