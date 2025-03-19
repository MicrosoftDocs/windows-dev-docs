---
title: IconData Class
description: The IconData class is used to represent icon data for a command in the Command Palette.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconData Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IIconData](../microsoft-commandpalette-extensions/iicondata.md)

The **IconData** class is used to represent icon data for a command in the Command Palette. It provides properties to access the icon data and its associated URI.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [IconData(IRandomAccessStreamReference)](icondata_constructor.md#icondatairandomaccessstreamreference-constructor) | Initializes the **Data** property with the provided **IRandomAccessStreamReference**. |
| [IconData(String)](icondata_constructor.md#icondatastring-constructor) | Initializes the **Icon** property with the provided **String**. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Data | **Windows.Storage.Streams.IRandomAccessStreamReference** | Gets or sets the icon data as a stream reference. This property is used to load the icon data from a stream. |
| Icon | **String** | Gets or sets the icon URI. This property is used to load the icon from a URI. |
