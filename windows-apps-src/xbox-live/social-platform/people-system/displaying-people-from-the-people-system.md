---
title: Display People from the People System
author: KevinAsgari
description: Learn abou the code flow to display people by using the Xbox Live people system.
ms.assetid: c97b699f-ebc2-4f65-8043-e99cca8cbe0c
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Display People from the People System

Services across Xbox Live return only data owned by that service and return only XUID references to users; for example, the People service only owns and returns the XUIDs that are on a user's People list and some very basic information about each of those XUIDs (such as favorite status). The Presence service owns data about the online status information of XUIDs. The leaderboards service owns ranking information on lists of XUIDs. Display name and gamertag information is never returned from any service other than the Profile service and, therefore, calling multiple services is necessary to render lists of people in experiences.

The general calling pattern for the service APIs is to make one round trip to first obtain a list of XUIDs from the service that can best filter or sort the list, then make simultaneous round-trip calls to other services needed to obtain any additional metadata required for each XIUD. In the case of images, a third round trip of calls may be required to obtain images from the image URLs.

To reduce the number of round trips required to obtain data about a user's People list, a People *moniker* is being introduced to relevant services. This new feature allows callers to abstractly express to the primary service that the service should obtain the list of the user's People from the People service, and then use that set of XUIDs to scope the return.

Following are some example call flow scenarios that illustrate how titles obtain data from services related to People:

-   List of users currently in game
-   List of the current user's People who are online
-   Global Leaderboard containing random users
-   Leaderboard of user's People


## List of users currently in game

| Title has  | Goal  | Field to render  | Call flow
|-------------------------------------------------|----------------------------------------------------|--------------------|--------------------------------------|
| List of random XUIDs of other users in the game | To render minimal info for each of the other users | GameDisplayName  \[Profile\] | Call Profile with the list of XUIDs. |


## List of the current user's People who are online

## Title has:
The current user's XUID

## Goal
To render rich list of online users that are in the current user's people list

## Field to render \[owning service\]
* Favorite indicator [People]
* Display picture [Profile]
* GameDisplayName [Profile]
* Basic online status (green ball) [Presence]

## Call flow
1. Call Presence, passing in the People moniker to get the XUIDs and online status for each of the user's People.
1. In parallel:
 1. Call Profile, passing in the entire list of XUIDs to get the display name and picture URL for each.
 1. Call People, passing in the list of XUIDs to find out if any are favorites of the user.
1. After calling Profile:
 1. Get images for each picture URL

## Global Leaderboard containing random users

## Title has:
The ID/name of the leaderboard

## Goal
To render basic info for each user on the leaderboard

## Field to render [owning service]
* Favorite indicator [People]
* GameDisplayName [Profile]
* Rank [Leaderboards]
* Score [Leaderboards]

## Call Flow
1. Call Leaderboards to get the XUIDs, rank, and scores for the particular leaderboard.
1. In parallel:
 1. Call Profile, passing in the list of XUIDs to get the display name and picture URL for each.
 1. Call People, passing in the list of XUIDs to find out if any are favorites of the user.

## Leaderboard of user's People

## Title has:
* The ID/name of the leaderboard
* The current user's XUID

## Goal
To render basic info for each user on the leaderboard

## Field to render [owning service]
* Favorite indicator [People]
* GameDisplayName [Profile]
* Rank [Leaderboards]
* Score [Leaderboards]

## Call Flow
1. Call Leaderboards, passing in the People moniker to get the XUIDs, rank, and scores for the particular leaderboard limited to the user's People list.
1. In parallel:
 1. Call Profile, passing in the list of XUIDs to get the display name and picture URL for each.
 1. Call People, passing in the list of XUIDs to find out if any are favorites of the user.
