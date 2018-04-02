---
title: Xbox Live People System
author: aablackm
description: Learn about the Xbox Live people system.
ms.assetid: f1881a52-8e65-4364-9937-d2b8b8476cbf
ms.author: aablackm
ms.date: 03/19/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, social, people system, friends
ms.localizationpriority: low
---

# Xbox Live People System

Before you begin integrating Xbox Live social services into your title it may be useful for you to understand the system you're working in. To that end, we will briefly describe the workings of the Xbox Live People System. The People System dictates and maintains relationships between gamers in the Xbox Live ecosystem. The People System allows for gamers to easily recognize and organize their contacts on Xbox Live, whether they be close friends or online acquaintances. You can show your real name to people you trust, and hide it from those you don't. You can play favorites with your friends so you'll always know what they're up to first while you're online. The Xbox Live People System allows you to add your good friends, and those who gave you good games all in one safe, easy to organize package.

## One and two way relationships

The People System allows you to have both one and two way relationships through the concept of *following*. Before you could only *friend* other gamers by sending an invitation and then waiting for them to accept that friend request, resulting in a two way relationship where you each follow each other. This process could take days depending on how frequently gamers checked their friend requests. With the addition of one way relationships you can *follow* a user and see their online activity immediately instead of waiting for them to respond. At this stage you are a *follower*. The person you have *followed* will receive still receive a notification and will have the opportunity to follow you back, an action that will make you *friends* in a two way relationship. One way relationships cut down on the wait time to interaction with the added bonus of allowing you to keep up with noted gamers who stream to viewers or have gained a following for their competitive prowess.

## Gametags and real names

Creating and maintaining a mental map of which real-world user is which Gamertag can become a herculean task when you have the potential for unlimited friends and following relationships. Which is why we give you the option to reveal your real name to gamers you know personally so they can recognize you more easily. Showing real names is optional and must be opted into by the user. Real names are never shown to everyone on Xbox Live, they are only shown to people the user knows which are expressed either through connections manually indicated by the user or through a social network the user has associated with the account. Users will always appear as their Gamertag when interacting with random strangers in match-making or other experiences; furthermore there is no option to appear as your real name to strangers.

Users may control how they appear in games and can decide that they want others to always see them as their Gamertag in games, even if some of those people have access to their real name. At the time users opt-into the functionality that allows their real name to show (only to those they know), they are presented with the option to always appear as their Gamertag in games. Selecting this option means that the name appearing on the screen above their head in game will always be their Gamertag.

## Privacy and people I know

A core feature of the People System is that it provides for both anonymous and non-anonymous relationships; users can have real-name friends as well as random Gamertag relationships with users they've encountered on Xbox Live. This means there are two different sets of rights that must be supported: one to allow trusted real-world friends to see sensitive information, and a second to enable users to add random Gamertag relationships, while feeling safe they won't reveal anything sensitive.
In addition to this users can place restrictions on individual pieces of information about themselves such as their profile, online status, and friends list. Access to this information can be placed into three buckets.

- Everybody: this makes the information public to anyone who comes across that user.
- Friends: this makes the information available only to those who the user has deemed a friend.
- Block: this makes the information inaccessible.

See the full list of privacy settings on [xbox.com](https://account.xbox.com/Settings).

## Favorite People

The Xbox Live People system provides the end user a simple category system for their own People list. Users can mark anyone on their list as a favorite and those people will show up first and are prioritized higher across features. Favorites will show first in titles anytime a list of people is shown. There is no limit to the number of favorites a user can have. Users can toggle anyone on their list as a favorite regardless of whether the person is a real-name or Gamertag relationship without privacy implications. Favorite people are a subset of the user's People list and does not behave like a separate list of people.