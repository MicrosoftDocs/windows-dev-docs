---
title: Add player stats and leaderboards to your Unity project
author: KevinAsgari
description: Lean how to use the Xbox Live Unity plugin to add player stats and leaderboards to your Unity project.
ms.assetid: 756b3c31-a459-4ad2-97af-119adcd522b5
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, Unity, creators
---

# Add player stats and leaderboards to your Unity project

> **Note:**
> The Xbox Live Unity plugin is only recommended for [Xbox Live Creators Program](../developer-program-overview.md) members, since currently there is no support for achievements or multiplayer.

Once you have added [Xbox Live sign in](sign-in-to-xbox-live-in-unity.md) to your Unity project, the next step is to add player stats and leaderboards based on those player stats.

With the Xbox Live Unity plugin, you can easily add player stats and leaderboards in your Unity project. Similar to the sign in steps, you can use the included prefabs, or you can attach the included scripts to your own custom objects.

> **Note:**
> This topic assumes that you have already set up the Xbox Live plugin in your Unity project. For information about how to do that, see [Configure Xbox Live in Unity](configure-xbox-live-in-unity.md).

## Player stats

A player stat is any interesting statistic that you want to track for your players. The stats that you track with Xbox Live should be stats that are relevant to the player, and are displayed in some manner. These player stats are most commonly used to build leaderboards, which players can view to determine how their performance ranks against other players. Some player stats can be marked as "featured player stats", which means that the player stat will be displayed in the GameHub page for the game.

Individual stats cannot be complex objects, but must represent a single value. The Xbox Live Unity plugin contains prefabs for integer, double, and string stats. In addition, a script is provided for a base stat object that can be extended to other data types.

For more information about player stats, see [Player stats](../leaderboards-and-stats-2017/player-stats.md).

> **Note:** In order to use player stats or leaderboards with the Xbox Live service, you must have a successfully signed in user before you can access any data.

## Using the player stat prefabs

There are several prefabs provided in the Xbox Live Unity plugin that you can use relating to player stats:

* IntegerStat - A stat that can be expressed as an integer value, such as total number of kills in a round.
* DoubleStat - A stat that can be expressed as a floating point value, such as a kill/death ratio.
* StringStat - A stat that can be expressed as a string value, typically an enumeration, such as a rank awarded for a round, such as "Gold", "Silver", or "Bronze".
* StatPanel - Sample UI that you can use to display the current value of a stat.

To add a player stat, simply drag the prefab that matches the data type of the stat onto the scene. In the Unity inspector for the stat, you can specify three values:

* The name of the stat.
* The display name of the stat (this name is displayed in the StatPanel prefab UI).
* The initial value of the stat when the scene starts.

You can use the **StatPanel** prefab to display the value of a stat. Simply drag a **StatPanel** prefab onto the scene, and specify which stat to display by using the Unity inspector for the **StatPanel** object.

### Manipulating the player stat values

The player stat objects have a number of functions that you can call to adjust the value of the stat. These functions can be called from other routines, or bound to UI elements. You can look at the **DoubleStat**, **IntegerStat**, and **StringStat** scripts to see examples of functions to change the value of the stat. You can modify or create new scripts to represents stats with more complex functions and logic. New stat classes should extend the **StatBase** class, defined in the **StatBase** script.

For example, as a simple test, you can add a UI button to your scene, and in the **OnClick** method of the button, in the Unity inspector, add an **IntegerStat** object, and call the **Increment()** function to increase the value of the stat by one every time you click the button.

If you have the stat also bound to a **StatPanel** object, you can see the stat value update every time you click the button.

## Leaderboards

A leaderboard represents an ordered, numbered list of the players who have achieved the "best" value of a stat. For example, a leaderboard might list the people who have achieved the fastest time on a race lap, so that players can compare their best race time against the best race times achieved by other players.

Leaderboards are based off of player stats that are sent to the Xbox Live service by the game. Therefore, leaderboard data is read only, as you cannot modify them directly.

The Xbox Live Unity plugin provides a sample Leaderboard prefab that you can use to understand how to implement leaderboards in your game.

For more information about leaderboards, see [Leaderboards](../leaderboards-and-stats-2017/leaderboards.md).

## Using the leaderboard prefabs

The Xbox Live Unity plugin contains two prefabs for leaderboards:

* LeaderBoard - an object that represents a leaderboard, and contains simple UI to display the values from the leaderboard.
* LeaderboardEntry - an object that represents a single row of a leaderboard.

You can drag a **Leaderboard** prefab onto the scene. In the Unity inspector, you can set the following attributes:

* Leaderboard Name - If you have defined a global leaderboard in your service configuration, this name should match the name of the configured leaderboard. Otherwise, this name should match the value of a player stat.
* Display Name - the name displayed in the UI for the prefab
* Entry Count - the number of rows of data to display per page.

In the Unity editor, the **Leaderboard** prefab will always display the same mock data regardless of the inspector settings. You must build and export your project to Visual Studio and sign in with an authorized user in order to see real data values. For more information, see [Configure Xbox Live in Unity](configure-xbox-live-in-unity.md).

## See also

* [Sign into Xbox Live in Unity](sign-in-to-xbox-live-in-unity.md)
* [Configure Xbox Live in Unity](configure-xbox-live-in-unity.md)
