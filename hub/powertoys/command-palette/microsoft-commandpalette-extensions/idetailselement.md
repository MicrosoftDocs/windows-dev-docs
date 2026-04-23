---
title: IDetailsElement Interface
description: The IDetailsElement interface is used to define an element in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IDetailsElement Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IDetailsElement** interface is used to define an element in the Command Palette. Elements can be used to display additional information or resources related to the item being displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Data | [IDetailsData](idetailsdata.md) | The data associated with the element. |
| Key | **String** | The key of the element. |
