---
title: GoToPageArgs Class
description: The GoToPageArgs class is used to represent the arguments for navigating to a page in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# GoToPageArgs Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IGoToPageArgs](../microsoft-commandpalette-extensions/igotopageargs.md)

The **GoToPageArgs** class is used to represent the arguments for navigating to a page in the Command Palette. It provides properties for the type of navigation mode and the ID of the page to navigate to.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| NavigationMode | [NavigationMode](../microsoft-commandpalette-extensions/navigationmode.md) | Gets or sets the navigation mode. Defaults to [NavigationMode.Push](../microsoft-commandpalette-extensions/navigationmode.md#fields). |
| PageId | **String** | Required. Gets or sets the ID of the page. |
