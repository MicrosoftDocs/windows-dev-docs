---
title: /users/me/scids/{scid}/clips
assetID: ed8317f7-7898-47ad-d18d-cd5150daf293
permalink: en-us/docs/xboxlive/rest/uri-usersmescidclips.html
author: KevinAsgari
description: ' /users/me/scids/{scid}/clips'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/me/scids/{scid}/clips
Access an initial upload request. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [URI parameters](#ID4EX)
 
<a id="ID4EX"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
  
<a id="ID4ETB"></a>

 
## Valid methods

[POST (/users/me/scids/{scid}/clips)](uri-usersmescidclipspost.md)

&nbsp;&nbsp;Make an initial upload request.
 
<a id="ID4E4B"></a>

 
## See also
 
<a id="ID4E6B"></a>

 
##### Parent 

[Game DVR URIs](atoc-reference-dvr.md)

   