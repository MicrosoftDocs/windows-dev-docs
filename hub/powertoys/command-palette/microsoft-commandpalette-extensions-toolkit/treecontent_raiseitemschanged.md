---
title: TreeContent.RaiseItemsChanged(Integer) Method
description: The RaiseItemsChanged method is used to notify the Command Palette that the items in the tree content have changed.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# TreeContent.RaiseItemsChanged(Integer) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **RaiseItemsChanged** method is used to notify the Command Palette that the items in the tree content have changed. This method is typically called when the number of items in the tree content has changed, and it allows the Command Palette to update its display accordingly.

## Parameters

*totalItems* **Integer**

An integer representing the total number of items in the tree content. This value is used to update the Command Palette with the new item count.
