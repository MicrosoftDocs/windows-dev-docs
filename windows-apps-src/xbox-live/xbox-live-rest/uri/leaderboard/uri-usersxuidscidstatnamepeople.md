---
title: /users/xuid({xuid})/scids/{scid}/stats/{statname)/people/{all|favorite}
assetID: 0983dad0-59b7-45b7-505d-603e341fe0cc
permalink: en-us/docs/xboxlive/rest/uri-usersxuidscidstatnamepeople.html
author: KevinAsgari
description: ' /users/xuid({xuid})/scids/{scid}/stats/{statname)/people/{all|favorite}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/scids/{scid}/stats/{statname)/people/{all|favorite}
Accesses a social (ranked) leaderboard.
The domain for these URIs is `leaderboards.xboxlive.com`.

  * [URI parameters](#ID4EV)

<a id="ID4EV"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| xuid| string| Identifier of the user.|
| scid| string| Identifier of the service configuration that contains the resource being accessed.|
| statname| string| Unique identifier of the user stat resource being accessed.|
| all\|favorite| enumeration| Whether to rank the stat values (scores) for all known contacts of the current user or only those contacts designated as favorite people by that user.|

<a id="ID4EOC"></a>


## Valid methods

[GET (/users/xuid({xuid})/scids/{scid}/stats/{statname)/people/{all\|favorite})](uri-usersxuidscidstatnamepeopleget.md)

&nbsp;&nbsp;Returns a social leaderboard by ranking the stat values (scores) for either all known contacts of the current user or only those contacts designated as favorite people by that user.

<a id="ID4EYC"></a>


## See also

<a id="ID4E1C"></a>


##### Parent

[Leaderboards URIs](atoc-reference-leaderboard.md)
