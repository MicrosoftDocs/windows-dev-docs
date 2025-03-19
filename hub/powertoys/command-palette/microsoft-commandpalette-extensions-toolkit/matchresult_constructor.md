---
title: MatchResult Constructors
description: Initializes a new instance of the MatchResult class with specified parameters.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# MatchResult Constructors

## MatchResult(Boolean, [SearchPrecisionScore](searchprecisionscore.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [MatchResult](matchresult.md) class with its [Success](matchresult.md#properties) property set to *success* and [SearchPrecision](matchresult.md#properties) set to *searchPrecision*.

```C#
public MatchResult(bool success, SearchPrecisionScore searchPrecision)
    {
        Success = success;
        SearchPrecision = searchPrecision;
    }
```

### Parameters

*success* **Boolean**

Indicates whether the match was successful.

*searchPrecision* [SearchPrecisionScore](searchprecisionscore.md)

The search precision score for the match. This score is used to determine how closely the match aligns with the search criteria.

## MatchResult(Boolean, [SearchPrecisionScore](searchprecisionscore.md), List\<Integer\>, Integer) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [MatchResult](matchresult.md) class with its [Success](matchresult.md#properties) property set to *success*, [SearchPrecision](matchresult.md#properties) set to *searchPrecision*, [MatchData](matchresult.md#properties) set to *matchData*, and [RawScore](matchresult.md#properties) set to *rawScore*.

```C#
public MatchResult(bool success, SearchPrecisionScore searchPrecision, List<int> matchData, int rawScore)
    {
        Success = success;
        SearchPrecision = searchPrecision;
        MatchData = matchData;
        RawScore = rawScore;
    }
```

### Parameters

*success* **Boolean**

Indicates whether the match was successful.

*searchPrecision* [SearchPrecisionScore](searchprecisionscore.md)

The search precision score for the match. This score is used to determine how closely the match aligns with the search criteria.

*matchData* **List\<Integer\>**

The list of match data. This data can be used to provide additional context or information about the match.

*rawScore* **Integer**

The raw score for the match. This score is used to quantify the quality of the match based on the search criteria.
