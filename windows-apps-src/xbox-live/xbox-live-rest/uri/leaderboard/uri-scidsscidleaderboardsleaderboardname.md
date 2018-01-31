---
title: /scids/{scid}/leaderboards/{leaderboardname}
assetID: 16345a17-6025-5453-5694-eaf97f0e83e9
permalink: en-us/docs/xboxlive/rest/uri-scidsscidleaderboardsleaderboardname.html
author: KevinAsgari
description: ' /scids/{scid}/leaderboards/{leaderboardname}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /scids/{scid}/leaderboards/{leaderboardname}
Accesses a predefined global leaderboard. 
The domain for these URIs is `leaderboards.xboxlive.com`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| GUID| Identifier of the service configuration which contains the resource being accessed.| 
| leaderboardname| string| Unique identifier of the predefined leaderboard resource being accessed.| 
  
<a id="ID4E3B"></a>

 
## Valid methods

[GET](uri-scidsscidleaderboardsleaderboardnameget.md)

&nbsp;&nbsp;
&nbsp;&nbsp;Gets a predefined global leaderboard.


[GET with value metadata](uri-scidsscidleaderboardsleaderboardnamegetvaluemetadata.md)

&nbsp;&nbsp;
&nbsp;&nbsp;Gets a predefined global leaderboard along with any metadata associated with the leaderboard values.

 
<a id="ID4EJC"></a>

 
## See also
 
<a id="ID4ELC"></a>

 
##### Parent 

[Leaderboards URIs](atoc-reference-leaderboard.md)

   