---
title: BaseObservable Class
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# BaseObservable Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Implements [INotifyPropChanged](../microsoft-commandpalette-extensions/inotifypropchanged.md)

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<object, [IPropChangedEventArgs](../microsoft-commandpalette-extensions/ipropchangedeventargs.md)>? PropChanged | Notifies that a property value has changed. |

## Methods

| Method | Description |
| :--- | :--- |
| [OnPropertyChanged(String)](baseobservable_onpropertychanged.md) | Raises the `PropChanged` event to notify that a property value has changed. |
