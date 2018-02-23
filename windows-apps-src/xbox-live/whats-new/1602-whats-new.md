---
title: What's new for the Xbox Live SDK - February 2016
author: KevinAsgari
description: What's new for the Xbox Live SDK - February 2016
ms.assetid: 7ff34ea4-f07d-4584-98e4-13977994ccca
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - February 2016

Please see the [What's New - October 2015](1510-whats-new.md) article for what was added in 1510

## OS and tool support
The Xbox Live SDK supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Throttling
- Fine-grained throttling will soon be rolled out to most Xbox Live Services.  Xbox Service API (XSAPI) will automatically handle retries and inform you of calls that are throttled during development.  More details can be found in the [Best Practices Calling Xbox Live](../using-xbox-live/best-practices/best-practices-for-calling-xbox-live.md) article in the Documentation.

## Leaderboards
- Multicolumn leaderboards can now be accessed by the GetLeaderboard API. If you provide a vector of the names of the additional columns, the vector of columns on the result will be filled out if those columns exist.

## Documentation
- [Application Insights](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/event-driven-data-platform/application-insights) documentation is here.  You can use Application Insights with a free Azure account to view Data Platform events in near-realtime.  This functionality is currently only available for UWP applications running on Windows 10 on the desktop.
- Updated documentation on the Xbox Common Events Tool for UWP developers discussing how to generate wrappers for sending Data Platform events.  Please note that this is optional and you can continue to use the WriteInGameEvent API if you prefer.
- Using Fiddler to debug Data Platform events and make sure they are properly being sent.  This is only for UWP events.
- Information on how to collect logs for the Live Trace Analyzer tool is available.  See the [Analyze calls to Xbox Live Services](../tools/analyze-service-calls.md) article.
