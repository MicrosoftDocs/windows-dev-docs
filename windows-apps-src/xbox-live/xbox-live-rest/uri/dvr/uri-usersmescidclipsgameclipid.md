---
title: /users/me/scids/{scid}/clips/{gameClipId}
assetID: f5bead69-4fc9-f551-39cb-c8754645ac88
permalink: en-us/docs/xboxlive/rest/uri-usersmescidclipsgameclipid.html
author: KevinAsgari
description: ' /users/me/scids/{scid}/clips/{gameClipId}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/me/scids/{scid}/clips/{gameClipId}
Access game clip data and metadata. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [URI parameters](#ID4EX)
 
<a id="ID4EX"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
| gameClipId| string| GameClip ID of the resource that is being accessed.| 
  
<a id="ID4E3B"></a>

 
## Valid methods

[DELETE (/users/me/scids/{scid}/clips/{gameClipId})](uri-usersmescidclipsgameclipiddelete.md)

&nbsp;&nbsp;Delete game clip

[POST (/users/me/scids/{scid}/clips/{gameClipId})](uri-usersmescidclipsgameclipidpost.md)

&nbsp;&nbsp;Update game clip metadata for the user's own data.
 
<a id="ID4EJC"></a>

 
## See also
 
<a id="ID4ELC"></a>

 
##### Parent 

[Game DVR URIs](atoc-reference-dvr.md)

   