---
title: Introduction to the Multiplayer system
author: KevinAsgari
description: Provides a high level introduction to the Xbox Live Multiplayer 2015 system.
ms.assetid: d025bd2b-2ca4-4ba9-9394-4950d96ad264
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer 2015
ms.localizationpriority: low
---

# Introduction to the Multiplayer system

| Note                                                                                                                                                                                                          |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| This article is for advanced API usage.  As a starting point, please take a look at the [Multiplayer Manager API](../multiplayer-manager.md) which significantly simplifies development.  Please let your DAM know if you find an unsupported scenario in the Multiplayer Manager. |

This document contains the following sections
* About the Multiplayer System
* Multiplayer Components, Interfaces, and Architectures
* Parties Supported by 2015 Multiplayer
* Multiplayer Terminology
* What's New in 2015 Multiplayer
* Differences Between Xbox 360 and Xbox One MPSD Session Functions

## About the Multiplayer System


2015 Multiplayer optimizes the direct use of MPSD and game sessions. It provides better support for multiple concurrent sessions for a single title or user, and updates the Xbox Services API (XSAPI) to enable titles to:

-   Advertise a user's current activity and availability for joins.
-   Send invites to sessions, along with user-visible (title-specified) context strings.
-   Discover and join sessions via title code.
-   Maintain web socket connections to MPSD so that they can receive brief notifications (shoulder taps) on session changes, for example, updates that reflect subscriptions to change events and connection state changes. MPSD also uses web socket connections to rapidly detect and act upon client disconnection.
-   Use SmartMatch matchmaking.

## Multiplayer Components, Interfaces, and Architectures

### Components of 2015 Multiplayer

Multiplayer is a system that consists of multiple components. It is flexible enough to allow other components, such as dedicated servers and external matchmaking systems.
#### Multiplayer Session Directory (MPSD)


Multiplayer session directory (MPSD) is a service that holds a collection of sessions. A session is defined as a secure document residing in the cloud and representing a group of people playing a game. For MPSD details, see [Multiplayer Session Directory (MPSD)](multiplayer-session-directory.md).


#### Multiplayer APIs

2015 Multiplayer provides a Multiplayer WinRT API, implemented through the **Microsoft.Xbox.Services.Multiplayer Namespace**. Its components include the **MultiplayerService Class**, defining a web service that wraps MPSD.

Also included is a Multiplayer REST API. It defines URIs and JSON objects that are called by the WinRT API methods. The REST functionality can be used in direct calls from your titles, but you are encouraged to access MPSD indirectly via the WinRT API. For more information, see **Calling MPSD**.

#### Xbox Party System

In 2015 Multiplayer, the Xbox party system supports only chat parties as external entities. Titles can interact with the party system to discover the membership of chat parties. For more information, see **Parties Supported by 2015 Multiplayer**.

The party system now supports gaming directly through the MPSD session, instead of using a game party. It is up to the title to use sessions to enable such operations as member interaction, pulling of players into a game as space becomes available, and user engagement during wait periods.


### 2015 Multiplayer Interfaces

2015 Multiplayer uses several interfaces to other Xbox components.
#### Xbox Secure Sockets

2015 Multiplayer supports low-level network communication among devices using Xbox secure sockets and Winsock. The networking functionality uses Internet Protocol Security (IPSec) to allow titles to provide secure device association. See **Networking Overviews** for details of the Xbox secure sockets feature.


#### Xbox Live Real-time Activity Service

2015 Multiplayer uses the **real-time activity service** to allow titles to subscribe to MPSD session changes, and enable automatic detection of client disconnects. More information is provided in [Real-Time Activity (RTA) Service](../../real-time-activity-service/real-time-activity-service.md).


#### Xbox Live Matchmaking Service

The **matchmaking service** forms groups from players, depending on their preferences and data, and information supplied during SmartMatch matchmaking. For details of Multiplayer use of this service, see [SmartMatch Matchmaking](smartmatch-matchmaking.md).


#### Xbox Live Reputation Service

The *reputation service* manages user statistics about reputation and during reputation-filtered matchmaking. It is used by 2015 Multiplayer through SmartMatch matchmaking. For more information, see [Reputation](../../social-platform/people-system/reputation.md).


#### Xbox Live Compute

Xbox Live Compute provides cloud processing power for titles using 2015 Multiplayer. For more information on using Xbox Live Compute, see [Using Xbox Live Compute in Multiplayer](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/xbox-live-compute/using-xbox-live-compute-in-multiplayer).


### Typical 2015 Multiplayer Architectures

This section presents the most typical 2015 Multiplayer architectures.
#### Peer-to-peer Architecture

In peer-to-peer architecture, the title uses MPSD and SmartMatch matchmaking to discover peer addresses. The addresses are then used to connect peers using Xbox secure sockets. For more information, see *Introduction to Winsock on Xbox One*.

![Diagram of peer to peer architecture](../../images/multiplayer/Mult2015ArchPeer.png)


#### Client-server Architecture

In the client-server multiplayer architecture, the title uses MPSD and SmartMatch matchmaking to discover dedicated server addresses. These are then used to connect to the dedicated server using Xbox secure sockets. For more information, see *Introduction to Winsock on Xbox One*.

| Note                                                                         |
|-------------------------------------------------------------------------------------------|
| Xbox Live Compute instances can be used as the servers in the client-server architecture. |

![Diagram of client server architecture](../../images/multiplayer/Mult2015ArchClientServer.png)


## Parties Supported by 2015 Multiplayer
2015 Multiplayer for Xbox One does not expose the "game party" as a system-level construct. However, it does support the "chat party" at a system level, as in 2014 Multiplayer.

| Note                                                                                                                    |
|--------------------------------------------------------------------------------------------------------------------------------------|
| Your titles can still implement user experiences similar to those implemented using the game party, but using MPSD sessions instead. |


### Chat Party

A chat party is a group of people who are chatting with each other, managed by the user through the Xbox One Party App. Users can be in a chat party while playing in a game session or while performing another console activity. However, there are no ties between the users in the chat party and other activities for those users.

The chat party is exposed using the *PartyChat Class*, which allows the title to examine the state of the chat party. For example, a title can enumerate the members of the chat party using the *PartyChat.GetPartyChatViewAsync Method*.


### Implementing Features Similar to Those Related to the Game Party

In 2014 Multiplayer, the game party served several purposes. It allowed a title to:

-   Advertise a user's current activity and availability for joins
-   Send invitations (invites) to sessions
-   Discover and join sessions
-   Receive notifications on certain changes to the sessions registered to the party
-   Use SmartMatch matchmaking
-   Keep a group of people together across multiple game sessions

2015 Multiplayer supports all the above features directly through MPSD sessions.

## Multiplayer Terminology


| Term                                 | Description|
| --- | --- |
| Active Player                        | A player who has been set to the Active state within the session. The title sets a player to this state when the player is taking part in a game. For more information, see [Session User States](mpsd-session-details.md).                                                                                                                                                                                                                                                                                          |
| Arbiter                              | The single console in a game session that manages the state of the multiplayer session directory (MPSD) session for a game, for example, in advertising the game session to matchmaking, to find more players. The arbiter is set by the title. The arbiter is not always the host of the game. See [Session Arbiter](mpsd-session-details.md).                                                                                                                                                                            |
| Arranged Game                        | A type of game that is created only through one player inviting other players to join, without any involvement from matchmaking.                                                                                                                                                                                                                                                                                                                                                                                                    |
| Chat Party                           | A group of people who are chatting together. The people might be engaged in the same activity or they might be engaged in different activities, such as games, music, or apps. See **Parties Supported by 2015 Multiplayer**.                                                                                                                                                                                                                                                                                 |
| Game Invite                          | An invitation to join a game session.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| Game Session                         | A session in which users are actually playing together. All multiplayer scenarios, for example, matchmaking or joining a lobby, ultimately end up in a game session. The session is often advertised as the users' current activities to enable joins. It is also used to build the recent players list. See [MPSD Session Details](mpsd-session-details.md).                                                                                                                                                           |
| Game Session Host                    | The console running the game play simulation for titles built on a host-based peer-to-peer network architecture. This console is typically the same as the arbiter, but it does not have to be the same.                                                                                                                                                                                                                                                                                                                            |
| Handle (or Session Handle)           | A reference to an MPSD session that has additional state and behavior associated with it. See [MPSD Handles to Sessions](multiplayer-session-directory.md).                                                                                                                                                                                                                                                                                                                                                                    |
| Inactive Player                      | A player who has been set to the Inactive state within the session. The title sets a player to this state when a game is suspended or is otherwise inactive, as defined by the title. In some instances, MPSD might also set a player as inactive, but it is primarily the responsibility of the title to do so. For more information, see [Session User States](mpsd-session-details.md).                                                                                                                           |
| Hopper                               | A Hopper is a logic-driven collection of match tickets. A title can have multiple hoppers, but only tickets within the same hopper can be matched. For example, a title might create one hopper for which player skill is the most important item for matching. It might use another hopper in which players are only matched if they have purchased the same downloadable content. For more information on where hoppers fit into the SmartMatch workflow, see [SmartMatch Runtime Operations](smartmatch-matchmaking.md) |
| Join in Progress                     | The concept of joining another player's game after game play has begun. Players can join a friend's game through the friend's gamer card. The title can then move those players into the game session at the appropriate time.                                                                                                                                                                                                                                                                                                      |
| Lobby Session                        | A helper session for invited players who are waiting to join a game session. See [MPSD Session Details](mpsd-session-details.md).                                                                                                                                                                                                                                                                                                                                                                                       |
| Match Target Session                 | A match session set up during SmartMatch matchmaking to represent the match. See [SmartMatch Matchmaking](smartmatch-matchmaking.md).                                                                                                                                                                                                                                                                                                                                                                                              |
| Match Ticket Session                 | A preliminary match session set up during SmartMatch matchmaking. See [SmartMatch Matchmaking](smartmatch-matchmaking.md).                                                                                                                                                                                                                                                                                                                                                                                                         |
| MPSD Session                         | A secure document that resides in the multiplayer session directory (MPSD) within the Xbox Live cloud. It contains a group of users who might be connected while running a title on Xbox One, along with metadata about the users and their game. See [MPSD Session Details](mpsd-session-details.md).                                                                                                                                                                                                                  |
| Multiplayer Session Directory (MPSD) | The service operating in the cloud that the multiplayer system uses to store and retrieve sessions. See [Multiplayer Session Directory (MPSD)](multiplayer-session-directory.md).                                                                                                                                                                                                                                                                                                                                                                |
| Party App                            | An Xbox One system snap app that allows users to view and manage their parties.                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| Server Session                       | A game session created by Xbox Live Compute processing. See [MPSD Session Details](mpsd-session-details.md).                                                                                                                                                                                                                                                                                                                                                                                                            |
| Shoulder Tap                         | A notification from MPSD to a title that a potentially interesting change has occurred on the service. The shoulder tap is a quick reminder, often less informational than a regular notification. See [MPSD Change Notification Handling and Disconnect Detection](multiplayer-session-directory.md).                                                                                                                                                                                                                     |
| SmartMatch Matchmaking               | An Xbox Live matchmaking capability available to Xbox One titles, implemented by the matchmaking service. Using MPSD and matchmaking, the title makes a request to be matched and is notified later that a matched group has been found. See [SmartMatch Matchmaking](smartmatch-matchmaking.md).                                                                                                                                                                                                                                  |

## What's New in 2015 Multiplayer

| Caution                                                                                                                                                                                                                                               |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| When using 2015 Multiplayer, it is important to note that the party-related classes in 2014 Multiplayer should not be used any longer. Mixing 2015 Multiplayer functionality with the party-related classes causes incoherent behavior and should never be attempted. |


### New Concepts in 2015 Multiplayer


#### Web Socket Connections to MPSD

MPSD now enables titles to maintain web socket connections with it. These connections allow clients to receive notifications when sessions change. For more information, see [MPSD Change Notification Handling and Disconnect Detection](multiplayer-session-directory.md).


#### MPSD Session Handles

2015 Multiplayer adds support for MPSD session handles, which are references to sessions that can include typed data. For more information, see [MPSD Handles to Sessions](multiplayer-session-directory.md).


### Summary of New 2015 Multiplayer WinRT API Functionality

New multiplayer WinRT API functionality is based on the existing XSAPI, to help ease the transition for titles already using the 2014 Multiplayer in combination with the game party API.

2015 Multiplayer adds the *MultiplayerActivityDetails Class*, which represents details of a user's current activity, for example, the session in which the user is joinable.

New functionality has been added to the *MultiplayerService Class*. Examples are methods and properties dealing with activities for users and social groups, retrieval of sessions using various filters and handles, sending of game invites, and session reads and writes using handles.

The *MultiplayerSession Class* adds functionality to work with session change types, subscriptions for notifications, session comparison, and setting a session as closed.

The *MultiplayerSessionReference Class* is changed to inherit from **IMultiplayerSessionReference**, to support cross-namespace calls. The class also has new URI path parsing methods.

| Note                                                                                                                                                                                                                                                                                                                                                                                   |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| To support events and subscriptions to notifications, 2015 Multiplayer adds functionality to the *Microsoft.Xbox.Services.RealTimeActivity Namespace*. Also included in the new functionality is *SystemUI.ShowSendGameInvitesAsync Method*, used to show the game invite UI for 2015 Multiplayer. |

## Differences Between Xbox 360 and Xbox One MPSD Session Functions

| Function | Xbox 360 | Xbox One |
|---|---|---|
| **Get game session information** | XSessionGetDetails, XSessionSearchByID, or title does tracking. | Title requests session information from MPSD. |
|**Migrate the host** | When needed, title calls XSessionMigrateHost. | Depending on the cause of the migration, title might be able to assign a new host for the session, or might create a new MPSD session. |
| **Multiple player sessions** | Tricky to handle more than one session at a time, for example, XNetReplaceKey versus XNetUnregisterKey. | Service-based session makes management of one session cleaner, and simplifies handling of multiple sessions. |
| **Signouts and disconnects** | Title has to handle disconnects and signout differently with XCloseHandle or XSessionDelete. | MPSD simplifies signouts and disconnect handling, and timeouts set in game configuration. |
| **Matchmaking** | Client-based matchmaking queries | Service-based matchmaking that allows better match quality and easier background matchmaking within the title. |


### Sessions

On the Xbox 360, a session represented an instance of game play. Users searched for sessions in the matchmaking service, and reported statistics at the end of a session.

On Xbox One, a session is more generic, and represents a group of players. A session is required for any network connectivity between consoles, and holds information that should be shared among all users in the session. Some examples of this information include the number of players allowed in the session, the secure address of each console in the session, and custom game data.


### Xbox Matchmaking

On the Xbox 360, titles performed matchmaking by configuring a schema of attributes, and a set of queries to search through those attributes. At run time, the title chose to either host a session or search for one.

On Xbox One, matchmaking is server-based, and players and titles no longer decide whether to host or search. Instead, each pre-formed group of players creates a "ticket" session and submits that session to the matchmaking service. The service then finds other sessions and combines the groups to form a new "target" session. The clients are notified of the match and perform quality of service (QoS) to validate connectivity with other session members before starting game play.


### Xbox Live Compute Service

The Xbox Live Compute service is a new offering on Xbox One. It enables developers to harness the elastic compute power of the cloud, and enables larger multiplayer scenarios than were possible in a peer-to-peer network. For more information about the Xbox Live Compute service, see the Xbox Live Compute documentation in the XDK docs.


## See also

[Multiplayer Session Directory (MPSD)](multiplayer-session-directory.md)

[SmartMatch Matchmaking](smartmatch-matchmaking.md)

[Real-Time Activity (RTA) Service](../../real-time-activity-service/real-time-activity-service.md)

[Reputation](../../social-platform/people-system/reputation.md)

[Using Xbox Live Compute in Multiplayer (requires managed partner access)](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/xbox-live-compute/using-xbox-live-compute-in-multiplayer)
