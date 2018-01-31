---
title: /users/{ownerId}/scids/{scid}/clips/{gameClipId}
assetID: 49b68418-71f1-c5a2-3a9b-869fd1fa663c
permalink: en-us/docs/xboxlive/rest/uri-usersowneridscidclipsgameclipid.html
author: KevinAsgari
description: ' /users/{ownerId}/scids/{scid}/clips/{gameClipId}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/{ownerId}/scids/{scid}/clips/{gameClipId}
Access a single game clip from the system if all the IDs to locate it are known. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [URI parameters](#ID4EX)
 
<a id="ID4EX"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| ownerId| string| User identity of the user whose resource is being accessed. Supported formats: "me" or "xuid(123456789)". Maximum length: 16.| 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
| gameClipId| string| GameClip ID of the resource that is being accessed.| 
  
<a id="ID4EFC"></a>

 
## Valid methods

[GET (/users/{ownerId}/scids/{scid}/clips/{gameClipId})](uri-usersowneridscidclipsgameclipidget.md)

&nbsp;&nbsp;Get a single game clip from the system if all the IDs to locate it are known.
 
<a id="ID4EPC"></a>

 
## See also
 
<a id="ID4ERC"></a>

 
##### Parent 

[Game DVR URIs](atoc-reference-dvr.md)

   