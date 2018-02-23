---
title: What's new for the Xbox Live SDK - October 2015
author: KevinAsgari
description: What's new for the Xbox Live SDK - October 2015
ms.assetid: 052be4aa-5f18-4eb7-ba5f-80c5f5cab6f2
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - October 2015

Please see the [What's New - September 2015](1509-whats-new.md) article for what was added in the September 2015 release.


## OS and tool support
The Xbox Live SDK supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Social Manager
* The Social Manager is intended to ease the use of the Xbox Live social APIs.  It will intelligently cache a user's social graph, provide a simpler API and more.  Please see the documentation for more information.

## Samples
* We have a new Social Manager sample demonstrating the API.

## Tools
* The Xbox Live Trace Analyzer is now included in the Xbox Live SDK.  Collect traces as described in the [Analyze calls to Xbox Live Services](../tools/analyze-service-calls.md), and then run the Live Trace Analyzer to ensure your title is using Xbox Live in an optimal way by viewing statistics about repeated calls, call frequency, and more.

## Bug Fixes
* Changed default timeout for HTTP socket operations to 30 seconds from 5 minutes.  For long running HTTP socket operations such as Title Storage upload and download calls, those remain using a 5 minute timeout.

## Documentation
* Introduction to the Social Manager added
* Introduction to the Multiplayer Manager updated
