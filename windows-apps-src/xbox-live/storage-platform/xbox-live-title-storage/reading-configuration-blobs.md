---
title: Reading a configuration blob
author: KevinAsgari
description: Learn how to read a configuration blob in Xbox Live Title Storage.
ms.assetid: ee62d221-69b9-4f52-9b5d-5a44d04de548
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Reading a configuration blob in Xbox Live Title Storage

*Configuration blobs* are files in Xbox Live title storage that hold game data. The data are JSON objects that include virtual nodes that can be filtered before being delivered to the game. For more information about configuration blobs, see **Title Storage URIs**.

### To read a configuration blob

1.  Send a request using the below method to read the data from title storage.

        GET https://titlestorage.xboxlive.com/global/scids/{scid}/data/config.json,config              
        Content-Type: application/octet-stream
        x-xbl-contract-version: 1
        Authorization: XBL3.0 x=<userHash>;<STSTokenString>
        Connection: Keep-Alive


-   The user must be in the session to update it.
-   STSTokenString is a placeholder for brevity and should be replaced with the token returned by the authentication request.

#### Reference

**/global/scids/{scid}/data/{pathAndFileName},{type}**
**GET (/global/scids/{scid}/data/{pathAndFileName},{type})**
