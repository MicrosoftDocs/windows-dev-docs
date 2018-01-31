---
title: TitleBlob (JSON)
assetID: fd1c904d-e8d0-f61f-e403-40b25bd4ac14
permalink: en-us/docs/xboxlive/rest/json-titleblob.html
author: KevinAsgari
description: ' TitleBlob (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# TitleBlob (JSON)
Contains information about a title from storage. 
<a id="ID4EP"></a>

 
## TitleBlob
 
The TitleBlob object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| clientFileTime| DateTime| [optional] Date and time of the last upload of the file.| 
| displayName| string| [optional] Name of the file that is shown to the user.| 
| etag| string| Tag for the file used in download and upload requests.| 
| fileName| string| Name of the file.| 
| size| 64-bit signed integer| Size of the file in bytes.| 
| smartBlobType| string| [optional] Type of data. Possible values are: config, json, binary.| 
  
<a id="ID4E6C"></a>

 
## Sample JSON syntax
 

```cpp
{
    "fileName":"foo\bar\blob.txt,binary",
    "clientFileTime":"2012-01-01T01:02:03.1234567Z",
    "displayName":"Friendly Name",
    "size":12,
    "etag":"0x8CEB3E4F8F3A5BF",
    "smartBlobType":"binary"
}
      
```

  
<a id="ID4EID"></a>

 
## See also
 
<a id="ID4EKD"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   