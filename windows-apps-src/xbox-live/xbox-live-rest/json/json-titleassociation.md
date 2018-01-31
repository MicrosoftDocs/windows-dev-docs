---
title: TitleAssociation (JSON)
assetID: 05f4edbb-cc3d-c17d-0979-aac9e44a4988
permalink: en-us/docs/xboxlive/rest/json-titleassociation.html
author: KevinAsgari
description: ' TitleAssociation (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# TitleAssociation (JSON)
A title that is associated with the achievement. 
<a id="ID4EN"></a>

 
## TitleAssociation
 
The TitleAssociation object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| name| string| The localized name of the content.| 
| id| string| The titleId (32-bit unsigned integer, returned in decimal).| 
| version| string| Specific version of the associated title (if appropriate).| 
  
<a id="ID4E4B"></a>

 
## Sample JSON syntax
 

```cpp
{
  "name":"Microsoft Achievements Sample",
  "id":3051199919,
  "version":"abc"
}
    
```

  
<a id="ID4EGC"></a>

 
## See also
 
<a id="ID4EIC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   