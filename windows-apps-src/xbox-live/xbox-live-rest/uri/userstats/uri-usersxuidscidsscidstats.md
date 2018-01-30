---
title: /users/xuid({xuid})/scids/{scid}/stats
assetID: 3cf9ffd4-9a8b-2658-402b-2e933f7f6f1b
permalink: en-us/docs/xboxlive/rest/uri-usersxuidscidsscidstats.html
author: KevinAsgari
description: ' /users/xuid({xuid})/scids/{scid}/stats'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/scids/{scid}/stats
Accesses a service configuration scoped by a comma-delimited list of user statistic names on behalf of the specified user. 
The domain for these URIs is `userstats.xboxlive.com`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid| GUID| Xbox User ID (XUID) of the user on whose behalf to access the service configuration.| 
| scid| GUID| Identifier of the service configuration that contains the resource being accessed.| 
  
<a id="ID4E4B"></a>

 
## Valid methods

[GET](uri-usersxuidscidsscidstatsget.md)

&nbsp;&nbsp;Gets a service configuration scoped by a comma-delimited list of user statistic names on behalf of the specified user.

[GET with value metadata](uri-usersxuidscidsscidstatsgetvaluemetadata.md)

&nbsp;&nbsp;Gets a list of specified statistics, including metadata associated with the statistic values, for a user in a specified service configuration.
 
<a id="ID4EKC"></a>

 
## See also
 
<a id="ID4EMC"></a>

 
##### Parent 

[User Statistics URIs](atoc-reference-userstats.md)

   