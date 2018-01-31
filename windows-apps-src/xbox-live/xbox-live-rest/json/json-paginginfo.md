---
title: PagingInfo (JSON)
assetID: 645e575d-3e8e-d954-90e6-e51dd83da93b
permalink: en-us/docs/xboxlive/rest/json-paginginfo.html
author: KevinAsgari
description: ' PagingInfo (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# PagingInfo (JSON)
Contains paging information for results that are returned in pages of data. 
<a id="ID4EN"></a>

 
## PagingInfo
 
| Member| Type| Description| 
| --- | --- | --- | 
| continuationToken| string| An opaque continuation token used to access the next page of results. Maximum 32 characters.Callers can supply this value in the <b>continuationToken</b> query parameter in order to retrieve the next set of items in the collection. If this property is <b>null</b>, then there are no additional items in the collection. This property is required and is provided even if the collection is being paged with <b>skipItems</b>.| 
| totalItems| 32-bit signed integer| The total number of items in the collection. This is not provided if the service is unable to provide a real-time view into the size of the collection.| 
  
<a id="ID4E4B"></a>

 
## Sample JSON syntax
 

```cpp
{
   "continuationToken":"16354135464161213-0708c1ba-147f-48cc-9ad9-546gaadg648"
   "totalItems":5,
}
    
```

  
<a id="ID4EGC"></a>

 
## See also
 
<a id="ID4EIC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   