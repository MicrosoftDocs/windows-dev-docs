---
title: Xbox Live Data Platform
author: KevinAsgari
description: Overview of the Xbox Live Data Platform, which consists of services to manage achievements, player stats, and leaderboards.
ms.assetid: a8bb7c4f-09fe-4dba-b3ce-1fab60453831
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, stats, achievements, leaderboards, data platform
ms.localizationpriority: low
---

# Xbox Live Data Platform - Stats, Leaderboards, Achievements

Writing game data to the Xbox Live Data Platform enables your title to run as a service. Additionally, the Xbox Live Data Platform drives user engagement with your title using stats, leaderboards, and achievements, and surfaces featured stats in the console shell and Xbox App.

One example of a stat for a racing game might be the total races run in drag strip mode, which can be used to create a leaderboard to compare your score against the community, and can be reflected in your Rich Presence string in real time (for example, "GoTeamEmily is playing Drag Strip Mode: 523 races completed"). Total races in drag strip mode could also be used as a criterion for Matchmaking, assuring players with similar experience are more likely to be matched together.

Your title can display leaderboards based on player stats. For example, the leaderboard could be a global ranking of races completed. You call these services using the Xbox Live APIs directly, or wrappers in a game engine like Unity.

You can designate certain stats as featured stats. Featured stats are shown in your title's GameHub and compare players against their friends.

Achievements are not powered by stats, and your title decides when an achievement is unlocked. For example your title might have an achievement for completing a race in under a minute. Your title keeps track of the parameters needed to unlock the achievement. In this example it would be up to your title to measure how long the race took, and then award the achievement. Typically, Gamerscore is awarded along with the completion of achievements. It is up to you to decided the amount of Gamerscore for each achievement.

## Features ##
The following topics provide more information about Xbox Live Data Platform features:

* [Player Stats](../leaderboards-and-stats-2017/player-stats.md)
* [Achievements](../achievements-2017/achievements.md)
* [Leaderboards](../leaderboards-and-stats-2017/leaderboards.md)
