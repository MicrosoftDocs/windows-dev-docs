---
title: /handles/{handleId}
assetID: 5b722d3e-fe80-fec5-a26b-8b3db6422004
permalink: en-us/docs/xboxlive/rest/uri-handleshandleid.html
author: KevinAsgari
description: ' /handles/{handleId}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /handles/{handleId}
Supports DELETE and GET operations for session handles specified by identifier. 

> [!NOTE] 
> This URI is used by 2015 Multiplayer and applies only to that multiplayer version and later. It is intended for use with template contract 104/105 or later.  

 
<a id="ID4EQ"></a>

 
## Domain
sessiondirectory.xboxlive.com  
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | --- | 
| handleId| GUID| The unique ID of the handle for the session.| 
  
<a id="ID4ERB"></a>

 
## Valid methods

[DELETE (/handles/{handleId})](uri-handleshandleiddelete.md)

&nbsp;&nbsp;Deletes handles specified by handle ID.

[GET (/handles/{handle-id})](uri-handleshandleidget.md)

&nbsp;&nbsp;Retrieves handles specified by handle ID.
 
<a id="ID4E4B"></a>

 
## See also
 
<a id="ID4E6B"></a>

 
##### Parent 

[Session Directory URIs](atoc-reference-sessiondirectory.md)

   