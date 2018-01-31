---
title: GET (/titles/{titleId}/sessions/{sessionId}/allocationStatus)
assetID: 613ba53f-03cb-5ed3-a5ba-be59e5a146d1
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidsessionssessionidallocationstatus-get.html
author: KevinAsgari
description: ' GET (/titles/{titleId}/sessions/{sessionId}/allocationStatus)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GET (/titles/{titleId}/sessions/{sessionId}/allocationStatus)
Returns the allocation status of the sessionhost identified by its sessionId. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
  * [Required Request Headers](#ID4E4)
  * [Required Response Headers](#ID4EEB)
  * [Response Body](#ID4ELB)
 
<a id="ID4E4"></a>

 
## Required Request Headers
 
None.
  
<a id="ID4EEB"></a>

 
## Required Response Headers
 
None.
  
<a id="ID4ELB"></a>

 
## Response Body
 
If the call is successful, the service will return a JSON object with the following members.
 
| Member| Description| 
| --- | --- | 
| description| Returns empty string (left in for backwards compatibility).| 
| clusterId| Returns empty string (left in for backwards compatibility).| 
| hostName| The URL of the session host.| 
| status| Indicates either Queued, Fulfilled, or Aborted.| 
| sessionHostId| The session host ID.| 
| sessionId| The client provided (at allocation time) session ID.| 
| secureContext| The secure device address.| 
| portMappings| The port mappings for the instance.| 
| region| The location of the instance.| 
| ticketId| The current session ID (left in for backwards compatibility).| 
| gameHostId| The current sessionHostId (left in for backwards compatibility).| 
 
<a id="ID4EGD"></a>

 
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
        "status",:"Fulfilled",
        "region": "WestUS",
        "secureContext": "AQDc8Hen/QCDJwWRPcW/1QEEAiABAACdOJU8JNujcXyUPwUBCnue+g==",
        "sessionId": "05328154-1bbe-4f5b-8caa-4e44106712f9",
        "description": "",
        "clusterId": "",
        "sessionHostId": "r111ybf4drgo12kq25tc-082yo7y9sz72f2odtq1ya5yhda-155169995-ncus.GSDKAgent_IN_0.0",
        "ticketId": "05328154-1bbe-4f5b-8caa-4e44106712f9",
        "gameHostId": "r111ybf4drgo12kq25tc-082yo7y9sz72f2odtq1ya5yhda-155169995-ncus.GSDKAgent_IN_0.0"

      
```

  
<a id="remarks"></a>

 
### Remarks
 
A title should only retry the call to the service when the following response codes are received:
 
   * 200—Success 
   * 400—Request contains invalid parameters 
   * 401—Unauthorized 
   * 404—The title ID or ticket ID was invalid or not found 
   * 500—Unexpected server error. 
    