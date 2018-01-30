---
title: Requirement (JSON)
assetID: 74faee8d-42e3-cfcf-22b3-9dcd9227de6b
permalink: en-us/docs/xboxlive/rest/json-requirement.html
author: KevinAsgari
description: ' Requirement (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Requirement (JSON)
The unlock criteria for the Achievement and how far the user is toward meeting them. 
<a id="ID4EN"></a>

 
## Requirement
 
The Requirement object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| id| string| The ID of the requirement.| 
| current| string| The current value of progression toward the requirement.| 
| target| string| The target value of the requirement.| 
| operationType| string| The operation type of the requirement. Valid values are Sum, Minimum, Maximum.| 
| ruleParticipationType| string| The participation type of the requirement. Valid values are Individual, Group.| 
  
<a id="ID4ETC"></a>

 
## See also
 
<a id="ID4EVC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   