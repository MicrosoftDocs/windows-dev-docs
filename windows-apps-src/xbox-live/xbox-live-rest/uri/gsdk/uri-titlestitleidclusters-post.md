---
title: POST (/titles/{titleId}/clusters)
assetID: 0977b0b0-872d-f7ad-9ba0-30d56cff4912
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidclusters-post.html
author: KevinAsgari
description: ' POST (/titles/{titleId}/clusters)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/titles/{titleId}/clusters)
URI that allows a client to create an Xbox Live Compute server instance. 
The domain for these URIs is `gameserverms.xboxlive.com`.
 
  * [URI Parameters](#ID4EX)
  * [Required Request Headers](#ID4EGB)
  * [Authorization](#ID4ELD)
  * [Request Body](#ID4EWD)
  * [Required Response Headers](#ID4EZE)
  * [Response Body](#ID4E5G)
 
<a id="ID4EX"></a>

 
## URI Parameters
 
| Parameter| Description| 
| --- | --- | 
| titleId| ID of the title that the request should operate on.| 
  
<a id="ID5EG"></a>

 
## Host Name

gameserverms.xboxlive.com
 
<a id="ID4EGB"></a>

 
## Required Request Headers
 
When making a request, the headers shown in the following table are required.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | 
| User-Agent|  | Information about the user agent making the request.| 
| Content-Type| application/json| Type of data being submitted.| 
| Host| gameserverms.xboxlive.com|  | 
| Content-Length|  | Length of the request object.| 
| x-xbl-contract-version| 1| API contract version.| 
| Authorization| XBL3.0 x=[hash];[token]| Authentication token.| 
  
<a id="ID4ELD"></a>

 
## Authorization
 
The request must include a valid Xbox Live authorization header. If the caller is not allowed to access this resource, the service returns 403 Forbidden in response. If the header is invalid or missing, the service returns 401 Unauthorized in response.
  
<a id="ID4EWD"></a>

 
## Request Body
 
The request must contain a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | 
| sessionId| Session identifier from the MPSD.| 
| abortIfQueued| Optional parameter, which when set to true tells GSMS not to queue this session for a resource if it can not be fulfilled immediately. If the request is aborted because this value is true, the response object will contain <code>"fulfillmentState" : "Aborted"</code>. | 
 
<a id="ID4ERE"></a>

 
### Sample Request
 

```cpp
{
  "sessionId" : "/serviceconfigs/00000000-0000-0000-0000-000000000000/sessiontemplates/quick/session/scott1",
  "abortIfQueued" : "true"
}

      
```

   
<a id="ID4EZE"></a>

 
## Required Response Headers
 
A response will always include the headers shown in the following table.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Cache-Control|  | Directives that must be obeyed by all caching mechanisms along the request/response chain.| 
| Content-Type| application/json| Type of data in the response.| 
| Content-Length|  | Length of the response body.| 
| X-Content-Type-Options|  |  | 
| X-XblCorrelationId|  | The mime type of the response body.| 
| Date|  |  | 
  
<a id="ID4E5G"></a>

 
## Response Body
 
If the call is successful, the service will return a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| pollIntervalMilliseconds| Recommended interval in ms to poll for completion. Note that this is not an estimate of when the cluster will be ready, but rather a recommendation for how frequently the caller should poll for a status update given the current pool of subscriptions and request and fulfillment rates.| 
| fulfillmentState| Indicates whether the provided session was allocated a resource immediately, "Fulfilled", added to the queue for the availability of a future resource, "Queued", or aborted, "Aborted", due to the inability to fulfill the request immediately when the request specified abortIfQueued as "true". | 
 
<a id="ID4EWH"></a>

 
### Sample Response
 

```cpp
{
  "pollIntervalMilliseconds" : "1000",
  "fulfillmentState" : "Fulfilled" | "Queued" | "Aborted"
}
      
```

   
<a id="remarks"></a>

 
## Remarks
 
A title should only retry the call to the service when the following response codes are received:
 
   * 408—Server Timeout
   * 429—Too Many Requests
   * 500—Server Error
   * 502—Bad Gateway
   * 503—Service Unavailable
   * 504—Gateway Timeout
   
<a id="ID4EFBAC"></a>

 
## See also
 [/titles/{titleId}/clusters](uri-titlestitleidclusters.md)

  