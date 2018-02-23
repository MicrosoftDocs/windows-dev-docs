---
title: Rich Presence best practices
author: KevinAsgari
description: Learn the best practices for using Xbox Live Rich Presence.
ms.assetid: 51a84137-37e4-4f98-b3d3-5ae70e27753d
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, rich presence, best practices
ms.localizationpriority: low
---

# Rich Presence best practices

The following tips will help you get the most from Rich Presence in your game. Keep in mind that the more Rich Presence strings you define, the richer the experience of other gamers who discover people playing your game.

-   Use your stats in your strings, to allow you to set your string and then not worry about it.

    If your string has the map name in it, and you're using the CurrentMap stat to fill in the blank, then the service will update your string appropriately, as your players travel from Map to Map in the game. This approach enables you not to worry about keeping the string up-to-date, as long as your title is sending the proper events to the data platform.

    Your title should set the Rich Presence base string with the presence service periodically, to make sure the Rich Presence information for a user is accurate and the service is using the correct base string.

-   Use Rich Presence to open up new conversation opportunities. Create strings that are likely to generate interest in a game for new players as well as casual players who might have missed a special feature.

-   Create Rich Presence strings that motivate players to take action. For example, instead of saying "Playing on Mausoleum," say, "Requests assistance; Defending Mausoleum." Use the Rich Presence state to enable cool scenarios, such as joining a game in progress. Then another gamer can join and help.

-   Create Rich Presence strings that empower gamers to show off their achievements, such as completion of levels or discovery of secret areas.

-   Localize your Rich Presence strings and their associated parameters so that Xbox gamers around the world can be a part of the community you are fostering.

-   Some parameters change rapidly, but it can take time for new Rich Presence data to show up for a friend. If your string contains "current weapon" and the player has the ability to switch back and forth between their pistol and rifle, their Rich Presence string may not be completely accurate at any given time. However, in some cases, that isn't a problem. If your Rich Presence string contains the value for total enemies defeated, and the value is off by 1 or 2 for a few seconds, that may be ok for your scenario.
