---
title: GET (/{uri})
assetID: a67a3288-88f9-c504-5fa8-8fd06055d079
permalink: en-us/docs/xboxlive/rest/uri-uriget.html
author: KevinAsgari
description: ' GET (/{uri})'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GET (/{uri})
Download game clip. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [Remarks](#ID4EX)
  * [URI parameters](#ID4EDB)
  * [Required Request Headers](#ID4EEC)
  * [Optional Request Headers](#ID4EQE)
  * [Request body](#ID4EZF)
  * [Required Response Headers](#ID4EEG)
  * [HTTP status codes](#ID4EYAAC)
  * [Optional Response Headers](#ID4EOFAC)
  * [Response body](#ID4EOGAC)
 
<a id="ID4EX"></a>

 
## Remarks
 
The client can download any clip or thumbnail that has reached the Published state and is of the Downloadable type, as specified in a **GameClipUri** object. The URI for requesting the file is included in the response body when retrieving a list of user or public clips.
  
<a id="ID4EDB"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| <b>uri</b>| string| The <b>uri</b> field within the <b>GameClipUri</b> object.| 
  
<a id="ID4EEC"></a>

 
## Required Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example values: <b>Xauth=&lt;authtoken></b>| 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
  
<a id="ID4EQE"></a>

 
## Optional Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Accept-Encoding| string| Acceptable compression encodings. Example values: gzip, deflate, identity.| 
| ETag| string| Used for cache optimization. Example value: "686897696a7c876b7e".| 
  
<a id="ID4EZF"></a>

 
## Request body
 
No objects are sent in the body of this request.
  
<a id="ID4EEG"></a>

 
## Required Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Cache-Control| string| Polite request to specify caching behavior.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
| Retry-After| string| Instructs client to try again later in the case of an unavailable server.| 
| Vary| string| Instructs downstream proxies how to cache responses.| 
  
<a id="ID4EYAAC"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 200| OK| The session was successfully retrieved.| 
| 301| Moved Permanently| The service has moved to a different URI.| 
| 307| Temporary Redirect| The service has moved to a different URI.| 
| 400| Bad Request| Service could not understand malformed request. Typically an invalid parameter.| 
| 401| Unauthorized| The request requires user authentication.| 
| 403| Forbidden| The request is not allowed for the user or service.| 
| 404| Not Found| The specified resource could not be found.| 
| 406| Not Acceptable| Resource version is not supported.| 
| 408| Request Timeout| The request took too long to complete.| 
| 410| Gone| The requested resource is no longer available.| 
  
<a id="ID4EOFAC"></a>

 
## Optional Response Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Etag| string| Used for cache optimization. Example: "686897696a7c876b7e".| 
  
<a id="ID4EOGAC"></a>

 
## Response body
 
<a id="ID4EUGAC"></a>

  
 
If successful, the server will return the video clip, possibly truncated according to the Range request header. For a truncated clip, the response will be Partial Content (206). If the server returns the entire file, it will respond OK (200). On error, a **GameClipsServiceErrorResponse** object may be returned along with an appropriate HTTP status code (e.g., 416, Requested Range Not Satisfiable).
   
<a id="ID4E4GAC"></a>

 
## See also
 
<a id="ID4E6GAC"></a>

 
##### Parent 

[/{uri}](uri-uri.md)

   