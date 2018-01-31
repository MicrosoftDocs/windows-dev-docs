---
title: POST (/users/me/scids/{scid}/clips)
assetID: 44535926-9fb8-5498-b1c8-514c0763e6c9
permalink: en-us/docs/xboxlive/rest/uri-usersmescidclipspost.html
author: KevinAsgari
description: ' POST (/users/me/scids/{scid}/clips)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/users/me/scids/{scid}/clips)
Make an initial upload request. 
The domains for these URIs are `gameclipsmetadata.xboxlive.com` and `gameclipstransfer.xboxlive.com`, depending on the function of the URI in question.
 
  * [Remarks](#ID4EX)
  * [URI parameters](#ID4EFB)
  * [Authorization](#ID4EQB)
  * [Required Request Headers](#ID4EKC)
  * [Optional Request Headers](#ID4ENE)
  * [Request body](#ID4ENF)
  * [Sample request](#ID4E1F)
  * [HTTP status codes](#ID4EDG)
  * [Response body](#ID4EVAAC)
  * [Sample response](#ID4EFBAC)
 
<a id="ID4EX"></a>

 
## Remarks
 
This is the first part of the GameClip upload process. Upon capture of a video, it's recommended to call the GameClips service immediately to obtain the ID and URI for the upload of the bits, even if the upload is not scheduled to start right away. This call will perform user quota checks and other checks through content isolation, privacy, and so on to see if a video should even be scheduled for upload by the client. A positive response from this call indicates the service is willing to accept the video clip for upload. All clips uploaded must be associated with a specific title (through a SCID) to be accepted in the system.
 
This call is not idempotent; subsequent calls will cause different IDs and URIs to be issued. Retries on failure should follow standard client-side back-off behavior.
  
<a id="ID4EFB"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| scid| string| Service Config ID of the resource that is being accessed. Must match the SCID of the authenticated user.| 
  
<a id="ID4EQB"></a>

 
## Authorization
 
The following claims are required for this method:
 
   * Xuid
   * DeviceType - Must be device to upload
   * DeviceId
   * TitleId
   * TitleSandboxId
   
<a id="ID4EKC"></a>

 
## Required Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example values: <b>Xauth=&lt;authtoken></b>| 
| X-RequestedServiceVersion| string| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, etc. Examples: 1, vnext.| 
| Content-Type| string| MIME type of the response body. Example: <b>application/json</b>.| 
| Accept| string| Acceptable values of Content-Type. Example: <b>application/json</b>.| 
  
<a id="ID4ENE"></a>

 
## Optional Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Accept-Encoding| string| Acceptable compression encodings. Example values: gzip, deflate, identity.| 
  
<a id="ID4ENF"></a>

 
## Request body
 
The body of the request should be an [InitialUploadRequest](../../json/json-initialuploadrequest.md) object in JSON format.
  
<a id="ID4E1F"></a>

 
## Sample request
 

```cpp
{
   "clipNameStringId": "1193045",
   "userCaption": "OMG Look at this!",
   "sessionRef": "4587552a-a5ad-4c4c-a787-5bc5af70e4c9",
   "dateRecorded": "2012-12-23T11:08:08Z",
   "durationInSeconds": 27,
   "expectedBlocks": 7,
   "fileSize": 1234567,
   "type": "MagicMoment, Achievement",
   "source": "Console",
   "visibility": "Default",
   "titleData": "{ 'Boss': 'The Invincible' }",
   "systemProperties": "{ 'Id': '123456', 'Location': 'C:\\videos\\123456.mp4' }",
   "thumbnailSource": "Offset",
   "thumbnailOffsetMillseconds": 20000,
   "savedByUser": false
 }
      
```

  
<a id="ID4EDG"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 200| OK| The session was successfully retrieved.| 
| 400| Bad Request| There was an error in the request body, or the user is over their quota.| 
| 401| Unauthorized| There is a problem with the auth token format in the request.| 
| 403| Forbidden| Some required claims are missing, or DeviceType is not .| 
| 503| Not Acceptable| The service or some downstream dependencies are down. Retry with standard back-off behavior.| 
  
<a id="ID4EVAAC"></a>

 
## Response body
 
The response can be an [InitialUploadResponse](../../json/json-initialuploadresponse.md) object or a [ServiceErrorResponse](../../json/json-serviceerrorresponse.md) object in JSON format.
  
<a id="ID4EFBAC"></a>

 
## Sample response
 

```cpp
{
   "clipName": "ClipName",
   "gameClipId": "6b364924-5650-480f-86a7-fc002a1ee752",  
   "titleName": "TitleName",
   "uploadUri": "https://gameclips.xbox.live/upload/xuid(2716903703773872)/6b364924-5650-480f-86a7-fc002a1ee752/container",
   "largeThumbnailUri": "https://gameclips.xbox.live/upload/xuid(2716903703773872)/6b364924-5650-480f-86a7-fc002a1ee752/container/thumbnails/large",
   "smallThumbnailUri": "https://gameclips.xbox.live/upload/xuid(2716903703773872)/6b364924-5650-480f-86a7-fc002a1ee752/container/thumbnails/small"
 }
         
```

  
<a id="ID4EOBAC"></a>

 
## See also
 
<a id="ID4EQBAC"></a>

 
##### Parent 

[/users/me/scids/{scid}/clips](uri-usersmescidclips.md)

   