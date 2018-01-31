---
title: Game DVR URIs
assetID: 472f705e-bf28-7894-b1ba-80933d8746a6
permalink: en-us/docs/xboxlive/rest/atoc-reference-dvr.html
author: KevinAsgari
description: ' Game DVR URIs'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Game DVR URIs
 
This section provides detail about the Universal Resource Identifier (URI) addresses and associated Hypertext Transport Protocol (HTTP) methods from Xbox Live Services for *game DVR*.
 
Only consoles can record a game clip, but any device that can access can display a clip.
 
Depending on the function of the URI in question, the domains for these URIs are:
 
   *  gameclipsmetadata.xboxlive.com 
   *  gameclipstransfer.xboxlive.com 
  
<a id="ID4EZB"></a>

 
## In this section

[/public/scids/{scid}/clips](uri-publicscidclips.md)

&nbsp;&nbsp;Access public clips. This URI actually can be specified in two forms, `/public/scids/{scid}/clips` and `/public/titles/{titleId}/clips`. See below for more details.

[/{uri}](uri-uri.md)

&nbsp;&nbsp;Access game clip data.

[/users/me/scids/{scid}/clips](uri-usersmescidclips.md)

&nbsp;&nbsp;Access an initial upload request.

[/users/me/scids/{scid}/clips/{gameClipId}](uri-usersmescidclipsgameclipid.md)

&nbsp;&nbsp;Access game clip data and metadata.

[/users/{ownerId}/clips](uri-usersowneridclips.md)

&nbsp;&nbsp;Access list of user's clips.

[/users/{ownerId}/scids/{scid}/clips/{gameClipId}](uri-usersowneridscidclipsgameclipid.md)

&nbsp;&nbsp;Access a single game clip from the system if all the IDs to locate it are known.
 
<a id="ID4EOC"></a>

 
## See also
 
<a id="ID4EQC"></a>

 
##### Parent 

[Universal Resource Identifier (URI) Reference](../atoc-xboxlivews-reference-uris.md)

   