---
title: What's new for the Xbox Live SDK - August 2016
author: KevinAsgari
description: What's new for the Xbox Live SDK - August 2016
ms.assetid: fa52e7bd-2c2c-4c25-94ab-761036a7ca79
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---


# What's new for the Xbox Live SDK - August 2016

Please see the [What's New - June 2016](1606-whats-new.md) article for what was added in the June 2016 release.

## OS and tool support
The Xbox Live SDK supports Windows 10 RTM [Version 10.0.10240] and Visual Studio 2015 RTM [Version 14.0.23107.0].

## Documentation
- If you are writing a UWP application, and are implementing the ability to invite users to a game, there are instructions on the ```.appxmanifest``` changes necessary in [Configure Your AppXManifest For Multiplayer](../multiplayer/service-configuration/configure-your-appxmanifest-for-multiplayer.md).  this was previously discussed on the [forums](https://forums.xboxlive.com) and the [porting xbox live code from era to uwp](../using-xbox-live/porting-xbox-live-code-from-xdk-to-uwp.md) article
- The [Introduction to Social Manager](../social-platform/intro-to-social-manager.md) article has been updated to reflect recent API changes, and provide more information about return codes for some of the functions.

## Unity Samples
Some new samples have been added for Unity developers writing a UWP application.
- There is now a Unity version of the Social sample, you can find this under the Samples/Social/Unity directory.
- There is also a sample describing how to use Connected Storage.  Please see Samples/GameSave/Unity for the sample.
There is a Unity version of AchievementsLeaderboard under Samples/AchievementsLeaderboard/Unity

## Social Manager
In addition to the documentation updates mentioned above, there are some new APIs that have been added.  These are described below, and you can see more detail in social_manager.h

- Added new API that allows updating of social groups without recreation:

```cpp
    _XSAPIIMP xbox_live_result<void> update_social_user_group(
        _In_ const std::shared_ptr<xbox_social_user_group>& group,
        _In_ const std::vector<string_t>& users
        );
```
- A completed update of social group is indicated by the
  ```social_user_group_updated``` event


## Multiplayer
Improved session browsing is now available and you can use new Multiplayer  APIs to utilize it.

Using the new APIs, you can filter on tags, strings, and other rich data to allow users to more easily find a session that they want to play.

We will be posting more comprehensive documentation in the coming months, but briefly you can now associate a "search handle" with an MPSD Session using ```set_search_handle``` and then users can search for sessions using a robust filtering mechanism by your title calling ```get_search_handles```

The new APIs are listed below.  Please try them out, and if you encounter any issues, post a support thread in the [forums](https://forums.xboxlive.com) or reach out to your DAM.  We will have examples of how to use these APIs soon.

```cpp
_XSAPIIMP pplx::task<xbox_live_result<void>> set_search_handle(
    _In_ multiplayer_search_handle_request searchHandleRequest
    );
```

```cpp
_XSAPIIMP pplx::task<xbox_live_result<std::vector<multiplayer_search_handle_details>>> get_search_handles(
    _In_ const string_t& serviceConfigurationId,
    _In_ const string_t& sessionTemplateName,
    _In_ const string_t& orderBy,
    _In_ bool orderAscending,
    _In_ const string_t& searchQuery
    );
```

```cpp
_XSAPIIMP pplx::task<xbox_live_result<void>> clear_search_handle(_In_ const string_t& handleId);
```

### Xbox Integrated Multiplayer

We have included documentation for the Xbox Integrated Multiplayer (XIM) API.  The API itself will be available in a subsequent release of the Xbox Live SDK, but the documentation and header are being made available for preview.

XIM is a self-contained interface for easily adding multiplayer real-time networking and chat communication to your game through the power of Xbox Live services.

This preview of the API’s documentation is shared here to encourage customer feedback and inquiry. We talked about this API earlier at Xfest 2016, and you can see archived [presentation material on the Xbox Developer Portal](https://developer.xboxlive.com/en-us/platform/documentlibrary/events/Pages/Xfest2016.aspx) from the “Turn-Key Multiplayer Networking and Chat” talk. Note that this preview documentation is only for the C++ API. WinRT equivalents for C# and other languages will be released later in the year.

If you are interested in XIM’s capabilities, have feedback or other questions about this project, please feel free to post on the [Xbox Developer Forum](https://forums.xboxlive.com/) or reach out through your developer account manager.

You can see this new documentation in xbox_integrated_multiplayer.chm in the Docs directory of the Xbox Live SDK.  The include file is available as a preview in \include\xim\XboxIntegratedMultiplayer.h.  
