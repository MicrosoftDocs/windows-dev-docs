---
ms.assetid: 66a9cfe2-b212-4c73-8a36-963c33270099
description: This article lists the HTTP Live Streaming (HLS) protocol tags supported for UWP apps.
title: HTTP Live Streaming (HLS) tag support
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# HTTP Live Streaming (HLS) tag support
The following table lists the HLS tags that are supported for UWP apps.

> [!NOTE] 
> Custom tags that start with "X-" can be accessed as timed metadata as described in the article [Media items, playlists, and tracks](media-playback-with-mediasource.md).

|Tag |Introduced in HLS Protocol version|HLS Protocol Document Draft Version|Required on Client|July release of Windows 10|Windows 10, Version 1511|Windows 10, Version 1607 |
|---------------------|-----------|--------------|---------|--------------|-----|-----|
|4.3.1.  Basic Tags                 |             |                   |         |             |     |    |
| 4.3.1.1.  EXTM3U |1|0|REQUIRED|Supported|Supported|Supported|
| 4.3.1.2.  EXT-X-VERSION |2|3|REQUIRED|Supported|Supported|Supported
|4.3.2.  Media Segment Tags                 |             |                   |         |             |     |    | 
| 4.3.2.1.  EXTINF  |1|0|REQUIRED|Supported|Supported|Supported
| 4.3.2.2.  EXT-X-BYTERANGE |4|7|OPTIONAL|Supported|Supported|Supported|
| 4.3.2.3.  EXT-X-DISCONTINUITY |1|2|OPTIONAL|Supported|Supported|Supported|
| 4.3.2.4.  EXT-X-KEY |1|0|OPTIONAL|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp; METHOD|1|0|Attribute|"NONE, AES-128"|"NONE, AES-128"|"NONE, AES-128, SAMPLE-AES"|
|&nbsp;&nbsp;&nbsp; URI|1|0|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp; IV|2|3|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp; KEYFORMAT|5|9|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp; KEYFORMATVERSIONS|5|9|Attribute|Not Supported|Not Supported|Not Supported|
| 4.3.2.5.  EXT-X-MAP |5|9|OPTIONAL|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp; URI|5|9|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp; BYTERANGE|5|9|Attribute|Not Supported|Not Supported|Not Supported|
| 4.3.2.6.  EXT-X-PROGRAM-DATE-TIME |1|0|OPTIONAL|Not Supported|Not Supported|Not Supported|
|4.3.3.  Media Playlist Tags                 |             |                   |         |             |     |    | 
| 4.3.3.1.  EXT-X-TARGETDURATION  |1|0|REQUIRED|Supported|Supported|Supported|
| 4.3.3.2.  EXT-X-MEDIA-SEQUENCE  |1|0|OPTIONAL|Supported|Supported|Supported|
| 4.3.3.3.  EXT-X-DISCONTINUITY-SEQUENCE|6|12|OPTIONAL|Not Supported|Not Supported|Not Supported|
| 4.3.3.4.  EXT-X-ENDLIST |1|0|OPTIONAL|Supported|Supported|Supported|
| 4.3.3.5.  EXT-X-PLAYLIST-TYPE |3|6|OPTIONAL|Supported|Supported|Supported|
| 4.3.3.6.  EXT-X-I-FRAMES-ONLY |4|7|OPTIONAL|Not Supported|Not Supported|Not Supported|
|4.3.4.  Master Playlist Tags                 |             |                   |         |             |     |    |
| 4.3.4.1.  EXT-X-MEDIA |4|7|OPTIONAL|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  TYPE|4|7|Attribute|"AUDIO, VIDEO"|"AUDIO, VIDEO"|"AUDIO, VIDEO, SUBTITLES"|
|&nbsp;&nbsp;&nbsp;  URI|4|7|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  GROUP-ID|4|7|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  LANGUAGE|4|7|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  ASSOC-LANGUAGE|6|13|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp;  NAME|4|7|Attribute|Not Supported|Not Supported|Supported|
|&nbsp;&nbsp;&nbsp;  DEFAULT|4|7|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp;  AUTOSELECT|4|7|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp;  FORCED|5|9|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp;  INSTREAM-ID|6|12|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp;  CHARACTERISTICS|5|9|Attribute|Not Supported|Not Supported|Not Supported|
| 4.3.4.2.  EXT-X-STREAM-INF  |1|0|REQUIRED|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  BANDWIDTH|1|0|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  PROGRAM-ID|1|0|Attribute|NA|NA|NA|
|&nbsp;&nbsp;&nbsp;  AVERAGE-BANDWIDTH|7|14|Attribute|Not Supported|Not Supported|Not Supported|
|&nbsp;&nbsp;&nbsp;  CODECS|1|0|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  RESOLUTION|2|3|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  FRAME-RATE|7|15|Attribute|NA|NA|NA|
|&nbsp;&nbsp;&nbsp;  AUDIO|4|7|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  VIDEO|4|7|Attribute|Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  SUBTITLES|5|9|Attribute|Not Supported|Not Supported|Supported|
|&nbsp;&nbsp;&nbsp;  CLOSED-CAPTIONS|6|12|Attribute|Not Supported|Not Supported|Not Supported|
| 4.3.4.3.  EXT-X-I-FRAME-STREAM-INF  |4|7|OPTIONAL|Not Supported|Not Supported|Not Supported|
| 4.3.4.4.  EXT-X-SESSION-DATA  |7|14|OPTIONAL|Not Supported|Not Supported|Not Supported|
| 4.3.4.5.  EXT-X-SESSION-KEY |7|17|OPTIONAL|Not Supported|Not Supported|Not Supported|
|4.3.5.  Media or Master Playlist Tags                  |             |                   |         |             |     |    |
| 4.3.5.1.  EXT-X-INDEPENDENT-SEGMENTS |6|13|OPTIONAL|Not Supported|Supported|Supported|
| 4.3.5.2.  EXT-X-START  |6|12|OPTIONAL|Not Supported|Partially Supported|Partially Supported|
|&nbsp;&nbsp;&nbsp;  TIME-OFFSET|6|12|Attribute|Not Supported|Supported|Supported|
|&nbsp;&nbsp;&nbsp;  PRECISE|6|12|Attribute|Not Supported|Default "NO" supported|Default "NO" supported|



## Related topics

* [Media playback](media-playback.md)
* [Adaptive streaming](adaptive-streaming.md)
 

 




