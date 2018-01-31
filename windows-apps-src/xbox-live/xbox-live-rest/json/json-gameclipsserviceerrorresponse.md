---
title: GameClipsServiceErrorResponse (JSON)
assetID: dd606f0f-d52d-f88f-0fff-41c15837f9ed
permalink: en-us/docs/xboxlive/rest/json-gameclipsserviceerrorresponse.html
author: KevinAsgari
description: ' GameClipsServiceErrorResponse (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GameClipsServiceErrorResponse (JSON)
An optional part of the response to the /users/{ownerId}/scids/{scid}/clips/{gameClipId}/uris/format/{gameClipUriType} API. 
<a id="ID4EN"></a>

 
## GameClipsServiceErrorResponse
 
The GameClipsServiceErrorResponse object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| <b>errorSource</b>| string| Source of the error.| 
| <b>errorResponseCode</b>| 32-bit signed integer| Code associated with the error (can be null).| 
| <b>errorMessage</b>| string| Additional details about the error.| 
  
<a id="ID4ECC"></a>

 
## See also
 
<a id="ID4EEC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   