---
title: Multiplayer scenarios
author: KevinAsgari
description: Learn about the different multiplayer scenarios and how Xbox Live supports those scenarios.
ms.assetid: 470914df-cbb5-4580-b33a-edb353873e32
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Multiplayer scenarios
There are many different types of multiplayer scenarios, and choosing the right scenario can increase player engagement and the player base for your game, which in turn helps extend the active life of your game for as long as possible.

Even games that are mostly single player experiences can benefit from competitive Xbox Live features like Leaderboards, Stats or Social elements.

The following list describes some of the more common multiplayer scenarios that you can implement with Xbox Live. The list is ordered in terms of complexity and amount of work required to implement and test, and you should consider these factors when deciding on type scenarios that you want your game to support.

## Comparative (Indirect) Play
In this scenario, players are competing with each other indirectly, without direct gameplay in the same instance of a game. It is well suited for games that are oriented towards a single player experience, but contain some competitive aspects that let players compare how they did to other players.

Example functionality for this scenario includes:

* Leaderboards, where a gamer tries to achieve the best "score" in a category relative to other players. This can include a social leaderboard system for competing with friends only.
* Achievements and stats, where a gamer wants to be able to compare his progress/performance against that of his friends, and possibly brag about beating a particularly challenging achievement.
* "Ghosting" or "virtual multiplayer", where a gamer can compete against a recording of another gamer's (or their own) previous performance, such as a lap in a racing game.

This type of multiplayer can be achieved by using the following Xbox Live services:

* Presence
* Stats
* Social Manager
* Achievements
* Leaderboards
* Connected storage

This type of multiplayer does not require any of the Xbox Live Multiplayer specific services. Testing this scenario requires multiple Xbox Live accounts only.

## Local Play (Living Room Play)
This type of multiplayer is based on two or more players playing a game together (either versus each other, or cooperatively) on a single device. A title may use a single screen for all players or a split screen experience for each player. Alternatively, in turn based games, you may use a "hot seat" approach where each player takes control of the game during their turn.

On the Xbox One console multiple gamers can be signed in on a single console. Each player is tied to a controller. Currently, Windows 10 devices only support sign-in of a single Xbox Live account, but Microsoft is investigating changing this in a future update.

While it is possible to design a local play only style of multiplayer by using Xbox Live Multiplayer services, it may be better to consider this scenario as a subset of a more expansive multiplayer scenario that incorporates online experiences, as the additional investment required is minimal compared to the potential return of expanding the multiplayer scenario.

This type of multiplayer can use similar services as the previous scenario:

* Presence
* Stats
* Social Manager
* Achievements
* Leaderboards
* Connected storage

Testing this scenario requires multiple Xbox Live accounts and multiple controllers on a single device.

##	Online Play with friends
This scenario is the most traditional online multiplayer experience. In this scenario, an Xbox Live member wants to play a game with friends only, but does not want to play with strangers. Friends are either invited or join an ongoing game as it is in progress.

For parental control a broader multiplayer title (as shown later) should have the ability to limit online multiplayer to friends only and fall back to this scenario. Parental controls that limit interactions with strangers are also enforced through the Xbox Live service.

This type of multiplayer can be achieved by using the following Xbox Live services:

* Multiplayer Manager
* Presence
* Stats
* Social Manager

Testing this scenario requires multiple Xbox Live accounts and multiple devices.

## Online Play through a list of game sessions
In this scenario, a player can browse a list of joinable gameplay sessions in a game and then select which one to join. The player also has the ability to create new game session instances by hosting a game locally. These game instances may allow custom preferences (like game mode, level or game rules). Depending on the title design, game sessions may support restrictions like requiring a password to join or certain player/skill levels. These game session instances can also be fully public or hidden, depending on how your game implements the session browsing and joining.

This scenario allows players to self-select a game session. This gives control to the player but there is no guarantee that a session will provide a good experience. A session may not be filled with the correct player set at an interesting skill level. A session list is most effective when games have a small active multiplayer community.

This type of multiplayer can use similar services as the previous scenario:

* Multiplayer Manager
* [Session Browse](session-browse.md)
* Presence
* Stats
* Social Manager

Testing this scenario requires a large set of Xbox Live accounts and devices to test accurately. Developers should note that true player dynamics for session lists can only be tested with large player bases.

## Online Play though Looking for group
This scenario is similar to the Session List scenario, however it diverges from it in important points. Instead of a game list in the title the platform provides functionality to list game sessions outside the game. These Looking For Group advertisements are aimed to provide a more social experience and include gameplay, skill, and social relationship restrictions. This allows a game to provide an improved experience over session lists and also gives the creator of the session more control.

The player who created the "looking for group" session can approve or deny requests to join their group from other players. This allows gamers to find other gamers that share their playstyle preferences.

The Xbox Live LFG service is new for 2016, and preview APIs are expected to be available for titles in May.

This type of multiplayer can use similar services as the previous scenario:

* Multiplayer Manager
* Xbox Looking for Group
* Presence
* Stats
* Social Manager
* LFG service

Testing this scenario requires a large set of Xbox Live accounts and devices to test.

## Simple MatchMaking
In this scenario, a player (or group of players) is looking for other players (who may or may not be known to the player) for an online game. Instead of selecting friends, the game provides a simple matchmaking flow that allows players to join into game sessions with other players. The matchmaking flow in this scenario is simple: players search and find other players without any significant matchmaking configuration. This scenario works best with a larger online audiences.

To match players together appropriately, any matchmaking should try to honor reputation and block lists on Xbox Live. The Xbox Live SmartMatch service handles these restrictions automatically. Honoring restrictions like these helps to ensure a safer and better experience for players.

A important part of matchmaking are Quality of Service (QoS) networking checks. These checks ensure that the network connectivity between two players is sufficient for a good gameplay experience. This is different than in the Online Play with friends scenario since it is possible to repeat matchmaking and find other players in case of a bad network connection.

This type of multiplayer can use similar services as the previous scenario:

* Multiplayer Manager
* Presence
* Stats
* Social Manager
* SmartMatch matchmaking

Testing this scenario requires a large set of Xbox Live accounts and devices to test accurately. Developers should note that true player dynamics for session lists can only be tested with large player bases. Tools are available to simplify network condition and SmartMatch testing.

## Skill-Based Matchmaking
Skill-based matchmaking is a refinement of simple matchmaking scenario. In this scenario the matchmaking service includes more advanced rule sets like skill, player level and other game-specific properties. The matchmaking service uses rules that use these match paramerters to find a more high-quality session for the player. Depending on game design match parameters are configured directly by the player or are automatically set by the title.

Xbox Live SmartMatch provides a set of rules that can be used for skill-based matchmaking. The SmartMatch service uses an Xbox Live feature called TrueSkill to help evaluate the relative skill of a player.

This type of multiplayer can use similar services as the previous scenario:

* Multiplayer Manager
* Presence
* Stats
* Social Manager
* SmartMatch matchmaking

Testing this scenario requires a large set of Xbox Live accounts and devices. Developers should note that true player dynamics for session lists can only be tested with large player bases. Tools are available to simplify network condition, TrueSkill and SmartMatch testing.

## Tournaments
Tournaments allow for skilled gamers to compete with each other in an organized format. Typically, tournaments are hosted and organized by independent third party organizations.

Xbox Live has added a new feature in 2016 to allow titles to use Xbox Live matchmaking in conjunction with approved third party tournament organizers. This feature allows titles to seamlessly integrate tournaments with the Xbox shell experience, without requiring players to register on external websites.

This type of multiplayer can be achieved by using Xbox Live services such as:

* Presence
* Stats
* Social
* Reputation
* Multiplayer
* SmartMatch matchmaking
* Tournaments

Testing this scenario requires a large set of Xbox Live accounts and devices.
