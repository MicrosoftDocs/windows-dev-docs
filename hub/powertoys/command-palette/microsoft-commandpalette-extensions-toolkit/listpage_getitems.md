---
title: ListPage.GetItems() Method
description: The GetItems method retrieves the items in the current page of the list.
ms.date: 06/03/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListPage.GetItems() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **GetItems** method retrieves the items in the current page of the list. This method is typically called when the user navigates to a different page in the list.

## Returns

An array of [IListItem](../microsoft-commandpalette-extensions/ilistitem.md) implementations containing the items in the current page of the list. The number of items returned is determined by the **PageSize** property of the **ListItems** class.

## Related content

- [IListItem](../microsoft-commandpalette-extensions/ilistitem.md)
- [LoadMore](listpage_loadmore.md)
