---
title: Updating Stats 2017
author: KevinAsgari
description: Learn how to update Xbox Live player stats by using Stats 2017.
ms.assetid: 019723e9-4c36-4059-9377-4a191c8b8775
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, player stats, stats 2017
ms.localizationpriority: low
---

# Updating Stats 2017

You update stats by sending the latest value for the Xbox Live Serivce using the `StatsManager` APIs which will be discussed below.

It is up to your title to keep track of player stats, and you call `StatsManager` to update these as appropriate.  `StatsManager` will buffer any changes and flush these to the service periodically.  Your title can also manually flush.

> **Note** Do not flush stats too often.  Otherwise your title will be rate limited.  A best practice is to flush at most once every 5 minutes.

### Multiple Devices

It is possible for a player to play your title on multiple devices.  In this case, you need to make certain effort to keep things in sync.

For example, if a player got 15 headshots on their Xbox at home.  Later, they got 10 more headshots on their Xbox at a friend's house.  You would need to send the stat value of 25 on the second device.  But you would have no way of knowing this without somehow synchronizing this information.

There are a few ways you can do this:

1. Store them using [Connected Storage](../storage-platform/connected-storage/connected-storage-technical-overview.md).  Usually you would use Connected Storage for per-user save data.  This data is kept in sync across different devices for a given user.
2. Use your own web service to keep stats in sync if you already have one for performing auxillary tasks for your title.

### Offline

As we mentioned above, your title is responsible to keep track of player stats and, therefore, responsbile to support offline scenarios. 

### Examples

We will go through an example to tie these concepts together.

A common stat in a racing game is lap time.  Usually lower is better for these stats.  So you would create a stat and associated leaderboard, where lower is better.  In other words, this leaderboard would be sorted in ascending order.

Your title would keep track of a user's lap times during their play session.  You would update the Stats Manager only if they had a lap time lower than their previous best.

You can track their previous best in one of the following ways:
1. From the save file using Connected Storage.
2. Your own web service.

The service will replace the stat value no matter what.  So even if you were to update with a lap time that's greater than their previous best, then their previous best would be overwritten.

So please ensure in your title, that you are only sending the proper stat values based on your gameplay scenario.  In some cases lower values might be better, in some other cases higher might be better, or something else entirely.

## Programming Guide

Typically your flow for using stats is:

1. Initialize the `StatsManager` API by passing in a local user.
1. As a user plays through your title, update stat values using the `set_stat` functions.
1. These stat updates will be periodically flushed and written to Xbox Live.  You can also do this manually.

### Initialization

You call the `StatsManager` with a local user to initialize the API with the necessary information.

See below for an example

```cpp
std::shared_ptr<stats_manager> statsManager = stats_manager::get_singleton_instance();
statsManager->add_local_user(user);
statsManager->do_work();  // returns stat_event_type::local_user_added
```

### Writing Stats

You write stats using the `stats_manager::set_stat` family of functions.  There are three variants of this function for each data type:

* `set_stat_number` for floats.
* `set_stat_integer` for integers.
* `set_stat_string` for strings.

When you call these, the stat updates are cached locally on the device.  Periodically these will be flushed to Xbox Live.

You have the option of manually flushing stats via the `stats_manager::request_flush_to_service` API.  Please note that if you call this function too often, you will be rate limited.  This does not mean that the stat will never get updated.  It merely means that the update will happen when the timeout expires.

Please note that for stats of type *SUM*, you can send incremental updates.  Eg: If you have a stat for *Lifetime Kills*, and the value on the server is 100.  If you call `set_stat_integer` with an argument of 5.  The new value on the server is 105.

```cpp
statsManager->set_stat_integer(user, L"numHeadshots", 20);
statsManager->request_flush_to_service(user); // requests flush to service, performs a do_work
statsManager->do_work();  // applies the stat changes, returns stat_update_complete after flush to service
```

#### Example

Let's say that you have a first-person shooter.  During a match you might accumulate the following stats:

| Stat Name | Format |
|-----------|--------|
| Best Kills Per Round | Integer |
| Lifetime Kills | Integer |
| Lifetime Deaths | Integer |
| Lifetime Kill/Death Ratio | Number |

As the player goes through the match, you would increment the *Kills Per Round*, *Lifetime Kills* and *Lifetime Deaths* locally.

At the end of the match you would do the following:
1. Compare the kills they got in the round, with their previous best.  If it is greater, then update `StatsManager`.
2. Update their Lifetime Kills and Deaths with the new values and update `StatsManager`.
3. Calculate Kills/Deaths and update `StatsManager`

Note that for 1 and 2, you need to know their previous stat values.  See the above sections for best practices on retrieving these.

Any of these stats could correspond to a leaderboard, which will be discussed in the next article.

### Flushing Stats

You can manually flush stats using `stats_manager::request_flush_to_service`.  You might want to do this if you are want to display a leaderboard.

For example, if you had a leaderboard for `Lifetime Kills` in the above example, you would want to make sure that the stat updates corresponding to this stat had been flushed to the server before displaying the leaderboard.  That way the leaderboards reflects the player's latest progress.

### Cleanup
When the title closes, remove the user from stats manager. This will flush the latest stat values to the service.

```cpp
statsManager->remove_local_user(user);
statsManager->do_work();  // applies the stat changes, returns local_user_removed after flush to service
```
