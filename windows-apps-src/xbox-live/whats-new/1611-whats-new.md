---
title: What's new for the Xbox Live SDK - November 2016
author: KevinAsgari
description: What's new for the Xbox Live SDK - November 2016
ms.assetid: 5cf9ba9d-5a15-4e62-bc1f-45ff8b8bf3b0
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - November 2016

Please see the [What's New - August 2016](1608-whats-new.md) article for what was added in the August 2016 release.

## Xbox Services API

### Simplified Achievements

* [Simplified Achievements](../achievements-2017/simplified-achievements.md) are a new and simpler way to configure and use achievements.  You no longer have to send events and have the Xbox Live services decide if your achievement is unlocked.  Your title is in charge of that decision.
* If you have been part of the early pilot of Simplified Achievements we have also added offline support.
* There's a new sample called SimplifiedAchievements that shows off how easy it is to use.

### Multiplayer

* [Session Browse](../multiplayer/session-browse.md) is a new way for your users to find a multiplayer game.  Session Browse allows players to search for a list of open multiplayer game sessions that meet specified criteria.
* The [Multiplayer Manager](../multiplayer/multiplayer-manager.md) now has auto-fill capabilities.  If enabled, Multiplayer Manager will find members via matchmaking to fill open slots during gameplay.
* A pre-production version of [XIM (Xbox Integrated Multiplayer)](../multiplayer/xbox-integrated-multiplayer.md) is now available for XDK development.  XIM is a self-contained interface for easily adding multiplayer real-time networking and chat communication to your game through the power of Xbox Live services.

### Social Manager

* Numerous [Social Manager](../social-platform/intro-to-social-manager.md) API changes:
	* Social manager has left the experimental namespace. Special thanks to those who were early adopters and gave feedback!
	* `xbox_social_user`
		* `string_t` objects have been changed to `char*` objects
		* Custom presence record with limit of six `social_manager_presence_title_record` per `social_manager_presence_record`
	* `social_event`
		* Returns a `std::vector<xbox_user_id_container>` instead of `std::vector<xbox_social_user>`
		* This vector can be passed into new API, `xbox_social_user_group::get_users_from_xbox_user_ids()`
	* `xbox_social_user_group`
		* `users()` API now returns a `std::vector<xbox_social_user*>`. These pointers become invalidated on the next call to `social_manager::do_work()`
		* `get_copy_of_users` return a `std::vector<xbox_social_user>` as a copy of the current users in the social_user_group to the caller. Calling this function may affect `do_work` completion time.
* Social Manager now will never fail after initialization. Social Manager will retry RTA automatically on disconnection. The `error` and `rta_disconnect_error` events have been deprecated and removed
* Title can specify custom memory allocators. See the new documentation: [Social Manager Memory and Performance](../social-platform/social-manager-memory-and-performance-overview.md)

### Xbox One UWP
* TCUI APIs adds support multi-user for Xbox One UWP apps.  The XSAPI C++ adds new Windows::System::User^ parameters specify the user, and the XSAPI WinRT API adds ForUserAsync APIs.
* Updated UWP samples to support multi-user on Xbox

### Other

* C++/WinRT support added.   More detail can be found [here](../introduction-to-xbox-live-apis.md)
* New functionality in to add and remove your own logging callbacks.  The diagnostic level will be passed to your callback so you can fine tune your behavior.  See `add_logging_handler` and `remove_logging_handler` in the `microsoft::xbox::services::system::xbox_live_services_settings` namespace

## Documentation
* There is new documentation on the [Multiplayer Manager](../multiplayer/multiplayer-manager.md), [XIM](../multiplayer/xbox-integrated-multiplayer.md), and [multiplayer concepts](../multiplayer/multiplayer-concepts.md) for Xbox Live.
* The [Xbox Live introduction](../get-started-with-partner/get-started-with-xbox-live-partner.md) sections have been rewritten.  If you are creating a new Xbox Live enabled title, or are curious about incorporating other Xbox Live functionality into your game, you can see the new docs [here](../get-started-with-partner/get-started-with-xbox-live-partner.md).

## Tools
* The Xbox Live Trace Analyzer which you can find at [http://aka.ms/XboxLiveTools](http://aka.ms/XboxLiveTools) now has a CERT mode.  
* Also in the Xbox Live Trace Analyzer is multi-console support.  If you pass in a Fiddler trace that contains HTTP requests for multiple console, separate reports will be generated for each one.
