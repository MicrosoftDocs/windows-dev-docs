---
title: Game Server Universal Resource Identifier (URI) Reference
assetID: bbd7e3f3-77ac-6ffd-8951-fe4b8b48eb4c
permalink: en-us/docs/xboxlive/rest/atoc-gsdk-uri-reference.html
author: KevinAsgari
description: ' Game Server Universal Resource Identifier (URI) Reference'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Game Server Universal Resource Identifier (URI) Reference
URIs used by clients to create Game Server Development Kit server instances for a title. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
<a id="ID4EY"></a>

 
## In this section

[/qosservers](uri-qosservers.md)

&nbsp;&nbsp;URI called by a client to get the list of QoS servers available for use with Xbox Live Compute.

[/titles/{titleId}/clusters](uri-titlestitleidclusters.md)

&nbsp;&nbsp;URI that allows a client to create an Xbox Live Compute server instance for a title.

[/titles/{titleId}/variants](uri-titlestitleidvariants.md)

&nbsp;&nbsp;URI called by a client to get the available variants for a title.

[/titles/{titleId}/sessionhosts](uri-titlestitleidsessionhosts.md)

&nbsp;&nbsp;Requests a Xbox Live Compute sessionhost to be allocated for a given title id.

[/titles/{titleId}/sessions/{sessionId}/allocationStatus](uri-titlestitleidsessionssessionidallocationstatus.md)

&nbsp;&nbsp;For the given title id and session id, get status of the ticket request.
 