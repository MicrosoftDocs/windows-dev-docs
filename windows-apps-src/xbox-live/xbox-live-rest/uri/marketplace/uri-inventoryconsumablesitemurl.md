---
title: /users/me/consumables/{itemID}
assetID: 45724827-5e35-326f-3f17-f49e606d9e08
permalink: en-us/docs/xboxlive/rest/uri-inventoryconsumablesitemurl.html
author: KevinAsgari
description: RESTful endpoint for Xbox consumable items for a user.
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# /users/me/consumables/{itemID}
Accesses the full set of details for a specific consumable inventory item.
The domain for these URIs is `inventory.xboxlive.com`.

  * [URI parameters](#ID4EV)

<a id="ID4EV"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- |
| itemID| string| the ID unique to each user for a singular inventory item|

<a id="ID4ERB"></a>


## Valid methods

[POST ({itemID})](uri-inventoryconsumablesitemurlpost.md)

&nbsp;&nbsp;Indicates that all or a portion of a consumable inventory item has been used and decrements the quantity of the consumable by the requested amount.

<a id="ID4E4B"></a>


## See also

<a id="ID4E6B"></a>


##### Parent

[Marketplace URIs](atoc-reference-marketplace.md)


<a id="ID4EJC"></a>


##### Further Information

[EDS Common Headers](../../additional/edscommonheaders.md)

 [EDS Parameters](../../additional/edsparameters.md)

 [EDS Query Refiners](../../additional/edsqueryrefiners.md)

 [Additional Reference](../../additional/atoc-xboxlivews-reference-additional.md)
