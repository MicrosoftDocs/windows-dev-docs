---
title: IListPage.GetItems() Method
description: The GetItems method retrieves the items to be displayed in the list.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IListPage.GetItems() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **GetItems** method retrieves the items to be displayed in the list. This method is used to fetch the data that populates the list.

## Returns

An [IListItem[]](ilistitem.md) containing the items to be displayed in the list. The array may be empty if there are no items to display or if the filters applied to the list result in no matching items.
