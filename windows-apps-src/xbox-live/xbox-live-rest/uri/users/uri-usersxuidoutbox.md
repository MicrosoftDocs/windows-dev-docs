---
title: /users/xuid({xuid})/outbox
assetID: 0b66b885-15ff-be55-f8be-e6e9d85d087e
permalink: en-us/docs/xboxlive/rest/uri-usersxuidoutbox.html
author: KevinAsgari
description: ' /users/xuid({xuid})/outbox'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/outbox
Provides send-only access to a user's messaging outbox for Xbox LIVE Services. 
The domain for these URIs is `msg.xboxlive.com`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters 
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid | unsigned 64-bit integer | The Xbox User ID (XUID) of the player who is making the request. | 
  
<a id="ID4EXB"></a>

 
## Valid methods 

[POST (/users/xuid({xuid})/outbox)](uri-usersxuidoutboxpost.md)

&nbsp;&nbsp;Sends a specified message to a list of recipients. 
 
<a id="ID4EFC"></a>

 
## See also
 
<a id="ID4EHC"></a>

 
##### Parent  

[Users URIs](atoc-reference-users.md)

   