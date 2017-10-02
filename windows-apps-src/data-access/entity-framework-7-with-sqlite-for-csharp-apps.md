---
author: normesta
ms.assetid: BC7E8130-A28A-443C-8D7E-353E7DA33AE3
description: Entity Framework (EF) is an object-relational mapper that enables you to work with relational data using domain-specific objects.
title: Entity framework Core with SQLite for C# apps
ms.author: normesta
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, SQLite, C#, EF, entity framework
---

# Entity Framework Core with SQLite for C# apps


Entity Framework (EF) is an object-relational mapper that enables you to work with relational data using domain-specific objects. This article explains how you can use Entity Framework Core with a SQLite database in a Universal Windows app.

Originally for .NET developers, Entity Framework Core can be used with SQLite on Universal Windows Platform (UWP) to store and manipulate relational data using domain specific objects. You can migrate EF code from a .NET app to a UWP app and expect it work with appropriate changes to the connection string.

Currently EF only supports SQLite on UWP. A detailed walkthrough on installing Entity Framework Core, and creating models is available at the [Getting Started on Universal Windows Platform page](http://go.microsoft.com/fwlink/p/?LinkId=735013). It covers the following topics:

-   Prerequisites
-   Create a new project
-   Install Entity Framework
-   Create your model
-   Create your database
-   Use your model
