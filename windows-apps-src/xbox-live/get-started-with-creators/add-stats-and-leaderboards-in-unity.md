---
title: Add player stats and leaderboards to your Unity project
author: KevinAsgari
description: Lean how to use the Xbox Live Unity plugin to add player stats and leaderboards to your Unity project.
ms.assetid: 756b3c31-a459-4ad2-97af-119adcd522b5
ms.author: kevinasg
ms.date: 10/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, Unity, creators
ms.localizationpriority: low
---

# Add player stats and leaderboards to your Unity project

> [!IMPORTANT]
> The Xbox Live Unity plugin does not support achievements or online multiplayer and is only recommended for [Xbox Live Creators Program](../developer-program-overview.md) members.

Once you have added [Xbox Live sign in](unity-prefabs-and-sign-in.md) to your Unity project, the next step is to add player stats and leaderboards based on those player stats.

With the [Xbox Live Unity plugin](https://github.com/Microsoft/xbox-live-unity-plugin), you can easily add player stats and leaderboards in your Unity project. Similar to the sign in steps, you can choose to use the included prefabs or attach the included scripts to your own custom game objects.

## Prerequisites
1. [Configure Xbox Live in Unity](configure-xbox-live-in-unity.md)
2. [Sign in to Xbox Live in Unity](unity-prefabs-and-sign-in.md)

## Player stats

A player stat is any interesting statistic that you want to track for your players. The stats that you track with Xbox Live should be stats that are relevant to the player, and are displayed in some manner. These player stats are most commonly used to build leaderboards, which players can view to determine how their performance ranks against other players. All player stats are considered "featured stats", which means that the stat will be displayed in the GameHub page for the game.

Player stats must represent a single value. The Xbox Live Unity plugin contains prefabs for integer, double, and string stats. Additionally, a script is provided for a base stat object that can be extended to other data types.

For more information about player stats, see [Player stats](../leaderboards-and-stats-2017/player-stats.md).

> [!NOTE]
> In order to use player stats or leaderboards with the Xbox Live service, you must have a successfully signed in user before you can access any data.

## Using the player stat prefabs

There are several prefabs provided in the Xbox Live Unity plugin that you can use relating to player stats:

* IntegerStat: A stat that can be expressed as an integer value, such as total number of kills in a round.
* DoubleStat: A stat that can be expressed as a floating point value, such as a kill/death ratio.
* StringStat: A stat that can be expressed as a string value, typically an enumeration, such as a rank awarded for a round, such as "Gold", "Silver", or "Bronze".
* StatPanel: Sample UI that you can use to display the current value of a stat.

To add a player stat, simply drag the prefab that matches the data type of the stat onto the scene. In the Unity inspector for the stat, you can specify three values:

* The ID of the stat. This must match the ID configured in Windows Dev Center and is case sensitive.
* The display name of the stat (this name is displayed in the StatPanel prefab UI).
* The initial value of the stat when the scene starts.

You can use the **StatPanel** prefab to display the value of a stat. To do this, drag a **StatPanel** prefab onto the scene. You can specify the stat to display by dragging the stat gameobject to the **Stat** field of the **StatPanel** object in the Unity Inspector.

### Manipulating the player stat values

The player stat objects have a number of functions that you can call to adjust the value of the stat. These functions can be called from other routines, or bound to UI elements. You can look at the **DoubleStat**, **IntegerStat**, and **StringStat** scripts to see examples of functions to change the value of the stat. You can modify or create new scripts to represent stats with more complex functions and logic. New stat classes should extend the `StatBase` class, defined in the `StatBase` script.

For example, as a simple test, you can add a UI button to your scene, and in the `OnClick` event of the button, in the Unity inspector, add an **IntegerStat** object, and call the `Increment()` function to increase the value of the stat by one every time you click the button.

If you have the stat also bound to a **StatPanel** object, you can see the stat value update every time you click the button.

Every time you update your stats (increment, decrement, etc.), the values get updated locally. To have these stat updates reflected in Xbox Live two things must happen. First, you need to set the stats value with the one of the StatisticManager.SetStatistic functions. There are three `StatisticManager` functions to set statistics, `StatisticManager.SetStatisticIntegerData(XboxLiveUser user, String statName, Int64 value)`, `StatisticManager.SetStatisticNumberData(XboxLiveUser user, String statName, Double value)`, and `StatisticManager.SetStatisticStringData(XboxLiveUser user, String statName, String value)`. Each one of these functions is used to set the appropriate value for the data type of your stat. The second thing you must do to update your stats on the server, is to *flush* the local data. The data gets flushed automatically every 5 minutes by the `StatManagerComponent` script.  If your game ends before the 5 minutes, you need to make sure to flush the data manually first to make sure you don't lose that progress. To do that, you'll need to call the `statManagerComponent.RequestFlushToService()` method, making sure to call it for the **XboxLiveUser** the stat is being written for.

> [!TIP]
> It is considered best practice to always flush the data before your game ends to make sure you do not lose the progress.

### Checking and Verifying Stats

The `StatisticManager` class has two functions which are useful for checking on the statistics configured for an `XboxLiveUser`, `StatisticManager.GetStatisticNames(XboxLiveUser user)` and `StatisticManager.GetStatistic(XboxLiveUser user, String statName)`. `GetStatisticNames()` will provide a `list<string>` populated by the names of the stats for the XboxLiveUser provided. Those names can be used to call for the current value of a statistic by calling the `GetStatistic()` function. It is important to note that while you can read statistics from the Xbox Live stats service it is not recommended that you use it for game logic, but rather to check on the state of the statistic after it has been pushed. The service is only meant to help run other services like Leaderboards and is not meant to be a source of truth for statistics in your game. It is important that your title handle all stats logic as no checks are made on your statistic by the Xbox service and it simply accepts whatever value given to it as the current stat.

## Leaderboards

A leaderboard represents an ordered, numbered list of the players who have achieved the "best" value of a stat. For example, a leaderboard might list the people who have achieved the fastest time on a race lap, so that players can compare their best race time against the best race times achieved by other players.

Leaderboards are based on the player stats that are sent to the Xbox Live service by the game. Therefore, leaderboard data is read only, as you cannot modify them directly.

The Xbox Live Unity plugin provides a sample leaderboard prefab that you can use to understand how to implement leaderboards in your game.

For more information about leaderboards, see [Leaderboards](../leaderboards-and-stats-2017/leaderboards.md).

## Using the leaderboard prefabs

The Xbox Live Unity plugin contains two prefabs for leaderboards:

* Leaderboard: An object that represents a leaderboard, and contains simple UI to display the values from the leaderboard.
* LeaderboardEntry: An object that represents a single row of a leaderboard.

You can drag a **Leaderboard** prefab onto the scene. In the Unity Inspector, you can set the following attributes:

* Stat: The stat gameobject that this leaderboard is associated with.
* Leaderboard Type: The scope of the results that should be returned for the leaderboard entries.
* Entry Count: The number of rows of data to display per page.

> [!NOTE]
> The Stat portion of the leaderboard prefab is initially blank. Try dragging one of the stat prefabs mentioned above into the gameobject slot for testing.

In the Unity editor, the **Leaderboard** prefab will always display the same mock data regardless of the inspector settings. You must build and export your project to Visual Studio and sign in with an authorized user to see real data values. For more information, see [Configure Xbox Live in Unity](configure-xbox-live-in-unity.md).

## See also

* [Sign into Xbox Live in Unity](unity-prefabs-and-sign-in.md)
* [Configure Xbox Live in Unity](configure-xbox-live-in-unity.md)
* [The Leaderboard Example Scene](setup-leaderboard-example-scene.md)
* [Get Leaderboard Data](unity-leaderboard-from-scratch.md)
