---
title: ItemsChangedEventArgs Constructors
description: Initializes a new instance of the ItemsChangedEventArgs class with the specified total number of items.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ItemsChangedEventArgs Constructors

## ItemsChangedEventArgs(Integer) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ItemsChangedEventArgs](itemschangedeventargs.md) class with the [TotalItems](itemschangedeventargs.md#properties) property set to *totalItems* that defaults to `-1`.

```C#
 public ItemsChangedEventArgs(int totalItems = -1)
    {
        TotalItems = totalItems;
    }
```

### Parameters

*totalItems* **Integer**

The total number of items that have changed. This parameter is optional and defaults to `-1` if not provided.
