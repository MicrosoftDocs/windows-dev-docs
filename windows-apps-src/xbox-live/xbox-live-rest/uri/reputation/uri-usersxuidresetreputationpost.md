---
title: POST (/users/xuid({xuid})/resetreputation)
assetID: 3b76857f-b043-2c76-cf0c-c8f355fe3849
permalink: en-us/docs/xboxlive/rest/uri-usersxuidresetreputationpost.html
author: KevinAsgari
description: ' POST (/users/xuid({xuid})/resetreputation)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/users/xuid({xuid})/resetreputation)
Enables the Enforcement team to set the specified user's Reputation Scores to some arbitrary values after (for example) an account hijacking. 
The domain for these URIs is `reputation.xboxlive.com`.
 
  * [Remarks](#ID4EV)
  * [URI parameters](#ID4E5)
  * [Authorization](#ID4EJB)
  * [Required Request Headers](#ID4E5B)
  * [Request body](#ID4EYD)
  * [HTTP status codes](#ID4EOE)
  * [Response body](#ID4EQH)
 
<a id="ID4EV"></a>

 
## Remarks
 
This method may also be called by any other Partners for all sandboxes except Retail, and by users in all sandboxes except Retail, for testing purposes. Note that this request sets a user's "base" reputation scores, and his positive feedback weightings will all be zeroed out. The user's actual reputation after making this call will be these base scores plus his ambassador bonus plus his follower bonus.
  
<a id="ID4E5"></a>

 
## URI parameters
 
| Parameter| Type| Description| 
| --- | --- | --- | 
| xuid| string| The Xbox User ID (XUID) of the specified user.| 
  
<a id="ID4EJB"></a>

 
## Authorization
 
From partner: For the Retail sandbox, **PartnerClaim** from the Enforcement team; for all other sandboxes, **PartnerClaim**.
 
From user: For all Sandboxes except Retail, **XuidClaim** and **TitleClaim**.
  
<a id="ID4E5B"></a>

 
## Required Request Headers
 
From all: **Content-Type: application/json**.
 
From partner: **X-Xbl-Contract-Version** (current version is 101), **X-Xbl-Sandbox**.
 
From user: **X-Xbl-Contract-Version** (current version is 101).
 
| Header| Type| Description| 
| --- | --- | --- | --- | --- | --- | 
| Authorization| string| Authentication credentials for HTTP authentication. Example value: "XBL3.0 x=&lt;userhash>;&lt;token>".| 
| X-RequestedServiceVersion|  | Build name/number of the Xbox LIVE service to which this request should be directed. The request will only be routed to that service after verifying the validity of the header, the claims in the auth token, and so on. Default value: 101.| 
  
<a id="ID4EYD"></a>

 
## Request body
 
<a id="ID4E5D"></a>

 
### Sample request
 
The request body is a simple [ResetReputation (JSON)](../../json/json-resetreputation.md) document.
 

```cpp
{
    "fairplayReputation": 75,
    "commsReputation": 75,
    "userContentReputation": 75
}
      
```

   
<a id="ID4EOE"></a>

 
## HTTP status codes
 
The service returns one of the status codes in this section in response to a request made with this method on this resource. For a complete list of standard HTTP status codes used with Xbox Live Services, see [Standard HTTP status codes](../../additional/httpstatuscodes.md).
 
| Code| Reason phrase| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| 200| OK| OK.| 
| 400| Bad Request| Service could not understand malformed request. Typically an invalid parameter.| 
| 401| Unauthorized| The request requires user authentication.| 
| 404| Not Found| The specified resource could not be found.| 
| 500| Internal Server Error| The server encountered an unexpected condition which prevented it from fulfilling the request.| 
| 503| Service Unavailable| Request has been throttled, try the request again after the client-retry value in seconds (e.g. 5 seconds later).| 
  
<a id="ID4EQH"></a>

 
## Response body
 
On success, the response body is empty. On failure, a [ServiceError (JSON)](../../json/json-serviceerror.md) document is returned.
 
<a id="ID4E3H"></a>

 
### Sample response
 

```cpp
{
    "code": 4000,
    "source": "ReputationFD",
    "description": "No targetXuid field was supplied in the request"
}
         
```

   
<a id="ID4EHAAC"></a>

 
## See also
 
<a id="ID4EJAAC"></a>

 
##### Parent 

[/users/xuid({xuid})/resetreputation](uri-usersxuidresetreputation.md)

   