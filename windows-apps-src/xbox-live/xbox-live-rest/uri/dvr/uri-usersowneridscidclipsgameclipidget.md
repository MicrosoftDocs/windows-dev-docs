---
title: GET (/users/{ownerId}/scids/{scid}/clips/{gameClipId})
assetID: dbd60c93-9d8e-609b-0ae3-b3f7ee26ba2d
permalink: en-us/docs/xboxlive/rest/uri-usersowneridscidclipsgameclipidget.html
author: KevinAsgari
description: ' GET (/users/{ownerId}/scids/{scid}/clips/{gameClipId})'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GET (/users/{ownerId}/scids/{scid}/clips/{gameClipId})
Get a single game clip from the system if all the IDs to locate it are known. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [Remarks](#ID4EX)
  * [URI parameters](#ID4EVB)
  * [Authorization](#ID4EAC)
  * [Required Request Headers](#ID4EUH)
  * [Optional Request Headers](#ID4EBCAC)
  * [Request body](#ID4ETDAC)
  * [HTTP status codes](#ID4E5DAC)
  * [Required Response Headers](#ID4EQIAC)
  * [Optional Response Headers](#ID4EJLAC)
  * [Response body](#ID4EJMAC)
 
<a id="ID4EX"></a>

 
## Remarks
 
All data to query for the clip is available in the **GameClip** objects as returned from any metadata query. **XUID**, **ServiceConfigId**, **GameClipId** and **SandboxId** in the claims of the request are required to get a single game clip via this API. If the game clip has been flagged for enforcement, or Content Isolation or privacy checks determine that the user does not have permission to get the specific game clip, the API will return an HTTP status code of 404 (Not Found).
 
**SandboxId** is now retrieved from the claim in the XToken and enforced. If the **SandboxId** is not present, then Entertainment Discovery Services (EDS) will throw a 400 Bad request error.
 
This API must also be used to refresh expired URIs. When the query is complete, any expired URIs for the game clip will be refreshed accordingly. 

> [!NOTE] 
> A URI refresh can take up to 30-40 seconds after this request is done. If the URI has expired, attempting to use it immediately for streaming operations will get an HTTP 500 status code from the IIS Smooth Streaming Servers. We are working on ways to shorten this, and this note will be updated accordingly as that work progresses. 


  
<a id="ID4EVB"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | --- | 
| ownerId| string| User identity of the user whose resource is being accessed. Supported formats: "me" or "xuid(123456789)". Maximum length: 16.| 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
| gameClipId| string| GameClip ID of the resource that is being accessed.| 
  
<a id="ID4EAC"></a>

 
## Authorization
 
Authorization claims used | Claim| Type| Required?| Example value| Remarks| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Xuid| 64-bit signed integer| yes| 1234567890|  | 
| TitleId| 64-bit signed integer| yes| 1234567890| Used for <b>Content-Isolation</b> check.| 
| SandboxId| hexadecimal binary| yes|  | Directs the system to the correct area for lookups, and used for <b>Content-Isolation</b> check.| 
  
Effect of Privacy Settings on Resource | Requesting User| Target User's Privacy Setting| Behavior| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| me| -| As described.| 
| friend| everyone| Forbidden.| 
| friend| friends only| Forbidden.| 
| friend| blocked| Forbidden.| 
| non-friend user| everyone| Forbidden.| 
| non-friend user| friends only| Forbidden.| 
| non-friend user| blocked| Forbidden.| 
| third-party site| everyone| Forbidden.| 
| third-party site| friends only| Forbidden.| 
| third-party site| blocked| Forbidden.| 
 
<a id="ID4EUH"></a>

 
## Required Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example values: <b>Xauth=&lt;authtoken></b>| 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
  
<a id="ID4EBCAC"></a>

 
## Optional Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Accept-Encoding| string| Acceptable compression encodings. Example values: gzip, deflate, identity.| 
| ETag| string| Used for cache optimization. Example value: "686897696a7c876b7e".| 
| Range| string|  | 
  
<a id="ID4ETDAC"></a>

 
## Request body
 
No objects are sent in the body of this request.
  
<a id="ID4E5DAC"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 200| OK| The session was successfully retrieved.| 
| 301| Moved Permanently|  | 
| 307| Temporary Redirect|  | 
| 400| Bad Request| Service could not understand malformed request. Typically an invalid parameter.| 
| 401| Unauthorized| The request requires user authentication.| 
| 403| Forbidden| The request is not allowed for the user or service.| 
| 404| Not Found| The specified resource could not be found.| 
| 406| Not Acceptable| Resource version is not supported.| 
| 408| Request Timeout| The request took too long to complete.| 
| 410| Gone| The requested resource is no longer available.| 
  
<a id="ID4EQIAC"></a>

 
## Required Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
| Retry-After| string| Instructs client to try again later in the case of an unavailable server. Example: <b>application/json</b>.| 
| Vary| string| Instructs downstream proxies how to cache responses. Example: <b>application/json</b>.| 
  
<a id="ID4EJLAC"></a>

 
## Optional Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| ETag| string| Used for cache optimization. Example value: "686897696a7c876b7e".| 
  
<a id="ID4EJMAC"></a>

 
## Response body
 
<a id="ID4EPMAC"></a>

 
### Sample response
 

```cpp
{
 "gameClip": {
   "xuid": "2716903703773872",
   "clipName": "1234567890",
   "titleName": "",
   "gameClipId": "cd42452a-8ec0-4289-9e9e-e4cd89d7d674-000",
   "state": "Published",
   "dateRecorded": "2013-05-08T21:32:17.4201279Z",
   "lastModified": "2013-05-08T21:34:48.8117829Z",
   "userCaption": "",
   "type": "DeveloperInitiated",
   "source": "Console",
   "visibility": "Public",
   "durationInSeconds": 30,
   "scid": "00000000-0000-0012-0023-000000000070",
   "titleId": 0,
   "rating": 0,
   "ratingCount": 0,
   "views": 0,
   "titleData": "",
   "systemProperties": "",
   "savedByUser": false,
   "thumbnails": [
     {
       "uri": "http:\/\/localhost\/users\/xuid(2716903703773872)\/scids\/00000000-0000-0012-0023-000000000070\/clips\/cd42452a-8ec0-4289-9e9e-e4cd89d7d674-000\/thumbnails\/large",
       "fileSize": 0,
       "thumbnailType": "Large"
     },
     {
       "uri": "http:\/\/localhost\/users\/xuid(2716903703773872)\/scids\/00000000-0000-0012-0023-000000000070\/clips\/cd42452a-8ec0-4289-9e9e-e4cd89d7d674-000\/thumbnails\/small",
       "fileSize": 0,
       "thumbnailType": "Small"
     }
   ],
   "gameClipUris": [
     {
       "uri": "http:\/\/localhost\/users\/xuid(2716903703773872)\/scids\/00000000-0000-0012-0023-000000000070\/clips\/cd42452a-8ec0-4289-9e9e-e4cd89d7d674-000",
       "fileSize": 9332015,
       "uriType": "Download",
       "expiration": "9999-12-31T23:59:59.9999999"
     }
   ]
 }
}
         
```

   
<a id="ID4EZMAC"></a>

 
## See also
 
<a id="ID4E2MAC"></a>

 
##### Parent 

[/users/{ownerId}/scids/{scid}/clips/{gameClipId}](uri-usersowneridscidclipsgameclipid.md)

  
<a id="ID4EFNAC"></a>

 
##### Further Information 

[Marketplace URIs](../marketplace/atoc-reference-marketplace.md)

 [Additional Reference](../../additional/atoc-xboxlivews-reference-additional.md)

   