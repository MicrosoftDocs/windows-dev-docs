---
title: What's new in Xbox Live APIs - July 2017
author: KevinAsgari
description: What's new in Xbox Live APIs - July 2017
ms.assetid: 
ms.author: kevinasg
ms.date: 07/16/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, what's new, july 2017
ms.localizationpriority: low
---

# What's new for the Xbox Live APIs - July 2017

Please see the [What's New - June 2017](1706-whats-new.md) article for what was added in the June 2017 release.

You can also check the [Xbox Live API GitHub commit history](https://github.com/Microsoft/xbox-live-api/commits/master) to see all of the recent code changes to the Xbox Live APIs.

## Xbox Live features

### Multiplayer updates

Querying activity handles and search handles now includes the custom session properties in the response.

### Tournaments

New APIs have been added to support tournaments. You can now use the xbox::services::tournaments::tournament_service class to access the tournaments service from your title.
These new tournament APIs enable the following scenarios:
* Query the service to find all existing tournaments for the current title.
* Retrieve details about a tournament from the service.
* Query the service to retrieve a list of teams for a tournament.
* Retrieve details about the teams for a tournament from the service.
* Track changes to tournaments and teams by using Real Time Activity (RTA) subscriptions.
