---
title: IContentPage Interface
description: The IContentPage interface is used to define a content page in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IContentPage Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IContentPage** interface is used to define a content page in the Command Palette. Content pages can be used to display additional information or resources related to the item being displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Commands | [IContextItem[]](icontextitem.md) | The commands associated with the content page. |
| Details | [IDetails](idetails.md) | The details associated with the content page. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetContent()](icontentpage_getcontent.md) | Retrieves the content associated with the content page. |
