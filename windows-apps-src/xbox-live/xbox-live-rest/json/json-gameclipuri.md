---
title: GameClipUri (JSON)
assetID: 03c097e8-7f29-1026-7a77-5c785b8511e9
permalink: en-us/docs/xboxlive/rest/json-gameclipuri.html
author: KevinAsgari
description: ' GameClipUri (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GameClipUri (JSON)
 
<a id="ID4EO"></a>

 
## GameClipUri
 
The GameClipUri object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| <b>uri</b>| string| The URI to the location of the video asset.| 
| <b>fileSize</b>| 32-bit unsigned integer| The total file size of the thumbnail image.| 
| <b>uriType</b>| GameClipUriType| The type of the URI.| 
| <b>expiration</b>| DateTime| The expiration time of the URI that is included in this response. If the URL is empty or deemed expired before playback, callers should call the RefreshUrl API.| 
  
<a id="ID4EMC"></a>

 
## Sample JSON syntax
 

```cpp
{
         "uri": "http://gameclips.xbox.com/clips/7ce5c1a7-1255-46d3-a90e-34a0e2dfab06/clip.mp4",
         "fileSize": 1234565,
         "uriType": "Download",
         "expiration": "9999-12-31T23:59:59.9999999"
       }
    
```

  
<a id="ID4EVC"></a>

 
## See also
 
<a id="ID4EXC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   