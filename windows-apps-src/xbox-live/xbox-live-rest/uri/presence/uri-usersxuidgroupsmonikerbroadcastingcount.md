---
title: /users/xuid({xuid})/groups/{moniker}/broadcasting/count
assetID: 535c8d46-7001-c31e-3e9d-82ad275095ae
permalink: en-us/docs/xboxlive/rest/uri-usersxuidgroupsmonikerbroadcastingcount.html
author: KevinAsgari
description: ' /users/xuid({xuid})/groups/{moniker}/broadcasting/count'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/groups/{moniker}/broadcasting/count
Accesses the count of the broadcasting users specified by the groups moniker related to the XUID that appears in the URI. 
The domain for these URIs is `userpresence.xboxlive.com`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid| string| Xbox User ID (XUID) of the user related to the XUIDs in the Group.| 
| moniker| string| String defining the group of users. The only accepted moniker at present is "People", with a capital 'P'.| 
  
<a id="ID4E4B"></a>

 
## Valid methods

[GET (/users/xuid({xuid})/groups/{moniker}/broadcasting/count )](uri-usersxuidgroupsmonikerbroadcastingcountget.md)

&nbsp;&nbsp;Retrieves the count of the broadcasting users specified by the groups moniker related to the XUID that appears in the URI.
 
<a id="ID4EHC"></a>

 
## See also
 
<a id="ID4EJC"></a>

 
##### Parent 

[Presence URIs](atoc-reference-presence.md)

   