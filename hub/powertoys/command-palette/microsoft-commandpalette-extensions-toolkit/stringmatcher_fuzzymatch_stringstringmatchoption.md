---
title: StringMatcher.FuzzyMatch(String, String, MatchOption) Method
description: The StringMatcher.FuzzyMatch method performs a fuzzy match between two strings to determine their similarity.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# StringMatcher.FuzzyMatch(String, String, [MatchOption](matchoption.md)) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FuzzyMatch** method performs a fuzzy match between two strings to determine their similarity. It compares the *query* parameter with the *stringToCompare* parameter and returns a [MatchResult](matchresult.md) object that indicates the result of the comparison.

## Parameters

*query* **String**

The string to be compared against the *stringToCompare* parameter.

*stringToCompare* **String**

The string to be compared with the *query* parameter.

*opt* [MatchOption](matchoption.md)

The options that control the fuzzy matching behavior. This parameter allows you to specify how the fuzzy match should be performed, such as case sensitivity or other matching criteria.

## Returns

A [MatchResult](matchresult.md) object that contains the result of the fuzzy match.
