---
title: ContentPage Class
description: The ContentPage class is a specialized page that can be used to display a collection of commands and details in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ContentPage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Page](page.md)

Implements [IContentPage](../microsoft-commandpalette-extensions/icontentpage.md)

The **ContentPage** class is a specialized page that can be used to display a collection of commands and details in the command palette. It provides a way to organize and present commands in a structured manner, allowing for better user interaction and navigation.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Commands | [IContextItem[]](../microsoft-commandpalette-extensions/icontextitem.md) | The collection of commands that will be displayed on the page. |
| Details | [IDetails](../microsoft-commandpalette-extensions/idetails.md) | The details associated with the page. This can include additional information or context about the commands displayed. |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler\<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)\> ItemsChanged | Occurs when the items in the page have changed. This event can be used to update the UI or perform other actions when the commands or details are modified. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetContent()](contentpage_getcontent.md) | Retrieves the content of the page, including the commands and details. This method can be used to access the current state of the page and its items. |
| [RaiseItemsChanged(Int)](contentpage_raiseitemschanged.md) | Raises the **ItemsChanged** event with the new number of items following the change. This method can be used to notify subscribers about changes to the items displayed on the page. |
