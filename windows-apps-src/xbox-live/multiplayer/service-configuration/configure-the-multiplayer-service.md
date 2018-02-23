---
title: Multiplayer Service Configuration
author: KevinAsgari
description: Learn how to configure the Xbox Live Multiplayer Service.
ms.assetid: d042d4d5-1c75-4257-8a6f-07eddd39ca7e
ms.author: kevinasg
ms.date: 07/12/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, service configuration, session template, custom invite string, smartmatch hopper
ms.localizationpriority: low
---

# Multiplayer Service Configuration
In order for your title to take advantage of the services that Xbox Live provides, you must first define your service configuration. This service configuration exists in the Xbox Live cloud service, and defines how the Xbox Live service interacts with any devices that are running your title/game.

For multiplayer services, there are three aspects of multiplayer that you can configure:
* Session templates
* SmartMatch Hoppers
* Custom invite strings

## Session templates
The Xbox multiplayer service allows gamers to create and join sessions, to exchange session messages with other gamers in the same session, and to post the results of their play to the session. (Posting the results cleans up the session, and also updates the leaderboards for all players in the session.)

For example, a multiplayer session could be a single game of chess between two players. Alternatively, it could be a continuing session of an action and adventure title played by a much larger number of players.

When a game creates a new session, it creates the session based off of a predefined session template. This template is essentially a JSON object that contains attributes that describe the session.

When you create a new session template, you must define the following:

| Field | Description |
| --- | --- |
| Session Name | Enter a name that characterizes the multiplayer session template, and that you will easily remember and recognize. The name must be a text string, with a maximum of 100 characters. |
| Contract Version | This value is auto-populated by the system and denotes the current system version of the JSON contract. Do not edit it. |
| Session Template (JSON text) | Specify the JSON data that describes the different attributes associated with a multiplayer session. |

For more info about multiplayer session templates, including several predefined templates that you can use as a basis for the JSON text, see [Multiplayer session templates](session-templates.md).

> **Important:** After a title passes Final Certification, existing multiplayer sessions in that title can no longer be changed or deleted.

## SmartMatch hoppers

An optional addition to the Xbox multiplayer service is the Xbox server-based matchmaking service, which provides a method of grouping players together based on information provided by the title or stored in user statistics, or based on the user's preferences, or based on quality of service.

Because Xbox One matchmaking is server-based, users can provide a request to the service and then be notified later, whenever a match is found. That is: the user is not forced to wait in your title while the matchmaking process occurs—they are free to play the single-player portion of your title, or even to play other titles, and still be candidates for matchmaking. This eliminates the need to achieve a "critical mass" of players before matches can be found.

A matchmaking hopper must be based on a previously defined session template.

When you create a new matchmaking hopper, you must define the following:

| Field | Description |
|---|---|
|Name| Enter a name that characterizes the matchmaking hopper, and that you will easily remember and recognize. The name must be a text string, with a maximum of 140 characters. |
| Min Group Size | Specify the minimum acceptable number of players. Minimum value is 1. |
| Max Group Size | Specify the maximum acceptable number of players. Maximum value is 256. |
| Should Rule Expansion Cycles | The default value is 3. The default value should not need to be changed for normal player populations. |
| Ranked hopper | If a hopper is marked as a Ranked Hopper it allows players in that hopper to be matched together even if they have blocked each other. This helps in preventing people from trying to avoid players with greater skill by blocking them. |
| Auto update from session | When this field is enabled, changes made to the session’s member list or members’ custom properties will automatically propagate to a previously submitted ticket. |

> **Important:** After a title passes Final Certification, existing matchmaking hoppers in that title can no longer be changed or deleted.

## Custom invite strings
When your title sends an invitation to a player to join a multiplayer game, you can choose to display a custom invite text string instead of the default invite string.

When you create a new custom invite string, you must define the following:

| Field | Description |
|---|---|
| ID | The ID of the custom invite string that will be used to identify the string. "custominvitestrings_" will automatically be appended to the beginning of your ID. Max 100 characters |
| Value | The text of the custom invite string that will appear in your custom invite toast. Max 100 characters |

## Additional information

For more information about configuring the multiplayer service, see the following articles:

**Article** | **Description**
--- | ---
[Configure your AppXManifest for Multiplayer](configure-your-appxmanifest-for-multiplayer.md) | Describes how to configure a UWP  AppXManifest file to work with the Xbox Live multiplayer service.
[Multiplayer session templates](session-templates.md) | Gives a brief overview of multiplayer session templates and provides several examples of templates that you can copy and modify for your multiplayer sessions.
[Session template constants](session-template-constants.md) | Describes the predefined elements of a multiplayer session template.
[Large sessions](large-sessions.md) | Describes when and how to use large sessions.
