---
title: /users/xuid({xuid})/achievements/{scid}/{achievementid}
assetID: 656a6d63-1a11-b0a5-63d2-2b010abd62e7
permalink: en-us/docs/xboxlive/rest/uri-usersxuidachievementsscidachievementid.html
author: KevinAsgari
description: ' /users/xuid({xuid})/achievements/{scid}/{achievementid}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/achievements/{scid}/{achievementid}
Returns details about the achievement, including its configured metadata and user-specific data. 

> [!NOTE] 
> Only supported for the platform. 

 
The domain for these URIs is `achievements.xboxlive.com`.
 
  * [URI parameters](#ID4E2)
 
<a id="ID4E2"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | --- | 
| xuid| 64-bit unsigned integer| Xbox User ID (XUID) of the user whose resource is being accessed. Must match the XUID of the authenticated user.| 
| scid| GUID| Unique identifier of the service configuration whose achievement is being accessed.| 
| achievementid| 32-bit unsigned integer| Unique (within the specified SCID) identifier of the achievement that is being accessed.| 
  
<a id="ID4EMC"></a>

 
## Valid methods

[GET (/users/xuid({xuid})/achievements/{scid}/{achievementid})](uri-usersxuidachievementsscidachievementidget.md)

&nbsp;&nbsp;Gets the details of the Achievement.
 
<a id="ID4EWC"></a>

 
## See also
 
<a id="ID4EYC"></a>

 
##### Parent 

[Achievements URIs](atoc-reference-achievementsv2.md)

   