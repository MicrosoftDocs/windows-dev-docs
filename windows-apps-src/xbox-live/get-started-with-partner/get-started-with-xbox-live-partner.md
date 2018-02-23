---
title: Get started with Xbox Live as a partner
author: KevinAsgari
description: Provides links to help a managed partner or ID@Xbox member get started with Xbox Live development.
ms.assetid: 69ab75d1-c0e7-4bad-bf8f-5df5cfce13d5
ms.author: kevinasg
ms.date: 06/07/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, partner, ID@Xbox
ms.localizationpriority: low
---

# Get started with Xbox Live as a managed partner or an ID@Xbox developer

This section covers getting started with Xbox Live as a managed partner or as an ID@Xbox developer. Partners and ID developers can access the full range of Xbox Live features, including achievements, multiplayer, and more.

Managed partners and ID@Xbox developers can develop Xbox Live titles for both the Universal Windows Platform (UWP) or the Xbox Developer Kit (XDK) platform.

In addition to the content available here, there is also additional documentation that is available to partners with an authorized Dev Center account. You can access that content here: [Xbox Live partner content](https://developer.microsoft.com/en-us/games/xbox/docs/xboxlive/xbox-live-partners/partner-content).

## Why should you use Xbox Live?

Xbox Live offers an array of features designed to help promote your game and keep gamers engaged:

- Xbox Live social platform lets gamers connect with friends and talk about your game.
- Xbox Live achievements helps your game get popular by giving your game free promotion to the Xbox Live social graph when gamers unlock achievements.
- Xbox Live leaderboards helps drive engagement of your game by letting gamers compete to beat their friends and move up the ranks.
- Xbox Live multiplayer lets gamers play with their friends or a get matched with other players to compete or cooperate in your game.
- Xbox Live connected storage offers free save game roaming between devices so gamers can easily continue game progress between Xbox One and Windows PC.

## 1. Choose a platform
Decide between making an Xbox Development Kit (XDK), Universal Windows Platform (UWP), or cross-play game:

- XDK based games only run on the Xbox One console
- UWP games can target any Windows platform such as Windows PC, Windows Phone, or Xbox One
  - For Xbox One, see [UWP on Xbox One](https://msdn.microsoft.com/en-us/windows/uwp/xbox-apps/index) and specifically [System resources for UWP apps and games on Xbox One](https://msdn.microsoft.com/en-us/windows/uwp/xbox-apps/system-resource-allocation)
- Cross-play games are typically games that target Xbox One and Windows PC using both the XDK and UWP paths.

## 2. Ensure that you have a title created on Dev Center or XDP
Every Xbox Live title must be defined on Dev Center or the Xbox Developer Portal (XDP) before you will be able to sign-in and make Xbox Live Service calls.  [Creating A New Title](create-a-new-title.md) will show you how to do this.

## 3. Follow the appropriate guide to setup your IDE or game engine
You can follow the appropriate Getting Started guide for your platform and engine and learn the basics of Xbox Live as you go along.

* [Getting started using Visual Studio for UWP games](get-started-with-visual-studio-and-uwp.md) will show you how to link your Visual Studio project with your Xbox Live configuration on Dev Center.
* [Getting started using Unity for UWP games](partner-add-xbox-live-to-unity-uwp.md) will show you how to create a new Xbox Live enabled Unity title, add features such as Leaderboards to your title, and generate a native Visual Studio project.
* [Getting started using Visual Studio for XDK based games](xdk-developers.md) will show you how to get your Visual Studio project setup if you are making an Xbox One title using the XDK.
* [Getting started making a cross-play game](get-started-with-cross-play-games.md) explains how to make a product that is an XDK based game for Xbox One and a UWP based game for Windows 10 PC.

## 4. Xbox Live Concepts
Once you have a title created, you should learn about the Xbox Live concepts that will affect your experience developing titles:

- [Xbox Live sandboxes](../xbox-live-sandboxes.md)
- [Xbox Live test accounts](../xbox-live-test-accounts.md)
- [Introduction to Xbox Live APIs](../introduction-to-xbox-live-apis.md)

## 5. Add Xbox Live Features

Add Xbox Live features to your game:

- [Xbox Live Social Platform - Profile, Friends, Presence](../social-platform/social-platform.md)
- [Xbox Live Data Platform - Stats, Leaderboards, Achievements](../data-platform/data-platform.md)
- [Xbox Live Multiplayer Platform - Invite, Matchmaking, Tournaments](../multiplayer/multiplayer-intro.md)
- [Xbox Live Storage Platform - Connected Storage, Title Storage](../storage-platform/storage-platform.md)
- [Contextual Search](../contextual-search/introduction-to-contextual-search.md)

## 6. Release Your Title

ID@Xbox and managed partners must go through a certification process before releasing their titles.  This is because the titles have access to additional features such as online multiplayer, achievements, and rich presence.  Once you are ready to release your title, contact your Microsoft representative for more details on the submission and release process.
