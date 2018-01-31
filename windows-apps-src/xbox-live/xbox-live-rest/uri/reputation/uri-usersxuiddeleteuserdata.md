---
title: /users/xuid({xuid})/deleteuserdata
assetID: 1925da6f-f6c1-ae5b-5af9-e143b70e6717
permalink: en-us/docs/xboxlive/rest/uri-usersxuiddeleteuserdata.html
author: KevinAsgari
description: ' /users/xuid({xuid})/deleteuserdata'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/xuid({xuid})/deleteuserdata
Completely resets the reputation data for a test user. For testing only. 
The domain for these URIs is `reputation.xboxlive.com`. This URI is always called on port 10443.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid| 64-bit unsigned integer| Xbox User ID (XUID) of the user whose data is being deleted.| 
  
<a id="ID4EYB"></a>

 
## Valid methods

[POST (/users/xuid({xuid})/deleteuserdata)](uri-usersxuiddeleteuserdatapost.md)

&nbsp;&nbsp;Completely resets the reputation data for a test user. For testing only.
 
<a id="ID4ECC"></a>

 
## See also
 
<a id="ID4EEC"></a>

 
##### Parent 

[Reputation URIs](atoc-reference-reputation.md)

   