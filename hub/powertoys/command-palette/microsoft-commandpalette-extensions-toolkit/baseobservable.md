---
title: BaseObservable Class
description: The BaseObservable class is a base class for objects that need to notify clients about changes to their properties.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# BaseObservable Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [INotifyPropChanged](../microsoft-commandpalette-extensions/inotifypropchanged.md)

The **BaseObservable** class is a base class for objects that need to notify clients about changes to their properties. It implements the **INotifyPropChanged** interface and provides a **PropChanged** event.

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler\<object, [IPropChangedEventArgs](../microsoft-commandpalette-extensions/ipropchangedeventargs.md)\> PropChanged | Notifies that a property value has changed. |

## Methods

| Method | Description |
| :--- | :--- |
| [OnPropertyChanged(String)](baseobservable_onpropertychanged.md) | Raises the **PropChanged** event to notify that a property value has changed. |
