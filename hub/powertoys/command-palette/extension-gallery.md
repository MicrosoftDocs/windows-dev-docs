---
title: Making your extension discoverable in Command Palette
description: Learn how to list your Command Palette extension in the Extension Gallery so users can discover it from within Command Palette settings.
ms.date: 04/10/2026
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to list my extension in the Command Palette Extension Gallery.
---

# Making your extension discoverable

The Extension Gallery is a curated directory of Command Palette extensions that users can browse directly from within Command Palette settings. Listing your extension in the Gallery makes it discoverable to all Command Palette users — they can see your extension's description, screenshots, and install it with a single click.

:::image type="content" source="../images/command-palette/gallery.png" alt-text="A screenshot of the Command Palette Extension Gallery showing a list of available extensions.":::

The Gallery itself doesn't host extensions. Instead, it links to your extension's install source — whether that's [WinGet](publish-extension-winget.md), the [Microsoft Store](publish-extension-store.md), or a direct download URL (such as a GitHub Releases page). When a user installs your extension from the Gallery, Command Palette uses the install source you specified.

## How to list your extension

The Extension Gallery is powered by the [microsoft/CmdPal-Extensions](https://github.com/microsoft/CmdPal-Extensions) repository on GitHub. The Contributing Guide covers the full submission process including folder structure, metadata format, and pull request requirements.

[Get started listing your extension in the Gallery](https://github.com/microsoft/CmdPal-Extensions/blob/main/docs/CONTRIBUTING.md)

Once listed, your extension's detail page is displayed directly in Command Palette, giving users a rich preview before they install.

:::image type="content" source="../images/command-palette/details.png" alt-text="A screenshot of an extension detail page in Command Palette showing the extension description, screenshots, and install button.":::

## Related content

- [Publishing overview](publish-extension.md)
- [Publish to WinGet](publish-extension-winget.md)
- [Publish to Microsoft Store](publish-extension-store.md)
- [Finding and installing extensions](finding-and-installing-extensions.md)
