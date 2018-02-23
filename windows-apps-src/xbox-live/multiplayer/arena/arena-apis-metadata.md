---
title: Arena APIs UI metadata
author: KevinAsgari
description: Contains a comprehensive list of UI metadata for Xbox Arena APIs.
ms.author: kevinasg
ms.date: 10-10-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, arena, tournament, ux
ms.localizationpriority: low
---

# Arena APIs: A comprehensive list of UI metadata

The Arena APIs return metadata for surfacing tournament, match, and player details inside a game. Here’s a complete list.

TOURNAMENT DETAILS	| MATCH DETAILS	| TEAM DETAILS	| REGISTRATION DETAILS
--- | --- | --- | ---
Organizer name | End time—The date/time that this tournament has reached either the “Canceled” or “Completed” state. This is set automatically when a tournament is updated. | Previous match (tournament game result states, ranking, end time, match start time, is a Bye, description, opposing team IDs) | Registration state (unknown, pending, withdrawn, rejected, registered, completed)
Tournament name (128 char max) | Is a Bye	| Team state (unknown, registered, waitlisted, standby, checked in, playing, completed) | Registration reason (unknown, closed, member already registered, tournament full, team eliminated, tournament completed)
Tournament description (1000 char max) | Tournament game result states (No contest/canceled, win, loss, draw, rank, no show) | Reason the team is in a completed state (rejected, evicted, eliminated, completed) | Minimum and maximum number of teams registered
Registration start/end time | Arbitration State: Completed, none, canceled, no results, partial results | Registration date—The date and time a team was registered. |
Check in start/end time | Match descriptive 'label' – ("final, Heat 1") | Team standing | Is Registration Open
Playing start/end time | Start time | Team name | Is Check in Open
Has a prize | Opposing Team Ids | Team final ranking | Registration start/end time
Min/max team size | | Continuation URI—takes the team members back to Arena UI | Check in start/end time
Game mode | | Current match metadata (description, start time, Is Bye, Opposing Team IDs) | Count of teams registered
Tournament style (single elimination, round robin) | | Team Summary (team state, ranking) |
Is Registration Open | | Gamertags |
Is Check in Open | | |
Teams registered count | | |
Is Playing Open | | |
