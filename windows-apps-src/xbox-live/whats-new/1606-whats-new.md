---
title: What's new for the Xbox Live SDK - June 2016
author: KevinAsgari
description: What's new for the Xbox Live SDK - June 2016
ms.assetid: 306e7fa8-14f0-437f-a991-6693f5c0d6a8
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - June 2016

Please see the [What's New - April 2016](1604-whats-new.md) article for what was added in the April 2016 release.

> **Note:** If you are using the Xbox Developers Kit (XDK), then the *What's New - April 2016* article covers additional new functionality that was added since the March XDK release.

## OS and tool support
The Xbox Live SDK supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Contextual Search
The following new classes have been added to the `contextual_search` API to retrieve game clips:

* `contextual_search_game_clip`
* `contextual_search_game_clip_stat`
* `contextual_search_game_clip_thumbnail`
* `contextual_search_game_clip_uri_info`
* `contextual_search_game_clips_result`

See the reference documentation for more information.

## Networking
The Xbox Live SDK now includes a new assert that detects if 5 or more websockets are created per user in a single title instance.

You can disable this assert by calling `disable_asserts_for_maximum_number_of_websockets_activated()`.

> **Note:** Social Manager and Multiplayer Manager use a single combined websocket if you use these features in your title.

## Tools
* The Xbox Live Resiliency Plugin For Fiddler is now included in the Xbox Live SDK.  
It is an add-on to Fiddler that enables developers to selectively block calls to Xbox Live.
Using this plugin, developers can test resiliency across partial service disruptions in their game titles.
The tool includes a number of Xbox Live service endpoints built-in and different error types to test against.
All Xbox One and UWP titles are supported.
