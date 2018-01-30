---
title: /users/{ownerId}/clips
assetID: cc200b89-dc63-9ab5-b037-7a910f46dae9
permalink: en-us/docs/xboxlive/rest/uri-usersowneridclips.html
author: KevinAsgari
description: ' /users/{ownerId}/clips'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/{ownerId}/clips
Access list of user's clips. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [URI parameters](#ID4EX)
 
<a id="ID4EX"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| ownerId| string| User identity of the user whose resource is being accessed. Supported formats: "me" or "xuid(123456789)". Maximum length: 16.| 
  
<a id="ID4EVB"></a>

 
## Valid methods

[GET (/users/{ownerId}/clips)](uri-usersowneridclipsget.md)

&nbsp;&nbsp;Retrieve list of user's clips.
 
<a id="ID4E6B"></a>

 
## See also
 
<a id="ID4EBC"></a>

 
##### Parent 

[Game DVR URIs](atoc-reference-dvr.md)

   