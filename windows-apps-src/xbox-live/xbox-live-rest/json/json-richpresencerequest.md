---
title: RichPresenceRequest (JSON)
assetID: 599266be-f747-0be1-fadf-f8e0262dc27f
permalink: en-us/docs/xboxlive/rest/json-richpresencerequest.html
author: KevinAsgari
description: ' RichPresenceRequest (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# RichPresenceRequest (JSON)
Request for information about which rich presence information should be used. 
<a id="ID4EN"></a>

 
## RichPresenceRequest
 
The RichPresenceRequest object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| id| string| The <b>friendlyName</b> of the rich presence string to use.| 
| scid| string| Scid that tells us where the rich presence strings are defined.| 
| params| array of string| Array of <b>friendlyName</b> strings with which to finish the rich presence string. Only enumeration-friendly names should be specified, not stats. Leaving this empty will remove any previous value.| 
  
<a id="ID4EDC"></a>

 
## Sample JSON syntax
 

```cpp
{
      id:"playingMapWeapon",
      scid:"abba0123-08ba-48ca-9f1a-21627b189b0f",
    }
    
```

  
<a id="ID4EMC"></a>

 
## See also
 
<a id="ID4EOC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   