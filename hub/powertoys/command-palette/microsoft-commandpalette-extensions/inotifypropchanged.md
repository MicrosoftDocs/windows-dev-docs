---
title: INotifyPropChanged Interface
description: The INotifyPropChanged interface represents an object that can notify listeners of property changes in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# INotifyPropChanged Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **INotifyPropChanged** interface represents an object that can notify listeners of property changes in the Command Palette. It is used to implement the observer pattern, allowing other components to subscribe to property change notifications.

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<Object, [IPropChangedEventArgs](ipropchangedeventargs.md)> PropChanged | Notifies that a property value has changed. |
