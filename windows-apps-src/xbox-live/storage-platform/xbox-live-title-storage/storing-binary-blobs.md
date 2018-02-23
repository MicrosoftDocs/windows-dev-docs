---
title: Storing a binary blob
author: KevinAsgari
description: Learn how to store a binary blob in Xbox Live Title Storage.
ms.assetid: a0da36ef-5a5a-466d-80a8-e055ba7e4cdc
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, title storage
ms.localizationpriority: low
---

# Storing a binary blob in Xbox Live Title Storage

1.  Send a request using the below method to send the data to title storage.

        PUT https://titlestorage.xboxlive.com/trustedplatform/users/xuid(1245111)/scids/{scid}/data/lastturn.bin,binary              
        Content-Type: application/octet-stream
        x-xbl-contract-version: 1
        Authorization: XBL3.0 x=<userHash>;<STSTokenString>
        Content-Length: 40
        Connection: Keep-Alive


-   The user must be in the session to update it.

-   STSTokenString is a placeholder for brevity and should be replaced with the token returned by the authentication request.

2.  Send the binary data. Since the data will be transferred through HTTP, the data must be constrained to the acceptable character set. Information such as image or audio data must be encoded. You may select any encoding method that generates HTTP compatible characters.
d
```
  01EAEFBAD05903A4
  1EA2311656677DFF
  CF00
```

#### Reference

**/trustedplatform/users/xuid({xuid})/scids/{scid}/data/{pathAndFileName},{type}**
