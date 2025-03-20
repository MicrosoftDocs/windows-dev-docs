---
title: ITreeContent Interface
description: The ITreeContent interface is used to represent a tree structure of content on a content page in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ITreeContent Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ITreeContent** interface is used to represent a tree structure of content on a content page in the Command Palette. It allows for hierarchical organization of content, enabling users to navigate through different levels of information.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| RootContent | [IContent](icontent.md) | Gets the root content of the tree. This is the top-level item in the hierarchy. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetChildren()](itreecontent_getchildren.md) | Retrieves the child items of the current content. This method is used to navigate through the tree structure. |
