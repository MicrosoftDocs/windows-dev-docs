---
title: MarkdownContent Class
description: The MarkdownContent class is used to define the content of a markdown file.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# MarkdownContent Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [IMarkdownContent](../microsoft-commandpalette-extensions/imarkdowncontent.md)

The **MarkdownContent** class is used to represent the content of a command palette item in Markdown format. It provides properties to specify the body of the Markdown content and whether it should be rendered as HTML.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Body | String | The body of the Markdown content. This property is required and cannot be null or empty. |
