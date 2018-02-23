---
title: Send game invites
author: KevinAsgari
description: Learn how to use Xbox Live multiplayer manager to let a player send game invites.
ms.assetid: 8b9a98af-fb78-431b-9a2a-876168e2fd76
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, multiplayer manager, flowchart, game invite
ms.localizationpriority: low
---

# Send game invites

One of the simpler multiplayer scenarios is to allow a gamer to play your game online with friends. This scenario covers the steps you need to send invites to another player to join your game.

Once you've [initialized the Multiplayer Manager](play-multiplayer-with-friends.md), and have [created a Lobby session by adding local users](play-multiplayer-with-friends.md), you must wait until you receive the `user_added` event before you can start sending invites.

There are two ways to send an invite:

1. [Xbox Platform Invite TCUI](#xbox-platform-invite-tcui)
2. [Title implemented custom UI](#title-implemented-custom-ui)

You can see a flowchart of the process here: [Flowchart - Send Invitation to another player](mpm-flowcharts/mpm-send-invites.md).

### 1) Xbox Platform Invite TCUI <a name="xbox-platform-invite-tcui">

| Method | Event triggered |
| -----|----------------|
| `multiplayer_lobby_session::invite_friends()` | `invite_sent` |

Calling `invite_friends()` will bring up the standard Xbox UI for inviting friends. This displays a UI that allows the player to select friends or recent players to invite to the game. Once the player hits confirm, Multiplayer Manager sends the invites to the selected players.

**Example:**

```cpp
auto result = mpInstance->lobby_session()->invite_friends(xboxLiveContext);
if (result.err())
{
  // handle error
}
```

**Functions performed by Multiplayer Manager**

* Brings up the Xbox stock title callable UI (TCUI)
* Sends invite directly to the selected players

### 2) Title implemented custom UI<a name="title-implemented-custom-ui">

| Method | Event triggered |
|-----|----------------|
| `multiplayer_lobby_session::invite_users()` | `invite_sent` |

Your title can implement a custom TCUI for viewing online friends and inviting them. Games can use the `invite_users()` method to send invites to a set of people defined by their Xbox Live User Ids. This is useful if you prefer to use your own in-game UI instead of the stock Xbox UI.

**Example:**

```cpp
std::vector<string_t>& xboxUserIds;
xboxUserIds.push_back();  // Add xbox_user_ids from your in-game roster list

auto result = mpInstance->lobby_session()->invite_users(xboxUserIds);
if (result.err())
{
  // handle error
}
```

**Functions performed by Multiplayer Manager**

* Sends invite directly to the selected players
