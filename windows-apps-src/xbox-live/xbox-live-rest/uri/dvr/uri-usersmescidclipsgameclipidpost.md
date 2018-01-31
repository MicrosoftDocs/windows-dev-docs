---
title: POST (/users/me/scids/{scid}/clips/{gameClipId})
assetID: 410aecad-57f9-c3dc-f35f-19c4d8dfb704
permalink: en-us/docs/xboxlive/rest/uri-usersmescidclipsgameclipidpost.html
author: KevinAsgari
description: ' POST (/users/me/scids/{scid}/clips/{gameClipId})'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/users/me/scids/{scid}/clips/{gameClipId})
Update game clip metadata for the user's own data. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [Remarks](#ID4EX)
  * [URI parameters](#ID4EAB)
  * [Required Request Headers](#ID4ELB)
  * [Optional Request Headers](#ID4EXD)
  * [Request body](#ID4EAF)
  * [Required Response Headers](#ID4EVF)
  * [Optional Response Headers](#ID4EJAAC)
  * [Response body](#ID4EJBAC)
  * [Related URIs](#ID4EWBAC)
 
<a id="ID4EX"></a>

 
## Remarks
 
The API for updating game clip metadata falls into two categories, updating the metadata of one's own game clips such as accessibility and title, and updating of the public attributes (like applying a rating or incrementing the view count) of any other game clip. If the XUID in the URI does not match the XUID in the claim, only the public data can be edited and any request to edit any of the other data will be denied. In the case multiple fields are attempting to be edited and one of them is invalid for the request, the entire request would fail.
  
<a id="ID4EAB"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
| gameClipId| string| GameClip ID of the resource that is being accessed.| 
  
<a id="ID4ELB"></a>

 
## Required Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example values: <b>Xauth=&lt;authtoken></b>| 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
  
<a id="ID4EXD"></a>

 
## Optional Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Accept-Encoding| string| Acceptable compression encodings. Example values: gzip, deflate, identity.| 
| ETag| string| Used for cache optimization. Example value: "686897696a7c876b7e".| 
  
<a id="ID4EAF"></a>

 
## Request body
 
The body of the request should be an [UpdateMetadataRequest](../../json/json-updatemetadatarequest.md) object in JSON format. Examples:
 
Changing User Clip Name and Visibility:
 

```cpp
{
  "userCaption": "I've changed this 100 Times!",
  "visibility": "Owner"
}

```

 
Changing just title properties (this is just an example, since the schema of this field is up to the caller):
 

```cpp
{
  "titleData": "{ 'Id': '123456', 'Location': 'C:\\videos\\123456.mp4' }"
}

```

  
<a id="ID4EVF"></a>

 
## Required Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Retry-After| string| Instructs client to try again later in the case of an unavailable server.| 
| Vary| string| Instructs downstream proxies how to cache responses.| 
  
<a id="ID4EJAAC"></a>

 
## Optional Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Etag| string| Used for cache optimization. Example: "686897696a7c876b7e".| 
  
<a id="ID4EJBAC"></a>

 
## Response body
 
Upon successful update of the metadata an HTTP status code of 200 will be returned.
 
Otherwise a ServiceErrorResponse object in JSON format will be returned with an appropriate HTTP status code.
  
<a id="ID4EWBAC"></a>

 
## Related URIs
 
The following URIs update public fields in the metadata. There is no body required for these requests. Upon successful update of the metadata an HTTP status code of 200 will be returned. Otherwise a ServiceErrorResponse object in JSON format will be returned with an appropriate HTTP status code.
 
   * **POST /users/{ownerId}/scids/{scid}/clips/{gameClipId}/ratings/{rating value}** - applies the specified rating to the specified clip. Rating Value should be an integer between 1 and 5.
   * **POST /users/{ownerId}/scids/{scid}/clips/{gameClipId}/flag** - flags the clip to contain potentially questionable content to be checked by enforcement.
   * **POST /users/{ownerId}/scids/{scid}/clips/{gameClipId}/views** - increments the view count for the specified game clip. It is recommended that this is called not right when playback is started, but when 75%-80% of playback has been completed.
   
<a id="ID4EMCAC"></a>

 
## See also
 
<a id="ID4EOCAC"></a>

 
##### Parent 

[/users/me/scids/{scid}/clips/{gameClipId}](uri-usersmescidclipsgameclipid.md)

   