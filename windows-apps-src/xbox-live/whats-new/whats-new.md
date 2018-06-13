---
title: What's new for the Xbox Live
author: PhillipLucas
description: What's new for the Xbox Live SDK
ms.author: sthaff
ms.date: 10/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for Xbox Live
You can also check the [Xbox Live API GitHub commit history](https://github.com/Microsoft/xbox-live-api/commits/master) to see all of the recent code changes to the Xbox Live APIs.

#### In this article

* [June 2018](#june-2018)
* [August 2017](#august-2017)
* [July 2017](#july-2017)
* [June 2017](#june-2017)
* [May 2017](#may-2017)
* [April 2017](#april-2017)
* [March 2017](#march-2017)
* [Archived](#archived)

## June 2018

### Xbox Live Features

#### C API layer for XSAPI

C APIs are now available for some Xbox Live features. The new API layer provides a number of benefits for the supported features, including custom memory management, manual thread management for asynchronous tasks, and a new HTTP library.

For more information, see [Xbox Live C APIs](../xsapi-flat-c.md).

## August 2017

### Xbox Live features

#### In-Game clubs

Developers can now create "in-game clubs". In-game clubs differ from standard Xbox clubs in that they are fully customizable by a developer and can be used both inside and outside of the game. As a game developer, you can use them to quickly build any type of persistent group scenarios inside your games such as teams, clans, squads, guilds, etc. that match your unique requirements.

Xbox live members can access in-game clubs outside of your game across any Xbox experience to stay connected to each other and to your game by using club features like chat, feed, LFG, and Mixer freely on Xbox console, PC, or iOS/Android devices.

APIs are available to create & manage in-game clubs directly from within your game. These APIs exist in the xbox::services::clubs namespace.


## July 2017

### Xbox Live features
#### Multiplayer updates

Querying activity handles and search handles now includes the custom session properties in the response.

#### Tournaments

New APIs have been added to support tournaments. You can now use the xbox::services::tournaments::tournament_service class to access the tournaments service from your title.
These new tournament APIs enable the following scenarios:
* Query the service to find all existing tournaments for the current title.
* Retrieve details about a tournament from the service.
* Query the service to retrieve a list of teams for a tournament.
* Retrieve details about the teams for a tournament from the service.
* Track changes to tournaments and teams by using Real Time Activity (RTA) subscriptions.

## June 2017

### Xbox Live features

#### Game Chat 2

An updated and improved version of Game Chat is now available. For more information, see the [Game Chat 2 overview](../multiplayer/chat/game-chat-2-overview.md).

### Xbox Live tools

#### Xbox Live PowerShell Module

* PowerShell modules have been added to make it easier to switch sandboxes on your development machine. For more information, see [Tools](../tools/tools.md)

#### Bug fixes

* Various bug fixes. Check the [GitHub commit history](https://github.com/Microsoft/xbox-live-api/commits/master) for a full list.

## May 2017

### Xbox Services APIs

#### Multiplayer

* Querying activity handles and search handles now includes the custom session properties in the response.

#### Bug fixes

* Fixed "bad json" being returned instead of a valid HTTP error code.

## April 2017

### Xbox Services APIs

#### Visual Studio 2017

The Xbox Live APIs have been updated to support Visual Studio 2017, for both Universal Windows Platform (UWP) and Xbox One titles.

#### Tournaments

New APIs have been added to support tournaments. You can now use the `xbox::services::tournaments::tournament_service` class to access the tournaments service from your title.

These new tournament APIs enable the following scenarios:

* Query the service to find all existing tournaments for the current title.
* Retrieve details about a tournament from the service.
* Query the service to retrieve a list of teams for a tournament.
* Retrieve details about the teams for a tournament from the service.
* Track changes to tournaments and teams by using Real Time Activity (RTA) subscriptions.

## March 2017

### Xbox Services API

#### Data Platform 2017

We have introduced a simplified Stats API.  Traditionally you had to send events corresponding to stat rules defined on XDP or Dev Center and these would update the stat values in the cloud.  We refer to this model as Stats 2013.

With Stats 2017, your title is now in control of your stat values.  You simply call an API with the most recent stat value, and that gets sent to the service directly without the need for events.  This uses the new `StatsManager` API and you can read more in [Player Stats](../leaderboards-and-stats-2017/player-stats.md)

#### GitHub

Xbox Live API (XSAPI) is now available on GitHub at [https://github.com/Microsoft/xbox-live-api](https://github.com/Microsoft/xbox-live-api).  Using the binaries that come with the XDK or as NuGet packages is still recommended, however you are welcome to use the source and we welcome source code contributions.  

### Xbox Live Creators Program

The Xbox Live Creators Program is a developer program offering a subset of Xbox Live functionality to a broader developer audience.  If you are already in the ID@Xbox program, this will not have any impact on you.

You can read more about the program in [Developer Program Overview](../developer-program-overview.md).

### Documentation

There are the following new articles:

| Article | Description |
|---------|-------------|
|[Xbox Live Service Configuration](../xbox-live-service-configuration.md) | Updated information on doing service configuration for your Xbox Live Title
| [Configure Xbox Live in Unity](../get-started-with-creators/configure-xbox-live-in-unity.md) | New information on Unity setup for Xbox Live Creators Program developers |
| [Xbox Live Sandboxes](../xbox-live-sandboxes.md) | A simplified guide to Xbox Live sandboxes and content isolation |
| [Xbox Live Test Accounts](../xbox-live-test-accounts.md) | Information about how test accounts work, and how to create them on Windows Dev Center |

## Archived

* [December 2016](1612-whats-new.md)
* [November 2016](1611-whats-new.md)
* [August 2016](1608-whats-new.md)
* [June 2016](1606-whats-new.md)
* [April 2016](1603-whats-new.md)
* [March 2016](1603-whats-new.md)
* [February 2016](1602-whats-new.md)
* [October 2015](1510-whats-new.md)
* [September 2015](1509-whats-new.md)
* [August 2015](1508-whats-new.md)
* [June 2015](1506-whats-new.md)
