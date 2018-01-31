---
title: Paging Parameters
assetID: f8d059fd-30e7-be60-0a46-c9a833c400c6
permalink: en-us/docs/xboxlive/rest/pagingparameters.html
author: KevinAsgari
description: ' Paging Parameters'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Paging Parameters
 
Some Xbox Live Services URIs return collections of JavaScript Object Notation (JSON) objects. These collections can be paged through by specifying paging parameters as part of the query string attached to the URI. A complete list of the paging parameters follows. All URIs that allow paging parameters are linked to at the bottom of this page.
 
<a id="ID4E2"></a>

 
## Query string parameters 
 
| Parameter| Required| Type| Description| 
| --- | --- | --- | --- | 
| continuationToken| No| string| Return the items starting at the given continuation token. | 
| maxItems| No| 32-bit signed integer| Maximum number of items to return from the collection, which can be combined with <b>skipItems</b> and <b>continuationToken</b> to return a range of items. The service may provide a default value if <b>maxItems</b> is not present, and may return fewer than <b>maxItems</b>, even if the last page of results has not yet been returned. | 
| skipItems| No| 32-bit signed integer| Return items beginning after the given number of items. For example, <b>skipItems="3"</b> will retrieve items beginning with the fourth item retrieved. | 
  
<a id="ID4EDD"></a>

 
## See also
 
<a id="ID4EFD"></a>

 
##### Parent  

[Additional Reference](atoc-xboxlivews-reference-additional.md)

  
<a id="ID4ERD"></a>

 
##### Reference  [GET (/users/xuid({xuid})/achievements)](../uri/achievements/uri-achievementsusersxuidachievementsgetv2.md)

   