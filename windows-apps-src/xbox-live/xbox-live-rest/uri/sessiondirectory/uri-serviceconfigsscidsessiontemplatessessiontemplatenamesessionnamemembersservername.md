---
title: /serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/sessions/{sessionName}/servers/{server-name}
assetID: aed0764f-4e3d-e0b3-1ea0-543c32f3f822
permalink: en-us/docs/xboxlive/rest/uri-serviceconfigsscidsessiontemplatessessiontemplatenamesessionnamemembersservername.html
author: KevinAsgari
description: ' /serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/sessions/{sessionName}/servers/{server-name}'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/sessions/{sessionName}/servers/{server-name}
Supports a DELETE operation to remove the specified server of a session.
<a id="ID4EO"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| scid| GUID| Service configuration identifier (SCID). Part 1 of the session identifier.|
| sessionTemplateName| string| Name of the current instance of the session template. Part 2 of the session identifier.|
| sessionName| GUID| Unique ID of the session. Part 3 of the session identifier.| 

<a id="ID4E3B"></a>


## Valid methods

[DELETE (/serviceconfigs/{scid}/sessiontemplates/{sessionTemplateName}/sessions/{sessionName}/servers/{server-name})](uri-serviceconfigsscidsessiontemplatessessiontemplatenamesessionnamemembersservernamedelete.md)

&nbsp;&nbsp;Removes the specified server from a session.

<a id="ID4EGC"></a>


## See also

<a id="ID4EIC"></a>


##### Parent

[Session Directory URIs](atoc-reference-sessiondirectory.md)
