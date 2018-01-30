---
title: DELETE (/users/me/scids/{scid}/clips/{gameClipId})
assetID: 486fac60-6884-2e3f-9ef8-8de5da0ad8af
permalink: en-us/docs/xboxlive/rest/uri-usersmescidclipsgameclipiddelete.html
author: KevinAsgari
description: ' DELETE (/users/me/scids/{scid}/clips/{gameClipId})'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# DELETE (/users/me/scids/{scid}/clips/{gameClipId})
Delete game clip 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [Remarks](#ID4EX)
  * [URI parameters](#ID4ECB)
  * [Authorization](#ID4ENB)
  * [Required Request Headers](#ID4EYB)
  * [Optional Request Headers](#ID4EEE)
  * [Request body](#ID4ENF)
  * [HTTP status codes](#ID4EYF)
  * [Required Response Headers](#ID4EIAAC)
  * [Optional Response Headers](#ID4E2CAC)
  * [Response body](#ID4E2DAC)
 
<a id="ID4EX"></a>

 
## Remarks
 
Provides a mechanism for deleting the user's own video from the GameClips service. Upon delete, all metadata and the actual video assets (generated and original) are removed from the system . This is a permanent operation. 

> [!NOTE] 
> The owner ID specified must match the caller in the authorization token for the delete request to succeed. 


  
<a id="ID4ECB"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | --- | 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
| gameClipId| string| GameClip ID of the resource that is being accessed.| 
  
<a id="ID4ENB"></a>

 
## Authorization
 
Only the Xuid claim is required for this method.
  
<a id="ID4EYB"></a>

 
## Required Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example values: <b>Xauth=&lt;authtoken></b>| 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
  
<a id="ID4EEE"></a>

 
## Optional Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Accept-Encoding| string| Acceptable compression encodings. Example values: gzip, deflate, identity.| 
| ETag| string| Used for cache optimization. Example value: "686897696a7c876b7e".| 
  
<a id="ID4ENF"></a>

 
## Request body
 
No objects are sent in the body of this request.
  
<a id="ID4EYF"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 204| OK| Successful deletion of the clip.| 
| 401| Unauthorized| There is a problem with the auth token format in the request.| 
| 403| Forbidden| Some required claims are missing.| 
| 404| Not Found| The clip specified in the URL was not present (or it was deleted a second time).| 
| 503| Not Acceptable| The service or some downstream dependencies are down. Retry with standard back-off behavior.| 
  
<a id="ID4EIAAC"></a>

 
## Required Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Retry-After| string| Instructs client to try again later in the case of an unavailable server.| 
| Vary| string| Instructs downstream proxies how to cache responses.| 
  
<a id="ID4E2CAC"></a>

 
## Optional Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Etag| string| Used for cache optimization. Example: "686897696a7c876b7e".| 
  
<a id="ID4E2DAC"></a>

 
## Response body
 
The service will respond with an HTTP status code of 204 (No content) upon success. Trying to delete the same object or a non-existent object will return 404.
 
In case of errors, a **ServiceErrorResponse** object will be returned.
  
<a id="ID4EJEAC"></a>

 
## See also
 
<a id="ID4ELEAC"></a>

 
##### Parent 

[/users/me/scids/{scid}/clips/{gameClipId}](uri-usersmescidclipsgameclipid.md)

   