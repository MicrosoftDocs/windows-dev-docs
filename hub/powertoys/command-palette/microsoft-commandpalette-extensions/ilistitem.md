---
title: IListItem Interface
description: The IListItem interface represents an item in the Command Palette list.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IListItem Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IListItem** interface represents an item in the Command Palette list. It is used to define the properties and behavior of individual items displayed in the list, allowing users to interact with and select from the available options.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Details | [IDetails](idetails.md) | The details of the item. This property can be used to provide additional information or context about the item, such as a description or metadata. |
| Section | **String** | The section to which the item belongs. This property can be used to group items into sections, allowing for better organization and navigation within the list. |
| Tags | [ITag[]](itag.md) | The tags associated with the item. This property can be used to categorize or label the item, making it easier for users to filter and search for specific items. |
| TextToSuggest | **String** | The text used to suggest the item. This property can be used to provide a hint or suggestion to the user about the item, helping them to understand its purpose or functionality. |
