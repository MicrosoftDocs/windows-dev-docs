---
title: PermissionCheckResponse (JSON)
assetID: 7a749001-b569-b0e0-2a35-f299474c8710
permalink: en-us/docs/xboxlive/rest/json-permissioncheckresponse.html
author: KevinAsgari
description: ' PermissionCheckResponse (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# PermissionCheckResponse (JSON)
The results of a check from a single user for a single permission setting against a single target user. 
<a id="ID4EN"></a>

 
## PermissionCheckResponse
 
The PermissionCheckResponse object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| IsAllowed| Boolean value| Required. This member is <b>true</b> if the requesting user is allowed to perform the requested action with the target user.| 
| Results| Array of [PermissionCheckResult (JSON)](json-permissioncheckresult.md)| Optional. If <b>IsAllowed</b> was false and the check was denied by something related to the requester, indicates why the permission was denied.| 
  
<a id="ID4E3B"></a>

 
## Sample JSON syntax
 

```cpp
{
    "isAllowed": false,
    "reasons":
    [
        {"reason": "BlockedByRequestor"},
        {"reason": "MissingPrivilege", "restrictedSetting": "VideoCommunications"}
    ]
}
    
```

  
<a id="ID4EFC"></a>

 
## See also
 
<a id="ID4EHC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   