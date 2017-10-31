---
author: normesta
ms.assetid: 76776b0f-3163-48c9-835b-3f4213968079
title: Data access
description: This section discusses storing data on the device in a private database and using object relational mapping in Universal Windows Platform (UWP) apps.
ms.author: normesta
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, data, database, relational, tables, sqlite
localizationpriority: medium
---
# Data access


This section discusses storing data on the device in a private database and using object relational mapping in Universal Windows Platform (UWP) apps.

SQLite is included in the UWP SDK. Entity Framework Core works with SQLite in UWP apps. Use these technologies to develop for offline / intermittent connectivity scenarios, and to persist data across app sessions.

| Topic | Description|
|-------|------------|
| [Entity framework Core with SQLite for C# apps](entity-framework-7-with-sqlite-for-csharp-apps.md) | Entity Framework (EF) is an object-relational mapper that enables you to work with relational data using domain-specific objects. This article explains how you can use Entity Framework Core with a SQLite database in a Universal Windows app. |
| [SQLite databases](sqlite-databases.md) | SQLite is a server-less, embedded database engine. This article explains how to use the SQLite library included in the SDK, package your own SQLite library in a Universal Windows app, or build it from the source. |
