---
title: IDynamicListPage Interface
description: The IDynamicListPage interface is used to define a dynamic list page in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IDynamicListPage Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IDynamicListPage** interface is used to define a dynamic list page in the Command Palette. This interface allows extensions to create and manage dynamic lists of items that can be displayed in the Command Palette.

A dynamic list leaves the extension in charge of filtering the list of items.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| SearchText | **String** | The search text used to filter the list of items. This property is used to define the text that is used to filter the items in the dynamic list. |
