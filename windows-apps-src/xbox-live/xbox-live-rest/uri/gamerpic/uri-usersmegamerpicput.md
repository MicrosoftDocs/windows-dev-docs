---
title: PUT (/users/me/gamerpic)
assetID: ddf71c62-197d-a81d-35a7-47c6dc9e1b0c
permalink: en-us/docs/xboxlive/rest/uri-usersmegamerpicput.html
author: KevinAsgari
description: ' PUT (/users/me/gamerpic)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# PUT (/users/me/gamerpic)
Uploads a 1080x1080 gamerpic. 
  * [Request body](#ID4EQ)
  * [HTTP status codes](#ID4EZ)
  * [Response body](#ID4EXC)
 
<a id="ID4EQ"></a>

 
## Request body
 
The request body is a gamerpic (1080x1080 PNG file).
  
<a id="ID4EZ"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | 
| 200| OK| Successful GET.| 
| 201| Created.| Upload was successful.| 
| 403| Forbidden| The privilege is revoked.| 
| 500| Error| Something went wrong.| 
  
<a id="ID4EXC"></a>

 
## Response body
 
No objects are sent in the body of the response.
  
<a id="ID4ECD"></a>

 
## See also
 
<a id="ID4EED"></a>

 
##### Parent 

[/users/me/gamerpic](uri-usersmegamerpic.md)

   