---
title: /titles/{titleId}/variants
assetID: bca30c8f-1f09-729f-4955-38b7809404eb
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidvariants.html
author: KevinAsgari
description: ' /titles/{titleId}/variants'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /titles/{titleId}/variants
URI called by a client to get the available variants for a title. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
  * [URI Parameters](#ID4EU)
  * [Host Name](#ID4EIB)
  * [Valid Methods](#ID4EPB)
 
<a id="ID4EU"></a>

 
## URI Parameters
 
| Parameter| Description| 
| --- | --- | 
| titleid| ID of the title that the request should operate on.| 
  
<a id="ID4EIB"></a>

 
## Host Name
 
gameserverds.xboxlive.com
  
<a id="ID4EPB"></a>

 
## Valid Methods
  
[POST](uri-titlestitleidvariants-post.md)
 
&nbsp;&nbsp;URI called by a client that retrieves a list of game variants for the specified title Id.
   