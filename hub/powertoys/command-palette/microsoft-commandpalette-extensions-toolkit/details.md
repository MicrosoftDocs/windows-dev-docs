---
title: Details Class
description: The Details class is used to define the details section of an item in the Command Palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Details Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [IDetails](../microsoft-commandpalette-extensions/idetails.md)

The **Details** class is used to define the details section of an item in the Command Palette. It provides properties for setting the title, body, and metadata of the details section.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Body | **String** | The body of the details section. This is the main content that will be displayed in the details section. |
| HeroImage | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | The hero image associated with the details section. This is an image that represents the details section visually. |
| Metadata | [IDetailsElement](../microsoft-commandpalette-extensions/idetailselement.md) | The metadata associated with the details section. This is additional information that can be displayed in the details section. |
| Title | **String** | The title of the details section. This is the main heading that will be displayed in the details section. |
