---
title: Achievements
author: KevinAsgari
description: Achievements
ms.assetid: 35e055c2-3c84-4d73-bb86-fc776327d901
ms.author: kevinasg
ms.date: 04-04-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---

# Achievements

In 2005, Xbox LIVE introduced the gaming industry to the notion of an achievement, a system-wide mechanism for directing and rewarding users' in-game actions in a consistent manner across all games.

With Xbox One, the achievement system expands to engage users across a broader range of activities, devices, and scenarios. Achievements are now more inclusive, more social, and more engaging than ever before! Developers and publishers can create achievements for every activity type on Xbox LIVE—gaming, video, music, or social—along with new achievement types and rewards that go beyond gamerscore. Gamerscore is still a key reward, however, so developers can now add more achievements and gamerscore over the lifetime of the title, even without a new content release. Across the board, achievements on Xbox One have been reimagined to give service-live flexibility to titles that support gaming as a service.

## Feature Summary ##
This list provides a summary of the key features and components that contribute to the achievement system on Xbox One.

Feature | Description
--- | ---
Persistent Achievements (achievements) | This type of achievement exists for the lifetime of the application. Users may unlock them at any time. A persistent achievement’s required activity must always be available for users to complete and its reward must always be earnable.
Challenge Achievements (challenges) | This type of achievement is only available for a limited time. The publisher determines the dates during which a challenge achievement is available to unlock, and users must unlock it within that predefined timeframe to receive the recognition and its reward.
Cloud Powered | Titles no longer directly call unlock achievement; instead, the Achievements service triggers the unlock based on pre-defined rules that are configured via XDP and stats that are sent through the Data Platform. The same data that you track for gameplay purposes or for business intelligence will now be used to determine when to unlock an achievement.
Post-Launch Achievements | Add achievements after title launch with no additional code required.
Achievement Progression | Now users can see how far along they are toward unlocking the achievement, even from the dashboard, giving them more reasons to fire up your title.
Unlock Tangible Rewards | In addition to Gamerscore, which will remain a critical part of the Xbox gaming experience, Xbox One users can now unlock special game-related rewards such as digital artwork, new maps, unlockable characters, and temporary stat boosts via Xbox LIVE Achievements. And this is not limited to games! Other Xbox One applications such as video and music apps can now use Achievements to bring you awesome sneak peek content, early access, or subscription extensions. Note that only games will give you Gamerscore.
Achievement Activity Feed | Users can easily discover which achievements are recently popular amongst their friends and which rewards are being earned.
Achievements for All Apps | Every Xbox LIVE game, application, and hub will now have achievements to add that extra layer of interactivity and fun to any experience.
One Gamerscore | Any Gamerscore that a user earns on legacy platforms or modern platforms will count toward a single Gamerscore.
Cross-Title Challenges | A publisher may create a challenge that spans multiple of its titles. For example, a title developer might have "Get 100 kills in Title 1 or Title 2".

## Making achievements work well with the Achievements Snap ##
There are two main guidelines to remember when trying to make your title's achievements and Xbox One's Achievements Snap (the snapped view of the Achievements app) work together:

1. As long as your achievements have gradual progress increments as opposed to "binary" ones (for example, locked/unlocked), they’ll show up nicely and be sorted by progress.
2. Updating the stats on which achievements are based in real time is usually more interesting to players than bundling individual stat updates into one big update, for example after the match is over. This is true even if your title's achievements are progress-based.
