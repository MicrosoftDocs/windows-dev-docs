---
title: User (JSON)
assetID: dbc733e4-0348-0e3d-1f55-17b465e599d6
permalink: en-us/docs/xboxlive/rest/json-user.html
author: KevinAsgari
description: ' User (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# User (JSON)
Contains user leaderboard data. 
<a id="ID4EN"></a>

 
## User
 
The User object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| gamertag| string| Gamertag of the player (maximum of 15 characters). The client should use this value in the UI when identifying the player.| 
| rank| 32-bit signed integer| The rank of the user relative to the user requesting the leaderboard data.| 
| rating| string| The user's rating.| 
| xuid| 64-bit unsigned integer| The Xbox User ID (XUID) of the user.| 
  
<a id="ID4EMC"></a>

 
## Sample JSON syntax
 

```cpp
{ 
   "gamertag":"TrueBlue402",
   "rank":2,
   "rating":"2:19:21.17",
   "xuid":1234567890123456 
}
    
```

  
<a id="ID4EVC"></a>

 
## See also
 
<a id="ID4EXC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   