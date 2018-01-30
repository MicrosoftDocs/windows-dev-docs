---
title: DeviceEndpoint (JSON)
assetID: bd6c4af8-e491-8885-970e-e53d1d60642b
permalink: en-us/docs/xboxlive/rest/json-deviceendpoint.html
author: KevinAsgari
description: ' DeviceEndpoint (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# DeviceEndpoint (JSON)
 
<a id="ID4EO"></a>

 
## DeviceEndpoint
 
The DeviceEndpoint object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| deviceName| string| Optional. A friendly name for the device, if applicable. Currently this value is not used.| 
| endpointUri| string| Required. The URL that the client platform (Windows or Windows Phone) has obtained from its push notification service (WNS or MPNS).| 
| locale| string| Required. The desired language of notifications sent to this endpoint. Can be a list of comma-separated values in preference order. Example: "de-DE, en-US, en".| 
| platform| string| Optional. Currently supported values are "WindowsPhone" and "Windows". If not specified, it is derived from the Device token.| 
| platformVersion| string| Optional. The format of this string is particular to each platform. Currently this value is not used.| 
| systemId| GUID| Required. Unique identifier for the "app instance" (device/user combination). Best practice implementation is for an app to generate a random GUID upon install/first-run, and continue to use that value on subsequent runs of the app.| 
| titleId| 32-bit unsigned integer| Required. The Title ID of the game issuing the call to the service.| 
  
<a id="ID4EGD"></a>

 
## Sample JSON syntax
 

```cpp
{
  "systemId": "e9af05b4-70de-4920-880f-026c6fb67d1b",
  "userId" : 1234567890
  "endpointUri": "http://cloud.notify.windows.com/.../",
  "platform": "Windows",
  "platformVersion": "1.0",
  "deviceName": "Test Endpoint",
  "locale": "en-US",
  "titleId": 1297290219
}
    
```

  
<a id="ID4EPD"></a>

 
## See also
 
<a id="ID4ERD"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

  
<a id="ID4E4D"></a>

 
##### Reference   