---
title: Xbox Live Storage Platform
author: KevinAsgari
description: Learn about the Xbox Live Storage Platform, which includes Connected Storage and Title Storage.
ms.assetid: 3c92549c-65fd-4d26-a693-3aded8bae498
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Xbox Live Storage Platform - Connected Storage, Title Storage

Xbox Live enables publishers to store global title data and player specific data in the cloud.

## In this section

[Connected Storage](connected-storage/connected-storage-overview.md)
Data stored by using the per-user Connected Storage API automatically roams for users across PC and multiple Xbox One consoles, and is also available for use offline. Use this service to allow gameplay to continue smoothly when restarting a title after switching between devices. You should use the Connected Storage service to frequently save progress data like inventory, game state, and current location in game. The Connected Storage service is the more fault tolerant cloud storage service, and is less susceptible to network and power failure.

[Xbox Live Title Storage](xbox-live-title-storage/xbox-live-title-storage.md)
The Xbox Live Title Storage service provides a way to store and share game data and title assets in the cloud. Games running on all platforms can use this online. This service gives more control over data visibility for the consumer as well as global per title data in addition to per-user data. Title Storage is great for storing player statistics, player rankings and title assets like unlockable artwork, and new maps.