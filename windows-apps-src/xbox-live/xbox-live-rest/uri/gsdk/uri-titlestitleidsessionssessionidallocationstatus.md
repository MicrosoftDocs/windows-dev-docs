---
title: /titles/{titleId}/sessions/{sessionId}/allocationStatus
assetID: 55611f4b-4ba4-fa9a-ce44-fcc4a6df1b35
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidsessionssessionidallocationstatus.html
author: KevinAsgari
description: ' /titles/{titleId}/sessions/{sessionId}/allocationStatus'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /titles/{titleId}/sessions/{sessionId}/allocationStatus
For the given title id and session id, get status of the ticket request. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
  * [URI Parameters](#ID4EU)
  * [Host Name](#ID4EPB)
  * [Valid Methods](#ID4EWB)
 
<a id="ID4EU"></a>

 
## URI Parameters
 
| Parameter| Description| 
| --- | --- | 
| titleId| ID of the title that the request should operate on.| 
| sessionId| the ID of the session to look up.| 
  
<a id="ID4EPB"></a>

 
## Host Name
 
gameserverms.xboxlive.com
  
<a id="ID4EWB"></a>

 
## Valid Methods
  
[GET](uri-titlestitleidsessionssessionidallocationstatus-get.md)
 
&nbsp;&nbsp;Returns the allocation status of the sessionhost identified by its sessionId.
   