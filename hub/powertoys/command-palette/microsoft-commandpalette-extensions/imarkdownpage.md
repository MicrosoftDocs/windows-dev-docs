---
title: IMarkdownPage Interface
description: The IMarkdownPage interface represents a page in the Command Palette that can display Markdown content.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IMarkdownPage Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IMarkdownPage** interface represents a page in the Command Palette that can display Markdown content. It is used to provide rich text formatting and layout options for displaying information in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Commands | [IContextItem[]](icontextitem.md) | The commands associated with the page. |

## Methods

| Method | Description |
| :--- | :--- |
| [Bodies()](imarkdownpage_bodies.md) | Returns the bodies of the page. |
| [Details()](imarkdownpage_details.md) | Returns the details of the page. |
