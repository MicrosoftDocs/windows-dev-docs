---
title: ContentPage.RaiseItemsChanged(Int) Method
description: The ContentPage.RaiseItemsChanged(Int) method raises the ItemsChanged event and specifies the number of changed items.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ContentPage.RaiseItemsChanged(Int) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Raises the **ItemsChanged** event with the specified change type. This method can be used to notify subscribers about changes in the items displayed on the page.

## Parameters

**`totalItems`** Int

The total number of items in the collection. This parameter is used to indicate the current state of the items and can be used for UI updates or other actions.
