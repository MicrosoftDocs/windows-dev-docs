---
title: /titles/{titleId}/sessionhosts
assetID: 92d9bdd2-5c8f-761b-3f9a-50f8db7b843c
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidsessionhosts.html
author: KevinAsgari
description: ' /titles/{titleId}/sessionhosts'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /titles/{titleId}/sessionhosts
Requests a Xbox Live Compute sessionhost to be allocated for a given title id. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
  * [URI Parameters](#ID4EU)
  * [Host Name](#ID4EIB)
  * [Valid Methods](#ID4EPB)
 
<a id="ID4EU"></a>

 
## URI Parameters
 
| Parameter| Description| 
| --- | --- | 
| titleId| ID of the title that the request should operate on.| 
  
<a id="ID4EIB"></a>

 
## Host Name
 
gameserverms.xboxlive.com
  
<a id="ID4EPB"></a>

 
## Valid Methods
  
[POST](uri-titlestitleidsessionhosts-post.md)
 
&nbsp;&nbsp;Create new cluster request.
   