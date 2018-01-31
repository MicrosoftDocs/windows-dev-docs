---
title: /users/{ownerId}/people/avoid
assetID: 13dc3a10-ed04-4600-3afb-aa95a4139a06
permalink: en-us/docs/xboxlive/rest/uri-privacyusersxuidpeopleavoid.html
author: KevinAsgari
description: ' /users/{ownerId}/people/avoid'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/{ownerId}/people/avoid
Accesses the Avoid list for a user

  * [URI parameters](#ID4EQ)

<a id="ID4EQ"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| ownerId| string| Required. Identifier of the user whose resource is being accessed. The possible values are <code>xuid({xuid})</code>. Must be the authenticated user. Example value: <code>xuid(2603643534573581)</code>. Maximum size: none. |

<a id="ID4ERB"></a>


## Valid methods

[GET (/users/{ownerId}/people/avoid)](uri-privacyusersxuidpeopleavoidget.md)

&nbsp;&nbsp;Gets the Avoid list for a user.

<a id="ID4E2B"></a>


## See also

<a id="ID4E4B"></a>


##### Parent

[Privacy URIs](atoc-reference-privacyv2.md)
