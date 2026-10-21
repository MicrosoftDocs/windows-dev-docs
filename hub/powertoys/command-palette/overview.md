---
title: PowerToys Command Palette Utility for Windows
description: Learn how to use PowerToys Command Palette, a quick launcher for Windows power users. Access apps, commands, and tools instantly with customizable shortcuts and extensions.
ms.date: 10/14/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the PowerToys Command Palette utility and how to use it.
---

# PowerToys Command Palette utility

PowerToys Command Palette helps you launch apps, run commands, search files, browse the web, and reach Windows tools from one place. It is customizable, extensible, and designed for keyboard-first workflows.

Press <kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>Space</kbd> and start typing. That's it.

> [!IMPORTANT]
> Command Palette must be enabled and running in the background. You can enable or disable it from the PowerToys settings Home page, and change the activation shortcut in [Command Palette settings](settings.md).

If you prefer a smaller launcher, set **How Command Palette opens** to **Compact search box** in [Command Palette settings](settings.md#layout-and-positioning). In compact mode, Command Palette opens as a smaller search box and expands when results or nested pages need more space. You can also turn on the [quick access shelf](#quick-access-shelf) to show your pinned and recent commands below the compact search box.

When compact mode shows only the search box, press <kbd>Down Arrow</kbd> or <kbd>Tab</kbd> to expand the palette and browse the top-level commands.

You can also choose Command Palette as the app that opens when you press the Copilot key. For more information, see [Use Command Palette with the Copilot key](settings.md#use-command-palette-with-the-copilot-key).

:::image type="content" source="../images/command-palette/cmdpal-overview1.gif" alt-text="An animated GIF of PowerToys Command Palette interface showing web search and git information features in action.":::

## What you can do

| Capability | How to use it |
| :--- | :--- |
| **Launch apps** | Start typing the name of any app and press <kbd>Enter</kbd> to launch it. |
| **Run commands** | Type `>` followed by a command (for example, `> cmd` or `> Shell:startup`). |
| **Search files and folders** | Navigate to `Search files` or type `file` followed by a space. |
| **Search the web** | Type `??` followed by your query to search with your default browser. |
| **Calculate** | Type a math expression like `23*47` or `sqrt(256)`, or type `=` followed by a space. The calculator supports the same operations as the [PowerToys Run Calculator plugin](../run.md#calculator-plugin), including `rand()`, `randi(x)`, `sign(x)`, `sgn(x)`, arbitrary-base logarithms with `logn(x, base)`, and n-th roots with `root(x, n)`. |
| **Navigate Windows Settings** | Type `$` followed by a settings page name (for example, `$ display`). This also includes Windows Update pages such as **Check for updates**, **Restart options**, **View optional updates**, and **View update history**. |
| **Find and install apps with WinGet** | Navigate to `Search WinGet` to discover and install apps. |
| **Switch windows** | Quickly switch between open windows without <kbd>Alt</kbd>+<kbd>Tab</kbd>. |
| **Access clipboard history** | Browse and paste from your recent clipboard entries. |
| **Look up time and date** | View the current time and date in various formats. |
| **Manage Windows Services** | Start, stop, or restart system services directly from the palette. |
| **Open Windows Terminal profiles** | Launch any configured Terminal profile or run commands in a specific profile. |
| **Query the Windows Registry** | Look up and manage Registry entries without opening regedit. |
| **Connect to Remote Desktop** | Start Remote Desktop sessions from discovered connections, or type any hostname directly into the list to connect on the fly. |
| **Monitor system performance** | Check CPU, memory, network usage, network speed, disk usage, GPU, and battery metrics at a glance, or pin individual metrics to the [Dock](dock.md) as standalone bands. |
| **Execute system commands** | Restart your computer, empty the Recycle Bin, and more. When Windows Update is waiting for a restart, Command Palette also surfaces **Update and restart** and **Update and shut down**. |
| **Access PowerToys modules** | Jump to other PowerToys utilities directly from Command Palette. |
| **Customize your look** | Personalize with themes, backdrop effects, background images, and custom colors in [Settings](settings.md). |

## Home page

The Home page is what you see when you first open Command Palette. It surfaces your most relevant commands, recent items, and a **Pinned commands** section that you control. Right-click any command in Command Palette and select **Pin to Home** to add it. From the Home page, use the right-click context menu to reorder your pinned commands or unpin them. Pinning to Home is independent of pinning to the Dock: use Home pins for quick access inside the palette and Dock pins for one-click access without opening the palette.

To show the commands you used most recently on the Home page, set **Recent items** > **Show on Home** to **After pinned** in [Command Palette settings](settings.md#general). To remove a command from the list, right-click it and select **Remove from recent**.

## Quick access shelf

When Command Palette opens as a compact search box, you can turn on the quick access shelf to show your pinned and recent commands directly below the search box. The shelf appears only while the palette is collapsed and hides when the palette expands to show results. To turn it on, right-click the search box and select **Show shelf**, or turn on **Quick access shelf** in [Command Palette settings](settings.md#layout-and-positioning).

The shelf shows the commands you pinned to Home, followed by recent commands if you turn on **Recent items** for the shelf. If not all items fit, the remaining items move to an overflow menu.

| Action | How to do it |
| :--- | :--- |
| Run a shelf item | Select the item, or press <kbd>Alt</kbd>+<kbd>1</kbd> through <kbd>Alt</kbd>+<kbd>9</kbd>. |
| Select a shelf item without running it | Press <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>1</kbd> through <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>9</kbd>. Then press <kbd>Ctrl</kbd>+<kbd>Enter</kbd> to run the item's secondary command, or press <kbd>Ctrl</kbd>+<kbd>K</kbd> to open its context menu. |
| Reorder pinned items | Drag a pinned item to a new position on the shelf. |
| Pin a recent item | Drag a recent item onto **Pin to home**. |
| Remove an item | Drag an item onto **Remove from quick access**. Removing a pinned item unpins it from Home, and removing a recent item removes it from your history. |

Right-click the shelf to open **Shelf settings** or to **Hide shelf**. If the shelf has nothing to show, it stays hidden. If you select **Show shelf** while the shelf is empty, Command Palette shows a tip that explains how to pin a command to Home or change which items appear in **Shelf settings**.

## Keyboard shortcuts in results

Use these shortcuts to work with results in Command Palette lists:

| Shortcut | Action |
| :--- | :--- |
| <kbd>Alt</kbd>+<kbd>1</kbd> through <kbd>Alt</kbd>+<kbd>9</kbd> | Run the corresponding visible list item, counted from the top. To select the item instead, change **Alt+number shortcuts in lists** in [Command Palette settings](settings.md#general). |
| <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>1</kbd> through <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>9</kbd> | Select the corresponding visible list item without running it. |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Enter</kbd> | Run the selected result as administrator. This shortcut works for supported results from Run, Windows Terminal, PowerToys, Bookmarks, and file search. For bookmarks and files, it applies to `.exe`, `.bat`, `.cmd`, `.msc`, and `.cpl` files. |

To share a command or start it from a script, right-click a top-level command and select **Copy command link**. For more information, see [Command links](settings.md#command-links).

## Bookmarks

Create Bookmarks for files, folders, URLs, or shell commands so you can launch them by name from Command Palette. Bookmarks can include placeholder values (for example, `{query}` in a URL). When you run a bookmark with placeholders, Command Palette prompts you for the values inline in the search experience instead of opening a separate dialog. You can also pin a Bookmark to the [Dock](dock.md) by dragging the underlying file or URL onto the Dock.

## Dock

Pin your most-used commands to a persistent toolbar on the edge of your screen. The [Dock](dock.md) gives you one-click access to frequently used commands and extensions without opening the full Command Palette. On multi-monitor setups, each display has its own Dock and its own set of pinned bands. The Dock can stay pinned or auto-hide until you hover over its edge.

For more information, see [Command Palette Dock](dock.md).

## Extensions

Command Palette is built to be extended. There's a large and growing ecosystem of extensions that add new commands, tools, and integrations to your workflow. Browse, install, update, and uninstall community extensions directly from the built-in **Extension Gallery** in Command Palette settings.

To learn more, see [Finding and installing extensions](finding-and-installing-extensions.md) and [Extension development overview](extensibility-overview.md).

## Related content

- [Command Palette Settings](settings.md)
- [Command Palette Dock](dock.md)
- [Finding and installing extensions](finding-and-installing-extensions.md)
- [Extension development overview](extensibility-overview.md)
- [Extension samples](samples.md)
