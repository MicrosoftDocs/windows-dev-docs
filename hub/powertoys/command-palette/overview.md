---
title: PowerToys Command Palette utility for Windows
description: Command Palette is a quick launcher for power users that contains additional features without sacrificing performance.
ms.date: 2/4/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the PowerToys Command Palette utility and how to use it.
---

# PowerToys Command Palette utility

PowerToys Command Palette allows you to easily access all of your most frequently used commands, apps, and development tools - all from a single solution that is fast, customizable to your unique preferences, and extensible to include your favorite apps. The Command Palette is intended to be the successor of [PowerToys Run](../run.md).

To use the Command Palette, select <kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>Space</kbd> and start typing! *(Note that the keyboard shortcut can be changed in the settings window.)*

> [!IMPORTANT]
> For this utility to work, the Command Palette must be enabled and running in the background.

## Features

Command Palette features include:

- Search for applications, folders or files
- Search for running processes (previously known as [Window Walker](https://github.com/betsegaw/windowwalker/))
- Invoke Shell Plugin using `>` (for example, `> Shell:startup` will open the Windows startup folder)
- Do a simple calculation using calculator
- Add bookmarks for frequently visited webpages
- Execute system commands
- Open web pages or start a web search

## Settings

The following general options are available within the Command Palette settings page. You can open the settings page by using the Command Palette.

| Setting | Description |
| :--- | :--- |
| Activation shortcut | Define the keyboard shortcut to show/hide the Command Palette. |
| Go home when activated | When the Command Palette is activated it will return to the home page. |
| Highlight search on activate | The previous search text will be selected when the Command Palette is opened. |
| Show app details | App details are automatically expanded when displaying an app as a result. |
| Backspace goes back | Typing <kbd>Backspace</kbd> will take you back to the previous page. |
| Single-click activates | Activate list items with a single click. When disabled, single clicking selects the item and double clicking activates it. |

## Related content

- [PowerToys Run](../run.md)
- [Extensibility overview](creating-an-extension.md)
- [Extension samples](samples.md)
