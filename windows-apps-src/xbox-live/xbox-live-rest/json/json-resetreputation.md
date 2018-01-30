---
title: ResetReputation (JSON)
assetID: 15edb5e7-a00b-4188-9b49-9db5774c4a10
permalink: en-us/docs/xboxlive/rest/json-resetreputation.html
author: KevinAsgari
description: ' ResetReputation (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# ResetReputation (JSON)
Contains the new base Reputation scores to which a user's existing scores should be changed. 
<a id="ID4EN"></a>

 
## ResetReputation
 
The ResetReputation object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| fairplayReputation| number| The desired new base Fairplay Reputation score for the user (valid range 0 to 75).| 
| commsReputation| number| The desired new base Comms Reputation score for the user (valid range 0 to 75).| 
| userContentReputation| number| The desired new base UserContent Reputation score for the user (valid range 0 to 75).| 
  
<a id="ID4E4B"></a>

 
## Sample JSON syntax
 

```cpp
{
    "fairplayReputation": 75,
    "commsReputation": 75,
    "userContentReputation": 75
}
    
```

  
<a id="ID4EGC"></a>

 
## See also
 
<a id="ID4EIC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   