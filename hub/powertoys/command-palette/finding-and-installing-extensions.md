---
title: Finding and installing Command Palette extensions
description: Learn how to discover, install, update, and uninstall extensions for PowerToys Command Palette using the built-in Gallery or community sources.
ms.date: 06/08/2026
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider, WinGet]
# Customer intent: Find and install extensions to add new functionality to Command Palette.
---

# Finding and installing extensions

Command Palette extensions add new commands, tools, and integrations to your Command Palette experience. You can discover and install extensions directly from the built-in Gallery, or find community-built extensions on GitHub.

## Browse the Gallery

The easiest way to find extensions is through the built-in **Gallery** in Command Palette settings.

1. Open Command Palette and select the **Settings** button.
2. In the sidebar, under **Extensions**, select **Gallery**.
3. Browse or search for extensions by name, author, or category.
4. Select an extension to view its details, screenshots, and description.
5. Select **Install** to add the extension to your Command Palette.

The Gallery pulls from a curated feed of extensions that have been reviewed and published. You can sort extensions by name, author, or installation status to find what you're looking for, and the Gallery uses cached data so it stays responsive when you reopen it.

Installs, updates, and uninstalls go through WinGet. Command Palette shows the live WinGet status and progress on the extension's gallery page, so you can see when an install is in flight, succeeded, or failed without leaving the palette. The Gallery only surfaces HTTP and HTTPS links (homepage, author, install, and metadata), so external links you open from the Gallery stay scoped to safe web destinations.

## Community extensions

The Command Palette extension ecosystem is open and growing. Community developers can build and share their own extensions. To discover community-built extensions, visit the [CmdPal-Extensions repository on GitHub](https://github.com/microsoft/CmdPal-Extensions).

If you've built an extension and want to share it with the community, you can submit it for inclusion in the Gallery from the same repository. For details, see [Making your extension discoverable](extension-gallery.md).

## Manage installed extensions

To view and manage your installed extensions:

1. Open Command Palette and select the **Settings** button.
2. In the sidebar, under **Extensions**, select **Installed**.

From here, you can:

- Enable or disable individual extensions.
- Configure per-extension settings (if the extension provides a settings page).
- Set custom aliases for extension commands.
- Assign global hotkeys to specific extension commands.

You can also update or uninstall any extension you installed through the Gallery directly from the Gallery page.

## Build your own

Want to create a custom extension? Command Palette extensions are built with .NET and support rich UI including lists, forms, markdown, detail views, and grid layouts. You can scaffold a new extension project directly from Command Palette by running the `Create extension` command.

For more information, see [Extension development overview](extensibility-overview.md) and [Extension samples](samples.md).

[!INCLUDE [install-powertoys.md](../../includes/install-powertoys.md)]

## Related content

- [Command Palette overview](overview.md)
- [Extension development overview](extensibility-overview.md)
- [Extension samples](samples.md)
