---
author: drewbatgit
ms.assetid: 3E0FBB43-F6A4-4558-AA89-20E7760BA73F
description: This article lists the Dynamic Adaptive Streaming over HTTP (DASH) profiles supported for UWP apps.
title: Dynamic Adaptive Streaming over HTTP (DASH) profile support
ms.author: drewbat
ms.date: 02/15/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Dynamic Adaptive Streaming over HTTP (DASH) profile support
The following table lists the DASH profiles that are supported for UWP apps.



|Tag | Manifest type | Notes|July release of Windows 10|Windows 10, Version 1511|Windows 10, Version 1607 |Windows 10, Version 1607 |Windows 10, Version 1703|
|----------------|------|-------|-----------|--------------|---------|-------|--------|
|urn:mpeg:dash:profile:isoff-live:2011 | Static |     |Supported            |  Supported              | Supported        |Supported| Supported|
|urn:mpeg:dash:profile:isoff-main:2011 |        | Best effort | Supported            |  Supported              | Supported        |Supported| Supported|
|urn:mpeg:dash:profile:isoff-live:2011 | Dynamic | $Time$ is supported but $Number$ is unsupported in segment templates | Not Supported            | Not Supported              | Not Supported        |Not Supported| Supported|


## Related topics

* [Media playback](media-playback.md)
* [Adaptive streaming](adaptive-streaming.md)
 

 




