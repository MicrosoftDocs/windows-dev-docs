---
title: PUT (/{uri})
assetID: 24a24c93-f43b-017e-91e0-29e190fec8a8
permalink: en-us/docs/xboxlive/rest/uri-uriput.html
author: KevinAsgari
description: ' PUT (/{uri})'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# PUT (/{uri})
Upload game clip data.
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.

  * [Remarks](#ID4EX)
  * [URI parameters](#ID4EQB)
  * [Query string parameters](#ID4ERC)
  * [Required Request Headers](#ID4EBE)
  * [Optional Request Headers](#ID4ENG)
  * [Request body](#ID4EWH)
  * [HTTP status codes](#ID4ECAAC)
  * [Required Response Headers](#ID4EYEAC)
  * [Optional Response Headers](#ID4ELHAC)
  * [Response body](#ID4ELIAC)

<a id="ID4EX"></a>


## Remarks

After the **InitialUploadResponse** is returned, the upload is performed through the **uploadUri** returned in that object. The client should split the file into **expectedBlocks** sequential blocks, no larger than 2 MB each. They can be uploaded in parallel.

If you are uploading the file in blocks, the server will return an HTTP status code of Accepted (202) for each request, until it has received all expected blocks, in which case it commits all blocks as one file, returning Created (201). In these cases, the response does not contain an object, and the server may schedule additional processing. On error, a **ServiceErrorResponse** object may be returned along with an appropriate HTTP status code.

On a recoverable error code, the client should retry using a standard back-off retry mechanism.

> [!NOTE] 
> Even if an upload completes successfully, further processing will occur that could reject the clip for reasons not related to the upload or metadata supplementing process.


<a id="ID4EQB"></a>


## URI parameters

| Parameter| Type| Description|
| --- | --- | --- | --- |
| <b>uri</b>| string| The <b>uploadUri</b> field within the <b>InitialUploadResponse</b> object.|

<a id="ID4ERC"></a>


## Query string parameters

| Parameter| Type| Description|
| --- | --- | --- | --- | --- | --- | --- |
| <b>blockNum</b>| 32-bit unsigned integer| Required if <b>expectedBlocks</b> is set. Zero-indexed block number determining ordering of block in file. For example, if <b>expectedBlocks</b> is 7, then <b>blockNum</b> can be from 0 to 6. |
| <b>uploadId</b>| string| Required. Opaque ID in <b>GameClipsServiceUploadResponse</b> object.|

<a id="ID4EBE"></a>


## Required Request Headers

| Header| Type| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Authorization| string| Authentication credentials for HTTP authentication. Example values: <b>Xauth=&lt;authtoken></b>|
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.|
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.|
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.|
| Cache-Control| string| Polite request to specify caching behavior.|

<a id="ID4ENG"></a>


## Optional Request Headers

| Header| Type| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Accept-Encoding| string| Acceptable compression encodings. Example values: gzip, deflate, identity.|
| ETag| string| Used for cache optimization. Example value: "686897696a7c876b7e".|

<a id="ID4EWH"></a>


## Request body

No objects are sent in the body of this request.

<a id="ID4ECAAC"></a>


## HTTP status codes

The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).

| Code| Reason phrase| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
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

<a id="ID4EYEAC"></a>


## Required Response Headers

| Header| Type| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.|
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.|
| Cache-Control| string| Polite request to specify caching behavior.|
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.|
| Retry-After| string| Instructs client to try again later in the case of an unavailable server.|
| Vary| string| Instructs downstream proxies how to cache responses.|

<a id="ID4ELHAC"></a>


## Optional Response Headers

| Header| Type| Description|
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Etag| string| Used for cache optimization. Example: "686897696a7c876b7e".|

<a id="ID4ELIAC"></a>


## Response body

No objects are sent in the body of the response.

<a id="ID4EWIAC"></a>


## See also

<a id="ID4EYIAC"></a>


##### Parent

[/{uri}](uri-uri.md)
