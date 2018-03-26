---
title: Large sessions
author: KevinAsgari
description: Learn about using large sessions with Xbox Live multiplayer platform.
ms.author: kevinasg
ms.date: 07/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, large session, recent players
ms.localizationpriority: low
---
# Large sessions

If you need a multiplayer session that can handle more than 100 members, you'll need to use what is known as a large session. This scenario is most common to massively multiplayer online (MMO) games and broadcasts (where most of the members are spectators), but may have applications to other styles of games as well.

In some circumstances, you may also wish to use large sessions even when dealing with smaller groups of players. If you want multiple players to be in the same session, but not necessarily be aware of each other if they don't encounter each other in game, you can use the "encounters" property of large sessions.

Large sessions are not currently supported by [Xbox Integrated Multiplayer (XIM)](../xbox-integrated-multiplayer.md) or by [Multiplayer Manager (MPM)](../multiplayer-manager.md), so you must use the Multiplayer 2015 APIs to use direct calls to the Multiplayer Service Directory (MPSD).

Large sessions are treated slightly differently than regular sessions:

* Contains less information than regular sessions, but are more efficient.
* Supports up to 10,000 members.
* You cannot subscribe to a large session.
* There is no automatic inclusion into the recent player lists for members of a large session.

## Recent players

One of the features of Xbox Live is that when Xbox Live players play multiplayer games with new people, after the game they can see those players on their dashboard in the **recent players** list. If a player had a great experience with a new player in a game, they may want to play with them again, or add them as a friend. If they had a poor experience with a player, they may wish to avoid them in the future, and/or report the bad behavior after the game is over.

With regular sessions, Xbox Live automatically adds players in the same session to the recent players list. If you use large sessions however, you must take some extra steps to ensure that the recent player lists are properly populated.

## Set up a large session

To set sessions up as large, add `“large”: true` to the capabilities section in the session template. That lets you set the `maxMembersCount` up to 10,000. A session template like the below should work:

```json
{
    "constants": {
        "system": {
            "version": 1,
            "maxMembersCount": 2000,
            "visibility": "open",
            "capabilities": {
                "gameplay": true,
                "large": true
            },
            "timeouts": {
                "inactive": 0,
                "ready": 180000,
                "sessionEmpty": 0
            }
        },
        "custom": { }
    }
}
```

## Working with large sessions

When writing large sessions to MPSD, we recommend that you do not to exceed 10 writes per second. This is typically a 1000 player session with a write every 2 minutes on average per player (e.g. join/leave).

Other properties should not be maintained in the large sessions.

### Associating players from the same large session

When you retrieve a large session from MPSD, the list of members doesn't come back with the response, and in fact there’s no way to get the full list. Instead, if the caller is in the session, their member record will be the only one in the “members” collection, labelled as “me” (just like in the request).

This means that clients members will only be able to update their own entry in the session, and will rely on the server to provide them with a common identifier that Xbox Live can use to associate players that played together.

There are two ways to indicate that people in a session played together (for updating reputation and recent players status).

#### 1. Persistent groups

If a group of people is staying together on an ongoing basis, potentially with people coming and going from it, you can give the group a name (for example, a guid – following the same naming rules as for regular sessions).  As each member comes and goes from the group, they should add or remove the group name to their own “groups” property, which is an array of strings:

```json
{
    "members": {
        "me": {
            "properties": {
                "system": {
                    "groups": [ "boffins-posse" ]
                }
            }
        }
    }
}
```

#### 2. Brief encounters

If two people have a brief one-time encounter, the game can instead use the “encounters” array. Give each encounter a name, and after the encounter, both (or all) participants would write the name to their own “encounters” property:

```json
{
    "members": {
        "me": {
            "properties": {
                "system": {
                    "encounters": [ "trade.0c7bbbbf-1e49-40a1-a354-0a9a9e23d26a" ]
                }
            }
        }
    }
}
```

You can use the same name for both “groups” and “encounters” – for example, if one player “trades with” a group, the people in the group won’t need to do anything (assuming they previously added the group name to their “groups”), and the person who had the encounter would upload the group name in their “encounters” list. That will cause the individual to see all the members of the group as recent players and vice versa.

Encounters count as having been a member of the group for 30 seconds. Since the encounters are considered one-off events, the “encounters” array is always immediately processed and then cleared from the session.  It will never appear in a response.  (The “groups” array sticks around until altered or removed, or the member leaves the session.)
