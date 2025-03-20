---
title: MatchOption Class
description: The MatchOption class is used to define the options for matching in the command palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# MatchOption Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **MatchOption** class is used to define the options for matching in the command palette. It provides properties to specify how the matching should be performed, including case sensitivity and prefix/suffix matching.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| IgnoreCase | **Boolean** | Indicates whether the match should ignore case sensitivity. |
| Prefix | **String** | Indicates whether the match should be a prefix match. |
| Suffix | **String** | Indicates whether the match should be a suffix match. |
