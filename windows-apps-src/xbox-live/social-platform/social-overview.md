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

# Xbox Live Social platform – Overview

For gamers to adopt your title and stay engaged, it is crucial for them
to play and compete with others. Xbox offers the best gaming social
network with over 50 million active gamers and growing. Integrating the
Xbox Live Social platform in your title is easy and the return on
investment is huge whether you are building a single player casual game,
a companion app or a massive multiplayer game:

-   Let your gamers play your title with the people he cares about:
    gamers use the people system to keep a list of their friends and
    other gamers they enjoy playing with. By giving them direct access
    to their friends in your game, they will stay more engaged with
    your game.

-   Free viral awareness of your title: By integrating with the Xbox
    Live Social platform, your title will automatically start showing up
    in Presence, Activity Feed, and Trending. Gamers will create gaming
    communities around your games to get together and play yor game.
    They will share screenshots & clips of your game broadly. These
    awareness features show up across all Xbox Live enabled devices (PC,
    iOS, Android, Xbox One, Xbox 360).

-   Keep your passionate fans informed, even when they are not playing:
    Build communities of fans around your title with Game Hubs and
    communicate through the Activity Feed. 50% of activity feed views
    occur on devices outside of the Xbox One.

-   Extend your title without building and running your own cloud
    services: Integrating the Xbox Live Social platform is the first
    step to leverage all the gaming services that Xbox Live has to offer
    (Multiplayer, Party chat, Achievements, Challenges, Leaderboards,
    Game clips, Social Network sharing, …)

Concepts
--------

Here are core concepts to help you understand how the Xbox Live Social
platform works:

## Profile

Each gamer on Xbox Live has a profile. The profile contains the
following information

-   **Gamertag**: nickname of the gamer. Every user on Xbox Live has
    a gamertag.

-   **Real Name**: First & last name of the gamer. Empty if the user
    doesn’t want to share his real-name via his privacy settings.

-   **Gamerpic**: picture of the gamer

-   **Presence**: state of the gamer (online, offline, playing a
    game, etc.)

-   **Gamerscore**: Single value representing total achievement score
    the user has accumulated

## People system (a.k.a Social Graph)

Each gamer on Xbox Live can maintain a personal ‘friends list’ of gamers
(real world friends, other gamers met on Xbox Live, well known gamers &
VIP (ex:Major Nelson).

The relationship between gamers are one way ‘follows’, which means you
can add someone to your friends list by following them. Gamers can add
up 1000 gamers in their ‘friends list’ and have unlimited followers:

-   A ‘friend’ is a person you have added to your ‘friends list’.

-   A ‘follower’ is someone who has added you to their friends list.

To make it easy to find their favorite gamers in their friends list,
each gamer can mark up to 100 friends as a favorite.

When a gamer plays your game, the Xbox Live social graph will show only
the gamer’s friends who have also played your game. That feature is
under development, and will go into effect in mid-2017.

## Presence

Each gamer on Xbox Live broadcast their presence on Xbox Live (Online,
Offline, in a game, etc.). It is a great way to know what gamers on your
friends list are up to and if they are available to play with. Presence
is automatically handled by

Other Xbox Live Social features you can benefit from
----------------------------------------------------

Once your game is on the Xbox Live Social platform, your game can
further take advantage from these popular features:

## Gamerscore & achievements

Each gamer can earn Gamerscore by unlocking achievements in your game.
It is a very popular feature that keeps your gamers engaged with your
title. They can compare their achievements with their friends in their
profile. [Achievements](../achievements-2017/achievements.md)

## Take clips & Screenshots of your game

Gamers can take clips & screenshot of your game on Xbox One and Windows
10 and share them on their activity feed & clubs, raising awareness of
your title to the community.

## Clubs

Gamers can build social groups of any size around your game: from a
small group of friends who love to play your game to a large community
of 100s of thousands of gamers who love to socialize & play together.

In a club, gamers can share clips/screenshots of your game with fans,
ask each other questions, chat together, find other players to play your
game via LFG. Gamers who join club increase their engagement by 40% and
make more friends.

Clubs are exclusively managed by the users.

## GameHub

By integrating with Xbox Live, your game automatically gets a gamehub.
Gamehub is the official destination for your game on Xbox Live. You can
optionally setup a community manager for your gamerhub to interact with
your users via the Activity Feed. Any gamers who plays your game is
automatically subscribed to your GameHub activity feed, giving you a
powerful mechanism to reach gamers. On average a post on the GameHub
activity feed will get 10x more engagement than a similar post on
popular social media platform (Facebook, Twitter, etc.).

## Trending

The most popular content published across Xbox Live is visible in the
Trending section of Xbox Live. If post an interesting question on your
GameHub or share a great clip of your game, you can expect your content
to trend on Xbox Live. This is another great way to raise awareness for
your title.

## Getting started

To use the various Social APIs, you use the `SocialManager` class.  You can learn how to use in [Intro to Social Manager](intro-to-social-manager.md)
