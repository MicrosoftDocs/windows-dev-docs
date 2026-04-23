---
title: IFilters Interface
description: The IFilters interface is used to manage the filters in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IFilters Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IFilters** interface is used to manage the filters in the Command Palette. It provides methods to retrieve the current filter ID and to get a list of available filters.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| CurrentFilterId | **String** | The ID of the currently selected filter. This property is used to identify which filter is currently active in the Command Palette. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetFilters()](ifilters_getfilters.md) | Retrieves a list of available filters in the Command Palette. This method is used to get the filters that can be applied to the items displayed in the Command Palette. |
