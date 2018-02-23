---
title: Rich Presence
author: KevinAsgari
description: Learn how Xbox Live Rich Presence can help promote your title.
ms.assetid: 00042359-f877-4b26-9067-58834590b1dd
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, rich presence
ms.localizationpriority: low
---

# Rich Presence

By using Rich Presence, your game can advertise what a player is doing right now. For example, your game can use Rich Presence strings to show all gamers the status of your game's players, such as *Away*. Rich Presence information is visible to gamers connected to Xbox Live. Ideally, a Rich Presence string tells other Xbox Live gamers what a player is doing, and where in your game the player is doing it. The concept of Rich Presence strings is the same on Xbox One as it was on Xbox 360, but the new implementation follows the Entertainment as a Service initiative. The topics in this section describe how to configure your Rich Presence strings and then how to set the string for a user playing your title.


## Definitions

**Enumerations**  
An enumeration is a list of some in-game dimension. Examples of these in-game dimensions are weapons, character classes, maps, and so on. We want to see a list of the possible weapons in your game, a list of all the possible character classes or maps, and so on.

**Locale-string pair**  
Every possible Rich Presence string must have a locale associated with it to specify in what locales the string can/should be used. Each enumeration will also have a set of locale-string pairs as well.

**String-set**  
A string set is made up of a group of locale-string pairs. This set defines the possible values of a Rich Presence string for all possible locales or the possible values for an enumeration for all possible locales.

**Friendly Names**  
There are two types of friendly names:

**Rich Presence string**  
The friendly name for a string-set is a unique identifier in the form of a string used to reference a string-set.

**Enumeration**  
These friendly names are used to uniquely identify a particular enumeration like the weapons enumeration or the character class enumeration.


## In this section

[Rich Presence configuration](rich-presence-strings-configuration.md)  
How to configure Rich Presence for use in your title.

[Rich Presence updating strings](rich-presence-strings-updating-strings.md)  
How to update Rich Presence Strings from your title.

[Rich Presence best practices](rich-presence-strings-best-practices.md)  
Best practices for use of Rich Presence in your title.

[Rich Presence policies and limitations](rich-presence-strings-policies-and-limitations.md)  
The policies about using Rich Presence in your title.

[Rich Presence appendix](rich-presence-strings-appendix.md)  
Additional examples and details about the Data Platform relevant to Rich Presence.

[Programming Xbox Live Rich Presence](programming-rich-presence.md)  
Demonstrates how to use Rich Presence with Xbox Live.
