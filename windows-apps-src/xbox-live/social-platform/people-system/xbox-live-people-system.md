---
title: Xbox Live People System
author: KevinAsgari
description: Learn about the Xbox Live people system.
ms.assetid: f1881a52-8e65-4364-9937-d2b8b8476cbf
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, social, people system, friends
localizationpriority: medium
---

# Xbox Live People System

For the Xbox One launch, Xbox Live migrated from the previous Xbox 360 era _friends_ system to a new _people_ system that reworks the social fabric in Xbox Live. The goals of the people system include:

- Make it easy for users to recognize people they know.

  Real names are available in all experiences and are the foundation of interaction for non-anonymous relationships.

- Existing Xbox Live relationships migrate to Xbox One.

  Users who have friends they've added in Xbox 360 don't have their lists reset when they use a modern experience.

- Continue to support maintaining anonymous gamertag relationships.

  After a great gameplay session from a match made by the matchmaking service, a user can add that person to his or her people list for easy access later.

- Provide a simple way to manage long lists of people.

  Users can mark people as favorites so that Xbox One experiences show those people first.

- Evolve the Xbox Live privacy system to enable users to give more rights to trusted real-world relationships and fewer rights to anonymous (gamertag-only) relationships.
- Ensure backward compatibility.

  Legacy clients and legacy service code continue to operate against the previous social infrastructure without requiring a move to the new system.

## Identity and display of real names on Xbox One
Users on Xbox One are authenticated by their Microsoft account identity. A user signs into the console by using his or her Microsoft account, and the user's associated name and picture is displayed throughout the shell. If a user has not linked his or her Xbox Live profile to an external social network that provides real names, that user will be represented in the system by his or her gamertag and gamerpic (and previous Xbox Live gamertag relationships are migrated to Xbox One).

Linking to external networks is promoted, but linking is optional. If a user has linked his or her profile to an external social network, when other friends on that network have Xbox Live accounts and they have also linked their profiles, they are represented on the console by their Microsoft account names and pictures to those friends with whom they share a connection in the external network. However, users who are connected in this way continue to be represented by their gamertags and gamerpics to people to whom they are not connected in the external network.

The subsequent topics in this section elaborate on these relationships. For information about the related API, see **Microsoft.Xbox.Services.Social Namespace**.

## In this section
[One Way Relationships](one-way-relationships.md)  
Describes the one-way relationship model in the Xbox Live People system.

[Gamertags and real names in the People System](gamertags-and-real-names.md)  
The Xbox Live People system is designed to allow the display of real names to people the user knows; consistently everywhere in new Xbox Live experiences regardless of context...even over the character's head when running around a map in an FPS online game.

[Favorite People](favorite-people.md)  
Favorites in the Xbox Live People system.

[Display People from the People System](displaying-people-from-the-people-system.md)  
How to display people returned from the Xbox Live People system.

[Xbox 360 Friends Backward Compatibility](xbox-360-friends-backward-compatibility.md)  
How to work with Xbox 360 friends in the new Xbox Live People system.

[Privacy and People I Know](privacy-and-people-i-know.md)  
Protecting privacy in the People system.

[Reputation](reputation.md)  
Describes how the reputation system is used to aggregate user reports of players, allowing Xbox services to provide high-quality game play, as well as to incentivize behavior.

[Sending Player Feedback From Your Title](sending-player-feedback-from-your-title.md)  
Describes how your title can help provide feedback that affects a user's reputation.

[Programming Xbox Live Social Services](programming-social-services.md)  
Demonstrates how to use social services provided by Xbox Live.
