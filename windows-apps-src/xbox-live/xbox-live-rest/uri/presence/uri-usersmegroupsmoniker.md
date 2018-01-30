---
title: /users/me/groups/{moniker}
assetID: 3d319a19-da5e-a485-985d-46dcff4bb521
permalink: en-us/docs/xboxlive/rest/uri-usersmegroupsmoniker.html
author: KevinAsgari
description: ' /users/me/groups/{moniker}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/me/groups/{moniker}
Accesses the PresenceRecord for my group. 
The domain for these URIs is `userpresence.xboxlive.com`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| moniker| string| String defining the group of users. The only accepted moniker at present is "People", with a capital 'P'.| 
  
<a id="ID4ERB"></a>

 
## Valid methods

[GET (/users/me/groups/{moniker} )](uri-usersmegroupsmonikerget.md)

&nbsp;&nbsp;Gets the PresenceRecord for my group.
 
<a id="ID4E2B"></a>

 
## See also
 
<a id="ID4E4B"></a>

 
##### Parent 

[Presence URIs](atoc-reference-presence.md)

   