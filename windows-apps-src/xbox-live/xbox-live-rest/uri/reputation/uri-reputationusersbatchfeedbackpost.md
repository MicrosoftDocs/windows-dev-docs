---
title: POST (/users/batchfeedback)
assetID: f94dcf19-a4e3-5bd0-5276-23e43bdcae52
permalink: en-us/docs/xboxlive/rest/uri-reputationusersbatchfeedbackpost.html
author: KevinAsgari
description: ' POST (/users/batchfeedback)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/users/batchfeedback)
Used by your title's service to send feedback in batch form outside of your title's interface. 
The domain for these URIs is `reputation.xboxlive.com`.
 
  * [Request body](#ID4EX)
  * [Required Headers](#ID4E3E)
  * [HTTP status codes](#ID4EWG)
  * [Response body](#ID4EDAAC)
 
<a id="ID4EX"></a>

 
## Request body 
 
Callers must include their Claims Cert in the ClientCertificates section of their web request object.
 
<a id="ID4EBB"></a>

 
### Required members 
 
The request should contain an array of **BatchFeedback** objects. 
  
<a id="ID4EPB"></a>

 
### Prohibited members 
 
All other members are prohibited in a request.
  
<a id="ID4E3B"></a>

 
### Sample request 
 

```cpp
{
    "items" :
    [
        {
            "targetXuid": "33445566778899",
            "titleId": "6487",
            "sessionRef":
            {
                "scid": "372D829B-FA8E-471F-B696-07B61F09EC20",
                "templateName": "CaptureFlag5",
                "name": "Halo556932",
            },
            "feedbackType": "FairPlayKillsTeammates",
            "textReason": "Killed 19 team members in a single session",
            "evidenceId": null
        },
        {
            "targetXuid": "33445566778899",
            "titleId": "6487",
            "sessionRef":
            {
                "scid": "372D829B-FA8E-471F-B696-07B61F09EC20",
                "templateName": "CaptureFlag5",
                "name": "Halo556932",
            },
            "feedbackType": "FairPlayQuitter",
            "textReason": "Quit 6 times from 9 sessions",
            "evidenceId": null
        }
    ]
}

      
```

 
| <b>Field</b>| <b>JSON Type</b>| <b>Description</b>| 
| --- | --- | --- | 
| items| array| A collection of Feedback JSON documents.| 
| targetXuid| string| The target user's XUID| 
| titleId| string| The title that this feedback was sent from, or NULL.| 
| sessionRef| object| An object describing the MPSD session this feedback relates to, or NULL.| 
| feedbackType| string| A string version of a value in the FeedbackType enumeration.| 
| textReason| string| Partner-supplied text that the sender may add to give more details about the feedback that was submitted.| 
| evidenceId| string| The ID of a resource that can be used as evidence of the feedback being submitted. e.g. the ID of a video file.| 
   
<a id="ID4E3E"></a>

 
## Required Headers
 
The following headers are required when making an Xbox Live Services request. 

> [!NOTE] 
> A Partner Claims Certificate must be sent up with the request in order to submit batch feedback. 


 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | --- | 
| x-xbl-contract-version| 101| API contract version.| 
| Content-Type| application/json| Type of data being submitted.| 
| Authorization| "XBL3.0 x=&lt;userhash>;&lt;token>"| Authentication credentials for HTTP authentication.| 
| X-RequestedServiceVersion| 101| Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, and so on.| 
  
<a id="ID4EWG"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 400| Bad Request| Service could not understand malformed request. Typically an invalid parameter.| 
| 401| Unauthorized| The request requires user authentication.| 
| 404| Not Found| The specified resource could not be found.| 
| 500| Internal Server Error| The server encountered an unexpected condition which prevented it from fulfilling the request.| 
| 503| Service Unavailable| Request has been throttled, try the request again after the client-retry value in seconds (e.g. 5 seconds later).| 
  
<a id="ID4EDAAC"></a>

 
## Response body 
 
If the call is successful, no objects are returned from this response. Otherwise, the service returns a [ServiceError](../../json/json-serviceerror.md) object.
  
<a id="ID4EXAAC"></a>

 
## See also
 
<a id="ID4EZAAC"></a>

 
##### Parent 

[/users/batchfeedback](uri-reputationusersbatchfeedback.md)

  
<a id="ID4EFBAC"></a>

 
##### Reference 

[Feedback (JSON)](../../json/json-feedback.md)

 [ServiceError (JSON)](../../json/json-serviceerror.md)

   