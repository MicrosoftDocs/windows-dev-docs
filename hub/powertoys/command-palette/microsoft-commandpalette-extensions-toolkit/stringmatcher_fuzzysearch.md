---
title: StringMatcher.FuzzySearch(String, String) Method
description: The StringMatcher.FuzzySearch method performs a fuzzy search between two strings to determine their similarity.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# StringMatcher.FuzzySearch(String, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FuzzySearch** method performs a fuzzy search between two strings to determine their similarity. It compares the *query* parameter with the *stringToCompare* parameter and returns a [MatchResult](matchresult.md) object that indicates the result of the comparison.

## Parameters

*query* **String**

The string to be compared against the *stringToCompare* string.

*stringToCompare* **String**

The string to be compared with the *query* string.

## Returns

A [MatchResult](matchresult.md) object that contains the result of the fuzzy search.
