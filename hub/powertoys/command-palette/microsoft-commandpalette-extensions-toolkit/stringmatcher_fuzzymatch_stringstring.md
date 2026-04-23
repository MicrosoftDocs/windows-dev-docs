---
title: StringMatcher.FuzzyMatch(String, String) Method
description: The StringMatcher.FuzzyMatch method performs a fuzzy match between two strings to determine their similarity, accepting parameters of strings to compare.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# StringMatcher.FuzzyMatch(String, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FuzzyMatch** method performs a fuzzy match between two strings to determine their similarity. It compares the *query* parameter with the *stringToCompare* parameter and returns a [MatchResult](matchresult.md) object that indicates the result of the comparison.

## Parameters

*query* **String**

The query to compare against.

*stringToCompare* **String**

The string to compare with the query.

## Returns

A [MatchResult](matchresult.md) object that indicates the result of the fuzzy match.
