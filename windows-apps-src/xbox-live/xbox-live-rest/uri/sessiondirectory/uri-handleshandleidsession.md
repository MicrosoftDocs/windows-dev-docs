---
title: /handles/{handleId}/session
assetID: 4ed2dcf5-5d1f-91ce-4a3f-eb3ba68727bf
permalink: en-us/docs/xboxlive/rest/uri-handleshandleidsession.html
author: KevinAsgari
description: ' /handles/{handleId}/session'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /handles/{handleId}/session
Supports PUT and GET operations for a session, using handle dereferencing. 

> [!NOTE] 
> This URI is used by 2015 Multiplayer and applies only to that multiplayer version and later. It is intended for use with template contract 104/105 or later.  

 

> [!NOTE] 
> This URI is currently only externally accessible by Xbox One consoles and servers using a service identifier.  

 
<a id="ID4ES"></a>

 
## Domain
sessiondirectory.xboxlive.com  
<a id="ID4EX"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | --- | --- | 
| handleId| GUID| The unique ID of the handle for the session.| 
  
<a id="ID4ESB"></a>

 
## Valid methods

[GET (/handles/{handleId}/session)](uri-handleshandleidsessionget.md)

&nbsp;&nbsp;Gets a session object for the specified handle identifier. 

[PUT (/handles/{handle-id}/session)](uri-handleshandleidsessionput.md)

&nbsp;&nbsp;Creates or updates a session by dereferencing a handle.
 
<a id="ID4E6B"></a>

 
## See also
 
<a id="ID4EBC"></a>

 
##### Parent 

[Session Directory URIs](atoc-reference-sessiondirectory.md)

   