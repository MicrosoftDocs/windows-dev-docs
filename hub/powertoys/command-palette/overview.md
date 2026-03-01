---
title: PowerToys Command Palette Utility for Windows
description: Learn how to use PowerToys Command Palette, a quick launcher for Windows power users. Access apps, commands, and tools instantly with customizable shortcuts and extensions.
ms.date: 11/13/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the PowerToys Command Palette utility and how to use it.
---

# PowerToys Command Palette utility

PowerToys Command Palette is a quick launcher utility that you can use to access all of your most frequently used commands, apps, and development tools from a single, fast solution. This customizable and extensible tool is designed for Windows power users and serves as the successor to [PowerToys Run](../run.md).

You can enable or disable Command Palette and all other PowerToys on the PowerToys settings Home page. To use the Command Palette, select <kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>Space</kbd> and start typing! *(You can change the keyboard shortcut in the settings window.)*

> [!IMPORTANT]
> For this utility to work, the Command Palette must be enabled and running in the background.

:::image type="content" source="../../images/pt-cmdpal-overview1.gif" alt-text="An animated GIF of PowerToys Command Palette interface showing web search and git information features in action.":::

## Features

Command Palette features include:

- Search for applications, folders, or files
- Run commands by using `>` (for example, `> cmd` launches Command prompt, or `> Shell:startup` opens the Windows startup folder)
- Switch between open windows (previously known as [Window Walker](https://github.com/betsegaw/windowwalker/))
- Do a simple calculation by using calculator
- Add bookmarks for frequently visited webpages
- Execute system commands
- Open web pages or start a web search
- Use rich extensions to add additional commands and features easily

## Using Command Palette

Command Palette is designed to be intuitive and easy to use for quick access to apps, commands, and tools with mouse or keyboard navigation support.

Here are some common tasks you can perform with Command Palette.

### Launch apps

To launch an app, open Command Palette and start typing the name of the app you want to launch. Use the arrow keys to navigate to the desired app and press <kbd>Enter</kbd> to launch it.

### Search the web

To search the web, open Command Palette, type `??` to start a web search, enter your search query, and press <kbd>Enter</kbd>. By default, Command Palette uses your default web browser and search engine to perform the search.

### Run commands

To run a command, open Command Palette and type `>` followed by the command you want to run. For example, typing `> cmd` launches Command Prompt, while typing `> notepad` opens Notepad.

### Search files and folders

To search for files and folders on your computer, open Command Palette and use the arrow keys to navigate to the `Search files` option or type `file` followed by a space. Type your search query, and Command Palette displays a list of matching files and folders.

### Use the calculator

To perform a calculation, open Command Palette and type your mathematical expression directly (for example, `23*47` or `sqrt(256)`). Command Palette displays the result of the calculation. You can also start by typing `=` followed by a space to enter calculator mode.

### Navigate to Windows Settings pages

To open a specific Windows Settings page, open Command Palette and type `$` followed by the name of the settings page you want to open (for example, `$` and `display` to open Display settings). Use the arrow keys to navigate to the desired settings page and press <kbd>Enter</kbd> to open it.

### Find an app in WinGet

To find an app available in WinGet, open Command Palette, use the arrow keys to navigate to the `Search WinGet` option, type the name of the app you want to find, and press <kbd>Enter</kbd>. Command Palette displays a list of matching apps available for installation via WinGet.

### Usage tips

When you enter one of the preceding modes (for example, by typing `>` for commands or `??` for web search), you can press the <kbd>Escape</kbd> key to exit that mode and return to the home screen.

You can enable or disable extensions and assign or change the alias for any command or extension in Command Palette settings. You can also assign global hotkeys to specific commands or extensions to launch them directly when Command Palette is activated.

## Extensibility

Command Palette supports building extensions that you can use to add new functionality and commands. You can create extensions by using .NET. PowerToys provides a [sample extension project](samples.md) to help you get started.

## Settings

Use the **Settings** button in the Command Palette to open the settings page:

:::image type="content" source="../../images/command-palette/cmdpal-settings.png" alt-text="An animated GIF of Command Palette interface with the Settings button highlighted in red.":::

The Command Palette settings page provides the following general options.

| Setting | Description |
| :--- | :--- |
| Activation key | Define the keyboard shortcut to show or hide the Command Palette. |
| Use low-level keyboard hook | Enable this option to use a low-level keyboard hook for activation key detection. This setting can help improve responsiveness but might cause compatibility issues with some software. |
| Ignore shortcut in fullscreen mode | When enabled, the Command Palette activation shortcut is ignored when an application is in fullscreen mode. |
| Go home when activated | When you activate the Command Palette, it returns to the home page. |
| Highlight search on activate | The previous search text is selected when you open the Command Palette. |
| Preferred monitor position | Choose the preferred monitor for the Command Palette to open on. The default setting is **Monitor with mouse cursor**. |
| Show app details | App details automatically expand when displaying an app as a result. |
| Backspace goes back | Typing <kbd>Backspace</kbd> takes you back to the previous page. |
| Single-click activation | Activate list items with a single click. When disabled, single clicking selects the item and double clicking activates it. |
| Show system tray icon | Show or hide the Command Palette icon in the system tray. |
| Disable animations | Disable animations in the Command Palette interface. |
| Enable external reload | Allow external processes to request a reload of Command Palette with the `x-cmdpal://reload` command. |

[!INCLUDE [install-powertoys.md](../../includes/install-powertoys.md)]

## Related content

- [PowerToys Run](../run.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
