---
title: IGoToPageArgs Interface
description: The IGoToPageArgs interface represents the arguments for navigating to a specific page in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IGoToPageArgs Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Derived [GoToPageArgs](../microsoft-commandpalette-extensions-toolkit/gotopageargs.md)

The **IGoToPageArgs** interface represents the arguments for navigating to a specific page in the Command Palette. It is used to define the parameters for navigating to a page, such as the page ID and navigation mode.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| NavigationMode | [NavigationMode](navigationmode.md) | Gets or sets the navigation mode. |
| PageId | **String** | Gets or sets the ID of the page to navigate to. |
