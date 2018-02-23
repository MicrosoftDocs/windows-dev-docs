---
title: Multiplayer appendix
author: KevinAsgari
description: Provides links to learn more about the Xbox Live multiplayer 2015 service.
ms.assetid: 412ae5f4-6975-4a8b-9cc2-9747e093ec4d
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Multiplayer appendix

The multiplayer system in Xbox One enables game play and the assembly of game players into groups. The system is secure, easy to use, and flexible, allowing you not only to build simple features quickly, but also to build more complex features and plug in your own services.

> **Note**  
This article is for advanced API usage.  As a starting point, please take a look at the [Multiplayer Manager API](../multiplayer-manager.md) which significantly simplifies development.  Please let your DAM know if you find an unsupported scenario in the Multiplayer Manager.

The current version of the multiplayer system is 2015 Multiplayer. This version abstracts the "game party" concept and uses the multiplayer session directory (MPSD) to control game sessions.

> **Note**  
The previous version of the multiplayer system is 2014 Multiplayer. This version is based on the concept of the game party and participation in games through parties. This version is now deprecated, although source code for it is still provided with the XDK. The 2014 Multiplayer documentation is no longer included with the XDK. If you need this documentation, please use a 2014 release of the XDK.


## In this section

[Introduction](introduction-to-the-multiplayer-system.md)  
Introduces the multiplayer system.

[Multiplayer Session Directory (mpsd)](multiplayer-session-directory.md)  
Describes multiplayer session directory (MPSD), which centralizes a game's multiplayer API metadata across all the titles and manages game sessions.

[MPSD Session Details](mpsd-session-details.md)  
Provides details of an MPSD session.

[Common Issues When Adapting Your Titles for 2015 Multiplayer](common-issues-when-adapting-multiplayer.md)  
Describes common issues to consider when you adapt titles to operate with 2015 Multiplayer.

[SmartMatch Matchmaking](smartmatch-matchmaking.md)  
Describes the matchmaking system used by Xbox Live.

[Migrating an Arbiter](migrating-an-arbiter.md)  
Describes the MPSD process for arbiter migration.

[Using SmartMatch Matchmaking](using-smartmatch-matchmaking.md)  
Describes how to use SmartMatch matchmaking.

[Multiplayer How To's](multiplayer-how-tos.md)  
Provides procedures for using 2015 Multiplayer in titles.

[Multiplayer Session Status Codes](multiplayer-session-status-codes.md)  
Defines multiplayer session status codes for Xbox One.

[2015 Multiplayer FAQs and Troubleshooting](multiplayer-2015-faq.md)  
Defines FAQs and troubleshooting for Multiplayer.

[Xbox One multiplayer session directory](xbox-one-multiplayer-session-directory.md)
Provides an overview of multiplayer session creation using the new Xbox One Multiplayer Session Directory (MPSD) service.

[Flows for multiplayer game invites](flows-for-multiplayer-game-invites.md)
Describes the flow of experiences involved in inviting another player to a multiplayer game.

[Game session and game party visibility and joinability](game-session-and-game-party-visibility-and-joinability.md)
Describes the differences between visibility and joinability as relates to a multiplayer game session.
