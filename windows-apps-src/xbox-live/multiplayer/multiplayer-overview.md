---
title : Multiplayer Overview
---

# Multiplayer overview

* What are the benefits of multiplayer
* How does the Xbox Live multiplayer system work? (this might be a good place to put Kevin’s doc with the definition of terms, since some context would have been provided)
  * What do we mean when we say “Multiplayer”?  Start each subsection with “why”.  And include possible scenarios on how each feature would be used.
  * Secure Networking
  * Voice Communication
  * Invites, Rosters, Sessions, etc
  * Matchmaking + LFG
  * Tournaments

While many games exist that are designed for a single player, there are also many games that are either enhanced by the ability to play with other players, or are designed specifically around playing with other players.  Whether it's co-operative or competitive, or a mixture of both, playing with other people brings a social aspect to games, and turns a game from a solitary experience into a shared human experience.

Computer programs are fairly deterministic, and no matter how good the AI of a game is, current technology cannot compare to the unpredictability and adaptability of a human opponent or ally. Playing with other people adds an element of uniqueness to every play through of a multiplayer game, and creates new experiences that are often remembered well beyond the game itself.

However, the cost and complexity of adding multiplayer to a game is not trivial, especially if you consider online multiplayer. So a game developer must decide if the value of adding multiplayer to their game is worth the added complexity of coding and testing that comes along with multiplayer scenarios.

## What is the value of multiplayer?

Multiplayer extends the life of games beyond the limited set of experiences and challenges that can be defined in code. A single player game can usually be "completed", whether that means finishing the story, unlocking all of the achievements, or beating the game on the hardest difficulty level.  Multiplayer however offers gamers a reason to continue playing indefinitely, as long as other people keep playing the game, even well after they have completed any single player mode in the game.

The longer a player plays a game, the more invested that gamer will be in the game and the franchise. This means that players are more likely to buy expansions, downloadable content, in-app purchases, and even additional games in the franchise.

Additionally, compelling multiplayer games can influence friends of a player to also purchase the game so that they can play with their friend. Getting a single player interested in a multiplayer game often leads to a group of friends all buying the game so that they can play together.

If the multiplayer of a game is done well, this can also lead to people sharing their unique experiences on social media, which further promotes your game.

If your game includes exciting competitive multiplayer, you can even set up tournaments to allow your best players to test their skills. These tournaments can become major events in themselves, drawing in thousands of viewers.

## What is the cost of multiplayer?

While adding multiplayer to a game can add a ton of value and extend the lifetime of a game, multiplayer also adds a great deal of complexity and cost to a game. For some games, especially ones focused on delivering a crafted single player experience, multiplayer may not make sense, or may not provide enough additional value to justify the cost. Poorly designed multiplayer is often worse than no multiplayer at all.

First, you must consider what the game play experience is like with the inclusion of additional players. Designing an AI or code logic to account for the actions of the player becomes vastly more complex as you start adding additional players, due to all the permutations of the players' actions.

If your game features competitive multiplayer, players will find and capitalize on any imbalances or bugs that may give them an advantage. This means that you may have to constantly tweak, balance, and patch your game to ensure that your game design keeps its integrity.

Second, you'll need to consider the networking aspect of connecting multiple players together, and synchronizing game state across multiple devices. While a game can choose to implement only local multiplayer on the same device in order to avoid networking concerns, this limits quite a large amount of the value of adding multiplayer.

There are many ways to implement networking between players, such as having every game client talk to every other game client (also known as peer to peer), or having a single game client act as the designated host, and every other game client communicates with the
host (also known as peer to host).

With any network connection, since the transmission of data is not instant, you must also account for the delay between sending data from one client, and another client receiving and processing that information. Different clients will likely have different transmission times and different connection speeds as well.

You also must account for interrupted network connections, data loss during transmission, and even potentially tampered data sent through connections.

Third, you need to have a way to connect players in order to allow them to play together and join the same game. This means you need to either maintain or connect to a social network to allow players to find each other. Originally, people connected by manually looking up their IP address and using that as the way to connect, but that method is error prone and difficult for the player to follow, doesn't allow for matchmaking or discovery, and modern players expect a much more sophisticated experience.

Using a social network adds additional complexity for the developer however. If a player has a bad experience with another player and bans them, your game should not allow the banned player to join the first player's game, unless it's in a ranked competitive situation. Your game may also need to deal with players disconnecting, rejoining, or joining a game already in progress.

If the social network supports parental controls, your game must honor those controls and restrictions as well in order to protect underage gamers.

Testing multiplayer in games requires multiple devices, multiple simulated network conditions, multiple test accounts, and a lot of time.

Despite the costs associated with adding multiplayer however, well designed multiplayer can often add more than enough value to justify the costs.

## What are the aspects of multiplayer?

* Secure Networking
* Voice Communication
* Invites, Rosters, Sessions, etc
* Matchmaking + LFG
* Tournaments

If you choose to implement multiplayer for your game, there are a number of aspects that you must manage. While there are a number of game design aspects related to multiplayer, this section focuses on the technical aspects of implementing multiplayer.

## Network types

One of the first decisions you'll need to make when you decide to implement multiplayer is around what type of networks your game will support.

A game can choose to only support multiplayer from the same device, which negates many of the networking concerns, also known as local multiplayer. However, this also only works for players that are physically present in the same room. This is a common scenario for party style games.

A much more common, and more challenging, scenario is when multiplayer games support players playing from many different devices, either over a local area network or over the internet. Each device is running a copy of the game, and the games need to be able to connect with each other and synchronize their game state.

There are multiple ways to set up the network structure for distributed multiplayer games. The first thing to decide is if your game network is authoritative or non authoritative.

### Authoritative

In an authoritative network, one device is selected to operate as "the source of truth" for the game. That means that this device, which we'll refer to as the "authority", is responsible for maintaining the integrity of the game state across all of the connected devices.

Generally, the network must support the authority directly sending data to each other device that is connected to the game play session.


### Non-authoritative

In a non authoritative network, every device is responsible for maintaining the game state, and the games must have a system in place for resolving discrepancies is the game states.

* **Peer to peer**  
A game can choose to connect every player's device to every other player's device, and synchronize game data across a fully connected mesh topology. This is commonly referred to as peer to peer. In this type of network, a single slow connection can severely impact the performance of the game for every player.


* **Client/server**
