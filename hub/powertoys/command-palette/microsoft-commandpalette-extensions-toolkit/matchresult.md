---
title: MatchResult Class
description: The MatchResult class is used to represent the result of a match operation in the Command Palette Extensions Toolkit.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# MatchResult Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **MatchResult** class is used to represent the result of a match operation in the Command Palette Extensions Toolkit. It contains information about whether the match was successful, the score of the match, and any additional data related to the match.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [MatchResult(Boolean, SearchPrecisionScore)](matchresult_constructor.md#matchresultboolean-searchprecisionscore-constructor) | Initializes a new instance of the MatchResult class with a success flag and a search precision score. |
| [MatchResult(Boolean, SearchPrecisionScore, List\<Integer\>, Integer)](matchresult_constructor.md#matchresultboolean-searchprecisionscore-listinteger-integer-constructor) | Initializes a new instance of the MatchResult class with a success flag, a search precision score, match data, and a raw score. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| MatchData | **List\<Integer\>** | A list of integers representing the match data. |
| RawScore | **Integer** | The raw score of the match. |
| Score | **Integer** | The score of the match. |
| SearchPrecision | [SearchPrecisionScore](searchprecisionscore.md) | The precision score of the match. |
| Success | **Boolean** | A boolean value indicating whether the match was successful. |

## Methods

| Method | Description |
| :--- | :--- |
| [IsSearchPrecisionScoreMet()](matchresult_issearchprecisionscoremet.md) | Checks if the search precision score is met. |
