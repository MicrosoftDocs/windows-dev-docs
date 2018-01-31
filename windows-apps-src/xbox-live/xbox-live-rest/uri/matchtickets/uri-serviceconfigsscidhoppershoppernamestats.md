---
title: /serviceconfigs/{scid}/hoppers/{name}/stats
assetID: 56bb4398-445b-e8c5-a4ce-1651576ee7e7
permalink: en-us/docs/xboxlive/rest/uri-serviceconfigsscidhoppershoppernamestats.html
author: KevinAsgari
description: ' /serviceconfigs/{scid}/hoppers/{name}/stats'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /serviceconfigs/{scid}/hoppers/{name}/stats

Supports a GET operation for retrieving statistics for a hopper.

> [!IMPORTANT]
> This URI is intended for use with contract 103 or later, and requires a header element of X-Xbl-Contract-Version: 103 or later on every request.

<a id="ID4ER"></a>


## Domain
momatch.xboxlive.com  
<a id="ID4EW"></a>


## Remarks
This URI supports the values xuid, gt, and me for the owner identifier in configuration of the target user. Only a ticket's creator can delete the ticket or retrieve the status of the URI.  
<a id="ID4E6"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- | --- |
| scid| GUID| The service configuration identifier (SCID) for the session.|
| name| string| The name of the hopper.|

<a id="ID4EEC"></a>


## Valid methods

[GET (/serviceconfigs/{scid}/hoppers/{name}/stats)](uri-serviceconfigsscidhoppershoppernamestatsget.md)

&nbsp;&nbsp;Gets the statistics for a hopper.

<a id="ID4EQC"></a>


## See also

<a id="ID4ESC"></a>


##### Parent  

[Matchmaking URIs](atoc-reference-matchtickets.md)
