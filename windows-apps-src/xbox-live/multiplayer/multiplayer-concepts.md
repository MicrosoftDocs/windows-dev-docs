---
title: Xbox Live multiplayer concepts
author: KevinAsgari
description: Learn about common multiplayer concepts used by Xbox Live multiplayer systems.
ms.assetid: 1e765f19-1530-4464-b5cf-b00259807fd3
ms.author: kevinasg
ms.date: 08/25/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer
ms.localizationpriority: low
---
# Xbox Live multiplayer concepts

This topic discusses a number of important multiplayer terms and concepts that are used frequently in the Xbox Live documentation. Having a good grasp of these concepts will help you understand how Xbox Live multiplayer works.

## Multiplayer session

A multiplayer session represents a group of Xbox Live users and properties associated with them. The session is created and maintained by titles, and is represented as a secure JSON document that resides in the Xbox Live cloud. The session document itself contains information about the Xbox Live users that are connected to the session, how many spots are available, custom metadata (for the session as well as for each session member), and other information related to the game session.

Each session is based on a session template, which are defined by the game developer, and are configured in the Xbox Live service configuration for a title instance.

While a title can create and update a session, it cannot directly delete a session.  Instead, once all players are removed from a session, the Xbox Live multiplayer service will automatically delete the session after a specified timeout. For detailed information about sessions, see [MPSD session details](multiplayer-appendix/mpsd-session-details.md).

Titles can choose to use multiple sessions, but a typical multiplayer implementation will use two sessions:

* Lobby session - this is a session that represents a group of friends that want to remain together across multiple rounds, levels, maps, etc., of the game.
* Game session - this is a session that represents the people that are playing in a specific session instance of a game, such as a round, match, level, etc. This session can include members from multiple lobby sessions that have joined the session instance together, typically through a matchmaking service.

Here is an example scenario:
Sally wants to play some multiplayer with her friends, John and Lisa. Sally starts up a game, and invites John and Lisa into her game. After they join, Sally, John, and Lisa are all in a lobby session. In this session, they decide to play in an online match with other people. The game creates a game session, and uses the Xbox Live matchmaking service to fill the remaining player slots with other Xbox Live players.

Let's say that Bob and Joe are matched up with them, and the five of them play the round together. After the round ends, Sally, John, and Lisa leave the game session, but are still together in the lobby session (without Bob and Joe), and can choose to play another round, or switch to different game mode.

### Session member

A session member is an Xbox Live user that is part of a session.

### Arbiter

An arbiter is a console or device that manages the state of the session for a game. For example, the arbiter would be responsible for advertising a game session to matchmaking in order to find more players.

The arbiter is set by the title. It may be the same as the host of the game, but does not have to be the same.

### Session host

The session host is the console or device that runs the game play simulation for titles built on a host-based peer-to-peer network architecture. This console or device is typically the same as the arbiter, but it does not have to be the same.

## Multiplayer service session directory

The Xbox Live Multiplayer service operates in the Xbox Live cloud, and centralizes a game's multiplayer system metadata across multiple clients. The system that tracks this metadata is known as the Multiplayer Session Directory, or MPSD for short. You can think of MPSD as a library of active game sessions. Your game can add, search, modify, or remove active sessions relating to your title. The MPSD also manages session state and updates sessions when necessary.

MPSD allows titles to share the basic information needed to connect a group of users. It ensures that session functionality is synchronized and consistent. It coordinates with the shell and Xbox One console operating system in sending/accepting invites and in being joined via the gamer card.

### Session handles

A session is uniquely identified in MPSD by a combination of pieces of data:

* The service configuration ID (SCID) of the title
* The name of the session template that was used to create the session
* The name of the session

A session handle is a JSON object that contains a reference to a specific session that exists in MPSD. Session handles enable Xbox Live members to join existing sessions.

Each session handle includes a guid that uniquely identifies the handle, which allows titles to reference the session by using a single guid.

There are several types of session handles:

* invite handle
* search handle
* activity handle
* correlation handle
* transfer handle

#### Invite handle

An invite handle is passed to a member when they are invited to join a game. The invite handle contains information that lets the invited member's game join the correct session.

#### Search handle

A search handle includes additional metadata about the session, and allows titles to search for sessions that meet the selected criteria.

#### Activity handle

An activity handle lets members see what other members on their social network are playing, and can be used join a friend's game.

#### Correlation handle

A correlation handle effectively works as an alias for a session, allowing a game to refer to a session by only using the id of the correlation handle.

### Transfer handle

A transfer handle is used to move players from one session to another session.

### Invites

Xbox Live provides an invite system that is supported by the Multiplayer service. It enables players to invite other players to their game sessions. Invited players receive a game invite and a title uses this information to join an existing session and multiplayer experience. Titles control invite flow and when invites can be sent.

Invites can be sent through the shell by the user or directly from the title. The notification text for an invite can be dynamically set by a title to provide more information to the invited player. Invites can also include additional data for the title that is not visible to the player and can be used to carry additional information.

### Join-in-progress

In addition to invites, Xbox Live also provides a shell option for players to join an active gameplay session of friends or other known players. This enables another path into an active game session and is also driven by the MPSD. Titles control when sessions are joinable and which session to expose for join-in-progress.

### Protocol activation

If Sally sends an invite to Lisa to join her game, Lisa receives a notification on her device that she can choose to accept or decline.

If she accepts the invitation, the OS attempts to launch the game, if the game is not already running, and triggers an activation event that contains information about why the game was activated, and any additional details (in the case of an invite, for example, the details include the ID of the player that invited, as well as the session that the member has been invited to.)

The process of handling this event is known as protocol activation, and indicates that the game should automatically go into a specific state, which is detailed in the activation event arguments. If the member is joining a multiplayer game, the session handle id is specified as one of the arguments.

In Lisa's case, accepting the invite should automatically start the game (if needed), and join her to the same game session as Sally, without Lisa needing to take any further actions.

Protocol activation can be triggered by accepting an invite, joining another member's gamer via their profile card, or clicking a deep linked achievement.

## SmartMatch matchmaking

SmartMatch is the name of the Xbox Live service for anonymous matchmaking. This service matches up players of the same game based on configurable a match rule set.

The matchmaking service works closely with the MPSD and uses sessions for matchmaking input and output. Matchmaking is performed on the service, which allows titles to easily do provide other experiences during the matchmaking flow, for example single-player within the title.

Individuals or groups that want to enter matchmaking create a match ticket session, then request the matchmaking service to find other players with whom to set up a match. This results in the creation of a temporary "match ticket" residing within the matchmaking service (on a match hopper) for a period of time.

The matchmaking service chooses sessions to play together based on rule configuration, statistics stored for each player, and any additional information given at the time of the match request. The service then creates a match target session that contains all players who have been matched, and notifies the users' titles of the match.

When the target session is ready, titles may perform quality of service (QoS) checks to confirm that the group can play together, and/or join players to the session to begin gameplay. During the QoS process and matchmade game play, titles keep the session state up to date within MPSD, and they receive notifications from MPSD about changes to the session. Such changes include users joining or leaving, and changes to the session arbiter.

### Match ticket session

A match ticket session represents the clients for the players who want to make a match. It is typically created based on a group of players who are in a lobby together, or on other title-specific groupings of players. In some cases, the ticket session might be a game session already in progress that is looking for more players.

### Match ticket

Submitting a ticket session to matchmaking results in the creation of a match ticket that tracks the matchmaking attempt. Attributes can be added to the ticket, for example, game map or player level. These, along with attributes of the players in the ticket session, are used to determine the match.

### Hoppers

Hoppers are logical places where match tickets are collected and specified at the start of matchmaking. Only tickets within the same hopper can be matched. A title can have multiple hoppers but can only start matchmaking on one at a time. For example, a title might create one hopper for which player skill is the most important item for matching. It might use another hopper in which players are only matched if they have purchased the same downloadable content.

You configure hoppers for matchmaking in the service configuration. TBD.

## Quality of service (QoS)

When gamers play a multiplayer game online, the quality of the game is affected by the quality of the network communication between the devices which are hosting the games. A poor network can result in undesirable game experiences like lag or connection drops due to insufficient bandwidth or latency.

QoS refers to measuring the strength of an online connection (bandwidth and latency) between players to ensure that all players have a sufficient network connection quality. This is specifically important for players that are matched during matchmaking to guarantee a good experience due to net. It is less applicable for invites where friends play together and are usually willing to accept the consequences of a poor connection.

You can configure the session to handle QoS automatically based on specific criteria, or your game can handle measuring the QoS whenever anyone joins the session.
