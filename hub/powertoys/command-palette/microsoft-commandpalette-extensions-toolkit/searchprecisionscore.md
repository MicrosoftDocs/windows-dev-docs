---
title: SearchPrecisionScore Enum
description: The SearchPrecisionScore enum is used to specify the precision score of a search. The precision score is a measure of how closely the search results match the user's query.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# SearchPrecisionScore Enum

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **SearchPrecisionScore** enum is used to specify the precision score of a search. The precision score is a measure of how closely the search results match the user's query. A higher precision score indicates that the search results are more relevant to the user's query.

## Fields

| Name | Value | Description |
| :--- | :--- | :--- |
| None | `0` | No precision score. |
| Low | `20` | Considered a low precision score. |
| Regular | `50` | Considered a regular precision score. |
