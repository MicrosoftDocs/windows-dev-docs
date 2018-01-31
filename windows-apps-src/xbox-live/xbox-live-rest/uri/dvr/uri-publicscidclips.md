---
title: /public/scids/{scid}/clips
assetID: 55a1f0ae-08bb-6d1e-a1da-cbeb481c42ee
permalink: en-us/docs/xboxlive/rest/uri-publicscidclips.html
author: KevinAsgari
description: ' /public/scids/{scid}/clips'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /public/scids/{scid}/clips
Access public clips. This URI actually can be specified in two forms, `/public/scids/{scid}/clips` and `/public/titles/{titleId}/clips`. See below for more details. 
The domain for this URI is `gameclipsmetadata.xboxlive.com`.
 
  * [URI parameters](#ID4E1)
 
<a id="ID4E1"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| string| The primary service configuration identifier of the public clips.| 
| titleid| string| The titleId of the public clips. Cannot be specified in the same URI as the SCID. If specified, will be used to look up the primary SCID.| 
  
<a id="ID4E6B"></a>

 
## Valid methods

[GET (/public/scids/{scid}/clips)](uri-publicscidclipsget.md)

&nbsp;&nbsp;List public clips.
 
<a id="ID4EJC"></a>

 
## See also
 
<a id="ID4ELC"></a>

 
##### Parent 

[Marketplace URIs](../marketplace/atoc-reference-marketplace.md)

   