---
title: ActivityRecord (JSON)
assetID: e3a7465b-3451-0266-f8ba-b7602b59f7af
permalink: en-us/docs/xboxlive/rest/json-activityrecord.html
author: KevinAsgari
description: ' ActivityRecord (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# ActivityRecord (JSON)
A formatted and localized string about one or more users' rich presence. 
<a id="ID4EN"></a>

 
## ActivityRecord
 
The ActivityRecord object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| richPresence| string| The rich presence string, formatted and localized.| 
| media| MediaRecord| What the user is watching or listening to.| 
  
<a id="ID4ETB"></a>

 
## Sample JSON syntax
 

```cpp
{
        richPresence:"Team Deathmatch on Nirvana"
      }
    
```

  
<a id="ID4E3B"></a>

 
## See also
 
<a id="ID4E5B"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   