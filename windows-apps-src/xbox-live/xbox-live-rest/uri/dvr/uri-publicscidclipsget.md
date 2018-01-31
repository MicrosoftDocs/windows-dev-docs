---
title: GET (/public/scids/{scid}/clips)
assetID: 15b3e873-1f96-b1da-2f79-6dac1369a4c0
permalink: en-us/docs/xboxlive/rest/uri-publicscidclipsget.html
author: KevinAsgari
description: ' GET (/public/scids/{scid}/clips)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GET (/public/scids/{scid}/clips)
List public clips. 
The domain for this URI is `gameclipsmetadata.xboxlive.com`.
 
  * [Remarks](#ID4EV)
  * [URI parameters](#ID4ECB)
  * [Query string parameters](#ID4ENB)
 
<a id="ID4EV"></a>

 
## Remarks
 
This API allows for various ways to list clips that are public. The list of clips is returned based on privacy checks and content isolation checks against the requesting XUID.
 
Queries are optimized per service configuration identifier (SCID). Specifying further filters or sort orders other than the defaults listed below can in some circumstances take longer to return. This is more evident for larger sets of videos. Queries cannot specify an ascending sort order.
 
The qualifier is required to get to the specific collection ofpublic clips. The requesting user must have access to the requested SCID, otherwise HTTP 403 will be returned.
  
<a id="ID4ECB"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| string| The primary service configuration identifier of the public clips.| 
| titleid| string| The titleId of the public clips. Cannot be specified in the same URI as the SCID. If specified, will be used to look up the primary SCID.| 
  
<a id="ID4ENB"></a>

 
## Query string parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | --- | --- | --- | 
| <b>?achievementId={achievementId}</b>| Most recent clips matching the specified <b>achievementId</b>.| Additional sorting/filtering is not supported.| 
| <b>?greatestMomentId={greatestMomentId}</b>| Most recent clips matching the specified <b>greatestMomentId</b>.| Additional sorting/filtering is not supported.| 
| <b>?qualifier=created </b>| Most Recent| Required.| 
  
<a id="ID4EDD"></a>

 
## See also
 
<a id="ID4EFD"></a>

 
##### Parent 

[/public/scids/{scid}/clips](uri-publicscidclips.md)

   