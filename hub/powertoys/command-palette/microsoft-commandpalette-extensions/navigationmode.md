---
title: NavigationMMode Enum
description: The NavigationMode enum defines the different modes of navigation that can be used in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# NavigationMode Enum

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **NavigationMode** enum defines the different modes of navigation that can be used in the Command Palette. Each mode specifies how the navigation stack should be managed when navigating to a new page. This is useful for controlling the behavior of the navigation experience in your application.

## Fields

| Field | Description |
| :--- | :--- |
| GoBack | Go back one level, then navigate to the page. Going back from the requested page will take you to the page before the current page. |
| GoHome | Clear the back stack, then navigate to the page. Going back from the requested page will take you to the home page. |
| Push | The new page gets added to the current navigation stack. Going back from the requested page will take you to the current page. |
