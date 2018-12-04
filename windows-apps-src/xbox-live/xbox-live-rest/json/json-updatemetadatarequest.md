---
title: UpdateMetadataRequest (JSON)
assetID: 0bc210e3-c1dc-9267-e322-aadb9f0a074a
permalink: en-us/docs/xboxlive/rest/json-updatemetadatarequest.html

description: ' UpdateMetadataRequest (JSON)'
ms.date: 10/12/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
---
# UpdateMetadataRequest (JSON)
The metadata that should be updated for a clip. 
<a id="ID4EN"></a>

 
## UpdateMetadataRequest
 
The UpdateMetadataRequest object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| userCaption| string| Changes the user entered non-localized string for the game clip.| 
| visibility| [GameClipVisibility Enumeration](../enums/gvr-enum-gameclipvisibility.md)| Changes the visibility of the game clip as it is published in the system.| 
| titleData| string| The title-specific property bag. Maximum size: 10 KB.| 
  
<a id="ID4EBC"></a>

 
## Sample JSON syntax
 
Changing User Clip Name and Visibility:
 

```json
{
  "userCaption": "I've changed this 100 Times!",
  "visibility": "Owner"
}

```

 
Changing just title properties (this is just an example, since the schema of this field is up to the caller):
 

```json
{
  "titleData": "{ 'Id': '123456', 'Location': 'C:\\videos\\123456.mp4' }"
}

```

  
<a id="ID4EQC"></a>

 
## See also
 
<a id="ID4ESC"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

  
<a id="ID4E3C"></a>

 
##### Reference 

[POST (/users/me/scids/{scid}/clips/{gameClipId})](../uri/dvr/uri-usersmescidclipsgameclipidpost.md)

   