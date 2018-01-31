---
title: MediaAsset (JSON)
assetID: 858c720a-1648-738b-bb43-f050e7cac19e
permalink: en-us/docs/xboxlive/rest/json-mediaasset.html
author: KevinAsgari
description: ' MediaAsset (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# MediaAsset (JSON)
The media assets associated with the achievement or its rewards.
<a id="ID4EN"></a>


## MediaAsset

The MediaAsset object has the following specification.

| Member| Type| Description|
| --- | --- | --- |
| name| string| The name of the MediaAsset, such as "tile01".|
| type| MediaAssetType enumeration| The type of the media asset: <ul><li>icon (0): The achievement icon.</li><li>art (1): The digital art asset.</li></ul> | 
| url| string| The URL of the MediaAsset.|

<a id="ID4EFC"></a>


## Sample JSON syntax


```cpp
{
  "name":"Icon Name",
  "type":"Icon",
  "url":"http://www.xbox.com"
}

```


<a id="ID4EOC"></a>


## See also

<a id="ID4EQC"></a>


##### Parent

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)
