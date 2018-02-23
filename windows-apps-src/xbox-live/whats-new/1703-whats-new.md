---
title: What's new for the Xbox Live SDK - March 2017
author: KevinAsgari
description: What's new for the Xbox Live SDK - March 2017
ms.assetid: 03180585-6f87-4929-acfc-750bd78988a0
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# What's new for the Xbox Live SDK - March 2017

Please see the [What's New - December 2016](1612-whats-new.md) article for what was added in the December 2016 release.

## Xbox Services API

### Data Platform 2017

We have introduced a simplified Stats API.  Traditionally you had to send events corresponding to stat rules defined on XDP or Dev Center and these would update the stat values in the cloud.  We refer to this model as Stats 2013.

With Stats 2017, your title is now in control of your stat values.  You simply call an API with the most recent stat value, and that gets sent to the service directly without the need for events.  This uses the new `StatsManager` API and you can read more in [Player Stats](../leaderboards-and-stats-2017/player-stats.md)

### GitHub

Xbox Live API (XSAPI) is now available on GitHub at [https://github.com/Microsoft/xbox-live-api](https://github.com/Microsoft/xbox-live-api).  Using the binaries that come with the XDK or as NuGet packages is still recommended, however you are welcome to use the source and we welcome source code contributions.  

## Xbox Live Creators Program

The Xbox Live Creators Program is a developer program offering a subset of Xbox Live functionality to a broader developer audience.  If you are already in the ID@Xbox program, this will not have any impact on you.

You can read more about the program in [Developer Program Overview](../developer-program-overview.md).

## Documentation

There are the following new articles

| Article | Description |
|---------|-------------|
|[Xbox Live Service Configuration](../xbox-live-service-configuration.md) | Updated information on doing service configuration for your Xbox Live Title
| [Configure Xbox Live in Unity](../get-started-with-creators/configure-xbox-live-in-unity.md) | New information on Unity setup for Xbox Live Creators Program developers |
| [Xbox Live Sandboxes](../xbox-live-sandboxes.md) | A simplified guide to Xbox Live sandboxes and content isolation |
| [Xbox Live Test Accounts](../xbox-live-test-accounts.md) | Information about how test accounts work, and how to create them on Windows Dev Center |
