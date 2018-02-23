---
title: Reading a JSON blob
author: KevinAsgari
description: Learn how to read a JSON blob in Xbox Live Title Storage.
ms.assetid: 3697af16-d054-4835-af7f-7fee8c628345
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Reading a JSON blob in Xbox Live Title Storage

1.  Send a request using the *GET* method to read the data from title storage. This example uses global title storage.

        GET https://titlestorage.xboxlive.com/global/scids/{scid}/data/surprise.json,json
        Content-Type: application/octet-stream
        x-xbl-contract-version: 1
        Authorization: XBL3.0 x=<userHash>;<STSTokenString>
        Connection: Keep-Alive

-   The user must be in the session to update it.

-   STSTokenString is a placeholder for brevity and should be replaced with the token returned by the authentication request.

#### Reference

**/global/scids/{scid}/data/{pathAndFileName},{type}**
