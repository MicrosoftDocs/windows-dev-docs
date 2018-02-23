---
title: What's new for the Xbox Live SDK - April 2016
author: KevinAsgari
description: What's new for the Xbox Live SDK - April 2016
ms.assetid: a6f26ffd-f136-4753-b0cd-92b0da522d93
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - April 2016

Please see the [What's New - March 2016](1603-whats-new.md) article for what was added in 1603

## OS and tool support
The Xbox Live SDK supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Tournaments
- The Xbox Live Tournaments Tool is now included with the SDK.  You can see it in the Tools directory, along with some information on how to use it.
- The APIs for Tournaments are also available.  See the Xbox::Services::Tournaments namespace
- Documentation is also available in the Programming Guide.

## Documentation
- [Sign-in Troubleshooting Guide](../using-xbox-live/troubleshooting/troubleshooting-sign-in.md) lists some general strategies to debug sign-in failures, as well as steps to follow based on error code.
- The [Marketplace](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/xbox-marketplace/marketplace-and-downloadable-content) docs for Xbox One developers only can now be found in the Programming Guide.  UWP developers should continue to consult Windows Dev Center for documentation on the store.
- There is an [XDK to UWP porting guide](../using-xbox-live/porting-xbox-live-code-from-xdk-to-uwp.md) you can refer to if you are interested in bringing an Xbox One title to the Universal Windows Platform.
- Please see the [Fine-Grained Rate Limiting](../using-xbox-live/best-practices/fine-grained-rate-limiting.md) article for a description of how these are enforced for various Xbox Live Service endpoints and scenarios, as well as information about what the limits are.

## Multiplayer Manager
The [Multiplayer Manager](../multiplayer/multiplayer-manager.md) is no longer in experimental.  We have incorporated feedback from developers using this API and made some of the APIs more consistent with each other.  Please use the Multiplayer Manager as a starting point when doing your multiplayer development, as it provides a simpler API that manages many of the complexities of the Multiplayer 2015 API for you.

In the below sections, we have listed some of the new features to the API, as well as a small number of breaking changes.

#### Completed events
All APIs now have a corresponding``` _competed``` event and all events are fired regardless of success or failure. The previous behavior was that it only triggered upon failure, for the title to take action on. And since calls are batched, it means that upon completion, the title will get multiple ```_competed``` events.

| API | Returned Event |
|-----|----------------|
| ```lobby_session()->set_local_member_properties``` |  ```local_member_property_write_completed ```
| ```lobby_session()->set_local_member_connection_address``` | ```local_member_connection_address_write_completed``` |
| ```lobby_session()->set_properties``` | ```session_property_write_completed``` |
| ```lobby_session()->set_synchronized_properties``` | ```session_synchronized_property_write_completed``` |
| ```lobby_session()->set_synchronized_host``` | ```synchronized_host_write_completed``` |

The same applies for ```game_session()```.

#### Application defined context
You can now pass in an optional application-defined context for each set_* methods to correlate the multiplayer event to the initiating call.
For example

```cpp
_XSAPIIMP xbox_live_result<void> set_properties(
        _In_ const string_t& name,
        _In_ const web::json::value& valueJson,
        _In_ context_t context = nullptr
       );
```

The context would then be returned back to the title through the ```_completed``` event.

#### Breaking Changes

1.	Both invite APIs (```invite_friends``` & ```invite_users```) are now synchronous. Upon completion, it returns an invite_sent event.

2.	```write_synchronized_properties_and_commit``` was renamed to ```set_synchronized_properties```. Upon completion, it returns a ```session_synchronized_property_write_completed``` event.

3.	```write_synchronized_host_and_commit``` was renamed to ```set_synchronized_host```. Upon completion, it returns a ```synchronized_host_write_completed``` event.

4.	On ```lobby_session()```

  *Removed*

```cpp
_XSAPIIMP const std::unordered_map<string_t, xbox::services:: multiplayer::multiplayer_session_tournaments_server& tournaments_server() const;
```

  *Added*

```cpp
_XSAPIIMP const std::unordered_map<string_t, xbox::services::tournaments::tournament_team_result>& tournament_team_results() const;
```

5.	On ```game_session()```

  *Removed*

```cpp
_XSAPIIMP const std::unordered_map<string_t, xbox::services:: multiplayer::multiplayer_session_tournaments_server& tournaments_server() const;
_XSAPIIMP const std::unordered_map<string_t, xbox::services:: multiplayer::multiplayer_session_arbitration_server& arbitration_server() const;
```
  *Added*

```cpp
_XSAPIIMP const std::unordered_map<string_t, multiplayer_session_reference>& tournament_teams() const;
_XSAPIIMP const std::unordered_map<string_t, xbox::services::tournaments::tournament_team_result>& tournament_team_results() const;
```

6.	Change in event types:

  *Removed*

```cpp
write_pending_changes_failed,
tournament_property_changed,
arbitration_property_changed
```

  *Renamed*

  ```write_synchronized_properties_completed``` renamed to ```session_synchronized_property_write_completed```

  ```write_synchronized_host_completed``` renamed to ```synchronized_host_write_completed```
