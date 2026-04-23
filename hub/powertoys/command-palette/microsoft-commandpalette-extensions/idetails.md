---
title: IDetails Interface
description: The IDetails interface is used to define the details view of a command in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IDetails Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IDetails** interface is used to define the details view of a command in the Command Palette. Details can be used to provide additional information or resources related to the item being displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Body | **String** | The body of the details. |
| HeroImage | [IIconInfo](iiconinfo.md) | The hero image associated with the details. |
| Metadata | [IDetailsElement[]](idetailselement.md) | The metadata associated with the details. |
| Title | **String** | The title of the details section. |
