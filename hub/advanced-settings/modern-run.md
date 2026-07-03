---
title: Modern Run dialog
description: Enable the modern Run dialog in Windows Advanced settings, replacing the classic Win+R with a faster, Fluent Design interface.
ms.topic: how-to
ms.date: 07/02/2026
author: GrantMeStrength
ms.author: jken
---

# Modern Run dialog

The modern Run dialog replaces the classic Win+R experience with a redesigned interface built on Fluent Design. It supports dark mode, launches faster than the classic dialog, and adds new features like quick home directory navigation.

The modern Run dialog is built with WinUI 3 and C#, compiled with .NET AOT for native performance. It shares its codebase with the Command Palette (CmdPal) in PowerToys.

> [!IMPORTANT]
> Organizational policies can disable or hide Advanced settings controls. If a toggle is unavailable, contact your administrator. See related guidance in [Group Policy](../dev-drive/group-policy.md).

## Enable the modern Run dialog

1. Open **Settings** and go to **[System > Advanced](ms-settings:developers)**.
2. Toggle on **Run Dialog** at the top of the screen.
3. Press **Win+R** to open the new Run experience.

To switch back to the classic dialog, return to the same settings page and toggle the option off.

## Features

The modern Run dialog supports everything the classic dialog does — running commands, opening paths (local and network), and browsing command history — with these additions:

- **Fluent Design and dark mode**: The interface matches the Windows 11 visual style and respects your system theme.
- **Quick home directory access**: Type `~\` to navigate directly to your user directory, then continue navigating subdirectories just like you would from the command line.
- **Faster launch time**: The modern dialog has a median time-to-show of 94 ms, compared to 103 ms for the classic dialog ([source](https://devblogs.microsoft.com/commandline/the-new-run-dialog-faster-cleaner-and-more-capable/)).
- **Command history**: Press the up and down arrow keys to cycle through previously run commands, just like the classic dialog.

## Requirements

The modern Run dialog requires Windows 11 with the Advanced settings system component. It's currently available in Windows Insider builds on the Experimental Channel.

## Related content

- [Advanced settings overview](index.md)
- [File Explorer version control integration](fe-version-control.md)
- [The new Run dialog: faster, cleaner, and more capable (DevBlog)](https://devblogs.microsoft.com/commandline/the-new-run-dialog-faster-cleaner-and-more-capable/)
