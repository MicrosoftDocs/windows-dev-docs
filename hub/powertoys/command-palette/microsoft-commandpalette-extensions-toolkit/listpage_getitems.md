---
title: ListItems.GetPage() Method
description: The GetPage method retrieves the items in the current page of the list.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListItems.GetPage() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **GetPage** method retrieves the items in the current page of the list. This method is typically called when the user navigates to a different page in the list.

## Returns

An [IListItem[]](../microsoft-commandpalette-extensions/ilistitem.md) array containing the items in the current page of the list. The number of items returned is determined by the *PageSize* property of the **ListItems** class.
