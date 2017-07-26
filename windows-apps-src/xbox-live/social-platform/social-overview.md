---
title: Xbox Live Social Platform Overview
author: KevinAsgari
description: Learn about the Xbox Live social platform service.
ms.assetid: b3636f10-9a4b-4275-8ae8-d47d2fa63a3d
ms.author: kevinasg
ms.date: 04-04-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, social, people, profile
---

# Xbox Live social platform – overview

For gamers to adopt your title and stay engaged, it is crucial for them
to play and compete with others. Xbox Live offers the best gaming social
network with over 50 million active gamers and growing. Integrating the
Xbox Live social platform in your title is easy and the return on
investment is huge whether you are building a single player casual game,
a companion app, or a massive multiplayer game:

-   **Friends and followers**: Let your players play your game with the people they care about. Players use the [people system](people-system/xbox-live-people-system.md) to keep a list of their friends and other people that they enjoy playing with. By giving them direct access
    to their friends in your game, they will stay more engaged.

-   **Presence, Activity Feed, and Trending**: By integrating with the Xbox Live Social platform, your game will automatically show up
    in Presence, Activity Feed, and Trending. This will help entice your players to create gaming communities to get together and play. They can also share screenshots & clips of your game broadly. These awareness features show up across all Xbox Live enabled devices (PC, iOS, Android, Xbox One, Xbox 360).

-   **Game Hubs**: Game Hubs provide a way for you to help build communities of fans around your game by providing a centralized location for players to keep up to date with what's happening in your game. From your game's Game Hub, fans can follow the activity feed, find and join clubs, look for groups of other people to play with, view upcoming tournaments, and more. Game Hubs keep your passionate fans stay informed, even when they're not playing or logged onto the Xbox One.

## Concepts

Here are core concepts to help you understand how the Xbox Live Social
platform works:

### Profile

Each gamer on Xbox Live has a profile. The profile contains the
following information

-   **Gamertag**: The unique nickname of the gamer. Every user on Xbox Live has
    a gamertag.

-   **Real Name**: The first & last name of the gamer. Empty if the user
    doesn’t want to share their real-name via their privacy settings.

-   **Gamerpic**: A picture or icon that the gamer has chosen to represent them.

-   **Presence**: The current state of the gamer (online, offline, playing a
    game, etc.)

-   **Gamerscore**: A single value that represents the total achievement score
    the user has accumulated across all of the Xbox Live games they have played.

### People system (a.k.a Social Graph)

Each Xbox Live member can maintain a personal ‘friends list’ of people
(real world friends, other gamers met on Xbox Live, and well known gamers &
VIPs such as Major Nelson).

The relationship between people are one way ‘follows’, which means you
can add someone to your friends list by following them. People can add
up to 1000 friends in their ‘friends list’ and have unlimited followers:

-   A ‘friend’ is a person you have added to your ‘friends list’.

-   A ‘follower’ is someone who has added you to their friends list.

To make it easy to find their favorite gamers in their friends list,
each gamer can mark up to 100 friends as a favorite.

When a gamer plays your game, the Xbox Live social graph will show only
the gamer’s friends who have also played your game.

### Presence

Each gamer on Xbox Live broadcast their presence on Xbox Live (Online,
Offline, in a game, etc.). It is a great way to know what gamers on your
friends list are up to and if they are available to play with. Presence
is automatically handled by the Xbox Live service, but you can configure some aspects of how the presence is displayed for people playing your game.

## Other Xbox Live Social features you can benefit from

Once your game is on the Xbox Live Social platform, your game can
further take advantage from these popular features:

### Gamerscore & achievements

Each gamer can earn Gamerscore by unlocking [achievements](../achievements-2017/achievements.md) in your game.
It is a very popular feature that keeps your gamers engaged with your
title. They can compare their achievements with their friends in their
profile.

> [!NOTE]
> Gamerscore and achievements are not available to games created under the Xbox Live Creators Program.

### Take clips & screenshots of your game

Gamers can take clips & screenshot of your game on Xbox One and Windows
10 and share them on their activity feed & clubs, raising awareness of
your title to the community.

### Clubs

Gamers can build social groups of any size around your game: from a
small group of friends who love to play your game to a large community
of hundreds of thousands of gamers who love to socialize & play together.

In a club, gamers can share clips/screenshots of your game with fans,
ask each other questions, chat together, and find other players to play your
game via Looking For Group (LFG). Gamers who join clubs increase their engagement by 40% and make more friends.

Clubs are exclusively managed by the players.

### Game Hubs

By integrating with Xbox Live, your game automatically gets a hub page known as the Game Hub. Game Hub is the official destination for your game on Xbox Live, and lets players find game related activity, achievements, groups, clubs, tournaments, and more.

You can optionally setup a community manager for your Game Hub to interact with
your users via the Activity Feed. Any gamer who plays your game is
automatically subscribed to your Game Hub activity feed, giving you a
powerful mechanism to reach gamers. On average, a post on the Game Hub
activity feed will get 10x more engagement than a similar post on
popular social media platform (Facebook, Twitter, etc.)

### Trending

The most popular content published across Xbox Live is visible in the
Trending section of Xbox Live. If a gamer posts an interesting question on your
Game Hub or shares a great clip of your game, you can expect your content
to trend on Xbox Live. This is another great way to raise awareness for
your title.

## Getting started

To use the various Social APIs, you use the `SocialManager` class.  You can learn how to use it in [Intro to Social Manager](intro-to-social-manager.md).
