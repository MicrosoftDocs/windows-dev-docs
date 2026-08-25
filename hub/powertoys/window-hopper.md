---
title: PowerToys Window Hopper utility for Windows
description: Learn how to use PowerToys Window Hopper to cycle between windows of the focused app and configure its next and previous shortcuts.
author: GrantMeStrength
ms.author: jken
ms.date: 08/25/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Window Hopper, Alt]
# Customer intent: As a Windows power user, I want to switch between windows of the focused app without cycling through unrelated apps.
---

# Window Hopper utility

PowerToys Window Hopper lets you cycle between windows that belong to the app currently in focus. Unlike <kbd>Alt</kbd>+<kbd>Tab</kbd>, which cycles through windows from all apps, Window Hopper limits the selection to the focused app.

## Cycle between app windows

To cycle forward through the windows of the focused app, hold <kbd>Alt</kbd> and press <kbd>&#96;</kbd>. Window Hopper shows previews of the app's open windows and selects the next window. Continue pressing <kbd>&#96;</kbd> while holding <kbd>Alt</kbd> to move through the windows, and then release <kbd>Alt</kbd> to switch to the selected window.

To cycle backward, hold <kbd>Shift</kbd>+<kbd>Alt</kbd> and press <kbd>&#96;</kbd>. Continue pressing <kbd>&#96;</kbd> while holding <kbd>Alt</kbd> to move backward, and then release <kbd>Alt</kbd> to switch to the selected window.

Press <kbd>Esc</kbd> before releasing <kbd>Alt</kbd> to close Window Hopper without switching windows. Window Hopper doesn't open when the focused app has fewer than two eligible windows.

> [!NOTE]
> On most keyboards, the grave accent or backtick key (<kbd>&#96;</kbd>) is above <kbd>Tab</kbd>.

## Settings

Configure Window Hopper from the PowerToys Settings page:

| Setting | Description |
| :--- | :--- |
| **Enable Window Hopper** | Turn the utility on or off. Window Hopper is off by default. |
| **Next window** | Customize the shortcut that cycles forward through windows of the focused app. The default is <kbd>Alt</kbd>+<kbd>&#96;</kbd>. |
| **Previous window** | Customize the shortcut that cycles backward through windows of the focused app. The default is <kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>&#96;</kbd>. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
