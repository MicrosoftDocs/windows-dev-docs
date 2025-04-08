---
title: ListHelpers.InPlaceUpdateList<T>(IList<T>, IEnumerable<T>) Method
description: The InPlaceUpdateList<T> method updates the contents of an existing list in place with new contents.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers.InPlaceUpdateList\<T\>(IList\<T\>, IEnumerable\<T\>) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **InPlaceUpdateList\<T\>** method updates the contents of an existing list in place with new contents. This is useful for modifying a list without creating a new instance, which can be more efficient in terms of memory and performance.

## Parameters

*original* **IList\<T\>**

The original list to be updated. This list will be modified in place.

*newContents* **IEnumerable\<T\>**

The new contents to be added to the original list. This can be any collection of items that implement the **IEnumerable\<T\>** interface.
