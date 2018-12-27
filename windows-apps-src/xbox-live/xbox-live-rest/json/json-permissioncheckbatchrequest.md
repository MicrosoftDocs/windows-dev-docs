---
title: PermissionCheckBatchRequest (JSON)
assetID: 3100b17c-1b60-fdf2-3af9-7e424f1903ee
permalink: en-us/docs/xboxlive/rest/json-permissioncheckbatchrequest.html

description: ' PermissionCheckBatchRequest (JSON)'
ms.date: 10/12/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
---
# PermissionCheckBatchRequest (JSON)
Collection of PermissionCheckBatchRequest objects. 
<a id="ID4EP"></a>

 
## PermissionCheckBatchRequest
 
The PermissionCheckBatchRequest object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| Users| Array of Users| Required. Array of targets to check permission against. Each entry in this array is either an Xbox User ID (XUID) or an anonymous off-network user for cross-network scenarios ("anonymousUser":"allUsers"). | 
| Permissions| Array of [PermissionId Enumeration](../enums/privacy-enum-permissionid.md)| Required. The permissions to check against each user.| 
  
<a id="ID4E3B"></a>

 
## Sample JSON syntax
 

```json
{
    "users":
    [
        {"xuid":"12345"},
        {"xuid":"54321"}
    ],
    "permissions":
    [
        "ShareFriendList",
        "ShareGameHistory"
    ]
}
    
```

  
<a id="ID4EFC"></a>

 
## See also
 
<a id="ID4EHC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   