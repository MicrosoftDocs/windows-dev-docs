---
title: /users/xuid({xuid})/inbox
assetID: 352740c6-42e2-0000-495d-bf384dc3e941
permalink: en-us/docs/xboxlive/rest/uri-usersxuidinbox.html
author: KevinAsgari
description: ' /users/xuid({xuid})/inbox'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/inbox
Provides access to a user's messaging inbox for Xbox LIVE Services. 
The domain for these URIs is `msg.xboxlive.com`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters 
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid | unsigned 64-bit integer | The Xbox User ID (XUID) of the player who is making the request. | 
| messageId | string[50] | ID of the message being retrieved or deleted. | 
  
<a id="ID4EDC"></a>

 
## Valid methods 

[GET (/users/xuid({xuid})/inbox)](uri-usersxuidinboxget.md)

&nbsp;&nbsp;Retrieves a specified number of user message summaries from the service. 

[DELETE (/users/xuid({xuid})/inbox/{messageId})](uri-usersxuidinboxmessageiddelete.md)

&nbsp;&nbsp;Deletes a user message in the user's inbox.

[GET (/users/xuid({xuid})/inbox/{messageId})](uri-usersxuidinboxmessageidget.md)

&nbsp;&nbsp;Retrieves the detailed message text for a particular user message, marking it as read on the service. 
 
<a id="ID4EVC"></a>

 
## See also
 
<a id="ID4EXC"></a>

 
##### Parent  

[Users URIs](atoc-reference-users.md)

   