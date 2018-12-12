---
title: TitleAssociation (JSON)
assetID: 05f4edbb-cc3d-c17d-0979-aac9e44a4988
permalink: en-us/docs/xboxlive/rest/json-titleassociation.html

description: ' TitleAssociation (JSON)'
ms.date: 10/12/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
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
 

```json
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

   