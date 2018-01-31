---
title: XuidList (JSON)
assetID: 06938a52-e582-a15b-ec7f-4b053dfc28ad
permalink: en-us/docs/xboxlive/rest/json-xuidlist.html
author: KevinAsgari
description: ' XuidList (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# XuidList (JSON)
List of XUIDs on which to perform an operation. 
<a id="ID4EN"></a>

 
## XuidList
 
The XuidList object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| xuids| array of string| List of Xbox User ID (XUID) values on which an operation should be performed or data should be returned.| 
  
<a id="ID4EMB"></a>

 
## Sample JSON syntax
 

```cpp
{
    "xuids": [
        "2533274790395904", 
        "2533274792986770", 
        "2533274794866999"
    ]
}
    
```

  
<a id="ID4EVB"></a>

 
## See also
 
<a id="ID4EXB"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

  
<a id="ID4EBC"></a>

 
##### Reference 

[POST (/users/{ownerId}/people/xuids)](../uri/people/uri-usersowneridpeoplexuidspost.md)

   