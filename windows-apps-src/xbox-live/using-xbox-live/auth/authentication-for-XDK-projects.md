---
title: Authentication for XDK projects
author: KevinAsgari
description: Learn how to sign in Xbox Live users in an Xbox Development Kit (XDK) title.
ms.assetid: 713bb2e3-80c5-4ac9-8697-257525f243d3
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Authentication for XDK projects

To take advantage of Xbox Live features in games, a user needs to create an Xbox Live profile to identify themselves in the Xbox Live community.  Xbox Live services keep track of game related activities using that Xbox Live profile, such as the user's gamertag and gamer picture, who the user's gaming friends are, what games the user has played, what achievements the user has unlocked, where the user stands on the leaderboard for a particular game, etc.

At a high level, you use the Xbox Live APIs by following these steps:
1. Identify the interacting user
2. Create an Xbox Live context based on the user
3. Use the Xbox Live context to access Xbox Live services
4. When the game exits or the user signs-out, release the XboxLiveContext object by setting it to null

### Creating an XboxLiveUser object
Most of the Xbox Live activities are related to the Xbox Live Users.  As a game developer, you need to first create an XboxLiveUser object to represent the local user.

C++:
```
Windows::Xbox::System::User^ user; // the interacting user.  From User::Users, etc
std::shared_ptr<xbox::services::xbox_live_context> xboxLiveContext = std::make_shared<xbox::services::xbox_live_context>( user );
```

WinRT:
```
Windows::Xbox::System::User^ user; // the interacting user.  From User::Users, etc
Microsoft::Xbox::Services::XboxLiveContext^ xboxLiveContext = ref new Microsoft::Xbox::Services::XboxLiveContext( user );
```
