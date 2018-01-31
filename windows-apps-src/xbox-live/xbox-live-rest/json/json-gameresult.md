---
title: GameResult (JSON)
assetID: 43d863c0-2179-ae46-5d4a-2f08cd44b667
permalink: en-us/docs/xboxlive/rest/json-gameresult.html
author: KevinAsgari
description: ' GameResult (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GameResult (JSON)
A JSON object representing data that describes the results of a game session. 
<a id="ID4EN"></a>

  
 
The GameResult JSON object has the following members.
 
| Member| Type| Description| 
| --- | --- | --- | 
| blob| array of 8-bit unsigned integers| Custom title-specific result data.| 
| outcome| string| The outcome of the player's participation in the game session. Valid values are "Win", "Loss", or "Tie". | 
| score| 64-bit signed integer| The score that the player received in the game session.| 
| time| 64-bit signed integer| The player's time for the game session.| 
| xuid| 64-bit unsigned integer| The Xbox user ID of the player to whom the results apply.| 
  
<a id="ID4EPC"></a>

 
## Sample JSON syntax
 

```cpp
{
   "xuid": 2533274790412952,
   "outcome": "Win",
   "score": 100
}
    
```

  
<a id="ID4EYC"></a>

 
## See also
 
<a id="ID4E1C"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   