---
title: GameClipThumbnail (JSON)
assetID: 3ed87fc1-734c-d8b5-d908-0ae3359769ed
permalink: en-us/docs/xboxlive/rest/json-gameclipthumbnail.html
author: KevinAsgari
description: ' GameClipThumbnail (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GameClipThumbnail (JSON)
Contains the information related to an individual thumbnail. There can be multiple sizes per clip, and it is up to the client to select the proper one for display. 
<a id="ID4EN"></a>

 
## GameClipThumbnail
 
The GameClipThumbnail object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| <b>uri</b>| string| The URI for the thumbnail image.| 
| <b>fileSize</b>| 32-bit unsigned integer| The total file size of the thumbnail image.| 
| <b>thumbnailType</b>| ThumbnailType| The type of thumbnail image.| 
  
<a id="ID4EAC"></a>

 
## Sample JSON syntax
 

```cpp
{
         "uri": "http://gameclips.xbox.com/thumbnails/7ce5c1a7-1255-46d3-a90e-34a0e2dfab06/small.jpg",
         "fileSize": 123,
         "width": 120,
         "height": 250
       }
    
```

  
<a id="ID4EJC"></a>

 
## See also
 
<a id="ID4ELC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   