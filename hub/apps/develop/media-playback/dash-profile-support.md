---
description: This article lists the Dynamic Adaptive Streaming over HTTP (DASH) profiles supported for WinUI apps.
title: Dynamic Adaptive Streaming over HTTP (DASH) profile support
ms.date: 02/24/2026
ms.topic: article
keywords: windows 10, winui, dash
ms.localizationpriority: medium
---
# Dynamic Adaptive Streaming over HTTP (DASH) profile support


## Supported DASH profiles
The following table lists the DASH profiles that are supported for WinUI apps.

|Tag | Manifest type | Notes|July release of Windows 10|Windows 10, Version 1511|Windows 10, Version 1607 |Windows 10, Version 1607 |Windows 10, Version 1703| Windows 10, Version 1809
|----------------|------|-------|-----------|--------------|---------|-------|--------|--------|
|urn:mpeg&#58;dash:profile:isoff-live:2011 | Static |     |Supported            |  Supported              | Supported        |Supported| Supported| Supported|
|urn:mpeg&#58;dash:profile:isoff-main:2011 |        | Best effort | Supported            |  Supported              | Supported        |Supported| Supported| Supported|
|urn:mpeg&#58;dash:profile:isoff-live:2011 | Dynamic | $Time$ is supported but $Number$ is unsupported in segment templates | Not Supported            | Not Supported              | Not Supported        |Not Supported| Supported| Supported|
|urn:mpeg&#58;dash:profile:isoff-on-demand:2011 |        |  | Not Supported            |  Not Supported              | Not Supported        |Not Supported| Not Supported| Supported|


## Unsupported DASH profiles
Profiles not listed in the above table are unsupported, including but not limited to the following:

* urn:mpeg&#58;dash:profile:full:2011
* urn:mpeg&#58;dash:profile:mp2t-main:2011
* urn:mpeg&#58;dash:profile:mp2t-simple:2011


## Related topics

* [Media playback](media-playback.md)
* [Adaptive streaming](adaptive-streaming.md)
 

 




