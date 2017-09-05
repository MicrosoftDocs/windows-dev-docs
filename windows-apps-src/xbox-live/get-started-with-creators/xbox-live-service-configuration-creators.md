---
title: Xbox Live Creators service configuration
author: KevinAsgari
description: Learn about Xbox Live service configuration for the Creators Program.
ms.assetid: 22b8f893-abb3-426e-9840-f79de0753702
ms.author: kevinasg
ms.date: 04-04-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---

# Xbox Live service configuration for the Creators Program

## What is Service Configuration?

You may be familiar with some of the Xbox Live features such as [Achievements](../achievements-2017/achievements.md), [Leaderboards](../leaderboards-and-stats-2017/leaderboards.md) and [Matchmaking](../multiplayer/multiplayer-concepts.md#SmartMatch).

In case you are not, we'll briefly explain Leaderboards as an example.  Leaderboards allow players to see a value representing an accomplishment, in comparison to other players.  For example high scores in an arcade game, lap times in a racing game, or headshots in a first-person shooter.  But unlike an arcade machine which only shows the top scores from the players who have played on that physical machine, with Xbox Live it is possible to display high scores from around the world.

But for this to happen, you need to perform some one-time configuration so that Xbox Live knows about your leaderboard.  For example whether the values should be sorted in ascending or descending value, and what piece of data it should be sorting.

This configuration happens on [Dev Center](http://dev.windows.com) for Xbox Live Creators Program, and you can read [Getting Started With Xbox Live](get-started-with-xbox-live-creators.md) to learn how to get set up.

## Get your IDs

To enable Xbox Live services, you will need to obtain several IDs to configure your development kit and your title. These can be obtained by doing Xbox Live service configuration.

If you do not currently have a title in Dev Center, see [Create and test a new Creators title](create-and-test-a-new-creators-title.md) for guidance.

### Critical IDs

There are three IDs which are critical for development of titles and applications for Xbox One: the sandbox ID, the TitleID, and the service configuration ID (SCID).

While it is necessary to have a sandbox ID to use a development kit, the TitleID and SCID are not required for initial development but are required for any use of Xbox Live services. We therefore recommend that you obtain all three at once.

#### Sandbox IDs

The sandbox provides content isolation for your development kit during development, ensuring that you have a clean environment for developing and testing your title. The Sandbox ID identifies your sandbox. A console may only access one sandbox at any one time, though one sandbox may be accessed by multiple consoles.

Sandbox IDs are case sensitive.

You can get your Sandbox ID on the "Xbox Live" root configuration page as shown below:

![](../images/getting_started/devcenter_sandbox_id.png)

#### Service Configuration ID (SCID)

As a part of development, you will create events, achievements, and a host of other online features. These are all part of your Service Configuration, and require the SCID for access. A given title may have multiple Service Configurations; each will have its own SCID.

SCIDs are case sensitive.

To retrieve your SCID on Dev Center, navigate to the Xbox Live Services section and go to *Xbox Live Setup* .  Your SCID is displayed in the table shown below:

![](../images/getting_started/devcenter_scid.png)

#### TitleID

The TitleID uniquely identifies your title to Xbox Live services. It is used throughout the services to enable your users to access your title's Live content, their user statistics, achievements, and so forth, and to enable Live multiplayer functionality.

Title IDs can be case sensitive, depending on how and where they are used.

Your Title ID on Dev Center is found in the same table as the SCID in the *Xbox Live Setup* page.
