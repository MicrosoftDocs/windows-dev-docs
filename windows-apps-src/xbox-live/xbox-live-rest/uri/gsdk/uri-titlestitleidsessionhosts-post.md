---
title: POST (/titles/{Title Id}/sessionhosts)
assetID: 8558b336-1af9-8143-9752-477ceb3a8e4e
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidsessionhosts-post.html
author: KevinAsgari
description: ' POST (/titles/{Title Id}/sessionhosts)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/titles/{Title Id}/sessionhosts)
Create new cluster request. 
The domain for these URIs is `gameserverms.xboxlive.com`.
 
  * [URI Parameters](#ID4EX)
  * [Required Request Headers](#ID4EGB)
  * [Request Body](#ID4E5B)
  * [Required Response Headers](#ID4ELD)
  * [Response Body](#ID4ESD)
 
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
| Content-Type| application/json| Type of data being submitted.| 
  
<a id="ID4E5B"></a>

 
## Request Body
 
The request must contain a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | 
| sessionId| This is the caller specified identifier. It is assigned to the session host that is allocated and returned. Later on you can reference the specific sessionhost by this identifier. It must be globally unique (i.e. GUID).| 
| SandboxId| The sandbox you wish the session host to be allocated in.| 
| cloudGameId| The cloud game identifier.| 
| locations| The ordered list of preferred locations you would like the session to be allocated from.| 
| sessionCookie| This is a caller specified opaque string. It is associated with the sessionhost and can be referenced in your game code. Use this member to pass a small amount of information from the client to the server (Max size is 4KB).| 
| gameModelId| The game mode identifier.| 
 
<a id="ID4EDD"></a>

 
### Sample Request
 

```cpp
{
        "sessionId": "3536d3e6-e85d-4f47-b898-9617d19dabcd",
        "sandboxId": "ISST.0",
        "cloudGameId": "1b7f9925-369c-4301-b1f7-1125dce25776",
        "locations": [
        "West US",
        "East US",
        "West Europe"
        ],
        "sessionCookie": "Caller provided opaque string",
        "gameModeId": "2162d32c-7ac8-40e9-9b1f-56676b8b2513"
        }
      
```

   
<a id="ID4ELD"></a>

 
## Required Response Headers
 
None.
  
<a id="ID4ESD"></a>

 
## Response Body
 
If the call is successful, the service will return a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| hostName| The host name of the instance.| 
| portMappings| The port mappings.| 
| region| Region the instance is hosted in.| 
| secureContext| The secure device address.| 
 
<a id="ID4ESE"></a>

 
### Sample Response
 

```cpp
{
        "hostName": "r111ybf4drgo12kq25tc-082yo7y9sz72f2odtq1ya5yhda-155169995-ncus.cloudapp.net",
        "portMappings": [
        {
        "Key": "GSDKTCPTest",
        "Value": {
        "internal": 10100,
        "external": 10103
        }
        },
        {
        "Key": "GSDKUDPTest",
        "Value": {
        "internal": 5000,
        "external": 5000
        }
        }
        ],
        "region": "WestUS",
        "secureContext": "AQDc8Hen/QCDJwWRPcW/1QEEAiABAACdOJU8JNujcXyUPwUBCnue+g=="
        }
      
```

   
<a id="remarks"></a>

 
## Remarks
 
A title should only retry the call to the service when the following response codes are received:
 
   * 200—Success - response returned.
   * 400—Invalid parameters or malformed request body.
   * 401—Unauthorized
   * 404—Title id doesnt have any subscriptions assigned to it.
   * 409—When identical request are made (same sessionId) at roughly the same time, this response is possible. If an allocate request is made and a session host already has the specified sessionId AND it is already Active we will return information detailing that sessionhost. If the session host however, is NOT Active yet, you will receive a Conflict.
   * 500—Unexpected server error.
   * 503—No sessionhosts StandingBy. Retry the request when some of these resources are free.
   
<a id="ID4EFG"></a>

 
## See also
 [/titles/{titleId}/sessionhosts](uri-titlestitleidsessionhosts.md)

  