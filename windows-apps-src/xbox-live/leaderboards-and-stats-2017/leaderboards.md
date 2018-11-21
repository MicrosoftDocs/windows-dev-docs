---
title: Leaderboards

description: Learn how to use Xbox Live leaderboards to compare players.
ms.assetid: 132604f9-6107-4479-9246-f8f497978db7
ms.date: 09/28/2018
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
---
# Leaderboards

## Introduction

As described in [Data Platform Overview](../data-platform/data-platform.md), Leaderboards are a great way to encourage competition between your players, and keep players engaged in trying to beat their previous best score as well as that of their friends.

Leaderboards for [Featured Stats](stats2017.md#configured-stats-and-featured-leaderboards) are always displayed in a title's Game Hub and sometimes displayed as a part of the UI for a title when it is pinned to the homepage. You can also use your configured Featured Stats to create Leaderboards inside of your title.

## Choosing Good Leaderboards

As discussed in [Player Stats](player-stats.md), a leaderboard corresponds to a stat that you have defined.  You should choose leaderboards that correspond to an accomplishment that a player can work towards improving.

For example, Best Lap Time in a racing game is a good leaderboard, because players will want to work towards improving their Best Lap Time.  Other examples are Kill/Death ratio for shooters, or Max Combo Size in a fighting game.

## When To Display Leaderboards

You have the ability to display leaderboards at any time in your title.  You should choose a time when a leaderboard will not interfere with the gameplay or the flow of your title.  In between rounds and after matches are both good times.

## How to Display Leaderboards

There are numerous options for displaying leaderboards provided in the Xbox Live SDK.  If you are using Unity with the Xbox Live Creators Program, you can get started by using a Leaderboard Prefab to display your leaderboard data.  See the [Configure Xbox Live in Unity](../get-started-with-creators/configure-xbox-live-in-unity.md) article for specifics.

If you are coding against the Xbox Live SDK directly, then read on to learn about the APIs you can use.

## Programming Guide

There are several Leaderboard APIs you can use to get the current state of a leaderboard.  All of the APIs are asynchronous and do not block.  You would make a request to get leaderboard data and continue your usual game processing.  When the leaderboard results are returned from the service, you can display the results at the appropriate time.

You should request the leaderboard data from the service, slightly ahead of when you want to display it, so that players are not blocked waiting for the leaderboard to display.

## Leaderboards 2013 APIs

You can see the `leaderboard_service` namespace for all Stats 2013 Leaderboard API.

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

```csharp
Windows::Foundation::IAsyncOperation< LeaderboardResult^> ^  GetLeaderboardAsync (
        _In_ Platform::String^ serviceConfigurationId,
         _In_ Platform::String^ leaderboardName
        ) 
```

</td>

<td>WinRT C# Code - Get a leaderboard for a single leaderboard given a service configuration ID and a leaderboard name.</td>

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

```csharp
Windows::Foundation::IAsyncOperation< LeaderboardResult^> ^  GetLeaderboardAsync (
         _In_ Platform::String^ serviceConfigurationId,
         _In_ Platform::String^ leaderboardName,
         _In_ uint32 skipToRank,
         _In_ uint32 maxItems
        ) 
```

</td>

<td>WinRT C# code - Get a page of leaderboard results for a single leaderboard give a service configuration ID and a leaderboard name, leaderboard results will start at the "skipToRank" rank.</td>

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

<tr>

<td markdown="block">

```csharp
Windows::Foundation::IAsyncOperation< LeaderboardResult^> ^  GetLeaderboardWithSkipToUserAsync (
         _In_ Platform::String^ serviceConfigurationId,
         _In_ Platform::String^ leaderboardName,
         _In_opt_ Platform::String^ skipToXboxUserId,
         _In_ uint32 maxItems
        )
```

</td>

<td>WinRT C# code - Get a leaderboard starting at a specified player, regardless of the player's rank or score, ordered by the player's percentile rank</td>

</tr>

</table>

## 2013 C++ Example

When using the C++ API layer you can then set a callback to be invoked once the Leaderboard results are returned from the service.  We will show an example of this below.

If you are unfamiliar with the `pplx::task` being returned from these APIs, this is an asynchronous task object from the Microsoft Parallel Programming Library (PPL).  You can learn more about that at [https://github.com/Microsoft/cpprestsdk/wiki/Programming-with-Tasks](https://github.com/Microsoft/cpprestsdk/wiki/Programming-with-Tasks).

The section below shows how you might retrieve Leaderboard results and use them.

### 1. Create an async task to retrieve leaderboard results

The first step is to call the Leaderboards service to retrieve the results for a particular leaderboard.

```cpp
pplx::task<xbox_live_result<leaderboard_result>> asyncTask;
auto& leaderboardService = xboxLiveContext->leaderboard_service();

asyncTask = leaderboardService.get_leaderboard(m_liveResources->GetServiceConfigId(), LeaderboardIdEnemyDefeats);
```

### 2. Setup a callback

You can setup a [continuation task](https://msdn.microsoft.com/en-us/library/dd492427(v=vs.110).aspx#continuations) to be called once the leaderboard results are returned.  You do that as follows below.

```cpp
asyncTask.then([this](xbox::services::xbox_live_result<xbox::services::leaderboard::leaderboard_result> result)
{
    // Handle result here
});
```

This continuation task is called in the context of the object that originally invoked it, and receives the ```leaderboard_result``` which can be displayed in a manner that suits your title.


### 3. Display Leaderboard

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

## 2013 WinRT C# Example

When using the WinRT C# layer you will not need to make a separate callback task and will simply need to use the `await` keyword when calling the leaderboard service.

### 1. Access the LeaderboardService

The `LeaderboardService` can be retrieved from the `XboxLiveContext` created when signing in a user to the game, you will need it to call for leaderboard data.

```csharp
XboxLiveContext xboxLiveContext = idManager.xboxLiveContext;
LeaderboardService boardService = xboxLiveContext.LeaderboardService;
```

### 2. Call the LeaderboardService

```csharp
LeaderboardResult boardResult = await boardService.GetLeaderboardAsync(
     xboxLiveConfig.ServiceConfigurationId,
     leaderboardName
     );
```

### 3. Retrieve Leaderboard data

`GetLeaderboardAsync()` returns a `LeaderboardResult` which will contain the statistics populating the named leaderboard.

`LeaderboardResult` has several functions and properties to facilitate the reading of leaderboard data.

|Property  |Description  |
|---------|---------|
|public IAsyncOperation<LeaderboardResult> GetNextAsync(uint maxItems);     |Retrieves the next set of ranks up to the number of the maxItems parameter. This is essentially another call to `GetLeaderboard()`         |
|public LeaderboardQuery GetNextQuery();     |Retrieves the LeaderboardQuery that could be used to make the Leaderboard call to retrieve the next set of data.         |
|public bool HasNext { get; }    |designates whether or not there are more leaderboard rows to retrieve         |
|public IReadOnlyList<LeaderboardRow> Rows { get; }     | Rows containing Leaderboard data per rank        |
|public IReadOnlyList<LeaderboardColumn> Columns { get; }     | The list of columns that make up the leaderboard        |
|public uint TotalRowCount { get; }     | Total amount of rows in Leaderboard        |
|public string DisplayName { get; }     | Name to be displayed for Leaderboard       |

Leaderboard data will be provided one page at a time. You may loop through the `LeaderboardResult` Rows and Columns to retrieve the data.  
Use the `HasNext` boolean and `GetNextAsync()` function to retrieve later pages of Leaderboard data.

```csharp
if (boardResult != null)
{
    foreach (LeaderboardRow row in boardResult.Rows)
    {
        Debug.Write(string.Format("Rank: {0} | Gamertag: {1} | {2}\n", row.Rank, row.Gamertag, row.Values.ToString()));
    }
}
```

## Leaderboard 2017

To make calls to the Stats 2017 Leaderboard service you will use the `StatisticManager` leaderboard apis instead of the `LeaderboardService` leaderboard apis.  

`xbox::services::stats:manager::stats_manager::get_leaderboard`  

```cpp
xbox_live_result< void >  get_leaderboard (
     _In_ const xbox_live_user_t &user,
     _In_ const string_t &statName,
     _In_ leaderboard::leaderboard_query query
     ) 
```  

`xbox::services::stats:manager::stats_manager::get_leaderboard`  

```cpp
xbox_live_result< void >  get_social_leaderboard (_In_ const xbox_live_user_t &user,
     _In_ const string_t &statName,
     _In_ const string_t &socialGroup,
     _In_ leaderboard::leaderboard_query query
)
```  

`Microsoft.Xbox.Services.Statistics.Manager.StatisticManager.GetLeaderboard`  

```csharp
public void GetLeaderboard(
    XboxLiveUser user,
    string statName,
    LeaderboardQuery query
    )
```  

`Microsoft.Xbox.Services.Statistics.Manager.StatisticManager.GetSocialLeaderboard`  

```csharp
public void GetSocialLeaderboard(
    XboxLiveUser user,
    string statName,
    string socialGroup,
    LeaderboardQuery query
    )
```  

## 2017 C++ Example

### 1. Get a Singleton Instance of the stats_manager

Before you can call the `stats_manager` functions you will need to set a variable to it's Singleton Instance.

```csharp
m_statsManager = stats_manager::get_singleton_instance();
```

### 2. Create a LeaderboardQuery

The `leaderboard_query` will dictate the amount, order, and starting point of the data returned from the leaderboard call.

A `leaderboard_query` has a few attributes that can be set which will effect the data returned:

|Property |Description  |
|---------|---------|
|m_skipResultToRank     |this uint variable will determine what ranking the leaderboard data will start with when returning. Rankings start at rank 1.         |
|m_skipResultToMe     |if set to true this boolean value will cause the leaderboard data returned to start at the `XboxLiveUser` used in the `get_leaderboard()` call.  |
|m_order     |Enums of type `xbox::services::leaderboard::sort_order` have two possible values, ascending, and descending. Setting this variable for your query will determine the sort order of your leaderboard.        |
|m_maxItems     |This uint determines the maximum number of rows to return per call to `get_leaderboard` or `get_social_leaderboard()`.         |

`leaderboard_query` has several set function you can use to assign value to these properties. The following code will show you how to setup your `leaderboard_query`

```cpp
leaderboard::leaderboard_query leaderboardQuery;
leaderboardQuery.set_skip_result_to_rank(10);
leaderboardQuery.set_max_items(10);
leaderboardQuery.set_order(sort_order::descending);
```

This query would return ten rows of the leaderboard starting at the 100th ranked individual.

> [!WARNING]
> Setting SkipResultToRank higher than the number of players contained within the leaderboard will cause the leaderboard data to return with zero rows.

### 3. Call get_leaderboard

```cpp
leaderboard::leaderboard_query leaderboardQuery;
m_statsManager->get_leaderboard(user, statName, leaderboardQuery);
```

> [!IMPORTANT]
> The `statName` used in the `GetLeaderboard()` call must be the same as the name of a stat configured for your title in [Partner Center](https://partner.microsoft.com/dashboard), which is case-sensitive.

### 4. Read the Leaderboard data

In order to read the leaderboard data you will need to call the `stats_manager::do_work()` function which will return a list of `stat_event` values. Leaderboard data will be contained in a `stat_event` of the type `stat_event_type::get_leaderboard_complete`. When you come across an event of this type in the list of `stat_event`s you may look through the `leaderboard_result` contained in the `stat_event` to access the data.

Sample `do_work()` handler

```cpp
void Sample::UpdateStatsManager()
{
    // Process events from the stats manager
    // This should be called each frame update

    auto statsEvents = m_statsManager->do_work();
    std::wstring text;

    for (const auto& evt : statsEvents)
    {
        switch (evt.event_type())
        {
            case stat_event_type::local_user_added: 
                text = L"local_user_added"; 
                break;

            case stat_event_type::local_user_removed: 
                text = L"local_user_removed"; 
                break;

            case stat_event_type::stat_update_complete: 
                text = L"stat_update_complete"; 
                break;

            case stat_event_type::get_leaderboard_complete: //leaderboard data is read here
                text = L"get_leaderboard_complete";
                auto getLeaderboardCompleteArgs = std::dynamic_pointer_cast<leaderboard_result_event_args>(evt.event_args());
                ProcessLeaderboards(evt.local_user(), getLeaderboardCompleteArgs->result());
                break;
        }

        stringstream_t source;
        source << _T("StatsManager event: ");
        source << text;
        source << _T(".");
        m_console->WriteLine(source.str().c_str());
    }
}
```

Reading the Leaderboard data from the Leaderboard Result  

```cpp
void Sample::PrintLeaderboard(const xbox::services::leaderboard::leaderboard_result& leaderboard)
{
    if (leaderboard.rows().size() > 0)
    {
        m_console->Format(L"%16s %6s %12s %12s\n", L"Gamertag", L"Rank", L"Percentile", L"Values");
    }

    for (const xbox::services::leaderboard::leaderboard_row& row : leaderboard.rows())
    {
        string_t colValues;
        for (auto columnValue : row.column_values())
        {
            colValues = colValues + L" ";
            colValues = colValues + columnValue;
        }
        m_console->Format(L"%16s %6d %12f %12s\n", row.gamertag().c_str(), row.rank(), row.percentile(), colValues.c_str());
    }
}
```  

Retrieve further pages of data from the leaderboard.  

```cpp
void Sample::ProcessLeaderboards(
    _In_ std::shared_ptr<xbox::services::system::xbox_live_user> user,
    _In_ xbox::services::xbox_live_result<xbox::services::leaderboard::leaderboard_result> result
    )
{
    if (!result.err())
    {
        auto leaderboardResult = result.payload();
        PrintLeaderboard(leaderboardResult);

        // Keep processing if there is more data in the leaderboard
        if (leaderboardResult.has_next())
        {
            if (!leaderboardResult.get_next_query().err())
            {               
                auto lbQuery = leaderboardResult.get_next_query().payload();
                if (lbQuery.social_group().empty())
                {
                    m_statsManager->get_leaderboard(user, lbQuery.stat_name(), lbQuery);
                }
                else
                {
                    m_statsManager->get_social_leaderboard(user, lbQuery.stat_name(), lbQuery.social_group(), lbQuery);
                }
            }
        }
    }
}
```  

## 2017 WinRT C# Example

### 1. Get a singleton instance of the StatisticManager

Before you can call the `StatisticManager` functions you will need to set a variable to it's Singleton Instance.

```csharp
statManager = StatisticManager.SingletonInstance;
```

### 2. Create a LeaderboardQuery

The `LeaderboardQuery` will dictate the amount, order, and starting point of the data returned from the leaderboard call.  

```csharp
public sealed class LeaderboardQuery : __ILeaderboardQueryPublicNonVirtuals
    {
        [Overload("CreateInstance1")]
        public LeaderboardQuery();

        public bool HasNext { get; }
        public string SocialGroup { get; }
        public string StatName { get; }
        public SortOrder Order { get; set; }
        public uint MaxItems { get; set; }
        public uint SkipResultToRank { get; set; }
        public bool SkipResultToMe { get; set; }
    }
```

A `LeaderboardQuery` has a few attributes that can be set which will effect the data returned:

|Property |Description  |
|---------|---------|
|SkipResultToRank     |this uint variable will determine what ranking the leaderboard data will start with when returning. Rankings start at rank 1.         |
|SkipResultToMe     |if set to true this boolean value will cause the leaderboard data returned to start at the `XboxLiveUser` used in the `GetLeaderboard()` call.  |
|Order     |Enums of type `Microsoft.Xbox.Services.Leaderboard.SortOrder` have two possible values, ascending, and descending. Setting this variable for your query will determine the sort order of your leaderboard.        |
|MaxItems     |This uint determines the maximum number of rows to return per call to `GetLeaderboard()` or `GetSocialLeaderboard()`.         |

Forming your `LeaderboardQuery` may look like the following:

```csharp
using Microsoft.Xbox.Services.Leaderboard;

LeaderboardQuery query = new LeaderboardQuery
        {
            SkipResultToRank = 100,
            MaxItems = 5
        };
```

This query would return five rows of the leaderboard starting at the 100th ranked individual.

> [!WARNING]
> Setting SkipResultToRank higher than the number of players contained within the leaderboard will cause the leaderboard data to return with zero rows.

### 3. Call GetLeaderboard()

You can now call `GetLeaderboard()` with your `XboxLiveUser`, the name of your statistic, and a `LeaderboardQuery`.

```csharp
statManager.GetLeaderboard(xboxLiveUser, statName, leaderboardQuery);
```

> [!IMPORTANT]
> The `statName` used in the `GetLeaderboard()` call must be the same as the name of a stat configured for your title in [Partner Center](https://partner.microsoft.com/dashboard), which is case-sensitive.

### 4. Read Leaderboard data

In order to read the leaderboard data you will need to call the `StatisticManager.DoWork()` function which will return a list of `StatisticEvent` values. Leaderboard data will be contained in a `StatisticEvent` of the type `GetLeaderboardComplete`. When you come across an event of this type in the list of `StatisticEvent`s you may look through the `LeaderboardResult` contained in the `StatisticEvent` to access the data.

```csharp
IReadOnlyList<StatisticEvent> statEvents = statManager.DoWork(); //In practice this should be called every update frame

foreach(StatisticEvent statEvent in statEvents)
{
    if(statEvent.EventType == StatisticEventType.GetLeaderboardComplete
        && statEvent.ErrorCode == 0)
    {
        LeaderboardResultEventArgs leaderArgs = (LeaderboardResultEventArgs)statEvent.EventArgs;
        LeaderboardResult leaderboardResult = leaderArgs.Result;
        foreach(LeaderboardRow leaderRow in leaderboardResult.Rows)
        {
            Debug.WriteLine(string.Format("Rank: {0} | Gamertag: {1} | {2}:{3}\n\n", leaderRow.Rank, leaderRow.Gamertag, "test", leaderRow.Values[0]));
        }
    }
}
```

In your title code `StatisticManager.DoWork()` should be used to handle all incoming Statistic Manager events and not just for leaderboards. 

> [!NOTE]
> In order to retrieve the `LeaderboardResultEventArgs` you will need to cast the `StatisticEvent.EventArgs` as a `LeaderboardResultEventArgs` variable.

### 5. Retrieve more leaderboard data

In order to retrieve later pages of leaderboard data you will need to use the `LeaderboardResult.HasNext` property and the `LeaderboardResult.GetNextQuery()` function to retrieve the `LeaderboardQuery` that will bring you the next page of data.

```csharp
while (leaderboardResult.HasNext)
{
    leaderboardQuery = leaderboardResult.GetNextQuery();
    statManager.GetLeaderboard(xboxLiveUser, leaderboardQuery.StatName, leaderboardQuery)
    // StatisticManager.DoWork() is called
    // Leaderboard results are read
}
```