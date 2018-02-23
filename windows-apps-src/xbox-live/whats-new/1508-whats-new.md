---
title: What's new for the Xbox Live SDK - August 2015
author: KevinAsgari
description: What's new for the Xbox Live SDK - August 2015
ms.assetid: a034867b-7cc0-4b97-89d5-3486e95a80b4
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - August 2015

Please see the [What's New - June 2015](1506-whats-new.md) article for what was added in the June 2015 release.

The August release of the Xbox Live SDK includes the following updates:

## OS and tool support
The Xbox Live SDK now supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Multiplayer Manager WinRT APIs
Multiplayer Manager (in the experimental namespace) now supports WinRT APIs (in addition to C++ APIs)

## Submit batch feedback from a title
Submits a number of feedback items at once from a title.

## New/Updated documentation
The Xbox Live SDK package now includes a "Docs" folder, contains updated API references and the new "Xbox Live programming guide".

Bug fixes:

* Crash while removing subscriptions in Real Time Activity Service
* Crash when logging in with a guest account
* Access violation when unplugging the network cable
* Tunnel failures now give an error code in the C++ APIs
* ETag issue with TitleStorageService::DownloadBlobAsync
* Various bug fixes for sample apps.
