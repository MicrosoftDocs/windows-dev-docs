---
title: Reputation URIs
assetID: d1cb76c0-86a4-8c51-19f6-5f223b517d46
permalink: en-us/docs/xboxlive/rest/atoc-reference-reputation.html
author: KevinAsgari
description: ' Reputation URIs'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Reputation URIs
 
This section provides detail about Universal Resource Identifier (URI) addresses and associated Hypertext Transport Protocol (HTTP) methods from Xbox Live Services for the **Microsoft.Xbox.Services.Social.ReputationService**. The domain for the reputation URIs is reputation.xboxlive.com. A typical URI representation might be https://reputation.xboxlive.com/users/xuid(2533274790412952)/feedback. 
 
The reputation service uses feedback, as described in [Feedback (JSON)](../../json/json-feedback.md), to calculate a reputation score. This score is saved in the statistics area for the user under the key ReputationOverall. For more information about retrieving user statistics, see [GET (/users/xuid({xuid})/scids/{scid}/stats)](../userstats/uri-usersxuidscidsscidstatsget.md). 
 
Games on all platforms can use the reputation service.
 
<a id="ID4EMB"></a>

 
## In this section

[/users/xuid({xuid})/feedback](uri-reputationusersxuidfeedback.md)

&nbsp;&nbsp;Used from your title if you desire to add a feedback option in your game, as opposed to using the shell.

[/users/batchfeedback](uri-reputationusersbatchfeedback.md)

&nbsp;&nbsp;Used by your title's service to send feedback in batch form outside of your title's interface.

[/users/me/resetreputation](uri-usersmeresetreputation.md)

&nbsp;&nbsp;Enables the Enforcement team to access the current user's Reputation scores.

[/users/xuid({xuid})/deleteuserdata](uri-usersxuiddeleteuserdata.md)

&nbsp;&nbsp;Completely resets the reputation data for a test user. For testing only.

[/users/xuid({xuid})/resetreputation](uri-usersxuidresetreputation.md)

&nbsp;&nbsp;Enables the Enforcement team to access the specified user's Reputation scores.
 
<a id="ID4E5B"></a>

 
## See also
 
<a id="ID4EAC"></a>

 
##### Parent 

[Universal Resource Identifier (URI) Reference](../atoc-xboxlivews-reference-uris.md)

   