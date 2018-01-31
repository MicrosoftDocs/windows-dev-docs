---
title: Player (JSON)
assetID: eaf6d082-869b-d2d3-d548-5cef65e54541
permalink: en-us/docs/xboxlive/rest/json-player.html
author: KevinAsgari
description: ' Player (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Player (JSON)
Contains data for a player in a game session. 
<a id="ID4EN"></a>

 
## Player
 
The Player object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| customData| array of 8-bit unsigned integer| 1024 bytes of Base64 encoded game-specific player data. This value is opaque to the server.| 
| gamertag| string| Gamertag—a maximum of 15 characters—of the player. The client should use this value in the UI when identifying the player. | 
| isCurrentlyInSession| Boolean value| Indicates if the player is currently in the session or left the session.| 
| seatIndex| 32-bit signed integer| The index of the player in the session.| 
| xuid| 64-bit unsigned integer| The Xbox User ID (XUID) of the player.| 
  
<a id="ID4E3C"></a>

 
## Sample JSON syntax
 

```cpp
{
    "xuid": 2533274790412952,
    "gamertag":"MyTestUser",
    "seatindex": 3
    "customData":"AIHJ2?iE?/jiKE!l5S=T..."
    "isCurrentlyInSession":"true"
}
    
```

  
<a id="ID4EFD"></a>

 
## See also
 
<a id="ID4EHD"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

  
<a id="ID4ERD"></a>

 
##### Reference 

[GameSession (JSON)](json-gamesession.md)

   