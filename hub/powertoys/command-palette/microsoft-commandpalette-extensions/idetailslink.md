---
title: IDetailsLink Interface
description: The IDetailsLink interface is used to define a link in a details page in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IDetailsLink Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IDetailsLink** interface is used to define a link in a details page in the Command Palette. Links are used to provide additional information or resources related to the item being displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Link | **Windows.Foundation.Uri** | The URI of the link. |
| Text | **String** | The text to display for the link. |
