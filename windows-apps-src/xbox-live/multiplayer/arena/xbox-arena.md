---
title: Xbox Arena
author: KevinAsgari
description: Learn how to use Xbox Arena to run tournaments for your game.
ms.author: kevinasg
ms.date: 09-20-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, arena, tournament, ux
ms.localizationpriority: low
---

# Xbox Arena

Xbox Arena is a platform for creating and running online tournaments across Xbox One and Windows 10 through Xbox Live, with the goal of bringing the fun of competitive gaming to a broad audience.
Every publisher has different needs when it comes to competitive gaming, and with Arena we want to be as flexible as possible. So there are three different ways to run tournaments for your titles on Arena:

* With franchise-run tournaments, Xbox Live provides you with the tools and services to set up and run your own tournaments.

* Xbox has partnered with industry-leading Tournament Organizers (TOs), who you can enable to run tournaments on behalf of your title through Arena. (For a list of supported tournament organizers, contact your Microsoft account manager.)

* Players can create and run their own Arena tournaments with our user-generated tournaments, or UGTs.

Most important, adding support for Arena to your titles lets you to take advantage of all three. We provide robust APIs that enable titles to promote tournaments in-game and to facilitate participants’ transition to and from matches. The Xbox Arena Hub supports tournament-management tasks like registration, notifications, brackets and seeding, and reporting results.

> [!IMPORTANT]  
> Xbox Arena is only available to managed partners and ID@Xbox developers. It is not available to the Xbox Live Creators Program.

## A title's baseline tournament experience

If your title integrates with one or more tournament organizers, it must provide a baseline tournament experience. You’re free to integrate more deeply with a specific tournament organizer if you choose, to provide a richer experience for competitions. But you must still provide the baseline experience for other tournament organizers, and for UGTs and franchise-run tournaments if you choose to run them.

### Baseline requirements for a title

* Contact your Microsoft account manager for a full list of requirements.

### UI recommendations

* Identify that the match is a tournament in the UI.

* In your Lobby UI, include a UI element that links users to your tournament hub and/or the tournament detail page in the Xbox Arena shell UI or tournament organizer app.



The baseline user experience that your title must provide is simple and flexible enough to work with lots of possible competition formats. You’re free to adapt the user experience guidance and requirements to fit your title’s UI flow and to ensure a smooth user experience.

For example:

* The required information doesn’t have to be displayed on the main screen, provided that it’s available somewhere, such as on a detail page or pop-up.

* There may be many versions of each screen, or they may be combined with each other or with existing game screens. For example, many games include a post-match “carnage report” screen, which could be adapted to meet both the “Awaiting Results” and “Ready” screen requirements.

* Screens aren’t required to change as soon as the stage does. For example, if the team’s stage switches from ”Ready” to ”Playing” while the user is on a “Ready” screen, your title isn’t required to jump immediately into gameplay. It can (and probably should) switch the “Waiting for match…” indicator to a button—for example, “Ready to Play”—so that the user is in control of the flow and therefore may have a better understanding of it. It’s okay for the requirements of the “Playing” stage to be postponed until the user acknowledges the transition.


## Arena vs. title ‘roles’

Moving in and out of the game, as they progress through tournament stages, can get complicated for users. When the process is different for each game they play, there’s even less chance to memorize where to go and what to expect.

> [!TIP]
> **UX recommendation**  
>
> Simplify functional roles between the game and the Xbox Arena UI, by keeping them clearly distinguishable. For example, all management-related tasks are completed in Arena, and all game play-related tasks are completed inside the game.

Xbox Arena role (setting up a tournament)	| Title's role (game play)
--- | ---
<ul><li>Registration and check-in</li><li>Notifications</li><li>Seeding and brackets</li><li>Team formation</li></ul> | 	<ul><li>Transition of participants to and from the Arena UI</li><li>Identifying tournament-specific matches in multiplayer lobby UI</li><li>Promotion and/or browsing of tournaments in-game</li></ul>

## Engineering guidance

Article | description
--- | ---
[Arena title integration](arena-title-integration.md) | Learn how to integrate support for Xbox Arena into your title.

## User experience guidance

Article | description
--- | ---
[Discovering Xbox tournaments](discovering-xbox-tournaments.md) | Provides tips and recommendations to craft a great user experience for discovering existing tournaments.
[Join a tournament](arena-ux-join-tournament.md)  |  Provides tips and recommendations to craft a great user experience for registering and joining a tournament.
[Match engagement](arena-ux-match-engagement.md) | Describes the user experience stages of players progressing through a tournament.
[Arena API UI metadata](arena-apis-metadata.md)  | Describes the metadata returned by the Arena APIs which you can use to display information in-game about the current state of the tournament.
[Arena notifications](arena-notifications.md)  | Describes the conditions when Xbox Arena sends a notification to tournament participants.
[Arena user scenarios](arena-user-scenarios.md)  | Describes Arena scenarios for gamers based on their most common motivations.
