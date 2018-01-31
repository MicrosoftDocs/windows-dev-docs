---
title: DeviceRecord (JSON)
assetID: aca4f4d3-f9b4-8919-5b6d-5ae0fe11e162
permalink: en-us/docs/xboxlive/rest/json-devicerecord.html
author: KevinAsgari
description: ' DeviceRecord (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# DeviceRecord (JSON)
Information about a device, including its type and the titles active on it. 
<a id="ID4EN"></a>

 
## DeviceRecord
 
The DeviceRecord object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| type| string| The device type of the device. Possibilities include "D", "Xbox360", "MoLIVE" (Windows), "WindowsPhone", "WindowsPhone7", and "PC" (G4WL). If the type is unknown (for example iOS, Android, or a title embedded in a web browser), "Web" is returned .| 
| titles| array of [TitleRecord](json-titlerecord.md)| The list of titles active on this device.| 
  
<a id="ID4EWB"></a>

 
## Sample JSON syntax
 

```cpp
[{
    type:"D",
    titles:
    [{
      id:"12341234",
      name:"Contoso 5",
      state:"active",
      placement:"fill",
      timestamp:"2012-09-17T07:15:23.4930000",
      activity:
      {
        richPresence:"Team Deathmatch on Nirvana"
      }
    },
    {
      id:"12341235",
      name:"Contoso Waypoint",
      timestamp:"2012-09-17T07:15:23.4930000",
      placement:"snapped",
      state:"active",
      activity:
      {
        richPresence:"Using radar"
      }
    }]
  }]
    
```

  
<a id="ID4E6B"></a>

 
## See also
 
<a id="ID4EBC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

  
<a id="ID4ENC"></a>

 
##### Reference   