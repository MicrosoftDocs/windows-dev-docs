---
title: ListHelpers.ScoreListItem(String, ICommandItem) Method
description: The ScoreListItem method is a static method that scores a list item based on a search string and a command item.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers.ScoreListItem(String, ICommandItem) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ScoreListItem** method is a static method that scores a list item based on a search string and a command item. It is used to determine how well the list item matches the search criteria.

## Parameters

*query* **String**

The search string used to score the list item. This string is typically the text entered by the user in the command palette.

*listItem* [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)

The command item to be scored. This item represents a command or action that can be executed in the command palette.

## Returns

An **Integer** representing the score of the list item based on the search string and command item.
