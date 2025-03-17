---
title: DynamicListPage Class
description: The DynamicListPage class allows for dynamic updates to the list of items displayed in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# DynamicListPage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [ListPage](listpage.md)

Implements [IDynamicListPage](../microsoft-commandpalette-extensions/idynamiclistpage.md)

The **DynamicListPage** class is a specialized version of the [ListPage](listpage.md) class that allows for dynamic updates to the list of items displayed in the command palette. This class is useful for scenarios where the list of items may change frequently or needs to be updated based on user interactions or other events.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| SearchText | String | Overrides the [SearchText](listpage.md#properties) property of the [ListPage](listpage.md) class. |

## Methods

| Method | Description |
| :--- | :--- |
| [UpdateSearchText(String, String)](dynamiclistpage_updatesearchtext.md) | Updates the search text for the dynamic list page. |
