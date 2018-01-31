---
title: /serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/batch
assetID: 4f8e1ece-2ba8-9ea4-e551-2a69c499d7b9
permalink: en-us/docs/xboxlive/rest/uri-serviceconfigscidsessiontemplatessessiontemplatenamebatch.html
author: KevinAsgari
description: ' /serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/batch'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/batch
Supports a POST operation to create a batch query at the session template level.

> [!IMPORTANT]
> This method is used by the 2015 Multiplayer and applies only to that multiplayer version and later. It is intended for use with template contract 104/105 or later, and requires a header element of X-Xbl-Contract-Version: 104/105 or later on every request.

<a id="ID4ER"></a>


## Domain
sessiondirectory.xboxlive.com  
<a id="ID4EW"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- | --- |
| scid| GUID| Service configuration identifier (SCID). Part 1 of the session identifier.|
| sessionTemplateName| string| Name of the current instance of the session template. Part 2 of the session identifier.|

<a id="ID4E2B"></a>


## Valid methods

[POST (/serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/batch)](uri-serviceconfigscidsessiontemplatessessiontemplatenamebatchpost.md)

&nbsp;&nbsp;Creates a batch query on multiple Xbox user IDs.

<a id="ID4EFC"></a>


## See also

<a id="ID4EHC"></a>


##### Parent

[Session Directory URIs](atoc-reference-sessiondirectory.md)
