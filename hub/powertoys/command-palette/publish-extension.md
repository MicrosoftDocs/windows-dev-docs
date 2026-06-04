---
title: Publish Command Palette extensions
description: Learn about the different ways to publish and distribute your Command Palette extensions, including WinGet and the Microsoft Store.
ms.date: 06/04/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to understand my options for publishing a Command Palette extension.
---

# Publish your extension

Once your Command Palette extension is built and tested, you need to distribute it so users can install it. There are several ways to get your extension into the hands of users, each with different trade-offs.

:::image type="content" source="../images/command-palette/gallery.png" alt-text="A screenshot of the Command Palette Extension Gallery showing a list of available extensions.":::

## Distribution options

| Method | Automatic updates | Requirements |
| :--- | :--- | :--- |
| **WinGet** | ✅ Yes | WinGet manifest with `windows-commandpalette-extension` tag |
| **Microsoft Store** | ✅ Yes | Partner Center account, MSIX package |

### Microsoft Store (recommended)

Publishing to the [Microsoft Store](publish-extension-store.md) is the recommended distribution method. Registration is free for individual developers, and the Store provides automatic updates and broad reach across Windows devices.

[Get started publishing to the Microsoft Store](publish-extension-store.md)

### WinGet

Publishing to [WinGet](publish-extension-winget.md) makes your extension discoverable within Command Palette via the `Search WinGet` command. When your extension is listed on WinGet with the `windows-commandpalette-extension` tag, users can find and install it directly from within Command Palette. WinGet also handles updates automatically.

[Get started publishing to WinGet](publish-extension-winget.md)

## List your extension in the Gallery

Regardless of which channel you publish to, you can list your extension in the **Command Palette Extension Gallery** so users can discover it from within Command Palette. The Gallery is a curated directory — it links to your extension's install source (WinGet or the Microsoft Store) but doesn't host the extension itself.

For more information, see [Extension Gallery](extension-gallery.md).

[Get started listing your extension in the Gallery](extension-gallery.md)

## Related content

- [Publish to WinGet](publish-extension-winget.md)
- [Publish to Microsoft Store](publish-extension-store.md)
- [Extension Gallery](extension-gallery.md)
- [Getting started](creating-an-extension.md)