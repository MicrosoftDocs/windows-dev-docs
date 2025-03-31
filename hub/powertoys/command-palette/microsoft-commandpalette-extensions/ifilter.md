---
title: IFilter Interface
description: The IFilter interface represents a filter in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IFilter Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IFilter** interface represents a filter in the Command Palette. It is used to define the properties and methods associated with a filter that can be applied to the items displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Icon | [IIconInfo](iiconinfo.md) | The icon associated with the filter. This property is used to define the visual representation of the filter in the Command Palette. |
| Id | **String** | The unique identifier for the filter. This property is used to identify the filter in the Command Palette. |
| Name | **String** | The name of the filter. This property is used to define the display name of the filter in the Command Palette. |
