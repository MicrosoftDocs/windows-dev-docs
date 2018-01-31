---
title: PermissionCheckBatchUserResponse (JSON)
assetID: c587dbc1-9436-4d55-afcb-deb47e3c2664
permalink: en-us/docs/xboxlive/rest/json-permissioncheckbatchuserresponse.html
author: KevinAsgari
description: ' PermissionCheckBatchUserResponse (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# PermissionCheckBatchUserResponse (JSON)
The reasons of a batch permission check for list of permission values for a single target user. 
<a id="ID4EN"></a>

 
## PermissionCheckBatchUserResponse
 
The PermissionCheckBatchUserResponse object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| User| string| Required. This member is <b>true</b> if the requesting user is allowed to perform the requested action with the target user.| 
| Permissions| Array of [PermissionCheckResponse (JSON)](json-permissioncheckresponse.md)| Required. A [PermissionCheckResponse (JSON)](json-permissioncheckresponse.md) for each permission that was asked for in the original request, in the same order as in the request.| 
  
<a id="ID4E4B"></a>

 
## Sample JSON syntax
 

```cpp
{
    "User": {"Xuid": "12345"},
    "Permissions":
    [
        {
            "isAllowed": true
        },
        {
            "isAllowed": false
        },
        {
            "isAllowed": false,
            "reasons":
            [
                {"reason": "BlockedByRequestor"},
                {"reason": "MissingPrivilege", "restrictedSetting": "VideoCommunications"}
            ]
        }
    ]
}
    
```

  
<a id="ID4EGC"></a>

 
## See also
 
<a id="ID4EIC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   