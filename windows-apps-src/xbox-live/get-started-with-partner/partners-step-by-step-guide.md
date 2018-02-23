---
title: Step by step guide for Xbox Live partners
author: KevinAsgari
description: Provides a guideline of steps to integrate Xbox Live for managed partners.
ms.assetid: f0c9db8f-f492-4955-8622-e1736f0a4509
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Step by step guide to integrate Xbox Live for managed partners and ID@Xbox members

This section will help you get up and running with Xbox Live:

## 1. Choose a Platform
Decide between making an Xbox Development Kit (XDK), Universal Windows Platform (UWP), or cross-play game.

- XDK based games only run on the Xbox One console
- UWP games can target any Windows platform such as Windows PC, Windows Phone, or Xbox One
  - For Xbox One, see [UWP on Xbox One](https://msdn.microsoft.com/en-us/windows/uwp/xbox-apps/index) and specifically [System resources for UWP apps and games on Xbox One](https://msdn.microsoft.com/en-us/windows/uwp/xbox-apps/system-resource-allocation)
- Cross-play games are typically games that target Xbox One and Windows PC using both the XDK and UWP paths.

## 2. Ensure you have a title created on Dev Center or XDP
Every Xbox Live title must be defined on Dev Center or the Xbox Developer Portal (XDP) before you will be able to sign-in and make Xbox Live Service calls.  [Creating A New Title](create-a-new-title.md) will show you how to do this.

## 3. Follow the appropriate guide to setup your IDE or Game Engine
You can follow the appropriate Getting Started guide for your platform and engine and learn the basics of Xbox Live as you go along.

* [Getting started using Visual Studio for UWP games](get-started-with-visual-studio-and-uwp.md) will show you how to link your Visual Studio project with your Xbox Live configuration on Dev Center.

* [Getting started using Unity for UWP games](partner-add-xbox-live-to-unity-uwp.md) will show you how to create a new Xbox Live enabled Unity title, add features such as Leaderboards to your title, and generate a native Visual Studio project.

* [Getting started using Visual Studio for XDK based games](xdk-developers.md) will show you how to get your Visual Studio project setup if you are making an Xbox One title using the XDK.

* [Getting started making a cross-play game](get-started-with-cross-play-games.md) explains how to make a product that is an XDK based game for Xbox One and a UWP based game for Windows 10 PC.

## 4. Xbox Live Concepts
Once you have a title created, you should learn about the Xbox Live concepts that will affect your experience developing titles.

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

ID@Xbox titles and titles released by game publisher is this a Microsoft Partner must go through a certification process before release.  This is because these titles have access to additional features such as Multiplayer, Achievements, and Rich Presence.  Once you are ready to release your title, contact your Microsoft representative for more detail on the submission and release process.
