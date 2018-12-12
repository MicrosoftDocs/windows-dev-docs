---
title: ActivityRequest (JSON)
assetID: 9eb03ab7-352c-4465-ec86-d544e76f63f9
permalink: en-us/docs/xboxlive/rest/json-activityrequest.html

description: ' ActivityRequest (JSON)'
ms.date: 10/12/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
---
# ActivityRequest (JSON)
A request for information about one or more users' rich presence. 
<a id="ID4EN"></a>

 
## ActivityRequest
 
The ActivityRequest object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| richPresence| [RichPresenceRequest](json-richpresencerequest.md)| The friendly name of the rich presence string that should be used.| 
| media| MediaRequest| Media information for what the user is watching or listening to.| 
  
<a id="ID4EVB"></a>

 
## Sample JSON syntax
 

```json
{
    richPresence:
    {
      id:"playingMapWeapon",
      scid:"abba0123-08ba-48ca-9f1a-21627b189b0f"
    }
  }
    
```

  
<a id="ID4E5B"></a>

 
## See also
 
<a id="ID4EAC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   