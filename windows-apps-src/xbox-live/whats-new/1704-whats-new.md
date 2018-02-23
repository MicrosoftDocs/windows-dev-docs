---
title: What's new for the Xbox Live APIs - April 2017
author: KevinAsgari
description: What's new for the Xbox Live APIs - April 2017
ms.assetid: 
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, arena, tournaments
ms.localizationpriority: low
---

# What's new for the Xbox Live APIs - April 2017

Please see the [What's New - March 2017](1703-whats-new.md) article for what was added in the March 2017 release.

## Xbox Services APIs

### Visual Studio 2017

The Xbox Live APIs have been updated to support Visual Studio 2017, for both Universal Windows Platform (UWP) and Xbox One titles.

### Tournaments

New APIs have been added to support tournaments. You can now use the `xbox::services::tournaments::tournament_service` class to access the tournaments service from your title.

These new tournament APIs enable the following scenarios:

* Query the service to find all existing tournaments for the current title.
* Retrieve details about a tournament from the service.
* Query the service to retrieve a list of teams for a tournament.
* Retrieve details about the teams for a tournament from the service.
* Track changes to tournaments and teams by using Real Time Activity (RTA) subscriptions.
