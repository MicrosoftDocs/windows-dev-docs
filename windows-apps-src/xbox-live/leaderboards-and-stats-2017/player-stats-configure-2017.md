---
title: Configure Stats and Leaderboards 2017
author: KevinAsgari
description: Learn how to configure Xbox Live Featured Stats and Leaderboards on Universal Dev Center with Data Platform 2017.
ms.assetid: e0f307d2-ea02-48ea-bcdf-828272a894d4
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Configuring Featured Stats or Leaderboards on Universal Dev Center with Data Platform 2017

With Data Platform 2017, you only need to configure a stat in two cases:

* The stat is used as a basis for a global leaderboard, or
* The stat is a featured player stat that will be displayed on the game hub page.

In either case, you must configure stats and leaderboards together. Every leaderboard is based on a stat, and you cannot configure a stat without also configuring an associated global leaderboard, even if you do not plan to use a leaderboard for a featured player stat.

You do not need to configure stats that are not featured player stats, and are not used by a global leaderboard.

## Configure a global leaderboard and an associated player stat

First, go to the Player Stats section for your title as shown below.

![](../images/omega/dev_center_player_stats_creators.png)

Then click the `Create your first featured stat/leaderboard` button, and then fill out this dialog and click Save.

![](../images/omega/dev_center_player_stats_creators_leaderboard.png)

The `Display name` field is what users will see in the GameHub.  This string can be localized in the `Localize strings` section.  Click `Show Options` in the `Localize strings` section to see details on how to localize these strings.

The `ID` field is the stat name and will be how you will refer to your stat when updating it from your title code.   See the [Updating Stats](player-stats-updating.md) section below for more detail.

The `Format` is the data format of the stat.

The `Display Logic` offers the choice between `Player progression`, `Personal best`, and `Counter`:
- Player progression: Represents an individual player level or progression in the game.  The last value set is what users will see.  For example, position in current round.
- Personal Best: Represents the current best score a player has posted. The max or min value set depending on sort order is what users will see.  For example, fastest lap.
- Counter: Can be added to other player's to calculate a cumulative number.  

The `Sort` field lets you change the sort order of the leaderboard.

You can also choose to make this stat a `Featured Stat`, but clicking on `Feature on players' profiles`.  

## Configure Featured Stats

When you define a player stat, you have the option of checking `Featured Stat`.  Please note the following requirements

| Developer Type | Requirement |
|----------------|-------------|
| Xbox Live Creators Program | There is no requirement to designate any stats as Featured Stats.  Though you are limited to a maximum of 10 |
| ID@Xbox and Microsoft Partners | You must designate between 3 and 10 Featured Stats |

## Next Steps

Next you'll need to update the stat from title code.  See [Updating Stats](player-stats-updating.md) for more detail.
