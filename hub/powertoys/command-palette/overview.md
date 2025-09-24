---
title: PowerToys Command Palette Utility for Windows
description: Learn how to use PowerToys Command Palette, a quick launcher for Windows power users. Access apps, commands, and tools instantly with customizable shortcuts and extensions.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the PowerToys Command Palette utility and how to use it.
---

# PowerToys Command Palette utility

PowerToys Command Palette is a quick launcher utility that allows you to easily access all of your most frequently used commands, apps, and development tools from a single, fast solution. This customizable and extensible tool is designed for Windows power users and serves as the successor to [PowerToys Run](../run.md).

Command Palette, and all other PowerToys, can be enabled or disabled on the PowerToys settings Home page. To use the Command Palette, select <kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>Space</kbd> and start typing! *(Note that the keyboard shortcut can be changed in the settings window.)*

> [!IMPORTANT]
> For this utility to work, the Command Palette must be enabled and running in the background.

:::image type="content" source="../../images/pt-cmdpal-overview1.gif" alt-text="An animated GIF of PowerToys Command Palette interface showing web search and git information features in action.":::

## Features

Command Palette features include:

- Search for applications, folders or files
- Run commands using `>` (for example, `> cmd` will launch Command prompt, or `> Shell:startup` will open the Windows startup folder)
- Switch between open windows (previously known as [Window Walker](https://github.com/betsegaw/windowwalker/))
- Do a simple calculation using calculator
- Add bookmarks for frequently visited webpages
- Execute system commands
- Open web pages or start a web search
- Rich extensions to add additional commands and features easily

## Settings

You can open the settings page by using the **Settings** button in the Command Palette:

:::image type="content" source="../../images/command-palette/cmdpal-settings.png" alt-text="An animated GIF of Command Palette interface with the Settings button highlighted in red.":::

The following general options are available on the Command Palette settings page.

| Setting | Description |
| :--- | :--- |
| Activation key | Define the keyboard shortcut to show/hide the Command Palette. |
| Go home when activated | When the Command Palette is activated it will return to the home page. |
| Highlight search on activate | The previous search text will be selected when the Command Palette is opened. |
| Preferred monitor position | Choose the preferred monitor for the Command Palette to open on. The default setting is **Monitor with mouse cursor**. |
| Show app details | App details are automatically expanded when displaying an app as a result. |
| Backspace goes back | Typing <kbd>Backspace</kbd> will take you back to the previous page. |
| Single-click activation | Activate list items with a single click. When disabled, single clicking selects the item and double clicking activates it. |

## Related content

- [PowerToys Run](../run.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
