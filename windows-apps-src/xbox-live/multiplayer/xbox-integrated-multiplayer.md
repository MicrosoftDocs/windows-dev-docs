---
title: Xbox Integrated Multiplayer
author: KevinAsgari
description: Learn about Xbox Integrated Multiplayer (XIM), an all-in-one multiplayer/networking/chat solution for Xbox Live games.
ms.assetid: edbb28e6-1b6c-4f12-a9c6-fa8961de99a8
ms.author: kevinasg
ms.date: 01/25/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, xbox integrated multiplayer
localizationpriority: medium
---
# Xbox Integrated Multiplayer (XIM)

- [Overview](#overview)
- [Concepts](#concepts)
- [Features](#features)
- [Relationship to other modules](#relationship-to-other-modules)

## Overview

Xbox Integrated Multiplayer (XIM) is a self-contained interface for easily adding multiplayer real-time networking and chat communication to your game through the power of Xbox Live services. The XIM interface does not require a project to choose between compiling with C++/CX versus traditional C++; it can be used with either. The implementation also doesn't throw exceptions as a means of non-fatal error reporting so you can consume it easily from exception-free projects if preferred.

To get started see [Using XIM](xbox-integrated-multiplayer/using-xim.md). If you are using C#, see [Using XIM C#](xbox-integrated-multiplayer/using-xim-cs.md).

## Concepts

XIM is oriented around a few key concepts:

- XIM network - A logical representation of a set of interconnected users participating in a particular multiplayer experience, as well as basic state describing that collection. Participants can be in only one XIM network at a time, but can move seamlessly from one conceptual XIM network to another.
- Matchmaking - The optional process of discovering additional remote players with similar interests or skill levels to participate in a XIM network without requiring an existing social relationship.
- Querying - The optional process of discovering XIM networks without requiring an existing social relationship between participants.
- `xim_player` - An object representing an individual human user signed in on a local or remote device and participating in a XIM network. A single physical user that joins, leaves, and then rejoins the same XIM network is considered to be two separate player instances.
- `xim_state_change` - A structure representing a notification to the local device regarding an asynchronous change in some aspect of the XIM network.
- `xim::start_processing_state_changes` and `xim::finish_processing_state_changes` - The pair of methods called by the app every UI frame to perform asynchronous operations, to retrieve results to be handled in the form of `xim_state_change` structures, and then to free the associated resources when finished.

At a very high level, the game application uses the XIM library to configure a set of users signed-in on the local device to be moved into a XIM network as new players. The app calls `xim::start_processing_state_changes` and `xim::finish_processing_state_changes` every UI frame. As app instances on remote devices add their users into a XIM network, every participating instance is provided `xim_state_change` updates describing the local and remote `xim_player`s joining that XIM network. When a player stops participating in the XIM network (gracefully or due to network connectivity problems), `xim_state_change` updates are provided to all app instances indicating the `xim_player` has left.

An app can determine the XIM network in which to participate through several means. Often the app starts up by automatically moving the local users to a new network available to the users' friends, where the local users can send invitations or have their XIM network discovered as a joinable activity (via player cards, for example). Once these socially discovered users are ready, the app can initiate the Xbox Live "matchmaking" process and move all the players to a new XIM network that also contains additional "matched" remote players to fill out team/opponent lists as desired. Then, when that multiplayer experience is completed, app instances can move their local players-- and optionally the original pre-matchmaking remote players as well-- back into a new private XIM network, or into another random XIM network found through matchmaking. Voice and text chat remain available throughout. This ease of moving players from XIM network to XIM network is central to the API and reflects modern expectations of good, highly social gaming experiences.

As opposed to a client-server model, a XIM network is logically a fully-connected mesh of peer devices. As described in the section of this document, any player can send directly to any other through the API. All methods that affect the state of the XIM network as a whole can be invoked by any participating device.

XIM uses simple last-write-wins conflict resolution if the app doesn't otherwise prevent more than one participant modifying the same XIM network state at effectively the same time. This means that XIM doesn't impose any role concepts for "host" or "server". XIM also doesn't constrain apps from using their own concepts, such as support for migrating app-defined roles to another participant when a player leaves a XIM network.

## Features

- Provides game with voice and text chat communication that observes and respects player privacy settings

    Voice and text chat communication is also automatically provided among all players where privacy settings and app configuration allow it. For players that have enabled speech-to-text or text-to-speech conversion, XIM will transparently perform this translation to deliver chat text messages representing incoming speech audio and play synthesized speech audio for outgoing chat text messages, respectively.

- Allows game to send its own game-specific data messages

    Within the XIM network, the app can send its own game-specific data messages, such as avatar movement updates. All received messages are delivered to the app as a `xim_state_change` indicating the intended source and local destination(s).

- Functions as a dedicated chat solution via out-of-band reservations

    For detailed documentation on using XIM via out-of-band reservations see [XIM Reservations](xbox-integrated-multiplayer/xim-reservations.md).

- Exception-free and can be used with either C++/CX or traditional C++

## Relationship to other modules

XIM is designed to provide a convenient, all-in-one interface for games with basic multiplayer needs. It encapsulates the functionality of several modules-- notably the Xbox Services API (XSAPI) `multiplayer_manager` module, the `xbox::services::game_chat_2` library, and `Windows::Networking::XboxLive` secure multiplayer networking-- as a single streamlined API. This reduces the typical amount of code, tasks, and concepts involved when building multiplayer games that don't require the absolute maximum flexibility or control. Apps whose requirements don't align with XIM's simplifying assumptions, however, may wish to use those components directly instead.

While XIM is intended to remove the need to manage things like Multiplayer Session Directory (MPSD) session documents or a network transport through the underlying components, it does not preclude their simultaneous/side-by-side use as part of a separate player roster or communication mesh. In this case it's the app's responsibility to ensure cooperative network resource usage between XIM and its own mechanisms. XIM currently supports "out of band reservations" to ease its use as a dedicated chat solution whose user list is driven solely by external input.

Xbox Live provides many other features that are valuable to multiplayer games but are less directly involved in setting up multiplayer chat and network communication, and are therefore not wrapped by this module. Apps are encouraged to look to the Xbox Services API (XSAPI) for player achievements, leaderboards, storage, and much more.
