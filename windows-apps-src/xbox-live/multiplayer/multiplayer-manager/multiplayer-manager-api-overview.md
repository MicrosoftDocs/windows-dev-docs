---
title: Multiplayer manager API overview
author: KevinAsgari
description: Learn about the Xbox Live Multiplayer Manager API.
ms.assetid: 658babf5-d43e-4f5d-a5c5-18c08fe69a66
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, multiplayer manager
ms.localizationpriority: low
---

# Multiplayer Manager API overview

This page describes a broad overview of the Multiplayer Manager API, and how they are used in a game. It calls out the most important classes and methods in the API. For detailed API information, see the reference documentation. For examples of how to use these APIs in an application, see the Multiplayer Sample.

## Namespace
The Multiplayer Manager classes are included the following namespace:

| language | namespace |
| --- | --- |
| C++/CX | Microsoft::Xbox::Services::Multiplayer::Manager |
| C++ | xbox::services::multiplayer::manager |

There following lists describes the major classes that you should understand:

* [Multiplayer Manager class](#multiplayer-manager-class)
* [Multiplayer Event class](#multiplayer-event-class)
* [Multiplayer Member class](#multiplayer-member-class)
* [Multiplayer Lobby Session class](#multiplayer-lobby-session-class)
* [Multiplayer Game Session class](#multiplayer-game-session-class)

## Multiplayer Manager class <a name="multiplayer-manager-class">

| language | class |
| --- | --- |
| C++/CX | MultiplayerManager |
| C++ | multiplayer_manager |

This class is the primary class for interacting with Multiplayer Manager. It is a singleton class, so you only need to create and initialize the class once.
This class contains a single lobby session object and a single game session object.

At a minimum, you must call the `initialize()` and `do_work()` methods on this class in order for Multiplayer Manager to function.

The following tables describes some, but not all, of the more commonly used methods and properties for this class. For a complete descriptive list of class members, see the reference.

| C++/CX | C++ | description |
| --- | --- | --- |
| **Methods** | | |
| Initialize() | initialize() | Initializes Multiplayer Manager. You must call this method before using Multiplayer Manager. |
| DoWork() | do_work() | Updates the app visible session states. You should call this method at least once per frame, and your game should handle the multiplayer events that are returned by the method. |
| JoinLobby() | join_lobby() | Provides you a way to joins a friend's lobby session either through a handleId that uniquely identifies the lobby you want to join or when the user accepts an invite that causes the title to get protocol activated. |
| JoinGameFromLobby() | join_game_from_lobby() | Join the lobby's game session if one exists and if there is room. If the session doesn't exist, it creates a new game session with the existing lobby members. This does not migrate existing lobby session properties over to the game session. After joining, you can set the properties or the host via `game_session()::set_synchronized_*` APIs. The title is required to call this API on all clients that want to join the game session.|
| JoinGame() | join_game() | Joins an existing game session given a globally unique session name, typically found through third party matchmaking services. You can pass in the list of xbox user IDs you want to be part of the game.|
| FindMatch() | find_match() | Use Xbox Live matchmaking to find and join a game. |
| LeaveGame() | leave_game() | Leaves a game and returns the member and all local members to the lobby. |
| **Properties** | | |
| LobbySession | lobby_session() | A handle to an object that represents the lobby session. |
| GameSession |  game_session() | A handle to an object that represents the game session. |

## Multiplayer Event class <a name="multiplayer-event-class">

| language | class |
| --- | --- |
| C++/CX | MultiplayerEvent |
| C++ | multiplayer_event |

When you call `do_work()`, Multiplayer Manager returns a list of events that represent the changes to the sessions since you last called `do_work()`. These events include changes such as a member has joined a session, a member has left a session, member properties have changed, the host client has changed, etc.

For a list of all possible event types, see the `multiplayer_event_type` enumeration.

Each returned `multiplayer_event` includes an `event_args`, which you must cast to the appropriate event_args class for the event type. For example, if the `event_type` is `member_joined`, then you would cast the `event_args` reference to a `member_joined_event_args` class.

Your game should handle each of the events as necessary after calling `do_work()`.

## Multiplayer Member class <a name="multiplayer-member-class">

| language | class |
| --- | --- |
| C++/CX | MultiplayerMember |
| C++ | multiplayer_member |

This class represents a player in a lobby or game session. The class contains properties about the member, including the XboxUserID for the player, the network connection address for the player, custom properties for each player, and more.

## Multiplayer Lobby Session class <a name="multiplayer-lobby-session-class">

| language | class |
| --- | --- |
| C++/CX | MultiplayerLobbySession |
| C++ | multiplayer_lobby_session |

A persistent session used for managing users that are local to this device and your invited friends that wish to play together. A lobby session must contain at least one member in order for Multiplayer Manager to take any multiplayer actions. You can initially create a new lobby session by calling the `add_local_user()` method.

The following tables describes some, but not all, of the more commonly used methods and properties for this class. For a complete descriptive list of class members, see the reference.

| C++/CX | C++ | description |
| --- | --- | --- |
| **Methods** | | |
| AddLocalUser() | add_local_user() | Adds a local user (a player who has signed in on the local device) to the lobby session. If this is the first member added to a lobby session, then it creates a new lobby session. |
| RemoveLocalUser() | remove_local_user() | Removes a specified member from the lobby and game session. |
| InviteFriends() | invite_friends() | Opens a standard Xbox Live UI that allows the player to select people from their friends list, and then invites those players to the game. |
| InviteUsers() | invite_users() | Invites the sepcified Xbox Live users to the game. |
| SetLocalMemberConnectionAddress() | set_local_member_connection_address() | Sets the network address for the local member. Games can use this network address to establish network communications between members. |
| SetLocalMemberProperties() | set_local_member_properties() | Sets a custom property for a local member. The property is stored in a JSON string. |
| DeleteLocalMemberProperties() | delete_local_member_properties() | Removes a custom property for a local member. |
| SetProperties() / SetSynchronizedProperties() | set_properties() / set_synchronized_properties() | Sets a custom property for the lobby session. The property is stored in a JSON string. If the property is shared between devices, and may be updated by several devices at the same time, use the synchronized version of the method. |
| IsHost() | is_host() | Indicates if the current device is acting as the lobby host. |
| SetSynchronizedHost() | set_synchronized_host() | Sets the host of the lobby. |
| **Properties** | | |
| LocalMembers | local_members() | The collection of members that are signed in on the local device. |
| Members | members() | The collection of members that are in the lobby session. |
| Properties | properties() | A JSON object that represents a collection of properties for the lobby session. |
| Host | host() | The host member for the lobby. |


## Multiplayer Game Session class <a name="multiplayer-game-session-class">

| language | class |
| --- | --- |
| C++/CX | MultiplayerGameSession |
| C++ | multiplayer_game_session |

The game session represents the group of Xbox Live members that are participating in an instance of actual game play. This can include players that have been matched up via matchmaking services.

To start a new game session that includes members from the `lobby_session`, you can call `multiplayer_manager::join_game_from_lobby()`. If you want to use Xbox Live matchmaking, you can call `multiplayer_manager::find_match()`. If you're using a third party matchmaking service, you can call `multiplayer_manager::join_game()`.

The following tables describes some, but not all, of the more commonly used methods and properties for this class. For a complete descriptive list of class members, see the reference.

| C++/CX | C++ | description |
| --- | --- | --- |
| **Methods** | | |
| SetProperties() / SetSynchronizedProperties() | set_properties() / set_synchronized_properties() | Sets a custom property for the game session. The property is stored in a JSON string. If the property is shared between devices, and may be updated by several devices at the same time, use the synchronized version of the method. |
| IsHost() | is_host() | Indicates if the current device is acting as the game host. |
| SetSynchronizedHost() | set_synchronized_host() | Sets the host of the game. |
| **Properties** | | |
| Members | members() | The collection of members that are in the game session. |
| Properties | properties() | A JSON object that represents a collection of properties for the game session. |
| Host | host() | The host member for the game. |
