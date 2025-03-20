---
title: TreeContent Class (Command Palette Extensions Toolkit)
description: The TreeContent class provides a way to manage tree-like structures in the Command Palette Extensions Toolkit.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# TreeContent Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [ITreeContent](../microsoft-commandpalette-extensions/itreecontent.md)

The **TreeContent** class provides a way to manage tree-like structures in the Command Palette Extensions Toolkit. It allows for the organization of content in a hierarchical manner, making it easier to navigate and manage complex data structures.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Children | [IContent[]](../microsoft-commandpalette-extensions/icontent.md) | The child content items of the tree node. |
| RootContent | [IContent](../microsoft-commandpalette-extensions/icontent.md) | The root content item of the tree. |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler\<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)\> ItemsChanged | Occurs when the items in the tree content change. This event is used to notify subscribers about changes in the tree structure, such as additions or removals of child items. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetChildren()](treecontent_getchildren.md) | Retrieves the child content items of the tree node. This method is used to access the children of a specific node in the tree structure. |
