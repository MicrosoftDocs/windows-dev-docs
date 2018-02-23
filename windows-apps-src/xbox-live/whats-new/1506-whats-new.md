---
title: What's new for the Xbox Live SDK - June 2015
author: KevinAsgari
description: What's new for the Xbox Live SDK - June 2015
ms.assetid: 354bcd47-2564-4dd5-89e3-242bca462b35
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - June 2015

The June release of the Xbox Live SDK includes the following updates:

## OS and tool support ##
The Xbox Live SDK now supports the latest Insider build of Windows 10 and Visual Studio 2015 RC.

## Title Callable UI APIs

| Note |
|------|
| This section only applies to UWP titles, XDK titles should refer to the Windows.Xbox.UI.SystemUI class for TCUI  |

The Xbox Live SDK now contains wrapper APIs that support Title Callable UI (TCUI) for displaying stock UI on a Windows 10 PC/Mobile device.

These APIs are only available for UWP apps.

You can call the TCUI wrappers from the TitleCallableUI (WinRT) and title_callable_ui (C++) classes.

The stock UIs include:
* A player picker UI
* A game invite picker UI
* A player profile card UI
* An add/remove friend UI
* A show game title achievements UI

See the new *TCUI* sample for example usage of the new APIs. You can find the sample under {*SDK source root*}\Samples\TCUI.

## New authentication model for UWP apps
The Xbox Live SDK now supports using a Microsoft Account (MSA) for identifying the Xbox Live player on a Windows 10 PC/Mobile device.

You can now sign in a user by using the XboxLiveUser (WinRT) and xbox_live_user (C++) classes.

## New API for writing events in UWP apps

| Note |
|------|
| This section only applies to UWP titles.  XDK developers should refer to the [Game Events](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/event-driven-data-platform/game-events) article for XDK specific information  |

The new EventsService (WinRT) and events_service (C++) classes let you write in-game events that can update user stats, achievements, leaderboards, etc. These new classes are for UWP apps only.

## Breaking change to event handlers ##
All event handlers in the C++ SDK have been changed from a single `void set_*_handler()` method to a pair of `function_context add_*_handler()` and `void remove_*_handler(function_context context)` methods.

Each `add_*_handler()` method now returns a `function_context` object. When you remove the event handler, you must pass in the `function_context` object.

For example:
```
function_context subscriptionLostContext = xbox_live_context()->multiplayer_service().add_multiplayer_subscription_lost_handler(...);

localUser->xbox_live_context()->multiplayer_service().remove_multiplayer_subscription_lost_handler(subscriptionLostContext);
```

## New APIs for managing multiplayer scenarios
The Xbox Live SDK now includes a set of C++ APIs for managing common multiplayer scenarios. The APIs are included in the xbox.services.experimental.multiplayer namespace.

These APIs are in the experimental namespace which means that they are likely to change based on feedback.  We encourage developers to use them and provide feedback.

See the new *MultiplayerManager* sample for example usage of these new APIs. You can find the sample under {*SDK source root*}\Samples\MultiplayerManager.
