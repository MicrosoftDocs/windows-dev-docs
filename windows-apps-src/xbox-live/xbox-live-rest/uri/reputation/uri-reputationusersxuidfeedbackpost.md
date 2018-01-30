---
title: POST (/users/xuid({xuid})/feedback)
assetID: 48a7a510-a658-f2a3-c6bc-28a3610006e7
permalink: en-us/docs/xboxlive/rest/uri-reputationusersxuidfeedbackpost.html
author: KevinAsgari
description: ' POST (/users/xuid({xuid})/feedback)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/users/xuid({xuid})/feedback)
Used from your title if you desire to add a feedback option in your game, as opposed to using the shell. 
The domain for these URIs is `reputation.xboxlive.com`.
 
  * [URI parameters](#ID4EZ)
  * [Required Request Headers](#ID4EEB)
  * [Request body](#ID4ENC)
  * [Required headers](#ID4EDE)
  * [Authorization](#ID4EXF)
  * [HTTP status codes](#ID4EEG)
  * [Response body](#ID4EZH)
 
<a id="ID4EZ"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid| ulong| Xbox User ID (XUID) of the user being reported.| 
  
<a id="ID4EEB"></a>

 
## Required Request Headers
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example value: "XBL3.0 x=&lt;userhash>;&lt;token>".| 
| X-RequestedServiceVersion|  | Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, and so on. Default value: 101.| 
  
<a id="ID4ENC"></a>

 
## Request body 
 
<a id="ID4EVC"></a>

 
### Required members 
 
The request should contain a [Feedback](../../json/json-feedback.md) object. 
  
<a id="ID4EED"></a>

 
### Prohibited members 
 
All other members are prohibited in a request.
  
<a id="ID4ETD"></a>

 
### Sample request 
 

```cpp
{
    "sessionRef":
    {
        "scid": "372D829B-FA8E-471F-B696-07B61F09EC20",
        "templateName": "CaptureFlag5",
        "name": "Halo556932",
    },
    "feedbackType": "CommsAbusiveVoice",
    "textReason": "He called me a doodoo-head!",
    "voiceReasonId": null,
    "evidenceId": null
}

      
```

   
<a id="ID4EDE"></a>

 
## Required headers
 
The following headers are required when making an Xbox Live Services request.
 
| <b>Header</b>| <b>Value</b>| <b>Deacription</b>| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| x-xbl-contract-version| 101| API contract version.| 
| Authorization| XBL3.0 x=[hash];[token]| STS authentication token. STSTokenString is replaced by the token returned by the authentication request.| 
Content-Type| 
application/json| 
Type of data being submitted.| 
  
<a id="ID4EXF"></a>

 
## Authorization
 
The request must include a valid Xbox Live authorization header. If the caller is not allowed to access this resource, the service returns a 403 Forbidden code. If the header is invalid or missing, the service returns a 401 Unauthorized code.
  
<a id="ID4EEG"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 204| No Content| The request has completed, but does not have content to return.| 
| 401| Unauthorized| The request requires user authentication.| 
| 404| Not Found| The specified resource could not be found.| 
| 406| Not Acceptable| Resource version is not supported.| 
| 408| Request Timeout| The request took too long to complete.| 
| 409| Conflict| Continuation token no longer valid.| 
  
<a id="ID4EZH"></a>

 
## Response body 
 
If the call is successful, no objects are returned from this response. Otherwise, the service returns a [ServiceError](../../json/json-serviceerror.md) object.
  
<a id="ID4EOAAC"></a>

 
## See also
 
<a id="ID4EQAAC"></a>

 
##### Parent 

[/users/xuid({xuid})/feedback](uri-reputationusersxuidfeedback.md)

  
<a id="ID4E3AAC"></a>

 
##### Reference 

[Feedback (JSON)](../../json/json-feedback.md)

 [ServiceError (JSON)](../../json/json-serviceerror.md)

   