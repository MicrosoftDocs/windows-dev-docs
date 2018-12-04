---
title: Xbox Live social platform - For games and gamers

description: Provides links to learn about the Xbox Live social platform service.
ms.assetid: 27b85218-60f3-4eb0-9f7e-fe90e027db5c
ms.date: 09/18/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: medium
---
# Xbox Live social platform - For games and gamers

For gamers to adopt your title and stay engaged, it is crucial for them to play and compete with others. Xbox Live offers the best gaming social network with over 50 million active gamers and growing. We have created a set of tools to bring gamers together and get their eyes on new and exciting games like the one you’re sure to create. Integrating the Xbox Live social platform in your title is easy and the return on investment is huge whether you are building a single player casual game, a companion app, or a massive multiplayer game.

## Concepts in this article
- [Profile](#profile)
- [Gamerscore and Achievements](#gamerscore-and-achievements)
- [The people system - Your Social Network On Xbox Live](#the-people-system---your-social-network-on-xbox-live)
- [Clubs](#clubs)
- [Clips and screen shots](#clips-and-screenshots)
- [The activity feed](#the-activity-feed)
- [Trending](#trending)
- [Rich Presence](#rich-presence)
- [Game Hubs](#game-hubs)
- [Use the Xbox Live Social APIs](#use-the-xbox-live-social-apis)

## Bringing gamers together
Xbox Live is committed to building an active community of gamers. That's why we provide a set of tools to help gamers find others to share experiences with and make gaming a social activity. So when they play your game they can't help but share it with their friends. 

### Profile
Each gamer on Xbox Live has a profile. The profile contains the
following information:

-   **Gamertag**: The unique nickname of the gamer. Every user on Xbox Live has
    a gamertag.

-   **Real Name**: The first and last name of the gamer. This is empty if the user
    doesn’t want to share their real name via their privacy settings.

-   **Gamerpic**: The picture or icon that the gamer has chosen to represent them.

-   **Presence**: The current state of the gamer (online, offline, playing a
    game, etc.)

-   **Gamerscore**: The single value that represents the total achievement score
    the user has accumulated across all of the Xbox Live games they have played.

### Gamerscore and Achievements
Each gamer can earn Gamerscore by unlocking [achievements](../achievements-2017/achievements.md) in your game.
Achievements is a very popular feature that keeps your gamers engaged with your
title by letting them both boast of their prowess and extend gameplay goals. Players can compare their Achievements with their friends in their
profile.

> [!NOTE]
> Gamerscore and Achievements are not available to games created under the Xbox Live Creators Program.

### The people system - Your social network on Xbox Live
Each Xbox Live member can maintain a personal friends list of people which could contain real world friends, gamers met on Xbox Live, and well known gamers and VIPs such as Major Nelson. 

The relationships between people are one way *follower* relationships, which means you
can add someone to your friends list just by following them. This is in contrast to other systems
where the person you are trying to follow has to confirm the relationship before you can become
friends in the system. You can add up to 1000 friends in your friends list and have unlimited followers:

-   A *friend* is a person you have added to your friends list by following them.

-   A *follower* is someone who has added you to their friends list by following you.

To make it easy to find their favorite gamers in their friends list,
each gamer can mark up to 100 friends as a favorite.

When a gamer plays your game, the Xbox Live social graph will show only
the gamer’s friends who have also played your game.

[Learn more about the people system](people-system/xbox-live-people-system.md) 

### Clubs
Gamers can build social groups of any size based on shared interests. These groups are called Clubs.
Clubs range from a small group of friends to a large community of hundreds of thousands of gamers who love to socialize and play together.
Clubs may also cover broad categories of interest and not just your game in particular. Some notable club names include:

- Awesome Screenshot Club
- Vintage Retro Games
- Memes Are Everything
- Club UFC

That's just a few of the multitude of Clubs Xbox Live gamers have created.

In a club, fans of your game may share clips and screenshots of your game with others,
ask each other questions, chat together, and find other players to play your
game via Looking For Group (LFG). Gamers who join Clubs increase their engagement by 40% and make more friends.

> [!NOTE]
> Clubs are exclusively managed by the players and will naturally appear within your fanbase, requiring no work or input on the developers part. But, a word from the > creator couldn't hurt. 

## Getting eyes on games
An important part of the Xbox social system is activity sharing. This lets all your friends on Xbox Live know what you've been up to, with the special added benefit of giving your game more exposure for every player getting in the game. The following features are meant to help Xbox users discover your game and keep them engaged after they’ve become yet another loyal fan. Even better than that, every time they play your game they’ll be enticing all their friends to join your legion. 

### Clips and screenshots
The easiest way for gamers to share your games content is through clips and screenshots. Players will be able to record gameplay videos and screenshots to share with their friends on Xbox Live. These moments of glory will automatically be showcased in the player's activity feed.

### The activity feed
When gamers play your title, their Achievement unlocks, clips, screenshots and other activities will become a part of an activity feed that is shared with their friends so they can all see the wonders your game has to offer.

### Trending
The most popular content published across Xbox Live is visible in the
Trending section of Xbox Live. If a gamer posts an interesting question on your
Game Hub or shares a great clip of your game, you can expect your content
to trend on Xbox Live. This is another great way to raise awareness for
your title.

### Rich Presence
Presence, the online status of the player, isn't just for checking whether the player is online or not. It also shows what app or game the player is currently engaged with. Furthermore,  Rich Presence strings allow you to advertise what someone is doing within your game, whether they are puttering around the menus, queuing up for a game, or right in the middle of play. 

[Learn more about Rich Presence](rich-presence-strings/rich-presence-strings-overview.md)

> [!NOTE]
> Rich Presence is not available for games created under the Xbox Live Creators Program.

### Game Hubs
By integrating with Xbox Live, your game automatically gets a hub page known as the Game Hub. Game Hub is the official destination for your game on Xbox Live, and lets players find game-related activity, Achievements, groups, Clubs, tournaments, and more.

You can optionally setup a community manager for your Game Hub to interact with
your users via the activity feed. Any gamer who plays your game is
automatically subscribed to your Game Hub activity feed, giving you a
powerful mechanism to reach gamers. On average, a post on the Game Hub
activity feed will get 10x more engagement than a similar post on an 
external popular social media platform.

##  Use the Xbox Live Social APIs
In order to keep gamers, and their friends engaged with your title through the Xbox Live Social APIs you will use the `SocialManager` class.  Start learning how to use it in [Introduction to Social Manager](intro-to-social-manager.md)