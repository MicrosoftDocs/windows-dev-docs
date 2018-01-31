---
title: /serviceconfigs/{scid}/hoppers/{hoppername}
assetID: ba1e129d-b4c4-6535-46ce-fd184465c85f
permalink: en-us/docs/xboxlive/rest/uri-serviceconfigsscidhoppershoppername.html
author: KevinAsgari
description: ' /serviceconfigs/{scid}/hoppers/{hoppername}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /serviceconfigs/{scid}/hoppers/{hoppername}

Supports a POST operation to create match tickets.

> [!IMPORTANT]
> This URI is intended for use with contract 103 or later, and requires a header element of X-Xbl-Contract-Version: 103 or later on every request.

<a id="ID4ER"></a>


## Domain
momatch.xboxlive.com  
<a id="ID4EW"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- | --- |
| scid| GUID| The service configuration identifier (SCID) for the session.|
| hoppername | string | The name of the hopper. |

<a id="ID4E2B"></a>


## Valid methods

[POST (/serviceconfigs/{scid}/hoppers/{hoppername})](uri-serviceconfigsscidhoppershoppernamepost.md)

&nbsp;&nbsp;Creates the specified match ticket.

<a id="ID4EFC"></a>


## See also

<a id="ID4EHC"></a>


##### Parent  

[Matchmaking URIs](atoc-reference-matchtickets.md)
