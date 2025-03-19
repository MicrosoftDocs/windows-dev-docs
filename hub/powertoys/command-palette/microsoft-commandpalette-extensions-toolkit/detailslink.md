---
title: DetailsLink Class
description: The DetailsLink class is used to define a link that can be displayed in the details section of an item in the Command Palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# DetailsLink Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IDetailsLink](../microsoft-commandpalette-extensions/idetailslink.md)

The **DetailsLink** class is used to define a link that can be displayed in the details section of an item in the Command Palette. This class is useful for providing additional information or resources related to the item, allowing users to easily access relevant content.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [DetailsLink()](detailslink_constructor.md#detailslink-constructor) | Initializes a new instance of the **DetailsLink** class. |
| [DetailsLink(String)](detailslink_constructor.md#detailslinkstring-constructor) | Initializes a new instance of the **DetailsLink** class with the specified text. |
| [DetailsLink(String, String)](detailslink_constructor.md#detailslinkstring-string-constructor) | Initializes a new instance of the **DetailsLink** class with the specified text and link. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Link | **Uri** | The URI of the link. |
| Text | **String** | The text to display for the link. |
