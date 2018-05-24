---
title: Leaderboards
author: KevinAsgari
description: Learn how to use Xbox Live leaderboards to compare players.
ms.assetid: 132604f9-6107-4479-9246-f8f497978db7
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Leaderboards

## Introduction

As described in [Data Platform Overview](../data-platform/data-platform.md), Leaderboards are a great way to encourage competition between your players, and keep players engaged in trying to beat their previous best score as well as that of their friends.

Leaderboards for [Featured Stats](player-stats.md#what-needs-to-be-configured) are visible in Game Hub.  Whereas you can Leaderboards for both Featured Stats as well as regular stats, right from in your title.

## Choosing Good Leaderboards

As discussed in [Player Stats](player-stats.md), a leaderboard corresponds to a stat that you have defined.  You should choose leaderboards that correspond to an accomplishment that a player can work towards improving.

For example, Best Lap Time in a car racing game is a good leaderboard, because players will want to work towards improving their Best Lap Time.  Other examples are Kill/Death ratio, or Max Combo Size in a fighting game.

## When To Display Leaderboards

You have the ability to display leaderboards at any time in your title.  You should choose a time when a leaderboard will not interfere with the gameplay or the flow of your title.  In between rounds, after matches, etc are all good times.

## How to Display Leaderboards

There are numerous options for displaying leaderboards provided in the Xbox Live SDK.  If you are using Unity with the Xbox Live Creators Program, you can get started with using a Leaderboard Prefab to display your leaderboard data.  See the [Configure Xbox Live in Unity](../get-started-with-creators/configure-xbox-live-in-unity.md) article for specifics.

If you are coding against the Xbox Live SDK directly, then read on to learn about the APIs you can use.

### Programming Guide

There are several Leaderboard APIs you can use to get the current state of a leaderboard.  All of the APIs are asynchronous and do not block.  You would make a request to get leaderboard data and continue your usual game processing.  When the leaderboard results are returned from the service, you can display the results at the appropriate time.

You should request the leaderboard data from the service, slightly ahead of when you want to display it, so that players are not blocked waiting for the leaderboard to display.

You can see the `leaderboard_service` namespace for all Leaderboard API.

<table>

<tr>
<td>C++ API</td><td>Description</td>
</tr>

<tr>
<td markdown="block">

```cpp

pplx::task<xbox_live_result<leaderboard_result>> get_leaderboard(
        const string_t& scid,
        const string_t& name
        );
```

</td>

<td>Most basic version of the API.  This will return the leaderboard values for the given leaderboard, starting from the player at the top of the leaderboard.</td>

</tr>

<tr>
<td markdown="block">

```cpp

pplx::task<xbox_live_result<leaderboard_result>> get_leaderboard(
    _In_ const string_t& scid,
    _In_ const string_t& name,
    _In_ uint32_t skipToRank,
    _In_ uint32_t maxItems = 0
    );

```

</td>

<td>This API provides some more flexibility, you can specify the rank (position) that you want to display, as well as a max value of items to return.  For example you would use this API if you wanted to display the leaderboard starting at position 1000.</td>

</tr>

<tr>

<td markdown="block">

```cpp

pplx::task<xbox_live_result<leaderboard_result>> get_leaderboard_skip_to_xuid(
    _In_ const string_t& scid,
    _In_ const string_t& name,
    _In_ const string_t& skipToXuid = string_t(),
    _In_ uint32_t maxItems = 0
    );

```

</td>

<td>

Use this if you want to skip the leaderboard to a certain user.  A `XUID` is a unique identifier for each Xbox User.  You can obtain for the signed in user, or any one of their friends, and pass that into this function.

</td>

</tr>

</table>

You can then set a callback to be invoked once the Leaderboard results are returned from the service.  We will show an example of this below.

If you are unfamiliar with the `pplx::task` being returned from these APIs, this is an asynchronous task object from the Microsoft Parallel Programming Library (PPL).  You can learn more about that at [https://github.com/Microsoft/cpprestsdk/wiki/Programming-with-Tasks](https://github.com/Microsoft/cpprestsdk/wiki/Programming-with-Tasks).

The section below shows how you might retrieve Leaderboard results and use them.

### Example

#### 1. Create an async task to retrieve leaderboard results

The first step is to call the Leaderboards service to retrieve the results for a particular leaderboard.

```cpp
pplx::task<xbox_live_result<leaderboard_result>> asyncTask;
auto& leaderboardService = xboxLiveContext->leaderboard_service();

asyncTask = leaderboardService.get_leaderboard(m_liveResources->GetServiceConfigId(), LeaderboardIdEnemyDefeats);
```

#### 2. Setup a callback

You can setup a [continuation task](https://msdn.microsoft.com/en-us/library/dd492427(v=vs.110).aspx#continuations) to be called once the leaderboard results.  You do that as follows below.

```cpp
asyncTask.then([this](xbox::services::xbox_live_result<xbox::services::leaderboard::leaderboard_result> result)
{
    // Handle result here
});
```

This continuation task is called in the context of the object that originally invoked it, and receives the ```leaderboard_result``` which can be displayed in a manner that suits your title.


#### 3. Display Leaderboard

The leaderboard data is contained in ```leaderboard_result``` and the fields are self explanatory.  See below for an example.

```cpp
auto leaderboard = result.payload();

for (const xbox::services::leaderboard::leaderboard_row& row : leaderboard.rows())
{
    string_t colValues;
    for (auto columnValue : row.column_values())
    {
        colValues = colValues + L" ";
        colValues = colValues + columnValue;
    }
    m_console->Format(L"%18s %8d %14f %10s\n", row.gamertag().c_str(), row.rank(), row.percentile(), colValues.c_str());
}

```