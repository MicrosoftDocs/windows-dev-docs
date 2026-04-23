---
title: StringMatcher Class
description: The StringMatcher class provides methods for performing fuzzy matching and searching on strings in the Command Palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# StringMatcher Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **StringMatcher** class provides methods for performing fuzzy matching and searching on strings. It is used to compare two strings and determine their similarity based on a specified precision score.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [StringMatcher()](stringmatcher_constructor.md) | Initializes a new instance of the **StringMatcher** class. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Instance | **StringMatcher** | Gets the singleton instance of the **StringMatcher** class. |
| UserSettingSearchPrecision | [SearchPrecisionScore](searchprecisionscore.md) | Gets or sets the precision score used for fuzzy matching. |

## Methods

| Method | Description |
| :--- | :--- |
| [FuzzyMatch(String, String)](stringmatcher_fuzzymatch_stringstring.md) | Performs a fuzzy match between two strings. |
| [FuzzyMatch(String, String, MatchOption)](stringmatcher_fuzzymatch_stringstringmatchoption.md) | Performs a fuzzy match between two strings with a specified match option. |
| [FuzzySearch(String, String)](stringmatcher_fuzzysearch.md) | Performs a fuzzy search between two strings. |
