---
title: Reading a binary blob
author: KevinAsgari
description: Learn about reading a binary blob in Xbox Live Title Storage.
ms.assetid: 9b8e0c35-0cea-4491-bf30-22fad224f11b
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, title storage
ms.localizationpriority: low
---

# Reading a binary blob in Xbox Live Title Storage

1.  Send a request using the *GET* method to read the data from title storage. This example uses global title storage.

        GET https://titlestorage.xboxlive.com/global/scids/{scid}/data/userinfo.bin,binary
        Content-Type: application/octet-stream
        x-xbl-contract-version: 1
        Authorization: XBL3.0 x=<userHash>;<STSTokenString>
        Connection: Keep-Alive



-   The user must be in the session to update it.

-   STSTokenString is a placeholder for brevity and should be replaced with the token returned by the authentication request.

#### Reference

**/global/scids/{scid}/data/{pathAndFileName},{type}**
