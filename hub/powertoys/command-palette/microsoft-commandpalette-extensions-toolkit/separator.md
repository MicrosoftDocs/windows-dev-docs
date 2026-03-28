---
title: Separator Class
description: Represents a visual separator in a list of items.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Separator Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements IListItem, ISeparatorContextItem, ISeparatorFilterItem

The **Separator** class represents a visual separator in a list of items. Can optionally include a title that acts as a section header.

## Constructors

| Constructor | Description |
| :--- | :--- |
| Separator(String) | Creates a separator with an optional title. The `title` parameter is the section header text; defaults to empty string. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Title | **String** | Gets or sets the section header text displayed alongside the separator. |
| Section | **String** | Gets the section name (same as Title). |
| Command | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | Always returns `null`. |
| Details | [IDetails](../microsoft-commandpalette-extensions/idetails.md) | Always returns `null`. |
| Icon | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | Always returns `null`. |
| MoreCommands | [IContextItem[]](../microsoft-commandpalette-extensions/icontextitem.md) | Always returns `null`. |
| Subtitle | **String** | Always returns `null`. |
| Tags | [ITag[]](../microsoft-commandpalette-extensions/itag.md) | Always returns `null`. |
| TextToSuggest | **String** | Always returns `null`. |
