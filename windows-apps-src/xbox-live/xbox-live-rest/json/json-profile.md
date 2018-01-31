---
title: Profile (JSON)
assetID: b92b1750-c2df-39b6-6c5c-f9e8068c8097
permalink: en-us/docs/xboxlive/rest/json-profile.html
author: KevinAsgari
description: ' Profile (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Profile (JSON)
The personal profile settings for a user. 
<a id="ID4EN"></a>

 
## Profile
 
The Profile object has the following specification.
 
| Member| Type| Description| 
| --- | --- | --- | 
| AppDisplayName| string| Name for displaying in apps. This could be the user's "real name" or their gamertag, depending on privacy. This setting represents the user's identity string that should be used for display in apps.| 
| GameDisplayName| string| Name for displaying in games. This could be the user's "real name" or their gamertag, depending on privacy. This setting represents the user's identity string that should be used for display in games.| 
| Gamertag| string| The user's gamertag.| 
| AppDisplayPicRaw| string| Raw app display pic URL (see below).| 
| GameDisplayPicRaw| string| Raw game display pic URL (see below).| 
| AccountTier| string| What type of account does the user have? Gold, Silver, or FamilyGold?| 
| TenureLevel| 32-bit unsigned integer| How many years has the user been with Xbox Live?| 
| Gamerscore| 32-bit unsigned integer| Gamerscore of the user.| 
  


> [!NOTE] 
> Pictures can be the user's 'real picture' or their XboxOne gamerpic, depending on privacy. These settings represent the user's picture url that should be used for display on the client. This image may be empty (indicating that the user hasn't set any picture). 


 
The Raw URL is a resizable URL. It can be used to specify one of the following sizes and formats using by appending `&format={format}&w={width}&h={height}` to the URI:
 
Format: png
 
Sizes: 64x64, 208x208, 424x424
 
<a id="ID4E2D"></a>

 
## See also
 
<a id="ID4E4D"></a>

 
##### Parent 

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)

   