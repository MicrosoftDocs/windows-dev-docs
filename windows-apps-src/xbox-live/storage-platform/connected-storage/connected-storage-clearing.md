---
title: Clearing local storage
author: KevinAsgari
description: Learn how to clear the local storage of Connect Storage data.
ms.assetid: 0701b03e-88e4-4720-9744-ca174f3c947d
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
localizationpriority: medium
---

# Clearing local storage

All data written using the Connected Storage API is saved to the storage volume on development kits. The locally stored data can be deleted using the *xbstorage* tool to perform a factory reset of connected storage.

### To clear local storage

1.  Terminate the app associated with the data you want to clear.
2.  Run the *xbstorage* tool specifying the **reset** command and the **/force** switch.

        xbstorage reset /force

3.  Reboot the console.
