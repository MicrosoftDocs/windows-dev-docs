---
title: Get started with Xbox Live Creators Program
author: KevinAsgari
description: Provides links to help you get started with the Xbox Live Creators Program.
ms.assetid: 2a744405-7ee4-42b4-8f36-9916e8c3a530
ms.author: kevinasg
ms.date: 04-04-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---
# Get started with the Xbox Live Creators Program
 
The Xbox Live Creators Program allows you to quickly and directly publish your games to Xbox One and Windows 10, with a simplified certification process and no concept approval required. If your game integrates Xbox Live and follows our [standard Store policies](https://msdn.microsoft.com/en-us/library/windows/apps/dn764944.aspx), you are ready to publish. This article will outline the steps needed to get your game up and running with Xbox Live integration. 

Xbox Live Creators Program games must be a Universal Windows Platform (UWP) application. For Xbox One, see [UWP on Xbox One](https://msdn.microsoft.com/en-us/windows/uwp/xbox-apps/index) and specifically [System resources for UWP apps and games on Xbox One](https://msdn.microsoft.com/en-us/windows/uwp/xbox-apps/system-resource-allocation). Games published through the Xbox Live Creators Program do not have access to the achievements or online multiplayer services. For a full list of supported services, see the [Developer program overview feature table](https://docs.microsoft.com/en-us/windows/uwp/xbox-live/developer-program-overview#feature-table).

## 1. Ensure you have a title created on Dev Center
Every Xbox Live title must be defined on Dev Center before you will be able to sign-in and make Xbox Live Service calls.  [Creating a new Creators title](create-and-test-a-new-creators-title.md) will show you how to do this.

## 2. Follow the appropriate guide to setup your IDE or game engine
You can follow the appropriate "getting started guide" for your platform and engine and learn the basics of Xbox Live as you go along:

* [Develop a Creators title with Visual Studio](develop-creators-title-with-visual-studio.md) will show you how to link your Visual Studio project with your Xbox Live configuration on Dev Center.
* [Develop a Creators title with Unity](develop-creators-title-with-unity.md) will show you how to create a new Xbox Live enabled Unity game, handle single user and multi-user sign in, add features such as leaderboards and stats, and generate a native Visual Studio project.

## 3. Xbox Live concepts & testing
Once you have a title created, you should learn about the Xbox Live concepts that will affect your experience developing titles. It's also important to test your game on all of the platforms that it will support to ensure that it behaves as expected.

- [Xbox Live service configuration for the Creators Program](xbox-live-service-configuration-creators.md)
- [Xbox Live test environment](../xbox-live-sandboxes.md)
- [Authorize Xbox Live accounts](authorize-xbox-live-accounts.md)

## 4. Enable Xbox Live sign-in
All Xbox Live Creators Program games must integrate Xbox Live sign-in and display the user identity (Gamertag, Gamerpic, etc.). You can choose to automatically sign in the user or have them push a button to initiate it. The Gamertag must be displayed once signed in so that the player can validate that they are using the right profile.

- [Xbox Live Social Platform - Profile, Friends, Presence](../social-platform/social-platform.md)

## 5. Add optional Xbox Live features

Xbox Live Creators Program offers an array of features designed to help promote your game and keep gamers engaged:

- [Xbox Live Data Platform - Stats, Leaderboards](../data-platform/data-platform.md) help drive engagement of your game by letting gamers compete to beat their friends and move up the ranks.
- [Xbox Live Storage Platform - Connected Storage, Title Storage](../storage-platform/storage-platform.md) offers free save game roaming between devices so gamers can easily continue game progress between Xbox One and Windows PC.
- [Xbox Live Social Platform - Profile, Friends, Presence](../social-platform/social-platform.md) lets gamers connect with friends and talk about your game.

It is important to note that the Xbox Live Creators Program does not support online multiplayer, achievements, or gamerscore.

## 6. Release your game

If you are using the Xbox Live Creators Program, then the process is no different than releasing any other UWP application:

- [Windows Store Policies](https://msdn.microsoft.com/en-us/library/windows/apps/dn764944.aspx) including [Gaming and Xbox Policies](https://msdn.microsoft.com/en-us/library/windows/apps/dn764944.aspx#pol_10_13)
- [Publish Windows apps](https://developer.microsoft.com/en-us/store/publish-apps)