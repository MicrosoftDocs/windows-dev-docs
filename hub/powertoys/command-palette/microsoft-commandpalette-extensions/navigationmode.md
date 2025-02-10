---
title: NavigationMMode Enum
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# NavigationMode Enum

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

## Fields

| Field | Description |
| :--- | :--- |
| GoBack | Go back one level, then navigate to the page. Going back from the requested page will take you to the page before the current page. |
| GoHome | Clear the back stack, then navigate to the page. Going back from the requested page will take you to the home page. |
| Push | The new page gets added to the current navigation stack. Going back from the requested page will take you to the current page. |
