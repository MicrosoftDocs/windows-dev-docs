---
title: GET (/qosservers)
assetID: 8b940c1b-947c-eab3-78ed-4384f57ea0bd
permalink: en-us/docs/xboxlive/rest/uri-qosservers-get.html
author: KevinAsgari
description: ' GET (/qosservers)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GET (/qosservers)
URI called by a client to get the list of QoS servers available for use with Xbox Live Compute. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
  * [Required Request Headers](#ID4EBB)
  * [Required Response Headers](#ID4EUC)
  * [Response Body](#ID4EVD)
 
<a id="ID5EG"></a>

 
## Host Name

gameserverds.xboxlive.com
 
<a id="ID4EBB"></a>

 
## Required Request Headers
 
When making a request, the headers shown in the following table are required.
 
| Header| Value| Description| 
| --- | --- | --- | 
| Content-Type| application/json| Type of data being submitted.| 
| Host| gameserverds.xboxlive.com|  | 
| Content-Length|  | Length of the request object.| 
| x-xbl-contract-version| 1| API contract version.| 
  
<a id="ID4EUC"></a>

 
## Required Response Headers
 
A response will always include the headers shown in the following table.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | 
| Content-Type| application/json| Type of data in the response body.| 
| Content-Length|  | Length of the response body.| 
  
<a id="ID4EVD"></a>

 
## Response Body
 
If the call is successful, the service will return a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | 
| qosservers| An array of server information.| 
| serverFqdn| The fully qualified domain name of the server.| 
| serverSecureDeviceAddress| The secure device address of the server.| 
| targetLocation| The geographic location of the server.| 
 
<a id="ID4EUE"></a>

 
### Sample Response
 

```cpp
{ 
  "qosServers" : 
  [ 
    { "serverFqdn" : "xblqosncus.cloudapp.net", "serverSecureDeviceAddress" : "&lt;base-64 encoded blob>", "targetLocation" : "North Central US" },
    { "serverFqdn" : "xblqoswus.cloudapp.net", "serverSecureDeviceAddress" : "&lt;base-64 encoded blob>", "targetLocation" : "West US" },
  ]
}

      
```

   
<a id="ID4EBF"></a>

 
## See also
 [/qosservers](uri-qosservers.md)

  