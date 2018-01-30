---
title: quotaInfo (JSON)
assetID: 3147ab78-e671-e142-0a3a-688dc4079451
permalink: en-us/docs/xboxlive/rest/json-quota.html
author: KevinAsgari
description: ' quotaInfo (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# quotaInfo (JSON)
Contains quota information about a title group. 
<a id="ID4EN"></a>

 
## quotaInfo
 
The quotaInfo object has the following specifications.
 
For global storage
 
| Member| Type| Description| 
| --- | --- | --- | 
| quotaBytes| 32-bit signed integer | Maximum number of bytes usable by the title.| 
| usedBytes| 32-bit signed integer | Number of bytes used by the title.| 
  
<a id="ID4EXB"></a>

 
## Sample JSON syntax
 
The following example shows the response for global storage:
 

```cpp
{
    "quotaInfo":
    {
        "usedBytes":4194304,
        "quotaBytes":536870912
    }
}
      
```

  
<a id="ID4ECC"></a>

 
## See also
 
<a id="ID4EEC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   