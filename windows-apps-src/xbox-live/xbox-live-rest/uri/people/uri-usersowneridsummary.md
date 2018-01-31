---
title: /users/{ownerId}/summary
assetID: 63f8ed09-532d-381e-59e6-2849893df5bf
permalink: en-us/docs/xboxlive/rest/uri-usersowneridsummary.html
author: KevinAsgari
description: ' /users/{ownerId}/summary'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/{ownerId}/summary
Accesses summary data about the owner from the caller's perspective.

  * [URI parameters](#ID4EQ)

<a id="ID4EQ"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| ownerId| string| Identifier of the user whose resource is being accessed. The possible values are "me", xuid({xuid}), or gt({gamertag}). Example values: <code>me</code>, <code>xuid(2603643534573581)</code>, <code>gt(SomeGamertag)</code>|

<a id="ID4ESB"></a>


## Valid methods

[GET (/users/{ownerId}/summary)](uri-usersowneridsummaryget.md)

&nbsp;&nbsp;Gets summary data about the owner from the caller's perspective.

<a id="ID4E3B"></a>


## See also

<a id="ID4E5B"></a>


##### Parent

[/users/{ownerId}/summary]()
