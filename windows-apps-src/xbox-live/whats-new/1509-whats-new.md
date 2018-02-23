---
title: What's new for the Xbox Live SDK - September 2015
author: KevinAsgari
description: What's new for the Xbox Live SDK - September 2015
ms.assetid: 84b82fde-f6f3-4dc2-b2df-c7c7313a2cc3
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - September 2015

Please see the [What's New - August 2015](1508-whats-new.md) article for what was added in the August 2015 release.

The September release of the Xbox Live SDK includes the following updates:

## OS and tool support ##
The Xbox Live SDK supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Contextual Search APIs
* Enable your title or application to search for broadcasts from your game(s) with real time stats of your choosing.
* Please see the new APIs in Microsoft::Xbox::Services::ContextualSearch

## App Insights for Events

| Note |
|------|
| App Insights only applies to UWP titles.  If you are developing a XDK title, this section does not apply to you |

<p/>

* Events written using write_in_game_event() can be viewed using AppInsights
* Documentation will be coming on this in the future, in the meantime please work with your DAM to get access

## Logging
* service_call_logging_config in xbox::services::experimental
* To start and stop traces via xbTrace.exe on the console, you have to call register_for_protocol_activation on the service_call_logging_config class.  Make this call once during your game initialization.

## Resync for RTA
* Resync may occur when the RTA service believes that the users information may be out of date
* Titles should call corresponding HTTP calls for the subscriptions that they are subscribed to
* Titles do not have to resubscribe
* Added xbox::services::real_time_activity_service::add_resync_handler
* Removed xbox::services::real_time_activity_service::remove_resync_handler
* Added http_status_429_too_many_requests
* This error condition will be seen when a title is being throttled for sending too many http requests

## Documentation
* Migrating to Xbox Live Services API 2.0
* Error Handling
* Xbox Live Authentication in Windows 10
* Contextual Search
