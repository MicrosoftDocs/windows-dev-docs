---
title: INotifyItemsChanged Interface
description: The INotifyItemsChanged interface represents an object that can notify listeners of item changes in the Command Palette.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# INotifyItemsChanged Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **INotifyItemsChanged** interface represents an object that can notify listeners of item changes in the Command Palette. It is used to implement the observer pattern, allowing other components to subscribe to item change notifications.

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<Object, [IItemsChangedEventArgs](iitemschangedeventargs.md)> ItemsChanged | Notifies that items have changed. |
