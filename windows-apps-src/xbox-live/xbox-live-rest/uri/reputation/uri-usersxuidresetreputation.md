---
title: /users/xuid({xuid})/resetreputation
assetID: 85c74beb-a12f-4015-e244-36942e366afc
permalink: en-us/docs/xboxlive/rest/uri-usersxuidresetreputation.html

description: ' /users/xuid({xuid})/resetreputation'
ms.date: 10/12/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
---
# /users/xuid({xuid})/resetreputation
Enables the Enforcement team to access the specified user's Reputation scores. 
The domain and port number for these URIs is `reputation.xboxlive.com:10433`.
 
  * [URI parameters](#ID4EV)
 
<a id="ID4EV"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid| string| The Xbox User ID (XUID) of the specified user.| 
  
<a id="ID4EVB"></a>

 
## Valid methods

[POST (/users/xuid({xuid})/resetreputation)](uri-usersxuidresetreputationpost.md)

&nbsp;&nbsp;Enables the Enforcement team to set the specified user's Reputation Scores to some arbitrary values after (for example) an account hijacking.
 
<a id="ID4E6B"></a>

 
## See also
 
<a id="ID4EBC"></a>

 
##### Parent 

[Reputation URIs](atoc-reference-reputation.md)

   