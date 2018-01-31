---
title: HopperStatsResults (JSON)
assetID: 91927da1-2e97-f7bc-ae62-7e0e9966b98e
permalink: en-us/docs/xboxlive/rest/json-hopperstatsresults.html
author: KevinAsgari
description: ' HopperStatsResults (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# HopperStatsResults (JSON)
A JSON object representing the statistics for a hopper. 
<a id="ID4EN"></a>

  
 
The HopperStatsResults JSON object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| hopperName| string| The name of the selected hopper.| 
| waitTime| 32-bit signed integer| Average matching time for the hopper (an integral number of seconds). | 
| population| 32-bit signed integer| The number of people waiting for matches in the hopper.| 
  
<a id="ID4EW"></a>

 
## Sample JSON syntax 
 

```cpp
{
      "hopperName":"contosoawesome2",
      "waitTime":30,
      "population":1
    }
  
    
```

  
<a id="ID4EGB"></a>

 
## See also
 
<a id="ID4EIB"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

  
<a id="ID4EUB"></a>

 
##### Reference 

[GET (/serviceconfigs/{scid}/hoppers/{name}/stats)](../uri/matchtickets/uri-serviceconfigsscidhoppershoppernamestatsget.md)

   