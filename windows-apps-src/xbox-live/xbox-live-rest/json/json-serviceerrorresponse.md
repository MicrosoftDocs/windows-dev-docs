---
title: ServiceErrorResponse (JSON)
assetID: a2077df8-f76c-0233-8e41-68267b681862
permalink: en-us/docs/xboxlive/rest/json-serviceerrorresponse.html
author: KevinAsgari
description: ' ServiceErrorResponse (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# ServiceErrorResponse (JSON)
When a service error is encountered, an appropriate HTTP error code will be returned. Optionally, the service may also include a ServiceErrorResponse object as defined below. In production environments, less data may be included. 
<a id="ID4EN"></a>

 
## ServiceErrorResponse
 
The ServiceErrorResponse object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| <b>errorCode</b>| 32-bit signed integer| Code associated with the error (can be null).| 
| <b>errorMessage</b>| string| Additional details about the error.| 
  
<a id="ID4EVB"></a>

 
## Sample JSON syntax
 

```cpp
{
   "errorCode": 8377,
   "errorMessage": "XUID specified in the claim does not match URI XUID."
 }
    
```

  
<a id="ID4E5B"></a>

 
## See also
 
<a id="ID4EAC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   