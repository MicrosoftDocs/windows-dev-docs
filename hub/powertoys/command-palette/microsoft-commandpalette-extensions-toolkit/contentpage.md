---
title: ContentPage Class
description: 
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ContentPage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Page](page.md)

Implements [IContentPage](../microsoft-commandpalette-extensions/icontentpage.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Commands | [IContextItem[]](../microsoft-commandpalette-extensions/icontextitem.md) | |
| Details | [IDetails](../microsoft-commandpalette-extensions/idetails.md) | |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)> ItemsChanged | |

## Methods

| Method | Description |
| :--- | :--- |
| [GetContent()](contentpage_getcontent.md) | |
| [RaiseItemsChanged(Int)](contentpage_raiseitemschanged.md) | |
