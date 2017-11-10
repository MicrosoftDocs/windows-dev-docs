---
title: Xbox Integrated Multiplayer overview
author: KevinAsgari
description: Learn about Xbox Integrated Multiplayer (XIM), an all-in-one multiplayer/networking/chat solution for Xbox Live games.
ms.assetid: edbb28e6-1b6c-4f12-a9c6-fa8961de99a8
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
localizationpriority: medium
---

# Xbox Integrated Multiplayer overview

 Xbox Integrated Multiplayer (XIM) is a self-contained interface for easily adding multiplayer real-time networking and chat communication to your game through the power of Xbox Live services. It's oriented around a few key concepts:

 - **XIM network** - A logical representation of a set of interconnected users participating in a particular multiplayer experience, as well as basic state describing that collection. Participants can be in only one XIM network at a time, but can move seamlessly from one conceptual XIM network to another.
 - `xim_player` - An object representing an individual human user signed in on a local or remote device and participating in a XIM network. A single physical user that joins, leaves, and then rejoins the same XIM network is considered to be two separate player instances.
 - `xim_authority` - An object representing a single entity with the permission for and responsibility of managing a consistent view of game-specific state within a XIM network for all participants. The use of this object is optional. Its functionality is also currently not available in this software release.
 - `xim_state_change` - A structure representing a notification to the local device regarding an asynchronous change in some aspect of the XIM network.
 - `xim::start/finish_processing_state_changes` - The pair of methods called by the app every UI frame to perform asynchronous operations, to retrieve results to be handled in the form of `xim_state_change` structures, and then to free the associated resources when finished.
 - **Matchmaking** - The optional process of discovering additional remote players with similar interests or skill levels to participate in a XIM network without requiring an existing social relationship.

At a very high level, the app uses the library to configure a set of users on the local device to be moved into a XIM network as new players. As app instances on remote devices do the same thing with their own users, and each continually start + finish processing state changes every UI frame, every participating instance is provided `xim_state_change` updates describing the local and remote `xim_player`s joining that XIM network.

Within the XIM network, the app can send its own game-specific data messages, such as avatar movement updates. These may target other players, or a `xim_authority` automatically selected to reside on one of the participating devices (based on the best network quality, stability, player reputation and other factors) so the app on that `xim_authority` device can send messages back out to players, typically merging them for network efficiency and to arbitrate conflicts. All received messages are delivered to the app as a `xim_state_change` indicating the intended source and local destination(s).

Voice and text chat communication is also automatically provided among all players where privacy settings and app configuration allow it. For players that have enabled speech-to-text or text-to-speech conversion, XIM will transparently perform this translation to deliver chat text messages representing incoming speech audio and play synthesized speech audio for outgoing chat text messages, respectively.

When a player stops participating in the XIM network (gracefully or due to network connectivity problems), `xim_state_change` updates are provided to all app instances indicating the `xim_player` has left. If the device selected to be the `xim_authority` departs, the app instances are similarly informed that the authority has begun moving and to start "reconciling" any game-specific state by optionally providing a data buffer to the replacement `xim_authority` for resynchronization purposes. The new authority can interpret the reconciliation data from all players as desired and in turn provide a "reconciled" data buffer back to all players to complete the authority move and resynchronization process. The same basic reconciliation procedure is also offered to newly participating devices so that the authority can conveniently provide them the current authoritative game state when their players join the XIM network, with no extra code. Note that authority state changes are not currently provided in this software release.

An app can determine the XIM network in which to participate through several means. Often the app starts up by automatically moving the local users to a new network available to the users' friends, where the local users can send invitations or have their XIM network discovered as a joinable activity (via player cards, for example). Once these socially discovered users are ready, the app can initiate the Xbox Live "matchmaking" process and move all the players to a new XIM network that also contains additional "matched" remote players to fill out team/opponent lists as desired. Then, when that multiplayer experience is completed, app instances can move their local players-- and optionally the original pre-matchmaking remote players as well-- back into a new private XIM network, or into another random XIM network found through matchmaking. Voice and text chat remain available throughout. This ease of moving players from XIM network to XIM network is central to the API and reflects modern expectations of good, highly social gaming experiences.

To get started see [Using XIM](xbox-integrated-multiplayer/using-xim.md).

## XIM's relationship to other modules

XIM is designed to provide a convenient, all-in-one interface for games with basic multiplayer needs. It encapsulates the functionality of several modules-- notably the Xbox Services API (XSAPI) `multiplayer_manager` module, the `Microsoft::Xbox::GameChat` library, and `Windows::Networking::XboxLive` secure multiplayer networking-- as a single streamlined API. This reduces the typical amount of code, tasks, and concepts involved when building multiplayer games that don't require the absolute maximum flexibility or control. Apps whose requirements don't align with XIM's simplifying assumptions, however, may wish to use those components directly instead.

While XIM is intended to remove the need to manage things like Multiplayer Session Directory (MPSD) session documents or a network transport through the underlying components, it does not preclude their simultaneous/side-by-side use as part of a separate player roster or communication mesh. In this case it's the app's responsibility to ensure cooperative network resource usage between XIM and its own mechanisms. XIM currently supports "out of band reservations" to ease its use as a dedicated chat solution whose user list is driven solely by external input.

Xbox Live provides many other features that are valuable to multiplayer games but are less directly involved in setting up multiplayer chat and network communication, and are therefore not wrapped by this module. Apps are encouraged to look to the Xbox Services API (XSAPI) for player feedback, achievements, leaderboards, storage, and much more.


## Using XIM as a dedicated chat solution via out-of-band reservations

Most apps use XIM to handle every aspect of getting players together. After all, a focus on assembling all the features needed to support common multiplayer scenarios end-to-end is the reason it's called "Xbox Integrated Multiplayer". However some apps that implement their own networking solutions using dedicated Internet servers would also like the advantages of establishing reliable, low latency, low cost peer-to-peer chat communication. XIM recognizes this need, and currently supports an extension mode to take advantage of its simplified peer communication to augment external player management happening outside the XIM API.

> For detailed documentation on using XIM via out-of-band reservations see [XIM Reservations](xbox-integrated-multiplayer/xim-reservations.md).
