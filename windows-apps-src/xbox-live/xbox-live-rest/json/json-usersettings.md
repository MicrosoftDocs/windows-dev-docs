---
title: UserSettings (JSON)
assetID: 17c030cb-05e0-f78e-5ab1-cdbd8b801ceb
permalink: en-us/docs/xboxlive/rest/json-usersettings.html
author: KevinAsgari
description: ' UserSettings (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# UserSettings (JSON)
Returns settings for current authenticated user. 
<a id="ID4EN"></a>

 
## UserSettings
 
The UserSettings object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| id| 32-bit unsigned integer| The identifier of the setting.| 
| source| 32-bit unsigned integer| Represents the source of the setting. | 
| titleId| 32-bit unsigned integer| The identifier of the title associated with the setting. | 
| value| array of 8-bit unsigned integer| Represents the value of the setting. Clients retrieving settings must understand the representation format to be able to read the data. | 
  
<a id="ID4EJC"></a>

 
## Sample JSON syntax
 

```cpp
{
   "id":268697600,
   "source":1,
   "titleId":1297287259,
   "value":"CAAAAA=="
}
    
```

  
<a id="ID4ESC"></a>

 
## See also
 
<a id="ID4EUC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   