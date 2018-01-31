---
title: UserList (JSON)
assetID: 894f5a39-4eed-0c5b-fc45-cb0097dc46fd
permalink: en-us/docs/xboxlive/rest/json-userlist.html
author: KevinAsgari
description: ' UserList (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# UserList (JSON)
A collection of [User](json-user.md) objects. 
<a id="ID4ER"></a>

 
## UserList
 
The UserList object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| users| Array of [User (JSON)](json-user.md)| See JSON example below.| 
  
<a id="ID4EPB"></a>

 
## Sample JSON syntax
 

```cpp
{
    "users":
    [
        { "xuid":"12345" },
        { "xuid":"23456" }
    ] 
}
    
```

  
<a id="ID4EYB"></a>

 
## See also
 
<a id="ID4E1B"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   