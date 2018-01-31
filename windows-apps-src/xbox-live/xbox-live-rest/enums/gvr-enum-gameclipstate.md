---
title: GameClipState Enumeration
assetID: 97fe5c1e-f7b5-537e-69eb-8284b69cd3e1
permalink: en-us/docs/xboxlive/rest/gvr-enum-gameclipstate.html
author: KevinAsgari
description: ' GameClipState Enumeration'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# GameClipState Enumeration
Details the GameClipState enumeration. 
<a id="ID4ET"></a>

 
## GameClipState
 
| <b>Enumerator</b>| <b>Description</b>| 
| --- | --- | 
| None | Game clip service state is unknown or not set.| 
| PendingUpload | Game clip service is waiting for the asset upload.| 
| PendingDelete | Game clip is in the queue for deletion. (Effectively means it is "deleted").| 
| Processed | Game clip has finished all processing.| 
| Processing| Game clip is being processed (encoding, thumbnails, etc.).| 
| Publishing| Game clip assets are being published.| 
| Published| Game clip assets are published – this state indicates it is all set to view.| 
| Flagged| Game clip has been flagged for enforcement.| 
| Banned| Game clip has been banned but not yet deleted.| 
| Uploaded| Game clip has completed upload.| 
| Deleted| Game clip has been deleted.| 
| Error| Game clip is in an error state and unusable.| 
  